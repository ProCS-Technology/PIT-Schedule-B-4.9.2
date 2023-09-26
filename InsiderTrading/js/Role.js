
jQuery(document).ready(function () {
    fnGetRoleList();
    $('#stack1').on('hide.bs.modal', function () {
    });

});

function initializeDataTable() {
    $('#tbl-Role-setup').DataTable({
        dom: 'Bfrtip',
        buttons: [
         {
             extend: 'pdf',
             className: 'btn green btn-outline',
             exportOptions: {
                 columns: [0]
             }
         },
         {
             extend: 'excel',
             className: 'btn yellow btn-outline ',
             exportOptions: {
                 columns: [0]
             }
         },

         //{ extend: 'colvis', className: 'btn dark btn-outline', text: 'Columns' }
        ]
    });
}

function fnGetRoleList() {
    $("#Loader").show();
    var webUrl = uri + "/api/Role/GetRoleList";
    $.ajax({
        type: "POST",
        url: webUrl,
        data: JSON.stringify({
            COMPANY_ID: 0
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: true,
        success: function (msg) {

            if (msg.StatusFl == true) {
                var result = "";
                for (var i = 0; i < msg.RoleList.length; i++) {
                    result += '<tr id="tr_' + msg.RoleList[i].ROLE_ID + '">';
                    result += '<td id="tdRoleNm_' + msg.RoleList[i].ROLE_ID + '">' + msg.RoleList[i].ROLE_NM + '</td>';
                    result += '<td id="tdEditdelete_' + msg.RoleList[i].ROLE_ID + '"><a data-target="#stack1" data-toggle="modal" id="a' + msg.RoleList[i].ROLE_ID + '" class="btn btn-outline dark" onclick=\"javascript:fnEditRole(' + msg.RoleList[i].ROLE_ID + ',\'' + msg.RoleList[i].ROLE_NM + '\');\">Edit</a><a style="margin-left:20px" data-target="#delete" data-toggle="modal" id="d' + msg.RoleList[i].ROLE_ID + '" class="btn btn-outline dark" onclick=\"javascript:Delete1Role(' + msg.RoleList[i].ROLE_ID + ');\">Delete</a></td>';

                    result += '</tr>';
                }

                var table = $('#tbl-Role-setup').DataTable();
                table.destroy();
                $("#tbdRoleList").html(result);
                initializeDataTable();
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

function fnAddRole() {
}

function fnSaveRole() {
    if (fnValidate()) {
        fnAddUpdateRole();
    }
    else {
        $('#btnSave').removeAttr("data-dismiss");
        return false;
    }
}

function fnAddUpdateRole() {
    var RoleID = $('#txtRoleKey').val();
    var RoleName = $('#txtRoleName').val();

    if (RoleID === "") {
        RoleID = 0;
    }
    $("#Loader").show();
    var webUrl = uri + "/api/Role/SaveRole";
    $.ajax({
        type: 'POST',
        url: webUrl,
        data: JSON.stringify({
            ROLE_ID: RoleID, ROLE_NM: RoleName
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            if (msg.StatusFl == false) {
                $("#Loader").hide();
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    alert(msg.Msg);
                    $('#btnSave').removeAttr("data-dismiss");
                    return false;
                }
            }
            else {
                alert(msg.Msg);
                if (msg.Role.ROLE_ID == RoleID) {
                    $("#tdRoleNm_" + msg.Role.ROLE_ID).html(msg.Role.ROLE_NM);
                    $("#a" + msg.Role.ROLE_ID).attr("onclick", "javascript:fnEditRole('" + msg.Role.ROLE_ID + "','" + msg.Role.ROLE_NM + "');");
                    $("#a" + msg.Role.ROLE_ID).attr("data-target", "#stack1");
                    $("#a" + msg.Role.ROLE_ID).attr("data-toggle", "modal");

                    $("#d" + msg.Role.ROLE_ID).attr("onclick", "javascript:Delete1Role('" + msg.Role.ROLE_ID + "');");
                    $("#d" + msg.Role.ROLE_ID).attr("data-target", "#delete");
                    $("#d" + msg.Role.ROLE_ID).attr("data-toggle", "modal");
                    $("#d" + msg.Role.ROLE_ID).css({ 'margin-left': '20px' });

                    var table = $('#tbl-Role-setup').DataTable();
                    table.destroy();
                    initializeDataTable();
                    $("#Loader").hide();
                }
                else {
                    var result = "";
                    result += '<tr id="tr_' + msg.Role.ROLE_ID + '">';
                    result += '<td id="tdRoleNm_' + msg.Role.ROLE_ID + '">' + msg.Role.ROLE_NM + '</td>';
                    result += '<td id="tdEditdelete_' + msg.Role.ROLE_ID + '"><a data-target="#stack1" data-toggle="modal" id="a' + msg.Role.ROLE_ID + '" class="btn btn-outline dark" onclick=\"javascript:fnEditRole(' + msg.Role.ROLE_ID + ',\'' + msg.Role.ROLE_NM + '\');\">Edit</a><a style="margin-left:20px" data-target="#delete" data-toggle="modal" id="d' + msg.Role.ROLE_ID + '" class="btn btn-outline dark" onclick=\"javascript:Delete1Role(' + msg.Role.ROLE_ID + ');\">Delete</a></td>';
                    result += '</tr>';
                    var table = $('#tbl-Role-setup').DataTable();
                    table.destroy();
                    $("#tbdRoleList").append(result);                  
                    initializeDataTable();
                    $("#Loader").hide();
                }

                fnClearForm();
                $('#btnSave').attr("data-dismiss", "modal");
                return true;
            }
        },
        error: function (error) {
            $("#Loader").hide();
            alert(error.status + ' ' + error.statusText);
            $('#btnSave').removeAttr("data-dismiss");
        }
    })
}

function fnValidate() {
        //alert("Here");
       var RoleName = $('#txtRoleName').val();        
       if (RoleName == '') {
           $('#lblRole').addClass('requied');
            return false;
        }
        else {
           $('#lblRole').removeClass('requied');
        }      
        return true;
    }

function fnClearForm() {
    $('#txtRoleName').val("");
    $('#txtRoleKey').val("");
}

function fnEditRole(Role_key, Role_name) {
    $('#txtRoleName').val(Role_name);
    $('#txtRoleKey').val(Role_key);
}
function Delete1Role(id) {
    $('input[id*=txtDlKey]').val(id);
    $("#deleteProduct").modal();
}

function DeleteRole() {
    var id = $('input[id*=txtDlKey]').val();
    $("#Loader").show();
    var webUrl = uri + "/api/Role/DeleteRole";
    $.ajax({
        type: 'POST',
        url: webUrl,
        data: JSON.stringify({
            Role_ID: id
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            if (msg.StatusFl == true) {
                alert("Record Deleted successfully !");
                var table = $('#tbl-Role-setup').DataTable();
                table.destroy();
                $("#tr_" + msg.Role.ROLE_ID).remove();
                initializeDataTable();
                $("#Loader").hide();
                fnGetRoleList();
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

function fnCloseModal() {
    fnClearForm();
}
