using ClanWebSite.Services.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;

namespace ClanWebSite.Services
{
    public class ClashRoyaleApi
    {
        private static string apiKey;
        private static HttpClient client;
        private static string apiBaseaddress = "http://api.cr-api.com/";
        private static string latestFoundTournament = string.Empty;

        public ClashRoyaleApi()
        {
            if (client == null)
            {
                apiKey = ConfigurationManager.AppSettings["apiKey"];
                client = new HttpClient { BaseAddress = new Uri(apiBaseaddress) };
            }
        }

        public static void CreateClient()
        {
            if (client == null)
            {
                apiKey = ConfigurationManager.AppSettings["apiKey"];
                client = new HttpClient { BaseAddress = new Uri(apiBaseaddress) };
            }
        }

        public TournamentInfo SearchTournaments()
        {
            //return new TournamentInfo()
            //{
            //    maxCapacity = 50,
            //    status = "open",
            //    playerCount = 20,
            //    realStartDate = DateTime.Now,
            //    type = "open",
            //    description = "descriptionm of the test",
            //    name = "name of the test"
            //};

            if (latestFoundTournament != string.Empty)
            {
               var tournamentInfo =  GetTournamentInfo(latestFoundTournament);
                if (tournamentInfo.playerCount < tournamentInfo.maxCapacity)
                {
                    return tournamentInfo;
                }
            }

            while (true)
            {
                TournamentSection result = new TournamentSection();
                var opened = GetOpenedTournaments();
                if (opened != null && opened.Any())
                {
                    result.All.AddRange(opened.Where(s => s.status.ToLower() != "inprogress"));
                }

               // var known = GetKnownTournaments();
                //if (known != null && known.Any())
                //{
                    //  result.All.AddRange(known.Where(s => s.status.ToLower() != "inprogress"));
               // }

                // var allOpened = opened.Where(s => (s.maxCapacity>s.playerCount && s.status.ToLower() != "ended"));
                //var allKnown = known.Where(s => (s.maxCapacity > s.playerCount && s.status.ToLower() != "ended"));


                result.All.Sort();
                var allSearchJoinable = result.All.Where(s => s.maxCapacity > s.capacity).ToList();
                allSearchJoinable.Sort();

                var tournamentInfo = IsTournamentActive(allSearchJoinable);
                if (tournamentInfo!=null) return tournamentInfo;


                var searched = GetActiveTournamentsbySearch("я в е р т ъ у и о п а с д ф г х");

                tournamentInfo = IsTournamentActive(searched);
                if (tournamentInfo != null) return tournamentInfo;

                searched = GetActiveTournamentsbySearch("й к л з ь ц ж б н м");

                tournamentInfo = IsTournamentActive(searched);
                if (tournamentInfo != null) return tournamentInfo;

                searched = GetActiveTournamentsbySearch("q w e r t");

                tournamentInfo = IsTournamentActive(searched);
                if (tournamentInfo != null) return tournamentInfo;

                searched = GetActiveTournamentsbySearch("1 2 3 4 5 6 7 8 9 0");

                tournamentInfo = IsTournamentActive(searched);
                if (tournamentInfo != null) return tournamentInfo;

                searched = GetActiveTournamentsbySearch("y u i o p [ ]");

                tournamentInfo = IsTournamentActive(searched);
                if (tournamentInfo != null) return tournamentInfo;

                searched = GetActiveTournamentsbySearch("a s d f g h j");

                tournamentInfo = IsTournamentActive(searched);
                if (tournamentInfo != null) return tournamentInfo;

                searched = GetActiveTournamentsbySearch("k l z x c v b");

                tournamentInfo = IsTournamentActive(searched);
                if (tournamentInfo != null) return tournamentInfo;

                searched = GetActiveTournamentsbySearch("n m ; ' , . / ?");

                tournamentInfo = IsTournamentActive(searched);
                if (tournamentInfo != null) return tournamentInfo;

                Thread.Sleep(30000);
            }
        }

        private TournamentInfo IsTournamentActive(List<TournamentInfo> allSearchJoinable)
        {
            foreach (var tournamentInfo in allSearchJoinable)
            {
                GetTournamentInfo(tournamentInfo.tag);
                if (tournamentInfo.playerCount < tournamentInfo.maxCapacity && tournamentInfo.status.ToLower() =="inprogress")
                {
                    {
                        TournamentInfo.SetRealStartDate(new List<TournamentInfo> {tournamentInfo});
                        return tournamentInfo;
                    }
                }
            }
            return null;
        }

