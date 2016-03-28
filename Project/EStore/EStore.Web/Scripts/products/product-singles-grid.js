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
        $(document.body).on("click", ".product-singles-grid .btn.save", self.save);
        $(document.body).on("click", ".product-singles-grid .btn.delete", self.delete);
        $(document.body).on("keyup", ".product-singles-grid .buy-price", self.buyPriceChanged);
        $(document.body).on("keyup", ".product-singles-grid .sell-price", self.buyPriceChanged);
        $(document.body).on("keyup", ".product-singles-grid .margin", self.marginChanged);
    }

    self.buyPriceChanged = function (event) {
        var parentTr = $(event.target).closest("tr");
        var price = accounting.unformat(parentTr.find(".buy-price").val(), ",");
        var sellPrice = accounting.unformat(parentTr.find(".sell-price").val(), ",");
        var newMargin = Math.round(((sellPrice - price) / price) * 100, 2);
        parentTr.find(".margin").val(accounting.unformat(newMargin, 2, "", ","));
    }

    self.marginChanged = function (event) {
        var parentTr = $(event.target).closest("tr");

        var price = accounting.unformat(parentTr.find(".buy-price").val(), ",");
        var margin = accounting.unformat(parentTr.find(".margin").val(), ",");
        var newCellPrice = Math.round(price + (price * (margin / 100)), 2);
        parentTr.find(".sell-price").val(accounting.unformat(newCellPrice, 2, "", ","));
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

    self.rowToJson = function () {

    }

    self.edit = function (event) {
        var parentTr = $(event.target).closest("tr");
        parentTr.addClass("edit-row").find("input").prop("readonly", false);
        parentTr.find(".notes").prop("contenteditable", true);
    }

    self.cancel = function (event) {
        self.table.draw();
    }

    self.save = function (event) {
        var parentTr = $(event.target).closest("tr");
        var text = parentTr.find(".notes").text();
        var id = parentTr.find("td:first").text();
        var price = accounting.unformat(parentTr.find(".buy-price").val(), ",");
        var sellPrice = accounting.unformat(parentTr.find(".sell-price").val(), ",");
        var margin = accounting.unformat(parentTr.find(".margin").val(), ",");
        var isNew = parentTr.find(".is-new").is(":checked");
        var stateVal = parentTr.find(".state").val();
        var productId = $(".entity-id").val();

        $.ajax({
            type: "POST",
            url: "/Products/SaveProductSingle",
            contentType: "application/json",
            dataType: "json",
            data: JSON.stringify({
                Id: id,
                BuyPrice: price,
                SellPrice: sellPrice,
                IsNew: isNew,
                ProductId: productId,
                StateId: stateVal,
                Notes: text
            })
        }).done(function (json) {
            self.cancel();
        });
    }

    self.delete = function (event) {
        var parentTr = $(event.target).closest("tr");
        var id = parentTr.find("td:first").text();

        dialogsApi.showConfirmModal("Подвтерждение удаления?", "Вы уверены что хотите удалить запись?", function () {
            $.ajax({
                type: "POST",
                url: "/Products/DeleteProductSingle",
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify({
                    id: id
                })
            }).done(function (json) {
                self.cancel();
            });
        });
    }

    self.init();

    return self;
}