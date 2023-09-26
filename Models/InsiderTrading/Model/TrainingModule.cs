using System;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class TrainingModule : BaseEntity
    {
        public Int32 trainingId { get; set; }
        public string trainingName { get; set; }
        public string trainingStartDate { get; set; }
        public string trainingEndDate { get; set; }
        public string trainingDocument { get; set; }
        public List<TrainingAudioVideo> lstTrainingAudioVideo { get; set; }
        public Int32 noOfPages { get; set; }
        public Int32 companyId { get; set; }
        public string userLogin { get; set; }
        public string createdOn { get; set; }
        public string createdBy { get; set; }
        public string modifiedOn { get; set; }
        public string modifiedBy { get; set; }
        public string trainingFrom { get; set; }
        public string trainingTo { get; set; }
        public TrainingModuleUserStatus trainingModuleUserStatus { get; set; }
    }
}