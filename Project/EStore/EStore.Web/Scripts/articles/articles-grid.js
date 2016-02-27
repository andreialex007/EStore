function ArticlesGrid() {

    var self = {};


    self.init = function () {
        console.log("ArticlesGrid init");

        self.table = $(".articles-grid table").DataTable({
            aoColumnDefs: [
                {
                    bSortable: false,
                    aTargets: [-1]
                }
            ]
        });

        $(document.body).on("click", ".articles-search-page td:last-of-type .edit", self.edit);
    }

    self.edit = function (event) {
        var rowIndex = $(event.target).closest("tr").index();
        var itemId = self.table.row(rowIndex).data()[0];
        location.href = "/Articles/" + itemId;
    }

    self.init();

    return self;

}