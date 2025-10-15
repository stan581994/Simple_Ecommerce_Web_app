# DRESSHY E-Commerce - Complete Documentation

## 📚 Table of Contents
1. [Getting Started](#getting-started)
2. [User Guide](#user-guide)
3. [Developer Guide](#developer-guide)
4. [API Reference](#api-reference)
5. [Database Schema](#database-schema)
6. [Troubleshooting](#troubleshooting)

---

## 🚀 Getting Started

### Prerequisites
- .NET 9 SDK or later
- Visual Studio 2022 or VS Code
- Git (for version control)

### Installation Steps

1. **Clone the Repository**
   ```bash
   git clone https://github.com/yourusername/Simple_Ecommerce_Web_app.git
   cd Simple_Ecommerce_Web_app
   ```

2. **Restore Dependencies**
   ```bash
   dotnet restore
   ```

3. **Run Database Migrations**
   ```bash
   dotnet ef database update
   ```

4. **Run the Application**
   ```bash
   dotnet run
   ```

5. **Access the Application**
   - Open browser: `http://localhost:5180`
   - Default Admin: username: `admin`, password: `admin123`
   - Default Customer: username: `customer`, password: `customer123`

---

## 👤 User Guide

### For Customers

#### 1. Registration & Login
- Click "Login" in the navigation bar
- Enter your credentials
- New users: Contact admin for account creation

#### 2. Browsing Products
- Navigate to "Products" page
- Use filters to narrow down products:
  - **Category Filter**: Select product category
  - **Price Range**: Choose price range
  - **Sort By**: Sort by name, price, or newest

#### 3. Shopping Cart
- Click "Add to Cart" on any product
- View cart by clicking cart icon (shows item count)
- In cart page:
  - **Update Quantity**: Use +/- buttons or type quantity
  - **Remove Items**: Click trash icon
  - **Clear Cart**: Remove all items at once

#### 4. Checkout Process
- Click "Proceed to Checkout" in cart
- **Must be logged in** to checkout
- System validates stock availability
- Upon success:
  - Inventory is automatically reduced
  - Cart is cleared
  - Redirected to products page

#### 5. Profile Management
- Click your username dropdown
- Select "Profile" to view account details
- View your role and permissions

### For Administrators

#### 1. Product Management
- Navigate to "Admin" → "Products"
- **Add Product**:
  - Click "Add New Product"
  - Fill in product details
  - Set initial stock quantity
  - Click "Save"
- **Edit Product**:
  - Click "Edit" on any product
  - Update details
  - Click "Update"
- **Delete Product**:
  - Click "Delete" on any product
  - Confirm deletion

#### 2. Category Management
- Navigate to "Admin" → "Categories"
- **Add Category**:
  - Click "Add New Category"
  - Enter name and description
  - Click "Save"
- **Edit/Delete**: Similar to products

#### 3. User Management
- Navigate to "Admin" → "Users"
- View all registered users
- See user roles and permissions

---

## 💻 Developer Guide

### Project Architecture

#### Folder Structure
```
Simple_Ecommerce_Web_app/
│
├── Components/              # Blazor Components
│   ├── Layout/             # Layout components
│   │   ├── MainLayout.razor       # Main app layout
│   │   ├── NavMenu.razor          # Navigation menu
│   │   ├── CartBadge.razor        # Cart counter badge
│   │   └── UserDropdown.razor     # User menu dropdown
│   │
│   └── Pages/              # Page components
│       ├── Home.razor             # Landing page
│       ├── Products.razor         # Product catalog
│       ├── Cart.razor             # Shopping cart
│       ├── Login.razor            # Login page
│       ├── Profile.razor          # User profile
│       ├── AdminProducts.razor    # Product management
│       ├── AdminCategories.razor  # Category management
│       └── Users.razor            # User management
│
├── Data/                   # Database Context
│   └── AppDbContext.cs            # EF Core DbContext
│
├── Models/                 # Data Models
│   ├── User.cs                    # User entity
│   ├── Role.cs                    # Role entity
│   ├── Product.cs                 # Product entity
│   ├── Category.cs                # Category entity
│   └── Inventory.cs               # Inventory entity
│
├── Services/               # Business Logic
│   ├── AuthService.cs             # Authentication service
│   ├── ProductService.cs          # Product operations
│   ├── CategoryService.cs         # Category operations
│   ├── InventoryService.cs        # Inventory management
│   └── CartService.cs             # Cart state management
│
├── Migrations/             # Database Migrations
│   └── [Migration Files]          # EF Core migrations
│
└── wwwroot/               # Static Files
    ├── css/                       # Stylesheets
    ├── js/                        # JavaScript files
    └── lib/                       # Third-party libraries
```

### Design Patterns Used

#### 1. Service Layer Pattern
- Business logic separated into service classes
- Injected via Dependency Injection
- Example: `AuthService`, `ProductService`

#### 2. Repository Pattern
- Entity Framework Core acts as repository
- DbContext provides data access abstraction
- LINQ queries for data retrieval

#### 3. Component-Based Architecture
- Blazor components for UI
- Reusable components (CartBadge, UserDropdown)
- Interactive server-side rendering

#### 4. Dependency Injection
- Services registered in `Program.cs`
- Injected into components and services
- Promotes loose coupling

### Key Technologies

#### Backend
- **.NET 9**: Latest framework version
- **Entity Framework Core**: ORM for database
- **SQLite**: Embedded database
- **BCrypt.Net**: Password hashing

#### Frontend
- **Blazor Server**: Interactive UI framework
- **Bootstrap 5**: CSS framework
- **Font Awesome**: Icon library
- **JavaScript**: Client-side cart management

---

## 🔌 API Reference

### AuthService

#### Methods

**`LoginAsync(AppDbContext dbContext, string username, string password)`**
- **Purpose**: Authenticates user credentials
- **Parameters**:
  - `dbContext`: Database context
  - `username`: User's username
  - `password`: Plain text password
- **Returns**: `Task<bool>` - True if login successful
- **Usage**:
  ```csharp
  var success = await AuthService.LoginAsync(dbContext, "admin", "admin123");
  ```

**`Logout()`**
- **Purpose**: Ends user session
- **Parameters**: None
- **Returns**: void
- **Usage**:
  ```csharp
  AuthService.Logout();
  ```

**`HasPermission(string permission)`**
- **Purpose**: Checks if current user has specific permission
- **Parameters**:
  - `permission`: Permission name (e.g., "AddProducts")
- **Returns**: `bool` - True if user has permission
- **Usage**:
  ```csharp
  if (AuthService.HasPermission("AddProducts")) { ... }
  ```

### ProductService

#### Methods

**`GetAllProductsAsync()`**
- **Purpose**: Retrieves all products with inventory
- **Returns**: `Task<List<Product>>` - List of products
- **Usage**:
  ```csharp
  var products = await ProductService.GetAllProductsAsync();
  ```

**`GetProductByIdAsync(int id)`**
- **Purpose**: Gets single product by ID
- **Parameters**: `id` - Product ID
- **Returns**: `Task<Product?>` - Product or null
- **Usage**:
  ```csharp
  var product = await ProductService.GetProductByIdAsync(1);
  ```

**`AddProductAsync(Product product, int initialStock)`**
- **Purpose**: Creates new product with inventory
- **Parameters**:
  - `product`: Product entity
  - `initialStock`: Initial stock quantity
- **Returns**: `Task<Product>` - Created product
- **Usage**:
  ```csharp
  var newProduct = await ProductService.AddProductAsync(product, 50);
  ```

**`UpdateProductAsync(Product product)`**
- **Purpose**: Updates existing product
- **Parameters**: `product` - Updated product entity
- **Returns**: `Task<Product>` - Updated product
- **Usage**:
  ```csharp
  await ProductService.UpdateProductAsync(product);
  ```

**`DeleteProductAsync(int id)`**
- **Purpose**: Deletes product and its inventory
- **Parameters**: `id` - Product ID
- **Returns**: `Task<bool>` - True if deleted
- **Usage**:
  ```csharp
  await ProductService.DeleteProductAsync(1);
  ```

### InventoryService

#### Methods

**`ReduceStockAsync(int productId, int quantity)`**
- **Purpose**: Reduces stock quantity for a product
- **Parameters**:
  - `productId`: Product ID
  - `quantity`: Amount to reduce
- **Returns**: `Task<bool>` - True if successful
- **Usage**:
  ```csharp
  var success = await InventoryService.ReduceStockAsync(1, 5);
  ```

**`CheckStockAvailabilityAsync(int productId, int quantity)`**
- **Purpose**: Checks if sufficient stock is available
- **Parameters**:
  - `productId`: Product ID
  - `quantity`: Required quantity
- **Returns**: `Task<bool>` - True if available
- **Usage**:
  ```csharp
  var available = await InventoryService.CheckStockAvailabilityAsync(1, 5);
  ```

**`GetStockQuantityAsync(int productId)`**
- **Purpose**: Gets current stock quantity
- **Parameters**: `productId` - Product ID
- **Returns**: `Task<int>` - Stock quantity
- **Usage**:
  ```csharp
  var stock = await InventoryService.GetStockQuantityAsync(1);
  ```

### CategoryService

#### Methods

**`GetAllCategoriesAsync()`**
- **Purpose**: Retrieves all categories
- **Returns**: `Task<List<Category>>` - List of categories
- **Usage**:
  ```csharp
  var categories = await CategoryService.GetAllCategoriesAsync();
  ```

**`AddCategoryAsync(Category category)`**
- **Purpose**: Creates new category
- **Parameters**: `category` - Category entity
- **Returns**: `Task<Category>` - Created category
- **Usage**:
  ```csharp
  await CategoryService.AddCategoryAsync(category);
  ```

### CartService

#### Methods

**`NotifyCartChanged(int count)`**
- **Purpose**: Notifies components of cart changes
- **Parameters**: `count` - New cart item count
- **Returns**: void
- **Usage**:
  ```csharp
  CartService.NotifyCartChanged(5);
  ```

**`SetCartCount(int count)`**
- **Purpose**: Sets cart count without notification
- **Parameters**: `count` - Cart item count
- **Returns**: void

---

## 🗄️ Database Schema

### Tables

#### Users
| Column | Type | Description |
|--------|------|-------------|
| Id | int | Primary key |
| Username | string | Unique username |
| Email | string | User email |
| PasswordHash | string | BCrypt hashed password |
| RoleId | int | Foreign key to Roles |
| CreatedAt | DateTime | Account creation date |
| UpdatedAt | DateTime? | Last update date |

#### Roles
| Column | Type | Description |
|--------|------|-------------|
| Id | int | Primary key |
| Name | string | Role name (Admin/Customer) |
| CanAddProducts | bool | Permission flag |
| CanEditPrices | bool | Permission flag |
| CanViewProducts | bool | Permission flag |
| CanAddToCart | bool | Permission flag |

#### Products
| Column | Type | Description |
|--------|------|-------------|
| Id | int | Primary key |
| Name | string | Product name |
| Description | string | Product description |
| Price | decimal | Current price |
| OriginalPrice | decimal | Original price (for sales) |
| IsOnSale | bool | Sale status |
| IsNew | bool | New product flag |
| ImageUrl | string | Product image URL |
| CategoryId | int | Foreign key to Categories |
| CreatedAt | DateTime | Creation date |
| UpdatedAt | DateTime? | Last update date |

#### Categories
| Column | Type | Description |
|--------|------|-------------|
| Id | int | Primary key |
| Name | string | Category name |
| Description | string | Category description |

#### Inventory
| Column | Type | Description |
|--------|------|-------------|
| Id | int | Primary key |
| ProductId | int | Foreign key to Products |
| StockQuantity | int | Available stock |
| ReorderLevel | int | Minimum stock threshold |
| LastRestocked | DateTime | Last restock date |
| LastSold | DateTime? | Last sale date |

### Relationships

```
User ──(Many-to-One)──> Role
Product ──(Many-to-One)──> Category
Product ──(One-to-One)──> Inventory
```

### Sample Queries

**Get all products with inventory:**
```csharp
var products = await context.Products
    .Include(p => p.Inventory)
    .Include(p => p.Category)
    .ToListAsync();
```

**Get user with role:**
```csharp
var user = await context.Users
    .Include(u => u.Role)
    .FirstOrDefaultAsync(u => u.Username == username);
```

---

## 🔧 Troubleshooting

### Common Issues

#### 1. Database Not Found
**Error**: `SQLite Error: no such table`
**Solution**:
```bash
dotnet ef database update
```

#### 2. Login Not Working
**Error**: Invalid credentials
**Solution**:
- Check default credentials (admin/admin123)
- Verify database has seeded data
- Check password hashing in AuthService

#### 3. Cart Not Persisting
**Error**: Cart empties on refresh
**Solution**:
- Check browser localStorage
- Verify cart.js is loaded
- Check browser console for errors

#### 4. Stock Not Reducing
**Error**: Inventory unchanged after checkout
**Solution**:
- Verify user is logged in
- Check InventoryService is registered
- Review checkout flow in Cart.razor

#### 5. Build Errors
**Error**: File locked or in use
**Solution**:
```bash
# Stop running processes
taskkill //F //IM dotnet.exe
# Clean and rebuild
dotnet clean
dotnet build
```

### Debug Tips

1. **Enable Detailed Logging**
   - Check `appsettings.json`
   - Set logging level to "Debug"

2. **Database Inspection**
   - Use DB Browser for SQLite
   - Check `ecommerce.db` file

3. **Browser DevTools**
   - Check Console for JS errors
   - Inspect Network tab for API calls
   - View Application → Local Storage for cart data

4. **Blazor Debugging**
   - Use browser DevTools
   - Set breakpoints in .razor files
   - Check Blazor Server logs

---

## 📝 Code Comments Guide

### Commenting Standards

#### 1. Class-Level Comments
```csharp
/// <summary>
/// Service for managing user authentication and authorization.
/// Handles login, logout, and permission checking.
/// </summary>
public class AuthService
{
    // Implementation
}
```

#### 2. Method-Level Comments
```csharp
/// <summary>
/// Authenticates a user with username and password.
/// </summary>
/// <param name="dbContext">Database context for user lookup</param>
/// <param name="username">User's username</param>
/// <param name="password">Plain text password to verify</param>
/// <returns>True if authentication successful, false otherwise</returns>
public async Task<bool> LoginAsync(App
