jQuery(document).ready(function () {
    fnGetUPSIType();
    $('#stack1').on('hide.bs.modal', function () {
    });
});
function fnGetUPSIType() {
    $("#Loader").show();
    var webUrl = uri + "/api/UPSIGroup/GetUPSITypeList";
    $.ajax({
        type: "GET",
        url: webUrl,
        data: JSON.stringify({
            CompanyId: 0
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
                for (var i = 0; i < msg.UPSITypeList.length; i++) {
                    result += '<tr id="tr_' + msg.UPSITypeList[i].TypeId + '">';
                    result += '<td id="tdUPSITypeNm_' + msg.UPSITypeList[i].TypeId + '">' + msg.UPSITypeList[i].TypeNm + '</td>';
                    result += '<td id="tdUPSITypeStatus_' + msg.UPSITypeList[i].TypeId + '">' + msg.UPSITypeList[i].Status + '</td>';
                    result += '<td id="tdEditdelete_' + msg.UPSITypeList[i].TypeId + '"><a data-target="#stack1" data-toggle="modal" id="a' + msg.UPSITypeList[i].TypeId + '" class="btn btn-outline dark" onclick=\"javascript:fnEditUPSIType(' + msg.UPSITypeList[i].TypeId + ',\'' + msg.UPSITypeList[i].TypeNm + '\',\'' + msg.UPSITypeList[i].Status + '\');\">Edit</a></td>';
                    result += '</tr>';
                }

                var table = $('#tbl-UPSIType-setup').DataTable();
                table.destroy();
                $("#tbdUPSITypeList").html(result);
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
function initializeDataTable() {
    $('#tbl-UPSIType-setup').DataTable({
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
function fnSaveUPSIType() {
    if (fnValidate()) {
        fnAddUpdateUPSIType();
    }
    else {
        $('#btnSave').removeAttr("data-dismiss");
        return false;
    }
}
function fnAddUpdateUPSIType() {
    var TypeId = $('#txtUPSITypeId').val();
    var TypeNm = $('#txtUPSIType').val();
    var Status = $('#ddlStatus').val();

    if (TypeId === "") {
        TypeId = 0;
    }
    $("#Loader").show();
    var webUrl = uri + "/api/UPSIGroup/SaveUPSIType";
    var token = $("#TokenKey").val();
    $.ajax({
        type: 'POST',
        headers: {
            'TokenKeyH': token,

        },
        url: webUrl,
        data: JSON.stringify({
            TypeId: TypeId, TypeNm: TypeNm, Status: Status
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
                window.location.reload();
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
    var UPSIType = $('#txtUPSIType').val();
    if (UPSIType == '' || UPSIType == null || UPSIType == '0') {
        $('#lblUPSIType').addClass('requied');
        return false;
    }
    else {
        $('#lblUPSIType').removeClass('requied');
    }
    var UPSIStatus = $('#ddlStatus').val();
    if (UPSIStatus == '' || UPSIStatus == null || UPSIStatus == '0') {
        $('#lblStatus').addClass('requied');
        return false;
    }
    else {
        $('#lblStatus').removeClass('requied');
    }
    return true;
}
function fnClearForm() {
    $('#txtUPSITypeId').val("");
    $('#txtUPSIType').val("");
    $('#ddlStatus').val("");
}
function fnEditUPSIType(TypeId, TypeNm, Status) {
    $('#txtUPSITypeId').val(TypeId);
    $('#txtUPSIType').val(TypeNm);
    $('#ddlStatus').val(Status);
}
function fnCloseModal() {
    fnClearForm();
}