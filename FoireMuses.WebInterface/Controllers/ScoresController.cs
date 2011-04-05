using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MindTouch.Dream;
using MindTouch.Tasking;
using Newtonsoft.Json.Linq;
using FoireMuses.WebInterface.Models;
using SportsStore.WebUI.Models;

namespace MvcMovie.Controllers
{
	public class ScoresController : Controller
	{

		protected Plug BasePlug = Plug.New(new XUri("http://localhost"));
        public int PageSize = 20;
		//
		// GET: /Scores/List

		public ViewResult List(int page=1)
		{
			//use mindtouch dream to access the web service.
			// treat the result and return it to the view
			DreamMessage msg = BasePlug.At("foiremuses", "scores").With("offset",(page-1)*PageSize).With("max",PageSize).WithCredentials("danny", "azerty").Get(DreamMessage.Ok());
			SearchResult<JScore> sr = new SearchResult<JScore>(JObject.Parse(msg.ToText()));
            var viewModel = new ListViewModel<JScore>
            {
                SearchResult = sr,
                CurrentPage = page,
                
            };
			return View(viewModel);
		}

        //
        // GET: /Scores/Details
        public ViewResult Details(string scoreId)
        {
            DreamMessage msg = BasePlug.At("foiremuses", "scores",scoreId).WithCredentials("danny", "azerty").Get(DreamMessage.Ok());
            JScore sr = new JScore(JObject.Parse(msg.ToText()));
            return View(sr);
        }

	}
}
