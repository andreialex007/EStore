function ProductsGrid() {

    var self = new AjaxGridBase();

    self.getEditUrl = function (id) {
        return "/Products/" + id;
    }

    self.deleteUrl = "/Products/Delete";
    self.gridSelector = ".products-grid";

    self.initTable = function () {
        self.table = $(".products-grid table").DataTable({
            "processing": true,
            "serverSide": true,
            "ajax": {
                "url": "/Products/Search",
                "type": "POST"
            },
            "columns": [
                { "data": "Id" },
                { "data": "Name" },
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