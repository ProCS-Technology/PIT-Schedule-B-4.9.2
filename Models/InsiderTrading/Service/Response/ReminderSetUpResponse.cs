using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;


namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class ReminderSetUpResponse : BaseResponse
    {

        public List<ReminderSetUp> listReminder { get; set; }
        public ReminderSetUp reminder { get; set; }
       
        private List<ReminderSetUp> lstReminderSetup;

        public void AddObject(ReminderSetUp o)
        {
            if (listReminder == null)
            {
                listReminder = new List<ReminderSetUp>();
            }
            listReminder.Add(o);
        }

    }
}