var arrFields = new Array();
var residentFlag = true;
$(document).ready(function () {
    $("#Loader").hide();
    fnGetRoleList();
    fnGetDesignationList();
    fnGetDepartmentList();
    fnGetBusinessUnitList();
    fnGetUserList();
    fnGetFormConfiguration();
    $("select[id*=ddlNationality]").on('change', function () {
        if ($(this).val() == 'NRI') {
            $("input[id*=txtPan]").prop('disabled', false);
            $("input[id*=txtPan]").val('');
            $("input[id*='txtIdentificationNumber']").val('');
            $("select[id*='ddlIdentificationType']").val('');
            $("select[id*='ddlIdentificationType']").prop('disabled', false);
            $("input[id*='txtIdentificationNumber']").prop('disabled', false);
            $("#spnPan").hide();
            $("#spnIdentificationType").show();
            $("#spnIdentification").show();
        }
        else if ($(this).val() == 'FOREIGN_NATIONAL') {
            $("input[id*=txtPan]").prop('disabled', false);
            //$("input[id*=txtPan]").val('NOT APPLICABLE');
            $("select[id*='ddlIdentificationType']").val('');
            $("input[id*='txtIdentificationNumber']").val('');
            $("select[id*='ddlIdentificationType']").prop('disabled', false);
            $("input[id*='txtIdentificationNumber']").prop('disabled', false);
            $("#spnPan").hide();
            $("#spnIdentificationType").show();
            $("#spnIdentification").show();
        }
        else if ($(this).val() == 'INDIAN_RESIDENT') {
            $("input[id*=txtPan]").prop('disabled', false);
            $("input[id*=txtPan]").val('');
            //if ($("input[id*=txtPan]").val() == 'NOT APPLICABLE') {
            //    $("input[id*=txtPan]").val('');
            //}
            $("select[id*='ddlIdentificationType']").val('');
            $("input[id*='txtIdentificationNumber']").val('');
            $("select[id*='ddlIdentificationType']").prop('disabled', false);
            $("input[id*='txtIdentificationNumber']").prop('disabled', false);
            $("#spnPan").show();
            $("#spnIdentificationType").hide();
            $("#spnIdentification").hide();
        }
    })
    $("#ddlRole").on('change', function () {
        if ($('#ddlRole option:selected').text() == 'UPSI Insider') {
            //$('#ddlDesignation').val(0);
            //$('#ddlDesignation option:selected').text('Not Applicable');
            //$('#ddlDesignation').attr('disabled', 'disabled');
            //$('#ddlDepartment').val(0);
            //$('#ddlDepartment option:selected').text('Not Applicable');
            //$('#ddlDepartment').attr('disabled', 'disabled');
        }
        else {
            //$('#ddlDesignation').val(0);
            //$('#ddlDesignation option:selected').text('Please Select');
            //$('#ddlDesignation').attr('disabled', false);
            //$('#ddlDepartment').val(0);
            //$('#ddlDepartment option:selected').text('Please Select');
            //$('#ddlDepartment').attr('disabled', false);
        }
    })
    $(".mobile").keypress(function (e) {
        //if the letter is not digit then display error and don't type anything

        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
            return false;
        }

        else if (this.value.length == 0 && e.which === 48) {
            return false;
        }

    });
    $('.mobile').bind("cut copy paste", function (e) {

        if (e.originalEvent.clipboardData.getData('Text').match(/[^\d]/)) {
            e.preventDefault();
        }


    });    
});
function initializeDataTable(id, columns) {
    $('#' + id).DataTable({
        dom: 'Bfrtip',
        pageLength: 17,
        "scrollY": "400px",
        "scrollX": true,
        buttons: [
            {
                extend: 'pdfHtml5',
                orientation: 'landscape',
                pageSize: 'TABLOID',
                title: 'User List',
                className: 'btn green btn-outline',
                exportOptions: {
                    columns: columns
                }
            },
            {
                extend: 'excel',
                className: 'btn yellow btn-outline ',
                exportOptions: {
                    columns: columns,
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
function isJson(str) {
    try {
        JSON.parse(str);
    } catch (e) {
        return false;
    }
    return true;
}
function fnCloseAdUserPopUp() {
    window.location.reload(true);
}
function btnSearch_Click() {
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/SerchByName?UserName=" + $("#txtUserName").val();
    if ($("#isSearchUsersFromLocalServer").prop("checked")) {
        webUrl = uri + "/api/UserIT/SearchByNameInLocalSystem?UserName=" + $("#txtUserName").val();
    }
    $.ajax({
        type: "POST",
        url: webUrl,
        // data: { UserName: $("#txtUserName").val() },
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        // async: false,
        success: function (msg) {
            if (msg.StatusFl == true) {
                var result = "";
                if (msg.UserList !== null) {
                    if (msg.UserList.length > 0) {
                        result += '<table class="TFtable">';
                        result += '<tr>';
                        result += '<th>';
                        result += '<input class="selectAllUser" type="checkbox"/>';
                        result += '</th>';
                        result += '<th>';
                        result += 'Login Id';
                        result += '</th>';
                        result += '<th>';
                        result += 'User Name';
                        result += '</th>';
                        result += '<th>';
                        result += 'User Email';
                        result += '</th>';
                        result += '<th>';
                        result += 'User Mobile';
                        result += '</th>';
                        result += '</tr>';
                        result += '<tbody>';
                        for (var i = 0; i < msg.UserList.length; i++) {
                            result += '<tr>';
                            result += '<td class="selectUser"><input type="checkbox"/></td>';
                            result += '<td>' + msg.UserList[i].LOGIN_ID + '</td>';
                            result += '<td>' + msg.UserList[i].USER_NM + '</td>';
                            result += '<td>' + msg.UserList[i].USER_EMAIL + '</td>';
                            result += '<td>' + msg.UserList[i].USER_MOBILE + '</td>';
                            result += '</tr>';
                        }
                        result += '</tbody>';
                        result += '</table>';
                    }
                }
                $("#userlist").html(result);

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
    //var msg = new Object();
    //msg.UserList = new Array();

    //for (var i = 0; i < 3; i++) {
    //    var obj = new Object();
    //    obj.LOGIN_ID = 'Login' + i;
    //    obj.USER_NM = 'User' + i;
    //    obj.USER_EMAIL = 'user' + i + '@gmail.com';
    //    msg.UserList.push(obj);
    //}



    $(".selectAllUser").on('change', function () {
        if ($(this).prop('checked')) {
            $(".selectUser input:checkbox").prop('checked', true);
        }
        else {
            $(".selectUser input:checkbox").prop('checked', false);
        }
    })

    $(".selectUser").on('change', function () {
        var countCheckboxes = $(".selectUser input:checkbox").length;
        var countCheckedCheckboxes = $(".selectUser [type='checkbox']:checked").length;
        if (countCheckboxes == countCheckedCheckboxes) {
            $(".selectAllUser").prop('checked', true);
        }
        else {
            $(".selectAllUser").prop('checked', false);
        }
    })
}
function fnAddADUserToUserList() {
    var userList = fnGetAllCheckedADUsers();
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/AddADUserToUserList";
    $.ajax({
        type: "POST",
        url: webUrl,
        data: JSON.stringify(userList),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            if (msg.StatusFl == true) {
                window.location.reload(true);
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
function fnGetAllCheckedADUsers() {
    var userList = new Array();
    var countCheckboxes = $(".selectUser input:checkbox").length;
    for (var i = 0; i <= countCheckboxes - 1; i++) {
        if ($($(".selectUser input:checkbox")[i]).prop('checked')) {
            var obj = new Object();
            obj.LOGIN_ID = $($($(".selectUser input:checkbox")[i]).closest('tr')[0].cells[1]).html();
            obj.USER_NM = $($($(".selectUser input:checkbox")[i]).closest('tr')[0].cells[2]).html();
            obj.USER_EMAIL = $($($(".selectUser input:checkbox")[i]).closest('tr')[0].cells[3]).html();
            obj.USER_MOBILE = $($($(".selectUser input:checkbox")[i]).closest('tr')[0].cells[4]).html();
            userList.push(obj);
        }
    }
    return userList;
}
function fnChangeUserStatus() {
    debugger;
    if ($("#ddlUserStatus").val() == "0") {
        fnGetAllUserList();
    }
    else {
        $("#Loader").show();
        var webUrl = uri + "/api/UserIT/GetUserList";
        $.ajax({
            type: "GET",
            url: webUrl,
            data: "{}",
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            success: function (msg) {
                if (msg.StatusFl == true) {
                    var result = "";
                    for (var i = 0; i < msg.UserList.length; i++) {
                        if (msg.UserList[i].status === $("#ddlUserStatus").val()) {
                            result += '<tr id="tr_' + msg.UserList[i].ID + '">';
                            result += '<td  id="tdedit_' + msg.UserList[i].LOGIN_ID + '">';
                            if ($("#ContentPlaceHolder1_txtBusinessUnitId").val().trim() == msg.UserList[i].businessUnit.businessUnitId) {
                                //result += '<a data-target="#userModel" data-toggle="modal" id="a' + msg.UserList[i].LOGIN_ID + '" class="btn btn-outline dark" onclick=\"javascript:fnEditUser(\'' + msg.UserList[i].LOGIN_ID + '\',\'' + msg.UserList[i].USER_NM + '\',\'' + msg.UserList[i].USER_EMAIL + '\',\'' + msg.UserList[i].USER_MOBILE + '\',\'' + msg.UserList[i].panNumber + '\',\'' + msg.UserList[i].userRole.ROLE_ID + '\',\'' + msg.UserList[i].nationality + '\',\'' + msg.UserList[i].USER_PWD + '\',\'' + msg.UserList[i].status + '\',\'' + msg.UserList[i].uploadAvatar + '\',\'' + msg.UserList[i].SALUTATION + '\',\'' + msg.UserList[i].designation.DESIGNATION_ID + '\',\'' + msg.UserList[i].department.DEPARTMENT_ID + '\',\'' + msg.UserList[i].businessUnit.businessUnitId + '\',\'' + msg.UserList[i].tenureEndDate + '\',\'' + msg.UserList[i].employeeId + '\',\'' + msg.UserList[i].joiningDate + '\',\'' + msg.UserList[i].PersonalEmail + '\');\">edit</a>';
                                result += '<a data-target="#userModel" data-toggle="modal" id="a' + msg.UserList[i].LOGIN_ID + '" class="btn btn-outline dark" onclick=\"javascript:fnEditUser(\'' + msg.UserList[i].LOGIN_ID + '\',\'' + msg.UserList[i].USER_NM + '\',\'' + msg.UserList[i].USER_EMAIL + '\',\'' + msg.UserList[i].USER_MOBILE + '\',\'' + msg.UserList[i].panNumber + '\',\'' + msg.UserList[i].userRole.ROLE_ID + '\',\'' + msg.UserList[i].nationality + '\',\'' + msg.UserList[i].USER_PWD + '\',\'' + msg.UserList[i].status + '\',\'' + msg.UserList[i].uploadAvatar + '\',\'' + msg.UserList[i].SALUTATION + '\',\'' + msg.UserList[i].designation.DESIGNATION_ID + '\',\'' + msg.UserList[i].department.DEPARTMENT_ID + '\',\'' + msg.UserList[i].businessUnit.businessUnitId + '\',\'' + msg.UserList[i].tenureEndDate + '\',\'' + msg.UserList[i].PersonalEmail + '\',\'' + msg.UserList[i].UserType + '\',\'' + msg.UserList[i].becomingInsiderDate + '\',\'' + msg.UserList[i].CategoryId + '\',\'' + msg.UserList[i].employeeId + '\',\'' + msg.UserList[i].joiningDate + '\',\'' + msg.UserList[i].ID + '\',\'' + msg.UserList[i].identificationType + '\',\'' + msg.UserList[i].identificationNumber + '\');\">Edit</a>';
                            }
                            result += '<td id="tdUserLogin_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].LOGIN_ID + '</td>';
                            result += '<td id="tdUserSalutation_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].SALUTATION + '</td>';
                            result += '<td id="tdUserNM_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].USER_NM + '</td>';
                            result += '<td id="tdUserEmail_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].USER_EMAIL + '</td>';
                            result += '<td id="tdusermobile_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].USER_MOBILE + '</td>';
                            result += '<td id="tduserPan_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].panNumber + '</td>';
                            result += '<td id="tduserRole_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].userRole.ROLE_NM + '</td>';
                            result += '<td id="tduserDesignation_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].designation.DESIGNATION_NM + '</td>';
                            result += '<td id="tduserDepartment_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].department.DEPARTMENT_NM + '</td>';
                            result += '<td id="tduserNationality_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].nationality + '</td>';
                            result += '<td id="tduserBusinessUnit_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].businessUnit.businessUnitName + '</td>';
                            result += '<td style="display:none;" id="tduserPersionemail_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].PersonalEmail + '</td>';
                            result += '<td style="display:none;" id="tduserBecomingdate_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].becomingInsiderDate + '</td>';
                            result += '<td style="display:none;" id="tduserJoingdate_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].joiningDate + '</td>';
                            result += '<td style="display:none;" id="tduserTennurestdate_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].tenureStartDate + '</td>';
                            result += '<td style="display:none;" id="tduserTennureenddate_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].tenureEndDate + '</td>';
                            result += '<td style="display:none;" id="tduserIdenttype_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].identificationType + '</td>';
                            result += '<td style="display:none;" id="tduserIdentnumber_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].identificationNumber + '</td>';
                            result += '<td  id="tduserStatus_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].status + '</td>';
                            result += '<td  id="tduserImage_' + msg.UserList[i].LOGIN_ID + '"><img style="width:50px;height:50px" class="img-circle" src="images/user/' + msg.UserList[i].uploadAvatar + '" alt="' + msg.UserList[i].uploadAvatar + '"/></td>';
                            // result += '<td id="tdedit_' + msg.UserList[i].LOGIN_ID + '">';
                            //if ($("#ContentPlaceHolder1_txtBusinessUnitId").val().trim() == msg.UserList[i].businessUnit.businessUnitId) {
                            //    //result += '<a data-target="#userModel" data-toggle="modal" id="a' + msg.UserList[i].LOGIN_ID + '" class="btn btn-outline dark" onclick=\"javascript:fnEditUser(\'' + msg.UserList[i].LOGIN_ID + '\',\'' + msg.UserList[i].USER_NM + '\',\'' + msg.UserList[i].USER_EMAIL + '\',\'' + msg.UserList[i].USER_MOBILE + '\',\'' + msg.UserList[i].panNumber + '\',\'' + msg.UserList[i].userRole.ROLE_ID + '\',\'' + msg.UserList[i].nationality + '\',\'' + msg.UserList[i].USER_PWD + '\',\'' + msg.UserList[i].status + '\',\'' + msg.UserList[i].uploadAvatar + '\',\'' + msg.UserList[i].SALUTATION + '\',\'' + msg.UserList[i].designation.DESIGNATION_ID + '\',\'' + msg.UserList[i].department.DEPARTMENT_ID + '\',\'' + msg.UserList[i].businessUnit.businessUnitId + '\',\'' + msg.UserList[i].tenureEndDate + '\',\'' + msg.UserList[i].employeeId + '\',\'' + msg.UserList[i].joiningDate + '\',\'' + msg.UserList[i].PersonalEmail + '\');\">edit</a>';
                            //    result += '<a data-target="#userModel" data-toggle="modal" id="a' + msg.UserList[i].LOGIN_ID + '" class="btn btn-outline dark" onclick=\"javascript:fnEditUser(\'' + msg.UserList[i].LOGIN_ID + '\',\'' + msg.UserList[i].USER_NM + '\',\'' + msg.UserList[i].USER_EMAIL + '\',\'' + msg.UserList[i].USER_MOBILE + '\',\'' + msg.UserList[i].panNumber + '\',\'' + msg.UserList[i].userRole.ROLE_ID + '\',\'' + msg.UserList[i].nationality + '\',\'' + msg.UserList[i].USER_PWD + '\',\'' + msg.UserList[i].status + '\',\'' + msg.UserList[i].uploadAvatar + '\',\'' + msg.UserList[i].SALUTATION + '\',\'' + msg.UserList[i].designation.DESIGNATION_ID + '\',\'' + msg.UserList[i].department.DEPARTMENT_ID + '\',\'' + msg.UserList[i].businessUnit.businessUnitId + '\',\'' + msg.UserList[i].tenureEndDate + '\',\'' + msg.UserList[i].identificationType + '\',\'' + msg.UserList[i].identificationNumber + '\',\'' + msg.UserList[i].PersonalEmail + '\');\">edit</a>';
                            //}
                            result += '</td>';
                            result += '</tr>';
                        }
                    }
                    var table = $('#tbl-user-setup').DataTable();
                    table.destroy();
                    $("#tbdUserList").html(result);
                    initializeDataTable('tbl-user-setup', [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18]);
                    // TableDatatablesButtons.init();
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
}
function fnGetUserList() {
    debugger;
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/GetUserList";
    $.ajax({
        type: "GET",
        url: webUrl,
        data: "{}",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            if (msg.StatusFl == true) {
                var result = "";
                for (var i = 0; i < msg.UserList.length; i++) {
                    if (msg.UserList[i].status == "Active") {
                        result += '<tr id="tr_' + msg.UserList[i].ID + '">';
                        result += '<td id="tdedit_' + msg.UserList[i].LOGIN_ID + '">';
                        result += '<a data-target="#userModel" data-toggle="modal" id="a' + msg.UserList[i].LOGIN_ID + '" class="btn btn-outline dark" onclick=\"javascript:fnEditUser(\'' + msg.UserList[i].LOGIN_ID + '\',\'' + msg.UserList[i].USER_NM + '\',\'' + msg.UserList[i].USER_EMAIL + '\',\'' + msg.UserList[i].USER_MOBILE + '\',\'' + msg.UserList[i].panNumber + '\',\'' + msg.UserList[i].userRole.ROLE_ID + '\',\'' + msg.UserList[i].nationality + '\',\'' + msg.UserList[i].USER_PWD + '\',\'' + msg.UserList[i].status + '\',\'' + msg.UserList[i].uploadAvatar + '\',\'' + msg.UserList[i].SALUTATION + '\',\'' + msg.UserList[i].designation.DESIGNATION_ID + '\',\'' + msg.UserList[i].department.DEPARTMENT_ID + '\',\'' + msg.UserList[i].businessUnit.businessUnitId + '\',\'' + msg.UserList[i].tenureEndDate + '\',\'' + msg.UserList[i].PersonalEmail + '\',\'' + msg.UserList[i].UserType + '\',\'' + msg.UserList[i].becomingInsiderDate + '\',\'' + msg.UserList[i].CategoryId + '\',\'' + msg.UserList[i].employeeId + '\',\'' + msg.UserList[i].joiningDate + '\',\'' + msg.UserList[i].ID + '\',\'' + msg.UserList[i].identificationType + '\',\'' + msg.UserList[i].identificationNumber + '\');\">Edit</a>';
                        result += '<td id="tdUserLogin_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].LOGIN_ID + '</td>';
                        result += '<td id="tdUserSalutation_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].SALUTATION + '</td>';
                        result += '<td id="tdUserNM_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].USER_NM + '</td>';
                        result += '<td id="tdUserEmail_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].USER_EMAIL + '</td>';
                        result += '<td id="tdusermobile_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].USER_MOBILE + '</td>';
                        result += '<td id="tduserPan_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].panNumber + '</td>';
                        result += '<td id="tduserRole_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].userRole.ROLE_NM + '</td>';
                        result += '<td id="tduserDesignation_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].designation.DESIGNATION_NM + '</td>';
                        result += '<td id="tduserDepartment_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].department.DEPARTMENT_NM + '</td>';
                        result += '<td id="tduserNationality_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].nationality + '</td>';
                        result += '<td id="tduserBusinessUnit_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].businessUnit.businessUnitName + '</td>';
                        result += '<td style="display:none;" id="tduserPersionemail_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].PersonalEmail + '</td>';
                        result += '<td style="display:none;" id="tduserBecomingdate_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].becomingInsiderDate + '</td>';
                        result += '<td style="display:none;" id="tduserJoingdate_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].joiningDate + '</td>';
                        result += '<td style="display:none;" id="tduserTennurestdate_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].tenureStartDate + '</td>';
                        result += '<td style="display:none;" id="tduserTennureenddate_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].tenureEndDate + '</td>';
                        result += '<td style="display:none;" id="tduserIdenttype_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].identificationType + '</td>';
                        result += '<td style="display:none;" id="tduserIdentnumber_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].identificationNumber + '</td>';
                        result += '<td  id="tduserStatus_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].status + '</td>';
                        result += '<td  id="tduserImage_' + msg.UserList[i].LOGIN_ID + '"><img style="width:50px;height:50px" class="img-circle" src="images/user/' + msg.UserList[i].uploadAvatar + '" alt="' + msg.UserList[i].uploadAvatar + '"/></td>';
                        //result += '<td id="tdedit_' + msg.UserList[i].LOGIN_ID + '">';
                        ////if ($("#ContentPlaceHolder1_txtBusinessUnitId").val().trim() == msg.UserList[i].businessUnit.businessUnitId) {
                        //result += '<a data-target="#userModel" data-toggle="modal" id="a' + msg.UserList[i].LOGIN_ID + '" class="btn btn-outline dark" onclick=\"javascript:fnEditUser(\'' + msg.UserList[i].LOGIN_ID + '\',\'' + msg.UserList[i].USER_NM + '\',\'' + msg.UserList[i].USER_EMAIL + '\',\'' + msg.UserList[i].USER_MOBILE + '\',\'' + msg.UserList[i].panNumber + '\',\'' + msg.UserList[i].userRole.ROLE_ID + '\',\'' + msg.UserList[i].nationality + '\',\'' + msg.UserList[i].USER_PWD + '\',\'' + msg.UserList[i].status + '\',\'' + msg.UserList[i].uploadAvatar + '\',\'' + msg.UserList[i].SALUTATION + '\',\'' + msg.UserList[i].designation.DESIGNATION_ID + '\',\'' + msg.UserList[i].department.DEPARTMENT_ID + '\',\'' + msg.UserList[i].businessUnit.businessUnitId + '\',\'' + msg.UserList[i].tenureEndDate + '\',\'' + msg.UserList[i].PersonalEmail + '\',\'' + msg.UserList[i].UserType + '\',\'' + msg.UserList[i].becomingInsiderDate + '\',\'' + msg.UserList[i].CategoryId + '\',\'' + msg.UserList[i].employeeId + '\',\'' + msg.UserList[i].joiningDate + '\',\'' + msg.UserList[i].identificationType + '\',\'' + msg.UserList[i].identificationNumber + '\',\'' + msg.UserList[i].ID + '\');\">edit</a>';
                        //}
                        result += '</td>';
                        result += '</tr>';
                    }
                }
                var table = $('#tbl-user-setup').DataTable();
                table.destroy();
                $("#tbdUserList").html(result);
                initializeDataTable('tbl-user-setup', [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18]);
                // TableDatatablesButtons.init();
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
    //   $('#loadingmessage').modal('hide');
}
function fnGetAllUserList() {
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/GetUserList";
    $.ajax({
        type: "GET",
        url: webUrl,
        data: "{}",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            if (msg.StatusFl == true) {
                var result = "";
                for (var i = 0; i < msg.UserList.length; i++) {
                    result += '<tr id="tr_' + msg.UserList[i].ID + '">';
                    result += '<td id="tdedit_' + msg.UserList[i].LOGIN_ID + '">';
                    result += '<a data-target="#userModel" data-toggle="modal" id="a' + msg.UserList[i].LOGIN_ID + '" class="btn btn-outline dark" onclick=\"javascript:fnEditUser(\'' + msg.UserList[i].LOGIN_ID + '\',\'' + msg.UserList[i].USER_NM + '\',\'' + msg.UserList[i].USER_EMAIL + '\',\'' + msg.UserList[i].USER_MOBILE + '\',\'' + msg.UserList[i].panNumber + '\',\'' + msg.UserList[i].userRole.ROLE_ID + '\',\'' + msg.UserList[i].nationality + '\',\'' + msg.UserList[i].USER_PWD + '\',\'' + msg.UserList[i].status + '\',\'' + msg.UserList[i].uploadAvatar + '\',\'' + msg.UserList[i].SALUTATION + '\',\'' + msg.UserList[i].designation.DESIGNATION_ID + '\',\'' + msg.UserList[i].department.DEPARTMENT_ID + '\',\'' + msg.UserList[i].businessUnit.businessUnitId + '\',\'' + msg.UserList[i].tenureEndDate + '\',\'' + msg.UserList[i].PersonalEmail + '\',\'' + msg.UserList[i].UserType + '\',\'' + msg.UserList[i].becomingInsiderDate + '\',\'' + msg.UserList[i].CategoryId + '\',\'' + msg.UserList[i].employeeId + '\',\'' + msg.UserList[i].joiningDate + '\',\'' + msg.UserList[i].ID + '\',\'' + msg.UserList[i].identificationType + '\',\'' + msg.UserList[i].identificationNumber + '\');\">Edit</a>';
                    result += '<td id="tdUserLogin_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].LOGIN_ID + '</td>';
                    result += '<td id="tdUserSalutation_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].SALUTATION + '</td>';
                    result += '<td id="tdUserNM_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].USER_NM + '</td>';
                    result += '<td id="tdUserEmail_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].USER_EMAIL + '</td>';
                    result += '<td id="tdusermobile_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].USER_MOBILE + '</td>';
                    result += '<td id="tduserPan_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].panNumber + '</td>';
                    result += '<td id="tduserRole_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].userRole.ROLE_NM + '</td>';
                    result += '<td id="tduserDesignation_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].designation.DESIGNATION_NM + '</td>';
                    result += '<td id="tduserDepartment_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].department.DEPARTMENT_NM + '</td>';
                    result += '<td id="tduserNationality_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].nationality + '</td>';
                    result += '<td id="tduserBusinessUnit_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].businessUnit.businessUnitName + '</td>';
                    result += '<td style="display:none;" id="tduserPersionemail_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].PersonalEmail + '</td>';
                    result += '<td style="display:none;" id="tduserBecomingdate_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].becomingInsiderDate + '</td>';
                    result += '<td style="display:none;" id="tduserJoingdate_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].joiningDate + '</td>';
                    result += '<td style="display:none;" id="tduserTennurestdate_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].tenureStartDate + '</td>';
                    result += '<td style="display:none;" id="tduserTennureenddate_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].tenureEndDate + '</td>';
                    result += '<td style="display:none;" id="tduserIdenttype_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].identificationType + '</td>';
                    result += '<td style="display:none;" id="tduserIdentnumber_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].identificationNumber + '</td>';
                    result += '<td  id="tduserStatus_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].status + '</td>';
                    result += '<td  id="tduserImage_' + msg.UserList[i].LOGIN_ID + '"><img style="width:50px;height:50px" class="img-circle" src="images/user/' + msg.UserList[i].uploadAvatar + '" alt="' + msg.UserList[i].uploadAvatar + '"/></td>';
                    //result += '<td id="tdedit_' + msg.UserList[i].LOGIN_ID + '">';
                    ////if ($("#ContentPlaceHolder1_txtBusinessUnitId").val().trim() == msg.UserList[i].businessUnit.businessUnitId) {
                    //result += '<a data-target="#userModel" data-toggle="modal" id="a' + msg.UserList[i].LOGIN_ID + '" class="btn btn-outline dark" onclick=\"javascript:fnEditUser(\'' + msg.UserList[i].LOGIN_ID + '\',\'' + msg.UserList[i].USER_NM + '\',\'' + msg.UserList[i].USER_EMAIL + '\',\'' + msg.UserList[i].USER_MOBILE + '\',\'' + msg.UserList[i].panNumber + '\',\'' + msg.UserList[i].userRole.ROLE_ID + '\',\'' + msg.UserList[i].nationality + '\',\'' + msg.UserList[i].USER_PWD + '\',\'' + msg.UserList[i].status + '\',\'' + msg.UserList[i].uploadAvatar + '\',\'' + msg.UserList[i].SALUTATION + '\',\'' + msg.UserList[i].designation.DESIGNATION_ID + '\',\'' + msg.UserList[i].department.DEPARTMENT_ID + '\',\'' + msg.UserList[i].businessUnit.businessUnitId + '\',\'' + msg.UserList[i].tenureEndDate + '\',\'' + msg.UserList[i].PersonalEmail + '\',\'' + msg.UserList[i].UserType + '\',\'' + msg.UserList[i].becomingInsiderDate + '\',\'' + msg.UserList[i].CategoryId + '\',\'' + msg.UserList[i].employeeId + '\',\'' + msg.UserList[i].joiningDate + '\',\'' + msg.UserList[i].identificationType + '\',\'' + msg.UserList[i].identificationNumber + '\',\'' + msg.UserList[i].ID + '\');\">edit</a>';
                    //}
                    result += '</td>';
                    result += '</tr>';
                }
                var table = $('#tbl-user-setup').DataTable();
                table.destroy();
                $("#tbdUserList").html(result);
                initializeDataTable('tbl-user-setup', [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18]);
                // TableDatatablesButtons.init();
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
    //   $('#loadingmessage').modal('hide');
}
function OpenNew() {
    $("span[Id*='spnTitle']").html("New User");
    $("#txtUsrId").val("0");
}
function fnSaveUser() {
    //debugger;
    $("input[id*='txtPassword']").val("P@ssw0rd");
    $("input[id*='txtConfirm']").val("P@ssw0rd");
    if (fnValidate()) {
        var nationality = $("select[id *= ddlNationality]").val();
        if (nationality == 'NRI') {
            if ($("input[id*='txtIdentificationNumber']").val() == "") {
                alert("Please Enter Identification Number because you selected Nationality as a NRI");
                return false;
            }
            if ($("select[id *= 'ddlIdentificationType']").val() == 0) {
                alert("Please Enater Identification Type because you selected Nationality as a NRI");
                return false;
            }
        }
        else if (nationality == 'FOREIGN_NATIONAL') {
            if ($("input[id*='txtIdentificationNumber']").val() == "") {
                alert("Please Enter Identification Number because you selected Nationality as a FOREIGN NATIONAL");
                return false;
            }
            if ($("select[id *= 'ddlIdentificationType']").val() == 0) {
                alert("Please Enter Identification Type because you selected Nationality as a FOREIGN NATIONAL");
                return false;
            }
        }
        else if (nationality == 'INDIAN_RESIDENT') {
            if ($("input[id*='txtPan']").val() == "") {
                alert("Please Enter Pan Number because you selected Nationality as a INDIAN RESIDENT");
                return false;
            }
        }
        if ($("span[Id*='spnTitle']").html().toUpperCase() === "EDIT USER" && $("select[id*='ddlStatus']").val().toLowerCase() == "inactive") {
            if (confirm("You are about to Inactive user.Please confirm if you want to allow user to login through his/her personal email.")) {
                $("input[id*='txtLoginUsingPersonalEmail']").val('Yes');
            }
            else {
                $("input[id*='txtLoginUsingPersonalEmail']").val('No');
            }
        }
        else if ($("span[Id*='spnTitle']").html().toUpperCase() == "NEW USER" && $("select[id*='ddlStatus']").val().toLowerCase() == "active") {
            if (confirm("You are about to create and send welcome email to the new user. Press okay to confirm.")) {
                //$("input[id*='txtLoginUsingPersonalEmail']").val('Yes');
            }
            else {
                $('#btnSave').removeAttr("data-dismiss");
                return false;
            }
        }
        $("#Loader").show();
        fnAddUpdateUser();
    }
    else {
        $('#btnSave').removeAttr("data-dismiss");
        return false;
    }
}
function fnAddUpdateUser() {
    var userData = new FormData();
    if ($("span[Id*='spnTitle']").html() === "New User") {
        var newpassword = hex_sha512("P@ssw0rd");
        $("input[id*='txtPassword']").val(newpassword);
        $("input[id*='txtConfirm']").val(newpassword);
    }

    var UserTyp = "";
    if ($("#radioAD").prop('checked') == true) {
        UserTyp = "AD/SAML";
    }
    if ($("#radioApplication").prop('checked') == true) {
        UserTyp = "Application";
    }
    var UserColl = [];
    UserColl[UserColl.length] = new User(
        $("input[id*='txtUsrId']").val() == 0 ? 0 : $("input[id*='txtUsrId']").val(),
        $("input[id*='txtName']").val(),
        $("input[id*='txtEmail']").val(),
        $("input[id*='txtPhone']").val(),
        $("#txtPan").val(),
        $("select[id*='ddlNationality']").val(),
        $("input[id*='txtUserid']").val(),
        $("input[id*='txtPassword']").val(),
        $("select[id*='ddlStatus']").val(),
        $("select[id*='ddlRole']").val(),
        $('#ddlSalutation').val(),
        $('#ddlDesignation').val(),
        $('#ddlDepartment').val(),
        $('#ddlBusinessUnit').val(),
        $("input[id*='txtEmailPersonal']").val(),
        $("input[id*='txtDateOfRetirement']").val(),
        UserTyp,
        $("input[id*='txtLoginUsingPersonalEmail']").val(),
        $("input[id*='txtDateOfBecomingInsider']").val(),
        $("select[id*='ddlCategory']").val(),
        $("#txtEmployeeId").val(),
        $("#txtDateOfJoining").val(),
        $("select[id*='ddlIdentificationType']").val(),
        $("input[id*='txtIdentificationNumber']").val(),
    );

    userData.append("Object", JSON.stringify(UserColl));

    if ($("input[id*='fileUploadImage']").get(0).files.length > 0) {
        userData.append("Files", $("input[id*='fileUploadImage']").get(0).files[0]);
    }

    var webUrl = uri + "/api/UserIT/SaveUser";
    $.ajax({
        type: 'POST',
        url: webUrl,
        data: userData,
        contentType: false,
        async: false,
        processData: false,
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
                $("#Loader").hide();
                alert(msg.Msg);
                //if (msg.User.ID == UserID) {
                //    $("#tdUserLogin_" + msg.User.LOGIN_ID).html(msg.User.LOGIN_ID);
                //    $("#tdUserSalutation_" + msg.User.LOGIN_ID).html(msg.User.SALUTATION);
                //    $("#tdUserNM_" + msg.User.LOGIN_ID).html(msg.User.USER_NM);
                //    $("#tdUserEmail_" + msg.User.LOGIN_ID).html(msg.User.USER_EMAIL);
                //    $("#tdusermobile_" + msg.User.LOGIN_ID).html(msg.User.USER_MOBILE);
                //    $("#a" + msg.User.LOGIN_ID).attr("onclick", "javascript:fnEditUser('" + msg.UserList[i].LOGIN_ID + "','" + msg.UserList[i].USER_NM + "','" + msg.UserList[i].USER_EMAIL + "','" + msg.UserList[i].USER_MOBILE + "','" + msg.UserList[i].role + "','" + msg.UserList[i].nationality + "','" + msg.UserList[i].USER_PWD + "','" + msg.UserList[i].status + "','" + msg.UserList[i].uploadAvatar + "','" + msg.UserList[i].SALUTATION + "','" + msg.UserList[i].DESIGNATION_ID + "','" + msg.UserList[i].DEPARTMENT_ID + "');");
                //    $("#a" + msg.User.LOGIN_ID).attr("data-target", "#userModel");
                //    $("#a" + msg.User.LOGIN_ID).attr("data-toggle", "modal");

                //    $("#Loader").hide();
                //    var table = $('#tbl-user-setup').DataTable();
                //    table.destroy();
                //    initializeDataTable('tbl-user-setup', [0, 1, 2, 3]);
                //}
                //else {
                //    var result = "";
                //    result += '<tr id="tr_' + msg.User.LOGIN_ID + '">';
                //    result += '<td id="tdUserLogin_' + msg.User.LOGIN_ID + '">' + msg.User.LOGIN_ID + '</td>';
                //    result += '<td id="tdUserSalutation_' + msg.User.LOGIN_ID + '">' + msg.User.SALUTATION + '</td>';
                //    result += '<td id="tdUserNM_' + msg.User.LOGIN_ID + '">' + msg.User.USER_NM + '</td>';
                //    result += '<td id="tdUserEmail_' + msg.User.LOGIN_ID + '">' + msg.User.USER_EMAIL + '</td>';
                //    result += '<td id="tdusermobile_' + msg.User.LOGIN_ID + '">' + msg.User.USER_MOBILE + '</td>';
                //    result += '<td id="tdedit_' + msg.User.LOGIN_ID + '"><a data-target="#userModel" data-toggle="modal" id="a' + msg.User.LOGIN_ID + '" class="btn btn-outline dark" onclick=\"javascript:fnEditUser(\'' + msg.UserList[i].LOGIN_ID + '\',\'' + msg.UserList[i].USER_NM + '\',\'' + msg.UserList[i].USER_EMAIL + '\',\'' + msg.UserList[i].USER_MOBILE + '\',\'' + msg.UserList[i].role + '\',\'' + msg.UserList[i].nationality + '\',\'' + msg.UserList[i].USER_PWD + '\',\'' + msg.UserList[i].status + '\',\'' + msg.UserList[i].uploadAvatar + '\',\'' + msg.UserList[i].SALUTATION + '\',\'' + msg.UserList[i].DESIGNATION_ID + '\',\'' + msg.UserList[i].DEPARTMENT_ID + '\');\">edit</a></td>';
                //    result += '</tr>';
                //    var table = $('#tbl-user-setup').DataTable();
                //    table.destroy();
                //    $("#tbdUserList").append(result);
                //    initializeDataTable('tbl-user-setup', [0, 1, 2, 3]);
                //    $("#Loader").hide();
                //}
                fnClearForm();
                fnGetUserList();
                $('#btnSave').attr("data-dismiss", "modal");
                return true;
            }
            $("#userModel").modal('hide');
        },
        error: function (error) {
            $("#Loader").hide();
            alert(error.status + ' ' + error.statusText);
            $('#btnSave').removeAttr("data-dismiss");
        }
    })
}
function fnValidatePasswordPolicy(str) {
    var strings = str;
    var specialCharacters = "!#$%&()*+@?^~";
    var setCounterUpperCase = 0;
    var setCounterLowerCase = 0;
    var setCounterNumeric = 0;
    var setCounterSpecialCharacters = 0;
    var setCounterPasswordLength = 0;
    var i = 0;
    var character = '';
    while (i <= strings.length) {
        character = strings.charAt(i);
        if (!isNaN(character * 1)) {
            //alert('character is numeric');
            setCounterNumeric = 1;
        }
        else if (specialCharacters.indexOf(character) !== -1) {
            setCounterSpecialCharacters = 1;
        }
        else {
            if (character == character.toUpperCase()) {
                // alert('upper case true');
                setCounterUpperCase = 1;
            }
            if (character == character.toLowerCase()) {
                // alert('lower case true');
                setCounterLowerCase = 1;
            }
        }
        i++;
    }

    if (strings.length >= 8) {
        setCounterPasswordLength = 1;
    }

    if (setCounterPasswordLength > 0 && setCounterNumeric > 0 && setCounterSpecialCharacters > 0 && setCounterUpperCase > 0 && setCounterLowerCase > 0) {
        return true;
    }
    else {
        return false;
    }
}
function validateEmail(value) {
    var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
    if (reg.test(value) == false) {
        return false;
    }
    return true;
}
function fnClearForm() {
    $('#txtUserId').val('');
    $('#txtName').val('');
    $('#txtEmail').val('');
    $('#ddlRole').val(0);
    $('#txtPhone').val('');
    $("#txtPan").val('');
    $('#txtAddress').val('');
    $('#txtTenurestartdate').val('');
    $('#txtTenureenddate').val('');
    $('#txtDateofbirth').val('');
    $('#ddlNationality').val(0)
    $('#txtUserid').val('');
    $('#txtPassword').val('');
    $('#txtConfirm').val('');
    $('#ddlStatus').val(0);
    $('#ddlSalutation').val(0);
    $('#ddlDesignation').val(0);
    $('#ddlDepartment').val(0);
    $('#ddlBusinessUnit').val(0);
    $('#txtUserid').prop('disabled', false);
    $('#txtEmail').prop('disabled', false);
    $("#txtPassword").prop('disabled', false);
    $("#txtConfirm").prop('disabled', false);
    $('#txtEmailPersonal').val('');
    $('#txtDateOfRetirement').val('');
    $('#txtLoginUsingPersonalEmail').val('No');
    $("select[id*='ddlCategory']").val(0);
    $("#txtEmployeeId").val('');
    //  uploadedFile = null;
    $("#file").val('');
    fnRemoveClass(null, 'Email');
    fnRemoveClass(null, 'Name');
    fnRemoveClass(null, 'Phone');
    fnRemoveClass(null, 'Pan');
    fnRemoveClass(null, 'Userid');
    fnRemoveClass(null, 'Password');
    fnRemoveClass(null, 'Confirm');
    fnRemoveClass(null, 'Role');
    fnRemoveClass(null, 'Status');
    fnRemoveClass(null, 'Upload');
    fnRemoveClass(null, 'Salutation');
    fnRemoveClass(null, 'Designation');
    fnRemoveClass(null, 'Department');
    fnRemoveClass(null, 'BusinessUnit');
    fnRemoveClass(null, 'EmailPersonal');
    fnRemoveClass(null, 'RetirementDate');
    fnRemoveClass(null, 'Category');
    fnRemoveClass(null, 'DateOfJoining');
    $("#aUserAvatarImageUploaded").hide();
    $("input[id*='txtDateOfBecomingInsider']").val('');
    $("input[name='radio2']").attr('checked', false);
    $("input[id*='txtDateOfJoining']").val('');
}
function fnEditUser(user_key, user_name, user_mail, phnumber, pan, role, nationality, USER_PWD, status, uploadAvatar, SALUTATION, DESIGNATION_ID, DEPARTMENT_ID,
    buId, tenureEndDate, PersonalEmail, UserType, DateOfBecomingInsider, Category, EmployeeId, DateOfJoining, ID, identificationType, identifiactionnumber) {
    $("span[Id*='spnTitle']").html("Edit User");
    $("#txtUsrId").val(ID);
    $('#txtUserid').val(user_key);
    $('#txtName').val(user_name);
    $('#txtEmail').val(user_mail);
    $('#txtPassword').val(USER_PWD);
    $('#txtConfirm').val(USER_PWD);
    $('#txtPhone').val(phnumber);
    $("#txtPan").val(pan);
    $('#ddlRole').val(role);
    $('#ddlNationality').val(nationality);
    $('#ddlStatus').val(status);
    $('#ddlSalutation').val(SALUTATION);
    $('#txtEmailPersonal').val(PersonalEmail);
    $('#txtDateOfRetirement').val(tenureEndDate);
    $('#txtDateOfBecomingInsider').val(DateOfBecomingInsider);
    if ($("#ddlRole option:selected").text().trim() == 'UPSI Insider') {
        $('#ddlDesignation').val(0);
        $('#ddlDesignation option:selected').text('Not Applicable');
        $('#ddlDesignation').attr('disabled', 'disabled');
        $('#ddlDepartment').val(0);
        $('#ddlDepartment option:selected').text('Not Applicable');
        $('#ddlDepartment').attr('disabled', 'disabled');
    }
    else {
        $('#ddlDesignation').val(0);
        $('#ddlDesignation option:selected').text('Please Select');
        $('#ddlDesignation').attr('disabled', false);
        $('#ddlDepartment').val(0);
        $('#ddlDepartment option:selected').text('Please Select');
        $('#ddlDepartment').attr('disabled', false);
        $('#ddlDesignation').val(DESIGNATION_ID);
        $('#ddlDepartment').val(DEPARTMENT_ID);
    }
    $('#ddlBusinessUnit').val(buId);
    $('#txtUserid').prop('disabled', true);
    //$('#txtEmail').prop('disabled', true);
    $("#txtPassword").prop('disabled', true);
    $("#txtConfirm").prop('disabled', true);
    //alert("uploadAvatar=" + uploadAvatar);
    if (uploadAvatar !== undefined && uploadAvatar !== null && uploadAvatar.trim() !== '') {
        //alert("In not undefine, not null and not blank");
        $("#aUserAvatarImageUploaded").show();
        $("#aUserAvatarImageUploaded").attr('href', 'images/user/' + uploadAvatar);
        $("#imgUsr").attr('src', 'images/user/' + uploadAvatar);
        //aUserAvatarImageUploaded
    }
    else {
        $("#aUserAvatarImageUploaded").hide();
    }
    $("select[id*='ddlCategory']").val(Category);
    if (UserType == "AD/SAML") {
        $("input[id='radioAD']").prop("checked", true);
    }
    else {
        $("input[id='radioApplication']").prop("checked", true);
    }

    $("#txtEmployeeId").val(EmployeeId);
    $("#txtDateOfJoining").val(DateOfJoining);
    if ($("select[id*=ddlNationality]").val() == 'NRI') {
        $("select[id*=ddlIdentificationType]").val(identificationType);
        $("input[id*='txtIdentificationNumber']").val(identifiactionnumber);
        $("#spnIdentificationType").show();
        $("#spnIdentification").show();
        $("#spnPan").hide();
    }
    else if ($("select[id*=ddlNationality]").val() == 'INDIAN_RESIDENT') {
        $("select[id*=ddlIdentificationType]").val(identificationType);
        $("input[id*='txtIdentificationNumber']").val(identifiactionnumber);
        $("input[id*='txtPan']").prop('disabled', false);
        $("#spnIdentificationType").hide();
        $("#spnIdentification").hide();
    }
    else if ($("select[id*=ddlNationality]").val() == 'FOREIGN_NATIONAL') {
        $("select[id*=ddlIdentificationType]").val(identificationType);
        $("input[id*='txtIdentificationNumber']").val(identifiactionnumber);
        if ($("input[id*=txtPan").val() === "") {
            $("input[id*=txtPan]").prop('disabled', false);
            $("#spnIdentificationType").show();
            $("#spnIdentification").show();
            $("#spnPan").hide();
        }
        else {
            $("input[id*=txtPan]").prop('disabled', false);
            $("#spnIdentificationType").hide();
            $("#spnIdentification").hide();
        }
    }
}
function fnRemoveClass(obj, val) {
    $("#lbl" + val + "").removeClass('requied');
}
function DeleteUser(user_key, userId) {
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/DeleteUser?moreSalt=" + $("#moreSalt").val();
    $.ajax({
        type: 'POST',
        url: webUrl,
        data: JSON.stringify({
            ID: user_key
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            if (msg.StatusFl == true) {
                alert("Record Deleted successfully !");
                table = $('#sample_1').DataTable();
                table.destroy();
                $("#tr_" + msg.User.ID).remove();
                var table = $('#tbl-user-setup').DataTable();
                table.destroy();
                initializeDataTable('tbl-user-setup', [0, 1, 2, 3]);
                $("#Loader").hide();
                //  TableDatatablesButtons.init();
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
function fnGetRoleList() {
    $("#Loader").show();
    var webUrl = uri + "/api/Role/GetRoleListWithAdmin";
    $.ajax({
        type: "POST",
        url: webUrl,
        data: JSON.stringify({
            COMPANY_ID: 0
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {

            if (msg.StatusFl == true) {
                var result = "";
                for (var i = 0; i < msg.RoleList.length; i++) {
                    result += '<option value="' + msg.RoleList[i].ROLE_ID + '">' + msg.RoleList[i].ROLE_NM + '</option>';
                }
                $("#ddlRole").append(result);
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
function fnGetDesignationList() {
    $("#Loader").show();
    var webUrl = uri + "/api/DesignationIT/GetDesignationList";
    $.ajax({
        type: "POST",
        url: webUrl,
        data: JSON.stringify({
            COMPANY_ID: 0
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                var result = "";
                for (var i = 0; i < msg.DesignationList.length; i++) {
                    result += '<option value="' + msg.DesignationList[i].DESIGNATION_ID + '">' + msg.DesignationList[i].DESIGNATION_NM + '</option>';
                }

                $("#ddlDesignation").append(result);
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
function fnGetDepartmentList() {
    $("#Loader").show();
    var webUrl = uri + "/api/Department/GetDepartmentList";
    $.ajax({
        type: "POST",
        url: webUrl,
        data: JSON.stringify({
            COMPANY_ID: 0
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                var result = "";
                for (var i = 0; i < msg.DepartmentList.length; i++) {
                    result += '<option value="' + msg.DepartmentList[i].DEPARTMENT_ID + '">' + msg.DepartmentList[i].DEPARTMENT_NM + '</option>';
                }

                $("#ddlDepartment").append(result);
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
function fnGetBusinessUnitList() {
    $("#Loader").show();
    var webUrl = uri + "/api/BusinessUnit/GetBusinessUnitList";
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

                $("#ddlBusinessUnit").append(result);
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
function User(ID, userName, emailId, phone, pan, nationality, userLogin, password, status, roleId, salutation, designationId, departmentId, buId, personalEmail, retirementDate, UserType, LoginUsingPersonalEmail, DateofBecomingInsider, Category, EmployeeId, DateOfJoining, identifcationType, identifcationNo) {
    this.ID = ID;
    this.USER_NM = userName;
    this.USER_EMAIL = emailId;
    this.USER_MOBILE = phone;
    this.panNumber = pan;
    this.nationality = nationality;
    this.LOGIN_ID = userLogin;
    this.USER_PWD = password;
    this.status = status;
    this.userRole = new Role(roleId);
    this.SALUTATION = salutation;
    this.designation = new Designation(designationId);
    this.department = new Department(departmentId);
    this.businessUnit = new BusinessUnit(buId);
    this.PersonalEmail = personalEmail;
    this.tenureEndDate = retirementDate;
    this.UserType = UserType;
    this.LoginUsingPersonalEmail = LoginUsingPersonalEmail;
    this.becomingInsiderDate = DateofBecomingInsider;
    this.CategoryId = Category;
    this.employeeId = EmployeeId;
    this.joiningDate = DateOfJoining;
    this.identificationType = identifcationType;
    this.identificationNumber = identifcationNo;
}
function Role(roleId) {
    this.ROLE_ID = roleId;
}
function Designation(designationId) {
    this.DESIGNATION_ID = designationId;
}
function Department(departmentId) {
    this.DEPARTMENT_ID = departmentId;
}
function BusinessUnit(buId) {
    this.businessUnitId = buId;
}
function fnGetFormConfiguration() {
    $("#Loader").show();
    var webUrl = uri + "/api/Transaction/GetUserFormConfig";
    $.ajax({
        url: webUrl,
        type: "GET",
        async: false,
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                for (var x = 0; x < msg.Modules.length; x++) {
                    arrFields.push(msg.Modules[x]);
                }

                for (var x = 0; x < arrFields.length; x++) {
                    for (var y = 0; y < arrFields[x].fields.length; y++) {
                        var obj = arrFields[x].fields[y];
                        if (obj.EditFl == 'N') {
                            if (obj.CntrlType == 'TextBox') {
                                $("input[id*=" + obj.ControlId + "]").attr("disabled", "disabled");
                            }
                            else if (obj.CntrlType == 'TextArea') {
                                $("textarea[id*=" + obj.ControlId + "]").attr("disabled", "disabled");
                            }
                            else if (obj.CntrlType == 'Dropdown') {
                                $("select[id*=" + obj.ControlId + "]").attr("disabled", "disabled");
                            }
                        }
                        if (obj.RequiredFl == 'N') {
                            var string = obj.DivNm;
                            string = string.replace(/^.{2}/g, 'spn');
                            $("#" + string).hide();
                        }
                        if (obj.DisplayFl == 'N') {
                            if (obj.RequiredFl == "Y") {
                                if (obj.FormatType == 'Date') {
                                    if (obj.CntrlType == 'TextBox') {
                                        $("input[id*=" + obj.ControlId + "]").val('01/01/1900');
                                    }
                                    else {
                                        $("input[id*=" + obj.ControlId + "]").val($("input[id*=" + obj.ControlId + "] option:first").val());
                                    }
                                }
                                else {
                                    if (obj.CntrlType == 'TextBox') {
                                        $("input[id*=" + obj.ControlId + "]").val('N/A');
                                    }
                                    else if (obj.CntrlType == 'TextArea') {
                                        $("textarea[id*=" + obj.ControlId + "]").val('N/A');
                                    }
                                    else {
                                        $("input[id*=" + obj.ControlId + "]").val($("input[id*=" + obj.ControlId + "] option:first").val());
                                    }
                                }
                            }
                            $("#" + obj.DivNm).hide();
                        }
                        else {
                            $("#" + obj.DivNm).show();
                        }
                    }
                }
            }
            else {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                }
            }
        },
        error: function (response) {
            $("#Loader").hide();
            alert(response.status + ' ' + response.statusText);
        }
    })
}
function fnValidate() {
    var flag = false;
    for (var x = 0; x < arrFields.length; x++) {
        for (y = 0; y < arrFields[x].fields.length; y++) {
            var obj = arrFields[x].fields[y];
            if (arrFields[x].fields[y].DisplayFl == "Y" && arrFields[x].fields[y].RequiredFl == "Y") {
                var value;
                if (arrFields[x].fields[y].FormatType == "Number") {
                    var regpan = /^\d+$/;
                    if (arrFields[x].fields[y].CntrlType == "TextBox") {
                        value = $("input[id*=" + obj.ControlId + "]").val();
                    }
                    else if (arrFields[x].fields[y].CntrlType == "Dropdown") {
                        value = $("select[id*=" + obj.ControlId + "]").val();
                    }
                    if (value == "" || value == null || value == undefined) {
                        flag = true;
                        $("#" + arrFields[x].fields[y].DivNm).addClass('has-error');

                    }
                    else if (!regpan.test(value)) {
                        flag = true;
                        $("#" + arrFields[x].fields[y].DivNm).addClass('has-error');

                    }
                    else {
                        $("#" + arrFields[x].fields[y].DivNm).removeClass('has-error');
                    }
                }
                else if (arrFields[x].fields[y].FormatType == "Email") {

                    if (arrFields[x].fields[y].CntrlType == "TextBox") {
                        value = $("input[id*=" + obj.ControlId + "]").val();
                    }
                    else if (arrFields[x].fields[y].CntrlType == "Dropdown") {
                        value = $("select[id*=" + obj.ControlId + "]").val();
                    }

                    var regemail = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;

                    if (value == "" || value == null || value == undefined) {
                        flag = true;
                        $("#" + arrFields[x].fields[y].DivNm).addClass('has-error');

                    }
                    else if (!regemail.test(value)) {
                        flag = true;
                        $("#" + arrFields[x].fields[y].DivNm).addClass('has-error');
                    }
                    else {
                        $("#" + arrFields[x].fields[y].DivNm).removeClass('has-error');
                    }
                }
                else if (arrFields[x].fields[y].FormatType == "Mobile") {
                    var regpan = /^([0-9]){10}?$/;
                    if (arrFields[x].fields[y].CntrlType == "TextBox") {
                        value = $("input[id*=" + obj.ControlId + "]").val();
                    }
                    else if (arrFields[x].fields[y].CntrlType == "Dropdown") {
                        value = $("select[id*=" + obj.ControlId + "]").val();
                    }
                    if (value == "" || value == null || value == undefined) {
                        flag = true;
                        $("#" + arrFields[x].fields[y].DivNm).addClass('has-error');

                    }
                    else if (!regpan.test(value)) {
                        flag = true;
                        $("#" + arrFields[x].fields[y].DivNm).addClass('has-error');

                    }
                    else {
                        $("#" + arrFields[x].fields[y].DivNm).removeClass('has-error');
                    }
                }
                else if (arrFields[x].fields[y].FormatType == "PAN") {

                    if (arrFields[x].fields[y].CntrlType == "TextBox") {
                        value = $("input[id*=" + obj.ControlId + "]").val();
                    }
                    else if (arrFields[x].fields[y].CntrlType == "Dropdown") {
                        value = $("select[id*=" + obj.ControlId + "]").val();
                    }
                    var regpan = /^([a-zA-Z]){5}([0-9]){4}([a-zA-Z]){1}?$/;
                    //if (value == "" || value == null || value == undefined) {
                    //    flag = true;
                    //    $("#" + arrFields[x].fields[y].DivNm).addClass('has-error');

                    //}
                    //else if (!regpan.test(value)) {
                    //    flag = true;
                    //    $("#" + arrFields[x].fields[y].DivNm).addClass('has-error');

                    //}
                    //else {
                    //    $("#" + arrFields[x].fields[y].DivNm).removeClass('has-error');
                    //}
                }
                else if (arrFields[x].fields[y].FormatType == "Date") {
                    var regpan = /^(0?[1-9]|1[012])[\/\-](0?[1-9]|[12][0-9]|3[01])[\/\-]\d{4}$/;
                    if (arrFields[x].fields[y].CntrlType == "TextBox") {
                        value = $("input[id*=" + obj.ControlId + "]").val();
                    }
                    else if (arrFields[x].fields[y].CntrlType == "Dropdown") {
                        value = $("select[id*=" + obj.ControlId + "]").val();
                    }
                    if (value == "" || value == null || value == undefined) {
                        flag = true;
                        $("#" + arrFields[x].fields[y].DivNm).addClass('has-error');

                    }
                    else if (!regpan.test(ConvertToDateTime(value))) {
                        flag = true;
                        $("#" + arrFields[x].fields[y].DivNm).addClass('has-error');

                    }
                    else {
                        $("#" + arrFields[x].fields[y].DivNm).removeClass('has-error');
                    }
                }
                else if (arrFields[x].fields[y].FormatType == "Text") {
                    if (arrFields[x].fields[y].CntrlType == "TextBox") {
                        value = $("input[id*=" + obj.ControlId + "]").val();
                    }
                    else if (arrFields[x].fields[y].CntrlType == "TextArea") {
                        value = $("textarea[id*=" + obj.ControlId + "]").val();
                    }
                    else if (arrFields[x].fields[y].CntrlType == "Dropdown") {
                        value = $("select[id*=" + obj.ControlId + "]").val();
                    }

                    if (value == "" || value == null || value == undefined || value == '0') {

                        $("#" + arrFields[x].fields[y].DivNm).addClass('has-error');
                        flag = true;
                    }
                    else {
                        $("#" + arrFields[x].fields[y].DivNm).removeClass('has-error');
                    }

                }
                //alert("value="+value);
            }
        }
    }
    //alert("flag=" + flag);
    return !flag;
}
function ConvertToDateTime(dateTime) {
    if (dateTime == null) {
        return "";
    }
    else {
        var date = dateTime.split(" ")[0];

        return (date.split("/")[1] + "/" + date.split("/")[0] + "/" + date.split("/")[2]);
    }
}