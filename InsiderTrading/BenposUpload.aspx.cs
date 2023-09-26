using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ProcsDLL.Models.Infrastructure;
using System.Web.UI.WebControls;
namespace ProcsDLL.InsiderTrading
{
    public partial class BenposUpload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String sConStr = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionStringIT"]), true);
            using (SqlConnection sCon = new SqlConnection(sConStr))
            {
                SqlCommand sCmd = new SqlCommand();
                sCon.Open();
                sCmd.Connection = sCon;

                string _sql = "DECLARE @ColumnNameList VARCHAR(MAX);" +
                    "SELECT @ColumnNameList=COALESCE(@ColumnNameList+' + ','')+DEPOSITORY_TYPE " +
                    "FROM PROCS_INSIDER_SHARES_THRESHOLD_BY_TIME_SETTINGS(NOLOCK) " +
                    "WHERE COMPANY_ID=" + Convert.ToString(Session["CompanyId"]) + " AND ISNULL(SHARES_COUNT,0)>0;" +
                    "SELECT @ColumnNameList";
                sCmd.CommandText = _sql;
                sCmd.CommandType = CommandType.Text;
                string sVal = Convert.ToString(sCmd.ExecuteScalar());
                if (sVal.ToString() == "CDSL" || sVal.ToString() == "NSDL")
                {
                    ddlDepository.Items.Add(new ListItem(sVal, sVal));
                    //Combined (CDSL + NSDL)
                }
                else
                {
                    ddlDepository.Items.Add(new ListItem("Combined (" + sVal + " + Physical Share)", "Combined (" + sVal + ")"));
                    //Combined (CDSL + NSDL)">Combined (CDSL + NSDL + Physical Share)
                }
            }
        }
    }
}