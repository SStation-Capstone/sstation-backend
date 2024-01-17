﻿using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Contracts.Sizes;
using ShipperStation.Application.Interfaces.Repositories;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Sizes.Queries.GetSizeById;
internal sealed class GetSizeByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetSizeByIdQuery, SizeResponse>
{
    private readonly IGenericRepository<Size> _sizeRepository = unitOfWork.Repository<Size>();
    public async Task<SizeResponse> Handle(GetSizeByIdQuery request, CancellationToken cancellationToken)
    {
        var size = await _sizeRepository
            .FindByAsync<SizeResponse>(x => x.Id == request.Id, cancellationToken);

        if (size == null)
        {
            throw new NotFoundException(nameof(Size), request.Id);
        }

        return size;
    }
}
