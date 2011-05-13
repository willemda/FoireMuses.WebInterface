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

        public static string ApiUrl
        {
            get { return ConfigurationManager.AppSettings["ApiUrl"]; }
        }

        public static string ApiPort
        {
            get { return ConfigurationManager.AppSettings["ApiPort"]; }
        }

        public static string ApiAt
        {
            get { return ConfigurationManager.AppSettings["ApiAt"]; }
        }
	}
}