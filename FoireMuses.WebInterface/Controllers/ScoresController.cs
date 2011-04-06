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

		/*
		public ViewResult Edit(string scoreId)
		{
			DreamMessage msg = BasePlug.At("foiremuses", "scores", scoreId).WithCredentials("danny", "azerty").Get(DreamMessage.Ok());
			if (msg.Status != DreamStatus.Ok)
			{
				//redirige
			}
			JScore sr = new JScore(JObject.Parse(msg.ToText()));
			return View(sr);
		}

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
