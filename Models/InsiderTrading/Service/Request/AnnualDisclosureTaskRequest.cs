using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using System.Web.Script.Serialization;

namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class AnnualDisclosureTaskRequest
    {
        private AnnualDisclosureTask _AnnualDisclosureTask;

        public AnnualDisclosureTaskRequest()
        {

        }

        public AnnualDisclosureTaskRequest(AnnualDisclosureTask AnnualDisclosureTask)
        {
            _AnnualDisclosureTask = new AnnualDisclosureTask();
            _AnnualDisclosureTask = AnnualDisclosureTask;
        }
        public AnnualDisclosureTaskResponse SaveAnnualDisclosureTask()
        {
            _AnnualDisclosureTask.Validate();

            if (_AnnualDisclosureTask.GetRules().Count == 0)
            {

                AnnualDisclosureTaskRepository oRepository = new AnnualDisclosureTaskRepository();
                if (_AnnualDisclosureTask.id == 0)
                {
                    return oRepository.SaveAnnualDisclosureTask(_AnnualDisclosureTask);
                }
                else
                {
                    return oRepository.UpdateAnnualDisclosureTask(_AnnualDisclosureTask);
                }
            }
            return null;
        }
    
        public AnnualDisclosureTaskResponse DeleteAnnualDisclosureTask()
        {
            AnnualDisclosureTaskRepository oRepository = new AnnualDisclosureTaskRepository();
            return oRepository.DeleteAnnualDisclosureTask(_AnnualDisclosureTask);
        }
        public AnnualDisclosureTaskResponse GetAnnualDisclosureTaskClosureInfo()
        {
            AnnualDisclosureTaskRepository oRepositry = new AnnualDisclosureTaskRepository();
            return oRepositry.GetAnnualDisclosureTaskClosureInfo(_AnnualDisclosureTask);
        }

        public AnnualDisclosureTaskResponse GetAnnualDisclosureTaskClosureInfoList()
        {
            AnnualDisclosureTaskRepository oRepositry = new AnnualDisclosureTaskRepository();
            return oRepositry.GetAnnualDisclosureTaskClosureInfoList(_AnnualDisclosureTask);
        }

        //public AnnualDisclosureTaskResponse SendEmailForTradingWindowClosure()
        //{
        //    InsiderTradingWindowRepository oRepository = new InsiderTradingWindowRepository();
        //    return oRepository.SendEmailForTradingWindowClosure(_InsiderTradingWindow);
        //}

        //public AnnualDisclosureTaskResponse GetAnnualDisclosureTask()
        //{
        //    AnnualDisclosureTaskRepository oRepository = new AnnualDisclosureTaskRepository();
        //    return oRepository.GetAnnualDisclosureTask(_AnnualDisclosureTask);
        //}
    }

}