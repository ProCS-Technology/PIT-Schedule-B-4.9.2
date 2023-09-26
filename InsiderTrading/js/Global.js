if (localStorage.getItem("masterTxtWhetherWindowsAuthentication") == 'true') {
    $("#liRecoverPassword").hide();
    $("#liRecoverDivider").hide();
    $("#liChangePassword").hide();
    $("#liChangeDivider").hide();
    $("#liCodeOfConductDivider").hide();
    $("#liLogOut").hide();
    var uri = "";
    if (window.location.port == "" && window.location.host != "localhost") {
        uri += window.location.protocol + "//" + window.location.hostname;
        if (window.location.pathname.split('/').length > 3) {
            for (var i = 0; i < window.location.pathname.split('/').length - 3; i++) {
                uri += "/" + window.location.pathname.split('/')[i + 1];
            }
        }
    }
    else if (window.location.port == "" && window.location.host == "localhost") {
        uri = window.location.protocol + "//" + window.location.hostname + "/" + window.location.pathname.split('/')[1];
    }
    else {
        uri = window.location.protocol + "//" + window.location.hostname + ":" + window.location.port;
        if (window.location.pathname.split('/').length > 3) {
            for (var i = 0; i < window.location.pathname.split('/').length - 3; i++) {
                uri += "/" + window.location.pathname.split('/')[i + 1];
            }
        }
    }
}
else if (localStorage.getItem("masterTxtWhetherADAuthentication") == 'true') {
    $("#liRecoverPassword").hide();
    $("#liRecoverDivider").hide();
    $("#liChangePassword").hide();
    $("#liChangeDivider").hide();
    var uri = "";
    if (window.location.port == "" && window.location.host != "localhost") {
        uri += window.location.protocol + "//" + window.location.hostname;
        if (window.location.pathname.split('/').length > 3) {
            for (var i = 0; i < window.location.pathname.split('/').length - 3; i++) {
                uri += "/" + window.location.pathname.split('/')[i + 1];
            }
        }
    }
    else if (window.location.port == "" && window.location.host == "localhost") {
        uri = window.location.protocol + "//" + window.location.hostname + "/" + window.location.pathname.split('/')[1];
    }
    else {
        uri = window.location.protocol + "//" + window.location.hostname + ":" + window.location.port;
        if (window.location.pathname.split('/').length > 3) {
            for (var i = 0; i < window.location.pathname.split('/').length - 3; i++) {
                uri += "/" + window.location.pathname.split('/')[i + 1];
            }
        }
    }
}
else {
    $("#liRecoverPassword").show();
    $("#liRecoverDivider").show();
    $("#liChangePassword").show();
    $("#liChangeDivider").show();
    $("#liCodeOfConductDivider").show();
    $("#liLogOut").show();
    var uri = "";
    if (window.location.port == "" && window.location.host != "localhost") {
        uri += window.location.protocol + "//" + window.location.hostname;
        if (window.location.pathname.split('/').length > 3) {
            for (var i = 0; i < window.location.pathname.split('/').length - 3; i++) {
                uri += "/" + window.location.pathname.split('/')[i + 1];
            }
        }
    }
    else if (window.location.port == "" && window.location.host == "localhost") {
        uri = window.location.protocol + "//" + window.location.hostname + "/" + window.location.pathname.split('/')[1];
    }
    else {
        uri = window.location.protocol + "//" + window.location.hostname + ":" + window.location.port;
        if (window.location.pathname.split('/').length > 3) {
            for (var i = 0; i < window.location.pathname.split('/').length - 3; i++) {
                uri += "/" + window.location.pathname.split('/')[i + 1];
            }
        }
    }
}

getLoggedInUserInformationMaster();

function downloadURL1(url) {
    window.open("../" + url, null);
};

