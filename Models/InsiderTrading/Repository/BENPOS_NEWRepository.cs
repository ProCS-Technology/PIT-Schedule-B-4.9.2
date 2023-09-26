using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.SessionState;

namespace ProcsDLL.Models.InsiderTrading.Repository
{
    public class BENPOS_NEWRepository : IRequiresSessionState
    {
        public BENPOS_NEWResponse GetAllBenposHdr(BenposHeader objBenpos)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@COMPANY_ID", objBenpos.companyId);
                parameters[1] = new SqlParameter("@Mode", "GET_IT_BENPOS_HDR");
                parameters[2] = new SqlParameter("@CREATED_BY", objBenpos.createdBy);
                parameters[3] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[3].Direction = ParameterDirection.Output;

                BENPOS_NEWResponse oBenpos = new BENPOS_NEWResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_BENPOS", objBenpos.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        BenposHeader obj = new BenposHeader();

                        obj.id = Convert.ToInt32(rdr["HDR_ID"]);
                        obj.asOfDate = !String.IsNullOrEmpty(Convert.ToString(rdr["AS_OF_DATE"])) ? Convert.ToString(rdr["AS_OF_DATE"]) : String.Empty;

                        obj.fromDate = !String.IsNullOrEmpty(Convert.ToString(rdr["START_DT"])) ? Convert.ToDateTime(Convert.ToString(rdr["START_DT"])).ToString("yyyy/MM/dd") : String.Empty;
                        obj.toDate = !String.IsNullOrEmpty(Convert.ToString(rdr["END_DT"])) ? Convert.ToDateTime(Convert.ToString(rdr["END_DT"])).ToString("yyyy/MM/dd") : String.Empty;

                        obj.fileName = !String.IsNullOrEmpty(Convert.ToString(rdr["FILENAME"])) ? Convert.ToString(rdr["FILENAME"]) : String.Empty;
                        obj.restrictedCompany = new RestrictedCompanies
                        {
                            companyName = !String.IsNullOrEmpty(Convert.ToString(rdr["COMPANY_NAME"])) ? Convert.ToString(rdr["COMPANY_NAME"]) : String.Empty
                        };
                        obj.type = !String.IsNullOrEmpty(Convert.ToString(rdr["TYPE"])) ? Convert.ToString(rdr["TYPE"]) : String.Empty;

                        oBenpos.AddObject(obj);
                    }
                    oBenpos.StatusFl = true;
                    oBenpos.Msg = "Document Data has been fetched successfully !";
                }
                else
                {
                    oBenpos.StatusFl = false;
                    oBenpos.Msg = "No data found !";
                }
                rdr.Close();
                return oBenpos;
            }
            catch (Exception ex)
            {
                BENPOS_NEWResponse oBenpos = new BENPOS_NEWResponse();
                oBenpos.StatusFl = false;
                oBenpos.Msg = "Processing failed, because of system error !";
                return oBenpos;
            }
        }

        public BENPOS_NEWResponse AddBenposHdr(BenposHeader objBenpos)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[12];
                parameters[0] = new SqlParameter("@HDR_ID", objBenpos.id);
                parameters[1] = new SqlParameter("@AS_OF_DATE", ConvertDate(objBenpos.asOfDate));
                parameters[2] = new SqlParameter("@COMPANY_ID", objBenpos.companyId);
                parameters[3] = new SqlParameter("@RESTRICTED_COMPANY_ID", objBenpos.restrictedCompany.ID);
                parameters[4] = new SqlParameter("@FILENAME", objBenpos.fileName);
                parameters[5] = new SqlParameter("@CREATED_BY", objBenpos.createdBy);
                parameters[6] = new SqlParameter("@MODE", "INSERT_IT_BENPOS_HDR_NEW");
                parameters[7] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[7].Direction = ParameterDirection.Output;
                parameters[8] = new SqlParameter("@TYPE", objBenpos.type);
                parameters[9] = new SqlParameter("@VWAP", objBenpos.vwap);
                parameters[10] = new SqlParameter("@FromDate", ConvertDate(objBenpos.fromDate));
                parameters[11] = new SqlParameter("@ToDate", ConvertDate(objBenpos.toDate));

                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_BENPOS", objBenpos.MODULE_DATABASE, parameters);
                var obj = parameters[7].Value;
                BENPOS_NEWResponse oBenpos = new BENPOS_NEWResponse();
                if ((Int32)obj > 0)
               
                {
                    oBenpos.StatusFl = true;
                    oBenpos.Msg = "Data has been saved successfully !";
                }
                   
               
                else
                {
                    oBenpos.StatusFl = false;
                    oBenpos.Msg = "Processing failed, because of system error !";
                }
                return oBenpos;
            }
            catch (Exception ex)
            {
                BENPOS_NEWResponse oBenpos = new BENPOS_NEWResponse();
                oBenpos.StatusFl = false;
                oBenpos.Msg = "Processing failed, because of system error !";
                return oBenpos;
            }
        }
        public bool ValidateBenposAsOfDate(BenposHeader objBenpos)
        {
            bool isValidAsOfDate = false;
            try
            {
                SqlParameter[] parameters = new SqlParameter[5];
                parameters[1] = new SqlParameter("@AS_OF_DATE", ConvertDate(objBenpos.asOfDate));
                parameters[2] = new SqlParameter("@COMPANY_ID", objBenpos.companyId);
                parameters[3] = new SqlParameter("@MODE", "VALIDATE_BENPOS_AS_OF_DATE");
                parameters[4] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[4].Direction = ParameterDirection.Output;

                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_BENPOS", objBenpos.MODULE_DATABASE, parameters);
                var obj = parameters[4].Value;
                if ((Int32)obj == 0)
                {
                    isValidAsOfDate = true;
                }
                else
                {
                    isValidAsOfDate = false;
                }
                return isValidAsOfDate;
            }
            catch (Exception ex)
            {
                return isValidAsOfDate;
            }
        }

        #region "Date Conversion"
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
        #endregion
    }
}

