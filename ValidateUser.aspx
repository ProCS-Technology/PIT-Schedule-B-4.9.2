<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ValidateUser.aspx.cs" Inherits="ProcsDLL.ValidateUser" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta charset="utf-8" />
        <title>pro-CS | Validation</title>
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <meta content="width=device-width, initial-scale=1" name="viewport" />
        <meta content="Preview page of Metronic Admin Theme #1 for " name="description" />
        <meta content="" name="author" />
        <!-- BEGIN GLOBAL MANDATORY STYLES -->
        <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css" />
        <link href="/assets/global/plugins/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
        <link href="/assets/global/plugins/simple-line-icons/simple-line-icons.min.css" rel="stylesheet" type="text/css" />
        <link href="/assets/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
        <link href="/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />
        <!-- END GLOBAL MANDATORY STYLES -->
        <!-- BEGIN PAGE LEVEL PLUGINS -->
        <link href="/assets/global/plugins/select2/css/select2.min.css" rel="stylesheet" type="text/css" />
        <link href="/assets/global/plugins/select2/css/select2-bootstrap.min.css" rel="stylesheet" type="text/css" />
        <!-- END PAGE LEVEL PLUGINS -->
        <!-- BEGIN THEME GLOBAL STYLES -->
        <link href="/assets/global/css/components.min.css" rel="stylesheet" id="style_components" type="text/css" />
        <link href="/assets/global/css/plugins.min.css" rel="stylesheet" type="text/css" />
        <!-- END THEME GLOBAL STYLES -->
        <!-- BEGIN PAGE LEVEL STYLES -->
        <link href="/assets/pages/css/login-5.min.css" rel="stylesheet" type="text/css" />
        <!-- END PAGE LEVEL STYLES -->
        <!-- BEGIN THEME LAYOUT STYLES -->
        <!-- END THEME LAYOUT STYLES -->
        <link rel="shortcut icon" href="favicon.ico" />
        <style type="text/css">
            .requied {
                color: red;
            }
        </style>
    </head>
    <body class="login">
        <form id="form1" runat="server">
            <asp:TextBox runat="server" ID="txtName" Visible="false" />
            <asp:TextBox runat="server" ID="txtEmail" Visible="false" />
            <div class="user-login-5">
                <div class="row bs-reset">
                    <div class="col-md-6 bs-reset mt-login-5-bsfix">
                        <div class="login-bg" style="background-image: url('assets/images/bg1.jpg')"></div>
                    </div>
                    <div class="col-md-6 login-container bs-reset mt-login-5-bsfix">
                        <div class="login-content" id="LoginPageContent">
                            <h1>ProCS</h1>
                            <p style="font-size:18px;color:navy;">Corporate Sustainability Suite</p>
                        </div>
                        <div class="login-footer">
                            <div class="row bs-reset">
                                <div class="col-xs-5 bs-reset" style="display:none;">
                                    <ul class="login-social">
                                        <li>
                                            <a href="javascript:;"><i class="icon-social-facebook"></i></a>
                                        </li>
                                        <li>
                                            <a href="javascript:;"><i class="icon-social-twitter"></i></a>
                                        </li>
                                        <li>
                                            <a href="javascript:;"><i class="icon-social-dribbble"></i></a>
                                        </li>
                                    </ul>
                                </div>
                                <div class="col-xs-7 bs-reset">
                                    <div class="login-copyright text-right">
                                        <p>Copyright &copy; ProCS Technology | 2019</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </form>
        <div class="modal fade" id="stack1" tabindex="-1" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog" style="width: 771px;">
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
        <script src="/assets/global/plugins/jquery.min.js" type="text/javascript"></script>
        <script src="/assets/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
        <script src="/assets/global/plugins/js.cookie.min.js" type="text/javascript"></script>
        <script src="/assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
        <script src="/assets/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
        <script src="/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js" type="text/javascript"></script>
        <script src="/assets/global/plugins/jquery-validation/js/jquery.validate.min.js" type="text/javascript"></script>
        <script src="/assets/global/plugins/jquery-validation/js/additional-methods.min.js" type="text/javascript"></script>
        <script src="/assets/global/plugins/select2/js/select2.full.min.js" type="text/javascript"></script>
        <script src="/assets/global/plugins/backstretch/jquery.backstretch.min.js" type="text/javascript"></script>
        <script src="/assets/global/scripts/app.min.js" type="text/javascript"></script>
        <script src="/assets/pages/scripts/login-5.js" type="text/javascript"></script>
        <script>
            function preventBack() { window.history.forward(); }
            setTimeout("preventBack()", 0);
            window.onunload = function () { null };
            $(document).ready(function () {
                $('#clickmewow').click(function () {
                    $('#radio1003').attr('checked', 'checked');
                });
            })
            function InValidCredential(samlEndpoint) {
                alert("You are not authorized to access this application, please contact Compliance Officer for the access of the application.");
                window.location = samlEndpoint;
            }
        </script>
    </body>
</html>