using Saml;
using System;
using System.Configuration;
namespace ProcsDLL
{
    public partial class SSOLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var samlEndpoint = ConfigurationManager.AppSettings["SAMLUrl"];
            var entityId = ConfigurationManager.AppSettings["EntityId"];
            var redirectUri = ConfigurationManager.AppSettings["RedirectUri"];
            var request = new AuthRequest(entityId, redirectUri);
            string sUrl = request.GetRedirectUrl(samlEndpoint);
            Response.Redirect(sUrl);
        }
    }
}