using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProcsDLL.Models.Infrastructure;
using System.Text;
using System.Security.Cryptography;
namespace ProcsDLL.InsiderTrading
{
    public partial class Office365Token : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var state = RandomGenerator.GenerateRandomString(64);
                var pkce = RandomGenerator.GenerateRandomString(64);
                var sha = SHA256.Create();
                var PkceHashed = Base64UrlEncoder.Encode(sha.ComputeHash(Encoding.ASCII.GetBytes(pkce)));

                txtPkce.Text = pkce;
                txtState.Text = state;
                txtPkceHashed.Text = PkceHashed;
            }
        }
    }
}