﻿// <auto-generated />
using System;
using Logiware.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Logiware.Infrastructure.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20250131172942_Initials")]
    partial class Initials
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Logiware.Domain.Models.Entities.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ItemCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Items");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Category = "category1",
                            CreatedAt = new DateTime(2025, 1, 31, 17, 29, 41, 944, DateTimeKind.Utc).AddTicks(4995),
                            Description = "this is a test item1",
                            ItemCode = "ITM-1083",
                            Name = "item1"
                        });
                });

            modelBuilder.Entity("Logiware.Domain.Models.Entities.ItemHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("OwnershipId")
                        .HasColumnType("integer");

                    b.Property<string>("Remarks")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OwnershipId");

                    b.ToTable("ItemHistories");
                });

            modelBuilder.Entity("Logiware.Domain.Models.Entities.Ownership", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("ItemId")
                        .HasColumnType("integer");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<int>("SiteId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("SiteId");

                    b.ToTable("Ownerships");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2025, 1, 31, 17, 29, 41, 944, DateTimeKind.Utc).AddTicks(5052),
                            ItemId = 1,
                            Quantity = 1000,
                            SiteId = 1
                        });
                });

            modelBuilder.Entity("Logiware.Domain.Models.Entities.Personnel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("SiteId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SiteId");

                    b.ToTable("Personnels");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Code = "I1ILJU",
                            CreatedAt = new DateTime(2025, 1, 31, 17, 29, 41, 944, DateTimeKind.Utc).AddTicks(5158),
                            FirstName = "Zeke",
                            LastName = "canares",
                            Role = "Driver",
                            SiteId = 1
                        },
                        new
                        {
                            Id = 2,
                            Code = "FECLVB",
                            CreatedAt = new DateTime(2025, 1, 31, 17, 29, 41, 944, DateTimeKind.Utc).AddTicks(5180),
                            FirstName = "Miko",
                            LastName = "canares",
                            Role = "Manager",
                            SiteId = 1
                        });
                });

            modelBuilder.Entity("Logiware.Domain.Models.Entities.Shipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("AuthorizedById")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("DestinationSiteId")
                        .HasColumnType("integer");

                    b.Property<int?>("DriverId")
                        .HasColumnType("integer");

                    b.Property<string>("ShipmentCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ShipmentDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("SiteId")
                        .HasColumnType("integer");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("StatusUpdate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("AuthorizedById");

                    b.HasIndex("DestinationSiteId");

                    b.HasIndex("DriverId");

                    b.HasIndex("SiteId");

                    b.ToTable("Shipments");
                });

            modelBuilder.Entity("Logiware.Domain.Models.Entities.ShipmentItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("OwnershipId")
                        .HasColumnType("integer");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<int>("ShipmentId")
                        .HasColumnType("integer");

                    b.Property<string>("ShipmentItemCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OwnershipId");

                    b.HasIndex("ShipmentId");

                    b.ToTable("ShipmentItems");
                });

            modelBuilder.Entity("Logiware.Domain.Models.Entities.ShipmentReceive", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("QuantityMissing")
                        .HasColumnType("integer");

                    b.Property<int>("QuantityReceived")
                        .HasColumnType("integer");

                    b.Property<int>("ShipmentItemId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ShipmentItemId");

                    b.ToTable("ShipmentReceives");
                });

            modelBuilder.Entity("Logiware.Domain.Models.Entities.Site", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Sites");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2025, 1, 31, 17, 29, 41, 944, DateTimeKind.Utc).AddTicks(4300),
                            Description = "naawan site",
                            Location = "naawan market",
                            Name = "naawan"
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2025, 1, 31, 17, 29, 41, 944, DateTimeKind.Utc).AddTicks(4309),
                            Description = "initao site",
                            Location = "initao market",
                            Name = "initao"
                        });
                });

            modelBuilder.Entity("Logiware.Domain.Models.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<int?>("SiteId")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("SiteId")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2025, 1, 31, 17, 29, 41, 944, DateTimeKind.Utc).AddTicks(6350),
                            Email = "admin@admin.com",
                            PasswordHash = new byte[] { 206, 218, 21, 250, 128, 237, 225, 180, 42, 63, 160, 0, 98, 235, 109, 35, 31, 64, 141, 201, 247, 160, 37, 108, 117, 29, 47, 142, 191, 148, 122, 212, 131, 213, 189, 26, 223, 40, 107, 217, 224, 220, 79, 250, 167, 90, 155, 126, 83, 0, 165, 72, 32, 24, 178, 179, 148, 75, 182, 10, 207, 69, 4, 0 },
                            PasswordSalt = new byte[] { 29, 168, 167, 79, 10, 246, 45, 176, 54, 155, 233, 17, 39, 12, 69, 201, 244, 153, 24, 8, 23, 13, 1, 138, 88, 7, 85, 174, 36, 1, 223, 248, 160, 122, 34, 213, 204, 237, 216, 253, 200, 233, 191, 173, 180, 83, 133, 118, 67, 91, 37, 213, 103, 189, 84, 254, 185, 139, 65, 220, 92, 143, 252, 119, 21, 77, 45, 251, 21, 119, 133, 186, 97, 191, 109, 51, 54, 205, 29, 110, 183, 58, 0, 168, 206, 198, 95, 121, 121, 50, 199, 98, 118, 160, 144, 140, 16, 160, 244, 222, 58, 16, 95, 219, 237, 183, 56, 203, 238, 32, 238, 242, 237, 83, 88, 180, 116, 26, 85, 182, 147, 236, 252, 100, 156, 103, 64, 247 },
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("Logiware.Domain.Models.Entities.ItemHistory", b =>
                {
                    b.HasOne("Logiware.Domain.Models.Entities.Ownership", "Ownership")
                        .WithMany("ItemHistories")
                        .HasForeignKey("OwnershipId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ownership");
                });

            modelBuilder.Entity("Logiware.Domain.Models.Entities.Ownership", b =>
                {
                    b.HasOne("Logiware.Domain.Models.Entities.Item", "Item")
                        .WithMany("Ownerships")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Logiware.Domain.Models.Entities.Site", "Site")
                        .WithMany("Ownerships")
                        .HasForeignKey("SiteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Site");
                });

            modelBuilder.Entity("Logiware.Domain.Models.Entities.Personnel", b =>
                {
                    b.HasOne("Logiware.Domain.Models.Entities.Site", "Site")
                        .WithMany("Personnels")
                        .HasForeignKey("SiteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Site");
                });

            modelBuilder.Entity("Logiware.Domain.Models.Entities.Shipment", b =>
                {
                    b.HasOne("Logiware.Domain.Models.Entities.Personnel", "AuthorizedBy")
                        .WithMany("AuthorizedShipments")
                        .HasForeignKey("AuthorizedById")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Logiware.Domain.Models.Entities.Site", "DestinationSite")
                        .WithMany("ShipmentDestination")
                        .HasForeignKey("DestinationSiteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Logiware.Domain.Models.Entities.Personnel", "Driver")
                        .WithMany("DrivenShipments")
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Logiware.Domain.Models.Entities.Site", "Site")
                        .WithMany("Shipments")
                        .HasForeignKey("SiteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AuthorizedBy");

                    b.Navigation("DestinationSite");

                    b.Navigation("Driver");

                    b.Navigation("Site");
                });

            modelBuilder.Entity("Logiware.Domain.Models.Entities.ShipmentItem", b =>
                {
                    b.HasOne("Logiware.Domain.Models.Entities.Ownership", "Ownership")
                        .WithMany("ShipmentItems")
                        .HasForeignKey("OwnershipId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Logiware.Domain.Models.Entities.Shipment", "Shipment")
                        .WithMany("ShipmentItems")
                        .HasForeignKey("ShipmentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Ownership");

                    b.Navigation("Shipment");
                });

            modelBuilder.Entity("Logiware.Domain.Models.Entities.ShipmentReceive", b =>
                {
                    b.HasOne("Logiware.Domain.Models.Entities.ShipmentItem", "ShipmentItem")
                        .WithMany("ShipmentReceives")
                        .HasForeignKey("ShipmentItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ShipmentItem");
                });

            modelBuilder.Entity("Logiware.Domain.Models.Entities.User", b =>
                {
                    b.HasOne("Logiware.Domain.Models.Entities.Site", "Site")
                        .WithOne("User")
                        .HasForeignKey("Logiware.Domain.Models.Entities.User", "SiteId");

                    b.Navigation("Site");
                });

            modelBuilder.Entity("Logiware.Domain.Models.Entities.Item", b =>
                {
                    b.Navigation("Ownerships");
                });

            modelBuilder.Entity("Logiware.Domain.Models.Entities.Ownership", b =>
                {
                    b.Navigation("ItemHistories");

                    b.Navigation("ShipmentItems");
                });

            modelBuilder.Entity("Logiware.Domain.Models.Entities.Personnel", b =>
                {
                    b.Navigation("AuthorizedShipments");

                    b.Navigation("DrivenShipments");
                });

            modelBuilder.Entity("Logiware.Domain.Models.Entities.Shipment", b =>
                {
                    b.Navigation("ShipmentItems");
                });

            modelBuilder.Entity("Logiware.Domain.Models.Entities.ShipmentItem", b =>
                {
                    b.Navigation("ShipmentReceives");
                });

            modelBuilder.Entity("Logiware.Domain.Models.Entities.Site", b =>
                {
                    b.Navigation("Ownerships");

                    b.Navigation("Personnels");

                    b.Navigation("ShipmentDestination");

                    b.Navigation("Shipments");

                    b.Navigation("User")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
