using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using MailKit.Security;
using MailKit.Net.Imap;
using MailKit.Net.Pop3;
using Google.Apis.Auth.OAuth2;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Microsoft.Identity.Client;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Net.Security;
namespace ProcsDLL.Models.Infrastructure
{
    public class EmailSender
    {
        private static String connectionString = SQLHelper.GetConnString();
        private static String connectionStringIT = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionStringIT"]), true);
        private static String dataBaseName = SQLHelper.GetDBName();
        public static void SendRequestMail(
             string strFrom, string strTo, string strSubject, string strMsg, string smtpHostName,
             bool ssl, string smtpUserName, string password, bool userDefaultCredential, Int32 port, String loginName, Int32 sCmpnId)
        {

            Int64 lLogId = 0;
            string strEmailAction = "Raise Request";
            try
            {
                DataTable dtEmails = new DataTable();

                using (SqlConnection sCon = new SqlConnection(connectionStringIT))
                {
                    SqlCommand sCmd = new SqlCommand();
                    sCmd.Connection = sCon;
                    sCon.Open();

                    string _sql = "SELECT COMPANY_ID,AUTHENTICATION_TYPE,SMART_TYPE,OFFICE_CLIENT_ID,OFFICE_CLIENT_SECRET,OFFICE_TENANT_ID," +
                        "GOOGLE_CERTIFICATE,GOOGLE_SERVICE_ACCOUNT_EMAIL,PIT_EMAIL,INCOMING_PROTOCOL,INCOMING_PROTOCOL_ADDR,INCOMING_PORT," +
                        "OUTGOING_PROTOCOL_ADDR,OUTGOING_PORT,SSL_FL,EMAIL_PASSWORD,ACCESS_TOKEN,REFRESH_TOKEN,EXPIRE_AT,ISNULL(CONSENT_EMAIL,'') AS CONSENT_EMAIL," +
                        "ISNULL(DISPLAY_NAME,'') AS DISPLAY_NAME FROM PROCS_INSIDER_EMAIL_CONFIG(NOLOCK) WHERE COMPANY_ID=" + sCmpnId;
                    sCmd.CommandType = CommandType.Text;
                    sCmd.CommandText = _sql;

                    DataSet dsEmails = new DataSet();
                    SqlDataAdapter daEmails = new SqlDataAdapter(sCmd);
                    daEmails.Fill(dsEmails);
                    dtEmails = dsEmails.Tables[0];
                }

                if (dtEmails.Rows.Count > 0)
                {
                    foreach (DataRow drEmail in dtEmails.Rows)
                    {
                        string sAuthType = Convert.ToString(drEmail["AUTHENTICATION_TYPE"]);
                        string sEmailType = Convert.ToString(drEmail["SMART_TYPE"]);

                        string sClientId = Convert.ToString(drEmail["OFFICE_CLIENT_ID"]);
                        string sClientSecret = Convert.ToString(drEmail["OFFICE_CLIENT_SECRET"]);
                        string sTenantId = Convert.ToString(drEmail["OFFICE_TENANT_ID"]);

                        string sGCertificate = Convert.ToString(drEmail["GOOGLE_CERTIFICATE"]);
                        string sGServiceEmail = Convert.ToString(drEmail["GOOGLE_SERVICE_ACCOUNT_EMAIL"]);

                        string sUpsiEmail = Convert.ToString(drEmail["PIT_EMAIL"]);
                        string sUpsiEmailPwd = Convert.ToString(drEmail["EMAIL_PASSWORD"]);

                        string sOutgoingProtocol = Convert.ToString(drEmail["OUTGOING_PROTOCOL_ADDR"]);
                        string sOutgoingPort = Convert.ToString(drEmail["OUTGOING_PORT"]);

                        string sProtocol = Convert.ToString(drEmail["INCOMING_PROTOCOL"]);
                        string sIncomingProtocol = Convert.ToString(drEmail["INCOMING_PROTOCOL_ADDR"]);
                        string sIncomingPort = Convert.ToString(drEmail["INCOMING_PORT"]);

                        string sAccessToken = Convert.ToString(drEmail["ACCESS_TOKEN"]);
                        string sRefreshToken = Convert.ToString(drEmail["REFRESH_TOKEN"]);
                        string sExpiryAt = Convert.ToString(drEmail["EXPIRE_AT"]);
                        string sDataElementId = "";
                        using (SqlConnection sCon = new SqlConnection(connectionStringIT))
                        {
                            SqlCommand sCmd = new SqlCommand();
                            sCmd.Connection = sCon;
                            sCon.Open();

                            string _sql = "INSERT INTO PROCS_INSIDER_EMAIL_LOG(" +
                                "EMAIL_TO,EMAIL_SUBJECT,EMAIL_MSG,EMAIL_ACTION,EMAIL_DT,DATA_ELEMENT_ID,EMAIL_STATUS,USER_LOGIN" +
                            ") SELECT '" + strTo + "','" + strSubject + "','" + strMsg.Replace("'", "''") + "','" + strEmailAction + "'," +
                            "GETDATE(),'" + sDataElementId + "','Pending','" + loginName + "';SELECT IDENT_CURRENT('PROCS_INSIDER_EMAIL_LOG')";
                            sCmd.CommandType = CommandType.Text;
                            sCmd.CommandText = _sql;
                            Int32 iLogId = Convert.ToInt32(sCmd.ExecuteScalar());
                            lLogId = iLogId;

                        }
                        if (sAuthType == "Basic")
                        {
                            fnSendBasicAuth(strFrom, strTo, strSubject, strMsg, null, strEmailAction, drEmail, null);
                        }
                        else
                        {
                            if (sEmailType == "Google")
                            {
                                fnSendGoogleOAuth(strFrom, strTo, strSubject, strMsg, null, strEmailAction, drEmail, null);
                            }
                            else
                            {
                                fnSendOffice365OAuth(strFrom, strTo, strSubject, strMsg, null, strEmailAction, drEmail, null);
                            }
                        }
                        using (SqlConnection sCon = new SqlConnection(connectionStringIT))
                        {
                            SqlCommand sCmd = new SqlCommand();
                            sCmd.Connection = sCon;
                            sCon.Open();

                            string _sql = "UPDATE PROCS_INSIDER_EMAIL_LOG SET EMAIL_STATUS='Success' WHERE LOG_ID=" + lLogId.ToString();
                            sCmd.CommandType = CommandType.Text;
                            sCmd.CommandText = _sql;
                            sCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                using (SqlConnection sCon = new SqlConnection(connectionStringIT))
                {
                    SqlCommand sCmd = new SqlCommand();
                    sCmd.Connection = sCon;
                    sCon.Open();

                    string _sql = "UPDATE PROCS_INSIDER_EMAIL_LOG SET EMAIL_STATUS='Failed'," +
                        "ERR_MSG='" + ex.Message.Replace("'", "''") + "' WHERE LOG_ID=" + lLogId.ToString();
                    sCmd.CommandType = CommandType.Text;
                    sCmd.CommandText = _sql;
                    sCmd.ExecuteNonQuery();
                }
            }
        }
        public static void SendMail(
            string strTo, string strSubject, string strMsg, List<string> listAttachment, string strEmailAction,
            string sCmpnId, string strCC = "", string sDataElementId = "", string sUserLogin = ""
        )
        {
            Int64 lLogId = 0;
            try
            {
                DataTable dtEmails = new DataTable();
                string _sqlX;
                using (SqlConnection sCon = new SqlConnection(connectionStringIT))
                {
                    SqlCommand sCmd = new SqlCommand();
                    sCmd.Connection = sCon;
                    sCon.Open();

                    string _sql = "SELECT COMPANY_ID,AUTHENTICATION_TYPE,SMART_TYPE,OFFICE_CLIENT_ID,OFFICE_CLIENT_SECRET,OFFICE_TENANT_ID," +
                        "GOOGLE_CERTIFICATE,GOOGLE_SERVICE_ACCOUNT_EMAIL,PIT_EMAIL,INCOMING_PROTOCOL,INCOMING_PROTOCOL_ADDR,INCOMING_PORT," +
                        "OUTGOING_PROTOCOL_ADDR,OUTGOING_PORT,SSL_FL,EMAIL_PASSWORD,ACCESS_TOKEN,REFRESH_TOKEN,EXPIRE_AT,ISNULL(CONSENT_EMAIL,'') AS CONSENT_EMAIL," +
                        "ISNULL(DISPLAY_NAME,'') AS DISPLAY_NAME FROM PROCS_INSIDER_EMAIL_CONFIG(NOLOCK) WHERE COMPANY_ID=" + sCmpnId;
                    _sqlX = _sql;
                    sCmd.CommandType = CommandType.Text;
                    sCmd.CommandText = _sql;

                    DataSet dsEmails = new DataSet();
                    SqlDataAdapter daEmails = new SqlDataAdapter(sCmd);
                    daEmails.Fill(dsEmails);
                    dtEmails = dsEmails.Tables[0];
                }

                if (dtEmails.Rows.Count > 0)
                {
                    foreach (DataRow drEmail in dtEmails.Rows)
                    {
                        string sAuthType = Convert.ToString(drEmail["AUTHENTICATION_TYPE"]);
                        string sEmailType = Convert.ToString(drEmail["SMART_TYPE"]);

                        string sClientId = Convert.ToString(drEmail["OFFICE_CLIENT_ID"]);
                        string sClientSecret = Convert.ToString(drEmail["OFFICE_CLIENT_SECRET"]);
                        string sTenantId = Convert.ToString(drEmail["OFFICE_TENANT_ID"]);

                        string sGCertificate = Convert.ToString(drEmail["GOOGLE_CERTIFICATE"]);
                        string sGServiceEmail = Convert.ToString(drEmail["GOOGLE_SERVICE_ACCOUNT_EMAIL"]);

                        string sUpsiEmail = Convert.ToString(drEmail["PIT_EMAIL"]);
                        string sUpsiEmailPwd = Convert.ToString(drEmail["EMAIL_PASSWORD"]);

                        string sOutgoingProtocol = Convert.ToString(drEmail["OUTGOING_PROTOCOL_ADDR"]);
                        string sOutgoingPort = Convert.ToString(drEmail["OUTGOING_PORT"]);

                        string sProtocol = Convert.ToString(drEmail["INCOMING_PROTOCOL"]);
                        string sIncomingProtocol = Convert.ToString(drEmail["INCOMING_PROTOCOL_ADDR"]);
                        string sIncomingPort = Convert.ToString(drEmail["INCOMING_PORT"]);

                        string sAccessToken = Convert.ToString(drEmail["ACCESS_TOKEN"]);
                        string sRefreshToken = Convert.ToString(drEmail["REFRESH_TOKEN"]);
                        string sExpiryAt = Convert.ToString(drEmail["EXPIRE_AT"]);

                        using (SqlConnection sCon = new SqlConnection(connectionStringIT))
                        {
                            SqlCommand sCmd = new SqlCommand();
                            sCmd.Connection = sCon;
                            sCon.Open();

                            //strMsg += "sOutgoingProtocol="+ sOutgoingProtocol;
                            //strMsg += "sOutgoingPort=" + sOutgoingPort;
                            //strMsg += "_sql=" + _sqlX;

                            string _sql = "INSERT INTO PROCS_INSIDER_EMAIL_LOG(" +
                                "EMAIL_TO,EMAIL_SUBJECT,EMAIL_MSG,EMAIL_ACTION,EMAIL_DT,DATA_ELEMENT_ID,EMAIL_STATUS,USER_LOGIN" +
                            ") SELECT '" + strTo + "','" + strSubject + "','" + strMsg.Replace("'", "''") + "','" + strEmailAction + "'," +
                            "GETDATE(),'" + sDataElementId + "','Pending','" + sUserLogin + "';SELECT IDENT_CURRENT('PROCS_INSIDER_EMAIL_LOG')";
                            sCmd.CommandType = CommandType.Text;
                            sCmd.CommandText = _sql;
                            Int32 iLogId = Convert.ToInt32(sCmd.ExecuteScalar());
                            lLogId = iLogId;
                            if (listAttachment != null)
                            {
                                foreach (string str in listAttachment)
                                {
                                    _sql = "INSERT INTO PROCS_INSIDER_EMAIL_LOG_ATTACHMENT(LOG_ID,EMAIL_ATTACHMENT) SELECT " + iLogId.ToString() + "," +
                                        "'" + str + "'";
                                    sCmd.CommandType = CommandType.Text;
                                    sCmd.CommandText = _sql;
                                    sCmd.ExecuteNonQuery();
                                }
                            }
                        }
                        if (sAuthType == "Basic")
                        {
                            fnSendBasicAuth(sUpsiEmail, strTo, strSubject, strMsg, listAttachment, strEmailAction, drEmail, strCC);
                        }
                        else
                        {
                            if (sEmailType == "Google")
                            {
                                fnSendGoogleOAuth(sUpsiEmail, strTo, strSubject, strMsg, listAttachment, strEmailAction, drEmail, strCC);
                            }
                            else
                            {
                                fnSendOffice365OAuth(sUpsiEmail, strTo, strSubject, strMsg, listAttachment, strEmailAction, drEmail, strCC);
                            }
                        }
                        using (SqlConnection sCon = new SqlConnection(connectionStringIT))
                        {
                            SqlCommand sCmd = new SqlCommand();
                            sCmd.Connection = sCon;
                            sCon.Open();

                            string _sql = "UPDATE PROCS_INSIDER_EMAIL_LOG SET EMAIL_STATUS='Success' WHERE LOG_ID=" + lLogId.ToString();
                            sCmd.CommandType = CommandType.Text;
                            sCmd.CommandText = _sql;
                            sCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                using (SqlConnection sCon = new SqlConnection(connectionStringIT))
                {
                    SqlCommand sCmd = new SqlCommand();
                    sCmd.Connection = sCon;
                    sCon.Open();

                    string _sql = "UPDATE PROCS_INSIDER_EMAIL_LOG SET EMAIL_STATUS='Failed'," +
                        "ERR_MSG='" + ex.Message.Replace("'", "''") + "' WHERE LOG_ID=" + lLogId.ToString();
                    sCmd.CommandType = CommandType.Text;
                    sCmd.CommandText = _sql;
                    sCmd.ExecuteNonQuery();
                }
            }
        }
        static void fnSendBasicAuth(
            string strFrom, string strTo, string strSubject, string strMsg, List<string> listAttachment, string strEmailAction,
            DataRow drEmail, string strCC = ""
        )
        {
            var gsuiteUser = Convert.ToString(drEmail["PIT_EMAIL"]);
            var gsuitePwd = Convert.ToString(drEmail["EMAIL_PASSWORD"]);
            string displayNm = Convert.ToString(drEmail["DISPLAY_NAME"]);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(displayNm, gsuiteUser));
            message.To.Add(new MailboxAddress("", strTo));

            if (!string.IsNullOrEmpty(strCC))
            {
                message.Cc.Add(new MailboxAddress("", strCC));
            }
            message.Subject = strSubject;

            var html = new TextPart("html");
            html.SetText(Encoding.UTF8, strMsg);

            var multipart = new Multipart("mixed");
            multipart.Add(html);

            if (listAttachment != null)
            {
                foreach (string sAttachment in listAttachment)
                {
                    var attachment = new MimePart()
                    {
                        Content = new MimeContent(File.OpenRead(sAttachment), ContentEncoding.Default),
                        ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                        ContentTransferEncoding = ContentEncoding.Base64,
                        FileName = Path.GetFileName(sAttachment)
                    };
                    multipart.Add(attachment);
                }
            }
            message.Body = multipart;

            string sChkCertification = Convert.ToString(ConfigurationManager.AppSettings["CertificateValidation"]);
            string sIsSSL = Convert.ToString(ConfigurationManager.AppSettings["IsSSL"]);
            using (var client = new SmtpClient())
            {
                if (sChkCertification == "No")
                {
                    client.CheckCertificateRevocation = false;
                }
                else
                {
                    client.ServerCertificateValidationCallback = MyServerCertificateValidationCallback;
                }

                if (sIsSSL == "Auto")
                {
                    //strMsg += " in Auto=";
                    client.Connect(Convert.ToString(drEmail["OUTGOING_PROTOCOL_ADDR"]), Convert.ToInt32(drEmail["OUTGOING_PORT"]),
                        MailKit.Security.SecureSocketOptions.Auto);
                    client.Authenticate(gsuiteUser, gsuitePwd);
                }
                else if (sIsSSL == "None")
                {
                    //strMsg += " in None=";
                    client.Connect(Convert.ToString(drEmail["OUTGOING_PROTOCOL_ADDR"]), Convert.ToInt32(drEmail["OUTGOING_PORT"]),
                        MailKit.Security.SecureSocketOptions.None);
                }
                else if (sIsSSL == "SslOnConnect")
                {
                    //strMsg += " in SslOnConnect=";
                    client.Connect(Convert.ToString(drEmail["OUTGOING_PROTOCOL_ADDR"]), Convert.ToInt32(drEmail["OUTGOING_PORT"]),
                        MailKit.Security.SecureSocketOptions.SslOnConnect);
                    client.Authenticate(gsuiteUser, gsuitePwd);
                }
                else if (sIsSSL == "StartTls")
                {
                    //strMsg += " in StartTls=";
                    client.Connect(Convert.ToString(drEmail["OUTGOING_PROTOCOL_ADDR"]), Convert.ToInt32(drEmail["OUTGOING_PORT"]),
                        MailKit.Security.SecureSocketOptions.StartTls);
                    client.Authenticate(gsuiteUser, gsuitePwd);
                }
                else if (sIsSSL == "StartTlsWhenAvailable")
                {
                    //strMsg += " in StartTlsWhenAvailable=";
                    client.Connect(Convert.ToString(drEmail["OUTGOING_PROTOCOL_ADDR"]), Convert.ToInt32(drEmail["OUTGOING_PORT"]),
                        MailKit.Security.SecureSocketOptions.StartTlsWhenAvailable);
                    client.Authenticate(gsuiteUser, gsuitePwd);
                }
                else
                {
                    //strMsg += " in none=";
                    client.Connect(Convert.ToString(drEmail["OUTGOING_PROTOCOL_ADDR"]), Convert.ToInt32(drEmail["OUTGOING_PORT"]), true);
                    client.Authenticate(gsuiteUser, gsuitePwd);
                }
                client.Send(message);
                client.Disconnect(true);
            }
        }
        static void fnSendGoogleOAuth(
            string strFrom, string strTo, string strSubject, string strMsg, List<string> listAttachment, string strEmailAction,
            DataRow drEmail, string strCC = ""
        )
        {
            string serviceAccount = Convert.ToString(drEmail["GOOGLE_SERVICE_ACCOUNT_EMAIL"]);
            string displayNm = Convert.ToString(drEmail["DISPLAY_NAME"]);
            var certificate = new X509Certificate2(Convert.ToString(drEmail["GOOGLE_CERTIFICATE"]), "notasecret", X509KeyStorageFlags.Exportable);
            var gsuiteUser = Convert.ToString(drEmail["PIT_EMAIL"]);
            var serviceAccountCredentialInitializer = new ServiceAccountCredential.Initializer(serviceAccount)
            {
                User = gsuiteUser,
                Scopes = new[] { "https://mail.google.com/" }
            }.FromCertificate(certificate);

            var credential = new ServiceAccountCredential(serviceAccountCredentialInitializer);
            if (!credential.RequestAccessTokenAsync(System.Threading.CancellationToken.None).Result)
                throw new InvalidOperationException("Access token failed.");

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(displayNm, gsuiteUser));
            message.To.Add(new MailboxAddress("", strTo));
            if (!string.IsNullOrEmpty(strCC))
            {
                message.Cc.Add(new MailboxAddress("", strCC));
            }
            message.Subject = strSubject;

            var html = new TextPart("html");
            html.SetText(Encoding.UTF8, strMsg);

            var multipart = new Multipart("mixed");
            multipart.Add(html);

            if (listAttachment != null)
            {
                foreach (string sAttachment in listAttachment)
                {
                    var attachment = new MimePart()
                    {
                        Content = new MimeContent(File.OpenRead(sAttachment), ContentEncoding.Default),
                        ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                        ContentTransferEncoding = ContentEncoding.Base64,
                        FileName = Path.GetFileName(sAttachment)
                    };
                    multipart.Add(attachment);
                }
            }
            message.Body = multipart;

            string sChkCertification = Convert.ToString(ConfigurationManager.AppSettings["CertificateValidation"]);
            string sIsSSL = Convert.ToString(ConfigurationManager.AppSettings["IsSSL"]);
            using (var client = new SmtpClient())
            {
                string CheckCertificateRevocation = Convert.ToString(ConfigurationManager.AppSettings["CheckCertificateRevocation"]);
                if (CheckCertificateRevocation == "No")
                {
                    client.CheckCertificateRevocation = false;
                }

                if (sIsSSL == "Auto")
                {
                    client.Connect(Convert.ToString(drEmail["OUTGOING_PROTOCOL_ADDR"]), Convert.ToInt32(drEmail["OUTGOING_PORT"]),
                        MailKit.Security.SecureSocketOptions.Auto);
                }
                else if (sIsSSL == "None")
                {
                    client.Connect(Convert.ToString(drEmail["OUTGOING_PROTOCOL_ADDR"]), Convert.ToInt32(drEmail["OUTGOING_PORT"]),
                        MailKit.Security.SecureSocketOptions.None);
                }
                else if (sIsSSL == "SslOnConnect")
                {
                    client.Connect(Convert.ToString(drEmail["OUTGOING_PROTOCOL_ADDR"]), Convert.ToInt32(drEmail["OUTGOING_PORT"]),
                        MailKit.Security.SecureSocketOptions.SslOnConnect);
                }
                else if (sIsSSL == "StartTls")
                {
                    client.Connect(Convert.ToString(drEmail["OUTGOING_PROTOCOL_ADDR"]), Convert.ToInt32(drEmail["OUTGOING_PORT"]),
                        MailKit.Security.SecureSocketOptions.StartTls);
                }
                else if (sIsSSL == "StartTlsWhenAvailable")
                {
                    client.Connect(Convert.ToString(drEmail["OUTGOING_PROTOCOL_ADDR"]), Convert.ToInt32(drEmail["OUTGOING_PORT"]),
                        MailKit.Security.SecureSocketOptions.StartTlsWhenAvailable);
                }
                else
                {
                    client.Connect(Convert.ToString(drEmail["OUTGOING_PROTOCOL_ADDR"]), Convert.ToInt32(drEmail["OUTGOING_PORT"]), true);
                }

                //client.Connect(Convert.ToString(drEmail["OUTGOING_PROTOCOL_ADDR"]), Convert.ToInt32(drEmail["OUTGOING_PORT"]), MailKit.Security.SecureSocketOptions.StartTls);
                var oauth2 = new SaslMechanismOAuth2(gsuiteUser, credential.Token.AccessToken);
                client.Authenticate(oauth2);
                client.Send(message);
                client.Disconnect(true);
            }
        }
        static void fnSendOffice365OAuth(
            string strFrom, string strTo, string strSubject, string strMsg, List<string> listAttachment, string strEmailAction,
            DataRow drEmail, string strCC = ""
        )
        {
            string client_id = Convert.ToString(drEmail["OFFICE_CLIENT_ID"]);
            string client_secret = Convert.ToString(drEmail["OFFICE_CLIENT_SECRET"]);
            string tenant = Convert.ToString(drEmail["OFFICE_TENANT_ID"]);
            string sToken = Convert.ToString(drEmail["ACCESS_TOKEN"]);
            string sRefToken = Convert.ToString(drEmail["REFRESH_TOKEN"]);
            Int32 iExpiresIn = 0;

            if (Convert.ToDateTime(drEmail["EXPIRE_AT"]) < DateTime.Now.AddMinutes(2))
            {
                string requestData = string.Format("client_id={0}&grant_type=refresh_token&refresh_token={1}&scope={2}&client_secret={3}", client_id,
                    sRefToken, "openid offline_access https://outlook.office.com/IMAP.AccessAsUser.All https://outlook.office.com/SMTP.Send", client_secret);
                string tokenUri = string.Format("https://login.microsoftonline.com/{0}/oauth2/v2.0/token", tenant);
                string responseText = GetOfficeOAuthToken(tokenUri, requestData);
                dynamic objInput = Newtonsoft.Json.JsonConvert.DeserializeObject(responseText);

                var x = (JObject)objInput;
                foreach (JToken tok in x.Children())
                {
                    if (tok is JProperty)
                    {
                        var prop = tok as JProperty;
                        var pName = prop.Name;

                        if (pName == "access_token")
                        {
                            sToken = prop.Value.ToString();
                        }
                        if (pName == "refresh_token")
                        {
                            sRefToken = prop.Value.ToString();
                        }
                        if (pName == "expires_in")
                        {
                            iExpiresIn = Convert.ToInt32(prop.Value.ToString());
                        }
                    }
                }
                //string sConStr = Convert.ToString(ConfigurationSettings.AppSettings["ConnectionString"]);
                using (SqlConnection sCon = new SqlConnection(connectionStringIT))
                {
                    SqlCommand sCmd = new SqlCommand();
                    sCmd.Connection = sCon;
                    sCon.Open();

                    string _sql = "UPDATE PROCS_INSIDER_EMAIL_CONFIG SET ACCESS_TOKEN='" + sToken + "',REFRESH_TOKEN='" + sRefToken + "'," +
                        "EXPIRE_AT=DATEADD(SECOND," + Convert.ToString(iExpiresIn) + ",GETDATE()) WHERE AUTHENTICATION_TYPE='Smart' " +
                        "AND SMART_TYPE='Office 365' AND COMPANY_ID=" + Convert.ToString(drEmail["COMPANY_ID"]);
                    sCmd.CommandType = CommandType.Text;
                    sCmd.CommandText = _sql;
                    sCmd.ExecuteNonQuery();
                }
            }
            var officeUser = Convert.ToString(drEmail["PIT_EMAIL"]);
            var consentEmail = Convert.ToString(drEmail["CONSENT_EMAIL"]);
            var displayNm = Convert.ToString(drEmail["DISPLAY_NAME"]);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(displayNm, officeUser));
            message.To.Add(new MailboxAddress("", strTo));
            if (!string.IsNullOrEmpty(strCC))
            {
                message.Cc.Add(new MailboxAddress("", strCC));
            }
            message.Subject = strSubject;

            var html = new TextPart("html");
            html.SetText(Encoding.UTF8, strMsg);

            var multipart = new Multipart("mixed");
            multipart.Add(html);

            if (listAttachment != null)
            {
                foreach (string sAttachment in listAttachment)
                {
                    var attachment = new MimePart()
                    {
                        Content = new MimeContent(File.OpenRead(sAttachment), ContentEncoding.Default),
                        ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                        ContentTransferEncoding = ContentEncoding.Base64,
                        FileName = Path.GetFileName(sAttachment)
                    };
                    multipart.Add(attachment);
                }
            }
            message.Body = multipart;

            string officeSMTP = Convert.ToString(drEmail["OUTGOING_PROTOCOL_ADDR"]);
            int officePort = Convert.ToInt32(drEmail["OUTGOING_PORT"]);
            string sChkCertification = Convert.ToString(ConfigurationManager.AppSettings["CertificateValidation"]);
            string sIsSSL = Convert.ToString(ConfigurationManager.AppSettings["IsSSL"]);

            using (var client = new SmtpClient())
            {
                if (sChkCertification == "No")
                {
                    client.CheckCertificateRevocation = false;
                }

                if (sIsSSL == "Auto")
                {
                    client.Connect(Convert.ToString(drEmail["OUTGOING_PROTOCOL_ADDR"]), Convert.ToInt32(drEmail["OUTGOING_PORT"]),
                        MailKit.Security.SecureSocketOptions.Auto);
                }
                else if (sIsSSL == "None")
                {
                    client.Connect(Convert.ToString(drEmail["OUTGOING_PROTOCOL_ADDR"]), Convert.ToInt32(drEmail["OUTGOING_PORT"]),
                        MailKit.Security.SecureSocketOptions.None);
                }
                else if (sIsSSL == "SslOnConnect")
                {
                    client.Connect(Convert.ToString(drEmail["OUTGOING_PROTOCOL_ADDR"]), Convert.ToInt32(drEmail["OUTGOING_PORT"]),
                        MailKit.Security.SecureSocketOptions.SslOnConnect);
                }
                else if (sIsSSL == "StartTls")
                {
                    client.Connect(Convert.ToString(drEmail["OUTGOING_PROTOCOL_ADDR"]), Convert.ToInt32(drEmail["OUTGOING_PORT"]),
                        MailKit.Security.SecureSocketOptions.StartTls);
                }
                else if (sIsSSL == "StartTlsWhenAvailable")
                {
                    client.Connect(Convert.ToString(drEmail["OUTGOING_PROTOCOL_ADDR"]), Convert.ToInt32(drEmail["OUTGOING_PORT"]),
                        MailKit.Security.SecureSocketOptions.StartTlsWhenAvailable);
                }
                else
                {
                    client.Connect(Convert.ToString(drEmail["OUTGOING_PROTOCOL_ADDR"]), Convert.ToInt32(drEmail["OUTGOING_PORT"]), true);
                }
                //client.Connect(officeSMTP, officePort, MailKit.Security.SecureSocketOptions.StartTls);
                var oauth2 = new SaslMechanismOAuth2(consentEmail, sToken);
                client.Authenticate(oauth2);
                client.Send(message);
                client.Disconnect(true);
            }
        }
        public static string GetOfficeOAuthToken(string uri, string requestData)
        {
            //string sFile = HttpContext.Current.Server.MapPath("~/InsiderTrading/emailAttachment/EmailLog.txt");
            //using (StreamWriter writer = new StreamWriter(sFile, true))
            //{
            //    writer.WriteLine("In EmailSender");
            //    writer.WriteLine(uri);
            //    writer.WriteLine(requestData);
            //    writer.WriteLine("----------------------------------------------------------------------------------------------------------------------");
            //}
            HttpWebRequest httpRequest = WebRequest.Create(uri) as HttpWebRequest;
            httpRequest.Method = "POST";
            //httpRequest.Headers.Add("Origin", "");
            httpRequest.ContentType = "application/x-www-form-urlencoded";

            using (Stream requestStream = httpRequest.GetRequestStream())
            {
                byte[] requestBuffer = Encoding.UTF8.GetBytes(requestData);
                requestStream.Write(requestBuffer, 0, requestBuffer.Length);
                requestStream.Close();
            }
            try
            {
                HttpWebResponse httpResponse = httpRequest.GetResponse() as HttpWebResponse;
                using (StreamReader reader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    string responseText = reader.ReadToEnd();
                    Console.WriteLine(responseText);
                    return responseText;
                }
            }
            catch (WebException ex)
            {
                throw ex;
                //string sFileX = HttpContext.Current.Server.MapPath("~/InsiderTrading/emailAttachment/EmailLog.txt");
                //using (StreamWriter writer = new StreamWriter(sFileX, true))
                //{
                //    writer.WriteLine("In EmailSender Exception");
                //    writer.WriteLine(ex.Message);
                //    writer.WriteLine("----------------------------------------------------------------------------------------------------------------------");
                //    writer.WriteLine(ex.InnerException);
                //    writer.WriteLine("----------------------------------------------------------------------------------------------------------------------");
                //}
                //if (ex.Response == null)
                //{
                //    using (StreamWriter writer = new StreamWriter(sFileX, true))
                //    {
                //        writer.WriteLine("In EmailSender Exception");
                //        writer.WriteLine(ex.Message);
                //        writer.WriteLine("----------------------------------------------------------------------------------------------------------------------");
                //        writer.WriteLine(ex.InnerException);
                //        writer.WriteLine("----------------------------------------------------------------------------------------------------------------------");
                //    }
                //    throw ex;
                //}
                //else
                //{
                //    using (StreamReader reader = new StreamReader(ex.Response.GetResponseStream()))
                //    {
                //        string responseText = reader.ReadToEnd();
                //        using (StreamWriter writer = new StreamWriter(sFileX, true))
                //        {
                //            writer.WriteLine("In EmailSender Exception response not null");
                //            writer.WriteLine(responseText);
                //            writer.WriteLine("----------------------------------------------------------------------------------------------------------------------");
                //        }
                //        return responseText;
                //    }
                //}
            }
        }
        static bool MyServerCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}