﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShipperStation.Application.Common.Constants;
using ShipperStation.Application.Features.Managers.Commands;
using ShipperStation.Application.Features.Staffs.Queries;
using ShipperStation.Application.Features.Stations.Models;
using ShipperStation.Application.Models;
using ShipperStation.Shared.Pages;

namespace ShipperStation.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = Policies.StationManager)]
public class ManagersController(ISender sender) : ControllerBase
{
    [Authorize(Roles = Policies.Admin)]
    [HttpPost]
    public async Task<ActionResult<MessageResponse>> CreateStoreManager(
        CreateStoreManagerCommand request,
        CancellationToken cancellationToken)
    {
        return await sender.Send(request, cancellationToken);
    }

    [HttpGet("stations")]
    public async Task<ActionResult<PaginatedResponse<StationResponse>>> GetStationsByStoreManager(
        [FromQuery] GetStationsByStoreManagerQuery request,
        CancellationToken cancellationToken)
    {
        return await sender.Send(request, cancellationToken);
    }

}