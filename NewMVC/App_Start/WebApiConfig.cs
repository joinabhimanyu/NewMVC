using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace NewMVC
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: new { id = @"\d" }
            );

            config.Routes.MapHttpRoute(
                name: "ApiRoute",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "CustomersApi",
                routeTemplate: "customers/{controller}/{customerID}",
                defaults: new { controller = "CustomersApi", customerID = RouteParameter.Optional },
                constraints: new { customerID = @"[a-zA-Z0-9]*" }
            );
        }
    }
}
