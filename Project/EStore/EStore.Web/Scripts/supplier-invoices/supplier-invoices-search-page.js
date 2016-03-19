$(function () {
    window.supplierInvoicesSearchPage = function () {

        var self = {};
        self.supplierInvoicesGrid = null;

        self.init = function () {

            self.supplierInvoicesGrid = new SupplierInvoicesGrid();
        }

        self.init();

        return self;

    }();
})