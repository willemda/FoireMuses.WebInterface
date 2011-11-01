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

	public class SourcePageSearchItem : SearchResultItem
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


		public string SourceId
		{
			get
			{
				return Json.RetrieveStringCheck("sourceId");
			}
			set
			{
				Json.AddCheck("sourceId", value);
			}
		}


		public string PageNumber
		{
			get
			{
				return Json.RetrieveStringCheck("pageNumber");
			}
			set
			{
				Json.AddCheck("pageNumber", value);
			}
		}


		public string DisplayPageNumber
		{
			get
			{
				return Json.RetrieveStringCheck("displayPageNumber");
			}
			set
			{
				Json.AddCheck("displayPageNumber", value);
			}
		}

	}
}
