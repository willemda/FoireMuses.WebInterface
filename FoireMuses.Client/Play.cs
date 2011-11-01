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
	public class Play : SearchResultItem
	{

		public Play()
		{
			Json = new JObject();
			Json.Add("otype", "play");
		}

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

		public string Rev
		{
			get
			{
				return Json.RetrieveStringCheck("_rev");
			}
			set
			{
				Json.AddCheck("_rev", value);
			}
		}

		public string Abstract
		{
			get
			{
				if (Json["abstract"] == null)
					return null;
				return Json["abstract"].Value<string>();
			}
			set
			{
				Json["abstract"] = value;
			}
		}

		public string ActionLocation
		{
			get
			{
				if (Json["actionLocation"] == null)
					return null;
				return Json["actionLocation"].Value<string>();
			}
			set
			{
				Json["actionLocation"] = value;
			}
		}

		public string Author
		{
			get
			{
				if (Json["author"] == null)
					return null;
				return Json["author"].Value<string>();
			}
			set
			{
				Json["author"] = value;
			}
		}

		public string ContemporaryComments
		{
			get
			{
				if (Json["comments"] == null)
					return null;
				return Json["comments"].Value<string>();
			}
			set
			{
				Json["comments"] = value;
			}
		}

		public string CreationPlace
		{
			get
			{
				if (Json["creationPlace"] == null)
					return null;
				return Json["creationPlace"].Value<string>();
			}
			set
			{
				Json["creationPlace"] = value;
			}
		}

		public string CreationYear
		{
			get
			{
				if (Json["creationYear"] == null)
					return null;
				return Json["creationYear"].Value<string>();
			}
			set
			{
				Json["creationYear"] = value;
			}
		}

		public string Critics
		{
			get
			{
				if (Json["critics"] == null)
					return null;
				return Json["critics"].Value<string>();
			}
			set
			{
				Json["critics"] = value;
			}
		}

		public string Decors
		{
			get
			{
				if (Json["decors"] == null)
					return null;
				return Json["decors"].Value<string>();
			}
			set
			{
				Json["decors"] = value;
			}
		}

		public string EntrepreneurName
		{
			get
			{
				if (Json["entrepreneurName"] == null)
					return null;
				return Json["entrepreneurName"].Value<string>();
			}
			set
			{
				Json["entrepreneurName"] = value;
			}
		}

		public string Genre
		{
			get
			{
				if (Json["type"] == null)
					return null;
				return Json["type"].Value<string>();
			}
			set
			{
				Json["type"] = value;
			}
		}

		public string Iconography
		{
			get
			{
				if (Json["iconography"] == null)
					return null;
				return Json["iconography"].Value<string>();
			}
			set
			{
				Json["iconography"] = value;
			}
		}

		public string MusicianName
		{
			get
			{
				if (Json["musicianName"] == null)
					return null;
				return Json["musicianName"].Value<string>();
			}
			set
			{
				Json["musicianName"] = value;
			}
		}

		public string Resonances
		{
			get
			{
				if (Json["resonances"] == null)
					return null;
				return Json["resonances"].Value<string>();
			}
			set
			{
				Json["resonances"] = value;
			}
		}

		public string SourceFolio
		{
			get
			{
				if (Json["sourceFolio"] == null)
					return null;
				return Json["sourceFolio"].Value<string>();
			}
			set
			{
				Json["sourceFolio"] = value;
			}
		}

		public string Title
		{
			get
			{
				if (Json["title"] == null)
					return null;
				return Json["title"].Value<string>();
			}
			set
			{
				Json["title"] = value;
			}
		}

		public string SourceId
		{
			get
			{
				if (Json["sourceId"] == null)
					return null;
				return Json["sourceId"].Value<string>();
			}
			set
			{
				Json["sourceId"] = value;
			}
		}

		public int? SourceTome
		{
			get
			{
				if (Json["sourceTome"] == null)
					return null;
				return Json["sourceTome"].Value<int?>();
			}
			set
			{
				Json["sourceTome"] = value;
			}
		}

		public int? SourceVolume
		{
			get
			{
				if (Json["sourceVolume"] == null)
					return null;
				return Json["sourceVolume"].Value<int?>();
			}
			set
			{
				Json["sourceVolume"] = value;
			}
		}

		public IEnumerable<string> Tags
		{
			get { return Json["tags"].Values<string>(); }
		}

		public void AddTag(string tag)
		{
			if (!Tags.Contains(tag))
			{
				JArray temp = Json["tags"].Value<JArray>();
				temp.Add(tag);
				Json["tags"] = temp;
			}
		}

		public void RemoveTag(string tag)
		{
			Json["tags"] = Json["tags"].Value<JArray>().Remove(tag);
		}

		public string CreatorId
		{
			get {
				if (Json["creatorId"] == null)
					return null;
				return Json["creatorId"].Value<string>(); }
			private set { Json["creatorId"] = value; }
		}

		public string LastModifierId
		{
			get {
				if (Json["lastModifierId"] == null)
					return null;
				return Json["lastModifierId"].Value<string>(); }
			private set { Json["lastModifierId"] = value; }
		}

		public IEnumerable<string> CollaboratorsId
		{
			get { return Json["collaboratorsId"].Values<string>(); }
		}

		public void AddCollaborator(string collab)
		{
			if (!Tags.Contains(collab))
			{
				JArray temp = Json["collaboratorsId"].Value<JArray>();
				temp.Add(collab);
				Json["collaboratorsId"] = temp;
			}
		}

		public void RemoveCollaborator(string collab)
		{
			Json["collaboratorsId"] = Json["collaboratorsId"].Value<JArray>().Remove(collab);
		}
		public override string ToString()
		{
			string jsonS = Json.ToString();
			return jsonS;
		}
	}
}