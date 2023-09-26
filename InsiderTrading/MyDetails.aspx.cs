using ProcsDLL.Models.InsiderTrading.Service.Request;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProcsDLL.InsiderTrading
{
    public partial class MyDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Convert.ToString(Session["EmployeeId"])) && !String.IsNullOrEmpty(Convert.ToString(Session["CompanyName"])))
            {
                txtLoginId.Value = Convert.ToString(Session["EmployeeId"]);
                txtParentCompanyName.Value = Convert.ToString(HttpContext.Current.Session["ParentBusinessUnitName"]);
                txtCompanyName.Value = Convert.ToString(Session["BusinessUnitName"]);//Convert.ToString(Session["CompanyName"]);

            }

            if (!Page.IsPostBack)
            {
                fnGetAllDepartments();
                fnGetAllDesignations();
                fnGetAllLocations();
                fnGetAllCategory();
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
            ProcsDLL.Models.InsiderTrading.Model.Category category = new ProcsDLL.Models.InsiderTrading.Model.Category();
            category.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
            category.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
            CategoryRequest categoryList = new CategoryRequest(category);
            CategoryResponse gResCategoryList = categoryList.GetCategoryList();

            if (gResCategoryList.StatusFl)
            {
                if (gResCategoryList.CategoryList.Count > 0)
                {
                    ddlCategory.Items.Add(new ListItem("", ""));
                    foreach (ProcsDLL.Models.InsiderTrading.Model.Category deptX in gResCategoryList.CategoryList)
                    {
                        ddlCategory.Items.Add(new ListItem(deptX.categoryName, deptX.ID.ToString()));
                    }
                }
            }
        }
    }
}