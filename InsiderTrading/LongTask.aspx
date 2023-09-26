<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LongTask.aspx.cs" Inherits="ProcsDLL.InsiderTrading.LongTask" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
    </head>
    <body>
        <form id="form1" runat="server">
            <asp:ScriptManager ID="sm1" runat="server" />
		    <%--<asp:ModalUpdateProgress ID="ModalUpdateProgress1" runat="server" DisplayAfter="0"
                BackgroundCssClass="modalProgressGreyBackground">
                <ProgressTemplate>
                    <div class="modalPopup">                                            
                        <table border="0" align="center" cellpadding="0" cellspacing="0">
                            <tr>
                                <td><b>Loading...</b></td>
                            </tr>
                            <tr><td>&nbsp;</td></tr>
                            <tr>
                                <td align="center">
                                   <img src="images/ig_progressIndicator1.gif" alt="" align="middle" />
                                </td>
                            </tr>
                        </table>     
                    </div>
                </ProgressTemplate>
            </asp:ModalUpdateProgress>--%>
            <%--<ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="108000" CombineScripts="false"/>
		    <asp:ModalUpdateProgress ID="ModalUpdateProgress1" runat="server" DisplayAfter="0"
                BackgroundCssClass="modalProgressGreyBackground">
                <ProgressTemplate>
                    
                </ProgressTemplate>
            </asp:ModalUpdateProgress>--%>
			<asp:UpdatePanel ID="UpdatePanel1" runat="server">
				<ContentTemplate>
					<asp:Button ID="btnpartial" runat="server" onclick="btnpartial_Click" Text="Partial PostBack" /><br /><br />
					<asp:Label ID="lblpartial" runat="server"></asp:Label>
				</ContentTemplate>
				<Triggers>
					<asp:AsyncPostBackTrigger ControlID="btntotal" />
				</Triggers>
			</asp:UpdatePanel>
			<p></p>
			<p>Outside the Update Panel</p>
			<p>
				<asp:Button ID="btntotal" runat="server" onclick="btntotal_Click" Text="Total PostBack" />
			</p>
			<asp:Label ID="lbltotal" runat="server"></asp:Label>
		</form>
    </body>
</html>