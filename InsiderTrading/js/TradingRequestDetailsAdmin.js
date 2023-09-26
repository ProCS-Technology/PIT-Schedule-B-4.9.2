var lstPreclearanceRequest = null;
jQuery(document).ready(function () {
    $("#Loader").hide();
    fnBindUserList();

    $("#bindUser").val('');
    $('#stack1').on('hide.bs.modal', function () {
    });
    $("#bindUser").select2({
        placeholder: "Select a user"
    });

    const urlParams = new URLSearchParams(window.location.search);
    const myParam = urlParams.get('Status');
    if (myParam == "Draft") {
        $("#bindUser").val('0').trigger('change');
        $("#bindStatus").val(myParam);
        fnGetTradingRequestDetailsReport();
    }
    else if (myParam == "InApproval") {
        $("#bindUser").val('0').trigger('change');
        $("#bindStatus").val(myParam);
        fnGetTradingRequestDetailsReport();
    }
    else if (myParam == "Approved") {
        $("#bindUser").val('0').trigger('change');
        $("#bindStatus").val(myParam);
        fnGetTradingRequestDetailsReport();
    }
    else if (myParam == "Rejected") {
        $("#bindUser").val('0').trigger('change');
        $("#bindStatus").val(myParam);
        fnGetTradingRequestDetailsReport();
    }
    else if (myParam == "All") {
        $("#bindUser").val('0').trigger('change');
        $("#bindStatus").val(myParam);
        fnGetTradingRequestDetailsReport();
    }
    else {
        fnGetTradingRequestDetails();
    }

    // Add event listener for opening and closing details
    $('#tbl-TradingRequest-setup tbody').on('click', 'td.details-control', function () {
        var table = $('#tbl-TradingRequest-setup').DataTable();
        var tr = $(this).closest('tr');
        var row = table.row(tr);

        if (row.child.isShown()) {
            // This row is already open - close it
            row.child.hide();
            tr.removeClass('shown');
        }
        else {
            // Open this row
            row.child(format(row.data())).show();
            tr.addClass('shown');
        }
    });
    
});


