using System;

namespace ProcsDLL.InsiderTrading
{
    public partial class TrainingModule : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Convert.ToString(Request.Form["TrainingModuleId"])))
            {
                txtTrainingModuleId.Value = Convert.ToString(Request.Form["TrainingModuleId"]);
            }

            if (!String.IsNullOrEmpty(Convert.ToString(Request.Form["TotalNoOfPages"])))
            {
                txtTotalNoOfPages.Value = Convert.ToString(Request.Form["TotalNoOfPages"]);
            }

            if (!String.IsNullOrEmpty(Convert.ToString(Request.Form["UserTrainingModuleStatus"])))
            {
                txtUserTrainingModuleStatus.Value = Convert.ToString(Request.Form["UserTrainingModuleStatus"]);
            }
        }
    }
}