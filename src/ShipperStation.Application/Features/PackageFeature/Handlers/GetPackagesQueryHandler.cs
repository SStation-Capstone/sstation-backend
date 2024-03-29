﻿using MediatR;
using ShipperStation.Application.Common.Enums;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.PackageFeature.Models;
using ShipperStation.Application.Features.PackageFeature.Queries;
using ShipperStation.Domain.Entities;
using ShipperStation.Shared.Pages;

namespace ShipperStation.Application.Features.PackageFeature.Handlers;
//TODO: không validate package có phải trong station không( lười làm )
internal sealed class GetPackagesQueryHandler(
    IUnitOfWork unitOfWork) : IRequestHandler<GetPackagesQuery, PaginatedResponse<PackageResponse>>
{
    private readonly IGenericRepository<Package> _packageRepository = unitOfWork.Repository<Package>();
    public async Task<PaginatedResponse<PackageResponse>> Handle(GetPackagesQuery request, CancellationToken cancellationToken)
    {
        request = request with
        {
            SortDir = SortDirection.Desc,
            SortColumn = nameof(Package.CreatedAt)
        };

        var packages = await _packageRepository
            .FindAsync<PackageResponse>(
                request.PageIndex,
                request.PageSize,
                request.GetExpressions(),
                request.GetOrder(),
                cancellationToken);

        return await packages.ToPaginatedResponseAsync();
    }
}
