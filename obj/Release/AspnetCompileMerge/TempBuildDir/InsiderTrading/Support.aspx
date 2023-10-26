<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" CodeBehind="Support.aspx.cs" Inherits="ProcsDLL.InsiderTrading.Support" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../assets/layouts/layout/css/layout.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/layouts/layout/css/themes/darkblue.min.css" rel="stylesheet" type="text/css" id="style_color" />
    <link href="../assets/layouts/layout/css/custom.min.css" rel="stylesheet" type="text/css" />
    <style>
        .page-content {
            min-height: 800px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-content-inner">
        <div class="row">
            <div class="col-md-12">
                <div class="portlet light" style="height: 500px;">
                    <div class="portlet-title">
                        <div class="caption">
                            <i class=" icon-layers font-red"></i>
                            <span class="caption-subject font-red bold uppercase">Support
                            </span>
                        </div>
                        <div class="actions">
                        </div>
                    </div>
                    <div class="portlet-body form">
                        <table class="table table-striped table-hover display text-nowrap">
                            <thead>
                                <tr>
                                    <th>For queries for Declaration Submission/Trade Request etc. contact at <span style="color: red">Compliance@iexindia.com / Team Compliance</span></th>
                                </tr>
                                <tr>
                                    <th>For any IT related support contact at <span style="color: red">IT-Helpdesk@iexindia.com</span></th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script src="../assets/global/plugins/js.cookie.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $("#Loader").hide();
    </script>
    <script src="js/Global.js?<%=DateTime.Now %>"></script>
</asp:Content>
