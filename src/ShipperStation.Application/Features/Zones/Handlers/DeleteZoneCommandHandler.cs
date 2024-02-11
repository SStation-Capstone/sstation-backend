﻿using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Features.Zones.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Zones.Handlers;
internal sealed class DeleteZoneCommandHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService) : IRequestHandler<DeleteZoneCommand, MessageResponse>
{
    private readonly IGenericRepository<Zone> _zoneRepository = unitOfWork.Repository<Zone>();
    public async Task<MessageResponse> Handle(DeleteZoneCommand request, CancellationToken cancellationToken)
    {
        var userId = await currentUserService.FindCurrentUserIdAsync();

        var zone = await _zoneRepository.FindByAsync(
            _ => _.Id == request.Id &&
                 _.StationId == request.StationId &&
                 _.Station.UserStations.Any(_ => _.UserId == userId),
            cancellationToken: cancellationToken);

        if (zone is null)
        {
            throw new NotFoundException(nameof(Zone), request.Id);
        }

        await _zoneRepository.DeleteAsync(zone);
        await unitOfWork.CommitAsync(cancellationToken);
        return new MessageResponse(Resource.DeletedSuccess);
    }
}
