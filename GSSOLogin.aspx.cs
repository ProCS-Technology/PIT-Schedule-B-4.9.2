using Saml;
using System;
using System.Configuration;

namespace ProcsDLL
{
    public partial class GSSOLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var samlEndpoint = ConfigurationManager.AppSettings["GSAMLUrl"];
            var entityId = ConfigurationManager.AppSettings["GEntityId"];
            var redirectUri = ConfigurationManager.AppSettings["GRedirectUri"];

            var request = new AuthRequest(
                entityId, //TODO: put your app's "entity ID" here
                redirectUri //TODO: put Assertion Consumer URL (where the provider should redirect users after authenticating)
                );

            //redirect the user to the SAML provider
            Response.Redirect(request.GetRedirectUrl(samlEndpoint));
        }
    }
}