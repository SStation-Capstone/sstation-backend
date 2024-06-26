﻿using MediatR;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Zones.Models;
using ShipperStation.Application.Features.Zones.Queries;
using ShipperStation.Domain.Entities;
using ShipperStation.Shared.Pages;

namespace ShipperStation.Application.Features.Zones.Handlers;
internal sealed class GetZonesQueryHandler(
    IUnitOfWork unitOfWork) : IRequestHandler<GetZonesQuery, PaginatedResponse<ZoneResponse>>
{
    private readonly IGenericRepository<Zone> _zoneRepository = unitOfWork.Repository<Zone>();

    public async Task<PaginatedResponse<ZoneResponse>> Handle(GetZonesQuery request, CancellationToken cancellationToken)
    {
        var zones = await _zoneRepository
            .FindAsync<ZoneResponse>(
                request.PageIndex,
                request.PageSize,
                request.GetExpressions(),
                request.GetOrder(),
                cancellationToken);

        return await zones.ToPaginatedResponseAsync();
    }
}
