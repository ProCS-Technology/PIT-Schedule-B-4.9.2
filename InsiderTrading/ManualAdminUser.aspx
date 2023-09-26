<%@ Page Title="" Language="C#" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" AutoEventWireup="true" CodeBehind="ManualAdminUser.aspx.cs" Inherits="ProcsDLL.InsiderTrading.ManualAdminUser" %>
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
    <link href="../assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/select2/css/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/select2/css/select2-bootstrap.min.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-content-inner">
        <div class="col-md-12">
            <div class="portlet light portlet-fit " style="height: 555px; margin-left: -15px; margin-right: -15px;">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="icon-settings font-red"></i>
                        <span class="caption-subject font-red sbold uppercase">Manual Document Upload</span>
                    </div>
                </div>
                <div class="portlet-body">
                    <div class="table-toolbar">
                        <div class="row">
                            <div class="caption-subject font-blue sbold uppercase col-md-4">Upload User Manual </div>
                            <div class="col-md-8">
                                <input id="fileUser" type="file" />
                                <br />
                                <a href="#" id="uploadedPolicyDocument1" class="fa fa-download" target="_blank"></a>
                                <input id="txtPolicyKey1" type="hidden" />
                            </div>
                            <br />
                            <br />
                            <br />
                            <br />
                            <div class="col-md-offset-3 col-md-9">
                                <button id="btnSave1" type="button" data-dismiss="modal" class="btn green" onclick="javascript:fnSaveUserManual();">Upload</button>
                            </div>
                        </div><br /><br /><br />



                         <div class="row">
                            <div class="caption-subject font-blue sbold uppercase col-md-4">Upload Admin Manual </div>
                            <div class="col-md-8">
                                <input id="fileAdmin" type="file" />
                                <br />
                                <a href="#" id="uploadedPolicyDocument" class="fa fa-download" target="_blank"></a>
                                <input id="txtPolicyKey" type="hidden" />
                            </div>
                            <br />
                            <br />
                            <br />
                            <br />
                            <div class="col-md-offset-3 col-md-9">
                                <button id="btnSave" type="button" data-dismiss="modal" class="btn green" onclick="javascript:fnSaveAdminManual();">Upload</button>
                            </div>
                        </div>

                    </div>
                   
                </div>
            </div>
        </div>
    </div>

    <%--Start Datetime--%>
    <%-- <script src="../assets/editor/jquery-te-1.4.0.min.js"></script>--%>
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
    <script src="js/Global.js"></script>
    <script src="js/ManualAdminUser.js"></script>
</asp:Content>
