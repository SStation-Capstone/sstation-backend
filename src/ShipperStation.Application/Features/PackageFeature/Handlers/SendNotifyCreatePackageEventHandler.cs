﻿using MediatR;
using ShipperStation.Application.Contracts.Services.Notifications;
using ShipperStation.Application.Features.PackageFeature.Events;
using ShipperStation.Application.Models.Notifications;
using ShipperStation.Domain.Enums;

namespace ShipperStation.Application.Features.PackageFeature.Handlers;
internal sealed class SendNotifyCreatePackageEventHandler(INotifier notifier) : INotificationHandler<SendNotifyCreatePackageEvent>
{
    public async Task Handle(SendNotifyCreatePackageEvent notification, CancellationToken cancellationToken)
    {
        var notificationMessage = new NotificationRequest
        {
            Type = NotificationType.CustomerPackageCreated,
            UserId = notification.ReceiverId,
        };
        await notifier.NotifyAsync(notificationMessage, true, cancellationToken);

        notificationMessage.UserId = notification.SenderId;
        await notifier.NotifyAsync(notificationMessage, true, cancellationToken);
    }
}
