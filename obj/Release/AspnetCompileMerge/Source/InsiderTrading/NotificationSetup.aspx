<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" CodeBehind="NotificationSetup.aspx.cs" Inherits="ProcsDLL.InsiderTrading.NotificationSetup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../assets/global/css/components.min.css" rel="stylesheet" />
    <link href="../assets/global/css/plugins.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/css/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <!-- BEGIN THEME LAYOUT STYLES -->
    <link href="../assets/layouts/layout/css/layout.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/layouts/layout/css/themes/darkblue.min.css" rel="stylesheet" type="text/css" id="style_color" />
    <link href="../assets/layouts/layout/css/custom.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/select2/css/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/select2/css/select2-bootstrap.min.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .requied {
            color: red;
        }
        .required-red {
            border-color: red !important;
        }
        .required-red-border {
            color: red !important;
            border: 1px solid red;
            border-color: red !important;
        }
        .select2-container--default.select2-container--focus .select2-selection--multiple {
            border: 1px solid red!important;
        }
        select {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="portlet light bordered">
        <div class="portlet-title">
            <div class="caption font-red-sunglo">
                <i class="icon-settings font-red-sunglo"></i>
                <span class="caption-subject bold uppercase">Notification Reminder Set Up</span>
            </div>
        </div>
        <div class="portlet-body">
            <div class="table-toolbar">
                <div class="row">
                    <div class="col-md-6">
                        <div class="btn-group-devided">
                            <button id="btnAddReminder" class="btn green">
                                Add New Reminder
                            </button>&nbsp;
                        </div>
                    </div>
                </div>
            </div>
            <table class="table table-striped table-hover table-bordered" id="tbl-reminder-setup">
                <thead>
                    <tr>
                        <th>Module</th>
                        <th>Reminder For</th>
                        <th>Frequency</th>
                        <th>Status</th>
                        <th>Action</th>
                    </tr>
                </thead>
                <tbody id="tbdreminderlist"></tbody>
            </table>
        </div>

        <div class="modal fade" id="stack1" tabindex="-1" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-dialog-centered" style="width:50% !important";>
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title caption-subject bold uppercase font-red-sunglo">Add Reminder</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnCloseModal();"></button>
                    </div>
                    <div class="modal-body">
                        <div class="portlet light bordered">
                            <div class="portlet-body form">
                                <form class="form-horizontal" runat="server" role="form">
                                    <div class="form-body modal-fixheight" id="">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label" id="lblreminderName" style="text-align:left"><b>Reminder</b></label>
                                            <div class="col-md-6">
                                                <input type="text" class="form-control form-control-inline" id="reminderName" onchange="removRequried(this)" />
                                                <input type="text" class="from-control" id="reminderID"  style="display:none"/>
                                            </div>
                                        </div><br />
                                        <div class="form-group">
                                            <label class="col-md-3 control-label" id="lbltypeofReminder"><b>Reminder Type</b></label>
                                            <div class="col-md-6">
                                                <select id="typeofReminder" class="form-control form-control-inline" onchange="removRequried(this)">
                                                    <option value="0">Select</option>
                                                    <option value="1">One Time</option>
                                                    <option value="2">Days</option>
                                                    <option value="3">Hours</option>
                                                </select>
                                            </div>
                                        </div><br />
                                        <div class="form-group">
                                            <label class="col-md-3 control-label" id="lbltypeValue"><b>Reminder Value</b></label>
                                            <div class="col-md-6">
                                                <input type="text" class="form-control form-control-inline" id="typeValue" onchange="removRequried(this)"/>
                                            </div>
                                        </div>
                                    </div>
                                </form>
                            </div>
                            <div class="form-actions">
                                <div class="row" style="text-align: center">
                                    <div class="col-md-offset-4 col-md-12">
                                        <button id="btnSave" type="button" class="btn green" onclick="javascript:fnSaveupsi();">Save UPSI</button>
                                        <button id="btnCancel1" type="button" data-dismiss="modal" class="btn default" onclick="javascript:fnCloseModal();">Cancel</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <script src="../assets/editor/jquery-te-1.4.0.min.js"></script>
        <script src="../assets/global/scripts/datatable.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/datatables/datatables.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/jquery-validation/js/jquery.validate.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/jquery-validation/js/additional-methods.min.js" type="text/javascript"></script>
        <script src="../assets/global/scripts/jquery-ui.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/js.cookie.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/select2/js/select2.full.min.js" type="text/javascript"></script>
        <script src="../assets/pages/scripts/components-select2.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/jquery-validation/js/jquery.validate.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/jquery-validation/js/additional-methods.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/bootstrap-wizard/jquery.bootstrap.wizard.min.js" type="text/javascript"></script>
        <script src="js/Global.js?<%=DateTime.Now %>" type="text/javascript"></script>
        <script src="js/NotificationSetUp.js?<%=DateTime.Now %>" type="text/javascript"></script>
        <script type="text/javascript">
            $("#Loader").hide();
        </script>
    </div>
</asp:Content>