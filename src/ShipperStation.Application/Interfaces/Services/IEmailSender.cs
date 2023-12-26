﻿namespace ShipperStation.Application.Interfaces.Services;
public interface IEmailSender
{
    Task SendEmailAsync(string email, string subject, string htmlMessage, CancellationToken cancellationToken = default);
}
