using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Razor;
using Demo.AspNetCore.Mvc.Http2.Http.Extensions;

namespace Demo.AspNetCore.Mvc.Http2.Mvc.Razor
{
    public class Http2ViewLocationExpander : IViewLocationExpander
    {
        private const string PROTOCOL_SUFFIX_KEY = "PROTOCOL_SUFFIX";

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            context.Values.TryGetValue(PROTOCOL_SUFFIX_KEY, out string protocolSuffix);

            if (String.IsNullOrWhiteSpace(protocolSuffix))
            {
                return viewLocations;
            }

            return ExpandViewLocationsCore(viewLocations, protocolSuffix);
        }

        private IEnumerable<string> ExpandViewLocationsCore(IEnumerable<string> viewLocations, string protocolSuffix)
        {
            foreach (var location in viewLocations)
            {
                yield return location.Insert(location.LastIndexOf('.'), protocolSuffix);
                yield return location;
            }
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            context.Values[PROTOCOL_SUFFIX_KEY] = context.ActionContext.HttpContext.Request.IsHttp2() ? "-h2" : null;
        }
    }
}
