using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.SessionState;
namespace ProcsDLL.Models.InsiderTrading.Repository
{
    public class EmailConfigRepository : IRequiresSessionState
    {
        public EmailConfigResponse GetEmailConfig(UPSIEmailConfig objUPSIConfig)
        {
            EmailConfigResponse configResponse = new EmailConfigResponse();
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@Mode", "GET_EMAIL_CONFIG");
                parameters[1] = new SqlParameter("@CompanyId", objUPSIConfig.CompanyId);
                parameters[2] = new SqlParameter("@UserLogin", objUPSIConfig.UserLogin);

                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_EMAIL_CONFIG", objUPSIConfig.MODULE_DATABASE, parameters);
                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        objUPSIConfig.ConfigId = Convert.ToInt32(dr["CONFIG_ID"]);
                        objUPSIConfig.AuthenticationType = Convert.ToString(dr["AUTHENTICATION_TYPE"]);
                        objUPSIConfig.ClientCertificate = Convert.ToString(dr["OFFICE_CLIENT_SECRET"]);
                        objUPSIConfig.ClientId = Convert.ToString(dr["OFFICE_CLIENT_ID"]);
                        objUPSIConfig.GoogleCertificate = Convert.ToString(dr["GOOGLE_CERTIFICATE"]);
                        objUPSIConfig.GoogleServiceAccount = Convert.ToString(dr["GOOGLE_SERVICE_ACCOUNT_EMAIL"]);
                        objUPSIConfig.Password = Convert.ToString(dr["EMAIL_PASSWORD"]);
                        objUPSIConfig.OutgoingPort = Convert.ToString(dr["OUTGOING_PORT"]);
                        objUPSIConfig.OutgoingProtocol = Convert.ToString(dr["OUTGOING_PROTOCOL_ADDR"]);
                        objUPSIConfig.IncomingPort = Convert.ToString(dr["INCOMING_PORT"]);
                        objUPSIConfig.IncomingProtocol = Convert.ToString(dr["INCOMING_PROTOCOL_ADDR"]);
                        objUPSIConfig.ProtocolType = Convert.ToString(dr["INCOMING_PROTOCOL"]);
                        objUPSIConfig.SmartType = Convert.ToString(dr["SMART_TYPE"]);
                        objUPSIConfig.TenantId = Convert.ToString(dr["OFFICE_TENANT_ID"]);
                        objUPSIConfig.UPSIEmail = Convert.ToString(dr["UPSI_EMAIL"]);
                        objUPSIConfig.AccessToken = Convert.ToString(dr["ACCESS_TOKEN"]);
                        objUPSIConfig.RefreshToken = Convert.ToString(dr["REFRESH_TOKEN"]);
                        objUPSIConfig.IsSSL = Convert.ToString(dr["SSL_FL"]);
                        objUPSIConfig.AuthenticationEmail = Convert.ToString(dr["CONSENT_EMAIL"]);
                        objUPSIConfig.DisplayNm = Convert.ToString(dr["DISPLAY_NAME"]);
                    }
                    configResponse.StatusFl = true;
                    configResponse.Msg = "Data has been fetched successfully !";
                    configResponse.emailConfig = objUPSIConfig;
                }
                else
                {
                    configResponse.StatusFl = false;
                    configResponse.Msg = "No data found !";
                }
                return configResponse;
            }
            catch (Exception ex)
            {
                configResponse.StatusFl = false;
                configResponse.Msg = "Processing failed, because of system error !";
            }
            return configResponse;
        }
        public EmailConfigResponse AddBasicEmailConfig(UPSIEmailConfig objUPSIConfig)
        {
            EmailConfigResponse configResponse = new EmailConfigResponse();
            try
            {
                SqlParameter[] parameters = new SqlParameter[14];
                parameters[0] = new SqlParameter("@Mode", "INSERT");
                parameters[1] = new SqlParameter("@CompanyId", objUPSIConfig.CompanyId);
                parameters[2] = new SqlParameter("@UserLogin", objUPSIConfig.UserLogin);
                parameters[3] = new SqlParameter("@PitEmail", objUPSIConfig.UPSIEmail);
                parameters[4] = new SqlParameter("@AuthenticationType", objUPSIConfig.AuthenticationType);
                parameters[5] = new SqlParameter("@IncomingProtocol", objUPSIConfig.ProtocolType);
                parameters[6] = new SqlParameter("@IncomingProtocolAddr", objUPSIConfig.IncomingProtocol);
                parameters[7] = new SqlParameter("@IncomingPort", objUPSIConfig.IncomingPort);
                parameters[8] = new SqlParameter("@OutgoingProtocolAddr", objUPSIConfig.OutgoingProtocol);
                parameters[9] = new SqlParameter("@OutgoingPort", objUPSIConfig.OutgoingPort);
                parameters[10] = new SqlParameter("@IsSsl", objUPSIConfig.IsSSL);
                parameters[11] = new SqlParameter("@EmailPwd", objUPSIConfig.Password);
                parameters[12] = new SqlParameter("@ConfigId", objUPSIConfig.ConfigId);
                parameters[13] = new SqlParameter("@DisplayNm", objUPSIConfig.DisplayNm);

                var obj = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_EMAIL_CONFIG", objUPSIConfig.MODULE_DATABASE, parameters);
                if ((string)obj == "Success")
                {
                    configResponse.StatusFl = true;
                    configResponse.Msg = "Data has been saved successfully !";
                }
                else
                {
                    configResponse.StatusFl = false;
                    configResponse.Msg = "Duplicate Entry !";
                }
            }
            catch (Exception ex)
            {
                configResponse.StatusFl = false;
                configResponse.Msg = "Processing failed, because of system error !";
            }
            return configResponse;
        }
        public EmailConfigResponse AddSmartEmailConfig(UPSIEmailConfig objUPSIConfig)
        {
            EmailConfigResponse configResponse = new EmailConfigResponse();
            try
            {
                SqlParameter[] parameters = new SqlParameter[23];
                parameters[0] = new SqlParameter("@Mode", "INSERT");
                parameters[1] = new SqlParameter("@CompanyId", objUPSIConfig.CompanyId);
                parameters[2] = new SqlParameter("@UserLogin", objUPSIConfig.UserLogin);
                parameters[3] = new SqlParameter("@PitEmail", objUPSIConfig.UPSIEmail);
                parameters[4] = new SqlParameter("@AuthenticationType", objUPSIConfig.AuthenticationType);
                parameters[5] = new SqlParameter("@IncomingProtocol", objUPSIConfig.ProtocolType);
                parameters[6] = new SqlParameter("@IncomingProtocolAddr", objUPSIConfig.IncomingProtocol);
                parameters[7] = new SqlParameter("@IncomingPort", objUPSIConfig.IncomingPort);
                
                parameters[8] = new SqlParameter("@OutgoingProtocolAddr", objUPSIConfig.OutgoingProtocol);
                parameters[9] = new SqlParameter("@OutgoingPort", objUPSIConfig.OutgoingPort);

                parameters[10] = new SqlParameter("@ClientId", objUPSIConfig.ClientId);
                parameters[11] = new SqlParameter("@ClientSecret", objUPSIConfig.ClientCertificate);
                parameters[12] = new SqlParameter("@TenantId", objUPSIConfig.TenantId);
                parameters[13] = new SqlParameter("@GoogleServiceAccount", objUPSIConfig.GoogleServiceAccount);
                parameters[14] = new SqlParameter("@GoogleCertificate", objUPSIConfig.GoogleCertificate);
                parameters[15] = new SqlParameter("@EmailType", objUPSIConfig.SmartType);

                parameters[16] = new SqlParameter("@IsSsl", objUPSIConfig.IsSSL);
                //parameters[17] = new SqlParameter("@UpsiTypId", objUPSIConfig.UpsiTypId);
                parameters[17] = new SqlParameter("@AccessToken", objUPSIConfig.AccessToken);
                parameters[18] = new SqlParameter("@RefreshToken", objUPSIConfig.RefreshToken);
                parameters[19] = new SqlParameter("@ExpiryAt", objUPSIConfig.ExpiryAt);
                parameters[20] = new SqlParameter("@ConfigId", objUPSIConfig.ConfigId);

                parameters[21] = new SqlParameter("@ConsentEmail", objUPSIConfig.AuthenticationEmail);
                parameters[22] = new SqlParameter("@DisplayNm", objUPSIConfig.DisplayNm);

                var obj = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_EMAIL_CONFIG", objUPSIConfig.MODULE_DATABASE, parameters);
                if ((string)obj == "Success")
                {
                    configResponse.StatusFl = true;
                    configResponse.Msg = "Data has been saved successfully !";
                }
                else
                {
                    configResponse.StatusFl = false;
                    configResponse.Msg = "Duplicate Entry !";
                }
            }
            catch (Exception ex)
            {
                configResponse.StatusFl = false;
                configResponse.Msg = "Processing failed, because of system error !";
            }
            return configResponse;
        }
    }
}