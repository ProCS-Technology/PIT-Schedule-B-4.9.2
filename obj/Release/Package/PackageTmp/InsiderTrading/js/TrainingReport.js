$(document).ready(function () {
    

    $("#Loader").hide();
    fnBindTrainingList();
    var table = $('#tbl-trainingReport-setup').DataTable();
    table.destroy();
    $("#tbdTrainingReportList").html('');
    initializeDataTable('tbl-trainingReport-setup', [0, 1, 2, 3, 4, 5]);

    $("#bindTrainings").select2({
        placeholder: "Select a training"
    });
})

function initializeDataTable(id, columns) {
    $('#' + id).DataTable({
        dom: 'Bfrtip',
        pageLength: 10,
        "scrollY": "300px",
        "scrollX": true,
        buttons: [
            {
                extend: 'pdf',
                className: 'btn green btn-outline',
                exportOptions: {
                    columns: columns
                }
            },
            {
                extend: 'excel',
                className: 'btn yellow btn-outline ',
                exportOptions: {
                    columns: columns
                }
            }
        ]
    });
}

function fnBindTrainingList() {
    $("#Loader").show();
    var webUrl = uri + "/api/Training/GetTrainingModules";
    $.ajax({
        type: "GET",
        url: webUrl,
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            if (msg.StatusFl == true) {
                var result = "";

                if (msg.TrainingModuleList !== null) {
                    for (var i = 0; i < msg.TrainingModuleList.length; i++) {
                        result += '<option value="' + msg.TrainingModuleList[i].trainingId + '">' + msg.TrainingModuleList[i].trainingName + '</option>';
                    }
                }

                $("#bindTrainings").html(result);
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

function fnGetTrainingReport() {
    if (fnValidate()) {
        $("#Loader").show();
        var webUrl = uri + "/api/Training/GetTrainingReport";
        $.ajax({
            type: "POST",
            url: webUrl,
            data: JSON.stringify({ trainingId: $("#bindTrainings").val(), trainingFrom: $("#txtFromDate").val(), trainingTo: $("#txtToDate").val() }),
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            success: function (msg) {
                var result = "";
                if (msg.StatusFl) {
                    if (msg.TrainingModuleList !== null) {
                        if (msg.TrainingModuleList.length > 0) {
                            for (var i = 0; i < msg.TrainingModuleList.length; i++) {
                                result += '<tr id="trainingModule_"' + msg.TrainingModuleList[i].trainingId + '>';
                                result += '<td>' + msg.TrainingModuleList[i].trainingModuleUserStatus.userDetail.USER_NM + '</td>';
                                result += '<td>' + msg.TrainingModuleList[i].trainingModuleUserStatus.userDetail.USER_EMAIL + '</td>';
                                result += '<td>' + msg.TrainingModuleList[i].trainingModuleUserStatus.userDetail.userRole.ROLE_NM + '</td>';
                                result += '<td>' + msg.TrainingModuleList[i].trainingModuleUserStatus.status + '</td>';
                                result += '<td>' + msg.TrainingModuleList[i].trainingModuleUserStatus.submittedOn + '</td>';
                                result += '<td>' + msg.TrainingModuleList[i].trainingModuleUserStatus.remarks + '</td>';
                                result += '</tr>';
                            }
                        }
                    }
                    var table = $('#tbl-trainingReport-setup').DataTable();
                    table.destroy();
                    $("#tbdTrainingReportList").html(result);
                    initializeDataTable('tbl-trainingReport-setup', [0, 1, 2, 3, 4, 5]);
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

function fnValidate() {
    if ($("#bindTrainings").val() == '' || $("#bindTrainings").val() == undefined || $("#bindTrainings").val() == null) {
        alert("Please select a training");
        return false;
    }
    else if ($("#txtFromDate").val() == '' || $("#txtFromDate").val() == undefined || $("#txtFromDate").val() == null) {
        alert("Please select from date");
        return false;
    }
    else if ($("#txtToDate").val() == '' || $("#txtToDate").val() == undefined || $("#txtToDate").val() == null) {
        alert("Please select to date");
        return false;
    }
    else {
        var FromDate = new Date(convertToDateTime($("#txtFromDate").val()));
        var Todate = new Date(convertToDateTime($("#txtToDate").val()));

        if (Todate < FromDate) {

            alert("To Date Should be greater than From Date");
            return false;
        }
    }
    return true;
}

function convertToDateTime(date) {
    return date.split("/")[1] + "/" + date.split("/")[0] + "/" + date.split("/")[2];
}