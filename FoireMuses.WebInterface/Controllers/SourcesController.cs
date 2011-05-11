using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoireMuses.Client;
using FoireMuses.WebInterface.Models;
using MindTouch.Tasking;

namespace FoireMuses.WebInterface.Controllers
{
	[Authorize]
	public class SourcesController : FoireMusesController
	{
		public int PageSize = 20;

		public ViewResult List(int page = 1)
		{
			//use mindtouch dream to access the web service.
			// treat the result and return it to the view
			FoireMusesConnection connection = GetConnection();
			SearchResult<Source> listSources = null;
			try
			{
				listSources = connection.GetSources((page - 1) * PageSize, PageSize, new Result<SearchResult<Source>>()).Wait();
			}
			catch (Exception e)
			{
				// do stuff to return error message to the screen
			}

			var viewModel = new ListViewModel<Source>()
			{
				SearchResult = listSources,
				CurrentPage = page,

			};
			return View(viewModel);
		}

		//
		// GET: /Scores/Details
		public ViewResult Details(string sourceId)
		{
			//use mindtouch dream to access the web service.
			// treat the result and return it to the view
			FoireMusesConnection connection = GetConnection();
			Source source = null;
			IEnumerable<string> attachedFiles = null;
			IEnumerable<string> documents = null;
			try
			{
				source = connection.GetSource(sourceId, new Result<Source>()).Wait();

				if (source.HasAttachement)
				{
					attachedFiles = source.GetAttachmentNames().Where(x => !x.StartsWith("$"));
					documents = source.GetAttachmentNames().Where(x => x.StartsWith("$"));
				}
			}
			catch (Exception e)
			{
				// do stuff to return error message to the screen
			}
			ViewBag.AttachedFiles = attachedFiles;
			ViewBag.Documents = documents;
			return View(source);
		}


		public ViewResult Edit(string sourceId)
		{
			FoireMusesConnection connection = GetConnection();
			Source source = null;
			try
			{
				if (sourceId != null)//get the score matching the id
				{
					source = connection.GetSource(sourceId, new Result<Source>()).Wait();
					ViewBag.HeadTitle = "Edit";
				}
				else
				{
					source = new Source();
					ViewBag.HeadTitle = "Create";
				}
			}
			catch (Exception e)
			{
				// on redirige
			}
			if (source == null)
			{
				//on redirige
			}
			return View("Edit", source);
		}

		public ActionResult Create()
		{
			return RedirectToAction("Edit");
		}

		public ActionResult PageCreate(string sourceId)
		{
			//test si sourceId est !null et appartient bien à une source
			return RedirectToAction("PageEdit", new {sourceId = sourceId});
		}

		public ViewResult PageEdit(string sourcePageId, string sourceId){
			SourcePage page = null;
			if(sourcePageId == null)
			{
				page = new SourcePage();
				page.SourceId = sourceId;
			}else{
				FoireMusesConnection connection = GetConnection();
				page = connection.GetSourcePage(sourcePageId, new Result<SourcePage>()).Wait();
			}
			return View(page);
		}

		[HttpPost]
		public ActionResult PageEdit(SourcePage model)
		{
			FoireMusesConnection connection = GetConnection();
			if (model.Id == null)
			{
				model = connection.CreateSourcePage(model, new Result<SourcePage>()).Wait();
			}
			else
			{
				SourcePage current = connection.GetSourcePage(model.Id, new Result<SourcePage>()).Wait();
				TryUpdateModel(current);
				model = connection.EditSourcePage(current, new Result<SourcePage>()).Wait();
			}
			return View("PageDetails", model);
		}

		public ViewResult PageDetails(string sourcePageId)
		{
			if (String.IsNullOrWhiteSpace(sourcePageId))
			{
				return View("Error", "The page you are looking for doesn't exist.");
			}
			FoireMusesConnection connection = GetConnection();
			SourcePage page = connection.GetSourcePage(sourcePageId, new Result<SourcePage>()).Wait();
			if (page == null)
			{
				return View("Error", "The page you are looking for doesn't exist.");
			}
			return View(page);
		}

		[HttpPost]
		public ActionResult Edit(Source model, FormCollection collection)
		{
			//little trick here because it automatically creates a TextualSource and MusicalSource even if they are empty

			FoireMusesConnection connection = GetConnection();
			try
			{
				//we use the same view to edit and create, so let's differentiate both
				if (model.Id == null)
				{
					model = connection.CreateSource(model, new Result<Source>()).Wait();
				}
				else
				{
					//when updating, first get the current score out of the db then update with values
					Source current = connection.GetSource(model.Id, new Result<Source>()).Wait();
					TryUpdateModel(current);
					model = connection.EditSource(current, new Result<Source>()).Wait();
				}
			}
			catch (Exception e)
			{
				//if during creation
				if (model.Id == null)
				{
					//log
					return View("Error", "Error while creating the source");
				}
				else
				{
					//during update, redirect to details/edit + error message?
					//log
					ViewBag.Error = "Error while updating the score";
					return Redirect("Details?sourceId=" + model.Id);
				}

			}

			//redirect to details
			return Redirect("Details?sourceId=" + model.Id);
		}

	}
}
