$(document).ready(function () {
    $.ajax({
        type: 'get',
        url: '/tournament/GetTournament',
        dataType: "json",
       
        success: function (tournamentInfo) {            
            $("#tournamentDetails").show();
            $("#spinner").hide();
            $("#tournamentName").text(tournamentInfo.name);
        }

    });

    //var text = $("#btnCopy").attr('data-clipboard-text');
    //copyToClipboard(text);

    //$("#btnCopy").click(function (e) {
    //    var text = $(e.currentTarget).attr('data-clipboard-text');
    //    copyToClipboard(text);
    //});

    //function copyToClipboard(text) {
    //    var $temp = $("<input>");
    //    $("body").append($temp);
    //    $temp.val(text).select();
    //    document.execCommand("copy");
    //    $temp.remove();
    //}
});
