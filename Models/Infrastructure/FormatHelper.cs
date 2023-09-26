using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
namespace ProcsDLL.Models.Infrastructure
{
    public class FormatHelper
    {
        public static string FormatDate(string date)
        {
            String strDate = String.Empty;
            string CustomFormat = Convert.ToString(ConfigurationManager.AppSettings["UniversalDateFormat"]);
            try
            {
                if (!string.IsNullOrEmpty(date) || !string.IsNullOrWhiteSpace(date))
                {
                    if (CustomFormat.ToUpper() == "DD/MM/YYYY")
                    {
                        strDate = date.Split('/')[2] + "-" + date.Split('/')[1] + "-" + date.Split('/')[0];
                    }
                    else if (CustomFormat.ToUpper() == "DD-MMM-YYYY")
                    {
                        if (date.Split('-')[1].ToUpper() == "JAN")
                        {
                            strDate = date.Split('-')[2] + "-01-" + date.Split('-')[0];
                        }
                        else if (date.Split('-')[1].ToUpper() == "FEB")
                        {
                            strDate = date.Split('-')[2] + "-02-" + date.Split('-')[0];
                        }
                        else if (date.Split('-')[1].ToUpper() == "MAR")
                        {
                            strDate = date.Split('-')[2] + "-03-" + date.Split('-')[0];
                        }
                        else if (date.Split('-')[1].ToUpper() == "APR")
                        {
                            strDate = date.Split('-')[2] + "-04-" + date.Split('-')[0];
                        }
                        else if (date.Split('-')[1].ToUpper() == "MAY")
                        {
                            strDate = date.Split('-')[2] + "-05-" + date.Split('-')[0];
                        }
                        else if (date.Split('-')[1].ToUpper() == "JUN")
                        {
                            strDate = date.Split('-')[2] + "-06-" + date.Split('-')[0];
                        }
                        else if (date.Split('-')[1].ToUpper() == "JUL")
                        {
                            strDate = date.Split('-')[2] + "-07-" + date.Split('-')[0];
                        }
                        else if (date.Split('-')[1].ToUpper() == "AUG")
                        {
                            strDate = date.Split('-')[2] + "-08-" + date.Split('-')[0];
                        }
                        else if (date.Split('-')[1].ToUpper() == "SEP")
                        {
                            strDate = date.Split('-')[2] + "-09-" + date.Split('-')[0];
                        }
                        else if (date.Split('-')[1].ToUpper() == "OCT")
                        {
                            strDate = date.Split('-')[2] + "-10-" + date.Split('-')[0];
                        }
                        else if (date.Split('-')[1].ToUpper() == "NOV")
                        {
                            strDate = date.Split('-')[2] + "-11-" + date.Split('-')[0];
                        }
                        else if (date.Split('-')[1].ToUpper() == "DEC")
                        {
                            strDate = date.Split('-')[2] + "-12-" + date.Split('-')[0];
                        }
                    }
                    else if (CustomFormat.ToUpper() == "MM/DD/YYYY")
                    {
                        strDate = date.Split('/')[2] + "-" + date.Split('/')[0] + "-" + date.Split('/')[1];
                    }
                }
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message.ToString(), ex.Source, ex.StackTrace, typeof(FormatHelper).Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, Convert.ToString(HttpContext.Current.Session["EmployeeId"]), Convert.ToInt32(HttpContext.Current.Session["ModuleId"]));
            }
            return strDate;
        }
        public static string ConvertDate(string date)
        {
            String strDate = String.Empty;
            string CustomFormat = Convert.ToString(ConfigurationManager.AppSettings["UniversalDateFormat"]);
            try
            {
                if (!string.IsNullOrEmpty(date) || !string.IsNullOrWhiteSpace(date))
                {
                    if (CustomFormat.ToUpper() == "DD/MM/YYYY")
                    {
                        strDate = Convert.ToDateTime(date).ToString("dd/MM/yyyy");
                    }
                    else if (CustomFormat.ToUpper() == "DD-MMM-YYYY")
                    {
                        strDate = Convert.ToDateTime(date).ToString("dd-MMM-yyyy");
                    }
                    else if (CustomFormat.ToUpper() == "MM/DD/YYYY")
                    {
                        strDate = Convert.ToDateTime(date).ToString("MM/dd/yyyy");
                    }
                }
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message.ToString(), ex.Source, ex.StackTrace, typeof(FormatHelper).Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, Convert.ToString(HttpContext.Current.Session["EmployeeId"]), Convert.ToInt32(HttpContext.Current.Session["ModuleId"]));
            }
            return strDate;
        }
        public static string FormatNumber(string Number)
        {
            decimal dNumber = Convert.ToDecimal(Number);
            var strNumber = string.Empty;
            string CustomFormat = Convert.ToString(ConfigurationManager.AppSettings["UniversalNumberFormat"]);
            try
            {
                if (!string.IsNullOrEmpty(Number) || !string.IsNullOrWhiteSpace(Number))
                {
                   strNumber= string.Format(CultureInfo.InvariantCulture, CustomFormat, dNumber);
                }
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message.ToString(), ex.Source, ex.StackTrace, typeof(FormatHelper).Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, Convert.ToString(HttpContext.Current.Session["EmployeeId"]), Convert.ToInt32(HttpContext.Current.Session["ModuleId"]));
            }
            return strNumber;
        }
        public static DateTime ConvertDateTime(string date)
        {
            String str = String.Empty;

            try
            {
                if (date.Contains("/"))
                {
                    str = date.Split('/')[2] + "-" + date.Split('/')[1] + "-" + date.Split('/')[0];
                }
                else
                {
                    str = date;
                }
            }
            catch (Exception ex)
            {

            }
            return Convert.ToDateTime(str);
        }
    }
}