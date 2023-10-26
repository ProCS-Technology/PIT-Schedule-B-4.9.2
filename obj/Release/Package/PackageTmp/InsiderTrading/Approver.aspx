<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" CodeBehind="Approver.aspx.cs" Inherits="ProcsDLL.InsiderTrading.Approver" %>

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
    <link href="../assets/global/plugins/bootstrap-multiselect/css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .requied {
            color: red;
        }
        .multiselect-container li {
            margin-left: 10px !important;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-content-inner">
        <div class="col-md-12" style="overflow-y: auto; overflow-x: auto; padding-left: 0px; padding-right: 0px;">
            <div class="portlet light portlet-fit " style="min-height: 525px;">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="icon-settings font-red"></i>
                        <span class="caption-subject font-red sbold uppercase">APPROVER</span>
                    </div>
                </div>
                <div class="portlet-body">
                    <form class="form-horizontal" role="form">
                        <div class="form-body">
                            <div class="form-group">
                                <label id="lblUser" class="col-md-3 control-label"><b>Approving Authority</b></label>
                                <div class="col-md-9">
                                    <select id="ddlUserList" class="form-control select2" onchange="javascript:fnRemoveClass(this,'User');">
                                        <option value="0">Please Select</option>
                                    </select>
                                </div>
                            </div>
                        </div>
                        <div class="form-actions">
                            <div class="row">
                                <div class="col-md-offset-3 col-md-9">
                                    <button id="btnSave" type="submit" class="btn green" onclick="javascript:fnSaveAuthorizedPerson();">Save</button>
                                    <button type="button" class="btn default" onclick="javascript:fnRollBack()">Cancel</button>
                                </div>
                            </div>
                        </div>
                    </form>
                    <br />
                    <br />

                    
                    <form class="form-horizontal" role="form">
                        <div class="form-body">
                            <div class="form-group">
                                <label id="lblUserForCO" class="col-md-3 control-label"><b>Approving Authority For Compliance Officer</b></label>
                                <div class="col-md-9">
                                    <select id="ddlUserListForCOApprover" class="form-control select2" onchange="javascript:fnRemoveClass(this,'UserForCO');">
                                        <option value="0">Please Select</option>
                                    </select>
                                </div>
                            </div>
                        </div>
                        <div class="form-actions">
                            <div class="row">
                                <div class="col-md-offset-3 col-md-9">
                                    <button id="btnSaveCO" type="submit" class="btn green" onclick="javascript:fnSaveAuthorizedPersonCO();">Save</button>
                                    <button type="button" class="btn default" onclick="javascript:fnRollBack()">Cancel</button>
                                </div>
                            </div>
                        </div>
                    </form>
                    <br />
                    <br />

                    <form class="form-horizontal" role="form">
                        <div class="form-body">
                            <div class="form-group">
                                <label id="lblDepositoryType" class="col-md-3 control-label"><b>Depository Type</b></label>
                                <div class="col-md-9">
                                    <select id="depositoryType" class="mt-multiselect btn btn-default" multiple="multiple" data-label="left" data-select-all="true" data-width="100%" data-filter="true" data-action-onchange="true">
                                        <option value="CDSL">CDSL</option>
                                        <option value="NSDL">NSDL</option>
                                    </select>
                                </div>
                            </div>
                        </div>
                        <div class="form-actions">
                            <div class="row">
                                <div class="col-md-offset-3 col-md-9">
                                    <button id="btnSaveDepositoryType" type="submit" class="btn green" onclick="javascript:fnSaveDepositoryType();">Save</button>
                                    <button type="button" class="btn default" onclick="javascript:fnRollBack()">Cancel</button>
                                </div>
                            </div>
                        </div>
                    </form>
                    <br />
                    <br />


                    <form id="frmThresholdSettings" class="form-horizontal" style="display: none" role="form">
                        <div class="form-body">
                            <div class="form-group">
                                <label id="" class="col-md-3 control-label"><b>Number of shares in depository</b></label>
                                <div class="col-md-9">
                                    <table id="addNumberOfShares">
                                        <tbody id="tbodyNumberOfShares">
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <br />
                            <div class="form-group">
                                <label id="" class="col-md-3 control-label">
                                    <b>Threshold Limit</b>
                                    <div class="mt-radio-inline">
                                        <label class="mt-radio">
                                            <input type="radio" name="optionsLimitType" id="optionsQty" value="Quantity" />Quantity
                                            <span></span>
                                        </label>
                                        <label class="mt-radio">
                                            <input type="radio" name="optionsLimitType" id="optionsVal" value="Value" />Value
                                            <span></span>
                                        </label>
                                    </div>
                                </label>
                                <div class="col-md-9">
                                    <table id="addThresholdLimit">
                                        <tbody id="tbodyThresholdLimit">
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="form-actions">
                            <div class="row">
                                <div class="col-md-offset-3 col-md-9">
                                    <button id="btnSaveThresholdLimitAndByTime" type="button" class="btn green" onclick="javascript:fnSaveThresholdLimitAndByTime();">Save</button>
                                    <button type="button" class="btn default" onclick="javascript:fnRollBack()">Cancel</button>
                                </div>
                            </div>
                        </div>
                    </form>
                    <br />
                    <br />

                    <form id="frmCompanyNmAndISIN" class="form-horizontal" role="form">
                        <div class="form-body">
                            <div class="form-group">
                                <label id="lblTotalPhysicalShares" class="col-md-3 control-label"><b>Total Physical Shares</b></label>
                                <div class="col-md-9">
                                    <input id="txtTotalPhysicalShares" type="number" class="form-control form-control-inline" value="" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label id="" class="col-md-3 control-label"><b>Company Name</b></label>
                                <div class="col-md-9">
                                    <input id="txtCompanyName" type="text" class="form-control form-control-inline" value="" />
                                </div>
                            </div>
                            <br />
                            <div class="form-group">
                                <label id="" class="col-md-3 control-label"><b>Company ISIN</b></label>
                                <div class="col-md-9">
                                    <input id="txtCompanyISIN" type="text" class="form-control form-control-inline" value="" />
                                </div>
                            </div>
                        </div>
                        <div class="form-actions">
                            <div class="row">
                                <div class="col-md-offset-3 col-md-9">
                                    <button id="btCompanyNmISIN" type="submit" class="btn green" onclick="javascript:fnSaveCompanyNameAndISIN();">Save</button>
                                    <button type="button" class="btn default" onclick="javascript:fnRollBack()">Cancel</button>
                                </div>
                            </div>
                        </div>
                    </form>
                    <%--<br />
                    <br />

                    <form class="form-horizontal" role="form">
                        <div class="form-body">
                            <div class="form-group">
                                <label class="col-md-offset-6 col-md-3"><b>Declaration Frequency</b></label>
                            </div>
                            <div class="form-group">
                                <label id="lblPeriodicDeclarationDate" class="col-md-3 control-label"><b>Date</b></label>
                                <div class="col-md-9">
                                    <input id="txtPeriodicDeclarationDate" data-date-format="dd/mm/yyyy" type="text" class="form-control form-control-inline date-picker" value="" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label id="" class="col-md-3 control-label"><b>Frequency(in Months)</b></label>
                                <div class="col-md-9">
                                    <input id="txtPeriodicDeclarationFrequencyInMonths" class="form-control form-control-inline" oninput="javascript: if (this.value.length > this.maxLength) this.value = this.value.slice(0, this.maxLength);" maxlength="2" type="number" value="" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label id="" class="col-md-3 control-label"><b>Valid Till(in Days)</b></label>
                                <div class="col-md-9">
                                    <input id="txtPeriodicDeclarationValidTill" class="form-control form-control-inline" oninput="javascript: if (this.value.length > this.maxLength) this.value = this.value.slice(0, this.maxLength);" maxlength="3" type="number" value="" />
                                </div>
                            </div>
                        </div>
                        <div class="form-actions">
                            <div class="row">
                                <div class="col-md-offset-3 col-md-9">
                                    <button id="btnSavePeriodicDeclaration" type="submit" class="btn green" onclick="javascript:fnSaveTaskForPeriodicDeclaration();">Save</button>
                                    <button type="button" class="btn default" onclick="javascript:fnRollBack()">Cancel</button>
                                </div>
                            </div>
                        </div>
                    </form>--%>

                </div>
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
    <script src="../assets/global/plugins/bootstrap-multiselect/js/bootstrap-multiselect.js" type="text/javascript"></script>
    <script src="../assets/pages/scripts/components-bootstrap-multiselect.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/select2/js/select2.full.min.js" type="text/javascript"></script>
    <script src="../assets/pages/scripts/components-select2.min.js" type="text/javascript"></script>
    <script src="js/Global.js?<%=DateTime.Now %>" type="text/javascript"></script>
    <script src="js/AuthorizedPerson.js?<%=DateTime.Now %>" type="text/javascript"></script>
</asp:Content>