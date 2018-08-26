$(document).ready(function (e) {
    $.ajax({
        type: 'get',
        url: '/tournament/GetTournament',
        dataType: "json",

        success: function (tournamentInfo) {
            $("#tournamentName").text(tournamentInfo.name);
            $("#tournamentDescription").text(tournamentInfo.description);
            $("#tournamentMaxCapacity").text(tournamentInfo.maxPlayers);
            $("#tournamentStatus").text(tournamentInfo.status);
            $("#btnCopy").href = $("#btnCopy").href + tournamentInfo.tag;
            

            if (tournamentInfo.status === 'inProgress') {
                $("#tournamentStatus").css('color', 'greenyellow');
            }

            $("#tournamentCapacity").text(tournamentInfo.capacity);
            $("#tournamentPlayerCount").text(tournamentInfo.currentPlayers);

            if (tournamentInfo.open === true) {
                $("#tournamentType").text("open");
            } else {
                $("#tournamentType").text("password");
            }

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
