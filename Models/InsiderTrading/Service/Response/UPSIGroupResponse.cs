using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class UPSIGroupResponse : BaseResponse
    {
        public List<UPSIGrp> UPSIGroups { set; get; }
        public string sException { set; get; }
    }
}