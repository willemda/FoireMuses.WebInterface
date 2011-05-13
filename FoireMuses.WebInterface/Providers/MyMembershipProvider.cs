using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using FoireMuses.Client;
using MindTouch.Dream;
using MindTouch.Tasking;
using FoireMuses.Webinterface.Configurations;

namespace FoireMuses.Webinterface
{
	public class MyMembershipProvider : SqlMembershipProvider
	{
		public override bool ValidateUser(string username, string password)
		{
            FoireMusesConnection connection = new FoireMusesConnection(new XUri(Configuration.ApiUrl + ":" + Configuration.ApiPort + "/" + Configuration.ApiAt), Configuration.ApiUsername, Configuration.ApiPassword);
			if (connection.Login(username, password, new Result<User>()).Wait() == null)
				return false;
			return true;
		}
	}
}