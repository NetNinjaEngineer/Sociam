﻿using Microsoft.EntityFrameworkCore;
using Sociam.Domain.Entities;
using Sociam.Domain.Entities.Identity;
using Sociam.Domain.Enums;
using Sociam.Domain.Interfaces;

namespace Sociam.Infrastructure.Persistence.Repositories;
public sealed class FriendshipRepository(
    ApplicationDbContext context) :
    GenericRepository<Friendship>(context), IFriendshipRepository
{
    public async Task<Friendship?> GetFriendshipAsync(string user1Id, string user2Id)
        => await context.Friendships
            .AsNoTracking()
            .Include(f => f.Requester)
            .Include(f => f.Receiver)
            .FirstOrDefaultAsync(f => f.RequesterId == user1Id && f.ReceiverId == user2Id ||
                                      f.RequesterId == user2Id && f.ReceiverId == user1Id);

    public async Task<List<Friendship>> GetUserFriendshipsAsync(string userId)
        => await context.Friendships
            .AsNoTracking()
            .Include(f => f.Requester)
            .Include(f => f.Receiver)
            .Where(f =>
                (f.RequesterId == userId || f.ReceiverId == userId)
                && f.FriendshipStatus == FriendshipStatus.Accepted)
            .ToListAsync();

    public async Task<List<Friendship>> GetSentFriendRequestsAsync(string requesterId)
        => await context.Friendships
            .AsNoTracking()
            .Include(f => f.Requester)
            .Include(f => f.Receiver)
            .Where(f => f.RequesterId == requesterId &&
                        f.FriendshipStatus == FriendshipStatus.Pending)
            .ToListAsync();

    public async Task<List<Friendship>> GetReceivedFriendRequestsAsync(string receiverId)
        => await context.Friendships
            .AsNoTracking()
            .Include(f => f.Requester)
            .Include(f => f.Receiver)
            .Where(f => f.ReceiverId == receiverId &&
                        f.FriendshipStatus == FriendshipStatus.Pending)
            .ToListAsync();

    public async Task<bool> AreFriendsAsync(string user1Id, string user2Id)
        => await context.Friendships
            .AsNoTracking()
            .Include(f => f.Requester)
            .Include(f => f.Receiver)
            .AnyAsync(f => (f.RequesterId == user1Id && f.ReceiverId == user2Id ||
                            f.RequesterId == user2Id && f.ReceiverId == user1Id) &&
                      f.FriendshipStatus == FriendshipStatus.Accepted);

    public async Task<int> GetFriendsCountAsync(string userId)
        => await context.Friendships
            .AsNoTracking()
            .Include(f => f.Receiver)
            .Include(f => f.Requester)
            .Where(x => (x.RequesterId == userId || x.ReceiverId == userId) &&
                        x.FriendshipStatus == FriendshipStatus.Accepted)
            .Select(x => x.RequesterId == userId ? x.Receiver : x.Requester)
            .CountAsync();

    public async Task<List<Friendship>> GetAcceptedFriendshipsForUserAsync(string userId)
    {
        var acceptedFriendShips = await context.Friendships
            .AsNoTracking()
            .Include(f => f.Receiver)
            .Include(f => f.Requester)
            .Where(f => (f.ReceiverId == userId || f.RequesterId == userId) &&
                        f.FriendshipStatus == FriendshipStatus.Accepted)
            .ToListAsync();

        return acceptedFriendShips;

    }

    public async Task<List<ApplicationUser>> GetFriendsOfUserAsync(string userId)
    {
        var users = await context.Friendships
            .AsNoTracking()
            .Include(x => x.Requester)
            .Include(x => x.Receiver)
            .Where(x => (x.RequesterId == userId || x.ReceiverId == userId) &&
                        x.FriendshipStatus == FriendshipStatus.Accepted)
            .Select(x => x.RequesterId == userId ? x.Receiver : x.Requester)
            .ToListAsync();

        var friendships = await context.Friendships
            .AsNoTracking()
            .Include(friendship => friendship.Requester)
            .Include(friendship => friendship.Receiver)
            .Where(f =>
                (f.RequesterId == userId || f.ReceiverId == userId) &&
                f.FriendshipStatus == FriendshipStatus.Accepted)
            .ToListAsync();

        var friends = new List<ApplicationUser>();

        foreach (var friendship in friendships)
        {
            if (friendship.RequesterId == userId)
                friends.Add(friendship.Receiver);
            else if (friendship.ReceiverId == userId)
                friends.Add(friendship.Requester);
        }

        return friends;

    }

    public async Task<List<string>> GetFriendIdsForUserAsync(string userId)
    {
        var friends = await context.Friendships
            .AsNoTracking()
            .Include(x => x.Requester)
            .Include(x => x.Receiver)
            .Where(x => (x.RequesterId == userId || x.ReceiverId == userId) &&
                        x.FriendshipStatus == FriendshipStatus.Accepted)
            .Select(x => x.RequesterId == userId ? x.Receiver : x.Requester)
            .ToListAsync();

        return friends.Select(x => x.Id).ToList();
    }
}
