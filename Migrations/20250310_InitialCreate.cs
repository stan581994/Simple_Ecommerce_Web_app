using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Simple_Ecommerce_Web_app.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            // Seed Roles
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name", "Description", "CanAddProducts", "CanEditPrices", "CanViewProducts", "CanAddToCart" },
                values: new object[,]
                {
                    { 1, "Admin", "Administrator with full access", true, true, true, true },
                    { 2, "Customer", "Regular customer", false, false, true, true }
                });

            // Seed Users
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Username", "Email", "PasswordHash", "RoleId", "CreatedAt" },
                values: new object[,]
                {
                    { 1, "admin", "admin@ecommerce.com", "$2a$11$XKVZqE5qYxH8YqKqYqKqYeN5N5N5N5N5N5N5N5N5N5N5N5N5N5N5N", 1, DateTime.UtcNow },
                    { 2, "customer", "customer@ecommerce.com", "$2a$11$YLWArF6rZyI9ZrLrZrLrZeO6O6O6O6O6O6O6O6O6O6O6O6O6O6O6O", 2, DateTime.UtcNow }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
