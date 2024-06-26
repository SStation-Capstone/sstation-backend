﻿using MediatR;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Stations.Models;
using ShipperStation.Application.Features.Stations.Queries;
using ShipperStation.Domain.Entities;
using ShipperStation.Shared.Pages;

namespace ShipperStation.Application.Features.Stations.Handlers;
internal sealed class GetAllStationsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllStationsQuery, PaginatedResponse<StationAllResponse>>
{
    private readonly IGenericRepository<Station> _stationRepository = unitOfWork.Repository<Station>();
    public async Task<PaginatedResponse<StationAllResponse>> Handle(GetAllStationsQuery request, CancellationToken cancellationToken)
    {
        var stations = await _stationRepository
            .FindAsync<StationAllResponse>(
                request.PageIndex,
                request.PageSize,
                request.GetExpressions(),
                request.GetOrder(),
                cancellationToken);

        return await stations.ToPaginatedResponseAsync();
    }
}
