<%@ Page Title="Send Welcome Email With Unique Password" Language="C#" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" AutoEventWireup="true" CodeBehind="Send_Welcome_Email_Password.aspx.cs" Inherits="ProcsDLL.InsiderTrading.Send_Welcome_Email_Password" ValidateRequest="false" %>
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
                                    <label>Upload User Manual</label>
                                    <asp:FileUpload ID="FileUpload1" CssClass="form-control" runat="server" />
                                </div>
                                <div class="col-md-6 margin-bottom-10 clearfix">
                                    <label>Ad/Saml User Tempalte</label>
                                    <asp:HiddenField ID="HiddenFieldAdTemplateId" runat="server" />
                                    <textarea id="TextareaAdTemplate" class="summernote" runat="server"></textarea>
                                </div>
                                <div class="col-md-6 margin-bottom-10">
                                    <label>Application User Tempalte</label>
                                    <asp:HiddenField ID="HiddenFieldApplicationTemplateId" runat="server" />
                                    <textarea id="TextareaAppTemplate" class="summernote" runat="server"></textarea>
                                </div>
                            </div>
                            <div class="navbar-fixed-bottom bg-white text-center" style="padding: 10px;">
                                <asp:LinkButton ID="LinkButtonSaveTemplate" runat="server" CssClass="btn blue" OnClick="LinkButtonSaveTemplate_Click">Save Changes</asp:LinkButton>&nbsp;
                                <button type="button" class="btn blue"  data-target="#ModalTestMail" data-toggle="modal">Send Test Mail</button>&nbsp;
                                <button type="button" class="btn red"  data-target="#ModalMailConfirmation" data-toggle="modal">Send Welcome Email</button>&nbsp;
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
                                    <asp:TextBox ID="TextBoxLoginId" CssClass="form-control" placeholder="'usera','userb'" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group margin-bottom-10 clearfix">
                                    <label>BCC Email</label>                                   
                                    <asp:TextBox ID="TextBoxBccEmail" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-actions text-right">
                            <asp:Button ID="ButtonSend" CssClass="btn blue" runat="server" Text="Send Welcome Email" OnClick="ButtonSend_OnClick" OnClientClick="javascript:fnloader()" />
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
                            <asp:LinkButton ID="LinkButtonSendTestMail" runat="server" CssClass="btn blue" OnClick="LinkButtonSendTestMail_Click">Send</asp:LinkButton>
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
                ]
            });
        });
        function fnloader() {
            $("#Loader").show();
        }
    </script>
</asp:Content>
