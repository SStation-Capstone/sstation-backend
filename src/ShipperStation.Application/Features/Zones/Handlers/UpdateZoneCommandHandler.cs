﻿using Mapster;
using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Zones.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Zones.Handlers;
internal sealed class UpdateZoneCommandHandler(
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateZoneCommand, MessageResponse>
{

    private readonly IGenericRepository<Zone> _zoneRepository = unitOfWork.Repository<Zone>();
    public async Task<MessageResponse> Handle(UpdateZoneCommand request, CancellationToken cancellationToken)
    {
        var zone = await _zoneRepository.FindByAsync(
            _ => _.Id == request.Id &&
                 _.StationId == request.StationId,
            cancellationToken: cancellationToken);

        if (zone is null)
        {
            throw new NotFoundException(nameof(Zone), request.Id);
        }

        if (await _zoneRepository.ExistsByAsync(_ => _.Id != request.Id && _.StationId == request.StationId && _.Name == request.Name, cancellationToken))
        {
            throw new ConflictException(nameof(Zone), request.Name);
        }

        request.Adapt(zone);
        await unitOfWork.CommitAsync(cancellationToken);

        return new MessageResponse(Resource.UpdatedSuccess);
    }
}
