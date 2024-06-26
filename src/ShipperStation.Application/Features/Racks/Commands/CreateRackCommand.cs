﻿using MediatR;
using ShipperStation.Application.Models;

namespace ShipperStation.Application.Features.Racks.Commands;
public sealed record CreateRackCommand : IRequest<MessageResponse>
{
    public int ShelfId { get; set; }
}
