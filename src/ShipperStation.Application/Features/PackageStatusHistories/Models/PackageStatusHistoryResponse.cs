﻿using ShipperStation.Application.Models;
using ShipperStation.Domain.Enums;

namespace ShipperStation.Application.Features.PackageStatusHistories.Models;
public sealed record PackageStatusHistoryResponse : BaseAuditableEntityResponse<int>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public PackageStatus Status { get; set; }
    public Guid PackageId { get; set; }

    //public PackageResponseOfStatusHistory Package { get; set; } = default!;
}
