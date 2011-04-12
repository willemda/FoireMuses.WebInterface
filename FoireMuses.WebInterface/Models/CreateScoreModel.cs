using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoireMuses.Client;

namespace FoireMuses.Webinterface.Models
{
    public class CreateScoreModel
    {
        public Score score { get; set; }
        public IEnumerable<Source> Sources { get; set; }
    }
}