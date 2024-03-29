﻿using MediatR;
using ShipperStation.Application.Models;

namespace ShipperStation.Application.Features.Managers.Commands;
public sealed record CreateStoreManagerCommand : IRequest<MessageResponse>
{
    public string UserName { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string? FullName { get; set; }
    public string? AvatarUrl { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }

    public bool IsActive => true;

}
