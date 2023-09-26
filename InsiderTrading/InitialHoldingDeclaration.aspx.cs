using System;

namespace ProcsDLL.InsiderTrading
{
    public partial class InitialHoldingDeclaration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Convert.ToString(Session["EmployeeId"])) && !String.IsNullOrEmpty(Convert.ToString(Session["CompanyName"])))
            {
                txtLoginId.Value = Convert.ToString(Session["EmployeeId"]);
                txtCompanyName.Value = Convert.ToString(Session["CompanyName"]);
            }
        }
    }
}