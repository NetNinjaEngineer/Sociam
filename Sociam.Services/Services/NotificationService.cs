using AutoMapper;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Notification;
using Sociam.Application.Features.Notifications.Queries.GetNotification;
using Sociam.Application.Interfaces.Services;
using Sociam.Domain.Entities;
using Sociam.Domain.Interfaces;
using Sociam.Domain.Specifications;
using System.Net;

namespace Sociam.Services.Services;

public sealed class NotificationService(
    IMapper mapper,
    IUnitOfWork unitOfWork,
    ICurrentUser currentUser) : INotificationService
{
    public async Task<Result<NotificationDto>> GetNotificationAsync(GetNotificationQuery query)
    {
        var notification = await unitOfWork.Repository<Notification>()!
            .GetBySpecificationAndIdAsync(
                specification: new GetNotificationSpecification(currentUser.Id), id: query.Id);

        if (notification == null)
            return Result<NotificationDto>.Failure(HttpStatusCode.NotFound);

        var mappedNotification = mapper.Map<NotificationDto>(notification);

        return Result<NotificationDto>.Success(mappedNotification);
    }
}