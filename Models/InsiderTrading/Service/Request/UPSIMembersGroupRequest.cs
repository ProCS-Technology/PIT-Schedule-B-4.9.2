using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;

namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class UPSIMembersGroupRequest
    {
        private UPSIMembersGroup _UPSIMemberGroup;

        public UPSIMembersGroupRequest()
        {


        }

        public UPSIMembersGroupRequest(UPSIMembersGroup UPSImembergroup)
        {

            _UPSIMemberGroup = new UPSIMembersGroup();
            _UPSIMemberGroup = UPSImembergroup;

        }


        public UPSIMembersGroupResponce GetVendorList()
        {
            UPSIMembersGroupRepository GroupRepo = new UPSIMembersGroupRepository();
            return GroupRepo.GetVendorList(_UPSIMemberGroup);

        }
        public UPSIMembersGroupResponce GetDesignatedUserList()
        {
            UPSIMembersGroupRepository GroupRepo = new UPSIMembersGroupRepository();
            return GroupRepo.GetDesignatedUserList(_UPSIMemberGroup);

        }

        public UPSIMembersGroupResponce SaveUPSI()
        {
            UPSIMembersGroupRepository GroupRepo = new UPSIMembersGroupRepository();
            if (_UPSIMemberGroup.GROUP_ID == 0)
            {
                return GroupRepo.SaveUPSI(_UPSIMemberGroup);
            }
            else
            {
                return GroupRepo.UpdateUPSI(_UPSIMemberGroup);
            }

        }

        public UPSIMembersGroupResponce GetUPSIGroupList()
        {
            UPSIMembersGroupRepository GroupRepo = new UPSIMembersGroupRepository();

            return GroupRepo.GetUPSIGroupList(_UPSIMemberGroup);
        }

        public UPSIMembersGroupResponce GetUPSIGroupListByID()
        {
            UPSIMembersGroupRepository GroupRepo = new UPSIMembersGroupRepository();

            return GroupRepo.GetUPSIGroupListByID(_UPSIMemberGroup);
        }

        public UPSIMembersGroupResponce DeleteUPSIGroupListByID()
        {
            UPSIMembersGroupRepository GroupRepo = new UPSIMembersGroupRepository();

            return GroupRepo.DeleteUPSIGroupListByID(_UPSIMemberGroup);
        }

        public UPSIMembersGroupResponce GetUPSITypeList()
        {
            UPSIMembersGroupRepository GroupRepo = new UPSIMembersGroupRepository();

            return GroupRepo.GetUPSITypeList(_UPSIMemberGroup);
        }


        public UPSIMembersGroupResponce GetNonDesignatedUPSIMember()
        {
            UPSIMembersGroupRepository GroupRepo = new UPSIMembersGroupRepository();

            return GroupRepo.GetNonDesignatedUPSIMember(_UPSIMemberGroup);
        }


    }
}