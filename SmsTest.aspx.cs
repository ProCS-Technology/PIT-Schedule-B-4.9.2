using System.Text;
using System;
using System.Configuration;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using MailKit.Security;
using MailKit.Net.Imap;
using MailKit.Net.Pop3;
using System.Configuration;
using System.Collections.Generic;
public partial class SmsTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {

       /*string x = MessageSenderC.SendSMS11(TextBox1.Text, "Thank you for your valuable feedback. We look forward to welcoming you back soon. Regards, Team Bikanervala.");
       string y = x.Replace("<!DOCTYPE RESULT SYSTEM 'http://bulkpush.mytoday.com/BulkSms/BulkSmsRespV1.01.dtd'>", "").Replace("\r", " ").Replace("\n", " ").Replace("'", "").Replace("<", "").Replace(">", "");
       Label1.Text = Convert.ToString(y);*/
       
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string strTo = "sandeep@pro-cs.in";
        var gsuiteUser = "pit@medanta.org";
        var gsuitePwd = "medanta@123";
        string strMsg = "Hi, this is test mail";
        string strSubject = "Test mail";

        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("", gsuiteUser));
        message.To.Add(new MailboxAddress("", strTo));

        message.Subject = strSubject;

        var html = new TextPart("html");
        html.SetText(Encoding.UTF8, strMsg);

        var multipart = new Multipart("mixed");
        multipart.Add(html);
        message.Body = multipart;

        using (var client = new SmtpClient())
        {
            client.Connect("mail.medanta.org", 25, MailKit.Security.SecureSocketOptions.Auto);
            client.Authenticate(gsuiteUser, gsuitePwd);
            client.Send(message);
            client.Disconnect(true);
        }

    }
}