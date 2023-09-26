using System;

namespace ProcsDLL.Models.Model
{
    public class CompanyModule : BaseEntity
    {
        private Int32 _COMPANY_ID;
        private Int32 _MODULE_ID;
        private String _MODULE_STATUS;
        private DateTime _MODULE_ST_DT;
        private DateTime _MODULE_EN_DT;
        private String _CREATE_BY;
        private DateTime _CREATED_ON;
        private String _LOGO;

        public Int32 COMPANY_ID
        {
            set
            {
                _COMPANY_ID = value;
            }
            get
            {
                return _COMPANY_ID;
            }
        }
        public Int32 MODULE_ID
        {
            set
            {
                _MODULE_ID = value;
            }
            get
            {
                return _MODULE_ID;
            }
        }

        public String MODULE_STATUS
        {
            set
            {
                _MODULE_STATUS = value;
            }
            get
            {
                return _MODULE_STATUS;
            }
        }
        public DateTime MODULE_ST_DT
        {
            set
            {
                _MODULE_ST_DT = value;
            }
            get
            {
                return _MODULE_ST_DT;
            }
        }
        public DateTime MODULE_EN_DT
        {
            set
            {
                _MODULE_EN_DT = value;
            }
            get
            {
                return _MODULE_EN_DT;
            }
        }
        public String CREATE_BY
        {
            set
            {
                _CREATE_BY = value;
            }
            get
            {
                return _CREATE_BY;
            }
        }
        public DateTime CREATED_ON
        {
            set
            {
                _CREATED_ON = value;
            }
            get
            {
                return _CREATED_ON;
            }
        }
        public String LOGO
        {
            set
            {
                _LOGO = value;
            }
            get
            {
                return _LOGO;
            }
        }
        public override void Validate()
        {
            base.Validate();
            //if (_companyNm.Length == 0)
            //{
            //    AddRule("Company name is missing");
            //}
            //if (_sapCode.Length == 0)
            //{
            //    AddRule("Sap Code is missing");
            //}
            //if (_status.Length == 0)
            //{
            //    AddRule("Status is missing");
            //}
        }
    }
}