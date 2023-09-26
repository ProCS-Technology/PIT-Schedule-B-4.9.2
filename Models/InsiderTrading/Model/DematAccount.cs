using System;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class DematAccount : BaseEntity
    {
        public Int32 ID { get; set; }
        public String lastModifiedOn { get; set; }
        public Int32 version { get; set; }
        public Int32 companyId { get; set; }
        public String depositoryName { get; set; }
        public String clientId { get; set; }
        public String depositoryParticipantName { get; set; }
        public String depositoryParticipantId { get; set; }
        public String tradingMemberId { get; set; }
        public String dematType { get; set; }
        public String accountNo { get; set; }
        public string CurrentHolding { set; get; }
        public string Pledged { set; get; }
        public String status { get; set; }
        public Relative relative { get; set; }
        public bool isDeleteDemat { get; set; }

        public Int32 RELATIVE_ID { get; set; }
        public string RELATIVE_NM { get; set; }
 
        public int FY_INITIAL { set; get; }
        public int FY_LAST { set; get; }
        public string FINANCIAL_YEAR { set; get; }
   
    }
}