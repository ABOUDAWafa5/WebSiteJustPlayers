using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyaleApi.Models
{

    public class Location
    {
        public string name { get; set; }
        public bool isCountry { get; set; }
        public string code { get; set; }
    }

    public class Arena
    {
        public string name { get; set; }
        public string arena { get; set; }
        public int arenaID { get; set; }
        public int trophyLimit { get; set; }
    }

    public class Member
    {
        public string name { get; set; }
        public string tag { get; set; }
        public int rank { get; set; }
        public int previousRank { get; set; }
        public string role { get; set; }
        public int expLevel { get; set; }
        public int trophies { get; set; }
        public int donations { get; set; }
        public int donationsReceived { get; set; }
        public int donationsDelta { get; set; }
        public Arena arena { get; set; }
        public double donationsPercent { get; set; }
    }

    public class Tracking
    {
        public bool active { get; set; }
        public bool available { get; set; }
        public int snapshotCount { get; set; }
    }

    public class ClanInfo
    {
        public string tag { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public int score { get; set; }
        public int memberCount { get; set; }
        public int requiredScore { get; set; }
        public int donations { get; set; }
        public Badge badge { get; set; }
        public Location location { get; set; }
        public List<Member> members { get; set; }
        public Tracking tracking { get; set; }
    }
}
