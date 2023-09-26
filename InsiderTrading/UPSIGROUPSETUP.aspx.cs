using System;
using System.Web;

namespace ProcsDLL.InsiderTrading
{
    public partial class UPSIGROUPSETUP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session.Count > 0)
            {
                txtupsiCREATEDBY.Value = HttpContext.Current.Session["EmployeeId"].ToString();
            }




        }


    }
}