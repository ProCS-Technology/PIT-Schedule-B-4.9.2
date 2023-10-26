$(document).ready(function () {
    //alert("IN document ready");
    $('#txtFromDate').datepicker({
        todayHighlight: true,
        autoclose: true,
        format: $("input[id*=hdnJSDateFormat]").val(),
        clearBtn: true
    });
    $('#txtToDate').datepicker({
        todayHighlight: true,
        autoclose: true,
        format: $("input[id*=hdnJSDateFormat]").val(),
        clearBtn: true
    });

    $("#Loader").hide();
    fnBindBusinessUnitList();
    fnBindUserList();

    
    //var table = $('#tbl-tradeReport-setup').DataTable();
    //table.destroy();
    //$("#tbdTradeReportList").html('');
    //initializeDataTable('tbl-tradeReport-setup', [0, 1, 2, 3, 4, 5, 6, 7, 8]);

    $("#bindBusinessUnit").select2({
        placeholder: "Select a company"
    });

    $("#bindUser").select2({
        placeholder: "Select a user"
    });

    $("#bindBusinessUnit").on('change', function () {
        fnBindUserList();
    });
})
function initializeDataTable(id, columns) {
    $('#' + id).DataTable({
        dom: 'Bfrtip',
        pageLength: 10,
        
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
                    format: {
                        body: function (data, column, row, node) {
                            return column === 3 ? "\u200C" + data : data;
                        }
                    }
                }
            },
            //    { extend: 'colvis', className: 'btn dark btn-outline', text: 'Columns' }
        ],
        "order": [],
        "aaSorting": []
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
    var webUrl = uri + "/api/UserIT/GetUserListByBusinessUnitId";
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
                result += '<option value="0">All</option>';
                for (var i = 0; i < msg.UserList.length; i++) {
                    result += '<option value="' + msg.UserList[i].ID + '">' + msg.UserList[i].USER_NM + '(' + msg.UserList[i].USER_EMAIL + ')' + '</option>';
                }
                $("#bindUser").html(result);
                var table = $('#tbl-tradeReport-setup').DataTable();
                table.destroy();
                $("#tbdTradeReportList").html('');
                initializeDataTable('tbl-tradeReport-setup', [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21]);
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
                    var table = $('#tbl-tradeReport-setup').DataTable();
                    table.destroy();
                    $("#tbdTradeReportList").html('');
                    initializeDataTable('tbl-tradeReport-setup', [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21]);
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
function fnGetTradingReport() {
    if (fnValidate()) {
        $("#Loader").show();
        var webUrl = uri + "/api/ReportsIT/GetNonComplianceReport";
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
                                result += '<tr>';
                                result += '<td>' + msg.lstTradingReport[i].name + '</td>';
                                result += '<td>' + msg.lstTradingReport[i].relativeName + '</td>';
                                result += '<td>' + msg.lstTradingReport[i].pan + '</td>';
                                result += '<td>' + msg.lstTradingReport[i].folioNumber + '</td>';
                                result += '<td>' + FormatDate(msg.lstTradingReport[i].PreclearanceDate, $("input[id*=hdnDateFormat]").val()) + '</td>';
                                result += '<td>' + msg.lstTradingReport[i].transactionSubType + '</td>';
                                result += '<td style="text-align:right">' + msg.lstTradingReport[i].TradeQuantity + '</td>';
                                result += '<td style="text-align:right">' + msg.lstTradingReport[i].equityValue + '</td>';
                                result += '<td>' + msg.lstTradingReport[i].nonComplianceTaskStatus + '</td>';
                                result += '</tr>';
                            }
                        }
                    }
                    //alert(result);
                    var table = $('#tbl-tradeReport-setup').DataTable();
                    table.destroy();
                    $("#tbdTradeReportList").html(result);
                    //  initializeDataTable('tbl-tradeReport-setup', [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22]);
                    initializeDataTable('tbl-tradeReport-setup', [0, 1, 2, 3, 4, 5, 6, 7, 8]);
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
function fnSetNonComplianceTaskId(nonComplianceTaskId) {
    $("#txtNonComplianceTaskId").val(nonComplianceTaskId);
}
function fnSubmitActionTaken(status, comment) {
    var nonComplianceTaskStatus = status;
    var coRemarks = $("#" + comment).val();
    var nonComplianceTaskId = $("#txtNonComplianceTaskId").val();

    $("#Loader").show();
    var webUrl = uri + "/api/ReportsIT/CloseNonComplianceTask";
    $.ajax({
        type: "POST",
        url: webUrl,
        data: JSON.stringify({ nonComplianceTaskId: nonComplianceTaskId, nonComplianceTaskStatus: nonComplianceTaskStatus, cORemarks: coRemarks }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            var result = "";
            if (msg.StatusFl) {
                alert(msg.Msg);
                fnCloseModal();
                fnGetTradingReport();
                //   window.location.reload(true);
                $("#Loader").hide();
            }
            else {
                $("#Loader").hide();
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../Login.aspx";
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
    $("#txtAreaApprove").val('');
    $("#approve").modal('hide');
    $("#txtAreaReject").val('');
    $('#reject').modal('hide');
    $("#txtNonComplianceTaskId").val('');
}
function convertToDateTime(date) {
    return date.split("/")[1] + "/" + date.split("/")[0] + "/" + date.split("/")[2];
}