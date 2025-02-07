using Sociam.Domain.Entities;

namespace Sociam.Application.Interfaces.Services;

public interface INotificationUrlGenerator
{
    string GenerateUrl(Notification notification);
}