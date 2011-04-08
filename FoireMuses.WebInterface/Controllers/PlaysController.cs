using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MindTouch.Dream;
using FoireMuses.WebInterface.Models;
using Newtonsoft.Json.Linq;
using FoireMuses.Client;

namespace MvcMovie.Controllers
{
    public class PlaysController : Controller
    {
        protected Plug BasePlug = Plug.New(new XUri("http://localhost"));
        public int PageSize = 20;
        //
        // GET: /Scores/

        public ViewResult List(int page = 1)
        {
            //use mindtouch dream to access the web service.
            // treat the result and return it to the view
            DreamMessage msg = BasePlug.At("foiremuses", "plays").With("offset", (page - 1) * PageSize).With("max", PageSize).WithCredentials("danny", "azerty").Get(DreamMessage.Ok());
            SearchResult<Play> sr = new SearchResult<Play>(x=>new Play(x),JObject.Parse(msg.ToText()));
            var viewModel = new ListViewModel<Play>
            {
                SearchResult = sr,
                CurrentPage = page,

            };
            return View(viewModel);
        }

    }
}