function fnBindUserList() {
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/GetUserList";
    $.ajax({
        type: "GET",
        url: webUrl,
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            if (msg.StatusFl == true) {
                var result = "";
                //result += '<option value="0">All</option>';
                result += '<option value="">Please Select</option>';
                result += '<option value="0">All</option>';
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

/* Formatting function for row details - modify as you need */
function format(d) {
    // `d` is the original data object for the row
    var preClearanceRequestId = d.DT_RowId.substr(3);
    var htmlString = '';

    if (lstPreclearanceRequest !== null) {
        if (lstPreclearanceRequest.length > 0) {
            for (var i = 0; i < lstPreclearanceRequest.length; i++) {
                if (lstPreclearanceRequest[i].PreClearanceRequestId == preClearanceRequestId) {
                    if (lstPreclearanceRequest[i].lstBrokerNoteUploaded !== null) {
                        if (lstPreclearanceRequest[i].lstBrokerNoteUploaded.length > 0) {
                            htmlString += '<table class="table table-striped table-bordered" cellpadding="5" cellspacing="0" border="0" style="padding-left:50px;">' +
                                '<thead>' +
                                '<tr>' +
                                '<th>Trade Quantity</th>' +
                                '<th>Value Per Share</th>' +
                                '<th>Total Amount</th>' +
                                '<th>Actual Transaction Date</th>' +
                                '<th>Remarks</th>' +
                                '<th>Broker Note</th>' +
                                '<th></th>' +
                                '</tr >' +
                                '</thead >' +
                                '<tbody>';
                            for (var j = 0; j < lstPreclearanceRequest[i].lstBrokerNoteUploaded.length; j++) {

                                htmlString += '<tr>' +
                                    '<td>' + lstPreclearanceRequest[i].lstBrokerNoteUploaded[j].ActualTradeQuantity + '</td>' +
                                    '<td>' + lstPreclearanceRequest[i].lstBrokerNoteUploaded[j].ValuePerShare + '</td>' +
                                    '<td>' + lstPreclearanceRequest[i].lstBrokerNoteUploaded[j].TotalAmount + '</td>' +
                                    '<td>' + lstPreclearanceRequest[i].lstBrokerNoteUploaded[j].ActualTransactionDate + '</td>' +
                                    '<td>' + lstPreclearanceRequest[i].lstBrokerNoteUploaded[j].remarks + '</td>' +
                                    '<td><a target="_blank" href="TradingRequestDetails/' + lstPreclearanceRequest[i].lstBrokerNoteUploaded[j].BrokerNote + '"><i class="fa fa-download"></i></a></td>' +
                                    '<td>' +
                                    '<div class="btn-group">' +
                                    '<a class="btn blue dropdown-toggle" href="javascript:;" data-toggle="dropdown">' +
                                    '<i class="fa fa-user"></i> Actions' +
                                    '<i class="fa fa-angle-down"></i>' +
                                    '</a>' +
                                    '<ul class="dropdown-menu pull-right">';

                                if (lstPreclearanceRequest[i].lstBrokerNoteUploaded[j].isFormCDJCreated) {
                                    htmlString += '<li>' +
                                        '<a target="_blank" onclick="javascript:fnDownloadFormsCDJ(' + preClearanceRequestId + ',' + lstPreclearanceRequest[i].lstBrokerNoteUploaded[j].brokerNoteId + ');"><i class="fa fa-download"></i>Download Form</a>' +
                                        '</li>' +
                                        '<li class="divider"></li>';
                                }

                                htmlString += '</ul>' +
                                    '</div>' +
                                    '</td>' +
                                    '</tr>';
                            }
                            htmlString += '</tbody>' +
                                '</table>';
                        }
                    }
                }

            }
        }
    }

    return htmlString;
}

function fnDownloadFormsCDJ(preClearanceRequestId, brokerNoteId) {
    $("#Loader").show();
    var webUrl = uri + "/api/PreClearanceRequest/GetFormsCDJ";
    $.ajax({
        url: webUrl,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        data: JSON.stringify({ PreClearanceRequestId: preClearanceRequestId, brokerNoteId: brokerNoteId }),
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                var formurl = 'emailAttachment/' + msg.PreClearanceRequest.formUrl;
                window.open(formurl, "_blank");
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

function isJson(str) {
    try {
        JSON.parse(str);
    } catch (e) {
        return false;
    }
    return true;
}

function initializeDataTable() {
    $('#tbl-TradingRequest-setup').DataTable({
        dom: 'Bfrtip',
        pageLength: 10,
        "scrollY": "300px",
        "scrollX": true,
        buttons: [
            {
                extend: 'pdf',
                className: 'btn green btn-outline',
                exportOptions: {
                    columns: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13]
                }
            },
            {
                extend: 'excel',
                className: 'btn yellow btn-outline ',
                exportOptions: {
                    columns: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13]
                }
            },
            //    { extend: 'colvis', className: 'btn dark btn-outline', text: 'Columns' }
        ],
        "columns": [
            {
                "className": 'details-control',
                "orderable": false,
                "data": null,
                "defaultContent": ''
            },
            { "data": "for" },
            { "data": "relation" },
            //{ "data": "company" },
            { "data": "quantity" },
            { "data": "type" },
            { "data": "tradeExchange" },
            { "data": "demat" },
            { "data": "requested" },
            { "data": "status" },
            //  { "data": "brokerNoteUploaded" },
            { "data": "reviewedOn" },
            { "data": "reviewedBy" },
            { "data": "remarks" },
            { "data": "" }
        ],
        "order": [[1, 'asc']]
    });
}

function fnGetTradingRequestDetails() {
    $("#Loader").show();
    var webUrl = uri + "/api/TradingRequestDetails/GetTradingRequestDetailsList";
    $.ajax({
        type: "POST",
        url: webUrl,
        data: JSON.stringify({
            COMPANY_ID: 0
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: true,
        success: function (msg) {
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                var result = "";

                if (msg.TradingRequestDetailsList !== null) {
                    lstPreclearanceRequest = msg.TradingRequestDetailsList;
                    for (var i = 0; i < msg.TradingRequestDetailsList.length; i++) {
                        result += '<tr id="tr_' + msg.TradingRequestDetailsList[i].PreClearanceRequestId + '">';
                        if (msg.TradingRequestDetailsList[i].PreClearanceRequestedForName == "") {
                            msg.TradingRequestDetailsList[i].PreClearanceRequestedForName = "Self";
                        }
                        result += '<td></td>';
                        result += '<td id="tdPreClearanceRequestFor_' + msg.TradingRequestDetailsList[i].PreClearanceRequestId + '">' + msg.TradingRequestDetailsList[i].PreClearanceRequestedForName + '</td>';
                        result += '<td id="tdRelationName_' + msg.TradingRequestDetailsList[i].PreClearanceRequestId + '">' + msg.TradingRequestDetailsList[i].relationName + '</td>';
                        //result += '<td id="tdTypeOfSecurity_' + msg.TradingRequestDetailsList[i].PreClearanceRequestId + '">' + msg.TradingRequestDetailsList[i].SecurityTypeName + '</td>';
                        //result += '<td id="tdRestrictedCompany_' + msg.TradingRequestDetailsList[i].PreClearanceRequestId + '">' + msg.TradingRequestDetailsList[i].TradeCompanyName + '</td>';
                        result += '<td id="tdTradeQuantity_' + msg.TradingRequestDetailsList[i].PreClearanceRequestId + '">' + msg.TradingRequestDetailsList[i].TradeQuantity + '</td>';
                        result += '<td id="tdTypeOfTransaction_' + msg.TradingRequestDetailsList[i].PreClearanceRequestId + '">' + msg.TradingRequestDetailsList[i].TransactionTypeName + '</td>';
                        result += '<td style="display:none;" id="tdTradeExchange_' + msg.TradingRequestDetailsList[i].PreClearanceRequestId + '">' + msg.TradingRequestDetailsList[i].TradeExchangeName + '</td>';
                        result += '<td id="tdDematAccount_' + msg.TradingRequestDetailsList[i].PreClearanceRequestId + '">' + msg.TradingRequestDetailsList[i].DematAccount + '</td>';
                        result += '<td id="tdRequestedTransactionDate_' + msg.TradingRequestDetailsList[i].PreClearanceRequestId + '">' + msg.TradingRequestDetailsList[i].TradeDate + '</td>';
                        if (msg.TradingRequestDetailsList[i].Status == "InApproval") {
                            result += '<td style="background-color:orange" id="tdPreClearanceRequestStatus_' + msg.TradingRequestDetailsList[i].PreClearanceRequestId + '">' + msg.TradingRequestDetailsList[i].Status + '</td>';
                        }
                        else if (msg.TradingRequestDetailsList[i].Status == "Approved") {
                            if (msg.TradingRequestDetailsList[i].TradeDate == "") {
                                result += '<td style="background-color:red" id="tdPreClearanceRequestStatus_' + msg.TradingRequestDetailsList[i].PreClearanceRequestId + '">Pre-clearance Not Taken</td>';
                            }
                            else {
                                result += '<td style="background-color:green" id="tdPreClearanceRequestStatus_' + msg.TradingRequestDetailsList[i].PreClearanceRequestId + '">' + msg.TradingRequestDetailsList[i].Status + '</td>';
                            }
                        }
                        else if (msg.TradingRequestDetailsList[i].Status == "Draft") {
                            result += '<td id="tdPreClearanceRequestStatus_' + msg.TradingRequestDetailsList[i].PreClearanceRequestId + '">' + msg.TradingRequestDetailsList[i].Status + '</td>';
                        }
                        else {
                            result += '<td style="background-color:red" id="tdPreClearanceRequestStatus_' + msg.TradingRequestDetailsList[i].PreClearanceRequestId + '">' + msg.TradingRequestDetailsList[i].Status + '</td>';
                        }
                        //   result += '<td id="tdIsBrokerNoteUploaded_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].isBrokerNoteUploaded + '</td>';
                        result += '<td>' + msg.TradingRequestDetailsList[i].reviewedOn + '</td>';
                        result += '<td>' + msg.TradingRequestDetailsList[i].reviewedBy + '</td>';
                        result += '<td>' + msg.TradingRequestDetailsList[i].reviewerRemarks + '</td>';
                        // result += '<td id="tdActions_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '"><a data-target="#stack1" data-toggle="modal" id="a_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '" class="btn btn-outline dark" onclick=\"javascript:fnEditDesignation(' + msg.PreClearanceRequestList[i].PreClearanceRequestId + ',\'' + msg.PreClearanceRequestList[i].DESIGNATION_NM + '\');\">Edit</a></td>';
                        result += '<td id="tdActions_' + msg.TradingRequestDetailsList[i].PreClearanceRequestId + '">';
                        var innerStr = "";
                        innerStr += '<div class="btn-group">';
                        innerStr += '<a class="btn blue dropdown-toggle" href="javascript:;" data-toggle="dropdown">';
                        innerStr += '<i class="fa fa-user"></i> Actions';
                        innerStr += '<i class="fa fa-angle-down"></i>';
                        innerStr += '</a>';
                        innerStr += '<ul class="dropdown-menu pull-right">';

                        innerStr += '<li id="liZippedTradeFilesDownload_' + msg.TradingRequestDetailsList[i].PreClearanceRequestId + '">';
                        innerStr += '<a href="javascript:fnDownloadTradingZipFile(\'' + msg.TradingRequestDetailsList[i].PreClearanceRequestId + '\');"  id="btnZippedTradeFilesDownload_' + msg.TradingRequestDetailsList[i].PreClearanceRequestId + '">';
                        innerStr += '<i class="fa fa-download"></i>Download Trade Files</a>';
                        innerStr += '</li>';
                        innerStr += '<li class="divider"> </li>';
                        innerStr += '</ul>';
                        innerStr += '</div>';

                        result += innerStr + '</td>';
                        result += '</tr>';
                    }
                }
                
                var table = $('#tbl-TradingRequest-setup').DataTable();
                table.destroy();
                $("#tbdTradingRequestList").html(result);
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

function fnGetTradingRequestDetailsReport() {
    $("#Loader").show();
    var webUrl = uri + "/api/TradingRequestDetails/GetTradingRequestDetailsListByAdmin";
    $.ajax({
        type: "POST",
        url: webUrl,
        data: JSON.stringify({
            LoginId: $("#bindUser").val(), Status: $("#bindStatus").val()
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: true,
        success: function (msg) {
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                var result = "";

                if (msg.TradingRequestDetailsList !== null) {
                    lstPreclearanceRequest = msg.TradingRequestDetailsList;
                    for (var i = 0; i < msg.TradingRequestDetailsList.length; i++) {
                        result += '<tr id="tr_' + msg.TradingRequestDetailsList[i].PreClearanceRequestId + '">';
                        if (msg.TradingRequestDetailsList[i].PreClearanceRequestedForName == "") {
                            msg.TradingRequestDetailsList[i].PreClearanceRequestedForName = "Self";
                        }
                        result += '<td></td>';
                        result += '<td id="tdPreClearanceRequestFor_' + msg.TradingRequestDetailsList[i].PreClearanceRequestId + '">' + msg.TradingRequestDetailsList[i].PreClearanceRequestedForName + '</td>';
                        result += '<td id="tdRelationName_' + msg.TradingRequestDetailsList[i].PreClearanceRequestId + '">' + msg.TradingRequestDetailsList[i].relationName + '</td>';
                        //result += '<td id="tdTypeOfSecurity_' + msg.TradingRequestDetailsList[i].PreClearanceRequestId + '">' + msg.TradingRequestDetailsList[i].SecurityTypeName + '</td>';
                        //result += '<td id="tdRestrictedCompany_' + msg.TradingRequestDetailsList[i].PreClearanceRequestId + '">' + msg.TradingRequestDetailsList[i].TradeCompanyName + '</td>';
                        result += '<td id="tdTradeQuantity_' + msg.TradingRequestDetailsList[i].PreClearanceRequestId + '">' + msg.TradingRequestDetailsList[i].TradeQuantity + '</td>';
                        result += '<td id="tdTypeOfTransaction_' + msg.TradingRequestDetailsList[i].PreClearanceRequestId + '">' + msg.TradingRequestDetailsList[i].TransactionTypeName + '</td>';
                        result += '<td style="display:none;" id="tdTradeExchange_' + msg.TradingRequestDetailsList[i].PreClearanceRequestId + '">' + msg.TradingRequestDetailsList[i].TradeExchangeName + '</td>';
                        result += '<td id="tdDematAccount_' + msg.TradingRequestDetailsList[i].PreClearanceRequestId + '">' + msg.TradingRequestDetailsList[i].DematAccount + '</td>';
                        result += '<td id="tdRequestedTransactionDate_' + msg.TradingRequestDetailsList[i].PreClearanceRequestId + '">' + msg.TradingRequestDetailsList[i].TradeDate + '</td>';
                        if (msg.TradingRequestDetailsList[i].Status == "InApproval") {
                            result += '<td style="background-color:orange" id="tdPreClearanceRequestStatus_' + msg.TradingRequestDetailsList[i].PreClearanceRequestId + '">' + msg.TradingRequestDetailsList[i].Status + '</td>';
                        }
                        else if (msg.TradingRequestDetailsList[i].Status == "Approved") {
                            if (msg.TradingRequestDetailsList[i].TradeDate == "") {
                                result += '<td style="background-color:red" id="tdPreClearanceRequestStatus_' + msg.TradingRequestDetailsList[i].PreClearanceRequestId + '">Pre-clearance Not Taken</td>';
                            }
                            else {
                                result += '<td style="background-color:green" id="tdPreClearanceRequestStatus_' + msg.TradingRequestDetailsList[i].PreClearanceRequestId + '">' + msg.TradingRequestDetailsList[i].Status + '</td>';
                            }
                        }
                        else if (msg.TradingRequestDetailsList[i].Status == "Draft") {
                            result += '<td id="tdPreClearanceRequestStatus_' + msg.TradingRequestDetailsList[i].PreClearanceRequestId + '">' + msg.TradingRequestDetailsList[i].Status + '</td>';
                        }
                        else {
                            result += '<td style="background-color:red" id="tdPreClearanceRequestStatus_' + msg.TradingRequestDetailsList[i].PreClearanceRequestId + '">' + msg.TradingRequestDetailsList[i].Status + '</td>';
                        }
                        //   result += '<td id="tdIsBrokerNoteUploaded_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].isBrokerNoteUploaded + '</td>';
                        result += '<td>' + msg.TradingRequestDetailsList[i].reviewedOn + '</td>';
                        result += '<td>' + msg.TradingRequestDetailsList[i].reviewedBy + '</td>';
                        result += '<td>' + msg.TradingRequestDetailsList[i].reviewerRemarks + '</td>';
                        // result += '<td id="tdActions_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '"><a data-target="#stack1" data-toggle="modal" id="a_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '" class="btn btn-outline dark" onclick=\"javascript:fnEditDesignation(' + msg.PreClearanceRequestList[i].PreClearanceRequestId + ',\'' + msg.PreClearanceRequestList[i].DESIGNATION_NM + '\');\">Edit</a></td>';
                        result += '<td id="tdActions_' + msg.TradingRequestDetailsList[i].PreClearanceRequestId + '">';
                        var innerStr = "";
                        innerStr += '<div class="btn-group">';
                        innerStr += '<a class="btn blue dropdown-toggle" href="javascript:;" data-toggle="dropdown">';
                        innerStr += '<i class="fa fa-user"></i> Actions';
                        innerStr += '<i class="fa fa-angle-down"></i>';
                        innerStr += '</a>';
                        innerStr += '<ul class="dropdown-menu pull-right">';

                        innerStr += '<li id="liZippedTradeFilesDownload_' + msg.TradingRequestDetailsList[i].PreClearanceRequestId + '">';
                        innerStr += '<a href="javascript:fnDownloadTradingZipFile(\'' + msg.TradingRequestDetailsList[i].PreClearanceRequestId + '\');"  id="btnZippedTradeFilesDownload_' + msg.TradingRequestDetailsList[i].PreClearanceRequestId + '">';
                        innerStr += '<i class="fa fa-download"></i>Download Trade Files</a>';
                        innerStr += '</li>';
                        innerStr += '<li class="divider"> </li>';
                        innerStr += '</ul>';
                        innerStr += '</div>';

                        result += innerStr + '</td>';
                        result += '</tr>';
                    }
                }
                
                var table = $('#tbl-TradingRequest-setup').DataTable();
                table.destroy();
                $("#tbdTradingRequestList").html(result);
                initializeDataTable();
                $("#Loader").hide();
            }
            else {
                var table = $('#tbl-TradingRequest-setup').DataTable();
                table.destroy();
                $("#tbdTradingRequestList").html('');
                initializeDataTable();
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

function fnDownloadTradingZipFile(PreClearanceRequestId) {
    var webUrl = uri + "/api/PreClearanceRequest/DownloadTradingZipFile";

    var ajax = new XMLHttpRequest();
    ajax.open("Post", webUrl, true);
    ajax.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    ajax.responseType = "blob";
    ajax.onreadystatechange = function () {
        if (this.readyState == 4) {
            var blob = new Blob([this.response], { type: "application/octet-stream" });
            var fileName = "TradingReport.zip";
            saveAs(blob, fileName);

        }
    };

    ajax.send(JSON.stringify({
        PreClearanceRequestId: PreClearanceRequestId
    }));
}
