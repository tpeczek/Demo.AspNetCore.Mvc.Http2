using System;
using Demo.AspNetCore.Mvc.Http2.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Demo.AspNetCore.Mvc.Http2.Mvc.Rendering
{
    public static class HtmlHelperHttp2Extensions
    {
        public static bool IsHttp2(this IHtmlHelper htmlHelper)
        {
            if (htmlHelper == null)
            {
                throw new ArgumentNullException(nameof(htmlHelper));
            }

            return htmlHelper.ViewContext.HttpContext.Request.IsHttp2();
        }
    }
}
