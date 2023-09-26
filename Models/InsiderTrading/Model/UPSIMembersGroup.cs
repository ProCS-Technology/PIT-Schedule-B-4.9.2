using System;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class UPSIMembersGroup : BaseEntity
    {
        public Int32 GROUP_ID { get; set; }
        public string GROUP_NM { get; set; }
        public string GROUP_DESC { get; set; }
        public string VALID_FROM { get; set; }
        public string VALID_TLL { get; set; }
        public string GROUP_OWER { get; set; }
        public string STATUS { get; set; }
        public string GROUP_TYPE { get; set; }
        public string VERSION { get; set; }
        public string TotalMembers { get; set; }

        public List<UPSIVendor> lisVender = new List<UPSIVendor>();
        public List<User> listUser = new List<User>();
        public List<UPSIMembersDesignatedAndNon> listDesignatedMember = new List<UPSIMembersDesignatedAndNon>();
        public List<UPSIMembersDesignatedAndNon> listNonDesignatedMember = new List<UPSIMembersDesignatedAndNon>();
        public List<UPSIRemarks> listGroupUserRemarks = new List<UPSIRemarks>();
        public List<UPSIGroupType> listGroupType = new List<UPSIGroupType>();
        public Int32 CompanyId { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }




    }
}