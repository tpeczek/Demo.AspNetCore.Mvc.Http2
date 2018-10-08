using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Primitives;

namespace Demo.AspNetCore.Mvc.Http2.Mvc.TagHelpers
{
    public class ProtocolTagHelper : TagHelper
    {
        private static readonly char[] PROTOCOLS_SEPARATOR = new[] { ',' };

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override int Order => -1000;

        public string Include { get; set; }

        public string Exclude { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            output.TagName = null;

            if (String.IsNullOrWhiteSpace(Include) && String.IsNullOrWhiteSpace(Exclude))
            {
                return;
            }

            string currentProtocol = ViewContext.HttpContext.Request.Protocol;

            if (Exclude != null)
            {
                StringTokenizer excludeTokenizer = new StringTokenizer(Exclude, PROTOCOLS_SEPARATOR);

                foreach (StringSegment excludeToken in excludeTokenizer)
                {
                    StringSegment excludedProtocol = excludeToken.Trim();

                    if (excludedProtocol.HasValue && excludedProtocol.Length > 0)
                    {

                        if (excludedProtocol.Equals(currentProtocol, StringComparison.OrdinalIgnoreCase))
                        {
                            output.SuppressOutput();
                            return;
                        }
                    }
                }
            }

            bool hasIncludeProtocols = false;

            if (Include != null)
            {
                StringTokenizer includeTokenizer = new StringTokenizer(Include, PROTOCOLS_SEPARATOR);

                foreach (StringSegment includedToken in includeTokenizer)
                {
                    StringSegment includedProtocol = includedToken.Trim();

                    if (includedProtocol.HasValue && includedProtocol.Length > 0)
                    {

                        hasIncludeProtocols = true;

                        if (includedProtocol.Equals(currentProtocol, StringComparison.OrdinalIgnoreCase))
                        {
                            return;
                        }
                    }
                }
            }

            if (hasIncludeProtocols)
            {
                output.SuppressOutput();
            }
        }
    }
}
