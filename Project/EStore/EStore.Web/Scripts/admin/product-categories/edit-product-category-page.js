$(function () {

    window.editProductCategoryPage = function () {

        var self = {};

        self.init = function () {
            console.log("editProductCategoryPage");

            var categoryId = $(".parent-category input[type='hidden']").data("selected-id");
            $(".parent-category input[type='hidden']").select2Custom({
                placeholder: "Выберите родительскую категорию",
                data: $(".parent-category input[type='hidden']").data("items")
            }).select2("val", categoryId);
        }

        self.init();

        return self;

    }();
})