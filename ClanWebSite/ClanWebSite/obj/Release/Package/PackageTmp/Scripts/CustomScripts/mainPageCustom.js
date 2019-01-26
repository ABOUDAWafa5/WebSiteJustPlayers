$(window).scroll(function () {
    if ($(this).scrollTop() > 5) {
        $(".navbar-me").addClass("fixed-me");
    } else {
        $(".navbar-me").removeClass("fixed-me");
    }
});

$(function () {
    $("#btnSubmit").click(function (e) {
        var textboxMessage = $("#txbSuggestion").val();

        $.ajax(
            {
                type: "POST",
                data: { message: textboxMessage },
                url: "Home/Save",
                success: function() {
                    $("#txbSuggestion").val('');
                    $('#modalAlert').modal('show');
                }
            });
    });

    $(".btnClipboard").click(function (e) {
        var text = $(e.currentTarget).attr('data-clipboard-text');
        copyToClipboard(text);
    });

    $("#toDiscord").click(function (e) {
        var chatDiv = $('#chatDiv');               
        if (chatDiv.is(':visible')) {
            chatDiv.hide();
        } else {
            chatDiv.show();
        }
        e.preventDefault()
    });



    $(".nav-item.nav-link").click(function (e) {
        $(".nav-item.nav-link").removeClass('active');
        $(".tab-pane").removeClass('show');
        
        $(".nav-item.nav-link").removeClass('active');
        $(e.target).addClass('active');
    });


    

    function copyToClipboard(text) {
        var $temp = $("<input>");
        $("body").append($temp);
        $temp.val(text).select();
        document.execCommand("copy");
        $temp.remove();
    }

});