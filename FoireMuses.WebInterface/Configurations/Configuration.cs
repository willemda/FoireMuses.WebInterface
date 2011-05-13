using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace FoireMuses.Webinterface.Configurations
{
	public class Configuration
	{
		public static string ApiUsername
		{
			get { return ConfigurationManager.AppSettings["ApiUsername"]; }
		}

		public static string ApiPassword
		{
			get { return ConfigurationManager.AppSettings["ApiPassword"]; }
		}
	}
}