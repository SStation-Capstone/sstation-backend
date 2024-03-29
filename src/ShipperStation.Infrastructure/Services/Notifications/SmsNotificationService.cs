﻿using Microsoft.Extensions.Logging;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Contracts.Services.Notifications;
using ShipperStation.Application.Models.Notifications;

namespace ShipperStation.Infrastructure.Services.Notifications;
public class SmsNotificationService(
    ISmsSender smsSender,
    ILogger<SmsNotificationService> logger) : ISmsNotificationService
{
    public async Task NotifyAsync(NotificationRequest notification, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(notification.PhoneNumber) || string.IsNullOrWhiteSpace(notification.Data))
        {
            logger.LogError($"[SMS NOTIFICATION] PhoneNumber: '{notification.PhoneNumber}' - Otp '{notification.Data}'");
            return;
        }
        logger.LogInformation($"[SMS NOTIFICATION] start send phoneNumber {notification.PhoneNumber}");
        await smsSender.SendAsync(notification.PhoneNumber, notification.Content, cancellationToken);
    }
}
