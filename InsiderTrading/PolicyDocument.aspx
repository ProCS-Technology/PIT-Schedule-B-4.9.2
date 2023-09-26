<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PolicyDocument.aspx.cs" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" Inherits="ProcsDLL.InsiderTrading.PolicyDocument" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../assets/layouts/layout/css/layout.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/layouts/layout/css/themes/darkblue.min.css" rel="stylesheet" type="text/css" id="style_color" />
    <link href="../assets/layouts/layout/css/custom.min.css" rel="stylesheet" type="text/css" />
    <link href="stylesheets/ej/web/default-theme/ej.web.all.min.css" rel="stylesheet" />
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
                <div class="portlet light">
                    <div class="portlet-title">
                        <div class="caption">
                            <i class=" icon-layers font-red"></i>
                            <span class="caption-subject font-red bold uppercase">Policy Document</span>
                        </div>
                        <div class="actions"></div>
                    </div>
                    <div class="portlet-body form">
                        <table class="table table-striped table-hover display text-nowrap">
                            <thead>
                                <tr>
                                    <th><div id="viewer" style="height: 700px; width: 950px; min-height: 404px;"></div></th>
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
    <script src="js/ej/ej.web.all.min.js"></script>
    <script src="js/Global.js?<%=DateTime.Now %>"></script>
    <script type="text/javascript">
        $(function () {
            var webUrl = uri + "/api/PdfViewerIT";
            $("#viewer").ejPdfViewer({
                serviceUrl: webUrl,
                enableStrikethroughAnnotation: false,
                toolbarSettings: { showTooltip: false }
            });
            var pdfViewer = $('#viewer').data('ejPdfViewer');
            pdfViewer.showSelectionTool(false);
            pdfViewer.showPrintTools(false);
            pdfViewer.showDownloadTool(false);
            pdfViewer.showSignatureTool(false);
            pdfViewer.showTextMarkupAnnotationTools(false);
            pdfViewer.showMagnificationTools(false);
            pdfViewer.model.enableTextSelection = false;
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#Loader").hide();
            fnGetPolicy();
        })
        function fnGetPolicy() {
            $("#Loader").show();
            var webUrl = uri + "/api/Policy/GetPolicy";
            $.ajax({
                type: 'GET',
                url: webUrl,
                data: {},
                contentType: "application/json; charset=utf-8",
                datatype: "json",
                async: false,
                success: function (msg) {
                    $("#Loader").hide();
                    if (msg.StatusFl == false) {
                        if (msg.Msg == "Session Expired") {
                            return false;
                        }
                        else {
                            $('#file').val("");
                            return false;
                        }
                    }
                    else {
                        var pdfViewer = $('#viewer').data('ejPdfViewer');
                        pdfViewer.load("../assets/logos/Policy/" + msg.PolicyList[0].DOCUMENT);
                    }
                },
                error: function (error) {
                    $("#Loader").hide();
                    alert(error.status + ' ' + error.statusText);
                }
            })
        }
    </script>
</asp:Content>