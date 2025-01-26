using Sociam.Domain.Entities;

namespace Sociam.Domain.Specifications;
public sealed class GetExistedConversationBetweenSenderAndReceiverSpecification(
    string senderId,
    string receiverId) : BaseSpecification<PrivateConversation>(c =>
        c.SenderUserId == senderId &&
        c.ReceiverUserId == receiverId);
