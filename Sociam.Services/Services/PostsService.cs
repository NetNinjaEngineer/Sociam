using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Sociam.Application.Bases;
using Sociam.Application.Extensions;
using Sociam.Application.Features.Posts.Commands.CreatePost;
using Sociam.Application.Hubs;
using Sociam.Application.Interfaces.Services;
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
        IFileService fileService) : IPostsService
    {
        public async Task<Result<Guid>> CreatePostAsync(CreatePostCommand command)
        {
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
    }
}
