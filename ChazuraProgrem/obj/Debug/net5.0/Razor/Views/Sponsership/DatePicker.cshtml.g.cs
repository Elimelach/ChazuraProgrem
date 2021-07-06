#pragma checksum "C:\Users\eli\source\repos\ChazuraProgrem\ChazuraProgrem\Views\Sponsership\DatePicker.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1428657265057fcde41a6e88b7b691560769fceb"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Sponsership_DatePicker), @"mvc.1.0.view", @"/Views/Sponsership/DatePicker.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\eli\source\repos\ChazuraProgrem\ChazuraProgrem\Views\_ViewImports.cshtml"
using ChazuraProgram;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\eli\source\repos\ChazuraProgrem\ChazuraProgrem\Views\_ViewImports.cshtml"
using ChazuraProgram.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1428657265057fcde41a6e88b7b691560769fceb", @"/Views/Sponsership/DatePicker.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3b5f81b224db223d8bccc307ba05385025ca0faf", @"/Views/_ViewImports.cshtml")]
    public class Views_Sponsership_DatePicker : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<SponserDTO>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "DatePicker", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("spin-at-Submit"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\eli\source\repos\ChazuraProgrem\ChazuraProgrem\Views\Sponsership\DatePicker.cshtml"
  
    ViewBag.Title = "Pick day to sponsor";
    DateTime dateStart = Model[0].Date;
    bool goPast = dateStart.Date > DateTime.Now.Date;
    bool goFwd = dateStart.Date < DateTime.Now.AddMonths(6).Date;

#line default
#line hidden
#nullable disable
            WriteLiteral("<h1>Select a date you want to sponsor</h1>\r\n<h2>Price is $36.00</h2>\r\n<div class=\"row\">\r\n    <div class=\"col-sm-6\"> <h6>To submit click your desired date</h6></div>\r\n    <div class=\"col-sm-6 form-group\">\r\n");
#nullable restore
#line 13 "C:\Users\eli\source\repos\ChazuraProgrem\ChazuraProgrem\Views\Sponsership\DatePicker.cshtml"
          
            if (goPast)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1428657265057fcde41a6e88b7b691560769fceb5607", async() => {
                WriteLiteral("Back 30 days");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-date", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 16 "C:\Users\eli\source\repos\ChazuraProgrem\ChazuraProgrem\Views\Sponsership\DatePicker.cshtml"
                                               WriteLiteral(dateStart.AddDays(-30).GetDashDate());

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["date"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-date", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["date"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 18 "C:\Users\eli\source\repos\ChazuraProgrem\ChazuraProgrem\Views\Sponsership\DatePicker.cshtml"
            }
            if (goFwd)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1428657265057fcde41a6e88b7b691560769fceb8176", async() => {
                WriteLiteral("Forward 30 days");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-date", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 21 "C:\Users\eli\source\repos\ChazuraProgrem\ChazuraProgrem\Views\Sponsership\DatePicker.cshtml"
                                               WriteLiteral(dateStart.AddDays(30).GetDashDate());

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["date"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-date", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["date"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 23 "C:\Users\eli\source\repos\ChazuraProgrem\ChazuraProgrem\Views\Sponsership\DatePicker.cshtml"
            }
        

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n</div>\r\n<div class=\"row text-center\">\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1428657265057fcde41a6e88b7b691560769fceb10765", async() => {
                WriteLiteral("\r\n");
#nullable restore
#line 29 "C:\Users\eli\source\repos\ChazuraProgrem\ChazuraProgrem\Views\Sponsership\DatePicker.cshtml"
          
            foreach (var dto in Model)
            {
                if (dto.Status == Status.rejected)
                {

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <button type=\"submit\"");
                BeginWriteAttribute("value", " value=\"", 1201, "\"", 1218, 1);
#nullable restore
#line 34 "C:\Users\eli\source\repos\ChazuraProgrem\ChazuraProgrem\Views\Sponsership\DatePicker.cshtml"
WriteAttributeValue("", 1209, dto.Date, 1209, 9, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" name=\"Date\" class=\"submit-noStyle p-0 bg-transparent rounded\">\r\n                        <section class=\"date-pick rounded-lg card\">\r\n                            <p class=\"card-text\">");
#nullable restore
#line 36 "C:\Users\eli\source\repos\ChazuraProgrem\ChazuraProgrem\Views\Sponsership\DatePicker.cshtml"
                                            Write(CalendarHebrew.GetHebrewDateString(dto.Date));

#line default
#line hidden
#nullable disable
                WriteLiteral("</p>\r\n                            <p class=\"card-text\">");
#nullable restore
#line 37 "C:\Users\eli\source\repos\ChazuraProgrem\ChazuraProgrem\Views\Sponsership\DatePicker.cshtml"
                                            Write(dto.Date.ToShortDateString());

#line default
#line hidden
#nullable disable
                WriteLiteral("</p>\r\n                            <p class=\"card-text\">");
#nullable restore
#line 38 "C:\Users\eli\source\repos\ChazuraProgrem\ChazuraProgrem\Views\Sponsership\DatePicker.cshtml"
                                            Write(StatusStringefy.GetStatusStringForAvalDates(dto.Status));

#line default
#line hidden
#nullable disable
                WriteLiteral("</p>\r\n                        </section>\r\n                    </button>\r\n");
#nullable restore
#line 41 "C:\Users\eli\source\repos\ChazuraProgrem\ChazuraProgrem\Views\Sponsership\DatePicker.cshtml"
                }
                else
                {

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <button disabled class=\"submit-noStyle p-0 bg-transparent rounded\">\r\n                        <section class=\"date-pick rounded-lg card\">\r\n                            <p>");
#nullable restore
#line 46 "C:\Users\eli\source\repos\ChazuraProgrem\ChazuraProgrem\Views\Sponsership\DatePicker.cshtml"
                          Write(CalendarHebrew.GetHebrewDateString(dto.Date));

#line default
#line hidden
#nullable disable
                WriteLiteral("</p>\r\n                            <p>");
#nullable restore
#line 47 "C:\Users\eli\source\repos\ChazuraProgrem\ChazuraProgrem\Views\Sponsership\DatePicker.cshtml"
                          Write(dto.Date.ToShortDateString());

#line default
#line hidden
#nullable disable
                WriteLiteral("</p>\r\n                            <p>");
#nullable restore
#line 48 "C:\Users\eli\source\repos\ChazuraProgrem\ChazuraProgrem\Views\Sponsership\DatePicker.cshtml"
                          Write(StatusStringefy.GetStatusStringForAvalDates(dto.Status));

#line default
#line hidden
#nullable disable
                WriteLiteral("</p>\r\n                        </section>\r\n                    </button>\r\n");
#nullable restore
#line 51 "C:\Users\eli\source\repos\ChazuraProgrem\ChazuraProgrem\Views\Sponsership\DatePicker.cshtml"
                }
            }
        

#line default
#line hidden
#nullable disable
                WriteLiteral("    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<SponserDTO>> Html { get; private set; }
    }
}
#pragma warning restore 1591