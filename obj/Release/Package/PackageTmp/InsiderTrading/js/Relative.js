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
    $('#tbl-Relative-setup').DataTable({
        //  "dom": '<"top"iflp<"clear">>rt<"bottom"iflp<"clear">>',
        dom: 'Bfrtip',
        pageLength: 10,
        "scrollX": true,
        buttons: [
         {
             extend: 'pdf',
             className: 'btn green btn-outline',
             exportOptions: {
                 columns: [1, 3, 4, 5, 6, 7, 8, 9, 10, 11,12]
             }
         },
         {
             extend: 'excel',
             className: 'btn yellow btn-outline ',
             exportOptions: {
                 columns: [1, 3, 4, 5, 6, 7, 8, 9, 10, 11,12]
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

                /* Relative Info */
                setRelativeInformation(msg);

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

function setRelativeInformation(msg) {
    var str = "";
    for (var i = 0 ; i < msg.User.lstRelative.length; i++) {
        str += '<tr>';
        str += '<td style="display:none;">' + msg.User.lstRelative[i].ID + '</td>';
        str += '<td>' + msg.User.lstRelative[i].relativeName + '</td>';
        str += '<td style="display:none;">' + msg.User.lstRelative[i].relation.RELATION_ID + '</td>';
        str += '<td>' + msg.User.lstRelative[i].relation.RELATION_NM + '</td>';
        str += '<td>' + msg.User.lstRelative[i].relativeEmail + '</td>';
        str += '<td>' + msg.User.lstRelative[i].panNumber + '</td>';
        str += '<td>' + msg.User.lstRelative[i].identificationType + '</td>';
        str += '<td>' + msg.User.lstRelative[i].identificationNumber + '</td>';
        str += '<td>' + msg.User.lstRelative[i].address + '</td>';
        str += '<td>' + msg.User.lstRelative[i].status + '</td>';
        str += '<td>' + msg.User.lstRelative[i].remarks + '</td>';
        str += '<td>' + ConvertToDateTime(msg.User.lstRelative[i].lastModifiedOn) + '</td>';
        //  str += '<td>' + msg.User.lstRelative[i].version + '</td>';
        str += '<td><a class="btn btn-default" data-toggle="modal" data-target="#modalRelativeVersion" onclick="fnGetTransactionalInfoByVersion(' + msg.User.lstRelative[i].ID + ')">Version</a></td>';
        // str += '<td><a data-target="#modalAddRelativeDetail" data-toggle="modal" class="btn btn-outline dark" onclick=\"javascript:fnEditRelativeDetail();\">Edit</a></td>';
        str += '</tr>';
    }
    var table = $('#tbl-Relative-setup').DataTable();
    table.destroy();
    $("#tbdRelativeList").html(str);
    initializeDataTable();
}

function fnGetTransactionalInfoByVersion(relativeId) {
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
                for (var i = 0 ; i < msg.User.lstRelative.length; i++) {
                    if (relativeId == msg.User.lstRelative[i].ID) {
                        str += '<div style="text-align:center;"><b><u>Version ' + msg.User.lstRelative[i].version + '</u></b></div><br/>';
                        str += '<table class="table table-striped table-hover table-bordered">';
                        str += '<tr>';
                        str += '<th>' + 'RELATIVE' + '</th>';
                        str += '<td>' + msg.User.lstRelative[i].relativeName + '</td>';
                        str += '</tr>';
                        str += '<tr>';
                        str += '<th>' + 'RELATION' + '</th>';
                        str += '<td>' + msg.User.lstRelative[i].relation.RELATION_NM + '</td>';
                        str += '</tr>';
                        str += '<tr>';
                        str += '<th>' + 'Email' + '</th>';
                        str += '<td>' + msg.User.lstRelative[i].relativeEmail + '</td>';
                        str += '</tr>';
                        str += '<tr>';
                        str += '<th>' + 'PAN' + '</th>';
                        str += '<td>' + msg.User.lstRelative[i].panNumber + '</td>';
                        str += '</tr>';
                        str += '<tr>';
                        str += '<th>' + 'IDENTIFICATION' + '</th>';
                        str += '<td>' + msg.User.lstRelative[i].identificationType + '</td>';
                        str += '</tr>';
                        str += '<tr>';
                        str += '<th>' + 'ID #' + '</th>';
                        str += '<td>' + msg.User.lstRelative[i].identificationNumber + '</td>';
                        str += '</tr>';
                        str += '<tr>';
                        str += '<th>' + 'ADDRESS' + '</th>';
                        str += '<td>' + msg.User.lstRelative[i].address + '</td>';
                        str += '</tr>';
                        str += '<tr>';
                        str += '<th>' + 'STATUS ' + '</th>';
                        str += '<td>' + msg.User.lstRelative[i].status + '</td>';
                        str += '</tr>';
                        str += '<tr>';
                        str += '<th>' + 'REMARKS ' + '</th>';
                        str += '<td>' + msg.User.lstRelative[i].remarks + '</td>';
                        str += '</tr>';
                        str += '<tr>';
                        str += '<th>' + 'Last Modified' + '</th>';
                        str += '<td>' + ConvertToDateTime(msg.User.lstRelative[i].lastModifiedOn) + '</td>';
                        str += '</tr>';
                        str += '</table><br/>';
                    }

                }

                $("#divRelativeVersion").html(str);
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