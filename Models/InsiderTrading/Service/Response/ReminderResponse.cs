using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class ReminderResponse : BaseResponse
    {
        private Reminder _reminder;
        private List<Reminder> lstReminder;
        public Reminder Reminder
        {
            set
            {
                _reminder = value;
            }
            get
            {
                return _reminder;
            }
        }
        public List<Reminder> ReminderList
        {
            set
            {
                lstReminder = value;
            }
            get
            {
                return lstReminder;
            }
        }
        public void AddObject(Reminder o)
        {
            if (lstReminder == null)
            {
                lstReminder = new List<Reminder>();
            }
            lstReminder.Add(o);
        }
    }
}