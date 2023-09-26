using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class UPSIGroupReportResponse : BaseResponse
    {

        public List<UPSIMembersGroup> UPSIMembersGroupList = new List<UPSIMembersGroup>();
        public UPSIMembersGroup UPSIMembersGroup = new UPSIMembersGroup();
    }
}