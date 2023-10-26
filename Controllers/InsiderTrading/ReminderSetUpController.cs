using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Request;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Configuration;

namespace ProcsDLL.Controllers.InsiderTrading
{
    [RoutePrefix("api/ReminderSetUp")]
    public class ReminderSetUpController : ApiController
    {
        [HttpPost]
        [Route("getallReminder")]
        public ReminderSetUpResponse getallReminder()
        {
            if (HttpContext.Current.Session.Count == 0)
            {
                ReminderSetUpResponse objResponse = new ReminderSetUpResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = "SessionExpired";
                return objResponse;
            }

            ReminderSetUp rem = new ReminderSetUp();
            rem.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
            rem.Company_Id = Convert.ToString(HttpContext.Current.Session["CompanyId"]);
            rem.Created_By = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
            //if (!rem.ValidateInput())
            //{
            //    ReminderSetUpResponse objResponse = new ReminderSetUpResponse();
            //    objResponse.StatusFl = false;
            //    objResponse.Msg = "Invalid Input Format";
            //    return objResponse;
            //}
            ReminderSetUpRequest remRequest = new ReminderSetUpRequest(rem);
            ReminderSetUpResponse remResponce = new ReminderSetUpResponse();
            remResponce = remRequest.getAllReminderType();

            return remResponce;



        }
        [HttpPost]
        [Route("getallMailReminder")]
        public ReminderSetUpResponse getallMailReminder()
        {
            if (HttpContext.Current.Session.Count == 0)
            {
                ReminderSetUpResponse objResponse = new ReminderSetUpResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = "SessionExpired";
                return objResponse;
            }

            ReminderSetUp rem = new ReminderSetUp();
            rem.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
            rem.Company_Id = Convert.ToString(HttpContext.Current.Session["CompanyId"]);
            rem.Created_By = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
            //if (!rem.ValidateInput())
            //{
            //    ReminderSetUpResponse objResponse = new ReminderSetUpResponse();
            //    objResponse.StatusFl = false;
            //    objResponse.Msg = "Invalid Input Format";
            //    return objResponse;
            //}
            ReminderSetUpRequest remRequest = new ReminderSetUpRequest(rem);
            ReminderSetUpResponse remResponce = new ReminderSetUpResponse();
            remResponce = remRequest.getAllMailReminderType();

            return remResponce;



        }

