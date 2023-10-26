<%@ Page Language="C#" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" AutoEventWireup="true" CodeBehind="UPSIRpt.aspx.cs" Inherits="ProcsDLL.InsiderTrading.UPSIRpt" %>
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
                             <asp:TextBox ID="showupsitoco"  runat="server"  type="hidden"></asp:TextBox>
                             <asp:TextBox  ID="ManagedByCo" runat="server" type="hidden"></asp:TextBox>
                        </div>
                        <div class="col-md-3 col-lg-3">
                            <label for="User" style="text-align: center; display: block;">Shared By</label>
                            <asp:DropDownList ID="ddlUsers" CssClass="form-control select2" runat="server"></asp:DropDownList>
                        </div>
                        <div class="col-md-2 col-lg-2">
                            <label for="from_date" style="text-align: center; display: block;">From</label>
                            <input id="txtFromDate" data-date-format="dd/mm/yyyy" type="text" class="form-control date-picker" name="from_date" autocomplete="off" />
                        </div>
                        <div class="col-md-2 col-lg-2">
                            <label for="to_date" style="text-align: center; display: block;">To</label>
                            <input id="txtToDate" data-date-format="dd/mm/yyyy" type="text" class="form-control date-picker" name="to_date" autocomplete="off" />
                        </div>
                        <div class="col-md-2 col-lg-2">
                            <input id="txtGetTadingReport" type="button" style="background-color: limegreen; margin-top: 25px;" class="form-control" value="Run" onclick="fnGetUPSIReportOnRun();" name="to_date" />
                        </div>
                    </div>
                </form>
                <br /><br /><br /><br />
                <div style="overflow: scroll;">
                <table class="table table-striped table-hover table-bordered" id="tbl-UPSIReport-setup" style="width:100%">
                    <thead>
                        <tr>
                            <th rowspan="2">S.No.</th>
                            <th colspan="3" style="text-align:center;">Information Shared By</th>
                            <th colspan="3" style="text-align:center;">Information Shared With</th>
                            <th colspan="11" style="text-align:center;">Other Details</th>
                        </tr>
                        <tr >
                             <th>Name</th>
                            <th>PAN/Other Identifier No</th>
                            <th>Insider Name</th>
                            <th>Email ID of Sender</th>
                            <th>Organisation Name</th>
                            <th>PAN/Other Identifier No</th>
                            <th>Date of sharing UPSI(A)</th>
                            <th>Time stamp for (A)</th>
                            <th>Nature of Information shared</th>
                            <th>Mode of Sharing</th>
                            <th>Date when information<br />ceases to<br />be UPSI (B)</th>
                            <th>Time stamp for (B)</th>
                            <th>Remarks, if any,<br />for cessation</th>
                            <th>Date of entry in SDD (B)</th>
                            <th>Time stamp for (B)</th>
                            <th>UPSI Reported through</th>
                            <th>Attachment</th>
                            <%--<th>Notice<br />Sent<br />(Yes/No)</th>--%>
                        </tr>
                    </thead>
                    <tbody id="tbdUPSIReportList"></tbody>
                </table>
                    </div>
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
    <script src="js/UPSIRpt.js?<%=DateTime.Now %>" type="text/javascript"></script>
    
</asp:Content>