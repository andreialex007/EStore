function SupplierInvoicesGrid() {

    var self = new AjaxGridBase();

    self.getEditUrl = function (id) {
        return "/admin/SupplierInvoices/" + id;
    }

    self.deleteUrl = "/admin/SupplierInvoices/Delete";
    self.gridSelector = ".supplier-invoices-grid";

    self.initTable = function () {
        self.table = $(self.gridSelector + " table").DataTable({
            "processing": true,
            "serverSide": true,
            "ajax": {
                "url": "/admin/SupplierInvoices/Search",
                "type": "POST"
            },
            "columns": [
                { "data": "Id" },
                { "data": "Notes" },
                { "data": "SupplierNumber" },
                { "data": "BuyDate" },
                { "data": "PositionsQty" },
                { "data": "Total" },
                { "data": "Actions" }
            ],
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
    }

    self.init();

    return self;

}