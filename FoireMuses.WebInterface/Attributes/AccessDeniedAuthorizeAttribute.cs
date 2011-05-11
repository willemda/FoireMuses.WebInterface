using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoireMuses.WebInterface.Attributes
{
	public class AccessDeniedAuthorizeAttribute : AuthorizeAttribute
	{
		public override void OnAuthorization(AuthorizationContext filterContext)
		{
			base.OnAuthorization(filterContext);
			if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
			{
				filterContext.Result = new RedirectResult("~/Users/Login");
				return;
			}

			if (filterContext.Result is HttpUnauthorizedResult)
			{
				filterContext.Result = new RedirectResult("~/Users/Denied");
			}
		}
	}
}