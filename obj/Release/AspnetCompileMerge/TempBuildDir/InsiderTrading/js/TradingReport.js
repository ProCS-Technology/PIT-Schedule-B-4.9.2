$(document).ready(function () {
    $("#Loader").hide();
    fnBindBusinessUnitList();
    fnBindUserList();
    var table = $('#tbl-tradeReport-setup').DataTable();
    table.destroy();
    $("#tbdTradeReportList").html('');
    initializeDataTable('tbl-tradeReport-setup', [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21]);
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
        "aaSorting": [],
        initComplete: function () {
            // Non Compliance Reason column
            this.api().column(20).every(function () {
                var column = this;
                var select = $('#ddlFilter').empty()
                    .on('change', function () {
                        var val = $.fn.dataTable.util.escapeRegex(
                            $(this).val()
                        );
                        column
                            .search(val ? '^' + val + '$' : '', true, false)
                            .draw();
                    });
                select.append('<option value="">All</option>')
                column.data().unique().sort().each(function (d, j) {
                    select.append('<option value="' + d + '">' + d + '</option>')
                });
            });

        }
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
function fnGetTradingReport() {
    if (fnValidate()) {
        $("#Loader").show();
        var webUrl = uri + "/api/ReportsIT/GetTradingReport";
        $.ajax({
            type: "POST",
            url: webUrl,
            data: JSON.stringify(
                {
                    userId: $("#bindUser").val(),
                    tradingFrom: $("#txtFromDate").val(),
                    tradingTo: $("#txtToDate").val()
                }
            ),
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            success: function (msg) {
                var result = "";
                if (msg.StatusFl) {
                    if (msg.lstTradingReport !== null) {
                        if (msg.lstTradingReport.length > 0) {
                            for (var i = 0; i < msg.lstTradingReport.length; i++) {
                                //if (msg.lstTradingReport[i].nonCompliantType == 'Folio Not Declared') {
                                //    msg.lstTradingReport[i].isNonCompliant = 'No';
                                //    msg.lstTradingReport[i].nonCompliantType = '';
                                //    msg.lstTradingReport[i].nonCompliantReason = '';

                                //}
                                result += '<tr>';
                                result += '<td>' + msg.lstTradingReport[i].name + '</td>';
                                result += '<td>' + msg.lstTradingReport[i].relativeName + '</td>';
                                result += '<td>' + msg.lstTradingReport[i].relation + '</td>';
                                result += '<td>' + msg.lstTradingReport[i].pan + '</td>';
                                result += '<td>' + msg.lstTradingReport[i].folioNumber + '</td>';
                                //if (msg.lstTradingReport[i].initialHoldingAfterDeclaration == -1) {
                                //    result += '<td>NA</td>';
                                //}
                                //else {
                                //    result += '<td>' + msg.lstTradingReport[i].initialHoldingAfterDeclaration + '</td>';
                                //}
                                result += '<td>' + msg.lstTradingReport[i].fileUploadedDate + '</td>';

                                
                                result += '<td style="text-align:right">' + msg.lstTradingReport[i].totaltradeQuantity + '</td>';
                                result += '<td style="text-align:right">' + msg.lstTradingReport[i].totaltradeValue + '</td>';
                                result += '<td>' + msg.lstTradingReport[i].tradeType + '</td>';

                                if (msg.lstTradingReport[i].method == "Esop") {
                                    result += '<td style="text-align:right"></td>';
                                    result += '<td style="text-align:right"></td>';
                                    result += '<td></td>';
                                }
                                else {
                                    result += '<td style="text-align:right">' + msg.lstTradingReport[i].equityQuantity + '</td>';
                                    result += '<td style="text-align:right">' + msg.lstTradingReport[i].equityValue + '</td>';
                                    result += '<td>' + msg.lstTradingReport[i].equityTradeDate + '</td>';
                                }

                                if (msg.lstTradingReport[i].method == "Esop") {
                                    result += '<td style="text-align:right">' + msg.lstTradingReport[i].equityQuantity + '</td>';
                                    result += '<td style="text-align:right">' + msg.lstTradingReport[i].equityValue + '</td>';
                                    result += '<td>' + msg.lstTradingReport[i].equityTradeDate + '</td>';
                                }
                                else {
                                    result += '<td style="text-align:right"></td>';
                                    result += '<td style="text-align:right"></td>';
                                    result += '<td></td>';
                                }
                                
                                //result += '<td style="text-align:right">' + msg.lstTradingReport[i].esopQuantity + '</td>';
                                //result += '<td style="text-align:right">' + msg.lstTradingReport[i].esopValue + '</td>';
                                //result += '<td>' + msg.lstTradingReport[i].esopTradeDate + '</td>';

                                result += '<td style="text-align:right">' + msg.lstTradingReport[i].physicalShareQuantity + '</td>';
                                result += '<td style="text-align:right">' + msg.lstTradingReport[i].physicalShareValue + '</td>';
                                result += '<td>' + msg.lstTradingReport[i].physicalShareTradeDate + '</td>';


                                //result += '<td>' + msg.lstTradingReport[i].holdingAsOnDate + '</td>';


                                result += '<td>' + msg.lstTradingReport[i].method + '</td>';
                                result += '<td>' + msg.lstTradingReport[i].isNonCompliant + '</td>';
                                //result += '<td>' + msg.lstTradingReport[i].nonCompliantType + '</td>';
                                if (msg.lstTradingReport[i].nonCompliantReason == "Contra Trade" || msg.lstTradingReport[i].nonCompliantReason == "Window Closure") {
                                    result += '<td> style="color:red;">';
                                }
                                else {
                                    result += '<td>';
                                }
                                result +=  msg.lstTradingReport[i].nonCompliantReason + '</td>';
                                result += '<td>' + msg.lstTradingReport[i].cORemarks + '</td>';
                                result += '</tr>';

                            }
                        }
                    }
                    var table = $('#tbl-tradeReport-setup').DataTable();
                    table.destroy();
                    $("#tbdTradeReportList").html(result);
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
function convertToDateTime(date) {
    return date.split("/")[1] + "/" + date.split("/")[0] + "/" + date.split("/")[2];
}