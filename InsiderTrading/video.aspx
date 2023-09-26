<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" CodeBehind="video.aspx.cs" Inherits="ProcsDLL.InsiderTrading.video" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .requied {
            color: red;
        }
        .multiselect-container li {
            margin-left: 10px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-content-inner">
        <div class="col-md-12" style="overflow-y: auto; overflow-x: auto; padding-left: 0px; padding-right: 0px;">
            <div class="portlet light portlet-fit " style="min-height: 525px;">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="icon-settings font-red"></i>
                        <span class="caption-subject font-red sbold uppercase">VIDEO</span>
                    </div>
                </div>
                <div class="portlet-body">
                    <%--<form class="form-horizontal" role="form">--%>
                        <div class="form-body">
                            <div class="form-group">
                                <label class="col-md-offset-1 col-md-11">
                                    <a href="javascript:fnGoToTrainingModule('1','Training Video for SEBI Prohibition of Insider Trading (PIT) Compliances');">
                                        Training Video for SEBI Prohibition of Insider Trading (PIT) Compliances
                                    </a>
                                </label>
                            </div>
                            <div class="form-group">
                                <label class="col-md-offset-1 col-md-11">
                                    <a href="javascript:fnGoToTrainingModule('2','Instructional Video - Submission of The Annual Disclosures by The Designated Persons');">
                                        Instructional Video - Submission of The Annual Disclosures by The Designated Persons
                                    </a>
                                </label>
                            </div>
                            <div class="form-group">
                                <label class="col-md-offset-1 col-md-11">
                                    <a href="javascript:fnGoToTrainingModule('3','Instructional Video - Raising a Pre-Clearance Approval Request and Uploading Broker Notes post Trade');">
                                        Instructional Video - Raising a Pre-Clearance Approval Request and Uploading Broker Notes post Trade
                                    </a>
                                </label>
                            </div>
                        </div>
                    <%--</form>--%>
                </div>
            </div>
        </div>
    </div>
    <script src="js/Global.js?<%=DateTime.Now %>" type="text/javascript"></script>
    <script src="js/video.js?<%=DateTime.Now %>" type="text/javascript"></script>
</asp:Content>