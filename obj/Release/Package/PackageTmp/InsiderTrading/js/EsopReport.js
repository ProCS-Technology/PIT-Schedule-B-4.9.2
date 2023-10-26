$(document).ready(function () {
    window.history.forward();
    function preventBack() { window.history.forward(1); }
    $("#Loader").hide();
    fnBindBusinessUnitList();
    $("#bindBusinessUnit").on('change', function () {
        if ($("#bindBusinessUnit").val() != '' || $("#bindBusinessUnit").val() != undefined || $("#bindBusinessUnit").val() != null || $("#bindBusinessUnit").val() != '0') {
            fnBindUserList();
        }
    });
    var table = $('#tbl-EsopReport-setup').DataTable();
    table.destroy();
    $("#tbdEsopReportList").html('');
    initializeDataTable('tbl-EsopReport-setup', [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]);

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
                result += '<option value="0">Select</option>';
                for (var i = 0; i < msg.BusinessUnitList.length; i++) {
                    result += '<option value="' + msg.BusinessUnitList[i].businessUnitId + '">' + msg.BusinessUnitList[i].businessUnitName + '</option>';
                }

                $("#bindBusinessUnit").append(result);
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
                    result += '<option value="' + msg.UserList[i].ID + '">' + msg.UserList[i].USER_NM + '(' + msg.UserList[i].USER_EMAIL + ')' + '</option>';
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
function fnGetEsopReport() {
    if (fnValidate()) {
        $("#Loader").show();
        var webUrl = uri + "/api/ReportsIT/GetEsopReport";
        $.ajax({
            type: "POST",
            url: webUrl,
            data: JSON.stringify({ userId: $("#bindUser").val(), tradingFrom: $("#txtFromDate").val(), tradingTo: $("#txtToDate").val() }),
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            success: function (msg) {
                var result = "";
                if (msg.StatusFl) {
                    if (msg.lstTradingReport !== null) {
                        if (msg.lstTradingReport.length > 0) {
                            for (var i = 0; i < msg.lstTradingReport.length; i++) {
                                var RowCount = i + 1;
                                result += '<tr>';
                                result += '<td>' + RowCount + '</td>';
                                result += '<td>' + msg.lstTradingReport[i].name + '</td>';
                                result += '<td>' + msg.lstTradingReport[i].pan + '</td>';
                                result += '<td>' + msg.lstTradingReport[i].folioNumber + '</td>';
                                result += '<td>' + msg.lstTradingReport[i].esopQuantity + '</td>';
                                result += '<td>' + msg.lstTradingReport[i].esopValue + '</td>';
                                result += '<td>' + msg.lstTradingReport[i].totaltradeValue + '</td>';
                                result += '<td>' + msg.lstTradingReport[i].esopTradeDate + '</td>';
                                result += '<td>' + msg.lstTradingReport[i].IsEsopFormSubmitted + '</td>';

                                if (msg.lstTradingReport[i].EsopFilePath != '') {
                                    result += '<td><a class="fa fa-download" target="_blank" href="emailAttachment/' + msg.lstTradingReport[i].EsopFilePath + '"></a></td>';
                                }
                                else {
                                    result += '<td></td>'
                                }
                                
                                result += '</tr>';

                            }
                        }
                    }
                    var table = $('#tbl-EsopReport-setup').DataTable();
                    table.destroy();
                    $("#tbdEsopReportList").html(result);
                    initializeDataTable('tbl-EsopReport-setup', [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]);
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
    return true;
}