using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoireMuses.Core.Interfaces
{
	public interface ITextualSource
	{
		string SourceId { get; set; }
		int? AirNumber { get; set; }
		string Page { get; set; }
		int? Tome { get; set; }
		int? Volume { get; set; }

		string Comment { get; set; }
		bool? IsSuggested { get; set; }

		string PieceId { get; set; }
		int? SceneNumber { get; set; }
		int? ActNumber { get; set; }
	}
}