        public TournamentSection GetTournaments()
        {
            TournamentSection result = new TournamentSection();
            var opened = GetOpenedTournaments();
            if (opened != null && opened.Any())
            {
                result.All.AddRange(opened.Where(s => s.status.ToLower() != "ended"));
            }

            var known = GetKnownTournaments();
            var allKnown = new List<TournamentInfo>();
            if (known != null && known.Any())
            {
                //  result.All.AddRange(known.Where(s => s.status.ToLower() != "ended"));
                allKnown = known.Where(s => (s.maxCapacity > s.playerCount && s.status.ToLower() != "ended")).ToList();
            }

            // var allOpened = opened.Where(s => (s.maxCapacity>s.playerCount && s.status.ToLower() != "ended"));
           


            result.All.Sort();
            var allSearchJoinable = result.All.Where(s => s.maxCapacity > s.capacity).ToList();
            allSearchJoinable.Sort();

            List<TournamentInfo> realCheckedJoinable = new List<TournamentInfo>();
            foreach (var tournamentInfo in allSearchJoinable)
            {
                GetTournamentInfo(tournamentInfo.tag);
                if (tournamentInfo.playerCount < tournamentInfo.maxCapacity)
                {
                    realCheckedJoinable.Add(tournamentInfo);
                    break;
                }
            }

            // var promotionTournaments = allSearchJoinable.Skip(0).Take(6).ToList();

             var promotionTournaments = allSearchJoinable.Skip(0).Take(6).ToList();
            
            promotionTournaments.Sort();
            TournamentInfo.SetRealStartDate(promotionTournaments);

            int count = 1;
            result.TournamentPromotion = new List<TournamentPromotion>();
            var promotion = new TournamentPromotion();
            foreach (var promotionTournament in allSearchJoinable)
            {
                if (count == 1)
                {
                    promotion.Title = "Open";
                    result.TournamentPromotion.Add(promotion);
                    promotion.TournamentLeft = promotionTournament;
                }
                if (count == 2)
                {
                    promotion.TournamentMiddle = promotionTournament;
                }
                if (count == 3)
                {
                    promotion.TournamentRight = promotionTournament;
                }
                if (count == 4)
                {
                    promotion = new TournamentPromotion {Title = "Highest Players Capacity"};
                    result.TournamentPromotion.Add(promotion);
                    promotion.TournamentLeft = promotionTournament;
                }
                if (count == 5)
                {
                    promotion.TournamentMiddle = promotionTournament;
                }
                if (count == 6)
                {
                    promotion.TournamentRight = promotionTournament;
                }

                count++;
            }
            result.All.AddRange(allKnown);



            return result;
        }

        private List<TournamentInfo> GetOpenedTournaments()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "tournaments/open");
            requestMessage.Headers.Add("Auth", apiKey);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var result = client.SendAsync(requestMessage).Result;






            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var stringResult = result.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<TournamentInfo>>(stringResult);
            }
            else
            {
                return null;
            }

        }

        private List<TournamentInfo> GetActiveTournamentsbySearch(string name)
        {
            //https://api.royaleapi.com/tournaments/search?name=a b v g d e
            var address = $"tournaments/search?name={name}";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, address);
            requestMessage.Headers.Add("Auth", apiKey);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var result = client.SendAsync(requestMessage).Result;
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var stringResult = result.Content.ReadAsStringAsync().Result;
                var tournaments = JsonConvert.DeserializeObject<List<TournamentInfo>>(stringResult);


                var activeTournaments = tournaments?.Where(p => p.maxCapacity > p.playerCount && p.status.ToLower() == "inprogress" && p.type == "open");
                return activeTournaments?.ToList();
            }
            else
            {
                return new List<TournamentInfo>();
            }
        }

        private List<TournamentInfo> GetKnownTournaments()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "tournaments/known");
            requestMessage.Headers.Add("Auth", apiKey);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var result = client.SendAsync(requestMessage).Result;
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var stringResult = result.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<TournamentInfo>>(stringResult);
            }
            else
            {
                return null;
            }
        }

        private TournamentInfo GetTournamentInfo(string tag)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"tournaments/{tag}");
            requestMessage.Headers.Add("Auth", apiKey);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var result = client.SendAsync(requestMessage).Result;
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var stringResult = result.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<TournamentInfo>(stringResult);
            }
            else
            {
                return null;
            }
        }

    }
}
