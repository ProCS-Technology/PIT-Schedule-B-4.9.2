<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpsiGrpAuditLog.aspx.cs" Inherits="ProcsDLL.InsiderTrading.UpsiGrpAuditLog" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
        <meta http-equiv="X-UA-Compatible" content="IE=edge" />
        <meta content="width=device-width, initial-scale=1" name="viewport" />
        <link href="../assets/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
        <link href="../assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />
        <script src="../assets/global/plugins/jquery.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/js.cookie.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js" type="text/javascript"></script>
        <link href="../assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
        <link href="../assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
        <link href="../assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
        <link href="../assets/global/css/jquery-ui.css" rel="Stylesheet" type="text/css" />
    </head>
    <body>
        <form id="form1" runat="server">
            <div class=" panel-group accordion" id="accordionLogs">
                <div class="panel panel-default">
                    <div class="panel-heading" id="headingGroup">
                        <button class="btn btn-link" type="button" data-toggle="collapse" data-target="#collapseGroup" aria-expanded="true" aria-controls="collapseGroup">
                            Group
                        </button>
                    </div>
                    <div class="panel-body collapse" id="collapseGroup" aria-labelledby="headingGroup" data-parent="#accordionLogs">
                        <table class="table table-hover table-bordered logtable">
                            <thead>
                                <tr>
                                    <th>No.</th>
                                    <th>Action</th>
                                    <th>Group Name</th>
                                    <th>Group #</th>
                                    <th>Valid From</th>
                                    <th>Valid To</th>
                                    <th>Status</th>
                                    <th>Created/Modified On</th>
                                    <th>Created/Modified By</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RepeaterUPSIGroup" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Container.ItemIndex + 1 %></td>
                                            <td><%#Eval("command") %></td>
                                            <td><%#Eval("GRP_NAME") %></td>
                                            <td><%#Eval("GRP_NO") %></td>
                                            <td><%#Eval("VALID_FROM", "{0:dd-MMM-yyyy}") %></td>
                                            <td><%#Eval("VALID_TO", "{0:dd-MMM-yyyy}") %></td>
                                            <td><%#Eval("GRP_STATUS") %></td>
                                            <td><%#Eval("CREATED_ON", "{0:dd-MMM-yyyy HH:mm:ss}") %></td>
                                            <td><%#Eval("CREATED_BY") %> (<%#Eval("USER_NM") %>)</td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="panel panel-default">
                    <div class="panel-heading" id="headingMembers">
                        <button class="btn btn-link" type="button" data-toggle="collapse" data-target="#collapseMembers" aria-expanded="true" aria-controls="collapseMembers">
                            Designated Members
                        </button>
                    </div>
                    <div class="panel-body collapse" id="collapseMembers" aria-labelledby="headingMembers" data-parent="#accordionLogs">
                        <table class="table table-hover table-bordered logtable">
                            <thead>
                                <tr>
                                    <th>No.</th>
                                    <th>Action</th>
                                    <th>User</th>
                                    <th>Membership</th>
                                    <th>Created/Modified On</th>
                                    <th>Created/Modified By</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RepeaterMember" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Container.ItemIndex + 1 %></td>
                                            <td><%#Eval("command") %></td>
                                            <td><%#Eval("User_Login") %></td>
                                            <td><%#Eval("GRP_OWNER") %></td>
                                            <td><%#Eval("CREATED_ON", "{0:dd-MMM-yyyy HH:mm:ss}") %></td>
                                            <td><%#Eval("CREATED_BY") %> (<%#Eval("USER_NM") %>)</td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="panel panel-default">
                    <div class="panel-heading" id="headingCp">
                        <button class="btn btn-link" type="button" data-toggle="collapse" data-target="#collapseCp" aria-expanded="true" aria-controls="collapseCp">
                            Connected Person
                        </button>
                    </div>
                    <div class="panel-body collapse" id="collapseCp" aria-labelledby="headingCp" data-parent="#accordionLogs">
                        <table class="table table-hover table-bordered logtable">
                            <thead>
                                <tr>
                                    <th>No.</th>
                                    <th>Action</th>
                                    <th>Name</th>
                                    <th>Firm Name</th>
                                    <th>Email</th>
                                    <th>Ident. Type</th>
                                    <th>Ident. No</th>
                                    <th>Status</th>
                                    <th>Created/Modified On</th>
                                    <th>Created/Modified By</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RepeaterCp" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Container.ItemIndex + 1 %></td>
                                            <td><%#Eval("command") %></td>
                                            <td><%#Eval("CP_NAME") %></td>
                                            <td><%#Eval("Firm") %></td>
                                            <td><%#Eval("CP_EMAIL") %></td>
                                            <td><%#Eval("CP_IDENTIFICATION_TYPE") %></td>
                                            <td><%#Eval("CP_IDENTIFICATION_NO") %></td>
                                            <td><%#Eval("CP_STATUS") %></td>
                                            <td><%#Eval("CREATED_ON", "{0:dd-MMM-yyyy HH:mm:ss}") %></td>
                                            <td><%#Eval("CREATED_BY") %> (<%#Eval("USER_NM") %>)</td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </div>
                <%--<div class="panel panel-default">
                    <div class="panel-heading" id="headingCommunication">
                        <button class="btn btn-link" type="button" data-toggle="collapse" data-target="#collapseCommunication" aria-expanded="true" aria-controls="collapseCommunication">
                            UPSI
                        </button>
                    </div>
                    <div class="panel-body collapse" id="collapseCommunication" aria-labelledby="headingCommunication" data-parent="#accordionLogs">
                        <table class="table table-hover table-bordered logtable">
                            <thead>
                                <tr>
                                    <th>No.</th>
                                    <th>Action</th>
                                    <th>From</th>
                                    <th>To/CC/BCC</th>
                                    <th>Recipient Name</th>
                                    <th>Recipient Firm</th>
                                    <th>CC</th>
                                    <th>Bcc</th>
                                    <th>Message</th>
                                    <th>Mode</th>
                                    <th>Comm. Date</th>
                                    <th>Created On</th>
                                    <th>Created By</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RepeaterCommunication" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Container.ItemIndex + 1 %></td>
                                            <td><%#Eval("command") %></td>
                                            <td><%#Eval("COMM_FROM") %> (<%#Eval("COMM_FROM_NAME") %>)</td>
                                            <td><%#Eval("COMM_TO") %></td>
                                            <td><%#Eval("RECIPIENT_NAME") %></td>
                                            <td><%#Eval("FIRM") %></td>
                                            <td><%#Eval("COMM_CC") %></td>
                                            <td><%#Eval("COMM_BCC") %></td>
                                            <td><%#Eval("COMM_MSG") %></td>
                                            <td><%#Eval("COMM_MODE") %></td>
                                            <td><%#Eval("COMM_DATE", "{0:dd-MMM-yyyy HH:mm}")%></td>
                                            <td><%#Eval("CREATED_ON","{0:dd-MMM-yyyy HH:mm:ss}") %></td>
                                            <td><%#Eval("CREATED_BY") %> (<%#Eval("USER_NM") %>)</td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </div>--%>
            </div>
        </form>
        <script src="../assets/editor/jquery-te-1.4.0.min.js"></script>
        <script src="../assets/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js"></script>
        <script src="../assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.js"></script>
        <script src="../assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
        <script src="../assets/global/plugins/bootstrap-timepicker/js/bootstrap-timepicker.min.js"></script>
        <script src="../assets/pages/scripts/components-date-time-pickers.min.js" type="text/javascript"></script>
        <script src="../assets/global/scripts/datatable.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/datatables/datatables.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/jquery-validation/js/jquery.validate.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/jquery-validation/js/additional-methods.min.js" type="text/javascript"></script>
        <script src="../assets/global/scripts/jquery-ui.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/js.cookie.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js" type="text/javascript"></script>
        <script src="js/Global.js?<%=DateTime.Now %>" type="text/javascript"></script>
        <script>
            $('.logtable').DataTable({
                dom: 'Bfrtip',
                "searching": false
            });
        </script>
    </body>
</html>