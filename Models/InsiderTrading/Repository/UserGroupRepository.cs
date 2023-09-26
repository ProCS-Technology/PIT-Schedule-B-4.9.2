using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.SessionState;
namespace ProcsDLL.Models.InsiderTrading.Repository
{
    public class UserGroupRepository : IRequiresSessionState
    {
        private static String connectionString = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionStringIT"]), true);
        public UserGroupResponse SaveUserGroup(UserGroup objusrgrp)
        {
            try
            {
                var mode = string.Empty;
                if (objusrgrp.GrpId == 0)
                {
                    mode = "INSERT";
                }
                else
                {
                    mode = "UPDATE";
                }

                SqlParameter[] parameters = new SqlParameter[7];
                parameters[0] = new SqlParameter("@MODE", mode);
                parameters[1] = new SqlParameter("@GRP_NAME", objusrgrp.GrpName);
                parameters[2] = new SqlParameter("@GRP_MEMBERS", objusrgrp.GroupMembers);
                parameters[3] = new SqlParameter("@COMPANY_ID", objusrgrp.CompanyId);
                parameters[4] = new SqlParameter("@CREATED_BY", objusrgrp.CreatedBy);
                parameters[5] = new SqlParameter("@GIDOUTPUT", SqlDbType.Int);
                parameters[5].Direction = ParameterDirection.Output;
                parameters[6] = new SqlParameter("@GRP_ID", objusrgrp.GrpId);

                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_GROUP", objusrgrp.MODULE_DATABASE, parameters);

                var obj = parameters[5].Value;

                UserGroupResponse objresp = new UserGroupResponse();
                if ((Int32)obj == 1)
                {
                    objresp.StatusFl = false;
                    objresp.Msg = "Group already exists !";

                }
                else if ((Int32)obj == 2)
                {
                    objresp.StatusFl = false;
                    objresp.Msg = "Invalid Group Id !";
                }
                else
                {
                    objresp.StatusFl = true;
                    objresp.Msg = "Data has been saved successfully !";

                }

                return objresp;
            }
            catch (Exception ex)
            {
                UserGroupResponse oUser = new UserGroupResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Processing failed, because of system error !";
                return oUser;
            }
        }

        public UserGroupResponse UserGroupList(UserGroup objusrgrp)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@COMPANY_ID", objusrgrp.CompanyId);
                parameters[1] = new SqlParameter("@Mode", "GET_USER_GROUP");
                //parameters[2] = new SqlParameter("@CREATED_BY", objusrgrp.CreatedBy);

