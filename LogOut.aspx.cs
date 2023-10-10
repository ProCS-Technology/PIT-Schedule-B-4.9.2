using ProcsDLL.Models.Login.Modal;
using ProcsDLL.Models.Login.Service.Request;
using ProcsDLL.Models.Login.Service.Response;
using System;
using System.Web;
namespace ProcsDLL
{
    public partial class LogOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionDTO sDTO = new SessionDTO();
            sDTO.EMP_ID = Convert.ToString(Session["EmployeeId"]);
            sDTO.MAC_ID = Convert.ToString(Session["MacId"]);
            sDTO.IP = Convert.ToString(Session["IP"]);
            sDTO.BROWSER = Convert.ToString(Session["Browser"]);
            SessionRequest sReq = new SessionRequest(sDTO);
            SessionResponse sRes1 = sReq.DeleteSession();

            Session.Abandon();
            Session.Clear();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Session.RemoveAll();
            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
                Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
            }
            if (Request.Cookies["AuthToken"] != null)
            {
                Response.Cookies["AuthToken"].Expires = DateTime.Now.AddDays(-1);
            }
            Response.Redirect("Login.aspx");
        }
    }
}