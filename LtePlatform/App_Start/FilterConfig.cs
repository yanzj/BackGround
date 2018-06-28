using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace LtePlatform
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CorsAttribute : Attribute
    {
        public Uri[] AllowOrigins { get; }

        public string ErrorMessage { get; private set; }

        public CorsAttribute(params string[] allowOrigins)
        {
            AllowOrigins = (allowOrigins ?? new string[0]).Select(origin => new Uri(origin)).ToArray();
        }

        public bool TryEvaluate(HttpRequestMessage request, ref IDictionary<string, string> headers)
        {
            if (!request.Headers.Contains("Origin")) return true;
            var origin = request.Headers.GetValues("Origin").First();
            var originUri = new Uri(origin);
            if (AllowOrigins.Contains(originUri))
            {
                GenerateResponseHeaders(headers, request, origin);
                return true;
            }
            ErrorMessage = "Cross-origin request denied";
            return false;
        }

        private void GenerateResponseHeaders(IDictionary<string, string> headers, HttpRequestMessage request, string origin)
        {
            headers.Add("Access-Control-Allow-Origin", "*");
            if (!request.IsPreflightRequest()) return;
            headers.Add("Access-Control-Allow-Methods", "*");
            var requestHeaders = request.Headers.GetValues("Access-Control-Request-Headers").FirstOrDefault();
            if (!string.IsNullOrEmpty(requestHeaders))
            {
                headers.Add("Access-Control-Allow-Headers", requestHeaders);
            }
        } 
    }

    public static class HttpRequestMessageExtensions
    {
        public static bool IsPreflightRequest(this HttpRequestMessage request)
        {
            return request.Method == HttpMethod.Options && request.Headers.GetValues("Origin").Any() &&
                   request.Headers.GetValues("Access-Control-Request-Method").Any();
        }
    }

    public class CorsMessageHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var originalMethod = request.Method;
            var isPreflightRequest = request.IsPreflightRequest();
            if (isPreflightRequest)
            {
                var method = request.Headers.GetValues("Access-Control-Request-Method").First();
                request.Method = new HttpMethod(method);
            }
            var configuration = request.GetConfiguration();
            var controllerDescriptor = configuration.Services.GetHttpControllerSelector().SelectController(request);
            var controllerContext = new HttpControllerContext(request.GetConfiguration(), request.GetRouteData(),
                request)
            {
                ControllerDescriptor = controllerDescriptor
            };
            var actionDescriptor = configuration.Services.GetActionSelector().SelectAction(controllerContext);
            var corsAttribute = actionDescriptor.GetCustomAttributes<CorsAttribute>().FirstOrDefault() ??
                                controllerDescriptor.GetCustomAttributes<CorsAttribute>().FirstOrDefault();
            if (corsAttribute == null)
            {
                return base.SendAsync(request, cancellationToken);
            }
            IDictionary<string, string> headers = new Dictionary<string, string>();
            request.Method = originalMethod;
            bool authorized = corsAttribute.TryEvaluate(request, ref headers);
            HttpResponseMessage response;
            if (isPreflightRequest)
            {
                response = authorized ? new HttpResponseMessage(HttpStatusCode.OK) : request.CreateErrorResponse(HttpStatusCode.BadRequest, corsAttribute.ErrorMessage);
            }
            else
            {
                response = base.SendAsync(request, cancellationToken).Result;
            }
            foreach (var item in headers)
            {
                response.Headers.Add(item.Key,item.Value);
            }
            return Task.FromResult(response);
        }
    }
}
