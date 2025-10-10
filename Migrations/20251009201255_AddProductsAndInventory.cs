using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Simple_Ecommerce_Web_app.Migrations
{
    /// <inheritdoc />
    public partial class AddProductsAndInventory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    CanAddProducts = table.Column<bool>(type: "INTEGER", nullable: false),
                    CanEditPrices = table.Column<bool>(type: "INTEGER", nullable: false),
                    CanViewProducts = table.Column<bool>(type: "INTEGER", nullable: false),
                    CanAddToCart = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    StockQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    ReorderLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    LastRestocked = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastSold = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inventories_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Clothing and accessories for women", "Women's Fashion" },
                    { 2, "Clothing and accessories for men", "Men's Fashion" },
                    { 3, "Fashion accessories and jewelry", "Accessories" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CanAddProducts", "CanAddToCart", "CanEditPrices", "CanViewProducts", "Description", "Name" },
                values: new object[,]
                {
                    { 1, true, true, true, true, "Administrator with full access", "Admin" },
                    { 2, false, true, false, true, "Regular customer", "Customer" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "ImageUrl", "Name", "Price", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Perfect for any occasion with its flowing design and comfortable fabric.", "https://images.unsplash.com/photo-1515372039744-b8f02a3ae446?ixlib=rb-4.0.3&auto=format&fit=crop&w=500&q=80", "Elegant Summer Dress", 89.99m, null },
                    { 2, 2, new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Timeless style meets modern comfort in this versatile denim jacket.", "https://images.unsplash.com/photo-1551028719-00167b16eac5?ixlib=rb-4.0.3&auto=format&fit=crop&w=500&q=80", "Classic Denim Jacket", 79.99m, null },
                    { 3, 3, new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Crafted from genuine leather with attention to every detail.", "https://images.unsplash.com/photo-1584917865442-de89df76afd3?ixlib=rb-4.0.3&auto=format&fit=crop&w=500&q=80", "Premium Leather Handbag", 199.99m, null },
                    { 4, 2, new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Soft, breathable cotton in a relaxed fit for everyday comfort.", "https://images.unsplash.com/photo-1521572163474-6864f9cf17ab?ixlib=rb-4.0.3&auto=format&fit=crop&w=500&q=80", "Casual Cotton T-Shirt", 24.99m, null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "PasswordHash", "RoleId", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), "admin@ecommerce.com", "$2a$11$mgO4puH0wsnMlbbaRd6YRO0APtVEJrILAQAV.7vtiXborKZjPWVoW", 1, "admin" },
                    { 2, new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), "customer@ecommerce.com", "$2a$11$rXJJaglNRhWxplqO1Y9buuxLpDL3DwrnUV2GIcVSkltc3nt.I6TRC", 2, "customer" }
                });

            migrationBuilder.InsertData(
                table: "Inventories",
                columns: new[] { "Id", "LastRestocked", "LastSold", "ProductId", "ReorderLevel", "StockQuantity" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), null, 1, 10, 50 },
                    { 2, new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), null, 2, 10, 30 },
                    { 3, new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), null, 3, 5, 15 },
                    { 4, new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), null, 4, 20, 100 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_ProductId",
                table: "Inventories",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
