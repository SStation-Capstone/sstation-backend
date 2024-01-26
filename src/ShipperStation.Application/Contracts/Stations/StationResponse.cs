﻿namespace ShipperStation.Application.Contracts.Stations;
public sealed record StationResponse : BaseAuditableEntityResponse<int>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string? ContactPhone { get; set; }
    public string Address { get; set; } = default!;
    public string? Latitude { get; set; }
    public string? Longitude { get; set; }
    // TODO: them fiel rack,slot,..
    public ICollection<StationImageResponse> StationImages { get; set; } = new HashSet<StationImageResponse>();
    public ICollection<StationPricingResponse> StationPricings { get; set; } = new HashSet<StationPricingResponse>();
}
