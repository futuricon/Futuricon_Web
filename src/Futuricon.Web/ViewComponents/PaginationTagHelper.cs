using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Futuricon.Web.ViewComponents
{
    /// <summary>
    /// Tag helper to render pagination UI.
    /// Requires client-side Bootstrap 4 files to display correctly.
    /// </summary>
    [HtmlTargetElement("pagination")]
    public class PaginationTagHelper : TagHelper
    {

        /// <summary>
        /// TotalPages - number of pages, default value is 10
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// PageIndex - number of the current page, default value is 1
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// PagePath - page path, default value is <c>"./Index"</c>
        /// </summary>
        public string PagePath { get; set; }

        /// <summary>
        /// PageHandler - page handler name to pass in query of the URL, default value is <c>"pageIndex"</c>
        /// </summary>
        public string PageHandler { get; set; }

        /// <summary>
        /// PageFragment - page fragment name to pass in query of the URL, default value is <c>null</c>
        /// </summary>
        public string PageFragment { get; set; }

        public PaginationTagHelper()
        {
            PageHandler = "pageIndex";
            //PagePath = "./blog";
            TotalPages = 10;
            PageIndex = 1;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var prevDisabled = PageIndex > 1 ? "" : "disabled";
            var nextDisabled = PageIndex < TotalPages ? "" : "disabled";

            output.TagName = "pagination";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Content.SetHtmlContent("<ul class='list-inline blog-nav mb-0'>");
            output.Content.AppendHtml($"\n<li class='{prevDisabled} outer radius-2 list-inline-item mr-1'>" +
                $"<a class='inner btn btn-round btn-icon' href='{PagePath}?{PageHandler}={PageIndex - 1}#{PageFragment}'><span class='fa fa-angle-left btn-icon-inner'></span></a></li>");

            if (TotalPages <= 10)
            {
                for (var i = 1; i <= TotalPages; i++)
                {
                    var activeClassName = i == PageIndex ? "active" : "";
                    output.Content.AppendHtml($"\n<li class='{activeClassName} outer radius-2 list-inline-item mr-1'>" +
                        $"<a class='{activeClassName} inner btn btn-round btn-icon' href='{PagePath}?{PageHandler}={i}#{PageFragment}'><span class='btn-icon-inner'>{i}</span></a></li>");
                }
            }
            else
            {
                var activeClassName = PageIndex == 1 ? "active" : "";

                if (PageIndex - 4 > 1)
                {
                    output.Content.AppendHtml($"\n<li class='{activeClassName} outer radius-2 list-inline-item mr-1'>" +
                        $"<a class='{activeClassName} inner btn btn-round btn-icon' href='{PagePath}?{PageHandler}=1#{PageFragment}'><span class='btn-icon-inner'>1</span></a></li>");
                    output.Content.AppendHtml($"\n<li class='disabled outer radius-2 list-inline-item mr-1'>" +
                        $"<a class='inner btn btn-round btn-icon'><span class='btn-icon-inner'>...</span></a></li>");
                }

                for (var i = PageIndex - 4; i <= PageIndex + 4; i++)
                {
                    if (i > TotalPages)
                        break;

                    if (i <= 0)
                        continue;
                    activeClassName = i == PageIndex ? "active" : "";
                    output.Content.AppendHtml($"\n<li class='{activeClassName} outer radius-2 list-inline-item mr-1'>" +
                        $"<a class='{activeClassName} inner btn btn-round btn-icon' href='{PagePath}?{PageHandler}={i}#{PageFragment}'><span class='btn-icon-inner'>{i}</span></a></li>");
                }

                if (TotalPages - PageIndex > 4)
                {
                    activeClassName = PageIndex == TotalPages ? "active" : "";
                    output.Content.AppendHtml($"\n<li class='disabled outer radius-2 list-inline-item mr-1'>" +
                        $"<a class='inner btn btn-round btn-icon'><span class='btn-icon-inner'>...</span></a></li>");
                    output.Content.AppendHtml($"\n<li class='{activeClassName} outer radius-2 list-inline-item mr-1'>" +
                        $"<a class='{activeClassName} inner btn btn-round btn-icon' href='{PagePath}?{PageHandler}={TotalPages}#{PageFragment}'><span class='btn-icon-inner'>{TotalPages}</span></a></li>");
                }
            }

            output.Content.AppendHtml($"\n<li class='{nextDisabled} outer radius-2 list-inline-item mr-1'>" +
                $"<a class='{nextDisabled} inner btn btn-round btn-icon' href='{PagePath}?{PageHandler}={PageIndex + 1}#{PageFragment}'><span class='fa fa-angle-right btn-icon-inner'></span></a></li>");
            output.Content.AppendHtml("</ul>");
        }
    }
}
