using ProcsDLL.Models.InsiderTrading.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Net;
using System.Web;
namespace ProcsDLL.Models.Infrastructure
{
    public class CommonFunctions
    {
        private static String connectionString = SQLHelper.GetConnString();
        private static String connectionStringIT = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionStringIT"]), true);
        private static String dataBaseName = SQLHelper.GetDBName();
        public static InsiderTrading.Model.User GetITApprover(String dbName, Int32 companyId, String createdBy, String AdminDb, Int32 businessUnitId)
        {
            InsiderTrading.Model.User objUser = new InsiderTrading.Model.User();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionStringIT))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_PROCS_INSIDER_FORMS", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new SqlParameter("@Mode", "GET_APPROVER_MULTI_BUSINESS_UNIT"));
                        cmd.Parameters.Add(new SqlParameter("@COMPANY_ID", companyId));
                        cmd.Parameters.Add(new SqlParameter("@USER_LOGIN", createdBy));
                        cmd.Parameters.Add(new SqlParameter("@ADMIN_DATABASE", AdminDb));
                        cmd.Parameters.Add(new SqlParameter("@BUSINESS_UNIT_ID", businessUnitId));
                        SqlDataReader rdr = cmd.ExecuteReader();
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                objUser.approverName = (!String.IsNullOrEmpty(Convert.ToString(rdr["APPROVER_NAME"]))) ? Convert.ToString(rdr["APPROVER_NAME"]) : String.Empty;
                                objUser.approverEmail = (!String.IsNullOrEmpty(Convert.ToString(rdr["APPROVER_EMAIL"]))) ? Convert.ToString(rdr["APPROVER_EMAIL"]) : String.Empty;
                                objUser.USER_NM = (!String.IsNullOrEmpty(Convert.ToString(rdr["USER_NM"]))) ? Convert.ToString(rdr["USER_NM"]) : String.Empty;
                                objUser.USER_EMAIL = (!String.IsNullOrEmpty(Convert.ToString(rdr["USER_EMAIL"]))) ? Convert.ToString(rdr["USER_EMAIL"]) : String.Empty;
                                objUser.companyName = (!String.IsNullOrEmpty(Convert.ToString(rdr["COMPANY_NAME"]))) ? Convert.ToString(rdr["COMPANY_NAME"]) : String.Empty;
                                objUser.category = new Category();
                                objUser.category.categoryName = (!String.IsNullOrEmpty(Convert.ToString(rdr["CATEGORY_NM"]))) ? Convert.ToString(rdr["CATEGORY_NM"]) : String.Empty;
                            }
                        }
                        rdr.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message.ToString(), ex.Source, ex.StackTrace, "EmailHelper", "GetITApprover", createdBy, 1, companyId);
            }
            return objUser;
        }
    }
}