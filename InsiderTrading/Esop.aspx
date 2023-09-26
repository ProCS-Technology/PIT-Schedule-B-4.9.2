<%@ Page Title="Esop" Language="C#" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" AutoEventWireup="true" CodeBehind="Esop.aspx.cs" Inherits="ProcsDLL.InsiderTrading.Esop" %>
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
    <style>
        .requied {
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
    <div class="page-content-inner">
        <div class="col-md-12">
            <div class="portlet light portlet-fit" style="margin-left: -15px; margin-right: -15px; min-height: 700px;">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="icon-settings font-red"></i>
                        <span class="caption-subject font-red sbold uppercase">CORPORATE ACTION</span>
                    </div>
                </div>
                <div class="portlet-body">
                    <div class="col-md-12">
                        <div class="table-toolbar">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="btn-group-devided">
                                        <button runat="server" class="btn green" data-target="#EsopModal" data-toggle="modal">
                                            Upload <i class="fa fa-upload"></i>
                                        </button>&nbsp;
                                    </div>
                                </div>
                            </div>
                        </div>
                        <table class="table table-striped table-hover display text-nowrap table-bordered" id="tbl-Esop-setup">
                            <thead>
                                <tr>
                                    <th>COMPANY</th>
                                    <th>Created On</th>
                                    <th>File</th>
                                </tr>
                            </thead>
                            <tbody id="tbdEsop"></tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="EsopModal" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnClearEsopForm();"></button>
                    <h4 class="modal-title">Corporate Action</h4>
                </div>
                <div class="modal-body">
                    <form class="form-horizontal" role="form" runat="server">
                        <div class="form-body modal-fixheight">
                            <div class="form-group">
                                <label id="lblEsopType" class="col-md-3 control-label">Corporate Action</label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <select id="ddlCorporateAction" class="form-control" onchange="javascript:fnRemoveClass(this,'Company');">
                                            <%--<option value="1">ESOP</option>
                                            <option value="2">Bonus</option>
                                            <option value="3">Split</option>
                                            <option value="4">Buy Back</option>--%>
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label id="lblCompany" class="col-md-3 control-label">Company</label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <select id="ddlRestrictedCompanies" class="form-control" onchange="javascript:fnRemoveClass(this,'Company');"></select>
                                    </div>
                                </div>
                            </div>
                            
                            
                            
                            <div class="form-group">
                                <div class="fileinput fileinput-new" data-provides="fileinput">
                                    <label id="lblUploadESOP" class="col-md-3 control-label">Upload Corporate Action</label>&nbsp;&nbsp;   
                                    <div class="col-md-9">
                                        <div class="btn default btn-file" style="min-width: 350px; max-width: 350px">
                                            <input id="fileUploadImageESOP" type="file" name="..." onchange="javascript:fnRemoveClass(this,'UploadESOP','fileUploadImageESOP');" />
                                        </div>
                                        <a id="aUserAvatarImageUploadedESOP" href="#" target="_blank">
                                            <img src="../assets/images/arrow-download-icon.png" style="width: 30px; height: 30px; display: none" />
                                        </a>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label id="lblCompanyShares" class="col-md-3 control-label">Number of shares in depository</label>
                                <div class="col-md-9">
                                    <table id="addNumberOfShares">
                                        <tbody id="tbodyNumberOfShares">
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <div class="form-group display-none">
                                <label id="" class="col-md-3 control-label">
                                    <b>Threshold Limit</b>
                                    <span class="mt-radio-inline">
                                        <label class="mt-radio">
                                            <input type="radio" name="optionsLimitType" id="optionsQty" value="Quantity" />Quantity
                                            <span></span>
                                        </label>
                                        <label class="mt-radio">
                                            <input type="radio" name="optionsLimitType" id="optionsVal" value="Value" />Value
                                            <span></span>
                                        </label>
                                    </span>
                                </label>
                                <div class="col-md-9">
                                    <table id="addThresholdLimit">
                                        <tbody id="tbodyThresholdLimit">
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
                <div class="modal-footer">
                    <button id="btnSubmitEsop" type="button" class="btn btn-outline dark" onclick="javascript:fnSubmitEsopFile();">Submit</button>
                    <button id="btnCancelEsop" type="button" class="btn red" data-dismiss="modal" onclick="javascript:fnClearEsopForm();">Cancel</button>
                </div>
            </div>
        </div>
    </div>
    <script src="../assets/global/scripts/datatable.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/datatables/datatables.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script src="../assets/pages/scripts/components-date-time-pickers.min.js" type="text/javascript"></script>
    <script src="js/Global.js?<%=DateTime.Now %>" type="text/javascript"></script>
    <script src="js/Esop.js?<%=DateTime.Now %>" type="text/javascript"></script>
</asp:Content>