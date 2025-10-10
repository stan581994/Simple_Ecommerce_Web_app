# Simple E-commerce Web Application - Memory Bank

## Project Overview
A Blazor Server e-commerce web application with user authentication, role-based access control, and product management capabilities.

## Technology Stack
- **Framework**: ASP.NET Core Blazor Server (.NET 8)
- **Database**: SQLite with Entity Framework Core
- **Authentication**: Custom authentication with BCrypt password hashing
- **State Management**: ProtectedSessionStorage for persisting auth state
- **UI**: Bootstrap 5 with custom CSS

## Database Schema

### Users Table
- `Id` (int, PK)
- `Username` (string, unique)
- `Email` (string)
- `PasswordHash` (string, BCrypt hashed)
- `RoleId` (int, FK to Roles)
- `CreatedAt` (DateTime)

### Roles Table
- `Id` (int, PK)
- `Name` (string) - "Admin" or "Customer"
- `CanAddProducts` (bool)
- `CanEditPrices` (bool)
- `CanViewProducts` (bool)
- `CanAddToCart` (bool)

### Products Table
- `Id` (int, PK)
- `Name` (string)
- `Description` (string)
- `Price` (decimal)
- `CategoryId` (int, FK to Categories)
- `ImageUrl` (string)
- `CreatedAt` (DateTime)
- `UpdatedAt` (DateTime)

### Categories Table
- `Id` (int, PK)
- `Name` (string)
- `Description` (string)

### Inventories Table
- `Id` (int, PK)
- `ProductId` (int, FK to Products, one-to-one)
- `StockQuantity` (int)
- `ReorderLevel` (int)
- `LastRestocked` (DateTime, nullable)
- `LastSold` (DateTime, nullable)

## Authentication System

### AuthService
**Location**: `Services/AuthService.cs`

**Key Features**:
- Session-based authentication using `ProtectedSessionStorage`
- Persists user ID across page reloads
- BCrypt password verification
- Role-based permission checking

**Methods**:
- `InitializeAsync()` - Loads user from session storage on app start
- `LoginAsync(dbContext, username, password)` - Authenticates user and stores session
- `LogoutAsync()` - Clears session and user state
- `HasPermission(permission)` - Checks if current user has specific permission

**Properties**:
- `CurrentUser` - Currently authenticated user object
- `IsAuthenticated` - Boolean indicating if user is logged in
- `IsAdmin` - Boolean indicating if current user has Admin role

### Default Users
Created during database initialization:

**Admin User**:
- Username: `admin`
- Password: `admin123`
- Email: admin@ecommerce.com
- Permissions: Full access (add products, edit prices, view products, add to cart)

**Customer User**:
- Username: `customer`
- Password: `customer123`
- Email: customer@ecommerce.com
- Permissions: View products and add to cart only

## Product Management System

### Overview
Full CRUD (Create, Read, Update, Delete) system for managing products with inventory tracking.

### Access Methods

#### Method 1: User Dropdown Menu (RECOMMENDED)
1. Login as admin (username: `admin`, password: `admin123`)
2. Click on your username in the top-right corner
3. A dropdown menu appears with:
   - Profile
   - **Manage Products** ← Click this!
   - Logout

#### Method 2: Sidebar Navigation
1. Login as admin
2. Look in the left sidebar navigation menu
3. Click "Manage Products" (appears below "Users")

#### Method 3: Direct URL
Navigate to: `http://localhost:5180/admin/products`

### Features

#### Product List View
- Displays all products in a responsive table
- Shows product image, name, description, category, price, stock, and status
- Color-coded stock status:
  - **Green (In Stock)**: Stock > reorder level
  - **Yellow (Low Stock)**: Stock ≤ reorder level but > 0
  - **Red (Out of Stock)**: Stock = 0
- Low stock alert banner when products need restocking
- Action buttons: Edit, Update Stock, Delete

#### Add New Product
**Button**: Blue "Add New Product" button at top of page

**Form Fields**:
- Product Name (required)
- Description (required)
- Price (required, decimal)
- Category (required, dropdown selection)
- Image URL (required)
- Initial Stock (required, integer)
- Reorder Level (required, integer)

**Validation**:
- All fields are required
- Price must be positive
- Stock quantities must be non-negative integers

#### Edit Product
**Access**: Click pencil icon in Actions column

**Editable Fields**:
- Product Name
- Description
- Price
- Category
- Image URL

**Note**: Stock is updated separately via "Update Stock" feature

#### Update Stock
**Access**: Click warehouse icon in Actions column

