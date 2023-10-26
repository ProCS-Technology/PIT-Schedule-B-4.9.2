<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" CodeBehind="VideoModule.aspx.cs" Inherits="ProcsDLL.InsiderTrading.VideoModule" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .page-content-wrapper .page-content {
            min-height: 1683px !important;
        }
    </style>
    <%--<link href="stylesheets/ej/web/default-theme/ej.web.all.min.css" rel="stylesheet" />--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <input runat="server" id="txtVideoId" type="hidden" />
    <input runat="server" id="txtVideoTitle" type="hidden" />

    
    <div class="col-md-12">
        <div class="portlet box">
            <div class="portlet-title">
                <div class="caption">
                    <span style="color:black" id="spnTitle"></span>
                </div>
                <div class="tools">
                </div>
            </div>
            <div class="portlet-body">
                <div class="row">
                    <div class="col-md-12" id="divTemplateAudioVideo">
                        <div id="viewerX" style="height: 700px; width: 100%; min-height: 404px;"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--<script src="js/ej/ej.web.all.min.js"></script>--%>
    <script src="js/Global.js?<%=DateTime.Now %>"></script>
    <script type="text/javascript">
        //$(function () {
        //    var webUrl = uri + "/api/PdfViewerIT";
        //    $("#viewerX").ejPdfViewer({
        //        serviceUrl: webUrl,
        //        //documentPath: sUrl,
        //        enableStrikethroughAnnotation: false,
        //        toolbarSettings: { showTooltip: false }
        //    });
        //    var pdfViewer = $('#viewerX').data('ejPdfViewer');
        //    pdfViewer.showSelectionTool(false);
        //    pdfViewer.showPrintTools(false);
        //    pdfViewer.showDownloadTool(false);
        //    pdfViewer.showSignatureTool(false);
        //    pdfViewer.showTextMarkupAnnotationTools(false);
        //    pdfViewer.showMagnificationTools(false);
        //    pdfViewer.showTextSearchTool(false);
        //    pdfViewer.showPageNavigationTools(false);
        //    pdfViewer.model.enableTextSelection = false;
        //});
    </script>
    <script src="js/VideoModule.js?<%=DateTime.Now %>"></script>
</asp:Content>