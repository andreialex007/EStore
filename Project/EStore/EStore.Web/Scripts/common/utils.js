$(function () {
    window.utils = function () {
        var self = {};

        jQuery.fn.outerHTML = function () {
            return jQuery('<div />').append(this.eq(0).clone()).html();
        };

        self.select2Focus = function () {
            var select2 = $(this).data('select2');
            setTimeout(function () {
                if (!select2.opened()) {
                    select2.open();
                }
            }, 5);
        }

        self.validateEmail = function (email) {
            var re = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;
            return re.test(email);
        }

        self.addGoogleAutocomplete = function (inputSelector, placeChanged) {
            var options = {
                componentRestrictions: { country: "au" }
            };
            var autocomplete = new google.maps.places.Autocomplete($(inputSelector)[0], options);
            autocomplete.setTypes(['address']);

            var func = function () {
                console.log("changed by google");
                (placeChanged || function () { }).call(this);
            };

            google.maps.event.addListener(autocomplete, 'place_changed', func);
        }

        self.addGoogleAutocompleteAutoFillInputs = function (input, postCodeEl, latEl, longEl, extraFunction) {
            extraFunction = extraFunction || function () { };

            $(input).on("change", function () {
                $(postCodeEl).val("");
                $(latEl).val("");
                $(longEl).val("");
            });
            self.addGoogleAutocomplete(input, function () {
                self.applyGooglePostCodeLatLonToInputs.call(this, postCodeEl, latEl, longEl);
                extraFunction.call(this);
            });
        }

        self.applyGooglePostCodeLatLonToInputs = function (postCodeEl, latEl, longEl) {

            var place = this.getPlace();
            var addressHtml = place.adr_address;
            var postCode = "";
            addressHtml = "<span>" + addressHtml + "</span>";
            var postalCodeNote = $.grep($(addressHtml).find("span"), function (x) { return $(x).is(".postal-code"); })[0];
            if (postalCodeNote) {
                postCode = $(postalCodeNote).text();
            }

            var location = place.geometry.location;
            var lattitude = location.lat();
            var longitude = location.lng();

            $(postCodeEl).val(postCode);
            $(latEl).val(parseFloat(lattitude.toFixed(8)));
            $(longEl).val(parseFloat(longitude.toFixed(8)));
        }

        self.initSelect2AutoOpenInContainer = function (parentElement) {
            var allElements = !parentElement ? $(".select2-container") : $(parentElement).find(".select2-container");
            $(allElements).next().one('select2-focus', self.select2Focus)
                .on("select2-blur", function (event) {
                    $(this).one('select2-focus', self.select2Focus);
                });
        }

        self.initSelect2AutoOpenSingle = function (element) {
            $(element).one('select2-focus', self.select2Focus).on("select2-blur", function () {
                $(this).one('select2-focus', self.select2Focus);
            });
        }

        self.init = function () {

            if ($.fn.dataTable) {
                $.extend(true, $.fn.dataTable.defaults, {
                    "language": {
                        "url": "/Content/assets/global/plugins/datatables/Russian.json"
                    },
                    "pageLength": 100
                });
            }
            

            String.prototype.capitalizeFirstLetter = function () {
                return this.charAt(0).toUpperCase() + this.slice(1);
            }

            $.fn.select2Custom = function (options) {
                $(this).data("select2Custom", options);
                $(this).select2(options);
                return this;
            }
            $.fn.select2getData = function (element) {
                return $(this).data("select2").opts.data;
            }
            $.fn.select2setData = function (data) {
                var options = $(this).last().data("select2Custom");
                options.data = data;
                $(this).select2(options);
                return $(this).last();
            }
            self.showSavedMessage();

            window.QueryString = function () {
                // This function is anonymous, is executed immediately and 
                // the return value is assigned to QueryString!
                var query_string = {};
                var query = window.location.search.substring(1);
                var vars = query.split("&");
                for (var i = 0; i < vars.length; i++) {
                    var pair = vars[i].split("=");
                    // If first entry with this name
                    if (typeof query_string[pair[0]] === "undefined") {
                        query_string[pair[0]] = decodeURIComponent(pair[1]);
                        // If second entry with this name
                    } else if (typeof query_string[pair[0]] === "string") {
                        var arr = [query_string[pair[0]], decodeURIComponent(pair[1])];
                        query_string[pair[0]] = arr;
                        // If third or later entry with this name
                    } else {
                        query_string[pair[0]].push(decodeURIComponent(pair[1]));
                    }
                }
                return query_string;
            }();
        }

        self.showSavedMessage = function (messageText) {
            if ($.cookie("Saved")) {
                $.removeCookie("Saved", { path: '/' });
                UIToastr.ShowMessage("success", "Saved", "Successfully saved", true);
            }
        }

        self.tableRowToArray = function (rowHtml) {

            var dataArr = [];
            $(rowHtml).find("td").each(function (index, element) {
                dataArr.push($(element).html());
            });

            return dataArr;
        }

        self.uploadFile = function (url, file, extraData, completedFunc) {

            extraData = extraData || {};
            completedFunc = completedFunc || function () { };

            var request = new XMLHttpRequest();
            var data = new FormData();
            data.append("file", file);

            for (var i in extraData) {
                var item = extraData[i];
                data.append(i, item);
            }

            request.open("POST", url, true);
            request.setRequestHeader("X-Requested-With", "XMLHttpRequest");
            request.addEventListener("load", completedFunc, false);
            request.send(data);
        }



        self.filesCounter = 0;
        self.uploadManyFiles = function (url, files, extraData, completedFunc) {

            Metronic.blockUI({
                boxed: true,
                message: "Загрузка.."
            });

            var results = [];

            $.each(files, function (i, file) {
                self.filesCounter += 1;
                self.uploadFile(url, file, extraData, function (xhr) {
                    results.push(JSON.parse(this.responseText));
                    self.filesCounter -= 1;
                    if (self.filesCounter == 0) {
                        Metronic.unblockUI();
                        completedFunc(results);
                    }
                });
            });
        }

        self.init();
        return self;
    }();
});