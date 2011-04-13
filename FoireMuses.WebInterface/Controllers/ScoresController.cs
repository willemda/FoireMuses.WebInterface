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

			var viewModel = new ListViewModel<Score>
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
				score = connection.GetScore(scoreId,new Result<Score>()).Wait();
				if (score.TextualSource != null)
				{
					sTextuelle = connection.GetSource(score.TextualSource.SourceId, new Result<Source>()).Wait();
					if (score.TextualSource.PieceId != null)
						assPlay = connection.GetPlay(score.TextualSource.PieceId, new Result<Play>()).Wait();
				}
				if(score.MusicalSource!=null)
					sMusicale = connection.GetSource(score.MusicalSource.SourceId, new Result<Source>()).Wait();
				if(score.HasAttachement){
					attachedFiles = score.GetAttachmentNames().Where(x => !x.StartsWith("$"));
					documents = score.GetAttachmentNames().Where(x => x.StartsWith("$"));
				}
			}
			catch (Exception e)
			{
				// do stuff to return error message to the screen
			}
			var viewModel = new ScoreDetailModel
			{
				Score = score,
				TextualSource = sTextuelle,
				AssociatedPlay = assPlay,
				MusicalSource = sMusicale,
				AttachedFiles = attachedFiles,
				Documents = documents
			};
			return View(viewModel);
		}



        public ViewResult Create()
        {
            return View();
        }

        public ViewResult CreateXml()
        {
            return View();
        }

        public ViewResult CreateScratch()
        {
            FoireMusesConnection connection = GetConnection();
            SearchResult<Source> searchResultSource = null;
            searchResultSource = connection.GetSources(0, 0, new Result<SearchResult<Source>>()).Wait();
            ViewBag.Sources = searchResultSource.Rows;
            return View(new Score());
        }

        [HttpPost]
        public ActionResult CreateScratch(Score score)
        {
            FoireMusesConnection connection = GetConnection();
            score = connection.CreateScore(score, new Result<Score>()).Wait();
            return Redirect("Details?scoreId="+score.Id);
        }
        
        public ActionResult GetPlaysForSource(string id)
        {
            FoireMusesConnection connection = GetConnection();
            SearchResult<Play> searchResultPlay = null;
            searchResultPlay = connection.GetPlaysFromSource(id, 0, 0, new Result<SearchResult<Play>>()).Wait();
            return PartialView("playList", searchResultPlay.Rows);
        }

        [HttpPost]
        public ActionResult CreateXml(HttpPostedFileBase file)
        {
            FoireMusesConnection connection = GetConnection();
            Score score = connection.CreateScoreWithXml(XDocFactory.From(file.InputStream, MimeType.XML), new Result<Score>()).Wait();
            return Redirect("Edit?scoreId=" + score.Id);
        }

		
		public ViewResult Edit(string scoreId)
		{
			FoireMusesConnection connection = GetConnection();
			Score score = null;
			SearchResult<Source> sourceList = null;
			try
			{
				score = connection.GetScore(scoreId, new Result<Score>()).Wait();
				sourceList = connection.GetSources(0,0, new Result<SearchResult<Source>>()).Wait();
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
			return View("CreateScratch",score);
		}

		[HttpPost]
		public ViewResult Edit(Score model, FormCollection collection)
		{
			FoireMusesConnection connection = GetConnection();
			Score score = null;
			/*if (collection["tSourceId"] != null)
			{
				model.TextualSource.SourceId = collection["tSourceId"];
			}
			if (collection["mSourceId"] != null)
			{
				model.TextualSource.SourceId = collection["tSourceId"];
			}*/
			try
			{
				score = connection.GetScore(model.Id, new Result<Score>()).Wait();
				TryUpdateModel(score);
				score = connection.EditScore(score, new Result<Score>()).Wait();
			}
			catch (Exception e)
			{
				// on redirige
			}
			if (score == null)
			{
				//on redirige
			}

			return View(score);
		}
		/*
		[HttpPost]
		public ActionResult Edit(JScore model, FormCollection collection)
		{

			try
			{
				DreamMessage msg = BasePlug.At("foiremuses", "scores", model.Id).WithCredentials("danny", "azerty").Get(DreamMessage.Ok());
				JScore score = null;
				if (msg.Status == DreamStatus.Ok)
					score = new JScore(JObject.Parse(msg.ToText()));

				UpdateModel(score);


				msg = BasePlug.At("foiremuses", "scores").WithCredentials("danny", "azerty").Put(DreamMessage.Ok(MimeType.JSON,));
				return RedirectToAction("Details", new { id = model.ID });
			}
			catch (Exception)
			{
				ModelState.AddModelError("", "Edit Failure, see inner exception");
			}

			return View(model);
		}*/
	}
}
