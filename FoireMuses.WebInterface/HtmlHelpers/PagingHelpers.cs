using System;
using System.Text;
using System.Web.Mvc;
using SportsStore.WebUI.Models;

namespace FoireMuses.WebInterface.HtmlHelpers
{
    public static class PagingHelpers
    {
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
                tag.MergeAttribute("href", pageUrl(currentPage-1));
                tag.InnerHtml = "previous";
                result.AppendLine(tag.ToString());
            }
     
            int pageToGoFrom = currentPage;
            if (currentPage >= (totalPage - 10))
                pageToGoFrom = (totalPage - 9);
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
                tag.MergeAttribute("href", pageUrl(currentPage+1));
                tag.InnerHtml = "next";
                result.AppendLine(tag.ToString());
                tag = new TagBuilder("a"); // Construct an <a> tag
                tag.MergeAttribute("href", pageUrl(totalPage));
                tag.InnerHtml = "last";
                result.AppendLine(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}