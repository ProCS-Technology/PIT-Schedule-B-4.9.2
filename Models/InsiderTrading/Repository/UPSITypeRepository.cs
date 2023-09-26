using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace ProcsDLL.Models.InsiderTrading.Repository
{
    public class UPSITypeRepository
    {
        public UPSITypeResponse GetUPSITypeList(UPSIType objUPSITyp)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@CompanyId", objUPSITyp.CompanyId);
                parameters[1] = new SqlParameter("@Mode", "GET_UPSI_TYPE");
                parameters[2] = new SqlParameter("@UserLogin", objUPSITyp.CreatedBy);

                UPSITypeResponse oGrp = new UPSITypeResponse();
                DataSet dsUPSI = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_UPSI_TYPE_OPERATION", objUPSITyp.MODULE_DATABASE, parameters);
                DataTable dtUPSI = dsUPSI.Tables[0];
                DataTable dtKeywords = dsUPSI.Tables[1];

                if (dtUPSI.Rows.Count > 0)
                {
                    foreach (DataRow drUPSI in dtUPSI.Rows)
                    {
                        UPSIType obj = new UPSIType();
                        obj.TypeId = Convert.ToInt32(drUPSI["UPSI_TYPE_ID"]);
                        obj.TypeNm = Convert.ToString(drUPSI["UPSI_TYPE_NM"]);
                        obj.Status = Convert.ToString(drUPSI["STATUS"]);

                        DataRow[] drKeywords = dtKeywords.Select("UPSI_TYPE_ID=" + Convert.ToInt32(drUPSI["UPSI_TYPE_ID"]));
                        List<UPSIKeywords> lstKeywords = new List<UPSIKeywords>();
                        foreach (DataRow drKeyword in drKeywords)
                        {
                            UPSIKeywords o = new UPSIKeywords();
                            o.keyword = Convert.ToString(drKeyword["KEYWORD"]);
                            o.sequence = Convert.ToInt32(drKeyword["MATCH_ORDER"]);
                            lstKeywords.Add(o);
                        }
                        obj.Keywords = lstKeywords;
                        oGrp.AddObject(obj);
                    }
                    oGrp.StatusFl = true;
                    oGrp.Msg = "UPSI Type has been fetched successfully !";
                }
                else
                {
                    oGrp.StatusFl = false;
                    oGrp.Msg = "No data found !";
                }
                return oGrp;
            }
            catch (Exception ex)
            {
                UPSITypeResponse oGrp = new UPSITypeResponse();
                oGrp.StatusFl = false;
                oGrp.Msg = "Processing failed, because of system error !";
                return oGrp;
            }
        }
        public UPSITypeResponse AddUPSIType(UPSIType objUPSITyp)
        {
            try
            {
                UPSITypeResponse oGrp = new UPSITypeResponse();
                DataTable dtUpsiKeywords = new DataTable();
                dtUpsiKeywords.Columns.Add("Keyword", typeof(string));
                dtUpsiKeywords.Columns.Add("Seq", typeof(int));
                foreach (UPSIKeywords kword in objUPSITyp.Keywords)
                {
                    DataRow dr = dtUpsiKeywords.NewRow();
                    dr["Keyword"] = kword.keyword;
                    dr["Seq"] = kword.sequence;
                    dtUpsiKeywords.Rows.Add(dr);
                }

                SqlParameter[] parameters = new SqlParameter[7];
                parameters[0] = new SqlParameter("@CompanyId", objUPSITyp.CompanyId);
                parameters[1] = new SqlParameter("@Mode", "INSERT");
                parameters[2] = new SqlParameter("@UserLogin", objUPSITyp.CreatedBy);
                parameters[3] = new SqlParameter("@UpsiNm", objUPSITyp.TypeNm);
                parameters[4] = new SqlParameter("@Status", objUPSITyp.Status);
                parameters[5] = new SqlParameter("@UpsiId", objUPSITyp.TypeId);
                parameters[6] = new SqlParameter("@UpsiKeywords", dtUpsiKeywords);

                Object obj = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_UPSI_TYPE_OPERATION", objUPSITyp.MODULE_DATABASE, parameters);
                if ((string)obj == "Success")
                {
                    oGrp.StatusFl = true;
                    oGrp.Msg = "Data Saved Successfully !";
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
                UPSITypeResponse oGrp = new UPSITypeResponse();
                oGrp.StatusFl = false;
                oGrp.Msg = "Processing failed, because of system error !";
                return oGrp;
            }
        }
        public UPSITypeResponse UpdateUPSIType(UPSIType objUPSITyp)
        {
            try
            {
                UPSITypeResponse oGrp = new UPSITypeResponse();
                DataTable dtUpsiKeywords = new DataTable();
                dtUpsiKeywords.Columns.Add("Keyword", typeof(string));
                dtUpsiKeywords.Columns.Add("Seq", typeof(int));
                foreach (UPSIKeywords kword in objUPSITyp.Keywords)
                {
                    DataRow dr = dtUpsiKeywords.NewRow();
                    dr["Keyword"] = kword.keyword;
                    dr["Seq"] = kword.sequence;
                    dtUpsiKeywords.Rows.Add(dr);
                }

                SqlParameter[] parameters = new SqlParameter[7];
                parameters[0] = new SqlParameter("@CompanyId", objUPSITyp.CompanyId);
                parameters[1] = new SqlParameter("@Mode", "UPDATE");
                parameters[2] = new SqlParameter("@UserLogin", objUPSITyp.CreatedBy);
                parameters[3] = new SqlParameter("@UpsiNm", objUPSITyp.TypeNm);
                parameters[4] = new SqlParameter("@Status", objUPSITyp.Status);
                parameters[5] = new SqlParameter("@UpsiId", objUPSITyp.TypeId);
                parameters[6] = new SqlParameter("@UpsiKeywords", dtUpsiKeywords);

                Object obj = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_UPSI_TYPE_OPERATION", objUPSITyp.MODULE_DATABASE, parameters);
                if ((string)obj == "Success")
                {
                    oGrp.StatusFl = true;
                    oGrp.Msg = "Data Updated Successfully !";
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
                UPSITypeResponse oGrp = new UPSITypeResponse();
                oGrp.StatusFl = false;
                oGrp.Msg = "Processing failed, because of system error !";
                return oGrp;
            }
        }
        public UPSITypeResponse GetUPSITypeById(UPSIType objUPSITyp)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@CompanyId", objUPSITyp.CompanyId);
                parameters[1] = new SqlParameter("@Mode", "GET_UPSI_TYPE_BY_ID");
                parameters[2] = new SqlParameter("@UserLogin", objUPSITyp.CreatedBy);
                parameters[2] = new SqlParameter("@UpsiId", objUPSITyp.TypeId);

                UPSITypeResponse oGrp = new UPSITypeResponse();
                DataSet dsUPSI = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_UPSI_TYPE_OPERATION", objUPSITyp.MODULE_DATABASE, parameters);

                DataTable dtUPSI = dsUPSI.Tables[0];
                DataTable dtKeywords = dsUPSI.Tables[1];

                if (dtUPSI.Rows.Count > 0)
                {
                    foreach (DataRow drUPSI in dtUPSI.Rows)
                    {
                        UPSIType obj = new UPSIType();
                        obj.TypeId = Convert.ToInt32(drUPSI["UPSI_TYPE_ID"]);
                        obj.TypeNm = Convert.ToString(drUPSI["UPSI_TYPE_NM"]);
                        obj.Status = Convert.ToString(drUPSI["STATUS"]);

                        DataRow[] drKeywords = dtKeywords.Select("UPSI_TYPE_ID=" + Convert.ToInt32(drUPSI["UPSI_TYPE_ID"]));
                        List<UPSIKeywords> lstKeywords = new List<UPSIKeywords>();
                        foreach (DataRow drKeyword in drKeywords)
                        {
                            UPSIKeywords o = new UPSIKeywords();
                            o.keyword = Convert.ToString(drKeyword["KEYWORD"]);
                            o.sequence = Convert.ToInt32(drKeyword["MATCH_ORDER"]);
                            lstKeywords.Add(o);
                        }
                        obj.Keywords = lstKeywords;
                        //oGrp.AddObject(obj);
                        oGrp.UPSITyp = obj;
                    }
                    oGrp.StatusFl = true;
                    oGrp.Msg = "UPSI Type has been fetched successfully !";
                }
                else
                {
                    oGrp.StatusFl = false;
                    oGrp.Msg = "No data found !";
                }
                return oGrp;
            }
            catch (Exception ex)
            {
                UPSITypeResponse oGrp = new UPSITypeResponse();
                oGrp.StatusFl = false;
                oGrp.Msg = "Processing failed, because of system error !";
                return oGrp;
            }
        }
        public UPSITypeResponse GetModeofCommunication(UPSIType objUPSITyp)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@Mode", "Get_UPSI_COMMUNICATION_List");
                
                UPSITypeResponse oupsicommtype = new UPSITypeResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_UPSI_TYPE_OPERATION", objUPSITyp.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        UPSIType obj = new UPSIType();
                        obj.COMMTYPE_ID = Convert.ToInt32(rdr.GetValue(0));
                        obj.COMMTYPE_NAME = Convert.ToString(rdr.GetValue(1));
                        oupsicommtype.AddObject(obj);
                    }
                    oupsicommtype.StatusFl = true;
                    oupsicommtype.Msg = "Data has been fetched successfully !";
                }
                else
                {
                    oupsicommtype.StatusFl = false;
                    oupsicommtype.Msg = "No data found !";
                }
                rdr.Close();
                return oupsicommtype;
            }
            catch (Exception ex)
            {
                UPSITypeResponse oupsicommtype = new UPSITypeResponse();
                oupsicommtype.StatusFl = false;
                oupsicommtype.Msg = "Processing failed, because of system error !";
                return oupsicommtype;
            }
        }
    }
}