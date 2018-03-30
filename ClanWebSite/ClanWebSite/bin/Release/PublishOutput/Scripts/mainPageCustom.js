$(function () {
    $("#btnSubmit").click(function (e) {
        var textboxMessage = $("#txbSuggestion").val();    

        $.ajax(
            {
                type: "POST",
                data: { message: textboxMessage },
                url: "Home/Save",
                success: function () {
                    $("#txbSuggestion").val('');
                    $('#modalAlert').modal('show');
                }
            })
    });

});