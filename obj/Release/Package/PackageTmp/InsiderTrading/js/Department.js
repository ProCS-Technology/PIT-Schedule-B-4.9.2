
jQuery(document).ready(function () {
    fnGetDepartmentList();
    $('#stack1').on('hide.bs.modal', function () {
    });
    
});

function initializeDataTable() {
    $('#tbl-Department-setup').DataTable({
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
        async: true,
        success: function (msg) {
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                var result = "";
                for (var i = 0; i < msg.DepartmentList.length; i++) {
                    result += '<tr id="tr_' + msg.DepartmentList[i].DEPARTMENT_ID + '">';
                    result += '<td id="tdDepartmentNm_' + msg.DepartmentList[i].DEPARTMENT_ID + '">' + msg.DepartmentList[i].DEPARTMENT_NM + '</td>';
                    result += '<td id="tdEditdelete_' + msg.DepartmentList[i].DEPARTMENT_ID + '"><a data-target="#stack1" data-toggle="modal" id="a' + msg.DepartmentList[i].DEPARTMENT_ID + '" class="btn btn-outline dark" onclick=\"javascript:fnEditDepartment(' + msg.DepartmentList[i].DEPARTMENT_ID + ',\'' + msg.DepartmentList[i].DEPARTMENT_NM + '\');\">Edit</a><a style="margin-left:20px" data-target="#delete" data-toggle="modal" id="d' + msg.DepartmentList[i].DEPARTMENT_ID + '" class="btn btn-outline dark" onclick=\"javascript:DeleteDepartment(' + msg.DepartmentList[i].DEPARTMENT_ID + ');\">Delete</a></td>';

                    result += '</tr>';
                }

                var table = $('#tbl-Department-setup').DataTable();
                table.destroy();
                $("#tbdDepartmentList").html(result);
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

function fnAddDepartment() {

}

function fnSaveDepartment() {
    if (fnValidate()) {
        debugger;
        fnAddUpdateDepartment();
    }
    else {
        $('#btnSave').removeAttr("data-dismiss");
        return false;
    }
}

function fnAddUpdateDepartment() {
    var DepartmentID = $('#txtDepartmentKey').val();
    var DepartmentName = $('#txtDepartmentName').val();

    if (DepartmentID === "") {
        DepartmentID = 0;
    }
    $("#Loader").show();
    var webUrl = uri + "/api/Department/SaveDepartment";
    $.ajax({
        type: 'POST',
        url: webUrl,
        data: JSON.stringify({
            DEPARTMENT_ID: DepartmentID, Department_NM: DepartmentName
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == false) {
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
            else {
                alert(msg.Msg);
                if (msg.Department.DEPARTMENT_ID == DepartmentID) {
                    $("#tdDepartmentNm_" + msg.Department.DEPARTMENT_ID).html(msg.Department.DEPARTMENT_NM);
                    $("#a" + msg.Department.DEPARTMENT_ID).attr("onclick", "javascript:fnEditDepartment('" + msg.Department.DEPARTMENT_ID + "','" + msg.Department.DEPARTMENT_NM + "');");
                    $("#a" + msg.Department.DEPARTMENT_ID).attr("data-target", "#stack1");
                    $("#a" + msg.Department.DEPARTMENT_ID).attr("data-toggle", "modal");

                    $("#d" + msg.Department.DEPARTMENT_ID).attr("onclick", "javascript:DeleteDepartment('" + msg.Department.DEPARTMENT_ID + "');");
                    $("#d" + msg.Department.DEPARTMENT_ID).attr("data-target", "#delete");
                    $("#d" + msg.Department.DEPARTMENT_ID).attr("data-toggle", "modal");
                    $("#d" + msg.Department.DEPARTMENT_ID).css({ 'margin-left': '20px' });

                    var table = $('#tbl-Department-setup').DataTable();
                    table.destroy();
                    initializeDataTable();
                    $("#Loader").hide();
                }
                else {
                    var result = "";
                    result += '<tr id="tr_' + msg.Department.DEPARTMENT_ID + '">';
                    result += '<td id="tdDepartmentNm_' + msg.Department.DEPARTMENT_ID + '">' + msg.Department.DEPARTMENT_NM + '</td>';
                    result += '<td id="tdEditdelete_' + msg.Department.DEPARTMENT_ID + '"><a data-target="#stack1" data-toggle="modal" id="a' + msg.Department.DEPARTMENT_ID + '" class="btn btn-outline dark" onclick=\"javascript:fnEditDepartment(' + msg.Department.DEPARTMENT_ID + ',\'' + msg.Department.DEPARTMENT_NM + '\');\">Edit</a><a style="margin-left:20px" data-target="#delete" data-toggle="modal" id="d' + msg.Department.DEPARTMENT_ID + '" class="btn btn-outline dark" onclick=\"javascript:DeleteDepartment(' + msg.Department.DEPARTMENT_ID + ');\">Delete</a></td>';
                    result += '</tr>';
                    var table = $('#tbl-Department-setup').DataTable();
                    table.destroy();
                    $("#tbdDepartmentList").append(result);
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
    debugger;
    var Department = $('#txtDepartmentName').val();
    if (Department == '' || Department == null || Department == '0') {
        $('#lblDepartment').addClass('requied');
        return false;
    }
    else if (!/^[a-zA-Z ]*$/.test(Department)) {
        alert("Please enter only characters");
        return false;
    }
    else {
        $('#lblDepartment').removeClass('requied');
    }
    return true;
}

function fnClearForm() {
    $('#txtDepartmentName').val("");
    $('#txtDepartmentKey').val("");
}

function fnEditDepartment(Department_key, Department_name) {
    $('#txtDepartmentName').val(Department_name);
    $('#txtDepartmentKey').val(Department_key);
}

function DeleteDepartment(Department_key) {
    $("#Loader").show();
    var webUrl = uri + "/api/Department/DeleteDepartment";
    $.ajax({
        type: 'POST',
        url: webUrl,
        data: JSON.stringify({
            Department_ID: Department_key
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                alert("Record Deleted successfully !");
                var table = $('#tbl-Department-setup').DataTable();
                table.destroy();
                $("#tr_" + msg.Department.DEPARTMENT_ID).remove();
                initializeDataTable();
                $("#Loader").hide();
                fnGetDepartmentList();
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
