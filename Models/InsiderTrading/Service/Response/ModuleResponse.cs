using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class ModuleResponse : BaseResponse
    {
        private List<Module> lstModules;
        public List<Module> Modules
        {
            set
            {
                lstModules = value;
            }
            get
            {
                return lstModules;
            }
        }
        public void AddObject(Module o)
        {
            if (lstModules == null)
            {
                lstModules = new List<Module>();
            }
            lstModules.Add(o);
        }
    }
}