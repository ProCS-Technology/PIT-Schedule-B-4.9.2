<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SmsTest.aspx.cs" Inherits="SmsTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <b>Mobile Number</b> <asp:TextBox ID="TextBox1" MaxLength="10" runat="server"></asp:TextBox><br /><br />

        <asp:Button ID="Button1" runat="server" Text="Send SMS" OnClick="Button1_Click" /><br /><br />
            
         <b>OutPut:-</b>  
        <br /><br />
        <asp:Label Id="Label1" runat="server"></asp:Label>

        <b>Email Id</b> <asp:TextBox ID="TextBox2" MaxLength="50" runat="server"></asp:TextBox><br /><br />
         <asp:Button ID="Button2" runat="server" Text="Send Email" OnClick="Button2_Click" /><br /><br />
       
                   
    </div>
    </form>
</body>
</html>
