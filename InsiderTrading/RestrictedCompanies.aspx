<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" CodeBehind="RestrictedCompanies.aspx.cs" Inherits="ProcsDLL.InsiderTrading.RestrictedCompanies" %>

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
    <%--<link href="../assets/global/plugins/bootstrap-wysihtml5/bootstrap-wysihtml5.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/bootstrap-markdown/css/bootstrap-markdown.min.css" rel="stylesheet" type="text/css" />--%>
    <link href="stylesheets/RestrictedCompanies.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-content-inner">
        <div class="row">
            <div class="col-md-12">
                <div class="portlet light portlet-fit ">
                    <div class="portlet-title">
                        <div class="caption">
                            <i class="icon-settings font-red"></i>
                            <span class="caption-subject font-red sbold uppercase">Companies</span>
                        </div>
                    </div>
                    <div class="portlet-body">
                        <div class="table-toolbar">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="btn-group-devided">
                                        <button id="btnAdd" runat="server" class="btn blue " data-target="#stack1" data-toggle="modal">
                                            Add New <i class="fa fa-plus"></i>
                                        </button>
                                        <button type="button" class="btn btn-danger" value="1" onclick="fnChangeCompanyStatus(this.value)">Mark Selected As Restricted</button> 
                                        <button type="button" class="btn btn-success" value="0" onclick="fnChangeCompanyStatus(this.value)">Mark Selected As Unrestricted</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <table class="table table-striped table-hover table-bordered" id="tbl-Designation-setup">
                            <thead>
                                <tr class="text-capitalize">
                                    <%-- <th style="display:none">DESIGNATION ID</th>--%>
                                    <th class="no-sort"><input type="checkbox" id="chkParent" /></th>
                                    <th>Company Name</th>
                                    <th>Company ABRR</th>
                                    <th>Is Restricted</th>
                                    <th>For Perpetuity(Restricted Forever)</th>
                                    <th>Stock Trade Limit</th>
                                    <th>Period of Restriction From</th>
                                    <th>Period of Restriction To</th>
                                    <th>Actions</th>
                                </tr>
                            </thead>
                            <tbody id="tbdRestrictedCompaniesList">
                            </tbody>
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
                    <h4 class="modal-title">Companies</h4>
                </div>
                <div class="modal-body">
                    <div class="portlet-body form">
                        <form class="form-horizontal" role="form">
                            <div class="form-body">
                                <div class="form-group">
                                    <label class="col-md-3 control-label">Company Name<span class="required"> * </span></label>
                                    <div class="col-md-9">
                                        <input type="text" id="txtCompanyName" data-required="1" class="form-control" placeholder="Enter Company Name" />
                                        <input type="text" id="txtID" class="form-control" style="display: none;" value="0" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-3 control-label">Company ABRR<span class="required"> * </span></label>
                                    <div class="col-md-9">
                                        <input type="text" id="txtCompanyABRR" data-required="1" maxlength="5" class="form-control" placeholder="Enter Company ABRR" />
                                        <span class="help-block notification">Maximum 5 Characters are allowed</span>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-3 control-label">Is Home Company<span class="required"> * </span></label>
                                    <div class="col-md-9">
                                        <div class="mt-radio-inline">
                                            <label class="mt-radio">
                                                <input type="radio" name="rdbIsHomeCompany" id="rdoIsHomeCompanyYes" value="Yes" />
                                                Yes
                                                               
                                                <span></span>
                                            </label>
                                            <label class="mt-radio">
                                                <input type="radio" name="rdbIsHomeCompany" id="rdoIsHomeCompanyNo" value="No" />
                                                No
                                                               
                                                <span></span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-3 control-label">Is Restricted<span class="required"> * </span></label>
                                    <div class="col-md-9">
                                        <div class="mt-radio-inline">
                                            <label class="mt-radio">
                                                <input type="radio" name="rdbIsRestricted" id="rdoIsRestrictedYes" value="Yes" />
                                                Yes
                                                               
                                                <span></span>
                                            </label>
                                            <label class="mt-radio">
                                                <input type="radio" name="rdbIsRestricted" id="rdoIsRestrictedNo" value="No" />
                                                No
                                                               
                                                <span></span>
                                            </label>
                                        </div>
                                        <span class="help-block notification">Do you want to allow this company to be restricted?</span>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-3 control-label">For Perpetuity (Restricted for ever)<span class="required"> * </span></label>
                                    <div class="col-md-9">
                                        <div class="mt-radio-inline">
                                            <label class="mt-radio">
                                                <input type="radio" name="rdbForPerpetuity" id="rdoForPerpetuityYes" value="Yes" />
                                                Yes
                                                               
                                                <span></span>
                                            </label>
                                            <label class="mt-radio">
                                                <input type="radio" name="rdbForPerpetuity" id="rdoForPerpetuitydNo" value="No" />
                                                No
                                                               
                                                <span></span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div id="divStockTradeLimit" class="form-group">
                                    <label class="col-md-3 control-label">Stock Trade Limit<span class="required"> * </span></label>
                                    <div class="col-md-9">
                                        <input type="text" id="txtStockTrade" data-required="1" class="form-control number" placeholder="Enter Stock Trade Limit" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-3 control-label">Period of Restriction From<span class="required"> * </span></label>
                                    <div class="col-md-9">
                                        <input id="txtPeriodFrom" data-required="1" class="form-control form-control-inline input-medium date-picker" size="16" type="text" placeholder="Enter Period of Restriction From" value="" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-3 control-label">Period of Restriction To<span class="required"> * </span></label>
                                    <div class="col-md-9">
                                        <input id="txtPeriodTo" data-required="1" class="form-control form-control-inline input-medium date-picker" data-resize="16" type="text" value="" placeholder="Enter Period of Restriction To" />
                                    </div>
                                </div>
                            </div>
                        </form>
                    </div>
                    <br />
                </div>
                <div class="modal-footer">
                    <button id="btnSave" type="button" data-dismiss="modal" class="btn blue" onclick="javascript:fnSaveRestrictedCompanies();">Save</button>
                    <button type="button" data-dismiss="modal" class="btn default" onclick="javascript:fnCloseModal();">Close</button>
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

    <script src="../assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js" type="text/javascript"></script>




    <script src="../assets/global/scripts/app.min.js" type="text/javascript"></script>


    <%--<script src="../assets/pages/scripts/table-datatables-buttons.min.js" type="text/javascript"></script>--%>
    <script src="../assets/global/plugins/select2/js/select2.min.js" type="text/javascript"></script>

    <script src="../assets/global/plugins/select2/js/select2.full.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery-validation/js/jquery.validate.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery-validation/js/additional-methods.min.js" type="text/javascript"></script>
    <script src="../assets/pages/scripts/form-validation.min.js" type="text/javascript"></script>
    <script src="js/Global.js?<%=DateTime.Now %>" type="text/javascript"></script>
    <script src="js/RestrictedCompanies.js?<%=DateTime.Now %>" type="text/javascript"></script>
</asp:Content>
