var GroupUserRemarks = new Array();
var upsiCommunicationList = new Array();
var GroupUserRemarksX = new Array();
$(document).ready(function () {
    window.history.forward();
    function preventBack() {
        window.history.forward(1);
    }
    //$("#Loader").hide();
    //var table = $('#tbl-UPSIReport-setup').DataTable();
    //table.destroy();
    //$("#tbdUPSIReportList").html('');
    //initializeDataTable('tbl-UPSIReport-setup', [0, 1, 2, 3, 4, 5, 6, 7]);
})
function isJson(str) {
    try {
        JSON.parse(str);
    } catch (e) {
        return false;
    }
    return true;
}
function GetAllUPSIGroup() {

    $("#Loader").show();
    var webUrl = uri + "/api/UPSIMembersGreoup/GetUPSIGroupList";
    $.ajax({
        url: webUrl,
        type: "POST",
        async: false,
        data: JSON.stringify({
            GROUP_ID: "0"

        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {


            $("#Loader").hide();
            if (msg.Msg == "SessionExpired") {
                alert("Your session is expired. Please login again to continue");
                window.location.href = "../LogOut.aspx";
            }

            if (msg.StatusFl == true) {

                $("#Loader").hide();
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }

                if (msg.StatusFl == true) {
                    var result = '<option value="0" selected>Select</option>';
                    for (var i = 0; i < msg.UPSIMembersGroupList.length; i++) {
                        result += '<option value="' + msg.UPSIMembersGroupList[i].GROUP_ID + '">' + msg.UPSIMembersGroupList[i].GROUP_NM + '</option>';

                    }


                    $("#bindGroup").html('');
                    $("#bindGroup").html(result);



                }
                else {
                    //alert(msg.Msg);
                }
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
}


function initializeDataTable(id, columns) {
    //alert("In function initializeDataTable");
    $('#tbl-UPSIReport-setup').DataTable({
        dom: 'Bfrtip',
        pageLength: 16,
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
                    columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16]
                }
            },
            {
                extend: 'excel',
                title: 'UPSI Report - ' + $("select[id*=ddlUPSIGrp] option:selected").text(),
                className: 'btn yellow btn-outline ',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16],
                    format: {
                        body: function (data, column, row, node) {
                            return column === 4 ? "\u200C" + data : data;
                            
                        }
                    }
                }
            },
            //    { extend: 'colvis', className: 'btn dark btn-outline', text: 'Columns' }
        ]
    });
}
function fnGetUPSIReportOnRun() {
    if (fnValidateUPSIReport()) {
        fnGetUPSIReport();
    }
}

function fnGetUPSIVisibility() {
    $.ajax({
        cache: false,
        url: '@Url.Action("fileInDb")',
        data: { 'empId': someVar },
        type: 'POST',
        success: function (response) {
            if (response.Data.FileExists === true) {
                // do something
            } else {
                // it was false
            }
        },
        error: function (er) {
            alert('Error!' + er);
        }
    });}

