using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;

namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class PreClearanceApprovalHierarchyRequest
    {
        private PreClearanceApprovalHierarchy _preClearanceApprovalHierarchy;

        public PreClearanceApprovalHierarchyRequest(PreClearanceApprovalHierarchy preClearanceApprovalHierarchy)
        {
            _preClearanceApprovalHierarchy = new PreClearanceApprovalHierarchy();
            _preClearanceApprovalHierarchy = preClearanceApprovalHierarchy;
        }

        #region "Save Officer Hirarchy Order"
        public PreClearanceApprovalHierarchyResponse SaveOfficerHierarchyOrder()
        {
            PreClearanceApprovalHierarchyRepository objRepository = new PreClearanceApprovalHierarchyRepository();
            return objRepository.SaveOfficerHierarchyOrder(_preClearanceApprovalHierarchy);
        }
        #endregion

        #region "Get All Officer Hirarchy Order"
        public PreClearanceApprovalHierarchyResponse GetAllOfficerHierarchyOrder()
        {
            PreClearanceApprovalHierarchyRepository objRepository = new PreClearanceApprovalHierarchyRepository();
            return objRepository.GetAllOfficerHierarchyOrder(_preClearanceApprovalHierarchy);
        }
        #endregion

        #region "Delete Officer Hierarchy Order"
        public PreClearanceApprovalHierarchyResponse DeleteOfficerHierarchyOrder()
        {
            PreClearanceApprovalHierarchyRepository objRepository = new PreClearanceApprovalHierarchyRepository();
            return objRepository.DeleteOfficerHierarchyOrder(_preClearanceApprovalHierarchy);
        }
        #endregion
    }
}