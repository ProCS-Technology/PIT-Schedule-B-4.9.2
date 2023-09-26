namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class ConnectedPerson : BaseEntity
    {
        public string MapId { set; get; }
        public string CPNm { set; get; }
        public string CPEmail { set; get; }
        public string IdentificationTyp { set; get; }
        public string IdentificationId { set; get; }
        public string CPStatus { set; get; }
        public string CPType { set; get; }
        public string GroupId { set; get; }
        public string CPFirmNm { set; get; }
    }
}