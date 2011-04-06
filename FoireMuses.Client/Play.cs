using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace FoireMuses.Client
{
	/// <summary>
	/// represent a Play(une pièce) object in json
	/// </summary>
	public class Play : JObject
	{

		public Play()
		{
			this.Add("type", "play");
		}

		public Play(JObject jobject)
			: base(jobject)
		{
			JToken type;
			if (this.TryGetValue("otype", out type))
			{
				if (type.Value<string>() != "play")
					throw new Exception("Bad object type");
			}
			else
			{
				this.Add("otype", "play");
			}
		}

		public string Id
		{
			get { return this["_id"].Value<string>(); }
		}

		public string Abstract
		{
			get
			{
				if (this["abstract"] == null)
					return null;
				return this["abstract"].Value<string>();
			}
			set
			{
				this["abstract"] = value;
			}
		}

		public string ActionLocation
		{
			get
			{
				if (this["actionLocation"] == null)
					return null;
				return this["actionLocation"].Value<string>();
			}
			set
			{
				this["actionLocation"] = value;
			}
		}

		public string Author
		{
			get
			{
				if (this["author"] == null)
					return null;
				return this["author"].Value<string>();
			}
			set
			{
				this["author"] = value;
			}
		}

		public string ContemporaryComments
		{
			get
			{
				if (this["comments"] == null)
					return null;
				return this["comments"].Value<string>();
			}
			set
			{
				this["comments"] = value;
			}
		}

		public string CreationPlace
		{
			get
			{
				if (this["creationPlace"] == null)
					return null;
				return this["creationPlace"].Value<string>();
			}
			set
			{
				this["creationPlace"] = value;
			}
		}

		public string CreationYear
		{
			get
			{
				if (this["creationYear"] == null)
					return null;
				return this["creationYear"].Value<string>();
			}
			set
			{
				this["creationYear"] = value;
			}
		}

		public string Critics
		{
			get
			{
				if (this["critics"] == null)
					return null;
				return this["critics"].Value<string>();
			}
			set
			{
				this["critics"] = value;
			}
		}

		public string Decors
		{
			get
			{
				if (this["decors"] == null)
					return null;
				return this["decors"].Value<string>();
			}
			set
			{
				this["decors"] = value;
			}
		}

		public string EntrepreneurName
		{
			get
			{
				if (this["entrepreneurName"] == null)
					return null;
				return this["entrepreneurName"].Value<string>();
			}
			set
			{
				this["entrepreneurName"] = value;
			}
		}

		public string Genre
		{
			get
			{
				if (this["type"] == null)
					return null;
				return this["type"].Value<string>();
			}
			set
			{
				this["type"] = value;
			}
		}

		public string Iconography
		{
			get
			{
				if (this["iconography"] == null)
					return null;
				return this["iconography"].Value<string>();
			}
			set
			{
				this["iconography"] = value;
			}
		}

		public string MusicianName
		{
			get
			{
				if (this["musicianName"] == null)
					return null;
				return this["musicianName"].Value<string>();
			}
			set
			{
				this["musicianName"] = value;
			}
		}

		public string Resonances
		{
			get
			{
				if (this["resonances"] == null)
					return null;
				return this["resonances"].Value<string>();
			}
			set
			{
				this["resonances"] = value;
			}
		}

		public string SourceFolio
		{
			get
			{
				if (this["sourceFolio"] == null)
					return null;
				return this["sourceFolio"].Value<string>();
			}
			set
			{
				this["sourceFolio"] = value;
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
			set
			{
				this["title"] = value;
			}
		}

		public string SourceId
		{
			get
			{
				if (this["sourceId"] == null)
					return null;
				return this["sourceId"].Value<string>();
			}
			set
			{
				this["sourceId"] = value;
			}
		}

		public int? SourceTome
		{
			get
			{
				if (this["sourceTome"] == null)
					return null;
				return this["sourceTome"].Value<int?>();
			}
			set
			{
				this["sourceTome"] = value;
			}
		}

		public int? SourceVolume
		{
			get
			{
				if (this["sourceVolume"] == null)
					return null;
				return this["sourceVolume"].Value<int?>();
			}
			set
			{
				this["sourceVolume"] = value;
			}
		}

		public IEnumerable<string> Tags
		{
			get { return this["tags"].Values<string>(); }
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
			get {
				if (this["creatorId"] == null)
					return null;
				return this["creatorId"].Value<string>(); }
			private set { this["creatorId"] = value; }
		}

		public string LastModifierId
		{
			get {
				if (this["lastModifierId"] == null)
					return null;
				return this["lastModifierId"].Value<string>(); }
			private set { this["lastModifierId"] = value; }
		}

		public IEnumerable<string> CollaboratorsId
		{
			get { return this["collaboratorsId"].Values<string>(); }
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