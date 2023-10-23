<%@ Page ValidateRequest="true" Language="C#" AutoEventWireup="true" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" CodeBehind="BenposFieldMapping.aspx.cs" Inherits="ProcsDLL.InsiderTrading.BenposFieldMapping" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../assets/global/css/components.min.css" rel="stylesheet" />
    <link href="../assets/global/css/plugins.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <div class="page-content-inner">
            <div class="row">
                <div class="col-md-12">
                    <div class="portlet light portlet-fit ">
                        <div class="portlet-title">
                            <div class="caption">
                                <i class="icon-settings font-red"></i>
                                <span class="caption-subject font-red sbold uppercase">Benpos/ESOP Field Mapping</span>
                            </div>
                        </div>
                        <div class="portlet-body">
                            <div class="table-toolbar text-center">
                                <div class="row">
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="DropDownListType" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListType_SelectedIndexChanged">
                                            <asp:ListItem Value="Benpos">Benpos</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <table class="table table-striped table-hover" id="tbl-Department-setup">
                                <thead>
                                    <tr>
                                        <th>&nbsp;</th>
                                        <th>Type</th>
                                        <th>Excel File Field Name</th>
                                    </tr>
                                </thead>
                                <tbody id="tbdDepartmentList">                                    
                                    <tr>
                                        <td>1</td>
                                        <td>Name</td>
                                        <td>
                                            <asp:TextBox ID="TextBoxName" CssClass="form-control" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>2</td>
                                        <td id="lblholding">
                                            Holding <span class="text-danger">* </span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBoxHolding" CssClass="form-control" runat="server" onkeypress="javascript:fnRemoveClass('lblholding');"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>3</td>
                                        <td id="lblfolio">
                                            Folio <span class="text-danger">* </span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBoxFolio" CssClass="form-control" runat="server" onkeypress="javascript:fnRemoveClass('lblfolio');"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>4</td>
                                        <td id="lblpan">
                                            PAN <span class="text-danger">* </span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBoxPan" CssClass="form-control" runat="server" onkeypress="javascript:fnRemoveClass('lblpan');"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>5</td>
                                        <td>Type</td>
                                        <td>
                                            <asp:TextBox ID="TextBoxCategory" CssClass="form-control" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <asp:Button ID="ButtonSave" runat="server" CssClass="btn btn-primary" OnClick="SaveBenposField" OnClientClick="return fnValidateForm();" Text="Save" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script src="../assets/global/scripts/datatable.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/datatables/datatables.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js" type="text/javascript"></script>
    <script src="../assets/global/scripts/app.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/jquery-validation/js/jquery.validate.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/jquery-validation/js/additional-methods.min.js" type="text/javascript"></script>
    <script src="js/Global.js"></script>
    <script type="text/javascript">
        function fnValidateForm() {
            var count = 0;
            var holding = $("input[id*='TextBoxHolding']").val();
            var pan = $("input[id*='TextBoxPan']").val();
            var folio = $("input[id*='TextBoxFolio']").val();
            if (holding == undefined || holding == null || holding == "") {
                $("#lblholding").addClass('text-danger');
                count++;
            }
            else {
                $("#lblholding").removeClass('text-danger');
            }

            if (folio == undefined || folio == null || folio == "") {
                $("#lblfolio").addClass('text-danger');
                count++;
            }
            else {
                $("#lblfolio").removeClass('text-danger');
            }
            if (pan == undefined || pan == null || pan == ""){
                $("#lblpan").addClass('text-danger');
                count++;
            }
            else {
                $("#lblpan").removeClass('text-danger');
            }

            if (count > 0) {
                return false;
            }
            else {               

                return true;
            }            
        }
        function fnRemoveClass(val) {
            $("#" + val).removeClass('text-danger');
        }
    </script>
</asp:Content>