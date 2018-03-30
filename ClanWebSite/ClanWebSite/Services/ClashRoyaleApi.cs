using ClanWebSite.Services.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ClanWebSite.Services
{
    public class ClashRoyaleApi
    {
        private static string apiKey;
        private static HttpClient client;
        private static string apiBaseaddress = "http://api.cr-api.com/";

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

        public TournamentSection GetTournaments()
        {
            TournamentSection result = new TournamentSection();
            var opened = GetOpenedTournaments();
            if (opened != null && opened.Any())
            {
                result.All.AddRange(opened.Where(s => s.status.ToLower() != "ended"));
            }

            var known = GetKnownTournaments();
            if (known != null && known.Any())
            {
                //  result.All.AddRange(known.Where(s => s.status.ToLower() != "ended"));
            }

            // var allOpened = opened.Where(s => (s.maxCapacity>s.playerCount && s.status.ToLower() != "ended"));
            var allKnown = known.Where(s => (s.maxCapacity > s.playerCount && s.status.ToLower() != "ended"));


            result.All.Sort();
            var joinable = result.All.Where(s => s.maxCapacity > s.capacity).ToList();
            joinable.Sort();

            var fisrtObjest = joinable.FirstOrDefault();

            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(long.Parse(fisrtObjest.startTime.ToString())).ToLocalTime();


            var starttime = long.Parse(fisrtObjest.startTime.ToString());
            var endTime = long.Parse(fisrtObjest.endTime.ToString());
            var timespent = (int)DateTime.Now.ToLocalTime().Subtract(new DateTime(1970, 1, 1)).TotalSeconds - (long.Parse(fisrtObjest.startTime.ToString()));
            var total = long.Parse(endTime.ToString()) - long.Parse(starttime.ToString());

            var promotionTournaments = joinable.Skip(0).Take(6).ToList();
            promotionTournaments.Sort();
            TournamentInfo.SetRealStartDate(promotionTournaments);

            int count = 1;
            result.TournamentPromotion = new List<TournamentPromotion>();
            var promotion = new TournamentPromotion();
            foreach (var promotionTournament in promotionTournaments)
            {
                if (count == 1)
                {
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
                    promotion = new TournamentPromotion();
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

            var current = (decimal)timespent / total * 100;
            //int secondsremaining = (int)(timespent / ((starttime-endTime)/100) * (progressBar1.Maximum - progressBar1.Value));


            // result.All.AddRange(allOpened);
            result.All.AddRange(allKnown);



            return result;
        }

        private List<TournamentInfo> GetOpenedTournaments()
        {
            //var requestMessage = new HttpRequestMessage(HttpMethod.Get, "tournaments/open");
            //requestMessage.Headers.Add("Auth", apiKey);
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //var result = client.SendAsync(requestMessage).Result;






            //if (result.StatusCode == System.Net.HttpStatusCode.OK)
            //{
            //    var stringResult = result.Content.ReadAsStringAsync().Result;

            //    return JsonConvert.DeserializeObject<List<TournamentInfo>>(stringResult);
            //}
            //else
            //{
            //    return null;
            //}

            //https://api.royaleapi.com/tournaments/search?name=a b v g d e
            var address = "tournaments/search?name=a";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, address);
            requestMessage.Headers.Add("Auth", apiKey);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var result = client.SendAsync(requestMessage).Result;
            var stringResult = result.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<List<TournamentInfo>>(stringResult);

        }

        private List<TournamentInfo> GetKnownTournaments()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, " tournaments/known");
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

    }
}
