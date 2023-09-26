<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EncryptDecrypt.aspx.cs" Inherits="ProcsDLL.EncryptDecrypt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        var counter = 1;
        function DisableButton() {
            document.getElementById("<%=Button1.ClientID %>").disabled = true;
        }
        function fnOnLoad() {
            document.getElementById("txtClientConuter").value = counter;
        }
        function fnValidate() {
            counter++;
            document.getElementById("txtClientConuter").value = counter;
        }
        window.onload = fnOnLoad;
        window.onbeforeunload = DisableButton;
    </script>
    <script type="text/javascript">
        function DisableButtons() {
            var inputs = document.getElementsByTagName("INPUT");
            for (var i in inputs) {
                if (inputs[i].type == "button" || inputs[i].type == "submit") {
                    inputs[i].disabled = true;
                }
            }
        }
            // window.onbeforeunload = DisableButtons; //uncomment to use this script.
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="txtClientConuter" runat="server" />
            <asp:TextBox ID="txtCounter" runat="server" />
            <asp:Button ID="Button1" runat="server" Text="Button" OnClientClick="fnValidate();" OnClick="Button1_Clicked" />
            <br />
            <br />
            <br />

            <table>
                <tr>
                    <td>String</td>
                    <td>
                        <asp:TextBox ID="txtString" runat="server" Width="800px" Height="50px" /></td>
                </tr>
                <tr>
                    <td>Encrypted</td>
                    <td>
                        <asp:TextBox ID="txtEString" runat="server" Width="800px" Height="50px" /></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnEncrypt" Text="Encrypt" runat="server" OnClick="btnEncrypt_onClick" /></td>
                </tr>

                <tr>
                    <td>Encrypted</td>
                    <td>
                        <asp:TextBox ID="txtEnString" runat="server" Width="800px" Height="50px" /></td>
                </tr>
                <tr>
                    <td>String</td>
                    <td>
                        <asp:TextBox ID="txtDString" runat="server" Width="800px" Height="50px" /></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnDecrypt" Text="Decrypt" runat="server" OnClick="btnDecrypt_onClick" /></td>
                </tr>

                <tr>
                    <td>String</td>
                    <td>
                        <asp:TextBox ID="txtWithoutShaString" runat="server" Width="800px" Height="50px" /></td>
                </tr>
                <tr>
                    <td>SHA 512 Encryption</td>
                    <td>
                        <asp:TextBox ID="txtShaString" runat="server" Width="800px" Height="50px" /></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="Button2" Text="Encrypt" runat="server" OnClick="btnEncryptSha512_onClick" /></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
