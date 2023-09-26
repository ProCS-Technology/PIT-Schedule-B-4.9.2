using ProcsDLL.Models.Infrastructure;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
namespace ProcsDLL.InsiderTrading
{
    public partial class Pre_Clearance_Request : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string s = ConfigurationManager.AppSettings["UndertakingBeforeSubmitRequest"];
            if (!String.IsNullOrEmpty(s))
            {
                string undertakingBeforeSubmitRequest = Convert.ToString(CryptorEngine.Decrypt(ConfigurationManager.AppSettings["UndertakingBeforeSubmitRequest"], true));
                if (Convert.ToBoolean(undertakingBeforeSubmitRequest))
                {
                    enableUndertakingBeforeRequest.Value = "true";
                }
                else
                {
                    enableUndertakingBeforeRequest.Value = "false";
                }
            }
            else
            {
                enableUndertakingBeforeRequest.Value = "false";
            }

            if (!Page.IsPostBack)
            {
                fnGetLastBenposDate();
                fnChkWindowClosurenUPSI();
            }
        }
        private void fnGetLastBenposDate()
        {
            try
            {
                string sConStr = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["ConnectionStringIT"], true);
                string sAdminDb = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["AdminDB"], true);
                Int32 iCmpnId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                using (SqlConnection sCon = new SqlConnection(sConStr))
                {
                    sCon.Open();
                    SqlCommand sCmd = new SqlCommand();
                    sCmd.CommandText = "SP_PROCS_INSIDER_PRE_CLEARANCE_REQUEST";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Connection = sCon;

                    sCmd.Parameters.Clear();
                    sCmd.Parameters.Add(new SqlParameter("@MODE", "GET_LAST_BENPOS_DATE"));
                    sCmd.Parameters.Add(new SqlParameter("@COMPANY_ID", iCmpnId));

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(sCmd);
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dt.Rows[0]["CNT"]) > 0)
                        {
                            HiddenLastBenposDate.Value = FormatHelper.ConvertDate(dt.Rows[0]["BP_DATE"].ToString());
                        }
                    }

                }
            }
            catch (Exception ex)
            {

            }
        }
        private void fnChkWindowClosurenUPSI()
        {
            try
            {
                string sConStr = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["ConnectionStringIT"], true);
                string sAdminDb = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["AdminDB"], true);
                Int32 iCmpnId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                string sLoginId = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                using (SqlConnection sCon = new SqlConnection(sConStr))
                {
                    sCon.Open();
                    SqlCommand sCmd = new SqlCommand();
                    sCmd.CommandText = "SELECT COUNT(*) FROM PROCS_INSIDER_TRADING_WINDOW " +
                        "WHERE CONVERT(DATE,GETDATE()) BETWEEN FROM_DATE AND CONVERT(DATE,ISNULL(TO_DATE,'9999-12-31'))";
                    sCmd.CommandType = CommandType.Text;
                    sCmd.Connection = sCon;
                    Int32 WindowClosureCnt = Convert.ToInt32(sCmd.ExecuteScalar());
                    //hdnWCCnt.Value = Convert.ToString(WindowClosureCnt);
                    hdnWCCnt.Value = Convert.ToString("0");

                    sCmd.CommandText = "SELECT COUNT(*) FROM PROCS_INSIDER_UPSI_GROUP(NOLOCK) A " +
                        "INNER JOIN PROCS_INSIDER_UPSI_GROUP_DP(NOLOCK) B ON A.GRP_ID=B.GRP_ID " +
                        "WHERE B.USER_LOGIN='" + sLoginId + "' AND CONVERT(DATE,GETDATE()) BETWEEN CONVERT(DATE,A.VALID_FROM) " +
                        "AND CONVERT(DATE,ISNULL(A.VALID_TO,'9999-12-31'))";
                    sCmd.CommandType = CommandType.Text;
                    sCmd.Connection = sCon;
                    Int32 UPSICnt = Convert.ToInt32(sCmd.ExecuteScalar());
                    //hdnUPSICnt.Value = Convert.ToString(UPSICnt);
                    hdnUPSICnt.Value = Convert.ToString("0");
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}