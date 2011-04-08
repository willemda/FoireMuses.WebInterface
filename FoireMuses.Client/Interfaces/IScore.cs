using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace FoireMuses.Core.Interfaces
{
	public interface IScore
	{

		string Id { get; }

		string Title{get;set;}
		string Code1 { get; set; }
		string Code2 { get; set; }
		string Coirault { get; set; }
		string Composer { get; set; }
		string CoupeMetrique { get; set; }
		string Verses { get; set; }
		string Delarue { get; set; }
		string Comments { get; set; }
		string Editor { get; set; }
		string RythmSignature { get; set; }
		string OtherTitles { get; set; }
		string Stanza { get; set; }
		string ScoreType { get; set; }

		IMusicalSource MusicalSource { get; set; }

		ITextualSource TextualSource { get; set; }

		//TODO: replace by list
		IList<string> Tags { get; set; }

		string CreatorId { get; }
		string LastModifierId { get;  }

		//TODO: replace by list
		IList<string> CollaboratorsId { get; set; }
	}
}
