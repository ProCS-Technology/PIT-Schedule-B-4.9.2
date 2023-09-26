<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UPSITypeMaster.aspx.cs" Inherits="ProcsDLL.InsiderTrading.UPSITypeMaster" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../assets/global/css/components.min.css" rel="stylesheet" />
    <link href="../assets/global/css/plugins.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/select2/css/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/select2/css/select2-bootstrap.min.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .requied {
            color: red;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-content-inner">
        <div class="row">
            <div class="col-md-12">
                <div class="portlet light portlet-fit">
                    <div class="portlet-title">
                        <div class="caption">
                            <i class="icon-settings font-red"></i>
                            <span class="caption-subject font-red sbold uppercase">UPSI Type</span>
                        </div>
                    </div>
                    <div class="portlet-body">
                        <div class="table-toolbar">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="btn-group-devided">
                                        <button id="btnAdd" runat="server" class="btn green" onclick="javascript:fnAddDepartment();" data-target="#stack1" data-toggle="modal">
                                            Add New <i class="fa fa-plus"></i>
                                        </button>&nbsp;
                                    </div>
                                </div>
                            </div>
                        </div>
                        <table class="table table-striped table-hover table-bordered" id="tbl-UPSIType-setup">
                            <thead>
                                <tr>
                                    <th>UPSI Type</th>
                                    <th>Status</th>
                                    <th>ACTION</th>
                                </tr>
                            </thead>
                            <tbody id="tbdUPSITypeList"></tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="stack1" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnCloseModal();"></button>
                    <h4 class="modal-title">UPSI Type</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-4" id="lblUPSIType" style="margin-top: 5px;">
                            UPSI Type <span class="red">*</span>
                        </div>
                        <div class="col-md-6">
                            <input id="txtUPSIType" class="form-control" type="text" />
                            <input class="form-control" id="txtUPSITypeId" type="text" style="display:none;" />
                        </div>
                        <div class="col-md-2"></div>
                    </div><br />
                    <div class="row">
                        <div class="col-md-4" id="lblStatus" style="margin-top: 5px;">
                            Status<span class="red">*</span>
                        </div>
                        <div class="col-md-6">
                            <select id="ddlStatus" class="form-control">
                                <option value=""></option>
                                <option value="Active">Active</option>
                                <option value="Inactive">Inactive</option>
                            </select>
                        </div>
                        <div class="col-md-2"></div>
                    </div><br />
                </div>
                <div class="modal-footer">
                    <button type="button" data-dismiss="modal" class="btn btn-outline dark" onclick="javascript:fnCloseModal();">Close</button>
                    <button id="btnSave" type="button" data-dismiss="modal" class="btn green" onclick="javascript:fnSaveUPSIType();">Save</button>
                </div>
            </div>
        </div>
    </div>
    <script src="../assets/global/scripts/datatable.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/datatables/datatables.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js" type="text/javascript"></script>
    <script src="../assets/global/scripts/app.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/jquery-validation/js/jquery.validate.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/jquery-validation/js/additional-methods.min.js" type="text/javascript"></script>
    <script src="js/Global.js"></script>
    <script src="js/UPSITypeMaster.js?<%=DateTime.Now %>"></script>
</asp:Content>