<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testEmail.aspx.cs" Inherits="ProcsDLL.InsiderTrading.testEmail" %>

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
                    <td>User Email</td>
                    <td>
                        <asp:TextBox ID="txtUserEmail" runat="server" Width="800px" Height="50px" /></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnADIntegration" Text="ADIntegration" runat="server" OnClick="btnEmailTesting_onClick" /></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
