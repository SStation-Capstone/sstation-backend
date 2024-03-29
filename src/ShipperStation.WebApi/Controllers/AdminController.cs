﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShipperStation.Application.Common.Constants;
using ShipperStation.Application.Features.Stations.Commands;
using ShipperStation.Application.Features.Stations.Models;
using ShipperStation.Application.Features.Stations.Queries;
using ShipperStation.Application.Features.Users.Models;
using ShipperStation.Application.Features.UserStations.Commands;
using ShipperStation.Application.Features.UserStations.Queries;
using ShipperStation.Application.Models;
using ShipperStation.Shared.Pages;

namespace ShipperStation.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = Policies.Admin)]
public class AdminController(ISender sender) : ControllerBase
{
    #region Stations
    [HttpPost("stations")]
    public async Task<ActionResult<MessageResponse>> CreateStation(
       CreateStationCommand command,
       CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }

    [HttpGet("stations")]
    public async Task<ActionResult<PaginatedResponse<StationResponse>>> GetAllStations(
        [FromQuery] GetAllStationsQuery request,
        CancellationToken cancellationToken)
    {
        return await sender.Send(request, cancellationToken);
    }

    [HttpGet("stations/{id}")]
    public async Task<ActionResult<StationResponse>> GetStationByIdForAdmin(
        int id,
        CancellationToken cancellationToken)
    {
        return await sender.Send(new GetStationByIdForAdminQuery(id), cancellationToken);
    }

    [HttpDelete("stations/{id}")]
    public async Task<ActionResult<MessageResponse>> DeleteStationByAdmin(
        int id,
        CancellationToken cancellationToken)
    {
        return await sender.Send(new DeleteStationByAdminCommand(id), cancellationToken);
    }

    [HttpPut("stations/{id}")]
    public async Task<ActionResult<MessageResponse>> UpdateStationByAdmin(
        int id,
        UpdateStationByAdminCommand request,
        CancellationToken cancellationToken)
    {
        return await sender.Send(request with { Id = id }, cancellationToken);
    }

    #endregion

    #region Manager in stations

    [HttpGet("stations/{stationId}/managers")]
    public async Task<ActionResult<PaginatedResponse<UserResponse>>> GetManagersInStation(
        int stationId,
        [FromQuery] GetManagersInStationQuery request,
        CancellationToken cancellationToken)
    {
        return await sender.Send(request with { StationId = stationId }, cancellationToken);
    }

    [HttpGet("stations/{stationId}/managers/{managerId}")]
    public async Task<ActionResult<UserResponse>> GetManagerInStation(
        int stationId,
        Guid managerId,
        CancellationToken cancellationToken)
    {
        return await sender.Send(new GetManagerInStationQuery(managerId) with { StationId = stationId }, cancellationToken);
    }

    [HttpDelete("stations/{stationId}/managers/{managerId}")]
    public async Task<ActionResult<MessageResponse>> DeleteManagerInStation(
        int stationId,
        Guid managerId,
        CancellationToken cancellationToken)
    {
        return await sender.Send(new DeleteManagerInStationCommand(managerId) with { StationId = stationId }, cancellationToken);
    }

    [HttpPost("stations/{stationId}/managers")]
    public async Task<ActionResult<MessageResponse>> CreateManagerIntoStation(
       int stationId,
       CreateManagerIntoStationCommand command,
       CancellationToken cancellationToken)
    {
        return await sender.Send(command with { StationId = stationId }, cancellationToken);
    }

    #endregion

}
