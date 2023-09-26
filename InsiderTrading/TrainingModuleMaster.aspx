<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" CodeBehind="TrainingModuleMaster.aspx.cs" Inherits="ProcsDLL.InsiderTrading.TrainingModuleMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../assets/global/css/components.min.css" rel="stylesheet" />
    <link href="../assets/global/css/plugins.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/bootstrap-timepicker/css/bootstrap-timepicker.min.css" rel="stylesheet" />
    <%--End date --%>
    <link href="../assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/select2/css/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/select2/css/select2-bootstrap.min.css" rel="stylesheet" type="text/css" />
    <style>
        .lblrequired {
            color: red;
        }

        .requiredBackground {
            border-color: red !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-left: 0px!important; margin-right: 0px!important;">
        <div class="col-md-12" style="margin-top: 15px;">
            <div class="portlet light portlet-fit ">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="icon-settings font-red"></i>
                        <span class="caption-subject font-red sbold uppercase">Training Module</span>
                    </div>
                </div>
                <div class="portlet-body">
                    <div class="table-toolbar">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="btn-group-devided">
                                    <button id="btnAdd" class="btn green" data-target="#stack1" data-toggle="modal">
                                        Add New <i class="fa fa-plus"></i>
                                    </button>
                                    &nbsp;
                                </div>
                            </div>
                        </div>
                    </div>
                    <table class="table table-striped table-hover display text-nowrap table-bordered" id="tbl-training-setup">
                        <thead>
                            <tr>
                                <th>SEQUENCE</th>
                                <th>TRAINING NAME</th>
                                <th>START DATE</th>
                                <th>END DATE</th>
                                <th>CREATED BY</th>
                                <th>ACTION</th>
                            </tr>
                        </thead>
                        <tbody id="tbdTrainingList">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade bs-modal-lg" id="trainingModel" tabindex="-1" aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="fnClearValidateTrainingModal();"></button>
                    <h4 class="modal-title caption-subject bold uppercase font-red-sunglo"><span id="spnTitle">Add/Update Item</span></h4>
                </div>
                <form class="form-horizontal" role="form">
                    <div class="modal-body">
                        <div class="portlet light bordered">
                            <div class="portlet-body form">

                                <div class="form-body">
                                    <div class="form-group">
                                        <label id="lblTrainingName" style="text-align: left" class="col-md-4 control-label">Training Name<span class="required"> * </span></label>
                                        <div class="col-md-8">
                                            <input id="txtTrainingName" type="text" class="form-control" placeholder="Enter Training Name" onkeypress="javascript:fnRemoveClass(this,'TrainingName','txtTrainingName');" autocomplete="off" />
                                            <input id="txtTrainingId" type="hidden" value="0" />
                                        </div>
                                    </div>
                                    <br />
                                    <div class="form-group">
                                        <label id="lblTrainingStartDate" style="text-align: left" class="col-md-4 control-label">Training Start Date<span class="required"> * </span></label>
                                        <div class="col-md-8">
                                            <input id="txtTrainingStartDate" type="text" data-date-format="dd/mm/yyyy" class="form-control date-picker" placeholder="Enter Training Start Date" onchange="javascript:fnRemoveClass(this,'TrainingStartDate','txtTrainingStartDate');" autocomplete="off" />
                                        </div>
                                    </div>
                                    <br />
                                    <div class="form-group">
                                        <label id="lblTrainingEndDate" style="text-align: left" class="col-md-4 control-label">Training End Date<span class="required"> * </span></label>
                                        <div class="col-md-8">
                                            <input id="txtTrainingEndtDate" type="text" data-date-format="dd/mm/yyyy" class="form-control date-picker" placeholder="Enter Training End Date" onchange="javascript:fnRemoveClass(this,'TrainingEndDate','txtTrainingEndtDate');" autocomplete="off" />
                                        </div>
                                    </div>
                                    <br />
                                    <div class="form-group">
                                        <div class="fileinput fileinput-new" data-provides="fileinput">
                                            <label id="lblUpload" class="col-md-4 control-label">Upload Training Module<span class="required"> * </span></label>
                                            <div class="col-md-8">
                                                <div class="btn default btn-file" style="min-width: 350px; max-width: 350px">
                                                    <input id="fileUploadImage" type="file" name="..." onchange="javascript:fnRemoveClass(this,'Upload','fileUploadImage');" />
                                                </div>
                                                
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button id="btnSave" type="button" class="btn green" onclick="javascript:fnSaveTrainingModule();">Save</button>
                        <button id="btnCancel" type="button" data-dismiss="modal" class="btn default" onclick="fnClearValidateTrainingModal();">Cancel</button>
                    </div>
                </form>
            </div>
        </div>
    </div>

    <div class="modal fade bs-modal-lg" id="trainingModelVideo" tabindex="-1" aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="fnClearValidateItemModalVideo();"></button>
                    <h4 class="modal-title caption-subject bold uppercase font-red-sunglo"><span id="spnTitleVideo">Add/Update Item</span></h4>
                </div>
                <form class="form-horizontal" role="form">
                    <div class="modal-body">
                        <div class="portlet light bordered">
                            <div class="portlet-body form">
                                <div class="form-body">
                                    <div class="form-group">
                                        <label id="lblTrainingNameVideo" style="text-align: left" class="col-md-4 control-label">Training Name<span class="required"> * </span></label>
                                        <div class="col-md-8">
                                            <input id="txtTrainingNameVideo" type="text" class="form-control" placeholder="Enter Training Name" onkeypress="javascript:fnRemoveClass(this,'TrainingNameVideo','txtTrainingNameVideo');" autocomplete="off" />
                                            <input id="txtTrainingIdVideo" type="hidden" value="0" />
                                        </div>
                                    </div>
                                    <br />
                                    <div class="form-group">
                                        <label id="lblTrainingStartDateVideo" style="text-align: left" class="col-md-4 control-label">Training Start Date<span class="required"> * </span></label>
                                        <div class="col-md-8">
                                            <input id="txtTrainingStartDateVideo" type="text" data-date-format="dd/mm/yyyy" class="form-control date-picker" placeholder="Enter Training Start Date" onchange="javascript:fnRemoveClass(this,'TrainingStartDateVideo','txtTrainingStartDateVideo');" autocomplete="off" />
                                        </div>
                                    </div>
                                    <br />
                                    <div class="form-group">
                                        <label id="lblTrainingEndDateVideo" style="text-align: left" class="col-md-4 control-label">Training End Date<span class="required"> * </span></label>
                                        <div class="col-md-8">
                                            <input id="txtTrainingEndtDateVideo" type="text" data-date-format="dd/mm/yyyy" class="form-control date-picker" placeholder="Enter Training End Date" onchange="javascript:fnRemoveClass(this,'TrainingEndDateVideo','txtTrainingEndtDateVideo');" autocomplete="off" />
                                        </div>
                                    </div>
                                    <br />
                                    <br />
                                    <div class="row">
                                        <table class="table" id="addvideo">
                                            <thead>
                                                <tr>
                                                    <th>Video Name</th>
                                                    <th>Sequence</th>
                                                </tr>
                                            </thead>
                                            <tbody id="tbodyVideo"></tbody>
                                        </table>
                                    </div>
                                </div>


                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button id="btnSaveVideo" type="button" class="btn green" onclick="javascript:fnSaveTrainingModuleVideo();">Save</button>
                        <button id="btnCancelVideo" type="button" data-dismiss="modal" class="btn default" onclick="fnClearValidateItemModalVideo();">Cancel</button>
                    </div>
                </form>
            </div>
        </div>
    </div>

    <div class="modal fade" id="stack1" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title"><b>Upload Type</b></h4>
                </div>
                <div class="modal-body">
                    <div class="portlet" style="margin-bottom: 0;">
                        <div class="portlet-body" id="chats">
                            <div class="row">
                                <div class="col-md-3">
                                    <b>Type <span style="color: red">*</span></b>
                                </div>
                                <div class="col-md-9">
                                    <select class="form-control" id="ddlUploadType" data-tabindex="1" onchange="fnSelectedUploadType()">
                                        <option value="">Select Type</option>
                                        <option value="File">Document</option>
                                        <option value="Video">Audio/Video</option>
                                    </select>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn dark btn-outline" data-dismiss="modal">Close</button>
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
    <%--End Datetime--%>

    <script src="../assets/global/scripts/datatable.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/datatables/datatables.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js" type="text/javascript"></script>
    <script src="../assets/global/scripts/app.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js" type="text/javascript"></script>
    <script src="../assets/pages/scripts/components-date-time-pickers.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/select2/js/select2.full.min.js" type="text/javascript"></script>
    <script src="../assets/pages/scripts/components-select2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="js/Global.js?<%=DateTime.Now %>"></script>
    <script type="text/javascript" src="js/TrainingModuleMaster.js?<%=DateTime.Now %>"></script>
</asp:Content>
