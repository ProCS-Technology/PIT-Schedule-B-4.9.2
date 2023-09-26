using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class PreClearanceApprovalHierarchyResponse : BaseResponse
    {
        private PreClearanceApprovalHierarchy _preClearanceApprovalHierarchy { get; set; }
        private List<PreClearanceApprovalHierarchy> _lstPreclearanceApprovalHierarchy { get; set; }

        public PreClearanceApprovalHierarchy PreClearanceApprovalHierarchy
        {
            set
            {
                _preClearanceApprovalHierarchy = value;
            }
            get
            {
                return _preClearanceApprovalHierarchy;
            }
        }

        public List<PreClearanceApprovalHierarchy> PreClearanceApprovalHierarchyList
        {
            set
            {
                _lstPreclearanceApprovalHierarchy = value;
            }
            get
            {
                return _lstPreclearanceApprovalHierarchy;
            }
        }
    }
}