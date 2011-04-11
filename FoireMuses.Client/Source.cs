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
			get { return json["_id"].Value<string>(); }
		}

		public string Name
		{
			get
			{
				if (json["name"] == null)
					return null;
				return json["name"].Value<string>();
			}
			set { json["name"] = value; }
		}

		public string Publisher
		{
			get
			{
				if (json["publisher"] == null)
					return null;
				return json["publisher"].Value<string>();
			}
			set { json["publisher"] = value; }
		}

		public string FreeZone
		{
			get
			{
				if (json["free"] == null)
					return null;
				return json["free"].Value<string>();
			}
			set { json["free"] = value; }
		}

		public string Cote
		{
			get
			{
				if (json["cote"] == null)
					return null;
				return json["cote"].Value<string>();
			}
			set { json["cote"] = value; }
		}

		public string Abbreviation
		{
			get { return json["abbr"].Value<string>(); }
			set { json["abbr"] = value; }
		}

		public bool? ApproxDate
		{
			get
			{
				if (json["approx"] == null)
					return null;
				return json["approx"].Value<bool>();
			}
			set { json["approx"] = value; }
		}

		public bool? IsMusicalSource
		{
			get
			{
				if (json["nmusicalSource"] == null)
					return null;
				return json["musicalSource"].Value<bool>();
			}
			set { json["musicalSource"] = value; }
		}

		public int? DateFrom
		{
			get
			{
				if (json["dateFrom"] == null)
					return null;
				return json["dateFrom"].Value<int>();
			}
			set { json["dateFrom"] = value; }
		}

		public int? DateTo
		{
			get
			{
				if (json["dateTo"] == null)
					return null;
				return json["dateTo"].Value<int>();
			}
			set { json["dateTo"] = value; }
		}

		public IList<string> Tags
		{
			get { return json["tags"].Values<string>().ToList<string>(); }
			set { json["tags"] = new JArray(value); }
		}



		public string CreatorId
		{
			get { return json["creatorId"].Value<string>(); }
			private set { json["creatorId"] = value; }
		}

		public string LastModifierId
		{
			get { return json["lastModifierId"].Value<string>(); }
			private set { json["lastModifierId"] = value; }
		}

		public IList<string> CollaboratorsId
		{
			get { return json["collaboratorsId"].Values<string>().ToList<string>(); }
			set { json["collaboratorsId"] = new JArray(value); }
		}


	}
}
