$(function () {

    window.adminUsersSearchPage = function () {

        var self = new SearchGridBase();
        self.deleteUrl = "/admin/AdminUsers/Delete";

        self.init = function () {

            self.table = $(".admin-users-search-page table").DataTable({
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

            $(document.body).on("click", ".admin-users-search-page td:last-of-type .delete", self.delete);
        }

        self.init();

        return self;

    }();

})