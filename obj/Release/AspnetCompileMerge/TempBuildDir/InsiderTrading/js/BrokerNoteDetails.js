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
    var groupColumns = [0];
    $('#' + id).DataTable({
        dom: 'Bfrtip',
        pageLength: 10,
        "scrollY": "300px",
        "scrollX": true,
        "columnDefs": [
            { "visible": false }
        ],
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
        "drawCallback": function (settings) {
            var api = this.api();
            var rows = api.rows({ page: 'current' }).nodes();
            var last = null;

            api.column(0, { page: 'current' }).data().each(function (group, i) {

                if (last !== group) {
                    var rowData = api.row(i).data();

                    $(rows).eq(i).before(
                        '<tr class="group bg-gray"><td colspan="6">' + rowData[1] + " - " + rowData[4] + '</td></tr>'
                    );
                    last = group;
                }
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
            $("#Loader").hide();
            if (msg.StatusFl == true) {
                var result = "";
                result += '<option value="0">All</option>';
                for (var i = 0; i < msg.UserList.length; i++) {
                    result += '<option value="' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].USER_NM + '(' + msg.UserList[i].LOGIN_ID + ')' + '</option>';
                }
                $("#bindUser").html(result);                
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
        var webUrl = uri + "/api/ReportsIT/GetBrokerNoteDetailsBetweenDates";
        $.ajax({
            type: "POST",
            url: webUrl,
            data: JSON.stringify({ UserType: $("#bindUser").val(), tradingFrom: $("#txtFromDate").val(), tradingTo: $("#txtToDate").val() }),
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            success: function (msg) {
                var result = "";
                var Preid = "";
                var PreSum = 0;
                var alertclass = "";
                if (msg.StatusFl) {
                    if (msg.lstTradingReport !== null) {
                        if (msg.lstTradingReport.length > 0) {
                            for (var i = 0; i < msg.lstTradingReport.length; i++) {
                                
                                if (Preid == msg.lstTradingReport[i].PreclearanceId) {
                                    PreSum += parseInt(msg.lstTradingReport[i].TradeQuantity);

                                }
                                else {
                                    PreSum = parseInt(msg.lstTradingReport[i].TradeQuantity);
                                    Preid = msg.lstTradingReport[i].PreclearanceId;
                                }
                                
                                if (PreSum > parseInt(msg.lstTradingReport[i].ReqTradeQuantity)) {
                                    alertclass = "text-danger";
                                }
                                else {
                                    alertclass = "";
                                }
                                result += '<tr class="' + alertclass +'">';
                                result += '<td class="display-none">' + msg.lstTradingReport[i].PreclearanceId + '</td>';
                                result += '<td class="display-none">' + msg.lstTradingReport[i].name + '</td>';
                                result += '<td>' + msg.lstTradingReport[i].relativeName + '</td>';
                                result += '<td>' + msg.lstTradingReport[i].relation + '</td>';
                                result += '<td class="display-none">' + msg.lstTradingReport[i].pan + '</td>';
                                result += '<td class="text-center">' + msg.lstTradingReport[i].ReqTradeQuantity + '</td>';
                                result += '<td class="text-center">' + msg.lstTradingReport[i].TradeQuantity + '</td>';
                                result += '<td>' + msg.lstTradingReport[i].PreclearanceDate + '</td>';
                                result += '<td>' + msg.lstTradingReport[i].tradeDate + '</td>';                                
                                result += '</tr>';
                            }
                        }
                    }
                    var table = $('#tbl-tradeReport-setup').DataTable();
                    table.destroy();
                    $("#tbdTradeReportList").html(result);
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
                        var table = $('#tbl-tradeReport-setup').DataTable();
                        table.destroy();
                        $("#tbdTradeReportList").html('');
                        initializeDataTable('tbl-tradeReport-setup', [0, 1, 2, 3, 4, 5, 6, 7, 8]);
                        
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
    return true;
}