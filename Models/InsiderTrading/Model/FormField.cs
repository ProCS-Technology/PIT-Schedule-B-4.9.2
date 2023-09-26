namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class FormField : BaseEntity
    {
        public string CntrlType { set; get; }
        public string ControlId { set; get; }
        public string ControlNm { set; get; }
        public string DisplayFl { set; get; }
        public string DivNm { set; get; }
        public string EditFl { set; get; }
        public string Field { set; get; }
        public string FormatType { set; get; }
        public string MaxLength { set; get; }
        public string MinLength { set; get; }
        public string RequiredFl { set; get; }
    }
}