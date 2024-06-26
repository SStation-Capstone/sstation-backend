﻿using Hangfire;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Extensions;
using ShipperStation.Application.Features.PackageFeature.Commands;
using ShipperStation.Application.Features.PackageFeature.Events;
using ShipperStation.Application.Features.PackageFeature.Models;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities;
using ShipperStation.Domain.Enums;
using System.Text.Json;

namespace ShipperStation.Application.Features.PackageFeature.Handlers;
internal sealed class PaymentPackageCommandHandler(
    IUnitOfWork unitOfWork,
    IPublisher publisher,
    ICacheService cacheService) : IRequestHandler<PaymentPackageCommand, MessageResponse>
{
    private readonly IGenericRepository<Package> _packageRepository = unitOfWork.Repository<Package>();
    public async Task<MessageResponse> Handle(PaymentPackageCommand request, CancellationToken cancellationToken)
    {
        var package = await _packageRepository
            .FindByAsync(_ => _.Id == request.Id,
            _ => _.Include(_ => _.Rack.Shelf.Zone.Station.Pricings)
                  .Include(_ => _.Receiver.Wallet),
            cancellationToken: cancellationToken);

        if (package == null)
        {
            throw new NotFoundException(nameof(Package), request.Id);
        }

        if (package.Status != PackageStatus.Initialized)
        {
            throw new BadRequestException("Package is not ready to pay");
        }

        var serviceFee = 1000.0;

        var pricingStation = package.Pricings
            .Where(_ => _.StartTime <= package.TotalDays && _.EndTime >= package.TotalDays)
            .FirstOrDefault() ?? package.Pricings.FirstOrDefault();

        if (pricingStation != null)
        {
            serviceFee = PackageExtensions.CalculateServiceFee(package.Volume, package.TotalDays, pricingStation.Price);
        }

        var totalPrice = serviceFee;

        if (totalPrice != request.TotalPrice)
        {
            throw new BadRequestException("Total price is not correct");
        }

        if (!request.IsCash)
        {
            if (package.Receiver.Wallet.Balance < totalPrice)
            {
                throw new BadRequestException("Not enough money to pay");
            }

            package.Receiver.Wallet.Balance -= totalPrice;

        }

        package.Status = PackageStatus.Paid;

        var transaction = new Transaction
        {
            Description = "Payment for package",
            Amount = totalPrice,
            Type = TransactionType.Pay,
            Status = TransactionStatus.Completed,
            Method = TransactionMethod.Wallet,
        };

        package.Receiver.Transactions.Add(transaction);

        package.PackageStatusHistories.Add(new PackageStatusHistory
        {
            Status = package.Status,
            Name = package.Status.ToString(),
            Description = $"Package '{package.Name}' is paid"
        });

        package.Payments.Add(new Payment
        {
            Description = $"Payment for package '{package.Name}' success",
            ServiceFee = serviceFee,
            Status = PaymentStatus.Success,
            TotalPrice = totalPrice,
            StationId = package.Rack.Shelf.Zone.StationId,
            Type = request.IsCash ? PaymentType.Cash : PaymentType.Wallet,
            Transaction = transaction
        });

        package.Rack.Shelf.Zone.Station.Balance += serviceFee;

        await unitOfWork.CommitAsync(cancellationToken);

        var staffGenQr = await cacheService
            .GetAsync<InfoStaffGennerateQrPaymentModel>(package.Id.ToString(), cancellationToken);

        if (staffGenQr != null)
        {
            var notify = new SendNotifyPackageEvent() with
            {
                UserId = staffGenQr.StaffId,
                Type = NotificationType.NotiPackagePaymentSuccessForStaff,
                Data = JsonSerializer.Serialize(new
                {
                    Id = package.Id,
                    Entity = nameof(Package)
                })
            };
            BackgroundJob.Enqueue(() => publisher.Publish(notify, cancellationToken));

            await cacheService.RemoveAsync(package.Id.ToString(), cancellationToken);
        }

        return new MessageResponse("Payment Success");
    }
}
