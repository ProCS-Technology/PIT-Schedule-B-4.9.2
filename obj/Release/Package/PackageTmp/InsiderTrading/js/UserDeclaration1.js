var isEdit = false;
var editableElement = null;
var isEditDemat = false;
var editableElementDemat = null;
var isEditInitialHoldingDeclarationDetail = false;
var isEditInitialHoldingDeclarationDetailPhysical = false;
var editableElementInitialDeclarationDetail = null;
var editableElementInitialDeclarationDetailPhysical = null;
var dematAccountNumberGlobal = '';
var deleteRelativeDetailElement = null;
var deleteDematDetailElement = null;
var deleteInitialHoldingDetailElement = null;
var deleteInitialHoldingDetailElementPhysical = null;
var arrFields = new Array();
var relativeDetails = new Array();
var residentFlag = true;
var enableabtnSubmitDeclaration = false;

jQuery(document).ready(function () {

    $("#Loader").hide();
    //FormWizard.init();
    fnGetTransactionalInfo();
    GetFormInfo('User Declaration');
    getLoggedInUserInformation();
    var txtRelativeEmail = $("input[id*='txtRelativeEmail']").val();
    var txtRelativeAddress = $("input[id*='txtRelativeAddress']").val();


    if (txtRelativeEmail.toUpperCase() == "TRUE") {
        $("span[id*='spanRelativeEmail']").show();
    }
    else {
        $("span[id*='spanRelativeEmail']").hide();
    }

    if (txtRelativeAddress.toUpperCase() == "TRUE") {
        $("span[id*='spanRelativeAddress']").show();
    }
    else {
        $("span[id*='spanRelativeAddress']").hide();
    }

    if ($("input[id*=txtCompanyName]").val() == "Spencer Retail Ltd.") {
        $("#divSubCategory").hide();
    }

    $("select[id*=ddlResidentType]").on('change', function () {
        if ($(this).val() == 'NRI') {
            residentFlag = false;
            $("select[id*=country]").prop('disabled', false);
            $("select[id*=country]").val('');
            $("#dvEsopOrEquity").hide();
            $("#dvApplicationPendingForPan").hide();
            $("input[id*=txtPermanentAccountNumber]").prop('disabled', false);
            $("input[id*=txtPermanentAccountNumber]").val('');
            $("input[id*='txtPincodeNumber']").attr('maxlength', '10');
            $("input[id*=txtPincodeNumber]").val('');
            for (var x = 0; x < arrFields.length; x++) {
                for (y = 0; y < arrFields[x].fields.length; y++) {
                    var obj = arrFields[x].fields[y];
                    if (obj.DivNm == "dvIdentificationType" || obj.DivNm == "dvIdentification") {
                        obj.RequiredFl = 'N';
                        obj.EditFl = 'N';
                        $("input[id*='txtIdentificationNumber']").val('');
                        $("select[id*='ddlIdentificationType']").prop('disabled', true);
                        $("input[id*='txtIdentificationNumber']").prop('disabled', true);
                        $("#spnIdentificationType").hide();
                        $("#spnIdentification").hide();
                    }
                }
            }
        }
        else if ($(this).val() == 'FOREIGN_NATIONAL') {
            residentFlag = false;
            $("select[id*=country]").prop('disabled', false);
            $("select[id*=country]").val('');
            $("#dvEsopOrEquity").show();
            $("#dvApplicationPendingForPan").show();
            $("#txtHoldsEsopOrEquity").prop("checked", true);
            $("#txtNotHoldsEsopOrEquity").prop("checked", false);
            $("#txtApplicationPendingForPAN").prop("checked", true);
            $("#txtApplicationNotPendingForPAN").prop("checked", false);
            $("input[id*=txtPermanentAccountNumber]").prop('disabled', true);
            $("input[id*=txtPermanentAccountNumber]").val('NOT APPLICABLE');
            $("input[id*='txtPincodeNumber']").attr('maxlength', '10');
            $("input[id*=txtPincodeNumber]").val('');
            for (var x = 0; x < arrFields.length; x++) {
                for (y = 0; y < arrFields[x].fields.length; y++) {
                    var obj = arrFields[x].fields[y];
                    if (obj.DivNm == "dvIdentificationType" || obj.DivNm == "dvIdentification") {
                        obj.RequiredFl = 'Y';
                        obj.EditFl = 'Y';
                        $("input[id*='txtIdentificationNumber']").val('');
                        $("select[id*='ddlIdentificationType']").prop('disabled', false);
                        $("input[id*='txtIdentificationNumber']").prop('disabled', false);
                        $("#spnIdentificationType").show();
                        $("#spnIdentification").show();
                    }
                }
            }
        }
        else if ($(this).val() == 'INDIAN_RESIDENT') {
            residentFlag = true;
            $("select[id*=country]").prop('disabled', true);
            $("select[id*=country]").val('IN');
            $("#dvEsopOrEquity").hide();
            $("#dvApplicationPendingForPan").hide();
            $("input[id*=txtPermanentAccountNumber]").prop('disabled', false);
            if ($("input[id*=txtPermanentAccountNumber]").val() == 'NOT APPLICABLE') {
                $("input[id*=txtPermanentAccountNumber]").val('');
            }
            $("input[id*=txtPincodeNumber]").addClass('number');
            $("input[id*='txtPincodeNumber']").attr('maxlength', '6');
            $("input[id*=txtPincodeNumber]").val('');
            for (var x = 0; x < arrFields.length; x++) {
                for (y = 0; y < arrFields[x].fields.length; y++) {
                    var obj = arrFields[x].fields[y];
                    if (obj.DivNm == "dvIdentificationType" || obj.DivNm == "dvIdentification") {
                        obj.RequiredFl = 'N';
                        obj.EditFl = 'N';
                        $("select[id*='ddlIdentificationType']").val('');
                        $("input[id*='txtIdentificationNumber']").val('');
                        $("select[id*='ddlIdentificationType']").prop('disabled', true);
                        $("input[id*='txtIdentificationNumber']").prop('disabled', true);
                        $("#spnIdentificationType").hide();
                        $("#spnIdentification").hide();
                    }
                }
            }
        }
    })
    $("select[id*=ddlCategory]").on('change', function () {
        getAllSubCategories();
    })
    if ($("input[id*='txtRole']").val().toUpperCase() == 'DIRECTOR' || $("input[id*='txtRole']").val().toUpperCase() == 'PROMOTER') {
        for (var x = 0; x < arrFields.length; x++) {
            for (y = 0; y < arrFields[x].fields.length; y++) {
                var obj = arrFields[x].fields[y];
                if (obj.DivNm == "dvDin") {
                    obj.RequiredFl = 'Y';
                    obj.EditFl = 'Y';
                    $("input[id*='txtDinNumber']").prop('disabled', false);
                    $("#spnDin").show();
                }
            }
        }
    }
    else {
        for (var x = 0; x < arrFields.length; x++) {
            for (y = 0; y < arrFields[x].fields.length; y++) {
                var obj = arrFields[x].fields[y];
                if (obj.DivNm == "dvDin") {
                    obj.RequiredFl = 'N';
                    obj.EditFl = 'N';
                    $("input[id*='txtDinNumber']").prop('disabled', true);
                    $("#spnDin").hide();
                }
            }
        }
    }

    $("#ddlFor").on('change', function () {
        if ($(this).val() !== undefined && $(this).val() !== null && $(this).val().trim() !== "") {
            if ($(this).val() == "0") {
                getPersonalInformationById();
                getAllDematAccountsOfRelative($(this).val());
                $("#txtTradingMemId").val("");
            }
            else {
                getRelativeInformationById($(this).val());
                getAllDematAccountsOfRelative($(this).val());
                $("#txtTradingMemId").val("");
            }
        }
    })

    $("#ddlForPhysical").on('change', function () {
        if ($(this).val() !== undefined && $(this).val() !== null && $(this).val().trim() !== "") {
            if ($(this).val() == "0") {
                getPersonalInformationById();
                getAllDematAccountsOfRelative($(this).val());
            }
            else {
                getRelativeInformationById($(this).val());
                getAllDematAccountsOfRelative($(this).val());
            }
        }
    })
    $("#ddlDematAccNo").on('change', function () {
        if ($(this).val() !== undefined && $(this).val() !== null && $(this).val().trim() !== "") {
            getDematAccountInfoById($(this).val());
        }
        else {
            $("#txtTradingMemId").val('');
        }
    })
    $('input[name="holdsEsopOrEquity"]').on('change', function () {
        if ($('input[name="holdsEsopOrEquity"]:checked').val() === 'Yes') {
            $("#dvApplicationPendingForPan").show();
            $("input[id*=txtPermanentAccountNumber]").prop('disabled', false);
            $("input[id*=txtPermanentAccountNumber]").val('');
            $("#txtApplicationPendingForPAN").prop("checked", false);
            $("#txtApplicationNotPendingForPAN").prop("checked", true);
            for (var x = 0; x < arrFields.length; x++) {
                for (y = 0; y < arrFields[x].fields.length; y++) {
                    var obj = arrFields[x].fields[y];
                    if (obj.DivNm == "dvIdentificationType" || obj.DivNm == "dvIdentification") {
                        obj.RequiredFl = 'N';
                        obj.EditFl = 'N';
                        $("input[id*='txtIdentificationNumber']").val('');
                        $("select[id*='ddlIdentificationType']").prop('disabled', true);
                        $("input[id*='txtIdentificationNumber']").prop('disabled', true);
                        $("#spnIdentificationType").hide();
                        $("#spnIdentification").hide();
                    }
                }
            }
        }
        else {
            $("#dvApplicationPendingForPan").hide();
            $("input[id*=txtPermanentAccountNumber]").prop('disabled', true);
            $("input[id*=txtPermanentAccountNumber]").val('NOT APPLICABLE');
            $("#txtApplicationPendingForPAN").prop("checked", true);
            $("#txtApplicationNotPendingForPAN").prop("checked", false);
            for (var x = 0; x < arrFields.length; x++) {
                for (y = 0; y < arrFields[x].fields.length; y++) {
                    var obj = arrFields[x].fields[y];
                    if (obj.DivNm == "dvIdentificationType" || obj.DivNm == "dvIdentification") {
                        obj.RequiredFl = 'Y';
                        obj.EditFl = 'Y';
                        $("input[id*='txtIdentificationNumber']").val('');
                        $("select[id*='ddlIdentificationType']").prop('disabled', false);
                        $("input[id*='txtIdentificationNumber']").prop('disabled', false);
                        $("#spnIdentificationType").show();
                        $("#spnIdentification").show();
                    }
                }
            }
        }
    })
    $('input[name="ApplicationPending"]').on('change', function () {
        if ($('input[name="ApplicationPending"]:checked').val() === 'Yes') {

            $("input[id*=txtPermanentAccountNumber]").prop('disabled', true);
            $("input[id*=txtPermanentAccountNumber]").val('NOT APPLICABLE');
            $("#txtApplicationPendingForPAN").prop("checked", true);
            $("#txtApplicationNotPendingForPAN").prop("checked", false);
            for (var x = 0; x < arrFields.length; x++) {
                for (y = 0; y < arrFields[x].fields.length; y++) {
                    var obj = arrFields[x].fields[y];
                    if (obj.DivNm == "dvIdentificationType" || obj.DivNm == "dvIdentification") {
                        obj.RequiredFl = 'Y';
                        obj.EditFl = 'Y';
                        $("input[id*='txtIdentificationNumber']").val('');
                        $("select[id*='ddlIdentificationType']").prop('disabled', false);
                        $("input[id*='txtIdentificationNumber']").prop('disabled', false);
                        $("#spnIdentificationType").show();
                        $("#spnIdentification").show();
                    }
                }
            }

        }
        else {
            $("input[id*=txtPermanentAccountNumber]").prop('disabled', false);
            $("input[id*=txtPermanentAccountNumber]").val('');
            $("#txtApplicationPendingForPAN").prop("checked", false);
            $("#txtApplicationNotPendingForPAN").prop("checked", true);
            for (var x = 0; x < arrFields.length; x++) {
                for (y = 0; y < arrFields[x].fields.length; y++) {
                    var obj = arrFields[x].fields[y];
                    if (obj.DivNm == "dvIdentificationType" || obj.DivNm == "dvIdentification") {
                        obj.RequiredFl = 'N';
                        obj.EditFl = 'N';
                        $("input[id*='txtIdentificationNumber']").val('');
                        $("select[id*='ddlIdentificationType']").prop('disabled', true);
                        $("input[id*='txtIdentificationNumber']").prop('disabled', true);
                        $("#spnIdentificationType").hide();
                        $("#spnIdentification").hide();
                    }
                }
            }

        }
    })
    fnGetApprover();
    fnGetPolicy();
    $("#inAcceptTermsAndConditions").on("click", function () {
        if ($(this).prop("checked") && enableabtnSubmitDeclaration == true) {
            $("#aSubmitYourDeclaration").attr("disabled", false);
            $("#aSubmitYourDeclaration").css("pointer-events", "auto");
            $("#aSubmitYourDeclaration").css({ "color": "#FFF", "background-color": "green", "border-color": "green" });
        }
        else {
            $("#aSubmitYourDeclaration").attr("disabled", true);
            $("#aSubmitYourDeclaration").css("pointer-events", "none");
            $("#aSubmitYourDeclaration").css({ "color": "#666", "background-color": "#e1e5ec", "border-color": "#e1e5ec" });
        }
    })
    $("#txtDepositoryParticipantId").on('focusout', function () {
        $("#txtDematAccountNumber").val($("#txtDepositoryParticipantId").val().trim() + $("#txtClientId").val().trim());
    });
    $("#txtClientId").on('focusout', function () {
        $("#txtDematAccountNumber").val($("#txtDepositoryParticipantId").val().trim() + $("#txtClientId").val().trim());
    })

    $("#ddlDematAccountDetailsFor").on('change', function () {
        if ($(this).val() == "-1") {
            $("#ddlDepositoryName").val("NotApplicable");
            $("#txtDepositoryParticipantName").val("Not Applicable");
            $("#txtDepositoryParticipantId").val("0");
            $("#txtClientId").val("0");
            $("#txtDematAccountNumber").val("Not Applicable");
            $("#txtTradingMemberId").val("Not Applicable");
            $("#ddlStatusDemat").val("INACTIVE");
            $("#ddlDepositoryName").attr("disabled", "disabled");
            $("#txtDepositoryParticipantName").attr("disabled", "disabled");
            $("#txtDepositoryParticipantId").attr("disabled", "disabled");
            $("#txtClientId").attr("disabled", "disabled");
            $("#txtTradingMemberId").attr("disabled", "disabled");
            $("#ddlStatusDemat").val("NotApplicable");
            $("#ddlStatusDemat").attr("disabled", "disabled");
        }
        else {
            $("#ddlDepositoryName").val("");
            $("#txtDepositoryParticipantName").val("");
            $("#txtDepositoryParticipantId").val("");
            $("#txtClientId").val("");
            $("#txtDematAccountNumber").val("");
            $("#txtTradingMemberId").val("");
            $("#ddlStatusDemat").val("");
            $("#ddlDepositoryName").prop("disabled", false);
            $("#txtDepositoryParticipantName").prop("disabled", false);
            $("#txtDepositoryParticipantId").prop("disabled", false);
            $("#txtClientId").prop("disabled", false);
            $("#txtTradingMemberId").prop("disabled", false);
            $("#ddlStatusDemat").prop("disabled", false);
        }
    })
    $("select[id*=ddlRestrictedCompaniesX]").on('change', function () {
        //alert("Company Selected=" + $(this).val());
        if ($(this).val() == "0") {
            $("select[id*=ddlSecurityType]").val("0");
            $("#ddlFor").html('<option value=""></option><option value="-1">Not Applicable</option>');
            $("#ddlFor").val("-1");
            $("#txtPan").val("Not Applicable");
            $("#ddlDematAccNo").html('<option value=""></option><option value="0">Not Applicable</option>');
            $("#ddlDematAccNo").val("0");
            $("#txtTradingMemId").val("Not Applicable");
            $("#txtNumberOfSecurities").val("0");
            $("select[id*=ddlSecurityType]").attr("disabled", "disabled");
            $("#ddlFor").attr("disabled", "disabled");
            $("#ddlDematAccNo").attr("disabled", "disabled");
            $("#txtNumberOfSecurities").attr("disabled", "disabled");
        }
        else {
            $("select[id*=ddlSecurityType]").val("");
            $("#ddlFor").val("");
            $("#txtPan").val("");
            $("#ddlDematAccNo").html('');
            $("#ddlDematAccNo").val("");
            $("#txtTradingMemId").val("");
            $("#txtNumberOfSecurities").val("");
            $("select[id*=ddlSecurityType]").prop("disabled", false);
            $("#ddlFor").prop("disabled", false);
            $("#ddlDematAccNo").prop("disabled", false);
            $("#txtNumberOfSecurities").prop("disabled", false);

            var result = "";
            result += "<option value=''></option>";
            //result += "<option value='0'>Self</option>";
            for (var i = 0; i < relativeDetails.length; i++) {
                if (relativeDetails[i].relativeName !== "Not Applicable") {
                    result += "<option value='" + relativeDetails[i].ID + "'>" + relativeDetails[i].relativeName + "</option>";
                }
            }
            $("#ddlFor").html(result);
        }
    })
    $("#ddlDepositoryName").on('change', function () {
        if ($(this).val() == "NSDL") {
            $('#spNsdlLabel').html("IN");
            $('#spNsdlDematLabel').html("IN");
            $("#txtDepositoryParticipantName").val("");
            $("#txtDepositoryParticipantId").val("");
            $("input[id*='txtDepositoryParticipantId']").attr('maxlength', '6');
            $("#txtClientId").val("");
            $("#txtDematAccountNumber").val("");
            $("#txtTradingMemberId").val("");
            $("#ddlStatusDemat").val("");
            $("#txtDepositoryParticipantName").prop("disabled", false);
            $("#txtDepositoryParticipantId").prop("disabled", false);
            $("#txtClientId").prop("disabled", false);
            $("#txtTradingMemberId").prop("disabled", false);
            $("#ddlStatusDemat").prop("disabled", false);
        }
        else if ($(this).val() == "NotApplicable") {
            $("#txtDepositoryParticipantName").val("Not Applicable");
            $("#txtDepositoryParticipantId").val("0");
            $("#txtClientId").val("0");
            $("#txtDematAccountNumber").val("Not Applicable");
            $("#txtTradingMemberId").val("Not Applicable");
            $("#ddlStatusDemat").val("NotApplicable");
            $("#txtDepositoryParticipantName").attr("disabled", "disabled");
            $("#txtDepositoryParticipantId").attr("disabled", "disabled");
            $("#txtClientId").attr("disabled", "disabled");
            $("#txtTradingMemberId").attr("disabled", "disabled");
            $("#ddlStatusDemat").val("INACTIVE");
            $("#ddlStatusDemat").attr("disabled", "disabled");
        }
        else {
            $('#spNsdlLabel').html("");
            $('#spNsdlDematLabel').html("");
            $("#txtDepositoryParticipantName").val("");
            $("#txtDepositoryParticipantId").val("");
            $("input[id*='txtDepositoryParticipantId']").attr('maxlength', '8');
            $("#txtClientId").val("");
            $("#txtDematAccountNumber").val("");
            $("#txtTradingMemberId").val("");
            $("#ddlStatusDemat").val("");
            $("#txtDepositoryParticipantName").prop("disabled", false);
            $("#txtDepositoryParticipantId").prop("disabled", false);
            $("#txtClientId").prop("disabled", false);
            $("#txtTradingMemberId").prop("disabled", false);
            $("#ddlStatusDemat").prop("disabled", false);
        }
    })
    $("#tbl-Relative-setup").on('change', 'select[id*=ddlStatusRelation]', function (event) {
        var $cntrl = $(this).closest('tr');
        var cntrlStatus = $($($cntrl).closest('tr').children()[3]).find("select[id*=ddlStatusRelation]");
        var cntrlName = $($($cntrl).closest('tr').children()[4]).find("input[id*=txtName]");
        var cntrlEmail = $($($cntrl).closest('tr').children()[5]).find("input[id*=txtEmail]");
        var cntrlIdentype = $($($cntrl).closest('tr').children()[6]).find("select[id*=ddlIdentificationTypeRelation]");
        var cntrlIdenNo = $($($cntrl).closest('tr').children()[7]).find("input[id*=txtIdentificationNumberRelation]");
        var cntrlAddress = $($($cntrl).closest('tr').children()[8]).find("input[id*=txtAddressRelation]");
        var cntrlPhone = $($($cntrl).closest('tr').children()[9]).find("input[id*=txtPhoneRelation]");
        var cntrlIsDesignated = $($($cntrl).closest('tr').children()[11]).find("input[id*=txtdpPan]");
        var cntrladdbtn = $($($cntrl).closest('tr').children()[12]).find("span[id*=spnaddrowbtn]");
        //var cntrldeletebtn = $($($cntrl).closest('tr').children()[12]).find("span[id*=spnaddrowbtn]");

        if ($(cntrlStatus).val() == "NotApplicable") {
            $(cntrlName).val("Not Applicable");
            $(cntrlEmail).val("Not Applicable");
            $(cntrlIdentype).val("NotApplicable");
            $(cntrlIdenNo).val("Not Applicable");
            $(cntrlAddress).val("Not Applicable");
            $(cntrlPhone).val("0");
            $(cntrlIsDesignated).prop("checked", false);
            $(cntrlIsDesignated).prop("disabled", "disabled");
            $(cntrlName).attr("disabled", "disabled");
            $(cntrlEmail).attr("disabled", "disabled");
            $(cntrlIdentype).attr("disabled", "disabled");
            $(cntrlIdenNo).attr("disabled", "disabled");
            $(cntrlAddress).attr("disabled", "disabled");
            $(cntrlPhone).attr("disabled", "disabled");
            $(cntrladdbtn).hide();
        }
        else {
            $(cntrlName).val("");
            $(cntrlEmail).val("");
            $(cntrlIdentype).val("");
            $(cntrlIdenNo).val("");
            $(cntrlAddress).val("");
            $(cntrlPhone).val("");
            $(cntrlIsDesignated).removeAttr("disabled", "disabled");
            $(cntrlName).removeAttr("disabled", "disabled");
            $(cntrlEmail).removeAttr("disabled", "disabled");
            $(cntrlIdentype).removeAttr("disabled", "disabled");
            $(cntrlIdenNo).removeAttr("disabled", "disabled");
            $(cntrlAddress).removeAttr("disabled", "disabled");
            $(cntrlPhone).removeAttr("disabled", "disabled");
            $(cntrladdbtn).removeAttr("disabled", "disabled");
            $(cntrladdbtn).show();
        }
    }).trigger('change');
    $("#tbl-Relative-setup").on('change', 'select[id*=ddlIdentificationTypeRelation]', function (event) {
        var $cntrl = $(this).closest('tr');
        var cntrlIdentype = $($($cntrl).closest('tr').children()[6]).find("select[id*=ddlIdentificationTypeRelation]");
        var cntrlIdenNo = $($($cntrl).closest('tr').children()[7]).find("input[id*=txtIdentificationNumberRelation]");
        var cntrlIsDesignated = $($($cntrl).closest('tr').children()[11]).find("input[id*=txtdpPan]");

        if ($(cntrlIdentype).val() == "NotApplicable") {
            $(cntrlIdenNo).val("Not Applicable");
            $(cntrlIdenNo).attr("disabled", "disabled");
            $(cntrlIsDesignated).prop("checked", false);
            $(cntrlIsDesignated).prop("disabled", "disabled");

        }
        else {
            $(cntrlIdenNo).val("");
            $(cntrlIdenNo).removeAttr("disabled", "disabled");
            $(cntrlIsDesignated).removeAttr("disabled", "disabled");
        }
    })

    $(".number").on("keypress keyup blur", function (e) {

        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
            return false;
        }
    });
});
function getDematAccountInfoById(dematAccountId) {
    $("#Loader").show();
    var webUrl = uri + "/api/DematAccount/DematAccountInfo";
    $.ajax({
        url: webUrl,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        data: JSON.stringify({
            ID: dematAccountId
        }),
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                $("#txtTradingMemId").val(msg.DematAccount.tradingMemberId);
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
function getRelativeInformationById(relativeId) {
    $("#Loader").show();
    var webUrl = uri + "/api/Relative/GetRelativeDetail";
    $.ajax({
        url: webUrl,
        type: "POST",
        async: false,
        data: JSON.stringify({
            ID: relativeId
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                $("#txtPan").val(msg.Relative.panNumber);
                $("#txtPanPhysical").val(msg.Relative.panNumber);
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
function getAllDematAccountsOfRelative(relativeId) {
    $("#Loader").show();
    var webUrl = uri + "/api/DematAccount/GetDematAccountListByRelativeId";
    $.ajax({
        url: webUrl,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        data: JSON.stringify({
            ID: relativeId
        }),
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                var result = "";
                result += "<option value=''></option>";
                if (msg.Relative.lstDematAccount !== null) {
                    for (var i = 0; i < msg.Relative.lstDematAccount.length; i++) {
                        result += "<option value='" + msg.Relative.lstDematAccount[i].ID + "'>" + msg.Relative.lstDematAccount[i].accountNo + "</option>";
                    }
                }

                $("#ddlDematAccNo").html(result);
                $("#ddlPhysicalDematAccNo").html(result);
            }
            else {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    alert(msg.Msg);
                    $("#ddlDematAccNo").html('');
                    $("#ddlPhysicalDematAccNo").html('');
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
function getRelativeInformationById(relativeId) {
    $("#Loader").show();
    var webUrl = uri + "/api/Relative/GetRelativeDetail";
    $.ajax({
        url: webUrl,
        type: "POST",
        async: false,
        data: JSON.stringify({
            ID: relativeId
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                $("#txtPan").val(msg.Relative.panNumber);
                $("#txtPanPhysical").val(msg.Relative.panNumber);
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
function getPersonalInformationById() {
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
                $("#txtPan").val(msg.User.panNumber);
                $("#txtPanPhysical").val(msg.User.panNumber);
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
        error: function (response) {
            $("#Loader").hide();
            alert(response.status + ' ' + response.statusText);
        }
    })
}
function GetFormInfo(module) {
    $("#Loader").show();
    var webUrl = uri + "/api/Transaction/GetFormDetails?moduleNm=" + module;
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
                for (var x = 0; x < msg.Modules.length; x++) {
                    arrFields.push(msg.Modules[x]);
                }

                for (var x = 0; x < arrFields.length; x++) {
                    for (var y = 0; y < arrFields[x].fields.length; y++) {
                        var obj = arrFields[x].fields[y];
                        if (obj.EditFl == 'N') {
                            if (obj.CntrlType == 'TextBox') {
                                $("input[id*=" + obj.ControlId + "]").attr("disabled", "disabled");
                            }
                            else if (obj.CntrlType == 'TextArea') {
                                $("textarea[id*=" + obj.ControlId + "]").attr("disabled", "disabled");
                            }
                            else if (obj.CntrlType == 'Dropdown') {
                                $("select[id*=" + obj.ControlId + "]").attr("disabled", "disabled");
                            }
                        }
                        if (obj.RequiredFl == 'N') {
                            var string = obj.DivNm;
                            string = string.replace(/^.{2}/g, 'spn');
                            $("#" + string).hide();
                        }
                        if (obj.DisplayFl == 'N') {
                            if (obj.RequiredFl == "Y") {
                                if (obj.FormatType == 'Date') {
                                    if (obj.CntrlType == 'TextBox') {
                                        $("input[id*=" + obj.ControlId + "]").val('01/01/1900');
                                    }
                                    else {
                                        $("input[id*=" + obj.ControlId + "]").val($("input[id*=" + obj.ControlId + "] option:first").val());
                                    }
                                }
                                else {
                                    if (obj.CntrlType == 'TextBox') {
                                        $("input[id*=" + obj.ControlId + "]").val('N/A');
                                    }
                                    else if (obj.CntrlType == 'TextArea') {
                                        $("textarea[id*=" + obj.ControlId + "]").val('N/A');
                                    }
                                    else {
                                        $("input[id*=" + obj.ControlId + "]").val($("input[id*=" + obj.ControlId + "] option:first").val());
                                    }
                                }
                            }
                            $("#" + obj.DivNm).hide();
                        }
                        else {
                            $("#" + obj.DivNm).show();
                        }
                    }
                }

                if (module == 'User Declaration') {
                    if ($("select[id*=ddlResidentType]").val() == 'NRI') {
                        $("select[id*=country]").prop('disabled', false);
                        $("#spnIdentificationType").hide();
                        $("#spnIdentification").hide();
                        residentFlag = false;
                    }
                    else if ($("select[id*=ddlResidentType]").val() == 'INDIAN_RESIDENT') {
                        $("select[id*=country]").prop('disabled', true);
                        $("select[id*=country]").val('IN');
                        $("#spnIdentificationType").hide();
                        $("#spnIdentification").hide();
                        residentFlag = true;
                    }
                    else if ($("select[id*=ddlResidentType]").val() == 'FOREIGN_NATIONAL') {
                        residentFlag = false;
                        $("select[id*=country]").prop('disabled', false);
                        $("#dvEsopOrEquity").show();

                        if ($("input[id*=txtPermanentAccountNumber").val() === "NOT APPLICABLE") {
                            $("#txtHoldsEsopOrEquity").prop("checked", false);
                            $("#txtNotHoldsEsopOrEquity").prop("checked", true);
                            $("#dvApplicationPendingForPan").hide();
                            $("#txtApplicationPendingForPAN").prop("checked", false);
                            $("#txtApplicationNotPendingForPAN").prop("checked", true);
                            $("input[id*=txtPermanentAccountNumber]").prop('disabled', true);
                            $("#spnIdentificationType").show();
                            $("#spnIdentification").show();
                        }
                        else {
                            $("#txtHoldsEsopOrEquity").prop("checked", true);
                            $("#txtNotHoldsEsopOrEquity").prop("checked", false);
                            $("#dvApplicationPendingForPan").show();
                            $("#txtApplicationPendingForPAN").prop("checked", false);
                            $("#txtApplicationNotPendingForPAN").prop("checked", true);
                            $("input[id*=txtPermanentAccountNumber]").prop('disabled', false);
                            $("#spnIdentificationType").hide();
                            $("#spnIdentification").hide();
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
                }
            }
        },
        error: function (response) {
            $("#Loader").hide();
            alert(response.status + ' ' + response.statusText);
        }
    })
}
function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[1]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}
function getTaskId() {
    $("#Loader").show();
    var webUrl = uri + "/api/UserDisclousure/GetTaskId";
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
function isJson(str) {
    try {
        JSON.parse(str);
    } catch (e) {
        return false;
    }
    return true;
}
function getLoggedInUserInformation() {
    //alert("In getLoggedInUserInformation");
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
                $("input[id*=txtUserName").val(msg.User.USER_NM);
                $("input[id*=txtEmailId").val(msg.User.USER_EMAIL);
                $("input[id*=txtRole").val(msg.User.userRole.ROLE_NM);
                $("#tdMailFromUserName").html(msg.User.USER_NM);
                $("#tdMailFrom").html(msg.User.USER_EMAIL);
                $("input[id*=txtPermanentAccountNumber").val(msg.User.panNumber);

                if (!(msg.User.panNumber == "" || msg.User.panNumber == null)) {
                    $("select[id*='ddlIdentificationType']").prop('disabled', true);
                    $("input[id*='txtIdentificationNumber']").prop('disabled', true);
                }
                $("input[id*=txtMobileNumber").val(msg.User.USER_MOBILE);
                 $("select[id*=ddlDepartment").val(msg.User.department.DEPARTMENT_ID).trigger('change');
                $("select[id*=ddlDesignation").val(msg.User.designation.DESIGNATION_ID).trigger('change');
                $("select[id*='ddlCategory']").select2("val", $("select[id*='ddlCategory'] option:contains('" + msg.User.userRole.ROLE_NM + "')").val());
                 if ($("select[id*='ddlCategory']").val() != null && $("select[id*='ddlCategory']").val() != "") {
                    $("select[id*='ddlCategory']").prop('disabled', 'disabled');
                }
                $("input[id*=txtPersonalEmail").val(msg.User.PersonalEmail);
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
function fnSaveScreenInformation() {

    if ($("#tab1").hasClass('active')) {
        savePersonInformation();
    }
    else if ($("#tab2").hasClass('active')) {
        saveRelativeDetails();
        fnGetRelativeDetail();
    }
    else if ($("#tab3").hasClass('active')) {
        if ($("#tbdDematList").children().length > 0) {
            saveDematAccounts();
        }
        else {
            $('.button-previous').click();
            alert('In case you do not have any "Demat" to declare, Kindly elect and Add "Not Applicable" from the options provided in Add button');
        }
    }
    else if ($("#tab4").hasClass('active')) {
        //alert("In tab4 active");
        if ($("#tbdInitialDeclaration").children().length > 0) {

            initialHoldings();
            fnGenerateDynamicBodyOfInitialHolding();
        }
        else {
            $('.button-previous').click();
            alert('In case you do not have any "Initial Holding" to declare, Kindly Select and Add "Not Applicable" from the options provided in Add button');
        }
        fnGetPolicy();
         //var tempViewer = $("#viewer").data('ejPdfViewer');
        //tempViewer.updateViewerSize();
    }
    else if ($("#tab5").hasClass('active')) {

        //alert("In no or last active");
        //if ($("#tbdInitialDeclaration").children().length > 0) {
        //    var tempViewer = $("#viewer").data('ejPdfViewer');
        //    tempViewer.updateViewerSize()
        //    initialHoldings();
        //    fnGenerateDynamicBodyOfInitialHolding();
        //}
        //else {
        //    $('.button-previous').click();
        //    alert('In case you do not have any "Initial Holding" to declare, Kindly elect and Add "Not Applicable" from the options provided in Add button');
        //}
    }
}
function fnBack() {
    if ($("#tab2").hasClass('active')) {
        $("#liPI").addClass('active');
        $("#liRD").removeClass('active');
        $("#liDA").removeClass('active');
        $("#liIH").removeClass('active');
        $("#liCon").removeClass('active');

        $("#tab1").addClass('active');
        $("#tab2").removeClass('active');
        $("#tab3").removeClass('active');
        $("#tab4").removeClass('active');
        $("#tab5").removeClass('active');

        $("#tab1").click();
        $("#spnTitle").html("Step 1 of 5");
    }
    else if ($("#tab3").hasClass('active')) {
        $("#liPI").removeClass('active');
        $("#liRD").addClass('active');
        $("#liDA").removeClass('active');
        $("#liIH").removeClass('active');
        $("#liCon").removeClass('active');

        $("#tab1").removeClass('active');
        $("#tab2").addClass('active');
        $("#tab3").removeClass('active');
        $("#tab4").removeClass('active');
        $("#tab5").removeClass('active');

        $("#tab2").click();
        $("#spnTitle").html("Step 2 of 5");
    }
    else if ($("#tab4").hasClass('active')) {
        $("#liPI").removeClass('active');
        $("#liRD").removeClass('active');
        $("#liDA").addClass('active');
        $("#liIH").removeClass('active');
        $("#liCon").removeClass('active');

        $("#tab1").removeClass('active');
        $("#tab2").removeClass('active');
        $("#tab3").addClass('active');
        $("#tab4").removeClass('active');
        $("#tab5").removeClass('active');

        $("#tab3").click();
        $("#spnTitle").html("Step 3 of 5");
    }
    else if ($("#tab5").hasClass('active')) {
        $("#liPI").removeClass('active');
        $("#liRD").removeClass('active');
        $("#liDA").removeClass('active');
        $("#liIH").addClass('active');
        $("#liCon").removeClass('active');

        $("#tab1").removeClass('active');
        $("#tab2").removeClass('active');
        $("#tab3").removeClass('active');
        $("#tab4").addClass('active');
        $("#tab5").removeClass('active');

        $("#tab4").click();
        $("#spnTitle").html("Step 4 of 5");

        $("#aSavenContinue").show();
        $("#aSubmitYourDeclaration").hide();
        $("#spnblinkpreview").hide();

    }
}
function fnSaveConfirmation() {

}
function Department() {
    this.DEPARTMENT_ID = $("select[id*=ddlDepartment]").val() == null ? 0 : ($("select[id*=ddlDepartment]").val().trim() == "" ? 0 : $("select[id*=ddlDepartment]").val());
}
function Designation() {
    //this.DESIGNATION_ID = $("#ddlDesignation").val() == null ? 0 : ($("#ddlDesignation").val().trim() == "" ? 0 : $("#ddlDesignation").val());
    this.DESIGNATION_ID = $("select[id*=ddlDesignation]").val() == null ? 0 : ($("select[id*=ddlDesignation]").val().trim() == "" ? 0 : $("select[id*=ddlDesignation]").val());
}
function Location() {

    //this.locationId = $("#ddlLocation").val() == null ? 0 : ($("#ddlLocation").val().trim() == "" ? 0 : $("#ddlLocation").val());
    this.locationId = $("select[id*=ddlLocation]").val() == null ? 0 : ($("select[id*=ddlLocation]").val().trim() == "" ? 0 : $("select[id*=ddlLocation]").val());
}
function Category() {
    //this.ID = $("#ddlCategory").val() == null ? 0 : ($("#ddlCategory").val().trim() == "" ? 0 : $("#ddlCategory").val());
    this.ID = $("select[id*=ddlCategory]").val() == null ? 0 : ($("select[id*=ddlCategory]").val().trim() == "" ? 0 : $("select[id*=ddlCategory]").val());
    this.subCategory = new SubCategory();
}
function SubCategory() {
    this.ID = $("select[id*= ddlSubCategory]").val() == null ? 0 : ($("select[id*=ddlSubCategory]").val().trim() == "" ? 0 : $("select[id*=ddlSubCategory]").val());
}
function Email() {
    this.id = 0;
    this.mailFromUserName = $("#tdMailFromUserName").html();
    this.mailFrom = $("#tdMailFrom").html();
    this.mailToUserName = $("#tdMailToUserName").html();
    this.mailTo = $("#tdMailTo").html();
    this.subject = $("#tdSubject").html();
    //this.body = $('<div>').append($("#templateBodyInitialHoldingDeclaration").closest('table').attr("border", 1).clone()).html();
    this.body = '';
}
function getAllSubCategories() {
    $("#Loader").show();
    var webUrl = uri + "/api/SubCategory/GetSubCategoryList";
    $.ajax({
        url: webUrl,
        type: "POST",
        data: JSON.stringify(new Category()),
        async: false,
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                var str = "";
                str += '<option value=""></option>';
                for (var index = 0; index < msg.SubCategoryList.length; index++) {
                    str += '<option value="' + msg.SubCategoryList[index].ID + '">' + msg.SubCategoryList[index].subCategoryName + '</option>';
                }
                $("select[id*=ddlSubCategory]").html(str);
            }
            else {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    $("select[id*=ddlSubCategory]").html('');
                }
            }
        },
        error: function (response) {
            $("#Loader").hide();
            alert(response.status + ' ' + response.statusText);
        }
    })
}
function fnRemoveClass(obj, val) {
    $("#lbl" + val + "").removeClass('requiredlbl');
}
function fnGetApprover() {
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/GetUserApprover";
    $.ajax({
        url: webUrl,
        type: "GET",
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            if (msg.StatusFl == true) {
                var mailTo = "";
                var mailToUserName = "";
                for (var i = 0; i < msg.UserList.length; i++) {

                    mailToUserName = msg.UserList[i].USER_NM;
                    mailTo = msg.UserList[i].USER_EMAIL;

                }
                $("#tdMailTo").html(mailTo);
                $("#tdMailToUserName").html(mailToUserName);
                $("#Loader").hide();
            }
            else {
                $("#Loader").hide();
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
function fnSendEmailNoticeConfirmation() {
    if ($("#inAcceptTermsAndConditions").prop('checked')) {
        $("#Loader").show();
        var personalInformation = new PersonalInformation();
        personalInformation.isFinalDeclared = true;
        var token = $("#TokenKey").val();
        var webUrl = uri + "/api/Transaction/SendEmailNoticeConfirmation";
        $.ajax({
            url: webUrl,
            type: "POST",
            data: JSON.stringify(personalInformation),
            // async: false,
            headers: {
                'TokenKeyH': token,

            },
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            success: function (msg) {
                $("#Loader").hide();
                if (isJson(msg)) {
                    msg = JSON.parse(msg);
                }
                if (msg.StatusFl == true) {
                    alert('Your declaration has been submitted');
                    window.location.href = "Dashboard.aspx";
                }
                else {
                    if (msg.Msg == "SessionExpired") {
                        alert("Your session is expired. Please login again to continue");
                        window.location.href = "../LogOut.aspx";
                    }
                    else if (msg.Msg == "Educational And Professional Qualification does not exist.") {
                        $("#modalAddEducationalAndProfessionalDetails").modal('show');
                    }
                    else {
                        alert(msg.Msg);
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
        alert("Please give declaration");
    }
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
        async: false,
        success: function (msg) {
            if (msg.StatusFl == true) {
                $("#Loader").hide();
                /* Personal Information */

                setPersonalInformation(msg);

                /* Relative Info */
                setRelativeInformation(msg);

                /* Demat Info */
                setDematInformation(msg);

                /* Initial Holding */

                setInitialHoldingDetail(msg);
            }
            else {
                $("#Loader").hide();
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
function ConvertToDateTime(dateTime) {
    if (dateTime == null) {
        return "";
    }
    else {
        var date = dateTime.split(" ")[0];

        return (date.split("/")[1] + "/" + date.split("/")[0] + "/" + date.split("/")[2]);
    }
}
function fnGetPolicy() {
    //alert("In getpolicy");
    $("#Loader").show();
    var webUrl = uri + "/api/Policy/GetPolicy";
    $.ajax({
        type: 'GET',
        url: webUrl,
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            //alert("In policy success");
            //alert("In policy success");
            $("#Loader").hide();
            if (msg.StatusFl == false) {

                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    alert(msg.Msg);
                    return false;
                }
            }
            else {
                //alert(msg.PolicyList[0].DOCUMENT);
                //alert("uri=" + uri);
                var policyDoc = uri + "/assets/logos/Policy/" + msg.PolicyList[0].DOCUMENT;
                //alert("policyDoc=" + policyDoc);

                var webUrl = uri + "/api/PdfViewerIT";
                $("#viewer").ejPdfViewer({
                    serviceUrl: webUrl,
                    documentPath: policyDoc,
                    enableStrikethroughAnnotation: false,
                    toolbarSettings: { showTooltip: false }
                });

                var pdfViewer = $('#viewer').data('ejPdfViewer');
                pdfViewer.load("../assets/logos/Policy/" + msg.PolicyList[0].DOCUMENT);
                /*var pdfViewer = $('#viewer').data('ejPdfViewer');
                        pdfViewer.showSelectionTool(false);
                        pdfViewer.showPrintTools(false);
                        pdfViewer.showDownloadTool(false);
                        pdfViewer.showSignatureTool(false);
                        pdfViewer.showTextMarkupAnnotationTools(false);
                        pdfViewer.showMagnificationTools(false);
                        pdfViewer.model.enableTextSelection = false;*/



                // $("#aInAcceptTermsAndConditions").attr('href', ("../assets/logos/Policy/" + msg.PolicyList[0].DOCUMENT));

                //var pdfviewer = $("#viewer").data("ejPdfViewer");
                //pdfviewer.load("../assets/logos/Policy/" + msg.PolicyList[0].DOCUMENT);


                //pdfViewer.showSelectionTool(false);
                //pdfViewer.showPrintTools(false);
                //pdfViewer.showDownloadTool(false);
                //pdfViewer.showSignatureTool(false);
                //pdfViewer.showTextMarkupAnnotationTools(false);
                //pdfViewer.showMagnificationTools(false);
                //pdfViewer.model.enableTextSelection = false;
            }
        },
        error: function (error) {
            $("#Loader").hide();
            alert(error.status + ' ' + error.statusText);
        }
    })
}
function fnCloseModalEducationAndProfessionalDetails() {
    fnClearFormModalEducationalAndProfessionalDetails();
}
function fnClearFormModalEducationalAndProfessionalDetails() {
    $("#txtInstitution").val('');
    $("#txtStream").val('');
    $("#txtEmployer").val('');
    $("#lblInstitution").removeClass('requiredlbl');
    $("#lblStream").removeClass('requiredlbl');
    $("#lblEmployer").removeClass('requiredlbl');
}
function fnValidateEducationalAndProfessionalDetails() {
    var count = 0;
    if ($("#txtInstitution").val() == null || $("#txtInstitution").val() == undefined || $("#txtInstitution").val().trim() == '') {
        count++;
        $("#lblInstitution").addClass('requiredlbl');
    }
    else {
        $("#lblInstitution").removeClass('requiredlbl');
    }
    if ($("#txtStream").val() == null || $("#txtStream").val() == undefined || $("#txtStream").val().trim() == '') {
        count++;
        $("#lblStream").addClass('requiredlbl');
    }
    else {
        $("#lblStream").removeClass('requiredlbl');
    }

    if (count > 0) {
        return false;
    }
    else {
        return true;
    }
}
function fnSubmitEducationalAndProfessionalDetails() {
    if (fnValidateEducationalAndProfessionalDetails()) {
        $("#Loader").show();
        var institutionName = $("#txtInstitution").val();
        var stream = $("#txtStream").val();
        var employerDetails = $("#txtEmployer").val();
        var declarationId = $("#txtD_ID").val();
        var token = $("#TokenKey").val();

        var webUrl = uri + "/api/Transaction/SaveEducationalAndProfessionalDetails";
        $.ajax({
            url: webUrl,
            headers: {
                'TokenKeyH': token,

            },
            type: "POST",
            data: JSON.stringify({
                D_ID: declarationId, institutionName: institutionName, stream: stream, employerDetails: employerDetails
            }),
            // async: false,
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            success: function (msg) {
                $("#Loader").hide();
                if (isJson(msg)) {
                    msg = JSON.parse(msg);
                }
                if (msg.StatusFl == true) {
                    alert(msg.Msg);
                    $("#modalAddEducationalAndProfessionalDetails").modal('hide');
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
            error: function (response) {
                $("#Loader").hide();
                alert(response.status + ' ' + response.statusText);
            }
        })
    }
}
function fnCheckPanMandatory() {
    if ($("#txtSkipPan").prop('checked')) {
        $("#txtPAN").val('Not Applicable');
        $("#txtPAN").prop("disabled", true);
        $("#lblPAN").removeClass('requiredlbl');
        $("#lblIdentificationTypeRelation").removeClass('requiredlbl');
        $("#lblIdentificationNumberRelation").removeClass('requiredlbl');
        $("#ddlIdentificationTypeRelation").val('');
        $("#ddlIdentificationTypeRelation").prop('disabled', false);
        $("#txtIdentificationNumberRelation").prop('disabled', false);
        $("#txtdpPan").prop('disabled', true);

    }
    else {
        $("#txtPAN").val('');
        $("#txtPAN").prop("disabled", false);
        $("#lblPAN").removeClass('requiredlbl');
        $("#lblIdentificationTypeRelation").removeClass('requiredlbl');
        $("#lblIdentificationNumberRelation").removeClass('requiredlbl');
        $("#ddlIdentificationTypeRelation").val('');
        $("#ddlIdentificationTypeRelation").prop('disabled', true);
        $("#txtIdentificationNumberRelation").prop('disabled', true);
        $("#txtdpPan").prop('disabled', false);
    }
}
function ddlDesignation_onChange() {
    var sDesignation = $("#ddlDesignation option:selected").text();
    if (sDesignation == "Director" || sDesignation == "DIRECTOR" || sDesignation == "director") {
        $("#txtDinNumber").removeAttr("disabled");
    }
    else {
        $("#txtDinNumber").val('N/A');
        $("#txtDinNumber").attr("disabled", "disabled");
    }
}
function ValidatePAN(valPan) {
    //alert("In function ValidatePAN");
    //alert("valPan=" + valPan);
    var regpan = /^([a-zA-Z]){5}([0-9]){4}([a-zA-Z]){1}?$/;
    var fl = true;
    if (valPan == "" || valPan == null || valPan == undefined) {
        fl = false;
    }
    else if (!regpan.test(valPan)) {
        fl = false;
    }
    return fl;
}
function fnSaveLocation() {
    var loca_name = $("#txtlocationname").val();
    if (loca_name == "" || loca_name == null) {
        alert("Please enter Location Name.");
        return false;

    }
    var lo_id = $("#idlocation").val();
    if (lo_id === "") {
        lo_id = 0;
    }



    //var location_d = new Location(lo_id, loca_name)

    var webUrl = uri + "/api/Location/SaveLocation";
    $("#Loader").show();
    setTimeout(function () {
        $.ajax({
            type: 'POST',
            url: webUrl,
            data: JSON.stringify({
                locationId: lo_id, locationName: loca_name
            }),
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            async: false,
            success: function (msg) {
                if (isJson(msg)) {
                    msg = JSON.parse(msg);
                }

                if (msg.StatusFl == false) {
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
                else {
                    $("#Loader").hide();
                    $("#stack1Location").modal('hide');
                    alert(msg.Msg);
                    window.location.reload();
                    //fnListLocation();
                }


            },
            error: function (error) {
                $("#Loader").hide();

                //   $('#btnSave').removeAttr("data-dismiss");

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
function fnSaveDepartment() {
    var DepartmentID = $('#txtDepartmentKey').val();
    var DepartmentName = $('#txtDepartmentName').val();
    var Department = $('#txtDepartmentName').val();
    if (DepartmentName == '' || DepartmentName == null || DepartmentName == undefined) {

        alert("Please enter Department name");
        return false;
    }
    else {
        $('#lblDepartment').removeClass('requied');
    }

    if (DepartmentID === "") {
        DepartmentID = 0;
    }
    $("#Loader").show();
    var webUrl = uri + "/api/Department/SaveDepartment";
    $.ajax({
        type: 'POST',
        url: webUrl,
        data: JSON.stringify({
            DEPARTMENT_ID: DepartmentID, Department_NM: DepartmentName
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == false) {
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
            else {
                alert(msg.Msg);
                window.location.reload();
                $('#btnSave').attr("data-dismiss", "modal");
                return true;
            }
        },
        error: function (error) {
            $("#Loader").hide();
            alert(error.status + ' ' + error.statusText);
            $('#btnSave').removeAttr("data-dismiss");
        }
    })
}
function fnSaveDesignation() {
    fnAddUpdateDesignation();
}
function fnAddUpdateDesignation() {

    if ($('#txtDesignationName').val().trim() == "" || $('#txtDesignationName').val() == null || $('#txtDesignationName').val() == undefined) {

        alert("Please enter designation name");
        return false;
    }
    var designation_Nm = $('#txtDesignationName').val();
    var designation_Id = $('#txtDesignationKey').val();
    if (designation_Id === "") {
        designation_Id = 0;
    }

    $("#Loader").show();
    var webUrl = uri + "/api/DesignationIT/SaveDesignation";
    $.ajax({
        type: 'POST',
        url: webUrl,
        data: JSON.stringify({
            DESIGNATION_ID: designation_Id, DESIGNATION_NM: designation_Nm
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == false) {
                $("#Loader").hide();
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    alert(msg.Msg);
                    //$('#btnSave').removeAttr("data-dismiss");
                    return false;
                }
            }
            else {
                alert(msg.Msg);
                window.location.reload();

            }
        },
        error: function (error) {
            $("#Loader").hide();
            alert(error.status + ' ' + error.statusText);
            $('#btnSave').removeAttr("data-dismiss");
        }
    })
}
function fnCloseModal() {
    fnClearForm();
}
function fnClearForm() {
    $('#txtDesignationName').val('');
    $('#txtDesignationKey').val('');
}
function fnAddLocation() {
    $("#txtlocationname").val('');
    $("#stack1Location").modal();
}
function fnAddDepartment() {
    $("#txtDepartmentName").val('');
    $("#stack1Department").modal();
}
function fnAddDesignation() {
    $("#txtDesignationName").val('');
    $("#stack1Designation").modal();

}
function validateEmail(values) {
    var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
    if (reg.test(values) == false) {
        return false;
    }
    return true;
}
function fnVerifyPan(UserPan, RelativeId, RelationId, IsDesignatedFl) {
    var flag = false;
    if (UserPan.toUpperCase() != "NOT APPLICABLE") {
        var token = $("#TokenKey").val();
        $("#Loader").show();
        var webUrl = uri + "/api/Transaction/VerifyPan";
        $.ajax({
            url: webUrl,
            headers: {
                'TokenKeyH': token,
            },
            type: "POST",
            data: JSON.stringify({
                panNumber: UserPan, ID: RelativeId, RELATION_ID: RelationId, IsDesignatedFl: IsDesignatedFl
            }),
            async: false,
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            success: function (msg) {
                $("#Loader").hide();
                if (isJson(msg)) {
                    msg = JSON.parse(msg);
                }
                if (msg.StatusFl == false) {
                    if (msg.Msg == "SessionExpired") {
                        alert("Your session is expired. Please login again to continue");
                        window.location.href = "../LogOut.aspx";
                    }
                    else {
                        alert(msg.Msg);

                    }
                }
                else {
                    flag = true;
                }
            },
            error: function (error) {
                $("#Loader").hide();
                alert(error.status + ' ' + error.statusText);
                return false;
            }
        })
        //alert(flag);

    }
    else {
        flag = true;
    }
    return flag;
}
function fnPreviewDeclarationForm() {
    $("#Loader").show();
    var personalInformation = new PersonalInformation();
    personalInformation.isFinalDeclared = false;
    var token = $("#TokenKey").val();
    var webUrl = uri + "/api/Transaction/PreviewDeclarationForm";
    $.ajax({
        url: webUrl,
        type: "POST",
        data: JSON.stringify(personalInformation),
        // async: false,
        headers: {
            'TokenKeyH': token,

        },
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                if (msg.User.lstAttachment !== null) {
                    for (var x = 0; x < msg.User.lstAttachment.length; x++) {
                        window.open(".." + msg.User.lstAttachment[x]);
                    }
                    enableabtnSubmitDeclaration = true;
                    $("#aSubmitYourDeclaration").css("pointer-events", "auto");
                    $("#aSubmitYourDeclaration").css({ "color": "#FFF", "background-color": "green", "border-color": "green" });
                    $("#aSubmitYourDeclaration").attr("disabled", false);

                }
                $('html, body').animate({
                    scrollTop: $("#aSubmitYourDeclaration").offset().top
                }, 200);
            }
            else {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else if (msg.Msg == "Educational And Professional Qualification does not exist.") {
                    $("#modalAddEducationalAndProfessionalDetails").modal('show');
                }
                else {
                    alert(msg.Msg);
                }
            }
        },
        error: function (response) {
            $("#Loader").hide();
            alert(response.status + ' ' + response.statusText);
        }
    })
}