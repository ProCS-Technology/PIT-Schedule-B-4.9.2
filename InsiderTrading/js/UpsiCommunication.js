jQuery(document).ready(function () {
    fnGetCommunicationtypeList();
    $('#stack1').on('hide.bs.modal', function () {
    });
});


function initializeDataTable() {
    $('#tbl-commtype-setup').DataTable({
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





function fnGetCommunicationtypeList() {
    debugger;
    $("#Loader").show();
    var webUrl = uri + "/api/UpsiCommunication/GetCommunicationtypeList";
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
                for (var i = 0; i < msg.CommunicationtypeList.length; i++) {
                    result += '<tr id="tr_' + msg.CommunicationtypeList[i].COMMTYPE_ID + '">';
                    result += '<td id="tdcommunicationtype_Name_' + msg.CommunicationtypeList[i].COMMTYPE_ID + '">' + msg.CommunicationtypeList[i].COMMTYPE_NAME + '</td>';
                    result += '<td id="tdEditDelete_' + msg.CommunicationtypeList[i].COMMTYPE_ID + '"><a data-target="#stack1" data-toggle="modal" id="a_' + msg.CommunicationtypeList[i].COMMTYPE_ID + '" class="btn btn-outline dark" onclick=\"javascript:fnEditCommunicationtype(' + msg.CommunicationtypeList[i].COMMTYPE_ID + ',\'' + msg.CommunicationtypeList[i].COMMTYPE_NAME + '\');\">Edit</a><a style="margin-left:20px" data-target="#delete" data-toggle="modal" id="d_' + msg.CommunicationtypeList[i].COMMTYPE_ID + '" class="btn btn-outline dark" onclick=\"javascript:fnDeleteCommunicationtype(' + msg.CommunicationtypeList[i].COMMTYPE_ID + ');\">Inactive</a></td>';
                    result += '</tr>';
                }

                var table = $('#tbl-commtype-setup').DataTable();
                table.destroy();
                $("#tbdcommtypeList").html(result);
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

function fnSaveCommunicationtype() {
    if (fnValidate()) {
        fnAddUpdateCommunicationtype();
    }
    else {
        $('#btnSave').removeAttr("data-dismiss");
        return false;
    }
}

function fnAddUpdateCommunicationtype() {
    debugger;
    var commtype_Nm = $('#txtCommunicationtype').val();
    var commtype_id = $('#txtCommunicationtypeKey').val();
    if (commtype_id === "") {
        commtype_id = 0;
    }

    $("#Loader").show();
    var webUrl = uri + "/api/UpsiCommunication/SaveCommunicationtype";
    $.ajax({
        type: 'POST',
        url: webUrl,
        data: JSON.stringify({
            COMMTYPE_ID: commtype_id, COMMTYPE_NAME: commtype_Nm
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            window.location.reload(true);

           
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
                if (msg.CommunicationtypeList.COMMTYPE_ID == commtype_id) {
                    $("#tdcommunicationtype_Name_" + msg.CommunicationtypeList.COMMTYPE_ID).html(msg.CommunicationtypeList.COMMTYPE_NAME);
                    $("#a_" + msg.CommunicationtypeList.COMMTYPE_ID).attr("onclick", "javascript:fnEditCommunicationtype('" + msg.CommunicationtypeList.COMMTYPE_ID + "','" + msg.CommunicationtypeList.COMMTYPE_NAME + "');");
                    $("#a_" + msg.CommunicationtypeList.COMMTYPE_ID).attr("data-target", "#stack1");
                    $("#a_" + msg.CommunicationtypeList.COMMTYPE_ID).attr("data-toggle", "modal");
                    $("#d_" + msg.CommunicationtypeList.COMMTYPE_ID).attr("onclick", "javascript:fnDeleteCommunicationtype('" + msg.CommunicationtypeList.COMMTYPE_ID + "');");
                    $("#d_" + msg.CommunicationtypeList.COMMTYPE_ID).css({ 'margin-left': '20px' });
                    $("#d_" + msg.CommunicationtypeList.COMMTYPE_ID).attr("data-target", "#delete");
                    $("#d_" + msg.CommunicationtypeList.COMMTYPE_ID).attr("data-target", "#modal");
                    var table = $('#tbl-commtype-setup').DataTable();
                    table.destroy();
                    initializeDataTable();
                    $("#Loader").hide();
                }
                else {
                    var result = "";
                    result += '<tr id="tr_' + msg.CommunicationtypeList.COMMTYPE_ID + '">';
                    result += '<td id="tdcommunicationtype_Name_' + msg.CommunicationtypeList.COMMTYPE_ID + '">' + msg.CommunicationtypeList.COMMTYPE_NAME + '</td>';

                    //result += '<td id="tdEditDelete_' + CommunicationtypeList.COMMTYPE_ID + '"><a data-target="#stack1" data-toggle="modal" id="a_' + CommunicationtypeList.COMMTYPE_ID + '" class="btn btn-outline dark" onclick=\"javascript:fnEditCommunicationtype(' + CommunicationtypeList.COMMTYPE_ID + ',\'' + CommunicationtypeList.COMMTYPE_NAME + '\');\">Edit</a><a style="margin-left:20px" data-target="#delete" data-toggle="modal" id="d_' + CommunicationtypeList.COMMTYPE_ID + '" class="btn btn-outline dark" onclick=\"javascript:fnDeleteCommunicationtype(' + CommunicationtypeList.COMMTYPE_ID + ');\">Delete</a></td>';
                    //result += '</tr>';

                    result += '<td id="tdEditDelete_' + msg.CommunicationtypeList.COMMTYPE_ID + '"><a data-target="#stack1" data-toggle="modal" id="a_' + msg.CommunicationtypeList.COMMTYPE_ID + '" class="btn btn-outline dark" onclick=\"javascript:fnEditCommunicationtype(' + msg.CommunicationtypeList.COMMTYPE_ID + ',\'' + msg.CommunicationtypeList.COMMTYPE_NAME + '\');\">Edit</a><a style="margin-left:20px" data-target="#delete" data-toggle="modal" id="d_' + msg.CommunicationtypeList.COMMTYPE_ID + '" class="btn btn-outline dark" onclick=\"javascript:fnDeleteCommunicationtype(' + msg.CommunicationtypeList.COMMTYPE_ID + ');\">Inactive</a></td>';
                    result += '</tr>';

                    var table = $('#tbl-commtype-setup').DataTable();
                    table.destroy();
                    $("#tbdcommtypeList").append(result);
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

function fnEditCommunicationtype(COMMTYPE_ID, COMMTYPE_NAME) {
    //alert(COMMTYPE_NAME);
    $('#txtCommunicationtype').val(COMMTYPE_NAME);
    $('#txtCommunicationtypeKey').val(COMMTYPE_ID);
}

function fnAddCommunicationtype() {

}

function fnCloseModal() {
    fnClearForm();
}

function fnClearForm() {
    $('#txtCommunicationtype').val('');
    $('#txtCommunicationtypeKey').val('');
}

function fnValidate() {
    if ($('#txtCommunicationtype').val().trim() == "" || $('#txtCommunicationtype').val() == null || $('#txtCommunicationtype').val() == undefined) {
        alert("Please enter type name");
        return false;
    }
    else if (!/^[a-zA-Z ]*$/.test($('#txtCommunicationtype').val())) {
        alert("Please enter only characters");
        return false;
    }
    return true;
}

function fnDeleteCommunicationtype(CommunicationtypeKey) {
    $("#Loader").show();
    var webUrl = uri + "/api/UpsiCommunication/DeleteCommunicationtype";
    $.ajax({
        type: 'POST',
        url: webUrl,
        data: JSON.stringify({
            COMMTYPE_ID: CommunicationtypeKey
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            window.location.reload(true);
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                alert("Record Deleted successfully !");
                var table = $('#tbl-commtype-setup').DataTable();
                table.destroy();
                $("#tr_" + msg.CommunicationtypeList.COMMTYPE_ID).remove();
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