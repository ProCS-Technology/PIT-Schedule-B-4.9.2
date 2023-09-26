using ProcsDLL.Models.Infrastructure;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
namespace ProcsDLL.InsiderTrading
{
    public partial class UPSIRptLimitedAccess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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

                string _sql = "SELECT B.USER_NM,B.USER_EMAIL FROM PROCS_INSIDER_USER(NOLOCK) A " +
                            "INNER JOIN " + sAdminDb + "..PROCS_USERS(NOLOCK) B ON A.USER_LOGIN=B.LOGIN_ID " +
                            "INNER JOIN " + sAdminDb + "..PROCS_USERS_BU_ACESS(NOLOCK) C ON B.LOGIN_ID=C.LOGIN_ID " +
                            "INNER JOIN " + sAdminDb + "..PROCS_BUSINESS_COMPANY_MODULE(NOLOCK) D ON C.COMPANY_ID=D.COMPANY_ID AND C.MODULE_ID=D.MODULE_ID " +
                            "INNER JOIN " + sAdminDb + "..PROCS_MODULE(NOLOCK) E ON D.MODULE_ID=E.MODULE_ID " +
                            "WHERE E.MODULE_NM='Insider Trading' AND C.COMPANY_ID=" + sCompanyId + " AND A.STATUS='Active' ORDER BY B.USER_NM";
                
                Cmnd.CommandText = _sql;
                Cmnd.CommandType = CommandType.Text;

                DataSet dsUser = new DataSet();
                SqlDataAdapter daUser = new SqlDataAdapter(Cmnd);
                daUser.Fill(dsUser);
                DataTable dtUser = new DataTable();
                dtUser = dsUser.Tables[0];

                if (dtUser.Rows.Count > 1)
                {
                    ddlUsers.Items.Add(new ListItem("", ""));
                    ddlUsers.Items.Add(new ListItem("All", "All"));
                }
                foreach (DataRow drUser in dtUser.Rows)
                {
                    ddlUsers.Items.Add(new ListItem(Convert.ToString(drUser["USER_NM"]), Convert.ToString(drUser["USER_EMAIL"])));
                }

                _sql = "SELECT A.GRP_ID,GRP_NAME FROM PROCS_INSIDER_UPSI_GROUP(NOLOCK) A ORDER BY GRP_NAME";

                Cmnd.CommandText = _sql;
                Cmnd.CommandType = CommandType.Text;

                DataSet dsGrp = new DataSet();
                SqlDataAdapter daGrp = new SqlDataAdapter(Cmnd);
                daGrp.Fill(dsGrp);
                DataTable dtGrp = new DataTable();
                dtGrp = dsGrp.Tables[0];

                if (dtGrp.Rows.Count > 1)
                {
                    ddlUPSIGrp.Items.Add(new ListItem("", ""));
                    ddlUPSIGrp.Items.Add(new ListItem("All", "0"));
                }
                foreach (DataRow drGrp in dtGrp.Rows)
                {
                    ddlUPSIGrp.Items.Add(new ListItem(Convert.ToString(drGrp["GRP_NAME"]), Convert.ToString(drGrp["GRP_ID"])));
                }
            }
        }
    }
}