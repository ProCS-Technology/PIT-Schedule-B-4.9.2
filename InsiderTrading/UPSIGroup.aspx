<%@ Page Language="C#" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" AutoEventWireup="true" CodeBehind="UPSIGroup.aspx.cs" Inherits="ProcsDLL.InsiderTrading.UPSIGroup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../assets/global/css/components.min.css" rel="stylesheet" />
    <link href="../assets/global/css/plugins.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/bootstrap-timepicker/css/bootstrap-timepicker.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/css/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <link href="../assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/layouts/layout/css/layout.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/layouts/layout/css/themes/darkblue.min.css" rel="stylesheet" type="text/css" id="style_color" />
    <link href="../assets/layouts/layout/css/custom.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/select2/css/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/select2/css/select2-bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/jquery-multi-select/css/multi-select.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .required {
            color: red;
        }
        .required-red {
            border-color: red !important;
        }
        .required-red-border {
            color: red !important;
            border: 1px solid red;
            border-color: red !important;
        }
        .select2-container--default.select2-container--focus .select2-selection--multiple {
            border: 1px solid red !important;
        }
        .select {
            width: 100%;
        }
        .m-0 {
            margin-bottom: 0;
            margin-top: 0;
        }
        .fa-info-circle:hover .tooltiptext {
            visibility: visible;
        }
        .tooltiptext {
            font-family:'Courier New';
            visibility: hidden;
            width: 500px;
            background-color:floralwhite;
            color: #000;
            text-align: left;
            padding: 5px 0;
            border:1px solid;
            /* Position the tooltip text - see examples below! */
            position: absolute;
            z-index: 1;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="portlet light bordered">
        <div class="portlet-title">
            <div class="caption font-red-sunglo">
                <i class="icon-settings font-red-sunglo"></i>
                <span class="caption-subject bold uppercase">UPSI</span>
            </div>
        </div>
        <div class="portlet-body">
            <div class="table-toolbar">
                <div class="row">
                    <div class="col-md-6">
                        <div class="btn-group-devided">
                            <button id="btnAddupsi" onclick="fnAddGrp();" visible="false" runat="server" class="btn green" data-target="#UPSIGrp" data-toggle="modal">
                                Add New UPSI Event
                            </button>
                            &nbsp;
                        </div>
                    </div>
                </div>
            </div>
            <table class="table table-striped table-hover table-bordered" id="tbl-upsilist-setup">
                <thead>
                    <tr>
                        <th>Name of the UPSI Project</th>
                        <th>Type</th>
                        <th>From</th>
                        <th>Till</th>
                        <th>Status</th>
                        <th>Action</th>
                    </tr>
                </thead>
                <tbody id="tbdupsilist"></tbody>
            </table>
        </div>
        <div class="modal fade" id="UPSIGrp" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-dialog-centered modal-lg modal-xl">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title caption-subject bold uppercase font-red-sunglo">Add UPSI Event</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnResetForm();"></button>
                    </div>
                    <form class="form-horizontal" runat="server" role="form">
                        <div class="modal-body">
                            <div class="portlet light bordered">
                                <div class="portlet-body form">
                                    <div class="form-body">
                                        <div class="form-group">
                                            <label id="lblUPSIType" style="text-align: left" class="col-md-4 control-label">
                                                Event Type<span class="required"> * </span>
                                            </label>
                                            <div class="col-md-8">
                                                <div class="input-group select2-bootstrap-append" id="colupsiDesignatedT">
                                                    <select id="ddlUPSIType" data-placeholder="Select UPSI Type" class="form-control select2" onchange="fnGetKeywords();"></select>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <label id="lblUPSIGrpNm" style="text-align: left" class="col-md-4 control-label">
                                                Name of the UPSI Project<span class="required"> * </span>
                                            </label>
                                            <div class="col-md-8">
                                                <input id="txtUPSIGrpNm" class="form-control form-control-inline" placeholder="Enter Name of the UPSI Project" type="text" autocomplete="off" onchange="removeRedClass('txtUPSIGrpNm', 'lblUPSIGrpNm')" />
                                                <input type="hidden" id="txtUPSIGrpId" value="0" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <label id="lblUPSIGrpDesc" style="text-align: left" class="col-md-4 control-label">
                                                Description <span class="required">* </span>
                                            </label>
                                            <div class="col-md-8">
                                                <textarea rows="4" class="form-control form-control-inline" id="txtUPSIGrpDesc" onchange="removeRedClass('txtUPSIGrpDesc','lblUPSIGrpDesc')"></textarea>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <label id="lblValidDt" style="text-align:left" class="col-md-4 control-label">
                                                Validity<span class="required"> * </span>
                                            </label>
                                            <div class="col-md-4">
                                                <input id="txtFromDt" class="form-control form-control-inline datepicker" placeholder="Enter Valid From Date" size="16" type="text" autocomplete="off" onchange="removeRedClass('txtFromDt', 'lblValidDt')" />
                                            </div>
                                            <div class="col-md-4">
                                                <input id="txtTillDt" class="form-control form-control-inline datepicker" placeholder="Enter Valid Till Date" size="16" type="text" autocomplete="off" onchange="removeRedClass('txtTillDt', 'lblValidDt')" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <label id="lblUPSIGrpStatus" style="text-align: left" class="col-md-4 control-label">
                                                Status <span class="required">*</span>&nbsp;&nbsp;&nbsp;
                                                <i class="fa fa-info-circle">
                                                    <div class="tooltiptext">
                                                        <strong>Abandoned</strong> - UPSI Event is left completely<br />
                                                        <strong>Active</strong> - UPSI Event is going on<br />
                                                        <strong>Inactive</strong> - UPSI Event is either expired or no longer exists<br />
                                                        <strong>Published</strong> - UPSI Event is in public domain or reported to exchanges
                                                    </div>
                                                </i>                                                
                                            </label>
                                            <div class="col-md-8">
                                                <select id="ddlUPSIGrpStatus" class="form-control" onchange="removeRedClass('ddlUPSIStatus','lblUPSIGrpStatus')">
                                                    <option value=""></option>
                                                    <option value="Abandoned">Abandoned</option>
                                                    <option value="Active">Active</option>
                                                    <option value="Inactive">Inactive</option>
                                                    <option value="Published">Published</option>
                                                </select>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <label id="lblUPSIGrpRemarks" style="text-align: left" class="col-md-4 control-label">
                                                Remarks <span class="required">* </span>
                                            </label>
                                            <div class="col-md-8">
                                                <textarea rows="4" class="form-control form-control-inline" id="txtUPSIGrpRemarks" onchange="removeRedClass('txtUPSIGrpRemarks','lblUPSIGrpRemarks')"></textarea>
                                            </div>
                                        </div>
                                        <br />
                                    </div>
                                </div>
                            </div>
                            <span class="caption-subject font-red sbold uppercase">UPSI Type Keyword</span>
                            <table class="table" id="tblKeyword">
                                <thead>
                                    <tr>
                                        <th>Keyword<span class="required">*</span></th>
                                        <th>Match Order<span class="required">*</span></th>
                                    </tr>
                                </thead>
                                <tbody id="tbodyKeyword">
                                    <tr>
                                        <td style="margin: 5px;">
                                            <input id="txtKeyword" class="form-control form-control-inline" placeholder="Enter Keyword" type="text" autocomplete="off" />
                                        </td>
                                        <td style="margin: 5px;">
                                            <input id="txtOrder" class="form-control form-control-inline numerictextbox" placeholder="Enter Match Order" type="text" autocomplete="off" />
                                        </td>
                                        <td style="margin: 5px;">
                                            <img onclick="javascript:fnAddRow();" src="images/icons/AddButton.png" height="24" width="24" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <button id="btnSave" type="button" class="btn green" onclick="javascript:fnSaveUPSIGrp();">Save</button>
                            <button id="btnCancel" type="button" data-dismiss="modal" class="btn default" onclick="javascript:fnCloseModal();">Cancel</button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
        <div class="modal fade" id="ModalMembers" tabindex="-1" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title caption-subject bold uppercase font-red-sunglo">Add Members</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnCloseModal();"></button>
                    </div>
                    <div class="modal-body">
                        <div class="portlet light bordered">
                            <div class="portlet-body form">
                                <div class="form-body">
                                    <input id="HiddenUpsiGrpId" type="hidden" />
                                    <div class="form-group">
                                        <label>User</label>
                                        <select id="dduserslist" data-placeholder="Select D/Ps" class="form-control select2" multiple="">
                                        </select>
                                    </div>
                                    <button id="btnSaveMember" type="button" onclick="javascript:fnSaveMember();" class="btn green">Save Members</button>

                                    <table class="table table-bordered table-hover margin-top-15" id="GrpMembersList">
                                        <thead>
                                            <tr>
                                                <th>User</th>
                                                <th>Membership</th>
                                                <th>Remove</th>
                                            </tr>
                                        </thead>
                                        <tbody id="tbodyGrpMembersList"></tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button id="btnCancelMember" type="button" data-dismiss="modal" onclick="javascript:fnCloseModal();" class="btn btn-danger">Cancel</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="GrpConnectecdPerson" tabindex="-1" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-dialog-centered" style="width:70%;">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title caption-subject bold uppercase font-red-sunglo">UPSI Connected Person(s)</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnCloseModal();"></button>
                    </div>
                    <div class="modal-body">
                        <div class="portlet light bordered">
                            <div class="portlet-body form">
                                <div class="form-body">
                                    <input id="HiddenUpsiGrpIdCP" type="hidden" />
                                    <div class="form-group">
                                        <label>User</label>
                                        <select id="ddlCPUsersList" data-placeholder="Select C/Ps" class="form-control select2" multiple="">
                                        </select>
                                    </div>
                                    <table class="table table-bordered table-hover margin-top-15" id="GrpCPMembersList">
                                        <thead>
                                            <tr>
                                                <th>Firm Name</th>
                                                <th>Name</th>
                                                <th>Email</th>
                                                <th>Identification</th>
                                                <th>Identification #</th>
                                                <th></th>
                                            </tr>
                                        </thead>
                                        <tbody id="tbodyGrpCPMembersList"></tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--<div class="modal-body">
                        <div class="form-group text-center">
                            <label id="lblupsiNonDesignatedM" style="text-align: left">
                                <span id="spnUPSIGrp"></span>
                            </label>

                        </div>
                        <div class="portlet light bordered">
                            <div class="portlet-body">
                                <div class="form-group">
                                    <input type="hidden" id="txtCPGrpId" value="0" />
                                    <table class="table" id="tblConnectedPerson">
                                        <thead>
                                            <tr>
                                                <th>Firm<span class="required">*</span></th>
                                                <th>Name<span class="required">*</span></th>
                                                <th>Email<span class="required">*</span></th>
                                                <th>Identification<span class="required">*</span></th>
                                                <th>Identification #<span class="required">*</span></th>
                                                <th></th>
                                            </tr>
                                        </thead>
                                        <tbody id="tbdCPAdd">
                                            <tr>
                                                <td style="margin: 5px;">
                                                    <input id="txtFirmNm" class="form-control form-control-inline" placeholder="Enter Firm Name" type="text" autocomplete="off" onchange="removeCPRedClass('txtFirmNm', 'lblFirmNm')" />
                                                </td>
                                                <td style="margin: 5px;">
                                                    <input id="txtCPNm" class="form-control form-control-inline" placeholder="Enter Connected Person" type="text" autocomplete="off" onchange="removeCPRedClass('txtCPNm', 'lblUPSIGrpNm')" />
                                                </td>
                                                <td style="margin: 5px;">
                                                    <input id="txtCPEmail" class="form-control form-control-inline" placeholder="Enter Connected Person" type="text" autocomplete="off" onchange="removeCPRedClass('txtCPEmail', 'lblCPEmail')" />
                                                </td>
                                                <td style="margin: 5px;">
                                                    <select id="ddlCPIdentification" class="form-control" onchange="removeRedClass('ddlCPIdentification','lblCPIdentification')">
                                                        <option value=""></option>
                                                        <option value="AADHAR CARD">AADHAR CARD</option>
                                                        <option value="DRIVING LICENSE">DRIVING LICENSE</option>
                                                        <option value="PAN">PAN</option>
                                                        <option value="PASSPORT">PASSPORT</option>
                                                        <option value="OTHER">OTHER</option>
                                                    </select>
                                                </td>
                                                <td style="margin: 5px;">
                                                    <input id="txtCPIdentificationNo" class="form-control form-control-inline" placeholder="Enter Connected Person" type="text" autocomplete="off" onchange="removeCPRedClass('txtCPIdentificationNo', 'lblCPIdentificationNo')" />
                                                </td>
                                                <td style="margin: 5px;">
                                                    <img onclick="javascript:fnAddCP();" src="images/icons/AddButton.png" height="24" width="24" />
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                                <div class="form-group">
                                    <h6><strong>Connected Person Added</strong></h6>
                                    <table class="table" id="OtherMemberDetail">
                                        <thead>
                                            <tr>
                                                <th>Firm Name</th>
                                                <th>Name</th>
                                                <th>Email</th>
                                                <th>Identification</th>
                                                <th>Identification #</th>
                                            </tr>
                                        </thead>
                                        <tbody id="tbodyCPDetail"></tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>--%>
                    <div class="modal-footer">
                        <button id="btnAdNewCP" class="btn green" data-target="#AddNewConnectedPerson" data-toggle="modal">
                            Add New CP
                        </button>
                        <button id="btnSaveCP" type="button" class="btn green" onclick="javascript:fnSaveConnectedPerson();">Save Connected Person(s)</button>
                        <button id="btnCancel1CP" type="button" data-dismiss="modal" class="btn default" onclick="javascript:fnCloseCPModal();">Cancel</button>

                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="AddNewConnectedPerson" tabindex="-1" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-dialog-centered" style="width: 90% !important">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title caption-subject bold uppercase font-red-sunglo">Insider Person(s)</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnCloseModal();"></button>
                    </div>
                    <div class="modal-body">
                        <div class="form-group text-center">
                            <label id="lblupsiNonDesignatedM" style="text-align: left">
                                <span id="spnNewUPSIGrp"></span>
                            </label>
                        </div>
                        <div class="portlet light bordered">
                            <div class="portlet-body">
                                <div class="form-group required" id="dvMsg" style="display:none;text-align:center;"></div>
                                <div class="form-group">
                                    <table class="table" id="tblNewConnectedPerson">
                                        <thead>
                                            <tr>
                                                <th>Firm<span class="required">*</span></th>
                                                <th>Name<span class="required">*</span></th>
                                                <th>Email<span class="required">*</span></th>
                                                <th>Identification<span class="required">*</span></th>
                                                <th>Identification #<span class="required">*</span></th>
                                                <th></th>
                                            </tr>
                                        </thead>
                                        <tbody id="tbdNewCPAdd">
                                            <tr>
                                                <td style="margin: 5px;">
                                                    <input id="txtNewFirmNm" class="form-control form-control-inline" placeholder="Enter Firm Name" type="text" autocomplete="off" onchange="removeCPRedClass('txtFirmNm', 'lblFirmNm')" />
                                                </td>
                                                <td style="margin: 5px;">
                                                    <input id="txtNewCPNm" class="form-control form-control-inline" placeholder="Enter Connected Person" type="text" autocomplete="off" onchange="removeCPRedClass('txtCPNm', 'lblUPSIGrpNm')" />
                                                </td>
                                                <td style="margin: 5px;">
                                                    <input id="txtNewCPEmail" class="form-control form-control-inline" placeholder="Enter Connected Person" type="text" autocomplete="off" onchange="removeCPRedClass('txtCPEmail', 'lblCPEmail')" />
                                                </td>
                                                <td style="margin: 5px;">
                                                    <select id="ddlNewCPIdentification" class="form-control" onchange="removeRedClass('ddlCPIdentification','lblCPIdentification')">
                                                        <option value=""></option>
                                                        <option value="AADHAR CARD">AADHAR CARD</option>
                                                        <option value="DRIVING LICENSE">DRIVING LICENSE</option>
                                                        <option value="PAN">PAN</option>
                                                        <option value="PASSPORT">PASSPORT</option>
                                                        <option value="OTHER">OTHER</option>
                                                    </select>
                                                </td>
                                                <td style="margin: 5px;">
                                                    <input id="txtNewCPIdentificationNo" class="form-control form-control-inline" placeholder="Enter Connected Person" type="text" autocomplete="off" onchange="removeCPRedClass('txtCPIdentificationNo', 'lblCPIdentificationNo')" />
                                                </td>
                                                <td style="margin: 5px;">
                                                    <img onclick="javascript:fnAddNewCP();" src="images/icons/AddButton.png" height="24" width="24" />
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <div class="col-md-8" style="float:left !important;margin-left:-300px;">
							<span class="required">
								*As per SEBI PIT Regulations, Unique PAN/Identifier is required for each Connected Person
							</span>
						</div>
                        <div class="col-md-4" style="float:right;">
                            <button id="btnSaveNewCP" type="button" class="btn green" onclick="javascript:fnSaveNewConnectedPerson();">Save Connected New Person(s)</button>
                            <button id="btnCancelNewCP" type="button" data-dismiss="modal" class="btn default" onclick="javascript:fnCloseNewCPModal();">Cancel</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="alertMeassage" tabindex="-1" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-dialog-centered" style="width: 90% !important">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title caption-subject bold uppercase font-red-sunglo">ALERT!</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnCloseModal();"></button>
                    </div>
                    <div class="modal-body">
                        <div class="form-group text-center">
                        </div>
                        <div class="portlet light bordered">
                            <div class="portlet-body">
                                <div class="form-group">
                                  <p style="font-size:14px">Please note that this action will trigger "Intimation of UPSI information" emails to users added. Are you sure you want to add users and send emails?</p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button id="btnOK" type="button" class="btn green" onclick="javascript:fnOK();">OK</button>
                        <button id="btnCancelAddNewCP" type="button" data-dismiss="modal" class="btn default" onclick="javascript:CancelAddNewCP();">Cancel</button>
                    </div>
                </div>
            </div>
        </div>
        <div id="GrpExcel" class="modal fade" tabindex="-1" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                        <h4 class="modal-title">Upload Group Communication</h4>
                    </div>
                    <div class="modal-body" style="height:200px !important;">
                        <div class="form-group">
                            <input id="txtXlsGrpId" typeof="text" style="display:none;" />
                            <input id="fuExcelUploadFile" class="btn blue btn-block btn-outline btn-file" data-toggle="modal" data-tabindex="1" value="Browse" type="file" />
                        </div>
                        <div class="form-group">
                            <div class="tooltip" style="opacity: initial!important;padding-top:8px;">
                                <i class="fa fa-info-circle" style="color: red;font-size:18px;" aria-hidden="true"></i>
                                <span>
                                    <ul>
                                        <li>Please use the downloaded template for UPSI communication upload</li>
                                        <li>Please enter email address for Shared by</li>
                                        <li>Please enter email address for Shared With(In both Designated & Connected Person)</li>
                                        <li>Please do not change the format of column UPSI Shared on Date(UPSI Shared on Date in the format yyyy-MM-dd HH:mm:ss{Time should be in 24 Hrs format})</li>
                                    </ul>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button id="btnDownloadExcel" type="button" data-dismiss="modal" class="btn green" onclick="javascript:fnDownloadExcel();" data-tabindex="2">Download Template</button>
                        <button type="button" data-dismiss="modal" class="btn btn-outline dark" onclick="javascript:fnCloseUploadModal();" data-tabindex="3">Close</button>
                        <button id="btnSaveUpload" type="button" data-dismiss="modal" class="btn green" onclick="javascript:fnUploadExcel();" data-tabindex="4">Upload Template</button>
                    </div>
                </div>
            </div>
        </div>


        <div id="GrpExcelException" class="modal fade" tabindex="-1" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-lg" style="width:70% !important;">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                        <h4 class="modal-title">
                                There are issues with the data uploaded in the excel template, please review and correct them and upload the template again
                        </h4>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <span id="spnException"></span>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" data-dismiss="modal" class="btn btn-outline dark" data-tabindex="3">Close</button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="GrpCommunication" tabindex="-1" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-dialog-centered  modal-lg modal-xl">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title caption-subject bold uppercase font-red-sunglo">UPSI Communication</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnCloseModal();"></button>
                    </div>
                    <div class="modal-body">
                        <div class="form-group text-center">
                            <input id="txtCommunicationGrpId" type="hidden" value="" />
                            <p>
                                <strong><span id="lblUPSICommunication"></span></strong>
                            </p>
                        </div>
                        <div class="portlet light bordered">
                            <div class="portlet-body form">
                                <div class="form-body">

                                    <div class="form-group">
                                        <div class="row">
                                            <div id="lblUPSISharedBy" style="text-align: left" class="col-md-4">
                                                UPSI Shared By<span class="required">*</span>
                                            </div>
                                            <div class="col-md-8">
                                                <select onchange="javascrript:UPSISharer_OnChange();" id="ddlUPSISharer" data-placeholder="Select Shared By" class="form-control">
                                                    <option value=""></option>
                                                    <option value="CP">Connected Person</option>
                                                    <option value="DP">Designated Person</option>
                                                </select>
                                                <input type="text" runat="server" style="display: none;" id="txtLoggedUser" />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="form-group">
                                        <div class="row">
                                            <div id="" style="text-align: left" class="col-md-4">                                                
                                            </div>
                                            <div class="col-md-8">
                                                <select id="ddlUPSISharedBy" data-placeholder="Select Shared By" class="form-control select2" onchange="removeRedClass('ddlUPSISharedBy','lblUPSISharedBy')"></select>
                                                <input type="text" runat="server" style="display: none;" id="Text1" />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="form-group">
                                        <div class="row">
                                            <div id="lblUPSIConnectedPerson" style="text-align: left" class="col-md-4">
                                                UPSI Shared With (Connected Person)<span class="required">*</span>
                                            </div>
                                            <div class="col-md-8">
                                                <select id="ddlUPSIConnectedPerson" data-placeholder="Select Connected Persons" class="form-control select2" multiple onchange="removeRedClass('ddlUPSIConnectedPerson','lblUPSIConnectedPerson')"></select>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="form-group">
                                        <div class="row">
                                            <div id="lblUPSIDesignatedPerson" style="text-align: left" class="col-md-4">
                                                UPSI Shared With (Designated Person)<span class="required">*</span>
                                            </div>
                                            <div class="col-md-8">
                                                <select id="ddlUPSIDesignatedPerson" data-placeholder="Select Designated Persons" class="form-control select2" multiple onchange="removeRedClass('ddlUPSIDesignatedPerson','lblUPSIDesignatedPerson')"></select>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="form-group">
                                        <div class="row">
                                            <div id="lblUPSISharedOn" style="text-align: left" class="col-md-4">
                                                UPSI Shared On<span class="required">*</span>
                                            </div>
                                            <div class="col-md-6">
                                                <input class="form-control form-control-inline" placeholder="Enter Shared On Date" size="16" type="text" autocomplete="off" id="txtUPSISharedOn" onchange="removeRedClass('txtUPSISharedOn','lblUPSISharedOn')" />
                                            </div>
                                            <div class="col-md-2">
                                                <input id="txtUPSISharedAt" type="text" class="form-control timepicker timepicker-24" />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="form-group">
                                        <div class="row">
                                            <div id="lblUPSISharingMode" style="text-align: left" class="col-md-4">
                                                Mode of Sharing UPSI<span class="required">*</span>
                                            </div>
                                            <div class="col-md-8">
                                                <%--<select id="ddlUPSISharingMode" class="form-control" onchange="removeRedClass('ddlUPSISharingMode','lblUPSISharingMode')">
                                                    <option value=""></option>
                                                    <option value="Discussion/Email">Discussion/Email</option>
                                                    <option value="Physical">Physical</option>
                                                    <option value="Telephonic">Telephonic</option>
                                                    <option value="Whatsapp">Whatsapp</option>
                                                </select>--%>
                                                 <select id="ddlUPSISharingMode" class="form-control" ></select>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="form-group">
                                        <div class="row">
                                            <div id="lblUPSIAttachment" style="text-align: left" class="col-md-4">
                                                Attachment (if any)
                                            </div>
                                            <div class="col-md-8">
                                                <input id="txtUPSIAttachment" class="btn blue btn-block btn-outline btn-file" data-toggle="modal" data-tabindex="5" value="Browse" type="file" />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="form-group">
                                        <div class="row">
                                            <div id="lblUPSIRemarks" style="text-align: left" class="col-md-4">
                                                Remarks
                                            </div>
                                            <div class="col-md-8">
                                                <textarea rows="4" id="txtUPSIRemarks" onchange="removeRedClass('txtUPSIRemarks','lblUPSIRemarks')" class="form-control"></textarea>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="modal-footer">
                        <button id="btnSaveCommunication" type="button" class="btn green" onclick="javascript:fnSaveUPSICommunication();">Save</button>
                        <button id="btnCancelCommunication" type="button" data-dismiss="modal" class="btn default" onclick="javascript:fnCloseModal();">Cancel</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="GrpAuditLog" tabindex="-1" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-dialog-centered modal-full">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title caption-subject bold uppercase font-red-sunglo">Audit Log</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnCloseLogModal();"></button>
                    </div>
                    <div class="modal-body">
                        <div class="portlet light bordered">
                            <div class="portlet-body form">
                                <%--<form class="form-horizontal" runat="server" role="form">--%>
                                <div class="form-body">
                                    <div id="GrpLogs">

                                    </div>
                                    </div>
                                </div>
                                <%--</form>--%>
                            </div>
                            
                        </div>
                    <div class="modal-footer">
                                <button id="btnCancelAL" type="button" data-dismiss="modal" class="btn default" onclick="javascript:fnCloseLogModal();">Cancel</button>
                            </div>
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
        <script src="../assets/global/plugins/jquery-validation/js/jquery.validate.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/jquery-validation/js/additional-methods.min.js" type="text/javascript"></script>
        <script src="../assets/global/scripts/jquery-ui.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/js.cookie.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/select2/js/select2.full.min.js" type="text/javascript"></script>
        <script src="../assets/pages/scripts/components-select2.min.js" type="text/javascript"></script>
        <script src="../assets/pages/scripts/components-select2.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/jquery-validation/js/jquery.validate.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/jquery-validation/js/additional-methods.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/bootstrap-wizard/jquery.bootstrap.wizard.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js" type="text/javascript"></script>
        <script src="../assets/pages/scripts/components-date-time-pickers.min.js" type="text/javascript"></script>
        <script src="js/Global.js?<%=DateTime.Now %>" type="text/javascript"></script>
        <script src="js/UPSIGroup.js?<%=DateTime.Now %>" type="text/javascript"></script>
        <script src="js/UPSIGroupDP.js?<%=DateTime.Now %>" type="text/javascript"></script>
        <script src="js/UPSIGroupCP.js?<%=DateTime.Now %>" type="text/javascript"></script>
        <script src="js/UPSIGroupCommunication.js?<%=DateTime.Now %>" type="text/javascript"></script>
        <script src="js/UPSIGroupExcel.js?<%=DateTime.Now %>" type="text/javascript"></script>
        <script type="text/javascript">
            $("#Loader").hide();
        </script>
    </div>
</asp:Content>