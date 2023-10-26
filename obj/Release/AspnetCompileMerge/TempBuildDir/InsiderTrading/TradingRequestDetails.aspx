<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" CodeBehind="TradingRequestDetails.aspx.cs" Inherits="ProcsDLL.InsiderTrading.TradingRequestDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/select2/css/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/select2/css/select2-bootstrap.min.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .requied {
            color: red;
        }

        td.details-control {
            background: url('images/icons/details_open.png') no-repeat center center;
            cursor: pointer;
        }

        tr.shown td.details-control {
            background: url('images/icons/details_close.png') no-repeat center center;
        }

        /*.page-content {
            width: 100% !important;
        }

        .page-content-wrapper > .page-content {
            min-height: 800px !important;
        }*/
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<div class="page-content-inner">--%>
    <div class="row" style="margin-left: 0px!important; margin-right: 0px !important;">
        <div class="col-md-12">
            <div class="portlet light portlet-fit ">
                <div class="portlet-title" style="margin-bottom: 45px">
                    <div class="caption">
                        <i class="icon-settings font-red"></i>
                        <span class="caption-subject font-red sbold uppercase">Trading Request Details</span>
                    </div>
                </div>
                <div class="portlet-body">
                    <div class="table-toolbar">
                        <div class="row">
                            <div class="col-md-4 col-lg-4">
                                <label for="Status" style="text-align: center; display: block;">Status</label>
                                <select class="form-control" onchange="fnGetTradingRequestDetails()" name="Status" id="bindStatus">
                                    <option value="All">All</option>
                                    <option value="Draft">Draft</option>
                                    <option value="InApproval">InApproval</option>
                                    <option value="Approved">Approved</option>
                                    <option value="Rejected">Rejected</option>
                                </select>
                            </div>
                        </div>
                    </div>
                    <table class="table table-striped table-bordered" id="tbl-TradingRequest-setup">
                        <thead>
                            <tr>
                                <th scope="col">#</th>
                                <th>For</th>
                                <th>Relation</th>
                                <%--<th>Security</th>
                                <th>Company</th>--%>
                                <th>Quantity</th>
                                <th>Type</th>
                                <th style="display: none;">Trade Exchange</th>
                                <th>Demat</th>
                                <th>Requested</th>
                                <th>Status</th>
                                <%--                                <th>Broker Note Uploaded</th>--%>
                                <th>Reviewed On</th>
                                <th>Reviewed By</th>
                                <th>Reviewer Remarks</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody id="tbdTradingRequestList">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <%--</div>--%>

    <div class="modal fade" id="stack1" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <%-- <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnCloseModal();"></button>
                    <h4 class="modal-title">Policy SetUp</h4>
                </div>--%>


                <%-- <div class="modal-footer">
                    <button type="button" data-dismiss="modal" class="btn btn-outline dark" onclick="javascript:fnCloseModal();">Close</button>
                    <button id="btnSave" type="button" data-dismiss="modal" class="btn green" onclick="javascript:fnSavePolicy();">Save</button>
                </div>--%>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->

    </div>
    <script src="../assets/editor/jquery-te-1.4.0.min.js"></script>
    <script src="../assets/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js"></script>
    <script src="../assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.js"></script>
    <script src="../assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
    <script src="../assets/global/plugins/bootstrap-timepicker/js/bootstrap-timepicker.min.js"></script>
    <script src="../assets/pages/scripts/components-date-time-pickers.min.js" type="text/javascript"></script>
    <script src="../assets/global/scripts/datatable.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/datatables/datatables.min.js" type="text/javascript"></script>
    <%--<script src="assets/global/plugins/datatables/datatables.min.js" type="text/javascript"></script>--%>
    <script src="../assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js" type="text/javascript"></script>
    <script src="../assets/global/scripts/app.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js" type="text/javascript"></script>
    <script src="../assets/pages/scripts/components-date-time-pickers.min.js" type="text/javascript"></script>
    <%--<script src="../assets/pages/scripts/table-datatables-buttons.min.js" type="text/javascript"></script>--%>
    <script src="../assets/global/plugins/select2/js/select2.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/jquery-validation/js/jquery.validate.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/jquery-validation/js/additional-methods.min.js" type="text/javascript"></script>
    <script src="js/Global.js?<%=DateTime.Now %>"></script>
    <script src="js/Trading_Request_Details.js?<%=DateTime.Now %>"></script>
    <script type="text/javascript" src="js/FileSaver.js"></script>
</asp:Content>
