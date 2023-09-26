<%@ Page Title="Preclearance Report" Language="C#" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" AutoEventWireup="true" CodeBehind="Pre_Clearance_Other_Company_Report.aspx.cs" Inherits="ProcsDLL.InsiderTrading.Pre_Clearance_Other_Company_Report" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--start date --%>
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
    <div class="page-content-inner">
        <!-- BEGIN Portlet PORTLET-->
        <div class="portlet light portlet-fit ">
            <div class="portlet-title">
                <div class="caption">
                    <i class="icon-settings font-red"></i>
                    <span class="caption-subject font-red sbold uppercase">Esop Report</span>
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
                        <select class="form-control select2" name="User" id="bindUser" data-placeholder="Select User">
                        </select>
                    </div>
                    <div class="col-md-2 col-lg-2">
                        <label for="from_date" style="text-align: center; display: block;">From</label>
                        <input id="txtFromDate" data-date-format="dd/mm/yyyy" type="text" class="form-control date-picker" name="from_date" autocomplete="off"/>
                    </div>
                    <div class="col-md-2 col-lg-2">
                        <label for="to_date" style="text-align: center; display: block;">To</label>
                        <input id="txtToDate" data-date-format="dd/mm/yyyy" type="text" class="form-control date-picker" name="to_date" autocomplete="off"/>
                    </div>
                    <div class="col-md-2 col-lg-2">
                        <label>&nbsp;</label>
                        <input id="txtGetEsopReport" type="button" style="background-color: limegreen;" class="form-control" value="Run" onclick="fnGetReport();" name="to_date" />
                    </div>
                </div>
                <br />
                <br />
                <br />
                <br />
                <div class="row">
                    <table class="table table-striped table-bordered" id="tbl-preclearance-other-setup">
                                <thead>
                                    <tr>
                                        <th scope="col">#</th>
                                        <th>For</th>
                                        <th>Relation</th>
                                        <th>Quantity</th>
                                        <th>Type</th>
                                        <th>Requested</th>
                                        <th>Status</th>
                                        <th>Reviewed On</th>
                                        <th>Reviewed By</th>
                                    </tr>
                                </thead>
                                <tbody id="tbdPreClearanceOtherList">
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
    <script src="js/PreclearanceOtherCompanyReport.js?<%=DateTime.Now %>" type="text/javascript"></script>
</asp:Content>