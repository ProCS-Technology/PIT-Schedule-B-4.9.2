using System.Data;
using System.Data.SqlClient;
namespace ProcsDLL.Models.Infrastructure
{
    public class LogSession
    {
        public static string LoggedSession(
            string SessionId, string UserLogin, string ConStr, string sIP, 
            string sMacAdddr, string sUsrAgent, string CmpnId, string sITDb
        )
        {
            SqlParameter[] parameters = new SqlParameter[7];
            parameters[0] = new SqlParameter("@COMPANY_ID", CmpnId);
            parameters[1] = new SqlParameter("@Mode", "LOGGED_SESSION");
            parameters[2] = new SqlParameter("@UserLogin", UserLogin);
            parameters[3] = new SqlParameter("@SessionId", SessionId);
            parameters[4] = new SqlParameter("@IPAddress", sIP);
            parameters[5] = new SqlParameter("@MACAddress", sMacAdddr);
            parameters[6] = new SqlParameter("@UsrAgent", sUsrAgent);
            return (string)SQLHelper.ExecuteScalar(ConStr, CommandType.StoredProcedure, "SP_PROCS_INSIDER_LOGGED_SESSION", sITDb, parameters);
        }
    }
}