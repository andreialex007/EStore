$(function () {
    window.productsListPage = function () {

        var self = {};

        self.init = function () {

            $(".add-to-cart-button").click(self.addToCart);

        }

        self.addToCart = function (event) {
            console.log("addToCart");
        }

        self.init();

        return self;

    }();
})