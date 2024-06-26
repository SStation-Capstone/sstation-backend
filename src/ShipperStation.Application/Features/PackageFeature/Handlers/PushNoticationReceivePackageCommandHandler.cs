﻿using Hangfire;
using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.PackageFeature.Commands;
using ShipperStation.Application.Features.PackageFeature.Events;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities;
using ShipperStation.Domain.Enums;
using System.Text.Json;

namespace ShipperStation.Application.Features.PackageFeature.Handlers;
internal sealed class PushNoticationReceivePackageCommandHandler(
    IUnitOfWork unitOfWork,
    IPublisher publisher) : IRequestHandler<PushNoticationReceivePackageCommand, MessageResponse>
{
    private readonly IGenericRepository<Package> _packageRepository = unitOfWork.Repository<Package>();
    public async Task<MessageResponse> Handle(PushNoticationReceivePackageCommand request, CancellationToken cancellationToken)
    {
        foreach (var item in request.Ids)
        {
            var package = await _packageRepository
                .FindByAsync(_ => _.Id == item, cancellationToken: cancellationToken);

            if (package == null)
            {
                throw new NotFoundException(nameof(Package), item);
            }

            var notify = new SendNotifyPackageEvent() with
            {
                UserId = package.ReceiverId,
                Type = NotificationType.PackageReceive,
                Data = JsonSerializer.Serialize(new
                {
                    Id = package.Id,
                    Entity = nameof(Package)
                })
            };
            BackgroundJob.Enqueue(() => publisher.Publish(notify, cancellationToken));

            package.NotificationCount += 1;
            await unitOfWork.CommitAsync(cancellationToken);
        }

        return new MessageResponse("Success");
    }
}
