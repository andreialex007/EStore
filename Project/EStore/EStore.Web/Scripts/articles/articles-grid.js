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

    }

    self.init();

    return self;

}