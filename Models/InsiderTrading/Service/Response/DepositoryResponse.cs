using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class DepositoryResponse : BaseResponse
    {
        private Depository _Depository;

        private List<Depository> lstDepository;


        public Depository Depository
        {
            set
            {
                _Depository = value;
            }
            get
            {
                return _Depository;
            }
        }
        public List<Depository> DepositoryList
        {
            set
            {
                lstDepository = value;
            }
            get
            {
                return lstDepository;
            }
        }

        public void AddObject(Depository o)
        {
            if (lstDepository == null)
            {
                lstDepository = new List<Depository>();
            }
            lstDepository.Add(o);
        }
    }
}