using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace FoireMuses.Client
{
	/// <summary>
	/// represent a Score(un air) object in json
	/// </summary>
	public class Score : JObject
	{

		public Score()
		{
			this.Add("otype", "score");
		}

		public Score(JObject jobject)
			: base(jobject)
		{
			JToken type;
			if (this.TryGetValue("otype", out type))
			{
				if (type.Value<string>() != "score")
					throw new Exception("Bad object type");
			}
			else
			{
				this.Add("otype", "score");
			}
		}

		public string Id
		{
			get
			{
				if (this["_id"] == null)
					return null;
				return this["_id"].Value<string>();
			}
		}

        
		public string Title
		{
			get
			{
				if (this["title"] == null)
					return null;
				return this["title"].Value<string>();
			}
			set { this["title"] = value; }
		}

		public string Code1
		{
			get
			{
				if (this["code1"] == null)
					return null;
				return this["code1"].Value<string>();
			}
			set { this["code1"] = value; }
		}

		public string Code2
		{
			get
			{
				if (this["code2"] == null)
					return null;
				return this["code2"].Value<string>();
			}
			set { this["code2"] = value; }
		}

		public string Coirault
		{
			get
			{
				if (this["coirault"] == null)
					return null;
				return this["coirault"].Value<string>();
			}
			set { this["coirault"] = value; }
		}

		public string Composer
		{
			get
			{
				if (this["composer"] == null)
					return null;
				return this["composer"].Value<string>();
			}
			set { this["composer"] = value; }
		}

		public string CoupeMetrique
		{
			get
			{
				if (this["coupeMetrique"] == null)
					return null;
				return this["coupeMetrique"].Value<string>();
			}
			set { this["coupeMetrique"] = value; }
		}

		public string Verses
		{
			get
			{
				if (this["verses"] == null)
					return null;
				return this["verses"].Value<string>();
			}
			set { this["verses"] = value; }
		}

		public string Delarue
		{
			get
			{
				if (this["delarue"] == null)
					return null;
				return this["delarue"].Value<string>();
			}
			set { this["delarue"] = value; }
		}

		public string Comments
		{
			get
			{
				if (this["comments"] == null)
					return null;
				return this["comments"].Value<string>();
			}
			set { this["comments"] = value; }
		}

		public string Editor
		{
			get
			{
				if (this["editor"] == null)
					return null;
				return this["editor"].Value<string>();
			}
			set { this["editor"] = value; }
		}

		public string RythmSignature
		{
			get
			{
				if (this["rythmSignature"] == null)
					return null;
				return this["rythmSignature"].Value<string>();
			}
			set { this["rythmSignature"] = value; }
		}

		public string OtherTitles
		{
			get
			{
				if (this["otherTitles"] == null)
					return null;
				return this["otherTitles"].Value<string>();
			}
			set { this["otherTitles"] = value; }
		}

		public string Stanza
		{
			get
			{
				if (this["stanza"] == null)
					return null;
				return this["stanza"].Value<string>();
			}
			set { this["stanza"] = value; }
		}

		public string ScoreType
		{
			get
			{
				if (this["type"] == null)
					return null;
				return this["type"].Value<string>();
			}
			set { this["type"] = value; }
		}

		public TextualSource TextualSource
		{
			get
			{
				if (this["textualSource"] == null)
					return null;
				return new TextualSource(this["textualSource"].Value<JObject>());
			}
			set { this["textualSource"] = value; }
		}

		public MusicalSource MusicalSource
		{
			get
			{
				if (this["musicalSource"] == null)
					return null;
				return new MusicalSource(this["musicalSource"].Value<JObject>());
			}
			set { this["musicalSource"] = value; }
		}

		public IEnumerable<string> Tags
		{
			get
			{
				if (this["tags"] == null)
					return null;
				return this["tags"].Values<string>();
			}
		}

		public void AddTag(string tag)
		{
			if (!Tags.Contains(tag))
			{
				JArray temp = this["tags"].Value<JArray>();
				temp.Add(tag);
				this["tags"] = temp;
			}
		}

		public void RemoveTag(string tag)
		{
			this["tags"] = this["tags"].Value<JArray>().Remove(tag);
		}

		public string CreatorId
		{
			get
			{
				if (this["creatorId"] == null)
					return null;
				return this["creatorId"].Value<string>();
			}
			private set { this["creatorId"] = value; }
		}

		public string LastModifierId
		{
			get
			{
				if (this["lastModifierId"] == null)
					return null;
				return this["lastModifierId"].Value<string>();
			}
			private set { this["lastModifierId"] = value; }
		}

		public IEnumerable<string> CollaboratorsId
		{
			get
			{
				if (this["collaboratorsId"] == null)
					return null;
				return this["collaboratorsId"].Values<string>();
			}
		}

		public void AddCollaborator(string collab)
		{
			if (!Tags.Contains(collab))
			{
				JArray temp = this["collaboratorsId"].Value<JArray>();
				temp.Add(collab);
				this["collaboratorsId"] = temp;
			}
		}

		public void RemoveCollaborator(string collab)
		{
			this["collaboratorsId"] = this["collaboratorsId"].Value<JArray>().Remove(collab);
		}
	}
}
