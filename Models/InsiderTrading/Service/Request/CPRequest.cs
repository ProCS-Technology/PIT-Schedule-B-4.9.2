using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Collections.Generic;


namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class CPRequest
    {
        private CP _cp;
        private UPSIGrp _upsiGrp;
        private List<CP> _lstCP;
        public CPRequest()
        {
            _cp = new CP();
        }
        public CPRequest(CP cp)
        {
            _cp = new CP();
            _cp = cp;
        }
        public CPRequest(List<CP> lstCP)
        {
            _lstCP = new List<CP>();
            _lstCP = lstCP;
        }
        public CPRequest(UPSIGrp upsiGrp)
        {
            _upsiGrp = new UPSIGrp();
            _upsiGrp = upsiGrp;
        }
        public CPResponse GetCPUserList()
        {
            CPRepository cpRes = new CPRepository();
            return cpRes.GetCPUserList(_cp);
        }
        public CPResponse GetUPSIGroupCP()
        {
            CPRepository cpRes = new CPRepository();
            return cpRes.GetUPSIGroupCP(_upsiGrp);
        }
        public CPResponse SaveConnectedPersons()
        {
            CPRepository cpRes = new CPRepository();
            return cpRes.SaveConnectedPersons(_lstCP);
        }
        public CPResponse SaveConnectedPersonsForUPSITask()
        {
            CPRepository cpRes = new CPRepository();
            return cpRes.SaveConnectedPersonsForUPSITask(_lstCP);
        }
        public CPResponse UploadCP()
        {
            CPRepository cpRes = new CPRepository();
            return cpRes.UploadConnectedPersons();
        }


        public CPResponse SaveNewConnectedPersons()
        {
            CPRepository cpRes = new CPRepository();
            return cpRes.SaveNewConnectedPersons(_lstCP);
        }
    }
}