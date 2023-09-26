using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Dynamic;
using ProcsDLL.Models.InsiderTrading.Model;
namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class GenericResponse
    {
        public string Msg { set; get; }
        public Boolean StatusFl { set; get; }
        public List<ExpandoObject> lst { set; get; }
        public ExpandoObject obj { set; get; }
    }
}