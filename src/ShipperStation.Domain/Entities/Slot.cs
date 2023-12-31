﻿using ShipperStation.Domain.Common;
using ShipperStation.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShipperStation.Domain.Entities;
public class Slot : BaseAuditableEntity<int>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public double Length { get; set; }
    public int Index { get; set; }
    public int NumberOfPackages { get; set; }

    [Column(TypeName = "nvarchar(24)")]
    public SlotStatus Status { get; set; }

    public int ShelfId { get; set; }
    public virtual Shelf Shelf { get; set; } = default!;

    public int SizeId { get; set; }
    public virtual Size Size { get; set; } = default!;
    public virtual ICollection<Package> Packages { get; set; } = new HashSet<Package>();

}
