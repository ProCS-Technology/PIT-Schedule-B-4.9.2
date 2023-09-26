using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Service.Request;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace ProcsDLL.InsiderTrading
{
    public partial class UserDeclaration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Convert.ToString(Session["EmployeeId"])) && !String.IsNullOrEmpty(Convert.ToString(Session["CompanyName"])))
            {
                txtLoginId.Value = Convert.ToString(Session["EmployeeId"]);
                txtParentCompanyName.Value = Convert.ToString(HttpContext.Current.Session["ParentBusinessUnitName"]);
                //txtCompanyName.Value = Convert.ToString(Session["CompanyName"]);
                txtCompanyName.Value = Convert.ToString(Session["BusinessUnitName"]);//Convert.ToString(Session["CompanyName"]);
                spnCompanyName.InnerText = Convert.ToString(HttpContext.Current.Session["ParentBusinessUnitName"]);
                txtSpousePANMandatory.Value = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["SpousePANMandatory"]), true);
                // txtMFRMandatory.Value = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["MFRMandatory"]), true);
                txtRelativeEmail.Value = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["RelativeEmailMandatory"]), true);
                txtRelativeAddress.Value = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["RelativeAddressMandatory"]), true);
                string visibleHoldingTransaction = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["Holdingtransaction"]), true);
                if (visibleHoldingTransaction == "true")
                {
                    divTransactionHistory.Visible = true;
                }
                else
                {
                    divTransactionHistory.Visible = false;
                }
            }
            if (!Page.IsPostBack)
            {
                fnGetFinalDeclarationHerebyContent();
                fnGetAllDepartments();
                fnGetAllDesignations();
                fnGetAllLocations();
                fnGetAllCategory();
                fnGetAllRelation();
                fnGetAllRestrictedCompanies();
                fnGetTypeOfSecurity();
                BtnOtherHolding.Visible = Convert.ToBoolean(CryptorEngine.Decrypt(ConfigurationManager.AppSettings["EnableAddOtherEquityBtn"], true));
                GetTaskStatus();
                getsupportMail();
            }
        }
        private void getsupportMail()
        {
            var mail = ConfigurationManager.AppSettings["supportMail"];
            linkSupportMail.Attributes["href"] = "mailto:" + mail;
            lblComplianceEmailId.InnerText = mail;
        }
        private void GetTaskStatus()
        {
            try
            {
                if (String.IsNullOrEmpty(Convert.ToString(Session["EmployeeId"])) || String.IsNullOrEmpty(Convert.ToString(Session["ModuleDatabase"])))
                {
                    Response.Redirect("../LogOut.aspx");
                }
                ProcsDLL.Models.InsiderTrading.Model.User user = new ProcsDLL.Models.InsiderTrading.Model.User();
                user.LOGIN_ID = Convert.ToString(Session["EmployeeId"]);
                user.MODULE_DATABASE = Convert.ToString(Session["ModuleDatabase"]);
                UserRequest objUser = new UserRequest(user);
                UserResponse objResponse = objUser.GetUserTask();
                if (objResponse.StatusFl)
                {
                    hdnTaskFor.Value = objResponse.User.TaskFor;
                    hdnTaskStatus.Value = objResponse.User.TaskStatus;
                    hdnTaskId.Value = Convert.ToString(objResponse.User.TaskId);
                    hdnTaskFrm.Value = "";
                    if (objResponse.User.TaskFor.ToUpper() == "INITIAL DISCLOSURE REMINDER")
                    {
                        spnTitleLbl.InnerHtml = "INITIAL DISCLOSURE - ";
                    }
                    else
                    {
                        spnTitleLbl.InnerHtml = "Annual DISCLOSURE - ";
                    }
                }
                else
                {
                    hdnTaskFor.Value = "";
                    hdnTaskId.Value = "0";
                    hdnTaskFrm.Value = "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void fnGetAllDepartments()
        {
            ProcsDLL.Models.InsiderTrading.Model.Department dept = new ProcsDLL.Models.InsiderTrading.Model.Department();
            dept.CREATE_BY = Convert.ToString(Session["EmployeeId"]);
            dept.COMPANY_ID = Convert.ToInt32(Session["CompanyId"]);
            dept.MODULE_DATABASE = Convert.ToString(Session["ModuleDatabase"]);
            DepartmentRequest departmentList = new DepartmentRequest(dept);
            DepartmentResponse gResGrpList = departmentList.GetDepartmentList();

            if (gResGrpList.StatusFl)
            {
                if (gResGrpList.DepartmentList.Count > 0)
                {
                    ddlDepartment.Items.Add(new ListItem("", ""));
                    foreach (ProcsDLL.Models.InsiderTrading.Model.Department deptX in gResGrpList.DepartmentList)
                    {
                        ddlDepartment.Items.Add(new ListItem(deptX.DEPARTMENT_NM, deptX.DEPARTMENT_ID.ToString()));
                    }
                }
            }
        }
        private void fnGetAllDesignations()
        {
            ProcsDLL.Models.InsiderTrading.Model.Designation designation = new ProcsDLL.Models.InsiderTrading.Model.Designation();
            designation.CREATE_BY = Convert.ToString(Session["EmployeeId"]);
            designation.COMPANY_ID = Convert.ToInt32(Session["CompanyId"]);
            designation.MODULE_DATABASE = Convert.ToString(Session["ModuleDatabase"]);
            DesignationRequest gReqDesignationList = new DesignationRequest(designation);
            DesignationResponse gResDesignationList = gReqDesignationList.GetDesignationList();

            if (gResDesignationList.StatusFl)
            {
                if (gResDesignationList.DesignationList.Count > 0)
                {
                    ddlDesignation.Items.Add(new ListItem("", ""));
                    foreach (ProcsDLL.Models.InsiderTrading.Model.Designation deptX in gResDesignationList.DesignationList)
                    {
                        ddlDesignation.Items.Add(new ListItem(deptX.DESIGNATION_NM, deptX.DESIGNATION_ID.ToString()));
                    }
                }
            }
        }
        private void fnGetAllLocations()
        {
            ProcsDLL.Models.InsiderTrading.Model.Location location = new ProcsDLL.Models.InsiderTrading.Model.Location();
            location.companyId = Convert.ToInt32(Session["CompanyId"]);
            location.MODULE_DATABASE = Convert.ToString(Session["ModuleDatabase"]);
            LocationRequest locationList = new LocationRequest(location);
            LocationResponse gResLocationList = locationList.GetLocationList();

            if (gResLocationList.StatusFl)
            {
                if (gResLocationList.LocationList.Count > 0)
                {
                    ddlLocation.Items.Add(new ListItem("", ""));
                    foreach (ProcsDLL.Models.InsiderTrading.Model.Location deptX in gResLocationList.LocationList)
                    {
                        ddlLocation.Items.Add(new ListItem(deptX.locationName, deptX.locationId.ToString()));
                    }
                }
            }
        }
        private void fnGetAllCategory()
        {

            ProcsDLL.Models.InsiderTrading.Model.Role rol = new ProcsDLL.Models.InsiderTrading.Model.Role();
            rol.CREATED_BY = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
            rol.COMPANY_ID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
            rol.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);

            string _sql = "SELECT ROLE_ID,ROLE_NAME FROM PROCS_INSIDER_ROLE_MSTR ORDER BY ROLE_NAME";

            DataSet dsRole = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.Text, _sql, rol.MODULE_DATABASE, null);
            DataTable dtRole = dsRole.Tables[0];
            if (dtRole.Rows.Count > 0)
            {
                ddlCategory.Items.Add(new ListItem("", ""));
                foreach (DataRow drRole in dtRole.Rows)
                {
                    ddlCategory.Items.Add(new ListItem(Convert.ToString(drRole["ROLE_NAME"]), Convert.ToString(drRole["ROLE_ID"])));
                }
            }

            //RoleRequest RoleList = new RoleRequest(rol);
            //RoleResponse gResRolList = RoleList.GetRoleListWithAdmin();

            //if (gResRolList.StatusFl)
            //{
            //    if (gResRolList.RoleList.Count > 0)
            //    {
            //        ddlCategory.Items.Add(new ListItem("", ""));
            //        foreach (ProcsDLL.Models.InsiderTrading.Model.Role roleX in gResRolList.RoleList)
            //        {
            //            ddlCategory.Items.Add(new ListItem(roleX.ROLE_NM, roleX.ROLE_ID.ToString()));
            //        }
            //    }
            //}
            //ProcsDLL.Models.InsiderTrading.Model.Category category = new ProcsDLL.Models.InsiderTrading.Model.Category();
            //category.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
            //category.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
            //CategoryRequest categoryList = new CategoryRequest(category);
            //CategoryResponse gResCategoryList = categoryList.GetCategoryList();

            //if (gResCategoryList.StatusFl)
            //{
            //    if (gResCategoryList.CategoryList.Count > 0)
            //    {
            //        ddlCategory.Items.Add(new ListItem("", ""));
            //        foreach (ProcsDLL.Models.InsiderTrading.Model.Category deptX in gResCategoryList.CategoryList)
            //        {
            //            ddlCategory.Items.Add(new ListItem(deptX.categoryName, deptX.ID.ToString()));
            //        }
            //    }
            //}
        }
        private void fnGetAllRelation()
        {
            ProcsDLL.Models.InsiderTrading.Model.Relation rel = new ProcsDLL.Models.InsiderTrading.Model.Relation();
            rel.CREATED_BY = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
            rel.COMPANY_ID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
            rel.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
            RelationRequest RelationList = new RelationRequest(rel);
            RelationResponse gResRelList = RelationList.GetRelationList();

            if (gResRelList.StatusFl)
            {
                if (gResRelList.RelationList.Count > 0)
                {
                    //ddlRelation.Items.Add(new ListItem("", ""));
                    //ddlRelation.Items.Add(new ListItem("Not Applicable", "0"));
                    foreach (ProcsDLL.Models.InsiderTrading.Model.Relation deptX in gResRelList.RelationList)
                    {
                        if (deptX.RELATION_NM.ToUpper() == "SPOUSE" || deptX.RELATION_NM.ToUpper() == "WIFE" || deptX.RELATION_NM.ToUpper() == "HUSBAND")
                        {
                            txtSpouseRelationId.Value = deptX.RELATION_ID.ToString();
                            txtSpouseRelationNm.Value = deptX.RELATION_NM.ToString();
                            //txtSpouseRelationId.Text = deptX.RELATION_ID.ToString();
                            //txtSpouseRelationNm.Text = deptX.RELATION_NM.ToString();
                        }
                        else if (deptX.RELATION_NM.ToUpper() == "MATERIAL FINANCIAL RELATION" || deptX.RELATION_NM.ToUpper().StartsWith("MFR"))
                        {
                            txtMFRId.Value = deptX.RELATION_ID.ToString();
                            txtMFRNm.Value = deptX.RELATION_NM.ToString();
                        }
                        //ddlRelation.Items.Add(new ListItem(deptX.RELATION_NM, deptX.RELATION_ID.ToString()));
                    }
                }
            }
        }
        private void fnGetAllRestrictedCompanies()
        {
            ProcsDLL.Models.InsiderTrading.Model.RestrictedCompanies objRestrictedCompanies = new ProcsDLL.Models.InsiderTrading.Model.RestrictedCompanies();
            objRestrictedCompanies.createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
            objRestrictedCompanies.companyID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
            objRestrictedCompanies.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
            RestrictedCompaniesRequest resCompaniesRequest = new RestrictedCompaniesRequest(objRestrictedCompanies);
            RestrictedCompaniesResponse resCompaniesResponse = resCompaniesRequest.GetRestrictedCompaniesList();

            if (resCompaniesResponse.StatusFl)
            {
                if (resCompaniesResponse.RestrictedCompaniesList.Count > 0)
                {
                    ddlRestrictedCompaniesX.Items.Add(new ListItem("", ""));
                    ddlRestrictedCompaniesX.Items.Add(new ListItem("Not Applicable", "0"));
                    foreach (ProcsDLL.Models.InsiderTrading.Model.RestrictedCompanies deptX in resCompaniesResponse.RestrictedCompaniesList)
                    {
                        ddlRestrictedCompaniesX.Items.Add(new ListItem(deptX.companyName, deptX.companyID.ToString()));
                        ddlRestrictedCompaniesPhysical.Items.Add(new ListItem(deptX.companyName, deptX.companyID.ToString()));
                    }
                }
            }

        }
        private void fnGetTypeOfSecurity()
        {
            ProcsDLL.Models.InsiderTrading.Model.PreClearanceRequest pClR = new ProcsDLL.Models.InsiderTrading.Model.PreClearanceRequest();
            pClR.CompanyId = Convert.ToInt32(Session["CompanyId"]);
            pClR.LoginId = Convert.ToString(Session["EmployeeId"]);
            pClR.MODULE_DATABASE = Convert.ToString(Session["ModuleDatabase"]);
            PreClearanceRequestRequest gReqPClR = new PreClearanceRequestRequest(pClR);
            PreClearanceRequestResponse gResPClR = gReqPClR.GetTypeOfSecurity();

            if (gResPClR.StatusFl)
            {
                if (gResPClR.SecurityTypeList.Count > 0)
                {
                    ddlSecurityType.Items.Add(new ListItem("", ""));
                    ddlSecurityType.Items.Add(new ListItem("Not Applicable", "0"));

                    ddlOtherSecurityType.Items.Add(new ListItem("", ""));
                    foreach (ProcsDLL.Models.InsiderTrading.Model.SecurityType deptX in gResPClR.SecurityTypeList)
                    {
                        //if (deptX.Name.ToLower() == "equity" || deptX.Name.ToLower() == "debt equity")
                        //{
                        //    ddlSecurityType.Items.Add(new ListItem(deptX.Name, deptX.Id.ToString()));
                        //    if (deptX.Name.ToLower() == "debt equity")
                        //    {
                        //        ddlOtherSecurityType.Items.Add(new ListItem(deptX.Name, deptX.Id.ToString()));
                        //    }
                        //}
                        //else
                        //{
                        //    ddlOtherSecurityType.Items.Add(new ListItem(deptX.Name, deptX.Id.ToString()));
                        //}
                        if (deptX.IsTradable.ToLower() == "y")
                        {
                            ddlSecurityType.Items.Add(new ListItem(deptX.Name, deptX.Id.ToString()));
                            ddlOtherSecurityType.Items.Add(new ListItem(deptX.Name, deptX.Id.ToString()));
                        }
                    }
                }
            }
        }
        private void fnGetFinalDeclarationHerebyContent()
        {
            if (!String.IsNullOrEmpty(Convert.ToString(Session["ModuleDatabase"])))
            {
                string ConnStr = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionString"]), true);
                using (SqlConnection conn = new SqlConnection(ConnStr))
                {
                    conn.Open();
                    conn.ChangeDatabase(Convert.ToString(Session["ModuleDatabase"]));
                    string sql = "SELECT A.CONTENT FROM PROCS_INSIDER_FINAL_DECLARATION_HEREBY_CONTENT(NOLOCK) A WHERE " +
                                 "A.COMPANY_ID = " + Convert.ToInt32(Session["CompanyId"]);
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        DataTable dtAccess = new DataTable();
                        SqlDataAdapter daAccess = new SqlDataAdapter(cmd);
                        daAccess.Fill(dtAccess);

                        if (dtAccess.Rows.Count > 0)
                        {
                            DeclarationHerebyContent.InnerHtml = Convert.ToString(dtAccess.Rows[0]["CONTENT"]);
                        }
                    }

                }
            }
        }
    }
}