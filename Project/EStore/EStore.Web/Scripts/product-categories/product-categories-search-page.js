$(function () {

    window.productCategoriesSearchPage = function () {

        var self = {};

        self.init = function () {

            self.table = $(".categories-search-page table").DataTable({
                aoColumnDefs: [
                    {
                        bSortable: false,
                        aTargets: [-1]
                    }
                ],
                order: [
                    [0, "asc"]
                ]
            });

            $(document.body).on("click", ".categories-search-page td:last-of-type .delete", self.delete);
        }

        self.delete = function (event) {
            var rowIndex = $(event.target).closest("tr").index();
            var itemId = self.table.row(rowIndex).data()[0];

            dialogsApi.showConfirmModal("Подтверждаете удаление?", "Вы действительно хотите удалить выбарнный элемент?", function () {

                $.ajax({
                    type: "POST",
                    url: "/admin/ProductCategories/Delete",
                    contentType: "application/json",
                    dataType: "json",
                    data: JSON.stringify({ id: itemId })
                }).done(function () {
                    var updatedRow = $.grep(self.table.rows()[0], function (x) { return self.table.row(x).data()[0] == itemId; });
                    self.table.row(updatedRow[0]).remove().draw();
                });
            });

            event.preventDefault();
            return false;
        }

        self.init();

        return self;

    }();

})