using ClosedXML.Excel;
using ProcsDLL.Models.Infrastructure;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web;

namespace ProcsDLL.InsiderTrading
{
    public partial class MISReport : System.Web.UI.Page
    {
        DataSet ds = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void LinkButtonMISReport_Click(object sender, EventArgs e)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[8];

                parameters[0] = new SqlParameter("@MODE", "GET_COMPANY_MIS_REPORT");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", Convert.ToInt32(HttpContext.Current.Session["CompanyId"]));
                parameters[3] = new SqlParameter("@LOGIN_ID", Convert.ToString(HttpContext.Current.Session["EmployeeId"]));

                parameters[4] = new SqlParameter("@FILE_NAME", "");
                parameters[5] = new SqlParameter("@ADMIN_DB", Convert.ToString(HttpContext.Current.Session["AdminDB"]));

                parameters[6] = new SqlParameter("@DATE_FROM", ConvertDate(txtFromDate.Value));
                parameters[7] = new SqlParameter("@DATE_TO", ConvertDate(txtToDate.Value));

                ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_MASTER", Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]), parameters);


                string sXlsPath = HttpContext.Current.Server.MapPath("~/InsiderTrading/emailAttachment/");
                String xFile = "MIS_Report_" + DateTime.UtcNow.ToString("yyyyMMddHHmmssfff", CultureInfo.InvariantCulture);
                string xFileName = sXlsPath + xFile + ".xlsx";


                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            wb.Worksheets.Add(ds.Tables[0], "Personal Details");
                            wb.Worksheets.Add(ds.Tables[1], "Relative Details");
                            wb.Worksheets.Add(ds.Tables[2], "Demat Details");
                            wb.Worksheets.Add(ds.Tables[3], "Holding Details");
                            wb.Worksheets.Add(ds.Tables[4], "Declartion");
                            wb.SaveAs(xFileName);
                        }
                    }
                }

                Response.Clear();
                Response.AddHeader("content-disposition", "attachment; filename=" + xFile + ".xlsx");
                Response.WriteFile(xFileName);
                Response.ContentType = "";
                Response.End();
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, "MISReport.aspx.cs", "GetMISReport", Convert.ToString(HttpContext.Current.Session["EmployeeId"]), 5, Convert.ToInt32(HttpContext.Current.Session["CompanyId"]));
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
    }
}