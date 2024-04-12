﻿using Hangfire;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Extensions;
using ShipperStation.Application.Features.PackageFeature.Commands;
using ShipperStation.Application.Features.PackageFeature.Events;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities;
using ShipperStation.Domain.Enums;

namespace ShipperStation.Application.Features.PackageFeature.Handlers;
internal sealed class PaymentPackageCommandHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService,
    IPublisher publisher) : IRequestHandler<PaymentPackageCommand, MessageResponse>
{
    private readonly IGenericRepository<Package> _packageRepository = unitOfWork.Repository<Package>();
    public async Task<MessageResponse> Handle(PaymentPackageCommand request, CancellationToken cancellationToken)
    {
        var userId = await currentUserService.FindCurrentUserIdAsync();

        var package = await _packageRepository
            .FindByAsync(_ =>
                _.Id == request.Id &&
                _.ReceiverId == userId,
            _ => _.Include(_ => _.Slot.Rack.Shelf.Zone.Station.Pricings)
                  .Include(_ => _.Receiver.Wallet)
                  .Include(_ => _.Sender.Wallet),
            cancellationToken: cancellationToken);

        if (package == null)
        {
            throw new NotFoundException(nameof(Package), request.Id);
        }

        if (package.Status != PackageStatus.Initialized)
        {
            throw new BadRequestException("Package is not ready to pay");
        }

        var pricingStation = package.Pricings
            .Where(_ => _.FromDate <= package.TotalDays && _.ToDate >= package.TotalDays)
            .FirstOrDefault();

        if (pricingStation == null)
        {
            throw new NotFoundException("Not found pricing to pay");
        }

        var serviceFee = PackageExtensions.CalculateServiceFee(package.Volume, package.TotalDays, pricingStation.Price);
        var priceCod = package.PriceCod;

        var totalPrice = priceCod + serviceFee;

        if (totalPrice != request.TotalPrice)
        {
            throw new BadRequestException("Total price is not correct");
        }

        if (package.Receiver.Wallet.Balance < totalPrice)
        {
            throw new BadRequestException("Not enough money to pay");
        }

        package.Status = PackageStatus.Paid;
        package.Receiver.Wallet.Balance -= totalPrice;

        if (package.IsCod)
        {
            package.Sender.Wallet.Balance += package.PriceCod;

            package.Sender.Transactions.Add(new Transaction
            {
                Description = "Receive cod for package",
                Amount = package.PriceCod,
                Type = TransactionType.Receive,
                Status = TransactionStatus.Completed,
                Method = TransactionMethod.Wallet,
            });

            var notifyPaymentPackageEvent = new SendNotifyPaymentPackageEvent() with
            {
                UserId = package.SenderId,
                PackageId = package.Id
            };
            BackgroundJob.Enqueue(() => publisher.Publish(notifyPaymentPackageEvent, cancellationToken));
        }

        package.PackageStatusHistories.Add(new PackageStatusHistory
        {
            Status = package.Status
        });

        package.Receiver.Transactions.Add(new Transaction
        {
            Description = "Payment for package",
            Amount = totalPrice,
            Type = TransactionType.Pay,
            Status = TransactionStatus.Completed,
            Method = TransactionMethod.Wallet,
        });

        package.ExprireReceiveGoods = DateTimeOffset.UtcNow.AddDays(1);

        package.Payments.Add(new Payment
        {
            ServiceFee = serviceFee,
            PriceCod = priceCod,
            Status = PaymentStatus.Success,
            TotalPrice = totalPrice,
            StationId = package.Slot.Rack.Shelf.Zone.StationId
        });

        package.IsCod = false;
        package.PriceCod = 0;

        package.Rack.Shelf.Zone.Station.Balance += serviceFee;

        await unitOfWork.CommitAsync(cancellationToken);

        BackgroundJob.Schedule<IPackageService>(_ => _.CheckReceivePackageAsync(package.Id), package.ExprireReceiveGoods.Value);
        return new MessageResponse("Payment Success");
    }
}
