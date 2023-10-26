<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" CodeBehind="UserNew.aspx.cs" Inherits="ProcsDLL.InsiderTrading.UserNew" %>

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
     <input type="hidden" id="txtUserId" />
    <div class="page-content-inner">
        <div class="col-md-12">
            <div class="portlet light portlet-fit" style="margin-left: -15px; margin-right: -15px; min-height: 700px;">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="icon-settings font-red"></i>
                        <span class="caption-subject font-red sbold uppercase">New User</span>
                    </div>
                </div>
                <div class="portlet-body">
                    <div class="col-md-12">
                        <div class="table-toolbar">
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="btn-group-devided">
                                        <button runat="server" class="btn green" data-target="#UserNewModal"   data-toggle="modal">
                                            Upload <i class="fa fa-upload"></i>
                                        </button>&nbsp;
                                    </div>
                                </div>
                                 <div class="col-md">
                                   <div class="btn-group-devided">
                                       <button runat="server" class="btn green" onclick ="window.location.href='./UserBulkUpload/UserFormat_userUpload.xlsx'">
                                            Download Template <i class="fa fa-download"></i>
                                        </button>&nbsp;
                                   </div>
                                </div>
                            </div>
                        </div>
                        <table class="table table-striped table-hover display text-nowrap table-bordered" id="tbl-UserNew-setup">
                            <thead>
                                <tr>
                                    <th>User Name</th>
                                    <th>Created On</th>
                                    <th>File</th>
                                </tr>
                            </thead>
                            <tbody id="tbdUserNewDetails"></tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="UserNewModal" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnClearUserNewForm();"></button>
                    <h4 class="modal-title">New User</h4>
                </div>
                <div class="modal-body">
                    <form class="form-horizontal" role="form" runat="server">
                        <div class="form-body modal-fixheight">
                            <%--<div class="form-group">
                                <label id="lblUserName" class="col-md-3 control-label">User Name</label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <input id="ddlUserName" class="form-control" placeolder="UserName"></input>
                                    </div>
                                </div>
                            </div>
                            --%>
                            <div class="form-group">
                                <div class="fileinput fileinput-new" data-provides="fileinput">
                                    <label id="lblUploadUsernew" class="col-md-3 control-label">Upload UserNew/Physical Shares(Allocated)</label>&nbsp;&nbsp;
                                    <div class="col-md-9">
                                        <div class="btn default btn-file" style="min-width: 350px; max-width: 350px">
                                            <input id="fileUploadImageUserNew" type="file" name="..." onchange="javascript:fnRemoveClass(this,'Excel');" <%--onchange="javascript:fnRemoveClass(this,'UploadUserNew','fileUploadImageUserNew');"--%> />
                                        </div>
                                        <a id="aUserAvatarImageUploadedUserNew" href="#" target="_blank">
                                            <img src="../assets/images/arrow-download-icon.png" style="width: 30px; height: 30px; display: none" />
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
                <div class="modal-footer">
                    <button id="btnSubmitUserNew" type="button" class="btn btn-outline dark" onclick="javascript:fnSubmitUserNewFile();">Submit</button>
                    <button id="btnCancelUserNew" type="button" class="btn red" data-dismiss="modal" onclick="javascript:fnClearUserNewForm();">Cancel</button>
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
    <script src="js/UserNew.js?<%=DateTime.Now %>" type="text/javascript"></script>
</asp:Content>
