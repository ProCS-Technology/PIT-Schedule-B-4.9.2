using System;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class DeclarationReport : BaseEntity
    {
        public Int32 companyId { get; set; }
        public string declarationNotMadeFrom { get; set; }
        public string declarationNotMadeTo { get; set; }
        public Int32 declarationMade { get; set; }
        public Int32 declarationNotMade { get; set; }
        public Int32 totalUsers { get; set; }
        public List<User> lstUser { get; set; }
        public BusinessUnit businessUnit { get; set; }
    }
}