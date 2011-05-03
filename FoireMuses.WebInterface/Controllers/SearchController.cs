using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoireMuses.Webinterface.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/

        public ActionResult Index(string type)
        {
            switch (type)
            {
                case "score":
                    return View("SearchScore");
                case "play":
                    return View("SearchPlay");
                case "music":
                    return View("SearchMusic");
                case "source":
                    return View("SearchSource");
                default:
                    return View("SearchScore");
            }
        }



    }
}
