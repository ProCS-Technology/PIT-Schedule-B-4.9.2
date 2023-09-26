using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
namespace ProcsDLL.Models.InsiderTrading.Repository
{
    public class ReminderRepository
    {
        public ReminderResponse SendReminder(Reminder objReminder)
        {
            try
            {
                foreach (var userEmail in objReminder.lstUser)
                {
                    if (objReminder.mailType == "1" || objReminder.mailType == "2")
                    {
                        List<string> attachments = new List<string>();
                        if (File.Exists(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/UserManual/User-Manual.pdf"))))
                        {
                            attachments.Add(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/UserManual/User-Manual.pdf")));
                        }
                        if (File.Exists(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/CodeOfConduct/Insider Trading Code.pdf"))))
                        {
                            attachments.Add(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/CodeOfConduct/Insider Trading Code.pdf")));
                        }
                        if (File.Exists(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/UserManual/ComplianceTrainingModule.pdf"))))
                        {
                            attachments.Add(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/UserManual/ComplianceTrainingModule.pdf")));
                        }
                        EmailSender.SendMail(
                            userEmail, objReminder.subject, objReminder.mailBody, attachments,
                            "Reminder module manual", objReminder.companyId.ToString(), "", userEmail, objReminder.LoggedInUser
                        );
                    }
                    else
                    {
                        EmailSender.SendMail(
                            userEmail, objReminder.subject, objReminder.mailBody, null,
                            "Reminder module manual", objReminder.companyId.ToString(), "", userEmail, objReminder.LoggedInUser
                        );
                    }
                }
                ReminderResponse objResponse = new ReminderResponse();
                objResponse.StatusFl = true;
                objResponse.Msg = "Mail sent successfully";
                return objResponse;
            }
            catch (Exception ex)
            {
                ReminderResponse objResponse = new ReminderResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message.ToString();
                return objResponse;
            }
        }

        public ReminderResponse SendMailSetup(Reminder objReminder)
        {
            try
            {
                foreach (var userEmail in objReminder.lstUser)
                {
                    if (objReminder.mailType == "1" || objReminder.mailType == "2")
                    {
                        List<string> attachments = new List<string>();
                        if (File.Exists(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/UserManual/User-Manual.pdf"))))
                        {
                            attachments.Add(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/UserManual/User-Manual.pdf")));
                        }
                        if (File.Exists(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/CodeOfConduct/Insider Trading Code.pdf"))))
                        {
                            attachments.Add(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/CodeOfConduct/Insider Trading Code.pdf")));
                        }
                        if (File.Exists(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/UserManual/ComplianceTrainingModule.pdf"))))
                        {
                            attachments.Add(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/UserManual/ComplianceTrainingModule.pdf")));
                        }
                        EmailSender.SendMail(
                            userEmail, objReminder.subject, objReminder.mailBody, attachments,
                            "Reminder module manual", objReminder.companyId.ToString(), "", userEmail, objReminder.LoggedInUser
                        );
                    }
                    else
                    {
                        EmailSender.SendMail(
                            userEmail, objReminder.subject, objReminder.mailBody, null,
                            "Reminder module manual", objReminder.companyId.ToString(), "", userEmail, objReminder.LoggedInUser
                        );
                    }
                }
                ReminderResponse objResponse = new ReminderResponse();
                objResponse.StatusFl = true;
                objResponse.Msg = "Mail sent successfully";
                return objResponse;
            }
            catch (Exception ex)
            {
                ReminderResponse objResponse = new ReminderResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message.ToString();
                return objResponse;
            }
        }
    }
}