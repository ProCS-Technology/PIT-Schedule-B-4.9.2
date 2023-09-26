using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class UPSIVendorResponce : BaseResponse
    {

        public UPSIVendor UPSIVendor { get; set; }
        public List<UPSIVendor> listUPSIVendor { get; set; }


    }
}