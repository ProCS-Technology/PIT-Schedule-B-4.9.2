var arrSecType = new Array();
var arrTransType = new Array();
var arrDetails = new Array();
var TradeFrm = "";
var TradeTo = "";
$(document).ready(function () {
    fnGetTypeOfSecurity();
    fnGetTypeOfTransaction();
    fnGetDetailsOfUser();
    fnGetTypeOfRestrictedCompanies();

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
});
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
                    $("#ddlFor").append(result);
                    $("#ddlForBN").append(result);
                    $("#ddlForOther").append(result);
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
function fnOpen(TransactionId, RelativeId, TradeQty, Demat, TradeDt, TransactionType, FromDt, ToDt) {
    $("#ddlRestrictedCompaniesBN").html($("input[id*=hdnCmpn]").val());
    $("#txtTransactionId").val(TransactionId);
    $("#ddlForBN").val(RelativeId);
    $("#txtTradequantityBrokerNote").val(TradeQty);
    $("#txtRemainingTradequantity").val(TradeQty);
    $("#txtRequestedTransactionDateBN").val(FormatDate(TradeDt, $("input[id*=hdnDateFormat]").val()));
    $("#ddlDematAccountBrokerNote").html('');
    $("#ddlDematAccountBrokerNote").html("<option value'" + Demat + "'>" + Demat + "</option>");
    $("#ddlTypeOfTransactionBN").html('');
    TradeFrm = FromDt;
    TradeTo = TradeDt;
    if (TransactionType == "Buy") {
        var strHtml = "";
        for (var x = 0; x < arrTransType.length; x++) {
            if (arrTransType[x].Nature == "+" && arrTransType[x].Name!="Release of Pledge") {
                strHtml += "<option value='" + arrTransType[x].Id + "'>" + arrTransType[x].Name + "</option>";
            }
        }
        $("#ddlTypeOfTransactionBN").html(strHtml);
    }
    else {
        var strHtml = "";
        for (var x = 0; x < arrTransType.length; x++) {
            if (arrTransType[x].Nature == "-" && arrTransType[x].Name != "Creation of Pledge") {
                strHtml += "<option value='" + arrTransType[x].Id + "'>" + arrTransType[x].Name + "</option>";
            }
        }
        $("#ddlTypeOfTransactionBN").html(strHtml);
    }
    fnMultiTradeAddRow();
    $("#mdlTradeDetails").modal('show');
    
}
function fnMultiTradeAddRow() {
    //alert("in function fnMultiTradeAddRow();")
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

    //if (FormatDate(TradeTo, "yyyymmdd") > FormatDate(today, "yyyymmdd")) {
    //    TradeTo = FormatDate(today, "yyyy-mm-dd");
    //}
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
            startDate: FormatDate(TradeFrm, $("input[id*=hdnDateFormat]").val()),
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
function AddUpdateBrokerNote() {
    if (fnValidateBrokerNoteRequestDetails()) {
        if ($("#txtTotalamount").val() == '0') {
            alert("Trade Value cannot be 0.");
        }
        else {
            $("#modalBrokerNoteUploadConfirmation").modal('show');
        }
    }
    else {

    }
}
function fnValidateBrokerNoteRequestDetails() {
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
    if ($('#ddlDematAccountBrokerNote').val() == undefined || $('#ddlDematAccountBrokerNote').val() == null || $('#ddlDematAccountBrokerNote').val().trim() == '' || $('#ddlDematAccountBrokerNote').val().trim() == '0') {
        alert("Please fill all mandatory fields");
        return false;
    }
    if ($('#ddlExchangeTradedOn').val() == undefined || $('#ddlExchangeTradedOn').val() == null || $('#ddlExchangeTradedOn').val().trim() == '') {
        alert("Please fill all mandatory fields");
        return false;
    }
    if ($('#remarks').val() == undefined || $('#remarks').val() == null || $('#remarks').val().trim() == '') {
        alert("Please fill all mandatory fields");
        return false;
    }


    var MulTiTradeList = new Array();
    var TotalTradedQty = 0;
    for (var i = 0; i < $("#tbdMultiTrade").children().length; i++) {
        var bNote = new Object();
        var mtDate = $($($($("#tbdMultiTrade").children()[i]).children()[0]).children()[0]).val();
        var mtQty = $($($($("#tbdMultiTrade").children()[i]).children()[1]).children()[0]).val();
        var mtRate = $($($($("#tbdMultiTrade").children()[i]).children()[2]).children()[0]).val();
        var mtAmount = $($($($("#tbdMultiTrade").children()[i]).children()[3]).children()[0]).val();
        /*alert("mtDate=" + mtDate);
        alert("mtQty=" + mtQty);
        alert("mtRate=" + mtRate);
        alert("mtAmount=" + mtAmount);
        alert("TotalTradedQty=" + TotalTradedQty);*/
        if (mtQty != "" && mtQty != null && mtQty != undefined) {
            TotalTradedQty = Number(TotalTradedQty) + Number(mtQty);
        }
        //alert("TotalTradedQty=" + TotalTradedQty);
        bNote.PartialTradeDate = mtDate;
        bNote.PartialTradeQuantity = mtQty;
        bNote.PartialValuePerShare = mtRate;
        bNote.PartialTotalAmount = mtAmount;
        MulTiTradeList.push(bNote);
    }
    //alert("TradedQty=" + $("#txtRemainingTradequantity").val());
    //alert("TotalTradedQty=" + TotalTradedQty);
    if (Number($("#txtRemainingTradequantity").val()) != Number(TotalTradedQty)) {
        alert("Total quantity should be equal to traded quantity");
        return false;
    }
    return true;
}
function fnSubmitBrokerNoteRequestDetails() {
    $("#Loader").show();
    var test = new FormData();
    var preClearanceRequestId = $("#txtTransactionId").val();
    var brokerNote = '';
    var tradeQuantity = $("#txtTradequantityBrokerNote").val();
    var actualTradeQuantity = $("#txtTradequantityBrokerNote").val();
    var valuePerShare = $('#txtValuePerShare').val();

    if (actualTradeQuantity == null || actualTradeQuantity == undefined || actualTradeQuantity.trim() == '') {
        $('#txtTotalamount').val(tradeQuantity * valuePerShare);
    }
    else {
        $('#txtTotalamount').val(actualTradeQuantity * valuePerShare);
    }
    var totalAmount = $('#txtTotalamount').val();
    var actualTransactionDate = $('#txtRequestedTransactionDateBN').val();
    var status = $("#txtStatusBN").val();
    var tradeExchange = $("#txtTradeExchangeBN").val();
    var dematAccountID = $("#txtDematAccountBN").val();
    var exchangeTradedOn = $("#ddlExchangeTradedOn").val();
    var remarks = $("#remarks").val();
    var brokerdetails = $("#txtareabrokerdetails").val();
    var TransactionTypId = $("#ddlTypeOfTransactionBN").val();
    //alert(TransactionTypId);
    var MulTiTradeList = new Array();

    var iQty=0;
    var iRate=0;
    var iAmount=0;
    for (var i = 0; i < $("#tbdMultiTrade").children().length; i++) {
        var bNote = new Object();
        var mtDate = $($($($("#tbdMultiTrade").children()[i]).children()[0]).children()[0]).val();
        var mtQty = $($($($("#tbdMultiTrade").children()[i]).children()[1]).children()[0]).val();
        var mtRate = $($($($("#tbdMultiTrade").children()[i]).children()[2]).children()[0]).val();
        var mtAmount = $($($($("#tbdMultiTrade").children()[i]).children()[3]).children()[0]).val();

        if (mtDate != "" && mtDate != null && mtDate != undefined) {
            if (mtQty != "" && mtQty != null && mtQty != undefined) {
                iQty = Number(iQty) + Number(mtQty);
            }
            if (mtRate != "" && mtRate != null && mtRate != undefined) {
                iRate = Number(iRate) + Number(mtRate);
            }
            if (mtAmount != "" && mtAmount != null && mtAmount != undefined) {
                iAmount = Number(iAmount) + Number(mtAmount);
            }
        }

        bNote.PartialTradeDate = mtDate;
        bNote.PartialTradeQuantity = mtQty;
        bNote.PartialValuePerShare = mtRate;
        bNote.PartialTotalAmount = mtAmount;
        MulTiTradeList.push(bNote);
    }
    valuePerShare = Number(iAmount) / Number(iQty);
    test.append("Object", JSON.stringify({
        PreClearanceRequestId: preClearanceRequestId,
        TransactionType: TransactionTypId,
        BrokerNote: brokerNote,
        ActualTransactionDate: actualTransactionDate,
        ValuePerShare: valuePerShare.toFixed(2),
        TotalAmount: iAmount,
        TradeQuantity: iQty,
        ActualTradeQuantity: iQty,
        Status: status,
        TradeExchange: tradeExchange,
        DematAccount: dematAccountID,
        remarks: remarks,
        exchangeTradedOn: exchangeTradedOn,
        lstMultiTrade: MulTiTradeList,
        BrokerDetails: brokerdetails
    }));
    test.append("Files", $("#btnBrokernote").get(0).files[0]);

    var webUrl = uri + "/api/PreClearanceRequest/SaveBenposTradeTransaction";
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
                //if (msg.PreClearanceRequest.lstFormUrl !== null) {
                //    if (msg.PreClearanceRequest.lstFormUrl.length) {
                //        for (var x = 0; x < msg.PreClearanceRequest.lstFormUrl.length; x++) {
                //            window.open("emailAttachment/" + msg.PreClearanceRequest.lstFormUrl[x]);
                //        }
                //    }
                //}
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