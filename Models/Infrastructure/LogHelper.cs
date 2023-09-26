using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace ProcsDLL.Models.Infrastructure
{
    public class LogHelper
    {
        private string connectionString = SQLHelper.GetConnString();
        public string moduleDataBase;
        public void AddExceptionLogs(string errorMessage, string errorSource, string errorStackTrace, string pageName, string methodName, string createdBy, Int32 moduleId)
        {
            Int32 companyId = 0;
        
            if (!String.IsNullOrEmpty(Convert.ToString(HttpContext.Current.Session["CompanyId"])))
            {
                companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
            }
            if (!String.IsNullOrEmpty(Convert.ToString(HttpContext.Current.Session["ModuleDatabase"])))
            {
                moduleDataBase = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
            }
            AddExceptionLogs(errorMessage, errorSource, errorStackTrace, pageName, methodName, createdBy, moduleId, companyId);
        }
        public void AddExceptionLogs(string errorMessage, string errorSource, string errorStackTrace, string pageName, string methodName, string createdBy, Int32 moduleId, Int32 companyId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                 using (SqlCommand cmd = new SqlCommand("SP_PROCS_LOG_EXCEPTION", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new SqlParameter("@ERROR_MESSAGE", errorMessage));
                    cmd.Parameters.Add(new SqlParameter("@ERROR_SOURCE", errorSource));
                    cmd.Parameters.Add(new SqlParameter("@ERROR_STACK_TRACE", errorStackTrace));
                    cmd.Parameters.Add(new SqlParameter("@PAGE_NAME", pageName));
                    cmd.Parameters.Add(new SqlParameter("@METHOD_NAME", methodName));
                    cmd.Parameters.Add(new SqlParameter("@CREATED_BY", createdBy));
                    cmd.Parameters.Add(new SqlParameter("@MODULE_ID", moduleId));
                    cmd.Parameters.Add(new SqlParameter("@COMPANY_ID", companyId));
                    cmd.Parameters.Add(new SqlParameter("@MODULE_DATABASE", moduleDataBase));
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}