﻿using Mapster;
using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Pricings.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Pricings.Handlers;
internal sealed class CreatePricingCommandHandler(
    IUnitOfWork unitOfWork) : IRequestHandler<CreatePricingCommand, MessageResponse>
{
    private readonly IGenericRepository<Pricing> _pricingRepository = unitOfWork.Repository<Pricing>();
    private readonly IGenericRepository<Station> _stationRepository = unitOfWork.Repository<Station>();
    public async Task<MessageResponse> Handle(CreatePricingCommand request, CancellationToken cancellationToken)
    {
        if (!await _stationRepository
            .ExistsByAsync(_ =>
                _.Id == request.StationId,
            cancellationToken))
        {
            throw new NotFoundException(nameof(Station), request.StationId);
        }

        var pricing = request.Adapt<Pricing>();
        await _pricingRepository.CreateAsync(pricing, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        return new MessageResponse(Resource.CreatedSuccess);
    }
}
