$(function () {

    window.productFeedbackParserModal = function () {

        var self = {};

        self.init = function () {


            $(document.body).on("click", ".feedback-parser-modal .select-feedbacks-button", self.selectFeedbacks);
            $(document.body).on("click", ".feedback-parser-modal .close-button", self.cancel);
            $(document.body).on("click", ".feedback-parser-modal .open-feedback-search-page .btn", self.openFeedbacksSearchPage);
            $(document.body).on("click", ".feedback-parser-modal .start-parsing-btn", self.startParsing);
        }

        self.openFeedbacksSearchPage = function (event) {
            var searchPage = $(".feedback-parser-modal li.active a").data().searchPage;
            var searchUrl = searchPage + encodeURIComponent($(".product-name-box input").val());
            var win = window.open(searchUrl, '_blank');
            win.focus();
        }

        self.startParsing = function (event) {
            var url = $(event.target).closest(".input-group").find("input").val();
            Metronic.blockUI({
                boxed: true,
                message: "Загрузка..."
            });
            $.ajax({
                type: "POST",
                url: "/admin/Products/ParseFeedbacks",
                contentType: "application/json",
                data: JSON.stringify({ url: url })
            }).done(function (result) {
                Metronic.unblockUI();
                $(event.target).closest(".tab-pane").find(".parsing-result-items").html(result);
            });
        }

        self.selectFeedbacks = function (event) {

            var values = $.map($(event.target).closest(".modal-content").find(":checkbox:checked"), function (x) { return $(x).val(); });

            Metronic.blockUI({
                boxed: true,
                message: "Загрузка..."
            });
            $.ajax({
                type: "POST",
                url: "/admin/Products/SelectFeedbacks",
                contentType: "application/json",
                data: JSON.stringify({ ids: values, productId: $(".entity-id").val() })
            }).done(function (result) {
                Metronic.unblockUI();
                self.feedbacksAdded(result);
            });

        }

        self.feedbacksAdded = function (resultk) { }

        self.cancel = function () {
            self.hide();
        }

        self.hide = function () {
            $(".feedback-parser-modal").modal("hide");
        }

        self.show = function () {
            $(".feedback-parser-modal").modal("show");
        }

        self.init();

        return self;

    }();

})