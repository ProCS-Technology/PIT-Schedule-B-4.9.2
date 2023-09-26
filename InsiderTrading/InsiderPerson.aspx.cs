using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Response;
namespace ProcsDLL.InsiderTrading
{
    public partial class InsiderPerson : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            fnLoadInsider();
        }
        private void fnLoadInsider()
        {
            string sConStr = SQLHelper.GetConnString();
            try
            {
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

                    string _sql = "DELETE FROM PROCS_INSIDER_CONNECTED_PERSONS WHERE CP_EMAIL IN("+
                        "SELECT USER_EMAIL FROM "+ sAdminDb+"..PROCS_USERS);"+
                        "SELECT FIRM,CP_NAME,CP_EMAIL,CP_IDENTIFICATION_TYPE,CP_IDENTIFICATION_NO,CP_STATUS FROM PROCS_INSIDER_CONNECTED_PERSONS "+
                        "ORDER BY CP_NAME";
                    Cmnd.CommandText = _sql;
                    Cmnd.CommandType = CommandType.Text;

                    DataSet dsCP = new DataSet();
                    SqlDataAdapter daCP = new SqlDataAdapter(Cmnd);
                    daCP.Fill(dsCP);
                    DataTable dtCP = new DataTable();
                    dtCP = dsCP.Tables[0];

                    if (dtCP.Rows.Count > 0)
                    {
                        string str = "";
                        foreach (DataRow drCP in dtCP.Rows)
                        {
                            str += "<tr>";
                            str += "<td>" + Convert.ToString(drCP["FIRM"]) + "</td>";
                            str += "<td>" + Convert.ToString(drCP["CP_NAME"]) + "</td>";
                            str += "<td>" + Convert.ToString(drCP["CP_EMAIL"]) + "</td>";
                            str += "<td>" + Convert.ToString(drCP["CP_IDENTIFICATION_TYPE"]) + "</td>";
                            str += "<td>" + Convert.ToString(drCP["CP_IDENTIFICATION_NO"]) + "</td>";
                            str += "<td>" + Convert.ToString(drCP["CP_STATUS"]) + "</td>";
                            str += "<td><button id=\"btnEditCP\" onclick=\"fnEdit('" + Convert.ToString(drCP["FIRM"]) + "','" + Convert.ToString(drCP["CP_NAME"]) + "','" + Convert.ToString(drCP["CP_EMAIL"]) + "','" + Convert.ToString(drCP["CP_IDENTIFICATION_TYPE"]) + "','" + Convert.ToString(drCP["CP_IDENTIFICATION_NO"]) + "','" + Convert.ToString(drCP["CP_STATUS"]) + "');\" class=\"btn btn-outline dark\">Edit</button></td>";
                            str += "</tr>";
                        }
                        tbdInsiderList.InnerHtml = str;
                    }                    
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}