using Sociam.Domain.Entities;

namespace Sociam.Domain.Specifications;

public sealed class GetNotificationSpecification : BaseSpecification<Notification>
{
    public GetNotificationSpecification(string currentUserId) : base(n => n.ActorId == currentUserId)
    {
        AddIncludes(notification => notification.Actor);
        AddIncludes(notification => notification.Recipient);
    }
}