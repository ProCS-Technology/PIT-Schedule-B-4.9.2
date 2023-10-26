<%@ Page Language ="C#" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" AutoEventWireup="true" CodeBehind="Holiday.aspx.cs" Inherits="ProcsDLL.InsiderTrading.Holiday" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
            /* provide some minimal visual accomodation for IE8 and below */
            .TFtable tr {
                background: #f2f4f7;
                font-size: 12px;
            }
                /*  Define the background color for all the ODD background rows  */
                .TFtable tr:nth-child(odd) {
                    background: #f2f4f7;
                }
                /*  Define the background color for all the EVEN background rows  */
                .TFtable tr:nth-child(even) {
                    background: #fff;
                }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="page-content-inner">
        <div class="row">
            <div class="col-md-12">
                <div class="portlet light portlet-fit ">
                    <div class="portlet-title">
                        <div class="caption">
                            <i class="icon-settings font-red"></i>
                            <span class="caption-subject font-red sbold uppercase">Holiday</span>
                        </div>
                    </div>
                    <div class="portlet-body">
                        <div class="table-toolbar">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="btn-group-devided">
                                        <button id="btnAdd" runat="server" class="btn green" data-target="#stack1" data-toggle="modal">
                                            Add New <i class="fa fa-plus"></i>
                                        </button>
                                         &nbsp;
                                    </div>
                                </div>
                            </div>
                        </div>

                          <table class="table table-striped table-hover table-bordered" id="tbl-Holiday-setup">
                            <thead>
                                <tr>
                                    <th>S.NO.</th>
                                    <th>Holiday Description</th>
                                    <th>Holiday Date</th>
                                    <th>ACTIONS</th>
                                    <%-- <th>DELETE</th>--%>

                                </tr>
                            </thead>
                            <tbody id="tbdHolidayList">
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade bs-modal-lg" id="stack1" tabindex="-1" aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnCloseModal();"></button>
                    <h4 class="modal-title caption-subject bold uppercase font-red-sunglo">New Holiday<span id="spnTitle"></span></h4>
                </div>
                <form class="form-horizontal" runat="server" role="form">
                    <div class="modal-body">
                        <br />
                        <br />
                        <div class="portlet light bordered">
                            <div class="portlet-body form">
                                <div class="form-body">

                                    <div class="form-group">
                                        <label id="lblName" style="text-align: left" class="col-md-4 control-label">Holiday Description<span class="required"> * </span></label>
                                        <div class="col-md-8">
                                            <input type="hidden" id="txtID" value="0" />
                                            <input id="txtDesc" type="text" class="form-control" placeholder="Enter Holiday Description" autocomplete="off" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label id="lbldate" style="text-align: left" class="col-md-4 control-label">Holiday Date<span class="required"> * </span></label>
                                        <div class="col-md-8">
                                            <input id="txtdate" type="text" class="form-control" placeholder="Enter Holiday Date" name="from_date" autocomplete="off" />
                                        </div>
                                    </div>
                                    <br />
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button id="btnSave" type="button" data-dismiss="modal" class="btn green" onclick="javascript:fnSaveHoliday();">Save</button>
                        <button id="btnCancel" type="button" data-dismiss="modal" class="btn default" onclick="javascript:fnCloseModal();">Cancel</button>
                    </div>
                </form>
            </div>
        </div>
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
    <script src="js/Global.js" type="text/javascript"></script>
    <script src="js/Holiday.js" type="text/javascript"></script>
</asp:Content>
