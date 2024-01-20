﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShipperStation.Infrastructure.Persistence.Data;

#nullable disable

namespace ShipperStation.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240120040702_Remove-Delivery")]
    partial class RemoveDelivery
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ShipperStation.Domain.Entities.Device", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("ShipperStation.Domain.Entities.Identities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("Roles", (string)null);
                });

            modelBuilder.Entity("ShipperStation.Domain.Entities.Identities.RoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims", (string)null);
                });

            modelBuilder.Entity("ShipperStation.Domain.Entities.Identities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("AvatarUrl")
                        .HasColumnType("longtext");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("FullName")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset?>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("ShipperStation.Domain.Entities.Identities.UserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims", (string)null);
                });

            modelBuilder.Entity("ShipperStation.Domain.Entities.Identities.UserLogin", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins", (string)null);
                });

            modelBuilder.Entity("ShipperStation.Domain.Entities.Identities.UserRole", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("char(36)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", (string)null);
                });

            modelBuilder.Entity("ShipperStation.Domain.Entities.Identities.UserToken", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens", (string)null);
                });

            modelBuilder.Entity("ShipperStation.Domain.Entities.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("longtext");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasColumnType("nvarchar(24)");

                    b.Property<DateTimeOffset?>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTimeOffset?>("ReadAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(24)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("ShipperStation.Domain.Entities.Package", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Barcode")
                        .HasColumnType("longtext");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<double>("Height")
                        .HasColumnType("double");

                    b.Property<double>("Length")
                        .HasColumnType("double");

                    b.Property<DateTimeOffset?>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<double>("PackagePrice")
                        .HasColumnType("double");

                    b.Property<string>("ReceiverName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ReceiverPhone")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("SenderName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("SenderPhone")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("SlotId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(24)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<double>("Volume")
                        .HasColumnType("double");

                    b.Property<double>("Weight")
                        .HasColumnType("double");

                    b.Property<double>("Width")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("SlotId");

                    b.HasIndex("UserId");

                    b.ToTable("Packages");
                });

            modelBuilder.Entity("ShipperStation.Domain.Entities.PackageImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("PackageId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("PackageId");

                    b.ToTable("PackageImages");
                });

            modelBuilder.Entity("ShipperStation.Domain.Entities.PackageStatusHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<DateTimeOffset?>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<Guid>("PackageId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(24)");

                    b.HasKey("Id");

                    b.HasIndex("PackageId");

                    b.ToTable("PackageStatusHistories");
                });

            modelBuilder.Entity("ShipperStation.Domain.Entities.Rack", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<int>("SizeId")
                        .HasColumnType("int");

                    b.Property<int>("StationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SizeId");

                    b.HasIndex("StationId");

                    b.ToTable("Racks");
                });

            modelBuilder.Entity("ShipperStation.Domain.Entities.Setting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("nvarchar(24)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(24)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("ShipperStation.Domain.Entities.Shelf", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<int>("RackId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RackId");

                    b.ToTable("Shelves");
                });

            modelBuilder.Entity("ShipperStation.Domain.Entities.Size", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<double>("Height")
                        .HasColumnType("double");

                    b.Property<double>("Length")
                        .HasColumnType("double");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<double>("Volume")
                        .HasColumnType("double");

                    b.Property<double>("Width")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.ToTable("Sizes");
                });

            modelBuilder.Entity("ShipperStation.Domain.Entities.Slot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<int>("NumberOfPackages")
                        .HasColumnType("int");

                    b.Property<int>("ShelfId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ShelfId");

                    b.ToTable("Slots");
                });

            modelBuilder.Entity("ShipperStation.Domain.Entities.Station", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ContactPhone")
                        .HasColumnType("longtext");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Latitude")
                        .HasColumnType("longtext");

                    b.Property<string>("Longitude")
                        .HasColumnType("longtext");

                    b.Property<DateTimeOffset?>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Stations");
                });

            modelBuilder.Entity("ShipperStation.Domain.Entities.StationImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("StationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StationId");

                    b.ToTable("StationImages");
                });

            modelBuilder.Entity("ShipperStation.Domain.Entities.StationSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CustomValue")
                        .HasColumnType("longtext");

                    b.Property<int>("SettingId")
                        .HasColumnType("int");

                    b.Property<int>("StationId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(24)");

                    b.HasKey("Id");

                    b.HasIndex("SettingId");

                    b.HasIndex("StationId");

                    b.ToTable("StationSettings");
                });

            modelBuilder.Entity("ShipperStation.Domain.Entities.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("Amount")
                        .HasColumnType("double");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Method")
                        .IsRequired()
                        .HasColumnType("nvarchar(24)");

                    b.Property<DateTimeOffset?>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(24)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(24)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("ShipperStation.Domain.Entities.UserStation", b =>
                {
                    b.Property<int>("StationId")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("StationId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserStations");
                });

            modelBuilder.Entity("ShipperStation.Domain.Entities.Wallet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("Balance")
                        .HasColumnType("double");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTimeOffset?>("LastDepositAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset?>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("ShipperStation.Domain.Entities.Device", b =>
                {
                    b.HasOne("ShipperStation.Domain.Entities.Identities.User", "User")
                        .WithMany("Devices")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ShipperStation.Domain.Entities.Identities.RoleClaim", b =>
                {
                    b.HasOne("ShipperStation.Domain.Entities.Identities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ShipperStation.Domain.Entities.Identities.UserClaim", b =>
                {
                    b.HasOne("ShipperStation.Domain.Entities.Identities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ShipperStation.Domain.Entities.Identities.UserLogin", b =>
                {
                    b.HasOne("ShipperStation.Domain.Entities.Identities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ShipperStation.Domain.Entities.Identities.UserRole", b =>
                {
                    b.HasOne("ShipperStation.Domain.Entities.Identities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShipperStation.Domain.Entities.Identities.User", null)
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("ShipperStation.Domain.Entities.Identities.UserToken", b =>
                {
                    b.HasOne("ShipperStation.Domain.Entities.Identities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ShipperStation.Domain.Entities.Notification", b =>
                {
                    b.HasOne("ShipperStation.Domain.Entities.Identities.User", "User")
                        .WithMany("Notifications")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ShipperStation.Domain.Entities.Package", b =>
                {
                    b.HasOne("ShipperStation.Domain.Entities.Slot", "Slot")
                        .WithMany("Packages")
                        .HasForeignKey("SlotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShipperStation.Domain.Entities.Identities.User", "User")
                        .WithMany("Packages")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Slot");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ShipperStation.Domain.Entities.PackageImage", b =>
                {
                    b.HasOne("ShipperStation.Domain.Entities.Package", "Package")
                        .WithMany("PackageImages")
                        .HasForeignKey("PackageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Package");
                });

            modelBuilder.Entity("ShipperStation.Domain.Entities.PackageStatusHistory", b =>
                {
                    b.HasOne("ShipperStation.Domain.Entities.Package", "Package")
                        .WithMany("PackageStatusHistories")
                        .HasForeignKey("PackageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Package");
                });

            modelBuilder.Entity("ShipperStation.Domain.Entities.Rack", b =>
                {
                    b.HasOne("ShipperStation.Domain.Entities.Size", "Size")
                        .WithMany("Racks")
                        .HasForeignKey("SizeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShipperStation.Domain.Entities.Station", "Station")
                        .WithMany("Racks")
                        .HasForeignKey("StationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Size");

                    b.Navigation("Station");
                });

            modelBuilder.Entity("ShipperStation.Domain.Entities.Shelf", b =>
                {
                    b.HasOne("ShipperStation.Domain.Entities.Rack", "Rack")
                        .WithMany("Shelves")
                        .HasForeignKey("RackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rack");
                });

            modelBuilder.Entity("ShipperStation.Domain.Entities.Slot", b =>
                {
                    b.HasOne("ShipperStation.Domain.Entities.Shelf", "Shelf")
                        .WithMany("Slots")
                        .HasForeignKey("ShelfId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Shelf");
                });

            modelBuilder.Entity("ShipperStation.Domain.Entities.StationImage", b =>
                {
                    b.HasOne("ShipperStation.Domain.Entities.Station", "Station")
                        .WithMany("StationImages")
                        .HasForeignKey("StationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Station");
                });

            modelBuilder.Entity("ShipperStation.Domain.Entities.StationSetting", b =>
                {
                    b.HasOne("ShipperStation.Domain.Entities.Setting", "Setting")
                        .WithMany("StationSettings")
                        .HasForeignKey("SettingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShipperStation.Domain.Entities.Station", "Station")
                        .WithMany("StationSettings")
                        .HasForeignKey("StationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Setting");

                    b.Navigation("Station");
                });

            modelBuilder.Entity("ShipperStation.Domain.Entities.Transaction", b =>
                {
                    b.HasOne("ShipperStation.Domain.Entities.Identities.User", "User")
                        .WithMany("Transactions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ShipperStation.Domain.Entities.UserStation", b =>
                {
                    b.HasOne("ShipperStation.Domain.Entities.Station", "Station")
                        .WithMany("UserStations")
                        .HasForeignKey("StationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShipperStation.Domain.Entities.Identities.User", "User")
                        .WithMany("UserStations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Station");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ShipperStation.Domain.Entities.Wallet", b =>
                {
                    b.HasOne("ShipperStation.Domain.Entities.Identities.User", "User")
                        .WithOne("Wallet")
                        .HasForeignKey("ShipperStation.Domain.Entities.Wallet", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ShipperStation.Domain.Entities.Identities.User", b =>
                {
                    b.Navigation("Devices");

                    b.Navigation("Notifications");

                    b.Navigation("Packages");

                    b.Navigation("Transactions");

                    b.Navigation("UserRoles");

                    b.Navigation("UserStations");

                    b.Navigation("Wallet")
                        .IsRequired();
                });

            modelBuilder.Entity("ShipperStation.Domain.Entities.Package", b =>
                {
                    b.Navigation("PackageImages");

                    b.Navigation("PackageStatusHistories");
                });

            modelBuilder.Entity("ShipperStation.Domain.Entities.Rack", b =>
                {
                    b.Navigation("Shelves");
                });

            modelBuilder.Entity("ShipperStation.Domain.Entities.Setting", b =>
                {
                    b.Navigation("StationSettings");
                });

            modelBuilder.Entity("ShipperStation.Domain.Entities.Shelf", b =>
                {
                    b.Navigation("Slots");
                });

            modelBuilder.Entity("ShipperStation.Domain.Entities.Size", b =>
                {
                    b.Navigation("Racks");
                });

            modelBuilder.Entity("ShipperStation.Domain.Entities.Slot", b =>
                {
                    b.Navigation("Packages");
                });

            modelBuilder.Entity("ShipperStation.Domain.Entities.Station", b =>
                {
                    b.Navigation("Racks");

                    b.Navigation("StationImages");

                    b.Navigation("StationSettings");

                    b.Navigation("UserStations");
                });
#pragma warning restore 612, 618
        }
    }
}
