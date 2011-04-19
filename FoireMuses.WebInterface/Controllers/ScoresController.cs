using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MindTouch.Dream;
using MindTouch.Tasking;
using Newtonsoft.Json.Linq;
using FoireMuses.WebInterface.Models;
using FoireMuses.Client;
using MindTouch.Xml;

namespace FoireMuses.WebInterface.Controllers
{
	public class ScoresController : FoireMusesController
	{
		public int PageSize = 20;

		public ViewResult List(int page = 1)
		{
			//use mindtouch dream to access the web service.
			// treat the result and return it to the view
			FoireMusesConnection connection = GetConnection();
			SearchResult<Score> listScores = null;
			try
			{
				listScores = connection.GetScores((page - 1) * PageSize, PageSize, new Result<SearchResult<Score>>()).Wait();
			}
			catch (Exception e)
			{
				// do stuff to return error message to the screen
			}

			var viewModel = new ListViewModel<Score>()
			{
				SearchResult = listScores,
				CurrentPage = page,

			};
			return View(viewModel);
		}

		//
		// GET: /Scores/Details
		public ViewResult Details(string scoreId)
		{
			//use mindtouch dream to access the web service.
			// treat the result and return it to the view
			FoireMusesConnection connection = GetConnection();
			Score score = null;
			Source sTextuelle = null;
			Source sMusicale = null;
			Play assPlay = null;
			IEnumerable<string> attachedFiles = null;
			IEnumerable<string> documents = null;
			try
			{
				score = connection.GetScore(scoreId, new Result<Score>()).Wait();
				if (score.TextualSource != null)
				{
					sTextuelle = connection.GetSource(score.TextualSource.SourceId, new Result<Source>()).Wait();
					if (score.TextualSource.PieceId != null)
						assPlay = connection.GetPlay(score.TextualSource.PieceId, new Result<Play>()).Wait();
				}
				if (score.MusicalSource != null)
					sMusicale = connection.GetSource(score.MusicalSource.SourceId, new Result<Source>()).Wait();
				if (score.HasAttachement)
				{
					attachedFiles = score.GetAttachmentNames().Where(x => !x.StartsWith("$"));
					documents = score.GetAttachmentNames().Where(x => x.StartsWith("$"));
				}
			}
			catch (Exception e)
			{
				// do stuff to return error message to the screen
			}
			ViewBag.TextualSource = sTextuelle;
			ViewBag.AssociatedPlay = assPlay;
			ViewBag.MusicalSource = sMusicale;
			ViewBag.AttachedFiles = attachedFiles;
			ViewBag.Documents = documents;
			return View(score);
		}



		public ViewResult Create()
		{
			return View();
		}

		public ViewResult Publish(string scoreId, string scoreRev)
		{
			if (scoreId == null || scoreRev == null)
				return View();
			ViewBag.ScoreId = scoreId;
			ViewBag.ScoreRev = scoreRev;
			return View();
		}


		public ActionResult GetPlaysForSource(string id)
		{
			FoireMusesConnection connection = GetConnection();
			SearchResult<Play> searchResultPlay = null;
			searchResultPlay = connection.GetPlaysFromSource(id, 0, 0, new Result<SearchResult<Play>>()).Wait();
			return PartialView("playList", searchResultPlay.Rows);
		}

		[HttpPost]
		public ActionResult Publish(string scoreId, string overwrite, HttpPostedFileBase file)
		{
			FoireMusesConnection connection = GetConnection();
			Score score = null;
			if (scoreId == null)
			{
				score = connection.CreateScoreWithXml(XDocFactory.From(file.InputStream, MimeType.XML), new Result<Score>()).Wait();
			}
			else
			{
				bool ovwrite;
				if (overwrite == "overwrite")
					ovwrite = true;
				else
					ovwrite = false;
				Score current = connection.GetScore(scoreId, new Result<Score>()).Wait();
				score = connection.UpdateScoreWithXml(current.Id, current.Rev, XDocFactory.From(file.InputStream, MimeType.XML), ovwrite, new Result<Score>()).Wait();
				return Redirect("Details?scoreId=" + current.Id);
			}
			return Redirect("Edit?scoreId=" + score.Id);
		}


		public ViewResult Edit(string scoreId)
		{
			FoireMusesConnection connection = GetConnection();
			Score score = null;
			SearchResult<Source> sourceList = null;
			try
			{
				if (scoreId != null)//get the score matching the id
				{
					score = connection.GetScore(scoreId, new Result<Score>()).Wait();
					if (score.TextualSource != null && score.TextualSource.PieceId != null)
					{
						SearchResult<Play> searchResultPlay = null;
						searchResultPlay = connection.GetPlaysFromSource(score.TextualSource.SourceId, 0, 0, new Result<SearchResult<Play>>()).Wait();
						ViewBag.Pieces = searchResultPlay.Rows;
					}
					ViewBag.HeadTitle = "Edit";
				}
				else
				{
					score = new Score();
					ViewBag.HeadTitle = "Create";
				}
				sourceList = connection.GetSources(0, 0, new Result<SearchResult<Source>>()).Wait();
			}
			catch (Exception e)
			{
				// on redirige
			}
			if (score == null || sourceList == null)
			{
				//on redirige
			}
			ViewBag.Sources = sourceList.Rows;
			return View("Edit", score);
		}

		[HttpPost]
		public ActionResult Edit(Score model, FormCollection collection)
		{
			//little trick here because it automatically creates a TextualSource and MusicalSource even if they are empty
			if (model.TextualSource.SourceId == null)
				model.TextualSource = null;
			if (model.MusicalSource.SourceId == null)
				model.MusicalSource = null;

			FoireMusesConnection connection = GetConnection();
			try
			{
				//we use the same view to edit and create, so let's differentiate both
				if (model.Id == null)
				{
					model = connection.CreateScore(model, new Result<Score>()).Wait();
				}
				else
				{
					//when updating, first get the current score out of the db then update with values
					Score current = connection.GetScore(model.Id, new Result<Score>()).Wait();
					TryUpdateModel(current);
					model = connection.EditScore(current, new Result<Score>()).Wait();
				}
			}
			catch (Exception e)
			{
				//if during creation
				if (model.Id == null)
					{
					return Redirect("erreur");
				}else
				{//during update, redirect to details/edit + error message?
					return Redirect("Details?scoreId=" + model.Id);
				}
				
			}

			//redirect to details
			return Redirect("Details?scoreId=" + model.Id);
		}
	}
}