function getLoggedInUserInformationMaster() {
    $("#Loader").show();
    var webUrl = uri + "/api/Transaction/GetUserDetails";
    $.ajax({
        url: webUrl,
        type: "GET",
        async: false,
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            $("#Loader").hide();
            if (isJsonMaster(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                userRole = msg.User.userRole.ROLE_NM;
                if (msg.User.userRole.ROLE_NM.toLowerCase() == 'admin' || msg.User.userRole.ROLE_NM.toLowerCase() == 'administrator') {
                    $("#liAdminManual").show();
                    $("#liAdminManualDivider").show();
                }
                else {
                    $("#liAdminManual").hide();
                    $("#liAdminManualDivider").hide();
                }
            }
            else {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    //alert(msg.Msg);
                    //return false;
                }
            }
        },
        error: function (response) {
            $("#Loader").hide();
            alert(response.status + ' ' + response.statusText);
        }
    })
}

function isJsonMaster(str) {
    try {
        JSON.parse(str);
    } catch (e) {
        return false;
    }
    return true;
}

function isValidInput(value) {
    var count = 0;
    if (value.indexOf('<applet>') !== -1) {
        count++;
    }
    if (value.indexOf('<body>') !== -1) {
        count++;
    }
    if (value.indexOf('<embed>') !== -1) {
        count++;
    }
    if (value.indexOf('<frame>') !== -1) {
        count++;
    }
    if (value.indexOf('<script>') !== -1) {
        count++;
    }
    if (value.indexOf('<frameset>') !== -1) {
        count++;
    }
    if (value.indexOf('<html>') !== -1) {
        count++;
    }
    if (value.indexOf('<iframe>') !== -1) {
        count++;
    }
    if (value.indexOf('<img>') !== -1) {
        count++;
    }
    if (value.indexOf('<style>') !== -1) {
        count++;
    }
    if (value.indexOf('<layer>') !== -1) {
        count++;
    }
    if (value.indexOf('<link>') !== -1) {
        count++;
    }
    if (value.indexOf('<ilayer>') !== -1) {
        count++;
    }
    if (value.indexOf('<meta>') !== -1) {
        count++;
    }
    if (value.indexOf('<object>') !== -1) {
        count++;
    }
    if (value.indexOf('<') !== -1) {
        count++;
    }
    if (value.indexOf('>') !== -1) {
        count++;
    }

    if (count > 0) {
        return false;
    }
    else {
        return true;
    }
}
function isValidPan(valPan) {
    //alert("In function ValidatePAN");
    //alert("valPan=" + valPan);
    var regpan = /^([a-zA-Z]){5}([0-9]){4}([a-zA-Z]){1}?$/;
    var fl = true;
    if (valPan == "" || valPan == null || valPan == undefined) {
        fl = false;
    }
    else if (!regpan.test(valPan)) {
        fl = false;
    }
    return fl;
}
if (window.location.pathname.split('/')[window.location.pathname.split('/').length - 1] == 'Dashboard.aspx') {
    $(".page-sidebar-menu li").removeClass('start')
    $(".page-sidebar-menu li").removeClass('active')
    $(".page-sidebar-menu li").removeClass('open')
    $("#liDashboard").addClass('start active open');
}
else if (window.location.pathname.split('/')[window.location.pathname.split('/').length - 1] == 'SMTP_CONFIG_SETUP.aspx') {
    $(".page-sidebar-menu li").removeClass('start')
    $(".page-sidebar-menu li").removeClass('active')
    $(".page-sidebar-menu li").removeClass('open')
    $("#liSMTP_CONFIG_SETUP").closest('ul').css({ 'display': 'block' });
    $($($($("#liSMTP_CONFIG_SETUP").closest('ul')).closest('li').children()[0]).children()[2]).addClass('open');
    $("#liSMTP_CONFIG_SETUP").addClass('start active open');
}
else if (window.location.pathname.split('/')[window.location.pathname.split('/').length - 1] == 'Approver.aspx') {
    $(".page-sidebar-menu li").removeClass('start')
    $(".page-sidebar-menu li").removeClass('active')
    $(".page-sidebar-menu li").removeClass('open')
    $("#liApprover").closest('ul').css({ 'display': 'block' });
    $($($($("#liApprover").closest('ul')).closest('li').children()[0]).children()[2]).addClass('open');
    $("#liApprover").addClass('start active open');
}
else if (window.location.pathname.split('/')[window.location.pathname.split('/').length - 1] == 'UPSI_CONFIG_SETUP.aspx') {
    $(".page-sidebar-menu li").removeClass('start')
    $(".page-sidebar-menu li").removeClass('active')
    $(".page-sidebar-menu li").removeClass('open')
    $("#liUPSI_CONFIG_SETUP").closest('ul').css({ 'display': 'block' });
    $($($($("#liUPSI_CONFIG_SETUP").closest('ul')).closest('li').children()[0]).children()[2]).addClass('open');
    $("#liUPSI_CONFIG_SETUP").addClass('start active open');
}
else if (window.location.pathname.split('/')[window.location.pathname.split('/').length - 1] == 'Benpos.aspx') {
    $(".page-sidebar-menu li").removeClass('start')
    $(".page-sidebar-menu li").removeClass('active')
    $(".page-sidebar-menu li").removeClass('open')
    $("#liBenpos").closest('ul').css({ 'display': 'block' });
    $($($($("#liBenpos").closest('ul')).closest('li').children()[0]).children()[2]).addClass('open');
    $("#liBenpos").addClass('start active open');
}
else if (window.location.pathname.split('/')[window.location.pathname.split('/').length - 1] == 'RestrictedCompanies.aspx') {
    $(".page-sidebar-menu li").removeClass('start')
    $(".page-sidebar-menu li").removeClass('active')
    $(".page-sidebar-menu li").removeClass('open')
    $("#liRestrictedCompanies").closest('ul').css({ 'display': 'block' });
    $($($($("#liRestrictedCompanies").closest('ul')).closest('li').children()[0]).children()[2]).addClass('open');
    $("#liRestrictedCompanies").addClass('start active open');
}
else if (window.location.pathname.split('/')[window.location.pathname.split('/').length - 1] == 'User.aspx') {
    $(".page-sidebar-menu li").removeClass('start')
    $(".page-sidebar-menu li").removeClass('active')
    $(".page-sidebar-menu li").removeClass('open')
    $("#liUser").closest('ul').css({ 'display': 'block' });
    $($($($("#liUser").closest('ul')).closest('li').children()[0]).children()[2]).addClass('open');
    $("#liUser").addClass('start active open');
}
else if (window.location.pathname.split('/')[window.location.pathname.split('/').length - 1] == 'Physical_Share_Master.aspx') {
    $(".page-sidebar-menu li").removeClass('start')
    $(".page-sidebar-menu li").removeClass('active')
    $(".page-sidebar-menu li").removeClass('open')
    $("#liPhysical_Share_Master").closest('ul').css({ 'display': 'block' });
    $($($($("#liPhysical_Share_Master").closest('ul')).closest('li').children()[0]).children()[2]).addClass('open');
    $("#liPhysical_Share_Master").addClass('start active open');
}
else if (window.location.pathname.split('/')[window.location.pathname.split('/').length - 1] == 'TradingWindowClosure.aspx') {
    $(".page-sidebar-menu li").removeClass('start')
    $(".page-sidebar-menu li").removeClass('active')
    $(".page-sidebar-menu li").removeClass('open')
    $("#liTradingWindowClosure").closest('ul').css({ 'display': 'block' });
    $($($($("#liTradingWindowClosure").closest('ul')).closest('li').children()[0]).children()[2]).addClass('open');
    $("#liTradingWindowClosure").addClass('start active open');
}
else if (window.location.pathname.split('/')[window.location.pathname.split('/').length - 1] == 'Department.aspx') {
    $(".page-sidebar-menu li").removeClass('start')
    $(".page-sidebar-menu li").removeClass('active')
    $(".page-sidebar-menu li").removeClass('open')
    $("#liDepartment").closest('ul').css({ 'display': 'block' });
    $($($($("#liDepartment").closest('ul')).closest('li').children()[0]).children()[2]).addClass('open');
    $("#liDepartment").addClass('start active open');
}
else if (window.location.pathname.split('/')[window.location.pathname.split('/').length - 1] == 'Designation.aspx') {
    $(".page-sidebar-menu li").removeClass('start')
    $(".page-sidebar-menu li").removeClass('active')
    $(".page-sidebar-menu li").removeClass('open')
    $("#liDesignation").closest('ul').css({ 'display': 'block' });
    $($($($("#liDesignation").closest('ul')).closest('li').children()[0]).children()[2]).addClass('open');
    $("#liDesignation").addClass('start active open');
}
else if (window.location.pathname.split('/')[window.location.pathname.split('/').length - 1] == 'Relation.aspx') {
    $(".page-sidebar-menu li").removeClass('start')
    $(".page-sidebar-menu li").removeClass('active')
    $(".page-sidebar-menu li").removeClass('open')
    $("#liRelation").closest('ul').css({ 'display': 'block' });
    $($($($("#liRelation").closest('ul')).closest('li').children()[0]).children()[2]).addClass('open');
    $("#liRelation").addClass('start active open');
}
else if (window.location.pathname.split('/')[window.location.pathname.split('/').length - 1] == 'Policy.aspx') {
    $(".page-sidebar-menu li").removeClass('start')
    $(".page-sidebar-menu li").removeClass('active')
    $(".page-sidebar-menu li").removeClass('open')
    $("#liPolicy").closest('ul').css({ 'display': 'block' });
    $($($($("#liPolicy").closest('ul')).closest('li').children()[0]).children()[2]).addClass('open');
    $("#liPolicy").addClass('start active open');
}
else if (window.location.pathname.split('/')[window.location.pathname.split('/').length - 1] == 'PreClearanceApprovalHierarchySetUp.aspx') {
    $(".page-sidebar-menu li").removeClass('start')
    $(".page-sidebar-menu li").removeClass('active')
    $(".page-sidebar-menu li").removeClass('open')
    $("#liPreClearanceApprovalHierarchySetUp").closest('ul').css({ 'display': 'block' });
    $($($($("#liPreClearanceApprovalHierarchySetUp").closest('ul')).closest('li').children()[0]).children()[2]).addClass('open');
    $("#liPreClearanceApprovalHierarchySetUp").addClass('start active open');
}
else if (window.location.pathname.split('/')[window.location.pathname.split('/').length - 1] == 'Category_Master.aspx') {
    $(".page-sidebar-menu li").removeClass('start')
    $(".page-sidebar-menu li").removeClass('active')
    $(".page-sidebar-menu li").removeClass('open')
    $("#liCategory_Master").closest('ul').css({ 'display': 'block' });
    $($($($("#liCategory_Master").closest('ul')).closest('li').children()[0]).children()[2]).addClass('open');
    $("#liCategory_Master").addClass('start active open');
}
else if (window.location.pathname.split('/')[window.location.pathname.split('/').length - 1] == 'Sub_Category_Master.aspx') {
    $(".page-sidebar-menu li").removeClass('start')
    $(".page-sidebar-menu li").removeClass('active')
    $(".page-sidebar-menu li").removeClass('open')
    $("#liSub_Category_Master").closest('ul').css({ 'display': 'block' });
    $($($($("#liSub_Category_Master").closest('ul')).closest('li').children()[0]).children()[2]).addClass('open');
    $("#liSub_Category_Master").addClass('start active open');
}
else if (window.location.pathname.split('/')[window.location.pathname.split('/').length - 1] == 'Location_Master.aspx') {
    $(".page-sidebar-menu li").removeClass('start')
    $(".page-sidebar-menu li").removeClass('active')
    $(".page-sidebar-menu li").removeClass('open')
    $("#liLocation_Master").closest('ul').css({ 'display': 'block' });
    $($($($("#liLocation_Master").closest('ul')).closest('li').children()[0]).children()[2]).addClass('open');
    $("#liLocation_Master").addClass('start active open');
}
else if (window.location.pathname.split('/')[window.location.pathname.split('/').length - 1] == 'Pre_Clearance_Request.aspx') {
    $(".page-sidebar-menu li").removeClass('start')
    $(".page-sidebar-menu li").removeClass('active')
    $(".page-sidebar-menu li").removeClass('open')
    $("#liPre_Clearance_Request").closest('ul').css({ 'display': 'block' });
    $($($($("#liPre_Clearance_Request").closest('ul')).closest('li').children()[0]).children()[2]).addClass('open');
    $("#liPre_Clearance_Request").addClass('start active open');
}
else if (window.location.pathname.split('/')[window.location.pathname.split('/').length - 1] == 'TradingRequestDetails.aspx') {
    $(".page-sidebar-menu li").removeClass('start')
    $(".page-sidebar-menu li").removeClass('active')
    $(".page-sidebar-menu li").removeClass('open')
    $("#liTradingRequestDetails").closest('ul').css({ 'display': 'block' });
    $($($($("#liTradingRequestDetails").closest('ul')).closest('li').children()[0]).children()[2]).addClass('open');
    $("#liTradingRequestDetails").addClass('start active open');
}
else if (window.location.pathname.split('/')[window.location.pathname.split('/').length - 1] == 'TradingRequestDetailsAdmin.aspx') {
    $(".page-sidebar-menu li").removeClass('start')
    $(".page-sidebar-menu li").removeClass('active')
    $(".page-sidebar-menu li").removeClass('open')
    $("#liTradingRequestDetailsAdmin").closest('ul').css({ 'display': 'block' });
    $($($($("#liTradingRequestDetailsAdmin").closest('ul')).closest('li').children()[0]).children()[2]).addClass('open');
    $("#liTradingRequestDetailsAdmin").addClass('start active open');
}
else if (window.location.pathname.split('/')[window.location.pathname.split('/').length - 1] == 'UserDeclaration.aspx') {
    $(".page-sidebar-menu li").removeClass('start')
    $(".page-sidebar-menu li").removeClass('active')
    $(".page-sidebar-menu li").removeClass('open')
    $("#liUserDeclaration").closest('ul').css({ 'display': 'block' });
    $($($($("#liUserDeclaration").closest('ul')).closest('li').children()[0]).children()[2]).addClass('open');
    $("#liUserDeclaration").addClass('start active open');
}
else if (window.location.pathname.split('/')[window.location.pathname.split('/').length - 1] == 'MyDetails.aspx') {
    $(".page-sidebar-menu li").removeClass('start')
    $(".page-sidebar-menu li").removeClass('active')
    $(".page-sidebar-menu li").removeClass('open')
    $("#liMyDetails").closest('ul').css({ 'display': 'block' });
    $($($($("#liMyDetails").closest('ul')).closest('li').children()[0]).children()[2]).addClass('open');
    $("#liMyDetails").addClass('start active open');
}
else if (window.location.pathname.split('/')[window.location.pathname.split('/').length - 1] == 'Relative.aspx') {
    $(".page-sidebar-menu li").removeClass('start')
    $(".page-sidebar-menu li").removeClass('active')
    $(".page-sidebar-menu li").removeClass('open')
    $("#liRelative").closest('ul').css({ 'display': 'block' });
    $($($($("#liRelative").closest('ul')).closest('li').children()[0]).children()[2]).addClass('open');
    $("#liRelative").addClass('start active open');
}
else if (window.location.pathname.split('/')[window.location.pathname.split('/').length - 1] == 'Demat.aspx') {
    $(".page-sidebar-menu li").removeClass('start')
    $(".page-sidebar-menu li").removeClass('active')
    $(".page-sidebar-menu li").removeClass('open')
    $("#liDemat").closest('ul').css({ 'display': 'block' });
    $($($($("#liDemat").closest('ul')).closest('li').children()[0]).children()[2]).addClass('open');
    $("#liDemat").addClass('start active open');
}
else if (window.location.pathname.split('/')[window.location.pathname.split('/').length - 1] == 'InitialHolding.aspx') {
    $(".page-sidebar-menu li").removeClass('start')
    $(".page-sidebar-menu li").removeClass('active')
    $(".page-sidebar-menu li").removeClass('open')
    $("#liInitialHolding").closest('ul').css({ 'display': 'block' });
    $($($($("#liInitialHolding").closest('ul')).closest('li').children()[0]).children()[2]).addClass('open');
    $("#liInitialHolding").addClass('start active open');
}
else if (window.location.pathname.split('/')[window.location.pathname.split('/').length - 1] == 'Declaration.aspx') {
    $(".page-sidebar-menu li").removeClass('start')
    $(".page-sidebar-menu li").removeClass('active')
    $(".page-sidebar-menu li").removeClass('open')
    $("#liDeclaration").closest('ul').css({ 'display': 'block' });
    $($($($("#liDeclaration").closest('ul')).closest('li').children()[0]).children()[2]).addClass('open');
    $("#liDeclaration").addClass('start active open');
}
else if (window.location.pathname.split('/')[window.location.pathname.split('/').length - 1] == 'MyDetailsAdmin.aspx') {
    $(".page-sidebar-menu li").removeClass('start')
    $(".page-sidebar-menu li").removeClass('active')
    $(".page-sidebar-menu li").removeClass('open')
    $("#liMyDetailsAdmin").closest('ul').css({ 'display': 'block' });
    $($($($("#liMyDetailsAdmin").closest('ul')).closest('li').children()[0]).children()[2]).addClass('open');
    $("#liMyDetailsAdmin").addClass('start active open');
}
else if (window.location.pathname.split('/')[window.location.pathname.split('/').length - 1] == 'RelativeAdmin.aspx') {
    $(".page-sidebar-menu li").removeClass('start')
    $(".page-sidebar-menu li").removeClass('active')
    $(".page-sidebar-menu li").removeClass('open')
    $("#liRelativeAdmin").closest('ul').css({ 'display': 'block' });
    $($($($("#liRelativeAdmin").closest('ul')).closest('li').children()[0]).children()[2]).addClass('open');
    $("#liRelativeAdmin").addClass('start active open');
}
else if (window.location.pathname.split('/')[window.location.pathname.split('/').length - 1] == 'DematAdmin.aspx') {
    $(".page-sidebar-menu li").removeClass('start')
    $(".page-sidebar-menu li").removeClass('active')
    $(".page-sidebar-menu li").removeClass('open')
    $("#liDematAdmin").closest('ul').css({ 'display': 'block' });
    $($($($("#liDematAdmin").closest('ul')).closest('li').children()[0]).children()[2]).addClass('open');
    $("#liDematAdmin").addClass('start active open');
}
else if (window.location.pathname.split('/')[window.location.pathname.split('/').length - 1] == 'InitialHoldingAdmin.aspx') {
    $(".page-sidebar-menu li").removeClass('start')
    $(".page-sidebar-menu li").removeClass('active')
    $(".page-sidebar-menu li").removeClass('open')
    $("#liInitialHoldingAdmin").closest('ul').css({ 'display': 'block' });
    $($($($("#liInitialHoldingAdmin").closest('ul')).closest('li').children()[0]).children()[2]).addClass('open');
    $("#liInitialHoldingAdmin").addClass('start active open');
}
else if (window.location.pathname.split('/')[window.location.pathname.split('/').length - 1] == 'DeclarationAdmin.aspx') {
    $(".page-sidebar-menu li").removeClass('start')
    $(".page-sidebar-menu li").removeClass('active')
    $(".page-sidebar-menu li").removeClass('open')
    $("#liDeclarationAdmin").closest('ul').css({ 'display': 'block' });
    $($($($("#liDeclarationAdmin").closest('ul')).closest('li').children()[0]).children()[2]).addClass('open');
    $("#liDeclarationAdmin").addClass('start active open');
}
else if (window.location.pathname.split('/')[window.location.pathname.split('/').length - 1] == 'DeclarationReports.aspx') {
    $(".page-sidebar-menu li").removeClass('start')
    $(".page-sidebar-menu li").removeClass('active')
    $(".page-sidebar-menu li").removeClass('open')
    $("#liDeclarationReports").closest('ul').css({ 'display': 'block' });
    $($($($("#liDeclarationReports").closest('ul')).closest('li').children()[0]).children()[2]).addClass('open');
    $("#liDeclarationReports").addClass('start active open');
}
else if (window.location.pathname.split('/')[window.location.pathname.split('/').length - 1] == 'TradingReport.aspx') {
    $(".page-sidebar-menu li").removeClass('start')
    $(".page-sidebar-menu li").removeClass('active')
    $(".page-sidebar-menu li").removeClass('open')
    $("#liTradingReport").closest('ul').css({ 'display': 'block' });
    $($($($("#liTradingReport").closest('ul')).closest('li').children()[0]).children()[2]).addClass('open');
    $("#liTradingReport").addClass('start active open');
}
else if (window.location.pathname.split('/')[window.location.pathname.split('/').length - 1] == 'PreClearanceTask.aspx') {
    $(".page-sidebar-menu li").removeClass('start')
    $(".page-sidebar-menu li").removeClass('active')
    $(".page-sidebar-menu li").removeClass('open')
    $("#liPreClearanceTask").closest('ul').css({ 'display': 'block' });
    $($($($("#liPreClearanceTask").closest('ul')).closest('li').children()[0]).children()[2]).addClass('open');
    $("#liPreClearanceTask").addClass('start active open');
}
else if (window.location.pathname.split('/')[window.location.pathname.split('/').length - 1] == 'CloseNonCompliantTask.aspx') {
    $(".page-sidebar-menu li").removeClass('start')
    $(".page-sidebar-menu li").removeClass('active')
    $(".page-sidebar-menu li").removeClass('open')
    $("#liCloseNonCompliantTask").closest('ul').css({ 'display': 'block' });
    $($($($("#liCloseNonCompliantTask").closest('ul')).closest('li').children()[0]).children()[2]).addClass('open');
    $("#liCloseNonCompliantTask").addClass('start active open');
}
else if (window.location.pathname.split('/')[window.location.pathname.split('/').length - 1] == 'UPSI.aspx') {
    $(".page-sidebar-menu li").removeClass('start')
    $(".page-sidebar-menu li").removeClass('active')
    $(".page-sidebar-menu li").removeClass('open')
    $("#liUPSI").closest('ul').css({ 'display': 'block' });
    $($($($("#liUPSI").closest('ul')).closest('li').children()[0]).children()[2]).addClass('open');
    $("#liUPSI").addClass('start active open');
}
else if (window.location.pathname.split('/')[window.location.pathname.split('/').length - 1] == 'UPSIReports.aspx') {
    $(".page-sidebar-menu li").removeClass('start')
    $(".page-sidebar-menu li").removeClass('active')
    $(".page-sidebar-menu li").removeClass('open')
    $("#liUPSIReports").closest('ul').css({ 'display': 'block' });
    $($($($("#liUPSIReports").closest('ul')).closest('li').children()[0]).children()[2]).addClass('open');
    $("#liUPSIReports").addClass('start active open');
}
else if (window.location.pathname.split('/')[window.location.pathname.split('/').length - 1] == 'Forms.aspx') {
    $(".page-sidebar-menu li").removeClass('start')
    $(".page-sidebar-menu li").removeClass('active')
    $(".page-sidebar-menu li").removeClass('open')
    $("#liForms").closest('ul').css({ 'display': 'block' });
    $($($($("#liForms").closest('ul')).closest('li').children()[0]).children()[2]).addClass('open');
    $("#liForms").addClass('start active open');
}
else if (window.location.pathname.split('/')[window.location.pathname.split('/').length - 1] == 'UserManualAndPolicyDocument.aspx') {
    $(".page-sidebar-menu li").removeClass('start')
    $(".page-sidebar-menu li").removeClass('active')
    $(".page-sidebar-menu li").removeClass('open')
    $("#liUserManualAndPolicyDocument").addClass('start active open');
}
else if (window.location.pathname.split('/')[window.location.pathname.split('/').length - 1] == 'Support.aspx') {
    $(".page-sidebar-menu li").removeClass('start')
    $(".page-sidebar-menu li").removeClass('active')
    $(".page-sidebar-menu li").removeClass('open')
    $("#liSupport").addClass('start active open');
}