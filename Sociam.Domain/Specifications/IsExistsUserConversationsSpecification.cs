using Sociam.Domain.Entities;

namespace Sociam.Domain.Specifications;
public sealed class IsExistsUserConversationsSpecification(string userId) : BaseSpecification<PrivateConversation>(c =>
    string.IsNullOrEmpty(userId) || c.SenderUserId == userId || c.ReceiverUserId == userId);
