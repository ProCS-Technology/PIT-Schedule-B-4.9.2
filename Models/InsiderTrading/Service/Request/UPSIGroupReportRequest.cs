using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;

namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class UPSIGroupReportRequest
    {

        private UPSIMembersGroup _UPSIMemberGroup;

        public UPSIGroupReportRequest()
        {


        }

        public UPSIGroupReportRequest(UPSIMembersGroup UPSImembergroup)
        {

            _UPSIMemberGroup = new UPSIMembersGroup();
            _UPSIMemberGroup = UPSImembergroup;

        }

        public UPSIGroupReportResponse GetUPSIGroupList()
        {
            UPSIGroupReportRepository GroupRepo = new UPSIGroupReportRepository();

            return GroupRepo.GetUPSIGroupList(_UPSIMemberGroup);
        }

        public UPSIGroupReportResponse HistoryUPSIGroup()
        {
            UPSIGroupReportRepository GroupRepo = new UPSIGroupReportRepository();

            return GroupRepo.HistoryUPSIGroup(_UPSIMemberGroup);
        }
        public UPSIGroupReportResponse GetUPSIMember()
        {
            UPSIGroupReportRepository GroupRepo = new UPSIGroupReportRepository();

            return GroupRepo.GetUPSIMember(_UPSIMemberGroup);
        }
        public UPSIGroupReportResponse GetUPSIReportByMember()
        {
            UPSIGroupReportRepository GroupRepo = new UPSIGroupReportRepository();

            return GroupRepo.GetUPSIReportByMember(_UPSIMemberGroup);
        }



    }
}