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

	public class SourceSearchItem
	{

		private JObject json { get; set; }

		public SourceSearchItem()
		{
		}

		public SourceSearchItem(JObject jobject)
		{
			json = jobject;
		}

		public string Id
		{
			get
			{
				return json.RetrieveStringCheck("_id");
			}
			set
			{
				json.AddCheck("_id", value);
			}
		}



		public string Name
		{
			get
			{
				return json.RetrieveStringCheck("name");
			}
			set
			{
				json.AddCheck("name", value);
			}
		}




		public string Publisher
		{
			get
			{
				return json.RetrieveStringCheck("publisher");
			}
			set
			{
				json.AddCheck("publisher", value);
			}
		}



		public string DateFrom
		{
			get
			{
				return json.RetrieveStringCheck("dateFrom");
			}
			set
			{
				json.AddCheck("dateFrom", value);
			}
		}



		public string DateTo
		{
			get
			{
				return json.RetrieveStringCheck("dateTo");
			}
			set
			{
				json.AddCheck("dateTo", value);
			}
		}
	}
}
