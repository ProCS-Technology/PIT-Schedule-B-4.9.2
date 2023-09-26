using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class Module : BaseEntity
    {
        public string ModuleNm { set; get; }
        public string SubModuleNm { set; get; }
        public List<FormField> fields { set; get; }
    }
}