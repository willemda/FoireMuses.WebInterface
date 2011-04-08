using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoireMuses.Webinterface.Models
{
	public class SourceModel
	{
		public string Id { get; internal set;}

		public string Name { get; set; }
		public string FreeZone { get; set; }
		public int? DateFrom { get; set; }
		public int? DateTo { get; set; }
		public string Cote { get; set; }
		public string Abbreviation { get; set; }
		public bool? ApproxDate { get; set; }
		public string Publisher { get; set; }

		public IList<string> Tags { get; set;}

		public string CreatorId { get; internal set;}
		public string LastModifierId { get; internal set;}

		public IList<string> CollaboratorsId { get; set; }
	}
}