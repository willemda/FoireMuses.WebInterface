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
using System.IO;

namespace FoireMuses.WebInterface.Controllers
{
	[Authorize]
	public class ScoresController : FoireMusesController
	{
		private static readonly log4net.ILog theLogger = log4net.LogManager.GetLogger(typeof(ScoresController));

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
				return View("Error","Error while trying to retrieve the scores list");
			}
			var viewModel = new ListViewModel<Score>()
			{
				CurrentPage = page,
				SearchResult = listScores
			};
			return View(viewModel);
		}


		public ViewResult Search()
		{
			return View();
		}

		//
		// GET: /Scores/Details
		public ViewResult Details(string scoreId)
		{
			if (String.IsNullOrWhiteSpace(scoreId))
			{
				return View("List");
			}
			//use mindtouch dream to access the web service.
			// treat the result and return it to the view
			FoireMusesConnection connection = GetConnection();
			Score score = null;
			Source sTextuelle = null;
			Source sMusicale = null;
			Play assPlay = null;
			IList<ScoreSearchItem> otherTitlesScore = null;
			IEnumerable<string> attachedFiles = null;
			IEnumerable<string> documents = null;
			try
			{
				score = connection.GetScore(scoreId, new Result<Score>()).Wait();
				if (score == null)
				{
					return View("Error", "No score exists for id " + scoreId);
				}
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
				if (score.IsMaster)
				{
					otherTitlesScore = connection.SearchScore(0, 0, new Dictionary<string, object>() { { "masterId", score.Id } }, new Result<SearchResult<ScoreSearchItem>>()).Wait().Rows;
				}
				else if(score.MasterId!=null)
				{
					otherTitlesScore = connection.SearchScore(0, 0, new Dictionary<string, object>() { { "masterId", score.MasterId} }, new Result<SearchResult<ScoreSearchItem>>()).Wait().Rows;
				}
			}
			catch (Exception e)
			{
				return View("Error", "Error while trying to retrieve informations of the score " + scoreId);
			}
			ViewBag.TextualSource = sTextuelle;
			ViewBag.AssociatedPlay = assPlay;
			ViewBag.MusicalSource = sMusicale;
			ViewBag.AttachedFiles = attachedFiles;
			ViewBag.Documents = documents;
			ViewBag.OtherTitlesScore = otherTitlesScore;
			return View(score);
		}



		public ViewResult Create()
		{
			return View();
		}

		public ViewResult Publish(string scoreId)
		{
			if (scoreId == null)
				return View();
			ViewBag.ScoreId = scoreId;
			return View();
		}


		public ActionResult GetPlaysForSource(string id)
		{
			if (String.IsNullOrWhiteSpace(id))
			{
				return PartialView("playList", new List<Play>());
			}
			FoireMusesConnection connection = GetConnection();
			SearchResult<Play> searchResultPlay = null;
			try
			{
				searchResultPlay = connection.GetPlaysFromSource(id, 0, 0, new Result<SearchResult<Play>>()).Wait();
			}
			catch (Exception e)
			{
				theLogger.Error("Error while getting plays from source " + id);
				theLogger.Error(e.StackTrace);
			}
			return PartialView("playList", searchResultPlay.Rows);
		}


		public ActionResult AjaxSearchMaster(string wordsToSearch)
		{
			if (String.IsNullOrWhiteSpace(wordsToSearch))
			{
				return PartialView("AjaxSearchForMaster", new List<ScoreSearchItem>());
			}
			FoireMusesConnection connection = GetConnection();
			SearchResult<ScoreSearchItem> searchResultMaster = null;
			searchResultMaster = connection.SearchScore(0, 20, new Dictionary<string, object>() { {"isMaster",true},{"titleWild",wordsToSearch}}, new Result<SearchResult<ScoreSearchItem>>()).Wait();
			return PartialView("AjaxSearchForMaster", searchResultMaster.Rows);
		}

		public FileStreamResult GetAttachements(string scoreId, string attachementName)
		{
			FoireMusesConnection connection = GetConnection();
			Stream file = connection.GetAttachements(scoreId, attachementName , new Result<Stream>()).Wait();
			return new FileStreamResult(file, "image/png");
		}

		[HttpPost]
		public ActionResult Publish(string scoreId, bool overwrite, HttpPostedFileBase file)
		{
			if (file == null || file.ContentType != "text/xml" || file.ContentLength == 0)
			{
				ViewBag.Error = "Error during the upload, please be sure to choose a valid xml file from your computer";
				return View("Publish");
			}
			theLogger.Info("Start Publishing");
			FoireMusesConnection connection = GetConnection();
			Score score = null;
			if (scoreId == null)
			{
				theLogger.Info("Starting To Create the xml doc");
				XDoc theDoc = XDocFactory.From(file.InputStream, MimeType.XML);
				theLogger.Info("Finished Creating the xml doc - making call to create the score");
				score = connection.CreateScoreWithXml(theDoc, new Result<Score>()).Wait();
			}
			else
			{
				Score current = connection.GetScore(scoreId, new Result<Score>()).Wait();
				if (current == null)
				{
					ViewBag.Error = "Error, there's no score with scoreId "+scoreId;
					return View("Publish");
				}
				score = connection.UpdateScoreWithXml(current.Id, current.Rev, XDocFactory.From(file.InputStream, MimeType.XML), overwrite, new Result<Score>()).Wait();
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
					if (score.MasterId != null)
					{
						Score maitre = connection.GetScore(score.MasterId, new Result<Score>()).Wait();
						ViewBag.MasterIdTitle = maitre.Title;
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
			ViewBag.Sources = sourceList.Rows.OrderBy(x=>x.Name);
			return View("Edit2", score);
		}

		[HttpPost]
		public ActionResult Edit(Score model, FormCollection collection)
		{
			//little trick here because it automatically creates a TextualSource and MusicalSource even if they are empty

			FoireMusesConnection connection = GetConnection();
			try
			{
				//we use the same view to edit and create, so let's differentiate both
				if (model.Id == null)
				{
					if (model.TextualSource.SourceId == null)
						model.TextualSource = null;
					if (model.MusicalSource.SourceId == null)
						model.MusicalSource = null;
					model = connection.CreateScore(model, new Result<Score>()).Wait();
				}
				else
				{
					//when updating, first get the current score out of the db then update with values
					Score current = connection.GetScore(model.Id, new Result<Score>()).Wait();
					TryUpdateModel(current);
					if (current.TextualSource.SourceId == null)
						current.TextualSource = null;
					if (current.MusicalSource.SourceId == null)
						current.MusicalSource = null;
					model = connection.EditScore(current, new Result<Score>()).Wait();
				}
			}
			catch (Exception e)
			{
				//if during creation
				if (model.Id == null)
				{
					return Redirect("Error");
				}
				else
				{//during update, redirect to details/edit + error message?
					return Redirect("Details?scoreId=" + model.Id);
				}

			}

			//redirect to details
			return Redirect("Details?scoreId=" + model.Id);
		}
	}
}
