using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;

namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class ReminderModuleRequest
    {

        private ReminderModule _reminderM;
        public ReminderModuleRequest(ReminderModule remsetup)
        {
            _reminderM = new ReminderModule();
            _reminderM = remsetup;

        }

        public ReminderModuleResponse GetAllActivity()
        {
            ReminderModuleRepository remrepo = new ReminderModuleRepository();

            return remrepo.getAllActivity(_reminderM);


        }

        public ReminderModuleResponse UpdateActivityById()
        {
            ReminderModuleRepository remrepo = new ReminderModuleRepository();

            return remrepo.UpdateActivityById(_reminderM);


        }

        public ReminderModuleResponse GetActivityById()
        {
            ReminderModuleRepository remrepo = new ReminderModuleRepository();

            return remrepo.GetActivityById(_reminderM);


        }



    }
}