using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BookService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            EnableCrossSiteRequests(config);
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
        }
        private static void EnableCrossSiteRequests(HttpConfiguration httpConfiguration)
        {
            var cors = new EnableCorsAttribute(
                origins: "*",
                headers: "*",
                methods: "*");
            httpConfiguration.EnableCors(cors);
        }
    }
}
