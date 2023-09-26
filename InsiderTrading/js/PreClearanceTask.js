$(document).ready(function () {
    $("#Loader").hide();
    fnGetAllPendingRequest();
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
    $('#tbl-preclearance-task').DataTable({
        dom: 'Bfrtip',
        pageLength: 10,
        "scrollY": "300px",
        "scrollX": true,
        buttons: [
         {
             extend: 'pdf',
             className: 'btn green btn-outline',
             exportOptions: {
                 columns: [0, 1, 2, 3, 4, 5, 6]
             }
         },
         {
             extend: 'excel',
             className: 'btn yellow btn-outline ',
             exportOptions: {
                 columns: [0, 1, 2, 3, 4, 5, 6]
             }
         }
        ]
    });
}
function fnGetAllPendingRequest() {
    $("#Loader").show();
    var webUrl = uri + "/api/PreClearanceRequest/GetAllPendingRequest";
    $.ajax({
        url: webUrl,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        data: {},
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                var result = "";
                for (var i = 0; i < msg.PreClearanceRequestList.length; i++) {
                    result += '<tr>';
                    result += '<td>' + msg.PreClearanceRequestList[i].PreClearanceRequestedForName + '</td>';
                    result += '<td>' + msg.PreClearanceRequestList[i].relationName + '</td>';
                    result += '<td>' + msg.PreClearanceRequestList[i].SecurityTypeName + '</td>';
                    result += '<td>' + msg.PreClearanceRequestList[i].TradeCompanyName + '</td>';
                    result += '<td style="text-align:right;padding-right:10px;display:none">' + msg.PreClearanceRequestList[i].PeriodQty + '</td>';
                    result += '<td style="text-align:right;padding-right:10px;display:none">' + msg.PreClearanceRequestList[i].PeriodVal + '</td>';
                    result += '<td style="text-align:right;padding-right:10px;">' + msg.PreClearanceRequestList[i].TradeQuantity + '</td>';
                    result += '<td style="text-align:right;padding-right:10px;">' + msg.PreClearanceRequestList[i].TotalAmount + '</td>';
                    result += '<td>' + msg.PreClearanceRequestList[i].TransactionTypeName + '</td>';
                    result += '<td>' + msg.PreClearanceRequestList[i].TradeDate + '</td>';
                    result += '<td>';
                    result += '<a href="#tradeDetails" class="btn btn-primary" onclick="fnGetTradeDetails(\'' + msg.PreClearanceRequestList[i].LoginId + '\',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '\')" data-toggle="modal">View Detail</a>'
                    result += '</td>';
                    result += '<td>';
                    result += '<a href="#approve" class="btn btn-success" onclick="fnSetTaskId(\'' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '\')" data-toggle="modal">Approve</a>';
                    result += '<a href="#reject" class="btn btn-danger" onclick="fnSetTaskId(\'' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '\')" data-toggle="modal">Reject</a>';
                    result += '</td>';
                    result += '</tr>';
                }
                

                var table = $('#tbl-preclearance-task').DataTable();
                table.destroy();
                $("#tbdPreClearanceTaskList").html(result);
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
function fnGetTradeDetails(loginId,taskId) {
    $("#Loader").show();
    var webUrl = uri + "/api/PreClearanceRequest/GetTemplateForApproverForAction";
    $.ajax({
        url: webUrl,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        data: JSON.stringify({
            PreClearanceRequestId: taskId, LoginId: loginId
        }),
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                if (msg.PreClearanceRequest !== null) {
                    $("#bdTradeDetails").html(msg.PreClearanceRequest.layoutTemplate);
                }

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
function fnSetTaskId(taskId) {
    $("#txtTaskId").val(taskId);
}
function fnSubmitActionTaken(status, id) {
    var taskId = $("#txtTaskId").val();
    var status = status;
    var comments = $("#" + id).val();
    if (comments == null || comments == undefined || comments == '') {
        alert("Please enter comments.");
        return false;
    }
    else {
        $("#Loader").show();
        var webUrl = uri + "/api/PreClearanceRequest/UpdateTaskUsers";
        $.ajax({
            url: webUrl,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            data: JSON.stringify({
                PreClearanceRequestId: taskId, Status: status, remarks: comments
            }),
            success: function (msg) {
                $("#Loader").hide();
                if (isJson(msg)) {
                    msg = JSON.parse(msg);
                }
                if (msg.StatusFl == true) {
                    window.location.reload(true);
                }
                else {
                    if (msg.Msg == "SessionExpired") {
                        alert("Your session is expired. Please login again to continue");
                        window.location.href = "../LogOut.aspx";
                    }
                    else {
                        alert(msg.Msg);
                        window.location.reload(true);
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
function fnCloseModal() {
    $("#txtTaskId").val('');
    $("#txtAreaApprove").val('');
    $("#txtAreaReject").val('');
    $("#approve").modal('hide');
    $("#reject").modal('hide');
}