<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" CodeBehind="TradingReport.aspx.cs" Inherits="ProcsDLL.InsiderTrading.TradingReport" %>
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
        .page-content {
            min-height: 800px !important;
        }

        table.dataTable tbody th,
        table.dataTable tbody td {
            white-space: nowrap;
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
                    <span class="caption-subject font-red sbold uppercase">Trading Report</span>
                </div>
            </div>

            <div class="portlet-body">
                <div class="row">
                    <div class="col-md-3 col-lg-3">
                        <label for="Company" style="text-align: center; display: block;">Company</label>
                        <select class="form-control select2" name="Company" id="bindBusinessUnit">
                        </select>
                    </div>
                    <div class="col-md-3 col-lg-3">
                        <label for="User" style="text-align: center; display: block;">User</label>
                        <select class="form-control select2" name="User" id="bindUser">
                        </select>
                    </div>
                    <div class="col-md-2 col-lg-2">
                        <label for="from_date" style="text-align: center; display: block;">From</label>
                        <input id="txtFromDate" data-date-format="dd/mm/yyyy" type="text" style="background-color:white" class="form-control date-picker" name="from_date" readonly="" autocomplete="off"/>
                    </div>
                    <div class="col-md-2 col-lg-2">
                        <label for="to_date" style="text-align: center; display: block;">To</label>
                        <input id="txtToDate" data-date-format="dd/mm/yyyy" style="background-color:white" type="text" class="form-control date-picker" readonly="" name="to_date" autocomplete="off"/>
                    </div>
                    <div class="col-md-2 col-lg-2">
                        <button id="txtGetTadingReport" type="button" style="background-color: limegreen;margin-top: 25px;" class="form-control" onclick="fnGetTradingReport();" name="to_date">Run</button>
                    </div>
                </div>
                <br />
                <br />
                <br />
                <br />
                <div class="row">
                    <div class="col-md-3">
                        <label>Reason for NC</label>
                        <select id="ddlFilter" class="form-control">
                        </select>
                    </div>
                    <table class="table table-striped table-hover display text-nowrap table-bordered" id="tbl-tradeReport-setup">
                        <thead>
                            <tr>
                                <th rowspan="2">Name</th>
                                <th rowspan="2">Relative Name</th>
                                <th rowspan="2">Relation</th>
                                <th rowspan="2">PAN</th>
                                <th rowspan="2">DPID/Client Id</th>
                                <%--      <th>Holding As On Date</th>--%>
                                <th rowspan="2">Benpos Upload Date</th>
                                <th rowspan="2">Total Trade Quantity</th>
                                <th rowspan="2">Total Trade Value</th>
                                <th rowspan="2">Transaction Type</th>
                                <th colspan="3" style="text-align: center;">Equity</th>
                                <th colspan="3" style="text-align: center;">ESOP</th>
                                <th colspan="3" style="text-align: center;">Physical Share</th>
                                <th rowspan="2">From</th>
                                <th rowspan="2">Is Non-Compliant?</th>
                                <th rowspan="2">Reason for NC</th>
                                <th rowspan="2">CO Remarks</th>
                            </tr>
                            <tr>
                                <th>Quantity</th>
                                <th>Value</th>
                                <th>Trade Date</th>

                                <th>Quantity</th>
                                <th>Value</th>
                                <th>Trade Date</th>

                                <th>Quantity</th>
                                <th>Value</th>
                                <th>Trade Date</th>
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
    <script src="js/TradingReport.js?<%=DateTime.Now %>" type="text/javascript"></script>
</asp:Content>