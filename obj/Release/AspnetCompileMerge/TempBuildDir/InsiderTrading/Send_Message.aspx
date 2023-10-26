<%@ Page Title="Send Email" Language="C#" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" AutoEventWireup="true" CodeBehind="Send_Message.aspx.cs" Inherits="ProcsDLL.InsiderTrading.Send_Message" ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../assets/global/css/components.min.css" rel="stylesheet" />
    <link href="../assets/global/css/plugins.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/bootstrap-summernote/summernote.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="Form1" runat="server">
        <div class="page-content-inner">
            <div class="page-content-inner">
                <div class="portlet light portlet-fit ">
                    
                    <div class="portlet-body slide-left">
                        <div class="table-toolbar">
                            <div class="margin-bottom-20">
                                <asp:Label ID="LabelMsg" runat="server" CssClass="text-danger" Text=""></asp:Label>
                            </div>
                            <div class="row">
                                <div class="col-md-12 margin-bottom-10 clearfix">
                                    <label>Email Subject</label>
                                    <asp:TextBox ID="TextBoxSubject" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-12 margin-bottom-10 clearfix">
                                    <label>Attachment</label>
                                    <asp:FileUpload ID="FileUpload1" CssClass="form-control" AllowMultiple="true" runat="server" />
                                </div>
                                
                                <div class="col-md-12 margin-bottom-10">
                                    <label>Email Body</label>
                                    <asp:HiddenField ID="HiddenFieldApplicationTemplateId" runat="server" />
                                    <textarea id="TextareaAppTemplate" class="summernote" runat="server"></textarea>
                                </div>
                            </div>
                            <div>
                                <button type="button" class="btn blue display-none"  data-target="#ModalTestMail" data-toggle="modal">Send Test Mail</button>&nbsp;
                                <button type="button" class="btn red"  data-target="#ModalMailConfirmation" data-toggle="modal">Send Email</button>&nbsp;
                                </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="ModalMailConfirmation" tabindex="-1" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title caption-subject bold uppercase font-red-sunglo">Send</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnClearTestMailForm();"></button>
                    </div>
                    <div class="modal-body">
                         <div class="portlet light bordered">
                            <div class="portlet-body form">
                                <div class="form-body">
                                    <div class="form-group margin-bottom-10 clearfix">
                                    <label>Login Id</label>                                    
                                    <asp:TextBox ID="TextBoxLoginId" CssClass="form-control" placeholder="enter comma seperated login id" runat="server"></asp:TextBox>
                                    <small>Please enter 'All' or comma seperated 'login id' to send mail to active user</small>
                                </div>
                                <div class="form-group margin-bottom-10 clearfix">
                                    <label>BCC Email</label>                                   
                                    <asp:TextBox ID="TextBoxBccEmail" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-actions text-right">
                            <asp:Button ID="ButtonSend" CssClass="btn blue" runat="server" Text="Send" OnClick="ButtonSend_OnClick" OnClientClick="javascript:fnloader()" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="ModalTestMail" tabindex="-1" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-dialog-centered" style="width: 40% !important">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title caption-subject bold uppercase font-red-sunglo">Test Mail</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnClearTestMailForm();"></button>
                    </div>
                    <div class="modal-body">
                         <div class="portlet light bordered">
                            <div class="portlet-body form">
                                <div class="form-body modal-fixheight">
                                    <input type="text" id="txtTestEmail" runat="server" placeholder="Please enter valid email" class="form-control" />
                                </div>
                            </div>
                        </div>
                        <div class="form-actions text-right">
                            <asp:LinkButton ID="LinkButtonSendTestMail" runat="server" CssClass="btn blue" OnClick="LinkButtonSendTestMail_Click"  OnClientClick="javascript:fnloader()">Send</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script src="../assets/global/plugins/bootstrap-summernote/summernote.min.js" type="text/javascript"></script>
    <script src="../assets/global/scripts/app.min.js" type="text/javascript"></script>
    <script src="js/Global.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.summernote').summernote({
                toolbar: [
                    ['style', ['style']],
                    ['font', ['bold', 'italic', 'underline', 'clear']],
                    ['fontname', ['fontname']],
                    ['color', ['color']],
                    ['para', ['ul', 'ol', 'paragraph']],
                    ['height', ['height']],
                    ['table', ['table']],
                    ['insert', ['link', 'hr']],
                    ['view', ['fullscreen', 'codeview']],
                    ['help', ['help']]
                ],
                height: 260
            });
        });
        function fnloader() {
            $("#Loader").show();
        }
    </script>
</asp:Content>
