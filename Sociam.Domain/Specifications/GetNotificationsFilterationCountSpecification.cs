using Sociam.Domain.Entities;
using Sociam.Domain.Enums;
using Sociam.Domain.Utils;

namespace Sociam.Domain.Specifications;

public sealed class GetNotificationsFilterationCountSpecification(
    NotificationsSpecParams? @params,
    string currentUserId) : BaseSpecification<Notification>(n =>
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
         (((GroupNotification)n).GroupRole != null &&
          ((GroupNotification)n).GroupRole!.ToLower().Contains(@params.SearchTerm)))
    ) &&
    n.RecipientId == currentUserId &&
    (@params.IsRead == null ||
     (@params.IsRead == true && n.Status == NotificationStatus.Read) ||
     (@params.IsRead == false && n.Status == NotificationStatus.UnRead)) &&
    (@params.StartDate == null || DateOnly.FromDateTime(n.CreatedAt.UtcDateTime) >= @params.StartDate) &&
    (@params.EndDate == null || DateOnly.FromDateTime(n.CreatedAt.UtcDateTime) <= @params.EndDate));