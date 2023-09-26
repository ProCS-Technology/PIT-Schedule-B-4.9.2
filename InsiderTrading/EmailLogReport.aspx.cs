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
using System.IO;
namespace ProcsDLL.InsiderTrading
{
    public partial class EmailLogReport : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        string sConStr = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["ConnectionStringIT"], true);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                GetModuleType();
                GetModuleSubType();
            }
        }
        private void GetModuleType()
        {
            try
            {
                using (SqlConnection sCon = new SqlConnection(sConStr))
                {
                    sCon.Open();
                    SqlCommand sCmd = new SqlCommand();
                    sCmd.CommandText = "SP_PROCS_INSIDER_EMAIL_LOG_REPORT";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Connection = sCon;
                    sCmd.Parameters.Clear();
                    sCmd.Parameters.Add(new SqlParameter("@MODE", "GET_MODULE_NAME"));

                    SqlDataAdapter da = new SqlDataAdapter(sCmd);
                    dt.Clear();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows.Count > 1)
                        {
                            DropDownListModule.Items.Add(new ListItem("All", "0"));
                        }
                        foreach (DataRow dr in dt.Rows)
                        {
                            string sModuleName = !String.IsNullOrEmpty(Convert.ToString(dr["EMAIL_ACTION"])) ? Convert.ToString(dr["EMAIL_ACTION"]) : String.Empty;
                            DropDownListModule.Items.Add(new ListItem(sModuleName, sModuleName));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            try
            {
                HiddenShowModal.Value = string.Empty;
                using (SqlConnection sCon = new SqlConnection(sConStr))
                {
                    sCon.Open();
                    SqlCommand sCmd = new SqlCommand();
                    sCmd.CommandText = "SP_PROCS_INSIDER_EMAIL_LOG_REPORT";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Connection = sCon;

                    sCmd.Parameters.Clear();
                    sCmd.Parameters.Add(new SqlParameter("@MODE", "GET_EMAIL_LOGS"));
                    sCmd.Parameters.Add(new SqlParameter("@EMAIL_ACTION", DropDownListModule.SelectedValue));
                    sCmd.Parameters.Add(new SqlParameter("@DATA_ELEMENT", ddlModuleSubType.Value));

                    if (!String.IsNullOrEmpty(Convert.ToString(txtFromDate.Value)))
                    {
                        sCmd.Parameters.Add(new SqlParameter("@FROM_DATE", ConvertDate(txtFromDate.Value)));
                    }
                    else
                    {
                        sCmd.Parameters.Add(new SqlParameter("@FROM_DATE", DBNull.Value));
                    }
                    if (!String.IsNullOrEmpty(Convert.ToString(txtToDate.Value)))
                    {
                        sCmd.Parameters.Add(new SqlParameter("@TO_DATE", ConvertDate(txtToDate.Value)));
                    }
                    else
                    {
                        sCmd.Parameters.Add(new SqlParameter("@TO_DATE", DBNull.Value));
                    }
                    SqlDataAdapter da = new SqlDataAdapter(sCmd);
                    dt.Clear();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        RepeaterTbl.DataSource = dt;
                        RepeaterTbl.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        protected void DropDownListModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetModuleSubType();
        }
        protected void GetModuleSubType()
        {
            try
            {
                ddlModuleSubType.Items.Clear();
                using (SqlConnection sCon = new SqlConnection(sConStr))
                {
                    sCon.Open();
                    SqlCommand sCmd = new SqlCommand();
                    sCmd.CommandText = "SP_PROCS_INSIDER_EMAIL_LOG_REPORT";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Connection = sCon;

                    sCmd.Parameters.Clear();
                    sCmd.Parameters.Add(new SqlParameter("@MODE", "GET_MODULE_SUB_TYPE"));
                    sCmd.Parameters.Add(new SqlParameter("@EMAIL_ACTION", DropDownListModule.SelectedValue));

                    SqlDataAdapter da = new SqlDataAdapter(sCmd);
                    dt.Clear();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows.Count > 1)
                        {
                            ddlModuleSubType.Items.Add(new ListItem("All", "0"));
                        }
                        foreach (DataRow dr in dt.Rows)
                        {
                            string sModuleId = !String.IsNullOrEmpty(Convert.ToString(dr["DATA_ELEMENT_ID"])) ? Convert.ToString(dr["DATA_ELEMENT_ID"]) : String.Empty;
                            string sModuleName = !String.IsNullOrEmpty(Convert.ToString(dr["DATA_ELEMENT_NAME"])) ? Convert.ToString(dr["DATA_ELEMENT_NAME"]) : String.Empty;

                            ddlModuleSubType.Items.Add(new ListItem(sModuleName, sModuleId));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        private DateTime ConvertDate(String date)
        {
            String str = String.Empty;
            try
            {
                if (date.Contains("/"))
                {
                    str = date.Split('/')[2] + "-" + date.Split('/')[1] + "-" + date.Split('/')[0];
                }
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message.ToString(), ex.Source, ex.StackTrace, this.GetType().Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, Convert.ToString(HttpContext.Current.Session["EmployeeId"]), Convert.ToInt32(HttpContext.Current.Session["ModuleId"]));
            }
            return Convert.ToDateTime(str);
        }
        protected void LinkButtonLogDetail_Click(object sender, EventArgs e)
        {
            try
            {
                HiddenShowModal.Value = "YES";
                LinkButton btn = (LinkButton)sender;
                RepeaterItem item = (RepeaterItem)btn.NamingContainer;
                HiddenField LogId = (HiddenField)item.FindControl("HiddenFieldLogId");

                using (SqlConnection sCon = new SqlConnection(sConStr))
                {
                    sCon.Open();
                    SqlCommand sCmd = new SqlCommand();
                    sCmd.CommandText = "SP_PROCS_INSIDER_EMAIL_LOG_REPORT";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Connection = sCon;

                    sCmd.Parameters.Clear();
                    sCmd.Parameters.Add(new SqlParameter("@MODE", "GET_EMAIL_LOGS_BY_ID"));
                    sCmd.Parameters.Add(new SqlParameter("@LOG_ID", Convert.ToInt64(LogId.Value)));

                    SqlDataAdapter da = new SqlDataAdapter(sCmd);
                    dt.Clear();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        dvMsg.InnerHtml = dt.Rows[0]["EMAIL_MSG"].ToString();
                        foreach (DataRow drat in dt.Rows)
                        {
                            string[] separator = new string[] { "InsiderTrading" };
                            string filePth = Convert.ToString(drat["EMAIL_ATTACHMENT"]);
                            //string[] fileurl = filePth.Split(separator, StringSplitOptions.None);
                            //string newfileurl = Server.MapPath(filePth);//"../InsiderTrading/" + fileurl[1].Replace('\'', '/');

                            string relativePath = filePth.Replace(HttpContext.Current.Server.MapPath("~/"), "").Replace(@"\", "/").Replace("insidertrading/", "");
                            drat["EMAIL_ATTACHMENT"] = relativePath;
                        }
                        dt.AcceptChanges();
                        RepeaterAttachment.DataSource = dt;
                        RepeaterAttachment.DataBind();
                    }
                    else
                    {
                        dvMsg.InnerHtml = string.Empty;
                        RepeaterAttachment.DataSource = string.Empty;
                        RepeaterAttachment.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        public static string MapPathReverse(string fullServerPath)
        {
            return @"~\" + fullServerPath.Replace(HttpContext.Current.Request.PhysicalApplicationPath, String.Empty);
        }
    }
}