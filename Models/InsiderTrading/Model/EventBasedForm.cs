using System;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class EventBasedForm : BaseEntity
    {
        public Int32 formId { get; set; }
        public Int32 companyId { get; set; }
        public string mainEvent { get; set; }
        public string subEvent { get; set; }
        public string fileName { get; set; }
        public string formName { get; set; }
        public string formTemplate { get; set; }
        public string formOrientation { get; set; }
        public string Task_Id { get; set; }
    }
}