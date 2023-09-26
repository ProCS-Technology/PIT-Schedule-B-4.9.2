using System;
using System.DirectoryServices;
namespace ProcsDLL
{
    public partial class ADIntegration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnADIntegration_onClick(object sender, EventArgs e)
        {

            try
            {
                string uid = txtUserId.Text;
                string pwd = txtPassword.Text;
                string username = txtUserName.Text;
                string ldap = txtPath.Text;

                DirectoryEntry de = new DirectoryEntry(ldap, uid, pwd);
                de.RefreshCache();

                DirectorySearcher dirSearch = null;
                dirSearch = new DirectorySearcher(de);

                //dirSearch.Filter = "(&((&(objectCategory=Person)(objectClass=User)))(cn=" + username + "))";
                dirSearch.Filter = "(&(objectClass=user)(objectCategory=person)(|(samaccountname=" + username + ")(cn=" + username + ")))";
                var searchResult = dirSearch.FindAll();


                if (searchResult != null)
                {

                    foreach (SearchResult rs in searchResult)
                    {
                        foreach (var pc in rs.GetDirectoryEntry().Properties.PropertyNames)
                        {
                            string ss = pc.ToString();
                            if (pc.ToString().ToUpper() == "CN")
                            {
                                lblMsg.Text += rs.GetDirectoryEntry().Properties[ss].Value.ToString();
                            }
                            if (pc.ToString().ToUpper() == "SAMACCOUNTNAME")
                            {
                                Response.Write(rs.GetDirectoryEntry().Properties[ss].Value.ToString());

                            }//if
                            if (pc.ToString().ToUpper() == "MAIL")
                            {
                                Response.Write(rs.GetDirectoryEntry().Properties[ss].Value.ToString());
                            }
                            if (pc.ToString().ToUpper() == "MOBILE")
                            {
                                Response.Write(rs.GetDirectoryEntry().Properties[ss].Value.ToString());
                            }
                        }

                    }
                }
                else
                {
                    Response.Write("No data found");
                }

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message.ToString());
            }
        }
    }
}