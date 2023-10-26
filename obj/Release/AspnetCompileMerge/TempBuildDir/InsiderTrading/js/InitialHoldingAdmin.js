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

                    /* Initial Holding */
                    setInitialHoldingDetail(msg);
                }
                else {
                    var table = $('#tbl-InitialHolding-setup').DataTable();
                    table.destroy();
                    $("#tbdInitialDeclaration").html('');
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
    $('#tbl-InitialHolding-setup').DataTable({
        //  "dom": '<"top"iflp<"clear">>rt<"bottom"iflp<"clear">>',
        dom: 'Bfrtip',
        pageLength: 10,
        buttons: [
         {
             extend: 'pdf',
             className: 'btn green btn-outline',
             exportOptions: {
                 columns: [1, 3, 5, 7, 8,11,12]
             }
         },
         {
             extend: 'excel',
             className: 'btn yellow btn-outline ',
             exportOptions: {
                 columns: [1, 3, 5, 7, 8,11,12]
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

                /* Initial Holding */
                setInitialHoldingDetail(msg);

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

function setInitialHoldingDetail(msg) {
    var str = "";
    for (var i = 0; i < msg.User.lstInitialHoldingDetail.length; i++) {
        str += '<tr>';
        str += '<td style="display:none;">' + msg.User.lstInitialHoldingDetail[i].restrictedCompany.ID + '</td>';
        str += '<td>' + msg.User.lstInitialHoldingDetail[i].restrictedCompany.companyName + '</td>';
        str += '<td style="display:none;">' + msg.User.lstInitialHoldingDetail[i].securityType + '</td>';
        str += '<td>' + msg.User.lstInitialHoldingDetail[i].securityTypeName + '</td>';
        str += '<td style="display:none;">' + msg.User.lstInitialHoldingDetail[i].relative.ID + '</td>';
        str += '<td>' + msg.User.lstInitialHoldingDetail[i].relative.relativeName + '</td>';
        str += '<td style="display:none;">' + msg.User.lstInitialHoldingDetail[i].dematAccount.ID + '</td>';
        str += '<td>' + msg.User.lstInitialHoldingDetail[i].dematAccount.accountNo + '</td>';
        str += '<td style="text-align: right;">' + msg.User.lstInitialHoldingDetail[i].noOfSecurities + '</td>';
        str += '<td style="display:none;">' + msg.User.lstInitialHoldingDetail[i].relative.panNumber + '</td>';
        str += '<td style="display:none;">' + msg.User.lstInitialHoldingDetail[i].dematAccount.tradingMemberId + '</td>';
        str += '<td>' + ConvertToDateTime(msg.User.lstInitialHoldingDetail[i].lastModifiedOn) + '</td>';
        // str += '<td>' + msg.User.lstInitialHoldingDetail[i].version + '</td>';
        str += '<td><a class="btn btn-default" data-toggle="modal" data-target="#modalInitialHoldingVersion" onclick="fnGetTransactionalInfoByVersion(' + msg.User.lstInitialHoldingDetail[i].ID + ')">Version</a></td>';
     //   str += '<td><a data-target="#modalAddInitialHoldingDeclarations" data-toggle="modal" class="btn btn-outline dark" onclick=\"javascript:fnEditInitialDeclarationDetail();\">Edit</a></td>';
        str += '</tr>';
    }
    var table = $('#tbl-InitialHolding-setup').DataTable();
    table.destroy();
    $("#tbdInitialDeclaration").html(str);
    initializeDataTable();
}

function fnGetTransactionalInfoByVersion(initialHoldingId) {
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
                for (var i = 0; i < msg.User.lstInitialHoldingDetail.length; i++) {
                    if (initialHoldingId == msg.User.lstInitialHoldingDetail[i].ID) {
                        str += '<div style="text-align:center;"><b><u>Version ' + msg.User.lstInitialHoldingDetail[i].version + '</u></b></div><br/>';
                        str += '<table class="table table-striped table-hover table-bordered">';
                        str += '<tr>';
                        str += '<th>' + 'COMPANY' + '</th>';
                        str += '<td>' + msg.User.lstInitialHoldingDetail[i].restrictedCompany.companyName + '</td>';
                        str += '</tr>';
                        str += '<tr>';
                        str += '<th>' + 'SECURITY TYPE' + '</th>';
                        str += '<td>' + msg.User.lstInitialHoldingDetail[i].securityTypeName + '</td>';
                        str += '</tr>';
                        str += '<tr>';
                        str += '<th>' + 'FOR' + '</th>';
                        str += '<td>' + msg.User.lstInitialHoldingDetail[i].relative.relativeName + '</td>';
                        str += '</tr>';
                        str += '<tr>';
                        str += '<th>' + 'DEMAT ACCOUNT NO' + '</th>';
                        str += '<td>' + msg.User.lstInitialHoldingDetail[i].dematAccount.accountNo + '</td>';
                        str += '</tr>';
                        str += '<tr>';
                        str += '<th>' + 'NUMBER OF SECURITIES' + '</th>';
                        str += '<td>' + msg.User.lstInitialHoldingDetail[i].noOfSecurities + '</td>';
                        str += '</tr>';
                        str += '<tr>';
                        str += '<th>' + 'Last Modified' + '</th>';
                        str += '<td>' + ConvertToDateTime(msg.User.lstInitialHoldingDetail[i].lastModifiedOn) + '</td>';
                        str += '</tr>';
                        str += '</table><br/>';
                    }

                }

                $("#divInitialHoldingVersion").html(str);
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