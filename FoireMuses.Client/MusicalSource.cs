using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using FoireMuses.Core.Interfaces;

namespace FoireMuses.Client
{
	public class MusicalSource : IMusicalSource
	{
		public JObject json { get; private set; }

		public MusicalSource()
		{
			json = new JObject();
		}

		public MusicalSource(JObject jobject)
		{
			json = jobject;
		}

		public string SourceId
		{
			get
			{
				if (json["id"] == null)
					return null;
				return json["id"].Value<string>();
			}
			set { json["id"] = value; }
		}


		public int? AirNumber
		{
			get
			{
				if (json["air"] == null)
					return null;
				return json["air"].Value<int>();
			}
			set
			{
				json["air"] = value;
			}
		}

		public string Page
		{
			get
			{
				if (json["page"] == null)
					return null;
				return json["page"].Value<string>();
			}
			set { json["page"] = value; }
		}


		public int? Tome
		{
			get
			{
				if (json["tome"] == null)
					return null;
				return json["tome"].Value<int?>();
			}
			set
			{
				json["tome"] = value;
			}
		}

		public int? Volume
		{
			get
			{
				if (json["volume"] == null)
					return null;
				return json["volume"].Value<int?>();
			}
			set
			{
				json["volume"] = value;
			}
		}
	}
}
