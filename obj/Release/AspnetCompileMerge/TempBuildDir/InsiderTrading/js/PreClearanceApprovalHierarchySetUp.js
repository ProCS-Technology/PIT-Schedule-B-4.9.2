$(document).ready(function () {
    $("#Loader").hide();
    fillUserDetails();
    $('#PreClearanceApprovalHirarchyModal').on('hide.bs.modal', function () {
        fnClearForm();
    });
    fnGetAllOfficerHierarchyOrder();
})

function initializeDataTable() {
    $('#tbl-ApprovalHierarchy-setup').DataTable({
        dom: 'Bfrtip',
        pageLength: 10,
        "scrollY": "300px",
        "scrollX": true,
        buttons: [
            {
                extend: 'pdf',
                className: 'btn green btn-outline',
                exportOptions: {
                    columns: [0, 1, 2, 3]
                }
            },
            {
                extend: 'excel',
                className: 'btn yellow btn-outline ',
                exportOptions: {
                    columns: [0, 1, 2, 3]
                }
            },

            //{ extend: 'colvis', className: 'btn dark btn-outline', text: 'Columns' }
        ]
    });
}

function fnGetAllOfficerHierarchyOrder() {
    $("#Loader").show();
    var webUrl = uri + "/api/PreClearanceApprovalHierarchy/GetAllOfficerHierarchyOrder";
    $.ajax({
        type: 'GET',
        url: webUrl,
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl) {
                var table = $('#tbl-ApprovalHierarchy-setup').DataTable();
                table.destroy();
                var result = '';
                if (msg.PreClearanceApprovalHierarchyList !== null) {
                    for (var index = 0; index < msg.PreClearanceApprovalHierarchyList.length; index++) {
                        result += '<tr id="tr_"' + msg.PreClearanceApprovalHierarchyList[index].ID + '>';
                        result += '<td>' + msg.PreClearanceApprovalHierarchyList[index].orderSequence + '</td>';
                        result += '<td>' + msg.PreClearanceApprovalHierarchyList[index].officerName + '</td>';
                        result += '<td>' + msg.PreClearanceApprovalHierarchyList[index].officerEmail + '</td>';
                        result += '<td>' + msg.PreClearanceApprovalHierarchyList[index].officerUserLogin + '</td>';
                        result += '<td>';
                        result += '<a data-target="#PreClearanceApprovalHirarchyModal" data-toggle="modal" id="a' + msg.PreClearanceApprovalHierarchyList[index].ID + '" class="btn btn-outline dark" onclick=\"javascript:fnEditHierarchy(\'' + msg.PreClearanceApprovalHierarchyList[index].officerUserLogin + '\',\'' + msg.PreClearanceApprovalHierarchyList[index].orderSequence + '\');\">Edit</a><a style="margin - left: 20px" data-target="#delete" data-toggle="modal" id="d' + msg.PreClearanceApprovalHierarchyList[index].ID + '" class="btn btn - outline dark" onclick=\"javascript:fnDeleteHierarchy(\'' + msg.PreClearanceApprovalHierarchyList[index].ID + '\');\">Delete</a>';
                        result += '</td>';
                        result += '</tr>';
                    }
                }

                $("#tbdApprovalHierarchyList").html(result);
                initializeDataTable();
            }
            else {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    alert(msg.Msg);
                }
            }
        },
        error: function (error) {
            $("#Loader").hide();
            alert(error.status + ' ' + error.statusText);
        }
    })
}

function fnSaveHierarchyOrder() {
    if (fnValidateHierarchyModal()) {
        fnAddOfficerHirarchyOrder();
    }
}

function fnAddOfficerHirarchyOrder() {
    var officerUserLogin = $("#ddlOfficerName").val();
    var orderNumber = $("#txtOrderNumber").val().trim();

    $("#Loader").show();
    var webUrl = uri + "/api/PreClearanceApprovalHierarchy/SaveOfficerHierarchyOrder";
    $.ajax({
        type: 'POST',
        url: webUrl,
        data: JSON.stringify({
            officerUserLogin: officerUserLogin, orderSequence: orderNumber
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl) {
                alert(msg.Msg);
                window.location.reload(true);
            }
            else {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    alert(msg.Msg);
                }
            }
        },
        error: function (error) {
            $("#Loader").hide();
            alert(error.status + ' ' + error.statusText);
        }
    })

}

function isJson(str) {
    try {
        JSON.parse(str);
    } catch (e) {
        return false;
    }
    return true;
}

function fnValidateHierarchyModal() {
    var count = 0;
    if ($("#ddlOfficerName").val() !== null && $("#ddlOfficerName").val() !== undefined && $("#ddlOfficerName").val() !== '' && $("#ddlOfficerName").val() !== '0') {
        $("#lblOfficerName").removeClass('required');
    }
    else {
        count++;
        $("#lblOfficerName").addClass('required');
    }
    if ($("#txtOrderNumber").val() !== null && $("#txtOrderNumber").val() !== undefined && $("#txtOrderNumber").val().trim() !== '') {
        $("#lblOrderNumber").removeClass('required');
    }
    else {
        count++;
        $("#lblOrderNumber").addClass('required');
    }

    if (count > 0) {
        return false;
    }
    else {
        return true;
    }
}

function fnRemoveClass(value, obj) {
    $("#lbl" + value).removeClass('required');
}

function fnCloseModal() {
    fnClearForm();
}

function fnClearForm() {
    $("#ddlOfficerName").val('0');
    $("#txtOrderNumber").val('');
}

function fillUserDetails() {
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
            if (msg.StatusFl) {
                var result = "";
                result += '<option value="">Please Select</option>';
                for (var i = 0; i < msg.UserList.length; i++) {
                    result += '<option value="' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].USER_NM + '(' + msg.UserList[i].USER_EMAIL + ')' + '</option>';
                }
                $("#ddlOfficerName").html(result);
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

function fnEditHierarchy(officerUserLogin, orderSequence) {
    $("#ddlOfficerName").val(officerUserLogin);
    $("#txtOrderNumber").val(orderSequence);
}

function fnDeleteHierarchy(id) {
    $("#Loader").show();
    var webUrl = uri + "/api/PreClearanceApprovalHierarchy/DeleteOfficerHierarchyOrder";
    $.ajax({
        type: 'POST',
        url: webUrl,
        data: JSON.stringify({
            ID: id
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl) {
                alert(msg.Msg);
                window.location.reload(true);
            }
            else {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    alert(msg.Msg);
                }
            }
        },
        error: function (error) {
            $("#Loader").hide();
            alert(error.status + ' ' + error.statusText);
        }
    })
}