using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;

namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class ReminderRequest
    {
        private Reminder _reminder;

        public ReminderRequest()
        {

        }

        public ReminderRequest(Reminder reminder)
        {
            _reminder = new Reminder();
            _reminder = reminder;
        }
        public ReminderResponse SendMailSetup()
        {
            ReminderRepository objRepository = new ReminderRepository();
             return objRepository.SendMailSetup(_reminder);
        }
        public ReminderResponse SendReminder()
        {
            ReminderRepository objRepository = new ReminderRepository();
            return objRepository.SendReminder(_reminder);
        }
    }
}