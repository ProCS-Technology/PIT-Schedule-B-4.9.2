using System;
using System.Collections.Generic;
namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class UPSIGrp : BaseEntity
    {
        public Int32 CompanyId { set; get; }
        public Int32 GrpId { set; get; }
        public string GrpNm { set; get; }
        public string GrpDesc { set; get; }
        public Int32 TypId { set; get; }
        public string TypNm { set; get; }
        public string ValidFrom { set; get; }
        public string ValidTo { set; get; }
        public string GrpStatus { set; get; }
        public string CreatedBy { get; set; }
        public string SharedBy { get; set; }
        public string SharedFrm { get; set; }
        public string Remarks { get; set; }
        public string CanEdit { get; set; }
        public Int32 DPCnt { set; get; }
        public Int32 CPCnt { set; get; }
        public Int32 COMMCnt { set; get; }
        public string IsGroupOwner { get; set; }
        public List<UPSIKeywords> Keywords { set; get; }
        public List<ConnectedPerson> ConnectedPersons { set; get; }
    }
}