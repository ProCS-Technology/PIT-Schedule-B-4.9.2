namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class UPSIRpt : BaseEntity
    {
        public string SharedBy { set; get; }
        public string SharedByIdentification { set; get; }
        public string EmailIDofSender { get; set; }
        public string DateofentryinSDD { get; set; }
       public string TimestampforB { get; set; }
        public string UPSIReportedthrough { get; set; }
        public string SharedWith { set; get; }
        public string SharedWithIdentification { set; get; }
        public string FirmNm { set; get; }
        public string SharedOn { set; get; }
        public string SharedTime { set; get; }
        public string UpsiTyp { set; get; }
        public string CommMode { set; get; }
        public string ValidFrm { set; get; }
        public string CreatedOn { set; get; }
        public string CreatedTm { set; get; }
        public string Remarks { set; get; }
        public string NoticeSent { set; get; }
        public string Attachment { get; set; }
       
    }
    
}