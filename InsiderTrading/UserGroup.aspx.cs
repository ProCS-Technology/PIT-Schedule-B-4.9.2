using ProcsDLL.Models.InsiderTrading.Service.Request;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Web;
using System.Web.UI.WebControls;

namespace ProcsDLL.InsiderTrading
{
    public partial class UserGroup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                ProcsDLL.Models.InsiderTrading.Model.User user = new ProcsDLL.Models.InsiderTrading.Model.User();
                user.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                user.moduleId = Convert.ToInt32(HttpContext.Current.Session["ModuleId"]);
                user.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                user.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDb"]);
                user.businessUnit = new ProcsDLL.Models.InsiderTrading.Model.BusinessUnit
                {
                    businessUnitId = Convert.ToInt32(HttpContext.Current.Session["BusinessUnitId"]),
                    businessUnitName = Convert.ToString(HttpContext.Current.Session["BusinessUnitName"])
                };
                if (!user.ValidateInput())
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Invalid Input Format";

                }
                UserRequest gReqUserList = new UserRequest(user);
                UserResponse gResUserList = gReqUserList.GetUserList();
                if (gResUserList.StatusFl)
                {
                    if (gResUserList.UserList.Count > 0)
                    {

                        foreach (ProcsDLL.Models.InsiderTrading.Model.User users in gResUserList.UserList)
                        {
                            dduserslist.Items.Add(new ListItem(users.LOGIN_ID + " (" + users.USER_NM + ")", users.LOGIN_ID.ToString()));
                        }
                    }
                }

            }
        }

    }
}