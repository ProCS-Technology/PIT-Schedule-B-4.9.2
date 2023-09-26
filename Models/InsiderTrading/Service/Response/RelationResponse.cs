using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class RelationResponse : BaseResponse
    {
        private Relation _relation;
        private List<Relation> lstRelation;
        public Relation Relation
        {
            set
            {
                _relation = value;
            }
            get
            {
                return _relation;
            }
        }
        public List<Relation> RelationList
        {
            set
            {
                lstRelation = value;
            }
            get
            {
                return lstRelation;
            }
        }
        public void AddObject(Relation o)
        {
            if (lstRelation == null)
            {
                lstRelation = new List<Relation>();
            }
            lstRelation.Add(o);
        }
    }
}