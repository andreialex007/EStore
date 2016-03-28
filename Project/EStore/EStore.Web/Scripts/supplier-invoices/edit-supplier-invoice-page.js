$(function () {

    window.editSupplierInvoicePage = function () {

        var self = {};
        self.supplierInvoicePositionsGrid = null;
        self.supplierInvoiceFilesGrid = null;

        self.init = function () {
            $("*[name='BuyDate']").datetimepicker({ format: "DD.MM.YYYY", locale: "ru" });

            var selectedSupplierId = $(".supplier-id-box input[type='hidden']").data("selected-id");
            $(".supplier-id-box input[type='hidden']").select2Custom({
                placeholder: "Выберите поставщика",
                data: $(".supplier-id-box input[type='hidden']").data("items")
            }).select2("val", selectedSupplierId);

            self.supplierInvoicePositionsGrid = new SupplierInvoicePositionsGrid();
            self.supplierInvoiceFilesGrid = new SupplierInvoiceFilesGrid();

            $(document.body).on("click", ".generate-products-btn", self.generateProducts);
        }

        self.generateProducts = function () {

            $.ajax({
                type: "POST",
                url: "/admin/SupplierInvoices/GenerateProductSingles",
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify({ id: $(".entity-id").val() })
            }).done(function (json) {
                UIToastr.ShowMessage("success", "Выполнено", "Созданы товары");
            });

        }

        self.init();

        return self;
    }();


})