$(document).ready(function () {
    $.ajax({
        type: 'get',
        url: '/tournament/GetTournament',
        dataType: "json",

        success: function (tournamentInfo) {
            $("#tournamentName").text(tournamentInfo.name);
            $("#tournamentDescription").text(tournamentInfo.description);
            $("#tournamentMaxCapacity").text(tournamentInfo.maxCapacity);
            $("#tournamentStatus").text(tournamentInfo.status);

            if (tournamentInfo.status === 'opened') {
                $("#tournamentStatus").css('color', 'greenyellow');
            }

            $("#tournamentCapacity").text(tournamentInfo.capacity);
            $("#tournamentPlayerCount").text(tournamentInfo.playerCount);
            $("#tournamentType").text(tournamentInfo.type);
            $("#tournamentStarted").text(new Date(parseInt(tournamentInfo.realStartDate.substr(6))).toLocaleTimeString([], { hour12: false, hourCycle: '24h' }));
            $("#btnCopy").attr('data-clipboard-text', tournamentInfo.name);

            $("#tournamentDetails").show();
            $("#spinner").hide();

        }
    });

    $("#btnCopy").click(function (e) {
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
