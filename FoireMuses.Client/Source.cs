using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using FoireMuses.Client.Helpers;

namespace FoireMuses.Client
{
	/// <summary>
	/// represent a Source(une source) object in json
	/// </summary>
	public class Source
	{

		private JObject json { get;  set; }

		public Source()
		{
			json = new JObject();
			json.Add("otype", "source");
		}

		public Source(JObject jobject)
		{
			json = jobject;
			JToken type;
			if (json.TryGetValue("otype", out type))
			{
				if (type.Value<string>() != "source")
					throw new Exception("Bad object type");
			}
			else
			{
				json.Add("otype", "source");
			}
		}

		public string Id
		{
			get { return json.RetrieveStringCheck("_id"); }
			set { json.AddCheck("_id", value); }
		}

		public string Rev
		{
			get { return json.RetrieveStringCheck("_rev"); }
			set { json.AddCheck("_rev", value); }
		}

		public string Name
		{
			get { return json.RetrieveStringCheck("name"); }
			set { json.AddCheck("name", value); }
		}

		public string Publisher
		{
			get { return json.RetrieveStringCheck("publisher"); }
			set { json.AddCheck("publisher", value); }
		}

		public string FreeZone
		{
			get { return json.RetrieveStringCheck("free"); }
			set { json.AddCheck("free", value); }
		}

		public string Cote
		{
			get { return json.RetrieveStringCheck("cote"); }
			set { json.AddCheck("cote", value); }
		}

		public string Abbreviation
		{
			get { return json.RetrieveStringCheck("abbr"); }
			set { json.AddCheck("abbr", value); }
		}

		public bool? ApproxDate
		{
			get { return json.RetrieveBoolCheck("approx"); }
			set { json.AddCheck("approx", value); }
		}

		public bool? IsMusicalSource
		{
			get { return json.RetrieveBoolCheck("musicalSource"); }
			set { json.AddCheck("musicalSource", value); }
		}

		public int? DateFrom
		{
			get { return json.RetrieveIntCheck("dateFrom"); }
			set { json.AddCheck("dateFrom", value); }
		}

		public int? DateTo
		{
			get { return json.RetrieveIntCheck("dateTo"); }
			set { json.AddCheck("dateTo", value); }
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
			return json.ToString();
		}
	}
}
