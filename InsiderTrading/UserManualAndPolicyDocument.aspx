<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" CodeBehind="UserManualAndPolicyDocument.aspx.cs" Inherits="ProcsDLL.InsiderTrading.UserManualAndPolicyDocument" %>

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
                            <span class="caption-subject font-red bold uppercase">User Manual 
                            </span>
                        </div>
                        <div class="actions">
                        </div>
                    </div>
                    <div class="portlet-body form">
                        <table class="table table-striped table-hover display text-nowrap">
                            <thead>
                                <tr>
                                    <th>
                                        <div id="viewer" style="height: 700px; width: 950px; min-height: 404px;"></div>
                                    </th>
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
            //alert("Here");
            var webUrl = uri + "/api/PdfViewerIT";
            $("#viewer").ejPdfViewer({
                serviceUrl: webUrl,
                //documentPath: "https://pit.firstsource.com/InsiderTrading/UserManual/User-Manual.pdf",
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

            setTimeout(fnLoadManual(), 5000);
        });

        

        //$(document).ready(function () {
        //    $("#Loader").hide();

        //    var pdfViewer = $('#viewer').data('ejPdfViewer');

        //    var path = "/";
        //    for (var i = 1; i <= (window.location.pathname.split("/")).length - 2; i++) {
        //        path += window.location.pathname.split("/")[i] + "/";
        //    }

        //    var manual = "UserManual/User-Manual.pdf";
        //    pdfViewer.load(window.location.origin + path + manual);
        //})
        function fnLoadManual() {
            var pdfViewer = $('#viewer').data('ejPdfViewer');
            var path = "https://pit.firstsource.com/InsiderTrading/UserManual/User-Manual.pdf";
            //alert("path=" + path);
            pdfViewer.load(path);
        }
    </script>

</asp:Content>
