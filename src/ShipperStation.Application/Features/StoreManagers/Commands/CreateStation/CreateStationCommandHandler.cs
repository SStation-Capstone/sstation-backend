﻿using Mapster;
using MediatR;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts;
using ShipperStation.Application.Interfaces.Repositories;
using ShipperStation.Application.Interfaces.Services;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.StoreManagers.Commands.CreateStation;
internal sealed class CreateStationCommandHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService) : IRequestHandler<CreateStationCommand, MessageResponse>
{
    private readonly IGenericRepository<Station> _stationRepository = unitOfWork.Repository<Station>();
    private readonly IGenericRepository<Pricing> _pricingRepository = unitOfWork.Repository<Pricing>();

    public async Task<MessageResponse> Handle(CreateStationCommand request, CancellationToken cancellationToken)
    {
        var userId = await currentUserService.FindCurrentUserIdAsync();

        var station = request.Adapt<Station>();
        station.UserStations.Add(new UserStation
        {
            UserId = userId
        });

        var pricingDefault = await _pricingRepository.FindAsync(cancellationToken: cancellationToken);

        foreach (var pricing in pricingDefault)
        {
            station.StationPricings.Add(new StationPricing
            {
                PricingId = pricing.Id
            });
        }

        await _stationRepository.CreateAsync(station, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        return new MessageResponse(Resource.StationCreatedSuccess);
    }
}
