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
    public class UPSIConfigRepository : IRequiresSessionState
    {
        public UPSIConfigResponse AddUPSIConfig(UPSIConfig objUPSIConfig)
        {
            UPSIConfigResponse configResponse = new UPSIConfigResponse();
            try
            {
                SqlParameter[] parameters = new SqlParameter[9];
                parameters[0] = new SqlParameter("@Mode", "INSERT");
                parameters[1] = new SqlParameter("@CompanyId", objUPSIConfig.CompanyId);
                parameters[2] = new SqlParameter("@UserLogin", objUPSIConfig.UserLogin);
                parameters[3] = new SqlParameter("@AccessibleToCO", objUPSIConfig.AccessibleToCO);
                parameters[4] = new SqlParameter("@ManagedToCO", objUPSIConfig.ManagedToCO);
                parameters[5] = new SqlParameter("@AccessibleType", objUPSIConfig.AccessibleType);
                parameters[6] = new SqlParameter("@EmailAutomation", objUPSIConfig.EmailAutomation);
                parameters[7] = new SqlParameter("@MultipleEmail", objUPSIConfig.MultipleEmail);
                parameters[8] = new SqlParameter("@AuthorizedUsr", objUPSIConfig.AuthorizedUsr);
                //parameters[9] = new SqlParameter("@AssignTask", objUPSIConfig.AssignTask);

                var obj = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_CONFIG", objUPSIConfig.MODULE_DATABASE, parameters);
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
        public UPSIEmailConfigResponse AddBasicUPSIEmailConfig(UPSIEmailConfig objUPSIConfig)
        {
            UPSIEmailConfigResponse configResponse = new UPSIEmailConfigResponse();
            try
            {
                SqlParameter[] parameters = new SqlParameter[14];
                parameters[0] = new SqlParameter("@Mode", "INSERT");
                parameters[1] = new SqlParameter("@CompanyId", objUPSIConfig.CompanyId);
                parameters[2] = new SqlParameter("@UserLogin", objUPSIConfig.UserLogin);
                parameters[3] = new SqlParameter("@UpsiEmail", objUPSIConfig.UPSIEmail);
                parameters[4] = new SqlParameter("@AuthenticationType", objUPSIConfig.AuthenticationType);
                parameters[5] = new SqlParameter("@IncomingProtocol", objUPSIConfig.ProtocolType);
                parameters[6] = new SqlParameter("@IncomingProtocolAddr", objUPSIConfig.IncomingProtocol);
                parameters[7] = new SqlParameter("@IncomingPort", objUPSIConfig.IncomingPort);
                parameters[8] = new SqlParameter("@OutgoingProtocolAddr", objUPSIConfig.OutgoingProtocol);
                parameters[9] = new SqlParameter("@OutgoingPort", objUPSIConfig.OutgoingPort);
                parameters[10] = new SqlParameter("@IsSsl", objUPSIConfig.IsSSL);
                parameters[11] = new SqlParameter("@EmailPwd", objUPSIConfig.Password);
                parameters[12] = new SqlParameter("@UpsiTypId", objUPSIConfig.UpsiTypId);
                parameters[13] = new SqlParameter("@ConfigId", objUPSIConfig.ConfigId);

                var obj = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_EMAIL_CONFIG", objUPSIConfig.MODULE_DATABASE, parameters);
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
        public UPSIEmailConfigResponse AddSmartUPSIEmailConfig(UPSIEmailConfig objUPSIConfig)
        {
            UPSIEmailConfigResponse configResponse = new UPSIEmailConfigResponse();
            try
            {
                SqlParameter[] parameters = new SqlParameter[23];
                parameters[0] = new SqlParameter("@Mode", "INSERT");
                parameters[1] = new SqlParameter("@CompanyId", objUPSIConfig.CompanyId);
                parameters[2] = new SqlParameter("@UserLogin", objUPSIConfig.UserLogin);
                parameters[3] = new SqlParameter("@UpsiEmail", objUPSIConfig.UPSIEmail);
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
                parameters[17] = new SqlParameter("@UpsiTypId", objUPSIConfig.UpsiTypId);
                parameters[18] = new SqlParameter("@AccessToken", objUPSIConfig.AccessToken);
                parameters[19] = new SqlParameter("@RefreshToken", objUPSIConfig.RefreshToken);
                parameters[20] = new SqlParameter("@ExpiryAt", objUPSIConfig.ExpiryAt);
                parameters[21] = new SqlParameter("@ConfigId", objUPSIConfig.ConfigId);
                parameters[22] = new SqlParameter("@ConsentEmail", objUPSIConfig.AuthenticationEmail);

                var obj = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_EMAIL_CONFIG", objUPSIConfig.MODULE_DATABASE, parameters);
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
        public UPSIConfigResponse AddSmtpConfig(UPSIConfiguration objSmtpConfig)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[15];
                parameters[0] = new SqlParameter("@SMPT_CONFIG_ID", objSmtpConfig.SMPT_CONFIG_ID);
                parameters[1] = new SqlParameter("@COMPANY_ID", objSmtpConfig.COMPANY_ID);
                parameters[2] = new SqlParameter("@Mode", "CHECK");
                parameters[3] = new SqlParameter("@ACTION_TYPE", "INSERT");
                parameters[4] = new SqlParameter("@EMPLOYEE_ID", objSmtpConfig.CREATE_BY);
                parameters[5] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[5].Direction = ParameterDirection.Output;
                parameters[6] = new SqlParameter("@DEFAULT_EMAIL_OUTGOING", objSmtpConfig.DEFAULT_EMAIL_OUTGOING);
                parameters[7] = new SqlParameter("@CONTINUAL_DISCLOSURE_EMAIL_OUTGOING", objSmtpConfig.CONTINUAL_DISCLOSURE_EMAIL_OUTGOING);
                parameters[8] = new SqlParameter("@SMTP_HOST_NAME_OUTGOING", objSmtpConfig.SMTP_HOST_NAME_OUTGOING);
                parameters[9] = new SqlParameter("@PORT_OUTGOING", objSmtpConfig.PORT_OUTGOING);
                parameters[10] = new SqlParameter("@SSL_OUTGOING", objSmtpConfig.SSL_OUTGOING);
                parameters[11] = new SqlParameter("@SMTP_USER_NAME_OUTGOING", objSmtpConfig.SMTP_USER_NAME_OUTGOING);
                parameters[12] = new SqlParameter("@PASSWORD_OUTGOING", objSmtpConfig.PASSWORD_OUTGOING);
                parameters[13] = new SqlParameter("@USER_DEFAULT_CREDENTIAL_OUTGOING", objSmtpConfig.USER_DEFAULT_CREDENTIAL_OUTGOING);
                parameters[14] = new SqlParameter("@IS_POP", objSmtpConfig.IS_POP);

                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_CONFIG_SETUP", objSmtpConfig.MODULE_DATABASE, parameters);
                var obj = parameters[5].Value;
                UPSIConfigResponse oSmtpConfig = new UPSIConfigResponse();
                if ((Int32)obj == 0)
                {
                    parameters[2] = new SqlParameter("@Mode", "INSERT_UPDATE");

                    SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_CONFIG_SETUP", objSmtpConfig.MODULE_DATABASE, parameters);

                    parameters[2] = new SqlParameter("@Mode", "GET_SMTP_CONFIG_ID_BY_SMTP_CONFIG_DETAIL");
                    var SMTP_CONFIG_ID = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_CONFIG_SETUP", objSmtpConfig.MODULE_DATABASE, parameters);
                    objSmtpConfig.SMPT_CONFIG_ID = (Int32)SMTP_CONFIG_ID;

                    oSmtpConfig.StatusFl = true;
                    oSmtpConfig.Msg = "Data has been saved successfully !";
                    oSmtpConfig.SmtpConfig = objSmtpConfig;

                }
                else
                {
                    oSmtpConfig.StatusFl = false;
                    oSmtpConfig.Msg = "Duplicate Entry !";
                }
                return oSmtpConfig;
            }
            catch (Exception ex)
            {
                UPSIConfigResponse oSmtpConfig = new UPSIConfigResponse();
                oSmtpConfig.StatusFl = false;
                oSmtpConfig.Msg = "Processing failed, because of system error !";
                return oSmtpConfig;
            }
        }
        public UPSIConfigResponse UpdateSmtpConfig(UPSIConfiguration objSmtpConfig)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[15];
                parameters[0] = new SqlParameter("@SMPT_CONFIG_ID", objSmtpConfig.SMPT_CONFIG_ID);
                parameters[1] = new SqlParameter("@COMPANY_ID", objSmtpConfig.COMPANY_ID);
                parameters[2] = new SqlParameter("@Mode", "CHECK");
                parameters[3] = new SqlParameter("@ACTION_TYPE", "UPDATE");
                parameters[4] = new SqlParameter("@EMPLOYEE_ID", objSmtpConfig.CREATE_BY);
                parameters[5] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[5].Direction = ParameterDirection.Output;
                parameters[6] = new SqlParameter("@DEFAULT_EMAIL_OUTGOING", objSmtpConfig.DEFAULT_EMAIL_OUTGOING);
                parameters[7] = new SqlParameter("@CONTINUAL_DISCLOSURE_EMAIL_OUTGOING", objSmtpConfig.CONTINUAL_DISCLOSURE_EMAIL_OUTGOING);
                parameters[8] = new SqlParameter("@SMTP_HOST_NAME_OUTGOING", objSmtpConfig.SMTP_HOST_NAME_OUTGOING);
                parameters[9] = new SqlParameter("@PORT_OUTGOING", objSmtpConfig.PORT_OUTGOING);
                parameters[10] = new SqlParameter("@SSL_OUTGOING", objSmtpConfig.SSL_OUTGOING);
                parameters[11] = new SqlParameter("@SMTP_USER_NAME_OUTGOING", objSmtpConfig.SMTP_USER_NAME_OUTGOING);
                parameters[12] = new SqlParameter("@PASSWORD_OUTGOING", objSmtpConfig.PASSWORD_OUTGOING);
                parameters[13] = new SqlParameter("@USER_DEFAULT_CREDENTIAL_OUTGOING", objSmtpConfig.USER_DEFAULT_CREDENTIAL_OUTGOING);
                parameters[14] = new SqlParameter("@IS_POP", objSmtpConfig.IS_POP);

                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_CONFIG_SETUP", objSmtpConfig.MODULE_DATABASE, parameters);
                var obj = parameters[5].Value;
                UPSIConfigResponse oSmtpConfig = new UPSIConfigResponse();
                if ((Int32)obj == 0)
                {
                    parameters[2] = new SqlParameter("@Mode", "INSERT_UPDATE");

                    SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_CONFIG_SETUP", objSmtpConfig.MODULE_DATABASE, parameters);

                    parameters[2] = new SqlParameter("@Mode", "GET_SMTP_CONFIG_ID_BY_SMTP_CONFIG_DETAIL");
                    var SMTP_CONFIG_ID = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_CONFIG_SETUP", objSmtpConfig.MODULE_DATABASE, parameters);
                    objSmtpConfig.SMPT_CONFIG_ID = (Int32)SMTP_CONFIG_ID;

                    oSmtpConfig.StatusFl = true;
                    oSmtpConfig.Msg = "Data has been updated successfully !";
                    oSmtpConfig.SmtpConfig = objSmtpConfig;
                }
                else
                {
                    oSmtpConfig.StatusFl = false;
                    oSmtpConfig.Msg = "Duplicate Entry !";
                }
                return oSmtpConfig;
            }
            catch (Exception ex)
            {
                UPSIConfigResponse oSmtpConfig = new UPSIConfigResponse();
                oSmtpConfig.StatusFl = false;
                oSmtpConfig.Msg = "Processing failed, because of system error !";
                return oSmtpConfig;
            }
        }
        public UPSIConfigResponse DeleteSmtpConfig(UPSIConfiguration objSmtpConfig)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@SMPT_CONFIG_ID", objSmtpConfig.SMPT_CONFIG_ID);
                parameters[1] = new SqlParameter("@COMPANY_ID", objSmtpConfig.COMPANY_ID);
                parameters[2] = new SqlParameter("@Mode", "Delete_Smtp_Config");
                parameters[3] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[3].Direction = ParameterDirection.Output;
                UPSIConfigResponse oSmtpConfig = new UPSIConfigResponse();

                var result = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_CONFIG_SETUP", objSmtpConfig.MODULE_DATABASE, parameters);
                oSmtpConfig.StatusFl = true;
                oSmtpConfig.Msg = "Data has been deleted successfully !";
                oSmtpConfig.SmtpConfig = objSmtpConfig;
                return oSmtpConfig;
            }
            catch (Exception ex)
            {
                UPSIConfigResponse oSmtpConfig = new UPSIConfigResponse();
                oSmtpConfig.StatusFl = false;
                oSmtpConfig.Msg = "Processing failed, because of system error !";
                return oSmtpConfig;
            }
        }
        public UPSIConfigResponse GetSmtpConfigList(UPSIConfiguration objSmtpConfig)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@Mode", "GET_Smtp_Config_List");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objSmtpConfig.COMPANY_ID);

                UPSIConfigResponse oSmtpConfig = new UPSIConfigResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_CONFIG_SETUP", objSmtpConfig.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        UPSIConfiguration obj = new UPSIConfiguration();
                        obj.SMPT_CONFIG_ID = !String.IsNullOrEmpty(Convert.ToString(rdr["SMTP_CONFIG_ID"])) ? Convert.ToInt32(rdr["SMTP_CONFIG_ID"]) : 0;
                        obj.COMPANY_ID = !String.IsNullOrEmpty(Convert.ToString(rdr["COMPANY_ID"])) ? Convert.ToInt32(rdr["COMPANY_ID"]) : 0;
                        obj.CREATE_BY = !String.IsNullOrEmpty(Convert.ToString(rdr["CREATE_BY"])) ? Convert.ToString(rdr["CREATE_BY"]) : String.Empty;
                        obj.CREATED_ON = !String.IsNullOrEmpty(Convert.ToString(rdr["CREATED_ON"])) ? Convert.ToString(rdr["CREATED_ON"]) : String.Empty;
                        obj.DEFAULT_EMAIL_OUTGOING = !String.IsNullOrEmpty(Convert.ToString(rdr["DEFAULT_EMAIL_OUTGOING"])) ? Convert.ToString(rdr["DEFAULT_EMAIL_OUTGOING"]) : String.Empty;
                        obj.CONTINUAL_DISCLOSURE_EMAIL_OUTGOING = !String.IsNullOrEmpty(Convert.ToString(rdr["CONTINUAL_DISCLOSURE_EMAIL_OUTGOING"])) ? Convert.ToString(rdr["CONTINUAL_DISCLOSURE_EMAIL_OUTGOING"]) : String.Empty;
                        obj.SMTP_HOST_NAME_OUTGOING = !String.IsNullOrEmpty(Convert.ToString(rdr["SMTP_HOST_NAME_OUTGOING"])) ? Convert.ToString(rdr["SMTP_HOST_NAME_OUTGOING"]) : String.Empty;
                        obj.PORT_OUTGOING = !String.IsNullOrEmpty(Convert.ToString(rdr["PORT_OUTGOING"])) ? Convert.ToString(rdr["PORT_OUTGOING"]) : String.Empty;
                        obj.SSL_OUTGOING = !String.IsNullOrEmpty(Convert.ToString(rdr["SSL_OUTGOING"])) ? Convert.ToString(rdr["SSL_OUTGOING"]) : String.Empty;
                        obj.SMTP_USER_NAME_OUTGOING = !String.IsNullOrEmpty(Convert.ToString(rdr["SMTP_USER_NAME_OUTGOING"])) ? Convert.ToString(rdr["SMTP_USER_NAME_OUTGOING"]) : String.Empty;
                        obj.PASSWORD_OUTGOING = !String.IsNullOrEmpty(Convert.ToString(rdr["PASSWORD_OUTGOING"])) ? Convert.ToString(rdr["PASSWORD_OUTGOING"]) : String.Empty;
                        obj.USER_DEFAULT_CREDENTIAL_OUTGOING = !String.IsNullOrEmpty(Convert.ToString(rdr["USER_DEFAULT_CREDENTIAL_OUTGOING"])) ? Convert.ToString(rdr["USER_DEFAULT_CREDENTIAL_OUTGOING"]) : String.Empty;
                        obj.IS_POP = !String.IsNullOrEmpty(Convert.ToString(rdr["IS_POP"])) ? Convert.ToString(rdr["IS_POP"]) : String.Empty;
                        oSmtpConfig.AddObject(obj);
                    }
                    oSmtpConfig.StatusFl = true;
                    oSmtpConfig.Msg = "Data has been fetched successfully !";
                }
                else
                {
                    oSmtpConfig.StatusFl = false;
                    oSmtpConfig.Msg = "No data found !";
                }
                rdr.Close();
                return oSmtpConfig;
            }
            catch (Exception ex)
            {
                UPSIConfigResponse oSmtpConfig = new UPSIConfigResponse();
                oSmtpConfig.StatusFl = false;
                oSmtpConfig.Msg = "Processing failed, because of system error !";
                return oSmtpConfig;
            }
        }
        public UPSIConfigResponse GetUPSIConfig(UPSIConfig objUPSIConfig)
        {
            UPSIConfigResponse configResponse = new UPSIConfigResponse();
            try
            {
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@Mode", "GET_UPSI_CONFIG");
                parameters[1] = new SqlParameter("@CompanyId", objUPSIConfig.CompanyId);

                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_CONFIG", objUPSIConfig.MODULE_DATABASE, parameters);
                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        objUPSIConfig.AccessibleToCO = Convert.ToString(dr["SHOW_UPSI_TO_CO"]);
                        objUPSIConfig.ManagedToCO = Convert.ToString(dr["Managed_By_Co"]);
                        objUPSIConfig.AccessibleType = Convert.ToString(dr["VISIBILITY"]);
                        objUPSIConfig.EmailAutomation = Convert.ToString(dr["EMAIL_AUTOMATION"]);
                        objUPSIConfig.MultipleEmail = Convert.ToString(dr["MUTIPLE_EMAIL"]);
                        objUPSIConfig.AuthorizedUsr = Convert.ToString(dr["AUTHORIZED_USR"]);
                    }
                    configResponse.StatusFl = true;
                    configResponse.Msg = "Data has been fetched successfully !";
                    configResponse.UpsiConfig = objUPSIConfig;
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
                return configResponse;
            }
        }
        public UPSIEmailConfigResponse GetUPSIEmailConfig(UPSIEmailConfig objUPSIConfig)
        {
            UPSIEmailConfigResponse configResponse = new UPSIEmailConfigResponse();
            try
            {
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@Mode", "GET_UPSI_EMAIL_CONFIG");
                parameters[1] = new SqlParameter("@CompanyId", objUPSIConfig.CompanyId);

                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_EMAIL_CONFIG", objUPSIConfig.MODULE_DATABASE, parameters);
                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count > 0)
                {
                    List<UPSIEmailConfig> EmailConfigs = new List<UPSIEmailConfig>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        UPSIEmailConfig obj = new UPSIEmailConfig();
                        obj.ConfigId = Convert.ToInt32(dr["CONFIG_ID"]);
                        obj.AuthenticationType = Convert.ToString(dr["AUTHENTICATION_TYPE"]);
                        obj.ClientCertificate = Convert.ToString(dr["OFFICE_CLIENT_SECRET"]);
                        obj.ClientId = Convert.ToString(dr["OFFICE_CLIENT_ID"]);
                        obj.GoogleCertificate = Convert.ToString(dr["GOOGLE_CERTIFICATE"]);
                        obj.GoogleServiceAccount = Convert.ToString(dr["GOOGLE_SERVICE_ACCOUNT_EMAIL"]);
                        obj.Password = Convert.ToString(dr["EMAIL_PASSWORD"]);
                        obj.OutgoingPort = Convert.ToString(dr["OUTGOING_PORT"]);
                        obj.OutgoingProtocol  = Convert.ToString(dr["OUTGOING_PROTOCOL_ADDR"]);
                        obj.IncomingPort = Convert.ToString(dr["INCOMING_PROTOCOL_ADDR"]);
                        obj.IncomingProtocol = Convert.ToString(dr["INCOMING_PORT"]);
                        obj.ProtocolType = Convert.ToString(dr["INCOMING_PROTOCOL"]);
                        obj.SmartType = Convert.ToString(dr["SMART_TYPE"]);
                        obj.TenantId = Convert.ToString(dr["OFFICE_TENANT_ID"]);
                        obj.UPSIEmail = Convert.ToString(dr["UPSI_EMAIL"]);
                        obj.UpsiTypId = Convert.ToInt32(dr["TYP_ID"]);
                        obj.UpsiTypNm = Convert.ToString(dr["TYP_NM"]);
                        EmailConfigs.Add(obj);
                    }
                    configResponse.StatusFl = true;
                    configResponse.Msg = "Data has been fetched successfully !";
                    configResponse.ListEmailConfig = EmailConfigs;
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
                return configResponse;
            }
        }
        public UPSIEmailConfigResponse GetUPSIEmailConfigById(UPSIEmailConfig objUPSIConfig)
        {
            UPSIEmailConfigResponse configResponse = new UPSIEmailConfigResponse();
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@Mode", "GET_UPSI_EMAIL_CONFIG_BY_ID");
                parameters[1] = new SqlParameter("@CompanyId", objUPSIConfig.CompanyId);
                parameters[2] = new SqlParameter("@ConfigId", objUPSIConfig.ConfigId);

                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_EMAIL_CONFIG", objUPSIConfig.MODULE_DATABASE, parameters);
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
                        objUPSIConfig.UpsiTypId = Convert.ToInt32(dr["TYP_ID"]);
                        objUPSIConfig.UpsiTypNm = Convert.ToString(dr["TYP_NM"]);
                        objUPSIConfig.IsSSL = Convert.ToString(dr["SSL_FL"]);
                        objUPSIConfig.AccessToken = Convert.ToString(dr["ACCESS_TOKEN"]);
                        objUPSIConfig.RefreshToken = Convert.ToString(dr["REFRESH_TOKEN"]);
                        objUPSIConfig.AuthenticationEmail = Convert.ToString(dr["CONSENT_EMAIL"]);
                    }
                    configResponse.StatusFl = true;
                    configResponse.Msg = "Data has been fetched successfully !";
                    configResponse.upsiEmailConfig = objUPSIConfig;
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
                return configResponse;
            }
        }
    }
}