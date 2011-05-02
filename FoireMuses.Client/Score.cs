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

	public class Score
	{

		private JObject json { get; set; }

		public Score()
		{
			json = new JObject();
			json.Add("otype", "score");
		}

		public Score(JObject jobject)
		{
			json = jobject;
			JToken type;
			if (json.TryGetValue("otype", out type))
			{
				if (type.Value<string>() != "score")
					throw new Exception("Bad object type");
			}
			else
			{
				json.Add("otype", "score");
			}
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

		public string Rev
		{
			get
			{
				return json.RetrieveStringCheck("_rev");
			}
			set
			{
				json.AddCheck("_rev", value);
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

		public string CodeMelodiqueRISM
		{
			get
			{
				return json.RetrieveStringCheck("codageMelodiqueRISM");
			}
			set
			{
				json.AddCheck("codageMelodiqueRISM", value);
			}
		}

		public string CodageParIntervalles
		{
			get
			{
				return json.RetrieveStringCheck("codageParIntervalles");
			}
			set
			{
				json.AddCheck("codageParIntervalles", value);
			}
		}

		public string CodageRythmique
		{
			get
			{
				return json.RetrieveStringCheck("codageRythmique");
			}
			set
			{
				json.AddCheck("codageRythmique", value);
			}
		}

		public string Coirault
		{
			get
			{
				return json.RetrieveStringCheck("coirault");
			}
			set
			{
				json.AddCheck("coirault", value);
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

		public string CoupeMetrique
		{
			get
			{
				return json.RetrieveStringCheck("coupeMetrique");
			}
			set
			{
				json.AddCheck("coupeMetrique", value);
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

		public string Delarue
		{
			get
			{
				return json.RetrieveStringCheck("delarue");
			}
			set
			{
				json.AddCheck("delarue", value);
			}
		}

		public string Comments
		{
			get
			{
				return json.RetrieveStringCheck("comments");
			}
			set
			{
				json.AddCheck("comments", value);
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

		public string RythmSignature
		{
			get
			{
				return json.RetrieveStringCheck("rythmSignature");
			}
			set
			{
				json.AddCheck("rythmSignature", value);
			}
		}

		public string OtherTitles
		{
			get
			{
				return json.RetrieveStringCheck("otherTitles");
			}
			set
			{
				json.AddCheck("otherTitles", value);
			}
		}

		public string Stanza
		{
			get
			{
				return json.RetrieveStringCheck("stanza");
			}
			set
			{
				json.AddCheck("stanza", value);
			}
		}

		public string ScoreType
		{
			get
			{
				return json.RetrieveStringCheck("type");
			}
			set
			{
				json.AddCheck("type", value);
			}
		}

		public TextualSource TextualSource
		{
			get
			{
				if (json["textualSource"] == null)
					return null;
				return new TextualSource(json["textualSource"].Value<JObject>());
			}
			set
			{

				if (value != null)
				{
					json["textualSource"] = value.json;
				}
				else
					json.Remove("textualSource");
			}
		}

		public MusicalSource MusicalSource
		{
			get
			{
				if (json["musicalSource"] == null)
					return null;
				return new MusicalSource(json["musicalSource"].Value<JObject>());
			}
			set
			{
				if (value != null)
				{
					json["musicalSource"] = value.json;
				}
				else
					json.Remove("musicalSource");
			}
		}

		public IList<string> Tags
		{
			get
			{
				if (json["tags"] == null)
					return null;
				return json["tags"].Values<string>().ToList<string>();
			}

			set
			{
				json["tags"] = new JArray(value);
			}
		}

		public string CreatorId
		{
			get
			{
				return json.RetrieveStringCheck("creatorId");
			}
			set
			{
				json.AddCheck("creatorId", value);
			}
		}

		public string LastModifierId
		{
			get
			{
				return json.RetrieveStringCheck("lastModifierId");
			}
			set
			{
				json.AddCheck("lastModifierId", value);
			}
		}

		public IList<string> CollaboratorsId
		{
			get
			{
				if (json["collaboratorsId"] == null)
					return null;
				return json["collaboratorsId"].Values<string>().ToList<string>();
			}
			set
			{
				json["collaboratorsId"] = new JArray(value);
			}
		}

		public bool HasAttachement
		{
			get { return json["_attachments"] != null; }
		}

		public IEnumerable<string> GetAttachmentNames()
		{
			var attachment = json["_attachments"];
			return attachment == null ? null : attachment.Select(x => x.Value<JProperty>().Name);
		}

		public override string ToString()
		{
			string jsonS = json.ToString();
			return jsonS;
		}
	}
}
