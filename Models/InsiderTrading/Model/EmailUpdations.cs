using System;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class EmailUpdations : BaseEntity
    {

        public String UserEmail { get; set; }
        public Int32 UserId { get; set; }
        public String UserLoginId { get; set; }
        public String UserNewEmail { get; set; }
        public String CREATE_BY { get; set; }
        public Int32 COMPANY_ID { get; set; }
        public string IS_APPROVER { get; set; }
        public string UserName { get; set; }

    }
}