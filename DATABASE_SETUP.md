# Database Setup Documentation

## Overview
This application uses **SQLite** as the database with **Entity Framework Core** for data access.

## Database Schema

### Tables

#### Roles Table
- **Id** (Primary Key, Auto-increment)
- **Name** (Text) - Role name (Admin, Customer)
- **Description** (Text) - Role description
- **CanAddProducts** (Boolean) - Permission to add products
- **CanEditPrices** (Boolean) - Permission to edit product prices
- **CanViewProducts** (Boolean) - Permission to view products
- **CanAddToCart** (Boolean) - Permission to add items to cart

#### Users Table
- **Id** (Primary Key, Auto-increment)
- **Username** (Text) - User's username
- **Email** (Text) - User's email address
- **PasswordHash** (Text) - BCrypt hashed password
- **RoleId** (Foreign Key) - References Roles table
- **CreatedAt** (DateTime) - Account creation timestamp

## Seeded Data

### Roles
1. **Admin Role**
   - Can Add Products: ✓
   - Can Edit Prices: ✓
   - Can View Products: ✓
   - Can Add to Cart: ✓

2. **Customer Role**
   - Can Add Products: ✗
   - Can Edit Prices: ✗
   - Can View Products: ✓
   - Can Add to Cart: ✓

### Users
1. **Admin Account**
   - Username: `admin`
   - Email: `admin@ecommerce.com`
   - Password: `admin123`
   - Role: Admin

2. **Customer Account**
   - Username: `customer`
   - Email: `customer@ecommerce.com`
   - Password: `customer123`
   - Role: Customer

## Database File Location
The SQLite database file is created at: `ecommerce.db` in the project root directory.

## Connection String
Located in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=ecommerce.db"
  }
}
```

## Viewing the Database
You can view and query the database using:
- **DB Browser for SQLite** (https://sqlitebrowser.org/)
- **Visual Studio Code** with SQLite extension
- **Command line** with `sqlite3 ecommerce.db`

## Database Initialization
The database is automatically created when the application starts (see `Program.cs`):
```csharp
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}
```

## Accessing User Data
Navigate to `/users` in the application to view all user accounts and their permissions.

## Password Hashing
Passwords are hashed using **BCrypt.Net-Next** library for security.

## Future Enhancements
- Add authentication and authorization
- Implement login/logout functionality
- Add user registration
- Create admin panel for user management
- Add product management based on role permissions
