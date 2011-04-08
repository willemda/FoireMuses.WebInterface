using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using FoireMuses.Core.Interfaces;

namespace FoireMuses.Client
{
	/// <summary>
	/// represent a Score(un air) object in json
	/// </summary>
	
	public class Score : IScore
	{

		public JObject json { get; private set; }

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
				if (json["_id"] == null)
					return null;
				return json["_id"].Value<string>();
			}
		}


		public string Title
		{
			get
			{
				if (json["title"] == null)
					return null;
				return json["title"].Value<string>();
			}
			set { json["title"] = value; }
		}

		public string Code1
		{
			get
			{
				if (json["code1"] == null)
					return null;
				return json["code1"].Value<string>();
			}
			set { json["code1"] = value; }
		}

		public string Code2
		{
			get
			{
				if (json["code2"] == null)
					return null;
				return json["code2"].Value<string>();
			}
			set { json["code2"] = value; }
		}

		public string Coirault
		{
			get
			{
				if (json["coirault"] == null)
					return null;
				return json["coirault"].Value<string>();
			}
			set { json["coirault"] = value; }
		}

		public string Composer
		{
			get
			{
				if (json["composer"] == null)
					return null;
				return json["composer"].Value<string>();
			}
			set { json["composer"] = value; }
		}

		public string CoupeMetrique
		{
			get
			{
				if (json["coupeMetrique"] == null)
					return null;
				return json["coupeMetrique"].Value<string>();
			}
			set { json["coupeMetrique"] = value; }
		}

		public string Verses
		{
			get
			{
				if (json["verses"] == null)
					return null;
				return json["verses"].Value<string>();
			}
			set { json["verses"] = value; }
		}

		public string Delarue
		{
			get
			{
				if (json["delarue"] == null)
					return null;
				return json["delarue"].Value<string>();
			}
			set { json["delarue"] = value; }
		}

		public string Comments
		{
			get
			{
				if (json["comments"] == null)
					return null;
				return json["comments"].Value<string>();
			}
			set { json["comments"] = value; }
		}

		public string Editor
		{
			get
			{
				if (json["editor"] == null)
					return null;
				return json["editor"].Value<string>();
			}
			set { json["editor"] = value; }
		}

		public string RythmSignature
		{
			get
			{
				if (json["rythmSignature"] == null)
					return null;
				return json["rythmSignature"].Value<string>();
			}
			set { json["rythmSignature"] = value; }
		}

		public string OtherTitles
		{
			get
			{
				if (json["otherTitles"] == null)
					return null;
				return json["otherTitles"].Value<string>();
			}
			set { json["otherTitles"] = value; }
		}

		public string Stanza
		{
			get
			{
				if (json["stanza"] == null)
					return null;
				return json["stanza"].Value<string>();
			}
			set { json["stanza"] = value; }
		}

		public string ScoreType
		{
			get
			{
				if (json["type"] == null)
					return null;
				return json["type"].Value<string>();
			}
			set { json["type"] = value; }
		}

		public ITextualSource TextualSource
		{
			get
			{
				if (json["textualSource"] == null)
					return null;
				return new TextualSource(json["textualSource"].Value<JObject>());
			}
			set { json["textualSource"] = (value as TextualSource).json; }
		}

		public IMusicalSource MusicalSource
		{
			get
			{
				if (json["musicalSource"] == null)
					return null;
				return new MusicalSource(json["musicalSource"].Value<JObject>());
			}
			set { json["musicalSource"] = (value as MusicalSource).json; }
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
				if (json["creatorId"] == null)
					return null;
				return json["creatorId"].Value<string>();
			}
			private set { json["creatorId"] = value; }
		}

		public string LastModifierId
		{
			get
			{
				if (json["lastModifierId"] == null)
					return null;
				return json["lastModifierId"].Value<string>();
			}
			private set { json["lastModifierId"] = value; }
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
	}
}
