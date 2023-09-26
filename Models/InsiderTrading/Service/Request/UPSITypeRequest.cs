using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;
namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class UPSITypeRequest
    {
        private UPSIType _upsiType;
        public UPSITypeRequest(UPSIType upsiType)
        {
            _upsiType = new UPSIType();
            _upsiType = upsiType;
        }
        public UPSITypeResponse GetUPSITypeList()
        {
            UPSITypeRepository upsiTypRepos = new UPSITypeRepository();
            return upsiTypRepos.GetUPSITypeList(_upsiType);
        }
        public UPSITypeResponse GetModeofCommunication()
        {
            UPSITypeRepository upsiTypRepos = new UPSITypeRepository();
            return upsiTypRepos.GetModeofCommunication(_upsiType);
        }
        public UPSITypeResponse AddUpdateUPSIType()
        {
            UPSITypeRepository upsiTypRepos = new UPSITypeRepository();
            if (_upsiType.TypeId == 0)
            {
                return upsiTypRepos.AddUPSIType(_upsiType);
            }
            else
            {
                return upsiTypRepos.UpdateUPSIType(_upsiType);
            }
        }
        public UPSITypeResponse GetUPSITypeById()
        {
            UPSITypeRepository upsiTypRepos = new UPSITypeRepository();
            return upsiTypRepos.GetUPSITypeById(_upsiType);
        }
    }
}