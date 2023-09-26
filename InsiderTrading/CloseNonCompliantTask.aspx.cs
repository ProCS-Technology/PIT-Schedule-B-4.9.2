using ProcsDLL.Models.Infrastructure;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace ProcsDLL.InsiderTrading
{
    public partial class CloseNonCompliantTask : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session.Count > 0)
            {
                string userLogin = Convert.ToString(Session["EmployeeId"]);
                string moduleDatabase = Convert.ToString(Session["ModuleDatabase"]);
                Int32 companyId = Convert.ToInt32(Session["CompanyId"]);
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@USER_LOGIN", userLogin);
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", companyId);
                parameters[3] = new SqlParameter("@MODE", "GET_USER_ROLE");
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_NC", moduleDatabase, parameters);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        string userRole = !String.IsNullOrEmpty(Convert.ToString(dr["USER_ROLE"])) ? Convert.ToString(dr["USER_ROLE"]) : String.Empty;
                        if ((userRole.ToLower()).Equals("admin"))
                        {
                            break;
                        }
                        else
                        {
                            Response.Redirect("../LogOut.aspx");
                            break;
                        }
                    }
                }
                else
                {
                    Response.Redirect("../LogOut.aspx");
                }

            }
            else
            {
                Response.Redirect("../LogOut.aspx");
            }
        }
    }
}