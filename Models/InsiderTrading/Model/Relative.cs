using System;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class Relative : BaseEntity
    {
        public Int32 ID { set; get; }
        public String lastModifiedOn { set; get; }
        public Int32 version { set; get; }
        public Int32 companyId { set; get; }
        public String relativeName { set; get; }
        public Relation relation { set; get; }
        public String panNumber { set; get; }
        public String identificationType { set; get; }
        public String identificationNumber { set; get; }
        public String mobile { set; get; }
        public String address { set; get; }
        public String pinCode { set; get; }
        public String country { set; get; }
        public String status { set; get; }
        public String remarks { set; get; }
        public string relativeEmail { set; get; }
        public List<DematAccount> lstDematAccount { set; get; }
        public String createdBy { set; get; }
        public String moduleDatabase { set; get; }
        public bool isDeleteRelative { set; get; }
        public String IsdesignatedPerson { set; get; }
        public string IsMandatory { set; get; }
        public string LastTransactionDt { set; get; }
    }
}