using Simple_Ecommerce_Web_app.Data;
using Simple_Ecommerce_Web_app.Models;
using Microsoft.EntityFrameworkCore;

namespace Simple_Ecommerce_Web_app.Services
{
    public class AuthService
    {
        private User? _currentUser;
        public event Action? OnAuthStateChanged;

        public User? CurrentUser => _currentUser;
        public bool IsAuthenticated => _currentUser != null;
        public bool IsAdmin => _currentUser?.Role.Name == "Admin";

        public async Task<bool> LoginAsync(AppDbContext dbContext, string username, string password)
        {
            var user = await dbContext.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
                return false;

            // Verify password using BCrypt
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);

            if (isPasswordValid)
            {
                _currentUser = user;
                OnAuthStateChanged?.Invoke();
                return true;
            }

            return false;
        }

        public void Logout()
        {
            _currentUser = null;
            OnAuthStateChanged?.Invoke();
        }

        public bool HasPermission(string permission)
        {
            if (_currentUser == null) return false;

            return permission switch
            {
                "AddProducts" => _currentUser.Role.CanAddProducts,
                "EditPrices" => _currentUser.Role.CanEditPrices,
                "ViewProducts" => _currentUser.Role.CanViewProducts,
                "AddToCart" => _currentUser.Role.CanAddToCart,
                _ => false
            };
        }
    }
}
