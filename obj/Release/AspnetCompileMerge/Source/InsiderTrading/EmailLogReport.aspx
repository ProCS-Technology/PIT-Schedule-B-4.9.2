<%@ Page Title="" Language="C#" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" AutoEventWireup="true" CodeBehind="EmailLogReport.aspx.cs" Inherits="ProcsDLL.InsiderTrading.EmailLogReport" %>


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
        <asp:HiddenField ID="HiddenShowModal" runat="server" />
        <div class="col-md-12">
            <!-- BEGIN Portlet PORTLET-->
            <div class="portlet light portlet-fit ">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="icon-settings font-red"></i>
                        <span class="caption-subject font-red sbold uppercase">Email Log Report</span>
                    </div>
                </div>

                <div class="portlet-body">
                    <div class="row">

                        <div class="col-md-3 col-lg-3">
                            <label for="ddlModule" style="text-align: center; display: block;">Module</label>
                            <asp:DropDownList ID="DropDownListModule" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListModule_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="col-md-3 col-lg-3">
                            <label for="ddlModuleSubType" style="text-align: center; display: block;">Module Type</label>
                            <select class="form-control" id="ddlModuleSubType" runat="server"></select>
                        </div>
                        <div class="col-md-2 col-lg-2">
                            <label for="txtFromDate" style="text-align: center; display: block;">From Date</label>
                            <input id="txtFromDate" data-date-format="dd/mm/yyyy" type="text" class="form-control date-picker" runat="server" autocomplete="off" />
                        </div>
                        <div class="col-md-2 col-lg-2">
                            <label for="txtToDate" style="text-align: center; display: block;">To Date</label>
                            <input id="txtToDate" data-date-format="dd/mm/yyyy" type="text" class="form-control date-picker" runat="server" autocomplete="off" />
                        </div>
                        <div class="col-md-2 col-lg-2">
                            <asp:Button ID="ButtonSearch" runat="server" CssClass="btn btn-primary m-t-25" Text="Run" OnClick="ButtonSearch_Click" />
                        </div>
                    </div>
                    <br />
                    <br />
                    <br />
                    <br />
                    <div class="row">
                        <table class="table table-hover table-bordered" id="tblReport" style="width:100%;">
                            <thead>
                                <tr>
                                    <th>#</th>
                                    <th>Event</th>
                                    <th>Subject</th>
                                    <th>Sent By</th>
                                    <th>Email To</th>
                                    <th>Email Date</th>
                                    <th>Status</th>
                                    <th></th>
                                </tr>
                            </thead>
                            <!--
                                LOG_ID			EMAIL_MSG			DATA_ELEMENT_ID		ERR_MSG	USER_LOGIN
                                -->
                            <tbody id="tbdReport">
                                <asp:Repeater ID="RepeaterTbl" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Container.ItemIndex + 1 %></td>
                                            <td><%#Eval("EMAIL_ACTION") %></td>
                                            <td><%#Eval("EMAIL_SUBJECT") %></td>
                                            <td><%#Eval("USER_LOGIN") %></td>
                                            <td><%#Eval("EMAIL_TO") %></td>
                                            <td><%#Eval("EMAIL_DT", "{0:dd/MM/yyyy HH:mm:ss}") %></td>
                                            <td><%#Eval("EMAIL_STATUS") %></td>
                                            <td>
                                                <%--<a href='EmailLogReport.aspx?LogId=<%#Eval("LOG_ID") %>'>View Message</a>--%>
                                                <asp:HiddenField ID="HiddenFieldLogId" runat="server" Value='<%#Eval("LOG_ID") %>' />
                                                <asp:LinkButton ID="LinkButtonLogDetail" runat="server" OnClick="LinkButtonLogDetail_Click">View Message</asp:LinkButton>

                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </div>

                <!-- END Portlet PORTLET-->
            </div>
        </div>

        <div class="modal fade" id="ModalFullAuditLog" tabindex="-1" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-dialog-centered modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title caption-subject bold uppercase font-red-sunglo">Email Message</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnCloseLogModal();"></button>
                    </div>
                    <div class="modal-body">
                        <div class="portlet light bordered">
                            <div class="portlet-body form">
                                <div class="form-body">
                                    <div id="dvMsg" runat="server"></div>

                                    <asp:Repeater ID="RepeaterAttachment" runat="server">
                                        <ItemTemplate>
                                            <p>
                                                <a href='<%#Eval("EMAIL_ATTACHMENT") %>' download="" class='<%#Eval("EMAIL_ATTACHMENT").ToString().Length > 0 ? "display-block" : "display-none" %>'>Download Attachment</a>
                                            </p>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                </div>
                            </div>

                        </div>
                        
                    </div>
                    <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn red" onclick="javascript:fnCloseLogModal();">Close</button>
                        </div>
                </div>
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
    <script>
        $(document).ready(function () {
            $("#Loader").hide();
            if ($("input[id*='HiddenShowModal']").val() == "YES") {
                $('#ModalFullAuditLog').modal('show');
            }
            //$("#bindUser").select2({
            //    placeholder: "Select a user"
            //});
            initializeDataTable();
            
        });
        function initializeDataTable() {
            $('#tblReport').DataTable({
                dom: 'Bfrtip',
                pageLength: 10,
                "scrollY": "300px",
                "scrollX": true,
                //"aaSorting": [[0, "desc"]],
                buttons: [
                    {
                        extend: 'pdf',
                        className: 'btn green btn-outline',

                    },
                    {
                        extend: 'excel',
                        className: 'btn yellow btn-outline ',

                    },

                ]
            });
        }
        function fnalert() {
            alert("Please enter required field.");
        }
        
    </script>
</asp:Content>
