$(document).ready(function () {
    window.history.forward();
    function preventBack() { window.history.forward(1); }
    $("#Loader").hide();
    fnBindBusinessUnitList();
    fnBindUserList();
    fnGetTransactionalInfo();
    $("#userLoginId").val('');

    $("#bindBusinessUnit").select2({
        placeholder: "Select a company"
    });

    $("#bindUser").select2({
        placeholder: "Select a user"
    });

    $("#bindBusinessUnit").on('change', function () {
        fnBindUserList();
    });
})

function ConvertToDateTime(dateTime) {
    var date = dateTime.split(" ")[0];

    return (date.split("/")[1] + "/" + date.split("/")[0] + "/" + date.split("/")[2]);
}

function isJson(str) {
    try {
        JSON.parse(str);
    } catch (e) {
        return false;
    }
    return true;
}

function fnBindBusinessUnitList() {
    $("#Loader").show();
    var webUrl = uri + "/api/BusinessUnit/GetAllBusinessUnitList";
    $.ajax({
        type: "GET",
        url: webUrl,
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                var result = "";
                for (var i = 0; i < msg.BusinessUnitList.length; i++) {
                    result += '<option value="' + msg.BusinessUnitList[i].businessUnitId + '">' + msg.BusinessUnitList[i].businessUnitName + '</option>';
                }

                $("#bindBusinessUnit").append(result);
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

function fnBindUserList() {
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/GetUserListByBusinessUnitId";
    $.ajax({
        type: "POST",
        url: webUrl,
        data: JSON.stringify({ businessUnit: { businessUnitId: $("#bindBusinessUnit").val() } }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        //   async: false,
        success: function (msg) {
            if (msg.StatusFl == true) {
                var result = "";
                //result += '<option value="0">All</option>';
                result += '<option value="">Please Select</option>';
                for (var i = 0; i < msg.UserList.length; i++) {
                    result += '<option value="' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].USER_NM + '(' + msg.UserList[i].USER_EMAIL + ')' + '</option>';
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
                    $("#bindUser").html('');
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

function fnGetMyDetailsReport() {
    if (fnValidate()) {
        $("#userLoginId").val($("#bindUser").val());
        $("#Loader").show();
        var webUrl = uri + "/api/UserIT/GetMyDetailReport";
        $.ajax({
            type: "POST",
            url: webUrl,
            data: JSON.stringify({ LOGIN_ID: $("#bindUser").val() }),
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            success: function (msg) {
                var result = "";
                if (msg.StatusFl == true) {
                    $("#Loader").hide();

                    /* Demat Info */
                    setDematInformation(msg);
                }
                else {
                    var table = $('#tbl-Demat-setup').DataTable();
                    table.destroy();
                    $("#tbdDematList").html('');
                    initializeDataTable();
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
}

function fnValidate() {
    if ($("#bindUser").val() == '' || $("#bindUser").val() == undefined || $("#bindUser").val() == null) {
        alert("Please select the user");
        return false;
    }
    return true;
}

function initializeDataTable() {
    $('#tbl-Demat-setup').DataTable({
        //  "dom": '<"top"iflp<"clear">>rt<"bottom"iflp<"clear">>',
        dom: 'Bfrtip',
        pageLength: 10,
        "scrollX": true,
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
    var webUrl = uri + "/api/UserIT/GetTransactionalInfoByVersionByAdmin";
    $.ajax({
        url: webUrl,
        type: "POST",
        data: JSON.stringify({ LOGIN_ID: $("#userLoginId").val() }),
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