function fnGetUPSIReport() {
    //$("#Loader").show();
    //upsiCommunicationList = [];

    var GrpId = $("select[id*=ddlUPSIGrp]").val();
    var UserId = $("select[id*=ddlUsers]").val();
    var fromDt = $("#txtFromDate").val();
    var toDt = $("#txtToDate").val();

    var from = fromDt.split("/");
    var frm_date = from.reverse().join("-");
    var from_date = frm_date;
   
    var to = toDt.split("/");
    var to_date = to.reverse().join("-");
    var till_date = to_date;

    var test = new FormData();
    test.append("UPSIGrpId", GrpId);
    test.append("UserId", UserId);
    test.append("from_date", from_date);
    test.append("to_date", till_date);
    var token = $("#TokenKey").val();
    var webUrl = uri + "/api/UPSIGroup/GetUPSIReport";
    $.ajax({
        url: webUrl,
        type: "POST",
        headers: {
            'TokenKeyH': token,
        },
        contentType: false,
        processData: false,
        data: test,
        async: true,
        success: function (msg) {
            $("#Loader").hide();
            if (msg.StatusFl) {
                if (msg.Msg == "Success") {
                    var result = "";
                    GroupUserRemarksX = new Array();
                    var iCntr = 1;
                    for (var i = 0; i < msg.lstRpt.length; i++) {
                        result += '<tr>';
                        result += '<td>' + iCntr + '</td>';
                        result += '<td>' + msg.lstRpt[i].SharedBy + '</td>';
                        result += '<td>' + msg.lstRpt[i].SharedByIdentification + '</td>';
                       

                        result += '<td>' + msg.lstRpt[i].SharedWith + '</td>';
                        result += '<td>' + msg.lstRpt[i].EmailIDofSender + '</td>';
                        result += '<td>' + msg.lstRpt[i].FirmNm + '</td>';
                        result += '<td>' + msg.lstRpt[i].SharedWithIdentification + '</td>';

                        result += '<td>' + msg.lstRpt[i].SharedOn + '</td>';
                        result += '<td>' + msg.lstRpt[i].SharedTime + '</td>';
                        result += '<td>' + msg.lstRpt[i].UpsiTyp + '</td>';
                        result += '<td>' + msg.lstRpt[i].CommMode + '</td>';
                        result += '<td>' + msg.lstRpt[i].CreatedOn + '</td>';
                        result += '<td>' + msg.lstRpt[i].CreatedTm + '</td>';
                        result += '<td>' + msg.lstRpt[i].Remarks + '</td>';
                        result += '<td>' + msg.lstRpt[i].DateofentryinSDD + '</td>';
                        result += '<td>' + msg.lstRpt[i].TimestampforB + '</td>';
                        result += '<td>' + msg.lstRpt[i].UPSIReportedthrough + '</td>';
                        result += '<td>' + msg.lstRpt[i].Attachment + '</td>';
                        //if ((showupsitoco.text.toUpperCase = 'YES') ||( ManagedByCo.text.toUpperCase = 'YES'))
                        //{

                            
                        //}
                        
                        
                        iCntr++;
                        result += '</tr>';
                    }

                    var table = $('#tbl-UPSIReport-setup').DataTable();
                    table.destroy();
                    $("#tbdUPSIReportList").html(result);
                    initializeDataTable();
                    $("#Loader").hide();
                   
                }
                else {
                    alert(msg.Msg);
                    var table = $('#tbl-UPSIReport-setup').DataTable();
                    table.destroy();
                    $("#tbdUPSIReportList").html("");
                    initializeDataTable();
                    $("#Loader").hide();
                   
                }
            }
            else {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    var table = $('#tbl-UPSIReport-setup').DataTable();
                    table.destroy();
                    $("#tbdUPSIReportList").html('');
                    initializeDataTable();
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
    if ($("select[id*=ddlUPSIGrp]").val() == '' || $("select[id*=ddlUPSIGrp]") == undefined || $("select[id*=ddlUPSIGrp]") == null) {
        alert("Please select the UPSI Type/Group");
        return false;
    }
    if ($("select[id*=ddlUsers]").val() == '' || $("select[id*=ddlUsers]") == undefined || $("select[id*=ddlUsers]") == null) {
        alert("Please select the Shared By User");
        return false;
    }
    if ($("#txtFromDate").val() == '' || $("#txtFromDate").val() == undefined || $("#txtFromDate").val() == null) {
        alert("Please select from date");
        return false;
    }
    else if ($("#txtToDate").val() == '' || $("#txtToDate").val() == undefined || $("#txtToDate").val() == null) {
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
            else {
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

    //$("#EditCommittee").hide();
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

    //$("#UPSI_Remarks").modal('hide');
    $("#UPSI_history").modal('hide');

}
function fnGetUPSIRemarks(upsiRemarks) {

    $("#dvupsiremarks").html("");
    $("#dvupsiremarks").html(GroupUserRemarksX[upsiRemarks]);
}
function convertToDateTime(date) {
    return date.split("/")[1] + "/" + date.split("/")[0] + "/" + date.split("/")[2];
}