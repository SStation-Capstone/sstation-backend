﻿using Microsoft.AspNetCore.Identity;
using ShipperStation.Domain.Common.Interfaces;
using ShipperStation.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShipperStation.Domain.Entities;
public class User : IdentityUser<Guid>, IAuditableEntity
{
    public string? FullName { get; set; }
    public string? AvatarUrl { get; set; }

    [Column(TypeName = "nvarchar(24)")]
    public UserStatus Status { get; set; }

    public string? CreatedBy { get; set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTimeOffset? ModifiedAt { get; set; }
    public string? DeletedBy { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }

    public virtual Wallet Wallet { get; set; } = default!;
    public virtual ICollection<Notification> Notifications { get; set; } = new HashSet<Notification>();
    public virtual ICollection<Transaction> Transactions { get; set; } = new HashSet<Transaction>();
    public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
}
