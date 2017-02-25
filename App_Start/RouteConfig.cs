using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Movies
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "TopMovies",
               url: "{controller}/{action}/{pageNumber}",
               defaults: new { controller = "TopMovies", action = "Index", pageNumber =1 }
           );
            routes.MapRoute(
                name: "Movies",
                url: "{controller}/{action}/{search}",
                defaults: new { controller = "Movies", action = "Index", search = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
