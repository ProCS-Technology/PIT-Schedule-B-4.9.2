jQuery(document).ready(function () {
    fnGetUserNewList();
    $('#UserNewModal').on('hide.bs.modal', function () {
    });
});
function initializeDataTable() {
    $('#tbl-UserNew-setup').DataTable({
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'pdf',
                className: 'btn green btn-outline',
                exportOptions: {
                    columns: [0, 1, 2]
                }
            },
            {
                extend: 'excel',
                className: 'btn yellow btn-outline ',
                exportOptions: {
                    columns: [0, 1, 2]
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
function fnRemoveClass(obj, val, source) {
    $("#lblUploadUsernew" + val + "").removeClass('requied');
}
function fnGetUserNewList() {
    //debugger;
    $("#Loader").hide();
    var webUrl = uri + "/api/UserNew/GetUserNewList";
    $.ajax({
        type: "GET",
        url: webUrl,
        data: JSON.stringify({
            COMPANY_ID: 0
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: true,
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                $("#Loader").hide();
                var result = "";
                //debugger;
                for (var i = 0; i < msg.UserNewHeaderList.length; i++) {
                    result += '<tr id="tr_' + msg.UserNewHeaderList[i].createdBy + '">';
                    result += '<td id="tdUserNewNm_' + msg.UserNewHeaderList[i].createdBy + '">' + msg.UserNewHeaderList[i].createdBy + '</td>';
                    result += '<td>' + (msg.UserNewHeaderList[i].asOfDate) + '</td>';
                    result += '<td><a class="fa fa-download" download href="UserNew/' + msg.UserNewHeaderList[i].fileName + '"></a></td>';
                }
                var table = $('#tbl-UserNew-setup').DataTable();
                table.destroy();
                $("#Loader").hide();
                $("#tbdUserNewDetails").html(result);
                initializeDataTable();
            }
            else {
                if (msg.Msg == "SessionExpired") {
                    $("#Loader").hide();
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
function fnSubmitUserNewFile() {
    fnAddUpdateUserNew();
    //if (fnValidate()) {
    //    fnAddUpdateUserNew();
    //}
}
function fnAddUpdateUserNew() {
    //debugger;
    var isValid = true;
    var fileUploadImageUserNew = $("input[id*='fileUploadImageUserNew']").get(0);
    var excelFile = fileUploadImageUserNew.files;
    if (excelFile.length == 0) {
        //debugger;
        alert("No file choosen to upload document");
        $("#lblUploadUsernew").addClass('required-red');
        isValid = false;
    }
    else {
        var itemFile = $("#fileUploadImageUserNew").get(0).files;
        var arrayExtensions = ["xlsx", "xls"];
        if ($.inArray(itemFile[0].name.split('.').pop().toLowerCase(), arrayExtensions) == -1) {
            isValid = false;
            alert("Only xlsx or xls format is allowed.");
            $("#lblUploadUsernew").addClass('required-red');
        }
        else {
            $("#lblUploadUsernew").removeClass('required-red');
        }
    }
    if (!isValid) {
        return false;
    }
    $("#Loader").show();
    var filesData = new FormData();
    var UserNew_Name = $('#ddlUserName').val();
    var document = $("#fileUploadImageUserNew").get(0).files[0].name;
    var UserID = $('#txtUserId').val();
    if (UserID === "") {
        UserID = 0;
    }
    filesData.append("Object", JSON.stringify({ UserNewHdrId: UserID, UserNewName: UserNew_Name }));
    filesData.append("Files", $("#fileUploadImageUserNew").get(0).files[0]);
    var webUrl = uri + "/api/UserNew/SaveUserNew";
    $.ajax({
        type: 'POST',
        url: webUrl,
        data: filesData,
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        contentType: false,
        async: false,
        processData: false,
        success: function (msg) {
            $('#UserNewModal').modal("hide");
            if (msg.StatusFl == false) {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    alert(msg.Msg);
                    $("#Loader").hide();
                    $('#btnSave').removeAttr("data-dismiss");
                    $('#fileUploadImageUserNew').val("");
                    return false;
                }
            }
            else {
                // UploadFiles(document);
                alert(msg.Msg);
                $("#Loader").hide();
                fnGetUserNewList();
            }
        },
        error: function (error) {
            $('#UserNewModal').hide();
            alert(error.status + ' ' + error.statusText);
        }
    })
}
function fnClearUserNewForm() {
    $('#ddlUserName').removeClass('requied');
    $("#ddlUserName").val('');
    $('#lblUploadUsernew').removeClass('requied');
    $("#fileUploadImageUserNew").val('');
}

function fnEditDeparytment(Department_key, Department_name) {
    $('#ddlUserName').val(UserNew_Name);
    // $('#txtDepartmentKey').val(Department_key);
}
function closeme() {
    document.forms["form1"].submit();
    window.close();
}
function fnCloseModal() {
    fnClearUserNewForm
}
//function isValidPanCardNo(panCardNo) {
//    debugger;
//    // Regex to check valid
//    // PAN Number
//    let regex = new RegExp(/^[A-Z]{5}[0-9]{4}[A-Z]{1}$/);
//    // if PAN Number
//    // is empty return false
//    if (panCardNo == null) {
//        return "false";
//    }
//    // Return true if the PAN NUMBER
//    // matched the ReGex
//    if (regex.test(panCardNo) == true) {
//        return "true";
//    }
//    else {
//        return "false";
//    }
//}