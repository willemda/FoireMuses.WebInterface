using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using FoireMuses.Client;
using MindTouch.Dream;
using MindTouch.Tasking;

namespace FoireMuses.Webinterface
{
	public class MyMembershipProvider : SqlMembershipProvider
	{
		public override bool ValidateUser(string username, string password)
		{
			FoireMusesConnection connection = new FoireMusesConnection(new XUri("http://localhost/foiremuses"), "secretusername", "secretpassword");
			if (connection.Login(username, password, new Result<User>()).Wait() == null)
				return false;
			return true;
		}
	}
}