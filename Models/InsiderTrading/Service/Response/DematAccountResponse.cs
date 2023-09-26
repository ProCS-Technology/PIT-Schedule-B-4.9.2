using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class DematAccountResponse : BaseResponse
    {
        private DematAccount _dematAccount;
        private List<DematAccount> lstDematAccount;

        public DematAccount DematAccount
        {
            set
            {
                _dematAccount = value;
            }
            get
            {
                return _dematAccount;
            }
        }

        public List<DematAccount> DematAccountList
        {
            set
            {
                lstDematAccount = value;
            }
            get
            {
                return lstDematAccount;
            }
        }

        public void AddObject(DematAccount o)
        {
            if (lstDematAccount == null)
            {
                lstDematAccount = new List<DematAccount>();
            }
            lstDematAccount.Add(o);
        }
    }
}