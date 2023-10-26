<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" CodeBehind="ApproverSetUp.aspx.cs" Inherits="ProcsDLL.InsiderTrading.ApproverSetUp" %>
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
        .requied {
            color: red;
        }
        .TFtable {
            width: 100%;
            border-collapse: collapse;
        }
            .TFtable th {
                padding: 7px;
                border: #dddddd 1px solid;
            }
            .TFtable td {
                padding: 7px;
                border: #dddddd 1px solid;
            }
            .TFtable tr {
                background: #f2f4f7;
                font-size: 12px;
            }
                .TFtable tr:nth-child(odd) {
                    background: #f2f4f7;
                }
                .TFtable tr:nth-child(even) {
                    background: #fff;
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
                            <span class="caption-subject font-red sbold uppercase">Approval</span>
                        </div>
                    </div>
                    <div class="portlet-body">
                        <div class="table-toolbar">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="btn-group-devided">
                                        <button id="btnAdd" runat="server" class="btn green" data-target="#userModel" onclick="javascript:OpenNew();" data-toggle="modal">
                                            Add New Approval <i class="fa fa-plus"></i>
                                        </button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <table class="table table-striped table-hover table-bordered" id="tbl-catlist-setup">
                            <thead>
                                <tr>
                                    <th>WF ID</th>
                                    <th>USER LOGIN</th>
                                    <th>MIN LIMIT</th>
                                    <th>MAX LIMIT</th>
                                    <th>CREATED BY</th>
                                    <th>Action</th>
                                </tr>
                            </thead>
                            <tbody id="tbApprover"></tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade bs-modal-lg" id="userModelNew" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" onclick="fnCloseAdUserPopUp();" aria-hidden="true"></button>
                    <h4 class="modal-title">Approval</h4>
                </div>
                <div class="modal-body">
                    <div class="portlet" style="margin-bottom: 0;">
                        <div class="row">
                            <div class="col-md-3"><b>User Login<span class="required" aria-required="true">*</span></b></div>
                            <div class="col-md-3">
                                <input type="text" id="txtUserLogin" class="form-control regtxt" data-tablindex="1" />
                            </div>
                            <div class="col-md-2">
                                <button id="btnserchUser" type="button" class="btn red" onclick="btnSearch_Click();">Search</button>
                            </div>
                            <div class="col-md-2">
                                <b>Is Search Users From Local</b>
                            </div>
                            <div class="col-md-2">
                                <input type="checkbox" id="isSearchUsersFromLocalServer" class="form-control regtxt" />
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12" id="userlist"></div>
                        </div>
                        <div id="MODALFOOTER1" class="modal-footer">
                            <button type="button" data-target="#userModelNew" data-toggle="modal" onclick="fnCloseAdUserPopUp();" class="btn dark  btn-outline">Close</button>
                            <button id="btnSaveUser1" type="button" onclick="fnAddADUserToUserList();" class="btn blue">Save</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade bs-modal-lg" id="userModel" tabindex="-1" aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnCloseModal();"></button>
                    <h4 class="modal-title caption-subject bold uppercase font-red-sunglo"><span id="spnTitle"></span></h4>
                </div>
                <form class="form-horizontal" runat="server" role="form">
                    <div class="modal-body">
                       
                        <div class="portlet light bordered">
                            <div class="portlet-body form">
                                <div class="form-body">
                                    <br />
                                    <div class="form-group">
                                        <label id="lblUser" style="text-align: left" class="col-md-4 control-label">User Login<span class="required"> * </span></label>
                                        <div class="col-md-8">
                                            <input type="hidden" id="txtWFId" value="0" />
                                            <select id="ddlUser" class="form-control">
                                                <option value="0" selected="selected">Please Select</option>
                                            </select>
                                        </div>
                                        <div class="row">
                                            <label class="col-sm-2"></label>
                                            <div class="col-sm-10">
                                                <p id="Status" />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="form-group">
                                        <label id="lblMinLimit" style="text-align: left" class="col-md-4 control-label">MIN Limit<span class="required"> * </span></label>
                                        <div class="col-md-8">
                                            <input type="number" id="txtMinLimit" class="form-control number" min="0" />
                                        </div>
                                    </div>
                                    <br />
                                    <div class="form-group">
                                        <label id="lblMaxLimit" style="text-align: left" class="col-md-4 control-label">Max Limit<span class="required"> * </span></label>
                                        <div class="col-md-8">
                                            <input type="number" class="form-control number" id="txtMaxLimit" min="1" />
                                        </div>
                                    </div>
                                    
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button id="btnSave" type="button" data-dismiss="modal" class="btn green" onclick="javascript:fnSaveUser();">Save</button>
                        <button id="btnCancel" type="button" data-dismiss="modal" class="btn default" onclick="javascript:fnCloseModal();">Cancel</button>

                    </div>
                </form>
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
    <script src="../assets/global/plugins/select2/js/select2.min.js" type="text/javascript"></script>
    <script src="js/Global.js?<%=DateTime.Now %>" type="text/javascript"></script>
    <script src="js/ApproverSetUp.js?<%=DateTime.Now %>" type="text/javascript"></script>
</asp:Content>