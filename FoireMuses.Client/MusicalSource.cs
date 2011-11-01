using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace FoireMuses.Client
{
	public class MusicalSource : SourceReference
	{
		public MusicalSource()
			:base()
		{
		}

		public MusicalSource(JObject anObject)
			:base(anObject)
		{
		}

		public bool? IsSuggested
		{
			get
			{
				if (theJson["isSuggested"] == null)
					return null;
				return theJson["isSuggested"].Value<bool>();
			}
			set
			{
				theJson["isSuggested"] = value;
			}
		}
	}

	public class CompleteMusicalSource : MusicalSource
	{
		public CompleteMusicalSource(JObject anObject) : base(anObject) { }
		public Source Source { get; set; }
	}
}
