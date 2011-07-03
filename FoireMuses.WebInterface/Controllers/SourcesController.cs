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
		public int PageSize = 2;

		public ViewResult List(int page = 1)
		{
			SearchResult<SourceSearchItem> listSources = null;
			try
			{
				listSources = FoireMusesConnection.GetSources((page - 1) * PageSize, PageSize, new Result<SearchResult<SourceSearchItem>>()).Wait();
			}
			catch (Exception e)
			{
				// do stuff to return error message to the screen
			}

			ListViewModel<SourceSearchItem> viewModel = new ListViewModel<SourceSearchItem>
			{
				SearchResult = listSources,
				CurrentPage = page,
			};

			return View(viewModel);
		}

		public ViewResult Details(string sourceId)
		{
			Source source = null;
			IList<SourcePageSearchItem> pages = null;
			IEnumerable<string> attachedFiles = null;
			IEnumerable<string> documents = null;
			try
			{
				source = FoireMusesConnection.GetSource(sourceId, new Result<Source>()).Wait();

				if (source.HasAttachement)
				{
					attachedFiles = source.GetAttachmentNames().Where(x => !x.StartsWith("$"));
					documents = source.GetAttachmentNames().Where(x => x.StartsWith("$"));
				}

				pages = FoireMusesConnection.GetSourcePagesFromSource(source.Id, 0, 0, new Result<SearchResult<SourcePageSearchItem>>()).Wait().Rows;
			}
			catch (Exception e)
			{
				// do stuff to return error message to the screen
			}
			ViewBag.AttachedFiles = attachedFiles;
			ViewBag.Documents = documents;
			ViewBag.Pages = pages;
			return View(source);
		}

		public ActionResult Edit(string sourceId)
		{
			Source source = null;
			try
			{
				if (sourceId != null)//get the score matching the id
				{
					source = FoireMusesConnection.GetSource(sourceId, new Result<Source>()).Wait();
					if (source == null)
					{
						return RedirectToAction("Missing", "Error", null);
					}
					ViewBag.HeadTitle = "Edition";
				}
				else
				{
					source = new Source();
					ViewBag.HeadTitle = "Creation";
				}
			}
			catch (ArgumentException e)
			{
				ViewBag.Error = "Veuillez remplir les champs correctement";
				return View("Edit", source);
			}
			catch (Exception e)
			{
				return RedirectToAction("Problem", "Error", null);
			}
			return View("Edit", source);
		}

		public ActionResult Create()
		{
			return RedirectToAction("Edit");
		}

		public ActionResult PageCreate(string sourceId)
		{
			return RedirectToAction("PageEdit", new {sourceId = sourceId});
		}

		public ActionResult PageEdit(string sourcePageId, string sourceId){
			SourcePage page = null;
			try
			{
				if (sourcePageId == null)
				{
					if (String.IsNullOrWhiteSpace(sourceId))
					{
						return RedirectToAction("Missing", "Error", null);
					}
					else
					{
						Source source = FoireMusesConnection.GetSource(sourceId, new Result<Source>()).Wait();
						if (source == null)
						{
							return RedirectToAction("Missing", "Error", null);
						}
					}
                    ViewBag.HeadTitle = "Creation";
					page = new SourcePage();
					page.SourceId = sourceId;
				}
				else
				{
					page = FoireMusesConnection.GetSourcePage(sourcePageId, new Result<SourcePage>()).Wait();
					if (page == null)
					{
						return RedirectToAction("Missing", "Error", null);
					}
                    ViewBag.HeadTitle = "Edition";
				}
			}
			catch (Exception e)
			{
				return RedirectToAction("Problem", "Error", null);
			}
			return View(page);
		}

		[HttpPost]
		public ActionResult PageEdit(SourcePage model)
		{
			if (!ValidatePage(model))
			{
				ViewBag.Error = "Certains champs sont mal rempli ou incomplet, veuillez les remplirs correctements.";
				if (model.Id != null)
					ViewBag.HeadTitle = "Edition";
				else
					ViewBag.HeadTitle = "Creation";
				return View("PageEdit", model);
			}
			
			try
			{
				if (model.Id == null)
				{
					model = FoireMusesConnection.CreateSourcePage(model.SourceId,model, new Result<SourcePage>()).Wait();
				}
				else
				{
					SourcePage current = FoireMusesConnection.GetSourcePage(model.Id, new Result<SourcePage>()).Wait();
					if(current == null)
						return RedirectToAction("Problem", "Error", null);
					TryUpdateModel(current);
					model = FoireMusesConnection.UpdateSourcePage(model.SourceId, current, new Result<SourcePage>()).Wait();
				}
			}
			catch (Exception e)
			{
				return RedirectToAction("Problem", "Error", null);
			}
			if (model == null)
				return RedirectToAction("Problem", "Error", null);
			return View("PageDetails", model);
		}

		public ActionResult PageDetails(string sourcePageId)
		{
			if (String.IsNullOrWhiteSpace(sourcePageId))
			{
				return RedirectToAction("Missing","Error",null);
			}

			SourcePage page;
			try
			{
				page = FoireMusesConnection.GetSourcePage(sourcePageId, new Result<SourcePage>()).Wait();
			}
			catch (Exception e)
			{
				return RedirectToAction("Problem", "Error", null);
			}
			if (page == null)
			{
				return RedirectToAction("Missing", "Error", null);
			}
			return View(page);
		}

		[HttpPost]
		public ActionResult Edit(Source model, FormCollection collection)
		{
			if (model == null)
			{
				return RedirectToAction("Problem", "Error", null);
			}

			if(!ValidateSource(model))
			{
				ViewBag.Error = "Certains champs sont mal rempli ou incomplet, veuillez les remplirs correctements.";
				if (model.Id != null)
					ViewBag.HeadTitle = "Edition";
				else
					ViewBag.HeadTitle = "Creation";
				return View("Edit", model);
			}
			try
			{
				//we use the same view to edit and create, so let's differentiate both
				if (model.Id == null)
				{
					model = FoireMusesConnection.CreateSource(model, new Result<Source>()).Wait();
				}
				else
				{
					//when updating, first get the current score out of the db then update with values
					Source current = FoireMusesConnection.GetSource(model.Id, new Result<Source>()).Wait();
					if (current == null)
						return RedirectToAction("Problem", "Error", null);
					TryUpdateModel(current);
					model = FoireMusesConnection.UpdateSource(current, new Result<Source>()).Wait();
				}
			}
			catch (Exception e)
			{
				return RedirectToAction("Problem", "Error", null);
			}
			if (model == null)
				return RedirectToAction("Problem", "Error", null);
			//redirect to details
			return Redirect("Details?sourceId=" + model.Id);
		}

		public ActionResult Fascimiles(string sourceId)
		{
			if (String.IsNullOrWhiteSpace(sourceId))
				return RedirectToAction("Missing", "Error", null);
			ViewBag.SourceId = sourceId;
			return View();
		}

		[HttpPost]
		public ActionResult Fascimiles(string sourceId, HttpPostedFileBase file)
		{
			if (String.IsNullOrWhiteSpace(sourceId))
				return RedirectToAction("Missing", "Error", null);
			if (file == null || file.ContentType != "application/x-zip-compressed" || file.ContentLength == 0)
			{
				ViewBag.Error = "Error during the upload, please be sure to choose a valid zip file from your computer";
				return View("Fascimiles");
			}

			try
			{
				FoireMusesConnection.CreateSourcePagesFromFacsimileZipFile(sourceId, file.InputStream, new Result<bool>()).Wait();
			}
			catch (Exception e)
			{
				return RedirectToAction("Problem", "Error", null);
			}
			return RedirectToAction("Details", new { sourceId = sourceId });
		}

		private bool ValidateSource(Source source)
		{
			if (String.IsNullOrWhiteSpace(source.Name))
				return false;
			return true;
		}

		private bool ValidatePage(SourcePage page)
		{
			if (page.PageNumber == null || page.PageNumber <0)
				return false;
			return true;
		}
	}
}
