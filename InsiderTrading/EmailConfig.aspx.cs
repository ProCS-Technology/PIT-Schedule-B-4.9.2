using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
namespace ProcsDLL.InsiderTrading
{
    public partial class EmailConfig : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sRedirectUrl = ConfigurationManager.AppSettings["Office365ReturnUrl"];
            txtRedirectUri.Text = sRedirectUrl;
        }
    }
}