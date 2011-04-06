using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace FoireMuses.Client
{
	/// <summary>
	/// represent a Source(une source) object in json
	/// </summary>
	public class Source : JObject
	{
		public Source()
		{
			this.Add("type", "source");
		}

		public Source(JObject jobject)
			: base(jobject)
		{
			JToken type;
			if (this.TryGetValue("otype", out type))
			{
				if (type.Value<string>() != "source")
					throw new Exception("Bad object type");
			}
			else
			{
				this.Add("otype", "source");
			}
		}

		public string Id
		{
			get { return this["_id"].Value<string>(); }
		}

		public string Name
		{
			get {
				if (this["name"] == null)
					return null;
				return this["name"].Value<string>(); }
			set { this["name"] = value; }
		}

		public string Publisher
		{
			get {
				if (this["publisher"] == null)
					return null;
				return this["publisher"].Value<string>(); }
			set { this["publisher"] = value; }
		}

		public string FreeZone
		{
			get {
				if (this["free"] == null)
					return null;
				return this["free"].Value<string>(); }
			set { this["free"] = value; }
		}

		public string Cote
		{
			get {
				if (this["cote"] == null)
					return null;
				return this["cote"].Value<string>(); }
			set { this["cote"] = value; }
		}

		public string Abbreviation
		{
			get { return this["abbr"].Value<string>(); }
			set { this["abbr"] = value; }
		}

		public bool? ApproxDate
		{
			get {
				if (this["approx"] == null)
					return null;
				return this["approx"].Value<bool>(); }
			set { this["approx"] = value; }
		}

		public bool? IsMusicalSource
		{
			get {
				if (this["nmusicalSource"] == null)
					return null;
				return this["musicalSource"].Value<bool>(); }
			set { this["musicalSource"] = value; }
		}

		public int? DateFrom
		{
			get {
				if (this["dateFrom"] == null)
					return null;
				return this["dateFrom"].Value<int>(); }
			set { this["dateFrom"] = value; }
		}

		public int? DateTo
		{
			get {
				if (this["dateTo"] == null)
					return null;
				return this["dateTo"].Value<int>(); }
			set { this["dateTo"] = value; }
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
            get { return this["creatorId"].Value<string>(); }
            private set { this["creatorId"] = value; }
        }

        public string LastModifierId
        {
            get { return this["lastModifierId"].Value<string>(); }
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
