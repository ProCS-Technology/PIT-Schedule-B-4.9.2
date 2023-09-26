using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class CP : BaseEntity
    {
        public Int32 CompanyId { set; get; }
        public string CPFirm { set; get; }
        public string CPName { set; get; }
        public string CPEmail { set; get; }
        public string CPIdentificationTyp { set; get; }
        public string CPIdentificationNo { set; get; }
        public string CPStatus { set; get; }
        public string CPConflict { set; get; }
        public string CPConflictFrm { set; get; }
        public string CreatedBy { set; get; }
    }
}