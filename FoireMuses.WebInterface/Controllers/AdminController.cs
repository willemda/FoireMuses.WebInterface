using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoireMuses.Webinterface.Models;
using System.Web.Mvc;
using FoireMuses.Client;
using FoireMuses.WebInterface.Models;
using MindTouch.Tasking;
using FoireMuses.WebInterface.Attributes;

namespace FoireMuses.WebInterface.Controllers
{
	[AccessDeniedAuthorizeAttribute(Roles="ADMIN")]
	public class AdminController : FoireMusesController
	{
		//
		// GET: /Admin/

		public ActionResult Index()
		{
			return View();
		}


		public ViewResult CreateUser()
		{
			return View();
		}

		[HttpPost]
		public ActionResult CreateUser(RegisterModel model)
		{
			User user = new User();
			user.Id = model.Username;
			user.Password = model.Password;
			user.Email = model.Email;
			user.IsAdmin = model.IsAdmin;
			FoireMusesConnection connection = GetConnection();
			user = connection.CreateUser(user, new Result<User>()).Wait();
			return RedirectToAction("Index", "Home", null);
		}

		/*public ViewResult ChangeRights(string sourceId)
		{
			FoireMusesConnection connection = GetConnection();
			IList<string> users = connection.GetSource(sourceId, new Result<Source>()).Wait().CollaboratorsId;
			SourceRightsModel srm = new SourceRightsModel()
			{
				SourceId = sourceId,
				Users = users
			};
			return View(srm);
		}

		public ViewResult AjaxAddRights(string sourceId, string userId)
		{
			FoireMusesConnection connection = GetConnection();
			
		}

		public ViewResult AjaxRemoveRights(string sourceId, string userId)
		{
			FoireMusesConnection connection = GetConnection();
		}*/
	}
}
