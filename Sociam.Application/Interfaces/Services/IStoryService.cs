using Sociam.Application.Bases;
using Sociam.Application.DTOs.Stories;
using Sociam.Application.Features.Stories.Commands.AddStoryComment;
using Sociam.Application.Features.Stories.Commands.AddStoryReaction;
using Sociam.Application.Features.Stories.Commands.ChangeStoryPrivacy;
using Sociam.Application.Features.Stories.Commands.CreateMediaStory;
using Sociam.Application.Features.Stories.Commands.CreateTextStory;
using Sociam.Application.Features.Stories.Commands.DeleteStory;
using Sociam.Application.Features.Stories.Commands.MarkAsViewed;
using Sociam.Application.Features.Stories.Queries.GetActiveFriendStories;
using Sociam.Application.Features.Stories.Queries.GetStoriesByParams;
using Sociam.Application.Features.Stories.Queries.GetStoryById;
using Sociam.Application.Features.Stories.Queries.GetStoryComments;
using Sociam.Application.Features.Stories.Queries.GetStoryReactions;
using Sociam.Application.Features.Stories.Queries.GetStoryViewers;
using Sociam.Application.Features.Stories.Queries.GetUserStories;
using Sociam.Application.Features.Stories.Queries.HasUnseenStories;
using Sociam.Application.Features.Stories.Queries.IsStoryViewed;
using Sociam.Domain.Interfaces.DataTransferObjects;
using Sociam.Domain.Utils;

namespace Sociam.Application.Interfaces.Services;
public interface IStoryService
{
    Task<Result<MediaStoryDto>> CreateMediaStoryAsync(CreateMediaStoryCommand command);
    Task<Result<TextStoryDto>> CreateTextStoryAsync(CreateTextStoryCommand command);
    Task<Result<IEnumerable<StoryDto>>> GetActiveFriendStoriesAsync(GetActiveFriendStoriesQuery query);
    Task<Result<IEnumerable<StoryDto>>> GetMyStoriesAsync();
    Task<Result<bool>> DeleteStoryAsync(DeleteStoryCommand command);
    Task<Result<bool>> MarkStoryAsViewedAsync(MarkStoryAsViewedCommand command);
    Task<Result<StoryDto>> GetStoryAsync(GetStoryByIdQuery query);
    Task<Result<bool>> HasUnseenStoriesAsync(HasUnseenStoriesQuery query);
    Task<Result<UserWithStoriesDto?>> GetActiveUserStoriesAsync(GetUserStoriesQuery query);
    Task<Result<bool>> IsStoryViewedAsync(IsStoryViewedQuery query);
    Task<Result<StoryViewsResponseDto?>> GetStoryViewsAsync(GetStoryViewersQuery query);
    Task<Result<List<StoryViewedDto>>> GetStoriesViewedByMeAsync();
    Task<Result<PagedResult<StoryViewsResponseDto>>> GetStoriesByParamsAsync(GetStoriesByParamsQuery query);
    Task<Result<PagedResult<StoryViewsResponseDto>>> GetExpiredStoriesAsync(StoryQueryParameters? parameters);
    Task<Result<PagedResult<StoryViewsResponseDto>>> GetStoryArchiveAsync(StoryQueryParameters? parameters);
    Task<Result<bool>> ReactToStoryAsync(AddStoryReactionCommand command);
    Task<Result<bool>> ChangeStoryPrivacy(ChangeStoryPrivacyCommand command);
    Task<Result<bool>> CommentToStoryAsync(AddStoryCommentCommand command);
    Task<Result<IEnumerable<StoryWithCommentsResponseDto>>> GetStoriesWithCommentsAsync();
    Task<Result<StoryWithCommentsResponseDto?>> GetStoryWithCommentsAsync(GetStoryCommentsQuery query);
    Task<Result<StoryWithReactionsResponseDto?>> GetStoryWithReactionsAsync(GetStoryReactionsQuery query);
}
