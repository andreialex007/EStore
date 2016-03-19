function InlineEditGridBase() {

    var self = {};
    self.gridSelector = ".supplier-invoice-positions-grid";
    self.productsGrid = new ProductsGrid();

    self.init = function () {
        $(self.gridSelector).find("tbody tr input[type='text']").prop("readonly", "readonly");
        self.initTable();
        $(document.body).on("click", self.gridSelector + " td:last-of-type .edit", self.edit);
        $(document.body).on("click", self.gridSelector + " td:last-of-type .delete", self.delete);
        $(document.body).on("click", self.gridSelector + " td:last-of-type .cancel", self.cancel);
        $(document.body).on("click", self.gridSelector + " td:last-of-type .save", self.save);
        $(document.body).on("click", self.gridSelector + " .select-product-btn", self.selectProduct);

    }

    self.initTable = function () {
        self.table = $(self.gridSelector + " table").DataTable({
            aoColumnDefs: [
                {
                    bSortable: false,
                    sDefaultContent: $("#all-table-actions-template").html(),
                    aTargets: [-1]
                }
            ],
            order: [
                [0, "asc"]
            ]
        });

    }

    self.selectProduct = function () {
        $(".products-modal").modal("show");
    }

    self.edit = function (event) {
        var parentTr = $(event.target).closest("tr");
        parentTr.find("input[type='text']").prop("readonly", false);
        parentTr.addClass("edit-row");
        self.initRow(parentTr, event);
    }

    self.initRow = function (parentTr, event) {

        parentTr.find(".qty").numeric();
        parentTr.find(".price").numeric({ decimal: ",", scale: 2 });

        console.log("initRow");
    }


    self.delete = function () {
        console.log("delete");
    }

    self.cancel = function (event) {
        var data = self.table.data();
        self.table.clear().rows.add(data).draw();

        console.log("cancel");
    }

    self.save = function () {

    }

    return self;

}