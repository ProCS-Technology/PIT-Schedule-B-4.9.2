using System;
namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class SecurityType : BaseEntity
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public String IsTradable { get; set; }
    }
}