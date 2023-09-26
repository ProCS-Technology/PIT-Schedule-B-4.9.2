using System;
using System.Web;
using System.Web.Routing;

namespace ProcsDLL
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            WebApiConfig.Register(RouteTable.Routes);
        }
        protected void Application_PostAuthorizeRequest()
        {
            System.Web.HttpContext.Current.SetSessionStateBehavior(System.Web.SessionState.SessionStateBehavior.Required);
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            if (ex is HttpRequestValidationException)
            {
                Response.Clear();
                Response.StatusCode = 200;
                Response.Redirect("~/XSSError.htm");
                Response.End();
            }
            if (ex is HttpUnhandledException)
            {
                Response.Clear();
                Response.StatusCode = 200;
                Response.Redirect("~/ErrorPage.htm");
                Response.End();
            }
        }
    }
}