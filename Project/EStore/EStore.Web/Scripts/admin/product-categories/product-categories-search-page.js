$(function () {

    window.productCategoriesSearchPage = function () {

        var self = new SearchGridBase();
        self.deleteUrl = "/admin/ProductCategories/Delete";

        self.init = function () {

            self.table = $(".categories-search-page table").DataTable({
                aoColumnDefs: [
                    {
                        bSortable: false,
                        aTargets: [-1]
                    }
                ],
                order: [
                    [0, "asc"]
                ]
            });

            $(document.body).on("click", ".categories-search-page td:last-of-type .delete", self.delete);
        }

        self.init();

        return self;

    }();

})