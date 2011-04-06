using System.Collections.Generic;
using FoireMuses.WebInterface.Models;
using System;
using Newtonsoft.Json.Linq;
using FoireMuses.Client;

namespace FoireMuses.WebInterface.Models
{
    public class ListViewModel<T> where T : JObject
    {
        public SearchResult<T> SearchResult { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)SearchResult.TotalCount / SearchResult.Max); }
        }
    }
}