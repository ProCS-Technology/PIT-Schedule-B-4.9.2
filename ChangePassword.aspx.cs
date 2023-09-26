using ProcsDLL.Models.Login.Service.Request;
using ProcsDLL.Models.Login.Service.Response;
using System;
using System.Web;
using System.Web.UI;

namespace ProcsDLL
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        Random random = new Random();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (String.IsNullOrEmpty(Convert.ToString(Session["EmployeeId"])))
                {
                    Response.Redirect("Login.aspx");
                }
                txtLoginId.Value = Convert.ToString(Session["EmployeeId"]);
                Session["salt"] = "PROCS"; /*random.Next(59999, 199999).ToString()*/;
                Session["moreSalt"] = random.Next(59999, 199999).ToString();
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                string daads = Session["salt"].ToString() + "~" + Session["moreSalt"].ToString();
            }
        }

        protected void GoToLogin(object sender, EventArgs e)
        {
            Response.Redirect("LogOut.aspx");
        }

        protected void SaveChangedPassword(object sender, EventArgs e)
        {
            ProcsDLL.Models.Login.Modal.Login login = new ProcsDLL.Models.Login.Modal.Login();
            login.LoginId = txtLoginId.Value;
            login.Password = txtOldPassword.Value;
            // login.Password = CryptorEngine.Encrypt(txtOldPassword.Value, true);

            LoginRequest objlogin = new LoginRequest(login);

            LoginResponse objResponse = objlogin.ValidateUser();
            if (objResponse.StatusFl)
            {
                if (objResponse.Msg == "Success")
                {
                    login.Password = txtNewPassword.Value;
                    // login.Password = CryptorEngine.Encrypt(txtNewPassword.Value, true);
                    objlogin = new LoginRequest(login);
                    objResponse = objlogin.ChangePassword();
                    if (objResponse.StatusFl)
                    {
                        objlogin.UpdateChangePasswordFlag();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert", "setTimeout(function(){fnGoToLoginPage();},1000);", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert", "setTimeout(function(){alert('Something went wrong.');},1000);", true);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert", "setTimeout(function(){alert('Not a valid user.Please verify your login id and password.');},1000);", true);
            }
        }
    }
}