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

	public class SourcePageSearchItem
	{

		private JObject json { get; set; }

		public SourcePageSearchItem()
		{
		}

		public SourcePageSearchItem(JObject jobject)
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


		public string SourceId
		{
			get
			{
				return json.RetrieveStringCheck("sourceId");
			}
			set
			{
				json.AddCheck("sourceId", value);
			}
		}


		public string PageNumber
		{
			get
			{
				return json.RetrieveStringCheck("pageNumber");
			}
			set
			{
				json.AddCheck("pageNumber", value);
			}
		}


		public string DisplayPageNumber
		{
			get
			{
				return json.RetrieveStringCheck("displayPageNumber");
			}
			set
			{
				json.AddCheck("displayPageNumber", value);
			}
		}

	}
}
