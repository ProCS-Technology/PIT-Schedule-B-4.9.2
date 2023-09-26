using System;
namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class UPSIConfig : BaseEntity
    {
        public string EmailAutomation { set; get; }
        public string MultipleEmail { set; get; }
        public string AccessibleToCO { set; get; }
        public string ManagedToCO { get; set; }
        public string AccessibleType { set; get; }
        public string UserLogin { set; get; }
        public Int32 CompanyId { set; get; }
        public string AuthorizedUsr { set; get; }
        public string AssignTask { set; get; }
    }
}