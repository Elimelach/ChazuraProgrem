using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;    // [ViewContext] attribute
using Microsoft.AspNetCore.Mvc.Rendering;       // ViewContext data type
using Microsoft.AspNetCore.Routing;             // LinkGenerator
using ChazuraProgram.Models;

namespace ChazuraProgram.TagHelpers
{
    [HtmlTargetElement("my-sorting-link")]
    public class SortingLinkTagHelper : TagHelper
    {
        private readonly LinkGenerator linkBuilder;
        public SortingLinkTagHelper(LinkGenerator lg) => linkBuilder = lg;

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewCtx { get; set; }

        public RouteDictionary Current { get; set; }
        public string SortField { get; set; }
        public string FilterField { get; set; }
        public string StartFilter { get; set; }

        public override void Process(TagHelperContext context,
        TagHelperOutput output)
        {
            var routes = Current.Clone();
            if (!string.IsNullOrEmpty(SortField))
            {
                IfChanged(SortField, Current.SortField, routes);
                routes.SetSortAndDirection(SortField, Current);
            }

            if (!string.IsNullOrEmpty(FilterField))
            {
                IfChanged(FilterField, Current.Filter, routes);
                routes.SetFilter(FilterField);
            }

            if (!string.IsNullOrEmpty(StartFilter))
            {
                IfChanged(StartFilter, Current.FilterTime, routes);
                routes.SetStartFilter(StartFilter);
            }

            string ctlr = ViewCtx.RouteData.Values["controller"].ToString();
            string action = ViewCtx.RouteData.Values["action"].ToString();
            string area = ViewCtx.RouteData.Values["area"]?.ToString() ?? "";
            routes["area"] = area;
            string url = linkBuilder.GetPathByAction(action, ctlr, routes);

            output.BuildLink(url, "text-white");
        }
        private void IfChanged(string newValue, string oldValue,RouteDictionary route)
        {
            if (newValue.ToLower()!= oldValue.ToLower())
            {
                route.PageNumber= 1;
            }
        }
    }
}
