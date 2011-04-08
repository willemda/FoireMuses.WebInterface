using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoireMuses.Client;
using FoireMuses.Core.Interfaces;

namespace FoireMuses.WebInterface.Models
{
	public class ScoreDetailModel
	{
		public IScore Score { get; set; }
		public ISource TextualSource { get; set; }
		public IPlay AssociatedPlay { get; set; }
		public ISource MusicalSource { get; set; }
	}
}
