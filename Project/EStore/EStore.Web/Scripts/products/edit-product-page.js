$(function () {

    window.editProductPage = function () {

        var self = {};
        self.productImagesGrid = null;

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

        }

        self.init();

        return self;

    }();


});