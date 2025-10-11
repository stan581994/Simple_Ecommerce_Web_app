// Shopping Cart Manager with localStorage
window.cartManager = {
    // Get all cart items from localStorage
    getCart: function() {
        const cart = localStorage.getItem('shopping_cart');
        return cart ? JSON.parse(cart) : [];
    },

    // Save cart to localStorage
    saveCart: function(cart) {
        localStorage.setItem('shopping_cart', JSON.stringify(cart));
    },

    // Add item to cart
    addToCart: function(product) {
        const cart = this.getCart();
        const existingItem = cart.find(item => item.id === product.id);

        if (existingItem) {
            existingItem.quantity += 1;
        } else {
            cart.push({
                id: product.id,
                name: product.name,
                description: product.description,
                price: product.price,
                originalPrice: product.originalPrice || product.price,
                imageUrl: product.imageUrl,
                isOnSale: product.isOnSale || false,
                category: product.category || '',
                stock: product.stock || 100,
                quantity: 1
            });
        }

        this.saveCart(cart);
        return cart.length;
    },

    // Remove item from cart
    removeFromCart: function(productId) {
        let cart = this.getCart();
        cart = cart.filter(item => item.id !== productId);
        this.saveCart(cart);
        return cart.length;
    },

    // Update item quantity
    updateQuantity: function(productId, quantity) {
        const cart = this.getCart();
        const item = cart.find(item => item.id === productId);
        
        if (item) {
            if (quantity <= 0) {
                return this.removeFromCart(productId);
            }
            item.quantity = quantity;
            this.saveCart(cart);
        }
        
        return cart.length;
    },

    // Clear entire cart
    clearCart: function() {
        localStorage.removeItem('shopping_cart');
        return 0;
    },

    // Get cart item count
    getCartCount: function() {
        const cart = this.getCart();
        return cart.reduce((total, item) => total + item.quantity, 0);
    },

    // Get cart total
    getCartTotal: function() {
        const cart = this.getCart();
        return cart.reduce((total, item) => total + (item.price * item.quantity), 0);
    }
};
