using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace FoireMuses.Client
{
	public abstract class SourceReference
	{
		internal JObject theJson { get; private set; }

		protected SourceReference()
		{
			theJson = new JObject();
		}

		protected SourceReference(JObject anObject)
		{
			theJson = anObject;
		}

		public string SourceId
		{
			get
			{
				if (theJson["id"] == null)
					return null;
				return theJson["id"].Value<string>();
			}
			set { theJson["id"] = value; }
		}

		public int? AirNumber
		{
			get
			{
				if (theJson["air"] == null)
					return null;
				return theJson["air"].Value<int?>();
			}
			set
			{
				theJson["air"] = value;
			}
		}

		public string Page
		{
			get
			{
				if (theJson["page"] == null)
					return null;
				return theJson["page"].Value<string>();
			}
			set { theJson["page"] = value; }
		}

		public int? Tome
		{
			get
			{
				if (theJson["tome"] == null)
					return null;
				return theJson["tome"].Value<int?>();
			}
			set
			{
				theJson["tome"] = value;
			}
		}

		public int? Volume
		{
			get
			{
				if (theJson["volume"] == null)
					return null;
				return theJson["volume"].Value<int?>();
			}
			set
			{
				theJson["volume"] = value;
			}
		}
	}
}
