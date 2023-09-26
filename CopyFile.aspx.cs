using System;
using System.IO;

namespace ProcsDLL
{
    public partial class CopyFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string strFileName;
                string strFilePath;
                string strFolder;
                strFolder = Server.MapPath("~/InsiderTrading/ProcsFileUplod/");
                // Get the name of the file that is posted.
                strFileName = FileUpload1.PostedFile.FileName;
                strFileName = Path.GetFileName(strFileName);
                if (FileUpload1.HasFiles)
                {
                    // Create the directory if it does not exist.
                    if (!Directory.Exists(strFolder))
                    {
                        Directory.CreateDirectory(strFolder);
                    }
                    // Save the uploaded file to the server.
                    strFilePath = strFolder + strFileName;
                    if (File.Exists(strFilePath))
                    {
                        LabelMessage.Text = strFileName + " already exists on the server!";
                    }
                    else
                    {
                        FileUpload1.PostedFile.SaveAs(strFilePath);
                        LabelMessage.Text = strFileName + " has been successfully uploaded.";
                    }
                }
            }
            catch (Exception ex)
            {
                LabelMessage.Text = ex.ToString();
            }



        }
    }
}