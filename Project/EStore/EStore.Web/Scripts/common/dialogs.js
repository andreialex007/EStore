window.dialogsApi = function () {
    var self = {};

    self.showConfirmModal = function (title, content, funcSuccess) {
        title = title || "Confirmation";
        content = content || "Are you really want to do action?";

        $(".confirm-modal .modal-title").text(title);
        $(".confirm-modal .modal-body p").text(content);

        $(".confirm-modal").show();
        $(".cancel-button")
            .unbind()
            .click(function () {
                $(".confirm-modal").hide();
            });

        $(".ok-button").unbind()
            .click(function () {
                funcSuccess();
                $(".confirm-modal").hide();
            });
    }

    self.showInfoModal = function (title, content) {
        title = title || "Information";
        $(".information-modal .modal-title").text(title);
        $(".information-modal .modal-body p").text(content);
        $(".information-modal").modal('show');
    }

    return self;
}();