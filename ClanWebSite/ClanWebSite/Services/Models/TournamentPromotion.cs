using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClanWebSite.Services.Models
{
    public class TournamentPromotion
    {
        public TournamentInfo TournamentLeft { get; set; }
        public TournamentInfo TournamentMiddle { get; set; }
        public TournamentInfo TournamentRight { get; set; }
    }
}