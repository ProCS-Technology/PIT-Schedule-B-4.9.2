using System;

namespace ProcsDLL.Models.Model
{
    public class Group : BaseEntity
    {
        public Int32 GROUP_ID { get; set; }
        public String GROUP_NM { get; set; }
        public String GROUP_STATUS { get; set; }
        public String SUBSCRIPTION_ST_DT { get; set; }
        public String SUBSCRIPTION_EN_DT { get; set; }
        public String CREATE_BY { get; set; }
        public String CREATED_ON { get; set; }
        public String LOGO { get; set; }

        //private Int32 _GROUP_ID;
        //private String _GROUP_NM;
        //private String _GROUP_STATUS;
        //private String _SUBSCRIPTION_ST_DT;
        //private String _SUBSCRIPTION_EN_DT;
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
        //public String GROUP_NM
        //{
        //    set
        //    {
        //        _GROUP_NM = value;
        //    }
        //    get
        //    {
        //        return _GROUP_NM;
        //    }
        //}
        //public String GROUP_STATUS
        //{
        //    set
        //    {
        //        _GROUP_STATUS = value;
        //    }
        //    get
        //    {
        //        return _GROUP_STATUS;
        //    }
        //}
        //public String SUBSCRIPTION_ST_DT
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
        //public String SUBSCRIPTION_EN_DT
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
        public override void Validate()
        {
            base.Validate();
        }
    }
}