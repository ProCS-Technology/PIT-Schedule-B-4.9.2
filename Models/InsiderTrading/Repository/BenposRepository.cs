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
    public class BenposRepository : IRequiresSessionState
    {
        public BenposMappingResponse GetBenposFieldMapping(BenposHeader objBenpos)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@COMPANY_ID", objBenpos.companyId);
                parameters[1] = new SqlParameter("@Mode", "GET_IT_BENPOS_MAPPING");
                parameters[2] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[2].Direction = ParameterDirection.Output;

                BenposMappingResponse oBenpos = new BenposMappingResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_BENPOS", objBenpos.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        BenposMapping obj = new BenposMapping();
                        obj.ExcelField = !String.IsNullOrEmpty(Convert.ToString(rdr["EXCEL_FIELD_NAME"])) ? Convert.ToString(rdr["EXCEL_FIELD_NAME"]) : String.Empty;
                        obj.FieldType = !String.IsNullOrEmpty(Convert.ToString(rdr["EXCEL_FIELD_TYPE"])) ? Convert.ToString(rdr["EXCEL_FIELD_TYPE"]) : String.Empty;
                        obj.TemplateType = !String.IsNullOrEmpty(Convert.ToString(rdr["TEMPLATE_TYPE"])) ? Convert.ToString(rdr["TEMPLATE_TYPE"]) : String.Empty;
                        oBenpos.AddObject(obj);
                    }
                    oBenpos.StatusFl = true;
                    oBenpos.Msg = "Success";
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
                BenposMappingResponse oBenpos = new BenposMappingResponse();
                oBenpos.StatusFl = false;
                oBenpos.Msg = "Processing failed, because of system error !";
                return oBenpos;
            }
        }
        public BenposResponse AddBenposHdr(BenposHeader objBenpos)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[13];
                parameters[0] = new SqlParameter("@HDR_ID", objBenpos.id);
                parameters[1] = new SqlParameter("@AS_OF_DATE", FormatHelper.FormatDate(objBenpos.asOfDate));
                parameters[2] = new SqlParameter("@COMPANY_ID", objBenpos.companyId);
                parameters[3] = new SqlParameter("@RESTRICTED_COMPANY_ID", objBenpos.restrictedCompany.ID);
                parameters[4] = new SqlParameter("@FILENAME", objBenpos.fileName);
                parameters[5] = new SqlParameter("@CREATED_BY", objBenpos.createdBy);
                parameters[6] = new SqlParameter("@MODE", "INSERT_IT_BENPOS_HDR");
                parameters[7] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[7].Direction = ParameterDirection.Output;
                parameters[8] = new SqlParameter("@TYPE", objBenpos.type);
                parameters[9] = new SqlParameter("@VWAP", objBenpos.vwap);
                parameters[10] = new SqlParameter("@FILENAME_ESOP", objBenpos.fileNameESOP);
                parameters[11] = new SqlParameter("@FromDate", FormatHelper.FormatDate(objBenpos.fromDate));
                parameters[12] = new SqlParameter("@ToDate", FormatHelper.FormatDate(objBenpos.toDate));

                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_BENPOS", objBenpos.MODULE_DATABASE, parameters);
                var obj = parameters[7].Value;
                BenposResponse oBenpos = new BenposResponse();
                if ((Int32)obj == 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        BenposHeader benpos = new BenposHeader();
                        benpos.id = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["HDR_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["HDR_ID"]) : 0;
                        benpos.esopHdrId = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["ESOP_HDR_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["ESOP_HDR_ID"]) : 0;
                        oBenpos.BenposHeader = benpos;
                    }
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
                BenposResponse oBenpos = new BenposResponse();
                oBenpos.StatusFl = false;
                oBenpos.Msg = "Processing failed, because of system error !";
                return oBenpos;
            }
        }
        public BenposResponse GetAllBenposHdr(BenposHeader objBenpos)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@COMPANY_ID", objBenpos.companyId);
                parameters[1] = new SqlParameter("@Mode", "GET_IT_BENPOS_HDR");
                parameters[2] = new SqlParameter("@CREATED_BY", objBenpos.createdBy);
                parameters[3] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[3].Direction = ParameterDirection.Output;

                BenposResponse oBenpos = new BenposResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_BENPOS", objBenpos.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        BenposHeader obj = new BenposHeader();
                        obj.id = Convert.ToInt32(rdr["HDR_ID"]);
                        obj.asOfDate = !String.IsNullOrEmpty(Convert.ToString(rdr["AS_OF_DATE"])) ? Convert.ToString(rdr["AS_OF_DATE"]) : String.Empty;
                        obj.fromDate = !String.IsNullOrEmpty(Convert.ToString(rdr["START_DT"])) ? rdr["START_DT"].ToString() : String.Empty;
                        obj.toDate = !String.IsNullOrEmpty(Convert.ToString(rdr["END_DT"])) ? rdr["END_DT"].ToString() : String.Empty;
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
                BenposResponse oBenpos = new BenposResponse();
                oBenpos.StatusFl = false;
                oBenpos.Msg = "Processing failed, because of system error !";
                return oBenpos;
            }
        }
        public BenposResponse UpdateBenposDetail(BenposHeader objBenpos)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[6];
                parameters[0] = new SqlParameter("@HDR_ID", objBenpos.id);
                parameters[1] = new SqlParameter("@AS_OF_DATE", FormatHelper.FormatDate(objBenpos.asOfDate));
                parameters[2] = new SqlParameter("@COMPANY_ID", objBenpos.companyId);
                parameters[3] = new SqlParameter("@MODE", "UPDATE_IT_BENPOS_DETAIL");
                parameters[4] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[4].Direction = ParameterDirection.Output;
                parameters[5] = new SqlParameter("@EsopId", objBenpos.esopHdrId);

                SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_BENPOS", objBenpos.MODULE_DATABASE, parameters);
                var obj = parameters[4].Value;
                BenposResponse oBenpos = new BenposResponse();
                if ((Int32)obj == 0)
                {
                    oBenpos.StatusFl = true;
                    oBenpos.Msg = "Data has been updated successfully !";
                }
                else
                {
                    oBenpos.StatusFl = false;
                    oBenpos.Msg = "Processing failed, because of system error";
                }
                return oBenpos;
            }
            catch (Exception ex)
            {
                BenposResponse oBenpos = new BenposResponse();
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
                parameters[1] = new SqlParameter("@AS_OF_DATE", FormatHelper.FormatDate(objBenpos.asOfDate));
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
        public bool isThreshollimitmatch(Int32 HDRID , string Module_db)
        {
            bool isThreshollimitmatch = false;
            try
            {
               

                SqlParameter[] parameters = new SqlParameter[4];
                parameters[1] = new SqlParameter("@HDR_ID", HDRID);      
                parameters[2] = new SqlParameter("@MODE", "VALIDATE_BENPOS_THRSHOLD_LIMIT");
                parameters[3] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[3].Direction = ParameterDirection.Output;

                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_BENPOS", Module_db, parameters);
                var obj = parameters[3].Value;
                if ((Int32)obj == 0)
                {
                    isThreshollimitmatch = false;
                }
                else
                {
                    isThreshollimitmatch = true;
                }
                return isThreshollimitmatch = true;
            }
            catch (Exception ex)
            {
                return isThreshollimitmatch;
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
        public BenposResponse AddEsopHdr(BenposHeader objBenpos)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[14];
                parameters[0] = new SqlParameter("@HDR_ID", objBenpos.id);
                parameters[1] = new SqlParameter("@AS_OF_DATE", FormatHelper.FormatDate(objBenpos.asOfDate));
                parameters[2] = new SqlParameter("@COMPANY_ID", objBenpos.companyId);
                parameters[3] = new SqlParameter("@RESTRICTED_COMPANY_ID", objBenpos.restrictedCompany.ID);
                parameters[4] = new SqlParameter("@FILENAME", objBenpos.fileName);
                parameters[5] = new SqlParameter("@CREATED_BY", objBenpos.createdBy);
                parameters[6] = new SqlParameter("@MODE", "INSERT_IT_BENPOS_HDR");
                parameters[7] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[7].Direction = ParameterDirection.Output;
                parameters[8] = new SqlParameter("@TYPE", objBenpos.type);
                parameters[9] = new SqlParameter("@VWAP", objBenpos.vwap);
                parameters[10] = new SqlParameter("@FILENAME_ESOP", objBenpos.fileNameESOP);
                parameters[11] = new SqlParameter("@FromDate", FormatHelper.FormatDate(objBenpos.fromDate));
                parameters[12] = new SqlParameter("@ToDate", FormatHelper.FormatDate(objBenpos.toDate));
                parameters[13] = new SqlParameter("@CORPORATE_ACTION", objBenpos.Corporate_Action);

                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_BENPOS", objBenpos.MODULE_DATABASE, parameters);
                var obj = parameters[7].Value;
                BenposResponse oBenpos = new BenposResponse();
                if ((Int32)obj == 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        BenposHeader benpos = new BenposHeader();
                        benpos.id = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["HDR_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["HDR_ID"]) : 0;
                        benpos.esopHdrId = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["ESOP_HDR_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["ESOP_HDR_ID"]) : 0;
                        oBenpos.BenposHeader = benpos;
                    }
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
                BenposResponse oBenpos = new BenposResponse();
                oBenpos.StatusFl = false;
                oBenpos.Msg = "Processing failed, because of system error !";
                return oBenpos;
            }
        }
        public BenposResponse SaveEsop(BenposHeader objBenpos)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[9];
                parameters[0] = new SqlParameter("@HDR_ID", objBenpos.id);
                parameters[1] = new SqlParameter("@COMPANY_ID", objBenpos.companyId);
                parameters[2] = new SqlParameter("@RESTRICTED_COMPANY_ID", objBenpos.restrictedCompany.ID);
                parameters[3] = new SqlParameter("@FILENAME", objBenpos.fileName);
                parameters[4] = new SqlParameter("@CREATED_BY", objBenpos.createdBy);
                parameters[5] = new SqlParameter("@CORPORATE_ACTION", objBenpos.Corporate_Action);
                parameters[6] = new SqlParameter("@MODE", "INSERT_ESOP");
                parameters[7] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[7].Direction = ParameterDirection.Output;
                parameters[8] = new SqlParameter("@FILENAME_ESOP", objBenpos.fileNameESOP);
                //parameters[8] = new SqlParameter("@CORPORATE_ACTION", objBenpos.Corporate_Action);


                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_BENPOS", objBenpos.MODULE_DATABASE, parameters);
                var obj = parameters[7].Value;
                BenposResponse oBenpos = new BenposResponse();
                if ((Int32)obj == 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        BenposHeader benpos = new BenposHeader();
                        benpos.esopHdrId = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["ESOP_HDR_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["ESOP_HDR_ID"]) : 0;
                        oBenpos.BenposHeader = benpos;
                    }
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
                BenposResponse oBenpos = new BenposResponse();
                oBenpos.StatusFl = false;
                oBenpos.Msg = "Processing failed, because of system error !";
                return oBenpos;
            }
        }
        public BenposResponse GetAllEsopHdr(BenposHeader objBenpos)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@COMPANY_ID", objBenpos.companyId);
                parameters[1] = new SqlParameter("@Mode", "GET_IT_ESOP_HDR");
                parameters[2] = new SqlParameter("@CREATED_BY", objBenpos.createdBy);
                parameters[3] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[3].Direction = ParameterDirection.Output;

                BenposResponse oBenpos = new BenposResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_BENPOS", objBenpos.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        BenposHeader obj = new BenposHeader();

                        obj.id = Convert.ToInt32(rdr["HDR_ID"]);
                        obj.asOfDate = !String.IsNullOrEmpty(Convert.ToString(rdr["UPLOADED_ON"])) ? rdr["UPLOADED_ON"].ToString() : String.Empty;
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
                BenposResponse oBenpos = new BenposResponse();
                oBenpos.StatusFl = false;
                oBenpos.Msg = "Processing failed, because of system error !";
                return oBenpos;
            }
        }
        public BenposResponse GetEsopListByUser(BenposHeader objBenpos)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@Mode", "GET_IT_ESOP_LIST_BY_USER");
                parameters[1] = new SqlParameter("@CREATED_BY", objBenpos.createdBy);
                parameters[2] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[2].Direction = ParameterDirection.Output;

                BenposResponse oBenpos = new BenposResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_BENPOS", objBenpos.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        BenposHeader obj = new BenposHeader();
                        obj.id = Convert.ToInt32(rdr["ID"]);
                        obj.asOfDate = !String.IsNullOrEmpty(Convert.ToString(rdr["DATE"])) ? rdr["DATE"].ToString() : String.Empty;
                        obj.FolioNo = !String.IsNullOrEmpty(Convert.ToString(rdr["FOLIO_NO"])) ? Convert.ToString(rdr["FOLIO_NO"]) : String.Empty;
                        obj.PanNo = !String.IsNullOrEmpty(Convert.ToString(rdr["PAN_NUMBER"])) ? Convert.ToString(rdr["PAN_NUMBER"]) : String.Empty;
                        obj.Qty = !String.IsNullOrEmpty(Convert.ToString(rdr["QTY"])) ? FormatHelper.FormatNumber(Convert.ToString(rdr["QTY"])) : String.Empty;
                        obj.ESOPAmount = !String.IsNullOrEmpty(Convert.ToString(rdr["AMOUNT"])) ? String.Format("{0:#,###0}", rdr["AMOUNT"]) : String.Empty;
                        obj.Holding = !String.IsNullOrEmpty(Convert.ToString(rdr["HOLDING"])) ? Convert.ToString(rdr["HOLDING"]) : String.Empty;
                        obj.Rate = !String.IsNullOrEmpty(Convert.ToString(rdr["RATE"])) ? string.Format("{0:#.00}", rdr["RATE"]) : String.Empty;
                        oBenpos.AddObject(obj);
                    }
                    oBenpos.StatusFl = true;
                    oBenpos.Msg = "Data has been fetched successfully !";
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
                BenposResponse oBenpos = new BenposResponse();
                oBenpos.StatusFl = false;
                oBenpos.Msg = "Processing failed, because of system error !";
                return oBenpos;
            }
        }
        public BenposResponse UpdateEsopAmount(BenposHeader objBenpos)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[5];
                parameters[0] = new SqlParameter("@HDR_ID", objBenpos.esopHdrId);
                parameters[1] = new SqlParameter("@MODE", "UPDATE_ESOP_DETAIL");
                parameters[2] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[2].Direction = ParameterDirection.Output;
                parameters[3] = new SqlParameter("@COMPANY_ID", objBenpos.companyId);
                parameters[4] = new SqlParameter("@CORPORATE_ACTION", objBenpos.Corporate_Action);

                SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_BENPOS", objBenpos.MODULE_DATABASE, parameters);
                var obj = parameters[2].Value;
                BenposResponse oBenpos = new BenposResponse();
                if ((Int32)obj == 0)
                {
                    oBenpos.StatusFl = true;
                    oBenpos.Msg = "Data has been updated successfully !";
                }
                else
                {
                    oBenpos.StatusFl = false;
                    oBenpos.Msg = "Processing failed, because of system error";
                }
                return oBenpos;
            }
            catch (Exception ex)
            {
                BenposResponse oBenpos = new BenposResponse();
                oBenpos.StatusFl = false;
                oBenpos.Msg = "Processing failed, because of system error !";
                return oBenpos;
            }
        }
        public BenposResponse GetCorporateListById(BenposHeader objCorporateAction)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@Mode", "GET_ESOP_CORPORATE_LIST_BY_ID");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objCorporateAction.companyId);

                BenposResponse oCorporate = new BenposResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_BENPOS", objCorporateAction.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        BenposHeader obj = new BenposHeader();
                        obj.id = Convert.ToInt32(rdr["ID"]);
                        obj.Corporate_Action = (!String.IsNullOrEmpty(Convert.ToString(rdr["CORPORATE_ACTION"]))) ? Convert.ToString(rdr["CORPORATE_ACTION"]) : String.Empty;
                        oCorporate.AddObject(obj);
                    }
                    oCorporate.StatusFl = true;
                    oCorporate.Msg = "Data has been fetched successfully !";
                }
                else
                {
                    oCorporate.StatusFl = false;
                    oCorporate.Msg = "No data found !";
                }
                rdr.Close();
                return oCorporate;
            }
            catch (Exception ex)
            {
                BenposResponse oCorporate = new BenposResponse();
                oCorporate.StatusFl = false;
                oCorporate.Msg = "Processing failed, because of system error !";
                return oCorporate;
            }
        }
        public void deleteBenposHdr(Int32 HDRID ,string Module_db)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@HDR_ID", HDRID);
                parameters[1] = new SqlParameter("@Mode", "DELETE_BENPOS_HDR");
                parameters[2] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[2].Direction = ParameterDirection.Output;

                SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_BENPOS", Module_db, parameters);

            }
            catch (Exception ex)
            {
               
            }
        }
        public BenposResponse DeleteBenposDetail(BenposHeader objBenpos)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@HDR_ID", objBenpos.id);
                parameters[1] = new SqlParameter("@MODE", "DELETE_BENPOS");
                parameters[2] = new SqlParameter("@SET_COUNT", -1);
                parameters[2].Direction = ParameterDirection.Output;

                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_BENPOS", objBenpos.MODULE_DATABASE, parameters);
                var obj = parameters[2].Value;
                BenposResponse oBenpos = new BenposResponse();
                if ((Int32)obj == 0)
                {
                    oBenpos.StatusFl = true;
                    oBenpos.Msg = "Data has been updated successfully !";
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
                BenposResponse oBenpos = new BenposResponse();
                oBenpos.StatusFl = false;
                oBenpos.Msg = "Processing failed, because of system error !";
                return oBenpos;
            }
        }   
    }
}