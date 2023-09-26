using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ProcsDLL.Models.Infrastructure
{
    public class ExceptionHandler
    {
        static void LogException(string LoggedUser, string ModuleNm, string Screen, string ExceptionMsg)
        {
            using (SqlConnection sCon = new SqlConnection(ConfigurationManager.AppSettings["connectionstring"].ToString()))
            {
                sCon.Open();
                SqlCommand sCmd = new SqlCommand();
                sCmd.Connection = sCon;
                sCmd.CommandType = CommandType.Text;
                sCmd.CommandText = "INSERT INTO PROCS_EXCEPTION(LOGGED_USER,MODULE_NAME,SCREEN_NAME,EXCEPTION,CREATED_ON) " +
                    "VALUES('" + LoggedUser + "','" + ModuleNm + "','" + Screen + "','" + ExceptionMsg + "',GETDATE())";
                sCmd.ExecuteNonQuery();
            }
        }
    }
}