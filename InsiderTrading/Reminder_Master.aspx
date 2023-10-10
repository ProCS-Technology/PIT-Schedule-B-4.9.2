<%@ Page Title="" Language="C#" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" AutoEventWireup="true" CodeBehind="Reminder_Master.aspx.cs" Inherits="ProcsDLL.InsiderTrading.Reminder_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
    <link href="../assets/global/plugins/select2/css/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/select2/css/select2-bootstrap.min.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .requied {
            color: red;
        }

        .TFtable {
            width: 100%;
            border-collapse: collapse;
        }

            .TFtable th {
                padding: 7px;
                border: #dddddd 1px solid;
            }

            .TFtable td {
                padding: 7px;
                border: #dddddd 1px solid;
            }

            .TFtable tr {
                background: #f2f4f7;
                font-size: 12px;
            }

                .TFtable tr:nth-child(odd) {
                    background: #f2f4f7;
                }

                .TFtable tr:nth-child(even) {
                    background: #fff;
                }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="Form1" runat="server">
        <%--Added by Jiten--%>
        <asp:ScriptManager ID="scriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="updatepanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:TextBox ID="hdnMailType" runat="server" Text="" style="display:none;" />
                <asp:TextBox ID="hdnEmailSubject" runat="server" Text="" style="display:none;" />
                <asp:TextBox ID="hdnMailBody" runat="server" Text="" style="display:none;" />
                <asp:TextBox ID="hdnUsers" runat="server" Text="" style="display:none;" />
                <asp:TextBox ID="hdnEmailTask" runat="server" style="display:none;" />

                <div class="row" style="margin-left: 0px!important; margin-right: 0px !important;">
                    <div class="col-md-12">
                        <div class="portlet light portlet-fit">
                            <div class="portlet-title" style="margin-bottom: 45px">
                                <div class="caption">
                                    <i class="icon-settings font-red"></i>
                                    <span class="caption-subject font-red sbold uppercase">Reminder Setup</span>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-md-4 col-lg-4" for="Status" style="right: -50px;">TYPE</label>
                                    <select class="form-control" id="bindTYPE" style="margin: 0px; height: 30px; width: 358px; left: 10px">
                                        <option value="">Select Type</option>
                                        <option value="1">WELCOME</option>
                                        <option value="2">DECLARATION</option>
                                        <option value="3">OTHER</option>
                                    </select>
                                </div>
                            </div>
                            <br />
                            <br />
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-md-4 col-lg-4" style="right: -50px;">
                                        SUBJECT
                                    </label>
                                    <div class="col-md-4">
                                        <input id="txtSUBJECT" class="form-control" name="SUBJECT" />
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-md-4 col-lg-4" style="right: -50px;">
                                        User
                                    </label>
                                    <div class="col-md-4">
                                        <select id="bindUser" class="form-control select2-multiple" multiple="multiple"></select>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-md-4 col-lg-4" style="right: -50px;">Mail Template</label>
                                    <div class="col-md-4">
                                        <textarea id="TextArea1" cols="40" rows="2" style="margin: 0px; height: 187px; width: 610px;"></textarea>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <br />
                            <div class="text-center" style="left: inherit">
                                <div class="form-group">
                                   <%-- <input type="button" value="Send" id="btnsend_reminders" onclick="javascript:fnSendReminderEmail();" style="margin: 0px; height: 30px; width: 80px;" />--%>
                                   <asp:Button ID="btnsend_reminders" Text="Send" runat="server" OnClientClick="javascript:fnSendReminderEmail();" onclick="btnsend_reminders_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnsend_reminders" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
    <script src="../assets/editor/jquery-te-1.4.0.min.js"></script>
    <script src="../assets/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js"></script>
    <script src="../assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.js"></script>
    <script src="../assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
    <script src="../assets/global/plugins/bootstrap-timepicker/js/bootstrap-timepicker.min.js"></script>
    <script src="../assets/pages/scripts/components-date-time-pickers.min.js" type="text/javascript"></script>
    <script src="../assets/global/scripts/datatable.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/datatables/datatables.min.js" type="text/javascript"></script>
    <script src="../assets/pages/scripts/components-date-time-pickers.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/select2/js/select2.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-multiselect/js/bootstrap-multiselect.js" type="text/javascript"></script>
    <script src="../assets/pages/scripts/components-bootstrap-multiselect.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/select2/js/select2.full.min.js" type="text/javascript"></script>
    <script src="../assets/pages/scripts/components-select2.min.js" type="text/javascript"></script>
    <script src="js/Global.js?<%=DateTime.Now %>" type="text/javascript"></script>
    <script src="../assets/global/plugins/select2/js/select2.min.js" type="text/javascript"></script>
    <link href="../assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/select2/css/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/select2/css/select2-bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/bootstrap-multiselect/css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
    <script src="../assets/pages/scripts/components-select2.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/select2/js/select2.full.min.js" type="text/javascript"></script>
    <script src="js/Global.js" type="text/javascript"></script>
    <script src="js/Reminder.js" type="text/javascript"></script>
</asp:Content>
