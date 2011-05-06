using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoireMuses.Client;
using MindTouch.Tasking;
using FoireMuses.WebInterface.Controllers;
using FoireMuses.WebInterface.Models;

namespace FoireMuses.WebInterface.Controllers
{
	public class SearchController : FoireMusesController
	{
		//
		// GET: /Search/
		private int PageSize = 20;


		public ActionResult Index()
		{
			return Redirect("Score");
		}

		public ViewResult Music()
		{
			return View();
		}

		public ViewResult Score()
		{
			return View();
		}

		public ViewResult Source()
		{
			return View();
		}

		public ViewResult Play()
		{
			return View();
		}
	

		public ActionResult SearchScore(string title = null, string editor = null, string composer = null, string verses = null,int page = 1)
		{
			FoireMusesConnection connection = GetConnection();
			Result<SearchResult<ScoreSearchItem>> result = new Result<SearchResult<ScoreSearchItem>>();
			if(title==null && editor == null && composer == null && verses == null){
				ViewBag.Error = "You must specify at least one criteria before making a search";
				return View("SearchScore");
			}
			SearchResult<ScoreSearchItem> searchResult = connection.SearchScore((page - 1) * PageSize, PageSize, new Dictionary<string, object>() { {"title",title},{"editor",editor},{"composer",composer},{"verses",verses}}, result).Wait();
			var viewModel = new ListViewModel<ScoreSearchItem>()
			{
				CurrentPage = page,
				SearchResult = searchResult
			};
			return View("ListScoreSearch", viewModel);
		}

		public ViewResult SearchMusic(string music = null, int page = 1)
		{
			if (String.IsNullOrWhiteSpace(music))
			{
				ViewBag.Error = "You must at least search for one note";
				return View("SearchMusic");
			}
			FoireMusesConnection connection = GetConnection();
			Result<SearchResult<ScoreSearchItem>> result = new Result<SearchResult<ScoreSearchItem>>();
			SearchResult<ScoreSearchItem> searchResult = connection.SearchScore((page - 1) * PageSize, PageSize, new Dictionary<string, object>() { {"music",music}}, result).Wait();
			var viewModel = new ListViewModel<ScoreSearchItem>()
			{
				CurrentPage = page,
				SearchResult = searchResult
			};
			return View("ListScoreSearch", viewModel);
		}
	}
}
