<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="ProcsDLL.ChangePassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>pro-CS | Change Password</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />
    <meta content="Preview page of Metronic Admin Theme #1 for user account page" name="description" />
    <meta content="" name="author" />
    <!-- BEGIN GLOBAL MANDATORY STYLES -->
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/simple-line-icons/simple-line-icons.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />
    <!-- END GLOBAL MANDATORY STYLES -->
    <!-- BEGIN PAGE LEVEL PLUGINS -->
    <link href="assets/global/plugins/bootstrap-fileinput/bootstrap-fileinput.css" rel="stylesheet" type="text/css" />
    <!-- END PAGE LEVEL PLUGINS -->
    <!-- BEGIN THEME GLOBAL STYLES -->
    <link href="assets/global/css/components.min.css" rel="stylesheet" id="style_components" type="text/css" />
    <link href="assets/global/css/plugins.min.css" rel="stylesheet" type="text/css" />
    <!-- END THEME GLOBAL STYLES -->
    <!-- BEGIN PAGE LEVEL STYLES -->
    <link href="assets/pages/css/profile.min.css" rel="stylesheet" type="text/css" />
    <!-- END PAGE LEVEL STYLES -->
    <!-- BEGIN THEME LAYOUT STYLES -->
    <link href="assets/layouts/layout/css/layout.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/layouts/layout/css/themes/darkblue.min.css" rel="stylesheet" type="text/css" id="style_color" />
    <link href="assets/layouts/layout/css/custom.min.css" rel="stylesheet" type="text/css" />
    <!-- END THEME LAYOUT STYLES -->
    <link rel="shortcut icon" href="favicon.ico" />
    <style>
        .required {
            color: red;
        }

        .tooltip {
            position: relative;
            display: inline-block;
            /* border-bottom: 1px dotted black;*/
        }

        .tooltip .tooltiptext {
            visibility: hidden;
            width: 400px;
            background-color: black;
            color: #fff;
            text-align: left;
            border-radius: 6px;
            padding: 5px 0;
            position: absolute;
            z-index: 1;
            top: -5px;
            left: 110%;
        }

        .tooltip .tooltiptext::after {
            content: "";
            position: absolute;
            top: 50%;
            right: 100%;
            margin-top: -5px;
            border-width: 5px;
            border-style: solid;
            border-color: transparent black transparent transparent;
        }

        .tooltip:hover .tooltiptext {
            visibility: visible;
        }
    </style>
