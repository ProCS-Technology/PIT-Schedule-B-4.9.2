using ProcsDLL.Models.Model;
using ProcsDLL.Models.Repository;
using ProcsDLL.Models.Service.Response;

namespace ProcsDLL.Models.Service.Request
{
    public class ModuleRequest
    {
        private Module _module;

        public ModuleRequest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public ModuleRequest(Module module)
        {
            _module = new Module();
            _module = module;

        }

        public ModuleResponse GetModuleList()
        {
            ModuleRepository oRepository = new ModuleRepository();
            return oRepository.GetModuleList();
        }
    }
}