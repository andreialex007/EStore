$(function () {
    window.articlesSearchPage = function () {

        var self = {};
        self.articlesGrid = null;

        self.init = function () {
            console.log("articlesSearchPage init");

            self.articlesGrid = new ArticlesGrid();
        }

        self.init();

        return self;

    }();
})