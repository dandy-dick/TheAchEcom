const CART_DETAIL_CONTROLS = {
    cart: {
        updateCartTotalPrice: function (totalPrice) {
            $('#cart_detail_total').html(totalPrice)
        },
        updateCartItemQuantity: function (cartItemId, action) {
            var quantity = +$(`#cart_item_${cartItemId} .cart_item_quantity`)
                .attr('quantity');
            switch (action) {
                case "increase":
                    ++quantity;
                    break;
                case "decrease":
                    --quantity;
                    break;
                case "remove":
                    $('#cart_item_' + cartItemId).remove();
                    return;
            }
            +$(`#cart_item_${cartItemId} .cart_item_quantity`).attr('quantity', quantity);
            +$(`#cart_item_${cartItemId} .cart_item_quantity`).html(quantity);
        },
        remove: function (cartItemId, productId) {
            var that = this;
            CART_MANAGER.removeFromCart(productId,
                (result) => {
                    that.updateCartItemQuantity(cartItemId, 'remove');
                    that.updateCartTotalPrice(result.cartTotalPrice);
                });
        },
        increase: function (cartItemId, productId) {
            var that = this;
            CART_MANAGER.increaseCartItemQuantity(productId,
                (result) => {
                    that.updateCartItemQuantity(cartItemId, 'increase');
                    that.updateCartTotalPrice(result.cartTotalPrice);
                });
        },
        decrease: function (cartItemId, productId) {
            var that = this;
            CART_MANAGER.decreaseCartItemQuantity(productId,
                (result) => {
                    if (result.cartItemQuantity == 0) {
                        that.remove(cartItemId, productId);
                    }
                    else {
                        that.updateCartItemQuantity(cartItemId, 'decrease');
                        that.updateCartTotalPrice(result.cartTotalPrice);
                    }
                });
        }
    }
};