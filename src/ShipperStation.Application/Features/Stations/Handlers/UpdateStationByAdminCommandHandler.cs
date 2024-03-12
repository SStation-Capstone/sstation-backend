﻿using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Stations.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Stations.Handlers;
internal sealed class UpdateStationByAdminCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateStationByAdminCommand, MessageResponse>
{
    private readonly IGenericRepository<Station> _stationRepository = unitOfWork.Repository<Station>();
    public async Task<MessageResponse> Handle(UpdateStationByAdminCommand request, CancellationToken cancellationToken)
    {
        var station = await _stationRepository
            .FindByAsync(x =>
                x.Id == request.Id,
                _ => _.Include(_ => _.StationImages),
                cancellationToken);

        if (station == null)
        {
            throw new NotFoundException(nameof(Station), request.Id);
        }

        request.Adapt(station);
        await unitOfWork.CommitAsync(cancellationToken);
        return new MessageResponse(Resource.UpdatedSuccess);
    }
}
