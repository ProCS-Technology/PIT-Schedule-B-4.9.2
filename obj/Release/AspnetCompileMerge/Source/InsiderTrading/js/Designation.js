jQuery(document).ready(function () {
    fnGetDesignationList();
    $('#stack1').on('hide.bs.modal', function () {
    });
});

function initializeDataTable() {
    $('#tbl-Designation-setup').DataTable({
        //  "dom": '<"top"iflp<"clear">>rt<"bottom"iflp<"clear">>',
        dom: 'Bfrtip',
        pageLength: 10,
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
        async: true,
        success: function (msg) {
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                var result = "";
                for (var i = 0; i < msg.DesignationList.length; i++) {
                    result += '<tr id="tr_' + msg.DesignationList[i].DESIGNATION_ID + '">';
                    //result += '<td style="display:none" id="tdDesignation_Id_' + msg.DesignationList[i].DESIGNATION_ID + '">' + msg.DesignationList[i].DESIGNATION_ID + '</td>';
                    result += '<td id="tdDesignation_Name_' + msg.DesignationList[i].DESIGNATION_ID + '">' + msg.DesignationList[i].DESIGNATION_NM + '</td>';
                    result += '<td id="tdEditDelete_' + msg.DesignationList[i].DESIGNATION_ID + '"><a data-target="#stack1" data-toggle="modal" id="a_' + msg.DesignationList[i].DESIGNATION_ID + '" class="btn btn-outline dark" onclick=\"javascript:fnEditDesignation(' + msg.DesignationList[i].DESIGNATION_ID + ',\'' + msg.DesignationList[i].DESIGNATION_NM + '\');\">Edit</a><a style="margin-left:20px" data-target="#delete" data-toggle="modal" id="d_' + msg.DesignationList[i].DESIGNATION_ID + '" class="btn btn-outline dark" onclick=\"javascript:fnDeleteDesignation(' + msg.DesignationList[i].DESIGNATION_ID + ');\">Delete</a></td>';
                    result += '</tr>';
                }

                var table = $('#tbl-Designation-setup').DataTable();
                table.destroy();
                $("#tbdDesignationList").html(result);
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

function fnSaveDesignation() {
    if (fnValidate()) {
        fnAddUpdateDesignation();
    }
    else {
        $('#btnSave').removeAttr("data-dismiss");
        return false;
    }
}

function fnAddUpdateDesignation() {
    var designation_Nm = $('#txtDesignationName').val();
    var designation_Id = $('#txtDesignationKey').val();
    if (designation_Id === "") {
        designation_Id = 0;
    }

    $("#Loader").show();
    var webUrl = uri + "/api/DesignationIT/SaveDesignation";
    $.ajax({
        type: 'POST',
        url: webUrl,
        data: JSON.stringify({
            DESIGNATION_ID: designation_Id, DESIGNATION_NM: designation_Nm
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
                    $('#btnSave').removeAttr("data-dismiss");
                    return false;
                }
            }
            else {
                alert(msg.Msg);
                if (msg.Designation.DESIGNATION_ID == designation_Id) {
                    $("#tdDesignation_Name_" + msg.Designation.DESIGNATION_ID).html(msg.Designation.DESIGNATION_NM);
                    $("#a_" + msg.Designation.DESIGNATION_ID).attr("onclick", "javascript:fnEditDesignation('" + msg.Designation.DESIGNATION_ID + "','" + msg.Designation.DESIGNATION_NM + "');");
                    $("#a_" + msg.Designation.DESIGNATION_ID).attr("data-target", "#stack1");
                    $("#a_" + msg.Designation.DESIGNATION_ID).attr("data-toggle", "modal");
                    $("#d_" + msg.Designation.DESIGNATION_ID).attr("onclick", "javascript:fnDeleteDesignation('" + msg.Designation.DESIGNATION_ID + "');");
                    $("#d_" + msg.Designation.DESIGNATION_ID).css({ 'margin-left': '20px' });
                    $("#d_" + msg.Designation.DESIGNATION_ID).attr("data-target", "#delete");
                    $("#d_" + msg.Designation.DESIGNATION_ID).attr("data-target", "#modal");
                    var table = $('#tbl-Designation-setup').DataTable();
                    table.destroy();
                    initializeDataTable();
                    $("#Loader").hide();
                }
                else {
                    var result = "";
                    result += '<tr id="tr_' + msg.Designation.DESIGNATION_ID + '">';
                    result += '<td id="tdDesignation_Name_' + msg.Designation.DESIGNATION_ID + '">' + msg.Designation.DESIGNATION_NM + '</td>';

                    result += '<td id="tdEditDelete_' + msg.Designation.DESIGNATION_ID + '"><a data-target="#stack1" data-toggle="modal" id="a_' + msg.Designation.DESIGNATION_ID + '" class="btn btn-outline dark" onclick=\"javascript:fnEditDesignation(' + msg.Designation.DESIGNATION_ID + ',\'' + msg.Designation.DESIGNATION_NM + '\');\">Edit</a><a style="margin-left:20px" data-target="#delete" data-toggle="modal" id="d_' + msg.Designation.DESIGNATION_ID + '" class="btn btn-outline dark" onclick=\"javascript:fnDeleteDesignation(' + msg.Designation.DESIGNATION_ID + ');\">Delete</a></td>';
                    result += '</tr>';
                    var table = $('#tbl-Designation-setup').DataTable();
                    table.destroy();
                    $("#tbdDesignationList").append(result);
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

function fnEditDesignation(DESIGNATION_ID, DESIGNATION_NM) {
    $('#txtDesignationName').val(DESIGNATION_NM);
    $('#txtDesignationKey').val(DESIGNATION_ID);
}

function fnAddDesignation() {

}

function fnCloseModal() {
    fnClearForm();
}

function fnClearForm() {
    $('#txtDesignationName').val('');
    $('#txtDesignationKey').val('');
}

function fnValidate() {
    if ($('#txtDesignationName').val().trim() == "" || $('#txtDesignationName').val() == null || $('#txtDesignationName').val() == undefined) {
        alert("Please enter designation name");
        return false;
    }
    else if (!/^[a-zA-Z ]*$/.test($('#txtDesignationName').val())) {
        alert("Please enter only characters");
        return false;
    }
    return true;
}

function fnDeleteDesignation(designationKey) {
    $("#Loader").show();
    var webUrl = uri + "/api/DesignationIT/DeleteDesignation";
    $.ajax({
        type: 'POST',
        url: webUrl,
        data: JSON.stringify({
            DESIGNATION_ID: designationKey
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
                var table = $('#tbl-Designation-setup').DataTable();
                table.destroy();
                $("#tr_" + msg.Designation.DESIGNATION_ID).remove();
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
                }
            }
        },
        error: function (response) {
            $("#Loader").hide();
            alert(response.status + ' ' + response.statusText);
        }
    });
}