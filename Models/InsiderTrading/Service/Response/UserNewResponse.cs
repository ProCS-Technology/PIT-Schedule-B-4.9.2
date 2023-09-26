using ProcsDLL.Models.InsiderTrading.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class UserNewResponse : BaseResponse
    {

        private UserNewHeader _UserNewHeader;
        private List<UserNewHeader> lstUserNewHdr;
        //private UserNewDetail _UserNewDetail;
        //private List<UserNewDetail> lstUserNewDtl;

        public UserNewHeader UserNewHeader
        {
            set
            {
                _UserNewHeader = value;
            }
            get
            {
                return _UserNewHeader;
            }
        }
        public List<UserNewHeader> UserNewHeaderList
        {
            set
            {
                lstUserNewHdr = value;
            }
            get
            {
                return lstUserNewHdr;
            }
        }
        public void AddObject(UserNewHeader o)
        {
            if (lstUserNewHdr == null)
            {
                lstUserNewHdr = new List<UserNewHeader>();
            }
            lstUserNewHdr.Add(o);
        }

    }
}


