using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.Login.Modal;
using ProcsDLL.Models.Login.Service.Request;
using ProcsDLL.Models.Login.Service.Response;
using Saml;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Web;

namespace ProcsDLL
{
    public partial class ValidateUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpBrowserCapabilities bc = Request.Browser;//nc
            if (Request.HttpMethod.ToUpper() == "POST")
            {
                string samlCer = ConfigurationManager.AppSettings["CertKey"];
                string sResponse = string.Empty;
                sResponse = Request.Form["SAMLResponse"];


                Saml.Response samlResponse = new Response(samlCer, Request.Form["SAMLResponse"]);
                var samlEndpoint = ConfigurationManager.AppSettings["LogoutUrl"];
                string username, email, firstname, lastname;
                username = samlResponse.GetNameID();
                email = samlResponse.GetEmail();
                firstname = samlResponse.GetFirstName();
                lastname = samlResponse.GetLastName();
                email = username;
                string ConnStr = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionString"]), true);
                using (SqlConnection conn = new SqlConnection(ConnStr))
                {
                    conn.Open();
                    string sql = "SELECT E.GROUP_ID,E.GROUP_NM,E.LOGO AS GROUP_LOGO,D.COMPANY_ID,D.COMPANY_NM,D.LOGO AS COMPANY_LOGO,C.MODULE_ID," +
                        "C.MODULE_NM,C.MODULE_FOLDER,CASE WHEN C.MODULE_FOLDER='InsiderTrading' THEN D.IT_DB_NAME " +
                        "WHEN C.MODULE_FOLDER='BoardMeeting' THEN D.BMS_DB_NAME END AS DATABASE_NAME,C.LOGO AS MODULE_LOGO FROM PROCS_USERS(NOLOCK) A " +
                        "INNER JOIN PROCS_USERS_BU_ACESS(NOLOCK) B ON A.LOGIN_ID=B.LOGIN_ID " +
                        "INNER JOIN PROCS_MODULE(NOLOCK) C ON B.MODULE_ID=C.MODULE_ID " +
                        "INNER JOIN PROCS_BUSINESS_COMPANY(NOLOCK) D ON B.COMPANY_ID=D.COMPANY_ID " +
                        "INNER JOIN PROCS_BUSINESS_GROUP(NOLOCK) E ON D.GROUP_ID=E.GROUP_ID " +
                        "WHERE A.LOGIN_ID='" + email + "'";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        DataTable dtAccess = new DataTable();
                        SqlDataAdapter daAccess = new SqlDataAdapter(cmd);
                        daAccess.Fill(dtAccess);

                        if (dtAccess.Rows.Count == 1)
                        {
                            Session["CompanyId"] = Convert.ToInt32(dtAccess.Rows[0]["COMPANY_ID"]);
                            Session["CompanyName"] = Convert.ToString(dtAccess.Rows[0]["COMPANY_NM"]);
                            Session["ModuleId"] = Convert.ToInt32(dtAccess.Rows[0]["MODULE_ID"]);
                            Session["ModuleName"] = Convert.ToString(dtAccess.Rows[0]["MODULE_NM"]);
                            Session["ModuleFolder"] = Convert.ToString(dtAccess.Rows[0]["MODULE_FOLDER"]);
                            Session["ModuleDatabase"] = Convert.ToString(dtAccess.Rows[0]["DATABASE_NAME"]);
                            Session["AuthenticatedFrom"] = "Azure AD";
                            Session["EmployeeId"] = email;
                            Session["AdminDb"] = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["AdminDB"]), true);
                            Session["AuthToken"] = Guid.NewGuid().ToString();
                            Response.Cookies["AuthToken"].Value = Session["AuthToken"].ToString();
                            //================nc session=============
                            if (HttpContext.Current.Request.UserAgent.Contains("Edg"))
                            {
                                Session["Browser"] = "Microsoft Edge";
                            }
                            else
                            {
                                Session["Browser"] = bc.Browser;//nc
                            }

                            Session["MacId"] = GetClientMAC(GetIPAddress());//nc
                            Session["IP"] = GetIPAddress();//nc

                            SessionDTO sDTO = new SessionDTO();
                            sDTO.EMP_ID = Convert.ToString(email);
                            sDTO.MAC_ID = GetClientMAC(GetIPAddress());
                            sDTO.IP = GetIPAddress().ToString();
                            if (HttpContext.Current.Request.UserAgent.Contains("Edg"))
                            {
                                sDTO.BROWSER = "Microsoft Edge";
                            }
                            else
                            {
                                sDTO.BROWSER = bc.Browser;
                            }


                            SessionRequest sReq = new SessionRequest(sDTO);
                            SessionResponse sRes = sReq.SaveSession();

                            if (sRes.StatusFl == true)
                            {
                                string sModule = Convert.ToString(ConfigurationManager.AppSettings["Module"]);
                                if (sModule.ToUpper() == "UPSI")
                                {
                                    Response.Redirect(Session["ModuleFolder"] + "/" + "DashboardUpsi.aspx", false);
                                }
                                else
                                {
                                    Response.Redirect(Session["ModuleFolder"] + "/" + "PITDashboard.aspx", false);
                                } 
                            }
                            else if (sRes.StatusFl == false && sRes.Msg == "Sorry You have already logged in with another Browser or Another System")
                            {
                                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script type = 'text/javascript'>window.onload=function(){alert('Sorry You have already logged in with another Browser or Another System')};</script>");
                                 
                            }
                        }
                        else
                        {
                            string sAdminDB = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["AdminDB"]), true);
                            string sConStr = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionStringIT"]), true);
                            string sUser = "";
                            using (SqlConnection connX = new SqlConnection(sConStr))
                            {
                                connX.Open();
                                sql = "SELECT TOP 1 A.SALUTATION+' '+B.USER_NM+' ('+B.USER_EMAIL+')' AS USR FROM PROCS_INSIDER_USER(NOLOCK) A " +
                                    "INNER JOIN " + sAdminDB + "..PROCS_USERS(NOLOCK) B ON A.USER_LOGIN=B.LOGIN_ID " +
                                    "WHERE A.IS_APPROVER='Yes'";
                                using (SqlCommand cmdX = new SqlCommand(sql, connX))
                                {
                                    cmdX.CommandText = sql;
                                    cmdX.CommandType = CommandType.Text;
                                    sUser = Convert.ToString(cmdX.ExecuteScalar());
                                }
                            }
                        }
                    }
                }
            }
        }
        public static string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }
        [DllImport("Iphlpapi.dll")]
        private static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);
        [DllImport("Ws2_32.dll")]
        private static extern Int32 inet_addr(string ip);

        private static string GetClientMAC(string strClientIP)
        {
            string mac_dest = "";
            try
            {
                Int32 ldest = inet_addr(strClientIP);
                Int32 lhost = inet_addr("");
                Int64 macinfo = new Int64();
                Int32 len = 6;
                int res = SendARP(ldest, 0, ref macinfo, ref len);
                string mac_src = macinfo.ToString("X");

                while (mac_src.Length < 12)
                {
                    mac_src = mac_src.Insert(0, "0");
                }

                for (int i = 0; i < 11; i++)
                {
                    if (0 == (i % 2))
                    {
                        if (i == 10)
                        {
                            mac_dest = mac_dest.Insert(0, mac_src.Substring(i, 2));
                        }
                        else
                        {
                            mac_dest = "-" + mac_dest.Insert(0, mac_src.Substring(i, 2));
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw new Exception("L?i " + err.Message);
            }
            return mac_dest;
        }
    }
}

/*using ProcsDLL.Models.Infrastructure;
using Saml;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace ProcsDLL
{
    public partial class ValidateUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string samlCer = ConfigurationManager.AppSettings["CertKey"];
            string sResponse = string.Empty;
            sResponse = "PHNhbWxwOlJlc3BvbnNlIElEPSJfOGVlNzdiZGMtYjAzYy00NWM4LTgyM2YtMzMzODRjN2E0MzVjIiBWZXJzaW9uPSIyLjAiIElzc3VlSW5zdGFudD0iMjAyMy0wOC0yOFQwNzoxNToyOC40NDhaIiBEZXN0aW5hdGlvbj0iaHR0cHM6Ly9waXQuYmlybGFzb2Z0LmNvbS92YWxpZGF0ZXVzZXIuYXNweCIgSW5SZXNwb25zZVRvPSJfNWQ5MzFiNzUtODdhYS00NDkyLTg3ODEtNzg3NDljMjNlMjAxIiB4bWxuczpzYW1scD0idXJuOm9hc2lzOm5hbWVzOnRjOlNBTUw6Mi4wOnByb3RvY29sIj48SXNzdWVyIHhtbG5zPSJ1cm46b2FzaXM6bmFtZXM6dGM6U0FNTDoyLjA6YXNzZXJ0aW9uIj5odHRwczovL3N0cy53aW5kb3dzLm5ldC9kNzlkYTJlOS1kMDNhLTQ3MDctOWRhNy02N2EzNGFjNjQ2NWMvPC9Jc3N1ZXI+PHNhbWxwOlN0YXR1cz48c2FtbHA6U3RhdHVzQ29kZSBWYWx1ZT0idXJuOm9hc2lzOm5hbWVzOnRjOlNBTUw6Mi4wOnN0YXR1czpTdWNjZXNzIi8+PC9zYW1scDpTdGF0dXM+PEFzc2VydGlvbiBJRD0iX2QwOGRiZjgwLWQ4NDUtNDIzZS1iMjc2LTQ5NTYwZTdlMjYwMCIgSXNzdWVJbnN0YW50PSIyMDIzLTA4LTI4VDA3OjE1OjI4LjQ0MFoiIFZlcnNpb249IjIuMCIgeG1sbnM9InVybjpvYXNpczpuYW1lczp0YzpTQU1MOjIuMDphc3NlcnRpb24iPjxJc3N1ZXI+aHR0cHM6Ly9zdHMud2luZG93cy5uZXQvZDc5ZGEyZTktZDAzYS00NzA3LTlkYTctNjdhMzRhYzY0NjVjLzwvSXNzdWVyPjxTaWduYXR1cmUgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvMDkveG1sZHNpZyMiPjxTaWduZWRJbmZvPjxDYW5vbmljYWxpemF0aW9uTWV0aG9kIEFsZ29yaXRobT0iaHR0cDovL3d3dy53My5vcmcvMjAwMS8xMC94bWwtZXhjLWMxNG4jIi8+PFNpZ25hdHVyZU1ldGhvZCBBbGdvcml0aG09Imh0dHA6Ly93d3cudzMub3JnLzIwMDEvMDQveG1sZHNpZy1tb3JlI3JzYS1zaGEyNTYiLz48UmVmZXJlbmNlIFVSST0iI19kMDhkYmY4MC1kODQ1LTQyM2UtYjI3Ni00OTU2MGU3ZTI2MDAiPjxUcmFuc2Zvcm1zPjxUcmFuc2Zvcm0gQWxnb3JpdGhtPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwLzA5L3htbGRzaWcjZW52ZWxvcGVkLXNpZ25hdHVyZSIvPjxUcmFuc2Zvcm0gQWxnb3JpdGhtPSJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzEwL3htbC1leGMtYzE0biMiLz48L1RyYW5zZm9ybXM+PERpZ2VzdE1ldGhvZCBBbGdvcml0aG09Imh0dHA6Ly93d3cudzMub3JnLzIwMDEvMDQveG1sZW5jI3NoYTI1NiIvPjxEaWdlc3RWYWx1ZT53ZjBzMlRPcjhsN0txZlVKS21URVRVdEdaMHc2QnVhaGpkdURMcWRZMk5nPTwvRGlnZXN0VmFsdWU+PC9SZWZlcmVuY2U+PC9TaWduZWRJbmZvPjxTaWduYXR1cmVWYWx1ZT4ySVdjdmV6YVI5VjV5Ym5tM1BoczB1bDFkMjJ3Y0xLTy9NTjZsbDlsWlJwVnhnby9reFY4S0Y5QVJkUUJ1VElUK2ZWL2xlczFlWkxpcWdYcmNsVUx2L3JybGVXTW5rZzhiLzdXc1hLOVJWK2hjZEt5c3BmYlNEU1pFUHZDcjdtMjl2WldmcnhoWEwrS09oQXljZGQ0ek5PRUlzZGFHQ0VPYVBqSjZLTytuc1hSK1JxUWpXOVNjeXdRL2IxN0p6bkFSbWpKNDZQa2FEaUJlUDZFbmg4RFVnN0lJUlp3M3o2SGdmYy9SczkyMk80Yjh6REJQc1Fyd09uK2ZlM2xWNmZocmV3YUJJZ0Z1RnlvZ1p0L1N6TDdwZUo5MmR0cVZVdlU0NW4vY25ITEVHelgvTkNjTHNGMGJ2Q1NBdHFYZ0ovQ3dncE1GZDFZYkJjTERrUGM2ZlF3dmc9PTwvU2lnbmF0dXJlVmFsdWU+PEtleUluZm8+PFg1MDlEYXRhPjxYNTA5Q2VydGlmaWNhdGU+TUlJQzhEQ0NBZGlnQXdJQkFnSVFGVXJKeTRaN2FZWkRIalFEem5KdXR6QU5CZ2txaGtpRzl3MEJBUXNGQURBME1USXdNQVlEVlFRREV5bE5hV055YjNOdlpuUWdRWHAxY21VZ1JtVmtaWEpoZEdWa0lGTlRUeUJEWlhKMGFXWnBZMkYwWlRBZUZ3MHlNekE0TURnd05URTJOVEJhRncweU5qQTRNRGd3TlRFMk5EZGFNRFF4TWpBd0JnTlZCQU1US1UxcFkzSnZjMjltZENCQmVuVnlaU0JHWldSbGNtRjBaV1FnVTFOUElFTmxjblJwWm1sallYUmxNSUlCSWpBTkJna3Foa2lHOXcwQkFRRUZBQU9DQVE4QU1JSUJDZ0tDQVFFQTRvTHFac3pRRVk5aHpLanpCdUF0YXZxS2ZiMjZndndmVEd5RUk5bmNoVElwLzc5dE5ZaTlsWHo0RFU4ZzFLY01JQ1k2ZHpGQldlYmo0MTdNMm5od2gzKzNoYVQvcUZxL243Vnc5c0srOUNqbWlSTUtRalNvM29Zb0xXckZrTDFRWFBPV25tMUNJbVFmWWl1aXFGb0M4RnVYaHh6Q2Exa0VXZGVWMUUva3hTMjdXSUovZExVS3F4NW9EWUtKMkZwNjlaK0pyazZrNjM1V0NNWEI2aVVaaERlNXNtTnMwRHV1cWFWVDRuakMrbXRMRHhTVzBKSkx6ZU5HcWh4V3ExcUx4REVhdmd5VEY3ajZRTStGTE8xdk1ZbnY3OUMybmdhOUtSeEk5Z2pqbXExU3dVYTI4T1IzZSt2RU5iSzlEc29FOVREMDdldWtHWGEyUkJSaXNmcmdGUUlEQVFBQk1BMEdDU3FHU0liM0RRRUJDd1VBQTRJQkFRRGhHVXdwQjQ2U2sxRXM1WVVqcnRUaEFVYnhtRG9Ia1lxTGU3NTMvRFQzNVFkY29jTVRqRDc2VHROMC9VWStqTDY5SWV2SllwKzdQL1M3UnJRZWU4RXBERGZGM0hVYzJsQXFQVURVSkcxZlBjM2VvUFhRNkV3VFAzNHp5cHZ1bXd3WWFva0xnNWY2RnVHZk95QVMwMGZpcklEUUhJdExQU2U0QWhlMFYwU2NDZ1UrZ3p2ZGhyZVdPNU1sM3VYdU53b2JWOFZpcEFHYjdVeFRLTDlHWXJuUmJJRXJqZk9jdm5aL2lBZHpoNEliWXdncCtudVhIaWtFcVA3dFlCQlBKS3pqYWdhZHZRY094djVDTFdZVGNlMWJFRVdkeERSWHdUejdyWHF4Z0wvN2FVNWtIRjhCb21GbDExTWIxZG9yY2xxcHpyRlB6V0pseDV1SXpnZkRKR0RmPC9YNTA5Q2VydGlmaWNhdGU+PC9YNTA5RGF0YT48L0tleUluZm8+PC9TaWduYXR1cmU+PFN1YmplY3Q+PE5hbWVJRCBGb3JtYXQ9InVybjpvYXNpczpuYW1lczp0YzpTQU1MOjEuMTpuYW1laWQtZm9ybWF0OmVtYWlsQWRkcmVzcyI+QkhBVklLQVMzQEJJUkxBU09GVC5DT008L05hbWVJRD48U3ViamVjdENvbmZpcm1hdGlvbiBNZXRob2Q9InVybjpvYXNpczpuYW1lczp0YzpTQU1MOjIuMDpjbTpiZWFyZXIiPjxTdWJqZWN0Q29uZmlybWF0aW9uRGF0YSBJblJlc3BvbnNlVG89Il81ZDkzMWI3NS04N2FhLTQ0OTItODc4MS03ODc0OWMyM2UyMDEiIE5vdE9uT3JBZnRlcj0iMjAyMy0wOC0yOFQwODoxNToyOC4yOTRaIiBSZWNpcGllbnQ9Imh0dHBzOi8vcGl0LmJpcmxhc29mdC5jb20vdmFsaWRhdGV1c2VyLmFzcHgiLz48L1N1YmplY3RDb25maXJtYXRpb24+PC9TdWJqZWN0PjxDb25kaXRpb25zIE5vdEJlZm9yZT0iMjAyMy0wOC0yOFQwNzoxMDoyOC4yOTRaIiBOb3RPbk9yQWZ0ZXI9IjIwMjMtMDgtMjhUMDg6MTU6MjguMjk0WiI+PEF1ZGllbmNlUmVzdHJpY3Rpb24+PEF1ZGllbmNlPmh0dHBzOi8vcGl0LmJpcmxhc29mdC5jb20vdmFsaWRhdGV1c2VyLmFzcHg8L0F1ZGllbmNlPjwvQXVkaWVuY2VSZXN0cmljdGlvbj48L0NvbmRpdGlvbnM+PEF0dHJpYnV0ZVN0YXRlbWVudD48QXR0cmlidXRlIE5hbWU9Imh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vaWRlbnRpdHkvY2xhaW1zL3RlbmFudGlkIj48QXR0cmlidXRlVmFsdWU+ZDc5ZGEyZTktZDAzYS00NzA3LTlkYTctNjdhMzRhYzY0NjVjPC9BdHRyaWJ1dGVWYWx1ZT48L0F0dHJpYnV0ZT48QXR0cmlidXRlIE5hbWU9Imh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vaWRlbnRpdHkvY2xhaW1zL29iamVjdGlkZW50aWZpZXIiPjxBdHRyaWJ1dGVWYWx1ZT5jZjE3NmI3My05NTU1LTRkYTctOTVkNS05YzFhZjgwMzM2Yjg8L0F0dHJpYnV0ZVZhbHVlPjwvQXR0cmlidXRlPjxBdHRyaWJ1dGUgTmFtZT0iaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS9pZGVudGl0eS9jbGFpbXMvZGlzcGxheW5hbWUiPjxBdHRyaWJ1dGVWYWx1ZT5CaGF2aWthIFN1cmFuYTwvQXR0cmlidXRlVmFsdWU+PC9BdHRyaWJ1dGU+PEF0dHJpYnV0ZSBOYW1lPSJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL2lkZW50aXR5L2NsYWltcy9pZGVudGl0eXByb3ZpZGVyIj48QXR0cmlidXRlVmFsdWU+aHR0cHM6Ly9zdHMud2luZG93cy5uZXQvZDc5ZGEyZTktZDAzYS00NzA3LTlkYTctNjdhMzRhYzY0NjVjLzwvQXR0cmlidXRlVmFsdWU+PC9BdHRyaWJ1dGU+PEF0dHJpYnV0ZSBOYW1lPSJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL2NsYWltcy9hdXRobm1ldGhvZHNyZWZlcmVuY2VzIj48QXR0cmlidXRlVmFsdWU+aHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2F1dGhlbnRpY2F0aW9ubWV0aG9kL3Bhc3N3b3JkPC9BdHRyaWJ1dGVWYWx1ZT48QXR0cmlidXRlVmFsdWU+aHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS9jbGFpbXMvbXVsdGlwbGVhdXRobjwvQXR0cmlidXRlVmFsdWU+PC9BdHRyaWJ1dGU+PEF0dHJpYnV0ZSBOYW1lPSJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9naXZlbm5hbWUiPjxBdHRyaWJ1dGVWYWx1ZT5CaGF2aWthPC9BdHRyaWJ1dGVWYWx1ZT48L0F0dHJpYnV0ZT48QXR0cmlidXRlIE5hbWU9Imh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL3N1cm5hbWUiPjxBdHRyaWJ1dGVWYWx1ZT5TdXJhbmE8L0F0dHJpYnV0ZVZhbHVlPjwvQXR0cmlidXRlPjxBdHRyaWJ1dGUgTmFtZT0iaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZW1haWxhZGRyZXNzIj48QXR0cmlidXRlVmFsdWU+QmhhdmlrYS5TdXJhbmFAYmlybGFzb2Z0LmNvbTwvQXR0cmlidXRlVmFsdWU+PC9BdHRyaWJ1dGU+PEF0dHJpYnV0ZSBOYW1lPSJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIj48QXR0cmlidXRlVmFsdWU+QkhBVklLQVMzQEJJUkxBU09GVC5DT008L0F0dHJpYnV0ZVZhbHVlPjwvQXR0cmlidXRlPjwvQXR0cmlidXRlU3RhdGVtZW50PjxBdXRoblN0YXRlbWVudCBBdXRobkluc3RhbnQ9IjIwMjMtMDgtMjhUMDc6MTU6MjguMzc5WiIgU2Vzc2lvbkluZGV4PSJfZDA4ZGJmODAtZDg0NS00MjNlLWIyNzYtNDk1NjBlN2UyNjAwIj48QXV0aG5Db250ZXh0PjxBdXRobkNvbnRleHRDbGFzc1JlZj51cm46b2FzaXM6bmFtZXM6dGM6U0FNTDoyLjA6YWM6Y2xhc3NlczpQYXNzd29yZDwvQXV0aG5Db250ZXh0Q2xhc3NSZWY+PC9BdXRobkNvbnRleHQ+PC9BdXRoblN0YXRlbWVudD48L0Fzc2VydGlvbj48L3NhbWxwOlJlc3BvbnNlPg==";

            Saml.Response samlResponse = new Response(samlCer, sResponse);
            var samlEndpoint = ConfigurationManager.AppSettings["LogoutUrl"];
            string username, email, firstname, lastname;
            username = samlResponse.GetNameID();
            email = samlResponse.GetEmail();
            firstname = samlResponse.GetFirstName();
            lastname = samlResponse.GetLastName();

            string ConnStr = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionString"]), true);
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                string sql = "SELECT E.GROUP_ID,E.GROUP_NM,E.LOGO AS GROUP_LOGO,D.COMPANY_ID,D.COMPANY_NM,D.LOGO AS COMPANY_LOGO,C.MODULE_ID," +
                    "C.MODULE_NM,C.MODULE_FOLDER,CASE WHEN C.MODULE_FOLDER='InsiderTrading' THEN D.IT_DB_NAME " +
                    "WHEN C.MODULE_FOLDER='BoardMeeting' THEN D.BMS_DB_NAME END AS DATABASE_NAME,C.LOGO AS MODULE_LOGO FROM PROCS_USERS(NOLOCK) A " +
                    "INNER JOIN PROCS_USERS_BU_ACESS(NOLOCK) B ON A.LOGIN_ID=B.LOGIN_ID " +
                    "INNER JOIN PROCS_MODULE(NOLOCK) C ON B.MODULE_ID=C.MODULE_ID " +
                    "INNER JOIN PROCS_BUSINESS_COMPANY(NOLOCK) D ON B.COMPANY_ID=D.COMPANY_ID " +
                    "INNER JOIN PROCS_BUSINESS_GROUP(NOLOCK) E ON D.GROUP_ID=E.GROUP_ID " +
                    "WHERE A.LOGIN_ID='" + email + "'";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    DataTable dtAccess = new DataTable();
                    SqlDataAdapter daAccess = new SqlDataAdapter(cmd);
                    daAccess.Fill(dtAccess);

                    if (dtAccess.Rows.Count == 1)
                    {
                        Session["CompanyId"] = Convert.ToInt32(dtAccess.Rows[0]["COMPANY_ID"]);
                        Session["CompanyName"] = Convert.ToString(dtAccess.Rows[0]["COMPANY_NM"]);
                        Session["ModuleId"] = Convert.ToInt32(dtAccess.Rows[0]["MODULE_ID"]);
                        Session["ModuleName"] = Convert.ToString(dtAccess.Rows[0]["MODULE_NM"]);
                        Session["ModuleFolder"] = Convert.ToString(dtAccess.Rows[0]["MODULE_FOLDER"]);
                        Session["ModuleDatabase"] = Convert.ToString(dtAccess.Rows[0]["DATABASE_NAME"]);
                        Session["AuthenticatedFrom"] = "Azure AD";
                        Session["EmployeeId"] = email;
                        Session["AdminDb"] = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["AdminDB"]), true);
                        Response.Redirect("/" + Session["ModuleFolder"] + "/" + "Dashboard.aspx");
                    }
                    else
                    {
                        string sAdminDB = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["AdminDB"]), true);
                        string sConStr = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionStringIT"]), true);
                        string sUser = "";
                        using (SqlConnection connX = new SqlConnection(sConStr))
                        {
                            connX.Open();
                            sql = "SELECT TOP 1 A.SALUTATION+' '+B.USER_NM+' ('+B.USER_EMAIL+')' AS USR FROM PROCS_INSIDER_USER(NOLOCK) A " +
                                "INNER JOIN " + sAdminDB + "..PROCS_USERS(NOLOCK) B ON A.USER_LOGIN=B.LOGIN_ID " +
                                "WHERE A.IS_APPROVER='Yes'";
                            using (SqlCommand cmdX = new SqlCommand(sql, connX))
                            {
                                cmdX.CommandText = sql;
                                cmdX.CommandType = CommandType.Text;
                                sUser = Convert.ToString(cmdX.ExecuteScalar());
                            }
                        }
                    }
                }
            }
        }
    }
}*/