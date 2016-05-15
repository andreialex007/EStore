﻿$(function () {

    window.cartPage = function () {

        var self = {};

        self.init = function () {
            console.log("cartPage");

            $(".items-amount")
                .numeric({ negative: false, decimal: false })
                .on("change", self.amountChange);

            $(".amount-up").click(self.amountUp);
            $(".amount-down").click(self.amountDown);
            $("td.delete .btn").click(self.deleteItem);
            $("th.delete .btn").click(self.deleteAll);
        }

        self.amountChange = function (event) {
            if ($(event.target).val() == 0) {
                $(event.target).val(1);
            }
            self.updateCart(event);
            self.calculatePositionTotal(event);
        }

        self.amountUp = function (event) {
            var parentTr = $(event.target).closest("tr");
            var currentVal = accounting.unformat(parentTr.find(".items-amount").val());
            currentVal++;
            parentTr.find(".items-amount").val(currentVal);
            self.updateCart(event);
            self.calculatePositionTotal(event);
        }

        self.amountDown = function (event) {
            var parentTr = $(event.target).closest("tr");
            var currentVal = accounting.unformat(parentTr.find(".items-amount").val());
            currentVal--;
            currentVal = currentVal == 0 ? 1 : currentVal;
            parentTr.find(".items-amount").val(currentVal);
            self.updateCart(event);
            self.calculatePositionTotal(event);
        }

        self.calculatePositionTotal = function (event) {
            var parentTr = $(event.target).closest("tr");
            var itemPrice = accounting.unformat(parentTr.find(".price").text());
            var amount = parseInt(parentTr.find("input").val());
            var total = itemPrice * amount;
            var totalFormatted = accounting.formatMoney(total, "", 0, " ", ",").toString();
            parentTr.find(".total span").text(totalFormatted);
            var sum = $.map($("tbody .total span"), function (x) { return accounting.unformat($(x).text()); }).reduce((a, b) => a + b, 0);
            $("tfoot .total").text(accounting.formatMoney(sum, "", 0, " ", ",").toString());

        }

        self.updateTimeout = null;
        self.updateCart = function (event) {
            var parentTr = $(event.target).closest("tr");
            var amount = parentTr.find("input").val();
            var id = accounting.unformat(parentTr.find("td:first").text().trim());

            clearTimeout(self.updateTimeout);
            self.updateTimeout = setTimeout(function () {

                $.ajax({
                    type: "POST",
                    url: "/Cart/SetItem",
                    contentType: "application/json",
                    data: JSON.stringify({ productId: id, amount: amount })
                }).done(function (result) {
                    $(".cart-link").replaceWith(result);
                });

            }, 500);
        }

        self.deleteItem = function (event) {
            var parentTr = $(event.target).closest("tr");
            var id = accounting.unformat(parentTr.find("td:first").text().trim());
            $(event.target).addClass("disabled");

            $.ajax({
                type: "POST",
                url: "/Cart/RemoveFromCart",
                contentType: "application/json",
                data: JSON.stringify({ productId: id })
            }).done(function (result) {
                $(".cart-link").replaceWith(result);
                parentTr.hide('fast', function () {
                    parentTr.remove();
                    if ($(".cart-items-table tbody tr").length == 0) {
                        location = "/catalog";
                    }
                });
            });
        }


        self.deleteAll = function (event) {

            Metronic.blockUI({
                boxed: true,
                message: 'Подождите...'
            });

            $.ajax({
                type: "POST",
                url: "/Cart/Clear",
                contentType: "application/json",
                dataType: "json"
            }).done(function (result) {
                location = "/catalog";
            });
        }

        self.init();

        return self;

    }();

})