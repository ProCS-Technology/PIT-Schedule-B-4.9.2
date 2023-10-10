using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProcsDLL.Models.Login.Modal
{
    public class Session : BaseEntity
    {
        private String _EMP_ID;
        private String _MAC_ID;
        private String _IP;
        private String _BROWSER;


        public String EMP_ID
        {
            set
            {
                _EMP_ID = value;
            }
            get
            {
                return _EMP_ID;
            }
        }
        public String MAC_ID
        {
            set
            {
                _MAC_ID = value;
            }
            get
            {
                return _MAC_ID;
            }
        }
        public String IP
        {
            set
            {
                _IP = value;
            }
            get
            {
                return _IP;
            }
        }
        public String BROWSER
        {
            set
            {
                _BROWSER = value;
            }
            get
            {
                return _BROWSER;
            }
        }
    }
}