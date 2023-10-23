var tradingWindowClosureList = null;
var lstClosure = null;
var userEmailsSelected = [];
var downloadComplete = false;
var intervalListener;
$(document).ready(function () {
    $("#Loader").hide();
    fnGetInsiderTradingWindowClosureInfoList();
    $("#txtTradingWindowFrom").datepicker({
        todayHighlight: true,
        autoclose: true,
        format: $("input[id*=hdnJSDateFormat]").val(),
        clearBtn: true
    });
    $("#txtTradingWindowTo").datepicker({
        todayHighlight: true,
        autoclose: true,
        format: $("input[id*=hdnJSDateFormat]").val(),
        clearBtn: true
    });

    var start = $("input[id*=hdnEmailTask]").val();
    //alert("start=" + start);
    if (start == "Start") {
        $("#LoaderProgerss").show();
        intervalListener = window.setInterval(function () {
            if (!downloadComplete) {
                CallCheckEmailStatus();
            }
        }, 2000);
    }
    else if (start == "NullbyteFileError") {
        alert("Uploaded document contains nullbyte, please correct the name and try again.");
    }
    else if (start == "FileError") {
        alert("Please upload only pdf format");
    }
    else if (start == "Content type of the uploaded document does not matched with the permissible document") {
        alert("Content type of the uploaded document does not matched with the permissible document");
    }

    //$('.summernote').summernote({
    //    toolbar: [
    //        ['style', ['style']],
    //        ['font', ['bold', 'italic', 'underline', 'clear']],
    //        ['fontname', ['fontname']],
    //        ['color', ['color']],
    //        ['para', ['ul', 'ol', 'paragraph']],
    //        ['height', ['height']],
    //        ['table', ['table']],
    //        ['insert', ['link', 'hr']],
    //        ['view', ['fullscreen', 'codeview']],
    //        ['help', ['help']]
    //    ],
    //    height: 260
    //});
    //$('.summernote').on("summernote.enter", function (we, e) {
    //    $(this).summernote("pasteHTML", "<br><br>");
    //    e.preventDefault();
    //});
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
    //window.location.reload();
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
        if (tradingWindowClosureList[i].toDate == "" || tradingWindowClosureList[i].toDate == null || tradingWindowClosureList[i].toDate == undefined || tradingWindowClosureList[i].toDate == "9999-12-31") {
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
        alert("A Trading window closure event is already open(today), pleas close before adding a new event");
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
    if (lstClosure != null) {
        for (var x = 0; x < lstClosure.length; x++) {
            if (lstClosure[x].id == tradingWindowId) {
                $("#txtTWCId").val(tradingWindowId);
                $('#summernote_1').summernote('code', lstClosure[x].EmailTemplate);
                $('#summernote_1').on("summernote.enter", function (we, e) {
                    $(this).summernote("pasteHTML", "<br><br>");
                    e.preventDefault();
                });
                //$('#summernote_1').summernote({
                //    callbacks: {
                //        onChange: function (contents, $editable) {
                //            alert($editable);
                //        }
                //    }
                //});
                $(tradingWindowClosureMail).modal();
            }
        }
    }
}
function fnMailTradingWindow() {
    var emailSubject = $("#txtSubject").val();
    var emailMsg = $('#summernote_1').summernote('code');
    emailMsg = '<span style="font-weight: normal !important;font-family:Trebuchet MS !important;">' + emailMsg + '</span>';
    var tradingWindowId = $('#txtTWCId').val();
    var isSelectAll = false;
    var tempUserSelected = [];
    var tmp = [];
    var usrSelected = "";
    var sUsers = "";

    $('.cbCheck:checkbox:checked').each(function () {
        if ($(this).is(':checked')) {
            var checked = ($(this).val());
            sUsers += checked + ";";
            tmp.push(checked);
        }
    });

    if (tmp.length > 0) {
        var user = tempUserSelected;
        var formData = new FormData();
        formData.append("TradingWindowId", tradingWindowId);
        formData.append("EmailSubject", emailSubject);
        formData.append("EmailTemplate", emailMsg);
        formData.append("Users", usrSelected);
        formData.append("mailTo", sUsers);

        $("input[id*=hdnTradingWindowId]").val(tradingWindowId);
        $("input[id*=hdnEmailSubject]").val(emailSubject);
        $("input[id*=hdnEmailTemplate]").val(emailMsg);
        $("input[id*=hdnUsers]").val(usrSelected);
        $("input[id*=hdnMailTo]").val(tmp);

        var arExtns = ['pdf'];
        var ctrl = $('#ContentPlaceHolder1_txtAttachment');
        var file = $('#ContentPlaceHolder1_txtAttachment').val();

        if (file != "" && file != null) {
            var ext = file.split(".");
            ext = ext[ext.length - 1].toLowerCase();
            if (arExtns.lastIndexOf(ext) == -1) {
                alert("Please select a file with extension(s).\n" + arExtns.join(', '));
                ctrl.value = '';
                return false;
            }
        }

        $("#tradingWindowClosureMail").hide();
        $("#modalUserSelection").hide();
        return true;
    }
    else {
        alert("Please select user");
        return false;
    }
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

                    result += '<td>' + FormatDate(msg.InsiderTradingWindowList[i].fromDate, $("input[id*=hdnDateFormat]").val()) + '</td>';
                    result += '<td>' + FormatDate(msg.InsiderTradingWindowList[i].toDate, $("input[id*=hdnDateFormat]").val()) + '</td>';
                    result += '<td>' + msg.InsiderTradingWindowList[i].remarks + '</td>';
                    result += '<td>';
                    result += '<div class="btn-group">';
                    result += '<a class="btn blue dropdown-toggle" href="javascript:;" data-toggle="dropdown">';
                    result += '<i class="fa fa-user"></i> Actions';
                    result += '<i class="fa fa-angle-down"></i>';
                    result += '</a>';
                    result += '<ul class="dropdown-menu pull-right">';
                    result += '<li>';
                    result += '<a data-target="#tradingWindowClosureModel" data-toggle="modal" class="btn btn-outline dark" onclick="javascript:fnEditTradingWindow(\'' + msg.InsiderTradingWindowList[i].id + '\',\'' + FormatDate(msg.InsiderTradingWindowList[i].fromDate, $("input[id*=hdnDateFormat]").val()) + '\',\'' + FormatDate(msg.InsiderTradingWindowList[i].toDate, $("input[id*=hdnDateFormat]").val()) + '\',\'' + msg.InsiderTradingWindowList[i].boardMeetingScheduledOn + '\',\'' + msg.InsiderTradingWindowList[i].quarterEndedOn + '\',\'' + msg.InsiderTradingWindowList[i].windowClosureTypeId + '\',this);">Edit</a>';
                    result += '</li>';
                    result += '<li>';
                    result += '<a class="btn btn-outline dark" onclick="javascript:fnSendMailTradingWindow(\'' + msg.InsiderTradingWindowList[i].id + '\');">View Window Closure Email</a>';
                    result += '</li>';
                    result += '</ul>';
                    result += '</div>';
                    result += '</td>';
                    result += '</tr>';
                }
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
function validateEmail(value) {
    var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
    if (reg.test(value) == false) {
        return false;
    }
    return true;
}
function fnChkStatus() {
    var start = $("input[id*=hdnEmailTask]").val();
    if (start == "Start") {
        $("#LoaderProgerss").show();
        intervalListener = window.setInterval(function () {
            if (!downloadComplete) {
                CallCheckEmailStatus();
            }
        }, 2000);
    }
}
function CallCheckEmailStatus() {
    $.ajax({
        type: "POST",
        url: "TradingWindowClosure.aspx/CheckDownload",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            updateStatus('completed', r.d);
            if (r.d.indexOf('All emails sent') > -1) {
                downloadComplete = true;
            }
        },
        error: function (r) {
            console.log('Check error : ' + r);
        },
        failure: function (r) {
            console.log('Check failure : ' + r);
        }
    });
    if (downloadComplete) {
        window.clearInterval(intervalListener);
        $("input[id*=hdnEmailTask]").val('');
        $("#LoaderProgerss").hide();
    }
}
function updateStatus(status, msg) {
    document.getElementById('lblMsg').innerHTML = msg;
    if (msg.indexOf('All emails sent') > -1) {
        downloadComplete = true;
        window.clearInterval(intervalListener);
        $("input[id*=hdnEmailTask]").val('');
        $("#LoaderProgerss").hide();
        alert("Trading window notification sent successfully");
    }
}
function fnFileFormatIssue() {
    alert("Please upload only pdf format");
    $("#tradingWindowClosureMail").show();
    $("#modalUserSelection").show();
}