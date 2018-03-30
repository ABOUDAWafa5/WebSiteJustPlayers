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