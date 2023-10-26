$(document).ready(function () {
    window.history.forward();
    function preventBack() { window.history.forward(1); }
    $("#Loader").hide();
    fnBindUserList();
    var table = $('#tbl-UPSIReport-setup').DataTable();
    table.destroy();
    $("#tbdUPSIReportList").html('');
    initializeDataTable();
})

function isJson(str) {
    try {
        JSON.parse(str);
    } catch (e) {
        return false;
    }
    return true;
}

function initializeDataTable() {
    $('#tbl-UPSIReport-setup').DataTable({
        dom: 'Bfrtip',
        pageLength: 10,
        "scrollY": "300px",
        "scrollX": true,
        //  "aaSorting": [[0, "desc"]],
        buttons: [
            {
                extend: 'pdf',
                className: 'btn green btn-outline',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]
                }
            },
            {
                extend: 'excel',
                className: 'btn yellow btn-outline ',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]
                }
            },

            //{ extend: 'colvis', className: 'btn dark btn-outline', text: 'Columns' }
        ]
    });
}

function fnGetUPSIReportOnRun() {
    if (fnValidateUPSIReport()) {
        fnGetUPSIReport();
    }
}

function fnGetUPSIReport() {
    $("#Loader").show();
    upsiCommunicationList = [];
    var webUrl = uri + "/api/ReportsIT/GetUPSITemplateReport";
    $.ajax({
        url: webUrl,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        data: JSON.stringify({
            upsiFrom: $("#txtFromDate").val(), upsiTo: $("#txtToDate").val()
        }),
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl) {
                var result = "";
                if (msg.lstUPSIReport.length > 0) {
                    for (var i = 0; i < msg.lstUPSIReport.length; i++) {
                        result += '<tr>';
                        result += '<td>' + (i + 1) + '</td>';
                        result += '<td>' + msg.lstUPSIReport[i].natureOfUPSI + '</td>';
                        result += '<td>' + msg.lstUPSIReport[i].whoShared + '</td>';
                        result += '<td>' + msg.lstUPSIReport[i].withWhomShared + '</td>';
                        result += '<td>' + msg.lstUPSIReport[i].panOrOtherIdentification + '</td>';
                        result += '<td>' + msg.lstUPSIReport[i].sharedOn + '</td>';
                        result += '<td>' + msg.lstUPSIReport[i].modeOfSharing + '</td>';
                        result += '<td>' + msg.lstUPSIReport[i].subjectCreatedOn + '</td>';
                        result += '<td>';
                        if (msg.lstUPSIReport[i].attachmentShared != '') {
                            result += '<a href="UPSI/' + msg.lstUPSIReport[i].attachmentShared + '" target="_blank">' + msg.lstUPSIReport[i].attachmentShared.split('/')[1] + '</a>';
                        }
                        result += '</td>';
                        result += '<td>' + msg.lstUPSIReport[i].createdOn + '</td>';
                        result += '<td>' + msg.lstUPSIReport[i].remarks + '</td>';
                        result += '</tr>';
                    }


                }
                else {
                    alert("No data found.");
                }
                var table = $('#tbl-UPSIReport-setup').DataTable();
                table.destroy();
                $("#tbdUPSIReportList").html(result);
                initializeDataTable();
                
            }
            else {
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
    })
}

function fnValidateUPSIReport() {
    if ($("#txtFromDate").val() == '' || $("#txtFromDate").val() == undefined || $("#txtFromDate").val() == null) {
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

function fnBindUserList() {
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
            if (msg.StatusFl == true) {
                var result = "";
                result += '<option value="0">All</option>';
                for (var i = 0; i < msg.UserList.length; i++) {
                    result += '<option value="' + msg.UserList[i].USER_EMAIL + '">' + msg.UserList[i].USER_NM + '(' + msg.UserList[i].USER_EMAIL + ')' + '</option>';
                }
                $("#bindUser").html(result);
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
function convertToDateTime(date) {
    return date.split("/")[1] + "/" + date.split("/")[0] + "/" + date.split("/")[2];
}