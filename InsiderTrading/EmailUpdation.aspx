<%@ Page Title="" Language="C#" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" AutoEventWireup="true" CodeBehind="EmailUpdation.aspx.cs" Inherits="ProcsDLL.InsiderTrading.EmailUpdation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--Start Datetime--%>
    <link href="../assets/global/css/components.min.css" rel="stylesheet" />
    <link href="../assets/global/css/plugins.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/bootstrap-timepicker/css/bootstrap-timepicker.min.css" rel="stylesheet" />
    <%--End Datetime--%>
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

    <%-- ========================= Email List ============================= --%>
    <div class="page-content-inner">
        <div class="row">
            <div class="col-md-12">
                <div class="portlet light portlet-fit ">
                    <div class="portlet-title">
                        <div class="caption">
                            <i class="icon-settings font-red"></i>
                            <span class="caption-subject font-red sbold uppercase">Email List</span>
                        </div>
                    </div>
                    <div class="portlet-body">
                        <div class="table-toolbar">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="btn-group-devided">
                                        <button id="btnAdd" runat="server" class="btn green" onclick="fnOpenModel()"  data-toggle="modal">
                                            Update Email <i class="fa fa-plus"></i>
                                        </button>
                                        &nbsp;
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--<table class="table table-striped table-hover table-bordered" id="tbl-Department-setup">
                            <thead>
                                <tr>
                                    <th>DEPARTMENT NAME</th>
                                    <th>ACTION</th>
                                </tr>
                            </thead>
                            <tbody id="tbdDepartmentList">
                            </tbody>
                        </table>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%-- ========================= Update Email from List ============================= --%>
    <div class="modal fade" id="stack1" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnCloseModal();"></button>
                    <h4 class="modal-title">Email Updation</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-4" id="lblDepartment" style="margin-top: 5px;">Email</div>
                        <div class="col-md-6">
                              <select id="ddlEmail" class="form-control">
                               
                            </select>
                        </div>
                        <div class="col-md-2"></div>
                    </div>
                    <br />

                    <div class="row">
                        <div class="col-md-4" style="margin-top: 5px;">New Email</div>
                        <div class="col-md-6">
                            <input id="NewEmail" class="form-control" type="text" data-tabindex="2" />
                            
                        </div>
                        <div class="col-md-2"></div>
                    </div><br /><br />
                     <div class="row">
                        <div class="col-md-4" style="margin-top: 5px;">Confirm Email</div>
                        <div class="col-md-6">
                            <input id="ConfEmail" class="form-control" type="password" data-tabindex="3" />
                           
                        </div>
                        <div class="col-md-2"></div>
                    </div>


                    <%--<div class="row">
                        <div class="col-md-4" style="margin-top: 5px;">Created By </div>
                        <div class="col-md-6">
                            <input id="txtCreateBy" class="form-control" type="text" data-tabindex="2" />
                            <input class="form-control" id="txtCreatedByKey" type="text" style="display: none;" />
                        </div>
                        <div class="col-md-2"></div>
                    </div>
                    <br />--%>
                    <%--<div class="row">
                        <div class="col-md-4" style="margin-top: 5px;">Company Name </div>
                        <div class="col-md-6">
                            <select id="ddlCompany" class="form-control">
                                <option value="0"></option>
                                <option value="Active">Active
                                </option>
                                <option value="Inactive">Inactive
                                </option>
                            </select>
                        </div>
                        <div class="col-md-2"></div>
                    </div>
                    <br />--%>
                    <%-- <div class="row">
                        <div class="col-md-4">Subsciption End Date </div>
                        <div class="col-md-6">
                            <%--<input id="txtSED" class="form-control" type="text" data-tabindex="2" />-%>
                            <div class="input-group input-medium date date-picker" data-date-format="yyyy/mm/dd" data-date-start-date="+0d">
                                <input id="txtSED" class="form-control" readonly="readonly" type="text" />
                                <span class="input-group-btn">
                                    <button class="btn default" type="button">
                                        <i class="fa fa-calendar"></i>
                                    </button>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-2"></div>
                    </div>--%>
                    <br />
                    <%--<div class="row">
                        <div class="col-md-4" style="margin-top: 5px;">Upload Logo </div>
                        <div class="col-md-6">
                            <input id="logo" class="form-control" type="file" data-tabindex="2" />
                        </div>
                        <div id="imagePreview"></div>
                        <div class="col-md-2"></div>
                    </div>--%>
                    <br />
                </div>
                <div class="modal-footer">
                    <button type="button" data-dismiss="modal" class="btn btn-outline dark" onclick="javascript:fnCloseModal();">Close</button>
                    <button id="btnSave" type="button" data-dismiss="modal" class="btn green" onclick="javascript:fnUpdateEmail();">Update</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <%--Start Datetime--%>
    <script src="../assets/editor/jquery-te-1.4.0.min.js"></script>
    <script src="../assets/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js"></script>
    <script src="../assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.js"></script>
    <script src="../assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
    <script src="../assets/global/plugins/bootstrap-timepicker/js/bootstrap-timepicker.min.js"></script>
    <script src="../assets/pages/scripts/components-date-time-pickers.min.js" type="text/javascript"></script>
    <%-- <script src="../assets/global/scripts/app.min.js" type="text/javascript"></script>--%>
    <%--End Datetime--%>
    <script src="../assets/global/scripts/datatable.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/datatables/datatables.min.js" type="text/javascript"></script>
    <%--<script src="assets/global/plugins/datatables/datatables.min.js" type="text/javascript"></script>--%>
    <script src="../assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js" type="text/javascript"></script>
    <script src="../assets/global/scripts/app.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js" type="text/javascript"></script>
    <script src="../assets/pages/scripts/components-date-time-pickers.min.js" type="text/javascript"></script>
    <%--<script src="../assets/pages/scripts/table-datatables-buttons.min.js" type="text/javascript"></script>--%>
    <script src="../assets/global/plugins/select2/js/select2.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/select2/js/select2.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/jquery-validation/js/jquery.validate.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/jquery-validation/js/additional-methods.min.js" type="text/javascript"></script>
    <script src="js/Global.js?<%=DateTime.Now %>"></script>
    <script src="js/EmailUpdation.js?<%=DateTime.Now %>"></script>
</asp:Content>