        [HttpPost]
        [Route("getallMailReminderById")]
        public ReminderSetUpResponse getallMailReminderById()
        {
            if (HttpContext.Current.Session.Count == 0)
            {
                ReminderSetUpResponse objResponse = new ReminderSetUpResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = "SessionExpired";
                return objResponse;
            }
            string input;
            using (System.IO.StreamReader rd = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
            {

                input = rd.ReadToEnd();
            }

            ReminderSetUp rem = new JavaScriptSerializer().Deserialize<ReminderSetUp>(input);
            rem.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
            rem.Company_Id = Convert.ToString(HttpContext.Current.Session["CompanyId"]);
            rem.Created_By = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
            //if (!rem.ValidateInput())
            //{
            //    ReminderSetUpResponse objResponse = new ReminderSetUpResponse();
            //    objResponse.StatusFl = false;
            //    objResponse.Msg = "Invalid Input Format";
            //    return objResponse;
            //}
            ReminderSetUpRequest remRequest = new ReminderSetUpRequest(rem);
            ReminderSetUpResponse remResponce = new ReminderSetUpResponse();
            remResponce = remRequest.getallMailReminderById();

            return remResponce;
        }

        [HttpPost]
        [Route("getallReminderById")]
        public ReminderSetUpResponse getallReminderById()
        {
            if (HttpContext.Current.Session.Count == 0)
            {
                ReminderSetUpResponse objResponse = new ReminderSetUpResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = "SessionExpired";
                return objResponse;
            }
            string input;
            using (System.IO.StreamReader rd = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
            {

                input = rd.ReadToEnd();
            }

            ReminderSetUp rem = new JavaScriptSerializer().Deserialize<ReminderSetUp>(input);
            rem.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
            rem.Company_Id = Convert.ToString(HttpContext.Current.Session["CompanyId"]);
            rem.Created_By = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
            //if (!rem.ValidateInput())
            //{
            //    ReminderSetUpResponse objResponse = new ReminderSetUpResponse();
            //    objResponse.StatusFl = false;
            //    objResponse.Msg = "Invalid Input Format";
            //    return objResponse;
            //}
            ReminderSetUpRequest remRequest = new ReminderSetUpRequest(rem);
            ReminderSetUpResponse remResponce = new ReminderSetUpResponse();
            remResponce = remRequest.getAllReminderTypeByID();

            return remResponce;

        }

        [HttpPost]
        [Route("MailReminderSave")]
        public ReminderSetUpResponse MailReminderSave()
        {
            if (HttpContext.Current.Session.Count == 0)
            {
                ReminderSetUpResponse objResponse = new ReminderSetUpResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = "SessionExpired";
                return objResponse;
            }
            string input;
            using (System.IO.StreamReader rd = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
            {

                input = rd.ReadToEnd();
            }

            ReminderSetUp rem = new JavaScriptSerializer().Deserialize<ReminderSetUp>(input);
            rem.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
            rem.Company_Id = Convert.ToString(HttpContext.Current.Session["CompanyId"]);
            rem.Created_By = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
            //if (!rem.ValidateInput())
            //{
            //    ReminderSetUpResponse objResponse = new ReminderSetUpResponse();
            //    objResponse.StatusFl = false;
            //    objResponse.Msg = "Invalid Input Format";
            //    return objResponse;
            //}
            ReminderSetUpRequest remRequest = new ReminderSetUpRequest(rem);
            ReminderSetUpResponse remResponce = new ReminderSetUpResponse();
            remResponce = remRequest.MailReminderSave();

            return remResponce;
        }

        [HttpPost]
        [Route("ReminderSave")]
        public ReminderSetUpResponse ReminderSave()
        {
            if (HttpContext.Current.Session.Count == 0)
            {
                ReminderSetUpResponse objResponse = new ReminderSetUpResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = "SessionExpired";
                return objResponse;
            }
            string input;
            using (System.IO.StreamReader rd = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
            {

                input = rd.ReadToEnd();
            }

            ReminderSetUp rem = new JavaScriptSerializer().Deserialize<ReminderSetUp>(input);
            rem.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
            rem.Company_Id = Convert.ToString(HttpContext.Current.Session["CompanyId"]);
            rem.Created_By = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
            //if (!rem.ValidateInput())
            //{
            //    ReminderSetUpResponse objResponse = new ReminderSetUpResponse();
            //    objResponse.StatusFl = false;
            //    objResponse.Msg = "Invalid Input Format";
            //    return objResponse;
            //}
            ReminderSetUpRequest remRequest = new ReminderSetUpRequest(rem);
            ReminderSetUpResponse remResponce = new ReminderSetUpResponse();
            remResponce = remRequest.ReminderSave();

            return remResponce;
        }

        [HttpPost]
        [Route("MailReminderDelete")]
        public ReminderSetUpResponse MailReminderDelete()
        {
            if (HttpContext.Current.Session.Count == 0)
            {
                ReminderSetUpResponse objResponse = new ReminderSetUpResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = "SessionExpired";
                return objResponse;
            }
            string input;
            using (System.IO.StreamReader rd = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
            {

                input = rd.ReadToEnd();
            }

            ReminderSetUp rem = new JavaScriptSerializer().Deserialize<ReminderSetUp>(input);
            rem.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
            rem.Company_Id = Convert.ToString(HttpContext.Current.Session["CompanyId"]);
            rem.Created_By = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
            //if (!rem.ValidateInput())
            //{
            //    ReminderSetUpResponse objResponse = new ReminderSetUpResponse();
            //    objResponse.StatusFl = false;
            //    objResponse.Msg = "Invalid Input Format";
            //    return objResponse;
            //}
            ReminderSetUpRequest remRequest = new ReminderSetUpRequest(rem);
            ReminderSetUpResponse remResponce = new ReminderSetUpResponse();
            remResponce = remRequest.MailReminderDelete();

            return remResponce;



        }


        [HttpPost]
        [Route("ReminderDelete")]
        public ReminderSetUpResponse ReminderDelete()
        {
            if (HttpContext.Current.Session.Count == 0)
            {
                ReminderSetUpResponse objResponse = new ReminderSetUpResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = "SessionExpired";
                return objResponse;
            }
            string input;
            using (System.IO.StreamReader rd = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
            {

                input = rd.ReadToEnd();
            }

            ReminderSetUp rem = new JavaScriptSerializer().Deserialize<ReminderSetUp>(input);
            rem.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
            rem.Company_Id = Convert.ToString(HttpContext.Current.Session["CompanyId"]);
            rem.Created_By = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
            //if (!rem.ValidateInput())
            //{
            //    ReminderSetUpResponse objResponse = new ReminderSetUpResponse();
            //    objResponse.StatusFl = false;
            //    objResponse.Msg = "Invalid Input Format";
            //    return objResponse;
            //}
            ReminderSetUpRequest remRequest = new ReminderSetUpRequest(rem);
            ReminderSetUpResponse remResponce = new ReminderSetUpResponse();
            remResponce = remRequest.ReminderDelete();

            return remResponce;



        }

        [HttpPost]
        [Route("GetReminderName")]
        public ReminderSetUpResponse GetReminderName()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    ReminderSetUpResponse objResponse = new ReminderSetUpResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                ReminderSetUp objReminderSetup = new JavaScriptSerializer().Deserialize<ReminderSetUp>(input);
                objReminderSetup.Created_By = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                objReminderSetup.Company_Id = Convert.ToString(HttpContext.Current.Session["CompanyId"]);
                objReminderSetup.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);

                //if (!objReminderSetup.ValidateInput())
                //{
                //    ReminderSetUpResponse objResponse = new ReminderSetUpResponse();
                //    objResponse.StatusFl = false;
                //    objResponse.Msg = "Invalid Input Format";
                //    return objResponse;
                //}
                ReminderSetUpRequest resRequest = new ReminderSetUpRequest(objReminderSetup);
                ReminderSetUpResponse resResponse = resRequest.GetReminderName(objReminderSetup);
                return resResponse;
            }
            catch (Exception ex)
            {
                ReminderSetUpResponse objResponse = new ReminderSetUpResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }

        [HttpPost]
        [Route("GetMailEventName")]
        public ReminderSetUpResponse GetMailEventName()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    ReminderSetUpResponse objResponse = new ReminderSetUpResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                ReminderSetUp objReminderSetup = new JavaScriptSerializer().Deserialize<ReminderSetUp>(input);
                objReminderSetup.Created_By = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                objReminderSetup.Company_Id = Convert.ToString(HttpContext.Current.Session["CompanyId"]);
                objReminderSetup.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);

                //if (!objReminderSetup.ValidateInput())
                //{
                //    ReminderSetUpResponse objResponse = new ReminderSetUpResponse();
                //    objResponse.StatusFl = false;
                //    objResponse.Msg = "Invalid Input Format";
                //    return objResponse;
                //}
                ReminderSetUpRequest resRequest = new ReminderSetUpRequest(objReminderSetup);
                ReminderSetUpResponse resResponse = resRequest.GetMailEventName(objReminderSetup);

                return resResponse;
            }
            catch (Exception ex)
            {
                ReminderSetUpResponse objResponse = new ReminderSetUpResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }

        [HttpPost]
        [Route("GetFieldName")]
        public ReminderSetUpResponse GetFieldName()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    ReminderSetUpResponse objResponse = new ReminderSetUpResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                ReminderSetUp objReminderSetup = new JavaScriptSerializer().Deserialize<ReminderSetUp>(input);
                objReminderSetup.Created_By = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                objReminderSetup.Company_Id = Convert.ToString(HttpContext.Current.Session["CompanyId"]);
                objReminderSetup.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);

                //if (!objReminderSetup.ValidateInput())
                //{
                //    ReminderSetUpResponse objResponse = new ReminderSetUpResponse();
                //    objResponse.StatusFl = false;
                //    objResponse.Msg = "Invalid Input Format";
                //    return objResponse;
                //}
                ReminderSetUpRequest resRequest = new ReminderSetUpRequest(objReminderSetup);
                ReminderSetUpResponse resResponse = resRequest.GetFieldName(objReminderSetup);
                return resResponse;
            }
            catch (Exception ex)
            {
                ReminderSetUpResponse objResponse = new ReminderSetUpResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }

