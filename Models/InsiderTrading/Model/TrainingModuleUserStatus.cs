using System;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class TrainingModuleUserStatus : BaseEntity
    {
        public Int32 Id { get; set; }
        public Int32 trainingId { get; set; }
        public string userLogin { get; set; }
        public User userDetail { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public Int32 currentPage { get; set; }
        public string status { get; set; }
        public string remarks { get; set; }
        public string submittedOn { get; set; }
    }
}