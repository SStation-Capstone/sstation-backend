﻿using MediatR;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Wallets.Events;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Wallets.Handlers;
internal sealed class InitWalletEventHandler(IUnitOfWork unitOfWork) : INotificationHandler<InitWalletEvent>
{
    private readonly IGenericRepository<Wallet> _walletRepository = unitOfWork.Repository<Wallet>();

    public async Task Handle(InitWalletEvent notification, CancellationToken cancellationToken)
    {
        if (!await _walletRepository.ExistsByAsync(_ => _.UserId == notification.UserId, cancellationToken))
        {
            await _walletRepository.CreateAsync(new Wallet
            {
                UserId = notification.UserId,
                Balance = 0,
            }, cancellationToken);

            await unitOfWork.CommitAsync();
        }
    }
}
