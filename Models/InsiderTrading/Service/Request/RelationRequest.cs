using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;
namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class RelationRequest
    {
        private Relation _relation;
        public RelationRequest()
        {
            _relation = new Relation();
        }
        public RelationRequest(Relation rel)
        {
            _relation = new Relation();
            _relation = rel;
        }
        public RelationResponse SaveRelation()
        {
            _relation.Validate();

            if (_relation.GetRules().Count == 0)
            {

                RelationRepository oRepository = new RelationRepository();
                if (_relation.RELATION_ID == 0)
                {
                    return oRepository.AddRelation(_relation);
                }
                else
                {
                    return oRepository.UpdateRelation(_relation);
                }
            }
            return null;
        }
        public RelationResponse DeleteRelation()
        {
            RelationRepository oRepository = new RelationRepository();
            return oRepository.DeleteRelation(_relation);
        }
        public RelationResponse GetRelationList()
        {
            RelationRepository oRepository = new RelationRepository();
            return oRepository.GetRelationList(_relation);
        }
        public RelationResponse GetRelationForRelative()
        {
            RelationRepository oRepository = new RelationRepository();
            return oRepository.GetRelationForRelative(_relation);
        }
        public RelationResponse GetRelationForDeclaration()
        {
            RelationRepository oRepository = new RelationRepository();
            return oRepository.GetRelationForDeclaration(_relation);
        }
    }
}