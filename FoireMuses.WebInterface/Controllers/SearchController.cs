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
	[Authorize]
	public class SearchController : FoireMusesController
	{
		//
		// GET: /Search/
		private int PageSize = 20;


		public ActionResult Index()
		{
			return RedirectToAction("Score");
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
			if (String.IsNullOrWhiteSpace(title) && String.IsNullOrWhiteSpace(editor) && String.IsNullOrWhiteSpace(composer) && String.IsNullOrWhiteSpace(verses))
			{
				ViewBag.Error = "You must specify at least one criteria before making a search";
				return View("Score");
			}
			SearchResult<ScoreSearchItem> searchResult = null;
			try
			{
				searchResult = FoireMusesConnection.SearchScore((page - 1) * PageSize, PageSize, new Dictionary<string, object>() { { "title", title }, { "editor", editor }, { "composer", composer }, { "verses", verses } }, new Result<SearchResult<ScoreSearchItem>>()).Wait();
			}
			catch (Exception e)
			{
				return RedirectToAction("Problem", "Error", null);
			}
			var viewModel = new ListViewModel<ScoreSearchItem>()
			{
				CurrentPage = page,
				SearchResult = searchResult
			};
			return View("ListScoreSearch", viewModel);
		}

		public ActionResult SearchMusic(string music = null, int page = 1)
		{
			if (String.IsNullOrWhiteSpace(music))
			{
				ViewBag.Error = "You must at least search for one note";
				return View("Music");
			}

			SearchResult<ScoreSearchItem> searchResult = null;
			try
			{
				searchResult = FoireMusesConnection.SearchScore((page - 1) * PageSize, PageSize, new Dictionary<string, object>() { { "music", music } }, new Result<SearchResult<ScoreSearchItem>>()).Wait();
			}
			catch (Exception e)
			{
				return RedirectToAction("Problem", "Error", null);
			}
			var viewModel = new ListViewModel<ScoreSearchItem>()
			{
				CurrentPage = page,
				SearchResult = searchResult
			};
			return View("ListScoreSearch", viewModel);
		}
	}
}
