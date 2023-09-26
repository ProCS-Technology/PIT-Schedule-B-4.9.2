$(document).ready(function () {
    $("#Loader").hide();
})

function initializeDataTable() {
    //var groupColumns = [0, 1, 2];
    var tablestr = $('#tbl-CPtradeReport-setup').DataTable({
        dom: 'Bfltip',
        "columnDefs": [
            { "visible": true }
        ],
        buttons: [
            {
                extend: 'print',
                className: 'btn green btn-outline',
                exportOptions: {
                    columns: [1, 2, 3, 4]
                }
            },
            {
                extend: 'excel',
                className: 'btn yellow btn-outline ',
                exportOptions: {
                    columns: [1, 2, 3, 4]
                }
            }],
        "order": [[0, 'asc'], [1, 'asc'], [2, 'desc'],],
        "displayLength": 25,
        //"drawCallback": function (settings) {
        //    var api = this.api();
        //    var rows = api.rows({ page: 'current' }).nodes();
        //    var last = null;

        //    api.column(0, { page: 'current' }).data().each(function (group, i) {

        //        if (last !== group) {
        //            var rowData = api.row(i).data();

        //            $(rows).eq(i).before(
        //                '<tr class="group"><td colspan="4">' + group + " - " + rowData[1] + " - " + rowData[2] + '</td></tr>'
        //            );
        //            last = group;
        //        }
        //    });
        //}
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

function fnGetTradingReport() {
    if (fnValidate()) {
        //$("#Loader").show();
        var webUrl = uri + "/api/ReportsIT/GetConnectedPersonTradingReport";
        $.ajax({
            type: "POST",
            url: webUrl,
            data: JSON.stringify({ UserType: $("#ddUserType").val(), tradingFrom: $("#txtFromDate").val(), tradingTo: $("#txtToDate").val() }),
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
                                result += '<td>' + msg.lstTradingReport[i].pan + '</td>';
                                result += '<td>' + msg.lstTradingReport[i].tradeDate + '</td>';
                                result += '<td>' + msg.lstTradingReport[i].holdingAsOnDate + '</td>';
                                result += '</tr>';

                            }
                        }
                    }
                    var table = $('#tbl-CPtradeReport-setup').DataTable();
                    table.destroy();
                    $("#tbdCPTradeReportList").html(result);
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
}

function fnValidate() {
    if ($("#ddUserType").val() == '' || $("#ddUserType").val() == undefined || $("#ddUserType").val() == null) {
        alert("Please select user type");
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