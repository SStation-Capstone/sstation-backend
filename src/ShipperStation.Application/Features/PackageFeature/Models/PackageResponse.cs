﻿using ShipperStation.Application.Extensions;
using ShipperStation.Application.Features.PackageStatusHistories.Models;
using ShipperStation.Application.Features.Payments.Models;
using ShipperStation.Application.Features.Pricings.Models;
using ShipperStation.Application.Features.Racks.Models;
using ShipperStation.Application.Features.Shelfs.Models;
using ShipperStation.Application.Features.Stations.Models;
using ShipperStation.Application.Features.Users.Models;
using ShipperStation.Application.Features.Zones.Models;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Enums;
using ShipperStation.Shared.Extensions;
using System.Text.Json.Serialization;

namespace ShipperStation.Application.Features.PackageFeature.Models;
public sealed record PackageResponse : BaseAuditableEntityResponse<Guid>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public PackageStatus Status { get; set; }
    public double Weight { get; set; }
    public double Height { get; set; }
    public double Width { get; set; }
    public double Length { get; set; }
    public double Volume { get; set; }

    public int NotificationCount { get; set; }
    public string? Reason { get; set; }

    public ZoneResponse Zone { get; set; } = default!;

    public RackCreatePackageResponse Rack { get; set; } = default!;

    public ShelfCreatePackageResponse Shelf { get; set; } = default!;

    public Guid ReceiverId { get; set; }
    public UserResponse Receiver { get; set; } = default!;

    public string Location { get; set; } = default!;

    public double TotalDays { get; set; }
    public double TotalPrice => ServiceFee;

    public double ServiceFee => Pricing is null ? 1000 : PackageExtensions.CalculateServiceFee(Volume, TotalDays, Pricing.Price);

    public string FormatTotalPrice => TotalPrice.FormatMoney();
    public string FormatServiceFee => ServiceFee.FormatMoney();

    public PricingResponse? Pricing => Pricings.Where(_ => _.StartTime <= TotalDays && _.EndTime >= TotalDays).FirstOrDefault() ?? Pricings.FirstOrDefault();

    [JsonIgnore]
    public ICollection<PricingResponse> Pricings { get; set; } = new HashSet<PricingResponse>();

    public ICollection<PackageImageResponse> PackageImages { get; set; } = new HashSet<PackageImageResponse>();

    public ICollection<PackageStatusHistoryResponse> PackageStatusHistories { get; set; } = new HashSet<PackageStatusHistoryResponse>();

    public StationResponse Station { get; set; } = default!;

    public ICollection<PaymentInfoPackageResponse> Payments { get; set; } = new HashSet<PaymentInfoPackageResponse>();
}
