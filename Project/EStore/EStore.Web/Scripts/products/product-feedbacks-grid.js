function ProductFeedbacksGrid() {

    var self = {};
    self.table = null;

    self.init = function () {

        self.table = $(".product-feedbacks-grid table").DataTable({
            paging: false,
            order: [
                   [0, "asc"]
            ]
        });

        productFeedbackParserModal.feedbacksAdded = self.addFeedbacks;
        $(".product-feedbacks-grid .add").click(function () {
            productFeedbackParserModal.show();
        });

        $(document.body).on("click", ".product-feedbacks-grid .delete", self.delete);
    }

    self.addFeedbacks = function (result) {
        for (var i in result.views) {
            var item = result.views[i];
            var dataArr = utils.tableRowToArray(item.view || item);
            self.table.row.add(dataArr).draw();
        }
        productFeedbackParserModal.hide();
    }

    self.delete = function (event) {
        var parentTr = $(event.target).closest("tr");
        var rowIndex = parentTr.index();
        var id = parentTr.find("td:first").text();

        dialogsApi.showConfirmModal("Подвтерждение удаления?", "Вы уверены что хотите удалить запись?", function () {
            $.ajax({
                type: "POST",
                url: "/admin/Products/DeleteFeedback",
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify({
                    id: id
                })
            }).done(function (json) {
                self.table.row(rowIndex).remove().draw();
            });
        });
    }


    self.init();

    return self;
}