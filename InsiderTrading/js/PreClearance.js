var arrSecType = new Array();
var arrTransType = new Array();
var arrDetails = new Array();
var sTransType = "";
var BrokerDetailsManadatory = false;
function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}
$(document).ready(function () {
    $('#txtRequestedTransactionDate').datepicker({
        todayHighlight: true,
        autoclose: true,
        format: $("input[id*=hdnJSDateFormat]").val(),
        clearBtn: true,
        startDate: "today",
        endDate: "today",
        daysOfWeekDisabled: [0, 6],
        showOnFocus: false
    }).val(FormatDate(GetCurrentDt(), $("input[id*=hdnDateFormat]").val()));
    $("#txtActualdateoftransaction").datepicker({
        todayHighlight: true,
        autoclose: true,
        format: $("input[id*=hdnJSDateFormat]").val(),
        clearBtn: true,
        endDate: "today",
        daysOfWeekDisabled: [0, 6]
    });

    fnGetTypeOfSecurity();
    fnGetTypeOfTransaction();
    fnGetDetailsOfUser();
    fnGetTypeOfRestrictedCompanies();

    const urlParams = new URLSearchParams(window.location.search);
    const myParam = urlParams.get('Status');

    if (myParam == "Draft") {
        $('#bindStatus').val(myParam).change();
        fnStatusChange();
    }
    else if (myParam == "InApproval") {
        $('#bindStatus').val(myParam).change();
        fnStatusChange();
    }
    else if (myParam == "Approved") {
        $('#bindStatus').val(myParam).change();
        fnStatusChange();
    }
    else if (myParam == "Rejected") {
        $('#bindStatus').val(myParam).change();
        fnStatusChange();
    }
    else {
        fnGetPreClearanceRequest();
    }

    $("#ddlFor").on('change', function () {
        var SelVal = $(this).val();

        if (!(SelVal == "" || SelVal == null || SelVal == "Select")) {
            var currentValue = $(this).val();
            fnGetDematAccount(currentValue, null);
            var DematAccount = $("#ddlDematAccount").val();
            if (!(DematAccount == "" || DematAccount == null || DematAccount == undefined)) {
                fnGetDematCurrentHoldings(DematAccount);
            }
        }
    }).trigger('change');
    $("#ddlDematAccount").on('change', function () {
        var DematAccount = $(this).val();
        if (!(DematAccount == "" || DematAccount == null || DematAccount == undefined)) {
            fnGetDematCurrentHoldings(DematAccount);
        }
    }).trigger('change');
    $("#txtActualTradequantityBrokerNote").on('focusout', function () {
        if ($("#txtActualTradequantityBrokerNote").val() !== null && $("#txtActualTradequantityBrokerNote").val() !== undefined && $("#txtActualTradequantityBrokerNote").val() !== '' && $("#txtValuePerShare").val() !== null && $("#txtValuePerShare").val() !== undefined && $("#txtValuePerShare").val() !== '') {
            var totalAmount = $("#txtActualTradequantityBrokerNote").val() * $("#txtValuePerShare").val();
            $("#txtTotalamount").val(totalAmount);
        }
        else {
            if ($("#txtTradequantityBrokerNote").val() !== null && $("#txtTradequantityBrokerNote").val() !== undefined && $("#txtTradequantityBrokerNote").val() !== '' && $("#txtValuePerShare").val() !== null && $("#txtValuePerShare").val() !== undefined && $("#txtValuePerShare").val() !== '') {
                var totalAmount = $("#txtTradequantityBrokerNote").val() * $("#txtValuePerShare").val();
                $("#txtTotalamount").val(totalAmount);
            }
        }
    })
    $("#txtValuePerShare").on('focusout', function () {
        if ($("#txtActualTradequantityBrokerNote").val() !== null && $("#txtActualTradequantityBrokerNote").val() !== undefined && $("#txtActualTradequantityBrokerNote").val() !== '' && $("#txtValuePerShare").val() !== null && $("#txtValuePerShare").val() !== undefined && $("#txtValuePerShare").val() !== '') {
            var totalAmount = $("#txtActualTradequantityBrokerNote").val() * $("#txtValuePerShare").val();
            $("#txtTotalamount").val(totalAmount);
        }
        else {
            if ($("#txtTradequantityBrokerNote").val() !== null && $("#txtTradequantityBrokerNote").val() !== undefined && $("#txtTradequantityBrokerNote").val() !== '' && $("#txtValuePerShare").val() !== null && $("#txtValuePerShare").val() !== undefined && $("#txtValuePerShare").val() !== '') {
                var totalAmount = $("#txtTradequantityBrokerNote").val() * $("#txtValuePerShare").val();
                $("#txtTotalamount").val(totalAmount);
            }
        }
    })
    fnCalculateMultiTrade();
    // Add event listener for opening and closing details
    $('#tbl-preclearance-setup tbody').on('click', 'td.details-control', function () {
        var table = $('#tbl-preclearance-setup').DataTable();
        var tr = $(this).closest('tr');
        var row = table.row(tr);

        if (row.child.isShown()) {
            row.child.hide();
            tr.removeClass('shown');
        }
        else {
            row.child(format(row.data())).show();
            tr.addClass('shown');
        }
    });
    $('.filter-status').on('change', function () {

        var tablefilter = $('#tbl-preclearance-setup').DataTable();

        var FilterValue = $('input:radio[name="PreClearancestatus"]:checked').val();

        if (FilterValue == 'Relatives') {
            tablefilter.column(4).search('^((?!\\Self).)*$', true, false, false).draw(false);
        }
        else if (FilterValue == 'Self') {
            var PreClearancestatus = $('input:radio[name="PreClearancestatus"]:checked').map(function () {
                return '^' + this.value + '$';
            }).get().join('|');

            tablefilter.column(4).search(PreClearancestatus, true, false, false).draw(false);

        }
        else if (FilterValue == '') {
            tablefilter.column(4).search('', true, false, false).draw(false);
        }
        else {
            // tablefilter.column(4).search().draw(true);

        }

    }).trigger('change');

    //alert("Here");
    var id = getUrlVars()["PreClearanceRequestId"];
    var qty = getUrlVars()["TradeQuantity"];
    var demat = getUrlVars()["DematAccount"];
    var rFor = getUrlVars()["PreClearanceRequestedFor"];
    var cmpnId = getUrlVars()["TradeCompany"];
    var reqDt = getUrlVars()["TradeDate"];
    var transactionTyp = getUrlVars()["TransactionType"];
    var TradeExchange = getUrlVars()["TradeExchange"];
    var proposedTransactionThrough = getUrlVars()["proposedTransactionThrough"];
    var validFrm = getUrlVars()["tradingFrom"];
    var validTo = getUrlVars()["tradingTo"];
    var eQty = getUrlVars()["RemainingTradeQuantity"];
    var eStatus = getUrlVars()["EStatus"];
    /*alert("id=" + id);
    alert("qty=" + qty);
    alert("demat=" + demat);
    alert("rFor=" + rFor);
    alert("cmpnId=" + cmpnId);
    alert("reqDt=" + reqDt);
    alert("transactionTyp=" + transactionTyp);
    alert("TradeExchange=" + TradeExchange);
    alert("proposedTransactionThrough=" + proposedTransactionThrough);
    alert("validFrm=" + validFrm);
    alert("validTo=" + validTo);
    alert("eQty=" + eQty);
    alert("eStatus=" + eStatus);*/
    if (id != null && id != undefined) {
        //alert("In here");
        fnEditBrokerNote(
            id, 'null', 'null', 'null', 'null', qty, 'null', eStatus, TradeExchange, demat, rFor, cmpnId, transactionTyp,
            reqDt, 'null', proposedTransactionThrough, validFrm, validTo, eQty
        );
    }
});
function fnStatusChange() {
    //alert("In fnStatusChange");
    $("#Loader").show();
    var webUrl = uri + "/api/PreClearanceRequest/GetPreClearanceRequestListFilterByStatus";
    $.ajax({
        url: webUrl,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        data: JSON.stringify({ Status: $("#bindStatus").val() }),
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                var result = "";
                if (msg.PreClearanceRequestList !== null) {
                    lstPreclearanceRequest = msg.PreClearanceRequestList;
                    /*
                    for (var i = 0; i < msg.PreClearanceRequestList.length; i++) {
                        userRole = msg.PreClearanceRequestList[i].userRole;
                        var ReqId = (msg.PreClearanceRequestList[i].PreClearanceRequestId + "").padStart(4, "0");
                        result += '<tr id="tr_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">';
                        if (msg.PreClearanceRequestList[i].PreClearanceRequestedForName == "") {
                            msg.PreClearanceRequestList[i].PreClearanceRequestedForName = "Self";
                        }
                        result += '<td></td>';
                        result += '<td id="tdActions_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">';
                        var innerStr = "";
                        innerStr += '<div class="btn-group">';
                        innerStr += '<a class="btn blue dropdown-toggle" href="javascript:;" data-toggle="dropdown">';
                        innerStr += '<i class="fa fa-user"></i> Actions';
                        innerStr += '<i class="fa fa-angle-down"></i>';
                        innerStr += '</a>';
                        innerStr += '<ul class="dropdown-menu">';
                        if (msg.PreClearanceRequestList[i].Status != "Approved" && msg.PreClearanceRequestList[i].Status != "Rejected") {
                            innerStr += '<li>';
                            innerStr += '<a  id="btnEdit_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '" data-toggle="modal" data-target="#stack1" onclick=\"javascript:fnEditPreClearanceRequest(' + msg.PreClearanceRequestList[i].PreClearanceRequestId + ',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedForName + '\',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequestList[i].SecurityTypeName + '\',\'' + msg.PreClearanceRequestList[i].SecurityType + '\',\'' + msg.PreClearanceRequestList[i].TradeCompanyName + '\',\'' + msg.PreClearanceRequestList[i].TradeCompany + '\',\'' + msg.PreClearanceRequestList[i].TradeQuantity + '\',\'' + msg.PreClearanceRequestList[i].TransactionTypeName + '\',\'' + msg.PreClearanceRequestList[i].TransactionType + '\',\'' + msg.PreClearanceRequestList[i].TradeExchangeName + '\',\'' + msg.PreClearanceRequestList[i].TradeExchange + '\',\'' + msg.PreClearanceRequestList[i].DematAccount + '\',\'' + msg.PreClearanceRequestList[i].TradeDate + '\',\'' + msg.PreClearanceRequestList[i].Status + '\',\'' + msg.PreClearanceRequestList[i].shareCurrentMarketPrice + '\',\'' + msg.PreClearanceRequestList[i].proposedTransactionThrough + '\');\">';
                            innerStr += '<i class="fa fa-pencil"></i> View/Withdraw Request </a>';
                            innerStr += '</li>';
                        }
                        else {
                            innerStr += '<li style="display:none">';
                            innerStr += '<a  id="btnEdit_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '" data-toggle="modal" data-target="#stack1" onclick=\"javascript:fnEditPreClearanceRequest(' + msg.PreClearanceRequestList[i].PreClearanceRequestId + ',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedForName + '\',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequestList[i].SecurityTypeName + '\',\'' + msg.PreClearanceRequestList[i].SecurityType + '\',\'' + msg.PreClearanceRequestList[i].TradeCompanyName + '\',\'' + msg.PreClearanceRequestList[i].TradeCompany + '\',\'' + msg.PreClearanceRequestList[i].TradeQuantity + '\',\'' + msg.PreClearanceRequestList[i].TransactionTypeName + '\',\'' + msg.PreClearanceRequestList[i].TransactionType + '\',\'' + msg.PreClearanceRequestList[i].TradeExchangeName + '\',\'' + msg.PreClearanceRequestList[i].TradeExchange + '\',\'' + msg.PreClearanceRequestList[i].DematAccount + '\',\'' + msg.PreClearanceRequestList[i].TradeDate + '\',\'' + msg.PreClearanceRequestList[i].Status + '\',\'' + msg.PreClearanceRequestList[i].shareCurrentMarketPrice + '\',\'' + msg.PreClearanceRequestList[i].proposedTransactionThrough + '\');\">';
                            innerStr += '<i class="fa fa-pencil"></i> View/Withdraw Request </a>';
                            innerStr += '</li>';
                        }
                        if (msg.PreClearanceRequestList[i].Status == "Approved" && msg.PreClearanceRequestList[i].TradeDate.trim() != '') {
                            innerStr += '<li id="liUploadbrokernote_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">';
                            innerStr += '<a  id="btnUploadbrokernote_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '" data-toggle="modal" data-target="#basic1" onclick=\"javascript:fnEditBrokerNote(' + msg.PreClearanceRequestList[i].PreClearanceRequestId + ',\'' + msg.PreClearanceRequestList[i].BrokerNote + '\',\'' + (msg.PreClearanceRequestList[i].ActualTransactionDate !== null ? msg.PreClearanceRequestList[i].ActualTransactionDate : msg.PreClearanceRequestList[i].ActualTransactionDate) + '\',\'' + msg.PreClearanceRequestList[i].ValuePerShare + '\',\'' + msg.PreClearanceRequestList[i].TotalAmount + '\',\'' + msg.PreClearanceRequestList[i].TradeQuantity + '\',\'' + msg.PreClearanceRequestList[i].ActualTradeQuantity + '\',\'' + msg.PreClearanceRequestList[i].Status + '\',\'' + msg.PreClearanceRequestList[i].TradeExchange + '\',\'' + msg.PreClearanceRequestList[i].DematAccount + '\',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequestList[i].TradeCompany + '\',\'' + msg.PreClearanceRequestList[i].TransactionType + '\',\'' + msg.PreClearanceRequestList[i].TradeDate + '\',\'' + msg.PreClearanceRequestList[i].remarks + '\');\">';
                            innerStr += '<i class="fa fa-upload"></i>Upload Trade Details </a>';
                            innerStr += '</li>';
                        }
                        else {
                            innerStr += '<li id="liUploadbrokernote_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '" style="display:none">';
                            innerStr += '<a  id="btnUploadbrokernote_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '" data-toggle="modal" data-target="#basic1" onclick=\"javascript:fnEditBrokerNote(' + msg.PreClearanceRequestList[i].PreClearanceRequestId + ',\'' + msg.PreClearanceRequestList[i].BrokerNote + '\',\'' + (msg.PreClearanceRequestList[i].ActualTransactionDate !== null ? msg.PreClearanceRequestList[i].ActualTransactionDate : msg.PreClearanceRequestList[i].ActualTransactionDate) + '\',\'' + msg.PreClearanceRequestList[i].ValuePerShare + '\',\'' + msg.PreClearanceRequestList[i].TotalAmount + '\',\'' + msg.PreClearanceRequestList[i].TradeQuantity + '\',\'' + msg.PreClearanceRequestList[i].ActualTradeQuantity + '\',\'' + msg.PreClearanceRequestList[i].Status + '\',\'' + msg.PreClearanceRequestList[i].TradeExchange + '\',\'' + msg.PreClearanceRequestList[i].DematAccount + '\',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequestList[i].TradeCompany + '\',\'' + msg.PreClearanceRequestList[i].TransactionType + '\',\'' + msg.PreClearanceRequestList[i].TradeDate + '\',\'' + msg.PreClearanceRequestList[i].remarks + '\');\">';
                            innerStr += '<i class="fa fa-upload"></i>Upload Trade Details </a>';
                            innerStr += '</li>';
                        }
                        innerStr += '<li id="liZippedTradeFilesDownload_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">';
                        innerStr += '<a href="javascript:fnDownloadTradingZipFile(\'' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '\');"  id="btnZippedTradeFilesDownload_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">';
                        innerStr += '<i class="fa fa-download"></i>Download Trade Files</a>';
                        innerStr += '</li>';
                        innerStr += '<li class="divider"> </li>';
                        innerStr += '</ul>';
                        innerStr += '</div>';
                        result += innerStr + '</td>';
                        result += '<td id="tdPreClearanceRequestId_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + ReqId + '</td>';
                        result += '<td id="tdPreClearanceRequestFor_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].PreClearanceRequestedForName + '</td>';
                        result += '<td id="tdRelationName_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].relationName + '</td>';
                        result += '<td id="tdTradeQuantity_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].TradeQuantity + '</td>';
                        result += '<td id="tdTypeOfTransaction_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].TransactionTypeName + '</td>';
                        result += '<td style="display:none;" id="tdTradeExchange_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].TradeExchangeName + '</td>';
                        result += '<td  style="display: none;" id="tdDematAccount_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].DematAccount + '</td>';
                        result += '<td id="tdRequestedTransactionDate_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].TradeDate + '</td>';
                        if (msg.PreClearanceRequestList[i].Status == "InApproval") {
                            msg.PreClearanceRequestList[i].Status = "Pending for  Approval";
                            result += '<td style="background-color:orange" id="tdPreClearanceRequestStatus_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].Status + '</td>';
                        }
                        else if (msg.PreClearanceRequestList[i].Status == "Approved") {
                            if (msg.PreClearanceRequestList[i].TradeDate == "") {
                                result += '<td style="background-color:red" id="tdPreClearanceRequestStatus_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">Pre-clearance Not Taken</td>';
                            }
                            else {
                                result += '<td style="background-color:green" id="tdPreClearanceRequestStatus_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].Status + '</td>';
                            }
                        }
                        else if (msg.PreClearanceRequestList[i].Status == "Draft") {
                            result += '<td id="tdPreClearanceRequestStatus_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].Status + '</td>';
                        }
                        else {
                            result += '<td style="background-color:red" id="tdPreClearanceRequestStatus_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].Status + '</td>';
                        }
                        result += '<td>' + msg.PreClearanceRequestList[i].reviewedOn + '</td>';
                        result += '<td>' + msg.PreClearanceRequestList[i].reviewedBy + '</td>';
                        result += '<td>' + msg.PreClearanceRequestList[i].reviewerRemarks + '</td>';
                        result += '</tr>';
                    }
                    */
                    for (var i = 0; i < msg.PreClearanceRequestList.length; i++) {
                        userRole = msg.PreClearanceRequestList[i].userRole;
                        var ReqId = (msg.PreClearanceRequestList[i].PreClearanceRequestId + "").padStart(4, "0");
                        result += '<tr id="tr_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">';
                        if (msg.PreClearanceRequestList[i].PreClearanceRequestedForName == "") {
                            msg.PreClearanceRequestList[i].PreClearanceRequestedForName = "Self";
                        }
                        result += '<td style="margin-bottom:10px !important;" id="tdActions_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">';
                        var innerStr = "";
                        innerStr += '<div class="btn-group">';
                        innerStr += '<a class="btn blue dropdown-toggle" href="javascript:;" data-toggle="dropdown">';
                        innerStr += '<i class="fa fa-user"></i> Actions';
                        innerStr += '<i class="fa fa-angle-down"></i>';
                        innerStr += '</a>';
                        innerStr += '<ul class="dropdown-menu">';
                        if (msg.PreClearanceRequestList[i].Status != "Approved" && msg.PreClearanceRequestList[i].Status != "Rejected") {
                            innerStr += '<li>';
                            innerStr += '<a  id="btnEdit_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '" data-toggle="modal" data-target="#stack1" onclick=\"javascript:fnEditPreClearanceRequest(' + msg.PreClearanceRequestList[i].PreClearanceRequestId + ',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedForName + '\',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequestList[i].SecurityTypeName + '\',\'' + msg.PreClearanceRequestList[i].SecurityType + '\',\'' + msg.PreClearanceRequestList[i].TradeCompanyName + '\',\'' + msg.PreClearanceRequestList[i].TradeCompany + '\',\'' + msg.PreClearanceRequestList[i].TradeQuantity + '\',\'' + msg.PreClearanceRequestList[i].TransactionTypeName + '\',\'' + msg.PreClearanceRequestList[i].TransactionType + '\',\'' + msg.PreClearanceRequestList[i].TradeExchangeName + '\',\'' + msg.PreClearanceRequestList[i].TradeExchange + '\',\'' + msg.PreClearanceRequestList[i].DematAccount + '\',\'' + msg.PreClearanceRequestList[i].TradeDate + '\',\'' + msg.PreClearanceRequestList[i].Status + '\',\'' + msg.PreClearanceRequestList[i].shareCurrentMarketPrice + '\',\'' + msg.PreClearanceRequestList[i].proposedTransactionThrough + '\');\">';
                            innerStr += '<i class="fa fa-pencil"></i> View/Withdraw Request </a>';
                            innerStr += '</li>';
                        }
                        else {
                            innerStr += '<li style="display:none">';
                            innerStr += '<a  id="btnEdit_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '" data-toggle="modal" data-target="#stack1" onclick=\"javascript:fnEditPreClearanceRequest(' + msg.PreClearanceRequestList[i].PreClearanceRequestId + ',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedForName + '\',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequestList[i].SecurityTypeName + '\',\'' + msg.PreClearanceRequestList[i].SecurityType + '\',\'' + msg.PreClearanceRequestList[i].TradeCompanyName + '\',\'' + msg.PreClearanceRequestList[i].TradeCompany + '\',\'' + msg.PreClearanceRequestList[i].TradeQuantity + '\',\'' + msg.PreClearanceRequestList[i].TransactionTypeName + '\',\'' + msg.PreClearanceRequestList[i].TransactionType + '\',\'' + msg.PreClearanceRequestList[i].TradeExchangeName + '\',\'' + msg.PreClearanceRequestList[i].TradeExchange + '\',\'' + msg.PreClearanceRequestList[i].DematAccount + '\',\'' + msg.PreClearanceRequestList[i].TradeDate + '\',\'' + msg.PreClearanceRequestList[i].Status + '\',\'' + msg.PreClearanceRequestList[i].shareCurrentMarketPrice + '\',\'' + msg.PreClearanceRequestList[i].proposedTransactionThrough + '\');\">';
                            innerStr += '<i class="fa fa-pencil"></i> View/Withdraw Request </a>';
                            innerStr += '</li>';
                        }
                        if (msg.PreClearanceRequestList[i].Status == "Approved" && msg.PreClearanceRequestList[i].TradeDate.trim() != '' && msg.PreClearanceRequestList[i].TradeExecutedStatus != "Closed") {
                            innerStr += '<li id="liUploadbrokernote_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">';
                            innerStr += '<a  id="btnUploadbrokernote_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '" onclick=\"javascript:fnEditBrokerNote(' + msg.PreClearanceRequestList[i].PreClearanceRequestId + ',\'' + msg.PreClearanceRequestList[i].BrokerNote + '\',\'' + (msg.PreClearanceRequestList[i].ActualTransactionDate !== null ? msg.PreClearanceRequestList[i].ActualTransactionDate : msg.PreClearanceRequestList[i].ActualTransactionDate) + '\',\'' + msg.PreClearanceRequestList[i].ValuePerShare + '\',\'' + msg.PreClearanceRequestList[i].TotalAmount + '\',\'' + msg.PreClearanceRequestList[i].TradeQuantity + '\',\'' + msg.PreClearanceRequestList[i].ActualTradeQuantity + '\',\'' + msg.PreClearanceRequestList[i].Status + '\',\'' + msg.PreClearanceRequestList[i].TradeExchange + '\',\'' + msg.PreClearanceRequestList[i].DematAccount + '\',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequestList[i].TradeCompany + '\',\'' + msg.PreClearanceRequestList[i].TransactionType + '\',\'' + msg.PreClearanceRequestList[i].TradeDate + '\',\'' + msg.PreClearanceRequestList[i].remarks + '\',\'' + msg.PreClearanceRequestList[i].proposedTransactionThrough + '\',\'' + msg.PreClearanceRequestList[i].tradingFrom + '\',\'' + msg.PreClearanceRequestList[i].tradingTo + '\',\'' + msg.PreClearanceRequestList[i].RemainingTradeQuantity + '\');\">';
                            innerStr += '<i class="fa fa-upload"></i>Upload Trade Details </a>';
                            innerStr += '</li>';
                        }
                        else {
                            innerStr += '<li id="liUploadbrokernote_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '" style="display:none">';
                            innerStr += '<a  id="btnUploadbrokernote_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '" data-toggle="modal" data-target="#basic1" onclick=\"javascript:fnEditBrokerNote(' + msg.PreClearanceRequestList[i].PreClearanceRequestId + ',\'' + msg.PreClearanceRequestList[i].BrokerNote + '\',\'' + (msg.PreClearanceRequestList[i].ActualTransactionDate !== null ? msg.PreClearanceRequestList[i].ActualTransactionDate : msg.PreClearanceRequestList[i].ActualTransactionDate) + '\',\'' + msg.PreClearanceRequestList[i].ValuePerShare + '\',\'' + msg.PreClearanceRequestList[i].TotalAmount + '\',\'' + msg.PreClearanceRequestList[i].TradeQuantity + '\',\'' + msg.PreClearanceRequestList[i].ActualTradeQuantity + '\',\'' + msg.PreClearanceRequestList[i].Status + '\',\'' + msg.PreClearanceRequestList[i].TradeExchange + '\',\'' + msg.PreClearanceRequestList[i].DematAccount + '\',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequestList[i].TradeCompany + '\',\'' + msg.PreClearanceRequestList[i].TransactionType + '\',\'' + msg.PreClearanceRequestList[i].TradeDate + '\',\'' + msg.PreClearanceRequestList[i].remarks + '\',\'' + msg.PreClearanceRequestList[i].proposedTransactionThrough + '\',\'' + msg.PreClearanceRequestList[i].tradingFrom + '\',\'' + msg.PreClearanceRequestList[i].tradingTo + '\',\'' + msg.PreClearanceRequestList[i].RemainingTradeQuantity + '\');\">';
                            innerStr += '<i class="fa fa-upload"></i>Upload Trade Details </a>';
                            innerStr += '</li>';
                        }
                        innerStr += '<li id="liZippedTradeFilesDownload_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">';
                        innerStr += '<a href="javascript:fnDownloadTradingZipFile(\'' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '\');"  id="btnZippedTradeFilesDownload_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">';
                        innerStr += '<i class="fa fa-download"></i>Download Trade Files</a>';
                        innerStr += '</li>';
                        innerStr += '<li class="divider"> </li>';
                        innerStr += '</ul>';
                        innerStr += '</div>';
                        result += innerStr + '</td>';
                        result += '<td id="tdPreClearanceRequestId_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + ReqId + '</td>';
                        result += '<td id="tdPreClearanceRequestFor_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].PreClearanceRequestedForName + '</td>';
                        result += '<td id="tdRelationName_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].relationName + '</td>';
                        result += '<td id="tdTradeQuantity_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].TradeQuantity + '</td>';
                        result += '<td id="tdTypeOfTransaction_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].TransactionTypeName + '</td>';
                        result += '<td style="display:none;" id="tdTradeExchange_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].TradeExchangeName + '</td>';
                        result += '<td style="display: none;" id="tdDematAccount_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].DematAccount + '</td>';
                        result += '<td id="tdRequestedTransactionDate_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + FormatDate(msg.PreClearanceRequestList[i].TradeDate, $("input[id*=hdnDateFormat]").val()) + '</td>';
                        if (msg.PreClearanceRequestList[i].Status == "InApproval") {
                            msg.PreClearanceRequestList[i].Status = "Pending for  Approval";
                            result += '<td style="background-color:orange" id="tdPreClearanceRequestStatus_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].Status + '</td>';
                        }
                        else if (msg.PreClearanceRequestList[i].Status == "Approved") {
                            if (msg.PreClearanceRequestList[i].TradeDate == "") {
                                result += '<td style="background-color:red" id="tdPreClearanceRequestStatus_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">Pre-clearance Not Taken</td>';
                            }
                            else {
                                result += '<td style="background-color:green" id="tdPreClearanceRequestStatus_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].Status + '</td>';
                            }
                        }
                        else if (msg.PreClearanceRequestList[i].Status == "Draft") {
                            result += '<td id="tdPreClearanceRequestStatus_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].Status + '</td>';
                        }
                        else {
                            result += '<td style="background-color:red" id="tdPreClearanceRequestStatus_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].Status + '</td>';
                        }
                        result += '<td>' + FormatDate(msg.PreClearanceRequestList[i].reviewedOn, $("input[id*=hdnDateFormat]").val()) + '</td>';
                        result += '<td>' + msg.PreClearanceRequestList[i].reviewedBy + '</td>';
                        result += '<td>' + msg.PreClearanceRequestList[i].reviewerRemarks + '</td>';
                        result += '</tr>';
                    }
                    $("#tbdPreClearanceList").html(result);
                }
                else {
                    $("#tbdPreClearanceList").html('');
                }
                //var table = $('#tbl-preclearance-setup').DataTable();
                //table.destroy();
                //$("#tbdPreClearanceList").html(result);
                //initializeDataTable();
            }
            else {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    //alert(msg.Msg);
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
    var webUrl = uri + "/api/PreClearanceRequest/GetPreClearanceRequestList";
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
                lstPreclearanceRequest = msg.PreClearanceRequestList;
                //alert($("input[id*=hdnDateFormat]").val());
                for (var i = 0; i < msg.PreClearanceRequestList.length; i++) {
                    userRole = msg.PreClearanceRequestList[i].userRole;
                    var ReqId = (msg.PreClearanceRequestList[i].PreClearanceRequestId + "").padStart(4, "0");
                    result += '<tr id="tr_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">';
                    if (msg.PreClearanceRequestList[i].PreClearanceRequestedForName == "") {
                        msg.PreClearanceRequestList[i].PreClearanceRequestedForName = "Self";
                    }
                    //result += '<td>123</td>';
                    result += '<td style="margin-bottom:10px !important;" id="tdActions_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">';
                    var innerStr = "";
                    innerStr += '<div class="btn-group">';
                    innerStr += '<a class="btn blue dropdown-toggle" href="javascript:;" data-toggle="dropdown">';
                    innerStr += '<i class="fa fa-user"></i> Actions';
                    innerStr += '<i class="fa fa-angle-down"></i>';
                    innerStr += '</a>';
                    innerStr += '<ul class="dropdown-menu">';
                    if (msg.PreClearanceRequestList[i].Status != "Approved" && msg.PreClearanceRequestList[i].Status != "Rejected") {
                        innerStr += '<li>';
                        innerStr += '<a  id="btnEdit_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '" data-toggle="modal" data-target="#stack1" onclick=\"javascript:fnEditPreClearanceRequest(' + msg.PreClearanceRequestList[i].PreClearanceRequestId + ',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedForName + '\',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequestList[i].SecurityTypeName + '\',\'' + msg.PreClearanceRequestList[i].SecurityType + '\',\'' + msg.PreClearanceRequestList[i].TradeCompanyName + '\',\'' + msg.PreClearanceRequestList[i].TradeCompany + '\',\'' + msg.PreClearanceRequestList[i].TradeQuantity + '\',\'' + msg.PreClearanceRequestList[i].TransactionTypeName + '\',\'' + msg.PreClearanceRequestList[i].TransactionType + '\',\'' + msg.PreClearanceRequestList[i].TradeExchangeName + '\',\'' + msg.PreClearanceRequestList[i].TradeExchange + '\',\'' + msg.PreClearanceRequestList[i].DematAccount + '\',\'' + msg.PreClearanceRequestList[i].TradeDate + '\',\'' + msg.PreClearanceRequestList[i].Status + '\',\'' + msg.PreClearanceRequestList[i].shareCurrentMarketPrice + '\',\'' + msg.PreClearanceRequestList[i].proposedTransactionThrough + '\');\">';
                        innerStr += '<i class="fa fa-pencil"></i> View/Withdraw Request </a>';
                        innerStr += '</li>';
                    }
                    else {
                        innerStr += '<li style="display:none">';
                        innerStr += '<a  id="btnEdit_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '" data-toggle="modal" data-target="#stack1" onclick=\"javascript:fnEditPreClearanceRequest(' + msg.PreClearanceRequestList[i].PreClearanceRequestId + ',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedForName + '\',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequestList[i].SecurityTypeName + '\',\'' + msg.PreClearanceRequestList[i].SecurityType + '\',\'' + msg.PreClearanceRequestList[i].TradeCompanyName + '\',\'' + msg.PreClearanceRequestList[i].TradeCompany + '\',\'' + msg.PreClearanceRequestList[i].TradeQuantity + '\',\'' + msg.PreClearanceRequestList[i].TransactionTypeName + '\',\'' + msg.PreClearanceRequestList[i].TransactionType + '\',\'' + msg.PreClearanceRequestList[i].TradeExchangeName + '\',\'' + msg.PreClearanceRequestList[i].TradeExchange + '\',\'' + msg.PreClearanceRequestList[i].DematAccount + '\',\'' + msg.PreClearanceRequestList[i].TradeDate + '\',\'' + msg.PreClearanceRequestList[i].Status + '\',\'' + msg.PreClearanceRequestList[i].shareCurrentMarketPrice + '\',\'' + msg.PreClearanceRequestList[i].proposedTransactionThrough + '\');\">';
                        innerStr += '<i class="fa fa-pencil"></i> View/Withdraw Request </a>';
                        innerStr += '</li>';
                    }
                    if (msg.PreClearanceRequestList[i].Status == "Approved" && msg.PreClearanceRequestList[i].TradeDate.trim() != '' && msg.PreClearanceRequestList[i].TradeExecutedStatus != "Closed") {
                        innerStr += '<li id="liUploadbrokernote_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">';
                        innerStr += '<a  id="btnUploadbrokernote_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '" onclick=\"javascript:fnEditBrokerNote(' + msg.PreClearanceRequestList[i].PreClearanceRequestId + ',\'' + msg.PreClearanceRequestList[i].BrokerNote + '\',\'' + (msg.PreClearanceRequestList[i].ActualTransactionDate !== null ? msg.PreClearanceRequestList[i].ActualTransactionDate : msg.PreClearanceRequestList[i].ActualTransactionDate) + '\',\'' + msg.PreClearanceRequestList[i].ValuePerShare + '\',\'' + msg.PreClearanceRequestList[i].TotalAmount + '\',\'' + msg.PreClearanceRequestList[i].TradeQuantity + '\',\'' + msg.PreClearanceRequestList[i].ActualTradeQuantity + '\',\'' + msg.PreClearanceRequestList[i].Status + '\',\'' + msg.PreClearanceRequestList[i].TradeExchange + '\',\'' + msg.PreClearanceRequestList[i].DematAccount + '\',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequestList[i].TradeCompany + '\',\'' + msg.PreClearanceRequestList[i].TransactionType + '\',\'' + msg.PreClearanceRequestList[i].TradeDate + '\',\'' + msg.PreClearanceRequestList[i].remarks + '\',\'' + msg.PreClearanceRequestList[i].proposedTransactionThrough + '\',\'' + msg.PreClearanceRequestList[i].tradingFrom + '\',\'' + msg.PreClearanceRequestList[i].tradingTo + '\',\'' + msg.PreClearanceRequestList[i].RemainingTradeQuantity + '\');\">';
                        innerStr += '<i class="fa fa-upload"></i>Upload Trade Details </a>';
                        innerStr += '</li>';
                    }
                    else {
                        innerStr += '<li id="liUploadbrokernote_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '" style="display:none">';
                        innerStr += '<a  id="btnUploadbrokernote_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '" data-toggle="modal" data-target="#basic1" onclick=\"javascript:fnEditBrokerNote(' + msg.PreClearanceRequestList[i].PreClearanceRequestId + ',\'' + msg.PreClearanceRequestList[i].BrokerNote + '\',\'' + (msg.PreClearanceRequestList[i].ActualTransactionDate !== null ? msg.PreClearanceRequestList[i].ActualTransactionDate : msg.PreClearanceRequestList[i].ActualTransactionDate) + '\',\'' + msg.PreClearanceRequestList[i].ValuePerShare + '\',\'' + msg.PreClearanceRequestList[i].TotalAmount + '\',\'' + msg.PreClearanceRequestList[i].TradeQuantity + '\',\'' + msg.PreClearanceRequestList[i].ActualTradeQuantity + '\',\'' + msg.PreClearanceRequestList[i].Status + '\',\'' + msg.PreClearanceRequestList[i].TradeExchange + '\',\'' + msg.PreClearanceRequestList[i].DematAccount + '\',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequestList[i].TradeCompany + '\',\'' + msg.PreClearanceRequestList[i].TransactionType + '\',\'' + msg.PreClearanceRequestList[i].TradeDate + '\',\'' + msg.PreClearanceRequestList[i].remarks + '\',\'' + msg.PreClearanceRequestList[i].proposedTransactionThrough + '\',\'' + msg.PreClearanceRequestList[i].tradingFrom + '\',\'' + msg.PreClearanceRequestList[i].tradingTo + '\',\'' + msg.PreClearanceRequestList[i].RemainingTradeQuantity + '\');\">';
                        innerStr += '<i class="fa fa-upload"></i>Upload Trade Details </a>';
                        innerStr += '</li>';
                    }

                    innerStr += '<li id="liZippedTradeFilesDownload_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">';
                    innerStr += '<a href="javascript:fnDownloadTradingZipFile(\'' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '\');"  id="btnZippedTradeFilesDownload_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">';
                    innerStr += '<i class="fa fa-download"></i>Download Trade Files</a>';
                    innerStr += '</li>';
                    innerStr += '<li class="divider"> </li>';
                    innerStr += '</ul>';
                    innerStr += '</div>';
                    result += innerStr + '</td>';
                    result += '<td id="tdPreClearanceRequestId_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + ReqId + '</td>';
                    result += '<td id="tdPreClearanceRequestFor_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].PreClearanceRequestedForName + '</td>';
                    result += '<td id="tdRelationName_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].relationName + '</td>';
                    result += '<td id="tdTradeQuantity_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].TradeQuantity + '</td>';
                    result += '<td id="tdTypeOfTransaction_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].TransactionTypeName + '</td>';
                    result += '<td style="display:none;" id="tdTradeExchange_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].TradeExchangeName + '</td>';
                    result += '<td style="display: none;" id="tdDematAccount_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].DematAccount + '</td>';
                    result += '<td id="tdRequestedTransactionDate_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + FormatDate(msg.PreClearanceRequestList[i].TradeDate, $("input[id*=hdnDateFormat]").val()) + '</td>';
                    if (msg.PreClearanceRequestList[i].Status == "InApproval") {
                        msg.PreClearanceRequestList[i].Status = "Pending for  Approval";
                        result += '<td style="background-color:orange" id="tdPreClearanceRequestStatus_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].Status + '</td>';
                    }
                    else if (msg.PreClearanceRequestList[i].Status == "Approved") {
                        if (msg.PreClearanceRequestList[i].TradeDate == "") {
                            result += '<td style="background-color:red" id="tdPreClearanceRequestStatus_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">Pre-clearance Not Taken</td>';
                        }
                        else {
                            result += '<td style="background-color:green" id="tdPreClearanceRequestStatus_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].Status + '</td>';
                        }
                    }
                    else if (msg.PreClearanceRequestList[i].Status == "Draft") {
                        result += '<td id="tdPreClearanceRequestStatus_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].Status + '</td>';
                    }
                    else {
                        result += '<td style="background-color:red" id="tdPreClearanceRequestStatus_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].Status + '</td>';
                    }
                    result += '<td>' + FormatDate(msg.PreClearanceRequestList[i].reviewedOn, $("input[id*=hdnDateFormat]").val()) + '</td>';
                    result += '<td>' + msg.PreClearanceRequestList[i].reviewedBy + '</td>';
                    result += '<td>' + msg.PreClearanceRequestList[i].reviewerRemarks + '</td>';

                    result += '</tr>';
                }

                //var table = $('#tbl-preclearance-setup').DataTable();
                //table.destroy();
                $("#tbdPreClearanceList").html(result);
                //initializeDataTable();
            }
            else {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else if (msg.Msg.toLowerCase() == "no data found !") {

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
function fnGetTypeOfSecurity() {
    $("#Loader").show();
    var webUrl = uri + "/api/PreClearance/GetTypeOfSecurityList";
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
                arrSecType = new Array();
                var result = "";
                var result2 = "";
                result += "<option value='0'>--Select--</option>";
                for (var i = 0; i < msg.SecurityTypeList.length; i++) {
                    var obj = new Object();
                    obj.Id = msg.SecurityTypeList[i].Id;
                    obj.Nm = msg.SecurityTypeList[i].Name;
                    arrSecType.push(obj);

                    if (msg.SecurityTypeList[i].Name == "Equity") {
                        result2 += "<option value='" + msg.SecurityTypeList[i].Id + "'>" + msg.SecurityTypeList[i].Name + "</option>";
                    }
                    result += "<option value='" + msg.SecurityTypeList[i].Id + "'>" + msg.SecurityTypeList[i].Name + "</option>";
                }
                $("#ddlTypeOfSecurity").append(result2);
                $("#ddlTypeOfSecurityOther").append(result2);
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
    var webUrl = uri + "/api/PreClearance/GetTypeOfTransactionList";
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
                arrTransType = new Array();
                var str = "";
                var strHtml = "";
                for (var i = 0; i < msg.TransactionTypeList.length; i++) {
                    var obj = new Object();
                    obj.Id = msg.TransactionTypeList[i].Id;
                    obj.Name = msg.TransactionTypeList[i].Name;
                    obj.Nature = msg.TransactionTypeList[i].Nature;
                    obj.AllowedWC = msg.TransactionTypeList[i].AllowedWC;
                    obj.AllowedUPSI = msg.TransactionTypeList[i].AllowedUPSI;
                    obj.WCCompliance = msg.TransactionTypeList[i].WCCompliance;
                    obj.UPSICompliance = msg.TransactionTypeList[i].UPSICompliance;
                    obj.LimitCompliance = msg.TransactionTypeList[i].LimitCompliance;
                    arrTransType.push(obj);

                    str += "<li>";
                    str += "<a href='javascript:fnOpenRequest(\"" + msg.TransactionTypeList[i].Name + "\");'>";
                    str += "&nbsp;" + msg.TransactionTypeList[i].Name;
                    str += "</a>";
                    str += "</li>";

                    strHtml += "<option value='" + msg.TransactionTypeList[i].Id + "'>" + msg.TransactionTypeList[i].Name + "</option>";
                }
                $("#ddlTypeOfTransactionBN").html(strHtml);
                $("#ulMenu").html(str);
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
function fnGetDetailsOfUser() {
    $("#Loader").show();
    var webUrl = uri + "/api/PreClearance/GetRelativeDetails";
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
                arrDetails = new Array();
                if (msg.RelativeDetailList != null) {
                    var result = "";
                    if (msg.RelativeDetailList.length > 1) {
                        result += "<option value='Select'>--Select--</option>";
                    }
                    for (var i = 0; i < msg.RelativeDetailList.length; i++) {
                        var obj = new Object();
                        obj.RelativeId = msg.RelativeDetailList[i].ID;
                        obj.RelativeNm = msg.RelativeDetailList[i].relativeName;
                        obj.Pan = msg.RelativeDetailList[i].panNumber;
                        obj.LastTransactionDt = msg.RelativeDetailList[i].LastTransactionDt;
                        result += "<option value='" + msg.RelativeDetailList[i].ID + "'>" + msg.RelativeDetailList[i].relativeName + "</option>";
                        var Demat = new Array();
                        //alert("msg.RelativeDetailList[" + i + "].lstDematAccount.length=" + msg.RelativeDetailList[i].lstDematAccount.length);
                        if (msg.RelativeDetailList[i].lstDematAccount != null) {
                            for (var j = 0; j < msg.RelativeDetailList[i].lstDematAccount.length; j++) {
                                var objdemat = new Object();
                                //alert("msg.RelativeDetailList[" + i + "].lstDematAccount[" + j + "].accountNo=" + msg.RelativeDetailList[i].lstDematAccount[j].accountNo);
                                objdemat.accountNo = msg.RelativeDetailList[i].lstDematAccount[j].accountNo;
                                objdemat.CurrentHolding = msg.RelativeDetailList[i].lstDematAccount[j].CurrentHolding;
                                objdemat.Pledged = msg.RelativeDetailList[i].lstDematAccount[j].Pledged;
                                Demat.push(objdemat);
                            }
                            obj.Demat = Demat;
                            arrDetails.push(obj);
                        }
                    }
                    $("#ddlFor").html(result);
                    $("#ddlForBN").html(result);
                    $("#ddlForOther").html(result);
                }
            }
            else {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    //alert(msg.Msg);
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
function fnGetLastTransactionDt() {
    $("#Loader").show();
    var webUrl = uri + "/api/PreClearance/GetRelativeTransactionDt";
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
                arrDetails = new Array();
                if (msg.RelativeDetailList != null) {
                    for (var i = 0; i < msg.RelativeDetailList.length; i++) {
                        var obj = new Object();
                        obj.RelativeId = msg.RelativeDetailList[i].ID;
                        obj.RelativeNm = msg.RelativeDetailList[i].relativeName;
                        obj.Pan = msg.RelativeDetailList[i].panNumber;

                        var Demat = new Array();
                        //alert("msg.RelativeDetailList[" + i + "].lstDematAccount.length=" + msg.RelativeDetailList[i].lstDematAccount.length);
                        if (msg.RelativeDetailList[i].lstDematAccount != null) {
                            for (var j = 0; j < msg.RelativeDetailList[i].lstDematAccount.length; j++) {
                                var objdemat = new Object();
                                //alert("msg.RelativeDetailList[" + i + "].lstDematAccount[" + j + "].accountNo=" + msg.RelativeDetailList[i].lstDematAccount[j].accountNo);
                                objdemat.accountNo = msg.RelativeDetailList[i].lstDematAccount[j].accountNo;
                                objdemat.CurrentHolding = msg.RelativeDetailList[i].lstDematAccount[j].CurrentHolding;
                                objdemat.Pledged = msg.RelativeDetailList[i].lstDematAccount[j].Pledged;
                                Demat.push(objdemat);
                            }
                            obj.Demat = Demat;
                            arrDetails.push(obj);
                        }
                    }
                }
            }
            else {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    //alert(msg.Msg);
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
function fnOpenRequest(TransactionType) {
    //alert("In function fnOpenRequest");
    //alert("TransactionType=" + TransactionType);
    sTransType = TransactionType;

    var WCCnt = $("input[id*=hdnWCCnt]").val();
    var UPSICnt = $("input[id*=hdnUPSICnt]").val();
    //alert("WCCnt=" + WCCnt);
    //alert("UPSICnt=" + UPSICnt);

    var allowedInWC = false;
    var allowedInUPSI = false;
    if (arrTransType != null) {
        for (var x = 0; x < arrTransType.length; x++) {
            if (arrTransType[x].Name == TransactionType) {
                if (arrTransType[x].AllowedWC == "Yes") {
                    allowedInWC = true;
                }
                if (arrTransType[x].AllowedUPSI == "Yes") {
                    allowedInUPSI = true;
                }
                break;
            }
        }
    }
    if (WCCnt > 0) {
        if (allowedInWC) {
            $("#modalSubmitPrevTradeConfirmation").modal('show');
            return;
        }
        else {
            alert("Pre-clearance request cannot be placed because of Trading Window Closure");
            return;
        }
    }
    else if (UPSICnt > 0) {
        if (allowedInUPSI) {
            $("#modalSubmitPrevTradeConfirmation").modal('show');
            return;
        }
        else {
            alert("Pre-clearance request cannot be placed because you are in possession of UPSI");
            return;
        }
    }

    $("#modalSubmitPrevTradeConfirmation").modal('show');
    //return;
}
function fnOpenPreClearance(typ) {
    //alert("In function fnOpenPreClearance");
    //alert("sTransType=" + sTransType);
    //alert("sTransType=" + sTransType);
    for (var x = 0; x < arrTransType.length; x++) {
        if (arrTransType[x].Name == sTransType) {
            var str = "<option value='" + arrTransType[x].Id + "'>" + arrTransType[x].Name + "</option>";
            $("#ddlTypeOfTransaction").html(str);
            $("#ddlTypeOfTransactionBN").html(str);
        }
    }
    if (typ == 'Yes') {
        fnGetTradeTransactions();
        fnAddRow();
        $("#mdlTradeDetails").modal('show');
    }
    else {
        fnSaveBrokerNote();
    }
}
function fnGetTradeTransactions() {
    $("#Loader").show();
    var webUrl = uri + "/api/PreClearanceRequest/GetTradeTransactions";
    $.ajax({
        url: webUrl,
        type: "GET",
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            if (msg.StatusFl == true) {
                $("#Loader").hide();
                var str = "";
                var strSelf = "";
                var strRelative = "";
                var RowCount = 0;
                var RowCountRelative = 0;
                for (var i = 0; i < msg.TradeTransactionList.length; i++) {

                    if (msg.TradeTransactionList[i].AsPer == "ESOP") {
                        msg.TradeTransactionList[i].TransType = msg.TradeTransactionList[i].TransType + ' (ESOP)';
                    }
                    if (msg.TradeTransactionList[i].RelationNm == "Self") {
                        RowCount = parseInt(RowCount) + 1;
                        strSelf += "<tr>";
                        strSelf += "<td>" + RowCount + "</td>";
                        strSelf += "<td>" + msg.TradeTransactionList[i].RelativeNm + "</td>";
                        strSelf += "<td>" + msg.TradeTransactionList[i].RelationNm + "</td>";
                        strSelf += "<td>" + FormatDate(msg.TradeTransactionList[i].TransDt, $("input[id*=hdnDateFormat]").val()) + "</td>";
                        strSelf += "<td>" + msg.TradeTransactionList[i].TransType + "</td>";
                        //strSelf += "<td>" + msg.TradeTransactionList[i].Pan + "</td>";
                        //strSelf += "<td>" + msg.TradeTransactionList[i].Demat + "</td>";
                        strSelf += "<td style='text-align:right;'>" + msg.TradeTransactionList[i].TransQty + "</td>";
                        strSelf += "<td style='text-align:right;'>" + msg.TradeTransactionList[i].TransVal + "</td>";
                        strSelf += "</tr>";
                    }
                    else {
                        RowCountRelative += parseInt(RowCountRelative) + 1;
                        strRelative += "<tr>";
                        strRelative += "<td>" + RowCountRelative + "</td>";
                        strRelative += "<td>" + msg.TradeTransactionList[i].RelativeNm + "</td>";
                        strRelative += "<td>" + msg.TradeTransactionList[i].RelationNm + "</td>";
                        strRelative += "<td>" + FormatDate(msg.TradeTransactionList[i].TransDt, $("input[id*=hdnDateFormat]").val()) + "</td>";
                        strRelative += "<td>" + msg.TradeTransactionList[i].TransType + "</td>";
                        //strRelative += "<td>" + msg.TradeTransactionList[i].Pan + "</td>";
                        //strRelative += "<td>" + msg.TradeTransactionList[i].Demat + "</td>";
                        strRelative += "<td style='text-align:right;'>" + msg.TradeTransactionList[i].TransQty + "</td>";
                        strRelative += "<td style='text-align:right;'>" + msg.TradeTransactionList[i].TransVal + "</td>";
                        strRelative += "</tr>";
                    }
                }

                str += '<tr><td colspan="7" style="background:#fafafa">Self Transactions</td></tr>';

                if (RowCount == 0) {
                    str += '<tr><td colspan="7">No Recent Transactions</td></tr>';
                }
                else {
                    str += strSelf;
                }

                str += '<tr><td colspan="7" style="background:#fafafa">Relative Transactions</td></tr>';

                if (RowCountRelative == 0) {
                    str += '<tr><td colspan="7">No Recent Transactions</td></tr>';
                }
                else {
                    str += strRelative;
                }


                $("#tbdTransactions").html("");
                $("#tbdTransactions").append(str);
            }
            else {
                $("#Loader").hide();
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    $("#tblTrades").hide();
                }
            }
        },
        error: function (response) {
            $("#Loader").hide();
            alert(response.status + ' ' + response.statusText);
        }
    })
}
function fnAddRow() {
    $("#tbdTrade").html("");
    //alert("In function fnAddRow");
    var str = "";
    str += "<tr>";
    str += "<td style='padding-left:5px;padding-right:5px;padding-top:10px;'>";
    str += "<select id='ddlForTD' class='form-control' onchange='javascript:fnForTD_onChange(this);'>";
    str += "<option value='-1'>Please Select</option>";
    //alert("arrDetails.length=" + arrDetails.length);
    for (var x = 0; x < arrDetails.length; x++) {
        //alert("arrDetails[" + x + "].RelativeId=" + arrDetails[x].RelativeId);
        //alert("arrDetails[" + x + "].RelativeNm=" + arrDetails[x].RelativeNm);
        str += "<option value='" + arrDetails[x].RelativeId + "'>" + arrDetails[x].RelativeNm + "</option>";
    }
    str += "</select>";
    str += "</td>";
    str += "<td id='tdPanTD' style='padding-left:5px;padding-right:5px;padding-top:10px;'>";
    str += "<input id='txtTDPan' disabled type='text' class='form-control' />";
    str += "</td>";
    str += "<td id='tdDematTD' style='padding-left:5px;padding-right:5px;padding-top:10px;'>";
    str += "<select id='ddlDematTD' class='form-control' onchange='javascript:fnDematTD_onChange(this);'>";
    str += "<option value='-1'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</option>";
    str += "</select>";
    str += "</td>";

    str += "<td id='tdHoldingTD' style='padding-left:5px;padding-right:5px;padding-top:10px;text-align:right;'>";
    str += "</td>";

    str += "<td id='tdPledgedTD' style='padding-left:5px;padding-right:5px;padding-top:10px;text-align:right;'>";
    str += "</td>";

    str += "<td style='padding-left:5px;padding-right:5px;padding-top:10px;'>";
    str += "<input id='txtTDDate' type='text' class='form-control' />";
    str += "</td>";
    str += "<td style='padding-left:5px;padding-right:5px;padding-top:10px;'>";
    str += "<select id='ddlTypeForTD' class='form-control'>";
    str += "<option value='-1'>Please Select</option>";
    for (var x = 0; x < arrTransType.length; x++) {
        str += "<option value='" + arrTransType[x].Id + "'>" + arrTransType[x].Name + "</option>";
    }
    str += "</select>";
    str += "</td>";
    str += "<td style='padding-left:5px;padding-right:5px;padding-top:10px;'>";
    str += "<input id='txtTDQty' type='number' class='form-control' />";
    str += "</td>";
    str += "<td style='padding-left:5px;padding-right:5px;padding-top:10px;'>";
    str += "<input id='txtTDValue' type='text' class='form-control' />";
    str += "</td>";

    str += "<td style='padding-left:5px;padding-right:5px;padding-top:10px;'>";
    str += "<select id='ddlProposedTransactionThrough' class='form-control'>";
    str += "<option value=''>--Select--</option>";
    str += "<option value='Stock Exchange'>Stock Exchange</option>";
    str += "<option value='Off-Market Deal'>Off-Market Deal</option>";
    str += "</select>";
    str += "</td>";

    str += "<td style='padding-left:5px;padding-right:5px;padding-top:10px;'>";
    str += "<select id='ddlExchangeTradedOn' class='form-control'>";
    str += "<option value=''>--Select--</option>";
    str += "<option value='BSE'>BSE</option>";
    str += "<option value='NSE'>NSE</option>";
    str += "</select>";
    str += "</td>";

    //str += "<td style='padding-left:5px;padding-right:5px;padding-top:10px;'>";
    //str += "<input id='txtTDAmount' type='text' class='form-control' />";
    //str += "</td>";
    str += "<td style='padding-left:5px;padding-right:5px;width:5%;padding-top:10px;'>";
    str += "<a onclick='javascript:fnAddNewRow();'>";
    str += "<i class='fa fa-plus'></i>";
    //str += "</a>&nbsp;&nbsp;";
    //str += "<a onclick='javascript:fnRemoveRow(this);'>";
    //str += "<i class='fa fa-minus'></i>";
    //str += "</a>";
    str += "</td>";
    str += "</tr>";
    $("#tbdTrade").append(str);

    //fnSetDate();
}
function fnAddNewRow() {
    var str = "";
    str += "<tr>";
    str += "<td style='padding-left:5px;padding-right:5px;padding-top:10px;'>";
    str += "<select id='ddlForTD' class='form-control' onchange='javascript:fnForTD_onChange(this);'>";
    str += "<option value='-1'>Please Select</option>";

    for (var x = 0; x < arrDetails.length; x++) {
        str += "<option value='" + arrDetails[x].RelativeId + "'>" + arrDetails[x].RelativeNm + "</option>";
    }
    str += "</select>";
    str += "</td>";
    str += "<td id='tdPanTD' style='padding-left:5px;padding-right:5px;padding-top:10px;'>";
    str += "<input id='txtTDPan' disabled type='text' class='form-control' />";
    str += "</td>";
    str += "<td id='tdDematTD' style='padding-left:5px;padding-right:5px;padding-top:10px;'>";
    str += "<select id='ddlDematTD' class='form-control' onchange='javascript:fnDematTD_onChange(this);'>";
    str += "<option value='-1'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</option>";
    str += "</select>";
    str += "</td>";
    str += "<td id='tdHoldingTD' style='padding-left:5px;padding-right:5px;padding-top:10px;text-align:right;'>";
    str += "</td>";

    str += "<td id='tdPledgedTD' style='padding-left:5px;padding-right:5px;padding-top:10px;text-align:right;'>";
    str += "</td>";
    str += "<td style='padding-left:5px;padding-right:5px;padding-top:10px;'>";
    str += "<input id='txtTDDate' type='text' class='form-control' />";
    str += "</td>";
    str += "<td style='padding-left:5px;padding-right:5px;padding-top:10px;'>";
    str += "<select id='ddlTypeForTD' class='form-control'>";
    str += "<option value='-1'>Please Select</option>";
    for (var x = 0; x < arrTransType.length; x++) {
        str += "<option value='" + arrTransType[x].Id + "'>" + arrTransType[x].Name + "</option>";
    }
    str += "</select>";
    str += "</td>";
    str += "<td style='padding-left:5px;padding-right:5px;padding-top:10px;'>";
    str += "<input id='txtTDQty' type='number' class='form-control' />";
    str += "</td>";
    str += "<td style='padding-left:5px;padding-right:5px;padding-top:10px;'>";
    str += "<input id='txtTDValue' type='text' class='form-control' />";
    str += "</td>";

    str += "<td style='padding-left:5px;padding-right:5px;padding-top:10px;'>";
    str += "<select id='ddlProposedTransactionThrough' class='form-control'>";
    str += "<option value=''>--Select--</option>";
    str += "<option value='Stock Exchange'>Stock Exchange</option>";
    str += "<option value='Off-Market Deal'>Off-Market Deal</option>";
    str += "</select>";
    str += "</td>";

    str += "<td style='padding-left:5px;padding-right:5px;padding-top:10px;'>";
    str += "<select id='ddlExchangeTradedOn' class='form-control'>";
    str += "<option value=''>--Select--</option>";
    str += "<option value='BSE'>BSE</option>";
    str += "<option value='NSE'>NSE</option>";
    str += "</select>";
    str += "</td>";
    //str += "<td style='padding-left:5px;padding-right:5px;padding-top:10px;'>";
    //str += "<input id='txtTDAmount' type='text' class='form-control' />";
    //str += "</td>";
    str += "<td style='padding-left:5px;padding-right:5px;width:5%;padding-top:10px;'>";
    str += "<a onclick='javascript:fnAddNewRow();'>";
    str += "<i class='fa fa-plus'></i>";
    str += "</a>&nbsp;&nbsp;";
    str += "<a onclick='javascript:fnRemoveRow(this);'>";
    str += "<i class='fa fa-minus'></i>";
    str += "</a>";
    str += "</td>";
    str += "</tr>";
    $("#tbdTrade").append(str);

    //fnSetDate();
}
function fnRemoveRow(cntrl) {
    var obj = $(cntrl).closest('tr');
    $(obj).remove();
    var flag = false;
    for (var i = 0; i < $("#tbdTrade").children().length; i++) {
        var sRelativeId = $($($($("#tbdTrade").children()[i]).children()[0]).children()[0]).val();
        if (sRelativeId != "-1") {
            flag = true;
        }
    }
    if (flag == false) {
        $("#btnSubmitTD").text("Skip & Continue to Pre-Clearance Request");
    }
}
function fnForTD_onChange(cntrl) {
    var obj = $(cntrl).closest('tr');
    var relative = $(cntrl).val();
    var strOption = "";
    var iCntr = 0;
    //alert(relative)

    $($($(obj).children()[3])).html('');
    $($($(obj).children()[4])).html('');

    var flg = true;
    for (var i = 0; i < $("#tbdTrade").children().length; i++) {
        var bNote = new Object();
        var sRelativeId = $($($($("#tbdTrade").children()[i]).children()[0]).children()[0]).val();

        if (!(sRelativeId == undefined || sRelativeId == "-1" || sRelativeId == null)) {
            flg = false;
        }
    }
    if (!flg) {
        $("#btnSubmitTD").text("Submit/Update holdings");
    }
    else {
        $("#btnSubmitTD").text("Skip & Continue to Pre-Clearance Request");
    }
    for (var x = 0; x < arrDetails.length; x++) {
        if (arrDetails[x].RelativeId == relative) {
            //alert("In here");
            $($($(obj).children()[1]).children()[0]).val(arrDetails[x].Pan);
            //alert(arrDetails[x].LastTransactionDt);
            var cntrlDt = $($($(obj).children()[5]).children()[0]);

            if ($(cntrlDt).data('datepicker') != undefined) {
                $(cntrlDt).data('datepicker').remove();
            }
            $(cntrlDt).val('');
            $(cntrlDt).datepicker({
                todayHighlight: true,
                autoclose: true,
                format: $("input[id*=hdnJSDateFormat]").val(),
                clearBtn: true,
                startDate: FormatDate(arrDetails[x].LastTransactionDt, $("input[id*=hdnDateFormat]").val()),
                endDate: "today",
                daysOfWeekDisabled: [0, 6]
            }).attr('readonly', 'readonly');
            for (var y = 0; y < arrDetails[x].Demat.length; y++) {
                strOption += "<option value='" + arrDetails[x].Demat[y].accountNo + "'>" + arrDetails[x].Demat[y].accountNo + "</option>";
                iCntr++;
            }
        }
    }
    if (iCntr == 1) {
        $($($(obj).children()[2]).children()[0]).html(strOption);
        fnDematTD_onChange($($($(obj).children()[2]).children()[0]));
        //$("#ddlDematTD" + id).html(strOption);
    }
    else {
        var str = "<option value=''>Please Select</option>" + strOption;
        $($($(obj).children()[2]).children()[0]).html(str);
        //$("#ddlDematTD" + id).html(str);
    }
}
function fnDematTD_onChange(cntrl) {
    var obj = $(cntrl).closest('tr');
    //var relative = $(cntrl).val();
    var demat = $(cntrl).val();
    //alert("demat=" + demat);
    var flg = false;;

    for (var x = 0; x < arrDetails.length; x++) {
        for (var y = 0; y < arrDetails[x].Demat.length; y++) {
            if (arrDetails[x].Demat[y].accountNo == demat) {
                $($($(obj).children()[3])).html(arrDetails[x].Demat[y].CurrentHolding);
                $($($(obj).children()[4])).html(arrDetails[x].Demat[y].Pledged);
                flg = true;
                break;
            }
        }
    }
    if (!flg) {
        $($($(obj).children()[3])).html("0");
        $($($(obj).children()[4])).html("0");
    }
}
function fnSetDate() {
    //alert("In function fnSetDate()");
    var BPDate = $("input[id*='HiddenLastBenposDate']").val();
    //alert(BPDate);
    $("[id='txtTDDate']").each(function () {
        $(this).datepicker({
            todayHighlight: true,
            autoclose: true,
            format: $("input[id*=hdnJSDateFormat]").val(),
            clearBtn: true,
            startDate: BPDate,
            endDate: "today",
            daysOfWeekDisabled: [0, 6]
        }).attr('readonly', 'readonly');
    })
    //$("#txtTDDate").datepicker({
    //    todayHighlight: true,
    //    autoclose: true,
    //    format: "dd/mm/yyyy",
    //    clearBtn: true,
    //    endDate: "today",
    //    daysOfWeekDisabled: [0, 6]
    //}).attr('readonly', 'readonly');
}
function fnValidateBrokerNoteList() {
    var flg = true;
    for (var i = 0; i < $("#tbdTrade").children().length; i++) {
        var bNote = new Object();
        var sRelativeId = $($($($("#tbdTrade").children()[i]).children()[0]).children()[0]).val();
        var sRelativeNm = $($($($("#tbdTrade").children()[i]).children()[0]).children().find("option:selected")).text();
        var sDemat = $($($($("#tbdTrade").children()[i]).children()[2]).children()[0]).val();
        var sHolding = $($($("#tbdTrade").children()[i]).children()[3]).html();
        var sPledged = $($($("#tbdTrade").children()[i]).children()[4]).html();
        var sDate = $($($($("#tbdTrade").children()[i]).children()[5]).children()[0]).val();
        var sTransactionTypeId = $($($($("#tbdTrade").children()[i]).children()[6]).children(0)).val();
        var sType = $($($($("#tbdTrade").children()[i]).children()[6]).children().find("option:selected")).text();
        var sQty = $($($($("#tbdTrade").children()[i]).children()[7]).children()[0]).val();
        var sRate = $($($($("#tbdTrade").children()[i]).children()[8]).children()[0]).val();
        var sTransactionThrough = $($($($("#tbdTrade").children()[i]).children()[9]).children()[0]).val();
        var sExchange = $($($($("#tbdTrade").children()[i]).children()[10]).children()[0]).val();

        /*alert("sHolding=" + sHolding);
        alert("sPledged=" + sPledged);
        alert("sRelativeId=" + sRelativeId);
        alert("sRelativeNm=" + sRelativeNm);
        alert("sDemat=" + sDemat);
        alert("sDate=" + sDate);
        alert("sType=" + sType);
        alert("sQty=" + sQty);
        alert("sRate=" + sRate);
        alert("sTransactionThrough=" + sTransactionThrough);
        alert("sExchange=" + sExchange);*/

        if (!(sRelativeId == undefined || sRelativeId == "-1" || sRelativeId == null)) {
            if (sDemat == undefined || sDemat == "" || sDemat == null) {
                flg = false;
                alert("Please select demat for " + sRelativeNm);
                break;
            }
            if (sDate == undefined || sDate == "" || sDate == null) {
                flg = false;
                alert("Please enter transaction date for " + sRelativeNm);
                break;
            }
            if (sQty == undefined || sQty == "" || sQty == null) {
                flg = false;
                alert("Please enter traded quantity for " + sRelativeNm);
                break;
            }
            if (sRate == undefined || sRate == "" || sRate == null) {
                flg = false;
                alert("Please enter traded amount for " + sRelativeNm);
                break;
            }
            if (sTransactionThrough == undefined || sTransactionThrough == "" || sTransactionThrough == null) {
                flg = false;
                alert("Please select transaction through for " + sRelativeNm);
                break;
            }
            if (sExchange == undefined || sExchange == "" || sExchange == null) {
                flg = false;
                alert("Please select exchange for " + sRelativeNm);
                break;
            }
            for (var x = 0; x < arrTransType.length; x++) {
                if (arrTransType[x].Id == sTransactionTypeId) {
                    if (arrTransType[x].Nature == "-") {
                        if (sType == "Invocation of Pledge") {
                            if (Number(sQty) > Number(sPledged)) {
                                flg = false;
                                alert("Pleadged holding is not enough to carry out the " + sType + " transaction");
                                break;
                            }
                        }
                        else {
                            if (Number(sQty) > Number(sHolding)) {
                                flg = false;
                                alert("Current holding is not enough to carry out the " + sType + " transaction");
                                break;
                            }
                        }
                    }
                    else {
                        if (sType == "Release of Pledge") {
                            if (Number(sQty) > Number(sPledged)) {
                                flg = false;
                                alert("Pleadged holding is not enough to carry out the " + sType + " transaction");
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
    //alert("flg=" + flg);
    if (flg == true) {
        return true;
    }
    else {
        return false;
    }
}
function fnSaveBrokerNote() {
    if ($("#tbdTrade").children().length > 0) {
        if (fnValidateBrokerNoteList()) {
            var BNList = new Array();
            for (var i = 0; i < $("#tbdTrade").children().length; i++) {
                var bNote = new Object();
                var sRelativeId = $($($($("#tbdTrade").children()[i]).children()[0]).children()[0]).val();
                var sDemat = $($($($("#tbdTrade").children()[i]).children()[2]).children()[0]).val();
                var sDate = $($($($("#tbdTrade").children()[i]).children()[5]).children()[0]).val();
                var sType = $($($($("#tbdTrade").children()[i]).children()[6]).children()[0]).val();
                var sQty = $($($($("#tbdTrade").children()[i]).children()[7]).children()[0]).val();
                var sRate = $($($($("#tbdTrade").children()[i]).children()[8]).children()[0]).val();
                var sTransactionThrough = $($($($("#tbdTrade").children()[i]).children()[9]).children()[0]).val();
                var sExchange = $($($($("#tbdTrade").children()[i]).children()[10]).children()[0]).val();
                var flg = true;

                if (sRelativeId == undefined || sRelativeId == "-1" || sRelativeId == null) {
                    flg = false;
                }
                if (sDemat == undefined || sDemat == "" || sDemat == null) {
                    flg = false;
                }
                if (sDate == undefined || sDate == "" || sDate == null) {
                    flg = false;
                }
                if (sQty == undefined || sQty == "" || sQty == null) {
                    flg = false;
                }
                if (sRate == undefined || sRate == "" || sRate == null) {
                    flg = false;
                }
                if (sTransactionThrough == undefined || sTransactionThrough == "" || sTransactionThrough == null) {
                    flg = false;
                }
                if (sExchange == undefined || sExchange == "" || sExchange == null) {
                    flg = false;
                }
                if (flg == true) {
                    bNote.PreClearanceRequestId = 0;
                    bNote.PreClearanceRequestedFor = sRelativeId;
                    bNote.relativeId = sRelativeId;
                    bNote.DematAccount = sDemat;
                    bNote.ActualTransactionDate = sDate;
                    bNote.ActualTradeQuantity = sQty;
                    bNote.ValuePerShare = sRate / sQty;
                    bNote.TotalAmount = sRate;
                    bNote.TransactionTypeName = sType;
                    bNote.TransactionType = sType;
                    bNote.TradeExchangeName = sExchange;
                    bNote.exchangeTradedOn = sExchange;
                    bNote.proposedTransactionThrough = sTransactionThrough;
                    BNList.push(bNote);
                }
            }
            if (BNList.length > 0) {
                $("#Loader").show();
                var webUrl = uri + "/api/PreClearance/SaveBrokerNoteList";
                $.ajax({
                    url: webUrl,
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    datatype: "json",
                    async: true,
                    data: JSON.stringify(BNList),
                    success: function (msg) {
                        $("#Loader").hide();
                        if (isJson(msg)) {
                            msg = JSON.parse(msg);
                        }
                        if (msg.StatusFl == true) {
                            fnGetDetailsOfUser();
                            alert("Trade details capture successfully !");
                            $("#mdlTradeDetails").modal('hide');
                            $("#stack1").modal('show');
                        }
                        else {
                            if (msg.Msg == "SessionExpired") {
                                alert("Your session is expired. Please login again to continue");
                                window.location.href = "../LogOut.aspx";
                            }
                            else {
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
            else {
                $("#mdlTradeDetails").modal('hide');
                $("#stack1").modal('show');
            }
        }
        else {
            if ($("#btnSubmitTD").text() == "Skip") {
                $("#mdlTradeDetails").modal('hide');
                $("#stack1").modal('show');
            }
        }
    }
    else {
        $("#mdlTradeDetails").modal('hide');
        $("#stack1").modal('show');
    }
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
                arrDematAccount = new Array();

                if (msg.DematDetailList.length > 1) {
                    result += "<option value='0'>--Select--</option>";
                }

                for (var i = 0; i < msg.DematDetailList.length; i++) {
                    var obj = new Object();
                    obj.accountNo = msg.DematDetailList[i].accountNo;
                    obj.CurrentHolding = msg.DematDetailList[i].CurrentHolding;
                    arrDematAccount.push(obj);
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
                    $("#ddlDematAccount").html('');
                    $("#spncurrentholding").html('0');
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
function fnCalculateMultiTrade() {
    $("#TableMultiTrade").on('keyup focusout mousedown', '.CalcMultiTrade', function (event) {

        var $cntrl = $(this).closest('tr');
        var cntrlqty = $($($cntrl).closest('tr').children()[1]).find("input[id*=txtMultiTradeQty]");
        var cntrlsharevalue = $($($cntrl).closest('tr').children()[2]).find("input[id*=txtMultiTradeValue]");
        var cntrlTotal = $($($cntrl).closest('tr').children()[3]).find("input[id*=txtMultiTradeAmount]");
        var cntrldate = $($($cntrl).closest('tr').children()[0]).find("input[id*=txtMultiTradeDate]");

        var qty = $(cntrlqty).val() || 0;
        var sharevalue = $(cntrlsharevalue).val() || 0;

        if (qty.length > 0 && sharevalue.length > 0) {
            var TotalAmount = parseFloat(qty) * parseFloat(sharevalue);
            $(cntrlTotal).val(TotalAmount);
            $("#btnnulltrade").attr('disabled', true);

        }
        else {

            $("#btnnulltrade").attr('disabled', false);
        }
    }).trigger('change');
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
            //if (msg.StatusFl == true) {
            //    var result = "";
            //    result += "<option value='0'>--Select--</option>";
            //    for (var i = 0; i < msg.RestrictedCompaniesList.length; i++) {
            //        result += "<option value='" + msg.RestrictedCompaniesList[i].ID + "'>" + msg.RestrictedCompaniesList[i].companyName + "</option>";
            //    }
            //    $("#ddlRestrictedCompanies").append(result);
            //    $("#ddlRestrictedCompaniesBN").append(result);
            //}
            if (msg.StatusFl == true) {
                var resultHomeCompany = "";
                var resultOtherCompany = "";
                var result = "";
                result += "<option value='0'>--Select--</option>";
                for (var i = 0; i < msg.RestrictedCompaniesList.length; i++) {
                    if (msg.RestrictedCompaniesList[i].IsHomeCompany == 0) {
                        resultOtherCompany += "<option value='" + msg.RestrictedCompaniesList[i].ID + "'>" + msg.RestrictedCompaniesList[i].companyName + "</option>";
                    }
                    else if (msg.RestrictedCompaniesList[i].IsHomeCompany == 1) {
                        resultHomeCompany += "<option value='" + msg.RestrictedCompaniesList[i].ID + "'>" + msg.RestrictedCompaniesList[i].companyName + "</option>";
                    }

                }
                //alert(resultOtherCompany); alert(resultHomeCompany); alert(result);
                $("#ddlRestrictedCompanies").append(resultHomeCompany);
                $("#ddlRestrictedCompaniesBN").append(resultHomeCompany);
                $("#ddlRestrictedCompaniesOther").append(resultOtherCompany);
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
function fnGetDematCurrentHoldings(caccountno) {
    var result = arrDetails;
    var relativeId = $("#ddlFor").val();
    //alert("In function fnGetDematCurrentHoldings");
    //alert("relativeId=" + relativeId);
    var flag = true;
    if (result != null) {
        result.forEach(function (e) {
            if (relativeId == e.RelativeId) {
                var demats = e.Demat;
                demats.forEach(function (f) {
                    if (caccountno == f.accountNo) {
                        $("input[id*='txtcurrentholding']").val(f.CurrentHolding);
                        $("#spncurrentholding").html(f.CurrentHolding);

                        $("input[id*='txtLockedHolding']").val(f.Pledged);
                        $("#spnLockedHolding").html(f.Pledged);
                        flag = false;
                    }
                })
            }
        })
    }
    if (flag) {
        $("input[id*='txtcurrentholding']").val("0");
        $("#spncurrentholding").html("");

        $("input[id*='txtLockedHolding']").val("0");
        $("#spnLockedHolding").html("");
    }
}
function fnValidate() {
    if ($('#ddlFor').val() == undefined || $('#ddlFor').val() == null || $('#ddlFor').val().trim() == '' || $('#ddlFor').val().trim() == 'Select') {
        alert("Please fill all mandatory fields");
        return false;
    }
    if ($('#ddlTypeOfSecurity').val() == undefined || $('#ddlTypeOfSecurity').val() == null || $('#ddlTypeOfSecurity').val().trim() == '' || $('#ddlTypeOfSecurity').val().trim() == '0') {
        alert("Please fill all mandatory fields");
        return false;
    }

    if ($("#ddlFor").val() != "0" && $("select[id*=ddlTypeOfSecurity] option:selected").text() == "ESOP") {
        alert("Cannot request ESOP to your relatives.");
        return false;
    }

    if ($('#ddlRestrictedCompanies').val() == undefined || $('#ddlRestrictedCompanies').val() == null || $('#ddlRestrictedCompanies').val().trim() == '' || $('#ddlRestrictedCompanies').val().trim() == '0') {
        alert("Please fill all mandatory fields");
        return false;
    }
    if ($('#txtTradeQuantity').val() == undefined || $('#txtTradeQuantity').val() == null || $('#txtTradeQuantity').val().trim() == '') {
        alert("Please fill all mandatory fields");
        return false;
    }
    //else if (parseInt($('#txtTradeQuantity').val()) > 100000) {
    //    alert("You cannot trade in more than 1,00,000 (One Lac) shares at a time.");
    //    return false;
    //}
    if ($("#ddlTypeOfTransaction").val() == undefined || $("#ddlTypeOfTransaction").val() == null || $("#ddlTypeOfTransaction").val() == "0") {
        alert("Please fill all mandatory fields");
        return false;
    }
    //if ($('#ddlTradeExchange').val() == undefined || $('#ddlTradeExchange').val() == null || $('#ddlTradeExchange').val().trim() == '' || $('#ddlTradeExchange').val().trim() == '0') {
    //    return false;
    //}
    if ($('#ddlDematAccount').val() == undefined || $('#ddlDematAccount').val() == null || $('#ddlDematAccount').val().trim() == '' || $('#ddlDematAccount').val().trim() == '0') {
        alert("Please fill all mandatory fields");
        return false;
    }
    if ($('#txtRequestedTransactionDate').val() == undefined || $('#txtRequestedTransactionDate').val() == null || $('#txtRequestedTransactionDate').val().trim() == '') {
        alert("Please fill all mandatory fields");
        return false;
    }
    if ($('#txtShareCurrentMarketPrice').val() == undefined || $('#txtShareCurrentMarketPrice').val() == null || $('#txtShareCurrentMarketPrice').val().trim() == '') {
        alert("Please fill all mandatory fields");
        return false;
    }

    var TransType = $("#ddlTypeOfTransaction").val();
    var TradeQty = $("#txtTradeQuantity").val();

    var FreeHolding = $("#txtcurrentholding").val();
    var LockedHolding = $("#txtLockedHolding").val();

    var flg = true;
    //alert("TransType=" + TransType);
    //alert("TradeQty=" + TradeQty);
    var result = arrTransType;
    result.forEach(function (e) {
        if (TransType == e.Id) {
            //alert("Name=" + e.Name);
            //alert("Nature=" + e.Nature);
            if (e.Nature == "-") {
                if (e.Name == "Creation of Pledge") {
                    if (Number(TradeQty) > Number(FreeHolding)) {
                        alert("Free holding is not enough to carry out the " + e.Name + " transaction");
                        flg = false;
                    }
                }
                else if (e.Name == "Gift Transfer") {
                    if (Number(TradeQty) > Number(FreeHolding)) {
                        alert("Free holding is not enough to carry out the " + e.Name + " transaction");
                        flg = false;
                    }
                }
                else if (e.Name == "Invocation of Pledge") {
                    if (Number(TradeQty) > Number(LockedHolding)) {
                        alert("Locked holding is not enough to carry out the " + e.Name + " transaction");
                        flg = false;
                    }
                }
                else if (e.Name == "Sell") {
                    if (Number(TradeQty) > Number(FreeHolding)) {
                        alert("Free holding is not enough to carry out the " + e.Name + " transaction");
                        flg = false;
                    }
                }
            }
            else {
                if (e.Name == "Release of Pledge") {
                    if (Number(TradeQty) > Number(LockedHolding)) {
                        alert("Locked holding is not enough to carry out the " + e.Name + " transaction");
                        flg = false;
                    }
                }
            }
        }
    });
    return flg;
}
function fnSubmitPreClearanceRequest(status) {
    if (fnValidate()) {
        if (status == "Cancel") {
            $("#modalWithdrawRequest").modal('show');
        }
        else if (status == "InApproval") {
            if ($("input[id*=enableUndertakingBeforeRequest]").val() == "true") {
                fnGetUndertakingTemplateBeforeSubmitPreclearanceRequest();
            }
            else {
                $("#modalSubmitRequest").modal('show');
            }
        }
        else if (status == "Draft") {
            $("#modalSaveAsDraftRequest").modal('show');
        }
        else {
            fnSubmitRequest(status);
        }
    }
}
function fnGetUndertakingTemplateBeforeSubmitPreclearanceRequest() {
    $("#Loader").show();
    var tradeQuantity = $('#txtTradeQuantity').val();
    var webUrl = uri + "/api/PreClearanceRequest/GetUndertakingTemplate";
    $.ajax({
        url: webUrl,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        // async: false,
        data: JSON.stringify({
            TradeQuantity: tradeQuantity
        }),
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                if (msg.PreClearanceRequest !== null) {
                    $("#UndertakingPreclearanceRequest").html(msg.PreClearanceRequest.underTakingText);
                    $("#modalUnterkaing").modal('show');
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
function fnRequestAction(status) {
    fnSubmitRequest(status);
}
function fnSubmitRequest(status) {
    $("#Loader").show();
    var preClearanceRequestId = $("#preClearanceRequestId").val() == '' ? 0 : $("#preClearanceRequestId").val();
    var preClearanceRequestFor = $('#ddlFor').val();
    var preClearanceRequestForName = $('#ddlFor :selected').text();
    var securityType = $('#ddlTypeOfSecurity').val();
    var securityTypeName = $('#ddlTypeOfSecurity :selected').text();
    var transactionType = $('#ddlTypeOfTransaction').val();
    var transactionTypeName = $('#ddlTypeOfTransaction :selected').text();
    var restrictedCompanyId = $('#ddlRestrictedCompanies').val();
    var restrictedCompanyName = $('#ddlRestrictedCompanies :selected').text();
    var tradeQuantity = $('#txtTradeQuantity').val();
    var tradeExchange = $('#ddlTradeExchange').val() == null ? 0 : $('#ddlTradeExchange').val();
    var tradeExchangeName = $('#ddlTradeExchange :selected').text();
    var dematAccountId = $('#ddlDematAccount').val();
    var requestedTransactionDate = $('#txtRequestedTransactionDate').val();
    var status = status;
    var shareCurrentMarketPrice = $('#txtShareCurrentMarketPrice').val();
    var proposedTransactionThrough = $('#ddlProposedTransactionThrough').val();

    var webUrl = uri + "/api/PreClearanceRequest/SavePreClearanceRequest";
    $.ajax({
        url: webUrl,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        // async: false,
        data: JSON.stringify({
            TradeExchangeName: tradeExchangeName, TradeCompanyName: restrictedCompanyName,
            TransactionTypeName: transactionTypeName, SecurityTypeName: securityTypeName,
            PreClearanceRequestedForName: preClearanceRequestForName,
            PreClearanceRequestId: preClearanceRequestId, TradeCompany: restrictedCompanyId,
            TradeDate: requestedTransactionDate, PreClearanceRequestedFor: preClearanceRequestFor,
            SecurityType: securityType, TransactionType: transactionType, TradeQuantity: tradeQuantity, TradeExchange: tradeExchange, DematAccount: dematAccountId, Status: status,
            shareCurrentMarketPrice: shareCurrentMarketPrice,
            proposedTransactionThrough: proposedTransactionThrough
        }),
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                $('#stack1').modal('hide');
                window.location.reload(true);
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
function fnCloseModal() {
    fnClearForm();
}
function fnClearForm() {
    $("#preClearanceRequestId").val('');
    $("#ddlFor")[0].selectedIndex = 0;
    $("#ddlTypeOfSecurity")[0].selectedIndex = 0;
    //$("#ddlRestrictedCompanies").val('');
    $("#txtTradeQuantity").val('');
    $("#ddlTypeOfTransaction")[0].selectedIndex = 0;
    $("#ddlTradeExchange")[0].selectedIndex = 0;
    $("#ddlDematAccount")[0].selectedIndex = 0;
    //$("#txtRequestedTransactionDate").val('');
    $("#txtShareCurrentMarketPrice").val('');
    $("#ddlProposedTransactionThrough")[0].selectedIndex = 0;


    $("#ddlFor").prop("disabled", false);
    $("#ddlTypeOfSecurity").prop("disabled", false);
    $("#ddlRestrictedCompanies").prop("disabled", false);
    $("#txtTradeQuantity").prop("readonly", false);
    $("#ddlTypeOfTransaction").prop("disabled", false);
    $("#ddlTradeExchange").prop("disabled", false);
    $("#ddlDematAccount").prop("disabled", false);
    $("#txtRequestedTransactionDate").prop("disabled", false);
    $("#txtShareCurrentMarketPrice").prop("readonly", false);
    $("#ddlProposedTransactionThrough").prop("disabled", false);
    $("textarea[id='txtNullTradeRemarks']").val('');
    $("#ddlRestrictedCompanies")[0].selectedIndex = 0;
    //$("#tbdMultiTrade").html("");

}
function fnEditBrokerNote(
    PreClearanceRequestId, BrokerNote, ActualTransactionDate, ValuePerShare, TotalAmount, TradeQuantity, ActualTradeQuantity, Status,
    TradeExchange, DematAccount, PreClearanceRequestedFor, TradeCompany, TransactionType, TradeDate, Remarks, proposedTransactionThrough,
    TradingFrom, TradingTo, RemainingTradeQuantity
) {
    //alert("In function fnEditBrokerNote");
    
    /*alert("PreClearanceRequestId=" + PreClearanceRequestId);
    alert("BrokerNote=" + BrokerNote);
    alert("ActualTransactionDate=" + ActualTransactionDate);
    alert("ValuePerShare=" + ValuePerShare);
    alert("TotalAmount=" + TotalAmount);
    alert("TradeQuantity=" + TradeQuantity);
    alert("ActualTradeQuantity=" + ActualTradeQuantity);
    alert("Status=" + Status);
    alert("TradeExchange=" + TradeExchange);
    alert("DematAccount=" + DematAccount);
    alert("PreClearanceRequestedFor=" + PreClearanceRequestedFor);
    alert("TradeCompany=" + TradeCompany);
    alert("TransactionType=" + TransactionType);
    alert("TradeDate=" + TradeDate);
    alert("Remarks=" + Remarks);
    alert("proposedTransactionThrough=" + proposedTransactionThrough);
    alert("TradingFrom=" + TradingFrom);
    alert("TradingTo=" + TradingTo);
    alert("RemainingTradeQuantity=" + RemainingTradeQuantity);*/
    TradeFrom = TradingFrom;
    TradeTo = TradingTo;

    //alert("TradingFrom=" + TradingFrom);
    //alert("TradingTo=" + TradingTo);

    if (BrokerNote == 'null') {
        BrokerNote = '';
    }
    if (ActualTransactionDate == 'null') {
        ActualTransactionDate = '';
    }
    if (ValuePerShare == 'null') {
        ValuePerShare = '';
    }
    if (TotalAmount == 'null') {
        TotalAmount = '';
    }
    if (ActualTradeQuantity == 'null') {
        ActualTradeQuantity = '';
    }
    if (Remarks == 'null') {
        Remarks = '';
    }
    $("#ddlForBN").val(PreClearanceRequestedFor);
    $("#ddlRestrictedCompaniesBN").val(TradeCompany);
    $("#ddlTypeOfTransactionBN").val(TransactionType);
    $("#txtRequestedTransactionDateBN").val(FormatDate(TradeDate, $("input[id*=hdnDateFormat]").val()));
    $("#preClearanceRequestIdBN").val(PreClearanceRequestId);
    $("#txtBrokerNoteFileName").val(BrokerNote);
    $('#txtActualdateoftransaction').val(FormatDate(ActualTransactionDate, $("input[id*=hdnDateFormat]").val()));
    $('#txtValuePerShare').val(ValuePerShare);
    if (ActualTradeQuantity == null || ActualTradeQuantity == undefined || ActualTradeQuantity == '') {
        $('#txtTotalamount').val(ValuePerShare * TradeQuantity);
    }
    else {
        $('#txtTotalamount').val(ValuePerShare * ActualTradeQuantity);
    }
    $("#txtTradequantityBrokerNote").val(TradeQuantity);
    $("#txtActualTradequantityBrokerNote").val(ActualTradeQuantity);
    $("#txtTradeExchangeBN").val(TradeExchange);
    $("#txtStatusBN").val(Status);
    $("#txtDematAccountBN").val(DematAccount);
    $("#remarks").val(Remarks);
    // fnGetDematAccount(TradeExchange, "fnEditBrokerNote");
    fnGetDematAccount(PreClearanceRequestedFor, "fnEditBrokerNote");
    $("#ddlDematAccountBrokerNote").val(DematAccount);
    if (proposedTransactionThrough == "Off-Market Deal") {
        $("#dvExchangeTradedOn").hide();
        $("#dvBrokerDetails").show();
        BrokerDetailsManadatory = true;
    }
    if (Status == "InApproval") {
        $("#btnSubmitBrokerNote").show();
        $("#btnBrokernote").prop("disabled", false);
        $("#txtValuePerShare").prop("readonly", false);
        $("#txtTotalamount").prop("readonly", true);
        $("#ddlDematAccountBrokerNote").prop("disabled", true);
        $("#txtActualdateoftransaction").prop("disabled", false);
    }
    else if (Status == "Draft") {
        $("#btnSubmitBrokerNote").show();
        $("#btnBrokernote").prop("disabled", false);
        $("#txtValuePerShare").prop("readonly", false);
        $("#txtTotalamount").prop("readonly", true);
        $("#ddlDematAccountBrokerNote").prop("disabled", true);
        $("#txtActualdateoftransaction").prop("disabled", false);
    }
    else if (Status == "Cancel") {
        $("#btnSubmitBrokerNote").show();
        $("#btnBrokernote").prop("disabled", false);
        $("#txtValuePerShare").prop("readonly", false);
        $("#txtTotalamount").prop("readonly", true);
        $("#ddlDematAccountBrokerNote").prop("disabled", true);
        $("#txtActualdateoftransaction").prop("disabled", false);
    }
    else {
        //do nothing
        $("#dvMultiTrade").show();
        $("#txtRemainingTradequantity").val(RemainingTradeQuantity);
        if (parseInt(RemainingTradeQuantity) > 0) {
            $("#modal-title-nulltrade").html('Please Submit Remarks in case of Null Trade for remaining quantity');
            $("#btnnulltrade").text('Click Here to Report Nill Trade for (' + RemainingTradeQuantity + ')');
        }
        else {
            $("#modal-title-nulltrade").html('Please Submit Remarks in case of Null Trade');
            $("#btnnulltrade").text('Click Here to Report Nill Trade');
        }
        fnMultiTradeAddRow();
    }

    $("#basic1").modal('show');
}
function fnMultiTradeAddRow() {
    $("#tbdMultiTrade").html("");
    var str = "";
    str += "<tr>";
    str += "<td style='padding-left:5px;padding-right:5px;'>";
    str += "<input id='txtMultiTradeDate' type='text' class='form-control bg-white' />";
    str += "</td>";
    str += "<td style='padding-left:5px;padding-right:5px;'>";
    str += "<input id='txtMultiTradeQty' type='number' min='0' class='form-control CalcMultiTrade' />";
    str += "</td>";
    str += "<td style='padding-left:5px;padding-right:5px;'>";
    str += "<input id='txtMultiTradeValue' type='number' min='0' class='form-control CalcMultiTrade' />";
    str += "</td>";
    str += "<td style='padding-left:5px;padding-right:5px;'>";
    str += "<input id='txtMultiTradeAmount' type='text' class='form-control' disabled='disabled' />";
    str += "</td>";
    str += "<td style='padding-left:5px;padding-right:5px;padding-top:10px;'>";
    str += "<a onclick='javascript:fnMultiTradeAddNewRow();'>";
    str += "<i class='fa fa-plus'></i>";
    str += "</a>";
    str += "</td>";
    str += "</tr>";
    $("#tbdMultiTrade").append(str);

    fnSetMultiTradeDate();
}
function fnMultiTradeAddNewRow() {
    var str = "";
    str += "<tr>";
    str += "<td style='padding-left:5px;padding-right:5px;'>";
    str += "<input id='txtMultiTradeDate' type='text' class='form-control bg-white' />";
    str += "</td>";
    str += "<td style='padding-left:5px;padding-right:5px;'>";
    str += "<input id='txtMultiTradeQty' type='number' min='0' class='form-control CalcMultiTrade' />";
    str += "</td>";
    str += "<td style='padding-left:5px;padding-right:5px;'>";
    str += "<input id='txtMultiTradeValue' type='number' min='0' class='form-control CalcMultiTrade' />";
    str += "</td>";
    str += "<td style='padding-left:5px;padding-right:5px;'>";
    str += "<input id='txtMultiTradeAmount' type='text' class='form-control'  disabled='disabled' />";
    str += "</td>";
    str += "<td style='padding-left:5px;padding-right:5px;padding-top:10px;'>";
    str += "<a onclick='javascript:fnMultiTradeAddNewRow();'>";
    str += "<i class='fa fa-plus'></i>";
    str += "</a>&nbsp;&nbsp;";
    str += "<a onclick='javascript:fnRemoveMultiTradeRow(this);'>";
    str += "<i class='fa fa-minus'></i>";
    str += "</a>";
    str += "</td>";
    str += "</tr>";

    $("#tbdMultiTrade").append(str);
    fnSetMultiTradeDate();
}
function fnRemoveMultiTradeRow(cntrl) {
    var obj = $(cntrl).closest('tr');
    $(obj).remove();

}
function fnSetMultiTradeDate() {
    //alert("In function fnSetMultiTradeDate");
    //alert("TradeFrom=" + TradeFrom);

    var dt = new Date();
    var yr = dt.getFullYear();
    var mn = Number(dt.getMonth()) + 1;
    var dy = dt.getDate();
    //alert(yr);
    //alert(mn);
    //alert(dy);

    mn = '0' + mn;
    dy = '0' + dy;
    //alert(yr);
    //alert(mn.slice(-2));
    //alert(dy.slice(-2));

    var today = yr + '-' + mn.slice(-2) + '-' + dy.slice(-2);
    //alert("today=" + today);
    //alert("TradeFrom=" + dateFormat(TradeFrom, "yyyymmdd"));
    //alert("TradeTo=" + TradeTo);
    //alert("TradeFrom=" + dateFormat(TradeTo, "yyyymmdd"));
    //alert("now=" + dateFormat(new Date(), "yyyymmdd"));
    //alert("1..=" + dateFormat(new Date(), "yyyy-mm-dd"));

    if (FormatDate(TradeTo, "yyyymmdd") > FormatDate(today, "yyyymmdd")) {
        TradeTo = FormatDate(today, "yyyy-mm-dd");
    }
    //else if (FormatDate(TradeFrom, "yyyymmdd") > FormatDate(today, "yyyymmdd")) {
    //    TradeFrom = FormatDate(today, "yyyy-mm-dd");
    //}
    //alert("Before=" + TradeFrom);
    //alert("Before=" + TradeTo);
    //if (new Date(TradeTo) > new Date()) {
    //    TradeTo = new Date();
    //}
    //else if (new Date(TradeFrom) > new Date()) {
    //    TradeFrom = new Date();
    //}
    //alert(FormatDate(TradeTo));
    $("input[id*=txtMultiTradeDate]").each(function () {
        //alert("Here");
        //alert($("input[id*=hdnDateFormat]").val());
        //alert(TradeFrom);
        //alert(TradeTo);
        $(this).datepicker({
            todayHighlight: true,
            autoclose: true,
            format: $("input[id*=hdnJSDateFormat]").val(),
            clearBtn: true,
            startDate: FormatDate(TradeFrom, $("input[id*=hdnDateFormat]").val()),
            endDate: FormatDate(TradeTo, $("input[id*=hdnDateFormat]").val()),
            daysOfWeekDisabled: [0, 6]
        }).attr('readonly', 'readonly');
    })
}
function fnCalculateMultiTrade() {
    $("#TableMultiTrade").on('keyup focusout mousedown', '.CalcMultiTrade', function (event) {

        var $cntrl = $(this).closest('tr');
        var cntrlqty = $($($cntrl).closest('tr').children()[1]).find("input[id*=txtMultiTradeQty]");
        var cntrlsharevalue = $($($cntrl).closest('tr').children()[2]).find("input[id*=txtMultiTradeValue]");
        var cntrlTotal = $($($cntrl).closest('tr').children()[3]).find("input[id*=txtMultiTradeAmount]");
        var cntrldate = $($($cntrl).closest('tr').children()[0]).find("input[id*=txtMultiTradeDate]");

        var qty = $(cntrlqty).val() || 0;
        var sharevalue = $(cntrlsharevalue).val() || 0;

        if (qty.length > 0 && sharevalue.length > 0) {
            var TotalAmount = parseFloat(qty) * parseFloat(sharevalue);
            $(cntrlTotal).val(TotalAmount);
            $("#btnnulltrade").attr('disabled', true);

        }
        else {

            $("#btnnulltrade").attr('disabled', false);
        }
    }).trigger('change');
}
function fnValidateMultiTradeList() {
    var validate = true;
    var TotalQty = 0;
    var TotalAmount = 0;
    var MaxDate = "";
    for (var i = 0; i < $("#tbdMultiTrade").children().length; i++) {
        var mtDate = $($($($("#tbdMultiTrade").children()[i]).children()[0]).children()[0]).val();
        var mtQty = $($($($("#tbdMultiTrade").children()[i]).children()[1]).children()[0]).val();
        var mtRate = $($($($("#tbdMultiTrade").children()[i]).children()[2]).children()[0]).val();
        var mtAmount = $($($($("#tbdMultiTrade").children()[i]).children()[3]).children()[0]).val();

        if (mtDate == undefined || mtDate == "" || mtDate == null) {
            validate = false;
        }
        //else {
        //    if (MaxDate == "") {
        //        MaxDate = mtDate;
        //    }
        //    else {
        //        if (convertToDateTime(mtDate) > convertToDateTime(MaxDate)) {
        //            MaxDate = mtDate;
        //        }
        //    }
        //}

        if (mtQty == undefined || mtQty == "" || mtQty == null) {
            validate = false;
        }

        else {
            TotalQty += parseInt(mtQty);
        }
        if (mtRate == undefined || mtRate == "" || mtRate == null) {
            validate = false;
        }
        if (mtAmount == undefined || mtAmount == "" || mtAmount == null) {
            validate = false;
        }
        else {
            TotalAmount += parseFloat(mtAmount);
        }
    }

    $('#txtActualdateoftransaction').val(MaxDate);
    $('#txtActualTradequantityBrokerNote').val(TotalQty);
    $('#txtValuePerShare').val(TotalAmount / TotalQty);
    $('#txtTotalamount').val(TotalAmount);

    if (TotalQty > parseInt($('#txtTradequantityBrokerNote').val())) {
        alert("Total trade disclosure quantity should be less or equal to the approved quantity");
        return false;
    }
    else if (TotalQty > parseInt($('#txtRemainingTradequantity').val())) {
        alert("Total trade disclosure quantity should be less or equal to the approved quantity");
        return false;
    }
    if (validate) {

        return true;
    }
    else {
        alert("Please fill all mandatory fields");
        return false;
    }
}
function AddUpdateBrokerNote() {
    if (fnValidateBrokerNote() && fnValidateMultiTradeList()) {
        if ($("#txtTotalamount").val() == '0') {
            $("#modalZeroTradeValue").modal('show');
        }
        else {
            $("#modalBrokerNoteUploadConfirmation").modal('show');
        }
    }
    else {

    }
}
function fnValidateBrokerNote() {
    if ($("#ddlForBN").val() == undefined || $("#ddlForBN").val() == null || $("#ddlForBN").val() == "Select") {
        alert("Please fill all mandatory fields");
        return false;
    }
    if ($("#ddlRestrictedCompaniesBN").val() == undefined || $("#ddlRestrictedCompaniesBN").val() == null || $("#ddlRestrictedCompaniesBN").val() == "0") {
        alert("Please fill all mandatory fields");
        return false;
    }
    if ($("#ddlTypeOfTransactionBN").val() == undefined || $("#ddlTypeOfTransactionBN").val() == null || $("#ddlTypeOfTransactionBN").val() == "0") {
        alert("Please fill all mandatory fields");
        return false;
    }
    if ($('#txtRequestedTransactionDateBN').val() == undefined || $('#txtRequestedTransactionDateBN').val() == null || $('#txtRequestedTransactionDateBN').val().trim() == '') {
        alert("Please fill all mandatory fields");
        return false;
    }
    if ($('#txtTradequantityBrokerNote').val() == undefined || $('#txtTradequantityBrokerNote').val() == null || $('#txtTradequantityBrokerNote').val().trim() == '') {
        alert("Please fill all mandatory fields");
        return false;
    }
    //if ($('#txtActualTradequantityBrokerNote').val() == undefined || $('#txtActualTradequantityBrokerNote').val() == null || $('#txtActualTradequantityBrokerNote').val().trim() == '') {
    //    alert("Please fill all mandatory fields");
    //    return false;
    //}
    //if ($('#txtValuePerShare').val() == undefined || $('#txtValuePerShare').val() == null || $('#txtValuePerShare').val().trim() == '') {
    //    alert("Please fill all mandatory fields");
    //    return false;
    //}
    if ($('#ddlDematAccountBrokerNote').val() == undefined || $('#ddlDematAccountBrokerNote').val() == null || $('#ddlDematAccountBrokerNote').val().trim() == '' || $('#ddlDematAccountBrokerNote').val().trim() == '0') {
        alert("Please fill all mandatory fields");
        return false;
    }
    //if ($('#txtActualdateoftransaction').val() == undefined || $('#txtActualdateoftransaction').val() == null || $('#txtActualdateoftransaction').val().trim() == '') {
    //    alert("Please fill all mandatory fields");
    //    return false;
    //}
    //if ($('#remarks').val() == undefined || $('#remarks').val() == null || $('#remarks').val().trim() == '') {
    //    alert("Please fill all mandatory fields");
    //    return false;
    //}

    if (BrokerDetailsManadatory) {
        if ($('#txtareabrokerdetails').val() == undefined || $('#txtareabrokerdetails').val() == null || $('#txtareabrokerdetails').val().trim() == '') {
            alert("Please fill all mandatory fields");
            return false;
        }
    }

    return true;
}
function goToNullTrade() {
    $("#modalNullTrade").modal('show');
}
function fnSubmitBrokerNote() {
    $("#Loader").show();
    var test = new FormData();
    var preClearanceRequestId = $("#preClearanceRequestIdBN").val();
    // var tempFile = $('#btnBrokernote').get(0).files[0].name;
    //  var brokerNote = tempFile.split('.')[0] + "_" + preClearanceRequestId + "." + tempFile.split('.')[1];
    var brokerNote = '';
    var tradeQuantity = $("#txtTradequantityBrokerNote").val();
    var actualTradeQuantity = $("#txtActualTradequantityBrokerNote").val();
    var valuePerShare = $('#txtValuePerShare').val();

    if (actualTradeQuantity == null || actualTradeQuantity == undefined || actualTradeQuantity.trim() == '') {
        $('#txtTotalamount').val(tradeQuantity * valuePerShare);
    }
    else {
        $('#txtTotalamount').val(actualTradeQuantity * valuePerShare);
    }
    var totalAmount = $('#txtTotalamount').val();
    var actualTransactionDate = $('#txtActualdateoftransaction').val();
    var status = $("#txtStatusBN").val();
    var tradeExchange = $("#txtTradeExchangeBN").val();
    var dematAccountID = $("#txtDematAccountBN").val();
    var exchangeTradedOn = $("#ddlExchangeTradedOn").val();
    var remarks = $("#remarks").val();
    var brokerdetails = $("#txtareabrokerdetails").val();

    var MulTiTradeList = new Array();
    for (var i = 0; i < $("#tbdMultiTrade").children().length; i++) {
        var bNote = new Object();
        var mtDate = $($($($("#tbdMultiTrade").children()[i]).children()[0]).children()[0]).val();
        var mtQty = $($($($("#tbdMultiTrade").children()[i]).children()[1]).children()[0]).val();
        var mtRate = $($($($("#tbdMultiTrade").children()[i]).children()[2]).children()[0]).val();
        var mtAmount = $($($($("#tbdMultiTrade").children()[i]).children()[3]).children()[0]).val();

        bNote.PartialTradeDate = mtDate;
        bNote.PartialTradeQuantity = mtQty;
        bNote.PartialValuePerShare = mtRate;
        bNote.PartialTotalAmount = mtAmount;
        MulTiTradeList.push(bNote);
    }

    var arExtns = ['pdf'];
    var ctrl = $('#btnBrokernote');
    var file = $('#btnBrokernote').val();
    var ext = file.split(".");
    ext = ext[ext.length - 1].toLowerCase();
    if (arExtns.lastIndexOf(ext) == -1) {
        alert("Please select a file with extension(s).\n" + arExtns.join(', '));
        ctrl.value = '';
        $("#Loader").hide();
        return false;
    }

    test.append("Object", JSON.stringify({ PreClearanceRequestId: preClearanceRequestId, BrokerNote: brokerNote, ActualTransactionDate: actualTransactionDate, ValuePerShare: valuePerShare, TotalAmount: totalAmount, TradeQuantity: tradeQuantity, ActualTradeQuantity: actualTradeQuantity, Status: status, TradeExchange: tradeExchange, DematAccount: dematAccountID, remarks: remarks, exchangeTradedOn: exchangeTradedOn, lstMultiTrade: MulTiTradeList, BrokerDetails: brokerdetails }));
    test.append("Files", $("#btnBrokernote").get(0).files[0]);

    var webUrl = uri + "/api/PreClearanceRequest/SaveBrokerNoteUpload";
    $.ajax({
        url: webUrl,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        contentType: false,
        processData: false,
        data: test,
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                alert(msg.Msg);
                if (msg.PreClearanceRequest.lstFormUrl !== null) {
                    if (msg.PreClearanceRequest.lstFormUrl.length) {
                        for (var x = 0; x < msg.PreClearanceRequest.lstFormUrl.length; x++) {
                            //alert(msg.PreClearanceRequest.lstFormUrl[x]);
                            window.open("emailAttachment/" + msg.PreClearanceRequest.lstFormUrl[x]);
                        }
                    }
                }

                window.location.reload(true);
                //if (msg.PreClearanceRequest.PreClearanceRequestId == preClearanceRequestId) {
                //    //if (msg.PreClearanceRequest.ExceededTradeLimit != "Yes") {
                //    //    alert(msg.Msg);
                //    //    window.location.reload(true);
                //    //}
                //    //else {
                //    $("input[id*='txtBrokerNoteId']").val(msg.PreClearanceRequest.brokerNoteId);
                //    if (msg.PreClearanceRequest.lstFormUrl != null) {
                //        $("select[id*='ddlForms']").empty();
                //        var strOption = "";
                //        var strHtml = "";
                //        arrForms = new Array();
                //        for (var x = 0; x < msg.PreClearanceRequest.lstFormUrl.length; x++) {
                //            var strValue = msg.PreClearanceRequest.lstFormUrl[x];
                //            strOption += strValue.split("~")[1] + " & ";
                //            arrForms.push(strValue.split("~")[0]);
                //            strHtml += '<div id="divForm_' + strValue.split("~")[0] + '">';
                //            strHtml += '<div class="col-md-4" id="lblUploadForm_' + strValue.split("~")[0] + '">' + strValue.split("~")[1] + '</div>';
                //            strHtml += '<div class="col-md-8">';
                //            strHtml += '<input type="file" id="txtUploadForm_' + strValue.split("~")[0] + '" class="form-control" data-tabindex="4" />';
                //            strHtml += '</div>';
                //            strHtml += '</div><br />';
                //        }
                //        var sHtml = strHtml.substr(0, strHtml.length - 6);
                //        $("#divUploadForm").html(sHtml);

                //        var s = strOption.substr(0, strOption.length - 3);
                //        ddlForms.append(new Option(s, "All"));

                //        if (msg.PreClearanceRequest.isNUllTrade) {
                //            fnDisplayNote(null, null, "Non Trade");
                //        }
                //        else {
                //            fnDisplayNote(null, null, "Trade");
                //        }
                //    }

                //    //$("select[id*='ddlForms']").find('option[value=FORM_J]').remove();
                //    //if (userCategory.toUpperCase() == "PROMOTER") {
                //    //    $("select[id*='ddlForms']").find('option[value=FORM_CJ]').remove();
                //    //    $("#divForm").show();
                //    //    $("#divAnnexure").show();
                //    //    $("select[id*='ddlForms']").find('option[value=FORM_DJ]').prop("selected", true);
                //    //    fnDisplayNote(null, null, "FORM_DJ");
                //    //}
                //    //else {
                //    //    $("select[id*='ddlForms']").find('option[value=FORM_DJ]').remove();
                //    //    $("#divForm").show();
                //    //    $("#divAnnexure").show();
                //    //    $("select[id*='ddlForms']").find('option[value=FORM_CJ]').prop("selected", true);
                //    //    fnDisplayNote(null, null, "FORM_CJ");
                //    //}
                //    $("#btnCancelBrokerNote").trigger("click");
                //    $("#btnOpenForm").trigger("click");

                //    //$("#btnOpenForm").attr('onclick', 'javascript:fnShowForm(\'' + msg.PreClearanceRequest.brokerNoteId + '\');');

                //    //$("#btnOpenForm").attr("data-target", "#modalForms");
                //    //$("#btnOpenForm").attr("data-toggle", "modal");

                //    //window.location.reload(); 
                //    //   $("#tdIsBrokerNoteUploaded_" + msg.PreClearanceRequest.PreClearanceRequestId).html('Y');
                //    $("#liUploadbrokernote_" + msg.PreClearanceRequest.PreClearanceRequestId).hide();
                //    //}
                //}

                //fnClearFormBrokerNote();
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
    });
}
function initializeDataTable() {
    var table = $('#tbl-preclearance-setup').DataTable({
        //  "dom": '<"top"iflp<"clear">>rt<"bottom"iflp<"clear">>',
        dom: 'Bfrtip',
        pageLength: 10,
        "scrollY": "300px",
        "scrollX": true,
        buttons: [
            {
                extend: 'pdf',
                className: 'btn green btn-outline',
                exportOptions: {
                    columns: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14]
                }
            },
            {
                extend: 'excel',
                className: 'btn yellow btn-outline ',
                exportOptions: {
                    columns: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14]
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
            { "data": "" },
            { "data": "Id" },
            { "data": "for" },
            { "data": "Relation" },
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

        ],
        "order": [[2, 'desc']]
    });
    table.columns.adjust().draw();
}
function initializeDataTable1() {
    $('#tbl-preclearance-other-setup').DataTable({
        dom: 'lBfrtip',
        pageLength: 10,
        buttons: [
            {
                extend: 'pdf',
                className: 'btn green btn-outline',
                exportOptions: {
                    columns: [1, 2, 3, 4, 5, 6, 7, 8, 9]
                }
            },
            {
                extend: 'excel',
                className: 'btn yellow btn-outline ',
                exportOptions: {
                    columns: [1, 2, 3, 4, 5, 6, 7, 8, 9]
                }
            },
        ],

    });
}
function fnValidateNullTrade() {
    if ($("textarea[id*='txtNullTradeRemarks']").val() == "") {
        $("#lblNullTradeRemarks").addClass('required');
        $("#btnSubmitNullTrade").removeAttr("data-dismiss");
        return false;
    }
    $("#nullTradeConfirmation").show();
}
function nullTrade() {
    $("#Loader").show();
    var test = new FormData();
    var preClearanceRequestId = $("#preClearanceRequestIdBN").val();
    var isNullTrade = true;
    test.append("Object", JSON.stringify({ PreClearanceRequestId: preClearanceRequestId, isNUllTrade: isNullTrade, nullTradeRemarks: $("textarea[id*='txtNullTradeRemarks']").val() }));
    var webUrl = uri + "/api/PreClearanceRequest/SaveBrokerNoteUpload";
    $.ajax({
        url: webUrl,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        contentType: false,
        //   async: false,
        processData: false,
        data: test,
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                alert(msg.Msg);
                if (msg.PreClearanceRequest.lstFormUrl != null) {
                    if (msg.PreClearanceRequest.lstFormUrl.length) {
                        for (var x = 0; x < msg.PreClearanceRequest.lstFormUrl.length; x++) {
                            //alert(msg.PreClearanceRequest.lstFormUrl[x]);
                            window.open("emailAttachment/" + msg.PreClearanceRequest.lstFormUrl[x]);
                        }
                    }
                }
                window.location.reload(true);
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
    });
}