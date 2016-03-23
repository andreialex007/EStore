$(function () {

    window.imagesSearchModal = function () {

        var self = {};

        self.init = function () {
            console.log("imagesSearchModal");
            $(".search-img-btn-modal").click(function () {
                self.search();
            });

            $(".images-search-modal .search-term").keyup(function (event) {
                if (event.keyCode == 13) {
                    self.search();
                }
            });
        }

        self.show = function () {
            self.search(function () {
                $(".images-search-modal").modal("show");
            });
        }

        self.hide = function () {
            $(".images-search-modal").modal("hide");
        }

        self.search = function (completeFunc) {
            completeFunc = completeFunc || function () { };

            var term = $(".images-search-modal .search-term").val();

            Metronic.blockUI({
                boxed: true,
                message: "Загрузка.."
            });
            $.ajax({
                type: "POST",
                url: "/Files/SearchImages",
                contentType: "application/json",
                data: JSON.stringify({ term: term })
            }).done(function (result) {
                completeFunc();
                Metronic.unblockUI();
                $(".images-search-results").html(result);
            });
        }

        self.init();

        return self;

    }();

})