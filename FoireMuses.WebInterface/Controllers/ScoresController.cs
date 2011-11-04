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

		public int PageSize = 4;

		public ActionResult List(int page = 1)
		{
			//use mindtouch dream to access the web service.
			// treat the result and return it to the view
			SearchResult<ScoreSearchItem> listScores = null;
			try
			{
				listScores = FoireMusesConnection.GetScores((page - 1) * PageSize, PageSize, new Result<SearchResult<ScoreSearchItem>>()).Wait();
			}
			catch (Exception e)
			{
				return RedirectToAction("Problem", "Error", null);
			}
			var viewModel = new ListViewModel<ScoreSearchItem>()
			{
				CurrentPage = page,
				SearchResult = listScores
			};
			return View(viewModel);
		}

		public ActionResult Details(string scoreId)
		{
			if (String.IsNullOrWhiteSpace(scoreId))
			{
				return RedirectToAction("Missing", "Error", null);
			}

			Score score = null;
			Score genericScore = null;
			CompleteTextualSource textualSource = null;
			CompleteMusicalSource musicalSource = null;
			IEnumerable<ScoreSearchItem> otherTitlesScore = null;
			IEnumerable<string> attachedFiles = null;
			IEnumerable<string> documents = null;
			try
			{
				score = FoireMusesConnection.GetScore(scoreId, new Result<Score>()).Wait();
				if (score == null)
				{
					return RedirectToAction("Missing", "Error", null);
				}
				if (score.TextualSource != null && !String.IsNullOrWhiteSpace(score.TextualSource.SourceId))
				{
					textualSource = new CompleteTextualSource(score.TextualSource);
					textualSource.Source = FoireMusesConnection.GetSource(score.TextualSource.SourceId, new Result<Source>()).Wait();
					if (!String.IsNullOrWhiteSpace(score.TextualSource.PieceId))
					{
						textualSource.Play = FoireMusesConnection.GetPlay(score.TextualSource.PieceId, new Result<Play>()).Wait();
					}
				}
				if (score.MusicalSource != null && !String.IsNullOrWhiteSpace(score.MusicalSource.SourceId))
				{
					musicalSource = new CompleteMusicalSource(score.MusicalSource);
					musicalSource.Source = FoireMusesConnection.GetSource(score.MusicalSource.SourceId, new Result<Source>()).Wait();
				}
				if (score.HasAttachement)
				{
					attachedFiles = score.GetAttachmentNames().Where(x => !x.StartsWith("$"));
					documents = score.GetAttachmentNames().Where(x => x.StartsWith("$"));
				}
				if (score.IsMaster)
				{
					SearchResult<ScoreSearchItem> results = FoireMusesConnection.SearchScore(0, 0, new Dictionary<string, object>() { { "masterId", score.Id } }, new Result<SearchResult<ScoreSearchItem>>()).Wait();
					if (results == null)
					{
						return RedirectToAction("Problem", "Error", null);
					}
					otherTitlesScore = results.Rows;
				}
				else if (score.MasterId != null)
				{
					genericScore = FoireMusesConnection.GetScore(score.MasterId, new Result<Score>()).Wait();
					if (genericScore == null)
					{
						return RedirectToAction("Problem", "Error", null);
					}
					SearchResult<ScoreSearchItem> results = FoireMusesConnection.SearchScore(0, 0, new Dictionary<string, object>() { { "masterId", score.MasterId } }, new Result<SearchResult<ScoreSearchItem>>()).Wait();
					if (results == null)
					{
						return RedirectToAction("Problem", "Error", null);
					}
					otherTitlesScore = results.Rows;
				}
			}
			catch (Exception e)
			{
				return RedirectToAction("Problem", "Error", null);
			}
			ViewBag.TextualSource = textualSource;
			ViewBag.MusicalSource = musicalSource;
			ViewBag.AttachedFiles = attachedFiles;
			ViewBag.Documents = documents;
			ViewBag.OtherTitlesScore = otherTitlesScore;
			ViewBag.GenericScore = genericScore;
			return View(score);
		}

		public ActionResult Images(string scoreId, string fileName)
		{
			Stream stream;
			try
			{
				stream = FoireMusesConnection.GetAttachements(scoreId, fileName, new Result<Stream>()).Wait();
			}
			catch (Exception e)
			{
				return null;
				return File(System.IO.File.OpenRead("~/Content/images/indisponible.gif"), "image/gif");
			}
			return File(stream, "image/png");
		}

		public ActionResult Download(string scoreId, string fileType, string fileName)
		{
			Stream stream;
			string contentType;
			switch (fileType)
			{
				case "pdf":
					contentType = "application/pdf";
					break;
				case "mid":
					contentType = "audio/midi";
					break;
				case "musicxml":
					contentType = "text/xml";
					break;
				default:
					contentType = "";
					break;
			}
			try
			{
				stream = FoireMusesConnection.GetConvertedScore(scoreId, fileName, new Result<Stream>()).Wait();
			}
			catch (Exception e)
			{
				return RedirectToAction("Problem", "Error", null);
			}
			return File(stream, contentType, fileName);
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

		//AJAX CALL
		public ActionResult GetPlaysForSource(string id)
		{
			theLogger.Info(String.Format("method GetPlaysForSource, parameter id {0}", id));
			if (String.IsNullOrWhiteSpace(id))
			{
				theLogger.Error("Reason: id is null or white space");
				return PartialView("playList", new List<Play>());
			}

			SearchResult<Play> searchResultPlay = null;
			try
			{
				searchResultPlay = FoireMusesConnection.GetPlaysFromSource(id, 0, 0, new Result<SearchResult<Play>>()).Wait();
			}
			catch (Exception e)
			{
				theLogger.Error("Stacktrace:\n" + e.StackTrace);
				return PartialView("playList", new List<Play>());
			}
			if (searchResultPlay == null)
			{
				theLogger.Error("Reason: searchResultPlay result is null");
				return PartialView("playList", new List<Play>());
			}
			return PartialView("playList", searchResultPlay.Rows);
		}

		//AJAX CALL
		public ActionResult AjaxSearchMaster(string wordsToSearch)
		{
			theLogger.Info(String.Format("method GetPlaysForSource, parameter wordsToSearch {0}", wordsToSearch));
			if (String.IsNullOrWhiteSpace(wordsToSearch))
			{
				theLogger.Error("Reason: wordsToSearch is null or white space");
				return PartialView("AjaxSearchForMaster", new List<ScoreSearchItem>());
			}

			SearchResult<ScoreSearchItem> searchResultMaster = null;
			try
			{
				searchResultMaster = FoireMusesConnection.SearchScore(0, 20, new Dictionary<string, object>() { { "isMaster", true }, { "titleWild", wordsToSearch } }, new Result<SearchResult<ScoreSearchItem>>()).Wait();
			}
			catch (Exception e)
			{
				theLogger.Error("Stacktrace:\n" + e.StackTrace);
				return PartialView("AjaxSearchForMaster", new List<ScoreSearchItem>());
			}
			if (searchResultMaster == null)
			{
				theLogger.Error("Reason: searchResultMaster is null");
				return PartialView("AjaxSearchForMaster", new List<ScoreSearchItem>());
			}
			return PartialView("AjaxSearchForMaster", searchResultMaster.Rows);
		}

		[HttpPost]
		public ActionResult Publish(string scoreId, bool overwrite, HttpPostedFileBase file)
		{
			theLogger.Info("POST Publish");
			if (file == null || file.ContentType != "text/xml" || file.ContentLength == 0)
			{
				ViewBag.Error = "Error during the upload, please be sure to choose a valid xml file from your computer";
				return View("Publish");
			}

			Score score = null;
			try
			{
				if (scoreId == null)
				{
					XDoc theDoc = XDocFactory.From(file.InputStream, MimeType.XML);
					score = FoireMusesConnection.CreateScoreWithXml(theDoc, new Result<Score>()).Wait();
				}
				else
				{
					Score current = FoireMusesConnection.GetScore(scoreId, new Result<Score>()).Wait();
					if (current == null)
						return RedirectToAction("Missing", "Error", null);
					score = FoireMusesConnection.UpdateScoreWithXml(current.Id, current.Rev, XDocFactory.From(file.InputStream, MimeType.XML), overwrite, new Result<Score>()).Wait();
					if (score == null)
						return RedirectToAction("Problem", "Error", null);
					return RedirectToAction("Details", new { scoreId = score.Id });
				}
			}
			catch (Exception e)
			{
				theLogger.Error("Stacktrace:\n" + e.StackTrace);
				return RedirectToAction("Problem", "Error", null);
			}
			return RedirectToAction("Edit", new { scoreId = score.Id });
		}

		public ActionResult Edit(string scoreId)
		{
			theLogger.Info(String.Format("GET method Edit, parameter scoreId {0}", scoreId));
			Score score = null;
			SearchResult<SourceSearchItem> sourceList = null;
			try
			{
				if (!String.IsNullOrWhiteSpace(scoreId))//get the score matching the id
				{
					score = FoireMusesConnection.GetScore(scoreId, new Result<Score>()).Wait();
					if (score == null)
						return RedirectToAction("Missing", "Error", null);
					if (score.TextualSource != null && score.TextualSource.PieceId != null)
					{
						SearchResult<Play> searchResultPlay = null;
						searchResultPlay = FoireMusesConnection.GetPlaysFromSource(score.TextualSource.SourceId, 0, 0, new Result<SearchResult<Play>>()).Wait();
						if (searchResultPlay == null)
							return RedirectToAction("Problem", "Error", null);
						ViewBag.Pieces = searchResultPlay.Rows;
					}
					if (score.MasterId != null)
					{
						Score maitre = FoireMusesConnection.GetScore(score.MasterId, new Result<Score>()).Wait();
						if (maitre == null)
						{
							score.MasterId = null;
						}
						else
						{
							ViewBag.MasterIdTitle = maitre.Title;
						}
					}
					ViewBag.HeadTitle = "Edition";
				}
				else
				{
					score = new Score();
					ViewBag.HeadTitle = "Création";
				}
				sourceList = FoireMusesConnection.GetSources(0, 0, new Result<SearchResult<SourceSearchItem>>()).Wait();
			}
			catch (Exception e)
			{
				return RedirectToAction("Problem", "Error", null);
			}
			if (score == null || sourceList == null)
			{
				return RedirectToAction("Missing", "Error", null);
			}
			ViewBag.Sources = sourceList.Rows.OrderBy(x => x.Name);
			return View("Edit", score);
		}

		[HttpPost]
		public ActionResult Edit(Score model, FormCollection collection)
		{
			theLogger.Info("POST method Edit");
			if (model == null)
				return RedirectToAction("Missing", "Error", null);
			try
			{
				if (model.Id == null)
				{
					if (model.TextualSource.SourceId == null)
						model.TextualSource = null;
					if (model.MusicalSource.SourceId == null)
						model.MusicalSource = null;
					model = FoireMusesConnection.CreateScore(model, new Result<Score>()).Wait();
				}
				else
				{
					Score current = FoireMusesConnection.GetScore(model.Id, new Result<Score>()).Wait();
					if (current == null)
						return RedirectToAction("Missing", "Error", null);
					TryUpdateModel(current);
					if (current.TextualSource.SourceId == null)
						current.TextualSource = null;
					if (current.MusicalSource.SourceId == null)
						current.MusicalSource = null;
					model = FoireMusesConnection.UpdateScore(current, new Result<Score>()).Wait();
				}
			}
			catch (Exception e)
			{
				theLogger.Error("Stacktrace:\n" + e.StackTrace);
				return RedirectToAction("Problem", "Error", null);
			}
			if (model == null)
			{
				theLogger.Error("Reason: model is null");
				return RedirectToAction("Problem", "Error", null);
			}
			return RedirectToAction("Details", new { scoreId = model.Id });
		}
	}
}

