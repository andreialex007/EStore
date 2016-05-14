$(function () {
    window.productsListPage = function () {

        var self = {};

        self.init = function () {

            $(".add-to-cart-button").click(self.addToCart);
            $(".remove-from-cart-button").click(self.addToCart);

        }

        self.addToCart = function (event) {
            utils.addRemoveInCart(event.target);
        }

        self.init();

        return self;

    }();
})