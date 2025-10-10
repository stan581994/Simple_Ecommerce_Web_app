using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Simple_Ecommerce_Web_app.Migrations
{
    /// <inheritdoc />
    public partial class AddProductCategoryInventoryTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Electronic devices and accessories", "Electronics" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Fashion and apparel", "Clothing" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Books and publications", "Books" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 4, "Home improvement and garden supplies", "Home & Garden" });

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastRestocked",
                value: new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "LastRestocked", "ReorderLevel", "StockQuantity" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 20, 100 });

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "LastRestocked", "ReorderLevel", "StockQuantity" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 10, 5 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "High-performance laptop for work and gaming", "https://via.placeholder.com/300x300?text=Laptop", "Laptop", 999.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Comfortable cotton t-shirt", "https://via.placeholder.com/300x300?text=T-Shirt", "T-Shirt", 19.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Learn C# programming from scratch", "https://via.placeholder.com/300x300?text=Book", "Programming Book", 39.99m });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "$2a$11$G2ALInfG1J0zMPD3SDPeBeu9S5A9rPi7t1b/iHM96aTNOG/3lBpGO" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "$2a$11$LPfx2V16f5aWANS8aJp1deknRwBzsdAoia.TRRlzBXuQwU0Lq2N7." });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Clothing and accessories for women", "Women's Fashion" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Clothing and accessories for men", "Men's Fashion" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Fashion accessories and jewelry", "Accessories" });

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastRestocked",
                value: new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "LastRestocked", "ReorderLevel", "StockQuantity" },
                values: new object[] { new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), 10, 30 });

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "LastRestocked", "ReorderLevel", "StockQuantity" },
                values: new object[] { new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), 5, 15 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Perfect for any occasion with its flowing design and comfortable fabric.", "https://images.unsplash.com/photo-1515372039744-b8f02a3ae446?ixlib=rb-4.0.3&auto=format&fit=crop&w=500&q=80", "Elegant Summer Dress", 89.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Timeless style meets modern comfort in this versatile denim jacket.", "https://images.unsplash.com/photo-1551028719-00167b16eac5?ixlib=rb-4.0.3&auto=format&fit=crop&w=500&q=80", "Classic Denim Jacket", 79.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Crafted from genuine leather with attention to every detail.", "https://images.unsplash.com/photo-1584917865442-de89df76afd3?ixlib=rb-4.0.3&auto=format&fit=crop&w=500&q=80", "Premium Leather Handbag", 199.99m });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "ImageUrl", "Name", "Price", "UpdatedAt" },
                values: new object[] { 4, 2, new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Soft, breathable cotton in a relaxed fit for everyday comfort.", "https://images.unsplash.com/photo-1521572163474-6864f9cf17ab?ixlib=rb-4.0.3&auto=format&fit=crop&w=500&q=80", "Casual Cotton T-Shirt", 24.99m, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), "$2a$11$mgO4puH0wsnMlbbaRd6YRO0APtVEJrILAQAV.7vtiXborKZjPWVoW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), "$2a$11$rXJJaglNRhWxplqO1Y9buuxLpDL3DwrnUV2GIcVSkltc3nt.I6TRC" });

            migrationBuilder.InsertData(
                table: "Inventories",
                columns: new[] { "Id", "LastRestocked", "LastSold", "ProductId", "ReorderLevel", "StockQuantity" },
                values: new object[] { 4, new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), null, 4, 20, 100 });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
