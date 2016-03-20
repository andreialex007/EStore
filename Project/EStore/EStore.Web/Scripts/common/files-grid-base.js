function FilesGridBase() {

    var self = {}
    self.gridSelector = ".common-files-grid";

    self.init = function () {
        self.table = $(self.gridSelector + " table").DataTable({
            paging: false,
            order: [
                   [0, "asc"]
            ]
        });
        $(document).on("change", self.gridSelector + " .file-upload-input", self.uploadFile);
        $(document).on("click", self.gridSelector + " .delete", self.deleteFile);
    }

    self.saveFileDescription = function (id, text) {

        $.ajax({
            type: "POST",
            url: "/Files/SaveFileDescription",
            contentType: "application/json",
            dataType: "json",
            data: JSON.stringify({ id: id, text: text })
        }).done(function () { });
    }

    self.uploadFile = function (event) {
        utils.uploadManyFiles("/Files/UploadFile/", event.target.files, self.getExtraData(), self.filesUploaded);
    }

    self.filesUploaded = function (result) {
        for (var i in result) {
            var item = result[i];
            var dataArr = utils.tableRowToArray(item.view);
            self.table.row.add(dataArr).draw();
        }
    }

    self.deleteFile = function (event) {
        var rowIndex = $(event.target).closest("tr").index();
        var itemId = self.table.row(rowIndex).data()[0];

        dialogsApi.showConfirmModal("", "", function () {
            $.ajax({
                type: "POST",
                url: "/Files/DeleteFile",
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify({ id: itemId })
            }).done(function () {
                self.table.row(rowIndex).remove().draw();
            });
        });
    }

    self.getExtraData = function () { };

    return self;

}