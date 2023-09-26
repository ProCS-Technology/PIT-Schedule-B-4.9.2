<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" CodeBehind="Non-ComplianceEvents.aspx.cs" Inherits="ProcsDLL.InsiderTrading.Non_ComplianceEvents" %>

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
        .amcharts-chart-div > a {
            display: none !important;
        }

        .page-container .page-content-wrapper .page-content {
            min-height: 857px !important;
        }

        /*#flotTip {
            padding: 3px 5px;
            background-color: #000;
            z-index: 100;
            color: #fff;
            opacity: .80;
            filter: alpha(opacity=85);
        }*/
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12">
        <!-- BEGIN Portlet PORTLET-->
        <div class="portlet box">
            <div class="portlet-title">
                <div class="caption">
                    <i class="icon-settings font-red"></i>
                    <span class="caption-subject font-red sbold uppercase">Non-Compliance Events</span>
                </div>
                <div class="tools">
                </div>
            </div>
            <div class="portlet-body">
                <div class="row">
                </div>

            </div>
                <div class="portlet-body">
                <div class="row">
                    <div class="col-md-1">
                        <span>Company</span>
                    </div>
                    <div class="col-md-2">
                        <select id="slCompany">
                            <option value="0"></option>
                            <option value="ACME_Corp_Ltd">ACME Corp Ltd</option>
                            <option value="TCS">TCS</option>
                            <option value="Infi">Infosys</option>
                        </select>
                    </div>
                    <div class="col-md-1">
                        <span>From</span>
                    </div>
                    <div class="col-md-2">
                        <input id="txtFromDate" data-date-format="dd/mm/yyyy" type="text" value="1/10/2019" class="form-control date-picker" name="from_date" />
                    </div>
                    <div class="col-md-1">
                        <span>To</span>
                    </div>
                    <div class="col-md-2">
                        <input id="txtToDate" data-date-format="dd/mm/yyyy" type="text" value="1/10/2019" class="form-control date-picker" name="to_date" />
                    </div>

                    <%--<br /><br />--%>
                    <div class="col-md-3 pull-right">
                        <button style="margin-right:4px" class="pull-right">Excel</button>
                        <button style="margin-right:4px" class="pull-right">PDF</button>
                        <button style="margin-right:4px" class="pull-right">Go</button>
                    </div>
                </div>
                <br />
                <br />
                <div class="row">
                    <div class="col-md-6">
                        <div style="text-align: center"><b>Total NonCompliance</b></div>
                        <br />
                        <div id="chart_NonCompliance" class="chart" style="height: 200px;">
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div style="text-align: center"><b>NonCompliance Difference</b></div>
                        <br />
                        <div id="chart_Difference" class="chart" style="height: 200px;">
                        </div>
                    </div>
                </div>
                <br />
                <br />
                <div class="row">
                    <div style="text-align: center"><b>User Details range who have made above three non Compliance events</b></div>
                    <table class="table table-striped table-hover table-bordered" id="tbl-NonComplianceDetails-setup" style="margin-left: 16px;width: 1043px;">
                        <thead>
                            <tr>
                                <th>Name</th>
                                <th>Email Id</th>
                                <th>Role</th>
                                <th>Login Id</th>
                                <th>Company</th>
                            </tr>
                        </thead>
                        <tbody id="tbdNonComplianceDetailsList">
                            <tr>
                                <td>Akshay</td>
                                <td>akshay.jha@softeratechnologies.com</td>
                                <td>Non-Admin</td>
                                <td>Akshay124</td>
                                <td>ACME Corp Ltd</td>
                            </tr>
                            <tr>
                                <td>Sanjay</td>
                                <td>sanjay.singh@softeratechnologies.com</td>
                                <td>Non-Admin</td>
                                <td>Sanjay124</td>
                                <td>TCS</td>
                            </tr>
                            <tr>
                                <td>Abhishek</td>
                                <td>abhishekgupta@softeratechnologies.com</td>
                                <td>Non-Admin</td>
                                <td>Abhishek124</td>
                                <td>Infosys</td>
                            </tr>
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
    <script src="../assets/global/scripts/app.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js" type="text/javascript"></script>
    <script src="../assets/pages/scripts/components-date-time-pickers.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/select2/js/select2.min.js" type="text/javascript"></script>

    <script src="../assets/global/plugins/flot/jquery.flot.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/flot/jquery.flot.resize.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/flot/jquery.flot.categories.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/flot/jquery.flot.pie.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/flot/jquery.flot.stack.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/flot/jquery.flot.crosshair.min.js" type="text/javascript"></script>
    <script src="js/Global.js" type="text/javascript"></script>
    <script src="js/Non-ComplianceEvents.js" type="text/javascript"></script>
</asp:Content>
