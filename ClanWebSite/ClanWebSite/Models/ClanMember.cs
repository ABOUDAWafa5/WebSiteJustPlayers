using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClanWebSite.Models
{
    public class ClanMember
    {
        public int Rank { get; set; }
        public string Name { get; set; }
        public int Trophies { get; set; }
        public int ClanChestCrowns { get; set; }
        public int Donations { get; set; }
        public string Tag { get; set; }
        public decimal Points { get; set; }
    }
}