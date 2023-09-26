using ProcsDLL.Models.Infrastructure;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ProcsDLL.InsiderTrading
{
    public partial class Declaration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Convert.ToString(Session["EmployeeId"])))
            {
                //GetFinalDeclarationDisplayName();
            }
        }

        private void GetFinalDeclarationDisplayName()
        {
            string ConnStr = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionString"]), true);
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                conn.ChangeDatabase(Convert.ToString(Session["ModuleDatabase"]));
                string sql = "SELECT A.DISPLAY_NAME FROM FORM_B_TEMPLATE_HDR(NOLOCK) A WHERE " +
                             "A.COMPANY_ID = " + Convert.ToInt32(Session["CompanyId"]);
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    DataTable dtAccess = new DataTable();
                    SqlDataAdapter daAccess = new SqlDataAdapter(cmd);
                    daAccess.Fill(dtAccess);

                    if (dtAccess.Rows.Count > 0)
                    {
                        formB.Value = !String.IsNullOrEmpty(Convert.ToString(dtAccess.Rows[0]["DISPLAY_NAME"])) ? Convert.ToString(dtAccess.Rows[0]["DISPLAY_NAME"]) : String.Empty;
                    }
                }

                sql = "SELECT A.DISPLAY_NAME FROM IT_ANNUAL_DISCLOSURE_BY_DESIGNATED_EMPLOYEES_TEMPLATE_HDR(NOLOCK) A WHERE " +
                             "A.COMPANY_ID = " + Convert.ToInt32(Session["CompanyId"]);
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    DataTable dtAccess = new DataTable();
                    SqlDataAdapter daAccess = new SqlDataAdapter(cmd);
                    daAccess.Fill(dtAccess);

                    if (dtAccess.Rows.Count > 0)
                    {
                        formK.Value = !String.IsNullOrEmpty(Convert.ToString(dtAccess.Rows[0]["DISPLAY_NAME"])) ? Convert.ToString(dtAccess.Rows[0]["DISPLAY_NAME"]) : String.Empty;
                    }
                }

                sql = "SELECT A.DISPLAY_NAME FROM IT_SUBMISSION_OF_DECLARATION_EMAIL_TEMPLATE_HDR(NOLOCK) A WHERE " +
                            "A.COMPANY_ID = " + Convert.ToInt32(Session["CompanyId"]) + " AND A.TYPE='Header' AND A.FORM_TYPE='FORM_E'";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    DataTable dtAccess = new DataTable();
                    SqlDataAdapter daAccess = new SqlDataAdapter(cmd);
                    daAccess.Fill(dtAccess);

                    if (dtAccess.Rows.Count > 0)
                    {
                        formE.Value = !String.IsNullOrEmpty(Convert.ToString(dtAccess.Rows[0]["DISPLAY_NAME"])) ? Convert.ToString(dtAccess.Rows[0]["DISPLAY_NAME"]) : String.Empty;
                    }
                }

                sql = "SELECT A.DISPLAY_NAME FROM IT_SUBMISSION_OF_DECLARATION_EMAIL_TEMPLATE_HDR(NOLOCK) A WHERE " +
                            "A.COMPANY_ID = " + Convert.ToInt32(Session["CompanyId"]) + " AND A.TYPE='Header' AND A.FORM_TYPE='FORM_F'";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    DataTable dtAccess = new DataTable();
                    SqlDataAdapter daAccess = new SqlDataAdapter(cmd);
                    daAccess.Fill(dtAccess);

                    if (dtAccess.Rows.Count > 0)
                    {
                        formF.Value = !String.IsNullOrEmpty(Convert.ToString(dtAccess.Rows[0]["DISPLAY_NAME"])) ? Convert.ToString(dtAccess.Rows[0]["DISPLAY_NAME"]) : String.Empty;
                    }
                }

            }
        }
    }
}