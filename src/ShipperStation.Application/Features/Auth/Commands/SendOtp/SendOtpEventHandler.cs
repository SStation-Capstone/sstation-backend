﻿using MediatR;
using ShipperStation.Application.Contracts.Notifications;
using ShipperStation.Application.Interfaces.Services.Notifications;
using ShipperStation.Domain.Enums;

namespace ShipperStation.Application.Features.Auth.Commands.SendOtp;
internal sealed class SendOtpEventHandler(INotifier notifier) : INotificationHandler<SendOtpEvent>
{
    public async Task Handle(SendOtpEvent notification, CancellationToken cancellationToken)
    {
        var notificationMessage = new NotificationRequest
        {
            PhoneNumber = notification.PhoneNumber,
            Data = notification.Otp,
            Type = NotificationType.VerificationCode,
        };

        await notifier.NotifyAsync(notificationMessage, false, cancellationToken);
    }
}
