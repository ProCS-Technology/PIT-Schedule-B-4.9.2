<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" CodeBehind="TradingWindowClosure.aspx.cs" Inherits="ProcsDLL.InsiderTrading.TradingWindowClosure" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../assets/global/css/components.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/css/plugins.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/bootstrap-timepicker/css/bootstrap-timepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/select2/css/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/select2/css/select2-bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/bootstrap-summernote/summernote.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/select2/css/select2-bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/bootstrap-multiselect/css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
    <!-- BEGIN PAGE LEVEL PLUGINS -->
    <link href="../assets/global/plugins/jstree/dist/themes/default/style.min.css" rel="stylesheet" type="text/css" />
    <!-- END PAGE LEVEL PLUGINS -->
    <link href="https://www.jqueryscript.net/css/jquerysctipttop.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/css/components.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/css/plugins.min.css" rel="stylesheet" type="text/css" />
    <style>
        .required {
            color: red;
        }
        .requiredBackground {
            border-color: red !important;
        }
        .modal-body.modal-auto {
            overflow-y: unset !important;
        }
    </style>
    <style>
        ul.tree, ul.tree * {
            list-style-type: none;
            margin: 0;
            padding: 0 0 5px 0;
        }
            ul.tree img.arrow {
                padding: 2px 0 0 0;
                border: 0;
                width: 20px;
            }
            ul.tree li {
                padding: 4px 0 0 0;
            }
                ul.tree li ul {
                    padding: 0 0 0 20px;
                    margin: 0;
                }
            ul.tree label {
                cursor: pointer;
                font-weight: bold;
                padding: 2px 0;
            }
                ul.tree label.hover {
                    color: red;
                }
            ul.tree li .arrow {
                width: 20px;
                height: 18px;
                padding: 0;
                margin: 0;
                cursor: pointer;
                float: left;
                background: transparent no-repeat 0 0px;
            }
            ul.tree li .collapsed {
                background-image: url(../images/right.svg);
            }
            ul.tree li .expanded {
                background-image: url(../images/down.svg);
            }
            ul.tree li .checkbox {
                width: 20px;
                height: 18px;
                padding: 0;
                margin: 0;
                cursor: pointer;
                float: left;
                background: url(../images/square.svg)no-repeat 0 0px;
            }
            ul.tree li .checked {
                background-image: url(../images/check.svg);
            }
            ul.tree li .half_checked {
                background-image: url(../images/square-minus.svg);
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-left: 0px!important; margin-right: 0px!important;">
        <div class="col-md-12">
            <div class="portlet light portlet-fit">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="icon-settings font-red"></i>
                        <span class="caption-subject font-red sbold uppercase">Trading Window Closure</span>
                    </div>
                </div>
                <div class="portlet-body">
                    <div class="table-toolbar">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="btn-group-devided">
                                    <button id="btnAdd" runat="server" class="btn green" onclick="fnOpenTradingWindowClosureModule()">
                                        Add New <i class="fa fa-plus"></i>
                                    </button>
                                    &nbsp;
                                </div>
                            </div>
                        </div>
                    </div>
                    <table class="table table-striped table-hover table-bordered" id="tbl-tradingWindow-setup">
                        <thead>
                            <tr>
                                <th>Type</th>
                                <th>FROM</th>
                                <th>TO</th>
                                <th>REMARKS</th>
                                <th>ACTIONS</th>
                            </tr>
                        </thead>
                        <tbody id="tbdTradingWindowList"></tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade bs-modal-bg" id="tradingWindowClosureModel" tabindex="-1" aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnRollBack();"></button>
                    <h4 class="modal-title caption-subject bold uppercase font-red-sunglo">Trading Window Closure</h4>
                </div>
                <%--<form class="form-horizontal" runat="server" role="form">--%>
                    <div class="modal-body">
                        <div class="portlet light bordered">
                            <div class="portlet-body form">
                                <div class="form-body">
                                    <div class="form-group">
                                        <label id="lblWindowClosureType" style="text-align: left" class="col-md-4 control-label">Type <span class="required">* </span></label>
                                        <div class="col-md-8">
                                            <div class="input-group">
                                                <span class="input-group-addon">
                                                    <i class="fa fa-envelope"></i>
                                                </span>
                                                <select id="ddlWindowClosureType" class="form-control form-control-inline" runat="server" onchange="javascript:fnRemoveClass(this,'WindowClosureType','ddlWindowClosureType')">
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="form-group">
                                        <label id="lblFrom" style="text-align: left" class="col-md-4 control-label">From <span class="required">* </span></label>
                                        <div class="col-md-8">
                                            <div class="input-group">
                                                <span class="input-group-addon">
                                                    <i class="fa fa-envelope"></i>
                                                </span>
                                                <input id="txtTradingWindowFrom" class="form-control form-control-inline datepicker" onchange="javascript:fnRemoveClass(this,'From','txtTradingWindowFrom');" type="text" value="" autocomplete="off" />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="form-group">
                                        <label id="lblTo" style="text-align: left" class="col-md-4 control-label">To</label>
                                        <div class="col-md-8">
                                            <div class="input-group">
                                                <span class="input-group-addon">
                                                    <i class="fa fa-envelope"></i>
                                                </span>
                                                <input id="txtTradingWindowTo" class="form-control form-control-inline datepicker" onchange="javascript:fnRemoveClass(this,'To','txtTradingWindowTo');" type="text" value="" autocomplete="off" />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="form-group" style="display: none;">
                                        <label id="lblBoardMeetingScheduledOn" style="text-align: left" class="col-md-4 control-label">Board Meeting Scheduled On</label>
                                        <div class="col-md-8">
                                            <div class="input-group">
                                                <span class="input-group-addon">
                                                    <i class="fa fa-envelope"></i>
                                                </span>
                                                <input id="txtBoardMeetingScheduledOn" class="form-control form-control-inline date-picker" onchange="javascript:fnRemoveClass(this,'BoardMeetingScheduledOn','txtBoardMeetingScheduledOn');" type="text" value="" autocomplete="off" />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="form-group" style="display: none;">
                                        <label id="lblQuarterEndedOn" style="text-align: left" class="col-md-4 control-label">Quarter Ended On</label>
                                        <div class="col-md-8">
                                            <div class="input-group">
                                                <span class="input-group-addon">
                                                    <i class="fa fa-envelope"></i>
                                                </span>
                                                <input id="txtQuarterEndedOn" class="form-control form-control-inline date-picker" onchange="javascript:fnRemoveClass(this,'QuarterEndedOn','txtQuarterEndedOn');" type="text" value="" autocomplete="off" />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="form-group">
                                        <label id="lblRemarks" style="text-align: left" class="col-md-4 control-label">Remarks<span class="required"> * </span></label>
                                        <div class="col-md-8">
                                            <textarea class="form-control" id="txtTradingWindowRemarks" onchange="javascript:fnRemoveClass(this,'Remarks','txtTradingWindowRemarks');"></textarea>
                                            <input type="hidden" id="txtTradingWindowId" value="0" />
                                        </div>
                                    </div>
                                    <%--<div class="form-group">
                                        <label id="lblEmailTemplate" style="text-align:left" class="col-md-4 control-label">Email<span class="required"> * </span></label>
                                        <div class="col-md-8">
                                            <div name="summernote" id="summernote_1"> </div>
                                        </div>
                                    </div>--%>
                                </div>

                            </div>
                        </div>

                    </div>
                    <div class="modal-footer">
                        <button id="btnSaveTradingWindow" type="button" class="btn green" onclick="javascript:fnSaveTradingWindow();">Save</button>
                        <button id="btnCancel" type="button" data-dismiss="modal" class="btn default" onclick="javascript:fnRollBack();">Cancel</button>
                    </div>
                <%--</form>--%>
            </div>
        </div>
    </div>

    <div class="modal fade bs-modal-bg" id="tradingWindowClosureMail" tabindex="-1" aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnRollBack();"></button>
                    <h4 class="modal-title caption-subject bold uppercase font-red-sunglo">Trading Window Closure Mail</h4>
                </div>
                <div class="modal-body">
                    <div class="portlet light bordered">
                        <div class="portlet-body form">

                            <div class="form-body">
                                <div class="form-group">
                                    <label id="lblEmailSubject" style="text-align: left">Subject<span class="required"> * </span></label>
                                    <input type="text" id="txtSubject" class="form-control" value="Trading Window Notification" />
                                </div>
                                <div class="form-group">
                                    <input type="text" id="txtTWCId" style="display: none;" />
                                    <label id="lblEmailTemplate" style="text-align: left">Email<span class="required"> * </span></label>
                                    <div style="font-weight: normal !important; font-family: Trebuchet MS !important;" name="summernote" id="summernote_1"></div>
                                </div>
                                <div class="clearfix"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button id="btnMailTradingWindow" type="button" class="btn green" data-toggle="modal" data-target="#modalUserSelection">Click to select user(s)</button>
                    <button id="btnMailCancel" type="button" data-dismiss="modal" class="btn default">Cancel</button>
                </div>
            </div>
        </div>
    </div>


    <div class="modal fade" id="modalUserSelection" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnCloseModal();"></button>
                    <h4 class="modal-title">User Selection</h4>
                </div>
                
                <form class="form-horizontal" runat="server" role="form" enctype="multipart/form-data">
                    <asp:ScriptManager ID="scriptManager1" runat="server"></asp:ScriptManager>
                    <asp:UpdatePanel ID="updatepanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox ID="hdnTradingWindowId" runat="server" Text="" style="display:none;" />
                            <asp:TextBox ID="hdnEmailSubject" runat="server" Text="" style="display:none;" />
                            <asp:TextBox ID="hdnEmailTemplate" runat="server" Text="" style="display:none;" />
                            <asp:TextBox ID="hdnUsers" runat="server" Text="" style="display:none;" />
                            <asp:TextBox ID="hdnMailTo" runat="server" Text="" style="display:none;" />
                            <asp:TextBox ID="hdnEmailTask" runat="server" style="display:none;" />
                            <div class="form-body">
                                <div class="form-group">
                                    <ul class="tree" style="margin-left:10px;">
                                        <li>
                                            <input type="checkbox" id="chkRelatives" />
                                            <label>Relatives</label>
                                            <ul>
                                                <asp:DataList ID="Datalist1" runat="server" RepeatDirection="Vertical" RepeatLayout="Table">
                                                    <ItemTemplate>
                                                        <li>
                                                            <input type="checkbox" class="cbCheck" name="TreeSelect" value='<%#Eval("RELATIVE_EMAIL") %>' />
                                                            <asp:Label ID="Itmnamelbl" Style="display:none" runat="server" Text='<%#Eval("RELATIVE_EMAIL") %>'></asp:Label>
                                                            <asp:Label ID="Label6" runat="server" Text='<%#Eval("RELATIVE_NAME") %>'></asp:Label>
                                                        </li>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </ul>
                                        </li>
                                        <li style="display:none">
                                            <input type="checkbox" id="chkCP" />
                                            <label>Connected persons</label>
                                            <ul>
                                                <asp:DataList ID="ddlConnected" runat="server" RepeatDirection="Vertical" RepeatLayout="Table">
                                                    <ItemTemplate>
                                                        <li>
                                                            <input type="checkbox" class="cbCheck" name="TreeSelect" value='<%#Eval("CP_EMAIL") %>' />
                                                            <asp:Label ID="Label5" Style="display: none" runat="server" Text='<%#Eval("CP_EMAIL") %>'></asp:Label>
                                                            <asp:Label ID="Label6" runat="server" Text='<%#Eval("CP_NAME") %>'></asp:Label>
                                                        </li>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </ul>
                                        </li>
                                        <asp:DataList ID="ddlRole" runat="server" RepeatDirection="Vertical">
                                            <ItemTemplate>
                                                <li>
                                                    <input type="checkbox" />
                                                    <asp:Label ID="Itmnamelbl1" runat="server" Text='<%#Eval("ROLE_NAME") %>'></asp:Label>
                                                    <ul>
                                                        <asp:DataList ID="ddlCP" runat="server" RepeatDirection="Vertical">
                                                            <ItemTemplate>
                                                                <li>
                                                                    <input type="checkbox" class="cbCheck" name="TreeSelect" value='<%#Eval("USER_EMAIL") %>' />
                                                                    <asp:Label ID="Label1" Style="display: none" runat="server" Text='<%#Eval("USER_EMAIL") %>'></asp:Label>
                                                                    <asp:Label ID="Label3" runat="server" Text='<%#Eval("USER_NM") %>'></asp:Label>
                                                                </li>
                                                            </ItemTemplate>
                                                        </asp:DataList>
                                                        <asp:DataList ID="ddlConnected" runat="server" RepeatDirection="Vertical">
                                                            <ItemTemplate>
                                                                <li>
                                                                    <input type="checkbox" class="cbCheck" value='<%#Eval("CP_EMAIL") %>' />
                                                                    <asp:Label ID="Label2" Style="display: none" runat="server" Text='<%#Eval("CP_EMAIL") %>'></asp:Label>
                                                                    <asp:Label ID="Label4" runat="server" Text='<%#Eval("CP_NAME") %>'></asp:Label>
                                                                </li>
                                                            </ItemTemplate>
                                                        </asp:DataList>
                                                    </ul>
                                                </li>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </ul>
                                </div>
                                <div class="form-group" style="margin-left: 15px;">
                                    <label>Attachment</label>
                                    <asp:FileUpload ID="txtAttachment" runat="server" />
                                    <%--<input id="txtAttachment" type="file" runat="server" NAME="txtAttachment">--%>
                                    <%--<input type="file" id="txtAttachment" />--%>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" data-dismiss="modal" class="btn btn-outline dark" onclick="javascript:fnCloseSendEmailTradingWindowModal();">Close</button>
                                <%--<button id="btnSendEmailTradingWindow" type="button" data-dismiss="modal" class="btn green" onclick="javascript:fnMailTradingWindow();">Submit</button>--%>
                                <asp:Button ID="btnSendEmailTradingWindow" runat="server" CssClass="btn green" Text="Submit"
                                    OnClientClick="return fnMailTradingWindow();" OnClick="btnSendEmailTradingWindow_Click"  />
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnSendEmailTradingWindow" />
                        </Triggers>
                    </asp:UpdatePanel>
                </form>
            </div>
        </div>
    </div>
    <script src="../assets/editor/jquery-te-1.4.0.min.js" type="application/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js" type="application/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.js" type="application/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js" type="application/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-timepicker/js/bootstrap-timepicker.min.js" type="application/javascript"></script>
    <script src="../assets/pages/scripts/components-date-time-pickers.min.js" type="application/javascript"></script>
    <script src="../assets/global/scripts/datatable.js" type="application/javascript"></script>
    <script src="../assets/global/plugins/datatables/datatables.min.js" type="application/javascript"></script>
    <script src="../assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js" type="application/javascript"></script>
    <%--<script src="../assets/global/scripts/app.min.js" type="application/javascript"></script>--%>
    <script src="../assets/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js" type="application/javascript"></script>
    <script src="../assets/pages/scripts/components-date-time-pickers.min.js" type="application/javascript"></script>
    <script src="../assets/global/plugins/select2/js/select2.min.js" type="application/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-summernote/summernote.min.js" type="text/javascript"></script>
    <script src="../assets/pages/scripts/components-editors.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/select2/js/select2.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-multiselect/js/bootstrap-multiselect.js" type="text/javascript"></script>
    <script src="../assets/pages/scripts/components-bootstrap-multiselect.js" type="text/javascript"></script>
    <script src="../assets/global/scripts/app.min.js" type="text/javascript"></script>
    <script src="../assets/pages/scripts/jquery.checktree.js" type="text/javascript"></script>
    <script src="js/Global.js" type="application/javascript"></script>
    <script src="js/TradingWindowClosure.js?<%=DateTime.Now %>" type="application/javascript"></script>
    <script>
        $(function () {
            $('ul.tree').checkTree({

            });
        });        
    </script>
</asp:Content>