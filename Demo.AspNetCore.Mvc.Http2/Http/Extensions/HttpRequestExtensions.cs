using Microsoft.AspNetCore.Http;

namespace Demo.AspNetCore.Mvc.Http2.Http.Extensions
{
    internal static class HttpRequestExtensions
    {
        private const string HTTP2_PROTOCOL = "HTTP/2";

        public static bool IsHttp2(this HttpRequest request)
        {
            return (request.Protocol == HTTP2_PROTOCOL);
        }
    }
}
