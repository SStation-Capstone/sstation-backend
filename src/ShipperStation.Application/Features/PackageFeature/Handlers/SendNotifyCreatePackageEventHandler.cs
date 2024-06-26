﻿using MediatR;
using ShipperStation.Application.Contracts.Services.Notifications;
using ShipperStation.Application.Features.PackageFeature.Events;
using ShipperStation.Application.Models.Notifications;
using ShipperStation.Domain.Entities;
using ShipperStation.Domain.Enums;
using System.Text.Json;

namespace ShipperStation.Application.Features.PackageFeature.Handlers;
internal sealed class SendNotifyCreatePackageEventHandler(INotifier notifier) : INotificationHandler<SendNotifyCreatePackageEvent>
{
    public async Task Handle(SendNotifyCreatePackageEvent notification, CancellationToken cancellationToken)
    {
        var notificationMessage = new NotificationRequest
        {
            Type = NotificationType.CustomerPackageCreated,
            UserId = notification.ReceiverId,
            Data = JsonSerializer.Serialize(new
            {
                Id = notification.PackageId,
                Entity = nameof(Package)
            })
        };
        await notifier.NotifyAsync(notificationMessage, true, cancellationToken);
    }
}
