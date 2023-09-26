using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Linq;
using System.Web.SessionState;
namespace ProcsDLL.Models.InsiderTrading.Repository
{
    public class InsiderTradingWindowRepository: IRequiresSessionState
    {
        private const string sessionKey = "TradingWindow";
        public Progress Progress
        {
            get
            {
                if (HttpContext.Current.Session[sessionKey] == null)
                {
                    HttpContext.Current.Session.Add(sessionKey, new Progress());
                }
                return HttpContext.Current.Session[sessionKey] as Progress;
            }
            set
            {
                if (HttpContext.Current.Session[sessionKey] == null)
                {
                    HttpContext.Current.Session.Add(sessionKey, new Progress());
                }
                HttpContext.Current.Session[sessionKey] = value;
            }
        }
        public InsiderTradingWindowResponse GetEmailTemplate(InsiderTradingWindow objInsiderTradingWindow)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@MODE", "GET_EMAIL_TEMPLATE");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objInsiderTradingWindow.companyId);
                parameters[3] = new SqlParameter("@WINDOW_CLOSURE_TYPE_ID", objInsiderTradingWindow.windowClosureTypeId);
                InsiderTradingWindowResponse oInsiderTradingWindow = new InsiderTradingWindowResponse();

                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_TRADING_WINDOW", objInsiderTradingWindow.MODULE_DATABASE, parameters);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    InsiderTradingWindow obj = new InsiderTradingWindow();
                    obj.EmailTemplate = Convert.ToString(dt.Rows[0]["EMAIL_TEMPLETE"]);
                    oInsiderTradingWindow.InsiderTradingWindow = obj;
                    oInsiderTradingWindow.StatusFl = true;
                    oInsiderTradingWindow.Msg = "Success";
                }
                else
                {
                    oInsiderTradingWindow.StatusFl = false;
                    oInsiderTradingWindow.Msg = "Email Template not defined !";
                }
                return oInsiderTradingWindow;
            }
            catch (Exception ex)
            {
                InsiderTradingWindowResponse oInsiderTradingWindow = new InsiderTradingWindowResponse();
                oInsiderTradingWindow.StatusFl = false;
                oInsiderTradingWindow.Msg = "Processing failed, because of system error !";
                return oInsiderTradingWindow;
            }
        }
        #region "Save Insider Trading Window"
        public InsiderTradingWindowResponse SaveInsiderTradingWindow(InsiderTradingWindow objInsiderTradingWindow)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[10];
                parameters[0] = new SqlParameter("@MODE", "ADD_INSIDER_TRADING_WINDOW");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objInsiderTradingWindow.companyId);
                parameters[3] = new SqlParameter("@FROM_DATE", FormatHelper.ConvertDateTime(objInsiderTradingWindow.fromDate));
                parameters[4] = new SqlParameter("@TO_DATE", !String.IsNullOrEmpty(objInsiderTradingWindow.toDate) ? FormatHelper.ConvertDateTime(objInsiderTradingWindow.toDate) : FormatHelper.ConvertDateTime("31/12/9999"));
                parameters[5] = new SqlParameter("@REMARKS", objInsiderTradingWindow.remarks);
                parameters[6] = new SqlParameter("@CREATED_BY", objInsiderTradingWindow.createdBy);
                parameters[7] = new SqlParameter("@BOARD_MEETING_SCHEDULED_ON", !String.IsNullOrEmpty(objInsiderTradingWindow.boardMeetingScheduledOn) ? FormatHelper.ConvertDateTime(objInsiderTradingWindow.boardMeetingScheduledOn) : FormatHelper.ConvertDateTime("31/12/9999"));
                parameters[8] = new SqlParameter("@QUARTER_ENDED_ON", !String.IsNullOrEmpty(objInsiderTradingWindow.quarterEndedOn) ? ConvertDate(objInsiderTradingWindow.quarterEndedOn) : FormatHelper.ConvertDateTime("31/12/9999"));
                parameters[9] = new SqlParameter("@WINDOW_CLOSURE_TYPE_ID", objInsiderTradingWindow.windowClosureTypeId);
                InsiderTradingWindowResponse oInsiderTradingWindow = new InsiderTradingWindowResponse();
                SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_TRADING_WINDOW", objInsiderTradingWindow.MODULE_DATABASE, parameters);
                //if (SendEmailToAllInsiderForWindowClosure(objInsiderTradingWindow))
                //{
                //    oInsiderTradingWindow.StatusFl = true;
                //    oInsiderTradingWindow.Msg = "Email sent successfully !";
                //}
                //else
                //{
                //    oInsiderTradingWindow.StatusFl = true;
                //    oInsiderTradingWindow.Msg = "Data has been fetched successfully !";
                //}
                oInsiderTradingWindow.StatusFl = true;
                oInsiderTradingWindow.Msg = "Data has been added successfully !";
                return oInsiderTradingWindow;
            }
            catch (Exception ex)
            {
                InsiderTradingWindowResponse oInsiderTradingWindow = new InsiderTradingWindowResponse();
                oInsiderTradingWindow.StatusFl = false;
                oInsiderTradingWindow.Msg = "Processing failed, because of system error !";
                return oInsiderTradingWindow;
            }
        }
        #endregion
        #region "Update Insider Trading Window"
        public InsiderTradingWindowResponse UpdateInsiderTradingWindow(InsiderTradingWindow objInsiderTradingWindow)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[11];
                parameters[0] = new SqlParameter("@MODE", "UPDATE_INSIDER_TRADING_WINDOW");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objInsiderTradingWindow.companyId);
                parameters[3] = new SqlParameter("@FROM_DATE", FormatHelper.ConvertDateTime(objInsiderTradingWindow.fromDate));
                parameters[4] = new SqlParameter("@TO_DATE", !String.IsNullOrEmpty(objInsiderTradingWindow.toDate) ? FormatHelper.ConvertDateTime(objInsiderTradingWindow.toDate) : FormatHelper.ConvertDateTime("31/12/9999"));
                parameters[5] = new SqlParameter("@REMARKS", objInsiderTradingWindow.remarks);
                parameters[6] = new SqlParameter("@CREATED_BY", objInsiderTradingWindow.createdBy);
                parameters[7] = new SqlParameter("@ID", objInsiderTradingWindow.id);
                parameters[8] = new SqlParameter("@BOARD_MEETING_SCHEDULED_ON", !String.IsNullOrEmpty(objInsiderTradingWindow.boardMeetingScheduledOn) ? FormatHelper.ConvertDateTime(objInsiderTradingWindow.boardMeetingScheduledOn) : FormatHelper.ConvertDateTime("31/12/9999"));
                parameters[9] = new SqlParameter("@QUARTER_ENDED_ON", !String.IsNullOrEmpty(objInsiderTradingWindow.quarterEndedOn) ? FormatHelper.ConvertDateTime(objInsiderTradingWindow.quarterEndedOn) : FormatHelper.ConvertDateTime("31/12/9999"));
                parameters[10] = new SqlParameter("@WINDOW_CLOSURE_TYPE_ID", objInsiderTradingWindow.windowClosureTypeId);
                InsiderTradingWindowResponse oInsiderTradingWindow = new InsiderTradingWindowResponse();
                SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_TRADING_WINDOW", objInsiderTradingWindow.MODULE_DATABASE, parameters);
                //if (SendEmailToAllInsiderForWindowClosure(objInsiderTradingWindow))
                //{
                //    oInsiderTradingWindow.StatusFl = true;
                //    oInsiderTradingWindow.Msg = "Email sent successfully !";
                //}
                //else
                //{
                //    oInsiderTradingWindow.StatusFl = true;
                //    oInsiderTradingWindow.Msg = "Data has been updated successfully !";
                //}
                oInsiderTradingWindow.StatusFl = true;
                oInsiderTradingWindow.Msg = "Data has been updated successfully !";
                return oInsiderTradingWindow;
            }
            catch (Exception ex)
            {
                InsiderTradingWindowResponse oInsiderTradingWindow = new InsiderTradingWindowResponse();
                oInsiderTradingWindow.StatusFl = false;
                oInsiderTradingWindow.Msg = "Processing failed, because of system error !";
                return oInsiderTradingWindow;
            }
        }
        #endregion
        #region "Get Insider Trading Window Closure Info"
        public InsiderTradingWindowResponse GetInsiderTradingWindowClosureInfo(InsiderTradingWindow objInsiderTradingWindow)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@MODE", "GET_IT_WINDOW_CLOSURE_INFO");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objInsiderTradingWindow.companyId);
                InsiderTradingWindowResponse oInsiderTradingWindow = new InsiderTradingWindowResponse();
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_TRADING_WINDOW", objInsiderTradingWindow.MODULE_DATABASE, parameters);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            InsiderTradingWindow obj = new InsiderTradingWindow();
                            obj.fromDate = !String.IsNullOrEmpty(Convert.ToString(dr["FROM_DATE"])) ? dr["FROM_DATE"].ToString() : String.Empty;
                            obj.toDate = !String.IsNullOrEmpty(Convert.ToString(dr["TO_DATE"])) ? dr["TO_DATE"].ToString() : String.Empty;
                            obj.remarks = !String.IsNullOrEmpty(Convert.ToString(dr["REMARKS"])) ? dr["REMARKS"].ToString() : String.Empty;

                            oInsiderTradingWindow.AddObject(obj);
                        }
                        oInsiderTradingWindow.StatusFl = true;
                        oInsiderTradingWindow.Msg = "Data has been fetched successfully !";
                    }
                    else
                    {
                        oInsiderTradingWindow.StatusFl = false;
                        oInsiderTradingWindow.Msg = "No data found !";
                    }
                }
                else
                {
                    oInsiderTradingWindow.StatusFl = false;
                    oInsiderTradingWindow.Msg = "No data found !";
                }

                return oInsiderTradingWindow;
            }
            catch (Exception ex)
            {
                InsiderTradingWindowResponse oInsiderTradingWindow = new InsiderTradingWindowResponse();
                oInsiderTradingWindow.StatusFl = false;
                oInsiderTradingWindow.Msg = "Processing failed, because of system error !";
                return oInsiderTradingWindow;
            }
        }
        #endregion
        #region "Get Insider Trading Window Closure Info List"
        public InsiderTradingWindowResponse GetInsiderTradingWindowClosureInfoList(InsiderTradingWindow objInsiderTradingWindow)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@MODE", "GET_IT_WINDOW_CLOSURE_INFO_LIST");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objInsiderTradingWindow.companyId);
                InsiderTradingWindowResponse oInsiderTradingWindow = new InsiderTradingWindowResponse();
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_TRADING_WINDOW", objInsiderTradingWindow.MODULE_DATABASE, parameters);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            InsiderTradingWindow obj = new InsiderTradingWindow();
                            obj.id = Convert.ToInt32(dr["ID"]);
                            obj.windowClosureTypeId = !String.IsNullOrEmpty(Convert.ToString(dr["TRADING_WINDOW_CLOSURE_TYPE_ID"])) ? Convert.ToInt32(dr["TRADING_WINDOW_CLOSURE_TYPE_ID"]) : 0;
                            obj.fromDate = !String.IsNullOrEmpty(Convert.ToString(dr["FROM_DATE"])) ? Convert.ToString(dr["FROM_DATE"]) : String.Empty;
                            obj.toDate = !String.IsNullOrEmpty(Convert.ToString(dr["TO_DATE"])) ? Convert.ToString(dr["TO_DATE"]) : String.Empty;
                            obj.remarks = !String.IsNullOrEmpty(Convert.ToString(dr["REMARKS"])) ? Convert.ToString(dr["REMARKS"]) : String.Empty;
                            obj.boardMeetingScheduledOn = !String.IsNullOrEmpty(Convert.ToString(dr["BOARD_MEETING_SCHEDULED_ON"])) ? Convert.ToString(dr["BOARD_MEETING_SCHEDULED_ON"]) : String.Empty;
                            obj.quarterEndedOn = !String.IsNullOrEmpty(Convert.ToString(dr["WINDOW_CLOSURE_TYPE"])) ? Convert.ToString(dr["WINDOW_CLOSURE_TYPE"]) : String.Empty;
                            obj.EmailTemplate = !String.IsNullOrEmpty(Convert.ToString(dr["EMAIL_TEMPLATE"])) ? Convert.ToString(dr["EMAIL_TEMPLATE"]) : String.Empty;

                            obj.EmailTemplate = obj.EmailTemplate.Replace("[Closer Type]", Convert.ToString(dr["WINDOW_CLOSURE_TYPE"]));
                            obj.EmailTemplate = obj.EmailTemplate.Replace("[Company Nm]", Convert.ToString(dr["COMPANY_NAME"]));
                            if (!String.IsNullOrEmpty(obj.fromDate))
                            {
                                obj.EmailTemplate = obj.EmailTemplate.Replace("[From Dt]", obj.fromDate);
                            }
                            if (!String.IsNullOrEmpty(obj.toDate) && obj.toDate != "31/12/9999")
                            {
                                obj.EmailTemplate = obj.EmailTemplate.Replace("[To Dt]", obj.toDate);
                            }
                            else
                            {
                                obj.EmailTemplate = obj.EmailTemplate.Replace("[To Dt]", "notified");
                            }
                            oInsiderTradingWindow.AddObject(obj);
                        }
                        oInsiderTradingWindow.StatusFl = true;
                        oInsiderTradingWindow.Msg = "Data has been fetched successfully !";
                    }
                    else
                    {
                        oInsiderTradingWindow.StatusFl = false;
                        oInsiderTradingWindow.Msg = "No data found !";
                    }
                }
                else
                {
                    oInsiderTradingWindow.StatusFl = false;
                    oInsiderTradingWindow.Msg = "No data found !";
                }

                return oInsiderTradingWindow;
            }
            catch (Exception ex)
            {
                InsiderTradingWindowResponse oInsiderTradingWindow = new InsiderTradingWindowResponse();
                oInsiderTradingWindow.StatusFl = false;
                oInsiderTradingWindow.Msg = "Processing failed, because of system error !";
                return oInsiderTradingWindow;
            }
        }
        #endregion
        #region "Send Email To All Insider For Window Closure"
        public Boolean SendEmailToAllInsiderForWindowClosure(InsiderTradingWindow objInsiderTradingWindow)
        {
            List<string> lstAttachments = new List<string>();
            if (objInsiderTradingWindow.TradingWindowDocumentPath != null)
            {
                lstAttachments.Add(objInsiderTradingWindow.TradingWindowDocumentPath);
            }

            //Progress = null;
            var sEmployeeId = HttpContext.Current.Session["EmployeeId"];
            var prog = HttpContext.Current.Session[sessionKey];
            if (HttpContext.Current.Session[sessionKey] == null)
            {
                HttpContext.Current.Session.Add(sessionKey, new ProgressStep("Process Started", ProgressStatus.InProgress));
            }

            //Progress.Add(new ProgressStep("Process Started", ProgressStatus.InProgress));
            foreach (string sEmail in objInsiderTradingWindow.lstUser)
            {
                string subject = objInsiderTradingWindow.EmailSubject;// "TRADING WINDOW CLOSURE NOTICE";
                string body = objInsiderTradingWindow.EmailTemplate;

                if (sEmail != "")
                {
                    EmailSender.SendMail(
                        sEmail, subject, body, lstAttachments, "Trading Window Notification", objInsiderTradingWindow.companyId.ToString(),
                        "", objInsiderTradingWindow.id.ToString()
                    );
                    Progress.Add(new ProgressStep(string.Format("Sent mail to {0}", sEmail), ProgressStatus.InProgress));
                }
            }
            foreach(string sMailto in objInsiderTradingWindow.lstmailTo)
            {
                string subject = objInsiderTradingWindow.EmailSubject; //"TRADING WINDOW CLOSURE NOTICE";
                string body = objInsiderTradingWindow.EmailTemplate;
                if (sMailto != "")
                {
                    EmailSender.SendMail(
                        sMailto, subject, body, lstAttachments, "Trading Window Notification", objInsiderTradingWindow.companyId.ToString(),
                        "", objInsiderTradingWindow.id.ToString()
                    );
                    Progress.Add(new ProgressStep(string.Format("Sent mail to {0}", sMailto), ProgressStatus.InProgress));
                }
            }
            return true;
        }
        #endregion
        #region "Send Email For Trading Window Closure"
        public InsiderTradingWindowResponse SendEmailForTradingWindowClosure(InsiderTradingWindow objInsiderTradingWindow)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@MODE", "GET_TRADING_WINDOW_CLOSURE_INFORMATION_BY_ID");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objInsiderTradingWindow.companyId);
                parameters[3] = new SqlParameter("@ID", objInsiderTradingWindow.id);
                InsiderTradingWindowResponse oInsiderTradingWindow = new InsiderTradingWindowResponse();
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_TRADING_WINDOW", objInsiderTradingWindow.MODULE_DATABASE, parameters);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        objInsiderTradingWindow.windowClosureTypeId = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["TRADING_WINDOW_CLOSURE_TYPE_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["TRADING_WINDOW_CLOSURE_TYPE_ID"]) : 0;
                        objInsiderTradingWindow.fromDate = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["FROM_DATE"])) ? FormatHelper.FormatDate(ds.Tables[0].Rows[0]["FROM_DATE"].ToString()) : String.Empty;
                        objInsiderTradingWindow.toDate = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["TO_DATE"])) ? FormatHelper.FormatDate(ds.Tables[0].Rows[0]["TO_DATE"].ToString()) : String.Empty;
                        objInsiderTradingWindow.boardMeetingScheduledOn = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["BOARD_MEETING_SCHEDULED_ON"])) ? FormatHelper.FormatDate(ds.Tables[0].Rows[0]["BOARD_MEETING_SCHEDULED_ON"].ToString()) : String.Empty;
                        objInsiderTradingWindow.quarterEndedOn = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["QUARTER_ENDED_ON"])) ? FormatHelper.FormatDate(ds.Tables[0].Rows[0]["QUARTER_ENDED_ON"].ToString()) : String.Empty;
                        objInsiderTradingWindow.remarks = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["REMARKS"])) ? Convert.ToString(ds.Tables[0].Rows[0]["REMARKS"]) : String.Empty;
                    }
                }
                List<string> mailto = objInsiderTradingWindow.lstmailTo.Distinct().ToList();
                objInsiderTradingWindow.lstmailTo = mailto;
                List<string> users = objInsiderTradingWindow.lstUser.Distinct().ToList();
                objInsiderTradingWindow.lstUser = users;
                if (SendEmailToAllInsiderForWindowClosure(objInsiderTradingWindow))
                {
                    oInsiderTradingWindow.StatusFl = true;
                    oInsiderTradingWindow.Msg = "Email sent successfully !";
                }
                else
                {
                    oInsiderTradingWindow.StatusFl = false;
                    oInsiderTradingWindow.Msg = "Processing failed, because of system error !";
                }
                return oInsiderTradingWindow;
            }
            catch (Exception ex)
            {
                InsiderTradingWindowResponse oInsiderTradingWindow = new InsiderTradingWindowResponse();
                oInsiderTradingWindow.StatusFl = false;
                oInsiderTradingWindow.Msg = "Processing failed, because of system error !";
                return oInsiderTradingWindow;
            }
        }
        #endregion
        private DateTime ConvertDate(String date)
        {
            String str = String.Empty;
            try
            {
                if (date.Contains("/"))
                {
                    str = date.Split('/')[2] + "-" + date.Split('/')[1] + "-" + date.Split('/')[0];
                }
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message.ToString(), ex.Source, ex.StackTrace, this.GetType().Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, Convert.ToString(HttpContext.Current.Session["EmployeeId"]), Convert.ToInt32(HttpContext.Current.Session["ModuleId"]));
            }

            return Convert.ToDateTime(str);
        }
    }
}