using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;

namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class TrainingRequest
    {
        private TrainingModule _trainingModule;
        public TrainingRequest()
        {
            _trainingModule = new TrainingModule();
        }

        public TrainingRequest(TrainingModule trainingModule)
        {
            _trainingModule = new TrainingModule();
            _trainingModule = trainingModule;
        }

        public TrainingResponse GetTrainingModules()
        {
            TrainingRepository oRepository = new TrainingRepository();
            return oRepository.GetTrainingModulesReport(_trainingModule);
        }

        public TrainingResponse GetTrainingReport()
        {
            TrainingRepository oRepository = new TrainingRepository();
            return oRepository.GetTrainingReport(_trainingModule);
        }

        public TrainingResponse GetTrainingFileOnLoadToPdfViewer()
        {
            TrainingRepository oRepository = new TrainingRepository();
            return oRepository.GetTrainingFileOnLoadToPdfViewer(_trainingModule);
        }

        public TrainingResponse GetTrainingFileOnNextButtonToPdfViewer()
        {
            TrainingRepository oRepository = new TrainingRepository();
            return oRepository.GetTrainingFileOnNextButtonToPdfViewer(_trainingModule);
        }

        public TrainingResponse GetTrainingFileOnPreviousButtonToPdfViewer()
        {
            TrainingRepository oRepository = new TrainingRepository();
            return oRepository.GetTrainingFileOnPreviousButtonToPdfViewer(_trainingModule);
        }

        public TrainingResponse OnSubmissionOfTrainingFile()
        {
            TrainingRepository oRepository = new TrainingRepository();
            return oRepository.OnSubmissionOfTrainingFile(_trainingModule);
        }

        public TrainingResponse GetAllTrainingModulesMaster()
        {
            TrainingRepository oRepository = new TrainingRepository();
            return oRepository.GetAllTrainingModulesMaster(_trainingModule);
        }

        public TrainingResponse SaveTrainingModule()
        {
            TrainingRepository oRepository = new TrainingRepository();
            return oRepository.SaveTrainingModule(_trainingModule);
        }

        public TrainingResponse GetAllTrainingModulesById()
        {
            TrainingRepository oRepository = new TrainingRepository();
            return oRepository.GetAllTrainingModulesById(_trainingModule);
        }

        public TrainingResponse DeleteTrainingModule()
        {
            TrainingRepository oRepository = new TrainingRepository();
            return oRepository.DeleteTrainingModule(_trainingModule);
        }
    }
}