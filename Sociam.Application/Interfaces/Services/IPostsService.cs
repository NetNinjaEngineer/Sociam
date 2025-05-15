using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Post;
using Sociam.Application.Features.Posts.Commands.AddReaction;
using Sociam.Application.Features.Posts.Commands.ChangePostPrivacy;
using Sociam.Application.Features.Posts.Commands.CreatePost;
using Sociam.Application.Features.Posts.Commands.DeletePost;
using Sociam.Application.Features.Posts.Commands.EditPost;
using Sociam.Application.Features.Posts.Commands.RemoveReaction;
using Sociam.Application.Features.Posts.Queries.GetPosts;
using Sociam.Domain.Interfaces.DataTransferObjects;

namespace Sociam.Application.Interfaces.Services;

public interface IPostsService
{
    Task<Result<Guid>> CreatePostAsync(CreatePostCommand command);
    Task<Result<Unit>> EditPostAsync(EditPostCommand command);
    Task<Result<Unit>> DeletePostAsync(DeletePostCommand command);
    Task<Result<PagedResult<PostDto>>> GetPostsAsync(GetPostsQuery query);
    Task<Result<bool>> AddReactionAsync(AddReactionCommand command);
    Task<Result<bool>> RemoveReactionAsync(RemoveReactionCommand command);
    Task<Result<bool>> ChangePostPrivacyAsync(ChangePostPrivacyCommand command);
}