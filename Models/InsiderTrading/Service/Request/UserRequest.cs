using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class UserRequest
    {
        private User _User;
        private EducationalAndProfessionalDetail _eduAndProfDetail;
        public UserRequest()
        {

        }
        public UserRequest(User user)
        {
            _User = new User();
            _User = user;

        }
        public UserRequest(EducationalAndProfessionalDetail eduAndProfDetail)
        {
            _eduAndProfDetail = new EducationalAndProfessionalDetail();
            _eduAndProfDetail = eduAndProfDetail;
        }
        public UserResponse SaveUser()
        {
            //_group.Validate();
            //if (_group.GetRules().Count == 0)
            //{
            UserRepository oRepository = new UserRepository();
            if (_User.ID == 0)
            {
                return oRepository.AddUser(_User);
            }
            else
            {
                return oRepository.UpdateUser(_User);
            }
            //}
            // return null;
        }
        public UserResponse GetUserTask()
        {
            UserRepository oRepository = new UserRepository();
            return oRepository.GetUserTask(_User);
        }
        public UserResponse SaveModifyRequest()
        {
            UserRepository oRepository = new UserRepository();
            return oRepository.SaveModifyRequest(_User);
        }
        public UserResponse UpdateModifyRequest()
        {
            UserRepository oRepository = new UserRepository();
            return oRepository.UpdateModifyRequest(_User);
        }
        #region "Get Debt Security Details"
        public UserResponse GetDebtSecurityDetails()
        {
            UserRepository oRepository = new UserRepository();
            return oRepository.GetDebtSecurityDetails(_User);
        }
        #endregion
        public UserResponse DeleteUser()
        {

            UserRepository oRepository = new UserRepository();
            return oRepository.DeleteUser(_User);
        }
        public UserResponse GetUserList()
        {
            UserRepository oRepository = new UserRepository();
            return oRepository.GetUserList(_User);
        }
        public UserResponse GetDPUsers()
        {
            UserRepository oRepository = new UserRepository();
            return oRepository.GetDPUsers(_User);
        }
        public UserResponse GetUserListByBusinessUnitId()
        {
            UserRepository oRepository = new UserRepository();
            return oRepository.GetUserListByBusinessUnitId(_User);
        }
        public UserResponse AssignedApprover()
        {
            UserRepository oRepository = new UserRepository();
            return oRepository.AssignedApprover(_User);
        }
        public UserResponse AssignedApproverForCO()
        {
            UserRepository oRepository = new UserRepository();
            return oRepository.AssignedApproverForCO(_User);
        }
        public UserResponse GetUserDetails()
        {
            UserRepository oRepository = new UserRepository();
            return oRepository.GetUserDetails(_User);
        }
        //public UserResponse GetAllUsersRole()
        //{
        //    UserRepository oRepository = new UserRepository();
        //    return oRepository.GetAllUsersRole();
        //}
        #region "Add Update Personal Detail"
        public UserResponse AddUpdatePersonalDetail()
        {
            try
            {
                UserRepository oRepository = new UserRepository();
                return oRepository.AddUpdatePersonalDetail(_User);
            }
            catch (Exception ex)
            {
                UserResponse oUser = new UserResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Processing failed, because of system error !";
                return oUser;
            }
        }
        #endregion
        #region "Add and Update Education Details"
        public UserResponse AddUpdateEducationDetails(User objUser)
        {
            try
            {
                UserRepository oRepository = new UserRepository();
                return oRepository.AddUpdateEducationDetails(_User);
            }
            catch (Exception ex)
            {
                UserResponse oUser = new UserResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Processing failed, because of system error !";
                return oUser;
            }
        }
        #endregion
        #region "Delete Education Details"
        public UserResponse DeleteEducationDetails(User objUser)
        {
            try
            {
                UserRepository oRepository = new UserRepository();
                return oRepository.DeleteEducationDetails(_User);
            }
            catch (Exception ex)
            {
                UserResponse oUser = new UserResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Processing failed, because of system error !";
                return oUser;
            }
        }
        #endregion
        #region "Add and Update Experience Details"
        public UserResponse AddUpdateExperienceDetails(User objUser)
        {
            try
            {
                UserRepository oRepository = new UserRepository();
                return oRepository.AddUpdateExperienceDetails(_User);
            }
            catch (Exception ex)
            {
                UserResponse oUser = new UserResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Processing failed, because of system error !";
                return oUser;
            }
        }
        #endregion
        #region "Delete Experience Details"
        public UserResponse DeleteExperienceDetails(User objUser)
        {
            try
            {
                UserRepository oRepository = new UserRepository();
                return oRepository.DeleteExperienceDetails(_User);
            }
            catch (Exception ex)
            {
                UserResponse oUser = new UserResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Processing failed, because of system error !";
                return oUser;
            }
        }
        #endregion
        #region "Add and Update Relative Details"
        public UserResponse AddUpdateRelativeDetails()
        {
            try
            {
                UserRepository oRepository = new UserRepository();
                return oRepository.AddUpdateRelativeDetails(_User);
            }
            catch (Exception ex)
            {
                UserResponse oUser = new UserResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Processing failed, because of system error !";
                return oUser;
            }
        }
        #endregion
        #region "Delete Initial Holding Detail"
        public UserResponse DeleteInitialHoldingDetail()
        {
            try
            {
                UserRepository oRepository = new UserRepository();
                return oRepository.DeleteInitialHoldingDeclarationDetail(_User);
            }
            catch (Exception ex)
            {
                UserResponse oUser = new UserResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Processing failed, because of system error !";
                return oUser;
            }
        }
        #endregion
        #region "Delete Physical Holding Detail"
        public UserResponse DeletePhysicalHoldingDetail()
        {
            try
            {
                UserRepository oRepository = new UserRepository();
                return oRepository.DeletePhysicalHoldingDeclarationDetail(_User);
            }
            catch (Exception ex)
            {
                UserResponse oUser = new UserResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Processing failed, because of system error !";
                return oUser;
            }
        }
        #endregion
        #region "Delete Demat Detail"
        public UserResponse DeleteDematDetail()
        {
            try
            {
                UserRepository oRepository = new UserRepository();
                return oRepository.DeleteDematDetail(_User);
            }
            catch (Exception ex)
            {
                UserResponse oUser = new UserResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Processing failed, because of system error !";
                return oUser;
            }
        }
        #endregion
        #region "Add and Update Demat Details"
        public UserResponse AddUpdateDematDetails()
        {
            try
            {
                UserRepository oRepository = new UserRepository();
                return oRepository.AddUpdateDematDetails(_User);
            }
            catch (Exception ex)
            {
                UserResponse oUser = new UserResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Processing failed, because of system error !";
                return oUser;
            }
        }
        #endregion
        #region "Add and Update Insider Holding Details"
        public UserResponse AddUpdateInitialHoldingDeclarationDetail()
        {
            try
            {
                UserRepository oRepository = new UserRepository();
                return oRepository.AddUpdateInitialHoldingDeclarationDetail(_User);
            }
            catch (Exception ex)
            {
                UserResponse oUser = new UserResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Processing failed, because of system error !";
                return oUser;
            }
        }
        #endregion
        #region "Delete Relative Detail"
        public UserResponse DeleteRelativeDetail()
        {
            try
            {
                UserRepository oRepository = new UserRepository();
                return oRepository.DeleteRelativeDetail(_User);
            }
            catch (Exception ex)
            {
                UserResponse oUser = new UserResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Processing failed, because of system error !";
                return oUser;
            }
        }
        #endregion
        #region "Send Email Notice Confirmation"
        public UserResponse SendEmailNoticeConfirmation()
        {
            try
            {
                UserRepository oRepository = new UserRepository();
                return oRepository.SendEmailNoticeConfirmation(_User);
            }
            catch (Exception ex)
            {
                UserResponse oUser = new UserResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Processing failed, because of system error !";
                return oUser;
            }
        }
        #endregion
        #region "Check Whether Educational And Professional Qualification Exist Or Not"
        public bool CheckWhetherEducationalAndProfessionalQualificationExist()
        {
            UserRepository oRepository = new UserRepository();
            return oRepository.CheckWhetherEducationalAndProfessionalQualificationExist(_User);
        }
        #endregion
        #region "Save Educational And Professional Qualification"
        public UserResponse SaveEducationalAndProfessionalQualification()
        {
            UserRepository oRepository = new UserRepository();
            return oRepository.SaveEducationalAndProfessionalQualification(_eduAndProfDetail);
        }
        #endregion
        #region "Get Transactional Information"
        public UserResponse GetTransactionalInfo()
        {
            UserRepository oRepository = new UserRepository();
            return oRepository.GetTransactionalInfo(_User);
        }
        #endregion
        public UserResponse GetDeclarationForms()
        {
            UserRepository oRepository = new UserRepository();
            return oRepository.GetDeclarationForms(_User);
        }
        #region "Get Transactional Information By Version"
        public UserResponse GetTransactionalInfoByVersion()
        {
            UserRepository oRepository = new UserRepository();
            return oRepository.GetTransactionalInfoByVersion(_User);
        }
        #endregion
        #region "Save Uploaded Form"
        public UserResponse SaveUploadedForm()
        {
            UserRepository oRepository = new UserRepository();
            return oRepository.SaveUploadedForm(_User);
        }
        #endregion
        #region "Get User Uploaded Forms"
        public UserResponse GetUserUploadedForms()
        {
            UserRepository oRepository = new UserRepository();
            return oRepository.GetUserUploadedForms(_User);
        }
        #endregion
        #region "Get Company Forms"
        public UserResponse GetCompanyForms()
        {
            UserRepository oRepository = new UserRepository();
            return oRepository.GetCompanyForms(_User);
        }
        #endregion
        #region "Get Company MIS Report"
        public UserResponse GetMISReport()
        {
            UserRepository oRepository = new UserRepository();
            return oRepository.GetMISReport(_User);
        }
        #endregion
        #region "Set User Role"
        public UserResponse SetUserRole()
        {
            UserRepository oRepository = new UserRepository();
            return oRepository.SetUserRole(_User);
        }
        #endregion
        #region "Validate Relative Detail Used In Higher Component"
        public bool ValidateRelativeDetailUsedInHigherComponent()
        {
            UserRepository oRepository = new UserRepository();
            return oRepository.ValidateRelativeDetailUsedInHigherComponent(_User.relativeInfo.ID, _User.companyId, _User.MODULE_DATABASE);
        }
        #endregion
        #region "Validate Demat Detail Used In Higher Component"
        public bool ValidateDematDetailUsedInHigherComponent()
        {
            UserRepository oRepository = new UserRepository();
            return oRepository.ValidateDematDetailUsedInHigherComponent(_User.dematInfo.ID, _User.companyId, _User.MODULE_DATABASE);
        }
        #endregion
        #region "Validate Initial Holding Detail Used In Higher Component"
        public bool ValidateInitialHoldingDetailUsedInHigherComponent()
        {
            UserRepository oRepository = new UserRepository();
            return oRepository.ValidateInitialHoldingDetailUsedInHigherComponent(_User.initialHoldingDetailInfo.ID, _User.companyId, _User.MODULE_DATABASE);
        }
        #endregion
        #region "Validate Physical Holding Detail Used In Higher Component"
        public bool ValidatePhysicalHoldingDetailUsedInHigherComponent()
        {
            UserRepository oRepository = new UserRepository();
            return oRepository.ValidatePhysicalHoldingDetailUsedInHigherComponent(_User.physicalHoldingDetailInfo.ID, _User.companyId, _User.MODULE_DATABASE);
        }
        #endregion
        public UserResponse AccessUserListByBusinessUnitId()
        {
            UserRepository oRepository = new UserRepository();
            return oRepository.AccessUserListByBusinessUnitId(_User);
        }
        public UserResponse GetUserApprover()
        {
            UserRepository oRepository = new UserRepository();
            return oRepository.GetUserApprover(_User);
        }
        public UserResponse VerifyPan()
        {
            try
            {
                UserRepository oRepository = new UserRepository();
                return oRepository.VerifyPan(_User);
            }
            catch (Exception ex)
            {
                UserResponse oUser = new UserResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Processing failed, because of system error !";
                return oUser;
            }
        }
        public UserResponse PreviewDeclarationForm()
        {
            try
            {
                UserRepository oRepository = new UserRepository();
                return oRepository.PreviewDeclarationForm(_User);
            }
            catch (Exception ex)
            {
                UserResponse oUser = new UserResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Processing failed, because of system error !";
                return oUser;
            }
        }
        public UserResponse GetRelativeList()
        {
            UserRepository oRepository = new UserRepository();
            return oRepository.GetRelativeList(_User);
        }
        public GenericResponse GetAllUsers()
        {
            UserRepository oRepository = new UserRepository();
            return oRepository.GetAllUsers(_User);
        }

        public UserResponse GetDisclouserTaskList()
        {
            UserRepository oRepository = new UserRepository();
            return oRepository.GetDisclouserTaskList(_User);
        }

        public UserResponse GetPersonalDetails()
        {
            UserRepository oRepository = new UserRepository();
            return oRepository.GetPersonDetails(_User);
        }
        public UserResponse GetRelativeDetails()
        {
            UserRepository oRepository = new UserRepository();
            return oRepository.GetRelativeDetails(_User);
        }
        public UserResponse GetDematDetails()
        {
            UserRepository oRepository = new UserRepository();
            return oRepository.GetDematDetails(_User);
        }
        public UserResponse GetHoldingDetails()
        {
            UserRepository oRepository = new UserRepository();
            return oRepository.GetHoldingDetails(_User);
        }
    }
}