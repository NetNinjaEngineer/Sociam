using System.Net;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Sociam.Application.Authorization.Helpers;
using Sociam.Application.Bases;
using Sociam.Application.Extensions;
using Sociam.Application.Features.Posts.Commands.CreatePost;
using Sociam.Application.Features.Posts.Commands.EditPost;
using Sociam.Application.Helpers;
using Sociam.Application.Hubs;
using Sociam.Application.Interfaces.Services;
using Sociam.Application.Interfaces.Services.Models;
using Sociam.Domain.Entities;
using Sociam.Domain.Entities.Identity;
using Sociam.Domain.Enums;
using Sociam.Domain.Interfaces;

namespace Sociam.Services.Services
{
    public sealed class PostsService(
        ICurrentUser currentUser,
        IUnitOfWork unitOfWork,
        UserManager<ApplicationUser> userManager,
        IHubContext<PostsHub> hubContext,
        IFileService fileService,
        IAuthorizationService authorizationService,
        ILogger<PostsService> logger) : IPostsService
    {
        public async Task<Result<Guid>> CreatePostAsync(CreatePostCommand command)
        {
            var validator = new CreatePostCommandValidator();
            await validator.ValidateAndThrowAsync(command, CancellationToken.None);

            var mappedPost = new Post
            {
                Id = Guid.NewGuid(),
                Text = command.Text,
                Privacy = command.Privacy,
                Location = command.Location != null ? new PostLocation()
                {
                    City = command.Location.City,
                    Country = command.Location.Country,
                    Latitude = command.Location.Latitude,
                    Longitude = command.Location.Longitude
                } : null,
                CreatedById = currentUser.Id,
                Feeling = command.Feeling
            };

            IEnumerable<ApplicationUser> taggedUsers = [];

            if (command.TaggedUserIds != null && command.TaggedUserIds.Count > 0)
            {
                taggedUsers = await userManager.GetTaggedUsersAsync(command.TaggedUserIds);
                foreach (var user in taggedUsers)
                {
                    // check if the tagged user is friend to the current post creator
                    var isTaggedUserFriend = await unitOfWork.FriendshipRepository.AreFriendsAsync(
                        currentUser.Id, user.Id);
                    if (!isTaggedUserFriend)
                        return Result<Guid>.Failure(HttpStatusCode.BadRequest, "Can not tag anyone that not a friend with you!!!!");

                    var postTag = new PostTag
                    {
                        Id = Guid.NewGuid(),
                        PostId = mappedPost.Id,
                        TaggedUserId = user.Id,
                    };
                    mappedPost.Tags.Add(postTag);
                }
            }

            if (command.Media != null && command.Media.Count > 0)
            {
                var cloudinaryUploadResult = await fileService.CloudinaryUploadMultipleFilesAsync(command.Media);
                if (cloudinaryUploadResult.IsFailure)
                    return Result<Guid>.Failure(HttpStatusCode.BadRequest, "Failed to upload media files", cloudinaryUploadResult.Errors);

                foreach (var mediaUploadResult in cloudinaryUploadResult.Value)
                {
                    var postMedia = new PostMedia
                    {
                        Id = Guid.NewGuid(),
                        PostId = mappedPost.Id,
                        Url = mediaUploadResult.Url,
                        MediaType = Enum.Parse<PostMediaType>(mediaUploadResult.Type.ToString()),
                        AssetId = mediaUploadResult.AssetId,
                        PublicId = mediaUploadResult.PublicId
                    };
                    mappedPost.Media.Add(postMedia);
                }

            }

            unitOfWork.Repository<Post>()?.Create(mappedPost);

            // Now that post is created, send notifications based on privacy settings
            // send notifications to tagged users if the post privacy is public or friends
            if (
                taggedUsers.Any() &&
                mappedPost.Privacy == PostPrivacy.Public &&
                mappedPost.Privacy == PostPrivacy.Friends)
            {
                foreach (var user in taggedUsers)
                {
                    var postNotification = new PostNotification
                    {
                        Id = Guid.NewGuid(),
                        CreatedAt = DateTime.UtcNow,
                        RecipientId = user.Id,
                        ActorId = currentUser.Id,
                        PostContent = mappedPost.Text,
                        Message = $"{currentUser.FullName} tagged you in a post",
                        ActionUrl = $"/posts/{mappedPost.Id}",
                        PostId = mappedPost.Id,
                        Status = NotificationStatus.UnRead,
                        Type = NotificationType.TaggedInPost,
                    };

                    unitOfWork.NotificationRepository.Create(postNotification);

                    await hubContext.Clients.User(user.Id).SendAsync("ReceiveTaggingNotification", postNotification);

                }
            }

            var creatorFriends = await unitOfWork.FriendshipRepository.GetFriendsOfUserAsync(currentUser.Id);
            foreach (var friend in creatorFriends.Select(f => f.Id).ToList())
            {
                var createdPostNotification = new PostNotification
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    RecipientId = friend,
                    ActorId = currentUser.Id,
                    PostContent = mappedPost.Text,
                    Message = $"{currentUser.FullName} created a new post",
                    ActionUrl = $"/posts/{mappedPost.Id}",
                    PostId = mappedPost.Id,
                    Status = NotificationStatus.UnRead,
                    Type = NotificationType.NewPostCreated
                };

                unitOfWork.NotificationRepository.Create(createdPostNotification);
                await hubContext.Clients.User(friend).SendAsync("ReceiveNewPostNotification", createdPostNotification);

            }

            await unitOfWork.SaveChangesAsync();


            return Result<Guid>.Success(mappedPost.Id, "Post created successfully");
        }

