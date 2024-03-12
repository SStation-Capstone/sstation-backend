﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShipperStation.Application.Common.Constants;
using ShipperStation.Application.Features.PackageFeature.Commands;
using ShipperStation.Application.Features.PackageFeature.Models;
using ShipperStation.Application.Features.PackageFeature.Queries;
using ShipperStation.Application.Models;
using ShipperStation.Shared.Pages;

namespace ShipperStation.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PackagesController(ISender sender) : ControllerBase
{
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<PaginatedResponse<PackageResponse>>> GetPackages(
        [FromQuery] GetPackagesQuery query,
        CancellationToken cancellationToken)
    {
        return await sender.Send(query, cancellationToken);
    }

    //[HttpGet("{id}")]
    //public async Task<ActionResult<PackageResponse>> GetPackageById(int id, CancellationToken cancellationToken)
    //{
    //    return await sender.Send(new GetPackageByIdQuery(id), cancellationToken);
    //}

    [HttpPost]
    [Authorize(Roles = Policies.StationManager_Or_Staff)]
    public async Task<ActionResult<MessageResponse>> CreatePackage(
        CreatePackageCommand command,
        CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }

    [HttpPost("{id}/payment")]
    public async Task<ActionResult<MessageResponse>> PaymentPackage(Guid id, CancellationToken cancellationToken)
    {
        return await sender.Send(new PaymentPackageCommand(id), cancellationToken);
    }

    //[HttpPut("{id}")]
    //public async Task<ActionResult<MessageResponse>> UpdatePackage(int id, UpdatePackageCommand command, CancellationToken cancellationToken)
    //{
    //    return await sender.Send(command with { Id = id }, cancellationToken);
    //}

    //[HttpDelete("{id}")]
    //public async Task<ActionResult<MessageResponse>> DeletePackage(int id, CancellationToken cancellationToken)
    //{
    //    return await sender.Send(new DeletePackageCommand(id), cancellationToken);
    //}
}