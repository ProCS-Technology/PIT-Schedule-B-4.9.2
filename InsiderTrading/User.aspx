<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" CodeBehind="User.aspx.cs" Inherits="ProcsDLL.InsiderTrading.User" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../assets/global/css/components.min.css" rel="stylesheet" />
    <link href="../assets/global/css/plugins.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/bootstrap-timepicker/css/bootstrap-timepicker.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/select2/css/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/select2/css/select2-bootstrap.min.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .requied {
            color: red;
        }
        
        .TFtable {
            width: 100%;
            border-collapse: collapse;
        }

            .TFtable th {
                padding: 7px;
                border: #dddddd 1px solid;
            }

            .TFtable td {
                padding: 7px;
                border: #dddddd 1px solid;
            }

            .TFtable tr {
                background: #f2f4f7;
                font-size: 12px;
            }

                .TFtable tr:nth-child(odd) {
                    background: #f2f4f7;
                }

                .TFtable tr:nth-child(even) {
                    background: #fff;
                }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12">
        <div class="portlet light portlet-fit">
            <div class="portlet-title">
                <div class="caption">
                    <i class="icon-settings font-red"></i>
                    <span class="caption-subject font-red sbold uppercase">User</span>
                </div>
            </div>
            <div class="portlet-body">
                <div class="table-toolbar">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="btn-group-devided">
                                <button id="Button1" runat="server" class="btn green" data-target="#userModel" onclick="javascript:OpenNew();" data-toggle="modal">
                                    Add New <i class="fa fa-plus"></i>
                                </button>
                                <button id="btnAddADUsers" runat="server" class="btn green" data-target="#userModelNew" onclick="javascript:OpenNew();" data-toggle="modal" style="display: none;">
                                    Add New AD Users <i class="fa fa-plus"></i>
                                </button>
                                <input id="txtBusinessUnitId" runat="server" type="hidden" />&nbsp;
                                Showing&nbsp;
                                <input id="Hidden1" runat="server" type="hidden" />&nbsp;
                                <select id="ddlUserStatus" onchange="fnChangeUserStatus();">
                                    <option value="0" >All</option>
                                    <option value="Active" selected="selected">Active</option>
                                    <option value="Inactive">Inactive</option>
                                </select>&nbsp;
                            </div>
                        </div>
                    </div>
                </div>
                <table class="table table-striped table-hover display text-nowrap table-bordered" id="tbl-user-setup">
                    <thead>
                        <tr>
                            <th>EDIT</th>
                            <th>LOGIN ID</th>
                            <th>SALUTATION</th>
                            <th>USER NAME</th>
                            <th>USER EMAIL</th>
                            <th>MOBILE NUMBER</th>
                            <th>PAN</th>
                            <th>ROLE</th>
                            <th>DESIGNATION</th>
                            <th>DEPARTMENT</th>
                            <th>NATIONALITY</th>
                            <th>COMPANY</th>
                            <th style="display:none;">PERSIONAL EMAIL</th>
                            <th style="display:none;">BECOMING DATE</th>
                            <th style="display:none;">JOINING DATE</th>
                            <th style="display:none;">TENNURE START DATE</th>
                             <th style="display:none;">TENNURE END DATE</th>
                            <th style="display:none;">IDENTIFICATION TYPE</th>
                            <th style="display:none;">IDENTIFICATION NUMBER</th>
                            <th>STATUS</th>
                            <th style="display:none;">User Image</th>
                        </tr>
                    </thead>
                    <tbody id="tbdUserList"></tbody>
                </table>
            
            </div>
        </div>
    </div>
    <div class="modal fade bs-modal-lg" id="userModelNew" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" onclick="fnCloseAdUserPopUp();" aria-hidden="true"></button>
                    <h4 class="modal-title">User</h4>
                </div>
                <div class="modal-body">
                    <div class="portlet" style="margin-bottom: 0;">
                        <div class="row">
                            <div class="col-md-3"><b>Name<span class="required" aria-required="true">*</span></b></div>
                            <div class="col-md-3">
                                <input type="text" id="txtUserName" class="form-control regtxt" data-tablindex="1" />
                            </div>
                            <div class="col-md-2">
                                <button id="btnserchUser" type="button" class="btn red" onclick="btnSearch_Click();">Search</button>
                            </div>
                            <div class="col-md-2">
                                <b>Is Search Users From Local</b>
                            </div>
                            <div class="col-md-2">
                                <input type="checkbox" id="isSearchUsersFromLocalServer" class="form-control regtxt" />
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12" id="userlist"></div>
                        </div>
                        <div id="MODALFOOTER1" class="modal-footer">
                            <button type="button" data-target="#userModelNew" data-toggle="modal" onclick="fnCloseAdUserPopUp();" class="btn dark  btn-outline">Close</button>
                            <button id="btnSaveUser1" type="button" onclick="fnAddADUserToUserList();" class="btn blue">Save</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    <div class="modal fade" id="userModel" tabindex="-1" aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnCloseModal();"></button>
                    <h4 class="modal-title caption-subject bold uppercase font-red-sunglo">
                        <span id="spnTitle"></span>
                        
                    </h4>
                    
                </div>
                <form class="form-horizontal" runat="server" role="form">
                    <div class="modal-body">
                        
                        <div class="portlet light bordered">
                            <div class="portlet-body form">
                                <div class="form-body">
                                    
                                    <div class="form-group" id="dvEmail">
                                        <label id="lblEmail" style="text-align: left" class="col-md-4 control-label">Email Id (Official)<span class="required" id="spnEmail">* </span></label>
                                        <div class="col-md-8">
                                            <div class="input-group">
                                                <span class="input-group-addon">
                                                    <i class="fa fa-envelope"></i>
                                                </span>
                                                <input id="txtEmail" type="email" class="form-control restrictpaste" placeholder="Email Address" onkeypress="javascript:fnRemoveClass(this,'Email');" autocomplete="off" />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="form-group" id="dvSalutation">
                                        <label id="lblSalutation" style="text-align: left" class="col-md-4 control-label">Salutation<span class="required" id="spnSalutation"> * </span></label>
                                        <div class="col-md-8">
                                            <select id="ddlSalutation" class="form-control" onchange="javascript:fnRemoveClass(this,'Salutation');">
                                                <option value="0">Please Select</option>
                                                <option value="Capt">Capt</option>
                                                <option value="Dr">Dr</option>
                                                <option value="Mr">Mr</option>
                                                <option value="Mrs">Mrs</option>
                                                <option value="Ms">Ms</option>
                                            </select>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="form-group" id="dvName">
                                        <label id="lblName" style="text-align: left" class="col-md-4 control-label">Name<span id="spnName" class="required"> * </span></label>
                                        <div class="col-md-8">
                                            <input id="txtName" type="text" class="form-control restrictpaste" placeholder="Enter name" onkeypress="javascript:fnRemoveClass(this,'Name');" autocomplete="off" />
                                        </div>
                                    </div>
                                    <br />
                                    <div class="form-group" id="dvPhone">
                                        <label id="lblPhone" style="text-align: left" class="col-md-4 control-label">Phone<span id="spnPhone" class="required"> * </span></label>
                                        <div class="col-md-8">
                                            <input id="txtPhone" type="text" onkeypress="javascript:fnRemoveClass(this,'Phone');"  maxlength="10" class="form-control mobile restrictpaste" placeholder="Enter Phone No" autocomplete="off" />
                                        </div>
                                    </div>
                                    <br />
                                     <div class="form-group" id="dvNationality">
                                        <label id="lblNationality" style="text-align: left" class="col-md-4 control-label">Nationality<span id="spnNationality" class="required">*</span></label>
                                        <div class="col-md-8">
                                            <select id="ddlNationality" class="form-control">
                                                <option value="0">Please Select</option>
                                                <option value="NRI">NRI</option>
                                                <option value="FOREIGN_NATIONAL">FOREIGN NATIONAL</option>
                                                <option value="INDIAN_RESIDENT">INDIAN RESIDENT</option>
                                            </select>
                                        </div>
                                        <br />
                                        <br />
                                    </div>
                                    <br />
                                    <div class="form-group" id="dvIdentificationType">
                                        <label id="spnIdentificationType1" style="text-align: left" class="col-md-4 control-label">Identification Type<span class="required" id="spnIdentificationType"> * </span></label>
                                        <div class="col-md-8">
                                            <select  class="form-control" runat="server" id="ddlIdentificationType">
                                                <option value=""></option>
                                                <option value="AADHAR_CARD">AADHAR CARD</option>
                                                <option value="DRIVING_LICENSE">DRIVING LICENSE</option>
                                                <option value="PASSPORT">PASSPORT</option>
                                            </select>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="form-group" id="dvIdentification">
                                        <label  style="text-align: left" class="col-md-4 control-label">Identification #<span id="spnIdentification" class="required"> * </span></label>
                                        <div class="col-md-8">
                                            <input id="txtIdentificationNumber" runat="server" type="text" onkeypress="javascript:fnRemoveClass(this,'Identification');" maxlength="16" class="form-control mobile restrictpaste" placeholder="Enter Identification No" autocomplete="off" />
                                        </div>
                                    </div>
                                    <br />
                                    <div class="form-group" id="dvPan">
                                        <label id="lblPan" style="text-align: left" class="col-md-4 control-label">PAN<span id="spnPan" class="required"> * </span></label>
                                        <div class="col-md-8">
                                            <input id="txtPan" type="text" onkeypress="javascript:fnRemoveClass(this,'Pan');" oninput="javascript:if(this.value.length>this.maxLength) this.value=this.value.slice(0, this.maxLength).toUpperCase(); else this.value=this.value.toUpperCase();" maxlength="10" class="form-control restrictpaste" placeholder="Enter PAN" autocomplete="off" />
                                        </div>
                                    </div>
                                    <br />
                                    <div class="form-group" id="dvUserid">
                                        <label id="lblUserid" style="text-align: left" class="col-md-4 control-label">User Login Id<span id="spnUserid" class="required"> * </span></label>
                                        <div class="col-md-8">
                                            <input id="txtUserid" type="text" class="form-control restrictpaste" placeholder="Enter login ID" autocomplete="off" onkeypress="javascript:fnRemoveClass(this,'Userid');" />
                                            <input id="txtUsrId" class="form-control" type="text" data-tabindex="1" style="display: none" value="0" />
                                            <input class="form-control" id="txtUserID" type="text" style="display: none;" />
                                        </div>
                                    </div>
                                    <br />
                                    <div class="form-group" id="dvEmployeeId">
                                        <label id="lblEmployeeId" style="text-align: left" class="col-md-4 control-label">Employee Id<span id="spnEmployeeId" class="required"> * </span></label>
                                        <div class="col-md-8">
                                            <input id="txtEmployeeId" type="text" class="form-control restrictpaste" placeholder="Enter Employee ID" autocomplete="off" onkeypress="javascript:fnRemoveClass(this,'EmployeeId');" />
                                        </div>
                                    </div>
                                    <br />
                                    <div class="form-group" id="dvEmailPersonal">
                                        <label id="lblEmailPersonal" style="text-align: left" class="col-md-4 control-label">Email Id (Personal)<span id="spnEmailPersonal" class="required">*</span></label>
                                        <div class="col-md-8">
                                            <div class="input-group">
                                                <span class="input-group-addon">
                                                    <i class="fa fa-envelope"></i>
                                                </span>
                                                <input id="txtEmailPersonal" type="email" class="form-control restrictpaste" placeholder="Email Address" onkeypress="javascript:fnRemoveClass(this,'Email');" autocomplete="off" />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="form-group" id="dvDateOfJoining">
                                        <label id="lblDateOfJoining" style="text-align: left" class="col-md-4 control-label">Date of Joining<span id="spnDateOfJoining" class="required">*</span></label>
                                        <div class="col-md-8">
                                            <input id="txtDateOfJoining" type="text" class="form-control" />
                                        </div>
                                    </div>
                                    <br />
                                    <div class="form-group" id="dvDateOfBecomingInsider">
                                        <label id="lblDateOfBecomingInsider" style="text-align: left" class="col-md-4 control-label">Date of Becoming Insider<span id="spnDateOfBecomingInsider" class="required">*</span></label>
                                        <div class="col-md-8">
                                            <input id="txtDateOfBecomingInsider" type="text" class="form-control" />
                                        </div>
                                    </div>
                                    <br />

                                    <div class="form-group" id="dvRetirementDate">
                                        <label id="lblRetirementDate" style="text-align: left" class="col-md-4 control-label">Date of Separation<span id="spnRetirementDate" class="required">*</span></label>
                                        <div class="col-md-8">
                                            <input id="txtDateOfRetirement" type="text" class="form-control" />
                                        </div>
                                    </div>
                                    <br />

                                    <div class="form-group" id="dvRole">
                                        <label id="lblRole" style="text-align: left" class="col-md-4 control-label">Role<span id="spnRole" class="required"> * </span></label>
                                        <div class="col-md-8">
                                            <select id="ddlRole" class="form-control" onchange="javascript:fnRemoveClass(this,'Role');">
                                                <option value="0">Please Select</option>
                                            </select>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="form-group" id="dvCategory">
                                        <label id="lblCategory" style="text-align: left" class="col-md-4 control-label">Category<span id="spnCategory" class="required"> * </span></label>
                                        <div class="col-md-8">
                                            <select id="ddlCategory" runat="server" class="form-control" onchange="javascript:fnRemoveClass(this,'Category');">
                                                <option value="0">Please Select</option>
                                            </select>
                                        </div>
                                    </div>

                                    <div class="form-group" style="display: none;">
                                        <label id="lblPassword" style="text-align: left" class="col-md-4 control-label">Password<span class="required"> * </span></label>
                                        <div class="col-md-8">
                                            <div class="input-group">
                                                <input id="txtPassword" type="password" class="form-control restrictpaste" placeholder="Password" autocomplete="off" onkeypress="javascript:fnRemoveClass(this,'Password');" value="P@ssw0rd" />
                                                <span class="input-group-addon">
                                                    <i class="fa fa-user"></i>
                                                </span>
                                            </div>
                                            <div class="tooltip" style="opacity: initial!important; padding-top: 8px;">
                                                <i class="fa fa-info-circle" style="color: red; font-size: 18px;" aria-hidden="true"></i>
                                                <div class="tooltiptext">
                                                    <ul>
                                                        <li>Password must be at least 8 characters</li>
                                                        <li>At least one upper case letter: (A - Z)</li>
                                                        <li>At least one lower case letter: (a - z)</li>
                                                        <li>At least one number: (0 - 9)</li>
                                                        <li>At least one Special Characters: !#$%&()*+@?^~</li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group" style="display: none;">
                                        <label id="lblConfirm" style="text-align: left" class="col-md-4 control-label">Confirm Password<span class="required"> * </span></label>
                                        <div class="col-md-8">
                                            <div class="input-group">
                                                <input id="txtConfirm" type="password" class="form-control restrictpaste" placeholder="Confirm Password" autocomplete="off" onkeypress="javascript:fnRemoveClass(this,'Confirm');" value="P@ssw0rd" />
                                                <span class="input-group-addon">
                                                    <i class="fa fa-user"></i>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="form-group" id="dvDesignation">
                                        <label id="lblDesignation" style="text-align: left" class="col-md-4 control-label">Designation<span id="spnDesignation" class="required">*</span></label>
                                        <div class="col-md-8">
                                            <select id="ddlDesignation" class="form-control" onchange="javascript:fnRemoveClass(this,'Designation');">
                                                <option value="0">Please Select</option>
                                            </select>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="form-group" id="dvDepartment">
                                        <label id="lblDepartment" style="text-align: left" class="col-md-4 control-label">Department<span id="spnDepartment" class="required">*</span></label>
                                        <div class="col-md-8">
                                            <select id="ddlDepartment" class="form-control" onchange="javascript:fnRemoveClass(this,'Department');">
                                                <option value="0">Please Select</option>
                                            </select>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="form-group" id="dvBusinessUnit">
                                        <label id="lblBusinessUnit" style="text-align: left" class="col-md-4 control-label">Company<span id="spnBusinessUnit" class="required"> * </span></label>
                                        <div class="col-md-8">
                                            <select id="ddlBusinessUnit" class="form-control" onchange="javascript:fnRemoveClass(this,'BusinessUnit');">
                                                <option value="0">Please Select</option>
                                            </select>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="form-group" id="dvStatus">
                                        <label id="lblStatus" style="text-align: left" class="col-md-4 control-label">Status<span id="spnStatus" class="required"> * </span></label>
                                        <div class="col-md-8">
                                            <select id="ddlStatus" class="form-control" onchange="javascript:fnRemoveClass(this,'Status');">
                                                <option value="0" selected="selected">Please Select</option>
                                                <option value="Active">Active</option>
                                                <option value="Inactive">Inactive</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="form-group" id="dvUpload" style="display:none;">
                                        <div class="fileinput fileinput-new" data-provides="fileinput">
                                            <div>
                                                <span id="lblUpload" class="fileinput-new col-md-4 ">Upload Photo<span id="spnUpload" class="required">*</span></span>&nbsp;&nbsp;                                                        
                                                <div class="btn default btn-file ">
                                                    <input id="fileUploadImage" type="file" name="..." onchange="javascript:fnRemoveClass(this,'Upload');" />
                                                    <a style="display: none" id="aUserAvatarImageUploaded" href="#" target="_blank">
                                                        <img id="imgUsr" class="img-circle" src="../assets/images/arrow-download-icon.png" style="width: 50px; height: 50px;" />
                                                    </a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="form-group" id="dvUserType">
                                        <label id="lblUserType" style="text-align: left" class="col-md-4 control-label">User Type<span class="required"> * </span></label>
                                        <div class="col-md-8">
                                            <div class="md-radio-inline">
                                                <div class="md-radio">
                                                    <input type="radio" id="radioAD" name="radio2" class="md-radiobtn" />
                                                    <label for="radioAD">
                                                        <span></span>
                                                        <span class="check"></span>
                                                        <span class="box"></span>AD/SAML</label>
                                                </div>
                                                <div class="md-radio">
                                                    <input type="radio" id="radioApplication" name="radio2" class="md-radiobtn" />
                                                    <label for="radioApplication">
                                                        <span></span>
                                                        <span class="check"></span>
                                                        <span class="box"></span>Application</label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        
                        <button id="btnSave" type="button" class="btn green" onclick="javascript:fnSaveUser();">Save</button>
                        <button id="btnCancel" type="button" data-dismiss="modal" class="btn default" onclick="javascript:fnCloseModal();">Cancel</button>
                    </div>
                </form>
            </div>
        </div>
    </div>
    <script src="../assets/editor/jquery-te-1.4.0.min.js"></script>
    <script src="../assets/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js"></script>
    <script src="../assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.js"></script>
    <script src="../assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
    <script src="../assets/global/plugins/bootstrap-timepicker/js/bootstrap-timepicker.min.js"></script>
    <script src="../assets/pages/scripts/components-date-time-pickers.min.js" type="text/javascript"></script>
    <script src="../assets/global/scripts/datatable.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/datatables/datatables.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js" type="text/javascript"></script>
    <script src="../assets/global/scripts/app.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js" type="text/javascript"></script>
    <script src="../assets/pages/scripts/components-date-time-pickers.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/select2/js/select2.min.js" type="text/javascript"></script>
    <script src="../assets/pages/scripts/sha512.js" type="text/javascript"></script>
    <script src="js/Global.js?<%=DateTime.Now %>" type="text/javascript"></script>
    <script src="js/User.js?<%=DateTime.Now %>" type="text/javascript"></script>
</asp:Content>