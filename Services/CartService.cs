namespace Simple_Ecommerce_Web_app.Services
{
    public class CartService
    {
        private int _cartItemCount = 0;
        public event Action? OnCartChanged;

        public int CartItemCount => _cartItemCount;

        public void NotifyCartChanged(int count)
        {
            _cartItemCount = count;
            OnCartChanged?.Invoke();
        }

        public void SetCartCount(int count)
        {
            _cartItemCount = count;
            OnCartChanged?.Invoke();
        }
    }
}
