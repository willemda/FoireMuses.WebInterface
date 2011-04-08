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
using FoireMuses.Client;
using FoireMuses.WebInterface.Models;
using FoireMuses.Core.Interfaces;

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
			SearchResult<IScore> listScores = null;
			try
			{
				listScores = connection.GetScores((page - 1) * PageSize, PageSize, new Result<SearchResult<IScore>>()).Wait();
			}
			catch (Exception e)
			{
				// do stuff to return error message to the screen
			}

			var viewModel = new ListViewModel<IScore>
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
			IScore score = null;
			ISource sTextuelle = null;
			ISource sMusicale = null;
			IPlay assPlay = null;
			try
			{
				score = connection.GetScore(scoreId,new Result<IScore>()).Wait();
				if (score.TextualSource != null)
				{
					sTextuelle = connection.GetSource(score.TextualSource.SourceId, new Result<ISource>()).Wait();
					if (score.TextualSource.PieceId != null)
						assPlay = connection.GetPlay(score.TextualSource.PieceId, new Result<IPlay>()).Wait();
				}
				if(score.MusicalSource!=null)
					sMusicale = connection.GetSource(score.MusicalSource.SourceId, new Result<ISource>()).Wait();
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
				MusicalSource = sMusicale
			};
			return View(viewModel);
		}

		
		public ViewResult Edit(string scoreId)
		{
			FoireMusesConnection connection = GetConnection();
			IScore score = null;
			SearchResult<ISource> sourceList = null;
			try
			{
				score = connection.GetScore(scoreId, new Result<IScore>()).Wait();
				sourceList = connection.GetSources(0,0, new Result<SearchResult<ISource>>()).Wait();
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
			return View(score);
		}

		[HttpPost]
		public ViewResult Edit(EditScoreModel model, FormCollection collection)
		{
			FoireMusesConnection connection = GetConnection();
			IScore score = null;
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
				score = connection.GetScore(model.Score.Id, new Result<IScore>()).Wait();
				TryUpdateModel(score);
				score = connection.EditScore(score, new Result<IScore>()).Wait();
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
