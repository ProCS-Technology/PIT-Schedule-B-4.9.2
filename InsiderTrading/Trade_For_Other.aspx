<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" CodeBehind="Trade_For_Other.aspx.cs" Inherits="ProcsDLL.InsiderTrading.Trade_For_Other" %>

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
    <style>
        .page-content-wrapper > .page-content{
            min-height: 1000px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-content-inner">
       <%-- <div class="row">--%>
            <div class="col-md-12">
                <div class="portlet light portlet-fit ">
                    <div class="portlet-title">
                        <div class="caption">
                            <i class="icon-settings font-red"></i>
                            <span class="caption-subject font-red sbold uppercase">PRE-CLEARANCE REQUEST FOR OTHERS</span>
                        </div>
                    </div>
                    <div class="portlet-body">
                        <div class="table-toolbar">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="btn-group-devided">
                                        <button id="btnAdd" runat="server" class="btn green" data-target="#stack1" onclick="javascript:fnAddPreClearance();" data-toggle="modal">
                                            DETAILS <i class="fa fa-plus"></i>
                                        </button>
                                        &nbsp;
                                    </div>
                                </div>
                            </div>
                        </div>
                        <table class="table table-striped table-hover table-bordered" id="tbl-preclearance-setup">
                            <thead>
                                <tr>
                                    <th>For</th>
                                    <th>Security</th>
                                    <th>Company</th>
                                    <th>Quantity</th>
                                    <th>Type</th>
                                    <th style="display:none;">Trade Exchange</th>
                                    <th>Demat</th>
                                    <th>Requested</th>
                                    <th>Status</th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tbody id="tbdPreClearanceList">
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
      <%--  </div>--%>
    </div>

    <div class="modal fade" id="basic1" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnClearFormBrokerNote();"></button>
                    <h4 class="modal-title">BROKER NOTE</h4>
                </div>
                <div class="modal-body">
                    <form class="form-horizontal" role="form">
                        <div class="form-body">
                            <div class="form-group" style="display:none">
                                <label id="lblForBN" class="col-md-3 control-label">For</label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <select id="ddlForBN" disabled="disabled" class="form-control">
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Company</label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <select id="ddlRestrictedCompaniesBN" disabled="disabled" class="form-control">
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Transaction Type</label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <select id="ddlTypeOfTransactionBN" disabled="disabled" class="form-control">
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Requested Transaction Date</label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="fa fa-bell-o"></i>
                                            </span>
                                            <input id="txtRequestedTransactionDateBN" disabled="disabled" class="form-control date-picker" data-date-format="mm/dd/yyyy" type="text" value="" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Trade Quantity</label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="fa fa-bell-o"></i>
                                            </span>
                                            <input id="txtTradequantityBrokerNote" type="number" class="form-control" placeholder="Enter quantity" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Value Per Share</label>
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
                            <div class="form-group">
                                <label class="col-md-3 control-label">Total Amount</label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="fa fa-bell-o"></i>
                                            </span>
                                            <input id="txtTotalamount" type="number" class="form-control" readonly="readonly" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Demat Account</label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <select id="ddlDematAccountBrokerNote" class="form-control">
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Atual Date Of Transaction</label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="fa fa-bell-o"></i>
                                            </span>
                                            <input id="txtActualdateoftransaction" data-date-format="mm/dd/yyyy" class="form-control date-picker" type="text" value="" />
                                            <input type="hidden" id="preClearanceRequestIdBN" value="0" />
                                            <input type="hidden" id="txtTradeExchangeBN" value="0" />
                                            <input type="hidden" id="txtStatusBN" value="" />
                                            <input type="hidden" id="txtDematAccountBN" value="" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Broker Note</label>
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
                        </div>
                    </form>
                </div>
                <div class="modal-footer">
                    <button id="btnSubmitBrokerNote" type="submit" data-dismiss="modal" class="btn btn-outline dark" onclick="javascript:AddUpdateBrokerNote();">Submit</button>
                    <%--                    <button id="btnSaveAsDraftPreClearanceRequest" type="button" data-dismiss="modal" class="btn green" onclick="javascript:fnSubmitPreClearanceRequest('Draft');">Save As Draft</button>
                    <button id="btnCancelPreClearanceRequest" style="display: none;" type="button" class="btn default" data-dismiss="modal" onclick="javascript:fnSubmitPreClearanceRequest('Cancel')">Withdraw Request</button>--%>
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
                    <h4 class="modal-title">PRE-CLEARANCE REQUEST FOR OTHERS</h4>
                </div>
                <div class="modal-body">
                    <form class="form-horizontal" role="form">
                        <div class="form-body">
                           <div class="form-group">
                                <label id="lblFor_u" class="col-md-3 control-label">Select User</label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <select id="ddlFor_user" class="form-control" onchange="fnGetRelativeDetail(this.value)">
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label id="lblFor" class="col-md-3 control-label">For</label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <select id="ddlFor" class="form-control">
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Type Of Security</label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <select id="ddlTypeOfSecurity" class="form-control">
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Company</label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <select id="ddlRestrictedCompanies" class="form-control">
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Trade Quantity</label>
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
                                <label class="col-md-3 control-label">Transaction Type</label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <select id="ddlTypeOfTransaction" class="form-control">
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group" style="display:none;">
                                <label class="col-md-3 control-label">Trade Exachange</label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <select id="ddlTradeExchange" class="form-control">
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Demat Account</label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <select id="ddlDematAccount" class="form-control">
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Transaction Date</label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="fa fa-bell-o"></i>
                                            </span>
                                            <input id="txtRequestedTransactionDate" class="form-control date-picker" data-date-format="mm/dd/yyyy" type="text" value="" />
                                            <input type="hidden" id="preClearanceRequestId" value="0" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                              <div class="form-group">
                                <label class="col-md-3 control-label">Approval Process</label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <input type="checkbox" checked="checked" value="Auto Approved"/>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Upload Benpos</label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <input type="checkbox" checked="checked" value="Auto Approved"/>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </form>
                </div>
                <div class="modal-footer">
                    <button id="btnSubmitPreClearanceRequest" type="submit" data-dismiss="modal" class="btn btn-outline dark" onclick="javascript:fnSubmitPreClearanceRequest('InApproval');">Submit</button>
                    <button id="btnSaveAsDraftPreClearanceRequest" type="button" data-dismiss="modal" class="btn green" onclick="javascript:fnSubmitPreClearanceRequest('Draft');">Save As Draft</button>
                    <button id="btnCancelPreClearanceRequest" style="display: none; background-color: red!important;" class="btn green" data-dismiss="modal" onclick="javascript:fnSubmitPreClearanceRequest('Cancel')">Withdraw Request</button>
                    <button id="btnCancel" type="button" class="btn default" data-dismiss="modal" onclick="javascript:fnClearForm();">Cancel</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

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
    <script type="text/javascript" src="js/Global.js"></script>
    <script type="text/javascript" src="js/Trade_for_others.js"></script>
</asp:Content>
