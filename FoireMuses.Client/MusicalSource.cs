using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace FoireMuses.Client
{
	public class MusicalSource : JObject
	{

		public MusicalSource()
		{

		}

		public MusicalSource(JObject jobject)
			: base(jobject)
		{
		}

		public string SourceId
		{
			get
			{
				if (this["id"] == null)
					return null;
				return this["id"].Value<string>();
			}
			set { this["id"] = value; }
		}


		public int? AirNumber
		{
			get
			{
				if (this["air"] == null)
					return null;
				return this["air"].Value<int>();
			}
			set
			{
				this["air"] = value;
			}
		}

		public string Page
		{
			get
			{
				if (this["page"] == null)
					return null;
				return this["page"].Value<string>();
			}
			set { this["page"] = value; }
		}


		public int? Tome
		{
			get
			{
				if (this["tome"] == null)
					return null;
				return this["tome"].Value<int?>();
			}
			set
			{
				this["tome"] = value;
			}
		}

		public int? Volume
		{
			get
			{
				if (this["volume"] == null)
					return null;
				return this["volume"].Value<int?>();
			}
			set
			{
				this["volume"] = value;
			}
		}
	}
}
