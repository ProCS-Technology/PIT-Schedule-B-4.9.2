using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.Menu.Modal;
using ProcsDLL.Models.Menu.Service.Response;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.SessionState;

namespace ProcsDLL.Models.Menu.Repository
{
    public class MenuRepository : IRequiresSessionState
    {
        public MenuResponse GetMenu(ProcsDLL.Models.Menu.Modal.Menu objMenu)
        {
            string ConnectionString = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["ConnectionString"].ToString(), true);
            MenuResponse res = new MenuResponse();
            res.StatusFl = false;
            res.Msg = "No Data Found";
            try
            {
                using (SqlConnection sCon = new SqlConnection(ConnectionString))
                {
                    sCon.Open();
                    sCon.ChangeDatabase(objMenu.DataBase);

                    SqlCommand sCmd = new SqlCommand();
                    sCmd.Connection = sCon;
                    sCmd.CommandType = CommandType.StoredProcedure;

                    sCmd.Parameters.Clear();
                    sCmd.Parameters.AddWithValue("@EmployeeId", objMenu.Email);
                    sCmd.Parameters.AddWithValue("@CompanyId", objMenu.CompanyId);
                    sCmd.CommandText = "SP_GET_MENU";

                    DataTable dtCnt = new DataTable();
                    SqlDataAdapter daCnt = new SqlDataAdapter(sCmd);
                    daCnt.Fill(dtCnt);
                    if (dtCnt.Rows.Count > 0)
                    {
                        List<MenuItems> lstItems = new List<MenuItems>();
                        foreach (DataRow drCnt in dtCnt.Rows)
                        {
                            MenuItems mnuItm = new MenuItems();
                            mnuItm.ID = Convert.ToInt32(drCnt["ID"]);
                            mnuItm.MenuId = Convert.ToInt32(drCnt["MENU_ID"]);
                            mnuItm.MenuItem = Convert.ToString(drCnt["MENU_ITEM"]);
                            mnuItm.ParentMenuItem = Convert.ToInt32(drCnt["PARENT_MENU_ID"]);
                            mnuItm.SubMenu = Convert.ToString(drCnt["SUB_MENU"]);
                            mnuItm.URL = Convert.ToString(drCnt["URL"]);
                            lstItems.Add(mnuItm);
                        }
                        objMenu.MItems = lstItems;
                        res.Msg = "Success";
                        res.StatusFl = true;
                        res.Menu = objMenu;
                    }
                    sCon.Close();
                }
            }
            catch (Exception ex)
            {
                res = new MenuResponse();
                res.StatusFl = false;
                res.Msg = "Something went wrong. Please try again or Contact Support!";
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, this.GetType().Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, Convert.ToString(HttpContext.Current.Session["EmployeeId"]), Convert.ToInt32(HttpContext.Current.Session["ModuleId"]), Convert.ToInt32(HttpContext.Current.Session["CompanyId"]));
            }
            return res;
        }
    }
}