using ClanWebSite.Models;
using ClanWebSite.Services;
using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClanWebSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HomePageViewModel viewModel = new HomePageViewModel {ClanMembers = GetClanStatistics().OrderByDescending(p => p.Points).ToList() };

            var clashRoyaleApi = new ClashRoyaleApi();
            viewModel.Tournaments = clashRoyaleApi.GetTournaments();

            return View(viewModel);
        }

        private static List<ClanMember> GetClanStatistics()
        {
            var now = DateTime.Now;
            DateTime today = new DateTime(now.Year, now.Month, now.Day);
            var todayTicks = today.Ticks;
            var monthsBefore = today.AddMonths(-1).Ticks;

            List<ClanMember> viewMember = new List<ClanMember>();
            using (var entities = new ClanManagerEntities())
            {
                var allMembers = entities.MemberHistory
                    .Where(p => p.Date <= todayTicks && p.Date > monthsBefore && p.Member.IsStillInTheClan).GroupBy(t => t.Tag);
                var clanDonations = entities.MemberHistory
                    .Where(p => p.Date <= todayTicks && p.Date > monthsBefore && p.Member.IsStillInTheClan)
                    .Sum(t => t.Donations);
                var clanChestTrophies = entities.MemberHistory
                    .Where(p => p.Date <= todayTicks && p.Date > monthsBefore && p.Member.IsStillInTheClan)
                    .Sum(t => t.ClanChestCrowns);


                foreach (var member in allMembers)
                {
                    var memberMain = entities.Member.FirstOrDefault(t => t.Tag == member.Key);

                    var memberChestTrophies = member.Sum(s => s.ClanChestCrowns);
                    var memberChestTrophiesPercantage = ((decimal) memberChestTrophies / (decimal) clanChestTrophies) * 100;

                    var memberChestDonations = member.Sum(s => s.Donations);
                    var memberChestDonationsPercantege = ((decimal) memberChestDonations / (decimal) clanDonations) * 100;


                    var latestClanRank = memberMain.ClanRank;
                    var clanRankPoints = ((decimal) (50 - latestClanRank) / (decimal) 1275) * 100;

                    viewMember.Add(new ClanMember
                    {                        
                        Donations = memberChestDonations,
                        Rank = latestClanRank,
                        Name = memberMain.Name,
                        Trophies = memberMain.Trophies,
                        Points = Math.Round(memberChestTrophiesPercantage + memberChestDonationsPercantege + clanRankPoints, 2)
                    });
                }
            }
            return viewMember;
        }

        [HttpPost]
        public ActionResult Save(string message)
        {

            if (!string.IsNullOrEmpty(message))
            {
                using (var entities = new ClanManagerEntities())
                {
                    entities.Suggestion.Add(new Suggestion
                    {
                        Message = message,
                        DateTime = DateTime.Now
                    });

                    entities.SaveChanges();
                }
            }
                return Json("Success");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}