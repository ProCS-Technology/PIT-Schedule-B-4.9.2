using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class ReminderModuleResponse : BaseResponse
    {
        public List<ReminderModule> listReminderModules { get; set; }
        public ReminderModule reminderModules { get; set; }



    }
}