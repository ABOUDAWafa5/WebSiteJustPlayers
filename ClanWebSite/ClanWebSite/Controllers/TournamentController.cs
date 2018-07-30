using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClanWebSite.Services;
using System.Web.Caching;
using ClanWebSite.Common;

namespace ClanWebSite.Controllers
{
    public class TournamentController : Controller
    {

        private static InMemoryCache memoryCache = new InMemoryCache();
        // GET: Tournament
        public ActionResult Search()
        {           
            return View();
        }


        public JsonResult GetTournament()
        {
            var clashRoyaleApi = new ClashRoyaleApi();
            //var tournament = memoryCache.GetOrSet("tournament", () => clashRoyaleApi.SearchTournaments());
            var tournament = clashRoyaleApi.SearchTournaments();
            return Json(tournament, JsonRequestBehavior.AllowGet);
        }
    }
}