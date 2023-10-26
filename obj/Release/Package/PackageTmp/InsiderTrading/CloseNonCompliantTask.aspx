<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" CodeBehind="CloseNonCompliantTask.aspx.cs" Inherits="ProcsDLL.InsiderTrading.CloseNonCompliantTask" %>
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
        .page-content {
            min-height: 800px !important;
        }

        table.dataTable tbody th,
        table.dataTable tbody td {
            white-space: nowrap;
        }

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
    <div class="col-md-12">
        <!-- BEGIN Portlet PORTLET-->
        <div class="portlet light portlet-fit ">
            <div class="portlet-title">
                <div class="caption">
                    <i class="icon-settings font-red"></i>
                    <span class="caption-subject font-red sbold uppercase">Non-Compliant Task</span>
                </div>
            </div>

            <div class="portlet-body">
                <div class="row">
                    <div class="col-md-3 col-lg-3">
                        <label for="Company" style="text-align: center; display: block;">Company</label>
                        <select class="form-control select2" name="Company" id="bindBusinessUnit">
                        </select>
                    </div>
                    <div class="col-md-4 col-lg-4">
                        <label for="User" style="text-align: center; display: block;">User</label>
                        <select class="form-control select2" name="User" id="bindUser">
                        </select>
                    </div>
                    <div class="col-md-2 col-lg-2">
                        <label for="from_date" style="text-align: center; display: block;">From</label>
                        <input id="txtFromDate" data-date-format="dd/mm/yyyy" type="text" style="background-color:white" class="form-control date-picker" readonly="" name="from_date" autocomplete="off" />
                    </div>
                    <div class="col-md-2 col-lg-2">
                        <label for="to_date" style="text-align: center; display: block;">To</label>
                        <input id="txtToDate" data-date-format="dd/mm/yyyy" type="text" style="background-color:white" class="form-control date-picker" readonly="" name="to_date" autocomplete="off" />
                    </div>
                    <div class="col-md-1 col-lg-1">
                        <input id="txtGetTadingReport" type="button" style="background-color: limegreen;margin-top: 25px;" class="form-control" value="Run" onclick="fnGetTradingReport();" />
                    </div>
                </div>
                <br />
                <br />
                <br />
                <br />
                <div class="row">
                    <table class="table table-striped table-hover display text-nowrap table-bordered" id="tbl-tradeReport-setup">
                        <thead>
                            <tr>
                                <th rowspan="2">Name</th>
                                <th rowspan="2">Relative Name</th>
                                <th rowspan="2">Relation</th>
                                <th rowspan="2">PAN</th>
                                <th rowspan="2">Folio No</th>
                                <%--      <th>Holding As On Date</th>--%>
                                <th rowspan="2">Benpos Upload Date</th>
                                <th rowspan="2">Total Trade Quantity</th>
                                <th rowspan="2">Total Trade Value</th>
                                <th rowspan="2">Transaction Type</th>
                                <th colspan="3" style="text-align: center;">Equity</th>
                                <th colspan="3" style="text-align: center;display:none;">ESOP</th>
                                <th colspan="3" style="text-align: center;display:none;">Physical Share</th>
                                <th rowspan="2">From</th>
                                <th rowspan="2">Is Non-Compliant?</th>
                                <th rowspan="2">Reason for NC</th>
                                <th rowspan="2">User Remarks</th>
                                <th rowspan="2">Action By CO</th>
                            </tr>
                            <tr>
                                <th>Quantity</th>
                                <th>Value</th>
                                <th>Trade Date</th>
                                <th style="display:none;">Quantity</th>
                                <th style="display:none;">Value</th>
                                <th style="display:none;">Trade Date</th>
                                <th style="display:none;">Quantity</th>
                                <th style="display:none;">Value</th>
                                <th style="display:none;">Trade Date</th>
                            </tr>
                        </thead>
                        <tbody id="tbdTradeReportList">
                        </tbody>
                    </table>
                </div>

            </div>

            <!-- END Portlet PORTLET-->
        </div>
    </div>

    <div id="approve" class="modal fade" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Non-Compliant</h5>

                    <button type="button" class="close" data-dismiss="modal" onclick="javascript:fnCloseModal();">&times;</button>
                </div>

                <div class="modal-body p-20">
                    <p>You have decided to make the transaction as <span class="text-green f-w-60">Non-Compliant</span>. Please Provide your comments below, if any, and press confirm to submit the confirmation of Non-Compliant.</p>

                    <textarea id="txtAreaApprove" name="textarea" class="form-control" placeholder="Type your comments here..."></textarea>
                    <input id="txtNonComplianceTaskId" type="hidden" value="" />
                    <br />
                    <div class="row m-t-20">
                        <div class="col-md-12 text-center">
                            <a class="f-s-15 result-btn p-r-100 p-l-100" href="javascript:fnSubmitActionTaken('Non-Compliant','txtAreaApprove');">Submit</a>
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
                    <h5 class="modal-title">Compliant</h5>

                    <button type="button" class="close" data-dismiss="modal" onclick="javascript:fnCloseModal();">&times;</button>
                </div>

                <div class="modal-body p-20">
                    <p>You have decided to make the transaction as <span class="text-green f-w-60">Compliant</span>. Please Provide your comments below, if any, and press confirm to submit the confirmation of Compliant.</p>

                    <textarea id="txtAreaReject" name="textarea" class="form-control" placeholder="Type your comments here..."></textarea>
                    <%--<input id="txtReject" type="hidden" value="" />--%>
                    <br />
                    <div class="row m-t-20">
                        <div class="col-md-12 text-center">
                            <a class="f-s-15 result-btn p-r-100 p-l-100" href="javascript:fnSubmitActionTaken('Compliant','txtAreaReject');">Submit</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

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
    <script src="../assets/global/plugins/select2/js/select2.full.min.js" type="text/javascript"></script>
    <script src="../assets/pages/scripts/components-select2.min.js" type="text/javascript"></script>

    <script src="../assets/global/plugins/flot/jquery.flot.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/flot/jquery.flot.resize.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/flot/jquery.flot.categories.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/flot/jquery.flot.pie.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/flot/jquery.flot.stack.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/flot/jquery.flot.crosshair.min.js" type="text/javascript"></script>
    <script src="js/Global.js?<%=DateTime.Now %>" type="text/javascript"></script>
    <script src="js/CloseNonCompliantTask.js?<%=DateTime.Now %>" type="text/javascript"></script>
</asp:Content>
