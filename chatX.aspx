<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chatX.aspx.cs" Inherits="ProcsDLL.chatX" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
        <!-- BEGIN GLOBAL MANDATORY STYLES -->
        <link href="assets/global/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
        <link href="assets/global/plugins/simple-line-icons/simple-line-icons.min.css" rel="stylesheet" type="text/css" />
        <link href="assets/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
        <link href="assets/global/plugins/uniform/css/uniform.default.css" rel="stylesheet" type="text/css" />
        <link href="assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />
        <!-- END GLOBAL MANDATORY STYLES -->
        <!-- BEGIN PAGE LEVEL PLUGINS -->
        <link href="assets/css/Preloader.css" rel="stylesheet" type="text/css" />
        <!-- END PAGE LEVEL PLUGINS -->
        <!-- BEGIN THEME GLOBAL STYLES -->
        <link href="assets/global/css/components.min.css" rel="stylesheet" id="style_components" type="text/css" />
        <link href="assets/global/css/plugins.min.css" rel="stylesheet" type="text/css" />
        <!-- END THEME GLOBAL STYLES -->
        <!-- BEGIN THEME LAYOUT STYLES -->
        <link href="assets/layouts/layout3/css/layout.min.css" rel="stylesheet" type="text/css" />
        <link href="assets/layouts/layout3/css/themes/default.min.css" rel="stylesheet" type="text/css" id="style_color" />
        <link href="assets/layouts/layout3/css/custom.min.css" rel="stylesheet" type="text/css" />
        <!-- END THEME LAYOUT STYLES -->
        <link rel="shortcut icon" href="favicon.ico" />
        <link href="assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
        <link href="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
        <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />
        <link href="assets/global/plugins/select2/css/select2.min.css" rel="stylesheet" type="text/css" />
        <link href="assets/global/plugins/select2/css/select2-bootstrap.min.css" rel="stylesheet" type="text/css" />
        <link rel="stylesheet" type="text/css" href="translucent/style.css"/>
        <style type="text/css">
            .page-header .page-header-top .page-logo .logo-default {margin: 3px 10px 0 !important;}
            .form-horizontal .control-label {text-align: left !important;}
        </style>
    </head>
    <body>
        <form id="form1" runat="server">
            <div class="page-container">
                <!-- BEGIN CONTENT -->
                <div class="page-content-wrapper">
                    <!-- BEGIN CONTENT BODY -->
                    <div class="page-content">
                        <div class="container">
                            <!-- BEGIN PAGE BREADCRUMBS -->
                            <ul class="page-breadcrumb breadcrumb">
                                <li><a href="DashBoard.aspx">Home</a><i class="fa fa-circle"></i></li>
                                <li>Masters<i class="fa fa-circle"></i></li>
                                <li><span>User Master</span></li>
                            </ul>
                            <!-- END PAGE BREADCRUMBS -->
                            <div class="page-content-inner">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="portlet light portlet-fit">
                                            <div class="portlet-title">
                                                <div class="caption">
                                                    <i class="icon-settings font-red"></i>
                                                    <span class="caption-subject font-red sbold uppercase">Chat annotation</span>
                                                </div>
                                            </div>
                                            <div class="portlet-body">
                                                <div class="table-toolbar">
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <div class="btn-group"></div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <%--<table class="table table-striped table-hover table-bordered" id="sample" style="display:none;">
                                                    <thead>
                                                        <tr>
                                                            <th>Initiator</th>
                                                            <th>Page</th>
                                                            <th>Type</th>
                                                            <th>Text</th>
                                                            <th></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody id="tblUserMasterlist">
                                                        <tr>
                                                            <td>Sandeep Jain</td>
                                                            <td>1</td>
                                                            <td>Text</td>
                                                            <td>Annotation 1 by Sandeep</td>
                                                            <td>
                                                                <img onclick="fnLoadChat('4','10','41','541','1','1','Text','Annotation 1 by Sandeep');" src="assets/img/chat-icon01.jpg" style="width:48px;height:48px;" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Sandeep Jain</td>
                                                            <td>2</td>
                                                            <td>Text</td>
                                                            <td>Annotation 2 by Sandeep</td>
                                                            <td>
                                                                <img onclick="fnLoadChat('4','10','41','541','2','2','Text','Annotation 2 by Sandeep');" src="assets/img/chat-icon01.jpg" style="width:48px;height:48px;" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Sandeep Jain</td>
                                                            <td>3</td>
                                                            <td>Text</td>
                                                            <td>Annotation 3 by Sandeep</td>
                                                            <td>
                                                                <img onclick="fnLoadChat('4','10','41','541','3','3','Text','Annotation 3 by Sandeep');" src="assets/img/chat-icon01.jpg" style="width:48px;height:48px;" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Sandeep Jain</td>
                                                            <td>4</td>
                                                            <td>Text</td>
                                                            <td>Annotation 4 by Sandeep</td>
                                                            <td>
                                                                <img onclick="fnLoadChat('4','10','41','541','4','4','Text','Annotation 4 by Sandeep');" src="assets/img/chat-icon01.jpg" style="width:48px;height:48px;" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Pulkit Goyal</td>
                                                            <td>3</td>
                                                            <td>Drawing</td>
                                                            <td></td>
                                                            <td>
                                                                <img onclick="fnLoadChat('4','10','41','541','5','5','Drawing','');" src="assets/img/chat-icon01.jpg" style="width:48px;height:48px;" />
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>--%>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- END PAGE CONTENT INNER -->
                        </div>
                    </div>
                    <!-- END PAGE CONTENT BODY -->
                </div>
                <!-- END CONTENT -->
            </div>
            <div class="modal fade" id="modalChat" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                            <h4 class="modal-title" id="hLbl"><b>User</b></h4>
                        </div>
                        <div class="modal-body">
                            <div class="portlet" style="margin-bottom: 0;">
                                <div class="portlet-body" id="chats">
                                    <div class="scroller" data-height="400px" data-always-visible="1" data-rail-visible1="1" id="dvChat">
                                        <ul class="chats" id="ulChat"></ul>
                                    </div>
                                    <div class="chat-form">
                                        <div class="input-cont">
                                            <input id="txtMessage" class="form-control" type="text" placeholder="Type a message here..."  />
                                        </div>
                                        <div class="btn-cont">
                                            <span class="arrow"> </span>
                                            <a href="#" class="btn blue icn-only" id="send">
                                                <i class="fa fa-check icon-white"></i>
                                            </a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--<div class="modal-footer">
                            <div class="input-cont">
                                <input class="form-control" type="text" placeholder="Type a message here..." />
                                d<a href="#" class="btn blue icn-only" id="send">
                                    <i class="fa fa-check icon-white"></i>
                                </a>
                            </div>
                            <div class="btn-cont">
                                <span class="arrow"> </span>
                                <a href="#" class="btn blue icn-only" id="send">
                                    <i class="fa fa-check icon-white"></i>
                                </a>
                            </div>
                        </div>--%>
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>
        </form>

        <script src="assets/global/plugins/jquery.min.js" type="text/javascript"></script>
        <script src="assets/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
        <script src="assets/global/plugins/js.cookie.min.js" type="text/javascript"></script>
        <script src="assets/global/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js" type="text/javascript"></script>
        <script src="assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
        <script src="assets/global/plugins/uniform/jquery.uniform.min.js" type="text/javascript"></script>
        <script src="assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js" type="text/javascript"></script>
        <!-- END CORE PLUGINS -->
        <!-- BEGIN PAGE LEVEL PLUGINS -->
        <script src="scripts/jquery.signalR-2.4.1.min.js" type="text/javascript"></script>
        <script src="signalr/hubs"></script>
        <!-- END PAGE LEVEL PLUGINS -->
        <!-- BEGIN THEME GLOBAL SCRIPTS -->
        <script src="assets/global/scripts/app.min.js" type="text/javascript"></script>
        <!-- END THEME GLOBAL SCRIPTS -->
        <!-- BEGIN PAGE LEVEL SCRIPTS -->
        <script src="assets/js/ControlValidation.js" type="text/javascript"></script>
        <!-- END PAGE LEVEL SCRIPTS -->
        <!-- BEGIN THEME LAYOUT SCRIPTS -->
        <script src="assets/layouts/layout3/scripts/layout.min.js" type="text/javascript"></script>
        <script src="assets/layouts/layout3/scripts/demo.min.js" type="text/javascript"></script>
        <script src="assets/layouts/global/scripts/quick-sidebar.min.js" type="text/javascript"></script>

        <script src="assets/global/scripts/datatable.js" type="text/javascript"></script>
        <script src="assets/global/plugins/datatables/datatables.min.js" type="text/javascript"></script>
        <script src="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js" type="text/javascript"></script>
        <script src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js" type="text/javascript"></script>
        <script src="assets/global/plugins/select2/js/select2.full.min.js" type="text/javascript"></script>
        <script src="assets/pages/scripts/table-datatables-buttons.js" type="text/javascript"></script>
        <script src="assets/pages/scripts/components-select2.min.js" type="text/javascript"></script>

        <script type="text/javascript">
            var uName = prompt('Enter your name:', '');
            var ChatId = "";
            var chat = null;
            var userId = "";
            var CompanyId = "";

            uName == "rajesh";
            userId = "34";
            CompanyId = "4";
            var committeeId = "10";
            var meetingId = "41";
            var agendaId = "541";
            var chatId = "1";
            var page = "1";
            var type = "Text";
            var text = "Annotation 1 by Sandeep";
            
            chat = $.connection.chatHub;
            registerClientMethods(chat);

            function registerClientMethods(chatHub) {
                chatHub.client.onConnected = function (id, userName, allUsers, messages, times) { }
                chatHub.client.onNewUserConnected = function (id, name, UserImage, loginDate) { }
                chatHub.client.onUserDisconnected = function (id, userName) { }
                chatHub.client.sendPrivateMessage = function (windowId, fromUserName, message, userimg, CurrentDateTime) { }
            }

            $.connection.hub.start().done(function () {
                console.log('Now connected, connection ID=' + $.connection.id);
                //alert("Connected");
                //alert("chat.id=" + chat.id);
                // Wire up Send button to call sendmessage on the server.
                $("#send").click(function () {
                    fnPushMessage();
                });
            }).fail(function () {
                console.log('Could not connect');
            });

            function fnLoadChat(companyId, committeeId, meetingId, agendaId, chatId, page, type, text) {
                ChatId = chatId;
                CompanyId = companyId;
                $('#ulChat').html('');
                registerEvents(companyId, committeeId, meetingId, agendaId, chatId);
                $("#hLbl").html("<b>Agend 01 - "+text+"</b>");
                $("#modalChat").modal('show');
            }
            function registerEvents(companyId, committeeId, meetingId, agendaId, chatId) {
                chat.server.connect(companyId, committeeId, meetingId, agendaId, chatId, uName);
            }
            chat.client.GetChatMessage = function (id, lstMsgs) {
                $('#ulChat').append(lstMsgs);
                var div = $('#dvChat');
                var height = div.height();
                div.animate({ scrollTop: height }, 500);

            }
            chat.client.Broadcast = function (name, message, iChatId, sAvatar, sLoginId) {
                //alert(sAvatar);
                if (ChatId == iChatId) {
                    const monthNames = ["Jan","Feb","Mar","Apr","May","Jun","Jul","Aug","Sep","Oct","Nov","Dec"];

                    var str = "";
                    var currentdate = new Date();
                    if (sLoginId == uName) {
                        str = '<li class="out">';
                    }
                    else {
                        str = '<li class="in">';
                    }
                    str += '<img class="avatar" alt="" src="' + sAvatar + '" />';
                    str += '<div class="message">';
                    str += '<span class="arrow"> </span>';
                    str += '<a href="javascript:;" class="name">' + name + '</a>';
                    str += '<span class="datetime"> at ' + monthNames[currentdate.getMonth()] + " " + currentdate.getDate() + " " + currentdate.getFullYear() + " " + currentdate.getHours() + ':' + currentdate.getMinutes() + '</span>';
                    str += '<span class="body">' + message + '</span>';
                    str += '</div>';
                    str += '</li>';

                    $('#ulChat').append(str);
                    var div = $('#dvChat');
                    var height = div.height();
                    div.animate({ scrollTop: height }, 500);
                }
            };
            function fnPushMessage(){
                chat.server.broadcast(uName, $('#txtMessage').val(), ChatId, userId, CompanyId);
                $('#txtMessage').val("");
                return false;
            }
            $("#txtMessage").keypress(function (e) {
                if (e.which == 13) {
                    fnPushMessage();
                    return false;
                }
            });

            fnLoadChat(CompanyId, committeeId, meetingId, agendaId, chatId, page, type, text);
        </script>
    </body>
</html>