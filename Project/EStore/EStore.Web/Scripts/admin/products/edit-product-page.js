$(function () {

    window.editProductPage = function () {

        var self = {};
        self.productImagesGrid = null;
        self.productSinglesGrid = null;
        self.productFeedbacksGrid = null;

        self.init = function () {

            $('textarea').summernote({
                toolbar: [
                    ['style', ['style']],
                    ['font', ['bold', 'italic', 'underline', 'superscript', 'subscript', 'strikethrough', 'clear']],
                    ['fontname', ['fontname']],
                    ['fontsize', ['fontsize']],
                    ['color', ['color']],
                    ['para', ['ul', 'ol', 'paragraph']],
                    ['height', ['height']],
                    ['table', ['table']],
                    ['insert', ['link', 'picture', 'video', 'hr']],
                    ['view', ['codeview']]
                ]
            });

            self.productImagesGrid = new ProductImagesGrid();
            self.productSinglesGrid = new ProductSinglesGrid();
            self.productFeedbacksGrid = new ProductFeedbacksGrid();

            $(".find-images-button").click(function () {
                $(".images-search-modal .search-term").val($(".product-name-box input").val());
                window.imagesSearchModal.show();
                window.imagesSearchModal.onImagesUploaded = self.onImagesUploaded;
            });

            var categoryId = $(".product-category input[type='hidden']").data("selected-id");
            $(".product-category input[type='hidden']").select2Custom({
                placeholder: "Выберите категорию",
                data: $(".product-category input[type='hidden']").data("items")
            }).select2("val", categoryId);
        }

        self.onImagesUploaded = function (params) {
            self.productImagesGrid.filesUploaded(params.views);
        }

        self.init();

        return self;

    }();


});