using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;
namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class DashboardUpsiRequest
    {
        private DashboardUpsi _category;

        public DashboardUpsiRequest()
        {

        }
        public DashboardUpsiRequest(DashboardUpsi category)
        {
            _category = new DashboardUpsi();
            _category = category;
        }
        public DashboardUpsiResponse GetUpsiCount()
        {
            DashboardUpsiRepository rep = new DashboardUpsiRepository();
            return rep.GetUpsiCount(_category);
        }
    }
}