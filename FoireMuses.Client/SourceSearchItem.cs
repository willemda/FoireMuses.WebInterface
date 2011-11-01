using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using FoireMuses.Client.Helpers;

namespace FoireMuses.Client
{
	/// <summary>
	/// represent a Score(un air) object in json
	/// </summary>

	public class SourceSearchItem : SearchResultItem
	{
		public string Id
		{
			get
			{
				return Json.RetrieveStringCheck("_id");
			}
			set
			{
				Json.AddCheck("_id", value);
			}
		}

		public string Name
		{
			get
			{
				return Json.RetrieveStringCheck("name");
			}
			set
			{
				Json.AddCheck("name", value);
			}
		}

		public string Publisher
		{
			get
			{
				return Json.RetrieveStringCheck("publisher");
			}
			set
			{
				Json.AddCheck("publisher", value);
			}
		}

		public string DateFrom
		{
			get
			{
				return Json.RetrieveStringCheck("dateFrom");
			}
			set
			{
				Json.AddCheck("dateFrom", value);
			}
		}

		public string DateTo
		{
			get
			{
				return Json.RetrieveStringCheck("dateTo");
			}
			set
			{
				Json.AddCheck("dateTo", value);
			}
		}
	}
}
