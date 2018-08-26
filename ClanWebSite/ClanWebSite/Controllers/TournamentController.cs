using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Caching;
using ClanWebSite.Common;
using RoyaleApi;

namespace ClanWebSite.Controllers
{
    public class TournamentController : Controller
    {
        private ClashRoyaleApi clashRoyaleApi = new ClashRoyaleApi();
        private static InMemoryCache memoryCache = new InMemoryCache();
        // GET: Tournament
        public ActionResult Search()
        {           
            return View();
        }


        public JsonResult GetTournament()
        {                    
            var tournament = clashRoyaleApi.SearchTournaments();
            return Json(tournament, JsonRequestBehavior.AllowGet);
        }
    }
}