                UserGroupResponse oGrp = new UserGroupResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_GROUP", objusrgrp.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        UserGroup obj = new UserGroup();
                        obj.GrpId = Convert.ToInt32(rdr["GRP_ID"]);
                        obj.GrpName = (!String.IsNullOrEmpty(Convert.ToString(rdr["GRP_NAME"]))) ? Convert.ToString(rdr["GRP_NAME"]) : String.Empty;
                        obj.GroupMembers = (!String.IsNullOrEmpty(Convert.ToString(rdr["GRP_MEMBERS"]))) ? Convert.ToString(rdr["GRP_MEMBERS"]) : String.Empty;
                        obj.CreatedOn = (!String.IsNullOrEmpty(Convert.ToString(rdr["CREATED_ON"]))) ? Convert.ToString(rdr["CREATED_ON"]) : String.Empty;
                        oGrp.AddObject(obj);
                    }
                    oGrp.StatusFl = true;
                    oGrp.Msg = "Data has been fetched successfully !";
                }
                else
                {
                    oGrp.StatusFl = false;
                    oGrp.Msg = "No data found !";
                }
                rdr.Close();
                return oGrp;
            }
            catch (Exception ex)
            {
                UserGroupResponse oUserGrp = new UserGroupResponse();
                oUserGrp.StatusFl = false;
                oUserGrp.Msg = "Processing failed, because of system error !";
                return oUserGrp;
            }
        }
        public UserGroupResponse UserGroupSentMailList(UserGroup objusrgrp)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@COMPANY_ID", objusrgrp.CompanyId);
                parameters[1] = new SqlParameter("@Mode", "SENT_EMAIL_LOG");

                UserGroupResponse oGrp = new UserGroupResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_GROUP_EMAIL_HDR", objusrgrp.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        UserGroup obj = new UserGroup();
                        obj.GrpId = Convert.ToInt32(rdr["GRP_ID"]);
                        obj.LogId = Convert.ToInt32(rdr["EID"]);
                        obj.GrpName = (!String.IsNullOrEmpty(Convert.ToString(rdr["GRP_NAME"]))) ? Convert.ToString(rdr["GRP_NAME"]) : String.Empty;
                        //obj.EmailId = (!String.IsNullOrEmpty(Convert.ToString(rdr["EMAIL_ID"]))) ? Convert.ToString(rdr["EMAIL_ID"]) : String.Empty;
                        //obj.CreatedOn = (!String.IsNullOrEmpty(Convert.ToString(rdr["SENT_ON"]))) ? Convert.ToString(rdr["SENT_ON"]) : String.Empty;

                        obj.GrpEmailSubject = (!String.IsNullOrEmpty(Convert.ToString(rdr["EMAIL_SUBJECT"]))) ? Convert.ToString(rdr["EMAIL_SUBJECT"]) : String.Empty;
                        obj.GrpEmailBody = (!String.IsNullOrEmpty(Convert.ToString(rdr["EMAIL_BODY"]))) ? Convert.ToString(rdr["EMAIL_BODY"]) : String.Empty;
                        obj.CreatedOn = (!String.IsNullOrEmpty(Convert.ToString(rdr["CREATED_ON"]))) ? Convert.ToString(rdr["CREATED_ON"]) : String.Empty;
                        //obj.CreatedBy = (!String.IsNullOrEmpty(Convert.ToString(rdr["CREATED_BY"]))) ? Convert.ToString(rdr["CREATED_BY"]) : String.Empty;
                        oGrp.AddObjectUserGroupSentMail(obj);
                    }
                    oGrp.StatusFl = true;
                    oGrp.Msg = "Data has been fetched successfully !";
                }
                else
                {
                    oGrp.StatusFl = false;
                    oGrp.Msg = "No data found !";
                }
                rdr.Close();
                return oGrp;
            }
            catch (Exception ex)
            {
                UserGroupResponse oUserGrp = new UserGroupResponse();
                oUserGrp.StatusFl = false;
                oUserGrp.Msg = "Processing failed, because of system error !";
                return oUserGrp;
            }
        }
        public UserGroupResponse SendUserGroupEmail(UserGroup objusrgrp)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[6];
                parameters[0] = new SqlParameter("@MODE", "GET_USER_EMAIL");
                parameters[1] = new SqlParameter("@ADMIN_DB", objusrgrp.ADMIN_DATABASE);
                parameters[2] = new SqlParameter("@EMAIL_SUBJECT", objusrgrp.GrpEmailSubject);
                parameters[3] = new SqlParameter("@EMAIL_BODY", objusrgrp.GrpEmailBody);
                parameters[4] = new SqlParameter("@CREATED_BY", objusrgrp.CreatedBy);
                parameters[5] = new SqlParameter("@GRP_ID", objusrgrp.EmailGrpId);


                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_GROUP_EMAIL_HDR", objusrgrp.MODULE_DATABASE, parameters);
                UserGroupResponse oGrp = new UserGroupResponse();
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        if (SendEmailToAllUserGroup(objusrgrp.GrpEmailSubject, objusrgrp.GrpEmailBody, ds.Tables[0]))
                        {
                            oGrp.StatusFl = true;
                            oGrp.Msg = "Email sent successfully !";
                        }
                    }
                }

                else
                {
                    oGrp.StatusFl = false;
                    oGrp.Msg = "Processing failed, because of system error !";
                }

                return oGrp;
            }
            catch (Exception ex)
            {
                UserGroupResponse oUserGrp = new UserGroupResponse();
                oUserGrp.StatusFl = false;
                oUserGrp.Msg = "Processing failed, because of system error !";
                return oUserGrp;
            }


        }
        public Boolean SendEmailToAllUserGroup(string Emailsubject, string EmailBody, DataTable UserEmail)
        {
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@Mode", "GET_Smtp_Config_List");
            parameters[1] = new SqlParameter("@COMPANY_ID", Convert.ToString(HttpContext.Current.Session["CompanyId"]));
            parameters[2] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
            parameters[2].Direction = ParameterDirection.Output;

            DataSet ds1 = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_CONFIG_SMTP", Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]), parameters);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                string defaultEmail = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["DEFAULT_EMAIL"]))) ? Convert.ToString(ds1.Tables[0].Rows[0]["DEFAULT_EMAIL"]) : String.Empty;
                string disclosureEmail = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["CONTINUAL_DISCLOSURE_EMAIL"]))) ? Convert.ToString(ds1.Tables[0].Rows[0]["CONTINUAL_DISCLOSURE_EMAIL"]) : String.Empty;
                string smtpHostName = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["SMTP_HOST_NAME"]))) ? Convert.ToString(ds1.Tables[0].Rows[0]["SMTP_HOST_NAME"]) : String.Empty;
                Int32 port = Convert.ToInt32(ds1.Tables[0].Rows[0]["PORT"]);
                bool ssl = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["SSL"]))) ? (Convert.ToString(ds1.Tables[0].Rows[0]["SSL"]) == "Yes" ? true : false) : false;
                string smtpUserName = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["SMTP_USER_NAME"]))) ? Convert.ToString(ds1.Tables[0].Rows[0]["SMTP_USER_NAME"]) : String.Empty;
                string password = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["PASSWORD"]))) ? CryptorEngine.Decrypt(Convert.ToString(ds1.Tables[0].Rows[0]["PASSWORD"]), true) : String.Empty;
                bool userDefaultCredential = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["USER_DEFAULT_CREDENTIAL"]))) ? (Convert.ToString(ds1.Tables[0].Rows[0]["USER_DEFAULT_CREDENTIAL"]) == "Yes" ? true : false) : false;


                if (UserEmail.Rows.Count > 0)
                {
                    SqlParameter[] parameter = new SqlParameter[6];
                    parameter[0] = new SqlParameter("@MODE", "CREATE_EMAIL_LOG");
                    parameter[1] = new SqlParameter("@EMAIL_SUBJECT", Emailsubject);
                    parameter[2] = new SqlParameter("@EMAIL_BODY", EmailBody);
                    parameter[3] = new SqlParameter("@CREATED_BY", Convert.ToString(HttpContext.Current.Session["EmployeeId"]));
                    parameter[4] = new SqlParameter("@COMPANY_ID", Convert.ToInt32(HttpContext.Current.Session["CompanyId"]));
                    parameter[5] = new SqlParameter("@ELOGIDOUTPUT", SqlDbType.Int);
                    parameter[5].Direction = ParameterDirection.Output;
                    SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_GROUP_EMAIL_HDR", Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]), parameter);

                    var obj = parameter[5].Value;


                    foreach (DataRow dr in UserEmail.Rows)
                    {
                        EmailSender.SendMail(
                            dr["USER_EMAIL"].ToString(), Emailsubject, EmailBody, null, "UPSI Reminder",
                            Convert.ToString(HttpContext.Current.Session["CompanyId"]), "", "",
                            Convert.ToString(HttpContext.Current.Session["EployeeId"])
                        );
                        //EmailHelper.SendEmailForUserGroup(defaultEmail, dr["USER_EMAIL"].ToString(), Emailsubject, EmailBody, smtpHostName, ssl, smtpUserName, password, userDefaultCredential, port);
                        SqlParameter[] parameter2 = new SqlParameter[4];
                        parameter2[0] = new SqlParameter("@MODE", "SAVE_EMAIL_LOG");
                        parameter2[1] = new SqlParameter("@ELOGID", (Int32)obj);
                        parameter2[2] = new SqlParameter("@EMAIL_ID", dr["USER_EMAIL"].ToString());
                        parameter2[3] = new SqlParameter("@GRP_ID", dr["GRP_ID"].ToString());
                        SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_GROUP_EMAIL_HDR", Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]), parameter2);
                    }

                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }

        }


    }

}