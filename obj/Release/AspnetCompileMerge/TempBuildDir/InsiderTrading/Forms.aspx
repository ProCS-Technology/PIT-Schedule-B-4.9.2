<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" CodeBehind="Forms.aspx.cs" Inherits="ProcsDLL.InsiderTrading.Forms" %>

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
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="page-content-inner">


        <div class="col-md-12">
            <div class="portlet light portlet-fit" style="margin-left: -15px; margin-right: -15px; min-height: 1800px;">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="icon-settings font-red"></i>
                        <span class="caption-subject font-red sbold uppercase">Forms</span>
                    </div>
                </div>
                <div class="portlet-body">
                    <div class="col-md-12">
                        <div class="col-md-3">
                            <label><b>Form Selection</b></label>
                            <br />
                            <select class="dropdown form-control" id="selFormSelection">
                            </select>
                        </div>
                        <div class="col-md-offset-2 col-md-3">
                            <a href="#" target="_blank" class="btn default" style="margin-left: 20px;" id="btDownloadForm" onclick="javascript:fnDownloadForm()">Download Form</a><br />
                        </div>
                        <br />
                        <br />
                        <br />
                        <br />
                        <hr />
                    </div>
                    <br />
                    <br />
                    <br />
                    <br />
                    <div class="col-md-12">
                        <div class="col-md-3">
                            <label><b>Upload Form (Pdf)</b></label>
                            <div class="btn default btn-file">
                                <input id="fileUploadImage" type="file" name="..." />
                            </div>
                        </div>
                        <div class="col-md-offset-2 col-md-3">
                            <button class="btn default" style="margin-left: 20px;" id="btSubmitFormToPdf" onclick="javascript:fnSubmitForm()">Submit Form</button><br />
                        </div>
                        <br />
                        <br />
                        <br />
                        <br />
                        <hr />
                        <br />
                        <br />
                        <br />
                        <br />
                    </div>
                    <br />
                    <br />
                    <br />
                    <br />

                    <div class="col-md-12">
                        <div class="row">
                            <label><b>List of Forms Submitted by the user</b></label>
                            <table class="table table-striped table-hover table-bordered" id="tbl-UploadedForm-setup">
                                <thead>
                                    <tr>
                                        <th style="width: 25%">User Name</th>
                                        <th style="width: 25%">User Email</th>
                                        <th style="width: 25%">Submitted On</th>
                                        <th style="width: 25%">File</th>
                                    </tr>
                                </thead>
                                <tbody id="tbdUploadedFormList">
                                </tbody>
                            </table>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

    <script src="../assets/global/scripts/datatable.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/datatables/datatables.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js" type="text/javascript"></script>
    <script src="js/Global.js" type="text/javascript"></script>
    <script src="js/Forms.js" type="text/javascript"></script>
</asp:Content>
