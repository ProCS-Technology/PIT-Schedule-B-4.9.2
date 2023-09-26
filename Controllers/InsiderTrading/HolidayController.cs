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
    [RoutePrefix("api/Holiday")]
    public class HolidayController : ApiController
    {
        string sXSSErrMsg = Convert.ToString(ConfigurationManager.AppSettings["XSSErrMsg"]);
        [Route("GetHolidayList")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Holiday APIs" })]
        public HolidayResponse GetHolidayList()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    HolidayResponse objHoliday = new HolidayResponse();
                    objHoliday.StatusFl = false;
                    objHoliday.Msg = "SessionExpired";
                    return objHoliday;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                Holiday Holiday = new JavaScriptSerializer().Deserialize<Holiday>(input);
                Holiday.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!Holiday.ValidateInput())
                {
                    HolidayResponse objResponse = new HolidayResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                HolidayRequest gReqHolidayList = new HolidayRequest(Holiday);
                HolidayResponse gResHolidayList = gReqHolidayList.GetHolidayList();
                return gResHolidayList;
            }
            catch (Exception ex)
            {
                HolidayResponse objResponse = new HolidayResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("SaveHoliday")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Holiday APIs" })]
        public HolidayResponse SaveHoliday()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    HolidayResponse objResponse = new HolidayResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                Holiday Holiday = new JavaScriptSerializer().Deserialize<Holiday>(input);
                Holiday.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!Holiday.ValidateInput())
                {
                    HolidayResponse objResponse = new HolidayResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                HolidayRequest gReqHolidayList = new HolidayRequest(Holiday);
                HolidayResponse gResHolidayList = gReqHolidayList.SaveHoliday();
                return gResHolidayList;
            }
            catch (Exception ex)
            {
                HolidayResponse objResponse = new HolidayResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("DeleteHoliday")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Holiday APIs" })]
        public HolidayResponse DeleteHoliday()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    HolidayResponse objResponse = new HolidayResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                Holiday Holiday = new JavaScriptSerializer().Deserialize<Holiday>(input);
                Holiday.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!Holiday.ValidateInput())
                {
                    HolidayResponse objResponse = new HolidayResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                HolidayRequest gReqHolidayList = new HolidayRequest(Holiday);
                HolidayResponse gResHolidayList = gReqHolidayList.DeleteHoliday();
                return gResHolidayList;
            }
            catch (Exception ex)
            {
                HolidayResponse objResponse = new HolidayResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
    }
}