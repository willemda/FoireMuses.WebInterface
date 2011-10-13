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
		private readonly JObject theJson;

		public ScoreSearchItem()
		{
		}

		public ScoreSearchItem(JObject aJObject)
		{
			theJson = aJObject;
		}

		public string Id
		{
			get
			{
				return theJson.RetrieveStringCheck("_id");
			}
			set
			{
				theJson.AddCheck("_id", value);
			}
		}

		public string Title
		{
			get
			{
				return theJson.RetrieveStringCheck("title");
			}
			set
			{
				theJson.AddCheck("title", value);
			}
		}

		public string Composer
		{
			get
			{
				return theJson.RetrieveStringCheck("composer");
			}
			set
			{
				theJson.AddCheck("composer", value);
			}
		}

		public string Verses
		{
			get
			{
				return theJson.RetrieveStringCheck("verses");
			}
			set
			{
				theJson.AddCheck("verses", value);
			}
		}

		public string Editor
		{
			get
			{
				return theJson.RetrieveStringCheck("editor");
			}
			set
			{
				theJson.AddCheck("editor", value);
			}
		}

		public string MusicalSourceReferenceText
		{
			get
			{
				return theJson.RetrieveStringCheck("musicalSourceReferenceText");
			}
			set
			{
				theJson.AddCheck("musicalSourceReferenceText", value);
			}
		}

		public string TextualSourceReferenceText
		{
			get
			{
				return theJson.RetrieveStringCheck("textualSourceReferenceText");
			}
			set
			{
				theJson.AddCheck("textualSourceReferenceText", value);
			}
		}
	}
}
