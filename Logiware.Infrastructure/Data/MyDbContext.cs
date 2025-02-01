using System.Security.Cryptography;
using System.Text;
using Logiware.Domain;
using Logiware.Domain.Enums;
using Logiware.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logiware.Infrastructure.Data;

public partial class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Site> Sites { get; set; }
    public DbSet<Personnel> Personnels { get; set; }
    public DbSet<Shipment> Shipments { get; set; }
    public DbSet<ShipmentItem> ShipmentItems { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<ItemHistory?> ItemHistories { get; set; }
    public DbSet<Ownership> Ownerships { get; set; }

    public DbSet<ShipmentReceive> ShipmentReceives { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);

        modelBuilder.Entity<User>()
            .HasOne<Site>(u => u.Site)
            .WithOne(s => s.User).HasForeignKey<User>(u => u.SiteId).IsRequired(false);

        modelBuilder.Entity<Site>()
            .HasOne<User>(s => s.User)
            .WithOne(u => u.Site);

        modelBuilder.Entity<Personnel>()
            .HasOne<Site>(p => p.Site)
            .WithMany(s => s.Personnels);

        modelBuilder.Entity<Site>()
            .HasMany<Personnel>(s => s.Personnels)
            .WithOne(p => p.Site);

        modelBuilder.Entity<Item>()
            .HasMany<Ownership>(i => i.Ownerships)
            .WithOne(s => s.Item).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Ownership>()
            .HasOne<Item>(s => s.Item)
            .WithMany(i => i.Ownerships);

        modelBuilder.Entity<Site>()
            .HasMany<Ownership>(s => s.Ownerships)
            .WithOne(o => o.Site).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Ownership>()
            .HasOne<Site>(o => o.Site)
            .WithMany(s => s.Ownerships);


        modelBuilder.Entity<ItemHistory>()
            .HasOne<Ownership>(i => i.Ownership)
            .WithMany(o => o.ItemHistories);


        modelBuilder.Entity<Shipment>()
           .HasOne<Site>(sh => sh.Site)
           .WithMany(s => s.Shipments);
        modelBuilder.Entity<Site>()
            .HasMany<Shipment>(s => s.Shipments)
            .WithOne(s => s.Site);

        modelBuilder.Entity<Shipment>()
            .HasOne<Site>(sh => sh.DestinationSite)
            .WithMany(s => s.ShipmentDestination)
            .HasForeignKey(s => s.DestinationSiteId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Site>()
            .HasMany<Shipment>(s => s.ShipmentDestination)
            .WithOne(s => s.DestinationSite).OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Shipment>()
            .HasOne<Personnel>(sh => sh.Driver)
            .WithMany(p => p.DrivenShipments)
            .HasForeignKey(s => s.DriverId)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Personnel>()
            .HasMany<Shipment>(p => p.DrivenShipments)
            .WithOne(s => s.Driver)
            .HasForeignKey(s => s.DriverId);

        modelBuilder.Entity<Shipment>()
            .HasOne<Personnel>(s => s.AuthorizedBy)
            .WithMany(p => p.AuthorizedShipments)
            .HasForeignKey(s => s.AuthorizedById)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Personnel>()
            .HasMany<Shipment>(p => p.AuthorizedShipments)
            .WithOne(s => s.AuthorizedBy);

        modelBuilder.Entity<ShipmentItem>()
            .HasOne<Shipment>(si => si.Shipment)
            .WithMany(s => s.ShipmentItems)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Shipment>()
            .HasMany<ShipmentItem>(s => s.ShipmentItems)
            .WithOne(si => si.Shipment);

        modelBuilder.Entity<ShipmentItem>()
            .HasOne<Ownership>(s => s.Ownership)
            .WithMany(i => i.ShipmentItems);
        modelBuilder.Entity<Ownership>()
            .HasMany<ShipmentItem>(i => i.ShipmentItems)
            .WithOne(si => si.Ownership);



        modelBuilder.Entity<ShipmentReceive>()
            .HasOne<ShipmentItem>(i => i.ShipmentItem)
            .WithMany(shi => shi.ShipmentReceives);

        modelBuilder.Entity<ShipmentItem>()
            .HasMany<ShipmentReceive>(shi => shi.ShipmentReceives)
            .WithOne(sr => sr.ShipmentItem);



        modelBuilder.Entity<ItemHistory>().Property(s => s.Status)
            .HasConversion(
                s => s.ToString(),
                s => (Status)Enum.Parse(typeof(Status), s)
                );

        modelBuilder.Entity<Shipment>().Property(s => s.Status).HasConversion(
            s => s.ToString(),
            s => (Status)Enum.Parse(typeof(Status), s)
            );

        modelBuilder.Entity<Personnel>().Property(u => u.Role).HasConversion(
            s => s.ToString(),
            s => (Role)Enum.Parse(typeof(Role), s)
        );


        modelBuilder.Entity<Site>().HasData([
            new Site()
            {
                Id = 1,
                Name = "naawan",
                Location = "naawan market",
                Description = "naawan site",
                CreatedAt = DateTime.UtcNow,
            },
        new Site()
        {
            Id = 2,
            Name = "initao",
            Location = "initao market",
            Description = "initao site",
            CreatedAt = DateTime.UtcNow,
        },
        ]);
        modelBuilder.Entity<Item>().HasData(
            [
                new Item()
                {
                    Id = 1,
                    Name = "item1",
                    Description = "this is a test item1",
                    Category = "category1",
                    CreatedAt = DateTime.UtcNow,

                }
            ]
        );

        modelBuilder.Entity<Ownership>().HasData([
            new Ownership()
            {
                Id = 1,
                ItemId = 1,
                Quantity = 1000,
                SiteId = 1,
            CreatedAt = DateTime.UtcNow,

            }
        ]);

        modelBuilder.Entity<Personnel>().HasData(
            [
                new Personnel()
                {
                    Id = 1,
                    FirstName = "Zeke",
                    LastName = "canares",
                    SiteId = 1,
                    Role = Role.Driver,
            CreatedAt = DateTime.UtcNow,

                },
                new Personnel()
                {
                    Id = 2,
                    FirstName = "Miko",
                    LastName = "canares",
                    SiteId = 1,
                    Role = Role.Manager,
            CreatedAt = DateTime.UtcNow,

                },
            ]
        );
        using var hmac = new HMACSHA512();
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes("admin123456"));
        var salt = hmac.Key;
        modelBuilder.Entity<User>().HasData(
            [
                new User()
                {
                    Id=1,
                    Username = "admin",
                    Email = "admin@admin.com",
                    PasswordHash = hash,
                    PasswordSalt = salt,
                   Site = null,
            CreatedAt = DateTime.UtcNow,

                }
            ]);



    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
