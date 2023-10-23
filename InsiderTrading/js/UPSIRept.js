$('input[id*=txtFromDate]').datepicker({
    todayHighlight: true,
    autoclose: true,
    format: $("input[id*=hdnJSDateFormat]").val(),
    clearBtn: true
});
$('input[id*=txtToDate]').datepicker({
    todayHighlight: true,
    autoclose: true,
    format: $("input[id*=hdnJSDateFormat]").val(),
    clearBtn: true
});

var table = $('#tbl-UPSIReport-setup').DataTable();
table.destroy();
var result = $("#ContentPlaceHolder1_txtReport").val();
initializeDataTable('tbl-UPSIReport-setup', [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14]);

$(document).ready(function () {
    window.history.forward();
    function preventBack() {
        window.history.forward(1);
    }
})
function initializeDataTable(id, columns) {
    $('#' + id).DataTable({
        dom: 'Bfrtip',
        pageLength: 10,
        "scrollY": "400px",
        "scrollX": true,
        buttons: [
            {
                extend: 'pdf',
                orientation: 'landscape',
                pageSize: 'TABLOID',
                title: 'UPSI Report',
                className: 'btn green btn-outline',
                exportOptions: {
                    columns: columns
                }
            },
            {
                extend: 'excel',
                title: 'UPSI Report - ' + $("select[id*=ddlUPSIGrp] option:selected").text(),
                className: 'btn yellow btn-outline ',
                exportOptions: {
                    columns: columns,
                    format: {
                        body: function (data, column, row, node) {
                            return column === 4 ? "\u200C" + data : data;
                        }
                    }
                }
            },
        ]
    });
}
function fnGetMessageBody(hdrId, lineId) {
    var msg = "";
    if (upsiCommunicationList !== null) {
        for (var i = 0; i < upsiCommunicationList.length; i++) {
            if (upsiCommunicationList[i].hdrId == hdrId && upsiCommunicationList[i].lineId == lineId) {
                msg = upsiCommunicationList[i].body;
                break;
            }
        }
    }
    $("#bdMessage").html(msg);
}
function fnGetAttachmentBody(hdrId, lineId) {
    var sMsg = $("#txtMsg_" + hdrId + "_" + lineId).html();
    var sAttachmentLnk = $("#txtAttachmentLnk_" + hdrId).val();

    if (sAttachmentLnk != "" && sAttachmentLnk != null) {
        sMsg += "<br />" + sAttachmentLnk;
    }
    $("#bdMessage").html(sMsg);
}
function fnValidateUPSIReport() {
    if ($("select[id*=ddlUPSIGrp]").val() == '' || $("select[id*=ddlUPSIGrp]") == undefined || $("select[id*=ddlUPSIGrp]") == null) {
        alert("Please select the UPSI Type/Group");
        return false;
    }
    if ($("select[id*=ddlUsers]").val() == '' || $("select[id*=ddlUsers]") == undefined || $("select[id*=ddlUsers]") == null) {
        alert("Please select the Shared By User");
        return false;
    }
    if ($("input[id*=txtFromDate]").val() == '' || $("input[id*=txtFromDate]").val() == undefined || $("input[id*=txtFromDate]").val() == null) {
        alert("Please select from date");
        return false;
    }
    else if ($("input[id*=txtToDate]").val() == '' || $("input[id*=txtToDate]").val() == undefined || $("input[id*=txtToDate]").val() == null) {
        alert("Please select to date");
        return false;
    }
    else {
        var FromDate = new Date(convertToDateTime($("#txtFromDate").val()));
        var Todate = new Date(convertToDateTime($("#txtToDate").val()));
        if (Todate < FromDate) {
            alert("To Date Should be greater than From Date");
            return false;
        }
    }
    return true;
}
function fnHistoryUPSIGroup(taskid) {
    $("#Loader").show();
    var webUrl = uri + "/api/DashboardIT/GetMyUPSITaskById";
    $.ajax({
        url: webUrl,
        type: "POST",
        async: false,
        data: {
            TaskId: taskid
        },
        success: function (msg) {
            $("#Loader").hide();
            if (msg.Msg == "SessionExpired") {
                alert("Your session is expired. Please login again to continue");
                window.location.href = "../LogOut.aspx";
            }
            if (msg.StatusFl == true) {
                $("#dvUPSITaskMSGBody").html(msg.Dashboard.listUPSITask[0].TaskMailBody);
                $("#dvUPSITaskMSGFrom").html(msg.Dashboard.listUPSITask[0].EmailFrom);
                $("#dvUPSITaskMSGTo").html(msg.Dashboard.listUPSITask[0].EmailTo);
                $("#dvUPSITaskMSGCC").html(msg.Dashboard.listUPSITask[0].EmailCC);
                $("#dvUPSITaskMsgDate").html(msg.Dashboard.listUPSITask[0].EmailDate);
                $("#dvAttechmentlistMsg").html('');
                var result = "";
                for (var i = 0; i < msg.Dashboard.listUPSITask[0].listAttachment.length; i++) {
                    result += '<p>';
                    result += '<a href="emailAttachment/' + msg.Dashboard.listUPSITask[0].listAttachment[i].Attachment + '" target="_blank">' + msg.Dashboard.listUPSITask[0].listAttachment[i].Attachment + '</a>';
                    result += '</p>';
                }
                $("#dvAttechmentlistMsg").html(result);
            }
        },
        error: function (response) {
            $("#Loader").hide();
            $("#txtUPSITaskID").val('');
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
}
function fnHistoryUPSIGroupRemarks(Gpid) {
    $('#tbody').html("");
    $('#tbody_prev').html("");
    $("#Loader").show();
    var webUrl = uri + "/api/UPSIGroupReport/HistoryUPSIGroup";
    $.ajax({
        type: "POST",
        url: webUrl,
        data: JSON.stringify({
            GROUP_ID: Gpid
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            $("#Loader").hide();
            if (msg.StatusFl) {
                $("#group_name_remarks").html('');
                $("#group_name_remarks").html(msg.UPSIMembersGroupList[0].GROUP_NM);
                GroupUserRemarks = msg.UPSIMembersGroupList[0].listGroupUserRemarks;
                var result = "";
                for (var i = 0; i < msg.UPSIMembersGroupList[0].listGroupUserRemarks.length; i++) {
                    var seq = i + 1;
                    result += '<tr id="tr_' + msg.UPSIMembersGroupList[0].listGroupUserRemarks[i].HdrId + '">';
                    result += '<td>' + msg.UPSIMembersGroupList[0].listGroupUserRemarks[i].Email + '</td>';
                    result += '<td>' + msg.UPSIMembersGroupList[0].listGroupUserRemarks[i].mailDate + '</td>';
                    result += '<td><a id="am_' + msg.UPSIMembersGroupList[0].listGroupUserRemarks[i].HdrId + '" class="btn btn-outline dark" onclick=\"javascript:fnHistoryUPSIGroupMSG(' + msg.UPSIMembersGroupList[0].listGroupUserRemarks[i].HdrId + ');\">Click</a></td>';
                    result += '<td><a id="au_' + msg.UPSIMembersGroupList[0].listGroupUserRemarks[i].HdrId + '" class="btn btn-outline dark" onclick=\"javascript:fnHistoryUPSIGroupTO(' + msg.UPSIMembersGroupList[0].listGroupUserRemarks[i].HdrId + ');\">Click</a></td>';
                    result += '<td><a id="aat_' + msg.UPSIMembersGroupList[0].listGroupUserRemarks[i].HdrId + '" class="btn btn-outline dark" onclick=\"javascript:fnHistoryUPSIGroupAttachment(' + msg.UPSIMembersGroupList[0].listGroupUserRemarks[i].HdrId + ');\">Click</a></td>';
                    result += '</tr>';
                }
                var table = $('#adduser_Remarks').DataTable();
                table.destroy();
                $("#tbody_Remarks").html(result);
                $("#UPSI_Remarks").modal('show');
            }
            else {
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
            if (response.responseText == "Session Expired") {
                alert("Your session is expired. Please login again to continue");
                $("#Loader").show();
                window.location.href = "../LogOut.aspx";
                return false;
            }
            else {
                alert(response.status + ' ' + response.statusText);
            }
        }
    });
}
function fnHistoryUPSIGroupMSG(hrid) {
    for (var i = 0; i < GroupUserRemarks.length; i++) {
        if (GroupUserRemarks[i].HdrId == hrid) {
            $("#bdMessage").html(GroupUserRemarks[i].msgBody);
        }
    }
    $("#messageBody").modal('show');
}
function fnHistoryUPSIGroupTO(hdrid) {
    var result = ""
    for (var i = 0; i < GroupUserRemarks.length; i++) {
        if (GroupUserRemarks[i].HdrId == hdrid && GroupUserRemarks[i].listUserDetail.length > 0) {
            for (var j = 0; j < GroupUserRemarks[i].listUserDetail.length; j++) {
                result += '<tr id="tr_' + GroupUserRemarks[i].listUserDetail[j].HdrId + '">';
                result += '<td>' + GroupUserRemarks[i].listUserDetail[j].EmailType + '</td>';
                result += '<td>' + GroupUserRemarks[i].listUserDetail[j].Email + '</td>';
                result += '</tr>';
            }
        }
    }
    $("#tbdmailtocc").html('');
    $("#tbdmailtocc").html(result);
    $("#MailtoCC").modal('show');
}
function fnHistoryUPSIGroupAttachment(hdrid) {
    var result = "No Attachment."
    for (var i = 0; i < GroupUserRemarks.length; i++) {
        if (GroupUserRemarks[i].HdrId == hdrid && GroupUserRemarks[i].listRemarksAttachments.length > 0) {
            result = ""
            for (var j = 0; j < GroupUserRemarks[i].listRemarksAttachments.length; j++) {
                result += '<tr id="tr_' + GroupUserRemarks[i].listRemarksAttachments[j].HdrId + '">';
                result += '<td><a href="emailAttachment/' + GroupUserRemarks[i].listRemarksAttachments[j].Attachment + '" target="_blank">' + GroupUserRemarks[i].listRemarksAttachments[j].Attachment + '</a></td>';
                result += '</tr>';
            }
        }
    }
    $("#tbdAttachmentBody").html('');
    $("#tbdAttachmentBody").html(result);
    $("#attachmentBody").modal('show');
}
function CancleHistory_model() {
    $("#UPSI_history").modal('hide');
}
function fnGetUPSIRemarks(upsiRemarks) {
    $("#dvupsiremarks").html("");
    $("#dvupsiremarks").html(GroupUserRemarksX[upsiRemarks]);
}
function convertToDateTime(date) {
    return date.split("/")[1] + "/" + date.split("/")[0] + "/" + date.split("/")[2];
}