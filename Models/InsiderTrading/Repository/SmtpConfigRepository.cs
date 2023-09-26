using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.SessionState;

namespace ProcsDLL.Models.InsiderTrading.Repository
{
    public class SmtpConfigRepository : IRequiresSessionState
    {
        public SmtpConfigResponse AddSmtpConfig(SmtpConfiguration objSmtpConfig)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[23];
                parameters[0] = new SqlParameter("@SMPT_CONFIG_ID", objSmtpConfig.SMPT_CONFIG_ID);
                parameters[1] = new SqlParameter("@DEFAULT_EMAIL", objSmtpConfig.DEFAULT_EMAIL);
                parameters[2] = new SqlParameter("@CONTINUAL_DISCLOSURE_EMAIL", objSmtpConfig.CONTINUAL_DISCLOSURE_EMAIL);
                parameters[3] = new SqlParameter("@SMTP_HOST_NAME", objSmtpConfig.SMTP_HOST_NAME);
                parameters[4] = new SqlParameter("@PORT", objSmtpConfig.PORT);
                parameters[5] = new SqlParameter("@SSL", objSmtpConfig.SSL);
                parameters[6] = new SqlParameter("@SMTP_USER_NAME", objSmtpConfig.SMTP_USER_NAME);
                parameters[7] = new SqlParameter("@PASSWORD", objSmtpConfig.PASSWORD);
                parameters[8] = new SqlParameter("@COMPANY_ID", objSmtpConfig.COMPANY_ID);
                parameters[9] = new SqlParameter("@Mode", "CHECK");
                parameters[10] = new SqlParameter("@ACTION_TYPE", "INSERT");
                parameters[11] = new SqlParameter("@EMPLOYEE_ID", objSmtpConfig.CREATE_BY);
                parameters[12] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[12].Direction = ParameterDirection.Output;
                parameters[13] = new SqlParameter("@USER_DEFAULT_CREDENTIAL", objSmtpConfig.USER_DEFAULT_CREDENTIAL);

                parameters[14] = new SqlParameter("@DEFAULT_EMAIL_OUTGOING", objSmtpConfig.DEFAULT_EMAIL_OUTGOING);
                parameters[15] = new SqlParameter("@CONTINUAL_DISCLOSURE_EMAIL_OUTGOING", objSmtpConfig.CONTINUAL_DISCLOSURE_EMAIL_OUTGOING);
                parameters[16] = new SqlParameter("@SMTP_HOST_NAME_OUTGOING", objSmtpConfig.SMTP_HOST_NAME_OUTGOING);
                parameters[17] = new SqlParameter("@PORT_OUTGOING", objSmtpConfig.PORT_OUTGOING);
                parameters[18] = new SqlParameter("@SSL_OUTGOING", objSmtpConfig.SSL_OUTGOING);
                parameters[19] = new SqlParameter("@SMTP_USER_NAME_OUTGOING", objSmtpConfig.SMTP_USER_NAME_OUTGOING);
                parameters[20] = new SqlParameter("@PASSWORD_OUTGOING", objSmtpConfig.PASSWORD_OUTGOING);
                parameters[21] = new SqlParameter("@USER_DEFAULT_CREDENTIAL_OUTGOING", objSmtpConfig.USER_DEFAULT_CREDENTIAL_OUTGOING);
                parameters[22] = new SqlParameter("@IS_POP", objSmtpConfig.IS_POP);

                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_CONFIG_SMTP", objSmtpConfig.MODULE_DATABASE, parameters);
                var obj = parameters[12].Value;
                SmtpConfigResponse oSmtpConfig = new SmtpConfigResponse();
                if ((Int32)obj == 0)
                {
                    parameters[9] = new SqlParameter("@Mode", "INSERT_UPDATE");

                    SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_CONFIG_SMTP", objSmtpConfig.MODULE_DATABASE, parameters);

                    parameters[9] = new SqlParameter("@Mode", "GET_SMTP_CONFIG_ID_BY_SMTP_CONFIG_DETAIL");
                    var SMTP_CONFIG_ID = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_CONFIG_SMTP", objSmtpConfig.MODULE_DATABASE, parameters);
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
                SmtpConfigResponse oSmtpConfig = new SmtpConfigResponse();
                oSmtpConfig.StatusFl = false;
                oSmtpConfig.Msg = "Processing failed, because of system error !";
                return oSmtpConfig;
            }
        }

