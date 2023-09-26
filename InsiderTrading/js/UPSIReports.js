var upsiCommunicationList = new Array();
$(document).ready(function () {

    window.history.forward();
    function preventBack() { window.history.forward(1); }
    $("#Loader").hide();
    fnBindUserList();
    var table = $('#tbl-UPSIReport-setup').DataTable();
    table.destroy();
    $("#tbdUPSIReportList").html('');
    initializeDataTable();
})

function isJson(str) {
    try {
        JSON.parse(str);
    } catch (e) {
        return false;
    }
    return true;
}

function initializeDataTable() {
    $('#tbl-UPSIReport-setup').DataTable({
        dom: 'Bfrtip',
        pageLength: 10,
        "scrollY": "300px",
        "scrollX": true,
        "aaSorting": [[0, "desc"]],
        buttons: [
            {
                extend: 'pdf',
                className: 'btn green btn-outline',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4]
                }
            },
            {
                extend: 'excel',
                className: 'btn yellow btn-outline ',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4]
                }
            },

            //{ extend: 'colvis', className: 'btn dark btn-outline', text: 'Columns' }
        ]
    });
}

function fnGetUPSIReportOnRun() {
    if (fnValidateUPSIReport()) {
        fnGetUPSIReport();
    }
}

function fnGetUPSIReport() {
    $("#Loader").show();
    upsiCommunicationList = [];
    var webUrl = uri + "/api/ReportsIT/GetUPSIReport";
    $.ajax({
        url: webUrl,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        data: JSON.stringify({
            upsiFrom: $("#txtFromDate").val(), upsiTo: $("#txtToDate").val(), upsiCommunicationFrom: $("#bindUser").val()
        }),
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl) {
                var result = "";
                for (var i = 0; i < msg.lstUPSIReport.length; i++) {
                    result += '<tr>';
                    result += '<td>' + msg.lstUPSIReport[i].subjectCreatedOn + '</td>';
                    result += '<td>' + msg.lstUPSIReport[i].subject + '</td>';
                 //   result += '<td>' + msg.lstUPSIReport[i].subjectCreatedBy + '</td>';
                 //   result += '<td>' + msg.lstUPSIReport[i].emailSentOn + '</td>';
                    result += '<td>' + msg.lstUPSIReport[i].from + '</td>';
                    result += '<td>' + msg.lstUPSIReport[i].to + '</td>';
                    result += '<td>' + msg.lstUPSIReport[i].cc + '</td>';
               //     result += '<td>' + msg.lstUPSIReport[i].bcc + '</td>';
                    result += '<td>';
                    result += '<a href="#messageBody" class="btn btn-primary" onclick="fnGetMessageBody(\'' + msg.lstUPSIReport[i].hdrId + '\',\'' + msg.lstUPSIReport[i].lineId + '\')" data-toggle="modal"><i style="color:white" class="fa fa-envelope" aria-hidden="true"></i></a>';
                    result += '</td>';
                    result += '<td>';
                    result += '<a href="#attachmentBody" class="btn btn-primary" onclick="fnGetAttachmentBody(\'' + msg.lstUPSIReport[i].hdrId + '\',\'' + msg.lstUPSIReport[i].lineId + '\')" data-toggle="modal"><i style="color:white" class="fa fa-paperclip" aria-hidden="true"></i></a>';
                    result += '</td>';
                    result += '</tr>';
                    upsiCommunicationList.push(msg.lstUPSIReport[i]);
                }

                var table = $('#tbl-UPSIReport-setup').DataTable();
                table.destroy();
                $("#tbdUPSIReportList").html(result);
                initializeDataTable();
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
            alert(response.status + ' ' + response.statusText);
        }
    })
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
    var str = "";
    if (upsiCommunicationList !== null) {
        for (var i = 0; i < upsiCommunicationList.length; i++) {
            if (upsiCommunicationList[i].hdrId == hdrId && upsiCommunicationList[i].lineId == lineId) {
                if (upsiCommunicationList[i].upsiSubLineAttachments !== null) {
                    for (var j = 0; j < upsiCommunicationList[i].upsiSubLineAttachments.length; j++) {
                        str += '<tr>';
                        str += '<td>';
                        str += '<a href="emailAttachment/' + upsiCommunicationList[i].upsiSubLineAttachments[j] + '" target="_blank">' + upsiCommunicationList[i].upsiSubLineAttachments[j] + '</a>';
                        str += '</td>';
                        str += '</tr>';
                    }
                }
            }
        }
    }
    $("#tbdAttachmentBody").html(str);
}

function fnValidateUPSIReport() {
    if ($("#bindUser").val() == '' || $("#bindUser").val() == undefined || $("#bindUser").val() == null) {
        alert("Please select the user");
        return false;
    }
    else if ($("#txtFromDate").val() == '' || $("#txtFromDate").val() == undefined || $("#txtFromDate").val() == null) {
        alert("Please select from date");
        return false;
    }
    else if ($("#txtToDate").val() == '' || $("#txtToDate").val() == undefined || $("#txtToDate").val() == null) {
        alert("Please select to date");
        return false;
    }
    return true;
}

function fnBindUserList() {
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/GetUserList";
    $.ajax({
        type: "GET",
        url: webUrl,
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        //   async: false,
        success: function (msg) {
            if (msg.StatusFl == true) {
                var result = "";
                result += '<option value="0">All</option>';
                for (var i = 0; i < msg.UserList.length; i++) {
                    result += '<option value="' + msg.UserList[i].USER_EMAIL + '">' + msg.UserList[i].USER_NM + '(' + msg.UserList[i].USER_EMAIL + ')' + '</option>';
                }
                $("#bindUser").html(result);
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