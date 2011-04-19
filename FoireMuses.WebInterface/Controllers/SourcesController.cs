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

    }
}
