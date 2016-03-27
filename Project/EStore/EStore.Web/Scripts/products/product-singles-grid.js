function ProductSinglesGrid() {

    var self = {};

    self.init = function () {

        console.log("ProductSinglesGrid init");

        self.table = $(".product-singles-grid table").DataTable({
            "processing": true,
            "serverSide": true,
            "ajax": {
                "url": "/Products/SearchProductSingle",
                "type": "POST",
                "data": function (data) {
                    data.ProductId = $(".entity-id").val();
                }
            },
            "columns": [
                { "data": "Id" },
                { "data": "BuyPrice" },
                { "data": "SellPrice" },
                { "data": "Margin" },
                { "data": "IsNew" },
                { "data": "StateName" },
                { "data": "SupplierInvoiceId" },
                { "data": "Notes" },
                { "data": "Actions" }
            ],
            "createdRow": function (row, data, index) {
                self.viewToRow(self.table.row(index).node(), data.View);
            },
            aoColumnDefs: [
                {
                    bSortable: false,
                    sDefaultContent: $("#edit-delete-table-actions-template").html(),
                    aTargets: [-1]
                }
            ],
            order: [
                [0, "asc"]
            ]
        });

        $(document.body).on("click", ".product-singles-grid .btn.edit", self.edit);
        $(document.body).on("click", ".product-singles-grid .btn.cancel", self.cancel);
    }

    self.viewToRow = function (row, view) {
        var dataArray = utils.tableRowToArray(view);
        for (var i in dataArray) {
            var item = dataArray[i];
            $(row).find("td").eq(i).html(item);
        }
        $(row).find(".sell-price").autoNumeric("init", { aSep: ' ', aDec: ',' });
        $(row).find(".buy-price").autoNumeric("init", { aSep: ' ', aDec: ',' });
        $(row).find(".margin").autoNumeric("init", { aSep: ' ', aDec: ',' });
        $(row).find("input").prop("readonly", true);
    }

    self.edit = function (event) {
        var parentTr = $(event.target).closest("tr");
        parentTr.addClass("edit-row").find("input").prop("readonly", false);
        parentTr.find(".notes").prop("contenteditable", true);
    }

    self.cancel = function (event) {
        self.table.draw();
    }


    self.init();

    return self;
}