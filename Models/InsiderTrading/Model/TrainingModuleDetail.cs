using System;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class TrainingModuleDetail : BaseEntity
    {
        public Int32 Id { get; set; }
        public Int32 trainingId { get; set; }
        public string trainingDocument { get; set; }
        public Int32 sequence { get; set; }
    }
}