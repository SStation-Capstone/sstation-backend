﻿using MediatR;
using Microsoft.AspNetCore.Identity;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts;
using ShipperStation.Domain.Constants;
using ShipperStation.Domain.Entities.Identities;
using ShipperStation.Domain.Enums;

namespace ShipperStation.Application.Features.StoreManagers.Commands.CreateStoreManager;
internal sealed class CreateStoreManagerCommandHandler(UserManager<User> userManager) : IRequestHandler<CreateStoreManagerCommand, MessageResponse>
{
    public async Task<MessageResponse> Handle(CreateStoreManagerCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            UserName = request.UserName,
            FullName = request.FullName,
            Status = UserStatus.Active
        };

        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            throw new ValidationBadRequestException(result.Errors);
        }

        result = await userManager.AddToRolesAsync(user, new[] { Roles.StoreManager });

        if (!result.Succeeded)
        {
            throw new ValidationBadRequestException(result.Errors);
        }

        return new MessageResponse(Resource.StoreManagerCreatedSuccess);
    }
}
