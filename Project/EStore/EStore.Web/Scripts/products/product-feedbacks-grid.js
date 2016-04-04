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



        $(".product-feedbacks-grid .add").click(function () {
            productFeedbackParserModal.show();
        });
    }

    self.init();

    return self;
}