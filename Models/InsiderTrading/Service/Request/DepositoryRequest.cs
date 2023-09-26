using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class DepositoryRequest
    {
        private Depository _Depository;

        private List<Depository> _DepositoryList;

        public DepositoryRequest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DepositoryRequest(Depository depository)
        {
            _Depository = new Depository();
            _Depository = depository;

        }

        public DepositoryRequest(List<Depository> deposiotryList)
        {
            _DepositoryList = new List<Depository>();
            _DepositoryList = deposiotryList;

        }

        public DepositoryResponse SaveDepositoryTypeOperation()
        {
            UserRepository oRepository = new UserRepository();
            return oRepository.SaveDepositoryTypeOperation(_Depository);
        }

        public DepositoryResponse SaveThresholdLimitAndByTime()
        {
            UserRepository oRepository = new UserRepository();
            return oRepository.SaveThresholdLimitAndByTime(_DepositoryList);
        }

        public DepositoryResponse GetDepositoryType()
        {
            UserRepository oRepository = new UserRepository();
            return oRepository.GetDepositoryType(_Depository);
        }

        public DepositoryResponse GetThresholdByTimeSettings()
        {
            UserRepository oRepository = new UserRepository();
            return oRepository.GetThresholdByTimeSettings(_Depository);
        }
    }
}