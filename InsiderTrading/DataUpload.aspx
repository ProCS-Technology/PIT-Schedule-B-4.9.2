<%@ Page Title="" Language="C#" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" AutoEventWireup="true" CodeBehind="DataUpload.aspx.cs" Inherits="ProcsDLL.InsiderTrading.DataUpload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <div class="page-content-inner">
            <div class="col-md-12">
                <div class="portlet light portlet-fit" style="margin-left: -15px; margin-right: -15px; min-height: 700px;">
                    <div class="portlet-title">
                        <div class="caption">
                            <i class="icon-settings font-red"></i>
                            <span class="caption-subject font-red sbold uppercase">Bulk Upload</span>
                        </div>
                    </div>
                    <div class="portlet-body">
                        <asp:Label ID="LABEL_MSG" runat="server" Text="" CssClass="text-danger bold margin-bottom-20"></asp:Label>
                        <asp:FileUpload ID="FileUploadExcel" class="form-control form-control-solid" runat="server" accept=".xls,.xlsx" required="" />
                            <div class="margin-top-20">
                            <asp:Button ID="ButtonUpload" runat="server" CssClass="btn btn-info" Text="Upload" OnClick="ButtonUpload_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <script type="text/javascript">
            $(document).ready(function () {
                $("#Loader").hide();
            });
        </script>
    </form>
</asp:Content>
