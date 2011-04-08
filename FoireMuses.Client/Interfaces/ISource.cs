using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace FoireMuses.Core.Interfaces
{
	public interface ISource
	{
		string Id { get; }

		string Name { get; set; }
		string FreeZone { get; set; }
		int? DateFrom { get; set; }
		int? DateTo { get; set; }
		string Cote { get; set; }
		string Abbreviation { get; set; }
		bool? ApproxDate { get; set; }
		string Publisher { get; set; }

		IList<string> Tags { get; }

		string CreatorId { get; }
		string LastModifierId { get; }

		IList<string> CollaboratorsId { get; }
	}
}
