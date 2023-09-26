using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
namespace ProcsDLL.InsiderTrading
{
    public partial class LongTask : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnpartial_Click(object sender, EventArgs e)
        {
            lblpartial.Text = "Partial clicked";
        }
        protected void btntotal_Click(object sender, EventArgs e)
        {
            lblpartial.Text = "Total clicked";
        }
    }
}