$(function () {
    window.productsSearchPage = function () {

        var self = {};
        self.productsGrid = null;

        self.init = function () {

            self.productsGrid = new ProductsGrid();
        }

        self.init();

        return self;

    }();
})