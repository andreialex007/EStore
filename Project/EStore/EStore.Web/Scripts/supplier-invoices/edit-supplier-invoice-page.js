$(function () {

    window.editSupplierInvoicePage = function () {

        var self = {};

        self.init = function () {
            $("*[name='BuyDate']").datetimepicker({ format: "DD.MM.YYYY", locale: "ru" });
            
            var selectedSupplierId = $(".supplier-id-box input[type='hidden']").data("selected-id");
            $(".supplier-id-box input[type='hidden']").select2Custom({
                placeholder: "Выберите поставщика",
                data: $(".supplier-id-box input[type='hidden']").data("items")
            }).select2("val", selectedSupplierId);
        }

        self.init();

        return self;
    }();


})