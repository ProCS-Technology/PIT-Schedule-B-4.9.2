namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class FileModel : BaseEntity
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public decimal FileSize { get; set; }
        public string ContentType { get; set; }
        public bool IsSelected { get; set; }
    }
}