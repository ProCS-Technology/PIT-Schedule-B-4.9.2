namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class ReminderSetUp : BaseEntity
    {

        public int REMINDER_ID { get; set; }

        public string REMINDER_NM { get; set; }

        public int FIELD_ID { get; set; }

        public string FIELD_NM { get; set; }

        public string REMINDER_TYPE { get; set; }
        public string REMINDER_TYPE_VALUE { get; set; }
        public string DURATION { get; set; }
        public string SUBJECT { get; set; }
        public string TEMPLATE { get; set; }
        public string Company_Id { get; set; }
        public string Created_By { get; set; }


    }
}