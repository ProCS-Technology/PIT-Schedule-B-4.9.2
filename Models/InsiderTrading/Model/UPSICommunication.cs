using System;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class UPSICommunication : BaseEntity
    {
        public Int32 companyId { get; set; }
        public Int32 hdrId { get; set; }
        public Int32 lineId { get; set; }
        public string body { get; set; }
        public string subject { get; set; }
        public string to { get; set; }
        public string from { get; set; }
        public string cc { get; set; }
        public string bcc { get; set; }
        public string emailSentOn { get; set; }
        public string subjectCreatedOn { get; set; }
        public string subjectCreatedBy { get; set; }
        public List<string> upsiSubLineAttachments { get; set; }
        public string upsiFrom { get; set; }
        public string upsiTo { get; set; }
        public string upsiCommunicationFrom { get; set; }

        #region "UPSI Template Reporting"
        public Int32 UPSITemplateId { get; set; }
        public string natureOfUPSI { get; set; }
        public string whoShared { get; set; }
        public string withWhomShared { get; set; }
        public string panOrOtherIdentification { get; set; }
        public string sharedOn { get; set; }
        public string modeOfSharing { get; set; }
        public string attachmentShared { get; set; }
        public string createdOn { get; set; }
        public string remarks { get; set; }
        #endregion

    }
}