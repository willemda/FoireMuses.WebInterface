using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoireMuses.Webinterface.Controllers
{
	public class ErrorController : Controller
	{
		//
		// GET: /Error/

		public ActionResult Problem()
		{
			ViewBag.Error = "Un problème est survenu, merci de contacter l'équipe technique.";
			return View("Error");
		}

		public ActionResult Missing()
		{
			ViewBag.Error = "Désolé, mais la page que vous voulez atteindre n'existe pas.";
			return View("Error");
		}

	}
}
