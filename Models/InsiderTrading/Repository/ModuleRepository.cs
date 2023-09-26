using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace ProcsDLL.Models.InsiderTrading.Repository
{
    public class ModuleRepository
    {
        public ModuleResponse GetModule(string ModuleNm, string ModuleDb)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@Mode", "GET_FORM");
                parameters[1] = new SqlParameter("@MODULE_NM", ModuleNm);
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_MODULE_FORM", ModuleDb, parameters);

                ModuleResponse oRes = new ModuleResponse();

                if (ds.Tables.Count > 0)
                {
                    DataTable dtModules = ds.Tables[0];
                    DataTable dtFields = ds.Tables[1];

                    if (dtModules.Rows.Count > 0)
                    {
                        List<Module> lstModules = new List<Module>();
                        foreach (DataRow drModule in dtModules.Rows)
                        {
                            Module oModule = new Module();
                            oModule.ModuleNm = Convert.ToString(drModule["MODULE"]);
                            oModule.SubModuleNm = Convert.ToString(drModule["SUB_MODULE"]);
                            if (dtFields.Rows.Count > 0)
                            {
                                DataRow[] drFields = dtFields.Select("MODULE='" + Convert.ToString(drModule["MODULE"]) + "' AND SUB_MODULE='" + Convert.ToString(drModule["SUB_MODULE"]) + "'");
                                if (drFields.Length > 0)
                                {
                                    List<FormField> lstFormFields = new List<FormField>();
                                    foreach (DataRow drField in drFields)
                                    {
                                        FormField oFormField = new FormField();
                                        oFormField.CntrlType = Convert.ToString(drField["CNTRL_TYPE"]);
                                        oFormField.ControlId = Convert.ToString(drField["FIELD_ID"]);
                                        oFormField.ControlNm = Convert.ToString(drField["FIELD_NM"]);
                                        oFormField.DisplayFl = Convert.ToString(drField["DISPLAY_FL"]);
                                        oFormField.EditFl = Convert.ToString(drField["EDIT_FL"]);
                                        oFormField.DivNm = Convert.ToString(drField["DIV_NM"]);
                                        oFormField.Field = Convert.ToString(drField["FIELD"]);
                                        oFormField.FormatType = Convert.ToString(drField["FORMAT_TYPE"]);
                                        oFormField.RequiredFl = Convert.ToString(drField["REQUIRED_FL"]);
                                        oFormField.MinLength = "6";
                                        oFormField.MaxLength = "10";
                                        lstFormFields.Add(oFormField);
                                    }
                                    oModule.fields = lstFormFields;
                                }
                            }
                            lstModules.Add(oModule);
                        }
                        oRes.Modules = lstModules;
                        oRes.StatusFl = true;
                        oRes.Msg = "Success";
                    }
                    else
                    {
                        oRes.StatusFl = false;
                        oRes.Msg = "No Data Found";
                    }
                }
                else
                {
                    oRes.StatusFl = false;
                    oRes.Msg = "No Data Found";
                }
                return oRes;
            }
            catch (Exception ex)
            {
                ModuleResponse oRes = new ModuleResponse();
                oRes.StatusFl = false;
                oRes.Msg = "Processing failed, because of system error !";
                return oRes;
            }
        }
        public ModuleResponse GetUserConfig(string ModuleDb)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@Mode", "GET_USER_FORM_CONFIG");
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_MODULE_FORM", ModuleDb, parameters);
                ModuleResponse oRes = new ModuleResponse();
                if (ds.Tables.Count > 0)
                {
                    DataTable dtModules = ds.Tables[0];
                    DataTable dtFields = ds.Tables[1];
                    if (dtModules.Rows.Count > 0)
                    {
                        List<Module> lstModules = new List<Module>();
                        foreach (DataRow drModule in dtModules.Rows)
                        {
                            Module oModule = new Module();
                            oModule.ModuleNm = Convert.ToString(drModule["MODULE"]);
                            oModule.SubModuleNm = Convert.ToString(drModule["SUB_MODULE"]);
                            if (dtFields.Rows.Count > 0)
                            {
                                DataRow[] drFields = dtFields.Select("MODULE='" + Convert.ToString(drModule["MODULE"]) + "' AND SUB_MODULE='" + Convert.ToString(drModule["SUB_MODULE"]) + "'");
                                if (drFields.Length > 0)
                                {
                                    List<FormField> lstFormFields = new List<FormField>();
                                    foreach (DataRow drField in drFields)
                                    {
                                        FormField oFormField = new FormField();
                                        oFormField.CntrlType = Convert.ToString(drField["CNTRL_TYPE"]);
                                        oFormField.ControlId = Convert.ToString(drField["FIELD_ID"]);
                                        oFormField.ControlNm = Convert.ToString(drField["FIELD_NM"]);
                                        oFormField.DisplayFl = Convert.ToString(drField["DISPLAY_FL"]);
                                        oFormField.EditFl = Convert.ToString(drField["EDIT_FL"]);
                                        oFormField.DivNm = Convert.ToString(drField["DIV_NM"]);
                                        oFormField.Field = Convert.ToString(drField["FIELD"]);
                                        oFormField.FormatType = Convert.ToString(drField["FORMAT_TYPE"]);
                                        oFormField.RequiredFl = Convert.ToString(drField["REQUIRED_FL"]);
                                        oFormField.MinLength = "6";
                                        oFormField.MaxLength = "10";
                                        lstFormFields.Add(oFormField);
                                    }
                                    oModule.fields = lstFormFields;
                                }
                            }
                            lstModules.Add(oModule);
                        }
                        oRes.Modules = lstModules;
                        oRes.StatusFl = true;
                        oRes.Msg = "Success";
                    }
                    else
                    {
                        oRes.StatusFl = false;
                        oRes.Msg = "No Data Found";
                    }
                }
                else
                {
                    oRes.StatusFl = false;
                    oRes.Msg = "No Data Found";
                }
                return oRes;
            }
            catch (Exception ex)
            {
                ModuleResponse oRes = new ModuleResponse();
                oRes.StatusFl = false;
                oRes.Msg = "Processing failed, because of system error !";
                return oRes;
            }
        }
    }
}