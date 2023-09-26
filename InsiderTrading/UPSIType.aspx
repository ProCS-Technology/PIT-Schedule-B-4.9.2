<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" CodeBehind="UPSIType.aspx.cs" Inherits="ProcsDLL.InsiderTrading.UPSIType" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../assets/global/css/components.min.css" rel="stylesheet" />
    <link href="../assets/global/css/plugins.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/css/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <link href="../assets/layouts/layout/css/layout.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/layouts/layout/css/themes/darkblue.min.css" rel="stylesheet" type="text/css" id="style_color" />
    <link href="../assets/layouts/layout/css/custom.min.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="portlet light bordered">
        <div class="portlet-title">
            <div class="caption font-red-sunglo">
                <i class="icon-settings font-red-sunglo"></i>
                <span class="caption-subject bold uppercase">UPSI</span>
            </div>
        </div>
        <div class="portlet-body">
            <div class="table-toolbar">
                <div class="row">
                    <div class="col-md-6">
                        <div class="btn-group-devided">
                            <button id="btnAddupsi" runat="server" class="btn green" data-target="#UPSIGrp" data-toggle="modal" onclick="javascript:reset();">
                                Add New UPSI
                            </button>
                            &nbsp;
                        </div>
                    </div>
                </div>
            </div>
            <table class="table table-striped table-hover table-bordered" id="tbl-upsilist-setup">
                <thead>
                    <tr>
                        <th>UPSI Type</th>
                        <th>Status</th>
                        <th>Action</th>
                    </tr>
                </thead>
                <tbody id="tbdupsilist"></tbody>
            </table>
        </div>

        <div class="modal fade" id="UPSIGrp" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-dialog-centered modal-lg modal-xl">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title caption-subject bold uppercase font-red-sunglo">Add UPSI Type</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fncloseModal();"></button>
                    </div>

                    <form class="form-horizontal" role="form">
                        <div class="modal-body">
                            <div class="portlet light bordered">
                                <div class="portlet-body">
                                    <div class="form-group">
                                        <div class="col-md-4">
                                            <label class="control-label">UPSI Type</label>
                                        </div>
                                        <div class="col-md-8">
                                            <div class="input-inline input-medium">
                                                <div class="input-group">
                                                    <input type="text" id="txtUPSIType" class="form-control" style="width:400px;" />
                                                    <input type="text" id="txtUPSITypeId" style="display:none;" class="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-4">
                                            <label class="control-label">Status</label>
                                        </div>
                                        <div class="col-md-8">
                                            <select id="ddlStatus" style="width:400px;" class="form-control">
                                                <option value="0">--Select--</option>
                                                <option value="Active">Active</option>
                                                <option value="Inactive">Inactive</option>
                                            </select>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <span class="caption-subject font-red sbold uppercase">UPSI Type Keyword</span>
                            <table class="table" id="tblKeyword">
                                <thead>
                                    <tr>
                                        <th>Keyword<span class="required">*</span></th>
                                        <th>Match Order<span class="required">*</span></th>
                                    </tr>
                                </thead>
                                <tbody id="tbodyKeyword">
                                    <tr>
                                        <td style="margin: 5px;">
                                            <input id="txtKeyword" class="form-control form-control-inline" placeholder="Enter Keyword" type="text" autocomplete="off" />
                                        </td>
                                        <td style="margin: 5px;">
                                            <input id="txtOrder" class="form-control form-control-inline numerictextbox" placeholder="Enter Match Order" type="text" autocomplete="off" />
                                        </td>
                                        <td style="margin: 5px;">
                                            <img onclick="javascript:fnAddRow();" src="images/icons/AddButton.png" height="24" width="24" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <div class="row" style="text-align:right;margin-right:30px;">
                                <div class="col-lg-12">
                                    <a href="#" id="btnSave" type="button" onclick="fnSaveUPSIType()" class="btn green">SAVE</a>
                                    <a href="#" id="btnCancel" type="button" data-dismiss="modal" aria-hidden="true" onclick="javascript:fncloseModal();" class="btn btn-outline dark">Close</a>
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
    <script src="../assets/editor/jquery-te-1.4.0.min.js"></script>
    <script src="../assets/global/scripts/datatable.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/datatables/datatables.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js" type="text/javascript"></script>
    <script src="../assets/global/scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/js.cookie.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>

    <script src="../assets/global/scripts/app.min.js" type="text/javascript"></script>
    <script src="js/Global.js?<%=DateTime.Now %>" type="text/javascript"></script>
    <script src="js/UPSIType.js?<%=DateTime.Now %>" type="text/javascript"></script>
</asp:Content>