using ProcsDLL.Models.Login.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProcsDLL.Models.Login.Service.Response
{
    public class SessionResponse :BaseResponse
    {
        private SessionDTO _SessionDTO;
        private List<SessionDTO> lstSessionDTO;
        public SessionDTO Session
        {
            set
            {
                _SessionDTO = value;
            }
            get
            {
                return _SessionDTO;
            }
        }
        public List<SessionDTO> SessionList
        {
            set
            {
                lstSessionDTO = value;
            }
            get
            {
                return lstSessionDTO;
            }
        }
        public void AddObject(SessionDTO o)
        {
            if (lstSessionDTO == null)
            {
                lstSessionDTO = new List<SessionDTO>();
            }
            lstSessionDTO.Add(o);
        }
        public void ClearList()
        {
            lstSessionDTO.Clear();
        }
    }
}