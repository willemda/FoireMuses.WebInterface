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


		public ActionResult Index(string type)
		{
			switch (type)
			{
				case "score":
					ViewBag.Action = "SearchScore";
					return View("SearchScore");
				case "play":
					return View("SearchPlay");
				case "music":
					ViewBag.Action = "SearchMusic";
					return View("SearchMusic");
				case "source":
					return View("SearchSource");
				default:
					ViewBag.Action = "SearchScore";
					return View("SearchScore");
			}
		}


		public ViewResult SearchScore(string title = null, string editor = null, string composer = null, string verses = null,int page = 1)
		{
			FoireMusesConnection connection = GetConnection();
			Result<SearchResult<ScoreSearchItem>> result = new Result<SearchResult<ScoreSearchItem>>();
			SearchResult<ScoreSearchItem> searchResult = connection.SearchScore((page - 1) * PageSize, PageSize, title, null, editor, composer, verses, null, null, result).Wait();
			var viewModel = new ListViewModel<ScoreSearchItem>()
			{
				CurrentPage = page,
				SearchResult = searchResult
			};
			return View("ListScoreSearch", viewModel);
		}

		public ViewResult SearchMusic(string music = null, int page = 1)
		{
			FoireMusesConnection connection = GetConnection();
			Result<SearchResult<ScoreSearchItem>> result = new Result<SearchResult<ScoreSearchItem>>();
			SearchResult<ScoreSearchItem> searchResult = connection.SearchScore((page - 1) * PageSize, PageSize, null, null, null, null, null, music, null, result).Wait();
			var viewModel = new ListViewModel<ScoreSearchItem>()
			{
				CurrentPage = page,
				SearchResult = searchResult
			};
			return View("ListScoreSearch", viewModel);
		}
	}
}
