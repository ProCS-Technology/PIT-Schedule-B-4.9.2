using System;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class UPSI : BaseEntity
    {
        public Int32 upsi_id { get; set; }
        public string upsi_group { get; set; }
        public string upsi_desc { get; set; }
        public string from_date { get; set; }
        public string till_date { get; set; }
        public Int32 version { get; set; }
        public string created_by { get; set; }
        public int noofmember { get; set; }
        public string checked_status { get; set; }
        public List<User> listuser { get; set; }
        public bool StatusFl { get; set; }
        public string Msg { get; set; }
        public string MODULE_DATABASE { get; set; }
        public string created_on { get; set; }
        public string COMPANY_ID { get; set; }
        public string upsiType { get; set; }
        public string upsiSharedWith { get; set; }
        public string upsiSharedOn { get; set; }
        public string upsiSharedCC { get; set; }
        public string upsiPan { get; set; }
        public string remarks { get; set; }
        public string upsiAttachment { get; set; }
        public List<User> listGroupUserRemarks { get; set; }
        public string UPSITemplate { get; set; }

        public override void Validate()
        {
            base.Validate();
        }
    }
}