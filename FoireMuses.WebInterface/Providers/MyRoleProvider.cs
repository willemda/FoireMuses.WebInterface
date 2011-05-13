using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using FoireMuses.Client;
using MindTouch.Tasking;
using MindTouch.Dream;
using FoireMuses.Webinterface.Configurations;

namespace FoireMuses.Webinterface
{
	public class MyRoleProvider : RoleProvider
	{
		private string[] Roles = new
		string[]{
			"ADMIN",
			"MEMBER"
		};

		public override void AddUsersToRoles(string[] usernames, string[] roleNames)
		{
			throw new NotImplementedException();
		}

		public override string ApplicationName
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public override void CreateRole(string roleName)
		{
			throw new NotImplementedException();
		}

		public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
		{
			throw new NotImplementedException();
		}

		public override string[] FindUsersInRole(string roleName, string usernameToMatch)
		{
			throw new NotImplementedException();
		}

		public override string[] GetAllRoles()
		{
			return Roles;
		}

		public override string[] GetRolesForUser(string username)
		{
            FoireMusesConnection connection = new FoireMusesConnection(new XUri(Configuration.ApiUrl + ":" + Configuration.ApiPort + "/" + Configuration.ApiAt), Configuration.ApiUsername, Configuration.ApiPassword);
			User user = connection.GetUser(username, new Result<User>()).Wait();
			if (user.IsAdmin)
				return new string[] { "ADMIN", "MEMBER" };
			else
				return new string[] { "MEMBER" };
		}

		public override string[] GetUsersInRole(string roleName)
		{
			throw new NotImplementedException();
		}

		public override bool IsUserInRole(string username, string roleName)
		{
			if (roleName == "MEMBER")
				return true;
            FoireMusesConnection connection = new FoireMusesConnection(new XUri(Configuration.ApiUrl + ":" + Configuration.ApiPort + "/" + Configuration.ApiAt), Configuration.ApiUsername, Configuration.ApiPassword);
			User user = connection.GetUser(username, new Result<User>()).Wait();
			if (user.IsAdmin)
				return true;
			return false;
		}

		public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
		{
			throw new NotImplementedException();
		}

		public override bool RoleExists(string roleName)
		{
			switch (roleName)
			{
				case "MEMBER":
					return true;
				case "ADMIN":
					return true;
				default:
					return false;
			}
		}
	}
}