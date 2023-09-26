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
    var table = $('#tbl-preclearance-other-setup').DataTable();
    table.destroy();
    $("#tbdPreClearanceOtherList").html('');
    initializeDataTable('tbl-preclearance-other-setup', [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]);
    
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
function fnGetReport() {
    if (fnValidate()) {
        $("#Loader").show();
        var webUrl = uri + "/api/PreClearanceRequest/GetPreClearanceRequestOtherCompanyReport";
        $.ajax({
            type: "POST",
            url: webUrl,
            data: JSON.stringify({ CompanyId: $("#bindBusinessUnit").val(), userId: $("#bindUser").val(), tradingFrom: $("#txtFromDate").val(), tradingTo: $("#txtToDate").val() }),
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            success: function (msg) {
                $("#Loader").hide();
                if (isJson(msg)) {
                    msg = JSON.parse(msg);
                }
                if (msg.StatusFl == true) {
                    var result = "";
                    lstPreclearanceRequest = msg.PreClearanceRequestList;
                    for (var i = 0; i < msg.PreClearanceRequestList.length; i++) {
                        result += '<tr id="tr_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">';
                        if (msg.PreClearanceRequestList[i].PreClearanceRequestedForName == "") {
                            msg.PreClearanceRequestList[i].PreClearanceRequestedForName = "Self";
                        }
                        var RowNo = i + 1;
                        result += '<td>' + RowNo + '</td>';
                        result += '<td id="tdPreClearanceRequestFor_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].PreClearanceRequestedForName + '</td>';
                        result += '<td id="tdRelationName_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].relationName + '</td>';
                        result += '<td id="tdTradeQuantity_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].TradeQuantity + '</td>';
                        result += '<td id="tdTypeOfTransaction_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].TransactionTypeName + '</td>';
                        result += '<td id="tdRequestedTransactionDate_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].TradeDate + '</td>';
                        if (msg.PreClearanceRequestList[i].Status == "Approved") {
                            result += '<td style="background-color:green" id="tdPreClearanceRequestStatus_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].Status + '</td>';
                        }

                        else {
                            result += '<td style="background-color:red" id="tdPreClearanceRequestStatus_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].Status + '</td>';
                        }
                        result += '<td>' + msg.PreClearanceRequestList[i].reviewedOn + '</td>';
                        result += '<td>' + msg.PreClearanceRequestList[i].reviewedBy + '</td>';

                        result += '</tr>';
                    }

                    var table = $('#tbl-preclearance-other-setup').DataTable();
                    table.destroy();
                    $("#tbdPreClearanceOtherList").html(result);
                    initializeDataTable1();
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