$(document).ready(function () {
    fnGetTypeOfSecurity();
    fnGetTypeOfRestrictedCompanies();
    fnGetTypeOfTransaction();
    fnGetTradeExchange();
   
    fnGetUserList();
    //$("#ddlTradeExchange").on('change', function () fnEditPreClearanceRequest
    //    var currentValue = $(this).val();
    //    fnGetDematAccount(currentValue, null);
    //})
    $("#ddlFor").on('change', function () {
        var currentValue = $(this).val();
        fnGetDematAccount(currentValue, null);
    })
    $("#txtTradequantityBrokerNote").on('focusout', function () {
        if ($("#txtTradequantityBrokerNote").val() !== null && $("#txtTradequantityBrokerNote").val() !== undefined && $("#txtTradequantityBrokerNote").val() !== '' && $("#txtValuePerShare").val() !== null && $("#txtValuePerShare").val() !== undefined && $("#txtValuePerShare").val() !== '') {
            var totalAmount = $("#txtTradequantityBrokerNote").val() * $("#txtValuePerShare").val();
            $("#txtTotalamount").val(totalAmount);
        }
    })
    $("#txtValuePerShare").on('focusout', function () {
        if ($("#txtTradequantityBrokerNote").val() !== null && $("#txtTradequantityBrokerNote").val() !== undefined && $("#txtTradequantityBrokerNote").val() !== '' && $("#txtValuePerShare").val() !== null && $("#txtValuePerShare").val() !== undefined && $("#txtValuePerShare").val() !== '') {
            var totalAmount = $("#txtTradequantityBrokerNote").val() * $("#txtValuePerShare").val();
            $("#txtTotalamount").val(totalAmount);
        }
    })
    fnGetPreClearanceRequest();
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
    $('#tbl-preclearance-setup').DataTable({
        //  "dom": '<"top"iflp<"clear">>rt<"bottom"iflp<"clear">>',
        dom: 'Bfrtip',
        pageLength: 10,
        buttons: [
         {
             extend: 'pdf',
             className: 'btn green btn-outline',
             exportOptions: {
                 columns: [0, 1, 2, 3, 4, 5, 6, 7, 8]
             }
         },
         {
             extend: 'excel',
             className: 'btn yellow btn-outline ',
             exportOptions: {
                 columns: [0, 1, 2, 3, 4, 5, 6, 7, 8]
             }
         },
     //    { extend: 'colvis', className: 'btn dark btn-outline', text: 'Columns' }
        ]
    });
}

function fnAddPreClearance() {
    $("#btnSubmitPreClearanceRequest").show();
    $("#btnSaveAsDraftPreClearanceRequest").show();
    $("#btnCancelPreClearanceRequest").hide();

    $("#ddlFor").prop("disabled", false);
    $("#ddlTypeOfSecurity").prop("disabled", false);
    $("#ddlRestrictedCompanies").prop("disabled", false);
    $("#txtTradeQuantity").prop("readonly", false);
    $("#ddlTypeOfTransaction").prop("disabled", false);
    $("#ddlTradeExchange").prop("disabled", false);
    $("#ddlDematAccount").prop("disabled", false);
    $("#txtRequestedTransactionDate").prop("disabled", false);
}

function fnCloseModal() {
    fnClearForm();
}

function fnGetTypeOfSecurity() {
    $("#Loader").show();
    var webUrl = uri + "/api/PreClearanceRequest/GetTypeOfSecurityList";
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
                result += "<option value='0'>--Select--</option>";
                for (var i = 0; i < msg.SecurityTypeList.length; i++) {
                    result += "<option value='" + msg.SecurityTypeList[i].Id + "'>" + msg.SecurityTypeList[i].Name + "</option>";
                }
                $("#ddlTypeOfSecurity").append(result);
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

function fnGetTypeOfRestrictedCompanies() {
    $("#Loader").show();
    var webUrl = uri + "/api/PreClearanceRequest/GetTypeOfRestrictedCompaniesList";
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
                result += "<option value='0'>--Select--</option>";
                for (var i = 0; i < msg.RestrictedCompaniesList.length; i++) {
                    result += "<option value='" + msg.RestrictedCompaniesList[i].ID + "'>" + msg.RestrictedCompaniesList[i].companyName + "</option>";
                }
                $("#ddlRestrictedCompanies").append(result);
                $("#ddlRestrictedCompaniesBN").append(result);
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

function fnGetTypeOfTransaction() {
    $("#Loader").show();
    var webUrl = uri + "/api/PreClearanceRequest/GetTypeOfTransactionList";
    $.ajax({
        url: webUrl,
        type: "POST",
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
                result += "<option value='0'>--Select--</option>";
                for (var i = 0; i < msg.TransactionTypeList.length; i++) {
                    result += "<option value='" + msg.TransactionTypeList[i].Id + "'>" + msg.TransactionTypeList[i].Name + "</option>";
                }
                $("#ddlTypeOfTransaction").append(result);
                $("#ddlTypeOfTransactionBN").append(result);
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

function fnGetTradeExchange() {
    $("#Loader").show();
    var webUrl = uri + "/api/PreClearanceRequest/GetTradeExchangeList";
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
                result += "<option value='0'>--Select--</option>";
                for (var i = 0; i < msg.TradeExchangeList.length; i++) {
                    result += "<option value='" + msg.TradeExchangeList[i].Id + "'>" + msg.TradeExchangeList[i].Name + "</option>";
                }
                $("#ddlTradeExchange").append(result);
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

function fnGetDematAccount(currentValue, source) {
    $("#Loader").show();
    var webUrl = uri + "/api/PreClearanceRequest/GetDematAccountList";
    $.ajax({
        url: webUrl,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        data: JSON.stringify({
            // DematType: currentValue
            relativeId: currentValue
        }),
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                var result = "";
                result += "<option value='0'>--Select--</option>";
                for (var i = 0; i < msg.DematDetailList.length; i++) {
                    result += "<option value='" + msg.DematDetailList[i].accountNo + "'>" + msg.DematDetailList[i].accountNo + "</option>";
                }
                if (source == "fnEditBrokerNote") {
                    $("#ddlDematAccountBrokerNote").html(result);
                }
                else {
                    $("#ddlDematAccount").html(result);
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

function fnGetRelativeDetail(user_login) {
    $("#Loader").show();
    $("#ddlFor").html('');
    $("#ddlForBN").html('');
    var result1;
    result1 = "<option value='Select'>--Select--</option>";
    result1 += "<option value='0'>Self</option>";
    $("#ddlFor").html(result1);
    $("#ddlForBN").append(result1);
    
    var webUrl = uri + "/api/PreClearanceRequest/GetRelativeDetailList_for_other";
    $.ajax({
        url: webUrl,
        type: "POST",
        datatype: "json",
        data: JSON.stringify({ LoginId: user_login }),
        contentType: "application/json; charset=utf-8",
        async: false,
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                var result = "";
                
                for (var i = 0; i < msg.RelativeDetailList.length; i++) {

                    
                    result += "<option value='" + msg.RelativeDetailList[i].ID + "'>" + msg.RelativeDetailList[i].relativeName + "</option>";
                }
                $("#ddlFor").append(result);
                $("#ddlForBN").append(result);
            }
            else {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                  //  alert(msg.Msg);
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

function fnGetPreClearanceRequest() {
    $("#Loader").show();
    var webUrl = uri + "/api/PreClearanceRequest/GetPreClearanceRequestList_for_other";
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
                    result += '<tr id="tr_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">';
                    if (msg.PreClearanceRequestList[i].PreClearanceRequestedForName == "") {
                        msg.PreClearanceRequestList[i].PreClearanceRequestedForName = "Self";
                    }
                    result += '<td id="tdPreClearanceRequestFor_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].PreClearanceRequestedForName + '</td>';
                    result += '<td id="tdTypeOfSecurity_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].SecurityTypeName + '</td>';
                    result += '<td id="tdRestrictedCompany_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].TradeCompanyName + '</td>';
                    result += '<td id="tdTradeQuantity_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].TradeQuantity + '</td>';
                    result += '<td id="tdTypeOfTransaction_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].TransactionTypeName + '</td>';
                    result += '<td style="display:none;" id="tdTradeExchange_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].TradeExchangeName + '</td>';
                    result += '<td id="tdDematAccount_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].DematAccount + '</td>';
                    result += '<td id="tdRequestedTransactionDate_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].TradeDate.split(" ")[0] + '</td>';
                    if (msg.PreClearanceRequestList[i].Status == "InApproval") {
                        result += '<td style="background-color:orange" id="tdPreClearanceRequestStatus_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].Status + '</td>';
                    }
                    else if (msg.PreClearanceRequestList[i].Status == "Approved") {
                        result += '<td style="background-color:green" id="tdPreClearanceRequestStatus_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].Status + '</td>';
                    }
                    else if (msg.PreClearanceRequestList[i].Status == "Draft") {
                        result += '<td id="tdPreClearanceRequestStatus_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].Status + '</td>';
                    }
                    else {
                        result += '<td style="background-color:red" id="tdPreClearanceRequestStatus_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].Status + '</td>';
                    }

                    // result += '<td id="tdActions_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '"><a data-target="#stack1" data-toggle="modal" id="a_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '" class="btn btn-outline dark" onclick=\"javascript:fnEditDesignation(' + msg.PreClearanceRequestList[i].PreClearanceRequestId + ',\'' + msg.PreClearanceRequestList[i].DESIGNATION_NM + '\');\">Edit</a></td>';
                    result += '<td id="tdActions_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">';
                    var innerStr = "";
                    innerStr += '<div class="btn-group">';
                    innerStr += '<a class="btn blue dropdown-toggle" href="javascript:;" data-toggle="dropdown">';
                    innerStr += '<i class="fa fa-user"></i> Actions';
                    innerStr += '<i class="fa fa-angle-down"></i>';
                    innerStr += '</a>';
                    innerStr += '<ul class="dropdown-menu pull-right">';
                    innerStr += '<li>';
                    innerStr += '<a  id="btnEdit_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '" data-toggle="modal" data-target="#stack1" onclick=\"javascript:fnEditPreClearanceRequest(' + msg.PreClearanceRequestList[i].PreClearanceRequestId + ',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedForName + '\',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequestList[i].SecurityTypeName + '\',\'' + msg.PreClearanceRequestList[i].SecurityType + '\',\'' + msg.PreClearanceRequestList[i].TradeCompanyName + '\',\'' + msg.PreClearanceRequestList[i].TradeCompany + '\',\'' + msg.PreClearanceRequestList[i].TradeQuantity + '\',\'' + msg.PreClearanceRequestList[i].TransactionTypeName + '\',\'' + msg.PreClearanceRequestList[i].TransactionType + '\',\'' + msg.PreClearanceRequestList[i].TradeExchangeName + '\',\'' + msg.PreClearanceRequestList[i].TradeExchange + '\',\'' + msg.PreClearanceRequestList[i].DematAccount + '\',\'' + msg.PreClearanceRequestList[i].TradeDate.split(" ")[0] + '\',\'' + msg.PreClearanceRequestList[i].Status + '\');\">';
                    innerStr += '<i class="fa fa-pencil"></i> Edit </a>';
                    innerStr += '</li>';
                    //innerStr += '<li>';
                    //innerStr += '<a  id="btnCancelrequest_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '" onclick=\"javascript:fnEditPreClearanceRequest(' + msg.PreClearanceRequestList[i].PreClearanceRequestId + ',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedForName + '\',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequestList[i].SecurityTypeName + '\',\'' + msg.PreClearanceRequestList[i].SecurityType + '\',\'' + msg.PreClearanceRequestList[i].TradeCompanyName + '\',\'' + msg.PreClearanceRequestList[i].TradeCompany + '\',\'' + msg.PreClearanceRequestList[i].TradeQuantity + '\',\'' + msg.PreClearanceRequestList[i].TransactionTypeName + '\',\'' + msg.PreClearanceRequestList[i].TransactionType + '\',\'' + msg.PreClearanceRequestList[i].TradeExchangeName + '\',\'' + msg.PreClearanceRequestList[i].TradeExchange + '\',\'' + msg.PreClearanceRequestList[i].DematAccount + '\',\'' + msg.PreClearanceRequestList[i].TradeDate.split(" ")[0] + '\',\'' + msg.PreClearanceRequestList[i].Status + '\');\">';
                    //innerStr += '<i class="fa fa-times"></i> Cancel Request </a>';
                    //innerStr += '</li>';
                    if (msg.PreClearanceRequestList[i].Status == "InApproval" || msg.PreClearanceRequestList[i].Status == "Approved") {
                        innerStr += '<li id="liUploadbrokernote_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">';
                        innerStr += '<a  id="btnUploadbrokernote_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '" data-toggle="modal" data-target="#basic1" onclick=\"javascript:fnEditBrokerNote(' + msg.PreClearanceRequestList[i].PreClearanceRequestId + ',\'' + msg.PreClearanceRequestList[i].BrokerNote + '\',\'' + (msg.PreClearanceRequestList[i].ActualTransactionDate !== null ? msg.PreClearanceRequestList[i].ActualTransactionDate.split(" ")[0] : msg.PreClearanceRequestList[i].ActualTransactionDate) + '\',\'' + msg.PreClearanceRequestList[i].ValuePerShare + '\',\'' + msg.PreClearanceRequestList[i].TotalAmount + '\',\'' + msg.PreClearanceRequestList[i].TradeQuantity + '\',\'' + msg.PreClearanceRequestList[i].Status + '\',\'' + msg.PreClearanceRequestList[i].TradeExchange + '\',\'' + msg.PreClearanceRequestList[i].DematAccount + '\',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequestList[i].TradeCompany + '\',\'' + msg.PreClearanceRequestList[i].TransactionType + '\',\'' + msg.PreClearanceRequestList[i].TradeDate.split(" ")[0] + '\');\">';
                        innerStr += '<i class="fa fa-upload"></i>Upload Broker Note </a>';
                        innerStr += '</li>';
                    }
                    else {
                        innerStr += '<li id="liUploadbrokernote_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '" style="display:none">';
                        innerStr += '<a  id="btnUploadbrokernote_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '" data-toggle="modal" data-target="#basic1" onclick=\"javascript:fnEditBrokerNote(' + msg.PreClearanceRequestList[i].PreClearanceRequestId + ',\'' + msg.PreClearanceRequestList[i].BrokerNote + '\',\'' + (msg.PreClearanceRequestList[i].ActualTransactionDate !== null ? msg.PreClearanceRequestList[i].ActualTransactionDate.split(" ")[0] : msg.PreClearanceRequestList[i].ActualTransactionDate) + '\',\'' + msg.PreClearanceRequestList[i].ValuePerShare + '\',\'' + msg.PreClearanceRequestList[i].TotalAmount + '\',\'' + msg.PreClearanceRequestList[i].TradeQuantity + '\',\'' + msg.PreClearanceRequestList[i].Status + '\',\'' + msg.PreClearanceRequestList[i].TradeExchange + '\',\'' + msg.PreClearanceRequestList[i].DematAccount + '\',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequestList[i].TradeCompany + '\',\'' + msg.PreClearanceRequestList[i].TransactionType + '\',\'' + msg.PreClearanceRequestList[i].TradeDate.split(" ")[0] + '\');\">';
                        innerStr += '<i class="fa fa-upload"></i>Upload Broker Note </a>';
                        innerStr += '</li>';
                    }
                    innerStr += '<li class="divider"> </li>';
                    innerStr += '</ul>';
                    innerStr += '</div>';
                    //if (msg.PreClearanceRequestList[i].Status == "Approved" || msg.PreClearanceRequestList[i].Status == "Rejected") {
                    //    result += '</td>';
                    //}
                    //else {
                    //    result += innerStr + '</td>';
                    //}
                    result += innerStr + '</td>';
                    result += '</tr>';
                }

                var table = $('#tbl-preclearance-setup').DataTable();
                table.destroy();
                $("#tbdPreClearanceList").html(result);
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

function fnSubmitPreClearanceRequest(status) {
    if (fnValidate()) {
        fnSubmitRequest(status);
    }
}

function fnSubmitRequest(status) {
    
    $("#Loader").show();
    var preClearanceRequestId = $("#preClearanceRequestId").val();
    var user_login = $('#ddlFor_user').val();
    var preClearanceRequestFor = $('#ddlFor').val();
    var preClearanceRequestForName = $('#ddlFor :selected').text();
    var securityType = $('#ddlTypeOfSecurity').val();
    var securityTypeName = $('#ddlTypeOfSecurity :selected').text();
    var transactionType = $('#ddlTypeOfTransaction').val();
    var transactionTypeName = $('#ddlTypeOfTransaction :selected').text();
    var restrictedCompanyId = $('#ddlRestrictedCompanies').val();
    var restrictedCompanyName = $('#ddlRestrictedCompanies :selected').text();
    var tradeQuantity = $('#txtTradeQuantity').val();
    var tradeExchange = $('#ddlTradeExchange').val();
    var tradeExchangeName = $('#ddlTradeExchange :selected').text();
    var dematAccountId = $('#ddlDematAccount').val();
    var requestedTransactionDate = $('#txtRequestedTransactionDate').val();
    var status = status;

    var webUrl = uri + "/api/PreClearanceRequest/SavePreClearanceRequest_for_other";
    $.ajax({
        url: webUrl,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        data: JSON.stringify({ LoginId: user_login, TradeExchangeName: tradeExchangeName, TradeCompanyName: restrictedCompanyName, TransactionTypeName: transactionTypeName, SecurityTypeName: securityTypeName, PreClearanceRequestedForName: preClearanceRequestForName, PreClearanceRequestId: preClearanceRequestId, TradeCompany: restrictedCompanyId, TradeDate: requestedTransactionDate, PreClearanceRequestedFor: preClearanceRequestFor, SecurityType: securityType, TransactionType: transactionType, TradeQuantity: tradeQuantity, TradeExchange: tradeExchange, DematAccount: dematAccountId, Status: status
        }),
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                if (msg.PreClearanceRequest.PreClearanceRequestId == preClearanceRequestId) {
                    $("#tdPreClearanceRequestFor_" + msg.PreClearanceRequest.PreClearanceRequestId).html(msg.PreClearanceRequest.PreClearanceRequestedForName);
                    $("#tdTypeOfSecurity_" + msg.PreClearanceRequest.PreClearanceRequestId).html(msg.PreClearanceRequest.SecurityTypeName);
                    $("#tdRestrictedCompany_" + msg.PreClearanceRequest.PreClearanceRequestId).html(msg.PreClearanceRequest.TradeCompanyName);
                    $("#tdTradeQuantity_" + msg.PreClearanceRequest.PreClearanceRequestId).html(msg.PreClearanceRequest.TradeQuantity);
                    $("#tdTypeOfTransaction_" + msg.PreClearanceRequest.PreClearanceRequestId).html(msg.PreClearanceRequest.TransactionTypeName);
                    $("#tdTradeExchange_" + msg.PreClearanceRequest.PreClearanceRequestId).html(msg.PreClearanceRequest.TradeExchangeName);
                    $("#tdDematAccount_" + msg.PreClearanceRequest.PreClearanceRequestId).html(msg.PreClearanceRequest.DematAccount);
                    $("#tdRequestedTransactionDate_" + msg.PreClearanceRequest.PreClearanceRequestId).html(msg.PreClearanceRequest.TradeDate.split(" ")[0]);

                    if (msg.PreClearanceRequest.Status == "InApproval") {
                        $("#tdPreClearanceRequestStatus_" + msg.PreClearanceRequest.PreClearanceRequestId).html(msg.PreClearanceRequest.Status);
                        $("#tdPreClearanceRequestStatus_" + msg.PreClearanceRequest.PreClearanceRequestId).css('background-color', 'orange');
                    }
                    else if (msg.PreClearanceRequest.Status == "Approved") {
                        $("#tdPreClearanceRequestStatus_" + msg.PreClearanceRequest.PreClearanceRequestId).html(msg.PreClearanceRequest.Status);
                        $("#tdPreClearanceRequestStatus_" + msg.PreClearanceRequest.PreClearanceRequestId).css('background-color', 'green');
                    }
                    else if (msg.PreClearanceRequest.Status == "Draft") {
                        $("#tdPreClearanceRequestStatus_" + msg.PreClearanceRequest.PreClearanceRequestId).html(msg.PreClearanceRequest.Status);
                        $("#tdPreClearanceRequestStatus_" + msg.PreClearanceRequest.PreClearanceRequestId).css('background-color', 'transparent');
                    }
                    else {
                        $("#tdPreClearanceRequestStatus_" + msg.PreClearanceRequest.PreClearanceRequestId).html(msg.PreClearanceRequest.Status);
                        $("#tdPreClearanceRequestStatus_" + msg.PreClearanceRequest.PreClearanceRequestId).css('background-color', 'red');
                    }


                    $("#btnEdit_" + msg.PreClearanceRequest.PreClearanceRequestId).attr('onclick', 'javascript:fnEditPreClearanceRequest(' + msg.PreClearanceRequest.PreClearanceRequestId + ',\'' + msg.PreClearanceRequest.PreClearanceRequestedForName + '\',\'' + msg.PreClearanceRequest.PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequest.SecurityTypeName + '\',\'' + msg.PreClearanceRequest.SecurityType + '\',\'' + msg.PreClearanceRequest.TradeCompanyName + '\',\'' + msg.PreClearanceRequest.TradeCompany + '\',\'' + msg.PreClearanceRequest.TradeQuantity + '\',\'' + msg.PreClearanceRequest.TransactionTypeName + '\',\'' + msg.PreClearanceRequest.TransactionType + '\',\'' + msg.PreClearanceRequest.TradeExchangeName + '\',\'' + msg.PreClearanceRequest.TradeExchange + '\',\'' + msg.PreClearanceRequest.DematAccount + '\',\'' + msg.PreClearanceRequest.TradeDate.split(" ")[0] + '\',\'' + msg.PreClearanceRequest.Status + '\');');
                    //  $("#btnCancelrequest_" + msg.PreClearanceRequest.PreClearanceRequestId).attr('onclick', 'javascript:fnEditPreClearanceRequest(' + msg.PreClearanceRequest.PreClearanceRequestId + ',\'' + msg.PreClearanceRequest.PreClearanceRequestedForName + '\',\'' + msg.PreClearanceRequest.PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequest.SecurityTypeName + '\',\'' + msg.PreClearanceRequest.SecurityType + '\',\'' + msg.PreClearanceRequest.TradeCompanyName + '\',\'' + msg.PreClearanceRequest.TradeCompany + '\',\'' + msg.PreClearanceRequest.TradeQuantity + '\',\'' + msg.PreClearanceRequest.TransactionTypeName + '\',\'' + msg.PreClearanceRequest.TransactionType + '\',\'' + msg.PreClearanceRequest.TradeExchangeName + '\',\'' + msg.PreClearanceRequest.TradeExchange + '\',\'' + msg.PreClearanceRequest.DematAccount + '\',\'' + msg.PreClearanceRequest.TradeDate.split(" ")[0] + '\',\'' + msg.PreClearanceRequest.Status + '\');');
                    $("#btnUploadbrokernote_" + msg.PreClearanceRequest.PreClearanceRequestId).attr('onclick', 'javascript:fnEditBrokerNote(' + msg.PreClearanceRequest.PreClearanceRequestId + ',\'' + msg.PreClearanceRequest.BrokerNote + '\',\'' + msg.PreClearanceRequest.ActualTransactionDate.split(" ")[0] + '\',\'' + msg.PreClearanceRequest.ValuePerShare + '\',\'' + msg.PreClearanceRequest.TotalAmount + '\',\'' + msg.PreClearanceRequest.TradeQuantity + '\',\'' + msg.PreClearanceRequest.Status + '\',\'' + msg.PreClearanceRequest.TradeExchange + '\',\'' + msg.PreClearanceRequest.DematAccount + '\',\'' + msg.PreClearanceRequest.PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequest.TradeCompany + '\',\'' + msg.PreClearanceRequest.TransactionType + '\',\'' + msg.PreClearanceRequest.TradeDate.split(" ")[0] + '\');');

                    $("#btnEdit_" + msg.PreClearanceRequest.PreClearanceRequestId).attr("data-target", "#stack1");
                    $("#btnEdit_" + msg.PreClearanceRequest.PreClearanceRequestId).attr("data-toggle", "modal");

                    $("#btnUploadbrokernote_" + msg.PreClearanceRequest.PreClearanceRequestId).attr("data-target", "#basic1");
                    $("#btnUploadbrokernote_" + msg.PreClearanceRequest.PreClearanceRequestId).attr("data-toggle", "modal");

                    if (msg.PreClearanceRequest.Status == "InApproval" || msg.PreClearanceRequest.Status == "Approved") {
                        $('#liUploadbrokernote_' + msg.PreClearanceRequest.PreClearanceRequestId).show();
                    }
                    else {
                        $('#liUploadbrokernote_' + msg.PreClearanceRequest.PreClearanceRequestId).hide();
                    }

                    var table = $('#tbl-preclearance-setup').DataTable();
                    table.destroy();
                    initializeDataTable();
                    alert(msg.Msg);
                }
                else {
                    var result = "";
                    result += '<tr id="tr_' + msg.PreClearanceRequest.PreClearanceRequestId + '">';
                    result += '<td id="tdPreClearanceRequestFor_' + msg.PreClearanceRequest.PreClearanceRequestId + '">' + msg.PreClearanceRequest.PreClearanceRequestedForName + '</td>';
                    result += '<td id="tdTypeOfSecurity_' + msg.PreClearanceRequest.PreClearanceRequestId + '">' + msg.PreClearanceRequest.SecurityTypeName + '</td>';
                    result += '<td id="tdRestrictedCompany_' + msg.PreClearanceRequest.PreClearanceRequestId + '">' + msg.PreClearanceRequest.TradeCompanyName + '</td>';
                    result += '<td id="tdTradeQuantity_' + msg.PreClearanceRequest.PreClearanceRequestId + '">' + msg.PreClearanceRequest.TradeQuantity + '</td>';
                    result += '<td id="tdTypeOfTransaction_' + msg.PreClearanceRequest.PreClearanceRequestId + '">' + msg.PreClearanceRequest.TransactionTypeName + '</td>';
                    result += '<td style="display:none;" id="tdTradeExchange_' + msg.PreClearanceRequest.PreClearanceRequestId + '">' + msg.PreClearanceRequest.TradeExchangeName + '</td>';
                    result += '<td  id="tdDematAccount_' + msg.PreClearanceRequest.PreClearanceRequestId + '">' + msg.PreClearanceRequest.DematAccount + '</td>';
                    result += '<td id="tdRequestedTransactionDate_' + msg.PreClearanceRequest.PreClearanceRequestId + '">' + msg.PreClearanceRequest.TradeDate.split(" ")[0] + '</td>';
                    if (msg.PreClearanceRequest.Status == "InApproval") {
                        result += '<td style="background-color:orange" id="tdPreClearanceRequestStatus_' + msg.PreClearanceRequest.PreClearanceRequestId + '">' + msg.PreClearanceRequest.Status + '</td>';
                    }
                    else if (msg.PreClearanceRequest.Status == "Approved") {
                        result += '<td style="background-color:green" id="tdPreClearanceRequestStatus_' + msg.PreClearanceRequest.PreClearanceRequestId + '">' + msg.PreClearanceRequest.Status + '</td>';
                    }
                    else if (msg.PreClearanceRequest.Status == "Draft") {
                        result += '<td style="background-color:transparent" id="tdPreClearanceRequestStatus_' + msg.PreClearanceRequest.PreClearanceRequestId + '">' + msg.PreClearanceRequest.Status + '</td>';
                    }
                    else {
                        result += '<td style="background-color:red" id="tdPreClearanceRequestStatus_' + msg.PreClearanceRequest.PreClearanceRequestId + '">' + msg.PreClearanceRequest.Status + '</td>';
                    }

                    // result += '<td id="tdActions_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '"><a data-target="#stack1" data-toggle="modal" id="a_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '" class="btn btn-outline dark" onclick=\"javascript:fnEditDesignation(' + msg.PreClearanceRequestList[i].PreClearanceRequestId + ',\'' + msg.PreClearanceRequestList[i].DESIGNATION_NM + '\');\">Edit</a></td>';
                    result += '<td id="tdActions_' + msg.PreClearanceRequest.PreClearanceRequestId + '">';
                    var innerStr = "";
                    innerStr += '<div class="btn-group">';
                    innerStr += '<a class="btn blue dropdown-toggle" href="javascript:;" data-toggle="dropdown">';
                    innerStr += '<i class="fa fa-user"></i> Actions';
                    innerStr += '<i class="fa fa-angle-down"></i>';
                    innerStr += '</a>';
                    innerStr += '<ul class="dropdown-menu pull-right">';
                    innerStr += '<li>';
                    innerStr += '<a  id="btnEdit_' + msg.PreClearanceRequest.PreClearanceRequestId + '" data-toggle="modal" data-target="#stack1" onclick=\"javascript:fnEditPreClearanceRequest(' + msg.PreClearanceRequest.PreClearanceRequestId + ',\'' + msg.PreClearanceRequest.PreClearanceRequestedForName + '\',\'' + msg.PreClearanceRequest.PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequest.SecurityTypeName + '\',\'' + msg.PreClearanceRequest.SecurityType + '\',\'' + msg.PreClearanceRequest.TradeCompanyName + '\',\'' + msg.PreClearanceRequest.TradeCompany + '\',\'' + msg.PreClearanceRequest.TradeQuantity + '\',\'' + msg.PreClearanceRequest.TransactionTypeName + '\',\'' + msg.PreClearanceRequest.TransactionType + '\',\'' + msg.PreClearanceRequest.TradeExchangeName + '\',\'' + msg.PreClearanceRequest.TradeExchange + '\',\'' + msg.PreClearanceRequest.DematAccount + '\',\'' + msg.PreClearanceRequest.TradeDate.split(" ")[0] + '\',\'' + msg.PreClearanceRequest.Status + '\');\">';
                    innerStr += '<i class="fa fa-pencil"></i> Edit </a>';
                    innerStr += '</li>';
                    //innerStr += '<li>';
                    //innerStr += '<a  id="btnCancelrequest_' + msg.PreClearanceRequest.PreClearanceRequestId + '" onclick=\"javascript:fnEditPreClearanceRequest(' + msg.PreClearanceRequest.PreClearanceRequestId + ',\'' + msg.PreClearanceRequest.PreClearanceRequestedForName + '\',\'' + msg.PreClearanceRequest.PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequest.SecurityTypeName + '\',\'' + msg.PreClearanceRequest.SecurityType + '\',\'' + msg.PreClearanceRequest.TradeCompanyName + '\',\'' + msg.PreClearanceRequest.TradeCompany + '\',\'' + msg.PreClearanceRequest.TradeQuantity + '\',\'' + msg.PreClearanceRequest.TransactionTypeName + '\',\'' + msg.PreClearanceRequest.TransactionType + '\',\'' + msg.PreClearanceRequest.TradeExchangeName + '\',\'' + msg.PreClearanceRequest.TradeExchange + '\',\'' + msg.PreClearanceRequest.DematAccount + '\',\'' + msg.PreClearanceRequest.TradeDate.split(" ")[0] + '\',\'' + msg.PreClearanceRequest.Status + '\');\">';
                    //innerStr += '<i class="fa fa-times"></i> Cancel Request </a>';
                    //innerStr += '</li>';
                    if (msg.PreClearanceRequest.Status == "InApproval" || msg.PreClearanceRequest.Status == "Approved") {
                        innerStr += '<li id="liUploadbrokernote_' + msg.PreClearanceRequest.PreClearanceRequestId + '">';
                        innerStr += '<a  id="btnUploadbrokernote_' + msg.PreClearanceRequest.PreClearanceRequestId + '" data-toggle="modal" data-target="#basic1" onclick=\"javascript:fnEditBrokerNote(' + msg.PreClearanceRequest.PreClearanceRequestId + ',\'' + msg.PreClearanceRequest.BrokerNote + '\',\'' + msg.PreClearanceRequest.ActualTransactionDate.split(" ")[0] + '\',\'' + msg.PreClearanceRequest.ValuePerShare + '\',\'' + msg.PreClearanceRequest.TotalAmount + '\',\'' + msg.PreClearanceRequest.TradeQuantity + '\',\'' + msg.PreClearanceRequest.Status + '\',\'' + msg.PreClearanceRequest.TradeExchange + '\',\'' + msg.PreClearanceRequest.DematAccount + '\',\'' + msg.PreClearanceRequest.PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequest.TradeCompany + '\',\'' + msg.PreClearanceRequest.TransactionType + '\',\'' + msg.PreClearanceRequest.TradeDate.split(" ")[0] + '\');\">';
                        innerStr += '<i class="fa fa-upload"></i>Upload Broker Note </a>';
                        innerStr += '</li>';
                    }
                    else {
                        innerStr += '<li id="liUploadbrokernote_' + msg.PreClearanceRequest.PreClearanceRequestId + '" style="display:none">';
                        innerStr += '<a  id="btnUploadbrokernote_' + msg.PreClearanceRequest.PreClearanceRequestId + '" data-toggle="modal" data-target="#basic1" onclick=\"javascript:fnEditBrokerNote(' + msg.PreClearanceRequest.PreClearanceRequestId + ',\'' + msg.PreClearanceRequest.BrokerNote + '\',\'' + msg.PreClearanceRequest.ActualTransactionDate.split(" ")[0] + '\',\'' + msg.PreClearanceRequest.ValuePerShare + '\',\'' + msg.PreClearanceRequest.TotalAmount + '\',\'' + msg.PreClearanceRequest.TradeQuantity + '\',\'' + msg.PreClearanceRequest.Status + '\',\'' + msg.PreClearanceRequest.TradeExchange + '\',\'' + msg.PreClearanceRequest.DematAccount + '\',\'' + msg.PreClearanceRequest.PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequest.TradeCompany + '\',\'' + msg.PreClearanceRequest.TransactionType + '\',\'' + msg.PreClearanceRequest.TradeDate.split(" ")[0] + '\');\">';
                        innerStr += '<i class="fa fa-upload"></i>Upload Broker Note </a>';
                        innerStr += '</li>';
                    }

                    innerStr += '<li class="divider"> </li>';
                    innerStr += '</ul>';
                    innerStr += '</div>';
                    result += innerStr + '</td>';
                    result += '</tr>';

                    var table = $('#tbl-preclearance-setup').DataTable();
                    table.destroy();
                    $("#tbdPreClearanceList").append(result);
                    initializeDataTable();
                    alert(msg.Msg);
                }
                fnClearForm();
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

function fnValidate() {
    if ($('#ddlFor_user').val() == undefined || $('#ddlFor_user').val() == null || $('#ddlFor_user').val().trim() == '' || $('#ddlFor_user').val().trim() == 'Select') {
        return false;
    }
    if ($('#ddlFor').val() == undefined || $('#ddlFor').val() == null || $('#ddlFor').val().trim() == '' || $('#ddlFor').val().trim() == 'Select') {
        return false;
    }
    if ($('#ddlTypeOfSecurity').val() == undefined || $('#ddlTypeOfSecurity').val() == null || $('#ddlTypeOfSecurity').val().trim() == '' || $('#ddlTypeOfSecurity').val().trim() == '0') {
        return false;
    }
    if ($('#ddlRestrictedCompanies').val() == undefined || $('#ddlRestrictedCompanies').val() == null || $('#ddlRestrictedCompanies').val().trim() == '' || $('#ddlRestrictedCompanies').val().trim() == '0') {
        return false;
    }
    if ($('#txtTradeQuantity').val() == undefined || $('#txtTradeQuantity').val() == null || $('#txtTradeQuantity').val().trim() == '') {
        return false;
    }
    //if ($('#ddlTradeExchange').val() == undefined || $('#ddlTradeExchange').val() == null || $('#ddlTradeExchange').val().trim() == '' || $('#ddlTradeExchange').val().trim() == '0') {
    //    return false;
    //}
    if ($('#ddlDematAccount').val() == undefined || $('#ddlDematAccount').val() == null || $('#ddlDematAccount').val().trim() == '' || $('#ddlDematAccount').val().trim() == '0') {
        return false;
    }
    if ($('#txtRequestedTransactionDate').val() == undefined || $('#txtRequestedTransactionDate').val() == null || $('#txtRequestedTransactionDate').val().trim() == '') {
        return false;
    }
    return true;
}

function fnEditPreClearanceRequest(preClearanceRequestId, PreClearanceRequestedForName, PreClearanceRequestedFor, SecurityTypeName, SecurityType, TradeCompanyName, TradeCompany, TradeQuantity, TransactionTypeName, TransactionType, TradeExchangeName, TradeExchange, DematAccount, TradeDate, Status) {
    $("#preClearanceRequestId").val(preClearanceRequestId);
    // PreClearanceRequestedForName
    $("#ddlFor").val(PreClearanceRequestedFor);
    //SecurityTypeName
    $("#ddlTypeOfSecurity").val(SecurityType);
    // TradeCompanyName
    $("#ddlRestrictedCompanies").val(TradeCompany);
    $("#txtTradeQuantity").val(TradeQuantity);
    // TransactionTypeName
    $("#ddlTypeOfTransaction").val(TransactionType);
    // TradeExchangeName
    $("#ddlTradeExchange").val(TradeExchange);
    //fnGetDematAccount(TradeExchange, null);
    fnGetDematAccount(PreClearanceRequestedFor, null);
    $("#ddlDematAccount").val(DematAccount);
    $("#txtRequestedTransactionDate").val(TradeDate);

    if (Status == "InApproval") {
        $("#btnSubmitPreClearanceRequest").show();
        $("#btnSaveAsDraftPreClearanceRequest").show();
        $("#btnCancelPreClearanceRequest").show();

        $("#ddlFor").prop("disabled", true);
        $("#ddlFor_user").prop("disabled", true);
        //$("#ddlTypeOfSecurity").prop("disabled", true);
        //$("#ddlRestrictedCompanies").prop("disabled", true);
        //$("#txtTradeQuantity").prop("readonly", true);
        //$("#ddlTypeOfTransaction").prop("disabled", true);
        //$("#ddlTradeExchange").prop("disabled", true);
        //$("#ddlDematAccount").prop("disabled", true);
        //$("#txtRequestedTransactionDate").prop("disabled", true);
    }
    else if (Status == "Draft") {
        $("#btnSubmitPreClearanceRequest").show();
        $("#btnSaveAsDraftPreClearanceRequest").show();
        $("#btnCancelPreClearanceRequest").show();

        $("#ddlFor").prop("disabled", false);
        $("#ddlTypeOfSecurity").prop("disabled", false);
        $("#ddlRestrictedCompanies").prop("disabled", false);
        $("#txtTradeQuantity").prop("readonly", false);
        $("#ddlTypeOfTransaction").prop("disabled", false);
        $("#ddlTradeExchange").prop("disabled", false);
        $("#ddlDematAccount").prop("disabled", false);
        $("#txtRequestedTransactionDate").prop("disabled", false);
    }
    else if (Status == "Cancel") {
        $("#btnSubmitPreClearanceRequest").show();
        $("#btnSaveAsDraftPreClearanceRequest").show();
        $("#btnCancelPreClearanceRequest").show();

        $("#ddlFor").prop("disabled", false);
        $("#ddlTypeOfSecurity").prop("disabled", false);
        $("#ddlRestrictedCompanies").prop("disabled", false);
        $("#txtTradeQuantity").prop("readonly", false);
        $("#ddlTypeOfTransaction").prop("disabled", false);
        $("#ddlTradeExchange").prop("disabled", false);
        $("#ddlDematAccount").prop("disabled", false);
        $("#txtRequestedTransactionDate").prop("disabled", false);
    }
    else {
        //do nothing
    }
}

function AddUpdateBrokerNote() {
    if (fnValidateBrokerNote()) {
        fnSubmitBrokerNote();
    }
    else {

    }
}

function fnSubmitBrokerNote() {
    $("#Loader").show();
    var test = new FormData();
    var preClearanceRequestId = $("#preClearanceRequestIdBN").val();
    var tempFile = $('#btnBrokernote').get(0).files[0].name;
    //  var brokerNote = tempFile.split('.')[0] + "_" + preClearanceRequestId + "." + tempFile.split('.')[1];
    var brokerNote = '';
    var tradeQuantity = $("#txtTradequantityBrokerNote").val();
    var valuePerShare = $('#txtValuePerShare').val();
    $('#txtTotalamount').val(tradeQuantity * valuePerShare);
    var totalAmount = $('#txtTotalamount').val();
    var actualTransactionDate = $('#txtActualdateoftransaction').val();
    var status = $("#txtStatusBN").val();
    var tradeExchange = $("#txtTradeExchangeBN").val();
    var dematAccountID = $("#txtDematAccountBN").val();
    test.append("Object", JSON.stringify({ PreClearanceRequestId: preClearanceRequestId, BrokerNote: brokerNote, ActualTransactionDate: actualTransactionDate, ValuePerShare: valuePerShare, TotalAmount: totalAmount, TradeQuantity: tradeQuantity, Status: status, TradeExchange: tradeExchange, DematAccount: dematAccountID }));
    test.append("Files", $("#btnBrokernote").get(0).files[0]);
    var webUrl = uri + "/api/PreClearanceRequest/SaveBrokerNoteUpload";
    $.ajax({
        url: webUrl,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        contentType: false,
        async: false,
        processData: false,
        data: test,
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                if (msg.PreClearanceRequest.PreClearanceRequestId == preClearanceRequestId) {
                    // UploadFiles(brokerNote);
                    //$("#btnEdit_" + msg.PreClearanceRequest.PreClearanceRequestId).attr('onclick', 'javascript:fnEditPreClearanceRequest(' + msg.PreClearanceRequest.PreClearanceRequestId + ',\'' + msg.PreClearanceRequest.PreClearanceRequestedForName + '\',\'' + msg.PreClearanceRequest.PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequest.SecurityTypeName + '\',\'' + msg.PreClearanceRequest.SecurityType + '\',\'' + msg.PreClearanceRequest.TradeCompanyName + '\',\'' + msg.PreClearanceRequest.TradeCompany + '\',\'' + msg.PreClearanceRequest.TradeQuantity + '\',\'' + msg.PreClearanceRequest.TransactionTypeName + '\',\'' + msg.PreClearanceRequest.TransactionType + '\',\'' + msg.PreClearanceRequest.TradeExchangeName + '\',\'' + msg.PreClearanceRequest.TradeExchange + '\',\'' + msg.PreClearanceRequest.DematAccount + '\',\'' + msg.PreClearanceRequest.TradeDate.split(" ")[0] + '\',\'' + msg.PreClearanceRequest.Status + '\');');
                    //  $("#btnCancelrequest_" + msg.PreClearanceRequest.PreClearanceRequestId).attr('onclick', 'javascript:fnEditPreClearanceRequest(' + msg.PreClearanceRequest.PreClearanceRequestId + ',\'' + msg.PreClearanceRequest.PreClearanceRequestedForName + '\',\'' + msg.PreClearanceRequest.PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequest.SecurityTypeName + '\',\'' + msg.PreClearanceRequest.SecurityType + '\',\'' + msg.PreClearanceRequest.TradeCompanyName + '\',\'' + msg.PreClearanceRequest.TradeCompany + '\',\'' + msg.PreClearanceRequest.TradeQuantity + '\',\'' + msg.PreClearanceRequest.TransactionTypeName + '\',\'' + msg.PreClearanceRequest.TransactionType + '\',\'' + msg.PreClearanceRequest.TradeExchangeName + '\',\'' + msg.PreClearanceRequest.TradeExchange + '\',\'' + msg.PreClearanceRequest.DematAccount + '\',\'' + msg.PreClearanceRequest.TradeDate.split(" ")[0] + '\',\'' + msg.PreClearanceRequest.Status + '\');');
                    $("#btnUploadbrokernote_" + msg.PreClearanceRequest.PreClearanceRequestId).attr('onclick', 'javascript:fnEditBrokerNote(' + msg.PreClearanceRequest.PreClearanceRequestId + ',\'' + msg.PreClearanceRequest.BrokerNote + '\',\'' + msg.PreClearanceRequest.ActualTransactionDate.split(" ")[0] + '\',\'' + msg.PreClearanceRequest.ValuePerShare + '\',\'' + msg.PreClearanceRequest.TotalAmount + '\',\'' + msg.PreClearanceRequest.TradeQuantity + '\',\'' + msg.PreClearanceRequest.Status + '\',\'' + msg.PreClearanceRequest.TradeExchange + '\',\'' + msg.PreClearanceRequest.DematAccount + '\',\'' + msg.PreClearanceRequest.PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequest.TradeCompany + '\',\'' + msg.PreClearanceRequest.TransactionType + '\',\'' + msg.PreClearanceRequest.TradeDate.split(" ")[0] + '\');');

                    $("#btnEdit_" + msg.PreClearanceRequest.PreClearanceRequestId).attr("data-target", "#stack1");
                    $("#btnEdit_" + msg.PreClearanceRequest.PreClearanceRequestId).attr("data-toggle", "modal");

                    $("#btnUploadbrokernote_" + msg.PreClearanceRequest.PreClearanceRequestId).attr("data-target", "#basic1");
                    $("#btnUploadbrokernote_" + msg.PreClearanceRequest.PreClearanceRequestId).attr("data-toggle", "modal");

                    var table = $('#tbl-preclearance-setup').DataTable();
                    table.destroy();
                    initializeDataTable();
                }
                fnClearFormBrokerNote();
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

function fnEditBrokerNote(PreClearanceRequestId, BrokerNote, ActualTransactionDate, ValuePerShare, TotalAmount, TradeQuantity, Status, TradeExchange, DematAccount, PreClearanceRequestedFor, TradeCompany, TransactionType, TradeDate) {
    $("#ddlForBN").val(PreClearanceRequestedFor);
    $("#ddlRestrictedCompaniesBN").val(TradeCompany);
    $("#ddlTypeOfTransactionBN").val(TransactionType);
    $("#txtRequestedTransactionDateBN").val(TradeDate);
    $("#preClearanceRequestIdBN").val(PreClearanceRequestId);
    $("#txtBrokerNoteFileName").val(BrokerNote);
    $('#txtActualdateoftransaction').val(ActualTransactionDate);
    $('#txtValuePerShare').val(ValuePerShare);
    $('#txtTotalamount').val(ValuePerShare * TradeQuantity);
    $("#txtTradequantityBrokerNote").val(TradeQuantity);
    $("#txtTradeExchangeBN").val(TradeExchange);
    $("#txtStatusBN").val(Status);
    $("#txtDematAccountBN").val(DematAccount);
    // fnGetDematAccount(TradeExchange, "fnEditBrokerNote");
    fnGetDematAccount(PreClearanceRequestedFor, "fnEditBrokerNote");
    $("#ddlDematAccountBrokerNote").val(DematAccount);
    if (Status == "InApproval") {
        //$("#btnSubmitBrokerNote").hide();
        //$("#btnBrokernote").prop("disabled", true);
        //$("#txtValuePerShare").prop("readonly", true);
        //$("#txtTradequantityBrokerNote").prop("readonly", true);
        //$("#txtTotalamount").prop("readonly", true);
        //$("#ddlTradeExchange").prop("disabled", true);
        //$("#ddlDematAccountBrokerNote").prop("disabled", true);
        //$("#txtActualdateoftransaction").prop("disabled", true);
        $("#btnSubmitBrokerNote").show();
        $("#btnBrokernote").prop("disabled", false);
        $("#txtValuePerShare").prop("readonly", false);
        $("#txtTradequantityBrokerNote").prop("readonly", false);
        $("#txtTotalamount").prop("readonly", true);
        //    $("#ddlTradeExchange").prop("disabled", false);
        $("#ddlDematAccountBrokerNote").prop("disabled", true);
        $("#txtActualdateoftransaction").prop("disabled", false);
    }
    else if (Status == "Draft") {
        $("#btnSubmitBrokerNote").show();
        $("#btnBrokernote").prop("disabled", false);
        $("#txtValuePerShare").prop("readonly", false);
        $("#txtTradequantityBrokerNote").prop("readonly", true);
        $("#txtTotalamount").prop("readonly", true);
        //  $("#ddlTradeExchange").prop("disabled", false);
        $("#ddlDematAccountBrokerNote").prop("disabled", true);
        $("#txtActualdateoftransaction").prop("disabled", false);
    }
    else if (Status == "Cancel") {
        $("#btnSubmitBrokerNote").show();
        $("#btnBrokernote").prop("disabled", false);
        $("#txtValuePerShare").prop("readonly", false);
        $("#txtTradequantityBrokerNote").prop("readonly", true);
        $("#txtTotalamount").prop("readonly", true);
        //  $("#ddlTradeExchange").prop("disabled", false);
        $("#ddlDematAccountBrokerNote").prop("disabled", true);
        $("#txtActualdateoftransaction").prop("disabled", false);
    }
    else {
        //do nothing
    }
}

function fnValidateBrokerNote() {
    if ($('#btnBrokernote').val() == undefined || $('#btnBrokernote').val() == null || $('#btnBrokernote').val().trim() == '') {
        return false;
    }
    if ($('#txtTradequantityBrokerNote').val() == undefined || $('#txtTradequantityBrokerNote').val() == null || $('#txtTradequantityBrokerNote').val().trim() == '') {
        return false;
    }
    if ($('#txtValuePerShare').val() == undefined || $('#txtValuePerShare').val() == null || $('#txtValuePerShare').val().trim() == '') {
        return false;
    }
    if ($('#ddlDematAccountBrokerNote').val() == undefined || $('#ddlDematAccountBrokerNote').val() == null || $('#ddlDematAccountBrokerNote').val().trim() == '' || $('#ddlDematAccountBrokerNote').val().trim() == '0') {
        return false;
    }
    if ($('#txtActualdateoftransaction').val() == undefined || $('#txtActualdateoftransaction').val() == null || $('#txtActualdateoftransaction').val().trim() == '') {
        return false;
    }
    return true;
}

function fnClearFormBrokerNote() {
    $('#btnBrokernote').val('');
    $('#txtTradequantityBrokerNote').val('');
    $('#txtValuePerShare').val('');
    $('#txtTotalamount').val('');
    $('#ddlDematAccountBrokerNote').val('');
    $('#txtActualdateoftransaction').val('');
}

function fnClearForm() {
    $("#preClearanceRequestId").val('');
    $("#ddlFor").val('');
    $("#ddlTypeOfSecurity").val('');
    $("#ddlRestrictedCompanies").val('');
    $("#txtTradeQuantity").val('');
    $("#ddlTypeOfTransaction").val('');
    $("#ddlTradeExchange").val('');
    $("#ddlDematAccount").val('');
    $("#txtRequestedTransactionDate").val('');
}

function fnGetUserList() {
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/GetUserList";
    $.ajax({
        type: "GET",
        url: webUrl,
        data: "{}",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            if (msg.StatusFl == true) {
                var result = "";
                result += '<option value="0" selected>---Select User---</option>';
                for (var i = 0; i < msg.UserList.length; i++) {
                   
                    //result += '<td id="tdUserLogin_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].LOGIN_ID + '</td>';
                    //result += '<td id="tdUserSalutation_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].SALUTATION + '</td>';
                    //result += '<td id="tdUserNM_' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].USER_NM + '</td>';
                    result += '<option value="' + msg.UserList[i].LOGIN_ID + '" selected>' + msg.UserList[i].USER_NM + '</option>';
                   
                }
                
                $("#ddlFor_user").html(result);
               
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
    //   $('#loadingmessage').modal('hide');

}
