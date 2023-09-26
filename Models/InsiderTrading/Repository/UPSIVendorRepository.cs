using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ProcsDLL.Models.InsiderTrading.Repository
{
    public class UPSIVendorRepository
    {


        public UPSIVendorResponce GetUPSIVendorList(UPSIVendor ovendor)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@Mode", "GET_VENDOR_LIST");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", ovendor.companyId);

                List<UPSIVendor> oVendor = new List<UPSIVendor>();
                UPSIVendorResponce res = new UPSIVendorResponce();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_VENDOR", ovendor.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        UPSIVendor obj = new UPSIVendor();
                        obj.VendorId = !String.IsNullOrEmpty(rdr["VENDOR_ID"].ToString()) ? Convert.ToString(rdr["VENDOR_ID"]) : String.Empty;
                        obj.vendorName = !String.IsNullOrEmpty(rdr["VENDOR_NAME"].ToString()) ? Convert.ToString(rdr["VENDOR_NAME"]) : String.Empty;
                        obj.VendorStatus = !String.IsNullOrEmpty(rdr["STATUS"].ToString()) ? Convert.ToString(rdr["STATUS"]) : String.Empty;

                        DateTime dt = Convert.ToDateTime(rdr["CREATED_ON"]);
                        obj.CreatedOn = dt.ToString("dd/MM/yyyy");
                        obj.CreatedBy = !String.IsNullOrEmpty(rdr["CREATED_BY"].ToString()) ? Convert.ToString(rdr["CREATED_BY"]) : String.Empty;
                        obj.companyId = Convert.ToInt32(rdr["COMPANY_ID"]);
                        oVendor.Add(obj);
                    }

                    res.listUPSIVendor = oVendor;
                    res.StatusFl = true;
                    res.Msg = "Data has been fetched successfully !";
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
                UPSIVendorResponce oCategory = new UPSIVendorResponce();
                oCategory.StatusFl = false;
                oCategory.Msg = "Processing failed, because of system error !";
                return oCategory;
            }
        }
        public UPSIVendorResponce ListUPSIVendor_ById(UPSIVendor ovendor)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@Mode", "GET_VENDOR_LIST_BY_ID");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", ovendor.companyId);
                parameters[3] = new SqlParameter("@UPSI_VENDOR_ID", ovendor.VendorId);

                List<UPSIVendor> oVendor = new List<UPSIVendor>();
                UPSIVendorResponce res = new UPSIVendorResponce();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_VENDOR", ovendor.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        UPSIVendor obj = new UPSIVendor();
                        obj.VendorId = !String.IsNullOrEmpty(rdr["VENDOR_ID"].ToString()) ? Convert.ToString(rdr["VENDOR_ID"]) : String.Empty;
                        obj.vendorName = !String.IsNullOrEmpty(rdr["VENDOR_NAME"].ToString()) ? Convert.ToString(rdr["VENDOR_NAME"]) : String.Empty;
                        obj.VendorStatus = !String.IsNullOrEmpty(rdr["STATUS"].ToString()) ? Convert.ToString(rdr["STATUS"]) : String.Empty;

                        DateTime dt = Convert.ToDateTime(rdr["CREATED_ON"]);
                        obj.CreatedOn = dt.ToString("dd/MM/yyyy");
                        obj.CreatedBy = !String.IsNullOrEmpty(rdr["CREATED_BY"].ToString()) ? Convert.ToString(rdr["CREATED_BY"]) : String.Empty;
                        obj.companyId = Convert.ToInt32(rdr["COMPANY_ID"]);
                        obj.fileName = !String.IsNullOrEmpty(rdr["DOCUMENTS"].ToString()) ? Convert.ToString(rdr["DOCUMENTS"]) : String.Empty;
                        obj.PanNo = !String.IsNullOrEmpty(rdr["PANNO"].ToString()) ? Convert.ToString(rdr["PANNO"]) : String.Empty;
                        oVendor.Add(obj);
                    }

                    res.listUPSIVendor = oVendor;
                    res.StatusFl = true;
                    res.Msg = "Data has been fetched successfully !";
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
                UPSIVendorResponce oCategory = new UPSIVendorResponce();
                oCategory.StatusFl = false;
                oCategory.Msg = "Processing failed, because of system error !";
                return oCategory;
            }
        }

        public UPSIVendorResponce SaveUPSIVendor(UPSIVendor objvendor)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[9];
                parameters[0] = new SqlParameter("@MODE", "INSERT_VENDOR");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@UPSI_VENDOR_ID", objvendor.VendorId);
                parameters[3] = new SqlParameter("@UPSI_VENDOR_NAME", objvendor.vendorName);
                parameters[4] = new SqlParameter("@CREATED_BY", objvendor.CreatedBy);
                parameters[5] = new SqlParameter("@COMPANY_ID", objvendor.companyId);
                parameters[6] = new SqlParameter("@UPSI_VENDOR_STATUS", objvendor.VendorStatus);
                parameters[7] = new SqlParameter("@DOCUMENTS", objvendor.fileName);
                parameters[8] = new SqlParameter("@PAN_NO", objvendor.PanNo);


                UPSIVendorResponce oVendor = new UPSIVendorResponce();
                int status = SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_VENDOR", objvendor.MODULE_DATABASE, parameters);
                var count = parameters[1].Value;
                Int32 set_count = Convert.ToInt32(count);
                if (set_count == 0)
                {
                    oVendor.StatusFl = false;
                    oVendor.Msg = "Vendor already Exists !";
                    return oVendor;
                }
                if (status > 0)
                {

                    oVendor.StatusFl = true;
                    oVendor.Msg = "Data has been Saved successfully !";
                }
                else
                {
                    oVendor.StatusFl = false;
                    oVendor.Msg = "No data found !";
                }

                return oVendor;
            }
            catch (Exception ex)
            {
                UPSIVendorResponce oVendor = new UPSIVendorResponce();
                oVendor.StatusFl = false;
                oVendor.Msg = ex.Message;
                //oVendor.Msg = "Processing failed, because of system error !";
                return oVendor;
            }
        }

        public UPSIVendorResponce UpdateUPSIVendor(UPSIVendor objvendor)
        {
            try
            {

                SqlParameter[] parameters = new SqlParameter[9];
                parameters[0] = new SqlParameter("@MODE", "UPDATE_VENDOR");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@UPSI_VENDOR_ID", objvendor.VendorId);
                parameters[3] = new SqlParameter("@UPSI_VENDOR_NAME", objvendor.vendorName);
                parameters[4] = new SqlParameter("@CREATED_BY", objvendor.CreatedBy);
                parameters[5] = new SqlParameter("@COMPANY_ID", objvendor.companyId);
                parameters[6] = new SqlParameter("@UPSI_VENDOR_STATUS", objvendor.VendorStatus);
                parameters[7] = new SqlParameter("@DOCUMENTS", objvendor.fileName);
                parameters[8] = new SqlParameter("@PAN_NO", objvendor.PanNo);
                UPSIVendorResponce oVendor = new UPSIVendorResponce();
                int status = SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_VENDOR", objvendor.MODULE_DATABASE, parameters);

                if (status > 0)
                {

                    oVendor.StatusFl = true;
                    oVendor.Msg = "Data has been Updated successfully !";
                }
                else
                {
                    oVendor.StatusFl = false;
                    oVendor.Msg = "No data found !";
                }

                return oVendor;
            }
            catch (Exception ex)
            {
                UPSIVendorResponce oCategory = new UPSIVendorResponce();
                oCategory.StatusFl = false;
                oCategory.Msg = "Processing failed, because of system error !";
                return oCategory;
            }
        }



        public UPSIVendorResponce DeleteUPSIVendor_ById(UPSIVendor objvendor)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@MODE", "DELETE_VENDOR");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objvendor.companyId);
                parameters[3] = new SqlParameter("@UPSI_VENDOR_ID", objvendor.VendorId);

                UPSIVendorResponce oVendor = new UPSIVendorResponce();
                Int32 ST = SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_VENDOR", objvendor.MODULE_DATABASE, parameters);

                var check = parameters[1].Value;
                int is_check = Convert.ToInt32(check);
                if (is_check == 0)
                {
                    oVendor.StatusFl = true;
                    oVendor.Msg = "Unable to Delete. This Category is Used in Higher Component!!";

                }
                else
                {
                    if (ST > 0)
                    {

                        oVendor.StatusFl = true;
                        oVendor.Msg = "Data has been Deleted successfully !";
                    }
                    else
                    {
                        oVendor.StatusFl = false;
                        oVendor.Msg = "No data found !";
                    }
                }



                return oVendor;
            }
            catch (Exception ex)
            {
                UPSIVendorResponce oVendor = new UPSIVendorResponce();
                oVendor.StatusFl = false;
                oVendor.Msg = "Processing failed, because of system error !";
                return oVendor;
            }
        }


    }
}