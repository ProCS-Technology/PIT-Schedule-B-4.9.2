<%@ Page Language="C#" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" AutoEventWireup="true" CodeBehind="UPSITaskAssignment.aspx.cs" Inherits="ProcsDLL.InsiderTrading.UPSITaskAssignment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
    <style type="text/css">
        .required {
            color: red;
        }
        .page-content {
            min-height: 800px !important;
        }
        table.dataTable tbody th,
        table.dataTable tbody td {
            white-space: nowrap;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12">
        <div class="portlet light portlet-fit">
            <div class="portlet-title">
                <div class="caption">
                    <i class="icon-settings font-red"></i>
                    <span class="caption-subject font-red sbold uppercase">UPSI Report</span>
                </div>
                <div class="tools">
                </div>
            </div>
            <div class="portlet-body">
                <form runat="server">
                    <div class="row">
                        <div class="col-md-3 col-lg-3">
                            <label for="User" style="text-align: center; display: block;">Name of the UPSI Project</label>
                            <asp:DropDownList ID="ddlUPSIGrp" CssClass="form-control select2" PlaceHolder="Select shared by" runat="server"></asp:DropDownList>
                        </div>
                        <%--<div class="col-md-3 col-lg-3">
                            <label for="User" style="text-align: center; display: block;">Shared By</label>
                            <asp:DropDownList ID="ddlUsers" CssClass="form-control select2" runat="server"></asp:DropDownList>
                        </div>--%>
                        <div class="col-md-2 col-lg-3">
                            <label for="from_date" style="text-align: center; display: block;">From</label>
                            <asp:TextBox runat="server" ID="txtFromDate" data-date-format="dd/mm/yyyy" CssClass="form-control date-picker" autocomplete="off" />
                        </div>
                        <div class="col-md-2 col-lg-3">
                            <label for="to_date" style="text-align: center; display: block;">To</label>
                            <asp:TextBox runat="server" ID="txtToDate" data-date-format="dd/mm/yyyy" CssClass="form-control date-picker" autocomplete="off" />
                        </div>
                        <div class="col-md-2 col-lg-3">
                            <asp:Button runat="server" ID="btnRun" style="background-color:limegreen;margin-top:25px;" CssClass="form-control" Text="Run" 
                                OnClientClick="return fnValidateUPSIReport();" OnClick="btnRun_Click" />
                        </div>
                    </div>
                </form>
                <br /><br /><br /><br />
                <table class="table table-striped table-hover display text-nowrap table-bordered" id="tbl-UPSIReport-setup">
                    <thead>
                        <%--<tr>
                            <th rowspan="2">S.No.</th>
                            <th colspan="2" style="text-align:center;">Information Shared By</th>
                            <th colspan="3" style="text-align:center;">Information Shared With</th>
                            <th colspan="5" style="text-align:center;">Other Details</th>
                        </tr>--%>
                        <tr>
                            <th>S.No.</th>
                            <th>Name</th>
                            <th>PAN/Other Identifier No</th>
                            <th>Shard With</th>
                            <%--<th>Organisation Name</th>
                            <th>PAN/Other Identifier No</th>--%>
                            <th>Date of sharing UPSI(A)</th>
                            <th>Time stamp for (A)</th>
                            <th>Nature of Information shared</th>
                            <th>Mode of Sharing</th>
                            <%--<th>Date when information<br />ceases to<br />be UPSI (B)</th>
                            <th>Time stamp for (B)</th>
                            <th>Remarks, if any,<br />for cessation</th>--%>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody id="tbdUPSIReportList" runat="server"></tbody>
                </table>
            </div>
        </div>
    </div>
    <div class="modal fade " id="stackUPSILogs" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-dialog-centered modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title caption-subject bold uppercase font-red-sunglo">UPSI</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnCloseModalMsg();"></button>
                </div>
                <div class="modal-body">
                    <div class="table-responsive">
                        <div id="dvupsiremarks">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="messageBody" class="modal fade" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-dialog-centered" style="min-width: 80%">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Message</h5>

                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>

                <div class="modal-body p-20" id="bdMessage" style="overflow: auto; max-height: 500px;">
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="stackUPSIMessage" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-dialog-centered" style="width:90% !important;">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title caption-subject bold uppercase font-red-sunglo">UPSI GROUP MESSAGE</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnCloseModalMsg();"></button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-2" style="text-align:left;">From</div>
                        <div class="col-md-10">
                            <span id="dvUPSITaskMSGFrom"></span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2" style="text-align:left;">To</div>
                        <div class="col-md-10">
                            <span id="dvUPSITaskMSGTo"></span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2" style="text-align:left;">CC</div>
                        <div class="col-md-10">
                            <span id="dvUPSITaskMSGCC"></span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2" style="text-align:left;">Date</div>
                        <div class="col-md-10">
                            <span id="dvUPSITaskMsgDate"></span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2" style="text-align:left;">Email Message</div>
                        <div class="col-md-10">
                            <span id="dvUPSITaskMSGBody"></span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2" style="text-align:left;">Attachment</div>
                        <div class="col-md-10">
                            <span id="dvAttechmentlistMsg"></span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="../assets/editor/jquery-te-1.4.0.min.js"></script>
    <script src="../assets/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js"></script>
    <script src="../assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.js"></script>
    <script src="../assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
    <script src="../assets/global/plugins/bootstrap-timepicker/js/bootstrap-timepicker.min.js"></script>
    <script src="../assets/pages/scripts/components-date-time-pickers.min.js" type="text/javascript"></script>
    <script src="../assets/global/scripts/datatable.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/datatables/datatables.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js" type="text/javascript"></script>
    <script src="../assets/global/scripts/app.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js" type="text/javascript"></script>
    <script src="../assets/pages/scripts/components-date-time-pickers.min.js" type="text/javascript"></script>
    <script src="../assets/pages/scripts/components-select2.min.js" type="text/javascript"></script>
    <script src="js/Global.js?<%=DateTime.Now %>" type="text/javascript"></script>
    <script src="js/UPSITaskAssignment.js?<%=DateTime.Now %>" type="text/javascript"></script>
</asp:Content>