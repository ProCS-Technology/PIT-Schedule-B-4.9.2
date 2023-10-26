<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" CodeBehind="PreClearance.aspx.cs" Inherits="ProcsDLL.InsiderTrading.PreClearance" %>
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
    <style>
        .required {
            color: red !important;
        }
        td.details-control {
            background: url('images/icons/details_open.png') no-repeat center center;
            cursor: pointer;
        }
        tr.shown td.details-control {
            background: url('images/icons/details_close.png') no-repeat center center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <input type="hidden" id="HiddenLastBenposDate" runat="server" />
    <input type="hidden" id="hdnWCCnt" runat="server" value="0" />
    <input type="hidden" id="hdnUPSICnt" runat="server" value="0" />
    <div class="row" style="margin-left: 0px!important; margin-right: 0px !important;">
        <div class="col-md-12">
            <div class="portlet light bordered">
                <div class="portlet-title  tabbable-line">
                    <div class="pull-right btn-group">
                        <a class="btn red btn-circle" href="javascript:;" data-toggle="dropdown">
                            New Trade Request<i class="fa fa-angle-down"></i>
                        </a>
                        <ul class="dropdown-menu" id="ulMenu"></ul>
                        <input id="enableUndertakingBeforeRequest" type="hidden" runat="server" />
                    </div>

                </div>
                <div class="portlet-body" style="margin-top: 40px;">
                    <div class="tab-content">
                        <div class="tab-pane active" id="Pre_Clearance_Request">
                            <div class="col-md-4 col-lg-4">
                                <div class="btn-group btn-group-devided" data-toggle="buttons">
                                    <label class="btn green btn-outline btn-circle active">
                                        <input type="radio" name="PreClearancestatus" class="toggle filter-status" value="" />All
                                    </label>
                                    <label class="btn  green btn-outline btn-circle">
                                        <input type="radio" name="PreClearancestatus" class="toggle filter-status" value="Self" />Self
                                    </label>
                                    <label class="btn  green btn-outline btn-circle">
                                        <input type="radio" name="PreClearancestatus" class="toggle filter-status" value="Relatives" />Relatives
                                    </label>
                                </div>
                            </div>

                            <div class="col-md-4 col-lg-4">
                                <div class="input-group">
                                    <span class="input-group-addon">Status</span>
                                    <select class="form-control" onchange="fnStatusChange()" name="Status" id="bindStatus">
                                        <option value="All">All</option>
                                        <option value="Draft">Draft</option>
                                        <option value="InApproval">Pending for Approval</option>
                                        <option value="Approved">Approved</option>
                                        <option value="Rejected">Rejected</option>
                                    </select>
                                </div>
                            </div>
                            <table class="table table-striped table-bordered" id="tbl-preclearance-setup">
                                <thead>
                                    <tr>
                                        <%--<th scope="col">#</th>--%>
                                        <th style="width:8% !important;"></th>
                                        <th>Id</th>
                                        <th>For</th>
                                        <th>Relation</th>
                                        <th>Quantity</th>
                                        <th>Type</th>
                                        <th style="display:none;">Trade Exchange</th>
                                        <th style="display:none;">Demat</th>
                                        <th>Requested</th>
                                        <th>Status</th>
                                        <th>Reviewed On</th>
                                        <th>Reviewed By</th>
                                        <th>Reviewer Remarks</th>
                                    </tr>
                                </thead>
                                <tbody id="tbdPreClearanceList"></tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="basic1" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnClearFormBrokerNote();"></button>
                    <h4 class="modal-title">Trade Details</h4>
                </div>
                <div class="modal-body">
                    <form class="form-horizontal" role="form">
                        <div class="form-body">
                            <div class="form-group">
                                <label id="lblForBN" class="col-md-3 control-label">
                                    For
                                <span class="required">* </span>
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <select id="ddlForBN" disabled="disabled" class="form-control">
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">
                                    Company
                                    <span class="required">* </span>
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <select id="ddlRestrictedCompaniesBN" disabled="disabled" class="form-control">
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">
                                    Transaction Type
                                    <span class="required">* </span>
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <select id="ddlTypeOfTransactionBN" disabled="disabled" class="form-control">
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">
                                    Request date
                                    <span class="required">* </span>
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="fa fa-bell-o"></i>
                                            </span>
                                            <input id="txtRequestedTransactionDateBN" disabled="disabled" class="form-control date-picker" data-date-format="dd/mm/yyyy" type="text" value="" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">
                                    Approved Trade Quantity
                                    <span class="required">* </span>
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="fa fa-bell-o"></i>
                                            </span>
                                            <input id="txtTradequantityBrokerNote" type="number" disabled="disabled" class="form-control" placeholder="Enter quantity" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">
                                    Remaining Trade Quantity
                                    <span class="required">* </span>
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <input id="txtRemainingTradequantity" type="number" disabled="disabled" class="form-control" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group display-none">
                                <label class="col-md-3 control-label">
                                    Actual Trade Quantity
                                    <span class="required">* </span>
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="fa fa-bell-o"></i>
                                            </span>
                                            <input id="txtActualTradequantityBrokerNote" type="number" class="form-control" placeholder="Enter quantity" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group display-none">
                                <label class="col-md-3 control-label">
                                    Value Per Share
                                    <span class="required">* </span>
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="fa fa-bell-o"></i>
                                            </span>
                                            <input id="txtValuePerShare" type="number" class="form-control" placeholder="Enter per share cost" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group display-none">
                                <label class="col-md-3 control-label">
                                    Total Amount
                                    <span class="required">* </span>
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="fa fa-bell-o"></i>
                                            </span>
                                            <input id="txtTotalamount" type="number"  disabled="disabled" class="form-control" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">
                                    Demat Account
                                    <span class="required">* </span>
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <select id="ddlDematAccountBrokerNote" class="form-control">
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group display-none">
                                <label class="col-md-3 control-label">
                                    Actual Date Of Transaction
                                    <span class="required">* </span>
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="fa fa-bell-o"></i>
                                            </span>
                                            <input id="txtActualdateoftransaction" class="form-control" type="text" />
                                            <input type="hidden" id="preClearanceRequestIdBN" value="0" />
                                            <input type="hidden" id="txtTradeExchangeBN" value="0" />
                                            <input type="hidden" id="txtStatusBN" value="" />
                                            <input type="hidden" id="txtDematAccountBN" value="" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Trade Details</label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="fa fa-upload"></i>
                                            </span>
                                            <input id="btnBrokernote" type="file" class="form-control" />
                                            <input type="hidden" id="txtBrokerNoteFileName" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group" id="dvExchangeTradedOn">
                                <label class="col-md-3 control-label">Exchange On Which Trade Executed</label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <select id="ddlExchangeTradedOn" class="form-control">
                                            <option value="">--Select--</option>
                                            <option value="BSE">BSE</option>
                                            <option value="NSE">NSE</option>
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group" id="dvBrokerDetails" style="display:none">
                                <label class="col-md-3 control-label">
                                    Details of Off Market Deal<span class="required">* </span>
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <textarea id="txtareabrokerdetails" class="form-control" rows="2"></textarea>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">
                                    Remarks
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="fa fa-upload"></i>
                                            </span>
                                            <textarea id="remarks" style="margin-top: 0px; margin-bottom: -4px; height: 42px; width: 100%;"></textarea>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group" id="dvMultiTrade" style="display:none; margin:15px;">
                                <table class="table table-condensed table-responsive" id="TableMultiTrade">
                                    <thead>
                                        <tr>
                                            <th style="padding-left: 5px; padding-right: 5px; width: 20%;">Date<span class="required">*</span></th>
                                            <th style="padding-left: 5px; padding-right: 5px; width: 20%;">Quantity<span class="required">*</span></th>
                                            <th style="padding-left: 5px; padding-right: 5px; width: 30%;">Value Per Share<span class="required">*</span></th>
                                            <th style="padding-left: 5px; padding-right: 5px; width: 20%;">Total Amount<span class="required">*</span></th>
                                            <th style="padding-left: 5px; padding-right: 5px; width: 10%;"></th>
                                        </tr>
                                    </thead>
                                    <tbody id="tbdMultiTrade">
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </form>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn dark btn-outline" data-toggle="modal" data-target="#modalNullTrade" id="btnnulltrade">Click Here to Report Nill Trade</button>
                    <button id="btnSubmitBrokerNote" type="submit" class="btn btn-outline dark" onclick="javascript:AddUpdateBrokerNote();">Submit</button>
                    <button id="btnCancelBrokerNote" type="button" class="btn default" data-dismiss="modal" onclick="javascript:fnClearFormBrokerNote();">Cancel</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <div class="modal fade" id="stack1" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnCloseModal();"></button>
                    <h4 class="modal-title">PRE-CLEARANCE REQUEST</h4>
                </div>
                <div class="modal-body">
                    <form class="form-horizontal" role="form">
                        <div class="form-body">
                            <div class="form-group">
                                <label id="lblFor" class="col-md-3 control-label">
                                    For
                                    <span class="required">* </span>
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <select id="ddlFor" class="form-control">
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">
                                    Type Of Security
                                    <span class="required">* </span>
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <select id="ddlTypeOfSecurity" class="form-control">
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">
                                    Company
                                    <span class="required">* </span>
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <select id="ddlRestrictedCompanies" class="form-control">
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">
                                    Trade Quantity
                                    <span class="required">* </span>
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="fa fa-bell-o"></i>
                                            </span>
                                            <input id="txtTradeQuantity" type="number" class="form-control" placeholder="Enter quantity" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">
                                    Transaction Type
                                    <span class="required">* </span>
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <select id="ddlTypeOfTransaction" class="form-control">
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group" style="display: none;">
                                <label class="col-md-3 control-label">
                                    Trade Exachange
                                    <span class="required">* </span>
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <select id="ddlTradeExchange" class="form-control">
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">
                                    Demat Account
                                    <span class="required">* </span>
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <select id="ddlDematAccount" class="form-control">
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">
                                    Free Holding(In system)
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <span id="spncurrentholding">0</span>
                                        <input type="hidden" id="txtcurrentholding" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">
                                    Locked Holding(In system)
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <span id="spnLockedHolding">0</span>
                                        <input type="hidden" id="txtLockedHolding" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">
                                    Requested Transaction Date
                                    <span class="required">* </span>
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="fa fa-bell-o"></i>
                                            </span>
                                            <input id="txtRequestedTransactionDate" class="form-control RequestedTransactionDate" readonly="" type="text" value="" />
                                            <input type="hidden" id="preClearanceRequestId" value="0" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">
                                    Proposed Trade Price per share
                                    <span class="required">* </span>
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="fa fa-bell-o"></i>
                                            </span>
                                            <input id="txtShareCurrentMarketPrice" type="number" class="form-control" placeholder="Proposed Trade Price per share" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Proposed Transaction Through</label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <select id="ddlProposedTransactionThrough" class="form-control">
                                            <option value="">--Select--</option>
                                            <option value="Stock Exchange">Stock Exchange</option>
                                            <option value="Off-Market Deal">Off-Market Deal</option>
                                        </select>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </form>
                </div>
                <div class="modal-footer">
                    <button id="btnSubmitPreClearanceRequest" type="submit" class="btn btn-outline dark" data-toggle="modal" onclick="javascript:fnSubmitPreClearanceRequest('InApproval');">Submit</button>
                    <button id="btnSaveAsDraftPreClearanceRequest" type="button" style="display: none;" class="btn green" data-toggle="modal" onclick="javascript:fnSubmitPreClearanceRequest('Draft');">Save As Draft</button>
                    <button id="btnCancelPreClearanceRequest" style="display: none;" class="btn red" data-toggle="modal" onclick="javascript:fnSubmitPreClearanceRequest('Cancel')">Withdraw Request</button>
                    <button id="btnCancel" type="button" class="btn default" data-dismiss="modal" onclick="javascript:fnClearForm();">Cancel</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <div class="modal fade" id="modalForms" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title">Please Select the Forms for Submission to CO</h4>
                    <input type="hidden" id="txtBrokerNoteId" value="0" />
                </div>
                <div class="modal-body">
                    <div class="portlet-body form">
                        <div id="divItemTitle" class="row">
                            <div class="col-md-4" id="lblForms">Select Form</div>
                            <div class="col-md-8">
                                <%--<select runat="server" style="display: none;" name="category" id="ddlCategory" class="form-control select2" data-placeholder="Select a Category">
                                </select>--%>
                                <select id="ddlForms" class="form-control" onchange="javascript:fnDisplayNote(this, 'Forms');">
                                    <option selected="selected" value="0">Please Select Form</option>
                                    <%--<option value="FORM_CJ">Form C</option>
                                    <option value="FORM_DJ">Form C</option>
                                    <option value="FORM_J">Form J</option>--%>
                                </select>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <label id="lblNote" class="control-label" style="display: none; color: black; text-align: left;"></label>
                            </div>
                        </div>
                        <br />
                        <div id="divOverwriteForm" class="row display-none">
                            <div class="col-md-12" id="lblOverwriteForm">
                                <input type="checkbox" id="chkOverwrite" onchange="javascript:fnShowUploadDiv(this);" />
                                <label id="lblOverwrite" class="control-label" style="color: darkorange; text-align: left;">Click here to Submit your own Forms</label>
                            </div>
                        </div>
                        <br />
                        <div id="divUploadForm" class="row" style="display: none;">
                            <div id="divForm">
                                <div class="col-md-4" id="lblUploadForm">Upload Form</div>
                                <div class="col-md-8">
                                    <input type="file" id="txtUploadForm" onchange="javascript:fnRemoveClass(this,'UploadForm');" class="form-control" data-tabindex="4" />
                                </div>
                            </div>
                            <br />
                            <div id="divAnnexure">
                                <div class="col-md-4" id="lblUploadFormAnnexure">Upload Form Annexure</div>
                                <div class="col-md-8">
                                    <input type="file" id="txtUploadFormAnnexure" onchange="javascript:fnRemoveClass(this,'UploadFormAnnexure');" class="form-control" data-tabindex="4" />
                                </div>
                            </div>
                        </div>
                        <br />
                        <div id="divUploadLbl" class="row" style="display: none;">
                            <div class="col-md-12">
                                <label id="lbl" class="control-label" style="color: darkorange; text-align: left;">Please Download & Review the Selected Forms before submission.</label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button id="btnDownloadForm" type="submit" class="btn green" onclick="javascript:fnDownloadForm();">Download & Review Forms</button>
                    <button id="btnSubmitForm" type="button" disabled="disabled" class="btn green" onclick="javascript:fnValidateForms();">Submit Forms</button>
                    <button id="btnOpenForm" type="button" style="display: none;" data-target="#modalForms" data-toggle="modal"></button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade in" id="modalSubmitConfirmation" style="z-index: 10000000" tabindex="-1" role="dialog" aria-hidden="True">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="True"></button>
                    <h4 class="modal-title"><b>This action is irrevokable. Please ensure that the correct Forms are uploaded for sharing. Are you sure, you want to submit the Forms to Compliance Officer?</b></h4>
                </div>
                <div class="modal-footer">
                    <button id="btnSubmitNo" type="button" class="btn dark btn-outline" onclick="javascript:fnHideShow('modalSubmitConfirmation');">NO</button>
                    <button id="btnSubmitYes" class="btn red" data-dismiss="modal" value="YES" onclick="javascript:fnSubmitForms()">YES</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade in" id="nullTradeConfirmation" style="z-index: 99999" tabindex="-1" role="dialog" aria-hidden="True" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog" style="width: 50%">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="True"></button>
                    <h4 class="modal-title"><b>Are you sure you want to report Null Trade? Press Okay to Confirm.</b></h4>
                </div>
                <div class="modal-footer">
                    <input id="txtnullTradeId" type="hidden" value="0" />
                    <button type="button" class="btn dark btn-outline" onclick="javascript:fnHideShow('nullTradeConfirmation');">NO</button>
                    <input value="Okay" id="btnNullTradeConfirm" data-dismiss="modal" class="btn red" onclick="javascript: nullTrade();" type="submit" />
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="modalNullTrade" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnCloseModal();"></button>
                    <h4 class="modal-title" id="modal-title-nulltrade">Please Submit Remarks in case of Null Trade</h4>
                </div>
                <div class="modal-body">
                    <div class="portlet-body form">
                        <div id="divNullTradeRemarks" class="row">
                            <div class="col-md-4" id="lblNullTradeRemarks">Please Enter Remarks</div>
                            <div class="col-md-8">
                                <textarea id="txtNullTradeRemarks" onkeypress="javascript:fnRemoveClass(this,'NullTradeRemarks');" class="form-control" data-tabindex="3" cols="20" rows="4" placeholder="Enter Remarks"></textarea>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button id="btnSubmitNullTrade" type="button" class="btn green" onclick="javascript:fnValidateNullTrade();">Submit</button>
                    <button id="btnCancelNullTrade" type="button" class="btn default" data-dismiss="modal" onclick="javascript:fnClearForm();">Cancel</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade in" id="modalZeroTradeValue" tabindex="-1" role="dialog" aria-hidden="True" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog" style="width: 50%">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="True"></button>
                    <h4 class="modal-title"><b>Trade Value cannot be 0. Are you reporting a Null Trade?</b></h4>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn dark btn-outline" data-toggle="modal" data-target="#modalZeroTradeValue">NO</button>
                    <input value="YES" id="btnZeroTradeValue" data-dismiss="modal" class="btn red" onclick="goToNullTrade()" type="submit" />
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade in" id="modalWithdrawRequest" tabindex="-1" role="dialog" aria-hidden="True" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog" style="width: 50%">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="True"></button>
                    <h4 class="modal-title"><b>Are you sure you want to withdraw this request?</b></h4>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn dark btn-outline" data-toggle="modal" data-target="#modalWithdrawRequest">NO</button>
                    <input value="YES" id="btnWithdrawTradeRequest" data-dismiss="modal" class="btn red" onclick="fnRequestAction('Cancel')" type="submit" />
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade in" id="modalSubmitRequest" tabindex="-1" role="dialog" aria-hidden="True" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog" style="width: 50%">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="True"></button>
                    <h4 class="modal-title"><b>Are you sure you want to submit this request?</b></h4>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn dark btn-outline" data-toggle="modal" data-target="#modalSubmitRequest">NO</button>
                    <input value="YES" id="btnSubmitTradeRequest" data-dismiss="modal" class="btn red" onclick="fnRequestAction('InApproval')" type="submit" />
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade in" id="modalUnterkaing" tabindex="-1" role="dialog" aria-hidden="True" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog" style="width: 50%">
            <div class="modal-content">
                <div class="modal-body" id="UndertakingPreclearanceRequest">
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn dark btn-outline" data-toggle="modal" data-target="#modalUnterkaing">NO</button>
                    <input value="YES" data-dismiss="modal" class="btn red" onclick="fnRequestAction('InApproval')" type="submit" />
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade in" id="modalSaveAsDraftRequest" tabindex="-1" role="dialog" aria-hidden="True" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog" style="width: 50%">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="True"></button>
                    <h4 class="modal-title"><b>Are you sure you want to save as draft this request?</b></h4>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn dark btn-outline" data-toggle="modal" data-target="#modalSaveAsDraftRequest">NO</button>
                    <input value="YES" id="btnSaveAsDraftTradeRequest" data-dismiss="modal" class="btn red" onclick="fnRequestAction('Draft')" type="submit" />
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade in" id="modalBrokerNoteUploadConfirmation" tabindex="-1" role="dialog" aria-hidden="True" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog" style="width: 50%">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="True"></button>
                    <h4 class="modal-title"><b>Are you sure you want to upload Trade Details amount against this request?</b></h4>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn dark btn-outline" data-toggle="modal" data-target="#modalBrokerNoteUploadConfirmation">NO</button>
                    <input value="YES" id="btnUploadBrokerNoteConfirmation" data-dismiss="modal" class="btn red" onclick="fnSubmitBrokerNote()" type="submit" />
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="ModalPreclaranceRequestOtherCompany" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnClearOtherRequest();"></button>
                    <h4 class="modal-title text-dark bold">Request for Other Securities</h4>
                </div>
                <div class="modal-body">
                    <form class="form-horizontal" role="form">
                        <div class="form-body">
                            <div class="form-group">
                                <label id="lblForOtherSecurity" class="col-md-3 control-label">
                                    For
                                    <span class="required">* </span>
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <select id="ddlForOther" class="form-control">
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">
                                    Type Of Security
                                    <span class="required">* </span>
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <select id="ddlTypeOfSecurityOther" class="form-control">
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">
                                    Company
                                    <span class="required">* </span>
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <select id="ddlRestrictedCompaniesOther" class="form-control select2" data-placeholder="Select Company"></select>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">
                                    Trade Quantity
                                    <span class="required">* </span>
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="fa fa-bell-o"></i>
                                            </span>
                                            <input id="txtTradeQuantityOther" type="number" class="form-control" placeholder="Enter quantity" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">
                                    Transaction Type
                                    <span class="required">* </span>
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <select id="ddlTypeOfTransactionOther" class="form-control">
                                        </select>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-3 control-label">
                                    Requested Transaction Date<span class="required">* </span>

                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </span>
                                            <input id="txtRequestedTransactionDateOther" class="form-control RequestedTransactionDate bg-white" readonly="" type="text" value="" />

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">
                                    Proposed Trade Price per share
                                    <span class="required">* </span>
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="fa fa-bell-o"></i>
                                            </span>
                                            <input id="txtShareCurrentMarketPriceOther" type="number" class="form-control" placeholder="Proposed Trade Price per share" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Proposed Transaction Through</label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <select id="ddlProposedTransactionThroughOther" class="form-control">
                                            <option value="">--Select--</option>
                                            <option value="Stock Exchange">Stock Exchange</option>
                                            <option value="Off-Market Deal">Off-Market Deal</option>
                                        </select>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </form>
                </div>
                <div class="modal-footer">
                    <button id="btnSubmitOtherSecurityRequest" type="submit" class="btn btn-outline dark" data-toggle="modal" onclick="javascript:fnSubmitPreClearanceRequestOther();">Submit</button>
                    <button id="btnCancelOther" type="button" class="btn default" data-dismiss="modal" onclick="javascript:fnClearOtherRequest();">Cancel</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <div class="modal fade" id="mdlTradeDetails" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-full">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnClearFormBrokerNote();"></button>
                    <div class="modal-title">
                        <ul>
                            <li><b>PLEASE REVIEW YOUR PAST TRADE TRANSACTIONS BEFORE SUBMISSION OF PRE-CLEARANCE REQUEST</b></li>
                            <li>List of Transactions that you or your relatives have completed in Company’s Shares over the last Six Months is mentioned below. Please add any Transaction (buy/ sell) if found missing.</li>
                        </ul>
                        
                        </div>
                        
                </div>
                <div class="modal-body">
                    
                    <form class="form-horizontal" role="form">
                        <div class="table-responsive table-hover">
                            <table id="tblTrades" class="table table-bordered">
                                <thead>
                                    <tr>
                                        <th>S.No</th>
                                        <th>By</th>
                                        <th>Relation</th>
                                        <th>Transaction Date</th>
                                        <th>Transaction (Buy/Sell etc.)</th>
                                        <th>No. of Shares</th>
                                        <th>Estimated Transaction Value</th>
                                        <%--<th>PAN#</th>
                                        <th>Folio</th>--%>
                                    </tr>
                                </thead>
                                <tbody id="tbdTransactions">
                                </tbody>
                            </table>
                        </div>
                        <h5><b>Please use the below fields to add any transaction that is missing. If above details are correct, please click on "Skip" button.</b></h5>
                        <table>
                            <thead>
                                <tr>
                                    <th style="padding-left:5px;padding-right:5px;">For</th>
                                    <th style="padding-left:5px;padding-right:5px;">PAN</th>
                                    <th style="padding-left:5px;padding-right:5px;">Demat</th>
                                    <th style="padding-left:5px;padding-right:5px;">Holding</th>
                                    <th style="padding-left:5px;padding-right:5px;">Pledged</th>
                                    <th style="padding-left:5px;padding-right:5px;">Date</th>
                                    <th style="padding-left:5px;padding-right:5px;">Buy/Sell</th>
                                    <th style="padding-left:5px;padding-right:5px;">Quantity</th>
                                    <th style="padding-left:5px;padding-right:5px;">Total Amount</th>
                                    <th style="padding-left:5px;padding-right:5px;">Transaction Through</th>
                                    <th style="padding-left:5px;padding-right:5px;">Exchange</th>
                                    <%--<th style="padding-left:5px;padding-right:5px;">Total Amount</th>--%>
                                    <th></th>
                                </tr>
                            </thead>
                            <tbody id="tbdTrade">
                            </tbody>
                        </table>

                    </form>
                </div>
                <div class="modal-footer">
                    <%--<small class="text-info">Please click on Submit, if you have not made any recent transaction in company shares to continue to new trade request screen.</small>--%>
                    <button id="btnSubmitTD" type="submit" class="btn btn-outline dark" onclick="javascript:fnSaveBrokerNote();">Skip & Continue to Pre-Clearance Request</button>
                    <button id="btnCancelTD" type="button" class="btn default" data-dismiss="modal">Cancel</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
    </div>
    <div class="modal bg-dark-90 fade in" id="modalSubmitPrevTradeConfirmation" tabindex="-1" role="dialog" aria-hidden="True"  data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">
                        <b>
                            Do you want to report trade transactions done in the current quarter before proceeding to add pre-clearance request?<br />
                            <ul>
                                <li>
                                    Click 'NO' to proceed pre-clearance request
                                </li>
                                <li>
                                    Click 'YES' to first report trade transactions done in the current quarter and then add pre-clearance request
                                </li>
                            </ul>
                        </b>
                    </h4>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn dark btn-outline" data-dismiss="modal" onclick="javascript:fnOpenPreClearance('No');">NO</button>
                    <button type="button" class="btn dark btn-outline" data-dismiss="modal" onclick="javascript:fnOpenPreClearance('Yes');">YES</button>
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
    <script src="../assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js" type="text/javascript"></script>
    <script src="../assets/global/scripts/app.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js" type="text/javascript"></script>
    <script src="../assets/pages/scripts/components-date-time-pickers.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/select2/js/select2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="js/Global.js?<%=DateTime.Now %>"></script>
    <%--<script src="js/DateFormat.js" type="text/javascript"></script>--%>
    <script type="text/javascript" src="js/PreClearance.js?<%=DateTime.Now %>"></script>
    <script type="text/javascript" src="js/FileSaver.js?<%=DateTime.Now %>"></script>
</asp:Content>