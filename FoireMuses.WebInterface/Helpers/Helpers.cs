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

	public static class PagingHelpers
	{

		public static MvcHtmlString CheckboxInputField(this HtmlHelper html, string fieldName, string fieldExpression, bool? fieldValue)
		{
			StringBuilder result = new StringBuilder();
			TagBuilder tagB = new TagBuilder("label");
			tagB.MergeAttribute("for", fieldExpression);
			tagB.InnerHtml = fieldName;
			result.AppendLine(tagB.ToString());
			tagB = new TagBuilder("input");
			tagB.MergeAttribute("class","checkbox");
			tagB.MergeAttribute("type", "checkbox");
			tagB.MergeAttribute("id", fieldExpression);
			tagB.MergeAttribute("name", fieldExpression);
			tagB.MergeAttribute("value", "true");
			if (fieldValue != null && fieldValue.Value)
			{
				tagB.MergeAttribute("checked", "checked");
			}
			result.AppendLine(tagB.ToString());
			tagB = new TagBuilder("input");
			tagB.MergeAttribute("type", "hidden");
			tagB.MergeAttribute("name", fieldExpression);
			tagB.MergeAttribute("value", "false");
			result.AppendLine(tagB.ToString());
			MvcHtmlString mvc = MvcHtmlString.Create(result.ToString());
			return mvc;
		}

		public static MvcHtmlString TextInputField(this HtmlHelper html, string fieldName, string fieldExpression, int? fieldValue)
		{
			StringBuilder result = new StringBuilder();
			TagBuilder tagB = new TagBuilder("label");
			tagB.MergeAttribute("for", fieldExpression);
			tagB.InnerHtml = fieldName;
			result.AppendLine(tagB.ToString());
			tagB = new TagBuilder("input");
			tagB.MergeAttribute("type", "text");
			tagB.MergeAttribute("id", fieldExpression);
			tagB.MergeAttribute("name", fieldExpression);
			if (fieldValue!=null)
				tagB.MergeAttribute("value", fieldValue.Value.ToString());
			result.AppendLine(tagB.ToString());
			MvcHtmlString mvc = MvcHtmlString.Create(result.ToString());
			return mvc;
		}

		public static MvcHtmlString TextInputField(this HtmlHelper html, string fieldName, string fieldExpression, string fieldValue)
		{
			StringBuilder result = new StringBuilder();
			TagBuilder tagB = new TagBuilder("label");
			tagB.MergeAttribute("for", fieldExpression);
			tagB.InnerHtml = fieldName;
			result.AppendLine(tagB.ToString());
			tagB = new TagBuilder("input");
			tagB.MergeAttribute("type", "text");
			tagB.MergeAttribute("id", fieldExpression);
			tagB.MergeAttribute("name", fieldExpression);
			if (!String.IsNullOrWhiteSpace(fieldValue))
				tagB.MergeAttribute("value", fieldValue);
			result.AppendLine(tagB.ToString());
			MvcHtmlString mvc = MvcHtmlString.Create(result.ToString());
			return mvc;
		}


        public static MvcHtmlString PasswordInputField(this HtmlHelper html, string fieldName, string fieldExpression, string fieldValue)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder tagB = new TagBuilder("label");
            tagB.MergeAttribute("for", fieldExpression);
            tagB.InnerHtml = fieldName;
            result.AppendLine(tagB.ToString());
            tagB = new TagBuilder("input");
            tagB.MergeAttribute("type", "password");
            tagB.MergeAttribute("id", fieldExpression);
            tagB.MergeAttribute("name", fieldExpression);
            if (!String.IsNullOrWhiteSpace(fieldValue))
                tagB.MergeAttribute("value", fieldValue);
            result.AppendLine(tagB.ToString());
            MvcHtmlString mvc = MvcHtmlString.Create(result.ToString());
            return mvc;
        }

        public static MvcHtmlString TextareaInputField(this HtmlHelper html, string fieldName, string fieldExpression, string fieldValue)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder tagB = new TagBuilder("label");
            tagB.MergeAttribute("for", fieldExpression);
            tagB.InnerHtml = fieldName;
            result.AppendLine(tagB.ToString());
            tagB = new TagBuilder("textarea");
            tagB.MergeAttribute("id", fieldExpression);
            tagB.MergeAttribute("name", fieldExpression);
            if (!String.IsNullOrWhiteSpace(fieldValue))
                tagB.InnerHtml = fieldValue;
            result.AppendLine(tagB.ToString());
            MvcHtmlString mvc = MvcHtmlString.Create(result.ToString());
            return mvc;
        }

		public static MvcHtmlString HiddenInputField(this HtmlHelper html, string fieldExpression, string fieldValue)
		{
			StringBuilder result = new StringBuilder();
			TagBuilder tagB = new TagBuilder("input");
			tagB.MergeAttribute("id", fieldExpression);
			tagB.MergeAttribute("name", fieldExpression);
			tagB.MergeAttribute("type", "hidden");
			if (!String.IsNullOrWhiteSpace(fieldValue))
				tagB.MergeAttribute("value", fieldValue);
			result.AppendLine(tagB.ToString());
			MvcHtmlString mvc = MvcHtmlString.Create(result.ToString());
			return mvc;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="aHtml"></param>
		/// <param name="aFieldName">Field name</param>
		/// <param name="aFieldValue">Field value</param>
		/// <param name="isBold">Field value must be bold</param>
		/// <param name="aDisplayNullValue"></param>
		/// <returns></returns>
		public static MvcHtmlString StringField(
			this HtmlHelper aHtml,
			string aFieldName,
			string aFieldValue,
			bool isBold = false,
			bool aDisplayNullValue = true)
		{
			if ((String.IsNullOrWhiteSpace(aFieldValue))&&(!aDisplayNullValue))
				return MvcHtmlString.Empty;

			StringBuilder result = new StringBuilder();
			TagBuilder divTagBuilder = new TagBuilder("div");
			divTagBuilder.AddCssClass("label");
			divTagBuilder.InnerHtml = aFieldName;
			result.AppendLine(divTagBuilder.ToString());

			divTagBuilder = new TagBuilder("div");
			divTagBuilder.AddCssClass("value");
			if (isBold)
			{
				divTagBuilder.MergeAttribute("style", "font-weight:bold;");
			}
			divTagBuilder.InnerHtml = aFieldValue != null ? aFieldValue.Replace("\n","<br/>") : String.Empty;

			result.AppendLine(divTagBuilder.ToString());
			MvcHtmlString mvc = MvcHtmlString.Create(result.ToString());
			return mvc;
		}

        public static MvcHtmlString NullableIntField(this HtmlHelper html, string fieldName, int? fieldValue, bool boldValue = false, bool displayNullValue = true)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder tagB = new TagBuilder("div");
            tagB.AddCssClass("label");
            tagB.InnerHtml = fieldName;
            result.AppendLine(tagB.ToString());
            tagB = new TagBuilder("div");
            tagB.AddCssClass("value");
            if (boldValue)
                tagB.MergeAttribute("style", "font-weight:bold;");
            if (fieldValue!=null)
                tagB.InnerHtml = fieldValue.Value.ToString();
            else
            {
                if (displayNullValue)
                    tagB.InnerHtml = "/";
                else
                    return MvcHtmlString.Empty;
            }
            result.AppendLine(tagB.ToString());
            MvcHtmlString mvc = MvcHtmlString.Create(result.ToString());
            return mvc;
        }

		public static MvcHtmlString NullableBooleanField(this HtmlHelper html, string fieldName, bool? fieldValue)
		{
			StringBuilder result = new StringBuilder();
			TagBuilder tagB = new TagBuilder("div");
			tagB.AddCssClass("label");
			tagB.InnerHtml = fieldName;
			result.AppendLine(tagB.ToString());
			tagB = new TagBuilder("div");
			tagB.AddCssClass("value");
			if (fieldValue != null)
			{
				TagBuilder input = new TagBuilder("input");
				input.MergeAttribute("type", "checkbox");
				input.MergeAttribute("disabled", "disabled");
				input.MergeAttribute("style", "height:15px;");
				if(fieldValue.Value)
					input.MergeAttribute("checked", "checked");
				tagB.InnerHtml = input.ToString();
			}
			else
				tagB.InnerHtml = "/";
			result.AppendLine(tagB.ToString());
			MvcHtmlString mvc = MvcHtmlString.Create(result.ToString());
			return mvc;
		}

		public static string AddPageToUrl(this Uri uri,
			int page)
		{
			return uri.ToString() + "&page=" + page;
		}

		public static MvcHtmlString PageLinks(this HtmlHelper html,
		int totalPage, int currentPage,
		Func<int, string> pageUrl)
		{
			StringBuilder result = new StringBuilder();
			TagBuilder tag;
			if (currentPage != 1)
			{
				tag = new TagBuilder("a"); // Construct an <a> tag
				tag.MergeAttribute("href", pageUrl(1));
				tag.InnerHtml = "first";
				result.AppendLine(tag.ToString());
				tag = new TagBuilder("a"); // Construct an <a> tag
				tag.MergeAttribute("href", pageUrl(currentPage - 1));
				tag.InnerHtml = "previous";
				result.AppendLine(tag.ToString());
			}

			int pageToGoFrom = currentPage;
			if (currentPage >= (totalPage - 10))
				pageToGoFrom = (totalPage - 9);
			if (pageToGoFrom < 1)
				pageToGoFrom = 1;
			for (int i = pageToGoFrom; i < pageToGoFrom + 10 && i <= totalPage; i++)
			{
				tag = new TagBuilder("a"); // Construct an <a> tag
				tag.MergeAttribute("href", pageUrl(i));
				tag.InnerHtml = i.ToString();
				if (i == currentPage)
					tag.AddCssClass("selected");
				result.AppendLine(tag.ToString());
			}
			if (currentPage != totalPage)
			{
				tag = new TagBuilder("a"); // Construct an <a> tag
				tag.MergeAttribute("href", pageUrl(currentPage + 1));
				tag.InnerHtml = "next";
				result.AppendLine(tag.ToString());
				tag = new TagBuilder("a"); // Construct an <a> tag
				tag.MergeAttribute("href", pageUrl(totalPage));
				tag.InnerHtml = "last";
				result.AppendLine(tag.ToString());
			}
			MvcHtmlString mvc = MvcHtmlString.Create(result.ToString());
			return mvc;
		}
	}

	public static class AddFieldHelpers
	{

		public static void ForModelDisplay(this HtmlHelper html, object o)
		{
			if (o is Score)
				html.RenderPartial("ScoreSummary",(Score)o);
			if (o is Source)
				html.RenderPartial("SourceSummary",(Source)o);
			if (o is ScoreSearchItem)
				html.RenderPartial("ScoreSearchItemSummary",(ScoreSearchItem)o);
		}

		public static MvcHtmlString AddField(this HtmlHelper html, string fieldName, string fieldValue)
		{
			if (fieldValue == null)
				return MvcHtmlString.Create("");
			StringBuilder result = new StringBuilder();
			TagBuilder div = new TagBuilder("div");
			div.MergeAttribute("class", "fieldName");
			div.InnerHtml = fieldName;
			result.AppendLine(div.ToString());
			div = new TagBuilder("div");
			div.MergeAttribute("class", "fieldValue");
			div.InnerHtml = fieldValue;
			result.AppendLine(div.ToString());
			return MvcHtmlString.Create(result.ToString());

		}

		public static MvcHtmlString AddField(this HtmlHelper html, string fieldName, int? fieldValue)
		{
			if (fieldValue == null)
				return MvcHtmlString.Create("");
			StringBuilder result = new StringBuilder();
			TagBuilder div = new TagBuilder("div");
			div.MergeAttribute("class", "fieldName");
			div.InnerHtml = fieldName;
			result.AppendLine(div.ToString());
			div = new TagBuilder("div");
			div.MergeAttribute("class", "fieldValue");
			div.InnerHtml = fieldValue.ToString();
			result.AppendLine(div.ToString());
			return MvcHtmlString.Create(result.ToString());
		}

		public static MvcHtmlString AddFieldEdit(this HtmlHelper html, string fieldName, string fieldValue)
		{
			StringBuilder result = new StringBuilder();
			TagBuilder div = new TagBuilder("div");
			TagBuilder input = new TagBuilder("input");
			div.MergeAttribute("class", "fieldName");
			div.InnerHtml = fieldName;
			result.AppendLine(div.ToString());
			div = new TagBuilder("div");
			div.MergeAttribute("class", "fieldValue");
			input.MergeAttribute("type", "text");
			input.InnerHtml = fieldValue;
			div.InnerHtml = input.ToString();
			result.AppendLine(div.ToString());
			return MvcHtmlString.Create(result.ToString());
		}


		public static MvcHtmlString AddFieldEdit(this HtmlHelper html, string fieldName, int? fieldValue)
		{
			StringBuilder result = new StringBuilder();
			TagBuilder div = new TagBuilder("div");
			TagBuilder input = new TagBuilder("input");
			div.MergeAttribute("class", "fieldName");
			div.InnerHtml = fieldName;
			result.AppendLine(div.ToString());
			div = new TagBuilder("div");
			div.MergeAttribute("class", "fieldValue");
			input.MergeAttribute("type", "text");
			if (fieldValue != null)
				input.InnerHtml = fieldValue.ToString();
			div.InnerHtml = input.ToString();
			result.AppendLine(div.ToString());
			return MvcHtmlString.Create(result.ToString());
		}
	}
}