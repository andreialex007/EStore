function InlineEditGridBase() {

    var self = {};
    self.gridSelector = "";

    self.init = function () {
        $(self.gridSelector).find("tbody tr input[type='text']").prop("readonly", "readonly");
        self.initTable();

        $(document.body).on("click", self.gridSelector + " .btn.add", function () {
            var newData = utils.tableRowToArray($(self.gridSelector + " .new-item").html());
            self.table.row.add(newData).draw();
        });
        $(document.body).on("click", self.gridSelector + " td:last-of-type .edit", self.edit);
        $(document.body).on("click", self.gridSelector + " td:last-of-type .delete", self.delete);
        $(document.body).on("click", self.gridSelector + " td:last-of-type .cancel", self.cancel);


        $(document.body).on("click", self.gridSelector + " td:last-of-type .save", self.save);
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

    self.tableData = null;
    self.edit = function (event) {
        var parentTr = $(event.target).closest("tr");
        parentTr.find("input[type='text']").prop("readonly", false);
        parentTr.addClass("edit-row");
        self.initRow(parentTr, event);
    }

    self.testing = function (event) {
        debugger;
    }

    self.delete = function () {
        console.log("delete");
    }

    self.cancel = function (event) {
        var data = self.table.data();
        self.table.clear().rows.add(data).draw();
    }

    self.save = function (event) {

        var parentTr = $(event.target).closest("tr");
        var json = self.rowViewToJson(parentTr);

        $.ajax({
            type: "POST",
            url: "/SupplierInvoices/SaveInvoicePosition",
            contentType: "application/json",
            dataType: "json",
            data: JSON.stringify({ item: json })
        }).done(function (json) {
            var id = parentTr.find("td:first").text().trim();
            var newData = utils.tableRowToArray(json.view);
            var updatedRow = $.grep(self.table.rows(), function (x) { return self.table.row(x).data()[0] == id; });
            self.table.row(updatedRow[0]).data(newData).draw();
            self.cancel();
        });

    }

    self.initRow = function (parentTr, event) { }
    self.rowViewToJson = function (parentTr) { }

    return self;

}