</head>
<body>
    <div class="profile-content">
        <div class="row">
            <div class="col-md-12">
                <div class="portlet light ">
                    <div class="portlet-title tabbable-line">
                        <div class="caption caption-md">
                            <i class="icon-globe theme-font hide"></i>
                            <span class="caption-subject font-blue-madison bold uppercase">Profile Account</span>
                        </div>
                        <ul class="nav nav-tabs">
                            <%--    <li class="active">
                                <a href="#tab_1_1" data-toggle="tab">Personal Info</a>
                            </li>
                            <li>
                                <a href="#tab_1_2" data-toggle="tab">Change Avatar</a>
                            </li>--%>
                            <li class="active">
                                <a href="#tab_1_3" data-toggle="tab">Change Password</a>
                            </li>
                            <%--   <li>
                                <a href="#tab_1_4" data-toggle="tab">Privacy Settings</a>
                            </li>--%>
                        </ul>
                    </div>
                    <div class="portlet-body">
                        <div class="tab-content">
                            <!-- PERSONAL INFO TAB -->
                            <%--<div class="tab-pane active" id="tab_1_1">
                                <form role="form" action="#">
                                    <div class="form-group">
                                        <label class="control-label">First Name</label>
                                        <input type="text" placeholder="John" class="form-control" />
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label">Last Name</label>
                                        <input type="text" placeholder="Doe" class="form-control" />
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label">Mobile Number</label>
                                        <input type="text" placeholder="+1 646 580 DEMO (6284)" class="form-control" />
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label">Interests</label>
                                        <input type="text" placeholder="Design, Web etc." class="form-control" />
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label">Occupation</label>
                                        <input type="text" placeholder="Web Developer" class="form-control" />
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label">About</label>
                                        <textarea class="form-control" rows="3" placeholder="We are KeenThemes!!!"></textarea>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label">Website Url</label>
                                        <input type="text" placeholder="http://www.mywebsite.com" class="form-control" />
                                    </div>
                                    <div class="margiv-top-10">
                                        <a href="javascript:;" class="btn green">Save Changes </a>
                                        <a href="javascript:;" class="btn default">Cancel </a>
                                    </div>
                                </form>
                            </div>--%>
                            <!-- END PERSONAL INFO TAB -->
                            <!-- CHANGE AVATAR TAB -->
                            <%--<div class="tab-pane" id="tab_1_2">
                                <p>
                                    Anim pariatur cliche reprehenderit, enim eiusmod high life accusamus terry richardson ad squid. 3 wolf moon officia aute, non cupidatat skateboard dolor brunch. Food truck quinoa nesciunt laborum
                                                                eiusmod.
                                </p>
                                <form action="#" role="form">
                                    <div class="form-group">
                                        <div class="fileinput fileinput-new" data-provides="fileinput">
                                            <div class="fileinput-new thumbnail" style="width: 200px; height: 150px;">
                                                <img src="http://www.placehold.it/200x150/EFEFEF/AAAAAA&amp;text=no+image" alt="" />
                                            </div>
                                            <div class="fileinput-preview fileinput-exists thumbnail" style="max-width: 200px; max-height: 150px;"></div>
                                            <div>
                                                <span class="btn default btn-file">
                                                    <span class="fileinput-new">Select image </span>
                                                    <span class="fileinput-exists">Change </span>
                                                    <input type="file" name="...">
                                                </span>
                                                <a href="javascript:;" class="btn default fileinput-exists" data-dismiss="fileinput">Remove </a>
                                            </div>
                                        </div>
                                        <div class="clearfix margin-top-10">
                                            <span class="label label-danger">NOTE! </span>
                                            <span>Attached image thumbnail is supported in Latest Firefox, Chrome, Opera, Safari and Internet Explorer 10 only </span>
                                        </div>
                                    </div>
                                    <div class="margin-top-10">
                                        <a href="javascript:;" class="btn green">Submit </a>
                                        <a href="javascript:;" class="btn default">Cancel </a>
                                    </div>
                                </form>
                            </div>--%>
                            <!-- END CHANGE AVATAR TAB -->
                            <!-- CHANGE PASSWORD TAB -->
                            <div class="tab-pane active" id="tab_1_3">
                                <form id="form1" runat="server" defaultbutton="btnChangePass">
                                    <div class="form-group">
                                        <label id="lblLoginId" class="control-label">Login Id <span class="required">* </span></label>
                                        <input id="txtLoginId" disabled="disabled" runat="server" type="text" class="form-control" placeholder="Enter your Login Id" onkeypress="javascript:fnRemoveClass(this,'LoginId');" autocomplete="off" />
                                    </div>
                                    <br />
                                    <div class="form-group">
                                        <label id="lblOldPassword" class="control-label">Password <span class="required">* </span></label>
                                        <input id="txtOldPassword" runat="server" type="password" class="form-control" placeholder="Enter your current password" onkeypress="javascript:fnRemoveClass(this,'OldPassword');" autocomplete="off" />
                                    </div>
                                    <div class="form-group">
                                        <label id="lblNewPassword" class="control-label">New Password <span class="required">* </span></label>
                                        <input id="txtNewPassword" runat="server" type="password" class="form-control" placeholder="Enter your new password" onkeypress="javascript:fnRemoveClass(this,'NewPassword');" autocomplete="off" />&nbsp;&nbsp;
                                        <div class="tooltip" style="opacity: initial!important;padding-top:8px;">
                                            <i class="fa fa-info-circle" style="color: red;font-size:18px;" aria-hidden="true"></i>
                                            <span class="tooltiptext">
                                                <ul>
                                                    <li>Password must be at least 8 characters</li>
                                                    <li>At least one upper case letter: (A - Z)</li>
                                                    <li>At least one lower case letter: (a - z)</li>
                                                    <li>At least one number: (0 - 9)</li>
                                                    <li>At least one Special Characters: !#$%&()*+@?^~</li>
                                                </ul>

                                            </span>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label id="lblConfirmNewPassword" class="control-label">Confirm New Password <span class="required">* </span></label>
                                        <input id="txtConfirmNewPassword" runat="server" type="password" class="form-control" placeholder="Confirm your new password" onkeypress="javascript:fnRemoveClass(this,'ConfirmNewPassword');" autocomplete="off" />
                                    </div>
                                    <div class="margin-top-10">
                                        <asp:Button runat="server" OnClick="GoToLogin" CssClass="btn default" OnClientClick="return true;" Text="Back To Login" />
                                        <asp:Button ID="btnChangePass" runat="server" OnClick="SaveChangedPassword" CssClass="btn green" OnClientClick="return fnValidateForm();" Text="Save Changes" />
                                    </div>
                                </form>
                            </div>
                            <!-- END CHANGE PASSWORD TAB -->
                            <!-- PRIVACY SETTINGS TAB -->
                            <%--<div class="tab-pane" id="tab_1_4">
                                <form action="#">
                                    <table class="table table-light table-hover">
                                        <tr>
                                            <td>Anim pariatur cliche reprehenderit, enim eiusmod high life accusamus.. </td>
                                            <td>
                                                <div class="mt-radio-inline">
                                                    <label class="mt-radio">
                                                        <input type="radio" name="optionsRadios1" value="option1" />
                                                        Yes
                                                                                   
                                                        <span></span>
                                                    </label>
                                                    <label class="mt-radio">
                                                        <input type="radio" name="optionsRadios1" value="option2" checked />
                                                        No
                                                                                   
                                                        <span></span>
                                                    </label>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Enim eiusmod high life accusamus terry richardson ad squid wolf moon </td>
                                            <td>
                                                <div class="mt-radio-inline">
                                                    <label class="mt-radio">
                                                        <input type="radio" name="optionsRadios11" value="option1" />
                                                        Yes
                                                                                   
                                                        <span></span>
                                                    </label>
                                                    <label class="mt-radio">
                                                        <input type="radio" name="optionsRadios11" value="option2" checked />
                                                        No
                                                                                   
                                                        <span></span>
                                                    </label>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Enim eiusmod high life accusamus terry richardson ad squid wolf moon </td>
                                            <td>
                                                <div class="mt-radio-inline">
                                                    <label class="mt-radio">
                                                        <input type="radio" name="optionsRadios21" value="option1" />
                                                        Yes
                                                                                   
                                                        <span></span>
                                                    </label>
                                                    <label class="mt-radio">
                                                        <input type="radio" name="optionsRadios21" value="option2" checked />
                                                        No
                                                                                   
                                                        <span></span>
                                                    </label>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Enim eiusmod high life accusamus terry richardson ad squid wolf moon </td>
                                            <td>
                                                <div class="mt-radio-inline">
                                                    <label class="mt-radio">
                                                        <input type="radio" name="optionsRadios31" value="option1" />
                                                        Yes
                                                                                   
                                                        <span></span>
                                                    </label>
                                                    <label class="mt-radio">
                                                        <input type="radio" name="optionsRadios31" value="option2" checked />
                                                        No
                                                                                   
                                                        <span></span>
                                                    </label>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <!--end profile-settings-->
                                    <div class="margin-top-10">
                                        <a href="javascript:;" class="btn red">Save Changes </a>
                                        <a href="javascript:;" class="btn default">Cancel </a>
                                    </div>
                                </form>
                            </div>--%>
                            <!-- END PRIVACY SETTINGS TAB -->
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>






    <script src="assets/global/plugins/jquery.min.js" type="text/javascript"></script>
    <script src="assets/pages/scripts/sha512.js" type="text/javascript"></script>

    <script type="text/javascript">
        function fnValidateForm() {
            var count = 0;
            if ($("#txtLoginId").val() == undefined || $("#txtLoginId").val() == null || $("#txtLoginId").val().trim() == "") {
                $("#lblLoginId").addClass('required');
                count++;
            }
            else {
                $("#lblLoginId").removeClass('required');
            }
            if ($("#txtOldPassword").val() == undefined || $("#txtOldPassword").val() == null || $("#txtOldPassword").val().trim() == "") {
                $("#lblOldPassword").addClass('required');
                count++;
            }
            else {
                $("#lblOldPassword").removeClass('required');
            }
            if ($("#txtNewPassword").val() == undefined || $("#txtNewPassword").val() == null || $("#txtNewPassword").val().trim() == "") {
                $("#lblNewPassword").addClass('required');
                count++;
            }
            else if ($("#txtNewPassword").val().length < 8) {
                alert(" Password at least 8 Character");
                $("#lblNewPassword").addClass('required');
            }
            else {
                $("#lblNewPassword").removeClass('required');
            }
            if ($("#txtConfirmNewPassword").val() == undefined || $("#txtConfirmNewPassword").val() == null || $("#txtConfirmNewPassword").val().trim() == "") {
                $("#lblConfirmNewPassword").addClass('required');
                count++;
            }
            else {
                $("#lblConfirmNewPassword").removeClass('required');
            }
            if ($("#txtConfirmNewPassword").val() !== $("#txtNewPassword").val()) {
                alert("New Password and Confirm New Password does not match");
                return false;
            }
            if (!fnValidatePasswordPolicy($("input[id*='txtNewPassword']").val().trim())) {
                alert("Given Password does not fulfill Password Policy!");
                return false;
            }

            if (count > 0) {
                return false;
            }
            else {
                var salt = '<%=Session["salt"]%>';
                var moreSalts = '<%=Session["moreSalt"]%>';
                var hash = hex_sha512(hex_sha512(hex_sha512($("#txtOldPassword").val()) + salt) + salt);
                var fff = hex_sha512(hash + moreSalts);

                document.getElementById('txtOldPassword').value = fff;
               

                var hash1 = hex_sha512($("#txtNewPassword").val());
          
                document.getElementById('txtNewPassword').value = hash1;
                

                document.getElementById('txtConfirmNewPassword').value = hash1;
                

                return true;
            }
        }


        function fnValidatePasswordPolicy(str) {
            var strings = str;
            var specialCharacters = "!#$%&()*+@?^~";
            var setCounterUpperCase = 0;
            var setCounterLowerCase = 0;
            var setCounterNumeric = 0;
            var setCounterSpecialCharacters = 0;
            var setCounterPasswordLength = 0;
            var i = 0;
            var character = '';
            while (i <= strings.length) {
                character = strings.charAt(i);
                if (!isNaN(character * 1)) {
                    //alert('character is numeric');
                    setCounterNumeric = 1;
                }
                else if (specialCharacters.indexOf(character) !== -1) {
                    setCounterSpecialCharacters = 1;
                }
                else {
                    if (character == character.toUpperCase()) {
                        // alert('upper case true');
                        setCounterUpperCase = 1;
                    }
                    if (character == character.toLowerCase()) {
                        // alert('lower case true');
                        setCounterLowerCase = 1;
                    }
                }
                i++;
            }

            if (strings.length >= 8) {
                setCounterPasswordLength = 1;
            }

            if (setCounterPasswordLength > 0 && setCounterNumeric > 0 && setCounterSpecialCharacters > 0 && setCounterUpperCase > 0 && setCounterLowerCase > 0) {
                return true;
            }
            else {
                return false;
            }
        }

        function fnGoToLoginPage() {
            alert('Password Changed Successfully. Please login again to continue !');
            window.location.href = "LogOut.aspx";
        }

        function fnRemoveClass(obj, val) {
            $("#lbl" + val + "").removeClass('required');
        }
    </script>

    <script src="assets/global/plugins/jquery.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/js.cookie.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js" type="text/javascript"></script>
    <!-- END CORE PLUGINS -->
    <!-- BEGIN PAGE LEVEL PLUGINS -->
    <script src="assets/global/plugins/bootstrap-fileinput/bootstrap-fileinput.js" type="text/javascript"></script>
    <script src="assets/global/plugins/jquery.sparkline.min.js" type="text/javascript"></script>
    <!-- END PAGE LEVEL PLUGINS -->
    <!-- BEGIN THEME GLOBAL SCRIPTS -->
    <script src="assets/global/scripts/app.min.js" type="text/javascript"></script>
    <!-- END THEME GLOBAL SCRIPTS -->
    <!-- BEGIN PAGE LEVEL SCRIPTS -->
    <script src="assets/pages/scripts/profile.min.js" type="text/javascript"></script>
    <!-- END PAGE LEVEL SCRIPTS -->
    <!-- BEGIN THEME LAYOUT SCRIPTS -->
    <script src="assets/layouts/layout/scripts/layout.min.js" type="text/javascript"></script>
    <script src="assets/layouts/layout/scripts/demo.min.js" type="text/javascript"></script>
    <script src="assets/layouts/global/scripts/quick-sidebar.min.js" type="text/javascript"></script>
    <script src="assets/layouts/global/scripts/quick-nav.min.js" type="text/javascript"></script>
    <!-- END THEME LAYOUT SCRIPTS -->
    <script>
        $(document).ready(function () {
            $('#clickmewow').click(function () {
                $('#radio1003').attr('checked', 'checked');
            });
        })

        </script>
</body>
</html>
