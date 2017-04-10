using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FindTheBooty
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }/*,
                constraints: new { controller = "Home|Account|Admin|Data|Manage|Participating" }*/
                //TODO: !!!PRODUCTION!!! Uncomment lines above for production
            );
            /*routes.MapRoute(
                name: "404",
                url: "{*url}",
                defaults: new { controller = "Error", action = "NotFound" }
            );*/
            //TODO: !!!PRODUCTION!!! Uncomment lines above for production
        }
    }
}
