using AutoMapper;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Notification;
using Sociam.Application.Features.Notifications.Commands.DeleteOne;
using Sociam.Application.Features.Notifications.Commands.MarkAsRead;
using Sociam.Application.Features.Notifications.Queries.GetNotification;
using Sociam.Application.Interfaces.Services;
using Sociam.Domain.Entities;
using Sociam.Domain.Interfaces;
using Sociam.Domain.Interfaces.DataTransferObjects;
using Sociam.Domain.Specifications;
using Sociam.Domain.Utils;
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

        var mappedNotification = mapper.Map<NotificationDto>(notification,
            options => options.Items["TimeZoneId"] = currentUser.TimeZoneId);

        return Result<NotificationDto>.Success(mappedNotification);
    }

    public async Task<Result<PagedResult<NotificationDto>>> GetNotificationsAsync(NotificationsSpecParams? @params)
    {
        var notifications = await unitOfWork.Repository<Notification>()!
        .GetAllWithSpecificationAsync(
                specification: new GetNotificationSpecification(@params, currentUser.Id));

        var mappedNotifications = mapper.Map<IEnumerable<NotificationDto>>(notifications,
            options => options.Items["TimeZoneId"] = currentUser.TimeZoneId);

        var specification = new GetNotificationsFilterationCountSpecification(@params, currentUser.Id);
        var totalCount = await unitOfWork.NotificationRepository.GetCountWithSpecificationAsync(specification);

        return Result<PagedResult<NotificationDto>>.Success(
            new PagedResult<NotificationDto>()
            {
                Page = @params?.Page,
                PageSize = @params?.PageSize,
                TotalCount = totalCount,
                Items = mappedNotifications.ToList()
            });
    }

    public async Task<Result<long>> GetUnReadNotificationsCountAsync()
        => Result<long>.Success(await unitOfWork.NotificationRepository.GetUnReadNotificationsCountAsync(currentUser.Id));

    public async Task<Result<long>> GetReadNotificationsCountAsync()
        => Result<long>.Success(await unitOfWork.NotificationRepository.GetReadNotificationsCountAsync(currentUser.Id));

    public async Task<Result<bool>> MarkAllAsReadAsync()
        => Result<bool>.Success(await unitOfWork.NotificationRepository.MarkAllAsReadAsync(currentUser.Id));

    public async Task<Result<bool>> MarkAsReadAsync(MarkAsReadCommand command)
    {
        var existedNotification = await unitOfWork.NotificationRepository.GetByIdAsync(command.NotificationId);

        if (existedNotification == null)
            return Result<bool>.Failure(HttpStatusCode.NotFound);

        return Result<bool>.Success(
            await unitOfWork.NotificationRepository.MarkAsReadAsync(currentUser.Id, command.NotificationId));
    }

    public async Task<Result<bool>> DeleteNotificationAsync(DeleteOneCommand command)
    {
        var existedNotification = await unitOfWork.NotificationRepository.GetByIdAsync(command.NotificationId);

        if (existedNotification == null)
            return Result<bool>.Failure(HttpStatusCode.NotFound);

        return Result<bool>.Success(
            await unitOfWork.NotificationRepository.DeleteOneAsync(currentUser.Id, command.NotificationId));
    }

    public async Task<Result<bool>> DeleteAllNotificationsAsync()
        => Result<bool>.Success(await unitOfWork.NotificationRepository.DeleteAllAsync(currentUser.Id));
}