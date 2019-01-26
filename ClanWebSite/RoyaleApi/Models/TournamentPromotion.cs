using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoyaleApi.Models
{
    public class TournamentPromotion
    {
        public string Title { get; set; }
        public TournamentInfo TournamentLeft { get; set; }
        public TournamentInfo TournamentMiddle { get; set; }
        public TournamentInfo TournamentRight { get; set; }

        public TournamentPromotion()
        {
            Title = string.Empty;
            TournamentLeft = new TournamentInfo();
            TournamentMiddle = new TournamentInfo();
            TournamentRight = new TournamentInfo();
        }

    }
}