using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Demo.AspNetCore.Mvc.Http2.Http.Extensions;

namespace Demo.AspNetCore.Mvc.Http2.Mvc.TagHelpers
{
    [HtmlTargetElement(Attributes = HTTP2_CLASS_ATTRIBUTE_NAME)]
    public class Http2ClassTagHelper : TagHelper
    {
        private const string HTTP2_CLASS_ATTRIBUTE_NAME = "asp-http2-class";
        private const string CLASS_ATTRIBUTE_NAME = "class";

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        [HtmlAttributeName(HTTP2_CLASS_ATTRIBUTE_NAME)]
        public string Http2Class { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!String.IsNullOrWhiteSpace(Http2Class) && ViewContext.HttpContext.Request.IsHttp2())
            {
                output.Attributes.SetAttribute(CLASS_ATTRIBUTE_NAME, Http2Class);
            }

            output.Attributes.RemoveAll(HTTP2_CLASS_ATTRIBUTE_NAME);
        }
    }
}
