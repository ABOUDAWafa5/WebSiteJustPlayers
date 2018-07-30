using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClanWebSite.Controllers
{
    public class VideoController : Controller
    {
        // GET: Video
        public ActionResult All()
        {
            return View("Video");
        }
    }
}