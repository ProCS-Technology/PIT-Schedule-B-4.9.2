
jQuery(document).ready(function () {
    //fnGetDepartmentList();
    fnGetModelCodeConductList();
    $('#stack1').on('hide.bs.modal', function () {
    });

});

function fnGetModelCodeConductList() {
    $("#Loader").show();
    var webUrl = uri + "/api/ModelCodeConduct/GetModelCodeConductList";
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

            if (msg.StatusFl == true) {
                var result = "";
                for (var i = 0; i < msg.ModelCodeConductList.length; i++) {
                    fnEditModelCodeConduct(msg.ModelCodeConductList[i].MODEL_ID, msg.ModelCodeConductList[i].FREQUENCY_OF_PERIOD, msg.ModelCodeConductList[i].CUT_OFF_DATES_FOR_PERIOD, msg.ModelCodeConductList[i].RESTRICTED_MONTHS_FOR_CONTRATRADE, msg.ModelCodeConductList[i].AMOUNTLIMIT_FOR_CONTINUAL_DISCLOSURE, msg.ModelCodeConductList[i].AMOUNTLIMIT_FOR_PRE_CLEARANCE, msg.ModelCodeConductList[i].SHARELIMIT_FOR_PRE_CLEARANCE, msg.ModelCodeConductList[i].VALIDITY_OF_PRE_CLEARANCE_APPROVAL);
                }
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
function fnAddDepartment() {

}

function fnSaveCodeConductModel() {
    if (fnValidate()) {
        fnAddUpdateCodeConductModel();
    }
    else {
        $('#btnSave').removeAttr("data-dismiss");
        return false;
    }
}

function fnAddUpdateCodeConductModel() { 
    var CodeConductModelKey = $('#CodeConductModelKey').val();
    var FREQUENCY_OF_PERIOD = $('#ddlFrequencyOfPeriodicClosure').val();
    var CUT_OFF_DATES_FOR_PERIOD = $('#txtPeriodicDisclosureDate').val();
    var RESTRICTED_MONTHS_FOR_CONTRATRADE = $('#txtContraTrade_mONTH').val();
    var AMOUNTLIMIT_FOR_CONTINUAL_DISCLOSURE = $('#txtContinualLimit').val();
    var AMOUNTLIMIT_FOR_PRE_CLEARANCE = $('#txtPre_clear_AmountLimit').val();
    var SHARELIMIT_FOR_PRE_CLEARANCE = $('#txtPre_clear_ShareLimit').val();
    var VALIDITY_OF_PRE_CLEARANCE_APPROVAL = $('#txtPre_clear_Approval').val();

    if (CodeConductModelKey === "") {
        CodeConductModelKey = 0;
    }
    $("#Loader").show();
    var webUrl = uri + "/api/ModelCodeConduct/SaveModelCodeConduct";
    $.ajax({
        type: 'POST',
        url: webUrl,
        data: JSON.stringify({
            MODEL_ID: CodeConductModelKey, FREQUENCY_OF_PERIOD: FREQUENCY_OF_PERIOD, CUT_OFF_DATES_FOR_PERIOD: CUT_OFF_DATES_FOR_PERIOD, RESTRICTED_MONTHS_FOR_CONTRATRADE: RESTRICTED_MONTHS_FOR_CONTRATRADE, AMOUNTLIMIT_FOR_CONTINUAL_DISCLOSURE: AMOUNTLIMIT_FOR_CONTINUAL_DISCLOSURE, AMOUNTLIMIT_FOR_PRE_CLEARANCE: AMOUNTLIMIT_FOR_PRE_CLEARANCE, SHARELIMIT_FOR_PRE_CLEARANCE: SHARELIMIT_FOR_PRE_CLEARANCE, VALIDITY_OF_PRE_CLEARANCE_APPROVAL: VALIDITY_OF_PRE_CLEARANCE_APPROVAL
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            if (msg.StatusFl == false) {
                $("#Loader").hide();
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    alert(msg.Msg);
                    $('#btnSave').removeAttr("data-dismiss");
                    return false;
                }
            }
            else {
                alert(msg.Msg);
                if (msg.ModelCodeConduct.MODEL_ID == CodeConductModelKey) {
                    fnEditModelCodeConduct(msg.ModelCodeConduct.MODEL_ID, msg.ModelCodeConduct.FREQUENCY_OF_PERIOD, msg.ModelCodeConduct.CUT_OFF_DATES_FOR_PERIOD, msg.ModelCodeConduct.RESTRICTED_MONTHS_FOR_CONTRATRADE, msg.ModelCodeConduct.AMOUNTLIMIT_FOR_CONTINUAL_DISCLOSURE, msg.ModelCodeConduct.AMOUNTLIMIT_FOR_PRE_CLEARANCE, msg.ModelCodeConduct.SHARELIMIT_FOR_PRE_CLEARANCE, msg.ModelCodeConduct.VALIDITY_OF_PRE_CLEARANCE_APPROVAL);
                    $("#Loader").hide();
                }
                else {
                    fnEditModelCodeConduct(msg.ModelCodeConduct.MODEL_ID, msg.ModelCodeConduct.FREQUENCY_OF_PERIOD, msg.ModelCodeConduct.CUT_OFF_DATES_FOR_PERIOD, msg.ModelCodeConduct.RESTRICTED_MONTHS_FOR_CONTRATRADE, msg.ModelCodeConduct.AMOUNTLIMIT_FOR_CONTINUAL_DISCLOSURE, msg.ModelCodeConduct.AMOUNTLIMIT_FOR_PRE_CLEARANCE, msg.ModelCodeConduct.SHARELIMIT_FOR_PRE_CLEARANCE, msg.ModelCodeConduct.VALIDITY_OF_PRE_CLEARANCE_APPROVAL);
                    $("#Loader").hide();
                }
             
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

function fnValidate() {
    if ($('#ddlFrequencyOfPeriodicClosure').val() == "" || $('#ddlFrequencyOfPeriodicClosure').val() == null || $('#ddlFrequencyOfPeriodicClosure').val() == undefined) {
        alert("Please enter FREQUENCY OF PERIOD");
        return false;
    }
    if ($('#txtPeriodicDisclosureDate').val() == "" || $('#txtPeriodicDisclosureDate').val() == null || $('#txtPeriodicDisclosureDate').val() == undefined) {
        alert("Please enter CUT OFF DATES FOR PERIOD");
        return false;
    }
    if ($('#txtContraTrade_mONTH').val() == "" || $('#txtContraTrade_mONTH').val() == null || $('#txtContraTrade_mONTH').val() == undefined) {
        alert("Please enter RESTRICTED MONTHS FOR CONTRATRADE");
        return false;
    }
    if ($('#txtContinualLimit').val() == "" || $('#txtContinualLimit').val() == null || $('#txtContinualLimit').val() == undefined) {
        alert("Please enter AMOUNT LIMIT FOR CONTINUAL DISCLOSURE");
        return false;
    }
    if ($('#txtPre_clear_AmountLimit').val() == "" || $('#txtPre_clear_AmountLimit').val() == null || $('#txtPre_clear_AmountLimit').val() == undefined) {
        alert("Please enter AMOUNT LIMIT FOR PRE CLEARANCE");
        return false;
    }
    if ($('#txtPre_clear_ShareLimit').val() == "" || $('#txtPre_clear_ShareLimit').val() == null || $('#txtPre_clear_ShareLimit').val() == undefined) {
        alert("Please enter SHARE LIMIT FOR PRE CLEARANCE");
        return false;
    }
    if ($('#txtPre_clear_Approval').val() == "" || $('#txtPre_clear_Approval').val() == null || $('#txtPre_clear_Approval').val() == undefined) {
        alert("Please enter VALIDITY OF PRE CLEARANCE APPROVAL");
        return false;
    }
    return true;
}

function fnEditModelCodeConduct(CodeConductModelKey,FREQUENCY_OF_PERIOD, CUT_OFF_DATES_FOR_PERIOD,RESTRICTED_MONTHS_FOR_CONTRATRADE,AMOUNTLIMIT_FOR_CONTINUAL_DISCLOSURE,AMOUNTLIMIT_FOR_PRE_CLEARANCE,SHARELIMIT_FOR_PRE_CLEARANCE,VALIDITY_OF_PRE_CLEARANCE_APPROVAL) {
    $('#CodeConductModelKey').val(CodeConductModelKey);
    $('#ddlFrequencyOfPeriodicClosure').val(FREQUENCY_OF_PERIOD);
    $('#txtPeriodicDisclosureDate').val(CUT_OFF_DATES_FOR_PERIOD);
    $('#txtContraTrade_mONTH').val(RESTRICTED_MONTHS_FOR_CONTRATRADE);
    $('#txtContinualLimit').val(AMOUNTLIMIT_FOR_CONTINUAL_DISCLOSURE);
    $('#txtPre_clear_AmountLimit').val(AMOUNTLIMIT_FOR_PRE_CLEARANCE);
    $('#txtPre_clear_ShareLimit').val(SHARELIMIT_FOR_PRE_CLEARANCE);
    $('#txtPre_clear_Approval').val(VALIDITY_OF_PRE_CLEARANCE_APPROVAL);
}

