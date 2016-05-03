function SupplierInvoiceFilesGrid() {

    var self = new ImagesGridBase();

    self.gridSelector = ".common-files-grid";

    self.getExtraData = function () {
        return { supplierInvoiceId: $(".entity-id").val() };
    }

    self.init();

    return self;

}