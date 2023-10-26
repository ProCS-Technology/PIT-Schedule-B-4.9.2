<%@ Page Title="" Language="C#" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" AutoEventWireup="true" CodeBehind="UpsiGrpAuditLogRpt.aspx.cs" Inherits="ProcsDLL.InsiderTrading.UpsiGrpAuditLogRpt" %>


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

        .bg-gray {
            background: #eef1f5 !important;
        }

        .m-t-25 {
            margin-top: 25px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="form1" runat="server">
        <div class="col-md-12">
            <!-- BEGIN Portlet PORTLET-->
            <div class="portlet light portlet-fit ">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="icon-settings font-red"></i>
                        <span class="caption-subject font-red sbold uppercase">UPSI Audit Log Report</span>
                    </div>
                </div>

                <div class="portlet-body">
                    <div class="row">
                        <div class="col-md-3 col-lg-3">
                            <label for="User" style="text-align: center; display: block;">Name of the UPSI Project</label>
                            <asp:DropDownList ID="ddlUPSIGrp" CssClass="form-control select2" PlaceHolder="Select shared by" runat="server" OnSelectedIndexChanged="ddlUPSIGrp_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                          </div>
                            <div class="col-md-3 col-lg-3">
                                <label for="from_date" style="text-align: center; display: block;">From</label>
                                <input id="txtFromDate" data-date-format="dd/mm/yyyy" type="text" class="form-control date-picker" runat="server" name="from_date" autocomplete="off" />
                            </div>
                            <div class="col-md-3 col-lg-3">
                                <label for="to_date" style="text-align: center; display: block;">To</label>
                                <input id="txtToDate" data-date-format="dd/mm/yyyy" type="text" class="form-control date-picker" runat="server" name="to_date" autocomplete="off" />
                            </div>
                            <div class="col-md-2 col-lg-2">
                            <asp:Button runat="server" ID="btnLogin" style="background-color:limegreen;margin-top:24px;" CssClass="form-control" Text="Run"
                                 OnClick="btnLogin_Click" />
                        </div>
                        

                    </div>
                    <br />

                    <br />
                    <div>
                        <div class=" panel-group accordion" id="accordionLogs">
                            <div class="panel panel-default">
                                <div class="panel-heading" id="headingGroup">
                                    <button class="btn btn-link" type="button" data-toggle="collapse" data-target="#collapseGroup" aria-expanded="true" aria-controls="collapseGroup">
                                        Group
                                    </button>
                                </div>
                                <div class="panel-body collapse" id="collapseGroup" aria-labelledby="headingGroup" data-parent="#accordionLogs" style="overflow-x: auto !important;">
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <table class="table table-hover table-bordered" id="logtable">
                                        <thead>
                                            <tr>
                                                <th>No.</th>
                                                <th>Action</th>
                                                <th>Group Name</th>
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
                                <div class="panel-body collapse" id="collapseMembers" aria-labelledby="headingMembers" data-parent="#accordionLogs" style="overflow-x: auto !important;">
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <table class="table table-hover table-bordered" id="logtable2">
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
                                <div class="panel-body collapse" id="collapseCp" aria-labelledby="headingCp" data-parent="#accordionLogs" style="overflow-x: auto !important;">
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <table class="table table-hover table-bordered" id="logtable3">
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
                            <div class="panel panel-default">
                                <div class="panel-heading" id="headingCommunication">
                                    <button class="btn btn-link" type="button" data-toggle="collapse" data-target="#collapseCommunication" aria-expanded="true" aria-controls="collapseCommunication">
                                        UPSI
                                    </button>
                                </div>
                                <div class="panel-body collapse" id="collapseCommunication" aria-labelledby="headingCommunication" data-parent="#accordionLogs" style="overflow-x: auto !important;">
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <table class="table table-hover table-bordered" id="logtable4">
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
                                                <%--<th>Desc</th>--%>
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
                                                        <td><%#Eval("CREATED_ON", "{0:dd-MMM-yyyy HH:mm:ss}") %></td>
                                                        <td><%#Eval("CREATED_BY") %> (<%#Eval("USER_NM") %>)</td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- END Portlet PORTLET-->
            </div>
        </div>
    </form>

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

    <script type="text/javascript">
        $(document).ready(function () {
            $("#Loader").hide();

            $("select[id*='ddlUPSIGrp']").select2({
                placeholder: "Select UPSI Project Name"
            });
            //$('#example').DataTable();
            initializeDataTable();
            initializeDataTable2();
            initializeDataTable3();
            initializeDataTable4();
        });
        function initializeDataTable() {
            $('#logtable').DataTable({
                dom: 'Brtip',
                pageLength: 10,
                //"scrollY": "300px",
                //"scrollX": true,
                buttons: [
                    {
                        extend: 'pdfHtml5',
                        orientation: 'landscape',
                        //pageSize: 'TABLOID',
                        title: 'UPSI Audit Log Report - ' + $("select[id*=ddlUPSIGrp] option:selected").text(),
                        className: 'btn green btn-outline',
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6, 7]
                        }
                    },
                    {
                        extend: 'excel',
                        title: 'UPSI Audit Log Report - ' + $("select[id*=ddlUPSIGrp] option:selected").text(),
                        className: 'btn yellow btn-outline ',
                        columns: [0, 1, 2, 3, 4, 5, 6, 7]
                    },

                ]
            });
            // table.columns.adjust().draw();
        }

        function initializeDataTable2() {
            $('#logtable2').DataTable({
                dom: 'Brtip',
                //pageLength: 10,
                //"scrollY": "300px",
                //"scrollX": true,
                buttons: [
                    {
                        extend: 'pdfHtml5',
                        orientation: 'landscape',
                        //pageSize: 'TABLOID',
                        title: 'UPSI Audit Log Report - ' + $("select[id*=ddlUPSIGrp] option:selected").text(),
                        className: 'btn green btn-outline',
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5]
                        }
                    },
                    {
                        extend: 'excel',
                        title: 'UPSI Audit Log Report - ' + $("select[id*=ddlUPSIGrp] option:selected").text(),
                        className: 'btn yellow btn-outline ',
                        columns: [0, 1, 2, 3, 4, 5]
                    },

                ]
            });

        }
        function initializeDataTable3() {
            $('#logtable3').DataTable({
                dom: 'Brtip',
                pageLength: 10,

                buttons: [
                    {
                        extend: 'pdfHtml5',
                        orientation: 'landscape',
                        pageSize: 'TABLOID',
                        title: 'UPSI Audit Log Report - ' + $("select[id*=ddlUPSIGrp] option:selected").text(),
                        className: 'btn green btn-outline',
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]
                        }
                    },
                    {
                        extend: 'excel',
                        title: 'UPSI Audit Log Report - ' + $("select[id*=ddlUPSIGrp] option:selected").text(),
                        className: 'btn yellow btn-outline ',
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]
                    },

                ]
            });
        }
        function initializeDataTable4() {
            $('#logtable4').DataTable({
                dom: 'Brtip',
                pageLength: 10,

                buttons: [
                    {
                        extend: 'pdfHtml5',
                        orientation: 'landscape',
                        pageSize: 'TABLOID',
                        title: 'UPSI Audit Log Report - ' + $("select[id*=ddlUPSIGrp] option:selected").text(),
                        className: 'btn green btn-outline',
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12]
                        }
                    },
                    {
                        extend: 'excel',
                        title: 'UPSI Audit Log Report - ' + $("select[id*=ddlUPSIGrp] option:selected").text(),
                        className: 'btn yellow btn-outline ',
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12]
                    },

                ]
            });
        }

        function fnalert() {
            alert("Please enter required field.");
        }
    </script>
</asp:Content>
