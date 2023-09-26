$(document).ready(function () {
    window.history.forward();
    function preventBack() { window.history.forward(1); }
    $("#Loader").hide();
    fnGetForms();
    fnBindBusinessUnitList();
    $("#bindBusinessUnit").on('change', function () {
        if ($("#bindBusinessUnit").val() != '' || $("#bindBusinessUnit").val() != undefined || $("#bindBusinessUnit").val() != null || $("#bindBusinessUnit").val() != '0') {
            fnBindUserList();
        }
    });
    var table = $('#tbl-FormLogsReport-setup').DataTable();
    table.destroy();
    $("#tbdFormLogsReportList").html('');
    initializeDataTable('tbl-FormLogsReport-setup', [0, 1, 2, 3]);

});
function isJson(str) {
    try {
        JSON.parse(str);
    } catch (e) {
        return false;
    }
    return true;
}

function initializeDataTable(id, columns) {
    $('#' + id).DataTable({
        dom: 'Bfrtip',
        pageLength: 10,
        "scrollY": "350px",
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
                    columns: columns,
                }
            },
            //    { extend: 'colvis', className: 'btn dark btn-outline', text: 'Columns' }
        ]
    });
}
function fnBindBusinessUnitList() {
    $("#Loader").show();
    var webUrl = uri + "/api/BusinessUnit/GetAllBusinessUnitList";
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
                if (msg.BusinessUnitList != null) {
                    if (msg.BusinessUnitList.length > 1) {
                        result += '<option value="0">Select</option>';
                    }

                    for (var i = 0; i < msg.BusinessUnitList.length; i++) {
                        result += '<option value="' + msg.BusinessUnitList[i].businessUnitId + '">' + msg.BusinessUnitList[i].businessUnitName + '</option>';
                    }
                }
                $("#bindBusinessUnit").append(result);
                $("#Loader").hide();

                if (msg.BusinessUnitList.length == 1) {
                    fnBindUserList();
                }
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
function fnBindUserList() {
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/AccessUserListByBusinessUnitId";
    $.ajax({
        type: "POST",
        url: webUrl,
        data: JSON.stringify({ businessUnit: { businessUnitId: $("#bindBusinessUnit").val() } }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        //   async: false,
        success: function (msg) {
            if (msg.StatusFl == true) {
                var result = "";
                if (msg.UserList.length > 1) {
                    result += '<option value="0">All</option>';
                }
                for (var i = 0; i < msg.UserList.length; i++) {
                    result += '<option value="' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].USER_NM + '(' + msg.UserList[i].USER_EMAIL + ')' + '</option>';
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
                    $("#bindUser").html('');
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
function fnGetForms() {
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/GetCompanyForms";
    $.ajax({
        type: "GET",
        url: webUrl,
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            if (msg.StatusFl == true) {
                var result = "";

                
                if (msg.UserList !== null) {

                    if (msg.UserList.length > 1) {
                        result += '<option value="0">All</option>';
                    }

                    for (var i = 0; i < msg.UserList.length; i++) {
                        result += '<option value = "' + msg.UserList[i].ID + '">' + msg.UserList[i].formName + '</option>';
                    }
                    $("#bindForm").html(result);
                }

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
function fnGetFormLogsReport() {
    if (fnValidate()) {
        $("#Loader").show();
        var webUrl = uri + "/api/ReportsIT/GetFormLogsReport";
        $.ajax({
            type: "POST",
            url: webUrl,
            data: JSON.stringify({ UserLogin: $("#bindUser").val(), FromDate: $("#txtFromDate").val(), ToDate: $("#txtToDate").val(), FormId: $("#bindForm").val(), CompanyId: $("#bindBusinessUnit").val() }),
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            success: function (msg) {
                var result = "";
                if (msg.StatusFl) {
                    if (msg.lstLogsReport !== null) {
                        if (msg.lstLogsReport.length > 0) {
                            for (var i = 0; i < msg.lstLogsReport.length; i++) {
                                var RowCount = i + 1;
                                result += '<tr>';
                                result += '<td>' + RowCount + '</td>';
                                result += '<td>' + msg.lstLogsReport[i].FormName + '</td>';
                                result += '<td>' + msg.lstLogsReport[i].CreatedOn + '</td>';
                                
                                if (msg.lstLogsReport[i].FilePath != '') {
                                    result += '<td><a class="fa fa-download" target="_blank" href="emailAttachment/' + msg.lstLogsReport[i].FilePath + '"></a></td>';
                                }
                                else {
                                    result += '<td></td>'
                                }
                                
                                result += '</tr>';

                            }
                        }
                    }
                    var table = $('#tbl-FormLogsReport-setup').DataTable();
                    table.destroy();
                    $("#tbdFormLogsReportList").html(result);
                    initializeDataTable('tbl-FormLogsReport-setup', [0, 1, 2, 3]);
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
    if ($("#bindBusinessUnit").val() == '' || $("#bindBusinessUnit").val() == undefined || $("#bindBusinessUnit").val() == null || $("#bindBusinessUnit").val() == '0') {
        alert("Please select the user");
        return false;
    }
    if ($("#bindUser").val() == '' || $("#bindUser").val() == undefined || $("#bindUser").val() == null) {
        alert("Please select the user");
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

    if ($("#bindForm").val() == '' || $("#bindForm").val() == undefined || $("#bindForm").val() == null) {
        alert("Please select the Forms");
        return false;
    }

    var FromDate = new Date(FormatDate($("#txtFromDate").val()));
    var Todate = new Date(FormatDate($("#txtToDate").val()));

    if (Todate < FromDate) {
        
        alert("To Date Should be greater than From Date");
        return false;
    }

    return true;
}

function FormatDate(dateString) {
    return dateString.split("/")[1] + "/" + dateString.split("/")[0] + "/" + dateString.split("/")[2];
}