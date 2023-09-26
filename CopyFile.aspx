<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CopyFile.aspx.cs" Inherits="ProcsDLL.CopyFile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" method="post" runat="server" enctype="multipart/form-data" action="CopyFile.aspx">
        <div>
            <asp:Label ID="LabelMessage" runat="server" Text=""></asp:Label><br /><br />
            <asp:FileUpload ID="FileUpload1" runat="server" /><br /><br />
            <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
        </div>
    </form>
</body>
</html>
