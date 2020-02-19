using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using QuoteManager.Filters;

namespace QuoteManager
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.EnableCors(new EnableCorsAttribute("*", headers:"*", methods:"*"));
            config.Filters.Add(new JwtAuthenticationFilter());
            // Web API configuration and services
            config.Filters.Add(new AuthorizeAttribute());
            // Web API routes

            var settings = config.Formatters.JsonFormatter.SerializerSettings;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            settings.Formatting = Formatting.Indented;

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: $"api/{{controller}}/{{id}}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "ApiByLogin",
                routeTemplate: "api/Login/{username}/{password}",
                defaults: new { controller = "Login", action = "Login" }
            );
            config.Routes.MapHttpRoute(
                name: "ApiByRegister",
                routeTemplate: "api/Register",
                defaults: new { controller = "Register"}
            );

            config.Filters.Add(new QuoteExceptionFilter());

        }
    }
}
