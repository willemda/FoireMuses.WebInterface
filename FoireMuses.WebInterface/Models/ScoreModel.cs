using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FoireMuses.WebInterface.Models
{
	public class ScoreModel 
	{
		public string Id { get; internal set; }

		public string Title { get; set; }
		[HiddenInput]
		public string Code1 { get; set; }
		[HiddenInput]
		public string Code2 { get; set; }
		public string Coirault { get; set; }
		public string Composer { get; set; }
		public string CoupeMetrique { get; set; }
		public string Verses { get; set; }
		public string Delarue { get; set; }

		[DataType(DataType.MultilineText)]
		public string Comments { get; set; }
		public string Editor { get; set; }
		public string RythmSignature { get; set; }
		public string OtherTitles { get; set; }
		public string Stanza { get; set; }
		public string ScoreType { get; set; }

		[DisplayFormat(NullDisplayText = "Pas de source musicale")]
		public MusicalSourceModel MusicalSource { get; set; }
		[DisplayFormat(NullDisplayText = "Pas de source Textuelle")]
		public TextualSourceModel TextualSource { get; set; }

		//TODO: replace by list
		public IList<string> Tags { get; set; }

		public string CreatorId { get; internal set;}
		public string LastModifierId { get; internal set; }

		//TODO: replace by list
		public IList<string> CollaboratorsId { get; set; }
	}
}
