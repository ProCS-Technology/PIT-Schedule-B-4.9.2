using ProcsDLL.Models.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProcsDLL.InsiderTrading
{
    public partial class DashboardUpsi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               
                //fnGetUpsiCount();
                 
            }
        }

        
        private void fnGetUpsiCount()
         {
             
        //        string loginUserId = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
        //        Int32 iCmpnId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
        //        string sConStr = Convert.ToString(CryptorEngine.Decrypt(ConfigurationManager.AppSettings["ConnectionStringIT"], true));
        //        using (SqlConnection sCon = new SqlConnection(sConStr))
        //        {
        //            sCon.Open();
        //            SqlCommand sCmd = new SqlCommand();
        //            sCmd.CommandText = "SP_PROCS_INSIDER_UPSI_USER_TYPE";
        //            sCmd.CommandType = CommandType.StoredProcedure;
        //            sCmd.Connection = sCon;

        //            sCmd.Parameters.Clear();
        //            sCmd.Parameters.Add(new SqlParameter("@LOGIN_ID", loginUserId));
                     

        //            DataSet dsNum = new DataSet();
        //            SqlDataAdapter daNum = new SqlDataAdapter(sCmd);
        //            daNum.Fill(dsNum);

        //        SqlCommand sCmdCount = new SqlCommand();
        //        sCmdCount.CommandText = "SP_PROCS_INSIDER_UPSI_USER_TYPE_COUNT";
        //        sCmdCount.CommandType = CommandType.StoredProcedure;
        //        sCmdCount.Connection = sCon;

        //        sCmdCount.Parameters.Clear();
        //        sCmdCount.Parameters.Add(new SqlParameter("@LOGIN_ID", loginUserId));
        //        sCmdCount.Parameters.Add(new SqlParameter("@Role_Name", dsNum.Tables[0].Rows[0]["Role_Name"].ToString()));
        //        sCmdCount.Parameters.Add(new SqlParameter("@Is_Approver", dsNum.Tables[0].Rows[0]["Is_Approver"].ToString()));


        //        DataSet dsNumCount = new DataSet();
        //        SqlDataAdapter daNumCount = new SqlDataAdapter(sCmdCount);
        //        daNumCount.Fill(dsNumCount);
                
        //        ActiveUpsi.InnerHtml = dsNumCount.Tables[0].Rows[0]["TotalActiveUPSIEvent"].ToString();
        //        InactiveUpsi.InnerHtml = dsNumCount.Tables[0].Rows[0]["TotalInactiveUPSIEvent"].ToString();
        //        AbandonedUpsi.InnerHtml = dsNumCount.Tables[0].Rows[0]["TotalAbandonedUPSIEvent"].ToString();
        //        PublishedUpsi.InnerHtml = dsNumCount.Tables[0].Rows[0]["TotalPublishedUPSIEvent"].ToString();
        //        AllUpsi.InnerHtml = dsNumCount.Tables[0].Rows[0]["TotalUPSIEvent"].ToString();
        //        sCon.Close();
        //    }
             
        }
    }
}