        [HttpPost]
        [Route("GetMailEventFieldName")]
        public ReminderSetUpResponse GetMailEventFieldName()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    ReminderSetUpResponse objResponse = new ReminderSetUpResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                ReminderSetUp objReminderSetup = new JavaScriptSerializer().Deserialize<ReminderSetUp>(input);
                objReminderSetup.Created_By = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                objReminderSetup.Company_Id = Convert.ToString(HttpContext.Current.Session["CompanyId"]);
                objReminderSetup.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);

                //if (!objReminderSetup.ValidateInput())
                //{
                //    ReminderSetUpResponse objResponse = new ReminderSetUpResponse();
                //    objResponse.StatusFl = false;
                //    objResponse.Msg = "Invalid Input Format";
                //    return objResponse;
                //}
                ReminderSetUpRequest resRequest = new ReminderSetUpRequest(objReminderSetup);
                ReminderSetUpResponse resResponse = resRequest.GetMailEventFieldName(objReminderSetup);
                return resResponse;
            }
            catch (Exception ex)
            {
                ReminderSetUpResponse objResponse = new ReminderSetUpResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }

        [HttpPost]
        [Route("MailEventSave")]
        public ReminderSetUpResponse MailEventSave()
        {
            if (HttpContext.Current.Session.Count == 0)
            {
                ReminderSetUpResponse objResponse = new ReminderSetUpResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = "SessionExpired";
                return objResponse;
            }
            string input;
            using (System.IO.StreamReader rd = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
            {

                input = rd.ReadToEnd();
            }
            ReminderSetUp rem = new JavaScriptSerializer().Deserialize<ReminderSetUp>(input);
            rem.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
            rem.Company_Id = Convert.ToString(HttpContext.Current.Session["CompanyId"]);
            rem.Created_By = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
            //if (!rem.ValidateInput())
            //{
            //    ReminderSetUpResponse objResponse = new ReminderSetUpResponse();
            //    objResponse.StatusFl = false;
            //    objResponse.Msg = "Invalid Input Format";
            //    return objResponse;
            //}
            ReminderSetUpRequest remRequest = new ReminderSetUpRequest(rem);
            ReminderSetUpResponse remResponce = new ReminderSetUpResponse();
            remResponce = remRequest.ReminderSave();
            return remResponce;

        }

    }

}
