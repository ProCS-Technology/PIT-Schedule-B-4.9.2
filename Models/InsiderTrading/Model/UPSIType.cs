using System;
using System.Collections.Generic;
namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class UPSIType : BaseEntity
    {
        public Int32 TypeId { set; get; }
        public string TypeNm { set; get; }
        public string Status { set; get; }
        public Int32 CompanyId { set; get; }
        public string CreatedBy { set; get; }
        public List<UPSIKeywords> Keywords { set; get; }
        public Int32 COMMTYPE_ID { get; set; }

        public String COMMTYPE_NAME { get; set; }

        public override void Validate()
        {
            base.Validate();
        }
    }
    public class UPSIKeywords : BaseEntity
    {
        public string keyword { set; get; }
        public Int32 sequence { set; get; }
    }



}