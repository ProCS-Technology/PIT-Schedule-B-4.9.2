using ProcsDLL.Models.Infrastructure;
using System;
using System.Web.UI;

namespace ProcsDLL
{
    public partial class EncryptDecrypt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtCounter.Text = "1";
            }
        }
        protected void Button1_Clicked(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(10000);
            Int32 x = Convert.ToInt32(txtCounter.Text);
            x++;
            txtCounter.Text = Convert.ToString(x);
        }

        protected void btnEncrypt_onClick(object sender, EventArgs e)
        {
            String str = txtString.Text;
            String encryptedStr = CryptorEngine.Encrypt(str, true);
            txtEString.Text = encryptedStr;
        }
        protected void btnDecrypt_onClick(object sender, EventArgs e)
        {
            String encryptedStr = txtEnString.Text;
            String decryptStr = CryptorEngine.Decrypt(encryptedStr, true);
            txtDString.Text = decryptStr;
        }

        protected void btnEncryptSha512_onClick(object sender, EventArgs e)
        {
            String str = txtWithoutShaString.Text;
            String encryptedShaString = hashcodegenerate.GetSHA512(str);
            txtShaString.Text = encryptedShaString;
        }
    }
}