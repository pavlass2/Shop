$(document).ready(function () {
    $('ul.nav li.dropdown').hover(function () {
        $(this).find('.dropdown-menu').stop(true, true).delay(100).fadeIn(400);
    }, function () {
        $(this).find('.dropdown-menu').stop(true, true).delay(100).fadeOut(400);
        });
});
$(document).ready(function () {
    $("#add-to-cart-loader").css("visibility", "hidden");
    $("#addToCart").submit(function (event) {
        let amount = $("#add-to-cart-number").val();
        $("#details-form").css('visibility', 'hidden');
        $("#add-to-cart-loader").css('visibility', 'visible');
        let param = $("#cSharpId").val();
        $.ajax({
            type: "Post",
            //async: false,
            url: "/Details?id=" + param + "&amount=" + amount,
            beforeSend: function (xhr) { //Startup: services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },/*
                data: JSON.stringify(param),
                contentType: "application/json; charset=UTF-8",
                dataType: "json",*/
            success: OnSucces
        });
        event.preventDefault();
    });
    function OnSucces(data) {

        let itemData = data.split(";"); //0 = name; 1 = price; 2 = amount; 3 = EncryptedId
        let intAmount = parseInt(itemData[2]);


        let priceBefore = parseFloat(CommaToPoint($(".shopping-cart").attr("total-price")));

        //total price of all items
        let totalPrice = parseFloat(CommaToPoint(itemData[1]) * intAmount) + priceBefore;

        //change the line on the shopping cart dropdown
        $(".shopping-cart").html(parseInt($(".shopping-cart").attr("items-count")) + intAmount + " items for $" + totalPrice.toFixed(2) + ' <b class="caret"></b>');
        //change atribute with total price
        $(".shopping-cart").attr("total-price", totalPrice);
        //change atribute with total items amount
        $(".shopping-cart").attr("items-count", parseInt($(".shopping-cart").attr("items-count")) + intAmount);

        //hide loading circle, show add ui again
        $("#add-to-cart-number").attr("type", "number");

        //total count of items of this type
        let previousTotalAmount = parseInt($("#add-to-cart-info").attr("same-item-already-in-shopping-cart"));
        //if we didn't get number from previous line, than there is no item of this type in the shopping cart, set the value to 0
        if (isNaN(previousTotalAmount)) {
            previousTotalAmount = 0;
        }
        //total amount of this item including items added in this run
        let totalAmoutfItem = previousTotalAmount + intAmount;

        //update atribute with information about total amount of this item in shopping cart in details.cshtml under add button
        $("#add-to-cart-info").remove();
        $("#addToCart").append('<a href="Identity/Account/Manage/ShoppingCartReview" id="add-to-cart-info" same-item-already-in-shopping-cart="' + totalAmoutfItem + '"><img src="/images/shopping-cart_icon.png" width="24" height="24" alt="Shopping cart" />' + totalAmoutfItem + 'x already in your shopping cart</a>');

        //node with li for currently added item/items
        let node = '<li id="' + itemData[3] + '"><a asp-page="Details" asp-route-id="Details" asp-route="' + itemData[3] + '">' + totalAmoutfItem + 'x ' + itemData[0] + ' for $' + (totalAmoutfItem * parseFloat(CommaToPoint(itemData[1]))).toFixed(2) + '</a></li>'

        if (priceBefore === 0) {
            //delete the "empty message"
            $("#shopping-cart-dropdown-menu").empty();
            //append line with new items
            $("#shopping-cart-dropdown-menu").append(node);
            //change id to indicate the shopping cart is no longer empty
            $("#shopping-cart-dropdown-menu").attr("id", "shopping-cart-dropdown-menu");
        }
        else {
            //let nodes = $("#shopping-cart-dropdown-menu").get();
            //alert(nodes);

            if (previousTotalAmount === 0) {
                $("#shopping-cart-dropdown-menu").append(node);
            }
            else {
                $("#" + itemData[3]).replaceWith(node);
            }
            

            //next we need to update the dropdown and show it
        }
        $("#add-to-cart-loader").css('visibility', 'hidden');
        $("#details-form").css('visibility', 'visible');
        
    }
    
    //(".remove-button").click(

    $(".remove-button").click(function () {
        let param = $(this).attr("parameter");
        let count = parseInt($(this).attr("count"));
        $.ajax({
            type: "Post",
            //async: false,
            url: "/Identity/Account/Manage/ShoppingCartReview?itemId=" + param,
            beforeSend: function (xhr) { //Startup: services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },/*
                data: JSON.stringify(param),
                contentType: "application/json; charset=UTF-8",
                dataType: "json",*/
            success: function (price) {
                //alert(price);
                price = parseFloat(price);
                //alert(price);


                /*
                price = parseFloat(CommaToPoint(price));
                alert(price);
                alert(count);*/
                //let count = $("#" + param).attr("count");
                //alert(count);//undefined, mozna kvuli spatnym selectorum?
                if (count > 1) {
                    $("[parameter='" + param + "']").attr("count", count - 1);
                    $("td.in-cart-count#" + param).text((count - 1) + "x");
                    //check if to remove "total" or leave it
                    if (count === 2) {
                        $("div.in-cart-price#" + param).text("$" + ((count - 1) * price).toFixed(2));
                    }
                    else {
                        $("div.in-cart-price#" + param).text("total $" + ((count - 1) * price).toFixed(2));
                    }
                }
                else {
                    location.reload();
                }               
            }
        });
    });
    //convert decimal number: 3,14 to 3.14
    //input as string, output as string
    function CommaToPoint(commaDecimal) {
        if (commaDecimal.includes(",")) {
            let noDecimal = commaDecimal.split(",");
            let pointDecimal = noDecimal[0] + "." + noDecimal[1];
            return pointDecimal;
        }
        else return commaDecimal;
    }
});