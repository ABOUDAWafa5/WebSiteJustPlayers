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

    function copyToClipboard(text) {
        var $temp = $("<input>");
        $("body").append($temp);
        $temp.val(text).select();
        document.execCommand("copy");
        $temp.remove();
    }

});