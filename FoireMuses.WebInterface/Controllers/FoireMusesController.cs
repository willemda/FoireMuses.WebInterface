using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoireMuses.Client;
using MindTouch.Dream;

namespace FoireMuses.WebInterface.Controllers
{
    public class FoireMusesController : Controller
    {
		protected FoireMusesConnection GetConnection()
		{
			string userName = "danny";//HttpContext.User.Identity.Name;
			if (String.IsNullOrEmpty(userName))
			{
				throw new ApplicationException(String.Format("Unable to authenticate the user with IP address {0} and user agent {1}", HttpContext.Request.UserHostAddress, HttpContext.Request.UserAgent));
			}

			// Enables the remote process to use the user's credentials instead of this process' credentials
			//use settings to create default creditentials to be used by the server.
			return new FoireMusesConnection(new XUri("http://localhost/foiremuses"), "danny", "azerty").Impersonate(userName);
		}

    }
}
