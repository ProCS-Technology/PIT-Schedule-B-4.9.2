$(document).ready(function () {
    window.history.forward();
    function preventBack() { window.history.forward(1); }
    $("#Loader").hide();
    fnGetTransactionalInfo();
})

function ConvertToDateTime(dateTime) {
    var date = dateTime.split(" ")[0];

    return (date.split("/")[1] + "/" + date.split("/")[0] + "/" + date.split("/")[2]);
}

function initializeDataTable() {
    $('#tbl-Demat-setup').DataTable({
        //  "dom": '<"top"iflp<"clear">>rt<"bottom"iflp<"clear">>',
        dom: 'Bfrtip',
        "scrollX": true,
        pageLength: 10,
        buttons: [
         {
             extend: 'pdf',
             className: 'btn green btn-outline',
             exportOptions: {
                 columns: [2, 3, 4, 5, 6, 7, 8, 9,10,11]
             }
         },
         {
             extend: 'excel',
             className: 'btn yellow btn-outline ',
             exportOptions: {
                 columns: [2, 3, 4, 5, 6, 7, 8, 9,10,11]
             }
         },
     //    { extend: 'colvis', className: 'btn dark btn-outline', text: 'Columns' }
        ]
    });
}

function fnGetTransactionalInfo() {
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/GetTransactionalInfo";
    $.ajax({
        url: webUrl,
        type: "GET",
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            if (msg.StatusFl == true) {
                $("#Loader").hide();

                /* Demat Info */
                setDematInformation(msg);

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

function setDematInformation(msg) {
    var str = "";
    for (var i = 0; i < msg.User.lstDematAccount.length; i++) {
        str += '<tr>';
        str += '<td style="display:none;">' + msg.User.lstDematAccount[i].ID + '</td>';
        str += '<td style="display:none;">' + msg.User.lstDematAccount[i].relative.ID + '</td>';
        str += '<td>' + msg.User.lstDematAccount[i].relative.relativeName + '</td>';
        str += '<td>' + msg.User.lstDematAccount[i].depositoryName + '</td>';
        str += '<td>' + msg.User.lstDematAccount[i].clientId + '</td>';
        str += '<td>' + msg.User.lstDematAccount[i].depositoryParticipantName + '</td>';
        str += '<td>' + msg.User.lstDematAccount[i].depositoryParticipantId + '</td>';
        str += '<td>' + msg.User.lstDematAccount[i].tradingMemberId + '</td>';
        str += '<td>' + msg.User.lstDematAccount[i].accountNo + '</td>';
        str += '<td>' + msg.User.lstDematAccount[i].status + '</td>';
        str += '<td>' + ConvertToDateTime(msg.User.lstDematAccount[i].lastModifiedOn) + '</td>';
       // str += '<td>' + msg.User.lstDematAccount[i].version + '</td>';
        str += '<td><a class="btn btn-default" data-toggle="modal" data-target="#modalDematVersion" onclick="fnGetTransactionalInfoByVersion(' + msg.User.lstDematAccount[i].ID + ')">Version</a></td>';
      //  str += '<td><a data-target="#modalAddDematDetail" data-toggle="modal" class="btn btn-outline dark" onclick=\"javascript:fnEditDematDetail();\">Edit</a></td>';
        str += '</tr>';
    }
    var table = $('#tbl-Demat-setup').DataTable();
    table.destroy();
    $("#tbdDematList").html(str);
    initializeDataTable();
}

function fnGetTransactionalInfoByVersion(accountId) {
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/GetTransactionalInfoByVersion";
    $.ajax({
        url: webUrl,
        type: "GET",
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            if (msg.StatusFl == true) {
                $("#Loader").hide();

                /* Personal Information */
                var str = '';
                for (var i = 0 ; i < msg.User.lstDematAccount.length; i++) {
                    if (accountId == msg.User.lstDematAccount[i].ID) {
                        str += '<div style="text-align:center;"><b><u>Version ' + msg.User.lstDematAccount[i].version + '</u></b></div><br/>';
                        str += '<table class="table table-striped table-hover table-bordered">';
                        str += '<tr>';
                        str += '<th>' + 'FOR' + '</th>';
                        str += '<td>' + msg.User.lstDematAccount[i].relative.relativeName + '</td>';
                        str += '</tr>';
                        str += '<tr>';
                        str += '<th>' + 'DEPOSITORY' + '</th>';
                        str += '<td>' + msg.User.lstDematAccount[i].depositoryName + '</td>';
                        str += '</tr>';
                        str += '<tr>';
                        str += '<th>' + 'CLIENT ID' + '</th>';
                        str += '<td>' + msg.User.lstDematAccount[i].clientId + '</td>';
                        str += '</tr>';
                        str += '<tr>';
                        str += '<th>' + 'PARTICIPANT' + '</th>';
                        str += '<td>' + msg.User.lstDematAccount[i].depositoryParticipantName + '</td>';
                        str += '</tr>';
                        str += '<tr>';
                        str += '<th>' + 'PARTICIPANT ID' + '</th>';
                        str += '<td>' + msg.User.lstDematAccount[i].depositoryParticipantId + '</td>';
                        str += '</tr>';
                        str += '<tr>';
                        str += '<th>' + 'TRADING MEMBER ID' + '</th>';
                        str += '<td>' + msg.User.lstDematAccount[i].tradingMemberId + '</td>';
                        str += '</tr>';
                        str += '<tr>';
                        str += '<th>' + 'DEMAT ' + '</th>';
                        str += '<td>' + msg.User.lstDematAccount[i].accountNo + '</td>';
                        str += '</tr>';
                        str += '<tr>';
                        str += '<th>' + 'STATUS ' + '</th>';
                        str += '<td>' + msg.User.lstDematAccount[i].status + '</td>';
                        str += '</tr>';
                        str += '<tr>';
                        str += '<th>' + 'Last Modified' + '</th>';
                        str += '<td>' + ConvertToDateTime(msg.User.lstDematAccount[i].lastModifiedOn) + '</td>';
                        str += '</tr>';
                        str += '</table><br/>';
                    }

                }

                $("#divDematVersion").html(str);
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