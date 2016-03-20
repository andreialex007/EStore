function SupplierInvoicePositionsGrid() {

    var self = new InlineEditGridBase();

    self.gridSelector = ".supplier-invoice-positions-grid";
    self.productsGrid = null;

    var initBase = self.init;
    self.init = function () {
        initBase();
        $(document.body).on("click", self.gridSelector + " .select-product-btn", self.selectProduct);
        $(document.body).on("keyup", self.gridSelector + " .qty, " + self.gridSelector + " .price", self.recalculateTotal);
        self.productsGrid = new ProductsGrid();
        self.productsGrid.onSelect = self.onProductSelected;
    }

    self.onProductSelected = function (item) {
        var updatedRow = $.grep(self.table.rows(), function (x) { return self.table.row(x).data()[0] == item.Id; });
        var nodeHtml = $(self.table.row(updatedRow[0]).node());
        nodeHtml
            .find(".product-name")
            .attr("data-product-id", item.Id)
            .text(item.Name);

    }

    self.recalculateTotal = function (event) {
        var parentTr = $(event.target).closest("tr");
        var qty = accounting.unformat(parentTr.find(".qty").val(), ",");
        var price = accounting.unformat(parentTr.find(".price").val(), ",");
        var total = qty * price;
        var totalFormatted = accounting.formatMoney(total, "", 2, " ", ",");
        parentTr.find(".total").text(totalFormatted);
    }

    self.selectProduct = function (event) {
        self.productsGrid.show();
    }

    self.initRow = function (parentTr, event) {
        parentTr.find(".qty").numeric();
        parentTr.find(".price").autoNumeric("init", { aSep: ' ', aDec: ',' });
    }

    self.rowViewToJson = function (parentTr) {

        var id = parentTr.find("td:first").text().trim();
        var productId = parentTr.find(".product-name").data("product-id");
        var productName = parentTr.find(".product-name").text();
        var qty = accounting.unformat(parentTr.find(".qty").val(), ",");
        var price = accounting.unformat(parentTr.find(".price").val(), ",");
        var note = parentTr.find(".note-text").val();

        var item = {
            Id: id,
            ProductId: productId,
            Qty: qty,
            Price: price,
            Note: note,
            ProductName: productName,
            SupplierInvoiceId: $(".entity-id").val()
        };

        return item;
    }

    self.init();

    return self;

}