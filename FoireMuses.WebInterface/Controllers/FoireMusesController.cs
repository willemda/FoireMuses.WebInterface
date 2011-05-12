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
		protected FoireMusesConnection GetConnection()
		{

			FoireMusesConnection connection = new FoireMusesConnection(new XUri("http://localhost/foiremuses"), Configuration.ApiUsername, Configuration.ApiPassword);

			if (!User.Identity.IsAuthenticated)
			{
				return connection;
			}
			else
			{
				// Enables the remote process to use the user's credentials instead of this process' credentials
				//use settings to create default creditentials to be used by the server.
				//use some secret key
				connection.Impersonate(User.Identity.Name);
				return connection;
			}
		}
	}
}
