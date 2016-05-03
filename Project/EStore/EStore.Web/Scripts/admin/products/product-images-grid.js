function ProductImagesGrid() {

    var self = new ImagesGridBase();

    self.gridSelector = ".common-files-grid";

    self.getExtraData = function () {
        return { productId: $(".entity-id").val() };
    }

    self.init();

    return self;

}