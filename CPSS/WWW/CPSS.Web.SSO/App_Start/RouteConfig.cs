using System.Web.Mvc;
using System.Web.Routing;

namespace CPSS.Web.SSO
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Default", "{controller}/{action}",
                new {controller = "Home", action = "Index"}
                );
        }
    }
}