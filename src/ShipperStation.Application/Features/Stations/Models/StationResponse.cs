﻿using ShipperStation.Application.Features.Pricings.Models;
using ShipperStation.Application.Models;
using ShipperStation.Shared.Extensions;

namespace ShipperStation.Application.Features.Stations.Models;
public sealed record StationResponse : BaseAuditableEntityResponse<int>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string? ContactPhone { get; set; }
    public string Address { get; set; } = default!;
    public string? Latitude { get; set; }
    public string? Longitude { get; set; }

    public double Balance { get; set; }

    public string FormatBalance => Balance.FormatMoney();
    // TODO: them fiel rack,slot,..
    public ICollection<StationImageResponse> StationImages { get; set; } = new HashSet<StationImageResponse>();
    public ICollection<PricingResponse> Pricings { get; set; } = new HashSet<PricingResponse>();
    //public ICollection<UserResponse> Users { get; set; } = new HashSet<UserResponse>();

    //public ICollection<PaymentResponse> Payments { get; set; } = new HashSet<PaymentResponse>();
}
