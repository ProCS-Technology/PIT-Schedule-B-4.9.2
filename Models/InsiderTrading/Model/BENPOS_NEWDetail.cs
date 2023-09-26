using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class BENPOS_NEWDetail : BaseEntity
    {
        public BenposHeader benposHeader { get; set; }
        public String folioNo { get; set; }
        public String panNumber { get; set; }
        public String shareHolderName { get; set; }
        public Int32 holding { get; set; }
        public override void Validate()
        {
            base.Validate();
        }
    }
}