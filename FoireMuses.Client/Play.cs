using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FoireMuses.Client.Helpers;
using Newtonsoft.Json.Linq;

namespace FoireMuses.Client
{
	/// <summary>
	/// represent a Play(une pièce) object in json
	/// </summary>
	public class Play 
	{

		public JObject json { get; private set; }

		public Play()
		{
			json = new JObject();
			json.Add("otype", "play");
		}

		public Play(JObject jobject)
		{
			json = jobject;
			JToken type;
			if (json.TryGetValue("otype", out type))
			{
				if (type.Value<string>() != "play")
					throw new Exception("Bad object type");
			}
			else
			{
				json.Add("otype", "play");
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

		public string Abstract
		{
			get
			{
				if (json["abstract"] == null)
					return null;
				return json["abstract"].Value<string>();
			}
			set
			{
				json["abstract"] = value;
			}
		}

		public string ActionLocation
		{
			get
			{
				if (json["actionLocation"] == null)
					return null;
				return json["actionLocation"].Value<string>();
			}
			set
			{
				json["actionLocation"] = value;
			}
		}

		public string Author
		{
			get
			{
				if (json["author"] == null)
					return null;
				return json["author"].Value<string>();
			}
			set
			{
				json["author"] = value;
			}
		}

		public string ContemporaryComments
		{
			get
			{
				if (json["comments"] == null)
					return null;
				return json["comments"].Value<string>();
			}
			set
			{
				json["comments"] = value;
			}
		}

		public string CreationPlace
		{
			get
			{
				if (json["creationPlace"] == null)
					return null;
				return json["creationPlace"].Value<string>();
			}
			set
			{
				json["creationPlace"] = value;
			}
		}

		public string CreationYear
		{
			get
			{
				if (json["creationYear"] == null)
					return null;
				return json["creationYear"].Value<string>();
			}
			set
			{
				json["creationYear"] = value;
			}
		}

		public string Critics
		{
			get
			{
				if (json["critics"] == null)
					return null;
				return json["critics"].Value<string>();
			}
			set
			{
				json["critics"] = value;
			}
		}

		public string Decors
		{
			get
			{
				if (json["decors"] == null)
					return null;
				return json["decors"].Value<string>();
			}
			set
			{
				json["decors"] = value;
			}
		}

		public string EntrepreneurName
		{
			get
			{
				if (json["entrepreneurName"] == null)
					return null;
				return json["entrepreneurName"].Value<string>();
			}
			set
			{
				json["entrepreneurName"] = value;
			}
		}

		public string Genre
		{
			get
			{
				if (json["type"] == null)
					return null;
				return json["type"].Value<string>();
			}
			set
			{
				json["type"] = value;
			}
		}

		public string Iconography
		{
			get
			{
				if (json["iconography"] == null)
					return null;
				return json["iconography"].Value<string>();
			}
			set
			{
				json["iconography"] = value;
			}
		}

		public string MusicianName
		{
			get
			{
				if (json["musicianName"] == null)
					return null;
				return json["musicianName"].Value<string>();
			}
			set
			{
				json["musicianName"] = value;
			}
		}

		public string Resonances
		{
			get
			{
				if (json["resonances"] == null)
					return null;
				return json["resonances"].Value<string>();
			}
			set
			{
				json["resonances"] = value;
			}
		}

		public string SourceFolio
		{
			get
			{
				if (json["sourceFolio"] == null)
					return null;
				return json["sourceFolio"].Value<string>();
			}
			set
			{
				json["sourceFolio"] = value;
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
			set
			{
				json["title"] = value;
			}
		}

		public string SourceId
		{
			get
			{
				if (json["sourceId"] == null)
					return null;
				return json["sourceId"].Value<string>();
			}
			set
			{
				json["sourceId"] = value;
			}
		}

		public int? SourceTome
		{
			get
			{
				if (json["sourceTome"] == null)
					return null;
				return json["sourceTome"].Value<int?>();
			}
			set
			{
				json["sourceTome"] = value;
			}
		}

		public int? SourceVolume
		{
			get
			{
				if (json["sourceVolume"] == null)
					return null;
				return json["sourceVolume"].Value<int?>();
			}
			set
			{
				json["sourceVolume"] = value;
			}
		}

		public IEnumerable<string> Tags
		{
			get { return json["tags"].Values<string>(); }
		}

		public void AddTag(string tag)
		{
			if (!Tags.Contains(tag))
			{
				JArray temp = json["tags"].Value<JArray>();
				temp.Add(tag);
				json["tags"] = temp;
			}
		}

		public void RemoveTag(string tag)
		{
			json["tags"] = json["tags"].Value<JArray>().Remove(tag);
		}

		public string CreatorId
		{
			get {
				if (json["creatorId"] == null)
					return null;
				return json["creatorId"].Value<string>(); }
			private set { json["creatorId"] = value; }
		}

		public string LastModifierId
		{
			get {
				if (json["lastModifierId"] == null)
					return null;
				return json["lastModifierId"].Value<string>(); }
			private set { json["lastModifierId"] = value; }
		}

		public IEnumerable<string> CollaboratorsId
		{
			get { return json["collaboratorsId"].Values<string>(); }
		}

		public void AddCollaborator(string collab)
		{
			if (!Tags.Contains(collab))
			{
				JArray temp = json["collaboratorsId"].Value<JArray>();
				temp.Add(collab);
				json["collaboratorsId"] = temp;
			}
		}

		public void RemoveCollaborator(string collab)
		{
			json["collaboratorsId"] = json["collaboratorsId"].Value<JArray>().Remove(collab);
		}
		public override string ToString()
		{
			string jsonS = json.ToString();
			return jsonS;
		}
	}
}