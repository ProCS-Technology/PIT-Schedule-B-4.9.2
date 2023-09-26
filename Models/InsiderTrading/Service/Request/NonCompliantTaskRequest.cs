using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;

namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class NonCompliantTaskRequest
    {
        private NonCompliantTask _nonCompliantTask;

        public NonCompliantTaskRequest()
        {
            _nonCompliantTask = new NonCompliantTask();
        }

        public NonCompliantTaskRequest(NonCompliantTask nonCompliantTask)
        {
            _nonCompliantTask = new NonCompliantTask();
            _nonCompliantTask = nonCompliantTask;
        }

        public NonCompliantTaskResponse GetAllNonCompliantTask()
        {
            try
            {
                NonCompliantTaskRepository oRepository = new NonCompliantTaskRepository();
                return oRepository.GetAllNonCompliant(_nonCompliantTask);
            }
            catch (Exception ex)
            {
                throw new Exception(Convert.ToString(ex.Message));
            }
        }

        public NonCompliantTaskResponse CloseNonCompliantTask()
        {
            try
            {
                NonCompliantTaskRepository oRepository = new NonCompliantTaskRepository();
                return oRepository.CloseNonCompliantTask(_nonCompliantTask);
            }
            catch (Exception ex)
            {
                throw new Exception(Convert.ToString(ex.Message));
            }
        }

        public NonCompliantTaskResponse RunNowCompliantFinder()
        {
            try
            {
                NonCompliantTaskRepository oRepository = new NonCompliantTaskRepository();
                return oRepository.RunNowCompliantFinder(_nonCompliantTask);
            }
            catch (Exception ex)
            {
                throw new Exception(Convert.ToString(ex.Message));
            }
        }

        public NonCompliantTaskResponse SendEmailForNC()
        {
            try
            {
                NonCompliantTaskRepository oRepository = new NonCompliantTaskRepository();
                return oRepository.SendEmailForNC(_nonCompliantTask);
            }
            catch (Exception ex)
            {
                throw new Exception(Convert.ToString(ex.Message));
            }
        }
    }
}