<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" CodeBehind="BenposReport.aspx.cs" Inherits="ProcsDLL.InsiderTrading.BenposReport" %>

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

        td.details-control {
            background: url('images/icons/details_open.png') no-repeat center center;
            cursor: pointer;
        }

       tr.details td.details-control {
            background: url('images/icons/details_close.png') no-repeat center center;
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
                    <span class="caption-subject font-red sbold uppercase">Benpos Report</span>
                </div>
            </div>

            <div class="portlet-body">
                <div class="row">
                    <div class="col-md-4 col-lg-4">
                        <label for="User" style="text-align: center; display: block;">Benpos Upload Date</label>
                        <select class="form-control" name="User" id="bindBenposUploadedOn">
                        </select>
                    </div>
                    <div class="col-md-2 col-lg-2">
                        <input id="txtGetTadingReport" type="button" style="background-color: limegreen;margin-top:23px;" class="form-control" value="Run" onclick="fnGetBenposReport();" name="to_date" />
                    </div>
                </div>
                <br />
                <br />
                <br />
                <br />
                <div class="row">
                    <table class="table table-striped display text-nowrap table-bordered" id="tbl-benposReport-setup" style="width:100%">
                        <thead>
                            <tr>
                                <th style="min-width:10px!important;"></th>
                                <th>Login Id</th>
                                <th>Name</th>
                                <th>PAN</th>
                                <th>Relative</th>
                                <th>Relation</th>
                                <%--<th>Folio No</th>--%>
                                <th>Holding At Declaration</th>
                                <th>Holding As On Date</th>
                                <th>Benpos Uploaded Date</th>
                                <th>Trade Quantity</th>
                                <th>Is Non-Compliant ?</th>
                                <th>Non-Compliant Type</th>
                            </tr>
                        </thead>
                        <tbody id="tbdBenposReportList">
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
    <script src="../assets/global/plugins/select2/js/select2.min.js" type="text/javascript"></script>

    <script src="../assets/global/plugins/flot/jquery.flot.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/flot/jquery.flot.resize.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/flot/jquery.flot.categories.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/flot/jquery.flot.pie.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/flot/jquery.flot.stack.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/flot/jquery.flot.crosshair.min.js" type="text/javascript"></script>
    <script src="js/Global.js" type="text/javascript"></script>
    <script src="js/BenposReport.js" type="text/javascript"></script>
</asp:Content>
