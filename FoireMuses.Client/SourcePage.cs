using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using FoireMuses.Client.Helpers;

namespace FoireMuses.Client
{
	public class SourcePage
	{
		internal JObject json { get; private set; }

		public SourcePage()
		{
			json = new JObject();
		}

		public SourcePage(JObject jobject)
		{
			json = jobject;
		}

		public string Id
		{
			get { return json.RetrieveStringCheck("_id"); }
			set { json.AddCheck("_id", value); }
		}

		public string Rev
		{
			get { return json.RetrieveStringCheck("_rev"); }
			set { json.AddCheck("_rev", value); }
		}

		public int? PageNumber
		{
			get { return json.RetrieveIntCheck("pageNumber"); }
			set { json.AddCheck("pageNumber",value); }
		}

		public int? DisplayPageNumber
		{
			get { return json.RetrieveIntCheck("displayPageNumber"); }
			set { json.AddCheck("displayPageNumber", value); }
		}

		public int? PageNumberFormat
		{
			get { return json.RetrieveIntCheck("pageNumberFormat"); }
			set { json.AddCheck("pageNumberFormat", value); }
		}

		public string TextContent
		{
			get { return json.RetrieveStringCheck("textContent"); }
			set { json.AddCheck("textContent", value); }
		}


		public string SourceId
		{
			get { return json.RetrieveStringCheck("sourceId"); }
			set { json.AddCheck("sourceId", value); }
		}

		public string ToString()
		{
			return json.ToString();
		}
	}
}
