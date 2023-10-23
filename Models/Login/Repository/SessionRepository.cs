using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.Login.Modal;
using ProcsDLL.Models.Login.Service.Response;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProcsDLL.Models.Login.Repository
{
    public class SessionRepository
    {
        public SessionResponse SaveSession(Session objSession)
        {
            try
            {
                var ModuleDatabase = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                SqlParameter[] parameters = new SqlParameter[6];
                parameters[0] = new SqlParameter("@EMP_ID", objSession.EMP_ID);
                parameters[1] = new SqlParameter("@MAC_ID", objSession.MAC_ID);
                parameters[2] = new SqlParameter("@IP", objSession.IP);
                parameters[3] = new SqlParameter("@BROWSER", objSession.BROWSER);
               // parameters[4] = new SqlParameter("@ITDB", ITDB);
                parameters[4] = new SqlParameter("@MODE", "SAVE");

                var obj = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SPROC_PIT_INSIDER_SESSION", Convert.ToString(CryptorEngine.Decrypt(ConfigurationManager.AppSettings["ITDB"], true)), parameters);

                //string _sQL = "SELECT COUNT(*) FROM MIS_BRAND_MSTR(NOLOCK) WHERE SAP_CODE='"+objBrand.SapCode+"' AND BRAND_ID!="+objBrand.BrandId;
                //var obj = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(),CommandType.Text,_sQL);
                if ((String)obj == "Sucess")
                {
                    SessionResponse oBrnd = new SessionResponse();
                    oBrnd.StatusFl = true;
                    oBrnd.Msg = "Data has been Set successfully !";
                    SessionDTO o = new SessionDTO();
                    o.EMP_ID = objSession.EMP_ID;
                    o.MAC_ID = objSession.MAC_ID;
                    o.IP = objSession.IP;
                    o.BROWSER = objSession.BROWSER;
                    oBrnd.Session = o;
                    return oBrnd;
                }
                else
                {
                    SessionResponse oBrnd = new SessionResponse();
                    oBrnd.StatusFl = false;
                    oBrnd.Msg = (String)obj;
                    return oBrnd;
                }
            }
            catch (Exception ex)
            {
                SessionResponse oBrnd = new SessionResponse();
                oBrnd.StatusFl = false;
                oBrnd.Msg = "Processing failed, because of system error !";
                return oBrnd;
            }
        }
        public SessionResponse DeleteSession(Session objSession)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[5];
                parameters[0] = new SqlParameter("@EMP_ID", objSession.EMP_ID);
                parameters[1] = new SqlParameter("@MAC_ID", objSession.MAC_ID);
                parameters[2] = new SqlParameter("@IP", objSession.IP);
                parameters[3] = new SqlParameter("@BROWSER", objSession.BROWSER);
                parameters[4] = new SqlParameter("@MODE", "DELETE");

                var obj = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SPROC_PIT_INSIDER_SESSION", Convert.ToString(CryptorEngine.Decrypt(ConfigurationManager.AppSettings["ITDB"], true)), parameters);

                if ((String)obj == "Sucess")
                {
                    SessionResponse oBrnd = new SessionResponse();
                    oBrnd.StatusFl = true;
                    oBrnd.Msg = "Session has been Removed successfully !";
                    SessionDTO o = new SessionDTO();
                    o.EMP_ID = objSession.EMP_ID;
                    o.MAC_ID = objSession.MAC_ID;
                    o.IP = objSession.IP;
                    o.BROWSER = objSession.BROWSER;
                    oBrnd.Session = o;
                    return oBrnd;
                }
                else
                {
                    SessionResponse oBrnd = new SessionResponse();
                    oBrnd.StatusFl = false;
                    oBrnd.Msg = (String)obj;
                    return oBrnd;
                }
            }
            catch (Exception ex)
            {
                SessionResponse oBrnd = new SessionResponse();
                oBrnd.StatusFl = false;
                oBrnd.Msg = "Processing failed, because of system error !";
                return oBrnd;
            }
        }
    }
}