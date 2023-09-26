using ProcsDLL.Models.Infrastructure;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace ProcsDLL.InsiderTrading
{
    public partial class UPSIGroup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AuthToken"] != null && Request.Cookies["AuthToken"] != null)
            {
                if (!Session["AuthToken"].ToString().Equals(Request.Cookies["AuthToken"].Value))
                {
                    Response.Redirect("../LogOut.aspx");
                }
            }
            else
            {
                Response.Redirect("../LogOut.aspx");
            }
            txtLoggedUser.Value = Convert.ToString(Session["EmployeeId"]);

            string sUPSIFl = Convert.ToString(ConfigurationManager.AppSettings["UPSIAdminCO"]);
            string sConStr = SQLHelper.GetConnString();
            using (SqlConnection Conn = new SqlConnection(sConStr))
            {
                Conn.Open();
                SqlCommand Cmnd = new SqlCommand();
                Cmnd.Connection = Conn;

                string sModuleDb = Convert.ToString(Session["ModuleDatabase"]);
                string sAdminDb = Convert.ToString(Session["AdminDb"]);
                string sCompanyId = Convert.ToString(Session["CompanyId"]);
                string sEmployeeId = Convert.ToString(Session["EmployeeId"]);
                Conn.ChangeDatabase(sModuleDb);

                string _sql = "SELECT TOP 1 B.ROLE_NAME,ISNULL(A.IS_APPROVER,'') AS IS_APPROVER FROM PROCS_INSIDER_USER(NOLOCK) A " +
                    "INNER JOIN PROCS_INSIDER_ROLE_MSTR(NOLOCK) B ON A.USER_ROLE=B.ROLE_ID " +
                    "WHERE A.USER_LOGIN='" + sEmployeeId + "' AND B.COMPANY_ID=" + sCompanyId;
                Cmnd.CommandText = _sql;
                Cmnd.CommandType = CommandType.Text;

                DataSet dsRole = new DataSet();
                SqlDataAdapter daRole = new SqlDataAdapter(Cmnd);
                daRole.Fill(dsRole);
                DataTable dtRole = new DataTable();
                dtRole = dsRole.Tables[0];

                if (sUPSIFl == "true")
                {
                    if (dtRole.Rows.Count > 0)
                    {
                        if (Convert.ToString(dtRole.Rows[0]["IS_APPROVER"]) == "Yes" || Convert.ToString(dtRole.Rows[0]["ROLE_NAME"]) == "Admin")
                        {
                            btnAddupsi.Visible = true;
                        }
                    }
                }
                else
                {
                    btnAddupsi.Visible = true;
                }
            }
        }
    }
}