using ProcsDLL.Models.Model;
using System.Collections.Generic;

namespace ProcsDLL.Models.Service.Response
{
    public class ModuleResponse : BaseResponse
    {
        private Module _module;
        private List<Module> lstModule;
        public Module Module
        {
            set
            {
                _module = value;
            }
            get
            {
                return _module;
            }
        }
        public List<Module> ModuleList
        {
            set
            {
                lstModule = value;
            }
            get
            {
                return lstModule;
            }
        }
        public void AddObject(Module o)
        {
            if (lstModule == null)
            {
                lstModule = new List<Module>();
            }
            lstModule.Add(o);
        }
    }
}