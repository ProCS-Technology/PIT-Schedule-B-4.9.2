using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;

namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class ReminderSetUpRequest
    {
        private ReminderSetUp _remsetup;
        public ReminderSetUpRequest(ReminderSetUp remsetup)
        {
            _remsetup = new ReminderSetUp();
            _remsetup = remsetup;

        }


        public ReminderSetUpResponse getAllReminderType()
        {
            ReminderSetUpRepository remrepo = new ReminderSetUpRepository();

            return remrepo.getAllReminderType(_remsetup);


        }

        public ReminderSetUpResponse getAllMailReminderType()
        {
            ReminderSetUpRepository remrepo = new ReminderSetUpRepository();

            return remrepo.getAllMailReminderType(_remsetup);


        }

        public ReminderSetUpResponse getallMailReminderById()
        {
            ReminderSetUpRepository remrepo = new ReminderSetUpRepository();

            return remrepo.getallMailReminderTypeById(_remsetup);


        }

        public ReminderSetUpResponse getAllReminderTypeByID()
        {
            ReminderSetUpRepository remrepo = new ReminderSetUpRepository();

            return remrepo.getAllReminderTypeByID(_remsetup);


        }


        public ReminderSetUpResponse MailReminderSave()
        {
            ReminderSetUpRepository remrepo = new ReminderSetUpRepository();
            if (_remsetup.REMINDER_ID == 0)
            {
                return remrepo.MailReminderSave(_remsetup);
            }
            else
            {
                return remrepo.MailReminderUpdate(_remsetup);
            }
        }

        public ReminderSetUpResponse ReminderSave()
        {
            ReminderSetUpRepository remrepo = new ReminderSetUpRepository();
            if (_remsetup.REMINDER_ID == 0)
            {
                return remrepo.ReminderSave(_remsetup);
            }
            else
            {
                return remrepo.ReminderUpdate(_remsetup);
            }
        }


        public ReminderSetUpResponse MailReminderDelete()
        {
            ReminderSetUpRepository remrepo = new ReminderSetUpRepository();
            return remrepo.MailReminderDelete(_remsetup);
        }

        public ReminderSetUpResponse ReminderDelete()
        {
            ReminderSetUpRepository remrepo = new ReminderSetUpRepository();
                return remrepo.ReminderDelete(_remsetup);
        }

        public ReminderSetUpResponse GetReminderName(ReminderSetUp remsetup)
        {
                ReminderSetUpRepository remrepo = new ReminderSetUpRepository();
                return remrepo.GetReminderName(remsetup);
        }
        public ReminderSetUpResponse GetMailEventName(ReminderSetUp remsetup)
        {
            ReminderSetUpRepository remrepo = new ReminderSetUpRepository();
            return remrepo.GetMailEventName(remsetup);
         }
        public ReminderSetUpResponse GetFieldName(ReminderSetUp remsetup)
        {
            ReminderSetUpRepository remrepo = new ReminderSetUpRepository();
            return remrepo.GetFieldName(remsetup);
        }
        public ReminderSetUpResponse GetMailEventFieldName(ReminderSetUp remsetup)
        {
            ReminderSetUpRepository remrepo = new ReminderSetUpRepository();
           return remrepo.GetMailEventFieldName(remsetup);
        }
    }
}