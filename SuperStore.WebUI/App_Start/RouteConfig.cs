using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SuperStore.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //shows all categories - /
            routes.MapRoute(
                name: null,
                url: "",
                defaults: new { controller = "Product", action = "List", category = (string)null, page = 1 }
             );

            //shows all categories - /Page2
            routes.MapRoute(
                name: null,
                url: "Page{page}",
                defaults: new { controller = "Product", action = "List", category = (string)null },
                new {page = @"\d+" }
            );

            //shows all categories - /Soccer
            routes.MapRoute(
                name: null,
                url: "{category}",
                defaults: new { controller = "Product", action = "List" , page = 1 }
            );

            //shows all categories - /Soccer/Page2
            routes.MapRoute(
                name: null,
                url: "{category}/Page{page}",
                defaults: new { controller = "Product", action = "List" },
                new { page = @"\d+" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Product", action = "List", id = UrlParameter.Optional }
            );
        }
    }
}
