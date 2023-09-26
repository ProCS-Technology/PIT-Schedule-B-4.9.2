<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrainingModule.aspx.cs" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" Inherits="ProcsDLL.InsiderTrading.TrainingModule" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .page-content-wrapper .page-content {
            min-height: 1683px !important;
        }
    </style>
    <link href="stylesheets/ej/web/default-theme/ej.web.all.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <input runat="server" id="txtTrainingModuleId" type="hidden" />
    <input runat="server" id="txtTotalNoOfPages" type="hidden" />
    <input runat="server" id="txtUserTrainingModuleStatus" type="hidden" />
    <input runat="server" id="txtTrainingModuleDeatilId" type="hidden" />
    <input runat="server" id="txtCurrentPage" type="hidden" />
    <div class="col-md-12">
        <div class="portlet box">
            <div class="portlet-title">
                <div class="caption">
                    <span style="color: black">Training Module</span>
                </div>
                <div class="tools"></div>
            </div>
            <div class="portlet-body">
                <ol>
                    <li>Please review the presentation on below to complete your training</li>
                    <li>Please use 'Previous' or 'Next' to navigate between the slides</li>
                    <li>On the last please submit your remarks and press 'Finish' to complete your training</li>
                </ol>
                <div class="row">
                    <div class="col-md-12">
                        <a href="javascript:fnGoToPreviousPage();" class="btn default button-previous" style="background-color: blue; color: white; display: none;">
                            <i class="fa fa-angle-left"></i>
                            Previous
                        </a>
                        <a href="javascript:fnGoToNextPage();" class="btn btn-outline green button-next pull-right" style="background-color: blue; color: white; display: none;">
                            Next
                            <i class="fa fa-angle-right"></i>
                        </a>
                        <a style="color: white; background-color: #26c281; border-color: #e1e5ec; display: none;" href="#approve" class="btn green btn-final pull-right" data-toggle="modal">
                            Submit <i class="fa fa-check"></i>
                        </a>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12" id="divTemplateAudioVideo">
                        <div id="viewer" style="height: 700px; width: 100%; min-height: 404px;"></div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <a href="javascript:fnGoToPreviousPage();" class="btn default button-previous" style="background-color: blue; color: white; display: none;">
                            <i class="fa fa-angle-left"></i>
                            Previous 
                        </a>
                        <a href="javascript:fnGoToNextPage();" class="btn btn-outline green button-next pull-right" style="background-color: blue; color: white; display: none;">
                            Next
                            <i class="fa fa-angle-right"></i>
                        </a>
                        <a style="color: white; background-color: #26c281; border-color: #e1e5ec; display: none;" href="#approve" class="btn green btn-final pull-right" data-toggle="modal">
                            Submit <i class="fa fa-check"></i>
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="approve" class="modal fade" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Complete</h5>

                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>

                <div class="modal-body p-20">
                    <p>Please provide your comments below, if any, and press confirm to submit the request.</p>

                    <textarea id="txtAreaApprove" name="textarea" class="form-control" placeholder="Type your comments here..."></textarea>
                    <br />
                    <div class="row m-t-20">
                        <div class="col-md-12 text-center">
                            <a class="btn green" href="javascript:fnSubmitActionTaken('Completed','txtAreaApprove');">Finish</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="js/ej/ej.web.all.min.js"></script>
    <script src="js/Global.js?<%=DateTime.Now %>"></script>
    <script type="text/javascript">
        $(function () {
            var webUrl = uri + "/api/PdfViewerIT";
            $("#viewer").ejPdfViewer({
                serviceUrl: webUrl,
                //documentPath: "HTTP Succinctly",
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
            pdfViewer.showTextSearchTool(false);
            pdfViewer.showPageNavigationTools(false);
            pdfViewer.model.enableTextSelection = false;
        });
    </script>
    <script src="js/TrainingModule.js?<%=DateTime.Now %>"></script>
</asp:Content>