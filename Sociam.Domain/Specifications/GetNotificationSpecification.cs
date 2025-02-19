using Sociam.Domain.Entities;
using Sociam.Domain.Enums;
using Sociam.Domain.Utils;

namespace Sociam.Domain.Specifications;

public sealed class GetNotificationSpecification : BaseSpecification<Notification>
{
    public GetNotificationSpecification(string currentUserId) : base(n => n.ActorId == currentUserId)
    {
        AddIncludes(notification => notification.Actor);
        AddIncludes(notification => notification.Recipient);
    }

    public GetNotificationSpecification(
        NotificationsSpecParams? @params,
        string currentUserId) : base(n =>
        @params != null &&
        (string.IsNullOrEmpty(@params.SearchTerm) ||
         n.Message.ToLower().Contains(@params.SearchTerm) ||
         (
             n is PostNotification &&
             ((PostNotification)n).PostContent != null &&
             ((PostNotification)n).PostContent!.ToLower().Contains(@params.SearchTerm)) ||

         (
             n is GroupNotification &&
             ((GroupNotification)n).GroupName.ToLower().Contains(@params.SearchTerm) &&
             (((GroupNotification)n).GroupRole != null && ((GroupNotification)n).GroupRole!.ToLower().Contains(@params.SearchTerm)))

         ) &&
        n.RecipientId == currentUserId &&
        (@params.IsRead == null ||
         (@params.IsRead == true && n.Status == NotificationStatus.Read) ||
         (@params.IsRead == false && n.Status == NotificationStatus.UnRead)) &&

        (@params.StartDate == null || DateOnly.FromDateTime(n.CreatedAt.UtcDateTime) >= @params.StartDate) &&

        (@params.EndDate == null || DateOnly.FromDateTime(n.CreatedAt.UtcDateTime) <= @params.EndDate))
    {
        AddIncludes(notification => notification.Actor);
        AddIncludes(notification => notification.Recipient);

        if (@params is null) return;

        if (string.IsNullOrEmpty(@params.Sort))
        {
            switch (@params.Sort)
            {
                case "DateCreatedAsc":
                    AddOrderBy(n => n.CreatedAt);
                    break;
                case "DateCreatedDesc":
                    AddOrderByDescending(n => n.CreatedAt);
                    break;

                case "DateReadAsc":
                    AddOrderBy(n => n.ReadAt ?? DateTimeOffset.MaxValue);
                    break;

                case "DateReadDesc":
                    AddOrderByDescending(n => n.ReadAt ?? DateTimeOffset.MinValue);
                    break;
                default:
                    AddOrderByDescending(n => n.CreatedAt);
                    break;
            }
        }

        ApplyPaging(@params.Page, @params.PageSize);
    }
}