using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace FoireMuses.Client
{
	public abstract class SearchResultItem
	{
		internal JObject Json { get; set; }
	}
}
