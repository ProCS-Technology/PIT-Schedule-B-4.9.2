<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" CodeBehind="UPSITemplateUpload.aspx.cs" Inherits="ProcsDLL.InsiderTrading.UPSITemplateUpload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <style>
        .required-red {
            color: red;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="portlet light bordered">
        <div class="portlet-title">
            <div class="caption font-red-sunglo">
                <i class="icon-settings font-red-sunglo"></i>
                <span class="caption-subject bold uppercase">UPSI Library</span>
            </div>
        </div>
        <div class="portlet-body">
            <div class="table-toolbar">
                <div class="row">
                    <div class="col-md-2">
                        <div class="btn-group-devided">
                            <button id="btnUpload" runat="server" class="btn green" data-toggle="modal" data-target="#modalUploadMOM">
                                Upload UPSI
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="modalUploadMOM" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title">Upload UPSI</h4>
                </div>
                <div class="modal-body">
                    <div class="portlet-body form">
                        <div id="divExcel" class="row">
                            <div class="col-md-4" id="lblExcel">Upload Excel</div>
                            <div class="col-md-8">
                                <input type="file" id="txtExcelDoc" onchange="javascript:fnRemoveClass(this,'Excel');" class="form-control" data-tabindex="4" />
                            </div>
                        </div><br />
                        <div id="divZip" class="row">
                            <div class="col-md-4" id="lblZip">Upload Zip</div>
                            <div class="col-md-8">
                                <input type="file" id="txtZipDoc" onchange="javascript:fnRemoveClass(this,'Zip');" class="form-control" data-tabindex="4" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" data-dismiss="modal" class="btn default" onclick="javascript:fnClearUploadMOM();">Cancel</button>
                    <button id="btnDownloadMOM" type="submit" class="btn green" onclick="javascript:fnDownloadMOMTemplate();">Download Template</button>
                    <button id="btnUploadMOM" type="submit" class="btn green" onclick="javascript:fnUploadUPSITemplate();">Upload</button>
                </div>
            </div>
        </div>
    </div>
    <script src="../assets/global/scripts/datatable.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/datatables/datatables.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js" type="text/javascript"></script>
    <script src="js/Global.js?<%=DateTime.Now %>" type="text/javascript"></script>
    <script src="js/UPSITemplateUpload.js?<%=DateTime.Now %>" type="text/javascript"></script>
</asp:Content>