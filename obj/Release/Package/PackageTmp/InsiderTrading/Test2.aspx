﻿<%@ Page Language="C#" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" AutoEventWireup="true" CodeBehind="Test2.aspx.cs" Inherits="ProcsDLL.InsiderTrading.Test2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../assets/pages/css/profile.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/jquery-ui/jquery-ui.min.css" rel="stylesheet" />
    <style type="text/css">
        .required-red {
            color: red;
        }
    </style>
    <link rel="shortcut icon" href="favicon.ico" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="tokengerating" style="margin-top:100px;text-align:center;">
        <h3 id="tokengenrated">Token is generating Please Wait......</h3>
    </div>
    <script src="../assets/global/scripts/app.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="js/Global.js?<%=DateTime.Now%>" type="text/javascript"></script>
    <script src="js/Office365Token.js?ts<% =DateTime.Now %>" type="text/javascript"></script>
</asp:Content>