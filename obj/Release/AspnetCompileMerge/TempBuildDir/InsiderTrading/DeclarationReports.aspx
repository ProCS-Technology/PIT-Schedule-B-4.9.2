<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" CodeBehind="DeclarationReports.aspx.cs" Inherits="ProcsDLL.InsiderTrading.DeclarationReports" %>

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
            min-height: 1212px !important;
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
        <div class="portlet light portlet-fit ">
            <div class="portlet-title">
                <div class="caption">
                    <i class="icon-settings font-red"></i>
                    <span class="caption-subject font-red sbold uppercase">Declaration Reports</span>
                </div>
                <div class="tools">
                </div>
            </div>
            <%--            <div class="portlet-body">
                <div class="row">
                </div>
            </div>--%>
            <div class="portlet-body">
                <div class="row">

                    <div class="col-md-3 col-lg-3">
                        <label for="Company" style="text-align: center; display: block;">Company</label>
                        <select class="form-control select2" name="Company" id="bindBusinessUnit">
                        </select>
                    </div>
                    <div class="col-md-3 col-lg-3">
                        <label for="fromDate" style="text-align: center; display: block;">From</label>
                        <input id="txtFromDate" data-date-format="dd/mm/yyyy" type="text" class="form-control date-picker" name="from_date" autocomplete="off"/>
                    </div>
                    <div class="col-md-3 col-lg-3">
                        <label for="toDate" style="text-align: center; display: block;">To</label>
                        <input id="txtToDate" data-date-format="dd/mm/yyyy" type="text" class="form-control date-picker" name="to_date" autocomplete="off"/>
                    </div>

                    <div class="col-md-3 pull-right">
                        <button style="background-color: lightgreen; margin-top: 25px" class="btn" onclick="javascript:fnGetDeclarationReportsBetweenDates();">Go</button>
                    </div>
                </div>
                <br />
                <br />
                <div class="row">
                    <div class="col-md-6">
                        <div style="text-align: center; margin-left: 380px"><b>Total Users</b></div>
                        <table class="table table-striped table-hover table-bordered">
                            <thead>
                                <tr>
                                    <th>
                                        Total Users
                                    </th>
                                    <th>
                                        User Made Declaration   
                                    </th>
                                    <th>
                                        User Not Made Declaration   
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td id="tdTotalUsers">

                                    </td>
                                    <td id="tdUserMadeDeclaration">

                                    </td>
                                    <td id="tdUserNotMadeDeclaration">

                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <br />
                        <div id="chart_Declarations" class="chart" style="height: 200px; margin-left: 250px">
                        </div>
                    </div>
                </div>
                <br />
                <br />
                <div class="row">
                    <div style="text-align: center"><b>User Details who have made/not made their declaration</b></div>
                    <br />
                    <br />
                    <a href="#usersMadeDeclaration" onclick="javascript:fnUserMadeDeclarationList()" class="btn btn-success" data-toggle="modal">User Made Declaration List</a>
                    <a href="#usersNotMadeDeclaration" onclick="javascript:fnUserNotMadeDeclarationList()" class="btn btn-danger" data-toggle="modal">User Not Made Declaration List</a>
                    <div class="table-responsive">
                    <table class="table table-striped table-hover table-bordered" id="tbl-DeclarationDetails-setup">
                        <thead>
                            <tr>
                                <th>Name</th>
                                <th class="display-none">Email Id</th>
                                <th class="display-none">Role</th>
                                <th>Department</th>
                                <th>Designation</th>
                                <th>PAN</th>
                                <th>Mobile</th>
                                <th>Login Id</th>
                                <th class="display-none">Declaration Start Date</th>
                                <th>Declaration Submission Date</th>
                                <th>Due Date</th>
                            </tr>
                        </thead>
                        <tbody id="tbdDeclarationDetailsList">
                        </tbody>
                    </table>
                        </div>
                </div>
            </div>
            <!-- END Portlet PORTLET-->
        </div>
    </div>

    <div id="usersMadeDeclaration" class="modal fade">
        <div class="modal-dialog modal-dialog-centered" style="width: 80% !important;">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Users Made Declaration</h5>

                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>

                <div class="modal-body p-20">
                    <table class="table" id="tbl-UsersMadeDeclaration-setup">
                        <thead class="blue-header">
                            <tr>
                                <th style="width: 10%">S.No.</th>
                                <th style="width: 20%">Name</th>
                                <th style="width: 20%">Email</th>
                                <th style="width: 20%">Login Id</th>                                
                                <th style="width:15%">Department</th>
                                <th style="width:15%">Designation</th>
                                <th style="width:15%">PAN</th>
                                <th style="width:15%">Mobile</th>
                            </tr>
                        </thead>
                        <tbody id="tbodyUsersMadeDeclarationBody">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>

    <div id="usersNotMadeDeclaration" class="modal fade">
        <div class="modal-dialog modal-dialog-centered" style="width: 80% !important;">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Users Not Made Declaration</h5>

                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>

                <div class="modal-body p-20">
                    <table class="table" id="tbl-UsersNotMadeDeclaration-setup">
                        <thead class="blue-header">
                            <tr>
                                <th style="width: 10%">S.No.</th>
                                <th style="width: 20%">Name</th>
                                <th style="width: 20%">Email</th>
                                <th style="width: 20%">Login Id</th>                                
                                <th style="width:15%">Department</th>
                                <th style="width:15%">Designation</th>
                                <th style="width:15%">PAN</th>
                                <th style="width:15%">Mobile</th>
                            </tr>
                        </thead>
                        <tbody id="tbodyUsersNotMadeDeclarationBody">
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
    <script src="js/Global.js?<%=DateTime.Now %>" type="text/javascript"></script>
    <script src="js/DeclarationReports.js?<%=DateTime.Now %>" type="text/javascript"></script>
</asp:Content>
