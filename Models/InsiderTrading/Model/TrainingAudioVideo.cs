using System;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class TrainingAudioVideo : BaseEntity
    {
        public Int32 Id { get; set; }
        public string fileName { get; set; }
        public Int32 sequence { get; set; }
    }
}