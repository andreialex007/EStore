function ImagesGridBase() {

    var self = new FilesGridBase();

    self.gridSelector = ".product-images-grid";

    var initBase = self.init;
    self.init = function () {
        initBase();
        self.imagesInit();
        $(document.body).on("click", ":radio[name='is-main']", self.mainSelected);
    }

    self.mainSelected = function (event) {
        var id = $(event.target).val();
    }

    var filesUploadedBase = self.filesUploaded;
    self.filesUploaded = function (result) {
        filesUploadedBase(result);
        $("*[id='imgPreviewContainer']").remove();
        self.initPreview();
    }

    self.initPreview = function () {
        if ($(".image-link-item").length > 0) {
            $(".image-link-item").unbind().imgPreview({
                imgCSS: {
                    width: '200px'
                }
            });
        }
    }

    self.imagesInit = function () {

        $(document).on("focus", self.gridSelector + " tbody .image-description-input", function (event) {
            var imageLinkItem = $(event.target).closest("tr").find('.image-link-item');
            imageLinkItem.trigger("mouseover");
            var offset = imageLinkItem.offset();
            offset.top += imageLinkItem.height();
            $("#imgPreviewContainer").offset(offset);
        });
        $(document).on("blur", self.gridSelector + " tbody .image-description-input", function (event) {
            var parentTr = $(event.target).closest("tr");
            var id = parentTr.find("td:first").text().trim();
            parentTr.find('.image-link-item').trigger("mouseout");
            self.saveFileDescription(id, $(event.target).val());
        });

        self.initPreview();
    }

    self.getExtraData = function () {
        return { productId: $(".entity-id").val() };
    }

    return self;

}