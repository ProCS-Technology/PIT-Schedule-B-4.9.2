<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" CodeBehind="EmailConfig.aspx.cs" Inherits="ProcsDLL.InsiderTrading.EmailConfig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../assets/global/css/components.min.css" rel="stylesheet" />
    <link href="../assets/global/css/plugins.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-content-inner">
        <div class="portlet light portlet-fit">
            <div class="portlet-title">
                <div class="caption">
                    <i class="icon-settings font-red"></i>
                    <span class="caption-subject font-red sbold uppercase">Compliance E-MAIL CONFIGURATION</span>
                </div>
            </div>
            <div class="portlet-body">
                <form class="form-horizontal" role="form" runat="server">
                    <table style='display:none;'>
                        <tr>
                            <td>Redirect Url:</td>
                            <td><asp:TextBox ID="txtRedirectUri" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>Code:</td>
                            <td><asp:TextBox ID="txtCode" runat="server" /></td>
                        </tr>

                        <tr>
                            <td>State:</td>
                            <td><asp:TextBox ID="txtState" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>Pkce:</td>
                            <td><asp:TextBox ID="txtPkce" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>Hashed Pkce:</td>
                            <td><asp:TextBox ID="txtHashedPkce" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>Access Token:</td>
                            <td><asp:TextBox ID="txtToken" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>Refresh Token:</td>
                            <td><asp:TextBox ID="txtRToken" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>Token Type:</td>
                            <td><asp:TextBox ID="txtTokenTyp" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>Start Time:</td>
                            <td><asp:TextBox ID="txtStTime" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>Expires In:</td>
                            <td><asp:TextBox ID="txtExpiresIn" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>Scope:</td>
                            <td><asp:TextBox ID="txtScope" runat="server" /></td>
                        </tr>
                    </table>
                    <input type="text" id="txtConfigId" class="form-control" value="0" style="display:none;" />
                    <div class="form-group">
                        <div class="col-md-4">
                            <label class="control-label">Authentication Type</label>
                        </div>
                        <div class="col-md-8">
                            <div class="input-inline input-medium">
                                <label class="radio-inline">
                                    <input id="isBasic" value="isBasic" type="radio" name="radioAuthenType" />BASIC
                                </label>
                                <label class="radio-inline">
                                    <input id="isSmart" value="isSmart" type="radio" name="radioAuthenType" />SMART
                                </label>
                            </div>
                        </div>
                    </div>
                    <div id="divBasic" style="display: none">
                        <div class="form-group">
                            <div class="col-md-4">
                                <label class="control-label">Display Name</label>
                            </div>
                            <div class="col-md-8">
                                <div class="input-inline input-medium">
                                    <input type="text" id="txtBasicEmailDisplayNm" class="form-control" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-4">
                                <label class="control-label">PIT Email</label>
                            </div>
                            <div class="col-md-8">
                                <div class="input-inline input-medium">
                                    <input type="text" id="txtBasicEmail" class="form-control" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-4">
                                <label class="control-label">Outgoing Protocol(SMTP)</label>
                            </div>
                            <div class="col-md-8">
                                <div class="input-inline input-medium">
                                    <input type="text" id="txtOutgoingProtocol" class="form-control" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-4">
                                <label class="control-label">Outgoing Port(SMTP)</label>
                            </div>
                            <div class="col-md-8">
                                <div class="input-inline input-medium">
                                    <input type="text" id="txtOutgoingPort" class="form-control" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-4">
                                <label class="control-label">Protocol</label>
                            </div>
                            <div class="col-md-8">
                                <div class="input-inline input-medium">
                                    <label class="radio-inline">
                                        <input id="isBasicPop" value="ispop" type="radio" name="radioProtocol" />POP
                                    </label>
                                    <label class="radio-inline">
                                        <input id="isBasicImap" value="isimap" type="radio" name="radioProtocol" />IMAP
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-4">
                                <label class="control-label">Incoming Protocol(Imap/Pop)</label>
                            </div>
                            <div class="col-md-8">
                                <div class="input-inline input-medium">
                                    <input type="text" id="txtIncomingProtocol" class="form-control" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-4">
                                <label class="control-label">Incoming Port(Imap/Pop)</label>
                            </div>
                            <div class="col-md-8">
                                <div class="input-inline input-medium">
                                    <input type="text" id="txtIncomingPort" class="form-control" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-4">
                                <label class="control-label">SSL</label>
                            </div>
                            <div class="col-md-8">
                                <div class="input-inline input-medium">
                                    <select id="ddlBasicSSL" class="form-control">
                                        <option value="0">--Select--</option>
                                        <option value="Yes">Yes</option>
                                        <option value="No">No</option>
                                        <option value="NoWA">No without Authentication</option>
                                    </select>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-4">
                                <label class="control-label">Password</label>
                            </div>
                            <div class="col-md-8">
                                <div class="input-inline input-medium">
                                    <input type="password" id="txtBasicPwd" class="form-control" />
                                </div>
                            </div>
                        </div>
                        <div id="divBasicbtn" class="row">
                            <div class="col-md-4 col-lg-4"></div>
                            <div class="col-md-4 col-lg-4">
                                <a href="#" id="btnBasicSave" onclick="saveEmailConfigBasic()" class="btn btn-large green">Save</a>
                            </div>
                            <div class="col-md-4 col-lg-4"></div>
                        </div>
                    </div>
                    <div id="divSmart" style="display: none">
                        <div class="form-group">
                            <div class="col-md-4">
                                <label class="control-label">Smart Type</label>
                            </div>
                            <div class="col-md-8">
                                <div class="input-inline input-medium">
                                    <select id="ddlSmart" class="form-control">
                                        <option value="0">--Select--</option>
                                        <option value="Google">Google</option>
                                        <option value="Office 365">Office 365</option>
                                    </select>&nbsp;<i id="btnSignIn" class="fa fa-sign-in" style="display:none;" onclick="fnLaunchUrl();"></i>
                                </div>
                            </div>
                        </div>
                        <div id="divOffice" style="display: none">
                            <div class="form-group">
                                <div class="col-md-4">
                                    <label class="control-label">Client Id</label>
                                </div>
                                <div class="col-md-8">
                                    <div class="input-inline input-medium">
                                        <input type="text" id="txtClientIdOffice" class="form-control" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-4">
                                    <label class="control-label">Client Secret</label>
                                </div>
                                <div class="col-md-8">
                                    <div class="input-inline input-medium">
                                        <input type="text" id="txtClientSecretOffice" class="form-control" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-4">
                                    <label class="control-label">Tenant Id</label>
                                </div>
                                <div class="col-md-8">
                                    <div class="input-inline input-medium">
                                        <input type="text" id="txtTenantIdOffice" class="form-control" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-4">
                                    <label class="control-label">Authentication Email</label>
                                </div>
                                <div class="col-md-8">
                                    <div class="input-inline input-medium">
                                        <input type="text" id="txtAuthenticationEmail" class="form-control" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divGoogle" style="display: none">
                            <div class="form-group">
                                <div class="col-md-4">
                                    <label class="control-label">Google Service Account Email</label>
                                </div>
                                <div class="col-md-8">
                                    <div class="input-inline input-medium">
                                        <input type="text" id="txtGoogleServiceAccouontEmail" class="form-control" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-4">
                                    <label class="control-label">Certificate Path</label>
                                </div>
                                <div class="col-md-8">
                                    <div class="input-inline input-medium">
                                        <input type="file" id="txtCertificatePath" class="form-control" />
                                        <div id="divDownload" style="display: none">
                                            <a target="_blank" href="#" id="download">Download</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-4">
                                <label class="control-label">Display Name</label>
                            </div>
                            <div class="col-md-8">
                                <div class="input-inline input-medium">
                                    <input type="text" id="txtSmartEmailDisplayNm" class="form-control" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-4">
                                <label class="control-label">PIT Email</label>
                            </div>
                            <div class="col-md-8">
                                <div class="input-inline input-medium">
                                    <input type="text" id="txtSmartEmail" class="form-control" />
                                    <input type="text" id="hdnSmartEmail" class="form-control" style="display:none;" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-4">
                                <label class="control-label">Outgoing Protocol(SMTP)</label>
                            </div>
                            <div class="col-md-8">
                                <div class="input-inline input-medium">
                                    <input type="text" id="txtSmartOutgoingProtocol" class="form-control" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-4">
                                <label class="control-label">Outgoing Port(SMTP)</label>
                            </div>
                            <div class="col-md-8">
                                <div class="input-inline input-medium">
                                    <input type="text" id="txtSmartOutgoingPort" class="form-control" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-4">
                                <label class="control-label">Protocol</label>
                            </div>
                            <div class="col-md-8">
                                <div class="input-inline input-medium">
                                    <label class="radio-inline">
                                        <input id="isSmartPop" value="ispop" type="radio" name="radioProtocolSmart" />POP
                                    </label>
                                    <label class="radio-inline">
                                        <input id="isSmartImap" value="isimap" type="radio" name="radioProtocolSmart" />IMAP
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-4">
                                <label class="control-label">Incoming Protocol(SMTP)</label>
                            </div>
                            <div class="col-md-8">
                                <div class="input-inline input-medium">
                                    <input type="text" id="txtSmartIncomingProtocol" class="form-control" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-4">
                                <label class="control-label">Incoming Port(SMTP)</label>
                            </div>
                            <div class="col-md-8">
                                <div class="input-inline input-medium">
                                    <input type="text" id="txtSmartIncomingPort" class="form-control" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-4">
                                <label class="control-label">SSL</label>
                            </div>
                            <div class="col-md-8">
                                <div class="input-inline input-medium">
                                    <select id="ddlSmartSSL" class="form-control">
                                        <option value="0">--Select--</option>
                                        <option value="Yes">Yes</option>
                                        <option value="No">No</option>
                                        <option value="NoWA">No without Authentication</option>
                                    </select>
                                </div>
                            </div>
                        </div>
                        
                        
                        <div id="divSmartbtn" class="row">
                            <div class="col-md-4 col-lg-4"></div>
                            <div class="col-md-4 col-lg-4">
                                <a href="#" id="btnSmartSave" onclick="saveEmailConfigSmart()" class="btn btn-large green">Save</a>
                            </div>
                            <div class="col-md-4 col-lg-4"></div>
                        </div>
                    </div>
                </form>                
            </div>
        </div>
    </div>
    <script src="../assets/global/scripts/app.min.js" type="text/javascript"></script>
    <script src="js/moment.js?<%=DateTime.Now %>" type="text/javascript"></script>
    <script src="js/Global.js?<%=DateTime.Now %>" type="text/javascript"></script>
    <script src="js/EmailConfig.js?<%=DateTime.Now %>" type="text/javascript"></script>
</asp:Content>