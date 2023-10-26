<%@ Page Language="C#" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" AutoEventWireup="true" CodeBehind="InsiderPerson.aspx.cs" Inherits="ProcsDLL.InsiderTrading.InsiderPerson" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../assets/global/css/components.min.css" rel="stylesheet" />
    <link href="../assets/global/css/plugins.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/css/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <link href="../assets/layouts/layout/css/layout.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/layouts/layout/css/themes/darkblue.min.css" rel="stylesheet" type="text/css" id="style_color" />
    <link href="../assets/layouts/layout/css/custom.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/select2/css/select2-bootstrap.min.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .required {
            color: red;
        }
        .required-red {
            border-color: red !important;
        }
        .required-red-border {
            color: red !important;
            border: 1px solid red;
            border-color: red !important;
        }
        .select2-container--default.select2-container--focus .select2-selection--multiple {
            border: 1px solid red !important;
        }
        .select {
            width: 100%;
        }
        .m-0 {
            margin-bottom: 0;
            margin-top: 0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="portlet light bordered">
        <div class="portlet-title">
            <div class="caption font-red-sunglo">
                <i class="icon-settings font-red-sunglo"></i>
                <span class="caption-subject bold uppercase">Insider</span>
            </div>
        </div>
        <div class="portlet-body">
            <div class="table-toolbar">
                <div class="row">
                    <div class="col-md-6">
                        <div class="btn-group-devided">
                            <button id="btnAddupsi" onclick="fnClearForm();" runat="server" class="btn green" data-target="#GrpConnectecdPerson" data-toggle="modal">
                                Add Insider
                            </button>
                            &nbsp;
                            <button id="btnDownloadTemplate" onclick="fnDownloadTemplate();" runat="server" class="btn green">
                                Download Template
                            </button>
                            &nbsp;
                            <button id="btnUploadTemplate" class="btn green" data-target="#dvUploadCP" data-toggle="modal">
                                Upload Template
                            </button>
                            &nbsp;
                        </div>
                    </div>
                </div>
            </div>
            <table class="table table-striped table-hover table-bordered" id="tbl-upsilist-setup">
                <thead>
                    <tr>
                        <th>Firm</th>
                        <th>Name</th>
                        <th>Email</th>
                        <th>Identification</th>
                        <th>Identification #</th>
                        <th>Status</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody id="tbdInsiderList" runat="server">
                </tbody>
            </table>
        </div>
        <div class="modal fade" id="GrpConnectecdPerson" tabindex="-1" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-dialog-centered" style="width: 90% !important">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title caption-subject bold uppercase font-red-sunglo">UPSI Insider Person(s)</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnCloseModal();"></button>
                    </div>
                    <div class="modal-body">
                        <div class="form-group text-center">
                            <label id="lblupsiNonDesignatedM" style="text-align: left">
                                <span id="spnUPSIGrp"></span>
                            </label>

                        </div>
                        <div class="portlet light bordered">
                            <div class="portlet-body">
                                <div class="form-group required" id="dvMsg" style="display:none;text-align:center;">                                    
                                </div>
                                <div class="form-group">
                                    <table class="table" id="tblConnectedPerson">
                                        <thead>
                                            <tr>
                                                <th>Firm<span class="required">*</span></th>
                                                <th>Name<span class="required">*</span></th>
                                                <th>Email<span class="required">*</span></th>
                                                <th>Identification<span class="required">*</span></th>
                                                <th>Identification #<span class="required">*</span></th>
                                                <th></th>
                                            </tr>
                                        </thead>
                                        <tbody id="tbdCPAdd">
                                            <tr>
                                                <td style="margin: 5px;">
                                                    <input id="txtFirmNm" class="form-control form-control-inline" placeholder="Enter Firm Name" type="text" autocomplete="off" />
                                                </td>
                                                <td style="margin: 5px;">
                                                    <input id="txtCPNm" class="form-control form-control-inline" placeholder="Enter Connected Person" type="text" autocomplete="off" />
                                                </td>
                                                <td style="margin: 5px;">
                                                    <input id="txtCPEmail" class="form-control form-control-inline" placeholder="Enter Connected Person" type="text" autocomplete="off" />
                                                </td>
                                                <td style="margin: 5px;">
                                                    <select id="ddlCPIdentification" class="form-control">
                                                        <option value=""></option>
                                                        <option value="AADHAR CARD">AADHAR CARD</option>
                                                        <option value="DRIVING LICENSE">DRIVING LICENSE</option>
                                                        <option value="PAN">PAN</option>
                                                        <option value="PASSPORT">PASSPORT</option>
                                                        <option value="OTHER">OTHER</option>
                                                    </select>
                                                </td>
                                                <td style="margin: 5px;">
                                                    <input id="txtCPIdentificationNo" class="form-control form-control-inline" placeholder="Enter Connected Person" type="text" autocomplete="off" />
                                                </td>
                                                <td style="margin: 5px;">
                                                    <img onclick="javascript:fnAddCP();" src="images/icons/AddButton.png" height="24" width="24" />
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
						<div class="col-md-8" style="float:left !important;margin-left:-300px;">
							<span class="required">
								*As per SEBI PIT Regulations, Unique PAN/Identifier is required for each Connected Person
							</span>
						</div>
						<div class="col-md-4" style="float:right;">
                        <button id="btnSaveCP" type="button" class="btn green" onclick="javascript:fnSaveConnectedPerson();">Save Connected Person(s)</button>
                        <button id="btnCancel1CP" type="button" data-dismiss="modal" class="btn default">Cancel</button>
						</div>

                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="GrpEditCP" tabindex="-1" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title caption-subject bold uppercase font-red-sunglo">UPSI Insider Person(s)</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnCloseModal();"></button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-4" id="lblCPFirm" style="margin-top: 5px;">Firm</div>
                            <div class="col-md-6">
                                <input id="txtCPFirm" class="form-control"  type="text" />
                            </div>
                            <div class="col-md-2"></div>
                        </div><br />
                        <div class="row">
                            <div class="col-md-4" id="lblCPName" style="margin-top: 5px;">Connected Person Name</div>
                            <div class="col-md-6">
                                <input id="txtCPName" class="form-control"  type="text" />
                            </div>
                            <div class="col-md-2"></div>
                        </div><br />
                        <div class="row">
                            <div class="col-md-4" id="lblCPEmail" style="margin-top: 5px;">Connected Person Email</div>
                            <div class="col-md-6">
                                <input id="txtEditCPEmail" class="form-control"  type="text" disabled="disabled" />
                            </div>
                            <div class="col-md-2"></div>
                        </div><br />
                        <div class="row">
                            <div class="col-md-4" id="lblIdentificationTyp" style="margin-top: 5px;">Identification Type</div>
                            <div class="col-md-6">
                                <select id="ddlIdentificationTyp" class="form-control">
                                    <option value=""></option>
                                    <option value="AADHAR CARD">AADHAR CARD</option>
                                    <option value="DRIVING LICENSE">DRIVING LICENSE</option>
                                    <option value="PAN">PAN</option>
                                    <option value="PASSPORT">PASSPORT</option>
                                    <option value="OTHER">OTHER</option>
                                </select>
                            </div>
                            <div class="col-md-2"></div>
                        </div><br />
                        <div class="row">
                            <div class="col-md-4" id="lblIdentificationNo" style="margin-top: 5px;">Identification No.</div>
                            <div class="col-md-6">
                                <input id="txtIdentificationNo" class="form-control"  type="text" />
                            </div>
                            <div class="col-md-2"></div>
                        </div><br />
                        <div class="row">
                            <div class="col-md-4" id="lblStatus" style="margin-top: 5px;">Status</div>
                            <div class="col-md-6">
                                <select id="ddlStatus" class="form-control">
                                    <option value=''></option>
                                    <option value='Active'>Active</option>
                                    <option value='Inactive'>Inactive</option>
                                </select>
                            </div>
                            <div class="col-md-2"></div>
                        </div><br />
                    </div>
                    <div class="modal-footer">
                        <button id="btnUpdateCP" type="button" class="btn green" onclick="javascript:fnUpdateCP();">Update</button>
                        <button id="btnCancelCP" type="button" data-dismiss="modal" class="btn default" onclick="javascript:fnCloseEditCPModal();">Cancel</button>
                    </div>
                </div>
            </div>
        </div>
        <div id="dvUploadCP" class="modal fade" tabindex="-1" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                        <h4 class="modal-title">Upload Connected Persons</h4>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <input id="fuCPUploadFile" class="btn blue btn-block btn-outline btn-file" data-toggle="modal" data-tabindex="1" value="Browse" type="file" />
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" data-dismiss="modal" class="btn btn-outline dark" onclick="javascript:fnCloseUploadModal();" data-tabindex="2">Close</button>
                        <button id="btnSaveUpload" type="button" data-dismiss="modal" class="btn green" onclick="javascript:fnCPUpload();" data-tabindex="3">Upload</button>
                    </div>
                </div>
            </div>
        </div>



        <div class="modal fade" id="mdlException" tabindex="-1" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-dialog-centered" style="width: 90% !important">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnCloseModal();"></button>
                    </div>
                    <div class="modal-body">
                        <div class="portlet light bordered">
                            <div class="portlet-body">
                                <div class="form-group required" style="text-align:center;">
                                    The Excel has not been uploaded, please correct the following enteries and re-upload
                                </div>
                                <div id="dvException" class="form-group">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
						<button id="btnCancel1CP1" type="button" data-dismiss="modal" class="btn default">Cancel</button>
                    </div>
                </div>
            </div>
        </div>
        <script src="../assets/editor/jquery-te-1.4.0.min.js"></script>
        <script src="../assets/global/scripts/datatable.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/datatables/datatables.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js" type="text/javascript"></script>
        <script src="../assets/global/scripts/app.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/jquery-validation/js/jquery.validate.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/jquery-validation/js/additional-methods.min.js" type="text/javascript"></script>
        <script src="../assets/global/scripts/jquery-ui.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/js.cookie.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/jquery-validation/js/jquery.validate.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/jquery-validation/js/additional-methods.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/bootstrap-wizard/jquery.bootstrap.wizard.min.js" type="text/javascript"></script>
        <script src="js/Global.js?<%=DateTime.Now %>" type="text/javascript"></script>
        <script src="js/InsiderPerson.js?<%=DateTime.Now %>" type="text/javascript"></script>
        <script type="text/javascript">
            $("#Loader").hide();
        </script>
    </div>
</asp:Content>