<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" CodeBehind="Dashboard.aspx.cs" Inherits="ProcsDLL.InsiderTrading.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../assets/global/css/components.min.css" rel="stylesheet" />
    <link href="../assets/global/css/plugins.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/bootstrap-timepicker/css/bootstrap-timepicker.min.css" rel="stylesheet" />
    <link href="stylesheets/Dashboard.css" rel="stylesheet" type="text/css"/>
    <link href="../assets/global/plugins/select2/css/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/select2/css/select2-bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/jquery-multi-select/css/multi-select.css" rel="stylesheet" type="text/css" />
    <style>
        .modal-body {
            max-height: calc(100vh - 212px);
            overflow-y: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form runat="server">
        <asp:TextBox runat="server" ID="txtWhetherWindowsAuthentication" Style="display: none;"></asp:TextBox>
        <asp:TextBox runat="server" ID="txtWhetherADAuthentication" Style="display: none;"></asp:TextBox>
        <asp:TextBox runat="server" ID="txtCompanyName" Style="display: none;"></asp:TextBox>
    </form>
    <script type="text/javascript">
        localStorage.setItem("masterTxtWhetherWindowsAuthentication", $("#ContentPlaceHolder1_txtWhetherWindowsAuthentication").val());
    </script>
    <div class="">
        <div id="" class="col-md-12">
            <div class="portlet box">
                <div class="portlet-title">
                    <div class="caption">
                        <span style="color: black">Dashboard</span>
                    </div>
                </div>
                <div class="portlet-body slide-left">
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 margin-bottom-10">
                            <marquee behavior="scroll" onmouseover="this.stop();" onmouseout="this.start();" direction="left" 
                                scrollamount="5" loop="infinite" id="divTradingWindowClosureNotification"></marquee>
                        </div>
                    </div>
                </div>
            </div>
        </div>       
        <div id="" class="col-md-12">
              <div class="portlet box" id="dvMyActionable">
                <div class="portlet-title">
                    <div class="caption">
                        <span style="color: black">My Actionable&nbsp;<span id="spnMyActionableCount"></span></span>
                    </div>
                    <div class="tools"></div>
                </div>
                <div class="portlet-body">
                    <%-- <p style="color:red;">(PLEASE KEEP THE FOLLOWING DETAILS HANDY FOR FILLING UP THE FORM.- PAN, SPOUSE'S PAN, DEMAT ACCOUNT DETAILS AND NUMBER OF SHARES HELD IN BIRLASOFT.)</p>
                    <br />--%>
                    <br />
                     <div id="divTaskOpenLink" runat="server" class="col-md-12" visible="false" style="padding-bottom: 2%;" >
            <u><a href="UserDeclaration.aspx" runat="server" id="taskOpenLink" style="color:blue">Please Click here to submit your <label runat="server" id="lblDisclosureName" ></label>. Last date of Submission for your disclosure is - <label runat="server" id="lblLastDate" style="color:red" ></label></a></u>
        </div><br />
                    <ol id="olMyActionableTask"></ol>
                    <br />
                    <div id="Trade_PortletBox" class="portlet box My_Trade_PortletBox" style="display: none;">
                        <div data-toggle="collapse" id="Trade_PortletTitle" class="portlet-title" data-target="#TradeDetailsTab">
                            <div class="caption" style="padding-top: 6px !important; font-size: 18px;">
                                <i class="fa fa-gift"></i>Trade
                            </div>
                            <div class="tools" style="padding-top: 6px !important;">
                                <a style="color: white"><i id="aTradeDetailsTab" class="fa fa-angle-up"></i></a>
                            </div>
                        </div>
                        <div id="TradeDetailsTab" class="portlet-body form collapse in">
                            <form class="form-horizontal" role="form">
                                <table class="table table-striped table-hover table-bordered">
                                    <thead>
                                        <tr>
                                            <th>Name</th>
                                            <th>PAN</th>
                                            <th>DEMAT</th>
                                            <th>Trade Date</th>
                                            <th>Trade Quantity</th>
                                            <th>Buy/Sell</th>
                                            <th>Action</th>
                                        </tr>
                                    </thead>
                                    <tbody id="tbodyTrade"></tbody>
                                </table>
                            </form>
                        </div>
                    </div>
                    <div id="ActionableCompliance_PortletBox" class="portlet box My_Trade_PortletBox display-none">
                        <div data-toggle="collapse" id="ActionableCompliance_PortletTitle" class="portlet-title" data-target="#ActionableComplianceTab">
                            <div class="caption" style="padding-top: 6px !important; font-size: 18px;">
                                <i class="fa fa-newspaper-o"></i>Transaction List
                            </div>
                            <div class="tools" style="padding-top: 6px !important;">
                                <a style="color: white"><i id="aActionableComplianceTab" class="fa fa-angle-up"></i></a>
                            </div>
                        </div>
                        <div id="ActionableComplianceTab" class="portlet-body form collapse in">
                            <form class="form-horizontal" role="form">
                                <table class="table table-striped table-hover table-bordered">
                                    <thead>
                                        <tr>
                                            <th>Relative</th>
                                            <th>Relation</th>
                                            <th>PAN</th>
                                            <th>DP/Client Id</th>
                                            <th>Qty</th>
                                            <th>Estimated Trade Value</th>
                                            <th>Buy/Sell</th>
                                            <th>Transaction Date</th>
                                            <th>Actions</th>
                                        </tr>
                                    </thead>
                                    <tbody id="tbodyCompliance"></tbody>
                                </table>
                            </form>
                        </div>
                    </div>
                    
                    <div id="NonCompliance_PortletBox" class="portlet box My_Trade_PortletBox">
                        <div data-toggle="collapse" id="NonCompliance_PortletTitle" class="portlet-title" data-target="#NonComplianceTab">
                            <div class="caption" style="padding-top: 6px !important; font-size: 18px;">
                                <i class="fa fa-gift"></i>Disclosure Required
                            </div>
                            <div class="tools" style="padding-top: 6px !important;">
                                <a style="color: white"><i id="aNonComplianceTab" class="fa fa-angle-up"></i></a>
                            </div>
                        </div>
                        <div id="NonComplianceTab" class="portlet-body form collapse in">
                            <form class="form-horizontal" role="form">
                                <table class="table table-striped table-hover table-bordered">
                                    <thead>
                                        <tr>
                                            <th class="display-none">User Name</th>
                                            <th>Relative</th>
                                            <th>Relation</th>
                                            <th>PAN</th>
                                            <th>DP/Client Id</th>
                                            <th>Non-Compliance Type</th>
                                            <th>Qty</th>
                                            <th>Estimated Trade Value</th>
                                            <th>Buy/Sell</th>
                                            <th>Transaction Date</th>
                                            <th>Actions</th>
                                        </tr>
                                    </thead>
                                    <tbody id="tbodyNonComplianceType"></tbody>
                                </table>
                            </form>
                        </div>
                    </div>
                    <div id="UPSI_PortletBox" class="portlet box My_Trade_PortletBox">
                        <div data-toggle="collapse" id="UPSI_PortletTitle" class="portlet-title" data-target="#UPSITab">
                            <div class="caption" style="padding-top: 6px !important; font-size: 18px;">
                                <i class="fa fa-gift"></i>UPSI
                            </div>
                            <div class="tools" style="padding-top: 6px !important;">
                                <a style="color: white"><i id="aUPSITab" class="fa fa-angle-up"></i></a>
                            </div>
                        </div>
                        <div id="UPSITab" class="portlet-body form collapse in">
                            <form class="form-horizontal" role="form">
                                <table class="table table-striped table-hover table-bordered">
                                    <thead>
                                        <tr>
                                            <th>Email Date</th>
                                            <th>Email From</th>
                                            <th>Email To</th>
                                            <th>Subject</th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                    <tbody id="UPSITaskTbody"></tbody>
                                </table>
                            </form>
                        </div>
                    </div>

                    <div id="Esop_PortletBox" class="portlet box My_Trade_PortletBox">
                        <div data-toggle="collapse" id="Esop_PortletTitle" class="portlet-title" data-target="#EsopTab">
                            <div class="caption" style="padding-top: 6px !important; font-size: 18px;">
                                <i class="fa fa-gift"></i>Shares allocated (ESOP)
                            </div>
                            <div class="tools" style="padding-top: 6px !important;">
                                <a style="color: white"><i id="aEsopTab" class="fa fa-angle-up"></i></a>
                            </div>
                        </div>
                        <div id="EsopTab" class="portlet-body form collapse in">
                            <form class="form-horizontal" role="form">
                                <table class="table table-striped table-hover table-bordered">
                                    <thead>
                                        <tr>
                                            <th>No.</th>
                                            <th>DP/Client Id</th>
                                            <th>PAN</th>
                                            <th>Quantity</th>
                                            <th>Rate</th>
                                            <th>Amount</th>
                                            <th>Alloc. Date</th>
                                            <th>Actions</th>
                                        </tr>
                                    </thead>
                                    <tbody id="tbodyEsop"></tbody>
                                </table>
                            </form>
                        </div>
                    </div>

                     <div id="dvDeclarationActionable" class="portlet box My_Trade_PortletBox">
                        <div data-toggle="collapse" class="portlet-title" data-target="#dvActionableDeclarationTab">
                            <div class="caption" style="padding-top:6px !important;font-size:18px;">
                                <i class="fa fa-gift"></i>Disclosure Approval Actionable
                            </div>
                            <div class="tools" style="padding-top: 6px !important;">
                                <a style="color: white"><i id="advActionableDeclarationTab" class="fa fa-angle-up"></i></a>
                            </div>
                        </div>
                        <div id="dvActionableDeclarationTab" class="portlet-body form collapse ">
                            <form class="form-horizontal" role="form">
                                <table id="tbl-declaration-task" class="table table-striped table-hover table-bordered">
                                    <thead>
                                        <tr>
                                            <th>By</th>
                                            <th>Requested On</th>
                                            <th>Reason</th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                    <tbody id="tbodyDeclarationApproval"></tbody>
                                </table>
                            </form>
                        </div>
                    </div>
                     <div id="dvActionableBN" class="portlet box My_Trade_PortletBox">
                        <div data-toggle="collapse" class="portlet-title" data-target="#dvActionableBNTab">
                            <div class="caption" style="padding-top:6px !important;font-size:18px;">
                                <i class="fa fa-gift"></i>Trade Details Actionable - Annexure C
                            </div>
                            <div class="tools" style="padding-top: 6px !important;">
                                <a style="color: white"><i id="advActionableBNTab" class="fa fa-angle-up"></i></a>
                            </div>
                        </div>
                        <div id="dvActionableBNTab" class="portlet-body form collapse in">
                            <form class="form-horizontal" role="form">
                                <table class="table table-striped table-hover table-bordered">
                                    <thead>
                                        <tr>
                                            <%--<th>Name</th>--%>
                                            <th>For</th>
                                            <th>Quantity</th>
                                            <th>Executed</th>
                                            <th>Type</th>
                                            <th>Security</th>
                                            <th>Requested</th>
                                            <th>Valid From</th>
                                            <th>Valid To</th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                    <tbody id="tbodyBNApproval"></tbody>
                                </table>
                            </form>
                        </div>
                    </div>

                </div>
            </div>
            <div class="portlet box">
                <div class="portlet-body">
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 margin-bottom-10">
                            <table style="width: 100%; border: solid 1px black;">
                                <tr>
                                    <td style="width:10%;border:solid 1px black;text-align:center;" rowspan="3">Name</td>
                                    <td style="width:10%;border:solid 1px black;text-align:center;" rowspan="3">Relation</td>
                                    <td style="width:40%;border:solid 1px black;text-align:center;" colspan="1">
                                        Equity Shares Holding as on date <span id="spnBenposDate" runat="server"></span>
                                    </td>
                                    <td style="width:40%;border:solid 1px black;text-align:center;" colspan="4">
                                        Equity Bought/Sold in this Quarter <span id="spnQuarter"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:20%;border:solid 1px black;text-align:center;" rowspan="2">As per Company Records</td>
                                    <td style="width:20%;border:solid 1px black;text-align:center;display:none;" rowspan="2">
                                        As per Your Disclosure (With ESOP)
                                    </td>
                                    <td style="width:20%;border:solid 1px black;text-align:center;" colspan="2">As per Company Records</td>
                                    <td style="width:20%;border:solid 1px black;text-align:center;" colspan="2">As per Your Disclosure</td>
                                </tr>
                                <tr>
                                    <td style="border:solid 1px black;text-align:center;">Buy</td>
                                    <td style="border:solid 1px black;text-align:center;">Sell</td>
                                    <td style="border:solid 1px black;text-align:center;">Buy</td>
                                    <td style="border:solid 1px black;text-align:center;">Sell</td>
                                </tr>
                                <tbody id="tbdNum" runat="server"></tbody>
                            </table><br />
                            <div id="divTradingDetails" runat="server" style="display: inline; white-space: nowrap;">
                                <p>Pre-clearance required for trading : <span id="tradingLimit"></span></p>
                                <%--<p>Equity holding as per BENPOS/Declaration (As on - <span id="spLastUpdateCurrentHoldingDate"></span>) : <span id="spCurrentHolding"></span></p>
                                <p>Stocks Traded in this Quarter (From Benpos) : <span id="stocksTradedForLoggedInUserUsingBP"></span></p>
                                <p>Stocks Traded in this Quarter (From Broker Notes) : <span id="stocksTradedForLoggedInUserUsingBN"></span></p>--%>
                                <p>If Aggregate Trade value (Buy/Sell) is in excess of INR 1,000,000 in any calendar quarter: <span id="limitRemainingForNoPC"></span></p>
                                <p class="text-danger">Have you completed a Trade without taking Pre-clearance & Need to Upload Broker Note?&nbsp;<a data-toggle="modal" data-target="#stack4">Click Here</a></p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="Div1" class="col-md-12">
            <div class="portlet box">
                <div class="portlet-title">
                    <div class="caption">
                        <span style="color: black">Stock Price (This is a Tentative Price based on Closing Market Price of NSE/BSE).Please Check <a href="https://www.bseindia.com/stock-share-price/niit-ltd/niitltd/500304/" target="_blank">BSE</a>/<a href="https://www.nseindia.com/get-quotes/equity?symbol=NIITLTD" target="_blank">NSE</a> Websites for real time prices.</span>
                    </div>
                </div>
                <div class="portlet-body slide-left" id="TradeChart">
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 margin-bottom-10" style="padding: 0 10px;">
                            <!-- TradingView Widget BEGIN -->
                            <div class="tradingview-widget-container">
                                <div id="tradingview_e0efa"></div>

                                <script type="text/javascript" src="https://s3.tradingview.com/tv.js"></script>
                                <script type="text/javascript">
                                    setTimeout(function () {
                                        new TradingView.MediumWidget({
                                            "symbols": [
                                                [
                                                    "NIIT Ltd.",
                                                    "BSE:NIITLTD|1M"
                                                ]
                                            ],

                                            "chartOnly": false,
                                            "width": "100%",
                                            "height": 300,
                                            "locale": "in",
                                            "colorTheme": "light",
                                            "gridLineColor": "rgba(240, 243, 250, 0)",
                                            "fontColor": "#787B86",
                                            "isTransparent": false,
                                            "autosize": false,
                                            "showVolume": false,
                                            "scalePosition": "no",
                                            "scaleMode": "Normal",
                                            "fontFamily": "-apple-system, BlinkMacSystemFont, Trebuchet MS, Roboto, Ubuntu, sans-serif",
                                            "noTimeScale": false,
                                            "valuesTracking": "1",
                                            "chartType": "line",
                                            "container_id": "tradingview_e0efa"
                                        });

                                    }, 100);

                                </script>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- END Portlet PORTLET-->
        </div>
        <div class="col-md-12">
            <!-- BEGIN Portlet PORTLET-->
          
            <div class="portlet box" id="MyUPSIGroup_PortletBox" runat="server" visible="false">
                <div class="portlet-body">
                    <div class="portlet box My_Trade_PortletBox">
                        <div data-toggle="collapse" id="MyUPSIGroup_PortletTitle" class="portlet-title" data-target="#MyUPSIGroupTab">
                            <div class="caption" style="font-size: 18px;">
                                <i class="fa fa-user-secret"></i>UPSI Group
                            </div>
                            <div class="tools">
                                <a style="color: white"><i id="aMyUPSIGroupTab" class="fa fa-angle-up"></i></a>
                            </div>
                        </div>
                        <div id="MyUPSIGroupTab" class="portlet-body form collapse in">
                            <table class="table table-striped table-hover table-bordered">
                                <thead>
                                    <tr>
                                        <th>No.</th>
                                        <th>Name of the UPSI Project</th>
                                        <th>Validity</th>
                                        <%--<th>Created By</th>--%>
                                        <th>Created On</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RepeaterMyUPSIGroup" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Container.ItemIndex + 1 %></td>
                                                <td><%#Eval("GRP_NAME") %></td>
                                                <td><%#Eval("VALID_FROM", "{0:dd-MMM-yyyy}") %>
                                                    <%#(String.IsNullOrEmpty(Eval("VALID_TO").ToString()) ? "" : "- "+ Eval("VALID_TO", "{0:dd-MMM-yyyy}"))%>
                                                </td>
                                                <%--<td><%#Eval("USER_NM") %></td>--%>
                                                <td><%#Eval("CREATED_ON", "{0:dd-MMM-yyyy}") %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="allPreClearanceRequestActionable" class="col-md-12" style="display: none;">
            <!-- BEGIN Portlet PORTLET-->
            <div class="portlet box">
                <div class="portlet-title">
                    <div class="caption">
                        <span style="color: black">All Pre Clearance Status (FY <span id="fy1"></span>)</span>
                    </div>
                    <div class="tools">
                        <a href="Pre_Clearance_Request.aspx" style="height: 28px;" class="btn btn-sm green">
                            <i class="fa fa-user"></i>Trade Request
                            <i class="fa fa-angle-down"></i>
                        </a>
                    </div>
                </div>
                <div class="portlet-body">
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12 margin-bottom-10">
                            <div class="dashboard-stat blue">
                                <div class="visual">
                                    <i class="fa fa-briefcase fa-icon-medium"></i>
                                </div>
                                <div class="details">
                                    <div class="number" id="allPreClearanceRequestForAlluser"></div>
                                    <div class="desc">All Requested</div>
                                </div>
                                <a class="more" href="TradingRequestDetailsAdmin.aspx?Status=All">View more<i class="m-icon-swapright m-icon-white"></i>
                                </a>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <div class="dashboard-stat purple">
                                <div class="visual"></div>
                                <div class="details">
                                    <div class="number" id="inApprovalPreClearanceRequestForAlluser"></div>
                                    <div class="desc">Pending </div>
                                </div>
                                <a class="more" href="PreClearanceTask.aspx">View more<i class="m-icon-swapright m-icon-white"></i>
                                </a>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <div class="dashboard-stat green-dark">
                                <div class="visual">
                                    <i class="fa fa-group fa-icon-medium"></i>
                                </div>
                                <div class="details">
                                    <div class="number" id="approvedPreClearanceRequestForAlluser"></div>
                                    <div class="desc">Approved </div>
                                </div>
                                <a class="more" href="TradingRequestDetailsAdmin.aspx?Status=Approved">View more<i class="m-icon-swapright m-icon-white"></i>
                                </a>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <div class="dashboard-stat red">
                                <div class="visual">
                                    <i class="fa fa-group fa-icon-medium"></i>
                                </div>
                                <div class="details">
                                    <div class="number" id="rejectedPreClearanceRequestForAlluser"></div>
                                    <div class="desc">Rejected </div>
                                </div>
                                <a class="more" href="TradingRequestDetailsAdmin.aspx?Status=Rejected">View more<i class="m-icon-swapright m-icon-white"></i>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- END Portlet PORTLET-->
        </div>
        <div class="col-md-12">
            <!-- BEGIN Portlet PORTLET-->
            <div class="portlet box">
                <div class="portlet-title">
                    <div class="caption">
                        <span style="color: black">My Pre Clearance Status (FY <span id="fy2"></span>)</span>
                    </div>
                    <div class="tools">
                        <a href="Pre_Clearance_Request.aspx" style="height: 28px;" class="btn btn-sm green">
                            <i class="fa fa-user"></i>Trade Request
                            <i class="fa fa-angle-down"></i>
                        </a>
                    </div>
                </div>
                <div class="portlet-body">
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12 margin-bottom-10">
                            <div class="dashboard-stat blue">
                                <div class="visual">
                                    <i class="fa fa-briefcase fa-icon-medium"></i>
                                </div>
                                <div class="details">
                                    <div class="number" id="allPreClearanceRequest"></div>
                                    <div class="desc">All Requested</div>
                                </div>
                                <a class="more" href="Pre_Clearance_Request.aspx">View more<i class="m-icon-swapright m-icon-white"></i>
                                </a>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <div class="dashboard-stat purple">
                                <div class="visual"></div>
                                <div class="details">
                                    <div class="number" id="inApprovalPreClearanceRequest"></div>
                                    <div class="desc">Pending </div>
                                </div>
                                <a class="more" href="Pre_Clearance_Request.aspx?Status=InApproval">View more<i class="m-icon-swapright m-icon-white"></i>
                                </a>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <div class="dashboard-stat green-dark">
                                <div class="visual">
                                    <i class="fa fa-group fa-icon-medium"></i>
                                </div>
                                <div class="details">
                                    <div class="number" id="approvedPreClearanceRequest"></div>
                                    <div class="desc">Approved </div>
                                </div>
                                <a class="more" href="Pre_Clearance_Request.aspx?Status=Approved">View more<i class="m-icon-swapright m-icon-white"></i>
                                </a>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <div class="dashboard-stat red">
                                <div class="visual">
                                    <i class="fa fa-group fa-icon-medium"></i>
                                </div>
                                <div class="details">
                                    <div class="number" id="rejectedPreClearanceRequest"></div>
                                    <div class="desc">Rejected </div>
                                </div>
                                <a class="more" href="Pre_Clearance_Request.aspx?Status=Rejected">View more<i class="m-icon-swapright m-icon-white"></i>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- END Portlet PORTLET-->
        </div>
        <div id="allTradeDetails" class="col-md-12" runat="server" visible="false">
            <!-- BEGIN Portlet PORTLET-->
            <div class="portlet box">
                <div class="portlet-title">
                    <div class="caption">
                        <span style="color: black">All Trade Details (FY <span id="fy3"></span>)</span>
                    </div>
                    <div class="tools"></div>
                </div>
                <div class="portlet-body">
                    <div class="row">
                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12 margin-bottom-10">
                            <div class="dashboard-stat blue">
                                <div class="visual">
                                    <i class="fa fa-briefcase fa-icon-medium"></i>
                                </div>
                                <div class="details">
                                    <div id="allSubmittedWithClearance" class="number"></div>
                                    <div class="desc">Submitted With Clearance </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                            <div class="dashboard-stat purple">
                                <div class="visual"></div>
                                <div class="details">
                                    <div id="allSubmittedWithoutClearance" class="number"></div>
                                    <div class="desc">Submitted Without Clearance </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                            <div class="dashboard-stat green-dark" style="background-color: #e7505a !important">
                                <div class="visual">
                                    <i class="fa fa-group fa-icon-medium"></i>
                                </div>
                                <div class="details">
                                    <div id="allNotDeclared" class="number"></div>
                                    <div class="desc">Not Declared </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- END Portlet PORTLET-->
        </div>
        <div class="col-md-12">
            <!-- BEGIN Portlet PORTLET-->
            <div class="portlet box">
                <div class="portlet-title">
                    <div class="caption">
                        <span style="color: black">My Trade Details (FY <span id="fy4"></span>)</span>
                    </div>
                    <div class="tools"></div>
                </div>
                <div class="portlet-body">
                    <div class="row">
                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12 margin-bottom-10">
                            <div class="dashboard-stat blue">
                                <div class="visual">
                                    <i class="fa fa-briefcase fa-icon-medium"></i>
                                </div>
                                <div class="details">
                                    <div id="mySubmittedWithClearance" class="number"></div>
                                    <div class="desc">Submitted With Clearance </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                            <div class="dashboard-stat purple">
                                <div class="visual"></div>
                                <div class="details">
                                    <div id="mySubmittedWithoutClearance" class="number"></div>
                                    <div class="desc">Submitted Without Clearance </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                            <div class="dashboard-stat green-dark" style="background-color: #e7505a !important">
                                <div class="visual">
                                    <i class="fa fa-group fa-icon-medium"></i>
                                </div>
                                <div class="details">
                                    <div id="myNotDeclared" class="number"></div>
                                    <div class="desc">Not Declared </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- END Portlet PORTLET-->
        </div>
    </div>
    <div class="modal fade" id="stack4" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnClearBrokerNoteRequestDetails();"></button>
                    <h4 class="modal-title">Trade Details</h4>
                </div>
                <div class="modal-body">
                    <form class="form-horizontal" role="form">
                        <div class="form-body">
                            <div class="form-group">
                                <label id="lblForBN" class="col-md-3 control-label">
                                    For<span class="required">* </span>
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <select id="ddlForBN" class="form-control"></select>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">
                                    Type Of Security
                                    <span class="required">* </span>
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <select id="ddlTypeOfSecurity" class="form-control"></select>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">
                                    Company
                                    <span class="required">* </span>
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <select id="ddlRestrictedCompanies" class="form-control"></select>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">
                                    Transaction Type
                                    <span class="required">* </span>
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <select id="ddlTypeOfTransaction" class="form-control"></select>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">
                                    Actual Trade Quantity
                                    <span class="required">* </span>
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="fa fa-bell-o"></i>
                                            </span>
                                            <input id="txtTradeQuantity" type="number" class="form-control" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">
                                    Value Per Share
                                    <span class="required">* </span>
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="fa fa-bell-o"></i>
                                            </span>
                                            <input id="txtValuePerShare" type="number" class="form-control" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">
                                    Total Amount
                                    <span class="required">* </span>
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="fa fa-bell-o"></i>
                                            </span>
                                            <input id="txtTotalamount" type="number" class="form-control" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">
                                    Demat Account<span class="required">* </span>
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <select id="ddlDematAccountBrokerNote" class="form-control"></select>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">
                                    Actual Transaction Date<span class="required">* </span>
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="fa fa-bell-o"></i>
                                            </span>
                                            <input id="txtRequestedTransactionDateBN" class="form-control bg-white" type="text" value="" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Trade Details</label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="fa fa-upload"></i>
                                            </span>
                                            <input id="btnBrokernote" type="file" class="form-control" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <%--    <div class="form-group" style="display:none;">
                                <label class="col-md-3 control-label">Current Market Price</label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="fa fa-bell-o"></i>
                                            </span>
                                            <input id="txtShareCurrentMarketPrice" type="number" class="form-control" placeholder="Current Market Price" />
                                        </div>
                                    </div>
                                </div>
                            </div>--%>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Transaction Through</label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <select id="ddlProposedTransactionThrough" class="form-control">
                                            <option value="">--Select--</option>
                                            <option value="Stock Exchange">Stock Exchange</option>
                                            <option value="Off-Market Deal">Off-Market Deal</option>
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group" id="dvBrokerDetails" style="display:none">
                                <label class="col-md-3 control-label">
                                    Details of Off Market Deal<span class="required">* </span>
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <textarea id="txtareabrokerdetails" class="form-control" rows="2"></textarea>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group" id="dvExchangeTradedOn">
                                <label class="col-md-3 control-label">Exchange On Which Trade Executed</label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <select id="ddlExchangeTradedOn" class="form-control">
                                            <option value="">--Select--</option>
                                            <option value="BSE">BSE</option>
                                            <option value="NSE">NSE</option>
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">
                                    Remarks
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="fa fa-bell-o"></i>
                                            </span>
                                            <textarea id="txtRemarks" style="margin-top: 0px; margin-bottom: -4px; height: 42px; width: 100%;"></textarea>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
                <div class="modal-footer">
                    <button id="btnSubmitBrokerNote" type="submit" class="btn btn-outline dark" onclick="javascript:AddUpdateBrokerNote();">Submit</button>
                    <button id="btnCancelBrokerNote" type="button" class="btn default" data-dismiss="modal" onclick="javascript:fnClearBrokerNoteRequestDetails();">Cancel</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <div class="modal fade" id="NcUploadBrokerNote" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnClearNcBrokerNoteRequestDetails();"></button>
                    <h4 class="modal-title">Trade Details</h4>
                </div>
                <div class="modal-body">
                    <form class="form-horizontal" role="form">
                        <div class="form-body">
                            <div class="form-group">
                                <label id="lblForNcBN" class="col-md-3 text-right">
                                    For<span class="required">* </span>
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <span id="spnforNcBN"></span>
                                        <input type="hidden" id="txthiddenRelativeId" />
                                        <input type="hidden" id="txtTransId" />
                                        <input type="hidden" id="txtNonComplianceId" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">
                                    Type Of Security
                                    <span class="required">* </span>
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <select id="ddlTypeOfSecurityNc" class="form-control"></select>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">
                                    Company
                                    <span class="required">* </span>
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <select id="ddlRestrictedCompaniesNc" class="form-control"></select>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 text-right">
                                    Transaction Type
                                    <span class="required">* </span>
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <select id="ddlTypeOfTransactionNc" class="form-control" disabled="disabled"></select>

                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 text-right">
                                    Trade Quantity
                                    <span class="required">* </span>
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <span id="spnTradeQuantity"></span>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">
                                    Value Per Share
                                    <span class="required">* </span>
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="fa fa-bell-o"></i>
                                            </span>
                                            <input id="txtValuePerShareNc" type="number" class="form-control" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">
                                    Total Amount
                                    <span class="required">* </span>
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="fa fa-bell-o"></i>
                                            </span>
                                            <input id="txtTotalamountNc" type="number" class="form-control" readonly="readOnly" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 text-right">
                                    Demat Account<span class="required">* </span>
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <span id="spnDematAccountBrokerNote"></span>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">
                                    Actual Transaction Date<span class="required">* </span>
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="fa fa-bell-o"></i>
                                            </span>
                                            <input id="txtRequestedTransactionDateCnBN" class="form-control bg-white" type="text" value="" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Trade Details</label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="fa fa-upload"></i>
                                            </span>
                                            <input id="btnBrokernoteNc" type="file" class="form-control" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Transaction Through</label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <select id="ddlProposedTransactionThroughNc" class="form-control">
                                            <option value="">--Select--</option>
                                            <option value="Stock Exchange">Stock Exchange</option>
                                            <option value="Off-Market Deal">Off-Market Deal</option>
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group" id="dvBrokerDetailsNC" style="display:none">
                                <label class="col-md-3 control-label">
                                    Details of Off Market Deal<span class="required">* </span>
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <textarea id="txtareabrokerdetailsNC" class="form-control" rows="2"></textarea>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group" id="dvExchangeTradedOnNC">
                                <label class="col-md-3 control-label">Exchange On Which Trade Executed</label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <select id="ddlExchangeTradedOnNc" class="form-control">
                                            <option value="">--Select--</option>
                                            <option value="BSE">BSE</option>
                                            <option value="NSE">NSE</option>
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">
                                    Remarks
                                </label>
                                <div class="col-md-9">
                                    <div class="input-inline input-medium">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="fa fa-bell-o"></i>
                                            </span>
                                            <textarea id="txtRemarksNc" style="margin-top: 0px; margin-bottom: -4px; height: 42px; width: 100%;"></textarea>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
                <div class="modal-footer">
                    <button id="btnSubmitNcBrokerNote" type="submit" class="btn btn-outline dark" onclick="javascript:AddUpdateNonComplianceBrokerNote();">Submit</button>
                    <button id="btnCancelNcBrokerNote" type="button" class="btn default" data-dismiss="modal" onclick="javascript:fnClearNcBrokerNoteRequestDetails();">Cancel</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="NonComplianceComments" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnClearNcRemarks();"></button>
                    <h4 class="modal-title">Non Compliance Remarks</h4>
                </div>
                <div class="modal-body">
                    <div class="table-responsive">
                        <table class="table table-bordered">
                            <thead>
                                <tr>
                                    <th>User Name</th>
                                    <th>Relative</th>
                                    <th>Relation</th>
                                    <th>Pan</th>
                                    <th>Folio No</th>
                                    <th>Non Compliant Reason</th>
                                    <th>Qty</th>
                                    <th>Trade Value</th>
                                </tr>
                            </thead>
                            <tbody id="tbdNonComplianceRemarks"></tbody>
                        </table>
                    </div>

                    <div class="form-body">
                        <div class="form-group">
                            <label id="lblForNcComments">
                                Remarks<span class="required">* </span>
                            </label>
                            <textarea id="textareaNcRemarks" class="form-control" rows="4"></textarea>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button id="btnSubmitNcRemarks" type="button" class="btn btn-outline dark" onclick="javascript:fnAddNcRemarks();">Submit</button>
                    <button id="btnCancelNcRemarks" type="button" class="btn default" data-dismiss="modal" onclick="javascript:fnClearNcRemarks();">Cancel</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="modalForms" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title">Please Select the Forms for Submission to CO</h4>
                    <input type="hidden" id="preClearanceRequestIdBN" value="0" />
                    <input type="hidden" id="txtBrokerNoteId" value="0" />
                </div>
                <div class="modal-body">
                    <div class="portlet-body form">
                        <div id="divItemTitle" class="row">
                            <div class="col-md-4" id="lblForms">Select Form</div>
                            <div class="col-md-8">
                                <select runat="server" style="display: none;" name="category" id="ddlCategory" class="form-control select2" data-placeholder="Select a Category">
                                </select>
                                <select id="ddlForms" class="form-control" onchange="javascript:fnDisplayNote(this, 'Forms');">
                                    <option selected="selected" value="0">Please Select Form</option>
                                    <option value="FORM_CJ">Form C</option>
                                    <option value="FORM_DJ">Form C</option>
                                    <option value="FORM_J">Form J</option>
                                </select>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <label id="lblNote" class="control-label" style="display: none; color: black; text-align: left;"></label>
                            </div>
                        </div>
                        <br />
                        <div id="divOverwriteForm" class="row">
                            <div class="col-md-12" id="lblOverwriteForm">
                                <input type="checkbox" id="chkOverwrite" onchange="javascript:fnShowUploadDiv(this);" />
                                <label id="lblOverwrite" class="control-label" style="color: darkorange; text-align: left;">Click here to Submit your own Forms</label>
                            </div>
                        </div>
                        <br />
                        <div id="divUploadForm" class="row" style="display: none;">
                            <div class="col-md-4" id="lblUploadForm">Upload Form</div>
                            <div class="col-md-8">
                                <input type="file" id="txtUploadForm" onchange="javascript:fnRemoveClass(this,'UploadForm');" class="form-control" data-tabindex="4" />
                            </div>
                        </div>
                        <br />
                        <div id="divUploadLbl" class="row" style="display: none;">
                            <div class="col-md-12">
                                <label id="lbl" class="control-label" style="color: darkorange; text-align: left;">Please Download & Review the Selected Forms before submission.</label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button id="btnDownloadForm" type="submit" class="btn green" onclick="javascript:fnDownloadForm();">Download & Review Forms</button>
                    <button id="btnSubmitForm" type="button" disabled="disabled" class="btn green" onclick="javascript:fnValidateForms();">Submit Forms</button>
                    <button id="btnOpenForm" type="button" style="display: none;" data-target="#modalForms" data-toggle="modal"></button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade in" id="modalSubmitConfirmation" style="z-index: 10000000" tabindex="-1" role="dialog" aria-hidden="True">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="True"></button>
                    <h4 class="modal-title"><b>This action is irrevokable. Please ensure that the correct Forms are uploaded for sharing. Are you sure, you want to submit the Forms to Compliance Officer?</b></h4>
                </div>
                <div class="modal-footer">
                    <button id="btnSubmitNo" type="button" class="btn dark btn-outline" onclick="javascript:fnHideShow('modalSubmitConfirmation');">NO</button>
                    <button id="btnSubmitYes" class="btn red" data-dismiss="modal" value="YES" onclick="javascript:fnSubmitForms()">YES</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade in" id="modalBrokerNoteUploadConfirmation" tabindex="-1" role="dialog" aria-hidden="True" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog" style="width: 50%">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="True"></button>
                    <h4 class="modal-title"><b>Are you sure you want to upload Trade Details?</b></h4>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn dark btn-outline" data-toggle="modal" data-target="#modalBrokerNoteUploadConfirmation">NO</button>
                    <input value="YES" id="btnUploadBrokerNoteConfirmation" data-dismiss="modal" class="btn red" onclick="javascript: fnSubmitBrokerNoteRequestDetails();" type="submit" />
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade in" id="modalNonComplianceBrokerNoteUploadConfirmation" tabindex="-1" role="dialog" aria-hidden="True" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog" style="width: 50%">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="True"></button>
                    <h4 class="modal-title"><b>Are you sure you want to upload Trade Details?</b></h4>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn dark btn-outline" data-toggle="modal" data-target="#modalNonComplianceBrokerNoteUploadConfirmation">NO</button>
                    <input value="YES" id="btnUploadNonComplianceBrokerNoteConfirmation" data-dismiss="modal" class="btn red" onclick="javascript: SubmitNcBrokerNoteRequestDetails();" type="submit" />
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="modalUPSIPolicyApplicable" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog" style="width: 95%!important">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title">UPSI Policy Applicable</h4>
                </div>
                <div class="modal-body">
                    <p id="modalContentUPSIPolicy">
                        <iframe style="height: 379px; width: 100%" src="CodeOfConduct.html"></iframe>
                    </p>
                    <label class="radio-inline">
                        <input type="radio" name="optradio" checked="checked" value="Non-UPSI Member" />Non-UPSI Member
                    </label>
                    <label class="radio-inline">
                        <input type="radio" name="optradio" value="UPSI Member" />UPSI Member
                    </label>
                </div>
                <div class="modal-footer">
                    <button id="btnSubmitModalUPSIPolicyApplicable" type="submit" class="btn btn-outline dark" onclick="javascript:fnSubmitUPSIPolicyApplicable();">Submit</button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="transactionHistoryModel" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-dialog-centered" style="width: 70%!important">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="fnClearValidateTradeBifurcation();"></button>
                    <h4 class="modal-title caption-subject bold uppercase font-red-sunglo"><span id="spnTitleTradeBifurcation">Please provide Trade Bifurcation for <span id="spShareNumber"></span>Shares as per details below.<br />
                        <span style="color: red">All fields are mandatory.</span></span></h4>
                </div>
                <div class="modal-body">
                    <div class="portlet light bordered">
                        <div class="portlet-body form">
                            <form class="form-horizontal" role="form">
                                <div class="form-body modal-fixheight">
                                    <div class="row" id="divTradeQuantityBifurcation">
                                    </div>
                                </div>
                                <div class="form-actions">
                                    <div class="row">
                                        <div class="col-md-offset-8 col-md-4">
                                            <button id="btnSaveTradeBifurcation" type="button" class="btn green" onclick="javascript:fnSaveTradeBifurcation();">Submit Trade Details</button>
                                            <button id="btnCancelTradeBifurcation" type="button" data-dismiss="modal" class="btn default" onclick="fnClearValidateTradeBifurcation();">Cancel</button>
                                        </div>
                                    </div>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="stackUPSIMessage" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-dialog-centered" style="width: 90% !important;">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title caption-subject bold uppercase font-red-sunglo">UPSI GROUP MESSAGE</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnCloseModalMsg();"></button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-2" style="text-align: left;">From</div>
                        <div class="col-md-10">
                            <span id="dvUPSITaskMSGFrom"></span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2" style="text-align: left;">To</div>
                        <div class="col-md-10">
                            <span id="dvUPSITaskMSGTo"></span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2" style="text-align: left;">CC</div>
                        <div class="col-md-10">
                            <span id="dvUPSITaskMSGCC"></span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2" style="text-align: left;">Date</div>
                        <div class="col-md-10">
                            <span id="dvUPSITaskMsgDate"></span>
                        </div>
                    </div>
                    <div class="row" id="dvSubjectHdr">
                        <div class="col-md-2" style="text-align: left;">Subject</div>
                        <div class="col-md-10">
                            <span id="dvUPSITaskMsgSubject"></span>
                        </div>
                    </div>
                    <div class="row" id="dvMessageHdr">
                        <div class="col-md-2" style="text-align: left;">Email Message</div>
                        <div class="col-md-10">
                            <span id="dvUPSITaskMSGBody"></span>
                        </div>
                    </div>
                    <div class="row" id="dvAttachmentHdr">
                        <div class="col-md-2" style="text-align: left;">Attachment</div>
                        <div class="col-md-10">
                            <span id="dvAttechmentlistMsg"></span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="stack1" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-dialog-centered modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title caption-subject bold uppercase font-red-sunglo">UPSI GROUP TASK</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnCloseModal();"></button>
                </div>
                <div class="modal-body">
                    <div class="portlet light bordered">
                        <div class="portlet-body form">
                            <div class="form-body">
                                <div class="form-group">
                                    <label id="lblupsiNonDesignatedM" style="text-align: left" class="control-label">
                                        <span id="spnUPSIGrp"></span>
                                    </label>
                                    <input type="hidden" id="hdnTaskId" />
                                </div>
                                <div class="form-group">
                                    <label>
                                        UPSI Group<span class="required">*</span>
                                    </label>
                                    <select id="ddlCPGroup" class="form-control" style="width: 320px">
                                        <option value="0">Select</option>
                                    </select>
                                </div>
                                <div class="form-group">
                                    <input type="hidden" id="txtCPGrpId" value="0" />
                                    <table class="table" id="tblConnectedPerson">
                                        <thead>
                                            <tr>
                                                <th>Email<span class="required">*</span></th>
                                                <th>Name<span class="required">*</span></th>
                                                <th>Identification<span class="required">*</span></th>
                                                <th>Identification #<span class="required">*</span></th>
                                                <th>Firm<span class="required">*</span></th>
                                            </tr>
                                        </thead>
                                        <tbody id="tbdCPAdd"></tbody>
                                    </table>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
                <div class="modal-footer">
                    <button id="btnTaskCP" type="button" class="btn green" onclick="javascript:fnAddUPSITaskCP();">Add CP</button>
                    <button id="btnTaskDP" type="button" class="btn green" onclick="javascript:fnAddUPSITaskDP();">Add DP</button>
                    <button id="btnCloseupsiTask" type="button" class="btn green" onclick="javascript:fnCloseUPSITask();">Save</button>
                    <button id="btnCancel1" type="button" data-dismiss="modal" class="btn default" onclick="javascript:fnCloseModal();">Cancel</button>
                    <button id="btnDiscard" type="button" data-dismiss="modal" class="btn default" onclick="javascript:fnDiscardTask();">Not an UPSI</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="modalEsopForms" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <%--<button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>--%>
                    <h4 class="modal-title">Please Download/Review the Forms for Submission to CO</h4>
                    <input type="hidden" id="txtEsopAllocId" value="0" />
                </div>
                <div class="modal-body">
                    <div class="portlet-body form">
                        <div class="row">
                            <div class="col-md-4" id="lblEsopFormC">Select Form</div>
                            <div class="col-md-8">
                                <select id="ddlEsopForms" class="form-control">
                                </select>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button id="btnDownloadEsopForms" type="submit" class="btn green" onclick="javascript:fnDownloadEsopForm();">Download & Review Forms</button>
                    <button id="btnSubmitEsopForms" type="button" disabled="disabled" class="btn green" onclick="javascript:fnSubmitSystemGeneratedEsopForm();">Submit Forms</button>
                    <button type="button" class="btn red" data-dismiss="modal" aria-hidden="true">Close</button>
                    
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="stack1UPSITaskClosed" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-dialog-centered" style="width: 40% !important">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title caption-subject bold uppercase font-red-sunglo">UPDATE UPSI TASK </h4>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnCloseModal();"></button>
                </div>
                <div class="modal-body">
                    <div class="portlet light bordered">
                        <div class="portlet-body form">
                            <form class="form-horizontal" role="form">
                                <div class="form-body modal-fixheight">

                                    <div class="form-group">
                                        <label id="lblUpsigroup" style="text-align: left" class="col-md-4 control-label">UPSI Group <span class="required">* </span></label>
                                        <div class="col-md-8">
                                            <select id="textUpsigroup" class="form-control" onchange="fnUPSIGroupChange()"></select>
                                            <input type="hidden" id="txtUPSIgroupid" value="" />
                                        </div>
                                    </div>
                                    <br />




                                </div>
                            </form>
                        </div>
                        <div class="form-actions">
                            <div class="row" style="text-align: center">
                                <div class="col-md-offset-4 col-md-12">
                                    <button id="btnCloseupsiTask11" type="button" class="btn green" onclick="javascript:fnUpdateUPSITask();">Submit</button>

                                    <button id="btnCancel11" type="button" data-dismiss="modal" class="btn default" onclick="javascript:fnCloseModal();">Cancel</button>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="TransComplianceModal" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnClearNcRemarks();"></button>
                    <h4 class="modal-title">Mark Transaction</h4>
                </div>
                <div class="modal-body">
                    <div class="table-responsive">
                        <table class="table table-bordered">
                            <thead>
                                <tr>
                                    <th>Name</th>
                                    <th>Relation</th>
                                    <th>Pan</th>
                                    <th>Folio No</th>
                                    <th>Qty</th>
                                    <th>Trade Value</th>
                                </tr>
                            </thead>
                            <tbody id="tbdTransComplianceRemarks"></tbody>
                        </table>
                    </div>

                    <div class="form-body">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label id="lblTransStatus">
                                    Status<span class="required">* </span>
                                </label>
                                <br />
                                <select class="form-control" id="ddlTransStatus">
                                    <option value="">Select Status</option>
                                    <option value="Compliant">Compliant</option>
                                    <option value="Non-Compliant">Non-Compliant</option>
                                </select>
                            </div>
                        </div>
                        <div class="col-md-12">

                            <div class="form-group">
                                <label id="lblForTransComments">
                                    Remarks<span class="required">* </span>
                                </label>
                                <textarea id="textareaTransRemarks" class="form-control" rows="4"></textarea>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button id="btnSubmitTransRemarks" type="button" class="btn btn-outline dark" onclick="javascript:fnAddTransStatus();">Submit</button>
                    <button id="btnCancelTransRemarks" type="button" class="btn default" data-dismiss="modal" onclick="javascript:fnClearTransStatus();">Cancel</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="AddNewConnectedPerson" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-dialog-centered" style="width: 90% !important">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title caption-subject bold uppercase font-red-sunglo">Insider Person(s)</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnCloseModal();"></button>
                </div>
                <div class="modal-body">
                    <div class="form-group text-center">
                        <label id="lblUPSIOpenTaskCP" style="text-align: left">
                            <span id="spnNewUPSIGrp"></span>
                        </label>
                    </div>
                    <div class="portlet light bordered">
                        <div class="portlet-body">
                            <div class="form-group">
                                <table class="table" id="tblNewConnectedPerson">
                                    <thead>
                                        <tr>
                                            <th>Firm<span class="required">*</span></th>
                                            <th>Name<span class="required">*</span></th>
                                            <th>Email<span class="required">*</span></th>
                                            <th>Identification<span class="required">*</span></th>
                                            <th>Identification #<span class="required">*</span></th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                    <tbody id="tbdNewCPAdd">
                                        <tr>
                                            <td style="margin:5px;">
                                                <input id="txtNewFirmNm" class="form-control form-control-inline" placeholder="Enter Firm Name" type="text" autocomplete="off" onchange="removeCPRedClass('txtFirmNm', 'lblFirmNm')" />
                                            </td>
                                            <td style="margin:5px;">
                                                <input id="txtNewCPNm" class="form-control form-control-inline" placeholder="Enter Connected Person" type="text" autocomplete="off" onchange="removeCPRedClass('txtCPNm', 'lblUPSIGrpNm')" />
                                            </td>
                                            <td style="margin:5px;">
                                                <input id="txtNewCPEmail" class="form-control form-control-inline" placeholder="Enter Connected Person" type="text" autocomplete="off" onchange="removeCPRedClass('txtCPEmail', 'lblCPEmail')" />
                                            </td>
                                            <td style="margin:5px;">
                                                <select id="ddlNewCPIdentification" class="form-control" onchange="removeRedClass('ddlCPIdentification','lblCPIdentification')">
                                                    <option value=""></option>
                                                    <option value="AADHAR CARD">AADHAR CARD</option>
                                                    <option value="DRIVING LICENSE">DRIVING LICENSE</option>
                                                    <option value="PAN">PAN</option>
                                                    <option value="PASSPORT">PASSPORT</option>
                                                    <option value="OTHER">OTHER</option>
                                                </select>
                                            </td>
                                            <td style="margin:5px;">
                                                <input id="txtNewCPIdentificationNo" class="form-control form-control-inline" placeholder="Enter Connected Person" type="text" autocomplete="off" onchange="removeCPRedClass('txtCPIdentificationNo', 'lblCPIdentificationNo')" />
                                            </td>
                                            <td style="margin:5px;">
                                                <img onclick="javascript:fnAddNewCP();" src="images/icons/AddButton.png" height="24" width="24" />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button id="btnSaveNewCP" type="button" class="btn green" onclick="javascript:fnSaveNewConnectedPerson();">Save Connected New Person(s)</button>
                    <button id="btnCancelNewCP" type="button" data-dismiss="modal" class="btn default" onclick="javascript:fnCloseNewCPModal();">Cancel</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="AddNewDP" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title caption-subject bold uppercase font-red-sunglo">Add DPs</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnCloseModal();"></button>
                </div>
                <div class="modal-body">
                    <div class="portlet light bordered">
                        <div class="portlet-body form">
                            <div class="form-body">
                                <input id="HiddenUpsiGrpId" type="hidden" />
                                <div class="form-group">
                                    <label>User</label>
                                    <select id="dduserslist" data-placeholder="Select D/Ps" class="form-control select2" multiple=""></select>
                                </div>
                                <button id="btnSaveMember" type="button" onclick="javascript:fnSaveMember();" class="btn green">Save Members</button>
                                <table class="table table-bordered table-hover margin-top-15" id="GrpMembersList">
                                    <thead>
                                        <tr>
                                            <th>User</th>
                                            <th>Membership</th>
                                            <th>Remove</th>
                                        </tr>
                                    </thead>
                                    <tbody id="tbodyGrpMembersList"></tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button id="btnCancelMember" type="button" data-dismiss="modal" onclick="javascript:fnCloseModal();" class="btn btn-danger">Cancel</button>
                </div>
            </div>
        </div>
    </div>
    <script src="../assets/global/plugins/moment.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js"></script>
    <script src="../assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.js"></script>
    <script src="../assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
    <script src="../assets/global/plugins/bootstrap-timepicker/js/bootstrap-timepicker.min.js"></script>
    <script src="../assets/pages/scripts/components-date-time-pickers.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js" type="text/javascript"></script>
    <script src="../assets/pages/scripts/components-date-time-pickers.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/select2/js/select2.full.min.js" type="text/javascript"></script>
    <script src="../assets/pages/scripts/components-select2.min.js" type="text/javascript"></script>
        <script src="../assets/pages/scripts/components-select2.js" type="text/javascript"></script>
    <script src="js/Global.js?<%=DateTime.Now %>" type="text/javascript"></script>
    <script src="js/Dashboard.js?<%=DateTime.Now %>" type="text/javascript"></script>
    <script src="js/Dashboard_Actionable.js?<%=DateTime.Now %>" type="text/javascript"></script>
    <script src="js/Dashboard_UPSITask.js?<%=DateTime.Now %>" type="text/javascript"></script>
    <script src="js/Dashboard_Esop.js?<%=DateTime.Now %>" type="text/javascript"></script>
</asp:Content>