        public SmtpConfigResponse UpdateSmtpConfig(SmtpConfiguration objSmtpConfig)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[23];
                parameters[0] = new SqlParameter("@SMPT_CONFIG_ID", objSmtpConfig.SMPT_CONFIG_ID);
                parameters[1] = new SqlParameter("@DEFAULT_EMAIL", objSmtpConfig.DEFAULT_EMAIL);
                parameters[2] = new SqlParameter("@CONTINUAL_DISCLOSURE_EMAIL", objSmtpConfig.CONTINUAL_DISCLOSURE_EMAIL);
                parameters[3] = new SqlParameter("@SMTP_HOST_NAME", objSmtpConfig.SMTP_HOST_NAME);
                parameters[4] = new SqlParameter("@PORT", objSmtpConfig.PORT);
                parameters[5] = new SqlParameter("@SSL", objSmtpConfig.SSL);
                parameters[6] = new SqlParameter("@SMTP_USER_NAME", objSmtpConfig.SMTP_USER_NAME);
                parameters[7] = new SqlParameter("@PASSWORD", objSmtpConfig.PASSWORD);
                parameters[8] = new SqlParameter("@COMPANY_ID", objSmtpConfig.COMPANY_ID);
                parameters[9] = new SqlParameter("@Mode", "CHECK");
                parameters[10] = new SqlParameter("@ACTION_TYPE", "UPDATE");
                parameters[11] = new SqlParameter("@EMPLOYEE_ID", objSmtpConfig.CREATE_BY);
                parameters[12] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[12].Direction = ParameterDirection.Output;
                parameters[13] = new SqlParameter("@USER_DEFAULT_CREDENTIAL", objSmtpConfig.USER_DEFAULT_CREDENTIAL);

