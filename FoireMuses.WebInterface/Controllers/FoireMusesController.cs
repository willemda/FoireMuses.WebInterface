using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoireMuses.Client;
using MindTouch.Dream;
using System.Web.Security;
using FoireMuses.Webinterface.Configurations;

namespace FoireMuses.WebInterface.Controllers
{
	public class FoireMusesController : Controller
	{
		private FoireMusesConnection theConnection;

		protected FoireMusesConnection FoireMusesConnection 
		{
			get
			{
				if (theConnection == null)
				{
					theConnection = new FoireMusesConnection(
						new XUri(Configuration.ApiUrl + ":" + Configuration.ApiPort + "/" + Configuration.ApiAt),
						Configuration.ApiUsername,
						Configuration.ApiPassword);
					if (User.Identity.IsAuthenticated)
					{
						theConnection.Impersonate(User.Identity.Name);
					}
				}
				return theConnection;
			}
		}
	}
}
