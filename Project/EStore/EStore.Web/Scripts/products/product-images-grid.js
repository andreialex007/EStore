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

        $(document).on("focus", ".product-images-grid tbody input[type='text']", function (event) {
            var imageLinkItem = $(event.target).closest("tr").find('.image-link-item');
            imageLinkItem.trigger("mouseover");
            var offset = imageLinkItem.offset();
            offset.top += imageLinkItem.height();
            $("#imgPreviewContainer").offset(offset);
        });
        $(document).on("blur", ".product-images-grid tbody input[type='text']", function (event) {
            var parentTr = $(event.target).closest("tr");
            var id = parentTr.find("td:first").text().trim();
            parentTr.find('.image-link-item').trigger("mouseout");
            self.saveImageDescription(id, $(event.target).val());
        });

        self.initPreview();
    }

    self.saveImageDescription = function (id, text) {

        $.ajax({
            type: "POST",
            url: "/Products/SaveImageDescription",
            contentType: "application/json",
            dataType: "json",
            data: JSON.stringify({ id: id, text: text })
        }).done(function () {

        });
    }

    self.uploadFile = function (event) {
        utils.uploadManyFiles("/Products/UploadImage/", event.target.files, { productId: $(".product-id").val() }, self.imagesUploaded);
    }

    self.imagesUploaded = function (result) {
        for (var i in result) {
            var item = result[i];
            var dataArr = utils.tableRowToArray(item.view);
            self.table.row.add(dataArr).draw();
        }
        $("*[id='imgPreviewContainer']").remove();
        self.initPreview();
    }

    self.initPreview = function () {
        $(".image-link-item").unbind().imgPreview({
            imgCSS: {
                width: '200px'
            }
        });
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