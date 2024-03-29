﻿using MediatR;
using Microsoft.AspNetCore.Identity;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Features.Users.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities.Identities;

namespace ShipperStation.Application.Features.Users.Handlers;
internal sealed class ResetPasswordRequestHandler(
    ICurrentUserService currentUserService,
    UserManager<User> userManager) : IRequestHandler<ResetPasswordRequest, MessageResponse>
{
    public async Task<MessageResponse> Handle(ResetPasswordRequest request, CancellationToken cancellationToken)
    {
        var user = await currentUserService.FindCurrentUserAsync();

        var result = await userManager.RemovePasswordAsync(user);

        if (!result.Succeeded)
        {
            throw new ValidationBadRequestException(result.Errors);
        }

        result = await userManager.AddPasswordAsync(user, request.NewPassword);

        if (!result.Succeeded)
        {
            throw new ValidationBadRequestException(result.Errors);
        }

        return new MessageResponse(Resource.PasswordResetSuccess);
    }
}
