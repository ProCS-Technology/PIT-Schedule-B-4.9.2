using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProcsDLL.Models.Login.Modal
{
    public class SessionDTO : BaseEntity
    {
        public String EMP_ID
        {
            set;
            get;
        }
        public String MAC_ID
        {
            set;
            get;
        }
        public String IP
        {
            set;
            get;
        }
        public String BROWSER
        {
            set;
            get;
        }
    }
}