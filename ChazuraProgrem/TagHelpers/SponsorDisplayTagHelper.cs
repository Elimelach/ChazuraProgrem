using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChazuraProgram.Models;

namespace ChazuraProgram.TagHelpers
{
    [HtmlTargetElement("sponsor-display",Attributes ="sponsor")]
    public class SponsorDisplayTagHelper:TagHelper
    {
        [HtmlAttributeName("sponsor")]
        public ISponsorPoints Sponsor { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string name = Sponsor.DescriptionName?.Trim() ?? "";
            string by = Sponsor.DescriptionElse?.Trim() ?? "";
            string[] startEnd = Sponsor.GetSponserType switch
            {
                SponserType.InHonor => new string[2] { "לכבוד", "הי\"ו" },
                SponserType.InMemory => new string[2] { "לעילו נשמת", "ע\"ה" },
                SponserType.ToHealth => new string[2] { "לרפואת", "הי\"ו" },
                SponserType.Other => new string[2] { "", "" },
                SponserType.ToSake => new string[2] { "לזכות", "" },
                _ => throw new ArgumentOutOfRangeException()
            };
            output.BuildTag("div");
            if (by != "")
            {
                TagBuilder builder = new TagBuilder("label");
                builder.InnerHtml.Append($"Sponsored by: {by}");
                builder.Attributes.Add("class", "mr-2");
                output.Content.AppendHtml(builder);
                builder = new TagBuilder("label");
                builder.InnerHtml.Append("נתנדב היום ע\"י");
                output.Content.AppendHtml(builder);
                output.Content.AppendHtml("<br/>");
            }
            if (name !="")
            {
                TagBuilder builder = new TagBuilder("label");
                builder.InnerHtml.Append(startEnd[1]);
                output.Content.AppendHtml(builder);
                builder = new TagBuilder("label");
                builder.InnerHtml.Append($" {name} ");
                builder.Attributes.Add("class", "ml-2 mr-2");
                output.Content.AppendHtml(builder);
                builder = new TagBuilder("label");
                builder.InnerHtml.Append(startEnd[0]);
                output.Content.AppendHtml(builder);
                
                
            }
            if (by == "" && name == "") 
            {
                TagBuilder builder = new TagBuilder("label");
                builder.InnerHtml.Append($"Sponsored by anonymous donor");
                output.Content.AppendHtml(builder);
            }
           
        }
    }
}
