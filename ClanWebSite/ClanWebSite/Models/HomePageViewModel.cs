using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RoyaleApi.Models;

namespace ClanWebSite.Models
{
    public class HomePageViewModel
    {
        public List<ClanMember> ClanMembers { get; set; }
        public TournamentSection Tournaments { get; set; }
    }
}