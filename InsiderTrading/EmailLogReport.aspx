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
        <%--Added by Jiten--%>

        <asp:HiddenField ID="HiddenShowModal" runat="server" />
        <asp:ScriptManager ID="scriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="updatepanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:TextBox ID="hdnEmailTask" runat="server" Style="display: none;" />

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
                                <%--Added by jiten--%>
                                <div class="col-md-2 col-lg-2">
                                    <label style="text-align: center; display: block;">Email Status</label>
                                    <asp:DropDownList ID="ddlStatus" runat="server" class="form-control">
                                        <asp:ListItem Text="All" Value="All"></asp:ListItem>
                                        <asp:ListItem Text="Failed" Value="Failed"></asp:ListItem>
                                        <asp:ListItem Text="Success" Value="Success"></asp:ListItem>
                                    </asp:DropDownList>                                 
                                </div>

                                <div>
                                    <asp:Button ID="ButtonSearch" runat="server" CssClass="btn btn-primary m-t-25" Text="Run" OnClick="ButtonSearch_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnReSendMail" runat="server" CssClass="btn btn-primary m-t-25" Text="ReSend Mail" OnClick="btnReSendMail_Click" />
                                </div>
                                <div class="col-md-2 col-lg-2">
                                </div>
                            </div>
                            <br />
                            <br />
                            <br />
                            <br />
                            <div class="row">
                                <table class="table table-hover table-bordered" id="tblReport" style="width: 100%;">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>Event</th>
                                            <th>Subject</th>
                                            <th>Sent By</th>
                                            <th>Email To</th>
                                            <th>Email Date</th>
                                            <th>Status</th>
                                            <th style="display: none"></th>
                                            <th>
                                                <input type="checkbox" id="chkSelectAll" data-status="SelectAll" onclick="selectAllRows();" /></th>
                                            <th></th>
                                        </tr>
                                    </thead>
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
                                                    <td style="display: none"><%#Eval("LOG_ID") %></td>
                                                    <td>
                                                        <input type="checkbox" id="CheckBoxSelect" onclick="SelectAllRows2();" class="rowCheckbox" data-status='<%#Eval("EMAIL_STATUS") %>' data-log-id='<%#Eval("LOG_ID") %>' <%# (string)Eval("EMAIL_STATUS") == "Success" || (string)Eval("EMAIL_STATUS") == "Pending" ? "disabled='disabled'" : "" %> />
                                                    </td>
                                                    <td>
                                                        <asp:HiddenField ID="HiddenFieldLogId" runat="server" Value='<%#Eval("LOG_ID") %>' />
                                                        <asp:LinkButton ID="LinkButtonLogDetail" runat="server" OnClick="LinkButtonLogDetail_Click">View Message</asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                                <asp:HiddenField ID="HiddenFieldLogIds" runat="server" />
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
                                                <%--<a href='<%#Eval("EMAIL_ATTACHMENT") %>' download="" class='<%#Eval("EMAIL_ATTACHMENT").ToString().Length > 0 ? "display-block" : "display-none" %>'>Download Attachment</a>--%>
                                                <a download="" class='<%#Eval("EMAIL_ATTACHMENT").ToString().Length > 0 ? "display-block" : "display-none" %>' onclick="fnDownloadAttachment('<%#Eval("LOG_ID") %>' , '<%# GetFileExtension(Eval("EMAIL_ATTACHMENT").ToString()) %>' )">Download Attachment</a>
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

            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnReSendMail" />
            </Triggers>
        </asp:UpdatePanel>
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
    <script src="../assets/plugins/custom/ckeditor/ckeditor-mention.js" type="text/javascript"></script>
    <script src="js/Global.js?<%=DateTime.Now %>" type="text/javascript"></script>
    <script>
        $(document).ready(function () {
            debugger
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
                //pageLength: 10,
                "scrollY": "300px",
                "scrollX": true,
                //"aaSorting": [[0, "desc"]],
                buttons: ['pageLength',
                    {
                        extend: 'pdf',
                        className: 'btn green btn-outline',
                        exportOptions: {
                            columns: [1, 2, 3, 4, 5, 6, 7]
                        }
                    },
                    {
                        extend: 'excel',
                        className: 'btn yellow btn-outline ',
                        exportOptions: {
                            columns: [1, 2, 3, 4, 5, 6, 7]
                        }
                    },
                ],
                "lengthMenu": [250,500,1000,2000],
            });
        }

        function fnalert() {
            alert("Please enter required field.");
        }

       
        function selectAllRows() {
            var headerCheckbox = document.getElementById('chkSelectAll');
            var rowCheckboxes = document.getElementsByClassName('rowCheckbox');
            var selectedLogIds = []; // Array to store selected LOG_ID values

            for (var i = 0; i < rowCheckboxes.length; i++) {
                var dataStatus = rowCheckboxes[i].getAttribute('data-status');
                var logId = rowCheckboxes[i].getAttribute('data-log-id');

                if (headerCheckbox.checked && dataStatus === 'Failed') {
                    rowCheckboxes[i].checked = true;
                    selectedLogIds.push(logId); // Add the LOG_ID to the array
                } else {
                    rowCheckboxes[i].checked = false;
                    if (dataStatus === 'Success' || dataStatus === 'Pending') {
                        rowCheckboxes[i].disabled = true; // Disable "Success" status rows
                    } else {
                        rowCheckboxes[i].disabled = false; // Enable other rows
                    }
                }
            }
            // Set the selectedLogIds array as a comma-separated string
            var selectedLogIdsStr = selectedLogIds.join(',');
            // Set the value of the HiddenField control with the selectedLogIds
            var hiddenField = document.getElementById('<%= HiddenFieldLogIds.ClientID %>');
            if (hiddenField) {
                hiddenField.value = selectedLogIdsStr;
            }

        }

        function SelectAllRows2() {
            var rowCheckboxes = document.getElementsByClassName('rowCheckbox');
            var failedLogIds = []; // Array to store selected LOG_ID values with "Failed" status

            for (var i = 0; i < rowCheckboxes.length; i++) {
                var dataStatus = rowCheckboxes[i].getAttribute('data-status');
                var logId = rowCheckboxes[i].getAttribute('data-log-id');

                if (rowCheckboxes[i].checked && dataStatus === 'Failed') {
                    failedLogIds.push(logId); // Add the LOG_ID to the array
                }
            }
            var failedLogIds = failedLogIds.join(',');
            var hiddenField = document.getElementById('<%= HiddenFieldLogIds.ClientID %>');
            if (hiddenField) {
                hiddenField.value = failedLogIds;
            }

        }

     
        var downloadComplete = false;
        var intervalListener;

        var start = $("input[id*=hdnEmailTask]").val();
        if (start == "Start") {
            $("#LoaderProgerss").show();
            intervalListener = window.setInterval(function () {
                if (!downloadComplete) {
                    CallCheckEmailStatus();
                }
            }, 2000);
        }
        else if (start == "NullbyteFileError") {
            alert("Uploaded document contains nullbyte, please correct the name and try again.");
        }
        else if (start == "FileError") {
            alert("Please upload only pdf format");
        }


        function fnChkStatus() {
            var start = $("input[id*=hdnEmailTask]").val();
            if (start == "Start") {
                $("#LoaderProgerss").show();
                intervalListener = window.setInterval(function () {
                    if (!downloadComplete) {
                        CallCheckEmailStatus();
                    }
                }, 2000);
            }
        }
        function CallCheckEmailStatus() {
            $.ajax({
                type: "POST",
                url: "EmailLogReport.aspx/CheckDownload",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    updateStatus('completed', r.d);
                    if (r.d.indexOf('All emails sent') > -1) {
                        downloadComplete = true;
                    }
                },
                error: function (r) {
                    console.log('Check error : ' + r);
                },
                failure: function (r) {
                    console.log('Check failure : ' + r);
                }
            });
            if (downloadComplete) {
                window.clearInterval(intervalListener);
                $("input[id*=hdnEmailTask]").val('');
                $("#LoaderProgerss").hide();
            }
        }
        function updateStatus(status, msg) {
            document.getElementById('lblMsg').innerHTML = msg;
            if (msg.indexOf('All emails sent') > -1) {
                downloadComplete = true;
                window.clearInterval(intervalListener);
                $("input[id*=hdnEmailTask]").val('');
                $("#LoaderProgerss").hide();
                alert("All Email notification sent successfully");
            }
        }

        function fnDownloadAttachment(LOG_ID, FileExtension) {
            var webUrl = uri + "/api/EmailUpdation/CheckDownloadS?LOG_ID=" + LOG_ID + "&FileExtension=" + FileExtension;
            $.ajax({
                url: webUrl,
                type: 'POST',
                //headers: {
                //    Accept: "application/octet-stream; base64",
                //},
                success: function (data) {
                    if (FileExtension == '.xls') {
                        var uri = 'data:application/vnd.ms-excel;base64,' + data;
                        var link = document.createElement("a");
                        link.href = uri;
                        link.style = "visibility:hidden";
                        link.download = "ExcelReport.xls";

                    }
                    else if (FileExtension == '.xlsx') {
                        var uri = 'data:application/vnd.ms-excel;base64,' + data;
                        var link = document.createElement("a");
                        link.href = uri;
                        link.style = "visibility:hidden";
                        link.download = "ExcelReport.xlsx";
                    }
                    else if (FileExtension == '.pdf') {
                        var uri = 'data:application/pdf;base64,' + data;
                        var link = document.createElement("a");
                        link.href = uri;
                        link.style = "visibility:hidden";
                        link.download = "PDFReport.pdf";
                    }
                    else if (FileExtension == '.txt') {
                        var uri = 'data:application/octet-stream;base64,' + data;
                        var link = document.createElement("a");
                        link.href = uri;
                        link.style = "visibility:hidden";
                        link.download = "TextFile.txt";
                    }
                    else if (FileExtension == '.png') {
                        var uri = 'data:image/png;base64,' + data;
                        var link = document.createElement("a");
                        link.href = uri;
                        link.style = "visibility:hidden";
                        link.download = "Img.png";
                    }
                    else if (FileExtension == '.jpeg') {
                        var uri = 'data:image/jpeg;base64,' + data;
                        var link = document.createElement("a");
                        link.href = uri;
                        link.style = "visibility:hidden";
                        link.download = "Img.jpeg";
                    }
                    else if (FileExtension == '.gif') {
                        var uri = 'data:image/gif;base64,' + data;
                        var link = document.createElement("a");
                        link.href = uri;
                        link.style = "visibility:hidden";
                        link.download = "Img.gif";
                    }
                    else if (FileExtension == '.zip') {
                        var uri = 'data:application/zip;base64,' + data;
                        var link = document.createElement("a");
                        link.href = uri;
                        link.style = "visibility:hidden";
                        link.download = "file.zip";
                    }
                    else if (FileExtension == '.doc') {
                        var uri = 'data:application/vnd.openxmlformats-officedocument.wordprocessingml.document;base64,' + data;
                        var link = document.createElement("a");
                        link.href = uri;
                        link.style = "visibility:hidden";
                        link.download = "DocReport.doc";
                    }
                    else if (FileExtension == '.docx') {
                        var uri = 'data:application/vnd.openxmlformats-officedocument.wordprocessingml.document;base64,' + data;
                        var link = document.createElement("a");
                        link.href = uri;
                        link.style = "visibility:hidden";
                        link.download = "DocReport.docx";
                    }
                    else if (FileExtension == '.ppt') {
                        var uri = 'data:application/vnd.ms-powerpoint;base64,' + data;
                        var link = document.createElement("a");
                        link.href = uri;
                        link.style = "visibility:hidden";
                        link.download = "File.ppt";
                    }
                    else if (FileExtension == '.pptx') {
                        var uri = 'data:application/vnd.openxmlformats-officedocument.presentationml.presentation;base64,' + data;
                        var link = document.createElement("a");
                        link.href = uri;
                        link.style = "visibility:hidden";
                        link.download = "File.pptx";
                    }

                    document.body.appendChild(link);
                    link.click();
                    document.body.removeChild(link);
                },
                error: function () {
                    console.log('error Occured while Downloading CSV file.');
                },
            });
        }

    </script>
</asp:Content>
