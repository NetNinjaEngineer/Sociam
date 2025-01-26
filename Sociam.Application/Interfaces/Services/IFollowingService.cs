using Sociam.Application.Bases;

namespace Sociam.Application.Interfaces.Services;
public interface IFollowingService
{
    Task<Result<bool>> UnfollowUserAsync(string followerId, string followedId);

    Task<Result<bool>> FollowUserAsync(string userFollowerId, string userToFollowId);
}