                parameters[14] = new SqlParameter("@DEFAULT_EMAIL_OUTGOING", objSmtpConfig.DEFAULT_EMAIL_OUTGOING);
                parameters[15] = new SqlParameter("@CONTINUAL_DISCLOSURE_EMAIL_OUTGOING", objSmtpConfig.CONTINUAL_DISCLOSURE_EMAIL_OUTGOING);
                parameters[16] = new SqlParameter("@SMTP_HOST_NAME_OUTGOING", objSmtpConfig.SMTP_HOST_NAME_OUTGOING);
                parameters[17] = new SqlParameter("@PORT_OUTGOING", objSmtpConfig.PORT_OUTGOING);
                parameters[18] = new SqlParameter("@SSL_OUTGOING", objSmtpConfig.SSL_OUTGOING);
                parameters[19] = new SqlParameter("@SMTP_USER_NAME_OUTGOING", objSmtpConfig.SMTP_USER_NAME_OUTGOING);
                parameters[20] = new SqlParameter("@PASSWORD_OUTGOING", objSmtpConfig.PASSWORD_OUTGOING);
                parameters[21] = new SqlParameter("@USER_DEFAULT_CREDENTIAL_OUTGOING", objSmtpConfig.USER_DEFAULT_CREDENTIAL_OUTGOING);
                parameters[22] = new SqlParameter("@IS_POP", objSmtpConfig.IS_POP);

                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_CONFIG_SMTP", objSmtpConfig.MODULE_DATABASE, parameters);
                var obj = parameters[12].Value;
                SmtpConfigResponse oSmtpConfig = new SmtpConfigResponse();
                if ((Int32)obj == 0)
                {
                    parameters[9] = new SqlParameter("@Mode", "INSERT_UPDATE");

                    SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_CONFIG_SMTP", objSmtpConfig.MODULE_DATABASE, parameters);

                    parameters[9] = new SqlParameter("@Mode", "GET_SMTP_CONFIG_ID_BY_SMTP_CONFIG_DETAIL");
                    var SMTP_CONFIG_ID = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_CONFIG_SMTP", objSmtpConfig.MODULE_DATABASE, parameters);
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
                SmtpConfigResponse oSmtpConfig = new SmtpConfigResponse();
                oSmtpConfig.StatusFl = false;
                oSmtpConfig.Msg = "Processing failed, because of system error !";
                return oSmtpConfig;
            }
        }

        public SmtpConfigResponse DeleteSmtpConfig(SmtpConfiguration objSmtpConfig)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@SMPT_CONFIG_ID", objSmtpConfig.SMPT_CONFIG_ID);
                parameters[1] = new SqlParameter("@COMPANY_ID", objSmtpConfig.COMPANY_ID);
                parameters[2] = new SqlParameter("@Mode", "Delete_Smtp_Config");
                parameters[3] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[3].Direction = ParameterDirection.Output;
                SmtpConfigResponse oSmtpConfig = new SmtpConfigResponse();

                var result = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_CONFIG_SMTP", objSmtpConfig.MODULE_DATABASE, parameters);
                oSmtpConfig.StatusFl = true;
                oSmtpConfig.Msg = "Data has been deleted successfully !";
                oSmtpConfig.SmtpConfig = objSmtpConfig;
                return oSmtpConfig;
            }
            catch (Exception ex)
            {
                SmtpConfigResponse oSmtpConfig = new SmtpConfigResponse();
                oSmtpConfig.StatusFl = false;
                oSmtpConfig.Msg = "Processing failed, because of system error !";
                return oSmtpConfig;
            }
        }

        public SmtpConfigResponse GetSmtpConfigList(SmtpConfiguration objSmtpConfig)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@Mode", "GET_Smtp_Config_List");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objSmtpConfig.COMPANY_ID);

                SmtpConfigResponse oSmtpConfig = new SmtpConfigResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_CONFIG_SMTP", objSmtpConfig.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        SmtpConfiguration obj = new SmtpConfiguration();
                        obj.SMPT_CONFIG_ID = Convert.ToInt32(rdr.GetValue(0));
                        obj.DEFAULT_EMAIL = Convert.ToString(rdr.GetValue(1));
                        obj.CONTINUAL_DISCLOSURE_EMAIL = Convert.ToString(rdr.GetValue(2));
                        obj.SMTP_HOST_NAME = Convert.ToString(rdr.GetValue(3));
                        obj.PORT = Convert.ToString(rdr.GetValue(4));
                        obj.SSL = Convert.ToString(rdr.GetValue(5));
                        obj.SMTP_USER_NAME = Convert.ToString(rdr.GetValue(6));
                        obj.PASSWORD = Convert.ToString(rdr.GetValue(7));
                        obj.COMPANY_ID = Convert.ToInt32(rdr.GetValue(8));
                        obj.CREATE_BY = Convert.ToString(rdr.GetValue(9));
                        obj.CREATED_ON = Convert.ToString(rdr.GetValue(10));
                        obj.USER_DEFAULT_CREDENTIAL = Convert.ToString(rdr.GetValue(11));

                        obj.DEFAULT_EMAIL_OUTGOING = Convert.ToString(rdr.GetValue(12));
                        obj.CONTINUAL_DISCLOSURE_EMAIL_OUTGOING = Convert.ToString(rdr.GetValue(13));
                        obj.SMTP_HOST_NAME_OUTGOING = Convert.ToString(rdr.GetValue(14));
                        obj.PORT_OUTGOING = Convert.ToString(rdr.GetValue(15));
                        obj.SSL_OUTGOING = Convert.ToString(rdr.GetValue(16));
                        obj.SMTP_USER_NAME_OUTGOING = Convert.ToString(rdr.GetValue(17));
                        obj.PASSWORD_OUTGOING = Convert.ToString(rdr.GetValue(18));
                        obj.USER_DEFAULT_CREDENTIAL_OUTGOING = Convert.ToString(rdr.GetValue(19));
                        obj.IS_POP = !String.IsNullOrEmpty(Convert.ToString(rdr.GetValue(20))) ? Convert.ToString(rdr.GetValue(20)) : String.Empty;
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
                SmtpConfigResponse oSmtpConfig = new SmtpConfigResponse();
                oSmtpConfig.StatusFl = false;
                oSmtpConfig.Msg = "Processing failed, because of system error !";
                return oSmtpConfig;
            }
        }
    }
}