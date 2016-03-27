$(function () {

    window.imagesSearchModal = function () {

        var self = {};

        self.init = function () {

            $(".search-img-btn-modal").click(function () {
                self.search();
            });

            $(".images-search-modal .search-term").keyup(function (event) {
                if (event.keyCode == 13) {
                    self.search();
                }
            });

            $(document.body).on("click", ".images-search-modal .ok-button", self.selectImages);
            $(document.body).on("click", ".images-search-modal .cancel-button", self.cancel);
        }

        self.cancel = function () {
            $(".images-search-results").empty();
            self.hide();
        }

        self.selectImages = function (event) {
            var selectedImages = $.map($(".search-results :checked"), function (x) { return $(x).closest("li").find("a").attr("href"); });
            var productId = $(".entity-id").val();

            Metronic.blockUI({
                boxed: true,
                message: "Загрузка.."
            });
            $.ajax({
                type: "POST",
                url: "/Files/UploadFoundImages",
                contentType: "application/json",
                data: JSON.stringify({ images: selectedImages, productId: productId })
            }).done(function (result) {
                self.onImagesUploaded(result);
                self.hide();
                Metronic.unblockUI();
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

        self.onImagesUploaded = function () { }

        self.init();

        return self;

    }();

})