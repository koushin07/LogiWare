using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Logiware.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ItemCode = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ownerships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    SiteId = table.Column<int>(type: "integer", nullable: false),
                    ItemId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ownerships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ownerships_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ownerships_Sites_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Sites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Personnels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    SiteId = table.Column<int>(type: "integer", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personnels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Personnels_Sites_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Sites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "bytea", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "bytea", nullable: false),
                    SiteId = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Sites_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Sites",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ItemHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OwnershipId = table.Column<int>(type: "integer", nullable: false),
                    Remarks = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemHistories_Ownerships_OwnershipId",
                        column: x => x.OwnershipId,
                        principalTable: "Ownerships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shipments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ShipmentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AuthorizedById = table.Column<int>(type: "integer", nullable: true),
                    ShipmentCode = table.Column<string>(type: "text", nullable: false),
                    DriverId = table.Column<int>(type: "integer", nullable: true),
                    SiteId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    StatusUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DestinationSiteId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shipments_Personnels_AuthorizedById",
                        column: x => x.AuthorizedById,
                        principalTable: "Personnels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Shipments_Personnels_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Personnels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Shipments_Sites_DestinationSiteId",
                        column: x => x.DestinationSiteId,
                        principalTable: "Sites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shipments_Sites_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Sites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShipmentItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ShipmentId = table.Column<int>(type: "integer", nullable: false),
                    ShipmentItemCode = table.Column<string>(type: "text", nullable: false),
                    OwnershipId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipmentItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShipmentItems_Ownerships_OwnershipId",
                        column: x => x.OwnershipId,
                        principalTable: "Ownerships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShipmentItems_Shipments_ShipmentId",
                        column: x => x.ShipmentId,
                        principalTable: "Shipments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ShipmentReceives",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuantityReceived = table.Column<int>(type: "integer", nullable: false),
                    QuantityMissing = table.Column<int>(type: "integer", nullable: false),
                    ShipmentItemId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipmentReceives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShipmentReceives_ShipmentItems_ShipmentItemId",
                        column: x => x.ShipmentItemId,
                        principalTable: "ShipmentItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Category", "CreatedAt", "Description", "ItemCode", "Name" },
                values: new object[] { 1, "category1", new DateTime(2025, 1, 31, 17, 29, 41, 944, DateTimeKind.Utc).AddTicks(4995), "this is a test item1", "ITM-1083", "item1" });

            migrationBuilder.InsertData(
                table: "Sites",
                columns: new[] { "Id", "CreatedAt", "Description", "Location", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 31, 17, 29, 41, 944, DateTimeKind.Utc).AddTicks(4300), "naawan site", "naawan market", "naawan" },
                    { 2, new DateTime(2025, 1, 31, 17, 29, 41, 944, DateTimeKind.Utc).AddTicks(4309), "initao site", "initao market", "initao" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "PasswordHash", "PasswordSalt", "SiteId", "Username" },
                values: new object[] { 1, new DateTime(2025, 1, 31, 17, 29, 41, 944, DateTimeKind.Utc).AddTicks(6350), "admin@admin.com", new byte[] { 206, 218, 21, 250, 128, 237, 225, 180, 42, 63, 160, 0, 98, 235, 109, 35, 31, 64, 141, 201, 247, 160, 37, 108, 117, 29, 47, 142, 191, 148, 122, 212, 131, 213, 189, 26, 223, 40, 107, 217, 224, 220, 79, 250, 167, 90, 155, 126, 83, 0, 165, 72, 32, 24, 178, 179, 148, 75, 182, 10, 207, 69, 4, 0 }, new byte[] { 29, 168, 167, 79, 10, 246, 45, 176, 54, 155, 233, 17, 39, 12, 69, 201, 244, 153, 24, 8, 23, 13, 1, 138, 88, 7, 85, 174, 36, 1, 223, 248, 160, 122, 34, 213, 204, 237, 216, 253, 200, 233, 191, 173, 180, 83, 133, 118, 67, 91, 37, 213, 103, 189, 84, 254, 185, 139, 65, 220, 92, 143, 252, 119, 21, 77, 45, 251, 21, 119, 133, 186, 97, 191, 109, 51, 54, 205, 29, 110, 183, 58, 0, 168, 206, 198, 95, 121, 121, 50, 199, 98, 118, 160, 144, 140, 16, 160, 244, 222, 58, 16, 95, 219, 237, 183, 56, 203, 238, 32, 238, 242, 237, 83, 88, 180, 116, 26, 85, 182, 147, 236, 252, 100, 156, 103, 64, 247 }, null, "admin" });

            migrationBuilder.InsertData(
                table: "Ownerships",
                columns: new[] { "Id", "CreatedAt", "ItemId", "Quantity", "SiteId" },
                values: new object[] { 1, new DateTime(2025, 1, 31, 17, 29, 41, 944, DateTimeKind.Utc).AddTicks(5052), 1, 1000, 1 });

            migrationBuilder.InsertData(
                table: "Personnels",
                columns: new[] { "Id", "Code", "CreatedAt", "FirstName", "LastName", "Role", "SiteId" },
                values: new object[,]
                {
                    { 1, "I1ILJU", new DateTime(2025, 1, 31, 17, 29, 41, 944, DateTimeKind.Utc).AddTicks(5158), "Zeke", "canares", "Driver", 1 },
                    { 2, "FECLVB", new DateTime(2025, 1, 31, 17, 29, 41, 944, DateTimeKind.Utc).AddTicks(5180), "Miko", "canares", "Manager", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemHistories_OwnershipId",
                table: "ItemHistories",
                column: "OwnershipId");

            migrationBuilder.CreateIndex(
                name: "IX_Ownerships_ItemId",
                table: "Ownerships",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Ownerships_SiteId",
                table: "Ownerships",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_Personnels_SiteId",
                table: "Personnels",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentItems_OwnershipId",
                table: "ShipmentItems",
                column: "OwnershipId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentItems_ShipmentId",
                table: "ShipmentItems",
                column: "ShipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentReceives_ShipmentItemId",
                table: "ShipmentReceives",
                column: "ShipmentItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_AuthorizedById",
                table: "Shipments",
                column: "AuthorizedById");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_DestinationSiteId",
                table: "Shipments",
                column: "DestinationSiteId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_DriverId",
                table: "Shipments",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_SiteId",
                table: "Shipments",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_SiteId",
                table: "Users",
                column: "SiteId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemHistories");

            migrationBuilder.DropTable(
                name: "ShipmentReceives");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ShipmentItems");

            migrationBuilder.DropTable(
                name: "Ownerships");

            migrationBuilder.DropTable(
                name: "Shipments");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Personnels");

            migrationBuilder.DropTable(
                name: "Sites");
        }
    }
}
