using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ProcsDLL.Models.InsiderTrading.Repository
{
    public class EmailUpdationRepository
    {
        private static String connectionString = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionStringIT"]), true);
        public EmailUpdationResponse ListEmail(EmailUpdations objemail)
        {
            try
            {

                EmailUpdationResponse res = new EmailUpdationResponse();


                SqlParameter[] parameters = new SqlParameter[5];
                parameters[0] = new SqlParameter("@MODE", "GET_ALL_EMAIL_BY_USER_LOGIN");
                parameters[1] = new SqlParameter("@EMPLOYEE_ID", objemail.CREATE_BY);
                parameters[2] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[2].Direction = ParameterDirection.Output;
                parameters[3] = new SqlParameter("@COMPANY_ID", objemail.COMPANY_ID);
                parameters[4] = new SqlParameter("@ADMIN_DATABASE", CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["AdminDB"]), true));

                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_EMAIL_UPDATION", objemail.MODULE_DATABASE, parameters);
                List<EmailUpdations> listEmail = new List<EmailUpdations>();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        EmailUpdations obj = new EmailUpdations();

                        obj.UserEmail = (!String.IsNullOrEmpty(Convert.ToString(rdr["USER_EMAIL"]))) ? Convert.ToString(rdr["USER_EMAIL"]) : String.Empty;
                        obj.UserLoginId = (!String.IsNullOrEmpty(Convert.ToString(rdr["USER_LOGIN"]))) ? Convert.ToString(rdr["USER_LOGIN"]) : String.Empty;
                        obj.UserId = (!String.IsNullOrEmpty(Convert.ToString(rdr["ID"]))) ? Convert.ToInt32(rdr["ID"]) : 0;
                        obj.IS_APPROVER = (!String.IsNullOrEmpty(Convert.ToString(rdr["IS_APPROVER"]))) ? Convert.ToString(rdr["IS_APPROVER"]) : String.Empty;
                        obj.UserName = (!String.IsNullOrEmpty(Convert.ToString(rdr["USER_NM"]))) ? Convert.ToString(rdr["USER_NM"]) : String.Empty;
                        listEmail.Add(obj);
                    }
                    res.StatusFl = true;
                    res.Msg = "Email Data has been fetched successfully !";
                    res.ListEmails = listEmail;

                }
                else
                {
                    res.StatusFl = false;
                    res.Msg = "No data found !";
                }
                rdr.Close();
                return res;

            }
            catch (Exception ex)
            {
                EmailUpdationResponse oEmail = new EmailUpdationResponse();
                oEmail.StatusFl = false;
                oEmail.Msg = ex.Message;
                oEmail.Msg = "Processing failed, because of system error !";
                return oEmail;
            }

        }

        public EmailUpdationResponse UpdateEmail(EmailUpdations objemail)
        {
            try
            {

                EmailUpdationResponse res = new EmailUpdationResponse();


                SqlParameter[] parameters = new SqlParameter[6];
                parameters[0] = new SqlParameter("@MODE", "UPDATE_EMAIL_BY_USER_LOGIN");
                parameters[1] = new SqlParameter("@EMPLOYEE_ID", objemail.UserLoginId);
                parameters[2] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[2].Direction = ParameterDirection.Output;
                parameters[3] = new SqlParameter("@COMPANY_ID", objemail.COMPANY_ID);
                parameters[4] = new SqlParameter("@NEW_EMAIL", objemail.UserNewEmail);
                parameters[5] = new SqlParameter("@ADMIN_DATABASE", CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["AdminDB"]), true));
                var dim = SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_EMAIL_UPDATION", objemail.MODULE_DATABASE, parameters);
                List<EmailUpdations> listEmail = new List<EmailUpdations>();
                if ((Int16)dim > 0)
                {

                    res.StatusFl = true;
                    res.Msg = "Email Data has been updated successfully !";
                    res.ListEmails = listEmail;

                }
                else
                {
                    res.StatusFl = false;
                    res.Msg = "Email Data has not been updated!";
                }

                return res;

            }
            catch (Exception ex)
            {
                EmailUpdationResponse oEmail = new EmailUpdationResponse();
                oEmail.StatusFl = false;
                oEmail.Msg = ex.Message;
                // oEmail.Msg = "Processing failed, because of system error !";
                return oEmail;
            }

        }







    }
}