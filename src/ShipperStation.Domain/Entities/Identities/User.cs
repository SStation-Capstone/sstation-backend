﻿using EntityFrameworkCore.Projectables;
using Microsoft.AspNetCore.Identity;
using ShipperStation.Domain.Common.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShipperStation.Domain.Entities.Identities;
public class User : IdentityUser<Guid>, IAuditableEntity
{
    public string? FullName { get; set; }
    public string? AvatarUrl { get; set; }

    public bool IsActive { get; set; }

    public string? CreatedBy { get; set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTimeOffset? ModifiedAt { get; set; }
    public string? DeletedBy { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    [Projectable]
    public bool IsDeleted => DeletedAt != null;

    public virtual Wallet Wallet { get; set; } = default!;

    public virtual ICollection<UserStation> UserStations { get; set; } = new HashSet<UserStation>();
    public virtual ICollection<Notification> Notifications { get; set; } = new HashSet<Notification>();
    public virtual ICollection<Transaction> Transactions { get; set; } = new HashSet<Transaction>();

    public virtual ICollection<Package> Packages { get; set; } = new HashSet<Package>();

    public virtual ICollection<Device> Devices { get; set; } = new HashSet<Device>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();

    [Projectable]
    [NotMapped]
    public IEnumerable<Role> Roles => UserRoles.Select(ur => ur.Role);

    [Projectable]
    [NotMapped]
    public IEnumerable<Station> Stations => UserStations.Select(us => us.Station);

}
