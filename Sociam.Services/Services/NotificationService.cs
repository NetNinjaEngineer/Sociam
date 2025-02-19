using AutoMapper;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Notification;
using Sociam.Application.Features.Notifications.Queries.GetNotification;
using Sociam.Application.Interfaces.Services;
using Sociam.Domain.Entities;
using Sociam.Domain.Interfaces;

namespace Sociam.Services.Services;

public sealed class NotificationService(
    IMapper mapper,
    IUnitOfWork unitOfWork) : INotificationService
{
    public async Task<Result<NotificationDto>> GetNotificationAsync(GetNotificationQuery query)
    {
        var notification = await unitOfWork.Repository<Notification>()!.GetByIdAsync(query.Id);
        throw new NotImplementedException();
    }
}