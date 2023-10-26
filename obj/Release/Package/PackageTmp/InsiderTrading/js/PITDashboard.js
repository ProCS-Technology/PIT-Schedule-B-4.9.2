var eventList = [];
var userRole = "";
var userCategory = "";
var transactionSubTypeMaster = null;
$(document).ready(function () {
    window.history.forward();
    function preventBack() { window.history.forward(1); }
    $("#Loader").hide();
    fnGetInsiderTradingWindowClosureInfo();
    getLoggedInUserInformation();
    getCountOfAllPreClearanceRequest();
    getCountOfAllPreClearanceRequestForAllUser();
    fnGetCountOfAllTradeDetails();
    fnGetCountOfMyTradeDetails();
    fnGetTradeDetailsInfo();
    fnGetMyActionable();
    //fnGetRelativeDetail();
    fnGetTypeOfSecurity();
    fnGetTypeOfRestrictedCompanies();
    fnGetTypeOfTransaction();
    fnGetOpenDisclosureRequest();

    $("#ddlForBN").on('change', function () {
        var SelVal = $(this).val();
        if (!(SelVal == "" || SelVal == null || SelVal == "Select")) {
            var currentValue = $(this).val();
            fnGetDematAccount(currentValue);
        }
        else {
            $("#ddlDematAccountBrokerNote").html('');
        }
    }).trigger('change');

    $("#txtTradeQuantity").on('focusout', function () {
        if ($("#txtTradeQuantity").val() !== null && $("#txtTradeQuantity").val() !== undefined && $("#txtTradeQuantity").val() !== '' && $("#txtValuePerShare").val() !== null && $("#txtValuePerShare").val() !== undefined && $("#txtValuePerShare").val() !== '') {
            var totalAmount = $("#txtTradeQuantity").val() * $("#txtValuePerShare").val();
            $("#txtTotalamount").val(totalAmount);
        }
        else {
            if ($("#txtTradeQuantity").val() !== null && $("#txtTradeQuantity").val() !== undefined && $("#txtTradeQuantity").val() !== '' && $("#txtValuePerShare").val() !== null && $("#txtValuePerShare").val() !== undefined && $("#txtValuePerShare").val() !== '') {
                var totalAmount = $("#txtTradeQuantity").val() * $("#txtValuePerShare").val();
                $("#txtTotalamount").val(totalAmount);
            }
        }
    })
    $("#txtValuePerShare").on('focusout', function () {
        if ($("#txtTradeQuantity").val() !== null && $("#txtTradeQuantity").val() !== undefined && $("#txtTradeQuantity").val() !== '' && $("#txtValuePerShare").val() !== null && $("#txtValuePerShare").val() !== undefined && $("#txtValuePerShare").val() !== '') {
            var totalAmount = $("#txtTradeQuantity").val() * $("#txtValuePerShare").val();
            $("#txtTotalamount").val(totalAmount);
        }
        else {
            if ($("#txtTradeQuantity").val() !== null && $("#txtTradeQuantity").val() !== undefined && $("#txtTradeQuantity").val() !== '' && $("#txtValuePerShare").val() !== null && $("#txtValuePerShare").val() !== undefined && $("#txtValuePerShare").val() !== '') {
                var totalAmount = $("#txtTradequantityBrokerNote").val() * $("#txtValuePerShare").val();
                $("#txtTotalamount").val(totalAmount);
            }
        }
    })
    $("#txtValuePerShareNc").on('focusout', function () {
        var NcTradeQty = $("#spnTradeQuantity").html();
        if (NcTradeQty !== null && NcTradeQty !== undefined && NcTradeQty !== '') {
            var totalAmount = NcTradeQty * $("#txtValuePerShareNc").val();
            $("#txtTotalamountNc").val(totalAmount);
        }
        else {
            $("#txtTotalamountNc").val('0');
        }

    })
    $("#txtRequestedTransactionDateBN").datepicker({
        todayHighlight: true,
        autoclose: true,
        format: "dd/mm/yyyy",
        clearBtn: true,
        endDate: "today",
        daysOfWeekDisabled: [0, 6]
    }).attr('readonly', 'readonly');

    $("#ddlProposedTransactionThroughNc").on('change', function () {
        //'Off-Market Deal'
        if ($(this).val() == "Off-Market Deal") {
            $("#dvBrokerDetailsNC").show();
            $("#dvExchangeTradedOnNC").hide();
        }
        else {
            $("#dvBrokerDetailsNC").hide();
            $("#dvExchangeTradedOnNC").show();
        }
    })

    $("#ddlProposedTransactionThrough").on('change', function () {
        //'Off-Market Deal'
        if ($(this).val() == "Off-Market Deal") {
            $("#dvBrokerDetails").show();
            $("#dvExchangeTradedOn").hide();
        }
        else {
            $("#dvBrokerDetails").hide();
            $("#dvExchangeTradedOn").show();
        }
    })

    $('body').on('focus', ".datepicker_recurring_start", function () {
        $(this).datepicker();
    })
    GetQuarter();
    var FinancialYear = getCurrentFinancialYear();

    $("#fy1").html(FinancialYear);
    $("#fy2").html(FinancialYear);
    $("#fy3").html(FinancialYear);
    $("#fy4").html(FinancialYear);
})
function isJson(str) {
    try {
        JSON.parse(str);
    } catch (e) {
        return false;
    }
    return true;
}
function fnGetTransactionalInfo() {
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/GetTransactionalInfo";
    $.ajax({
        url: webUrl,
        type: "GET",
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            if (msg.StatusFl == true) {
                $("#Loader").hide();
                var options = document.getElementById("ContentPlaceHolder1_ddlCategory").options;
                if (options !== null && options !== undefined && options.length > 0) {
                    $.each(options, function (index, item) {
                        if (msg.User.category.ID == item.value) {
                            userCategory = item.text;
                        }
                    })
                }
            }
            else {
                $("#Loader").hide();
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
function fnDisplayNote(ddlForms, val, strVal) {
    var note = "";
    var mode = "";
    if (ddlForms == null) {
        mode = strVal;
    }
    else {
        mode = $(ddlForms).val();
        fnRemoveClass(ddlForms, val);
    }
    switch (mode) {
        case "All":
            note = "To be submitted by the Designated Person for submission to Compliance Officer which will inturn be forwarded to Stock Exchanges, if required (in case the transaction in a calendar quarter is exceeding INR 1,000,000 ).";
            break;
        case "FORM_CJ":
            note = "To be submitted by the Designated Person for submission to Compliance Officer which will inturn be forwarded to Stock Exchanges, if required (in case the transaction in a calendar quarter is exceeding INR 1,000,000).";
            break;
        case "FORM_DJ":
            note = "To be submitted by the Designated Person for submission to Compliance Officer which will inturn be forwarded to Stock Exchanges, if required (in case the transaction in a calendar quarter is exceeding INR 1,000,000).";
            break;
        case "FORM_J":
            note = "Nil Disclosure Submits by Employee and connected person";
            break;
        default:
            break;
    }
    $("label[id*='lblNote']").text(note);
    $("label[id*='lblNote']").show();
}
function fnShowUploadDiv(chkOverwrite) {
    var check = $(chkOverwrite).prop("checked");
    if (check) {
        $("#divUploadForm").show();
        $("#divUploadLbl").show();
        $("input[id*='txtUploadForm']").val(null);
    }
    else {
        $("#divUploadForm").hide();
        $("#divUploadLbl").hide();
        $("input[id*='txtUploadForm']").val(null);
    }
}
function fnDownloadForm() {
    if ($("select[id*='ddlForms']").val() == "0") {
        alert("Please Select Forms to Download");
        return false;
    }

    $("#Loader").show();
    var preClearanceRequestId = $("#preClearanceRequestIdBN").val();
    var brokerNoteId = $("input[id*='txtBrokerNoteId']").val();
    var webUrl = uri + "/api/PreClearanceRequest/GetForms";
    $.ajax({
        url: webUrl,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        contentType: false,
        data: JSON.stringify({ PreClearanceRequestId: preClearanceRequestId, brokerNoteId: brokerNoteId, formType: $("select[id*='ddlForms']").val() }),
        success: function (msg) {
            $("#Loader").hide();
            if (msg.StatusFl) {
                $("#btnSubmitForm").prop("disabled", false);
                if (msg.PreClearanceReques !== null && msg.PreClearanceRequest.lstFormUrl !== null) {
                    for (var x = 0; x < msg.PreClearanceRequest.lstFormUrl.length; x++) {
                        //alert("msg.PreClearanceRequest.lstFormUrl[" + x + "]=" + msg.PreClearanceRequest.lstFormUrl[x]);
                        window.open(".." + msg.PreClearanceRequest.lstFormUrl[x]);
                    }
                    //if (msg.PreClearanceRequest.lstFormUrl.length) {
                    //    $.each(msg.PreClearanceRequest.lstFormUrl, function (index, element) {
                    //        downloadURL1(element);
                    //    })
                    //}
                }
                //downloadURL1(msg.PreClearanceRequest.formUrl);
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
    });
}
function fnValidateForms() {
    if (!fnValidateSubmitForms()) {
        $('#btnSubmitForm').removeAttr("data-dismiss");
        return false;
    }
    else {
        $("#modalSubmitConfirmation").show();
    }
}
function fnSubmitForms() {
    $("#Loader").show();
    if ($("input[id*='chkOverwrite']").prop('checked') == true) {
        fnSubmitCustomForm();
    }
    else {
        fnSubmitSystemGeneratedForm();
    }
}
function fnSubmitCustomForm() {
    var filesData = new FormData();
    var uploadFormName = "";

    if ($("input[id*='txtUploadForm']").get(0).files.length > 0) {
        uploadFormName = $("input[id*='txtUploadForm']").get(0).files[0].name;
    }

    filesData.append("Object", JSON.stringify({ PreClearanceRequestId: $("#preClearanceRequestIdBN").val(), brokerNoteId: $("input[id*='txtBrokerNoteId']").val(), BrokerNote: uploadFormName, formType: $("select[id*='ddlForms']").val() }));

    if ($("input[id*='txtUploadForm']").get(0).files.length > 0) {
        //  filesData.append("Files", $("input[id*='txtUploadForm']").get(0).files[0]);
        filesData.append($("input[id*='txtUploadForm']").get(0).files[0].name, $("input[id*='txtUploadForm']").get(0).files[0]);
    }

    //if ($("input[id*='txtUploadFormAnnexure']").get(0).files.length > 0) {
    //    filesData.append($("input[id*='txtUploadFormAnnexure']").get(0).files[0].name, $("input[id*='txtUploadFormAnnexure']").get(0).files[0]);
    //}

    var webUrl = uri + "/api/PreClearanceRequest/SubmitCustomForm";
    $.ajax({
        type: 'POST',
        url: webUrl,
        data: filesData,
        contentType: false,
        // async: false,
        processData: false,
        success: function (msg) {
            $("#Loader").hide();
            if (msg.StatusFl) {
                alert(msg.Msg);
                window.location.reload(true);
            }
            else {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    //  alert(msg.Msg);
                    $('#btnSubmitForm').removeAttr("data-dismiss");
                }
                return false;
            }
        },
        error: function (error) {
            $("#Loader").hide();
            $('#btnSubmitForm').removeAttr("data-dismiss");
            alert(error.status + ' ' + error.statusText);
        }
    })
}
function fnSubmitSystemGeneratedForm() {
    var filesData = new FormData();

    filesData.append("Object", JSON.stringify({ PreClearanceRequestId: $("#preClearanceRequestIdBN").val(), brokerNoteId: $("input[id*='txtBrokerNoteId']").val(), formType: $("select[id*='ddlForms']").val() }));

    var webUrl = uri + "/api/PreClearanceRequest/SubmitSystemGeneratedForm";
    $.ajax({
        type: 'POST',
        url: webUrl,
        data: filesData,
        contentType: false,
        //  async: false,
        processData: false,
        success: function (msg) {
            $("#Loader").hide();
            if (msg.StatusFl) {
                alert(msg.Msg);
                window.location.reload(true);
            }
            else {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    //  alert(msg.Msg);
                    $('#btnSubmitForm').removeAttr("data-dismiss");
                }
                return false;
            }
        },
        error: function (error) {
            $("#Loader").hide();
            $('#btnSubmitForm').removeAttr("data-dismiss");
            alert(error.status + ' ' + error.statusText);
        }
    })
}
function fnValidateSubmitForms() {
    var isValid = true;
    if ($("input[id*='chkOverwrite']").prop('checked') == true) {
        var arrayExtensions = ["docx", "pdf"];
        var specialChars = "#%&*:<>?/{|}";

        var txtUploadForm = $("input[id*='txtUploadForm']").get(0);
        var formFile = txtUploadForm.files;
        if (formFile.length == 0) {
            isValid = false;
            $("#lblUploadForm").addClass('required');
            return isValid;
        }

        if (formFile.length > 0) {

            /**** #region Restrict Special Characters in Item Document ****/

            for (j = 0; j < specialChars.length; j++) {
                if (formFile[0].name.indexOf(specialChars[j]) > -1) {
                    $("#lblUploadForm").addClass('required');
                    alert("Special Characters [ # % & * : < > ? / { | } ] are not allowed in Upload Form \n\n\n" + formFile[0].name);
                    return false;
                }
            }
            $("#lblUploadForm").removeClass('required');

            /**** #endregion ****/

            /**** #region Restrict Extensions in Item Document ****/

            if ($.inArray(formFile[0].name.split('.').pop().toLowerCase(), arrayExtensions) == -1) {
                $("#lblUploadForm").addClass('required');
                alert("Only docx, pdf format is allowed in Upload Forms!");
                isValid = false;
                return false;
            }
            else {
                $("#lblUploadForm").removeClass('required');
            }
        }
        if (!isValid) {
            alert("Only mentioned File Types (docx, pdf) are allowed in Upload Forms!");
            return false;
        }
    }
    else {
        if ($("select[id*='ddlForms']").val() == "0") {
            isValid = false;
            alert("Please Select Form Type to Submit");
            return false;
        }
    }
    return isValid;
}
function fnRemoveClass(obj, val) {
    $("#lbl" + val + "").removeClass('required');
}
function fnHideShow(val) {
    switch (val) {
        case "modalSubmitConfirmation":
            $("#modalSubmitConfirmation").hide();
            break;

        case "nullTradeConfirmation":
            $("#nullTradeConfirmation").hide();
            break;
    }
}
function fnGetDematAccount(currentValue) {
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

                if (msg.DematDetailList.length > 1) {
                    result += "<option value='0'>--Select--</option>";
                }

                for (var i = 0; i < msg.DematDetailList.length; i++) {
                    result += "<option value='" + msg.DematDetailList[i].accountNo + "'>" + msg.DematDetailList[i].accountNo + "</option>";
                }
                $("#ddlDematAccountBrokerNote").html(result);
            }
            else {
                $("#ddlDematAccountBrokerNote").html('');
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

                //if (msg.SecurityTypeList.length > 1) {
                //    result += "<option value='0'>--Select--</option>";
                //}
                for (var i = 0; i < msg.SecurityTypeList.length; i++) {
                    if (msg.SecurityTypeList[i].Name == "Equity") {
                        result += "<option value='" + msg.SecurityTypeList[i].Id + "'>" + msg.SecurityTypeList[i].Name + "</option>";
                    }
                }
                $("#ddlTypeOfSecurity").append(result);
                $("#ddlTypeOfSecurityNc").append(result);
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
                if (msg.RestrictedCompaniesList.length > 1) {
                    result += "<option value='0'>--Select--</option>";
                }
                for (var i = 0; i < msg.RestrictedCompaniesList.length; i++) {
                    result += "<option value='" + msg.RestrictedCompaniesList[i].ID + "'>" + msg.RestrictedCompaniesList[i].companyName + "</option>";
                }
                $("#ddlRestrictedCompanies").append(result);
                $("#ddlRestrictedCompaniesNc").append(result);
            }
            else {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    //   alert(msg.Msg);
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
                if (msg.TransactionTypeList.length > 1) {
                    result += "<option value='0'>--Select--</option>";
                }
                for (var i = 0; i < msg.TransactionTypeList.length; i++) {
                    result += "<option value='" + msg.TransactionTypeList[i].Id + "'>" + msg.TransactionTypeList[i].Name + "</option>";
                }
                $("#ddlTypeOfTransaction").append(result);
                $("#ddlTypeOfTransactionNc").append(result);
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

function fnClearBrokerNoteRequestDetails() {
    $("#ddlForBN").val('');
    $('#ddlTypeOfSecurity').val('');
    $('#ddlRestrictedCompanies').val('');
    $("#ddlTypeOfTransaction").val('');
    $('#txtTradeQuantity').val('');
    $("#txtValuePerShare").val('');
    $("#txtTotalamount").val('');
    $('#ddlDematAccountBrokerNote').html('');
    $('#txtRequestedTransactionDateBN').datepicker("setDate", "");
    $("#btnBrokernote").val('');
    $("#txtShareCurrentMarketPrice").val('');
    $("#ddlProposedTransactionThrough").val('');
    $('#txtRemarks').val('');
    $("#ddlExchangeTradedOn").val('');
}
function fnGetRelativeDetail() {
    $("#Loader").show();
    var webUrl = uri + "/api/PreClearanceRequest/GetRelativeDetailList";
    $.ajax({
        url: webUrl,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        // async: false,
        data: {},
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                var result = "";
                if (msg.RelativeDetailList.length > 1) {
                    result += "<option value='Select'>--Select--</option>";
                }
                //result += "<option value='Select'>--Select--</option>";
                //result += "<option value='0'>Self</option>";
                for (var i = 0; i < msg.RelativeDetailList.length; i++) {
                    result += "<option value='" + msg.RelativeDetailList[i].ID + "'>" + msg.RelativeDetailList[i].relativeName + "</option>";
                }

                $("#ddlForBN").append(result).trigger('change');
            }
            else {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    // alert(msg.Msg);
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
function GetEncryptedKey(meetingId) {
    $("#Loader").show();
    var encKey = "";
    //   setTimeout(function () {
    $.ajax({
        url: uri + "/api/DashboardIT/GetEncryptedMeetingId",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({
            ID: meetingId
        }),
        dataType: "json",
        async: false,
        success: function (msg) {
            encKey = msg;
            $("#Loader").hide();
        },
        error: function (error) {
            $("#Loader").hide();
            if (error.responseText == "Session Expired") {
                alert("Your session is expired. Please login again to continue");
                window.location.href = "../LogOut.aspx";
                return false;
            }
            else {
                alert(error.status + ' ' + error.statusText);
            }
        }
    });
    //  },10)

    return encKey;
}
function getMonth(numericMonth) {
    var month = "";
    switch (numericMonth) {
        case "1":
            month = "JAN";
            break;
        case "2":
            month = "FEB";
            break;
        case "3":
            month = "MAR";
            break;
        case "4":
            month = "APR";
            break;
        case "5":
            month = "MAY";
            break;
        case "6":
            month = "JUN";
            break;
        case "7":
            month = "JUL";
            break;
        case "8":
            month = "AUG";
            break;
        case "9":
            month = "SEP";
            break;
        case "10":
            month = "OCT";
            break;
        case "11":
            month = "NOV";
            break;
        case "12":
            month = "DEC";
            break;
    }
    return month;
}
function GoToMeetingWorkspace(meetingId) {
    $("#Loader").show();
    setTimeout(function () {
        $.ajax({
            url: uri + "/api/DashboardIT/GetEncryptedMeetingId",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({
                ID: meetingId
            }),
            dataType: "json",
            async: false,
            success: function (msg) {
                $("#Loader").hide();
                window.location.replace("MeetingWorkspace.aspx?ID=" + msg);
            },
            error: function (error) {
                $("#Loader").hide();

                if (error.responseText == "Session Expired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                    return false;
                }
                else {
                    alert(error.status + ' ' + error.statusText);
                }
            }
        })
    }, 10);
}
var AppCalendar = function () {

    return {
        //main function to initiate the module
        init: function () {
            this.initCalendar();
            var titleMonthYear = $("#calendar1 .fc-toolbar .fc-left h2").html();
            var titleMonth = titleMonthYear.split(" ")[0].substring(0, 3);
            var titleYear = titleMonthYear.split(" ")[1];
            $("#calendar1 .fc-toolbar .fc-left h2").html(titleMonth + " " + titleYear);
            $("#calendar1 .fc-toolbar .fc-left h2").css({ 'color': '#0F4C71', 'font-size': '20px', 'font-weight': 'bold' });
            $("#calendar1 .fc-toolbar .fc-left").css({ 'margin-left': '10px' });
            $('#calendar1 .fc-toolbar .fc-right').css({ 'margin-right': '10px' });
            $('#calendar1 .fc-toolbar .fc-left .fc-prev-button').click(function () {
                var titleMonthYear = $("#calendar1 .fc-toolbar .fc-left h2").html();
                var titleMonth = titleMonthYear.split(" ")[0].substring(0, 3);
                var titleYear = titleMonthYear.split(" ")[1];
                $("#calendar1 .fc-toolbar .fc-left h2").html(titleMonth + " " + titleYear);
                $("#calendar1 .fc-toolbar .fc-left h2").css({ 'color': '#0F4C71', 'font-size': '20px', 'font-weight': 'bold' });
                $("#calendar1 .fc-toolbar .fc-left").css({ 'margin-left': '10px' });
                $('#calendar1 .fc-toolbar .fc-right').css({ 'margin-right': '10px' });
            });

            $('#calendar1 .fc-toolbar .fc-left .fc-next-button').click(function () {
                var titleMonthYear = $("#calendar1 .fc-toolbar .fc-left h2").html();
                var titleMonth = titleMonthYear.split(" ")[0].substring(0, 3);
                var titleYear = titleMonthYear.split(" ")[1];
                $("#calendar1 .fc-toolbar .fc-left h2").html(titleMonth + " " + titleYear);
                $("#calendar1 .fc-toolbar .fc-left h2").css({ 'color': '#0F4C71', 'font-size': '20px', 'font-weight': 'bold' });
                $("#calendar1 .fc-toolbar .fc-left").css({ 'margin-left': '10px' });
                $('#calendar1 .fc-toolbar .fc-right').css({ 'margin-right': '10px' });
            });

            $('#calendar1 .fc-toolbar .fc-right .fc-today-button').click(function () {
                var titleMonthYear = $("#calendar1 .fc-toolbar .fc-left h2").html();
                var titleMonth = titleMonthYear.split(" ")[0].substring(0, 3);
                var titleYear = titleMonthYear.split(" ")[1];
                $("#calendar1 .fc-toolbar .fc-left h2").html(titleMonth + " " + titleYear);
                $("#calendar1 .fc-toolbar .fc-left h2").css({ 'color': '#0F4C71', 'font-size': '20px', 'font-weight': 'bold' });
                $("#calendar1 .fc-toolbar .fc-left").css({ 'margin-left': '10px' });
                $('#calendar1 .fc-toolbar .fc-right').css({ 'margin-right': '10px' });
            });

            $('#calendar1 .fc-toolbar .fc-right .fc-month-button').click(function () {
                var titleMonthYear = $("#calendar1 .fc-toolbar .fc-left h2").html();
                var titleMonth = titleMonthYear.split(" ")[0].substring(0, 3);
                var titleYear = titleMonthYear.split(" ")[1];
                $("#calendar1 .fc-toolbar .fc-left h2").html(titleMonth + " " + titleYear);
                $("#calendar1 .fc-toolbar .fc-left h2").css({ 'color': '#0F4C71', 'font-size': '20px', 'font-weight': 'bold' });
                $("#calendar1 .fc-toolbar .fc-left").css({ 'margin-left': '10px' });
                $('#calendar1 .fc-toolbar .fc-right').css({ 'margin-right': '10px' });
            });

            //$('#calendar1 .fc-toolbar .fc-right .fc-agendaWeek-button').click(function () {
            //    debugger;
            //    var titleMonthYear = $("#calendar1 .fc-toolbar .fc-left h2").html();
            //    var titleMonth = titleMonthYear.split(",")[0];
            //    var titleYear = titleMonthYear.split(",")[1].substring(2,4);
            //    $("#calendar1 .fc-toolbar .fc-left h2").html(titleMonth + "," + titleYear);
            //});

            //$('#calendar1 .fc-toolbar .fc-right .fc-agendaDay-button').click(function () {
            //    debugger;
            //    var titleMonthYear = $("#calendar1 .fc-toolbar .fc-left h2").html();
            //    var titleMonth = titleMonthYear.split(" ")[0].substring(0, 3);
            //    var titleYear = titleMonthYear.split(" ")[1];
            //    $("#calendar1 .fc-toolbar .fc-left h2").html(titleMonth + " " + titleYear);
            //});
        },

        initCalendar: function () {

            if (!jQuery().fullCalendar) {
                return;
            }

            var date = new Date();
            var d = date.getDate();
            var m = date.getMonth();
            var y = date.getFullYear();

            var h = {};

            if (App.isRTL()) {
                if ($('#calendar1').parents(".portlet").width() <= 720) {
                    $('#calendar1').addClass("mobile");
                    h = {
                        right: 'title, prev, next',
                        center: '',
                        // left: 'agendaDay, agendaWeek, month, today'
                        left: 'month, today'
                    };
                } else {
                    $('#calendar1').removeClass("mobile");
                    h = {
                        right: 'title',
                        center: '',
                        // left: 'agendaDay, agendaWeek, month, today, prev,next'
                        left: 'month, today, prev,next'
                    };
                }
            } else {
                if ($('#calendar1').parents(".portlet").width() <= 720) {
                    $('#calendar1').addClass("mobile");
                    h = {
                        left: 'title, prev, next',
                        center: '',
                        // right: 'today,month,agendaWeek,agendaDay'
                        right: 'today,month'
                    };
                } else {
                    $('#calendar1').removeClass("mobile");
                    h = {
                        left: 'title',
                        center: '',
                        //  right: 'prev,next,today,month,agendaWeek,agendaDay'
                        right: 'prev,next,today,month'
                    };
                }
            }

            var initDrag = function (el) {
                // create an Event Object (http://arshaw.com/fullcalendar/docs/event_data/Event_Object/)
                // it doesn't need to have a start or end
                var eventObject = {
                    title: $.trim(el.text()) // use the element's text as the event title
                };
                // store the Event Object in the DOM element so we can get to it later
                el.data('eventObject', eventObject);
                // make the event draggable using jQuery UI
                //el.draggable({
                //    zIndex: 999,
                //    revert: true, // will cause the event to go back to its
                //    revertDuration: 0 //  original position after the drag
                //});
            };

            var addEvent = function (title) {
                title = title.length === 0 ? "Untitled Event" : title;
                var html = $('<div class="external-event label label-default">' + title + '</div>');
                jQuery('#event_box').append(html);
                initDrag(html);
            };

            $('#external-events div.external-event').each(function () {
                initDrag($(this));
            });

            $('#event_add').unbind('click').click(function () {
                var title = $('#event_title').val();
                addEvent(title);
            });

            //predefined events
            $('#event_box').html("");
            addEvent("My Event 1");
            addEvent("My Event 2");
            addEvent("My Event 3");
            addEvent("My Event 4");
            addEvent("My Event 5");
            addEvent("My Event 6");

            $('#calendar1').fullCalendar('destroy'); // destroy the calendar
            $('#calendar1').fullCalendar({ //re-initialize the calendar
                header: h,
                defaultView: 'month', // change default view with available options from http://arshaw.com/fullcalendar/docs/views/Available_Views/ 
                slotMinutes: 15,
                editable: true,
                droppable: true, // this allows things to be dropped onto the calendar !!!
                drop: function (date, allDay) { // this function is called when something is dropped

                    // retrieve the dropped element's stored Event Object
                    var originalEventObject = $(this).data('eventObject');
                    // we need to copy it, so that multiple events don't have a reference to the same object
                    var copiedEventObject = $.extend({}, originalEventObject);

                    // assign it the date that was reported
                    copiedEventObject.start = date;
                    copiedEventObject.allDay = allDay;
                    copiedEventObject.className = $(this).attr("data-class");

                    // render the event on the calendar
                    // the last `true` argument determines if the event "sticks" (http://arshaw.com/fullcalendar/docs/event_rendering/renderEvent/)
                    $('#calendar1').fullCalendar('renderEvent', copiedEventObject, true);

                    // is the "remove after drop" checkbox checked?
                    if ($('#drop-remove').is(':checked')) {
                        // if so, remove the element from the "Draggable Events" list
                        $(this).remove();
                    }
                },
                eventRender: function (eventObj, $el) {
                    $el.popover({
                        title: eventObj.title,
                        // content: eventObj.description,
                        trigger: 'hover',
                        placement: 'top',
                        container: 'body'
                    });
                },
                events: eventList
            });

        }

    };

}();
function getCountOfAllPreClearanceRequest() {
    $("#Loader").show();
    $.ajax({
        url: uri + "/api/DashboardIT/GetCountOfAllPreClearanceRequest",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (msg) {
            $("#Loader").hide();
            if (msg.StatusFl) {
                $("#allPreClearanceRequest").html(msg.Dashboard.allPreClearanceRequestCount);
                $("#inApprovalPreClearanceRequest").html(msg.Dashboard.inApprovalPreClearanceRequestCount);
                $("#approvedPreClearanceRequest").html(msg.Dashboard.approvedPreClearanceRequestCount);
                $("#rejectedPreClearanceRequest").html(msg.Dashboard.rejectedPreClearanceRequestCount);
            }
            else {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    //  alert(msg.Msg);
                }
            }

        },
        error: function (error) {
            $("#Loader").hide();
            if (error.responseText == "Session Expired") {
                alert("Your session is expired. Please login again to continue");
                window.location.href = "../LogOut.aspx";
                return false;
            }
            else {
                alert(error.status + ' ' + error.statusText);
            }
        }
    });
}
function getCountOfAllPreClearanceRequestForAllUser() {
    $("#Loader").show();
    $.ajax({
        url: uri + "/api/DashboardIT/GetCountOfAllPreClearanceRequestForAllUser",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (msg) {
            $("#Loader").hide();
            if (msg.StatusFl) {
                $("#allPreClearanceRequestForAlluser").html(msg.Dashboard.allPreClearanceRequestCount);
                $("#inApprovalPreClearanceRequestForAlluser").html(msg.Dashboard.inApprovalPreClearanceRequestCount);
                $("#approvedPreClearanceRequestForAlluser").html(msg.Dashboard.approvedPreClearanceRequestCount);
                $("#rejectedPreClearanceRequestForAlluser").html(msg.Dashboard.rejectedPreClearanceRequestCount);
            }
            else {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    // alert(msg.Msg);
                }
            }
        },
        error: function (error) {
            $("#Loader").hide();
            if (error.responseText == "Session Expired") {
                alert("Your session is expired. Please login again to continue");
                window.location.href = "../LogOut.aspx";
                return false;
            }
            else {
                alert(error.status + ' ' + error.statusText);
            }
        }
    });
}
function getLoggedInUserInformation() {
    $("#Loader").show();
    var webUrl = uri + "/api/Transaction/GetUserDetails";
    $.ajax({
        url: webUrl,
        type: "GET",
        async: false,
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                userRole = msg.User.userRole.ROLE_NM;
                userid = msg.User.userRole.ID;
                if (msg.User.userRole.ROLE_NM.toLowerCase() == 'undefined') {
                    $("#modalUPSIPolicyApplicable").modal('show');
                }

                if (msg.User.userRole.ROLE_NM.toLowerCase() == 'admin' || msg.User.userRole.ROLE_NM.toLowerCase() == 'administrator' || msg.User.ID > 0) {
                    $("#allPreClearanceRequestActionable").show();
                    $("#allTradeDetails").show();
                    //  $(".page-content-wrapper .page-content").css({ 'min-height': '1683px !important' });

                }
                else {
                    $("#allPreClearanceRequestActionable").hide();
                    $("#allTradeDetails").hide();
                    //  $(".page-content-wrapper .page-content").css({ 'min-height': '1280px !important' });
                }
            }
            else {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    //alert(msg.Msg);
                    //return false;
                }
            }
        },
        error: function (response) {
            $("#Loader").hide();
            alert(response.status + ' ' + response.statusText);
        }
    })
}
function fnGetInsiderTradingWindowClosureInfo() {
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/GetInsiderTradingWindowClosureInfo";
    $.ajax({
        url: webUrl,
        type: "GET",
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            if (msg.StatusFl == true) {
                $("#Loader").hide();
                var result = "";
                for (var i = 0; i < msg.InsiderTradingWindowList.length; i++) {
                    if (msg.InsiderTradingWindowList[i].toDate == "31/12/9999") {
                        result += "Please Note: The Trading Window is closed from " + msg.InsiderTradingWindowList[i].fromDate + " till further notified - " + msg.InsiderTradingWindowList[i].remarks;
                    }
                    else {
                        result += "Please Note: The Trading Window is closed from " + msg.InsiderTradingWindowList[i].fromDate + " to " + msg.InsiderTradingWindowList[i].toDate + ". - " + msg.InsiderTradingWindowList[i].remarks;
                    }
                    result += "<span>&#9728;</span>";
                }
                result += " You are requested not to enter into a contra trade transaction, i.e. If you have purchased shares, do not sell them within 6 months from the date of purchase  and vice versa.<span>&#9728;</span>";
                $("#divTradingWindowClosureNotification").html(result);
            }
            else {
                $("#Loader").hide();
                var result = "";
                result += " You are requested not to enter into a contra trade transaction, i.e. If you have purchased shares, do not sell them within 6 months from the date of purchase  and vice versa.<span>&#9728;</span>";
                $("#divTradingWindowClosureNotification").html(result);
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
function fnGetCountOfAllTradeDetails() {
    $("#Loader").show();
    $.ajax({
        url: uri + "/api/DashboardIT/GetCountOfAllTradeDetails",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (msg) {
            $("#Loader").hide();
            if (msg.StatusFl) {
                $("#allSubmittedWithClearance").html(msg.Dashboard.submittedWithClearanceCount);
                $("#allSubmittedWithoutClearance").html(msg.Dashboard.submittedWithoutClearanceCount);
                $("#allNotDeclared").html(msg.Dashboard.notDeclaredCount);
            }
            else {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    // alert(msg.Msg);
                }
            }
        },
        error: function (error) {
            $("#Loader").hide();
            if (error.responseText == "Session Expired") {
                alert("Your session is expired. Please login again to continue");
                window.location.href = "../LogOut.aspx";
                return false;
            }
            else {
                alert(error.status + ' ' + error.statusText);
            }
        }
    });
}
function fnGetCountOfMyTradeDetails() {
    $("#Loader").show();
    $.ajax({
        url: uri + "/api/DashboardIT/GetCountOfMyTradeDetails",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (msg) {
            $("#Loader").hide();
            if (msg.StatusFl) {
                $("#mySubmittedWithClearance").html(msg.Dashboard.submittedWithClearanceCount);
                $("#mySubmittedWithoutClearance").html(msg.Dashboard.submittedWithoutClearanceCount);
                $("#myNotDeclared").html(msg.Dashboard.notDeclaredCount);
            }
            else {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    // alert(msg.Msg);
                }
            }
        },
        error: function (error) {
            $("#Loader").hide();
            if (error.responseText == "Session Expired") {
                alert("Your session is expired. Please login again to continue");
                window.location.href = "../LogOut.aspx";
                return false;
            }
            else {
                alert(error.status + ' ' + error.statusText);
            }
        }
    });
}
function fnGetTradeDetailsInfo() {
    $("#Loader").show();
    $.ajax({
        url: uri + "/api/DashboardIT/GetTradeDetailsInfo",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (msg) {
            $("#Loader").hide();
            if (msg.StatusFl) {
                if (msg.Dashboard.tradingLimitType == "Quantity") {
                    var tradeLimit = '<b><u>Above ' + msg.Dashboard.tradingLimitPerQuarterBeforePC + ' Equity Shares</u></b>';
                    $("#tradingLimit").html(tradeLimit);
                }
                else {
                    var tradeLimit = '<b><u>Above INR ' + msg.Dashboard.tradingLimitPerQuarterBeforePC + '</u></b>';
                    $("#tradingLimit").html(tradeLimit);
                }

                /*if ($("#ContentPlaceHolder1_txtCompanyName").val() == 'IEX') {
                    var tradeLimit = '<b><u>Above INR 10,00,000 in a quarter</u></b>';
                    $("#tradingLimit").html(tradeLimit);
                }
                else if ($("#ContentPlaceHolder1_txtCompanyName").val() == "Spencer Retail Ltd.") {
                    var tradeLimit = '<b><u>Amount aggregating Rs. 10 Lakhs each quarter</u></b>';
                    $("#tradingLimit").html(tradeLimit);
                }
                else {
                    var tradeLimit = '<b><u>Above ' + msg.Dashboard.tradingLimitPerQuarterBeforePC + '</u></b>';
                    $("#tradingLimit").html(tradeLimit);
                }*/

                var lastCurrentHoldingUpdateDate = '<b><u>' + msg.Dashboard.lastCurrentHoldingUpdateDate + '</b></u>';
                var userCurrentHolding = '<b><u>' + msg.Dashboard.userCurrentHolding + '</b></u>';

                $("#spLastUpdateCurrentHoldingDate").html(lastCurrentHoldingUpdateDate);
                $("#spCurrentHolding").html(userCurrentHolding);

                var stocksTradedForLoggedInUserUsingBP = '<b><u>' + msg.Dashboard.stocksTradedInPeriodUsingBenpos + '</u></b>';
                $("#stocksTradedForLoggedInUserUsingBP").html(stocksTradedForLoggedInUserUsingBP);

                var stocksTradedForLoggedInUserUsingBN = '<b><u>' + msg.Dashboard.stocksTradedInPeriodUsingBrokerNote + '</u></b>';
                $("#stocksTradedForLoggedInUserUsingBN").html(stocksTradedForLoggedInUserUsingBN);

                var limitRemainingForNoPC = '<b><u>Form-C is required</u></b>';
                $("#limitRemainingForNoPC").html(limitRemainingForNoPC);
                //if (msg.Dashboard.limitRemainingForNoPC >= msg.Dashboard.stocksTradedInQuarter) {
                //    var result = msg.Dashboard.tradingLimitPerQuarterBeforePC - msg.Dashboard.stocksTradedInQuarter;
                //    $("#limitRemainingForNoPC").html(result);
                //}
                //else {
                //    $("#limitRemainingForNoPC").html("Please ensure to take Pre-Clearance for your next trade");
                //}
            }
            else {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    //  alert(msg.Msg);
                }
            }
        },
        error: function (error) {
            $("#Loader").hide();
            if (error.responseText == "Session Expired") {
                alert("Your session is expired. Please login again to continue");
                window.location.href = "../LogOut.aspx";
                return false;
            }
            else {
                alert(error.status + ' ' + error.statusText);
            }
        }
    });
}
function fnGetMyActionable() {
    $("#Loader").show();
    $.ajax({
        url: uri + "/api/DashboardIT/GetMyActionable",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (msg) {
            $("#Loader").hide();
            if (msg.StatusFl == true) {
                var str = '';
                var CountActionable = 0;
                var FY_End = new Date().getFullYear();
                var FY_Start = parseInt(FY_End) - 1;

                if (msg.Dashboard.myDeclarationText !== null) {
                    str += '<li><a href="UserDeclaration.aspx">' + msg.Dashboard.myDeclarationText + " (Annual PIT Disclosure Submission for Financial Year " + FY_Start + "-" + FY_End + ". <br/> PLEASE KEEP THE FOLLOWING DETAILS HANDY FOR FILLING UP THE FORM.- PAN, SPOUSE'S PAN, DEMAT ACCOUNT DETAILS AND NUMBER OF SHARES HELD IN THE COMPANY.)" + '</a><br /><br /></li>';
                    CountActionable = 1;
                }
                if (msg.Dashboard.lstNonComplianceTask !== null) {
                    CountActionable += parseInt(msg.Dashboard.lstNonComplianceTask.length);
                    for (var i = 0; i < msg.Dashboard.lstNonComplianceTask.length; i++) {
                        str += '<li><a href="#">' + msg.Dashboard.lstNonComplianceTask[i].nonCompliantTaskText + '</a><br /><br /></li>';
                    }
                }
                if (msg.Dashboard.trainingModule !== null) {
                    if (msg.Dashboard.trainingModule.StatusFl) {
                        if (msg.Dashboard.trainingModule.TrainingModuleList !== null) {
                            for (var i = 0; i < msg.Dashboard.trainingModule.TrainingModuleList.length; i++) {

                                //if (ConvertToDate(msg.Dashboard.trainingModule.TrainingModuleList[i].trainingEndDate).setHours(0, 0, 0, 0) >= (new Date()).setHours(0, 0, 0, 0)) {
                                //    str += '<li><a style="pointer-events:auto;" href="javascript:fnGoToTrainingModule(\'' + msg.Dashboard.trainingModule.TrainingModuleList[i].trainingId + '\',\'' + msg.Dashboard.trainingModule.TrainingModuleList[i].noOfPages + '\');">Training Submission for - \'' + msg.Dashboard.trainingModule.TrainingModuleList[i].trainingName + '\' required by ' + msg.Dashboard.trainingModule.TrainingModuleList[i].trainingEndDate + '</a><br /><br /></li>';
                                //}
                                //else {
                                //    str += '<li><a style="pointer-events:none;" href="javascript:fnGoToTrainingModule(\'' + msg.Dashboard.trainingModule.TrainingModuleList[i].trainingId + '\',\'' + msg.Dashboard.trainingModule.TrainingModuleList[i].noOfPages + '\');">Training Submission for - \'' + msg.Dashboard.trainingModule.TrainingModuleList[i].trainingName + '\' required by ' + msg.Dashboard.trainingModule.TrainingModuleList[i].trainingEndDate + '</a><br /><br /></li>';
                                //}
                                if (msg.Dashboard.trainingModule.TrainingModuleList[i].trainingModuleUserStatus.status === "Pending") {
                                    CountActionable += parseInt(CountActionable) + 1;
                                    str += '<li><a href="javascript:fnGoToTrainingModule(\'' + msg.Dashboard.trainingModule.TrainingModuleList[i].trainingId + '\',\'' + msg.Dashboard.trainingModule.TrainingModuleList[i].noOfPages + '\',\'' + msg.Dashboard.trainingModule.TrainingModuleList[i].trainingModuleUserStatus.status + '\');">Training Submission for - \'' + msg.Dashboard.trainingModule.TrainingModuleList[i].trainingName + '\' required by ' + msg.Dashboard.trainingModule.TrainingModuleList[i].trainingEndDate + '</a><br /><br /></li>';
                                }
                            }
                        }
                    }
                }

                var htmlNonComplianceTask = '';

                if (msg.Dashboard.lstNonCompliance !== null) {
                    if (msg.Dashboard.lstNonCompliance.length > 0) {
                        CountActionable += parseInt(msg.Dashboard.lstNonCompliance.length);
                        for (var i = 0; i < msg.Dashboard.lstNonCompliance.length; i++) {
                            htmlNonComplianceTask += '<tr>';
                            htmlNonComplianceTask += '<td class="display-none">';
                            htmlNonComplianceTask += msg.Dashboard.lstNonCompliance[i].sName;
                            htmlNonComplianceTask += '</td>';
                            htmlNonComplianceTask += '<td>';
                            htmlNonComplianceTask += msg.Dashboard.lstNonCompliance[i].RelativeName;
                            htmlNonComplianceTask += '</td>';
                            htmlNonComplianceTask += '<td>';
                            htmlNonComplianceTask += msg.Dashboard.lstNonCompliance[i].Relation;
                            htmlNonComplianceTask += '</td>';
                            htmlNonComplianceTask += '<td>';
                            htmlNonComplianceTask += msg.Dashboard.lstNonCompliance[i].panNumber;
                            htmlNonComplianceTask += '</td>';
                            htmlNonComplianceTask += '<td>';
                            htmlNonComplianceTask += msg.Dashboard.lstNonCompliance[i].Folio;
                            htmlNonComplianceTask += '</td>';

                            if (msg.Dashboard.lstNonCompliance[i].nonComplianceType == "Contra Trade" || msg.Dashboard.lstNonCompliance[i].nonComplianceType == "Window Closure") {
                                htmlNonComplianceTask += '<td style="color:red;">';
                            }
                            else {
                                htmlNonComplianceTask += '<td>';
                            }
                            htmlNonComplianceTask += msg.Dashboard.lstNonCompliance[i].nonComplianceType;
                            htmlNonComplianceTask += '</td>';
                            htmlNonComplianceTask += '<td>';
                            htmlNonComplianceTask += msg.Dashboard.lstNonCompliance[i].ncQuantity;
                            htmlNonComplianceTask += '</td>';
                            htmlNonComplianceTask += '<td>';
                            htmlNonComplianceTask += msg.Dashboard.lstNonCompliance[i].ncAmount;
                            htmlNonComplianceTask += '</td>';
                            htmlNonComplianceTask += '<td>';
                            htmlNonComplianceTask += msg.Dashboard.lstNonCompliance[i].subType;
                            htmlNonComplianceTask += '</td>';
                            htmlNonComplianceTask += '<td>' + msg.Dashboard.lstNonCompliance[i].transactionDate + '</td>'

                            //htmlNonComplianceTask += '<td>';
                            //htmlNonComplianceTask += msg.Dashboard.lstNonCompliance[i].reportedOn;
                            //htmlNonComplianceTask += '</td>';
                            htmlNonComplianceTask += '<td>';
                            if (msg.Dashboard.lstNonCompliance[i].nonComplianceType == "Broker note required") {
                                htmlNonComplianceTask += '<button type="button" id="btnNcUploadbrokernote_' + msg.Dashboard.lstNonCompliance[i].nonComplianceId + '" class="btn btn-outline dark btn-sm" data-toggle="modal" data-target="#NcUploadBrokerNote" onclick=\"javascript:fnUploadNcBrokernote(\'' + msg.Dashboard.lstNonCompliance[i].TransactionId + '\',\'' + msg.Dashboard.lstNonCompliance[i].nonComplianceId + '\',\'' + msg.Dashboard.lstNonCompliance[i].Relation + '\',\'' + msg.Dashboard.lstNonCompliance[i].RelativeId + '\',\'' + msg.Dashboard.lstNonCompliance[i].subType + '\',\'' + msg.Dashboard.lstNonCompliance[i].ncQuantity + '\',\'' + msg.Dashboard.lstNonCompliance[i].Folio + '\',\'' + msg.Dashboard.lstNonCompliance[i].transactionStartDate + '\',\'' + msg.Dashboard.lstNonCompliance[i].transactionEndDate + '\');\">Upload Broker Note</button>';
                            }

                            else {
                                htmlNonComplianceTask += '<button type="button"  id="btnNcComments_' + msg.Dashboard.lstNonCompliance[i].nonComplianceId + '" class="btn btn-outline dark btn-sm" data-toggle="modal" data-target="#NonComplianceComments" onclick=\"javascript:fnAddNonComplianceComments(\'' + msg.Dashboard.lstNonCompliance[i].TransactionId + '\',\'' + msg.Dashboard.lstNonCompliance[i].nonComplianceId + '\',\'' + msg.Dashboard.lstNonCompliance[i].sName + '\',\'' + msg.Dashboard.lstNonCompliance[i].RelativeName + '\',\'' + msg.Dashboard.lstNonCompliance[i].Relation + '\',\'' + msg.Dashboard.lstNonCompliance[i].panNumber + '\',\'' + msg.Dashboard.lstNonCompliance[i].Folio + '\',\'' + msg.Dashboard.lstNonCompliance[i].nonComplianceType + '\',\'' + msg.Dashboard.lstNonCompliance[i].ncQuantity + '\',\'' + msg.Dashboard.lstNonCompliance[i].ncAmount + '\');\">Add Comments</button>';
                            }
                            htmlNonComplianceTask += '</td>';
                            htmlNonComplianceTask += '</tr>';
                        }
                    }
                    else {
                        $("#NonCompliance_PortletBox").addClass('display-none');
                    }
                }

                $('#tbodyNonComplianceType').html(htmlNonComplianceTask);

                var htmlTradeDetails = '';

                if (msg.Dashboard.transactionHistory !== null) {
                    if (msg.Dashboard.transactionHistory.StatusFl) {
                        if (msg.Dashboard.transactionHistory.TransactionHistoryList !== null) {
                            if (msg.Dashboard.transactionHistory.TransactionHistoryList.length > 0) {
                                CountActionable += parseInt(msg.Dashboard.transactionHistory.TransactionHistoryList.length);
                                for (var i = 0; i < msg.Dashboard.transactionHistory.TransactionHistoryList.length; i++) {
                                    htmlTradeDetails += '<tr>';
                                    htmlTradeDetails += '<td>';
                                    htmlTradeDetails += msg.Dashboard.transactionHistory.TransactionHistoryList[i].userName;
                                    htmlTradeDetails += '</td>';
                                    htmlTradeDetails += '<td>';
                                    htmlTradeDetails += msg.Dashboard.transactionHistory.TransactionHistoryList[i].panNumber;
                                    htmlTradeDetails += '</td>';
                                    htmlTradeDetails += '<td>';
                                    htmlTradeDetails += msg.Dashboard.transactionHistory.TransactionHistoryList[i].folioNumber;
                                    htmlTradeDetails += '</td>'
                                    htmlTradeDetails += '<td>';
                                    htmlTradeDetails += msg.Dashboard.transactionHistory.TransactionHistoryList[i].transactionDate;
                                    htmlTradeDetails += '</td>';
                                    htmlTradeDetails += '<td>';
                                    htmlTradeDetails += msg.Dashboard.transactionHistory.TransactionHistoryList[i].tradeQuantity;
                                    htmlTradeDetails += '</td>';
                                    htmlTradeDetails += '<td>';
                                    htmlTradeDetails += msg.Dashboard.transactionHistory.TransactionHistoryList[i].transactionSubType;
                                    htmlTradeDetails += '</td>';
                                    htmlTradeDetails += '<td>';
                                    htmlTradeDetails += '<a class="btn btn-default" data-target="#transactionHistoryModel" data-toggle="modal" onclick=\"javascript:fnGetTransactionSubTypeMaster(\'' + msg.Dashboard.transactionHistory.TransactionHistoryList[i].transactionId + '\',\'' + msg.Dashboard.transactionHistory.TransactionHistoryList[i].tradeQuantity + '\',\'' + msg.Dashboard.transactionHistory.TransactionHistoryList[i].transactionSubType + '\');\">Verify</a>';
                                    htmlTradeDetails += '</td>';
                                    htmlTradeDetails += '</tr>';
                                }
                            }
                        }
                    }
                }

                $("#tbodyTrade").html(htmlTradeDetails);

                var strTrans = "";
                if (msg.Dashboard.CompliantTransactionHistory !== null) {
                    if (msg.Dashboard.CompliantTransactionHistory.StatusFl) {
                        if (msg.Dashboard.CompliantTransactionHistory.TransactionHistoryList !== null) {
                            if (msg.Dashboard.CompliantTransactionHistory.TransactionHistoryList.length > 0) {
                                CountActionable += parseInt(msg.Dashboard.CompliantTransactionHistory.TransactionHistoryList.length);
                                for (var i = 0; i < msg.Dashboard.CompliantTransactionHistory.TransactionHistoryList.length; i++) {
                                    strTrans += '<tr>';
                                    strTrans += '<td>';
                                    strTrans += msg.Dashboard.CompliantTransactionHistory.TransactionHistoryList[i].userName;
                                    strTrans += '</td>';
                                    strTrans += '<td>';
                                    strTrans += msg.Dashboard.CompliantTransactionHistory.TransactionHistoryList[i].Relation;
                                    strTrans += '</td>';
                                    strTrans += '<td>';
                                    strTrans += msg.Dashboard.CompliantTransactionHistory.TransactionHistoryList[i].panNumber;
                                    strTrans += '</td>';
                                    strTrans += '<td>';
                                    strTrans += msg.Dashboard.CompliantTransactionHistory.TransactionHistoryList[i].folioNumber;
                                    strTrans += '</td>'
                                    strTrans += '<td>';
                                    strTrans += msg.Dashboard.CompliantTransactionHistory.TransactionHistoryList[i].Quantity;
                                    strTrans += '</td>';
                                    strTrans += '<td>';
                                    strTrans += msg.Dashboard.CompliantTransactionHistory.TransactionHistoryList[i].TradeValue;
                                    strTrans += '</td>';
                                    strTrans += '<td>';
                                    strTrans += msg.Dashboard.CompliantTransactionHistory.TransactionHistoryList[i].transactionSubType;
                                    strTrans += '</td>';
                                    strTrans += '<td>';
                                    strTrans += msg.Dashboard.CompliantTransactionHistory.TransactionHistoryList[i].transactionDate;
                                    strTrans += '</td>';
                                    strTrans += '<td>';
                                    strTrans += '<a class="btn btn-default" data-target="#TransComplianceModal" data-toggle="modal" onclick=\"javascript:fnAddTransComplianceComments(\'' + msg.Dashboard.CompliantTransactionHistory.TransactionHistoryList[i].transactionId + '\',\'' + msg.Dashboard.CompliantTransactionHistory.TransactionHistoryList[i].userName + '\',\'' + msg.Dashboard.CompliantTransactionHistory.TransactionHistoryList[i].Relation + '\',\'' + msg.Dashboard.CompliantTransactionHistory.TransactionHistoryList[i].panNumber + '\',\'' + msg.Dashboard.CompliantTransactionHistory.TransactionHistoryList[i].folioNumber + '\',\'' + msg.Dashboard.CompliantTransactionHistory.TransactionHistoryList[i].Quantity + '\',\'' + msg.Dashboard.CompliantTransactionHistory.TransactionHistoryList[i].TradeValue + '\');\">Update Status</a>';
                                    strTrans += '</td>';
                                    strTrans += '</tr>';
                                }
                                $("#ActionableCompliance_PortletBox").removeClass('display-none');
                            }

                        }
                    }
                }
                $("#tbodyCompliance").html(strTrans);

                transactionSubTypeMaster = msg.Dashboard.transactionSubTypeMaster;


                $("#olMyActionableTask").html(str);

                if (CountActionable > 0) {
                    $('#spnMyActionableCount').html('(' + CountActionable + ')');
                }
                //else {
                //    $('#dvMyActionable').hide();
                //}
            }
            else {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    //  alert(msg.Msg);
                }
            }
        },
        error: function (error) {
            $("#Loader").hide();
            if (error.responseText == "Session Expired") {
                alert("Your session is expired. Please login again to continue");
                window.location.href = "../LogOut.aspx";
                return false;
            }
            else {
                alert(error.status + ' ' + error.statusText);
            }
        }
    });
}
function fnGetTransactionSubTypeMaster(transactionId, tradeQuantity, transactionSubType) {
    var htmlStr = '';
    htmlStr += '<input id="txtTransactionIdModal" type="hidden" value="' + transactionId + '"/>';
    htmlStr += '<input id="txtTradeQuantityModal" type="hidden" value="' + tradeQuantity + '"/>';
    htmlStr += '<input id="txtTransactionSubTypeModal" type="hidden" value="' + transactionSubType + '"/>';
    if (transactionSubTypeMaster !== null) {
        if (transactionSubTypeMaster.StatusFl) {
            if (transactionSubTypeMaster.TransactionSubTypeMasterList !== null) {
                if (transactionSubTypeMaster.TransactionSubTypeMasterList.length > 0) {
                    htmlStr += '<table class="table table-striped table-hover table-bordered">';
                    htmlStr += '<tr>';
                    htmlStr += '<th>Category</th>';
                    htmlStr += '<th>Total Quantity</th>';
                    htmlStr += '<th>Total Value</th>';
                    htmlStr += '<th>Transaction Date</th>';
                    htmlStr += '<th>Remarks</th>';
                    htmlStr += '</tr>';
                    for (var i = 0; i < transactionSubTypeMaster.TransactionSubTypeMasterList.length; i++) {
                        if (transactionSubTypeMaster.TransactionSubTypeMasterList[i].type == transactionSubType) {
                            htmlStr += '<tr>';
                            htmlStr += '<td>';
                            htmlStr += transactionSubTypeMaster.TransactionSubTypeMasterList[i].category;
                            htmlStr += '</td>';
                            htmlStr += '<td>';
                            htmlStr += '<input type="number" class="form-control" autocomplete="off" id="txtTradeQuantityBifurcation_' + transactionSubTypeMaster.TransactionSubTypeMasterList[i].transactionSubTypeMasterId + '"/>';
                            htmlStr += '</td>'
                            htmlStr += '<td>';
                            htmlStr += '<input type="number" class="form-control" autocomplete="off" id="txtTradeValueBifurcation_' + transactionSubTypeMaster.TransactionSubTypeMasterList[i].transactionSubTypeMasterId + '"/>';
                            htmlStr += '</td>';
                            htmlStr += '<td>';
                            htmlStr += '<input data-date-format="dd/mm/yyyy" type="text" class="form-control date-picker datepicker_recurring_start" name="trans_date" autocomplete="off" id="txtTradeDateBifurcation_' + transactionSubTypeMaster.TransactionSubTypeMasterList[i].transactionSubTypeMasterId + '"/>';
                            htmlStr += '</td>';
                            htmlStr += '<td>';
                            htmlStr += '<textarea class="form-control" autocomplete="off" id="txtRemarksBifurcation_' + transactionSubTypeMaster.TransactionSubTypeMasterList[i].transactionSubTypeMasterId + '"></textarea>';
                            htmlStr += '</td>';
                            htmlStr += '</tr>';
                        }
                    }
                    htmlStr += '</table>';
                }
            }
        }
    }

    $("#divTradeQuantityBifurcation").html(htmlStr);
}
function fnSaveTradeBifurcation() {
    if (fnValidateTradeBifurcation()) {
        var lstItems = new Array();
        var length = $("#divTradeQuantityBifurcation table tbody").children().length;
        if (length > 1) {
            for (var i = 1; i < length; i++) {
                var childrenElement = $("#divTradeQuantityBifurcation table tbody").children()[i];

                var category = $($(childrenElement).children()[0]).html();

                var type = $("#txtTransactionSubTypeModal").val();

                var childrenTdElementFirst = $(childrenElement).children()[1];
                var tradeQuantity = $($(childrenTdElementFirst).children()[0]).val();

                var childrenTdElementSecond = $(childrenElement).children()[2];
                var tradeValue = $($(childrenTdElementSecond).children()[0]).val();

                var childrenTdElementThird = $(childrenElement).children()[3];
                var transactionDate = $($(childrenTdElementThird).children()[0]).val();

                var childrenTdElementFourth = $(childrenElement).children()[4];
                var remarks = $($(childrenTdElementFourth).children()[0]).val();

                var transactionSubTypeMasterId = $($(childrenTdElementFirst).children()[0]).attr('id').split('_')[1];

                if (tradeQuantity !== null && tradeQuantity !== undefined && tradeQuantity !== "") {
                    var obj = new Object();

                    obj.transactionSubTypeMasterId = transactionSubTypeMasterId;
                    obj.type = type;
                    obj.category = category;
                    obj.tradeQuantity = tradeQuantity;
                    obj.tradeValue = tradeValue;
                    obj.transactionDate = transactionDate;
                    obj.remarks = remarks;

                    lstItems.push(obj);
                }

            }
        }

        $("#Loader").show();
        $.ajax({
            url: uri + "/api/DashboardIT/UpdateTradeBifurcation",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({ transactionHistoryBifurcation: { transactionId: $("#txtTransactionIdModal").val(), lstTransactionBifurcation: lstItems } }),
            success: function (msg) {
                $("#Loader").hide();
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
                    }
                }
            },
            error: function (error) {
                $("#Loader").hide();
                if (error.responseText == "Session Expired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                    return false;
                }
                else {
                    alert(error.status + ' ' + error.statusText);
                }
            }
        });
    }
}
function fnValidateTradeBifurcation() {
    var lengthTrade = $("input[id*=txtTradeQuantityBifurcation_").length;
    var lengthTradeValue = $("input[id*=txtTradeValueBifurcation_").length;
    var totalTradeQuantity = 0;

    if (lengthTrade > 0) {
        for (var i = 0; i < lengthTrade; i++) {
            if ($($("input[id*=txtTradeQuantityBifurcation_")[i]).val() !== '' && $($("input[id*=txtTradeQuantityBifurcation_")[i]).val() !== null && $($("input[id*=txtTradeQuantityBifurcation_")[i]).val() !== undefined) {
                totalTradeQuantity += parseInt($($("input[id*=txtTradeQuantityBifurcation_")[i]).val());
            }
        }
    }

    if (parseInt($("#txtTradeQuantityModal").val()) !== parseInt(totalTradeQuantity)) {
        alert("Total trade quantity bifurcation cannot be lesser or greater than actual trade quantity!");
        return false;
    }

    if (lengthTrade > 0) {
        for (var i = 0; i < lengthTrade; i++) {
            if ($($("input[id*=txtTradeQuantityBifurcation_")[i]).val() !== null && $($("input[id*=txtTradeQuantityBifurcation_")[i]).val() !== undefined && $($("input[id*=txtTradeQuantityBifurcation_")[i]).val() !== "") {
                if ($($("input[id*=txtTradeValueBifurcation_")[i]).val() === "" || $($("input[id*=txtTradeValueBifurcation_")[i]).val() === null || $($("input[id*=txtTradeValueBifurcation_")[i]).val() === undefined) {
                    alert("Trade value cannot be empty corresponding to trade quantity!");
                    return false;
                }
            }
        }
    }

    if (lengthTrade > 0) {
        for (var i = 0; i < lengthTrade; i++) {
            if ($($("input[id*=txtTradeQuantityBifurcation_")[i]).val() !== null && $($("input[id*=txtTradeQuantityBifurcation_")[i]).val() !== undefined && $($("input[id*=txtTradeQuantityBifurcation_")[i]).val() !== "") {
                if ($($("input[id*=txtTradeDateBifurcation_")[i]).val() === "" || $($("input[id*=txtTradeDateBifurcation_")[i]).val() === null || $($("input[id*=txtTradeDateBifurcation_")[i]).val() === undefined) {
                    alert("Date cannot be empty corresponding to trade quantity!");
                    return false;
                }
            }
        }
    }

    if (lengthTrade > 0) {
        for (var i = 0; i < lengthTrade; i++) {
            if ($($("input[id*=txtTradeQuantityBifurcation_")[i]).val() !== null && $($("input[id*=txtTradeQuantityBifurcation_")[i]).val() !== undefined && $($("input[id*=txtTradeQuantityBifurcation_")[i]).val() !== "") {
                if ($($("textarea[id*=txtRemarksBifurcation_")[i]).val() === "" || $($("textarea[id*=txtRemarksBifurcation_")[i]).val() === null || $($("textarea[id*=txtRemarksBifurcation_")[i]).val() === undefined) {
                    alert("Remarks cannot be empty corresponding to trade quantity!");
                    return false;
                }
            }
        }
    }

    return true;
}
function fnSubmitUPSIPolicyApplicable() {
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/SetUserRole";
    $.ajax({
        url: webUrl,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        data: JSON.stringify({ userRole: { ROLE_NM: $("input[name=optradio]:checked").val() } }),
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                alert(msg.Msg);
                $("#modalUPSIPolicyApplicable").modal('hide');
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
function ConvertToDate(dateString) {

    var dateParts = dateString.split("/");

    // month is 0-based, that's why we need dataParts[1] - 1
    var dateObject = new Date(+dateParts[2], dateParts[1] - 1, +dateParts[0]);

    return dateObject;
}
function fnGoToTrainingModule(trainingModuleId, totalNoOfPages, status) {
    var form = $(document.createElement('form'));
    $(form).attr("action", "TrainingModule.aspx");
    $(form).attr("method", "POST");
    $(form).css("display", "none");

    var input_committee_id = $("<input>")

    var input_training_module_id = $("<input>")
        .attr("type", "text")
        .attr("name", "TrainingModuleId")
        .val(trainingModuleId);
    $(form).append($(input_training_module_id));

    var input_total_no_of_pages = $("<input>")
        .attr("type", "text")
        .attr("name", "TotalNoOfPages")
        .val(totalNoOfPages);
    $(form).append($(input_total_no_of_pages));

    var input_user_training_module_status = $("<input>")
        .attr("type", "text")
        .attr("name", "UserTrainingModuleStatus")
        .val(status);
    $(form).append($(input_user_training_module_status));

    form.appendTo(document.body);
    $(form).submit();
}
function fnUploadNcBrokernote(TransactionId, nonComplianceId, Relation, RelativeId, TransactionType, TradeQty, DematAccountNo, startdate, enddate) {
    $('#txtRequestedTransactionDateCnBN').datepicker({
        startDate: FormatDate(startdate, $("input[id*=hdnDateFormat]").val()),
        endDate: FormatDate(enddate, $("input[id*=hdnDateFormat]").val()),
        autoclose: true,
        format: "dd/mm/yyyy",
        clearBtn: true,
        daysOfWeekDisabled: [0, 6]
    }).attr('readonly', 'readonly');

    $("input[id*='txthiddenRelativeId']").val(RelativeId);
    $("input[id*='txtTransId']").val(TransactionId);
    $("input[id*='txtNonComplianceId']").val(nonComplianceId);
    $("#spnforNcBN").html(Relation);
    //$("select[id*='ddlTypeOfTransactionNc']").val(TransactionType);
    $("select[id*='ddlTypeOfTransactionNc'] option:contains('" + TransactionType + "')").attr('selected', true);
    $("#spnTradeQuantity").html(TradeQty);
    $("#spnDematAccountBrokerNote").html(DematAccountNo);
    //$("[id*='']").val();
}
function AddUpdateNonComplianceBrokerNote() {
    if (fnValidateNcBrokerNoteRequestDetails()) {
        if ($("#txtTotalamount").val() == '0') {
            alert("Trade Value cannot be 0.");
        }
        else {
            $("#modalNonComplianceBrokerNoteUploadConfirmation").modal('show');
        }
    }
    else {

    }
}
function fnValidateNcBrokerNoteRequestDetails() {


    if ($('#ddlTypeOfSecurityNc').val() == undefined || $('#ddlTypeOfSecurityNc').val() == null || $('#ddlTypeOfSecurityNc').val().trim() == '' || $('#ddlTypeOfSecurityNc').val().trim() == '0') {
        alert("Please fill all mandatory fields");
        return false;
    }

    if ($('#ddlRestrictedCompaniesNc').val() == undefined || $('#ddlRestrictedCompaniesNc').val() == null || $('#ddlRestrictedCompaniesNc').val().trim() == '' || $('#ddlRestrictedCompaniesNc').val().trim() == '0') {
        alert("Please fill all mandatory fields");
        return false;
    }

    if ($("#ddlTypeOfTransactionNc").val() == undefined || $("#ddlTypeOfTransactionNc").val() == null || $("#ddlTypeOfTransactionNc").val() == "0") {
        alert("Please fill all mandatory fields");
        return false;
    }

    if ($("#ddlProposedTransactionThroughNc").val() == "Off-Market Deal") {
        if ($('#txtareabrokerdetailsNC').val() == null || $('#txtareabrokerdetailsNC').val() == undefined || $('#txtareabrokerdetailsNC').val().trim() == '') {
            alert("Please fill all mandatory fields");
            return false;
        }
    }

    if ($('#txtValuePerShareNc').val() == null || $('#txtValuePerShareNc').val() == undefined || $('#txtValuePerShareNc').val().trim() == '') {
        alert("Please fill all mandatory fields");
        return false;
    }

    if ($('#txtRequestedTransactionDateCnBN').val() == null || $('#txtRequestedTransactionDateCnBN').val() == undefined || $('#txtRequestedTransactionDateCnBN').val().trim() == '') {
        alert("Please fill all mandatory fields");
        return false;
    }


    return true;
}
function SubmitNcBrokerNoteRequestDetails() {
    var TransactionId = $("input[id*='txtTransId']").val();
    var NonComplianceId = $("input[id*='txtNonComplianceId']").val();
    var relativeId = $("input[id*='txthiddenRelativeId']").val();
    var securityType = $('#ddlTypeOfSecurityNc').val();
    var restrictedCompanyId = $('#ddlRestrictedCompaniesNc').val();
    var transactionType = $("select[id*='ddlTypeOfTransactionNc']").val();
    var actualTransactionDate = $('#txtRequestedTransactionDateCnBN').val();
    var tradeQuantity = $('#spnTradeQuantity').html();
    var valuePerShare = $('#txtValuePerShareNc').val();
    var totalAmount = tradeQuantity * valuePerShare;
    var dematAccountID = $("#spnDematAccountBrokerNote").html();

    var proposedTransactionThrough = $('#ddlProposedTransactionThroughNc').val();
    var exchangeTradedOn = $("#ddlExchangeTradedOnNc").val();
    var remarks = $("#txtRemarksNc").val();
    var brokerdetails = $("#txtareabrokerdetailsNC").val();

    $("#Loader").show();
    var test = new FormData();
    test.append("Object", JSON.stringify({ PreClearanceRequestId: 0, relativeId: relativeId, SecurityType: securityType, TradeCompany: restrictedCompanyId, TransactionType: transactionType, BrokerNote: '', ActualTransactionDate: actualTransactionDate, ValuePerShare: valuePerShare, TotalAmount: totalAmount, ActualTradeQuantity: tradeQuantity, DematAccount: dematAccountID, proposedTransactionThrough: proposedTransactionThrough, remarks: remarks, exchangeTradedOn: exchangeTradedOn, TransactionId: TransactionId, NonComplianceId: NonComplianceId, BrokerDetails: brokerdetails }));
    test.append("Files", $("#btnBrokernoteNc").get(0).files[0]);

    $.ajax({
        url: uri + "/api/DashboardIT/SubmitNcBrokerNoteRequestDetails",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        contentType: false,
        //  async: false,
        processData: false,
        data: test,
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl) {
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
                //$("input[id*='preClearanceRequestIdBN']").val(msg.PreClearanceRequest.PreClearanceRequestId);
                //$("input[id*='txtBrokerNoteId']").val(msg.PreClearanceRequest.brokerNoteId);

                //$("select[id*='ddlForms']").empty();
                //if (msg.PreClearanceRequest.lstFormUrl != null) {
                //    var strOption = "";
                //    for (var x = 0; x < msg.PreClearanceRequest.lstFormUrl.length; x++) {
                //        var strValue = msg.PreClearanceRequest.lstFormUrl[x];
                //        strOption += strValue.split("~")[1] + "&";
                //    }
                //    var s = strOption.substr(0, strOption.length - 1);
                //    ddlForms.append(new Option(s, "All"));
                //    fnDisplayNote(null, null, "All");
                //}


                //$("#btnCancelNcBrokerNote").trigger("click");
                //$("#btnOpenForm").trigger("click");
                //// window.location.reload();
                //fnClearNcBrokerNoteRequestDetails();

            }
            else {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    alert(msg.Msg);
                }
            }
        },
        error: function (error) {
            $("#Loader").hide();
            if (error.responseText == "Session Expired") {
                alert("Your session is expired. Please login again to continue");
                window.location.href = "../LogOut.aspx";
                return false;
            }
            else {
                alert(error.status + ' ' + error.statusText);
            }
        }
    });
}
function fnClearNcBrokerNoteRequestDetails() {
    $("#spnforNcBN").html('');
    $('#ddlTypeOfSecurityNc').val('');
    $('#ddlRestrictedCompaniesNc').val('');
    $("select[id*='ddlTypeOfTransactionNc']").val('');
    $("#spnTradeQuantity").html('');
    $("#txtValuePerShareNc").val('');
    $("#txtTotalamountNc").val('');
    $('#spnDematAccountBrokerNote').html('');
    $('#txtRequestedTransactionDateCnBN').datepicker("setDate", "");
    $("#btnBrokernoteNc").val('');
    $("#ddlProposedTransactionThroughNc").val('');
    $('#txtRemarksNc').val('');
    $("#ddlExchangeTradedOnNc").val('');
    $("input[id*='txtTransId']").val();
    $("input[id*='txtNonComplianceId']").val();
}
function fnAddNonComplianceComments(TransactionId, nonComplianceId, UserName, RelativeName, Relation, Pan, Folio, NonComplianceType, TradeQty, TradeVal) {
    $("input[id*='txtTransId']").val(TransactionId);
    $("input[id*='txtNonComplianceId']").val(nonComplianceId);
    var result = "";
    result += '<tr>';
    result += '<td>' + UserName + '</td>';
    result += '<td>' + RelativeName + '</td>';
    result += '<td>' + Relation + '</td>';
    result += '<td>' + Pan + '</td>';
    result += '<td>' + Folio + '</td>';
    result += '<td>' + NonComplianceType + '</td>';
    result += '<td>' + TradeQty + '</td>';
    result += '<td>' + TradeVal + '</td>';
    result += '</tr>';

    $("#tbdNonComplianceRemarks").html(result);
}
function fnAddNcRemarks() {

    var NcRemarks = $("textarea[id*='textareaNcRemarks']").val();
    if (NcRemarks == null || NcRemarks == undefined || NcRemarks == '') {
        $("#lblForNcComments").addClass('text-danger');
        alert("Please enter remarks");
        return false;
    }
    else {
        $("#Loader").show();
        var TransId = $("input[id*='txtTransId']").val();
        var NonComplianceId = $("input[id*='txtNonComplianceId']").val();
        var token = $("#TokenKey").val();
        var webUrl = uri + "/api/DashboardIT/UpdateNonComplianceTask";
        $.ajax({
            url: webUrl,
            type: "POST",
            data: JSON.stringify({
                NonComplianceId: NonComplianceId, TransactionId: TransId, NonComplianceRemarks: NcRemarks
            }),
            async: false,
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            success: function (msg) {
                if (msg.StatusFl == true) {
                    $("#NonComplianceComments").modal('hide');
                    alert("Task updated successfully !");
                    fnClearNcRemarks();
                    fnGetMyActionable();
                }
                else {
                    if (msg.Msg == "SessionExpired") {
                        alert("Your session is expired. Please login again to continue");
                        window.location.href = "../LogOut.aspx";
                    }
                    else {
                        alert(msg.Msg);
                    }
                }
                $("#Loader").hide();
            },
            error: function (response) {
                $("#Loader").hide();
                alert(response.status + ' ' + response.statusText);
            }
        })

    }
}
function fnClearNcRemarks() {
    $("input[id*='txtTransId']").val('');
    $("input[id*='txtNonComplianceId']").val('');
    $("textarea[id*='textareaNcRemarks']").val('');
    $("#lblForNcComments").removeClass('text-danger');
}
function GetQuarter() {
    var d = new Date();
    var m = Math.floor(d.getMonth() / 3 + 1);//Math.floor(d.getMonth() / 3) + 2;

    var QMonth = "";

    if (m == 1) {
        QMonth = "(Jan-Mar)";
    }
    else if (m == 2) {
        QMonth = "(Apr-Jun)";
    }

    else if (m == 3) {
        QMonth = "(Jul-Sep)";
    }
    else if (m == 4) {
        QMonth = "(Oct-Dec)";
    }

    $("#spnQuarter").text(QMonth);
}
function convertToDateTime(date) {
    date = new Date(date)
    return date.getDate() + '/' + (date.getMonth() + 1) + '/' + date.getFullYear();
}

function getCurrentFinancialYear() {
    var fiscalyear = "";
    var today = new Date();
    if ((today.getMonth() + 1) <= 3) {
        fiscalyear = (today.getFullYear() - 1) + "-" + today.getFullYear()
    } else {
        fiscalyear = today.getFullYear() + "-" + (today.getFullYear() + 1)
    }
    return fiscalyear
}
//function FormatDate(date) {
//    date = new Date(date)
//    return date.getDate() + '/' + (date.getMonth() + 1) + '/' + date.getFullYear();
//}
function fnAddTransComplianceComments(TransactionId, UserName, Relation, Pan, Folio, TradeQty, TradeVal) {
    $("input[id*='txtTransId']").val(TransactionId);
    //$("input[id*='txtNonComplianceId']").val(nonComplianceId);
    var result = "";
    result += '<tr>';
    result += '<td>' + UserName + '</td>';
    result += '<td>' + Relation + '</td>';
    result += '<td>' + Pan + '</td>';
    result += '<td>' + Folio + '</td>';
    result += '<td>' + TradeQty + '</td>';
    result += '<td>' + TradeVal + '</td>';
    result += '</tr>';

    $("#tbdTransComplianceRemarks").html(result);
}
function fnAddTransStatus() {

    var TransRemarks = $("textarea[id*='textareaTransRemarks']").val();
    var TransStatus = $("select[id*='ddlTransStatus']").val();

    if (TransStatus == null || TransStatus == undefined || TransStatus == '') {
        $("#lblTransStatus").addClass('text-danger');
        alert("Please select status");
        return false;
    }
    if (TransRemarks == null || TransRemarks == undefined || TransRemarks == '') {
        $("#lblForTransComments").addClass('text-danger');
        alert("Please enter remarks");
        return false;
    }
    else {
        $("#Loader").show();
        var TransId = $("input[id*='txtTransId']").val();
        var token = $("#TokenKey").val();
        //alert(TransId);
        var webUrl = uri + "/api/DashboardIT/UpdateTransactionHistory";
        $.ajax({
            url: webUrl,
            type: "POST",
            data: JSON.stringify({
                TransactionId: TransId, remarks: TransRemarks, Status: TransStatus
            }),
            async: false,
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            success: function (msg) {
                if (msg.StatusFl == true) {
                    $("#TransComplianceModal").modal('hide');
                    alert("Task updated successfully !");
                    fnClearTransStatus();
                    fnGetMyActionable();
                }
                else {
                    if (msg.Msg == "SessionExpired") {
                        alert("Your session is expired. Please login again to continue");
                        window.location.href = "../LogOut.aspx";
                    }
                    else {
                        alert(msg.Msg);
                    }
                }
                $("#Loader").hide();
            },
            error: function (response) {
                $("#Loader").hide();
                alert(response.status + ' ' + response.statusText);
            }
        })

    }
}
function fnClearTransStatus() {
    $("input[id*='txtTransId']").val('');
    $("textarea[id*='textareaTransRemarks']").val('');
    $("select[id*='ddlTransStatus']").val('');
    $("#lblTransStatus").removeClass('text-danger');
    $("#lblForTransComments").removeClass('text-danger');
}
function fnGetOpenDisclosureRequest() {


    $("#Loader").show();
    var webUrl = uri + "/api/DashboardIT/GetOpenDisclosureRequest";
    $.ajax({
        url: webUrl,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        data: JSON.stringify({ Status: "Approved" }),
        success: function (msg) {
            $("#Loader").hide();
            //debugger;
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                var result = "";
                lstDisclosureRequest = msg.lstActioanble;
                for (var i = 0; i < lstDisclosureRequest.length; i++) {
                    result += '<tr id="tr_' + lstDisclosureRequest[i].TaskId + '">';
                    result += '<td>' + lstDisclosureRequest[i].UserNm + '</td>';
                    result += '<td>' + lstDisclosureRequest[i].TaskCreatedOn + '</td>';
                    result += '<td>' + lstDisclosureRequest[i].TaskType + '</td>';

                    result += '<td>';
                    result += '<a title="Approve Disclosure Request" onclick="javascript:fnRequestAction(\'' + lstDisclosureRequest[i].TaskId + '\',\'Approved\');">';
                    result += '<i class="fa fa-thumbs-o-up"></i></a>&nbsp;&nbsp;&nbsp;';
                    result += '<a title="Reject Disclosure Request" onclick="javascript:fnRequestAction(\'' + lstDisclosureRequest[i].TaskId + '\',\'Rejected\');">';
                    result += '<i class="fa fa-thumbs-o-down"></i></a>&nbsp;&nbsp;&nbsp;';
                    result += '</td>';

                    result += '</tr>';
                }
                //var table = $('#tbl-preclearance-setup').DataTable();
                //table.destroy();
                $("#tbodyDeclarationApproval").html(result);
                $("#dvDeclarationActionable").show();
                //initializeDataTable();
            }
            else {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else if (msg.Msg.toLowerCase() == "no data found!") {
                    $("#tbodyDeclarationApproval").html('');
                    $("#dvDeclarationActionable").hide();
                    $("#dvActionableBN").hide();
                }
                else {
                    $("#dvActionableBN").hide();
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