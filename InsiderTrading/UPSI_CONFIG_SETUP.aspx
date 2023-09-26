<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" CodeBehind="UPSI_CONFIG_SETUP.aspx.cs" Inherits="ProcsDLL.InsiderTrading.UPSI_CONFIG_SETUP" %>

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
        <div class="portlet light portlet-fit ">
            <div class="portlet-title">
                <div class="caption">
                    <i class="icon-settings font-red"></i>
                    <span class="caption-subject font-red sbold uppercase">UPSI CONFIGURATION</span>
                </div>
            </div>
            <div class="portlet-body">
                <form class="form-horizontal" role="form">
                    <div class="col-md-6">
                        <div class="col-md-offset-4 col-md-8">
                            <label class="radio-inline">
                                <input id="isPop" type="radio" name="emailReadingConfiguration" />POP</label>
                            <label class="radio-inline">
                                <input id="isImap" type="radio" name="emailReadingConfiguration" />IMAP</label>
                        </div>
                        <br />
                        <br />
                        <div class="form-body">
                            <div class="form-group">
                                <label class="col-md-3 control-label">DEFAULT EMAIL</label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="fa fa-envelope"></i>
                                            </span>
                                            <input id="txtDefaultEmailOutgoing" type="email" class="form-control" placeholder="Enter DEFAULT EMAIL" />
                                        </div>
                                    </div>
                                    <span class="help-inline"><i class="fa fa-info-circle" title="Inline help." aria-hidden="true"></i></span>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Default Email Display Name</label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="fa fa-envelope"></i>
                                            </span>
                                            <input id="txtContinualDisclosureEmailOutgoing" type="text" class="form-control" placeholder="Enter DISCLOSURE EMAIL" />
                                            <input class="form-control" id="txtSmtpConfigurationKey" type="text" style="display: none;" />
                                        </div>
                                    </div>
                                    <span class="help-inline"><i class="fa fa-info-circle" title="Inline help." aria-hidden="true"></i></span>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">SMTP HOST NAME</label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="fa fa-user"></i>
                                            </span>
                                            <input id="txtSmtpHostNameOutgoing" type="text" class="form-control" placeholder="Enter SMTP HOST NAME" />
                                        </div>
                                    </div>
                                    <span class="help-inline"><i class="fa fa-info-circle" title="Inline help." aria-hidden="true"></i></span>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">PORT</label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="fa fa-bell-o"></i>
                                            </span>
                                            <input id="txtPortNumberOutgoing" type="text" class="form-control number" placeholder="Enter PORT" />
                                        </div>
                                    </div>
                                    <span class="help-inline"><i class="fa fa-info-circle" title="Inline help." aria-hidden="true"></i></span>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">SSL</label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <select id="txtSslOutgoing" class="form-control">
                                            <option value="0">Select</option>
                                            <option value="Yes">Yes</option>
                                            <option value="No">No</option>
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">User Default Credential</label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <select id="txtUserDefaultCredentialOutgoing" class="form-control">
                                            <option value="0">Select</option>
                                            <option value="Yes">Yes</option>
                                            <option value="No">No</option>
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">SMTP USER NAME</label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="fa fa-user"></i>
                                            </span>
                                            <input id="txtSmtpUserNameOutgoing" class="form-control" type="text" placeholder="Enter SMTP USER NAME" autocomplete="off" />
                                        </div>
                                    </div>
                                    <span class="help-inline"><i class="fa fa-info-circle" title="Inline help." aria-hidden="true"></i></span>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">PASSWORD</label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="fa fa-bell-o"></i>
                                            </span>
                                            <input id="txtPasswordOutgoing" class="form-control" type="password" placeholder="Enter PASSWORD" autocomplete="off" />
                                        </div>
                                    </div>
                                    <span class="help-inline"><i class="fa fa-info-circle" title="Inline help." aria-hidden="true"></i></span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-actions">
                        <div class="row">
                            <div class="col-md-offset-3 col-md-9">
                                <button id="btnSave" type="button" class="btn green" onclick="javascript:fnSaveSmtpConfig();">Save</button>
                            </div>
                        </div>
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
    <%--End Datetime--%>
    <script src="../assets/global/scripts/datatable.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/datatables/datatables.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js" type="text/javascript"></script>
    <script src="../assets/global/scripts/app.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js" type="text/javascript"></script>
    <script src="../assets/pages/scripts/components-date-time-pickers.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/select2/js/select2.min.js" type="text/javascript"></script>
    <script src="js/Global.js?<%=DateTime.Now %>" type="text/javascript"></script>
    <script src="js/UPSIConfigSetup.js?<%=DateTime.Now %>" type="text/javascript"></script>
</asp:Content>


