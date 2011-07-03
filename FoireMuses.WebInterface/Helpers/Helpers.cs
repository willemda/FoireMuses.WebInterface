using System;
using System.Text;
using System.Web.Mvc;
using FoireMuses.Client;
using System.Web.Mvc.Html;
using MindTouch.Dream;
using System.Security.Policy;
using System.Web;

namespace FoireMuses.WebInterface.HtmlHelpers
{
	public static class PlugHelpers
	{
		public static Plug WithCheck(this Plug plug, string fieldName, string fieldValue)
		{
			if (!String.IsNullOrWhiteSpace(fieldValue))
				return plug.With(fieldName, fieldValue);
			return plug;
		}
		public static Plug WithCheck(this Plug plug, string fieldName, bool? fieldValue)
		{
			if (fieldValue != null && fieldValue.HasValue)
				return plug.With(fieldName, fieldValue.Value);
			return plug;
		}
	}
}