        public async Task<Result<Unit>> EditPostAsync(EditPostCommand command)
        {
            var validator = new EditPostCommandValidator();
            await validator.ValidateAndThrowAsync(command, CancellationToken.None);

            var existedPost = await unitOfWork.Repository<Post>()!.GetByIdAsync(command.PostId);

            if (existedPost == null)
                return Result<Unit>.Failure(HttpStatusCode.NotFound);

            var authorizationResult = await authorizationService.AuthorizeAsync(
                user: currentUser.GetUser()!,
                resource: existedPost,
                policyName: PostPolicies.EditPost);

            if (!authorizationResult.Succeeded)
            {
                logger.LogWarning("Authorization failed for user {UserId} on post {PostId}. Reasons: {Reasons}",
                   currentUser.Id,
                   command.PostId,
                   string.Join(", ", authorizationResult.Failure?.FailureReasons.Select(r => r.Message) ?? ["Unknown reason"]));

                return Result<Unit>.Failure(HttpStatusCode.Forbidden, DomainErrors.Posts.Forbiden);
            }

            // Check if the post is a shared/reposted post
            if (existedPost.OriginalPostId != null && command.Media != null && command.Media.Any())
                return Result<Unit>.Failure(HttpStatusCode.Forbidden, "Media cannot be added to shared posts.");


            if (!string.IsNullOrEmpty(command.Content))
                existedPost.Text = command.Content;

            if (command.PostLocation != null)
                existedPost.Location = new PostLocation
                {
                    City = command.PostLocation.City,
                    Country = command.PostLocation.Country,
                    Latitude = command.PostLocation.Latitude,
                    Longitude = command.PostLocation.Longitude
                };

            existedPost.Privacy = command.PostPrivacy;
            existedPost.Feeling = command.PostFeeling;
            existedPost.UpdatedAt = DateTimeOffset.UtcNow;

            // Update media if its not a shared post
            if (command.Media != null && command.Media.Any())
            {
                var errors = new List<string>();

                foreach (var mediaFile in existedPost.Media)
                {
                    var mediaFileType = GetMediaFileType(mediaFile.MediaType);
                    var deletionResult = await fileService.DeleteCloudinaryResourceAsync(mediaFile.PublicId, mediaFileType);
                    if (deletionResult.IsFailure)
                    {
                        // Log the error
                        logger.LogError("Error deleting resource with PublicId {PublicId}: {Error}", mediaFile.PublicId, deletionResult.Message);
                        errors.Add($"Failed to delete resource with PublicId {mediaFile.PublicId}: {deletionResult.Message}");
                    }
                }

                if (errors.Count != 0)
                {
                    var errorSummary = string.Join("; ", errors);
                    logger.LogError("Some errors occurred while deleting media resources: {ErrorSummary}", errorSummary);
                }

                existedPost.Media.Clear();

                var cloudinaryUploadResult = await fileService.CloudinaryUploadMultipleFilesAsync(command.Media);
                if (cloudinaryUploadResult.IsFailure)
                    return Result<Unit>.Failure(HttpStatusCode.BadRequest, "Failed to upload media files, try later !!!", cloudinaryUploadResult.Errors);

                foreach (var mediaUploadResult in cloudinaryUploadResult.Value)
                {
                    var postMedia = new PostMedia
                    {
                        Id = Guid.NewGuid(),
                        PostId = existedPost.Id,
                        Url = mediaUploadResult.Url,
                        MediaType = Enum.Parse<PostMediaType>(mediaUploadResult.Type.ToString()),
                        AssetId = mediaUploadResult.AssetId,
                        PublicId = mediaUploadResult.PublicId
                    };

                    existedPost.Media.Add(postMedia);
                }
            }


            unitOfWork.Repository<Post>()?.Update(existedPost);

            await unitOfWork.SaveChangesAsync();

            return Result<Unit>.Success(HttpStatusCode.NoContent);
        }

        private static FileType GetMediaFileType(PostMediaType mediaType)
        {
            if (mediaType == PostMediaType.Audio)
                return FileType.Audio;
            else if (mediaType == PostMediaType.Video)
                return FileType.Video;
            else if (mediaType == PostMediaType.Image)
                return FileType.Image;
            else if (mediaType == PostMediaType.Link)
                return FileType.Link;
            else if (mediaType == PostMediaType.Document)
                return FileType.Document;
            else if (mediaType == PostMediaType.Text)
                return FileType.Text;
            else
                return FileType.Pdf;
        }
    }
}