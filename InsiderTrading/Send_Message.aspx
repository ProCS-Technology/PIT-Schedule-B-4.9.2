<%@ Page Title="Send Email" Language="C#" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" AutoEventWireup="true" CodeBehind="Send_Message.aspx.cs" Inherits="ProcsDLL.InsiderTrading.Send_Message" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../assets/global/css/components.min.css" rel="stylesheet" />
    <link href="../assets/global/css/plugins.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/bootstrap-summernote/summernote.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/select2/css/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/select2/css/select2-bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/bootstrap-multiselect/css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
    <!-- BEGIN PAGE LEVEL PLUGINS -->
    <link href="../assets/global/plugins/jstree/dist/themes/default/style.min.css" rel="stylesheet" type="text/css" />
    <!-- END PAGE LEVEL PLUGINS -->
    <link href="https://www.jqueryscript.net/css/jquerysctipttop.css" rel="stylesheet" type="text/css" />

    <link href="../assets/global/css/components.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/css/plugins.min.css" rel="stylesheet" type="text/css" />
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
                                <button type="button" class="btn blue display-none" data-target="#ModalTestMail" data-toggle="modal">Send Test Mail</button>&nbsp;
                                <button type="button" class="btn red" data-target="#ModalMailConfirmation" data-toggle="modal">Send Email</button>&nbsp;
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

                    <%--<form class="form-horizontal" runat="server" role="form" enctype="multipart/form-data">--%>
                        <asp:ScriptManager ID="scriptManager1" runat="server"></asp:ScriptManager>
                        <asp:UpdatePanel ID="updatepanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:TextBox ID="hdnEmailTask" runat="server" style="display:none;" />
                                <div class="modal-body">
                                    <div class="portlet light bordered">
                                        <div class="portlet-body form">
                                            <div class="form-body">
                                                <div class="form-group margin-bottom-10 clearfix" style="display: none">
                                                    <label>Login Id</label>
                                                    <asp:TextBox ID="TextBoxLoginId" CssClass="form-control" placeholder="enter comma seperated login id" runat="server"></asp:TextBox>
                                                    <small>Please enter 'All' or comma seperated 'login id' to send mail to active user</small><br />

                                                    <select id="ddlUsers" class="mt-multiselect btn btn-default" multiple data-placeholder="Select User(s)" data-label="left" data-select-all="true"
                                                        data-width="100%" data-filter="true" data-action-onchange="true" runat="server">
                                                    </select>

                                                    <%--<select id="bindUser" runat="server" class="mt-multiselect btn btn-default" 
                                            multiple data-placeholder="Select User(s)" data-label="left" data-select-all="true" 
                                            data-width="100%" data-filter="true" data-action-onchange="true">
                                        </select>--%>
                                                </div>
                                                <div class="form-group margin-bottom-10 clearfix">
                                                    <label>Other Email</label>
                                                    <asp:TextBox ID="TextOtherEmail" placeholder="enter comma seperated OtherEmail id" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <ul class="tree" style="margin-left: 10px;">
                                                        <li>
                                                            <input type="checkbox" id="chkRelatives" />
                                                            <label>Relatives</label>
                                                            <ul>
                                                                <asp:DataList ID="Datalist1" runat="server" RepeatDirection="Vertical"
                                                                    RepeatLayout="Table">
                                                                    <ItemTemplate>
                                                                        <li>
                                                                            <input type="checkbox" class="cbCheck" name="TreeSelect" value='<%#Eval("RELATIVE_EMAIL") %>' />
                                                                            <asp:Label ID="Itmnamelbl" Style="display: none" runat="server" Text='<%#Eval("RELATIVE_EMAIL") %>'></asp:Label>
                                                                            <asp:Label ID="Label6" runat="server" Text='<%#Eval("RELATIVE_NAME") %>'></asp:Label>

                                                                        </li>
                                                                    </ItemTemplate>
                                                                </asp:DataList>
                                                            </ul>
                                                        </li>
                                                        <li style="display: none">
                                                            <input type="checkbox" id="chkCP" />
                                                            <label>Connected persons</label>
                                                            <ul>
                                                                <asp:DataList ID="ddlConnected" runat="server" RepeatDirection="Vertical"
                                                                    RepeatLayout="Table">
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
                                                                    <asp:Label ID="Itmnamelbl1" runat="server" Text='<%#Eval("ROLE_NAME") %>'>
                                                                    </asp:Label>
                                                                    <ul>
                                                                        <asp:DataList ID="ddlCP" runat="server" RepeatDirection="Vertical">
                                                                            <ItemTemplate>
                                                                                <li>
                                                                                    <input type="checkbox" class="cbCheck" name="TreeSelect" value='<%#Eval("USER_EMAIL") %>' />
                                                                                    <asp:Label ID="Label1" Style="display: none" runat="server" Text='<%#Eval("USER_EMAIL") %>'>
                                                                                    </asp:Label>
                                                                                    <asp:Label ID="Label3" runat="server" Text='<%#Eval("USER_NM") %>'>
                                                                                    </asp:Label>
                                                                                </li>
                                                                            </ItemTemplate>
                                                                        </asp:DataList>
                                                                        <asp:DataList ID="ddlConnected" runat="server" RepeatDirection="Vertical">
                                                                            <ItemTemplate>
                                                                                <li>
                                                                                    <input type="checkbox" class="cbCheck" value='<%#Eval("CP_EMAIL") %>' />
                                                                                    <asp:Label ID="Label2" Style="display: none" runat="server" Text='<%#Eval("CP_EMAIL") %>'>
                                                                                    </asp:Label>
                                                                                    <asp:Label ID="Label4" runat="server" Text='<%#Eval("CP_NAME") %>'>
                                                                                    </asp:Label>
                                                                                </li>
                                                                            </ItemTemplate>
                                                                        </asp:DataList>
                                                                    </ul>
                                                                </li>
                                                            </ItemTemplate>
                                                        </asp:DataList>
                                                    </ul>
                                                </div>


                                                <div class="form-group margin-bottom-10 clearfix">
                                                    <label>BCC Email</label>
                                                    <asp:TextBox ID="TextBoxBccEmail" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:TextBox ID="txtSelectedUsers" CssClass="form-control" runat="server" Style="display: none;"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-actions text-right">
                                        <asp:Button ID="ButtonSend" CssClass="btn blue" runat="server" Text="Send" OnClick="ButtonSend_OnClick" OnClientClick="javascript:fnloader()" />
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="ButtonSend" />
                            </Triggers>
                        </asp:UpdatePanel>
                 <%--   </form>--%>
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
                            <asp:LinkButton ID="LinkButtonSendTestMail" runat="server" CssClass="btn blue" OnClick="LinkButtonSendTestMail_Click" OnClientClick="javascript:fnloader()">Send</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script src="../assets/global/plugins/bootstrap-summernote/summernote.min.js" type="text/javascript"></script>
    <script src="../assets/global/scripts/app.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/select2/js/select2.min.js" type="application/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-multiselect/js/bootstrap-multiselect.js" type="text/javascript"></script>
    <script src="../assets/pages/scripts/components-bootstrap-multiselect.js" type="text/javascript"></script>
    <script src="../assets/pages/scripts/jquery.checktree.js" type="text/javascript"></script>
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
            $('.summernote').on("summernote.enter", function (we, e) {
                $(this).summernote("pasteHTML", "<br><br>");
                e.preventDefault();
            });
        });
        function fnloader() {
            $("input[id*=txtSelectedUsers]").val($("select[id*=ddlUsers]").val());
        }
        //function fnLoadCostCenterAccess() {
        //    var webUrl = "api/Master/User.ashx?caller=LoadCoscenterAccess&EmployeeId=" + $('#txtEmpCode').val();
        //    var webUrl = uri + "/api/UserIT/GetUserRoleWise";
        //    $.ajax({
        //        type: 'GET',
        //        url: webUrl,
        //        cache: false,
        //        async: false,
        //        success: function (data) {
        //            var dataArray = new Array();
        //            for (var n = 0; n < data.length; n++) {
        //                for (var x = 0; x < data[n].UserCompany.length; x++) {
        //                    var obj = new Object();
        //                    obj.id = 'COMPANY_COMP_' + data[n].UserCompany[x].COMP_ID;
        //                    obj.text = data[n].UserCompany[x].COMP_NAME;
        //                    obj.children = new Array();
        //                    for (var y = 0; y < data[n].UserCompany[x].UserBrand.length; y++) {
        //                        var objBrand = new Object();
        //                        objBrand.id = 'BRAND_COMP_' + data[n].UserCompany[x].COMP_ID + '_BRAND_' + data[n].UserCompany[x].UserBrand[y].BRAND_ID;
        //                        objBrand.text = data[n].UserCompany[x].UserBrand[y].BRAND_NAME;
        //                        obj.children.push(objBrand);
        //                        objBrand.children = new Array();
        //                        for (var z = 0; z < data[n].UserCompany[x].UserBrand[y].UserRegion.length; z++) {
        //                            var objRegion = new Object();
        //                            objRegion.id = 'REGION_COMP_' + data[n].UserCompany[x].COMP_ID + '_BRAND_' + data[n].UserCompany[x].UserBrand[y].BRAND_ID + '_REGION_' + data[n].UserCompany[x].UserBrand[y].UserRegion[z].REGION_ID;
        //                            objRegion.text = data[n].UserCompany[x].UserBrand[y].UserRegion[z].REGION_NAME;
        //                            objBrand.children.push(objRegion);
        //                            objRegion.children = new Array();
        //                            for (var a = 0; a < data[n].UserCompany[x].UserBrand[y].UserRegion[z].UserCostcenter.length; a++) {
        //                                var objCostcenter = new Object();
        //                                objCostcenter.id = 'COSTCENTER_COMP_' + data[n].UserCompany[x].COMP_ID + '_BRAND_' + data[n].UserCompany[x].UserBrand[y].BRAND_ID + '_REGION_' + data[n].UserCompany[x].UserBrand[y].UserRegion[z].REGION_ID + '_COSTCENTER_' + data[n].UserCompany[x].UserBrand[y].UserRegion[z].UserCostcenter[a].COSCENTER_ID;
        //                                objCostcenter.text = data[n].UserCompany[x].UserBrand[y].UserRegion[z].UserCostcenter[a].COSTCENTER_NAME + "&nbsp;&nbsp;<img src='assets/layouts/layout/img/sidebar_toggler_icon_darkblue.png' onclick='javascript:fnShow(\"" + data[n].UserCompany[x].COMP_ID + "\",\"" + data[n].UserCompany[x].UserBrand[y].BRAND_ID + "\",\"" + data[n].UserCompany[x].UserBrand[y].UserRegion[z].REGION_ID + "\",\"" + data[n].UserCompany[x].UserBrand[y].UserRegion[z].UserCostcenter[a].COSCENTER_ID + "\");' />"

        //                                var objState = new Object();
        //                                for (var b = 0; b < data[n].UserAccessStatus.length; b++) {
        //                                    if (objCostcenter.id == 'COSTCENTER_COMP_' + data[n].UserAccessStatus[b].CompanyId + '_BRAND_' + data[n].UserAccessStatus[b].BrandId + '_REGION_' + data[n].UserAccessStatus[b].RegionId + '_COSTCENTER_' + data[n].UserAccessStatus[b].CostCenterId) {
        //                                        objState.selected = true;
        //                                    }
        //                                    objCostcenter.state = objState;
        //                                }
        //                                objRegion.children.push(objCostcenter);
        //                            }
        //                        }
        //                    }
        //                    dataArray.push(obj);
        //                }
        //            }
        //            /*Loop End*/
        //            /*Cost Center Access Draw Start*/
        //            var UITree = function () {
        //                var handleSample1 = function () {
        //                    $('#tree_2').jstree({
        //                        'plugins': ["wholerow", "checkbox", "types"],
        //                        'core': {
        //                            "themes": {
        //                                "responsive": false
        //                            },
        //                            'data': dataArray,
        //                        },
        //                        "types": {
        //                            "default": {
        //                                "icon": "fa fa-folder icon-state-warning icon-lg"
        //                            },
        //                            "file": {
        //                                "icon": "fa fa-file icon-state-warning icon-lg"
        //                            }
        //                        }
        //                    });
        //                }
        //                return {
        //                    //main function to initiate the module
        //                    init: function () {
        //                        handleSample1();
        //                    }

        //                };

        //            }();
        //            if (App.isAngularJsApp() === false) {
        //                jQuery(document).ready(function () {
        //                    UITree.init();
        //                });
        //            }
        //            /*Cost Center Access Draw Start*/
        //        }
        //    });
        //}
        $(function () {
            $('ul.tree').checkTree({

            });
        });


        var downloadComplete = false;
        var intervalListener;

        var start = $("input[id*=hdnEmailTask]").val();
        if (start == "Start") {
            $("#LoaderProgerss").show();
            intervalListener = window.setInterval(function () {
                if (!downloadComplete) {
                    CallCheckEmailStatus();
                }
            }, 2000);
        }
        else if (start == "NullbyteFileError") {
            alert("Uploaded document contains nullbyte, please correct the name and try again.");
        }
        else if (start == "FileError") {
            alert("Please upload only pdf format");
            }


        function fnChkStatus() {
            debugger;
            var start = $("input[id*=hdnEmailTask]").val();
            if (start == "Start") {
                $("#LoaderProgerss").show();
                intervalListener = window.setInterval(function () {
                    if (!downloadComplete) {
                        CallCheckEmailStatus();
                    }
                }, 2000);
            }
        }
        function CallCheckEmailStatus() {
            debugger;
            $.ajax({
                type: "POST",
                url: "Send_Message.aspx/CheckDownload",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    updateStatus('completed', r.d);
                    if (r.d.indexOf('All emails sent') > -1) {
                        downloadComplete = true;
                    }
                },
                error: function (r) {
                    console.log('Check error : ' + r);
                },
                failure: function (r) {
                    console.log('Check failure : ' + r);
                }
            });
            if (downloadComplete) {
                window.clearInterval(intervalListener);
                $("input[id*=hdnEmailTask]").val('');
                $("#LoaderProgerss").hide();
            }
        }
        function updateStatus(status, msg) {
            debugger;
            document.getElementById('lblMsg').innerHTML = msg;
            if (msg.indexOf('All emails sent') > -1) {
                downloadComplete = true;
                window.clearInterval(intervalListener);
                $("input[id*=hdnEmailTask]").val('');
                $("#LoaderProgerss").hide();
                alert("Custom Email notification sent successfully");
            }
        }


    </script>
