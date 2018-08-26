using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoyaleApi.Models
{
    public class TournamentSection
    {
        public List<TournamentInfo> All { get; set; }
        public List<TournamentPromotion> TournamentPromotion { get; set; }

        public TournamentSection()
        {
            All = new List<TournamentInfo>();
        }
    }
}