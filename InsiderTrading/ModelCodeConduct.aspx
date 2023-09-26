<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" CodeBehind="ModelCodeConduct.aspx.cs" Inherits="ProcsDLL.InsiderTrading.ModelCodeConduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--Start Datetime--%>
    <link href="../assets/global/css/components.min.css" rel="stylesheet" />
    <link href="../assets/global/css/plugins.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/bootstrap-timepicker/css/bootstrap-timepicker.min.css" rel="stylesheet" />
    <%--End Datetime--%>
    <link href="../assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/select2/css/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/select2/css/select2-bootstrap.min.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%-- ========================= ModelCodeConduct List ============================= --%>
    <div class="page-content-inner">
        <div class="row">
            <div class="col-md-12">
                <div class="portlet light portlet-fit ">
                    <div class="portlet-title">
                        <div class="caption">
                            <i class="icon-settings font-red"></i>
                            <span class="caption-subject font-red sbold uppercase">Configuration List</span>
                        </div>
                    </div>
                    <div class="portlet-body">

                        <div class="row" style="overflow-y: auto; overflow-x: hidden; max-height: 58vh;">
                            <div class="portlet box purple ">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-gift"></i>General Settings
                                    </div>
                                    <div class="tools">
                                        <a data-toggle="collapse" style="color: white" data-target="#GeneralSettings"><i class="fa fa-plus-circle"></i></a>
                                    </div>
                                </div>
                                <div id="GeneralSettings" class="portlet-body form collapse">
                                    <form class="form-horizontal" role="form">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">Frequency of Periodic Disclosure</label>
                                                <div class="col-md-9">
                                                    <div class="input-inline input-medium">
                                                        <select id="ddlFrequencyOfPeriodicClosure" class="form-control">
                                                            <option value="Yearly">Yearly
                                                            </option>
                                                            <option value="Half_Yearly">Half Yearly
                                                            </option>
                                                            <option value="Quaterly">Quaterly  
                                                            </option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">Cut off Date for Periodic Disclosure</label>
                                                <div class="col-md-9">
                                                    <div class="input-inline input-medium">
                                                        <div class="input-group input-medium date date-picker" data-date-format="yyyy/mm/dd" data-date-start-date="+0d">
                                                            <input id="txtPeriodicDisclosureDate" class="form-control" readonly="readonly" type="text" />
                                                            <span class="input-group-btn">
                                                                <button class="btn default" type="button">
                                                                    <i class="fa fa-calendar"></i>
                                                                </button>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">Days Allowed For Reporting OF Periodic Disclosure</label>
                                                <div class="col-md-9">
                                                    <div class="input-inline input-medium">
                                                        <div class="input-group">
                                                            <span class="input-group-addon">
                                                                <i class="fa fa-envelope"></i>
                                                            </span>
                                                            <input id="txtDaysAllowed_For_Reporting_Periodic_Disclosure" type="number" class="form-control" />
                                                        </div>
                                                    </div>
                                                    <span class="help-inline">Inline help. </span>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-6 control-label"><b>Trade Limit Based on (Value)</b></label>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">Amount Limit for Pre-clearance </label>
                                                <div class="col-md-9">
                                                    <div class="input-inline input-medium">
                                                        <div class="input-group">
                                                            <span class="input-group-addon">
                                                                <i class="fa fa-envelope"></i>
                                                            </span>
                                                            <input id="txtPre_clear_AmountLimit" type="number" class="form-control" />
                                                        </div>
                                                    </div>
                                                    <span class="help-inline">Inline help. </span>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">Share Limit for Pre-clearance </label>
                                                <div class="col-md-9">
                                                    <div class="input-inline input-medium">
                                                        <div class="input-group">
                                                            <span class="input-group-addon">
                                                                <i class="fa fa-envelope"></i>
                                                            </span>
                                                            <input id="txtPre_clear_ShareLimit" type="number" class="form-control" />
                                                        </div>
                                                    </div>
                                                    <span class="help-inline">Inline help. </span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-actions right1">
                                            <button type="button" class="btn default">Cancel</button>
                                            <button id="btnSave" type="submit" data-dismiss="modal" class="btn green" onclick="javascript:fnSaveCodeConductModel();">Save</button>
                                        </div>
                                    </form>
                                </div>
                            </div>

                            <div class="portlet box purple ">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-gift"></i>Pre-Clearance Requirements
                                    </div>
                                    <div class="tools">
                                        <a data-toggle="collapse" style="color: white" data-target="#Pre-ClearanceRequirements"><i class="fa fa-plus-circle"></i></a>
                                    </div>
                                </div>
                                <div id="Pre-ClearanceRequirements" class="portlet-body form collapse">
                                    <form class="form-horizontal" role="form">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">Pre-Clearance required for </label>
                                                <div class="col-md-9">
                                                    <div class="input-inline input-medium">
                                                        <select id="ddlPre_Clearance_Required" multiple="multiple" class="form-control">
                                                            <option value="Buy">Buy
                                                            </option>
                                                            <option value="Cashless_All">Cashless All 
                                                            </option>
                                                            <option value="Cashless_Partial">Cashless Partial
                                                            </option>
                                                            <option value="ESOP">ESOP
                                                            </option>
                                                            <option value="Pledge">Pledge
                                                            </option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">Prohibit Pre-clearance during non trading period for </label>
                                                <div class="col-md-9">
                                                    <div class="input-inline input-medium">
                                                        <select id="ddlProhibit_Pre_clearance" multiple="multiple" class="form-control">
                                                            <option value="Buy">Buy
                                                            </option>
                                                            <option value="Sell">Sell
                                                            </option>
                                                            <option value="Pledge">Pledge
                                                            </option>
                                                            <option value="Pledge_Revoke">Pledge Revoke
                                                            </option>
                                                            <option value="Pledge_Invoke">Pledge Invoke
                                                            </option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">Allow multiple pre-clearance request for Self </label>
                                                <div class="col-md-9">
                                                    <div class="input-inline input-medium">
                                                        <select id="ddlMultiple_Pre_clearance_Request_Self" class="form-control">
                                                            <option value="Yes">Yes
                                                            </option>
                                                            <option value="No">No
                                                            </option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">Allow multiple pre-clearance request for relatives </label>
                                                <div class="col-md-9">
                                                    <div class="input-inline input-medium">
                                                        <select id="ddlMultiple_Pre_clearance_Request_Relatives" class="form-control">
                                                            <option value="Yes">Yes
                                                            </option>
                                                            <option value="No">No
                                                            </option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">Pre-Clearance approval to be given within </label>
                                                <div class="col-md-9">
                                                    <div class="input-inline input-medium">
                                                        <div class="input-group">
                                                            <span class="input-group-addon">
                                                                <i class="fa fa-envelope"></i>
                                                            </span>
                                                            <input id="txtPre_Clearance_Approval_Given_Within" type="number" class="form-control" />
                                                        </div>
                                                    </div>
                                                    <span class="help-inline">trading days by Compliance officer (Including request submission day) </span>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">Next designated person for pre-clearance approval </label>
                                                <div class="col-md-9">
                                                    <div class="input-inline input-medium">
                                                        <div class="input-group">
                                                            <span class="input-group-addon">
                                                                <i class="fa fa-envelope"></i>
                                                            </span>
                                                            <input id="txtNext_Designated_Person_Pre_Clearance_Approval" type="email" class="form-control" />
                                                        </div>
                                                    </div>
                                                    <span class="help-inline">if approval not provided by compliance officer </span>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">Pre-Clearance approval to be given within </label>
                                                <div class="col-md-9">
                                                    <div class="input-inline input-medium">
                                                        <div class="input-group">
                                                            <span class="input-group-addon">
                                                                <i class="fa fa-envelope"></i>
                                                            </span>
                                                            <input id="txt_Pre_Clearance_Approval_Given_Within" type="number" class="form-control" />
                                                        </div>
                                                    </div>
                                                    <span class="help-inline">trading days by next designated approval person  </span>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">Auto approval if no response is provided by both </label>
                                                <div class="col-md-9">
                                                    <div class="input-inline input-medium">
                                                        <select id="ddlAuto_Approval_" class="form-control">
                                                            <option value="Yes">Yes
                                                            </option>
                                                            <option value="No">No
                                                            </option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">Seek decleration from employee regarding possession of UPSI </label>
                                                <div class="col-md-9">
                                                    <div class="input-inline input-medium">
                                                        <select id="ddlSeek_decleration_" class="form-control">
                                                            <option value="Yes">Yes
                                                            </option>
                                                            <option value="No">No
                                                            </option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">If Yes, Enter decleration to be sought from Insider at the time of submission of trading request</label>
                                                <div class="col-md-3">
                                                    <textarea id="txtarea_Seek_Declaration" class="form-control" rows="3" placeholder="Minimum 256 characters"></textarea>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-actions right1">
                                            <button type="button" class="btn default">Cancel</button>
                                            <button id="btnSave_Pre-ClearanceRequirements" type="submit" data-dismiss="modal" class="btn green" onclick="javascript:fnSaveCodeConductModel();">Save</button>
                                        </div>
                                    </form>
                                </div>
                            </div>

                            <div class="portlet box purple ">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-gift"></i>Contra Trade
                                    </div>
                                    <div class="tools">
                                        <a data-toggle="collapse" style="color: white" data-target="#ContraTrade"><i class="fa fa-plus-circle"></i></a>
                                    </div>
                                </div>
                                <div id="ContraTrade" class="portlet-body form collapse">
                                    <form class="form-horizontal" role="form">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">Contra Trade Based on </label>
                                                <div class="col-md-9">
                                                    <div class="input-inline input-medium">
                                                        <select id="ddlContra_Trade_Based_On" multiple="multiple" class="form-control">
                                                            <option value="Shares">Shares
                                                            </option>
                                                            <option value="Warrants">Warrants
                                                            </option>
                                                            <option value="Convertible_debentures">Convertible debentures
                                                            </option>
                                                            <option value="Future_Contracts">Future Contracts
                                                            </option>
                                                            <option value="Option_Contracts">Option Contracts
                                                            </option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">Contra Trade not allowed for </label>
                                                <div class="col-md-9">
                                                    <div class="input-inline input-medium">
                                                        <div class="input-group">
                                                            <span class="input-group-addon">
                                                                <i class="fa fa-envelope"></i>
                                                            </span>
                                                            <input id="txtContra_Trade_not_allowed_for" type="number" class="form-control" />
                                                        </div>
                                                    </div>
                                                    <span class="help-inline">calendar days from opposite direction </span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-actions right1">
                                            <button type="button" class="btn default">Cancel</button>
                                            <button id="btnSave_ContraTrade" type="submit" data-dismiss="modal" class="btn green" onclick="javascript:fnSaveCodeConductModel();">Save</button>
                                        </div>
                                    </form>
                                </div>
                            </div>

                            <div class="portlet box purple ">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-gift"></i>Financial Result Declaration
                                    </div>
                                    <div class="tools">
                                        <a data-toggle="collapse" style="color: white" data-target="#FinancialResultDeclaration"><i class="fa fa-plus-circle"></i></a>
                                    </div>
                                </div>
                                <div id="FinancialResultDeclaration" class="portlet-body form collapse">
                                    <form class="form-horizontal" role="form">
                                        <div class="form-body">
                                        </div>
                                        <div class="form-actions right1">
                                            <button type="button" class="btn default">Cancel</button>
                                            <button id="btnSave_FinancialResultDeclaration" type="submit" data-dismiss="modal" class="btn green" onclick="javascript:fnSaveCodeConductModel();">Save</button>
                                        </div>
                                    </form>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%-- ========================= Add New Record in ModelCodeConduct List ============================= --%>
    <%--Start Datetime--%>
    <script src="../assets/editor/jquery-te-1.4.0.min.js"></script>
    <script src="../assets/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js"></script>
    <script src="../assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.js"></script>
    <script src="../assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
    <script src="../assets/global/plugins/bootstrap-timepicker/js/bootstrap-timepicker.min.js"></script>
    <script src="../assets/pages/scripts/components-date-time-pickers.min.js" type="text/javascript"></script>
    <%-- <script src="../assets/global/scripts/app.min.js" type="text/javascript"></script>--%>
    <%--End Datetime--%>
    <script src="../assets/global/scripts/datatable.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/datatables/datatables.min.js" type="text/javascript"></script>
    <%--<script src="assets/global/plugins/datatables/datatables.min.js" type="text/javascript"></script>--%>
    <script src="../assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js" type="text/javascript"></script>
    <script src="../assets/global/scripts/app.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js" type="text/javascript"></script>
    <script src="../assets/pages/scripts/components-date-time-pickers.min.js" type="text/javascript"></script>
    <%--<script src="../assets/pages/scripts/table-datatables-buttons.min.js" type="text/javascript"></script>--%>
    <script src="../assets/global/plugins/select2/js/select2.min.js" type="text/javascript"></script>
    <script src="js/Global.js"></script>
    <script src="js/ModelCodeConduct.js"></script>
</asp:Content>
