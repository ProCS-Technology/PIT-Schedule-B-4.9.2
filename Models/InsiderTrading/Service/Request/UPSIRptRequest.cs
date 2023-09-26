using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;
namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class UPSIRptRequest
    {
        public UPSIRptResponse GetUPSIReport(
            string sUPSIGrpId, string sUserId, string sFrmDt, string sToDt,
            string sModuleDatabase, string sCompanyId, string sEmployeeId, string sAdminDb
        )
        {
            UPSIGroupRepository upsiRepository = new UPSIGroupRepository();
            return upsiRepository.GetUPSIReport(
                sUPSIGrpId, sUserId, sFrmDt, sToDt, sModuleDatabase, sCompanyId,
                sEmployeeId, sAdminDb
            );
        }
    }
}