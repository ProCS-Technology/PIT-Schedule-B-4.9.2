using System;

namespace ProcsDLL.Models.Model
{
    public class Company : BaseEntity
    {
        public Int32 GROUP_ID { get; set; }
        public Int32 COMPANY_ID { get; set; }
        public String COMPANY_NM { get; set; }
        public String COMPANY_STATUS { get; set; }
        public string subscription_st_dt { get; set; }
        public string SUBSCRIPTION_en_dt { get; set; }
        public string Logo { get; set; }
        public String createdBy { get; set; }
        public string createOn { get; set; }

        //private Int32 _GROUP_ID;
        //private Int32 _COMPANY_ID;
        //private String _COMPANY_NM;
        //private String _COMPANY_STATUS;
        //private DateTime _SUBSCRIPTION_ST_DT;
        //private DateTime _SUBSCRIPTION_EN_DT;
        //private String _CREATE_BY;
        //private DateTime _CREATED_ON;
        //private String _LOGO;

        //public Int32 GROUP_ID
        //{
        //    set
        //    {
        //        _GROUP_ID = value;
        //    }
        //    get
        //    {
        //        return _GROUP_ID;
        //    }
        //}
        //public Int32 COMPANY_ID
        //{
        //    set
        //    {
        //        _COMPANY_ID = value;
        //    }
        //    get
        //    {
        //        return _COMPANY_ID;
        //    }
        //}
        //public String COMPANY_NM
        //{
        //    set
        //    {
        //        _COMPANY_NM = value;
        //    }
        //    get
        //    {
        //        return _COMPANY_NM;
        //    }
        //}
        //public String COMPANY_STATUS
        //{
        //    set
        //    {
        //        _COMPANY_STATUS = value;
        //    }
        //    get
        //    {
        //        return _COMPANY_STATUS;
        //    }
        //}
        //public DateTime SUBSCRIPTION_ST_DT
        //{
        //    set
        //    {
        //        _SUBSCRIPTION_ST_DT = value;
        //    }
        //    get
        //    {
        //        return _SUBSCRIPTION_ST_DT;
        //    }
        //}
        //public DateTime SUBSCRIPTION_EN_DT
        //{
        //    set
        //    {
        //        _SUBSCRIPTION_EN_DT = value;
        //    }
        //    get
        //    {
        //        return _SUBSCRIPTION_EN_DT;
        //    }
        //}
        //public String CREATE_BY
        //{
        //    set
        //    {
        //        _CREATE_BY = value;
        //    }
        //    get
        //    {
        //        return _CREATE_BY;
        //    }
        //}
        //public DateTime CREATED_ON
        //{
        //    set
        //    {
        //        _CREATED_ON = value;
        //    }
        //    get
        //    {
        //        return _CREATED_ON;
        //    }
        //}
        //public String LOGO
        //{
        //    set
        //    {
        //        _LOGO = value;
        //    }
        //    get
        //    {
        //        return _LOGO;
        //    }
        //}
        //public override void Validate()
        //{
        //    base.Validate();
        //}
    }
}