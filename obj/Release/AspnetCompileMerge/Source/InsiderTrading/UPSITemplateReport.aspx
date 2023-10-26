<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" CodeBehind="UPSITemplateReport.aspx.cs" Inherits="ProcsDLL.InsiderTrading.UPSITemplateReport" %>

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
                    <div class="col-md-4 col-lg-4" style="display:none;">
                        <label for="User" style="text-align: center; display: block;">User</label>
                        <select class="form-control select2" name="User" id="bindUser">
                        </select>
                    </div>
                    <div class="col-md-3 col-lg-3">
                        <label for="from_date" style="text-align: center; display: block;">UPSI Shared From</label>
                        <input id="txtFromDate" data-date-format="dd/mm/yyyy" type="text" class="form-control date-picker bg-white" name="from_date" readonly="" autocomplete="off"/>
                    </div>
                    <div class="col-md-3 col-lg-3">
                        <label for="to_date" style="text-align: center; display: block;">UPSI Shared To</label>
                        <input id="txtToDate" data-date-format="dd/mm/yyyy" type="text" class="form-control date-picker bg-white" name="to_date" readonly=""  autocomplete="off"/>
                    </div>
                    <div class="col-md-2 col-lg-2">
                        <input id="txtGetTadingReport" type="button" style="background-color: limegreen; margin-top:25px;" class="form-control" value="Run" onclick="fnGetUPSIReportOnRun();" name="to_date" />
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
                                <th>SL No.</th>
                                <th>Nature of UPSI shared</th>
                                <th>Names of persons who have shared the UPSI</th>
                                <th>Names of persons with whom UPSI is shared</th>
                                <th>PAN or any other identifier authorized by law where PAN is not available of all the persons.</th>
                                <th>Date when UPSI shared</th>
                                <th>Mode of Sharing UPSI</th>
                                <th>Date when information ceases to be UPSI</th>
                                <th>Attachment, if Any</th>
                                <th>Date of Entry Shared UPSI in SDD</th>
                                <th>Remarks</th>
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
    <script src="../assets/pages/scripts/components-select2.min.js" type="text/javascript"></script>

    <script src="js/Global.js?<%=DateTime.Now %>" type="text/javascript"></script>
    <script src="js/UPSITemplateReport.js?<%=DateTime.Now %>" type="text/javascript"></script>
</asp:Content>
