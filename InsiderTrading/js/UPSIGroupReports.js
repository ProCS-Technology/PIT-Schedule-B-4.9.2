var GroupUserRemarks = new Array();
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
    GetAllUPSIGroup();
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
    var gid = $("#bindGroup").val();
    if (gid == undefined || gid == null || gid == "") {
        gid = 0;
    }
    var groupid = parseInt(gid);
    var userDate = $("#txtFromDate").val();
    var from = userDate.split("/");
    //var f = new Date(from[2], from[1], from[0]);
    //var fr_date = f.getFullYear() + "-" + f.getMonth() + "-" + f.getDate();
    var fr_date = from.reverse().join("-");

    var from_date = fr_date;

   

    
    var userDate = $("#txtToDate").val();
    var from = userDate.split("/");
    //var f = new Date(from[2], from[1], from[0]);
    //var fr_date = f.getFullYear() + "-" + f.getMonth() + "-" + f.getDate();
    var fr_date = from.reverse().join("-");

    var till_date = fr_date;
    
    var webUrl = uri + "/api/UPSIGroupReport/GetUPSIReport";
    $.ajax({
        url: webUrl,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        data: JSON.stringify({
            VALID_FROM: from_date, VALID_TLL: till_date, GROUP_ID: groupid
        }),
        success: function (msg) {
           
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl) {
                var result = "";
                for (var i = 0; i < msg.UPSIMembersGroupList.length; i++) {
                    result += '<tr>';
                    result += '<td>' + msg.UPSIMembersGroupList[i].GROUP_NM + '</td>';
                    result += '<td>' + msg.UPSIMembersGroupList[i].TotalMembers + '</td>';
                    result += '<td>' + msg.UPSIMembersGroupList[i].VALID_FROM + '</td>';
                    result += '<td>' + msg.UPSIMembersGroupList[i].VALID_TLL + '</td>';
                    
                    result += '<td>' + msg.UPSIMembersGroupList[i].CreatedBy + '</td>';
                    result += '<td id="tdEditDelete_' + msg.UPSIMembersGroupList[i].GROUP_ID + '"><a id="a_' + msg.UPSIMembersGroupList[i].GROUP_ID + '" class="btn btn-outline dark" onclick=\"javascript:fnHistoryUPSIGroup(' + msg.UPSIMembersGroupList[i].GROUP_ID + ');\">History</a><a style="margin-left:20px" data-target="#delete" data-toggle="modal" id="d_' + msg.UPSIMembersGroupList[i].GROUP_ID + '" class="btn btn-outline dark" onclick=\"javascript:fnHistoryUPSIGroupRemarks(' + msg.UPSIMembersGroupList[i].GROUP_ID + ');\">Remarks</a></td>';

                    result += '</tr>';
                   // upsiCommunicationList.push(msg.lstUPSIReport[i]);
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
    //if ($("#bindUser").val() == '' || $("#bindUser").val() == undefined || $("#bindUser").val() == null) {
    //    //alert("Please select the user");
    //    //return false;
    //}
    //else
        if ($("#txtFromDate").val() == '' || $("#txtFromDate").val() == undefined || $("#txtFromDate").val() == null) {
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

function fnHistoryUPSIGroup(Gpid) {

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
                $("#group_name").html('');
                $("#group_name").html(msg.UPSIMembersGroupList[0].GROUP_NM);

                $("#txtgroupDetail").html('');
                $("#txtgroupDetail").html(msg.UPSIMembersGroupList[0].GROUP_DESC);
                $("#txtvalid_from").html('');
                $("#txtvalid_from").html(msg.UPSIMembersGroupList[0].VALID_FROM);
                $("#txtvalid_till").html('');
                $("#txtvalid_till").html(msg.UPSIMembersGroupList[0].VALID_TLL);


                var result = "";
                for (var i = 0; i < msg.UPSIMembersGroupList.length; i++) {
                    var seq = i + 1;
                    result += '<tr id="tr_' + msg.UPSIMembersGroupList[i].GROUP_ID + '">';
                    result += '<td> <b>Version: ' + msg.UPSIMembersGroupList[i].VERSION + '</b></td>';
                    result += '<td> <b>Created BY: ' + msg.UPSIMembersGroupList[i].CreatedBy + '</b></td>';
                    result += '<td> <b>Created On: ' + msg.UPSIMembersGroupList[i].CreatedOn + '</b></td>';
                    result += '</tr>';
                    for (var j = 0; j < msg.UPSIMembersGroupList[i].listNonDesignatedMember.length; j++) {
                        //userlistforcommittee.push(msg.upsi.upsi[i]);
                        var seq = j + 1;
                        result += '<tr id="tr_' + seq + '">';
                     
                        result += '<td>' + seq + '</td>';
                        // result += '<td>' + msg.upsilist[i].listuser[j].USER_NM + '</td>';
                        result += '<td>' + msg.UPSIMembersGroupList[i].listNonDesignatedMember[j].NAME + '(' + msg.UPSIMembersGroupList[i].listNonDesignatedMember[j].EMAIL + ')</td>';
                        result += '<td>' + msg.UPSIMembersGroupList[i].listNonDesignatedMember[j].MEMBER_TYPE + '</td>';
                        result += '</tr>';
                    }

                }

                var table = $('#adduser_prev').DataTable();
                table.destroy();
                $("#tbody_prev").html(result);

                


                $("#UPSI_history").modal('show');


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
                    result += '<td><a id="au_' + msg.UPSIMembersGroupList[0].listGroupUserRemarks[i].HdrId + '" class="btn btn-outline dark" onclick=\"javascript:fnHistoryUPSIGroupTO(' + msg.UPSIMembersGroupList[0].listGroupUserRemarks[i].HdrId +');\">Click</a></td>';
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

        if(GroupUserRemarks[i].HdrId == hrid)
        {
            $("#bdMessage").html(GroupUserRemarks[i].msgBody);

            
        }

    }
    $("#messageBody").modal('show');
    
  
}

function fnHistoryUPSIGroupTO(hdrid)
{

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
       
        if (GroupUserRemarks[i].HdrId == hdrid && GroupUserRemarks[i].listRemarksAttachments.length>0) {
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