using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoireMuses.Client;

namespace FoireMuses.WebInterface.Models
{
	public class ScoreDetailModel
	{
		public Score Score { get; set; }
		public Source TextualSource { get; set; }
		public Play AssociatedPlay { get; set; }
		public Source MusicalSource { get; set; }
	}
}
