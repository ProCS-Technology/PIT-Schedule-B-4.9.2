using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;
namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class ModuleRequest
    {
        public ModuleResponse GetModule(string moduleNm, string ModuleDb)
        {
            ModuleRepository mRepository = new ModuleRepository();
            return mRepository.GetModule(moduleNm, ModuleDb);
        }
        public ModuleResponse GetUserConfig(string ModuleDb)
        {
            ModuleRepository mRepository = new ModuleRepository();
            return mRepository.GetUserConfig(ModuleDb);
        }
    }
}