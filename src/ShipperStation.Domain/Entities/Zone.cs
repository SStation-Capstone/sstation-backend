﻿using ShipperStation.Domain.Common;

namespace ShipperStation.Domain.Entities;
public class Zone : BaseEntity<int>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public int StationId { get; set; }
    public virtual Station Station { get; set; } = default!;
    public virtual ICollection<Shelf> Shelves { get; set; } = new HashSet<Shelf>();

}
