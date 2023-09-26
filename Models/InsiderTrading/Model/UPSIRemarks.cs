using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class UPSIRemarks : BaseEntity
    {

        public string HdrId { get; set; }
        public string Email { get; set; }
        public string GroupId { get; set; }

        public string msgBody { get; set; }
        public string mailDate { get; set; }
        public List<UPSIRemarksUser> listUserDetail { get; set; }
        public List<UPSIRemarksAttachments> listRemarksAttachments { get; set; }



    }
}