</asp:Content>

<%--<%@ Page Title="Send Email" Language="C#" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" AutoEventWireup="true" CodeBehind="Send_Message.aspx.cs" Inherits="ProcsDLL.InsiderTrading.Send_Message" ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../assets/global/css/components.min.css" rel="stylesheet" />
    <link href="../assets/global/css/plugins.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/bootstrap-summernote/summernote.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/select2/css/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/select2/css/select2-bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/bootstrap-multiselect/css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/jstree/dist/themes/default/style.min.css" rel="stylesheet" type="text/css" />
    <link href="https://www.jqueryscript.net/css/jquerysctipttop.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/css/components.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/css/plugins.min.css" rel="stylesheet" type="text/css" />
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
                                <button type="button" class="btn blue display-none" data-target="#ModalTestMail" data-toggle="modal">Send Test Mail</button>&nbsp;
                                <button type="button" class="btn red" data-target="#ModalMailConfirmation" data-toggle="modal">Send Email</button>&nbsp;
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
                                    <div class="form-group margin-bottom-10 clearfix" style="display:none">
                                        <label>Login Id</label>
                                        <asp:TextBox ID="TextBoxLoginId" CssClass="form-control" placeholder="enter comma seperated login id" runat="server"></asp:TextBox>
                                        <small>Please enter 'All' or comma seperated 'login id' to send mail to active user</small><br />

                                        <select id="ddlUsers" class="mt-multiselect btn btn-default" multiple data-placeholder="Select User(s)" data-label="left" data-select-all="true"
                                            data-width="100%" data-filter="true" data-action-onchange="true" runat="server">
                                        </select>
                                    </div>
                                     <div class="form-group margin-bottom-10 clearfix">
                                        <label>Other Email</label>
                                        <asp:TextBox ID="TextOtherEmail" placeholder="enter comma seperated OtherEmail id" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <ul class="tree" style="margin-left: 12px;">
                                            <li>
                                                <input type="checkbox" id="chkRelatives" />
                                                <label>Relatives</label>
                                                <ul>
                                                    <asp:DataList ID="Datalist1" runat="server" RepeatDirection="Vertical"
                                                        RepeatLayout="Table">
                                                        <ItemTemplate>
                                                            <li>
                                                                <input type="checkbox" class="cbCheck"  name="TreeSelect" value='<%#Eval("RELATIVE_EMAIL") %>' />
                                                                <asp:Label ID="Itmnamelbl" Style="display: none" runat="server" Text='<%#Eval("RELATIVE_EMAIL") %>'></asp:Label>
                                                                <asp:Label ID="Label6" runat="server" Text='<%#Eval("RELATIVE_NAME") %>'></asp:Label>

                                                            </li>
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                                </ul>
                                            </li>
                                            <li>
                                                <input type="checkbox" id="chkCP" />
                                                <label>Connected persons</label>
                                                <ul>
                                                    <asp:DataList ID="ddlConnected" runat="server" RepeatDirection="Vertical"
                                                        RepeatLayout="Table">
                                                        <ItemTemplate>
                                                            <li>
                                                                <input type="checkbox" class="cbCheck"  name="TreeSelect" value='<%#Eval("CP_EMAIL") %>' />
                                                                <asp:Label ID="Label2" Style="display: none" runat="server" Text='<%#Eval("CP_EMAIL") %>'></asp:Label>
                                                                <asp:Label ID="Label4" runat="server" Text='<%#Eval("CP_NAME") %>'></asp:Label>
                                                            </li>
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                                </ul>
                                            </li>
                                            <asp:DataList ID="ddlRole" runat="server" RepeatDirection="Vertical">
                                                <ItemTemplate>
                                                    <li>
                                                        <input type="checkbox" />
                                                        <asp:Label ID="Itmnamelbl1" runat="server" Text='<%#Eval("ROLE_NAME") %>'>
                                                        </asp:Label>
                                                        <ul>
                                                            <asp:DataList ID="ddlCP" runat="server" RepeatDirection="Vertical">
                                                                <ItemTemplate>
                                                                    <li>
                                                                        <input type="checkbox" class="cbCheck"  name="TreeSelect" value='<%#Eval("USER_EMAIL") %>' />
                                                                        <asp:Label ID="Label1" Style="display: none" runat="server" Text='<%#Eval("USER_EMAIL") %>'>
                                                                        </asp:Label>
                                                                        <asp:Label ID="Label3" runat="server" Text='<%#Eval("USER_NM") %>'>
                                                                        </asp:Label>
                                                                    </li>
                                                                </ItemTemplate>
                                                            </asp:DataList>
                                                        </ul>
                                                    </li>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </ul>
                                    </div>
                                    <div class="form-group margin-bottom-10 clearfix">
                                        <label>BCC Email</label>
                                        <asp:TextBox ID="TextBoxBccEmail" CssClass="form-control" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtSelectedUsers" CssClass="form-control" runat="server" Style="display: none;"></asp:TextBox>
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
                            <asp:LinkButton ID="LinkButtonSendTestMail" runat="server" CssClass="btn blue" OnClick="LinkButtonSendTestMail_Click" OnClientClick="javascript:fnloader()">Send</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script src="../assets/global/plugins/bootstrap-summernote/summernote.min.js" type="text/javascript"></script>
    <script src="../assets/global/scripts/app.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/select2/js/select2.min.js" type="application/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-multiselect/js/bootstrap-multiselect.js" type="text/javascript"></script>
    <script src="../assets/pages/scripts/components-bootstrap-multiselect.js" type="text/javascript"></script>
    <script src="../assets/pages/scripts/jquery.checktree.js" type="text/javascript"></script>
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
            $('.summernote').on("summernote.enter", function (we, e) {
                $(this).summernote("pasteHTML", "<br><br>");
                e.preventDefault();
            });
        });
        function fnloader() {
            $("input[id*=txtSelectedUsers]").val($("select[id*=ddlUsers]").val());
        }
        $(function () {
            $('ul.tree').checkTree({

            });
        });
    </script>
</asp:Content>--%>