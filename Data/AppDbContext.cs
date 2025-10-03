using Microsoft.EntityFrameworkCore;
using Simple_Ecommerce_Web_app.Models;

namespace Simple_Ecommerce_Web_app.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);

            // Seed Roles
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    Name = "Admin",
                    Description = "Administrator with full access",
                    CanAddProducts = true,
                    CanEditPrices = true,
                    CanViewProducts = true,
                    CanAddToCart = true
                },
                new Role
                {
                    Id = 2,
                    Name = "Customer",
                    Description = "Regular customer",
                    CanAddProducts = false,
                    CanEditPrices = false,
                    CanViewProducts = true,
                    CanAddToCart = true
                }
            );

            // Seed Users (using simple password hashing for demo - in production use proper hashing)
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    Email = "admin@ecommerce.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    RoleId = 1,
                    CreatedAt = DateTime.UtcNow
                },
                new User
                {
                    Id = 2,
                    Username = "customer",
                    Email = "customer@ecommerce.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("customer123"),
                    RoleId = 2,
                    CreatedAt = DateTime.UtcNow
                }
            );
        }
    }
}
