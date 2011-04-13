using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoireMuses.Client;

namespace FoireMuses.WebInterface.Models
{
    public class CreateScoreModel
    {
        public Score Score { get; set; }
        public IEnumerable<Source> Sources { get; set; }
    }
}