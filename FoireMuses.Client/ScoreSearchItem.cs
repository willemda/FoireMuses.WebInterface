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
	public class ScoreSearchItem : SearchResultItem
	{
		private CompleteTextualSource theCompleteTextualSource;
		private CompleteMusicalSource theCompleteMusicalSource;

		public string Id
		{
			get
			{
				return Json.RetrieveStringCheck("_id");
			}
		}

		public string Title
		{
			get
			{
				return Json.RetrieveStringCheck("title");
			}
		}

		public string Composer
		{
			get
			{
				return Json.RetrieveStringCheck("composer");
			}
		}

		public string Verses
		{
			get
			{
				return Json.RetrieveStringCheck("verses");
			}
		}

		public string Editor
		{
			get
			{
				return Json.RetrieveStringCheck("editor");
			}
		}

		public CompleteMusicalSource MusicalSource
		{
			get
			{
				if (theCompleteMusicalSource == null)
					theCompleteMusicalSource = new CompleteMusicalSource((JObject)Json["musicalSource"]);
				return theCompleteMusicalSource;
			}
		}

		public CompleteTextualSource TextualSource
		{
			get
			{
				if (theCompleteTextualSource == null)
					theCompleteTextualSource = new CompleteTextualSource((JObject)Json["textualSource"]);
				return theCompleteTextualSource;
			}
		}
	}
}
