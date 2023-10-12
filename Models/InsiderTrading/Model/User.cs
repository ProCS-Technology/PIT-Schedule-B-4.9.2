using System;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class User : BaseEntity
    {
        public Int32 ID { set; get; }
        public Int32 RELATION_ID { set; get; }
        public String USER_NM { set; get; }
        public Int32 TaskId { set; get; }
        public String TaskFor { set; get; }
        public String TaskStatus { set; get; }
        public Boolean IS_MARRIED { set; get; }
        public string IsMarried { set; get; }
        public String USER_EMAIL { set; get; }
        public String approverName { set; get; }
        public String approverEmail { set; get; }
        public String USER_PWD { set; get; }
        public String USER_MOBILE { set; get; }
        public Designation designation { set; get; }
        public Department department { set; get; }
        public BusinessUnit businessUnit { set; get; }
        public String LOGIN_ID { set; get; }
        public string SALUTATION { set; get; }
        public String CREATED_BY { set; get; }
        public String address { set; get; }
        public String tenureStartDate { set; get; }
        public String tenureEndDate { set; get; }
        public String dateOfBirth { set; get; }
        public String nationality { set; get; }
        public String status { set; get; }
        public Role userRole { set; get; }
        public String uploadAvatar { set; get; }
        public String isApprover { set; get; }
        public String isApproverForCO { set; get; }
        public Int32 companyId { set; get; }
        public Int32 moduleId { set; get; }
        public String companyName { set; get; }
        public Int32 D_ID { set; get; }
        public String lastModifiedOn { set; get; }
        public Int32 version { set; get; }
        public Boolean isFinalDeclared { set; get; }
        public String residentType { set; get; }
        public String panNumber { set; get; }
        public String identificationType { set; get; }
        public String identificationNumber { set; get; }
        public String pinCode { set; get; }
        public String country { set; get; }
        public String joiningDate { set; get; }
        public String becomingInsiderDate { set; get; }
        public Category category { set; get; }
        public Location location { set; get; }
        public String dinNumber { set; get; }
        public String institutionName { set; get; }
        public String stream { set; get; }
        public String employerDetails { set; get; }
        public String employeeId { set; get; }
        public String Ssn { set; get; }
        public String UserType { set; get; }
        public Int32 SpouseCnt { set; get; }
        public List<TransactionHistory> lstTransactionHistory { get; set; }
        public List<Education> lstEducation { set; get; }
        public List<Experience> lstExperience { set; get; }
        public List<Relative> lstRelative { set; get; }
        public List<InitialHoldingDetail> lstInitialHoldingDetail { set; get; }
        public List<PhysicalHoldingDetail> lstPhysicalHoldingDetail { set; get; }
        public List<FinalDeclaration> lstFinalDeclaration { set; get; }
        public List<DematAccount> lstDematAccount { set; get; }
        public Relative relativeInfo { set; get; }
        public DematAccount dematInfo { set; get; }
        public InitialHoldingDetail initialHoldingDetailInfo { set; get; }
        public PhysicalHoldingDetail physicalHoldingDetailInfo { set; get; }
        public Email email { set; get; }
        public List<Email> emailReport { set; get; }
        public string upsiType { set; get; }
        public string upsiSharedWith { set; get; }
        public string upsiSharedOn { set; get; }
        public string upsiSharedCC { set; get; }
        public string upsiPan { set; get; }
        public String remarks { set; get; }
        public string upsiAttachment { set; get; }
        public string declarationStartDate { set; get; }
        public string declarationSubmissionDate { set; get; }
        public string declarationDueDate { set; get; }
        public string formSubmittedOn { set; get; }
        public string formName { set; get; }
        public string attachmentAnnualOrBiannualDeclaration { set; get; }
        public string attachmentAnnualDisclosureByDesignatedEmployees { set; get; }
        public string attachmentFormBDeclaration { set; get; }
        public string misReportPath { set; get; }
        public string PersonalEmail { set; get; }
        public List<string> lstAttachment { set; get; }
        public string DepartmentName { set; get; }
        public string DesignationName { set; get; }
        public string LoginUsingPersonalEmail { set; get; }
        public string CategoryId { set; get; }
        public String IsDesignatedFl { set; get; }
        public string Task_Id { get; set; }

        public string Disclouser { get; set; }
        public string FinancialYear { get; set; }
        public string subjectReport { get; set; }
        public string reportTemplate { get; set; }
        public Boolean emailFl;
        public override void Validate()
        {
            base.Validate();
        }
    }
}