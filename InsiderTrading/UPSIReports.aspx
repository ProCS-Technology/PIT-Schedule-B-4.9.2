<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" CodeBehind="UPSIReports.aspx.cs" Inherits="ProcsDLL.InsiderTrading.UPSIReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../assets/global/css/components.min.css" rel="stylesheet" />
    <link href="../assets/global/css/plugins.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/bootstrap-timepicker/css/bootstrap-timepicker.min.css" rel="stylesheet" />
    <%--End date --%>
    <link href="../assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/select2/css/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/select2/css/select2-bootstrap.min.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .required {
            color: red;
        }

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
        <div class="portlet light portlet-fit">
            <div class="portlet-title">
                <div class="caption">
                    <i class="icon-settings font-red"></i>
                    <span class="caption-subject font-red sbold uppercase">UPSI Report</span>
                </div>
                <div class="tools">
                </div>
            </div>
            <div class="portlet-body">
                <div class="row">
                    <div class="col-md-4 col-lg-4">
                        <label for="User" style="text-align: center; display: block;">User</label>
                        <select class="form-control select2" name="User" id="bindUser">
                        </select>
                    </div>
                    <div class="col-md-3 col-lg-3">
                        <label for="from_date" style="text-align: center; display: block;">From</label>
                        <input id="txtFromDate" data-date-format="dd/mm/yyyy" type="text" class="form-control date-picker" name="from_date" autocomplete="off"/>
                    </div>
                    <div class="col-md-3 col-lg-3">
                        <label for="to_date" style="text-align: center; display: block;">To</label>
                        <input id="txtToDate" data-date-format="dd/mm/yyyy" type="text" class="form-control date-picker" name="to_date" autocomplete="off"/>
                    </div>
                    <div class="col-md-2 col-lg-2">
                        <input id="txtGetTadingReport" type="button" style="background-color: limegreen;" class="form-control" value="Run" onclick="fnGetUPSIReportOnRun();" name="to_date" />
                    </div>
                </div>
                <br />
                <br />
                <br />
                <br />
                <div class="row">
                    <table class="table table-striped table-hover display text-nowrap table-bordered" id="tbl-UPSIReport-setup">
                        <thead>
                            <tr>
                                <th>UPSI SHARED ON</th>
                                <th>SUBJECT</th>
                                <%--<th>SUBJECT CREATED BY</th>--%>
                                <%--<th>SUBJECT LINE CREATED ON</th>--%>
                                <th>FROM</th>
                                <th>TO</th>
                                <th>CC</th>
                                <%--<th>BCC</th>--%>
                                <th>BODY</th>
                                <th>ATTACHMENT</th>
                            </tr>
                        </thead>
                        <tbody id="tbdUPSIReportList">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        <!-- END Portlet PORTLET-->
    </div>

    <div id="messageBody" class="modal fade" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-dialog-centered" style="min-width: 80%">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Message</h5>

                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>

                <div class="modal-body p-20" id="bdMessage" style="overflow: auto; max-height: 500px;">
                </div>
            </div>
        </div>
    </div>

    <div id="attachmentBody" class="modal fade" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-dialog-centered" style="min-width: 80%">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Attachments</h5>

                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>

                <div class="modal-body p-20" id="bdAttachments" style="overflow: auto; max-height: 500px;">
                    <table class="table table-striped table-hover display text-nowrap table-bordered">
                        <thead>
                            <tr>
                                <th>Attachments</th>
                            </tr>
                        </thead>
                        <tbody id="tbdAttachmentBody">
                        </tbody>
                    </table>
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
    <script src="../assets/global/plugins/select2/js/select2.full.min.js" type="text/javascript"></script>
    <script src="../assets/global/scripts/app.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js" type="text/javascript"></script>
    <script src="../assets/pages/scripts/components-date-time-pickers.min.js" type="text/javascript"></script>
    <%--<script src="../assets/global/plugins/select2/js/select2.min.js" type="text/javascript"></script>--%>
    <script src="../assets/pages/scripts/components-select2.min.js" type="text/javascript"></script>

    <script src="../assets/global/plugins/flot/jquery.flot.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/flot/jquery.flot.resize.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/flot/jquery.flot.categories.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/flot/jquery.flot.pie.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/flot/jquery.flot.stack.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/flot/jquery.flot.crosshair.min.js" type="text/javascript"></script>
    <script src="js/Global.js?<%=DateTime.Now %>" type="text/javascript"></script>
    <script src="js/UPSIReports.js?<%=DateTime.Now %>" type="text/javascript"></script>
</asp:Content>

