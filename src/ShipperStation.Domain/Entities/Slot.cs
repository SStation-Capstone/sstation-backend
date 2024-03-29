﻿using EntityFrameworkCore.Projectables;
using ShipperStation.Domain.Common;
using ShipperStation.Domain.Enums;

namespace ShipperStation.Domain.Entities;
public class Slot : BaseEntity<int>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int Index { get; set; }

    public double Width { get; set; }
    public double Height { get; set; }
    public double Length { get; set; }
    public double Volume { get; set; }

    [Projectable]
    public int NumberOfPackages => Packages.Where(_ => _.Status != PackageStatus.Completed).Count();

    public int RackId { get; set; }
    public virtual Rack Rack { get; set; } = default!;

    public virtual ICollection<Package> Packages { get; set; } = new HashSet<Package>();

}
