function ProductImagesGrid() {

    var self = {}


    self.init = function () {

        self.table = $(".product-images-grid table").DataTable({
            paging: false,
            order: [
                   [0, "asc"]
            ]
        });

        $(document).on("change", ".product-images-grid .file-upload-input", self.uploadFile);
        $(document).on("click", ".product-images-grid .delete", self.deleteImage);
    }

    self.uploadFile = function (event) {

        var request = new XMLHttpRequest();
        var data = new FormData();
        data.append("file", event.target.files[0]);
        data.append("productId", $(".product-id").val());
        request.open("POST", "/Products/UploadImage/", true);
        request.setRequestHeader("X-Requested-With", "XMLHttpRequest");
        request.addEventListener("load", self.imageUploaded, false);
        request.send(data);

    }

    self.imageUploaded = function (xhr) {
        debugger;
        var json = JSON.parse(this.responseText);
        var dataArr = utils.tableRowToArray(json.view);
        self.table.row.add(dataArr).draw();
    }

    self.deleteImage = function (event) {
        var rowIndex = $(event.target).closest("tr").index();
        var itemId = self.table.row(rowIndex).data()[0];

        dialogsApi.showConfirmModal("", "", function () {
            $.ajax({
                type: "POST",
                url: "/Products/DeleteImage",
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify({ id: itemId })
            }).done(function () {
                self.table.row(rowIndex).remove().draw();
            });
        });
    }

    self.init();

    return self;

}