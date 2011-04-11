using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoireMuses.WebInterface.Models
{
	public class MusicalSourceModel 
	{
		public string SourceId { get; set; }
		public int? AirNumber { get; set; }
		public string Page { get; set; }
		public int? Tome { get; set; }
		public int? Volume { get; set; }
	}
}