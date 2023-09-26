<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chat.aspx.cs" Inherits="ProcsDLL.chat" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Access-Control-Allow-Origin" />
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
    <link rel="stylesheet" type="text/css" href="translucent/style.css" />
    <style type="text/css">
        .page-header .page-header-top .page-logo .logo-default {
            margin: 3px 10px 0 !important;
        }

        .form-horizontal .control-label {
            text-align: left !important;
        }
    </style>
</head>
<body>
    <div class="portlet" style="margin-bottom: 0;">
        <div class="portlet-body" id="chats">
            <div class="scroller" data-height="400px" data-always-visible="1" data-rail-visible1="1" id="dvChat">
                <ul class="chats" id="ulChat"></ul>
            </div>
            <div class="chat-form">
                <div class="input-cont">
                    <input id="txtMessage" class="form-control" type="text" placeholder="Type a message here..." />
                </div>
                <div class="btn-cont">
                    <span class="arrow"></span>
                    <a href="#" class="btn blue icn-only" id="send">
                        <i class="fa fa-check icon-white"></i>
                    </a>
                </div>
            </div>
        </div>
    </div>

    <script src="assets/global/plugins/jquery.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/js.cookie.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/uniform/jquery.uniform.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js" type="text/javascript"></script>
    <!-- END CORE PLUGINS -->
    <!-- BEGIN PAGE LEVEL PLUGINS -->
    <%-- <script src="Scripts/jquery-3.4.1.min.js"></script>--%>
    <script src="Scripts/jquery.signalR-2.2.2.js"></script>
    <script src="signalr/hubs"></script>
<%--    <script src="scripts/jquery.signalR-2.4.1.min.js" type="text/javascript"></script>
    <script src="signalr/hubs"></script>--%>

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
        var chat = null;

        /*uName = "rajesh";
        userId = "34";
        CompanyId = "4";
        var committeeId = "10";
        var meetingId = "41";
        var agendaId = "541";
        var chatId = "1";
        var page = "1";
        var type = "Text";
        var text = "Annotation 1 by Sandeep";*/

        var UName = getUrlparam()["UName"];
        var ChatId = getUrlparam()["ChatId"];
        var UserId = getUrlparam()["UserId"];
        var CompanyId = getUrlparam()["CompanyId"];
        var CommitteeId = getUrlparam()["CommitteeId"];
        var MeetingId = getUrlparam()["MeetingId"];
        var AgendaId = getUrlparam()["AgendaId"];
        var Text = getUrlparam()["Text"];

        function getUrlparam() {
            var vars = {};
            var parts = window.location.href.replace(/[?&]+([^=&]+)=([^&]*)/gi, function (m, key, value) {
                vars[key] = value;
            });
            return vars;
        }

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
            fnLoadChat(CompanyId, CommitteeId, MeetingId, AgendaId, ChatId, Text);
        }).fail(function () {
            console.log('Could not connect');
        });

        function fnLoadChat(companyId, committeeId, meetingId, agendaId, chatId, text) {
            $('#ulChat').html('');
            registerEvents(companyId, committeeId, meetingId, agendaId, chatId);
        }
        function registerEvents(companyId, committeeId, meetingId, agendaId, chatId) {
            /*alert("In function registerEvents");
            alert("companyId=" + companyId);
            alert("committeeId=" + committeeId);
            alert("meetingId=" + meetingId);
            alert("agendaId=" + agendaId);
            alert("chatId=" + chatId);
            alert("uName=" + uName);*/
            chat.server.connect(companyId, committeeId, meetingId, agendaId, chatId, UName);
        }
        chat.client.GetChatMessage = function (id, lstMsgs) {
            $('#ulChat').append(lstMsgs);
            var div = $('#dvChat');
            var height = div.height();
            div.animate({ scrollTop: height }, 500);

        }
        chat.client.Broadcast = function (name, message, iChatId, sAvatar, sLoginId) {
            //alert("In function Broadcast");
            //alert("name=" + name);
            //alert("message=" + message);
            //alert("iChatId=" + iChatId);
            //alert("sAvatar=" + sAvatar);
            //alert("sLoginId=" + sLoginId);
            //alert("ChatId=" + ChatId);
            //alert("#chat-notification_" + iChatId);
            if (ChatId == iChatId) {
                const monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];

                var str = "";
                var currentdate = new Date();
                if (sLoginId == UName) {
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
                //alert("displaying none");
                callNotification("chat-notification_" + iChatId, "none")
                //window.parent.fnShowHide();
            }
            else {
                //alert("displaying block");
                callNotification("chat-notification_" + iChatId, "block")
                //window.parent.fnShowHide("chat-notification_" + iChatId, "block");
            }
        };
        function callNotification(id, disp) {
           // alert("In function callNotification");
           // alert("id=" + id);
           // alert("disp=" + disp);
            window.parent.fnShowHide(id, disp);
        }
        function fnPushMessage() {
            chat.server.broadcast(UName, $('#txtMessage').val(), ChatId, UserId, CompanyId);
            $('#txtMessage').val("");
            return false;
        }
        $("#txtMessage").keypress(function (e) {
            if (e.which == 13) {
                fnPushMessage();
                return false;
            }
        });


    </script>
</body>
</html>
