using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;    // [ViewContext] attribute
using Microsoft.AspNetCore.Mvc.Rendering;       // ViewContext data type

namespace ChazuraProgram.TagHelpers
{
    [HtmlTargetElement( Attributes = "mark-it-active")]
    public class ActiveNavbarTagHelper : TagHelper
    {
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewCtx { get; set; }

        [HtmlAttributeName("my-mark-area-active")]
        public bool IsAreaOnly { get; set; }
        [HtmlAttributeName("mark-it-active")]
        public bool MarkItActive { get; set; }
        [HtmlAttributeName("action-only")]
        public bool ActionActive { get; set; }
        public bool FilterOnly { get; set; }
        public bool StartFilterOnly { get; set; }

        public override void Process(TagHelperContext context,
        TagHelperOutput output)
        {
            string area = ViewCtx.RouteData.Values["area"]?.ToString() ?? "";
            string ctlr = ViewCtx.RouteData.Values["controller"]?.ToString();
            string actn = ViewCtx.RouteData.Values["action"].ToString();
            string fltr = ViewCtx.RouteData.Values["filter"]?.ToString() ?? "all";
            string strtfltr = ViewCtx.RouteData.Values["FilterTime"]?.ToString() ?? "last-month";


            string aspArea = context.AllAttributes["asp-area"]?.Value?.ToString() ?? "";
            string aspCtlr = context.AllAttributes["asp-controller"]?.Value?.ToString();
            string aspActn = context.AllAttributes["asp-action"]?.Value?.ToString();
            string tagFilter = context.AllAttributes["filter-field"]?.Value?.ToString() ?? "";
            string tagStartFltr = context.AllAttributes["start-filter"]?.Value?.ToString() ?? "";

            if (!ActionActive && area == aspArea && ctlr == aspCtlr)
                output.Attributes.AppendCssClass("active");
            else if (IsAreaOnly && area == aspArea)
                output.Attributes.AppendCssClass("active");
            else if (ActionActive && area == aspArea && ctlr == aspCtlr && actn == aspActn)
                output.Attributes.AppendCssClass("active");
            else if (FilterOnly && tagFilter == fltr)
                output.Attributes.AppendCssClass("active");
            else if (StartFilterOnly && strtfltr == tagStartFltr)
                output.Attributes.AppendCssClass("active");
        }
    }
}
