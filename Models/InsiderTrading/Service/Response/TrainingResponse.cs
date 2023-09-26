using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class TrainingResponse : BaseResponse
    {
        private TrainingModule _trainingModule;
        private TrainingModuleDetail _trainingModuleDetail;
        private List<TrainingModule> lstTrainingModule;
        public TrainingModule TrainingModule
        {
            set
            {
                _trainingModule = value;
            }
            get
            {
                return _trainingModule;
            }
        }

        public TrainingModuleDetail TrainingModuleDetail
        {
            set
            {
                _trainingModuleDetail = value;
            }
            get
            {
                return _trainingModuleDetail;
            }
        }

        public List<TrainingModule> TrainingModuleList
        {
            set
            {
                lstTrainingModule = value;
            }
            get
            {
                return lstTrainingModule;
            }
        }
        public void AddObject(TrainingModule o)
        {
            if (lstTrainingModule == null)
            {
                lstTrainingModule = new List<TrainingModule>();
            }
            lstTrainingModule.Add(o);
        }
    }
}