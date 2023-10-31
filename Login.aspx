<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ProcsDLL.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>pro-CS | Login</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta content="width=device-width, initial-scale=1" name="viewport" />
    <meta content="Preview page of Metronic Admin Theme #1 for " name="description" />
    <meta content="" name="author" />
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/simple-line-icons/simple-line-icons.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/select2/css/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/select2/css/select2-bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/css/components.min.css" rel="stylesheet" id="style_components" type="text/css" />
    <link href="assets/global/css/plugins.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/pages/css/login-5.min.css" rel="stylesheet" type="text/css" />
    <link rel="shortcut icon" href="favicon.ico" />
    <style type="text/css">
        .requied {
            color: red;
        }

        .backgroundimage {
            background-image: url('assets/images/bg1.jpg');
            background-repeat: no-repeat;
            background-size: auto;
            background-size: 100% 100% !important;
        }

        @media(min-width:992px) {
            #LoginPageContent {
                margin-right: 30%;
            }
        }

        #LoginPageContent {
            padding: 40px;
            height: 100vh;
            background: #dae9f0;
            font-size: 16px;
        }

            #LoginPageContent p {
                text-align: center;
            }

        .input-group-addon {
            background-color: transparent;
            border: none;
        }
    </style>
</head>
<body class="login backgroundimage">
    <%--defaultbutton="btnLogin"--%> 
    <form id="form1" runat="server">
        <asp:Label ID="lblMsg" runat="server" Visible="false" />
        <asp:HiddenField ID="HiddenADFSUrl" runat="server" />
        <div class="user-login-5">
            <div class="row bs-reset">
                <div class="col-md-8 bs-reset mt-login-5-bsfix"></div>
                <div class="col-md-4 login-container bs-reset mt-login-5-bsfix">
                    <div id="LoginPageContent">
                        <img src="assets/images/logo.png" class="img-responsive" style="margin: 0 auto;" /><br />
                        <div class="form-group">
                            <p>
                                Online Portal<br />
                                for<br />
                                <b>Insider Trading Compliances</b>
                            </p>
                        </div>
                        <br />
                        <div class="form-group">
                            <asp:Label ID="lblEmail" runat="server">User Name</asp:Label>
                            <asp:TextBox runat="server" ID="UserName" CssClass="form-control" autocomplete="off" />

                        </div>
                        <br />                         
                        <div class="form-group" style="margin-left:40%;" >
                            <div class="input-group">                                 
                                <%--<Button ID="BtnNext" runat="server"    Text="Next" CssClass="btn btn-block green" OnClientClick="return fnChkAuthType();">--%>
                                    <button id="BtnNext" type="button" style="display: none;"   class="btn green" onclick="fnChkAuthType()">Next</button>

                                  <%--<button id="BtnNext" type="button"   class="btn green">Next</button>--%>
                                 <%--<asp:Button ID="BtnNext" runat="server"  Text="Next" CssClass="btn btn-block green" onclick="ChkAuthType_click"  OnClientClick="return fnChkAuthType();" />--%>
                                <%--<asp:Button ID="BtnNext" runat="server" Text="Next" CssClass="btn btn-block green" OnClick="ChkAuthType_click" OnClientClick="return fnChkAuthType();" />--%>
                            </div>
                        </div>

                        <div  class="form-group" >
                            <asp:Label ID="lblPassword" style="display: none;" runat="server">Password</asp:Label>
                            <asp:TextBox runat="server" ID="Password" style="display: none;" TextMode="Password" CssClass="form-control" autocomplete="off" />
                            <asp:HiddenField runat="server" ID="enableCaptcha" />
                            <asp:HiddenField runat="server" ID="enableADLogin" />
                            <asp:HiddenField runat="server" ID="authenticationType" />
                            <asp:HiddenField runat="server" ID="hdnChkAuthType" />
                            <asp:HiddenField runat="server" ID="hdnChkSSOType" />
                        </div>
                        <div class="form-group" style="display: none;">
                            <input type="radio" id="txtADLogin" name="loginAuthenticationType" value="Yes" />
                            <label for="txtADLogin">AD USER</label>
                            <input type="radio" id="txtFormLogin" name="loginAuthenticationType" checked="checked" value="No" />
                            <label for="txtFormLogin">SYSTEM USER</label>
                        </div>
                        <br />
                        <div id="dvCaptcha" class="form-grop" style="padding-top: 10px; padding-left: 34px; display: none;">
                            <div class="input-group">
                                <asp:Image ID="ImageCaptcha" CssClass="img-responsive" runat="server" ImageUrl="CaptchaImage.aspx" />
                                <span class="input-group-btn">
                                    <button type="button" class="btn btn-default" onclick="javascript:fnRefreshCaptcha();">
                                        <img src="assets/pages/img/login/refresh.png" alt="Refresh" />
                                    </button>
                                </span>
                            </div>
                            <br />
                            <%--</div>
                            <div>--%>
                            <label id="lblCaptcha" class="required">Enter Above Captcha Code</label>
                            <asp:TextBox runat="server" ID="txtCaptchaBox" CssClass="form-control restrictpaste" autocomplete="off" />
                        </div>
                        <br />
                        <div class="form-group" >
                            <div class="input-group">
                                <span id="spnForgetPsw" class="input-group-addon" style="display: none;">
                                    <a href="javascript:fnForgetPassword();" id="forget-password" class="forget-password">Forgot Password?</a>
                                </span>
                                <asp:Button ID="btnLogin" runat="server" OnClick="LogIn" Text="Sign In" CssClass="btn btn-block green" style="display: none;" OnClientClick="return fnLogin();" />
                            </div>
                        </div>
                    </div>
                    <div class="login-content" id="ForgetPasswordPageContent" style="display: none; padding: 25px; margin-right: 10%; margin-right: 30%; background-color: #dae9f0;">
                        <p style="text-align: center; font-size: 18px; color: navy;">
                            Online Portal<br />
                            for<br />
                            <b>Insider Trading Compliances</b>
                        </p>
                        <div class="row">
                            <asp:Label ID="LabelResetPassword" runat="server" CssClass="col-md-12 control-label">Email</asp:Label>
                            <div class="col-md-12">
                                <asp:TextBox runat="server" ID="TextBoxLoginId" CssClass="form-control restrictpaste" autocomplete="off" />
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-12 text-right">
                                <a href="Login.aspx" class="forget-password">Back To Login?</a>&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="BtResetPassword" runat="server" OnClick="ResetPassword" Text="Reset Password" CssClass="btn green" OnClientClick="return fnResetPassword();" />
                            </div>
                        </div>
                    </div>
                    <div class="login-footer">
                        <div class="row bs-reset">
                            <div class="col-xs-5 bs-reset" style="display: none;">
                                <ul class="login-social">
                                    <li>
                                        <a href="javascript:;">
                                            <i class="icon-social-facebook"></i>
                                        </a>
                                    </li>
                                    <li>
                                        <a href="javascript:;">
                                            <i class="icon-social-twitter"></i>
                                        </a>
                                    </li>
                                    <li>
                                        <a href="javascript:;">
                                            <i class="icon-social-dribbble"></i>
                                        </a>
                                    </li>
                                </ul>
                            </div>
                            <div class="col-xs-12 bs-reset">
                                <div class="login-copyright" style="padding-left: 20px;">
                                    <p>Copyright &copy; ProCS Technology | 2022</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <div class="modal fade" id="stack1" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title">Company Module Selection</h4>
                </div>
                <div class="modal-body">
                    <div id="ShowListing" runat="server"></div>
                </div>
                <div class="modal-footer">
                    <button type="button" data-dismiss="modal" class="btn btn-outline dark">Close</button>
                </div>
            </div>
        </div>
    </div>
    <script src="assets/global/plugins/jquery.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/js.cookie.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/jquery-validation/js/jquery.validate.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/jquery-validation/js/additional-methods.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/select2/js/select2.full.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/backstretch/jquery.backstretch.min.js" type="text/javascript"></script>
    <script src="assets/global/scripts/app.min.js" type="text/javascript"></script>
    <script src="assets/pages/scripts/login-5.js" type="text/javascript"></script>
    <script src="assets/pages/scripts/sha512.js" type="text/javascript"></script>
    <script>
        function InValidCaptcha(msg) {
            alert(msg);
        }
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
        $(document).ready(function () {
            var authType = $("input[id*=hdnChkAuthType]").val();
            if (authType == 'Hybrid') {
                $("#BtnNext").show();
            }
            else if (authType == 'Application') {
                $("#lblPassword").show();
                $("#Password").show(); 
                $("#BtnNext").hide();
                $("#spnForgetPsw").show();
                $("#btnLogin").show();
            }
            else {
                $("#lblPassword").show();
                $("#Password").show();
                $("#BtnNext").hide();
                $("#btnLogin").show();
            }

            if ($("#enableCaptcha").val() == "true") {
                // $("#divEnableCaptcha").show();
            }
            else {
                // $("#divEnableCaptcha").hide();
            }
            // DrawCaptcha();
            $('body').on('keypress', ".regLength100", function (event) {
                $(".regLength200").attr("maxlength", "200");
            });
            $('body').on('keypress', ".regtxtnumber", function (event) {
                if (event.keyCode != 46 && event.keyCode != 37 && event.keyCode != 39 && event.keyCode != 40 && event.keyCode !== 9 && event.keyCode !== 13) {
                    var regex = new RegExp("^[0-9A-Za-z \b]+$");
                    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
                    if (!regex.test(key) || (key == 46) || (key == 37) || (key == 39) || (key == 40) || (key === 9) || (key === 13)) {
                        event.preventDefault();
                        alert("You can write only number and alphabets into this textbox!");
                        return false;
                    }
                }
            });
            $('body').on('paste', ".regtxt,.regreadonly,.number,.regtxtnumber,.numberWithdot,.regLength3,.regLength4,.regLength20,.regLength50,.regLength100,.regLength200,.regLength500,.regLength1000,.restrictpaste", function (e) {
                e.preventDefault();
                alert("You cannot paste text into this textbox!");
                return false;
            });
            $('body').bind('copy', ".restrictCopy", function (e) {
                e.preventDefault();
                return false;
            });
            $('#clickmewow').click(function () {
                $('#radio1003').attr('checked', 'checked');
            });
            $('input[name="loginAuthenticationType"]').on('change', function () {
                if ($(this).val() == "Yes") {
                    $("#forget-password").hide();
                }
                else {
                    $("#forget-password").show();
                }
            })
        })
    
         
        //$('#BtnNext').off('click').on('click', function () {

        //    alert("The paragraph was clicked.");
        //    fnChkAuthType();
        //});
        

        function fnChkAuthType() {            
            var UserType = '';
            var uFlag = '';
            var LoginId = $('#UserName').val();
            var webUrl = "/api/UserIT/GetUserAuthTypeByLoginId";
            $.ajax({
                type: "POST",
                url: webUrl,
                data: JSON.stringify({
                    LOGIN_ID: LoginId
                }),
                contentType: "application/json; charset=utf-8",
                datatype: "json",
                async: false,
                success: function (res) {
                   
                    //alert(res.StatusFl)
                    /*window.location.href = res + "/" + "Dashboard.aspx";*/
                    
                    uFlag = res.StatusFl;
                    if (uFlag == false) {
                        
                        alert('Please Enter Valid ID');
                         
                    }
                    else {
                        UserType = res.User.UserType;
                    }
                },
                error: function (error) {
                    alert(error);
                }
            })
            if (UserType != null) {
                $("#btnLogin").click(function () {
                    //var inputData = "Your data here"; // Replace with your data

                    $.ajax({
                        type: "POST",
                        url: "Login.aspx/ReceiveDataFromClient",
                        data: JSON.stringify({ data: UserType }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            // Handle the server's response
                            //alert(response.d);
                        },
                        error: function (xhr, textStatus, errorThrown) {
                            alert("Error: " + textStatus);
                        }
                    });
                });
            }
            
            //===========================================
            var authType = $("input[id*=hdnChkAuthType]").val();
            var SSOType = $("input[id*=hdnChkSSOType]").val();
            //$('#UserName').hide();
            //$('#lblEmail').hide();
             /*if (authType == 'Application' || authType == 'Hybrid') {*/
             
            //if (SSOType == 'LocalAD' || SSOType == '') {
            //    $("#lblPassword").show();
            //    $("#Password").show();
            //    $("#BtnNext").hide();
            //    $("#btnLogin").show();
            //}
            //else {
            //    //debugger;
            //    var sUrl = $("input[id*=HiddenADFSUrl]").val();
            //    window.location.href = sUrl;
            //}
            //======================================================
            //if (UserType == 'Application' && authType != 'Hybrid') {
            //    $("#lblPassword").show();
            //    $("#Password").show();
            //    $("#BtnNext").hide();
            //    $("#btnLogin").show();
            //}
            //else {
            //if (uFlag == 'false') {
            //    alert('Please Enter Valid ID !!')
            //}
            if (uFlag == true) {
                $('#UserName').hide();
                $('#lblEmail').hide();
                if (authType == 'Hybrid' && SSOType == 'Cloud' && UserType != 'Application') {
                    var sUrl = $("input[id*=HiddenADFSUrl]").val();
                    window.location.href = sUrl;
                }
                else {
                    $("#lblPassword").show();
                    $("#Password").show();
                    $("#BtnNext").hide();
                    $("#btnLogin").show();
                }
            }
                

             

            //}
            
            return true;
            //}
        }

        function fnLogin() {
            //debugger;
            //var sUrl = $("input[id*=HiddenADFSUrl]").val();
            var Email = $('#UserName').val();
            var Password = $('#Password').val();
            if (Email == '') {
                $('#lblEmail').addClass('requied');
                return false;
            }
            else {
                $('#lblEmail').removeClass('requied');
            }
            //if (SSOType == 'LocalAD' || SSOType == '') {
                if (Password == '') {
                    $('#lblPassword').addClass('requied');
                    //DrawCaptcha();
                    return false;
                }
                else {
                    $('#lblPassword').removeClass('requied');
                }
            //}
            
            //alert($("#enableCaptcha").val());
            var sCaptcha = removeSpaces(document.getElementById('txtCaptchaBox').value);
            //alert("sCaptcha=" + sCaptcha);
            //if ($("#enableCaptcha").val() == "true") {
            //    //alert("In true");
            //    if (sCaptcha == "" || sCaptcha == null || sCaptcha == undefined) {
            //        //alert("Captcha is blank");
            //        alert("Please enter Captcha");
            //        $('#lblCaptcha').addClass('requied');
            //        $('#txtInput').focus();
            //        return false;
            //    }
            //    else {
            //        $('#lblCaptcha').removeClass('requied');
            //    }
            //}

            $("#authenticationType").val($('input[name="loginAuthenticationType"]:checked').val());
            if ($("#enableADLogin").val() == "true") {
                if ($('input[name="loginAuthenticationType"]:checked').val() == 'Yes') {
                    localStorage.setItem("masterTxtWhetherADAuthentication", 'true');
                    document.getElementById('Password').value = Password;
                }
                else {
                    localStorage.setItem("masterTxtWhetherADAuthentication", 'false');
                    var salt = '<%=Session["salt"]%>';
                        var moreSalts = '<%=Session["moreSalt"]%>';
                        var hash = hex_sha512(hex_sha512(hex_sha512(Password) + salt) + salt);
                        var fff = hex_sha512(hash + moreSalts);
                        document.getElementById('Password').value = fff;
                    }
                }
                else {
                    localStorage.setItem("masterTxtWhetherADAuthentication", 'false');
                    var salt = '<%=Session["salt"]%>';
                    var moreSalts = '<%=Session["moreSalt"]%>';
                var hash = hex_sha512(hex_sha512(hex_sha512(Password) + salt) + salt);
                var fff = hex_sha512(hash + moreSalts);
                document.getElementById('Password').value = fff;
            }
            return true;
        }
        function openModal() {
            $('#stack1').modal({ show: true });
        }
        function unValidCredential(msg) {
            alert("You are not authorized to access the application, Please contact compliance officer or system administrator !");
        }
        function inValidCredential() {
            alert("You are not authorized to access the application, Please contact compliance officer or system administrator !");
        }
        function UserInfo(msg) {
            alert(msg);
        }
        function GoToDashBoard(companyId, CompanyNm, ModuleId, ModuleNm, ModuleFolder, ModuleDataBase, EmployeeId) {
            var webUrl = "api/UserCompanyModuleSelection/SetSession";
            $.ajax({
                type: "POST",
                url: webUrl,
                data: JSON.stringify({
                    CompanyId: companyId, CompanyNm: CompanyNm, ModuleId: ModuleId, ModuleNm: ModuleNm,
                    ModuleFolder: ModuleFolder, ModuleDataBase: ModuleDataBase, EmployeeId: EmployeeId
                }),
                contentType: "application/json; charset=utf-8",
                datatype: "json",
                async: true,
                success: function (res) {
                    window.location.href = res + "/" + "Dashboard.aspx";
                },
                error: function (error) {
                    alert(error);
                }
            })
        }
        function fnForgetPassword() {
            if ($("#enableCaptcha").val() == "true") {
                // $('#divEnableCaptcha1').show();
            }
            else {
                // $('#divEnableCaptcha1').hide();
            }
            $('#UserName').val('');
            $('#Password').val('');
            $("#ForgetPasswordPageContent").show();
            $("#LoginPageContent").hide();
            //DrawCaptcha1();
        }
        function fnResetPassword() {
            if ($("#TextBoxLoginId").val() == undefined || $("#TextBoxLoginId").val() == null || $("#TextBoxLoginId").val().trim() == "") {
                $("#LabelResetPassword").addClass('required');
                //DrawCaptcha1();
                return false;
            }
            else {
                $("#LabelResetPassword").removeClass('required');
            }
            return true;
        }
        function removeSpaces(string) {
            return string.split(' ').join('');
        }
        function fnRefreshCaptcha(captchatype) {
            //alert("In function fnRefreshCaptcha");
            //alert("captchatype=" + captchatype);
            if (captchatype == 'resetpassword') {
                $("#ImageCaptchaResetPass").attr("src", "CaptchaImage.aspx");
            }
            else {
                //alert("Here in elase of reset password");
                //alert($("#ImageCaptcha").attr("src"));
                $("#ImageCaptcha").attr("src", "CaptchaImage.aspx");
            }
        }
    </script>
</body>
</html>
