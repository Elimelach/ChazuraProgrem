#pragma checksum "C:\Users\eli\source\repos\ChazuraProgrem\ChazuraProgrem\Areas\Admin\Views\Sponsor\Detail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "aa291edceb149e0a0b3bccd9dc595db6faa3b595"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_Sponsor_Detail), @"mvc.1.0.view", @"/Areas/Admin/Views/Sponsor/Detail.cshtml")]
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
#line 1 "C:\Users\eli\source\repos\ChazuraProgrem\ChazuraProgrem\Areas\Admin\Views\_ViewImports.cshtml"
using ChazuraProgram;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\eli\source\repos\ChazuraProgrem\ChazuraProgrem\Areas\Admin\Views\_ViewImports.cshtml"
using ChazuraProgram.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\eli\source\repos\ChazuraProgrem\ChazuraProgrem\Areas\Admin\Views\_ViewImports.cshtml"
using ChazuraProgram.Areas.Admin.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"aa291edceb149e0a0b3bccd9dc595db6faa3b595", @"/Areas/Admin/Views/Sponsor/Detail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6d46bf40a2e0512e82355587f07ca4cf0c208f2c", @"/Areas/Admin/Views/_ViewImports.cshtml")]
    public class Areas_Admin_Views_Sponsor_Detail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<User>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\eli\source\repos\ChazuraProgrem\ChazuraProgrem\Areas\Admin\Views\Sponsor\Detail.cshtml"
  
    ViewBag.Title = "User detail";

#line default
#line hidden
#nullable disable
            WriteLiteral("<h1>User details</h1>\r\n\r\n\r\n<h4>");
#nullable restore
#line 8 "C:\Users\eli\source\repos\ChazuraProgrem\ChazuraProgrem\Areas\Admin\Views\Sponsor\Detail.cshtml"
Write(Model.FirstName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 8 "C:\Users\eli\source\repos\ChazuraProgrem\ChazuraProgrem\Areas\Admin\Views\Sponsor\Detail.cshtml"
                Write(Model.LastName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n");
#nullable restore
#line 9 "C:\Users\eli\source\repos\ChazuraProgrem\ChazuraProgrem\Areas\Admin\Views\Sponsor\Detail.cshtml"
  
    if (!string.IsNullOrEmpty(Model.Address))
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div>\r\n            <p>");
#nullable restore
#line 13 "C:\Users\eli\source\repos\ChazuraProgrem\ChazuraProgrem\Areas\Admin\Views\Sponsor\Detail.cshtml"
          Write(Model.Address);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 13 "C:\Users\eli\source\repos\ChazuraProgrem\ChazuraProgrem\Areas\Admin\Views\Sponsor\Detail.cshtml"
                         Write(Model.City);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 13 "C:\Users\eli\source\repos\ChazuraProgrem\ChazuraProgrem\Areas\Admin\Views\Sponsor\Detail.cshtml"
                                     Write(Model.State);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 13 "C:\Users\eli\source\repos\ChazuraProgrem\ChazuraProgrem\Areas\Admin\Views\Sponsor\Detail.cshtml"
                                                  Write(Model.ZipCode);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n        </div>\r\n");
#nullable restore
#line 15 "C:\Users\eli\source\repos\ChazuraProgrem\ChazuraProgrem\Areas\Admin\Views\Sponsor\Detail.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n    <div class=\"col-sm-3\">\r\n        <p>Username: ");
#nullable restore
#line 20 "C:\Users\eli\source\repos\ChazuraProgrem\ChazuraProgrem\Areas\Admin\Views\Sponsor\Detail.cshtml"
                Write(Model.UserName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n    </div>\r\n    <div class=\"col-sm-3\"> <p>Password: ");
#nullable restore
#line 22 "C:\Users\eli\source\repos\ChazuraProgrem\ChazuraProgrem\Areas\Admin\Views\Sponsor\Detail.cshtml"
                                   Write(Model.SavedPassword);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p></div>\r\n    <div class=\"col-sm-3\"><p>Email: ");
#nullable restore
#line 23 "C:\Users\eli\source\repos\ChazuraProgrem\ChazuraProgrem\Areas\Admin\Views\Sponsor\Detail.cshtml"
                               Write(Model.Email);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p></div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<User> Html { get; private set; }
    }
}
#pragma warning restore 1591
