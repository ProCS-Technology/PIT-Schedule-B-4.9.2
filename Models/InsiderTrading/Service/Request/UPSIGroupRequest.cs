using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;

namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class UPSIGroupRequest
    {
        private UPSIGrp _upsiGrp;
        public UPSIGroupRequest()
        {
        }
        public UPSIGroupRequest(UPSIGrp upsiGrp)
        {
            _upsiGrp = upsiGrp;
        }
        public UPSIGroupResponse GetUPSIType()
        {
            UPSIGroupRepository upsiRepository = new UPSIGroupRepository();
            return upsiRepository.GetUPSIType(_upsiGrp);
        }
        public UPSIGroupResponse GetUPSIGroups()
        {
            UPSIGroupRepository upsiRepository = new UPSIGroupRepository();
            return upsiRepository.GetUPSIGroups(_upsiGrp);
        }
        public UPSIGroupResponse SaveUPSIGroup()
        {
            UPSIGroupRepository upsiRepository = new UPSIGroupRepository();
            return upsiRepository.SaveUPSIGroup(_upsiGrp);
        }
        public UPSIGroupResponse GetUPSIConnectedPersons()
        {
            UPSIGroupRepository upsiRepository = new UPSIGroupRepository();
            return upsiRepository.GetUPSIConnectedPersons(_upsiGrp);
        }
        public UPSIGroupResponse GetUPSIDesignatedPersons()
        {
            UPSIGroupRepository upsiRepository = new UPSIGroupRepository();
            return upsiRepository.GetUPSIDesignatedPersons(_upsiGrp);
        }
        public UPSIGroupResponse SaveUPSIConnectedPersons()
        {
            UPSIGroupRepository upsiRepository = new UPSIGroupRepository();
            return upsiRepository.SaveUPSIConnectedPersons(_upsiGrp);
        }
        public UPSIGroupResponse SaveUPSIGroupCommunication()
        {
            UPSIGroupRepository upsiRepository = new UPSIGroupRepository();
            return upsiRepository.SaveUPSIGroupCommunication(_upsiGrp);
        }
        public UPSIGroupResponse GetTaskDetails(UPSITask upsiTask)
        {
            UPSIGroupRepository upsiRepository = new UPSIGroupRepository();
            return upsiRepository.GetTaskDetails(upsiTask);
        }
        public UPSIGroupResponse UpdateUPSITask()
        {
            UPSIGroupRepository upsiRepository = new UPSIGroupRepository();
            return upsiRepository.UpdateUPSITask(_upsiGrp);
        }
        public UPSIGroupResponse DiscardUPSITask()
        {
            UPSIGroupRepository upsiRepository = new UPSIGroupRepository();
            return upsiRepository.DiscardUPSITask(_upsiGrp);
        }
        public UPSIGroupResponse SaveUPSIGroupMembers()
        {
            UPSIGroupRepository upsiRepository = new UPSIGroupRepository();
            return upsiRepository.SaveUPSIGroupMembers(_upsiGrp);
        }
        public UPSIGroupResponse DeleteUPSIGroupMember()
        {
            UPSIGroupRepository upsiRepository = new UPSIGroupRepository();
            return upsiRepository.DeleteUPSIGroupMember(_upsiGrp);
        }
        public UPSIGroupResponse GetUPSIGroupMember()
        {
            UPSIGroupRepository upsiRepository = new UPSIGroupRepository();
            return upsiRepository.GetUPSIGroupMember(_upsiGrp);
        }
        public UPSIGroupResponse GetAllUPSIGroups()
        {
            UPSIGroupRepository upsiRepository = new UPSIGroupRepository();
            return upsiRepository.GetAllUPSIGroups(_upsiGrp);
        }
        public UPSIGroupResponse GetUPSIGrpAuditLog()
        {
            UPSIGroupRepository upsiRepository = new UPSIGroupRepository();
            return upsiRepository.GetUPSIGrpAuditLog(_upsiGrp);
        }
        public UPSIGroupResponse AddUPSITaskDP()
        {
            UPSIGroupRepository upsiRepository = new UPSIGroupRepository();
            return upsiRepository.AddUPSITaskDP(_upsiGrp);
        }
    }
}