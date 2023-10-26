jQuery(document).ready(function () {
    $('#txtstartDt').datepicker({
        todayHighlight: true,
        autoclose: true,
        format: $("input[id*=hdnJSDateFormat]").val(),
        clearBtn: true,
        //startDate: "today"
    });

    fnGetAnnualDisclosureTaskList();
    $('#stack1').on('hide.bs.modal', function () { });
});
function initializeDataTable() {
    $('#tbl-AnnualDisclosureTask-setup').DataTable({
        dom: 'Bfrtip',
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
function fnGetAnnualDisclosureTaskList() {
    $("#Loader").show();
    var webUrl = uri + "/api/AnnualDisclosureTask/GetAnnualDisclosureTaskClosureInfoList";
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
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                var result = "";
                for (var i = 0; i < msg.AnnualDisclosureTaskList.length; i++) {
                    //alert(msg.AnnualDisclosureTaskList[i].STARTDATE);
                    //alert($("input[id*=hdnDateFormat]").val());
                    result += '<tr id="tr_' + msg.AnnualDisclosureTaskList[i].id + '">';
                    result += '<td id="tdAnnualDisclosureTaskfy_' + msg.AnnualDisclosureTaskList[i].id + '">' + msg.AnnualDisclosureTaskList[i].FINANCIALYEARS + '</td>';
                    result += '<td id="tdAnnualDisclosureTaskT_' + msg.AnnualDisclosureTaskList[i].id + '">' + msg.AnnualDisclosureTaskList[i].Title + '</td>';
                    result += '<td id="tdAnnualDisclosureTasksd_' + msg.AnnualDisclosureTaskList[i].id + '">' + FormatDate(msg.AnnualDisclosureTaskList[i].STARTDATE, $("input[id*=hdnDateFormat]").val()) + '</td>';
                    result += '<td id="tdAnnualDisclosureTasktd_' + msg.AnnualDisclosureTaskList[i].id + '">' + msg.AnnualDisclosureTaskList[i].TILLDATE + '</td>';
                    result += '<td style="width:160px" id="tdEditdelete_' + msg.AnnualDisclosureTaskList[i].TASK_ID + '"><a data-target="#stack1" data-toggle="modal" id="a' + msg.AnnualDisclosureTaskList[i].id + '" class="btn btn-outline dark" onclick="javascript:fnEditAnnualDisclosureTask(\'' + msg.AnnualDisclosureTaskList[i].id + '\',\'' + msg.AnnualDisclosureTaskList[i].FINANCIALYEARS + '\',\'' + msg.AnnualDisclosureTaskList[i].Title + '\',\'' + FormatDate(msg.AnnualDisclosureTaskList[i].STARTDATE, $("input[id*=hdnDateFormat]").val()) + '\',\'' + msg.AnnualDisclosureTaskList[i].TILLDATE + '\');">Edit</a><a style="margin-left:20px" data-target="#delete" data-toggle="modal" id="d' + msg.AnnualDisclosureTaskList[i].id + '" class="btn btn-outline dark" onclick=\"javascript:fnDeleteAnnualDisclosureTask(' + msg.AnnualDisclosureTaskList[i].id + ');\">Delete</a></td>';
                    result += '</tr>';
                }
                var table = $('#tbl-AnnualDisclosureTask-setup').DataTable();
                table.destroy();
                $("#tbdAnnualDisclosureTaskList").html(result);

                $("#Loader").hide();
                initializeDataTable();

            }
            else {
                $("#Loader").hide();
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    //alert(msg.Msg);
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
function fnAddAnnualDisclosureTask() {
}
function fnSaveAnnualDisclosureTask() {
    if (fnValidate()) {
        fnAddUpdateAnnualDisclosureTask();
    }
    else {
        $('#btnSave').removeAttr("data-dismiss");
        return false;
    }
}
function fnAddUpdateAnnualDisclosureTask() {
    var ID = $('#txtAnnualDisclosureTaskId').val();
    var FINANCIAL_YEARS = $('#txtAnnualDisclosureTaskName').val();
    var Title = $('#txtTitleName').val();
    var TASK_START_DATE = $('#txtstartDt').val();
    var TASK_END_DATE = $('#txtendDt').val();

    if (ID === "") {
        ID = 0;
    }
    $("#Loader").show();
    var webUrl = uri + "/api/AnnualDisclosureTask/SaveAnnualDisclosureTask";
    $.ajax({

        type: 'POST',
        url: webUrl,
        data: JSON.stringify({

            id: ID, FINANCIALYEARS: FINANCIAL_YEARS, Title: Title, STARTDATE: TASK_START_DATE, TILLDATE: TASK_END_DATE
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
                    return false;
                }
            }
            else {
                alert(msg.Msg);
                if (msg.AnnualDisclosureTask.id == ID) {
                    $("#tdAnnualDisclosureTaskfy_" + msg.AnnualDisclosureTask.id).html(msg.AnnualDisclosureTask.FINANCIALYEARS);
                    $("#tdAnnualDisclosureTaskT_" + msg.AnnualDisclosureTask.id).html(msg.AnnualDisclosureTask.Title);
                    $("#tdAnnualDisclosureTasksd_" + msg.AnnualDisclosureTask.id).html(msg.AnnualDisclosureTask.STARTDATE);
                    $("#tdAnnualDisclosureTasktd_" + msg.AnnualDisclosureTask.id).html(msg.AnnualDisclosureTask.TILLDATE);

                    $("#a" + msg.AnnualDisclosureTask.id).attr("onclick", "javascript:fnEditAnnualDisclosureTask('" + msg.AnnualDisclosureTask.id + "','" + msg.AnnualDisclosureTask.FINANCIALYEARS + "','" + msg.AnnualDisclosureTask.Title + "','" + msg.AnnualDisclosureTask.STARTDATE + "','" + msg.AnnualDisclosureTask.TILLDATE + "');");
                    $("#a" + msg.AnnualDisclosureTask.id).attr("data-target", "#stack1");
                    $("#a" + msg.AnnualDisclosureTask.id).attr("data-toggle", "modal");
                    $("#d" + msg.AnnualDisclosureTask.id).attr("onclick", "javascript:fnDeleteAnnualDisclosureTask(" + msg.AnnualDisclosureTask.id + ");");
                    $("#d" + msg.AnnualDisclosureTask.id).attr("data-target", "#delete");
                    $("#d" + msg.AnnualDisclosureTask.id).attr("data-toggle", "modal");
                    $("#d" + msg.AnnualDisclosureTask.id).css({ 'margin-left': '20px' });
                    var table = $('#tbl-AnnualDisclosureTask-setup').DataTable();
                    table.destroy();
                    initializeDataTable();
                    $("#Loader").hide();
                }
                else {
                    var result = "";
                    result += '<tr id="tr_' + msg.AnnualDisclosureTask.id + '">';
                    $("#tdAnnualDisclosureTaskfy_" + msg.AnnualDisclosureTask.id).html(msg.AnnualDisclosureTask.FINANCIALYEARS);
                    $("#tdAnnualDisclosureTaskT_" + msg.AnnualDisclosureTask.id).html(msg.AnnualDisclosureTask.Title);
                    $("#tdAnnualDisclosureTasksd_" + msg.AnnualDisclosureTask.id).html(msg.AnnualDisclosureTask.STARTDATE);
                    $("#tdAnnualDisclosureTasktd_" + msg.AnnualDisclosureTask.id).html(msg.AnnualDisclosureTask.TILLDATE);

                    result += '<td  id="tdEditdelete_' + msg.AnnualDisclosureTask.id + '"><a data-target="#stack1" data-toggle="modal" id="a' + msg.AnnualDisclosureTask.id + '" class="btn btn-outline dark" onclick=\"javascript:fnEditAnnualDisclosureTask(' + msg.AnnualDisclosureTaskList[i].id + ',\'' + msg.AnnualDisclosureTaskList[i].FINANCIALYEARS + ',\'' + msg.AnnualDisclosureTaskList[i].Title + ',\'' + msg.AnnualDisclosureTaskList[i].STARTDATE + ',\'' + msg.AnnualDisclosureTaskList[i].TILLDATE + '\');\">Edit</a><a style="margin-left:20px padding: unset;" data-target="#delete" data-toggle="modal" id="d' + msg.AnnualDisclosureTask.TASK_ID + '" class="btn btn-outline dark" onclick=\"javascript:fnDeleteAnnualDisclosureTask(' + msg.AnnualDisclosureTask.TASK_ID + ');\">Delete</a></td>';
                    result += '</tr>';
                    var table = $('#tbl-AnnualDisclosureTask-setup').DataTable();
                    table.destroy();
                    $("#tbdAnnualDisclosureTaskList").append(result);
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
    var FINANCIAL_YEARS = $('#txtAnnualDisclosureTaskName').val();
    var Title = $('#txtTitleName').val();
    var TASK_START_DATE = $('#txtstartDt').val();
    var TASK_END_DATE = $('#txtendDt').val();
    if (FINANCIAL_YEARS == '' || FINANCIAL_YEARS == null || FINANCIAL_YEARS == '0') {
        $('#lblFinancialYear').addClass('requied');
        return false;
    }
    else {
        $('#lblFinancialYear').removeClass('requied');
    }
    if (Title == '' || Title == null || Title == '0') {
        $('#lblTitle').addClass('requied');
        return false;
    }
    else {
        $('#lblTitle').removeClass('requied');
    }
    if (TASK_START_DATE == '' || TASK_START_DATE == null || TASK_START_DATE == '0') {
        $('#lblTASK_START_DATE').addClass('requied');
        return false;
    }
    else {
        $('#lblTASK_START_DATE').removeClass('requied');
    }
    return true;
    if (TASK_END_DATE == '' || TASK_END_DATE == null || TASK_END_DATE == '0') {
        $('#lblTASK_END_DATE').addClass('requied');
        return false;
    }
    else {
        $('#lblTASK_END_DATE').removeClass('requied');
    }
    return true;
}
function fnClearForm() {
    $('#txtAnnualDisclosureTaskName').val("");
    $('#txtTitleName').val("");
    $('#txtstartDt').val("");
    $('#txtendDt').val("");
}
function fnEditAnnualDisclosureTask(id, FINANCIALYEARS, Title, STARTDATE, TILLDATE) {
    $('#txtAnnualDisclosureTaskId').val(id);
    $('#txtAnnualDisclosureTaskName').val(FINANCIALYEARS);
    $('#txtTitleName').val(Title);
    $('#txtstartDt').val(STARTDATE);
    $('#txtendDt').val(TILLDATE);
}
function fnDeleteAnnualDisclosureTask(AnnualDisclosureTaskKey) {
    var webUrl = uri + "/api/AnnualDisclosureTask/DeleteAnnualDisclosureTask";
    $.ajax({
        type: 'POST',
        url: webUrl,
        data: JSON.stringify({
            id: AnnualDisclosureTaskKey
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
                var table = $('#tbl-AnnualDisclosureTask-setup').DataTable();
                table.destroy();
                $("#tr_" + msg.AnnualDisclosureTask.id).remove();
                initializeDataTable();
                $("#Loader").hide();
                fnGetAnnualDisclosureTaskList();
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