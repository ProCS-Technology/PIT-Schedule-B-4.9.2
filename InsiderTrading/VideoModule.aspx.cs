using System;

namespace ProcsDLL.InsiderTrading
{
    public partial class VideoModule : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Convert.ToString(Request.Form["VideoId"])))
            {
                txtVideoId.Value = Convert.ToString(Request.Form["VideoId"]);
            }

            if (!String.IsNullOrEmpty(Convert.ToString(Request.Form["VideoTitle"])))
            {
                txtVideoTitle.Value = Convert.ToString(Request.Form["VideoTitle"]);
            }
        }
    }
}