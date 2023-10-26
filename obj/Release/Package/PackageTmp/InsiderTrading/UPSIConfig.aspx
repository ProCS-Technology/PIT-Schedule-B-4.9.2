<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" CodeBehind="UPSIConfig.aspx.cs" Inherits="ProcsDLL.InsiderTrading.UPSIConfig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../assets/global/css/components.min.css" rel="stylesheet" />
    <link href="../assets/global/css/plugins.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
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
                    <div class="form-group">
                        <div class="col-md-6">
                            <label class="control-label">Email Automation</label>
                        </div>
                        <div class="col-md-6">
                            <label class="radio-inline">
                                <input id="isAutomateYes" value="Yes" type="radio" name="radioAutomate" />Yes
                            </label>
                            <label class="radio-inline">
                                <input id="isAutomateNo" value="No" type="radio" name="radioAutomate" />No
                            </label>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-6">
                            <label class="control-label">Multiple Email(s) for different UPSI type</label>
                        </div>
                        <div class="col-md-6">
                            <div class="input-inline input-medium">
                                <div class="input-group">
                                    <label class="radio-inline">
                                        <input id="isMultipleYes" value="Yes" type="radio" name="radioMultipleEmail" />Yes
                                    </label>
                                    <label class="radio-inline">
                                        <input id="isMultipleNo" value="No" type="radio" name="radioMultipleEmail" />No
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-6">
                            <label class="control-label">All UPSI accessible to Compliance Officer</label>
                        </div>
                        <div class="col-md-6">
                            <div class="input-inline input-medium">
                                <div class="input-group">
                                    <label class="radio-inline">
                                        <input id="isUPSIYes" value="Yes" type="radio" name="radioUPSI" />Yes
                                    </label>
                                    <label class="radio-inline">
                                        <input id="isUPSINo" value="No" type="radio" name="radioUPSI" />No
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-6">
                            <label class="control-label">All UPSI Managed By Compliance Officer</label>
                        </div>
                        <div class="col-md-6">
                            <div class="input-inline input-medium">
                                <div class="input-group">
                                    <label class="radio-inline">
                                        <input id="isManagedYes" value="Yes" type="radio" name="radioManaged" onclick="fnManagedSelect(this);" />Yes
                                    </label>
                                    <label class="radio-inline">
                                        <input id="isManagedNo" value="No" type="radio" name="radioManaged" onclick="fnManagedSelect(this);" />No
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group" id="dvAuthorizeUsr" style="display:none;">
                        <div class="col-md-6">
                            <label class="control-label">Authorize User</label>
                        </div>
                        <div class="col-md-6">
                            <div class="input-inline input-medium">
                                <select id="ddlUsr" class="form-control">
                                    <%--<option value=""></option>
                                    <option value="AmitGarg">Amit Garg</option>
                                    <option value="AnilSharma">Anil Sharma</option>
                                    <option value="ManishJain">Manish Jain</option>
                                    <option value="MehakJain">Mehak Jain</option>
                                    <option value="SandeepJain">Sandeep Jain</option>
                                    <option value="SanjayJain">Sanjay Jain</option>
                                    <option value="SushmaJain">Sushma Jain</option>--%>
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-6">
                            <label class="control-label">Show Only Subject/Subject & Message/Subject, Message & Attachments to Compliance Officer</label>
                        </div>
                        <div class="col-md-6">
                            <div class="input-inline input-medium">
                                <div class="input-group">
                                    <label class="radio-inline">
                                        <input id="isSubject" value="Subject" type="radio" name="radioMessage" />Only Subject
                                    </label>
                                    <label class="radio-inline">
                                        <input id="isMessage" value="Message" type="radio" name="radioMessage" />Subject & Message
                                    </label><br />
                                    <label class="radio-inline">
                                        <input id="isMessagenAttachment" value="MessageAttachment" type="radio" name="radioMessage" />Subject, Message & Attachments
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div>
                        <div class="row">
                            <div class="col-md-offset-6 col-md-6">
                                <button id="btnSave" type="button" onclick="return saveUPSIConfig()" class="btn green">Save</button>
                            </div>
                        </div>
                    </div>
                </form>
            </div>

            <div class="modal fade in" id="modalSubmitConfirmation" style="z-index: 10000000" tabindex="-1" role="dialog" aria-hidden="True">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="True"></button>
                            <h4 class="modal-title">
                                <b>
                                    Do you want to assign all the current open UPSI task to the Authorized user?
                                </b>
                            </h4>
                        </div>
                        <div class="modal-footer">
                            <button id="btnSubmitNo" type="button" class="btn dark btn-outline" onclick="javascript:fnSubmitForms('No');">NO</button>
                            <button id="btnSubmitYes" class="btn red" data-dismiss="modal" value="YES" onclick="javascript:fnSubmitForms('Yes')">YES</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="../assets/global/scripts/app.min.js" type="text/javascript"></script>
    <script src="js/Global.js?<%=DateTime.Now %>" type="text/javascript"></script>
    <script src="js/UPSIConfig.js?<%=DateTime.Now %>" type="text/javascript"></script>
</asp:Content>