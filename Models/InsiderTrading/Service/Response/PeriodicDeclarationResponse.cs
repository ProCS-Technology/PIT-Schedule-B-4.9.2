using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class PeriodicDeclarationResponse : BaseResponse
    {
        private PeriodicDeclaration _periodicDeclaration;
        private List<PeriodicDeclaration> lstPeriodicDeclaration;

        public PeriodicDeclaration PeriodicDeclaration
        {
            set
            {
                _periodicDeclaration = value;
            }
            get
            {
                return _periodicDeclaration;
            }
        }

        public List<PeriodicDeclaration> PeriodicDeclarationList
        {
            set
            {
                lstPeriodicDeclaration = value;
            }
            get
            {
                return lstPeriodicDeclaration;
            }
        }

        public void AddObject(PeriodicDeclaration o)
        {
            if (lstPeriodicDeclaration == null)
            {
                lstPeriodicDeclaration = new List<PeriodicDeclaration>();
            }
            lstPeriodicDeclaration.Add(o);
        }
    }
}