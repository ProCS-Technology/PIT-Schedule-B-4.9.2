using System.Web.Http;
using System.Web.Routing;
namespace ProcsDLL
{
    public static class WebApiConfig
    {
        public static void Register(RouteCollection routes)
        {
            routes.MapHttpRoute(
               name: "DefaultApi",
               routeTemplate: "api/{controller}/{action}/{id}",
               defaults: new { id = RouteParameter.Optional }
           );
        }
    }
}