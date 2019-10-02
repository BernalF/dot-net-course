using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace WebServer.Utils
{
    public class UrlUtils
    {
        public static string GetAbsoluteUrl(HttpRequest request)
        {
            var uriBuilder = new UriBuilder
            {
                Host = request.Host.Host,
                Scheme = request.Scheme
            };

            if (request.Host.Port.HasValue)
                uriBuilder.Port = request.Host.Port.Value;

            return uriBuilder.ToString();
        }

        public static string GetControllerAbsoluteUrl(HttpRequest request, LinkGenerator linkGenerator, string actionName, string controllerName, object values)
        {
            var absoluteUrl = GetAbsoluteUrl(request);

            if (absoluteUrl.EndsWith("/"))
            {
                absoluteUrl = absoluteUrl.Remove(absoluteUrl.Length - 1);
            }
            absoluteUrl += linkGenerator.GetPathByAction(actionName,
                                                        controllerName,
                                                        values);

            return absoluteUrl;
        }
    }
}
