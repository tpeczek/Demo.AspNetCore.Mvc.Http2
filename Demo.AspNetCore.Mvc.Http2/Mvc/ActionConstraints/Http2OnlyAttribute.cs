using System;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Demo.AspNetCore.Mvc.Http2.Http.Extensions;

namespace Demo.AspNetCore.Mvc.Http2.Mvc.ActionConstraints
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class Http2OnlyAttribute : Attribute, IActionConstraint
    {
        public int Order { get; set; }

        public bool Accept(ActionConstraintContext context)
        {
            return context.RouteContext.HttpContext.Request.IsHttp2();
        }
    }
}
