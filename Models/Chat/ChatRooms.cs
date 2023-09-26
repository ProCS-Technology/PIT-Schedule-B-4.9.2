using System;
using System.Collections.Generic;

namespace ProcsDLL
{
    public class ChatRooms
    {
        public Int32 CompanyId { get; set; }
        public Int32 CommitteeId { get; set; }
        public Int32 MeetingId { get; set; }
        public Int32 AgendaId { get; set; }
        public string ChatId { get; set; }
        public string LoginTime { get; set; }
        private List<CUser> _users = new List<CUser>();
        public void AddUser(CUser o)
        {
            _users.Add(o);
        }
        public List<CUser> users
        {
            set
            {
                _users = value;
            }
            get
            {
                return _users;
            }
        }
    }
}