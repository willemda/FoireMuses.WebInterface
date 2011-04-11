using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoireMuses.WebInterface.Models
{
	public class TextualSourceModel 
	{
		public string SourceId { get; set; }
		public int? AirNumber { get; set; }
		public string Page { get; set; }
		public int? Tome { get; set; }
		public int? Volume { get; set; }

		public string Comment { get; set; }
		public bool? IsSuggested { get; set; }

		public string PieceId { get; set; }
		public int? SceneNumber { get; set; }
		public int? ActNumber { get; set; }
	}
}