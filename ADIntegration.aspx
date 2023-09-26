<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ADIntegration.aspx.cs" Inherits="ProcsDLL.ADIntegration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <table>
                <tr>
                    <td>Path (LDAP)</td>
                    <td><asp:TextBox ID="txtPath" runat="server" Width="800px" Height="50px" /></td>
                </tr>
                <tr>
                    <td>User Id</td>
                    <td><asp:TextBox ID="txtUserId" runat="server" Width="800px" Height="50px" /></td>
                </tr>
                <tr>
                    <td>Password</td>
                    <td><asp:TextBox ID="txtPassword" runat="server"  Width="800px" Height="50px" /></td>
                </tr>
                <tr>
                    <td>User Name</td>
                    <td><asp:TextBox ID="txtUserName" runat="server"  Width="800px" Height="50px" /></td>
                </tr>
                <tr>
                    <td colspan="2"><asp:Button ID="btnADIntegration" Text="ADIntegration" runat="server" OnClick="btnADIntegration_onClick" /></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblMsg" runat="server" />
                </tr>
            </table>
    </div>
    </form>
</body>
</html>
