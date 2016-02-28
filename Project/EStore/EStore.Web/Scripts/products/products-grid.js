function ProductsGrid() {

    var self = {};

    self.init = function () {

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

        $(document.body).on("click", ".products-search-page td:last-of-type .edit", self.edit);
        $(document.body).on("click", ".products-search-page td:last-of-type .delete", self.delete);
    }

    self.edit = function (event) {
        var rowIndex = $(event.target).closest("tr").index();
        var itemId = self.table.row(rowIndex).data().Id;
        location.href = "/Products/" + itemId;
    }

    self.delete = function (event) {
        var rowIndex = $(event.target).closest("tr").index();
        var itemId = self.table.row(rowIndex).data().Id;

        dialogsApi.showConfirmModal("Подтверждаете удаление?", "Вы действительно хотите удалить выбарнный элемент?", function () {

            $.ajax({
                type: "POST",
                url: "/Products/Delete",
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify({ id: itemId })
            }).done(function () {
                self.table.ajax.reload();
            });
        });

        event.preventDefault();
        return false;
    }

    self.init();

    return self;

}