**Features**:
- Add or remove stock quantity
- Automatically updates `LastRestocked` timestamp when stock is added
- Automatically updates `LastSold` timestamp when stock is removed
- Real-time stock status updates

#### Delete Product
**Access**: Click trash icon in Actions column

**Behavior**:
- Requires confirmation
- Permanently deletes product and associated inventory record
- Cannot be undone

### ProductService
**Location**: `Services/ProductService.cs`

**Key Methods**:
- `GetAllProductsAsync()` - Retrieves all products with categories and inventory
- `GetLowStockProductsAsync()` - Gets products at or below reorder level
- `GetProductByIdAsync(id)` - Retrieves single product with full details
- `CreateProductAsync(product, initialStock, reorderLevel)` - Creates product with inventory
- `UpdateProductAsync(product)` - Updates product details
- `DeleteProductAsync(id)` - Deletes product and inventory
- `UpdateStockAsync(productId, quantityChange)` - Adjusts stock levels
- `GetAllCategoriesAsync()` - Retrieves all product categories

### Seeded Data
The database is pre-populated with:

**Categories**:
1. Women's Fashion
2. Men's Fashion
3. Accessories

**Products**:
1. **Classic White T-Shirt**
   - Category: Women's Fashion
   - Price: $29.99
   - Stock: 50
   - Reorder Level: 10

2. **Denim Jeans**
   - Category: Men's Fashion
   - Price: $79.99
   - Stock: 30
   - Reorder Level: 15

3. **Leather Wallet**
   - Category: Accessories
   - Price: $49.99
   - Stock: 25
   - Reorder Level: 10

4. **Summer Dress**
   - Category: Women's Fashion
   - Price: $89.99
   - Stock: 20
   - Reorder Level: 8

## Navigation Structure

### Top Navigation Bar
- **Left**: Application logo/name
- **Right**: User dropdown (when authenticated)
  - Shows username with "Admin" badge for admin users
  - Dropdown menu with Profile, Manage Products (admin only), and Logout

### Sidebar Navigation
- Home
- Counter (demo page)
- Weather (demo page)
- Users
- Manage Products (admin only, visible when logged in)

## Component Structure

### Pages
- `Components/Pages/Home.razor` - Landing page
- `Components/Pages/Login.razor` - Authentication page
- `Components/Pages/Profile.razor` - User profile page
- `Components/Pages/Users.razor` - User management page
- `Components/Pages/AdminProducts.razor` - Product management page (admin only)
- `Components/Pages/Products.razor` - Public product catalog
- `Components/Pages/Cart.razor` - Shopping cart

### Layout Components
- `Components/Layout/MainLayout.razor` - Main application layout
- `Components/Layout/NavMenu.razor` - Navigation menu with auth-aware rendering
- `Components/Layout/UserDropdown.razor` - User dropdown component (deprecated, functionality moved to NavMenu)

## Key Implementation Details

### Session Persistence
- Uses `ProtectedSessionStorage` to persist user ID across page reloads
- AuthService initializes on component load to restore user state
- Login uses `forceLoad: true` to ensure full page reload and proper state initialization

### Render Modes
- Interactive Server render mode for all interactive components
- Enables real-time updates and server-side state management

### Security
- Passwords hashed with BCrypt (work factor: 12)
- Role-based access control for admin features
- Protected session storage for auth tokens

## Running the Application

1. **Start the application**:
   ```bash
   dotnet run
   ```

2. **Access the application**:
   - URL: `http://localhost:5180`

3. **Login**:
   - Navigate to `/login` or click login link
   - Use admin credentials: `admin` / `admin123`

4. **Access Product Management**:
   - Click username dropdown in top-right
   - Select "Manage Products"
   - Or navigate directly to `/admin/products`

## Troubleshooting

### User Dropdown Not Appearing After Login
**Issue**: NavMenu doesn't show user dropdown after successful login

**Solution**: 
- Ensure AuthService is registered as `Scoped` in Program.cs
- Login page uses `forceLoad: true` for navigation
- NavMenu calls `await AuthService.InitializeAsync()` in `OnInitializedAsync`

### Products Not Loading
**Issue**: Product list is empty or not displaying

**Solution**:
- Check database was properly seeded
- Verify ProductService is injected correctly
- Check browser console for errors

### Permission Denied
**Issue**: Cannot access admin features

**Solution**:
- Ensure logged in as admin user
- Check user's role in database
- Verify role permissions are set correctly

## Future Enhancements
- Shopping cart functionality
- Order management
- Payment integration
- Product search and filtering
- Image upload for products
- Customer reviews and ratings
- Email notifications for low stock
- Sales analytics dashboard
