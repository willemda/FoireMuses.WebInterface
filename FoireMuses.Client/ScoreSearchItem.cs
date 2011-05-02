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

	public class ScoreSearchItem
	{

		private JObject json { get; set; }

		public ScoreSearchItem()
		{
		}

		public ScoreSearchItem(JObject jobject)
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



		public string Title
		{
			get
			{
				return json.RetrieveStringCheck("title");
			}
			set
			{
				json.AddCheck("title", value);
			}
		}




		public string Composer
		{
			get
			{
				return json.RetrieveStringCheck("composer");
			}
			set
			{
				json.AddCheck("composer", value);
			}
		}



		public string Verses
		{
			get
			{
				return json.RetrieveStringCheck("verses");
			}
			set
			{
				json.AddCheck("verses", value);
			}
		}



		public string Editor
		{
			get
			{
				return json.RetrieveStringCheck("editor");
			}
			set
			{
				json.AddCheck("editor", value);
			}
		}
	}
}
