namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class BenposDetailReport : BaseEntity
    {
        public string folioNumber { get; set; }
        public string holdingAsOnDate { get; set; }
        public string holdingAfterDeclaration { get; set; }
        public string transactionType { get; set; }
        public string tradedQuantity { get; set; }
    }
}