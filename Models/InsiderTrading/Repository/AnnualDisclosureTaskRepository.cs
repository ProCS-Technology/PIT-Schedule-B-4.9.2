using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Syncfusion.DocToPDFConverter;
using Syncfusion.Pdf;
using Syncfusion.DocIO.DLS;
using System.Configuration;
using OfficeExcel = Microsoft.Office.Interop.Excel;
using Syncfusion.HtmlConverter;

namespace ProcsDLL.Models.InsiderTrading.Repository
{
    public class AnnualDisclosureTaskRepository
    {

        public AnnualDisclosureTaskResponse GetAnnualDisclosureTask(AnnualDisclosureTask objAnnualDisclosureTask)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@MODE", "GET_EMAIL_TEMPLATE");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objAnnualDisclosureTask.companyId);
                // parameters[3] = new SqlParameter("@WINDOW_CLOSURE_TYPE_ID", objAnnualDisclosureTask.windowClosureTypeId);
                AnnualDisclosureTaskResponse oAnnualDisclosureTask = new AnnualDisclosureTaskResponse();

                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_TRADING_WINDOW", objAnnualDisclosureTask.MODULE_DATABASE, parameters);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    AnnualDisclosureTask obj = new AnnualDisclosureTask();
                    // obj.EmailTemplate = Convert.ToString(dt.Rows[0]["EMAIL_TEMPLETE"]);
                    oAnnualDisclosureTask.AnnualDisclosureTask = obj;
                    oAnnualDisclosureTask.StatusFl = true;
                    oAnnualDisclosureTask.Msg = "Success";
                }
                else
                {
                    oAnnualDisclosureTask.StatusFl = false;
                    oAnnualDisclosureTask.Msg = "Email Template not defined !";
                }
                return oAnnualDisclosureTask;
            }
            catch (Exception ex)
            {
                AnnualDisclosureTaskResponse oAnnualDisclosureTask = new AnnualDisclosureTaskResponse();
                oAnnualDisclosureTask.StatusFl = false;
                oAnnualDisclosureTask.Msg = "Processing failed, because of system error !";
                return oAnnualDisclosureTask;
            }
        }
        #region "Save Annual Disclosure Task"
        public AnnualDisclosureTaskResponse SaveAnnualDisclosureTask(AnnualDisclosureTask objAnnualDisclosureTask)
        {
            try
            {

                SqlParameter[] parameters = new SqlParameter[11];
                parameters[0] = new SqlParameter("@ID", objAnnualDisclosureTask.id);
                parameters[1] = new SqlParameter("@FINANCIAL_YEARS", objAnnualDisclosureTask.FINANCIALYEARS);
                parameters[2] = new SqlParameter("@Title", objAnnualDisclosureTask.Title);
                //parameters[3] = new SqlParameter("@START_DATE", ConvertDate(objAnnualDisclosureTask.STARTDATE));
                parameters[3] = new SqlParameter("@START_DATE", FormatHelper.FormatDate(objAnnualDisclosureTask.STARTDATE));
                parameters[4] = new SqlParameter("@END_DATE", objAnnualDisclosureTask.TILLDATE);
                parameters[5] = new SqlParameter("@COMPANY_ID", objAnnualDisclosureTask.companyId);
                parameters[6] = new SqlParameter("@Mode", "INSERT_UPDATE");
                parameters[7] = new SqlParameter("@ACTION_TYPE", "INSERT");
                parameters[8] = new SqlParameter("@CREATED_BY", objAnnualDisclosureTask.createdBy);
                parameters[9] = new SqlParameter("@EMPLOYEE_ID", objAnnualDisclosureTask.createdBy);
                parameters[10] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[10].Direction = ParameterDirection.Output;

                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_ANNUAL_DISCLOSURE_TASK1", objAnnualDisclosureTask.MODULE_DATABASE, parameters);
                var obj = parameters[10].Value;
                AnnualDisclosureTaskResponse oDep = new AnnualDisclosureTaskResponse();
                if ((Int32)obj == 0)
                {
                    oDep.StatusFl = true;
                    oDep.Msg = "Data has been saved successfully !";
                    oDep.AnnualDisclosureTask = objAnnualDisclosureTask;
                }
                else
                {
                    oDep.StatusFl = false;
                    oDep.Msg = "Annual Disclosure Task  aleady exists !";
                }
                return oDep;
            }
            catch (Exception ex)
            {
                AnnualDisclosureTaskResponse oDep = new AnnualDisclosureTaskResponse();
                oDep.StatusFl = false;
                oDep.Msg = "Processing failed, because of system error !";
                return oDep;
            }
        }
        #endregion
        #region "Update Annual Disclosure Task"
        public AnnualDisclosureTaskResponse UpdateAnnualDisclosureTask(AnnualDisclosureTask objAnnualDisclosureTask)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[10];
                parameters[0] = new SqlParameter("@ID", objAnnualDisclosureTask.id);
                parameters[1] = new SqlParameter("@FINANCIAL_YEARS", objAnnualDisclosureTask.FINANCIALYEARS);
                parameters[2] = new SqlParameter("@Title", objAnnualDisclosureTask.Title);
                parameters[3] = new SqlParameter("@END_DATE", objAnnualDisclosureTask.TILLDATE);
                //parameters[2] = new SqlParameter("@START_DATE", ConvertDate(objAnnualDisclosureTask.STARTDATE));
                //parameters[4] = new SqlParameter("@START_DATE", ConvertDate(objAnnualDisclosureTask.STARTDATE));
                parameters[4] = new SqlParameter("@START_DATE", FormatHelper.FormatDate(objAnnualDisclosureTask.STARTDATE));
                // parameters[1] = new SqlParameter("@END_DATE", objAnnualDisclosureTask.TILLDATE);
                parameters[5] = new SqlParameter("@COMPANY_ID", objAnnualDisclosureTask.companyId);
                parameters[6] = new SqlParameter("@Mode", "INSERT_UPDATE");
                parameters[7] = new SqlParameter("@ACTION_TYPE", "UPDATE");
                parameters[8] = new SqlParameter("@EMPLOYEE_ID", objAnnualDisclosureTask.createdBy);

                parameters[9] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[9].Direction = ParameterDirection.Output;

                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_ANNUAL_DISCLOSURE_TASK1", objAnnualDisclosureTask.MODULE_DATABASE, parameters);
                var obj = parameters[9].Value;
                AnnualDisclosureTaskResponse oAnnualDisclosureTask = new AnnualDisclosureTaskResponse();
                if ((Int32)obj == 0)
                {


                    oAnnualDisclosureTask.StatusFl = true;
                    oAnnualDisclosureTask.Msg = "Data has been updated successfully !";
                    oAnnualDisclosureTask.AnnualDisclosureTask = objAnnualDisclosureTask;
                }
                else
                {
                    oAnnualDisclosureTask.StatusFl = false;
                    oAnnualDisclosureTask.Msg = "Data has been updated successfully !";
                }
                return oAnnualDisclosureTask;
            }
            catch (Exception ex)
            {
                AnnualDisclosureTaskResponse oAnnualDisclosureTask = new AnnualDisclosureTaskResponse();
                oAnnualDisclosureTask.StatusFl = false;
                oAnnualDisclosureTask.Msg = "Processing failed, because of system error !";
                return oAnnualDisclosureTask;
            }

        }
        #endregion
        #region "Get Annual Disclosure Task Info"
        public AnnualDisclosureTaskResponse GetAnnualDisclosureTaskClosureInfo(AnnualDisclosureTask objAnnualDisclosureTask)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@MODE", "GET_IT_WINDOW_CLOSURE_INFO");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objAnnualDisclosureTask.companyId);
                AnnualDisclosureTaskResponse oAnnualDisclosureTask = new AnnualDisclosureTaskResponse();
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_TRADING_WINDOW", objAnnualDisclosureTask.MODULE_DATABASE, parameters);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            AnnualDisclosureTask obj = new AnnualDisclosureTask();
                            parameters[2] = new SqlParameter("@START_DATE", objAnnualDisclosureTask.STARTDATE);
                            parameters[3] = new SqlParameter("@END_DATE", objAnnualDisclosureTask.TILLDATE);

                            // obj.STARTDATE = !String.IsNullOrEmpty(Convert.ToString(dr["FROM_DATE"])) ? Convert.ToString(dr["FROM_DATE"]) : String.Empty;
                            //obj.TILLDATE= !String.IsNullOrEmpty(Convert.ToString(dr["TO_DATE"])) ? Convert.ToString(dr["TO_DATE"]) : String.Empty;
                            //obj.remarks = !String.IsNullOrEmpty(Convert.ToString(dr["REMARKS"])) ? Convert.ToString(dr["REMARKS"]) : String.Empty;

                            oAnnualDisclosureTask.AddObject(obj);
                        }
                        oAnnualDisclosureTask.StatusFl = true;
                        oAnnualDisclosureTask.Msg = "Data has been fetched successfully !";
                    }
                    else
                    {
                        oAnnualDisclosureTask.StatusFl = false;
                        oAnnualDisclosureTask.Msg = "No data found !";
                    }
                }
                else
                {
                    oAnnualDisclosureTask.StatusFl = false;
                    oAnnualDisclosureTask.Msg = "No data found !";
                }

                return oAnnualDisclosureTask;
            }
            catch (Exception ex)
            {
                AnnualDisclosureTaskResponse oAnnualDisclosureTask = new AnnualDisclosureTaskResponse();
                oAnnualDisclosureTask.StatusFl = false;
                oAnnualDisclosureTask.Msg = "Processing failed, because of system error !";
                return oAnnualDisclosureTask;
            }
        }
        #endregion
        #region "Get Annual Disclosure Task Info List"
        public AnnualDisclosureTaskResponse GetAnnualDisclosureTaskClosureInfoList(AnnualDisclosureTask objAnnualDisclosureTask)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@COMPANY_ID", objAnnualDisclosureTask.companyId);
                parameters[1] = new SqlParameter("@Mode", "GET_ANNUAL_DISCLOSURE_TASK_List");
                parameters[2] = new SqlParameter("@EMPLOYEE_ID", objAnnualDisclosureTask.createdBy);
                parameters[3] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[3].Direction = ParameterDirection.Output;

                AnnualDisclosureTaskResponse oAnnualDisclosureTask = new AnnualDisclosureTaskResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_ANNUAL_DISCLOSURE_TASK1", objAnnualDisclosureTask.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        AnnualDisclosureTask obj = new AnnualDisclosureTask();

                        obj.id = Convert.ToInt32(rdr.GetValue(0));
                        obj.FINANCIALYEARS = Convert.ToString(rdr.GetValue(1));
                        obj.Title = Convert.ToString(rdr.GetValue(2));
                        //obj.STARTDATE = !String.IsNullOrEmpty(rdr["TASK_START_DATE"].ToString()) ? FormatHelper.FormatDate(rdr["TASK_START_DATE"].ToString()) : String.Empty;
                        obj.STARTDATE = rdr["TASK_START_DATE"].ToString();

                        obj.TILLDATE = Convert.ToInt32(rdr["valid_till"]);

                        oAnnualDisclosureTask.AddObject(obj);
                    }
                    oAnnualDisclosureTask.StatusFl = true;
                    oAnnualDisclosureTask.Msg = "Annual Disclosure Task Data has been fetched successfully !";
                }
                else
                {
                    oAnnualDisclosureTask.StatusFl = false;
                    oAnnualDisclosureTask.Msg = "No data found !";
                }
                rdr.Close();
                return oAnnualDisclosureTask;
            }
            catch (Exception ex)
            {
                AnnualDisclosureTaskResponse oAnnualDisclosureTask = new AnnualDisclosureTaskResponse();
                oAnnualDisclosureTask.StatusFl = false;
                oAnnualDisclosureTask.Msg = "Processing failed, because of system error !";
                return oAnnualDisclosureTask;
            }

        }
        #endregion
        public AnnualDisclosureTaskResponse DeleteAnnualDisclosureTask(AnnualDisclosureTask objAnnualDisclosureTask)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@ID", objAnnualDisclosureTask.id);
                parameters[1] = new SqlParameter("@Mode", "Delete_ANNUAL_DISCLOSURE_TASK");
                parameters[2] = new SqlParameter("@COMPANY_ID", objAnnualDisclosureTask.companyId);
                parameters[3] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[3].Direction = ParameterDirection.Output;
                AnnualDisclosureTaskResponse oDept = new AnnualDisclosureTaskResponse();

                var result = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_ANNUAL_DISCLOSURE_TASK1", objAnnualDisclosureTask.MODULE_DATABASE, parameters);
                oDept.StatusFl = true;
                oDept.Msg = "Data has been deleted successfully !";
                oDept.AnnualDisclosureTask = objAnnualDisclosureTask;
                return oDept;
            }
            catch (Exception ex)
            {
                AnnualDisclosureTaskResponse oDept = new AnnualDisclosureTaskResponse();
                oDept.StatusFl = false;
                oDept.Msg = "Processing failed, because of system error !";
                return oDept;
            }
        }

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