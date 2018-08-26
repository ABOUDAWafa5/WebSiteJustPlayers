using System;

namespace ClanWebSite.Models
{

    public class ClanMember
    {
        public string Tag { get; set; }
        public string Name { get; set; }
        public int ClanWins { get; set; }
        public int ClanWars { get; set; }
        public double Percent { get; set; }
        public DateTime JoinDate { get; set; }
    }
}