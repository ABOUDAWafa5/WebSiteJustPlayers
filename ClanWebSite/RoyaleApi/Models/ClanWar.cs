using System.Collections.Generic;

namespace RoyaleApi.Models
{
    public class Participant
    {
        public string tag { get; set; }
        public string name { get; set; }
        public int cardsEarned { get; set; }
        public int battlesPlayed { get; set; }
        public int wins { get; set; }
    }

    public class Badge
    {
        public string name { get; set; }
        public string category { get; set; }
        public int id { get; set; }
        public string image { get; set; }
    }

    public class Standing
    {
        public string tag { get; set; }
        public string name { get; set; }
        public int participants { get; set; }
        public int battlesPlayed { get; set; }
        public int wins { get; set; }
        public int crowns { get; set; }
        public int warTrophies { get; set; }
        public int warTrophiesChange { get; set; }
        public Badge badge { get; set; }
    }

    public class ClanWar
    {
        public int createdDate { get; set; }
        public List<Participant> participants { get; set; }
        public List<Standing> standings { get; set; }
        public int seasonNumber { get; set; }
    }
}
