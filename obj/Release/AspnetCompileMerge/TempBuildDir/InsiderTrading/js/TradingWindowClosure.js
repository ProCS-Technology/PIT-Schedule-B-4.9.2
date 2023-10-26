var tradingWindowClosureList = null;
var lstClosure = null;
var userEmailsSelected = [];

$(document).ready(function () {
    $("#Loader").hide();
    fnGetInsiderTradingWindowClosureInfoList();
    $('.datepicker').datepicker({
        todayHighlight: true,
        autoclose: true,
        format: "dd/mm/yyyy",
        clearBtn: true
        //startDate: "today"
    });
    //fnBindUserList();
});
function isJson(str) {
    try {
        JSON.parse(str);
    }
    catch (e) {
        return false;
    }
    return true;
}
function initializeDataTable(id, columns) {
    $('#' + id).DataTable({
        dom: 'Bfrtip',
        pageLength: 10,
        "scrollY": "300px",
        // "scrollX": true,
        "bSort": false,
        buttons: [
            {
                extend: 'pdf',
                className: 'btn green btn-outline',
                exportOptions: {
                    columns: columns
                }
            }, 
            {
                extend: 'excel',
                className: 'btn yellow btn-outline ',
                exportOptions: {
                    columns: columns
                }
            },
        ]
    });
}
function fnRollBack() {
    window.location.reload();
}
function fnBindUserList() {
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/GetUserList";
    $.ajax({
        type: "GET",
        url: webUrl,
        placeholder: "Select User",
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            if (msg.StatusFl == true) {
                var result = "";

                result += '<option value="0">Select All</option>';
                for (var i = 0; i < msg.UserList.length; i++) {
                    userEmailsSelected.push(msg.UserList[i].USER_EMAIL);
                    result += '<option value="' + msg.UserList[i].USER_EMAIL + '">' + msg.UserList[i].USER_NM + '(' + msg.UserList[i].USER_EMAIL + ')' + '</option>';
                }
                //$("#bindUser").html(result);
                //$("#bindUser").multiselect();
                $("#Loader").hide();

            }
            else {
                $("#Loader").hide();
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    alert(msg.Msg);
                    return false;
                }
            }
        },
        error: function (response) {
            $("#Loader").hide();
            alert(response.status + ' ' + response.statusText);
        }
    });

}
function fnSaveTradingWindow() {
    if (fnVaidateTradingWindow()) {
        if (fnValidateToDateEmptyOrNot()) {
            if (confirm("This action will make the trading window closure to boundless time until you come again and update the from date. Are you sure you want to continue?")) {
                fnAddTradingWindow();
            }
        }
        else {
            fnAddTradingWindow();
        }
    }
}
function fnValidateToDateEmptyOrNot() {
    if ($("#txtTradingWindowTo").val() == undefined || $("#txtTradingWindowTo").val() == null || $("#txtTradingWindowTo").val().trim() == "") {
        return true;
    }
    return false;
}
function fnGetWhetherToDateBlankOrNotInTradingWindowClosureList() {
    var found = false;
    if (tradingWindowClosureList == null) {
        tradingWindowClosureList = [];
    }
    for (var i = 0; i < tradingWindowClosureList.length; i++) {
        if (tradingWindowClosureList[i].toDate == "" || tradingWindowClosureList[i].toDate == null || tradingWindowClosureList[i].toDate == undefined) {
            found = true;
        }
    }
    return found;
}
function fnOpenTradingWindowClosureModule() {

    if (!fnGetWhetherToDateBlankOrNotInTradingWindowClosureList()) {
        $("#tradingWindowClosureModel").modal('show');
    }
    else {
        alert("Firstly update the trading window closure 'To' date from blank of existing list !");
    }
}
function fnAddTradingWindow() {
    var windowClosureTypeId = $("select[id*=ddlWindowClosureType]").val();
    var fromDate = $("#txtTradingWindowFrom").val();
    var toDate = $("#txtTradingWindowTo").val();
    var boardMeetingScheduledOn = $("#txtBoardMeetingScheduledOn").val();
    var quarterEndedOn = $("#txtQuarterEndedOn").val();
    var remarks = $("#txtTradingWindowRemarks").val();
    var id = $("#txtTradingWindowId").val();
    $("#Loader").show();
    var webUrl = uri + "/api/TradingWindow/SaveInsiderTradingWindow";
    $.ajax({
        url: webUrl,
        type: "POST",
        data: JSON.stringify({ id: id, fromDate: fromDate, toDate: toDate, remarks: remarks, boardMeetingScheduledOn: boardMeetingScheduledOn, quarterEndedOn: quarterEndedOn, windowClosureTypeId: windowClosureTypeId }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        //  async: false,
        success: function (msg) {
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                $("#Loader").hide();
                alert(msg.Msg);
                window.location.reload();
            }
            else {
                $("#Loader").hide();
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    alert(msg.Msg);
                    return false;
                }
            }

        },
        error: function (response) {
            $("#Loader").hide();
            alert(response.status + ' ' + response.statusText);
        }
    })
}
function fnEditTradingWindow(id, from, to, boardMeetingScheduledOn, quarterEndedOn, windowClosureTypeId, cntrl) {
    //alert(from);
    $("select[id*=ddlWindowClosureType]").val(windowClosureTypeId);
    $("#txtTradingWindowFrom").val(from);
    $("#txtTradingWindowTo").val(to);//.datepicker("setDate", new Date(to));
    $("#txtBoardMeetingScheduledOn").val(boardMeetingScheduledOn);//.datepicker("setDate", boardMeetingScheduledOn);
    $("#txtQuarterEndedOn").datepicker("setDate", quarterEndedOn);
    $("#txtTradingWindowId").val(id);
    var tr = $(cntrl).closest('tr');
    var remarks = $(tr.children()[3]).html();
    $("#txtTradingWindowRemarks").val(remarks);
}
function fnSendMailTradingWindow(tradingWindowId) {
    //alert("tradingWindowId=" + tradingWindowId);
    if (lstClosure != null) {
        for (var x = 0; x < lstClosure.length; x++) {
            //alert("lstClosure[" + x + "].id=" + lstClosure[x].id);
            if (lstClosure[x].id == tradingWindowId) {
                //alert("In true condition");
                $("#txtTWCId").val(tradingWindowId);
                //alert(lstClosure[x].EmailTemplate);
                $('#summernote_1').summernote('code', lstClosure[x].EmailTemplate);
                $(tradingWindowClosureMail).modal();
            }
        }
    }
}
function fnMailTradingWindow() {
    //$("#Loader").show();
    var emailMsg = $('#summernote_1').summernote('code');
    emailMsg = '<span style="font-weight: normal !important;font-family:Trebuchet MS !important;">' + emailMsg + '</span>';
    var tradingWindowId = $('#txtTWCId').val();
    var isSelectAll = false;
    var tempUserSelected = [];
    var usrSelected = "";

    $('#ContentPlaceHolder1_bindUser option:selected').each(function () {
        var EmailSelectedUser = $(this).val();
        if (validateEmail(EmailSelectedUser)) {
            if (EmailSelectedUser != "") {
                usrSelected += EmailSelectedUser + ",";
            }
            tempUserSelected.push(EmailSelectedUser);
        }
    });

    var relatives;
    if ($("#chkRelatives").is(':checked')) {
        relatives = "Yes";
    }
    else {
        relatives = "No";
    }

    var CPs;
    if ($("#chkCP").is(':checked')) {
        CPs = "Yes";
    }
    else {
        CPs = "No";
    }
    alert("Relatives=" + relatives);
    alert("CPs=" + CPs);
    
    var user = tempUserSelected;

    var formData = new FormData();
    formData.append("TradingWindowId", tradingWindowId);
    formData.append("EmailTemplate", emailMsg);
    formData.append("Users", usrSelected);
    formData.append("relatives", relatives);
    formData.append("CPs", CPs);

    var totalFiles = document.getElementById("txtAttachment").files.length;
    var file = "";
    for (var i = 0; i < totalFiles; i++) {
        file = document.getElementById("txtAttachment").files[i];
        formData.append("file", file);
    }

    $("#Loader").show();
    var webUrl = uri + "/api/TradingWindow/SendEmailForTradingWindowClosure";
    $.ajax({
        url: webUrl,
        type: "POST",
        cache: false,
        contentType: false,
        processData: false,
        data: formData,
        async: false,
        success: function (msg) {
            $("#Loader").hide();
            if (msg.Msg == "SessionExpired") {
                alert("Your session is expired. Please login again to continue");
                window.location.href = "../LogOut.aspx";
            }
            if (msg.StatusFl == true) {
                window.location.reload();
            }
            else {
                alert(msg.Msg);
            }
        },
        error: function (response) {
            $("#Loader").hide();
            if (response.responseText == "Session Expired") {
                alert("Your session is expired. Please login again to continue");
                window.location.href = "../LogOut.aspx";
                return false;
            }
            else {
                alert(response.status + ' ' + response.statusText);
            }
        }
    });

    /*var webUrl = uri + "/api/TradingWindow/SendEmailForTradingWindowClosure";
    $.ajax({
        url: webUrl,
        type: "POST",
        data: JSON.stringify({ id: tradingWindowId, EmailTemplate: emailMsg, lstUser: user, relatives:relatives, CPs:CPs }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                $("#Loader").hide();
                alert(msg.Msg);
                fnCloseMailWindow();
            }
            else {
                $("#Loader").hide();
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    alert(msg.Msg);
                    return false;
                }
            }
        },
        error: function (response) {
            $("#Loader").hide();
            alert(response.status + ' ' + response.statusText);
        }
    })*/
}
function fnVaidateTradingWindow() {
    var count = 0;
    if ($("select[id*=ddlWindowClosureType]").val() == undefined || $("select[id*=ddlWindowClosureType]").val() == null || $("select[id*=ddlWindowClosureType]").val().trim() == "0" || $("select[id*=ddlWindowClosureType]").val().trim() == "") {
        count++;
        $("#lblWindowClosureType").addClass('required');
        $("select[id*=ddlWindowClosureType]").addClass('requiredBackground');
    }
    else {
        $("#lblWindowClosureType").removeClass('required');
        $("select[id*=ddlWindowClosureType]").removeClass('requiredBackground');
    }
    if ($("#txtTradingWindowFrom").val() == undefined || $("#txtTradingWindowFrom").val() == null || $("#txtTradingWindowFrom").val().trim() == "") {
        count++;
        $("#lblFrom").addClass('required');
        $("#txtTradingWindowFrom").addClass('requiredBackground');
    }
    else {
        $("#lblFrom").removeClass('required');
        $("#txtTradingWindowFrom").removeClass('requiredBackground');
    }
    if ($("#txtTradingWindowTo").val() != null) {
        
        var FromDate = convertToDateTime($("#txtTradingWindowFrom").val());
        var Todate = convertToDateTime($("#txtTradingWindowTo").val());

        if (Todate < FromDate) {
            count++;
            alert("To Date Should be greater than From Date");
            return false;
        }
    }
    //if ($("#txtBoardMeetingScheduledOn").val() == undefined || $("#txtBoardMeetingScheduledOn").val() == null || $("#txtBoardMeetingScheduledOn").val().trim() == "") {
    //    count++;
    //}
    //if ($("#txtQuarterEndedOn").val() == undefined || $("#txtQuarterEndedOn").val() == null || $("#txtQuarterEndedOn").val().trim() == "") {
    //    count++;
    //    $("#lblQuarterEndedOn").addClass('required');
    //    $("#txtQuarterEndedOn").addClass('requiredBackground');
    //}
    //else {
    //    $("#lblQuarterEndedOn").removeClass('required');
    //    $("#txtQuarterEndedOn").removeClass('requiredBackground');
    //}
    if ($("#txtTradingWindowRemarks").val() == undefined || $("#txtTradingWindowRemarks").val() == null || $("#txtTradingWindowRemarks").val().trim() == "") {
        count++;
        $("#lblRemarks").addClass('required');
        $("#txtTradingWindowRemarks").addClass('requiredBackground');
    }
    else {
        $("#lblRemarks").removeClass('required');
        $("#txtTradingWindowRemarks").removeClass('requiredBackground');
    }

    if (count > 0) {
        return false;
    }
    else {
        return true;
    }
}
function fnGetInsiderTradingWindowClosureInfoList() {
    $("#Loader").show();
    var webUrl = uri + "/api/TradingWindow/GetInsiderTradingWindowClosureInfoList";
    $.ajax({
        url: webUrl,
        type: "GET",
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                $("#Loader").hide();
                var result = "";
                lstClosure = msg.InsiderTradingWindowList;
                for (var i = 0; i < msg.InsiderTradingWindowList.length; i++) {
                    result += '<tr>';
                    result += '<td>' + msg.InsiderTradingWindowList[i].quarterEndedOn + '</td>';
                    result += '<td>' + msg.InsiderTradingWindowList[i].fromDate + '</td>';
                    
                    if (msg.InsiderTradingWindowList[i].toDate == "31/12/9999") {
                        msg.InsiderTradingWindowList[i].toDate = "";
                    }
                    result += '<td>' + msg.InsiderTradingWindowList[i].toDate + '</td>';
                    if (msg.InsiderTradingWindowList[i].boardMeetingScheduledOn == "31/12/9999") {
                        msg.InsiderTradingWindowList[i].boardMeetingScheduledOn = "";
                    }
                    //result += '<td>' + msg.InsiderTradingWindowList[i].boardMeetingScheduledOn + '</td>';

                    //if (msg.InsiderTradingWindowList[i].quarterEndedOn == "31/12/9999") {
                    //    msg.InsiderTradingWindowList[i].quarterEndedOn = "";
                    //}
                    //result += '<td>' + msg.InsiderTradingWindowList[i].quarterEndedOn + '</td>';
                    result += '<td>' + msg.InsiderTradingWindowList[i].remarks + '</td>';
                    result += '<td>';
                    result += '<div class="btn-group">';
                    result += '<a class="btn blue dropdown-toggle" href="javascript:;" data-toggle="dropdown">';
                    result += '<i class="fa fa-user"></i> Actions';
                    result += '<i class="fa fa-angle-down"></i>';
                    result += '</a>';
                    result += '<ul class="dropdown-menu pull-right">';
                    result += '<li>';
                    result += '<a data-target="#tradingWindowClosureModel" data-toggle="modal" class="btn btn-outline dark" onclick="javascript:fnEditTradingWindow(\'' + msg.InsiderTradingWindowList[i].id + '\',\'' + msg.InsiderTradingWindowList[i].fromDate + '\',\'' + msg.InsiderTradingWindowList[i].toDate + '\',\'' + msg.InsiderTradingWindowList[i].boardMeetingScheduledOn + '\',\'' + msg.InsiderTradingWindowList[i].quarterEndedOn + '\',\'' + msg.InsiderTradingWindowList[i].windowClosureTypeId + '\',this);">Edit</a>';
                    result += '</li>';
                    //     result += '<li class="divider"> </li>';
                    result += '<li>';
                    result += '<a class="btn btn-outline dark" onclick="javascript:fnSendMailTradingWindow(\'' + msg.InsiderTradingWindowList[i].id + '\');">Send Window Closure Notification</a>';
                    result += '</li>';
                    //result += '<li>';
                    //result += '<a class="btn btn-outline dark" onclick="javascript:fnSendMailTradingWindowOpen(\'' + msg.InsiderTradingWindowList[i].id + '\');">Send Window Opening Notification</a>';
                    //result += '</li>';
                    result += '</ul>';
                    result += '</div>';
                    result += '</td>';
                    result += '</tr>';
                }
                //alert(result);
                tradingWindowClosureList = msg.InsiderTradingWindowList;
                var table = $('#tbl-tradingWindow-setup').DataTable();
                table.destroy();
                $("#tbdTradingWindowList").html(result);
                initializeDataTable('tbl-tradingWindow-setup', [0, 1, 2, 3]);
            }
            else {
                $("#Loader").hide();
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    alert(msg.Msg);
                    return false;
                }
            }

        },
        error: function (response) {
            $("#Loader").hide();
            alert(response.status + ' ' + response.statusText);
        }
    })
}
function fnRemoveClass(obj, val1, val2) {
    $("#lbl" + val1).removeClass('required');
    $("#" + obj.id).removeClass('requiredBackground');
}
function ddlWindowClosureType_onChange(obj, val1, val2) {
    var closureTypeId = $("#ddlWindowClosureType").val();
    if (closureTypeId == "0" || closureTypeId == "") {
        $("#lbl" + val1).addClass('required');
        $("#" + val2).addClass('requiredBackground');
    }
    else {
        $("#lbl" + val1).removeClass('required');
        $("#" + val2).removeClass('requiredBackground');
    }
    $("#Loader").show();
    var webUrl = uri + "/api/TradingWindow/GetEmailTemplateForWindowClosure?ClosureTypeId=" + closureTypeId;
    $.ajax({
        url: webUrl,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                $("#Loader").hide();
                alert(msg.Msg);
                window.location.reload();
            }
            else {
                $("#Loader").hide();
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    alert(msg.Msg);
                    return false;
                }
            }
        },
        error: function (response) {
            $("#Loader").hide();
            alert(response.status + ' ' + response.statusText);
        }
    })
}
function fnCloseSendEmailTradingWindowModal() {
    $("#modalUserSelection").modal('hide');
    $("#bindUser").html('');
    fnBindUserList();
}
function fnCloseMailWindow() {
    $("#txtTWCId").val(0);
    $("#tradingWindowClosureMail").modal('hide');
}
function convertToDateTime(date) {
    date = new Date(date)
    return date.getDate() + '/' + (date.getMonth() + 1) + '/' + date.getFullYear();
}
function validateEmail(value) {
    var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
    if (reg.test(value) == false) {
        return false;
    }
    return true;
}