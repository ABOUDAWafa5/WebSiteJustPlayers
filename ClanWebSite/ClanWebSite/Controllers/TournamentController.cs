using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClanWebSite.Controllers
{
    public class TournamentController : Controller
    {
        // GET: Tournament
        public ActionResult Search()
        {
            return View();
        }
    }
}