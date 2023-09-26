using System;
using System.Drawing;
using System.Drawing.Imaging;
using SRVTextToImage;

namespace ProcsDLL
{
    public partial class CaptchaImage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CaptchaRandomImage CI = new CaptchaRandomImage();
            string captchaText = CI.GetRandomString(5);

            Session["CaptchaText"] = captchaText;
            CI.GenerateImage(captchaText, 200, 50, Color.Black, Color.White);

            this.Response.Clear();
            this.Response.ContentType = "image/jpeg";
            CI.Image.Save(this.Response.OutputStream, ImageFormat.Jpeg);
            CI.Dispose();
        }
    }
}