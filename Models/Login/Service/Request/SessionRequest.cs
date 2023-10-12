using ProcsDLL.Models.Login.Modal;
using ProcsDLL.Models.Login.Repository;
using ProcsDLL.Models.Login.Service.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProcsDLL.Models.Login.Service.Request
{
    public class SessionRequest
    {
        private SessionDTO _SessionDTO;
        private Session _Session;

        public SessionRequest(SessionDTO SessionDTO)
        {
            _SessionDTO = SessionDTO;
            _Session = new Session();
            ConvertToEntity();
        }
        public SessionRequest(String EMP_ID, String PMU_ID, String IP, String BROWSER)
        {
            _SessionDTO = new SessionDTO();
            _Session = new Session();
        }
        private void ConvertToEntity()
        {
            _Session.EMP_ID = _SessionDTO.EMP_ID;
            _Session.MAC_ID = _SessionDTO.MAC_ID;
            _Session.IP = _SessionDTO.IP;
            _Session.BROWSER = _SessionDTO.BROWSER;

        }
        private void ConvertToDTO()
        {
            _SessionDTO.EMP_ID = _Session.EMP_ID;
            _SessionDTO.MAC_ID = _Session.MAC_ID;
            _SessionDTO.IP = _Session.IP;
            _SessionDTO.BROWSER = _Session.BROWSER;
        }
        public SessionResponse SaveSession()
        {
            SessionRepository oRepository = new SessionRepository();
            return oRepository.SaveSession(_Session);
        }
        public SessionResponse DeleteSession()
        {
            SessionRepository oRepository = new SessionRepository();
            return oRepository.DeleteSession(_Session);
        }
    }
}