using Sociam.Domain.Entities;

namespace Sociam.Domain.Specifications;

public sealed class GetUserActiveStoriesSpecification(string creatorId) : BaseSpecification<Story>(s =>
    string.IsNullOrEmpty(creatorId) || (s.UserId == creatorId && s.ExpiresAt > DateTimeOffset.UtcNow));