$(document).ready(function () {
    window.history.forward();
    function preventBack() { window.history.forward(1); }
    $("#Loader").hide();
    fnBindBusinessUnitList();
    fnBindUserList();
    fnGetTransactionalInfo();

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
        // $("#userLoginId").val($("#bindUser").val());
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

                    /* Final Declaration Info */
                    setFinalDeclartion(msg);
                }
                else {
                    var table = $('#tbl-FinalDeclaration-setup').DataTable();
                    table.destroy();
                    $("#tbdFinalDeclaration").html('');
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
    $('#tbl-FinalDeclaration-setup').DataTable({
        //  "dom": '<"top"iflp<"clear">>rt<"bottom"iflp<"clear">>',
        dom: 'Bfrtip',
        pageLength: 10,
        buttons: [
            {
                extend: 'pdf',
                className: 'btn green btn-outline',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6]
                }
            },
            {
                extend: 'excel',
                className: 'btn yellow btn-outline ',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 5, 6]
                }
            },
            //    { extend: 'colvis', className: 'btn dark btn-outline', text: 'Columns' }
        ]
    });
}

function fnGetTransactionalInfo() {
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/GetDeclarationForms";
    $.ajax({
        url: webUrl,
        type: "POST",

        data: JSON.stringify({
            LOGIN_ID: $("#bindUser").val()
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            if (msg.StatusFl == true) {
                $("#Loader").hide();

                /* Final Declaration Info */
                setFinalDeclartion(msg);

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

function setFinalDeclartion(msg) {
    var str = "";
    if (msg.User.lstFinalDeclaration != null) {
        for (var i = 0; i < msg.User.lstFinalDeclaration.length; i++) {
            str += '<tr>';
            str += '<td>' + ConvertToDateTime(msg.User.lstFinalDeclaration[i].createdOn) + " " + msg.User.lstFinalDeclaration[i].createdOn.split(" ")[1] + '</td>';
            str += '<td>' + msg.User.lstFinalDeclaration[i].createdBy + '</td>';
            str += '<td><a href="../assets/logos/Policy/' + msg.User.lstFinalDeclaration[i].fileName + '" target="_blank">Policy</a></td>';
            str += '<td>' + msg.User.lstFinalDeclaration[i].PolicyVersion + '</td>';
            if (msg.User.lstFinalDeclaration[i].fileFormEOrF != "") {
                str += '<td><a href="emailAttachment/' + msg.User.lstFinalDeclaration[i].fileFormEOrF + '" target="_blank">' + msg.User.lstFinalDeclaration[i].fileFormB + '</a></td>';
            }
            else {
                str += '<td></td>';
            }
            str += '</tr>';
        }
    }

    var table = $('#tbl-FinalDeclaration-setup').DataTable();
    table.destroy();
    $("#tbdFinalDeclaration").html(str);
    initializeDataTable();
}

function ConvertToDateTime(dateTime) {
    var date = dateTime.split(" ")[0];

    return (date.split("/")[1] + "/" + date.split("/")[0] + "/" + date.split("/")[2]);
}