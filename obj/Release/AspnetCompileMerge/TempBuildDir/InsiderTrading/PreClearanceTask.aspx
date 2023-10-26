<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PreClearanceTask.aspx.cs" Inherits="ProcsDLL.InsiderTrading.PreClearanceTask" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" %>
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
        .modal-dialog {
            max-width: 700px !important;
            margin: 1.75rem auto;
        }
        .result-btn {
            background: #fb9101;
            color: #fff;
            padding: 5px 18px;
            border-radius: 16px;
        }
        .text-green {
            color: #36c752 !important;
        }
        .text-red {
            color: #ff4040;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-left: 0px!important; margin-right: 0px !important;">
        <div class="col-md-12">
            <div class="portlet light portlet-fit ">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="icon-settings font-red"></i>
                        <span class="caption-subject font-red sbold uppercase">Pre-Clearance Task</span>
                    </div>
                </div>
                <div class="portlet-body">
                    <div class="table-toolbar">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="btn-group-devided"></div>
                            </div>
                        </div>
                    </div>
                    <br /><br />
                    <div class="">
                        <table class="table table-striped table-hover display text-nowrap table-bordered" id="tbl-preclearance-task">
                            <thead>
                                <tr>
                                    <th rowspan="2">For</th>
                                    <th rowspan="2">Relation</th>
                                    <th rowspan="2">Type Of Security</th>
                                    <th rowspan="2">Company</th>
                                    <th colspan="2" class="display-hide">Trade in Month</th>
                                    <th rowspan="2">Trade Quantity</th>
                                    <th rowspan="2">Value</th>
                                    <th rowspan="2">Transaction Type</th>
                                    <th rowspan="2">Trade Date</th>
                                    <th rowspan="2">Trade Detail</th>
                                    <th rowspan="2"></th>
                                </tr>
                                <tr class="display-hide">
                                    <th>Quantity</th>
                                    <th>Value</th>
                                </tr>
                            </thead>
                            <tbody id="tbdPreClearanceTaskList">
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="approve" class="modal fade" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Approve</h5>

                    <button type="button" class="close" data-dismiss="modal" onclick="javascript:fnCloseModal();">&times;</button>
                </div>

                <div class="modal-body p-20">
                    <p>You have decided to <span class="text-green f-w-60">Approve</span> this request. Please Provide your comments below, if any, and press confirm to submit the request.</p>

                    <textarea id="txtAreaApprove" name="textarea" class="form-control" placeholder="Type your comments here..."></textarea>
                    <%--<input id="txtApprove" type="hidden" value="" />--%>
                    <br />
                    <div class="row m-t-20">
                        <div class="col-md-12 text-center">
                            <a class="f-s-15 result-btn p-r-100 p-l-100" href="javascript:fnSubmitActionTaken('Approved','txtAreaApprove');">Submit</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="reject" class="modal fade" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Reject</h5>

                    <button type="button" class="close" data-dismiss="modal" onclick="javascript:fnCloseModal();">&times;</button>
                </div>

                <div class="modal-body p-20">
                    <p>You have decided to <span class="text-red f-w-60">Reject</span> this request. Please Provide your comments below, if any, and press confirm to submit the request.</p>

                    <textarea id="txtAreaReject" name="textarea" class="form-control" placeholder="Type your comments here..."></textarea>
                    <%--<input id="txtReject" type="hidden" value="" />--%>
                    <br />
                    <div class="row m-t-20">
                        <div class="col-md-12 text-center">
                            <a class="f-s-15 result-btn p-r-100 p-l-100" href="javascript:fnSubmitActionTaken('Rejected','txtAreaReject');">Submit</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="tradeDetails" class="modal fade" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-dialog-centered" style="min-width:80%">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Approval Required on Pre-Clearance Request</h5>

                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>

                <div class="modal-body p-20" id="bdTradeDetails" style="overflow:auto;">
                   
                </div>
            </div>
        </div>
    </div>
    <input type="hidden" id="txtTaskId" />

    <%--Start Datetime--%>
    <script src="../assets/editor/jquery-te-1.4.0.min.js"></script>
    <script src="../assets/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js"></script>
    <script src="../assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.js"></script>
    <script src="../assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
    <script src="../assets/global/plugins/bootstrap-timepicker/js/bootstrap-timepicker.min.js"></script>
    <script src="../assets/pages/scripts/components-date-time-pickers.min.js" type="text/javascript"></script>
    <%--End Datetime--%>
    <script src="../assets/global/scripts/datatable.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/datatables/datatables.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js" type="text/javascript"></script>
    <script src="../assets/global/scripts/app.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js" type="text/javascript"></script>
    <script src="../assets/pages/scripts/components-date-time-pickers.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/select2/js/select2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="js/Global.js?<%=DateTime.Now %>"></script>
    <script type="text/javascript" src="js/PreClearanceTask.js?<%=DateTime.Now %>"></script>
</asp:Content>