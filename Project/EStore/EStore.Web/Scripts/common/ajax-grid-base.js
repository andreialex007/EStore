function AjaxGridBase() {

    var self = {};

    self.init = function () {
        self.initTable();

        $(document.body).on("click", self.gridSelector + " td:last-of-type .edit", self.edit);
        $(document.body).on("click", self.gridSelector + " td:last-of-type .delete", self.delete);
        if (self.isModalPicker()) {
            $(document.body).on("click", self.gridSelector + " tbody td", self.selectItem);
        }
    }

    self.isModalPicker = function () {
        return $(self.gridSelector).closest(".modal").is(".item-picker");
    }

    self.close = function () {
        $(self.gridSelector).closest(".modal").modal("hide");
    }

    self.show = function () {
        $(self.gridSelector).closest(".modal").modal("show");
    }

    self.selectItem = function (event) {
        var parentTr = $(event.target).closest("tr");
        var rowIndex = parentTr.index();
        var data = self.table.row(rowIndex).data();
        self.close();
        self.onSelect(data);
    }

    self.edit = function (event) {
        var parentTr = $(event.target).closest("tr");
        var rowIndex = parentTr.index();
        var itemId = self.table.row(rowIndex).data().Id;
        var url = self.getEditUrl(itemId);
        parentTr.find("a.edit")
            .attr("href", url)
            .attr("target", self.isModalPicker() ? "_blank" : "");
    }

    self.delete = function (event) {
        var rowIndex = $(event.target).closest("tr").index();
        var itemId = self.table.row(rowIndex).data().Id;

        dialogsApi.showConfirmModal("Подтверждаете удаление?", "Вы действительно хотите удалить выбарнный элемент?", function () {

            $.ajax({
                type: "POST",
                url: self.deleteUrl,
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

    self.getEditUrl = function (id) { }
    self.deleteUrl = "";
    self.gridSelector = "";
    self.initTable = function () { }
    self.onSelect = function () { }

    return self;

}