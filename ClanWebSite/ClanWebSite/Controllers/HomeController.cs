using ClanWebSite.Models;
using DataBase;
using RoyaleApi;
using RoyaleApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClanWebSite.Controllers
{
    public class HomeController : Controller
    {
        private ClashRoyaleApi clashRoyaleApi = new ClashRoyaleApi();
        public ActionResult Index()
        {
            HomePageViewModel viewModel = new HomePageViewModel { ClanMembers = GetClanStatistics() };            
            viewModel.Tournaments = clashRoyaleApi.GetTournaments();

            return View(viewModel);
        }

        private List<ClanMember> GetClanStatistics()
        {
            using (var entities = new ClanManagerEntities())
            {

                var dbMembers = entities.Member;               

                List<ClanWar> latestWars = clashRoyaleApi.GetLatestClanWars();
                var lastInfoPlayers = clashRoyaleApi.GetClanInfo();

                List<ClanMember> members = new List<ClanMember>();

                foreach (var player in lastInfoPlayers.members)
                {

                    var clanMember = new ClanMember
                    {
                        Name = player.name,
                        Tag = player.tag
                    };
                    members.Add(clanMember);

                    var dbMember = dbMembers.FirstOrDefault(s => s.Tag == player.tag);

                    if (dbMember != null)
                    {
                        clanMember.JoinDate = dbMember.JoinDate;
                    }
                    else
                    {
                        clanMember.JoinDate = DateTime.Now;
                    }

                }

                if (latestWars != null)
                {
                    foreach (ClanWar war in latestWars)
                    {
                        foreach (var participant in war.participants)
                        {
                            var clanMember = members.FirstOrDefault(s => s.Tag == participant.tag);
                            if (clanMember != null)
                            {

                                clanMember.ClanWars += participant.battlesPlayed;
                                clanMember.ClanWins += participant.wins;

                                clanMember.Percent = Math.Round((((double)(clanMember.ClanWins * 100)) / clanMember.ClanWars), 2);
                            }
                        }
                    }
                }

                List<ClanMember> loyalMembers = members.Where(s => s.ClanWars > 5).ToList();
                var newMembers = members.Where(s => s.ClanWars <= 5).ToList();

                loyalMembers =loyalMembers.OrderByDescending(p => p.Percent).ToList();
                newMembers= newMembers.OrderByDescending(p => p.Percent).ToList();
                loyalMembers.AddRange(newMembers);
                return loyalMembers;
            }
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