
$(function () {

    window.productDetails = function () {

        var self = {};

        self.init = function () {

            $("#owl-demo").owlCarousel({

                items: 4,
                pagination: false,
                navigation: true,
                navigationText: [
                    "<i class='fa fa-chevron-left'></i>",
                    "<i class='fa fa-chevron-right'></i>"
                ]

            });

            $(".fancybox").fancybox();

            $("#owl-demo img").hover(self.hoverImage);

            $(".more-info-about-delivery").click(function () {
                $("a[href='#delivery']").click();
            });
        }

        self.hoverImage = function (event) {
            var imgSrc = $(event.target).attr("src");
            $(".main-img img").prop("src", imgSrc);
            $(".main-img a").prop("href", imgSrc);
        }

        self.init();

        return self;

    }();


});