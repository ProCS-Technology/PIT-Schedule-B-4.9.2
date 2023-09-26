$('input[type=radio][name=radioMarried]').change(function () {
    //debugger;
    if (this.value == 'Yes') {
        isMarried = "Yes";
    }
    else {
        isMarried = "No";
    }
});
function savePersonInformation() {
    if (fnValidatePersonalInformation() == true) {
        if (fnVerifyPan($("input[id*=txtPermanentAccountNumber").val(), 0, 0, 'No') == true) {
            var token = $("#TokenKey").val();
            $("#Loader").show();
            var webUrl = uri + "/api/Transaction/SavePersonalInformation";
            $.ajax({
                url: webUrl,
                headers: {
                    'TokenKeyH': token,
                },
                type: "POST",
                data: JSON.stringify(new PersonalInformation()),
                async: false,
                contentType: "application/json; charset=utf-8",
                datatype: "json",
                success: function (msg) {
                    $("#Loader").hide();
                    //debugger;
                    if (msg.StatusFl == true) {
                        $("input[id*=txtD_ID]").val(msg.User.D_ID);
                        //fnGetTransactionalInfo();
                        alert("Personal information updated successfully !");
                        if ($("#UserCheck").prop('checked') == true) {
                            fnGetDPHoldingDetails("FromPersonal");
                            $("#tab5").show();
                            $("#aSubmitYourDeclaration").show();
                            $("#tab1").hide();
                            $("#tab2").hide();
                            $("#tab3").hide();
                            $("#tab4").hide();
                            $("#aSavenContinue").hide();
                            $("#tab1").removeClass('active');
                            $("#tab2").removeClass('active');
                            $("#tab3").removeClass('active');
                            $("#tab4").removeClass('active');
                            $("#tab5").removeClass('active');
                            $("#liPI").removeClass('active');
                            $("#liRD").removeClass('active');
                            $("#liDA").removeClass('active');
                            $("#liIH").removeClass('active');
                            $("#liCon").addClass('active');
                        }
                        else {
                            $("#aSavenContinue").show();
                            $("#aSubmitYourDeclaration").hide();
                            $("#spnblinkpreview").hide();

                            fnGetDPRelativeDetails();

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

                            $("#tab1").hide();
                            $("#tab2").show();
                            $("#tab3").hide();
                            $("#tab5").hide();
                            $("#tab4").hide();

                            $("#tab2").click();
                            $("#spnTitle").html("Step 2 of 5");
                        }
                    }
                    else {
                        if (msg.Msg == "SessionExpired") {
                            alert("Your session is expired. Please login again to continue");
                            window.location.href = "../LogOut.aspx";
                        }
                        else {
                            //do nothing
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
    else {
        alert("Please provide all mandatory fields highlighted in red.");
    }
}
function fnValidatePersonalInformation() {
    //debugger;
    var flag = false;
    for (var x = 0; x < arrFields.length; x++) {
        for (y = 0; y < arrFields[x].fields.length; y++) {
            var obj = arrFields[x].fields[y];
            if (arrFields[x].fields[y].DisplayFl == "Y" && arrFields[x].fields[y].RequiredFl == "Y") {
                var value;
                //alert("arrFields["+x+"].fields["+y+"].ControlNm="+arrFields[x].fields[y].ControlNm);
                if (arrFields[x].fields[y].FormatType == "Number") {
                    var regpan = /^\d+$/;
                    if (arrFields[x].fields[y].CntrlType == "TextBox") {
                        value = $("input[id*=" + obj.ControlId + "]").val();
                    }
                    else if (arrFields[x].fields[y].CntrlType == "Dropdown") {
                        value = $("select[id*=" + obj.ControlId + "]").val();
                    }
                    if (value == "" || value == null || value == undefined) {
                        flag = true;
                        $("#" + arrFields[x].fields[y].DivNm).addClass('has-error');

                    }
                    else if (!regpan.test(value)) {
                        flag = true;
                        $("#" + arrFields[x].fields[y].DivNm).addClass('has-error');

                    }
                    else {
                        $("#" + arrFields[x].fields[y].DivNm).removeClass('has-error');
                    }
                }
                else if (arrFields[x].fields[y].FormatType == "Mobile") {
                    var regpan = /^([0-9]){10}?$/;
                    if (arrFields[x].fields[y].CntrlType == "TextBox") {
                        value = $("input[id*=" + obj.ControlId + "]").val();
                    }
                    else if (arrFields[x].fields[y].CntrlType == "Dropdown") {
                        value = $("select[id*=" + obj.ControlId + "]").val();
                    }
                    if (value == "" || value == null || value == undefined) {
                        flag = true;
                        $("#" + arrFields[x].fields[y].DivNm).addClass('has-error');

                    }
                    else if (!regpan.test(value)) {
                        flag = true;
                        $("#" + arrFields[x].fields[y].DivNm).addClass('has-error');

                    }
                    else {
                        $("#" + arrFields[x].fields[y].DivNm).removeClass('has-error');
                    }
                }
                //else if (arrFields[x].fields[y].FormatType == "PAN") {
                //    var PANfl = false;
                //    if (arrFields[x].fields[y].CntrlType == "TextBox") {
                //        value = $("input[id*=" + obj.ControlId + "]").val();
                //    }
                //    else if (arrFields[x].fields[y].CntrlType == "Dropdown") {
                //        value = $("select[id*=" + obj.ControlId + "]").val();
                //    }
                //    var regpan = /^([a-zA-Z]){5}([0-9]){4}([a-zA-Z]){1}?$/;
                //    if (value == "" || value == null || value == undefined) {
                //        flag = true;
                //        $("#" + arrFields[x].fields[y].DivNm).addClass('has-error');

                //    }
                //    else if (!regpan.test(value) && residentFlag == true) {
                //        flag = true;
                //        $("#" + arrFields[x].fields[y].DivNm).addClass('has-error');

                //    }
                //    else {
                //        $("#" + arrFields[x].fields[y].DivNm).removeClass('has-error');
                //    }
                //}
                else if (arrFields[x].fields[y].FormatType == "Date") {
                    var regpan = /^(0?[1-9]|1[012])[\/\-](0?[1-9]|[12][0-9]|3[01])[\/\-]\d{4}$/;
                    if (arrFields[x].fields[y].CntrlType == "TextBox") {
                        value = $("input[id*=" + obj.ControlId + "]").val();
                    }
                    else if (arrFields[x].fields[y].CntrlType == "Dropdown") {
                        value = $("select[id*=" + obj.ControlId + "]").val();
                    }
                    if (value == "" || value == null || value == undefined) {
                        flag = true;
                        $("#" + arrFields[x].fields[y].DivNm).addClass('has-error');

                    }
                    //else if (!regpan.test(ConvertToDateTime(value))) {
                    //    flag = true;
                    //    $("#" + arrFields[x].fields[y].DivNm).addClass('has-error');

                    //}
                    else {
                        $("#" + arrFields[x].fields[y].DivNm).removeClass('has-error');
                    }
                }
                else if (arrFields[x].fields[y].FormatType == "Text") {
                    if (arrFields[x].fields[y].CntrlType == "TextBox") {
                        value = $("input[id*=" + obj.ControlId + "]").val();
                    }
                    else if (arrFields[x].fields[y].CntrlType == "TextArea") {
                        value = $("textarea[id*=" + obj.ControlId + "]").val();
                    }
                    else if (arrFields[x].fields[y].CntrlType == "Dropdown") {
                        value = $("select[id*=" + obj.ControlId + "]").val();
                    }
                    if (value == "" || value == null || value == undefined) {
                        flag = true;
                        $("#" + arrFields[x].fields[y].DivNm).addClass('has-error');

                    }
                    else {
                        $("#" + arrFields[x].fields[y].DivNm).removeClass('has-error');
                    }
                    var Relation1 = "";
                    if ($("#YesMarried").prop("checked")) {
                        Relation1 = "Yes";
                    }
                    else if ($("#NoMarried").prop("checked")) {
                        Relation1 = "No";
                    }
                    if (Relation1 == '' || Relation1 == null || Relation1 == '0') {
                        $('#dvMaritalStatus').addClass('has-error');
                        return false;
                    }
                    else {
                        $('#dvMaritalStatus').removeClass('has-error');
                    }
                }
            }
        }
    }
    if ($("select[id*='country']").val().toUpperCase() == "IN") {
        if ($("input[id*='txtMobileNumber']").val().length > 0 && $("input[id*='txtMobileNumber']").val().length != 10) {
            flag = true;
            alert("Please enter 10 digit mobile number.");
        }
        if ($("input[id*='txtPincodeNumber']").val().length > 0 && $("input[id*='txtPincodeNumber']").val().length != 6) {
            flag = true;
            alert("Please enter 6 digit pincode.");
        }


    }
    else {

        if ($("input[id*='txtPincodeNumber']").val().length > 0 && $("input[id*='txtPincodeNumber']").val().length < 3) {
            flag = true;
            alert("Please enter valid pincode.");
        }
    }
    if ($("select[id*=ddlResidentType]").val() == "INDIAN_RESIDENT") {
        if ($("select[id*=country]").val() == "" || $("select[id*=country]").val() == null || $("select[id*=country]").val() == undefined) {
            alert("Please select Country");
            return false;
        }
        if ($("input[id*=txtPermanentAccountNumber]").val() == "") {
            alert("Please enter PAN Number");
            return false;
        }
        if ($("input[id*=txtPermanentAccountNumber]").val() != "") {
            var PAN = $("input[id*=txtPermanentAccountNumber]").val();
            var regpan = /^([a-zA-Z]){5}([0-9]){4}([a-zA-Z]){1}?$/;
            if (!regpan.test(PAN)) {
                alert("PAN Number is not correct");
                return false;
            }
        }
    }
    else if ($("select[id*=ddlResidentType]").val() == "FOREIGN_NATIONAL") {
        if ($("select[id*=country]").val() == "" || $("select[id*=country]").val() == null || $("select[id*=country]").val() == undefined) {
            alert("Please select Country");
            return false;
        }
        if ($("select[id*=ddlIdentificationType]").val() == "" || $("select[id*=ddlIdentificationType]").val() == null || $("select[id*=ddlIdentificationType]").val() == undefined) {
            alert("Please select Identification Type");
            return false;
        }
        if ($("input[id*='txtIdentificationNumber']").val() == "") {
            alert("Please enter Identification Number");
            return false;
        }
    }
    else if ($("select[id*=ddlResidentType]").val() == "NRI") {
        if ($("select[id*=country]").val() == "" || $("select[id*=country]").val() == null || $("select[id*=country]").val() == undefined) {
            alert("Please select Country");
            return false;
        }
        if ($("select[id*=ddlIdentificationType]").val() == "" || $("select[id*=ddlIdentificationType]").val() == null || $("select[id*=ddlIdentificationType]").val() == undefined) {
            alert("Please select Identification Type");
            return false;
        }
        if ($("input[id*='txtIdentificationNumber']").val() == "") {
            alert("Please enter Identification Number");
            return false;
        }
    }
    //alert("flag=" + flag);
    return !flag;
}
function PersonalInformation() {
    this.ID = 0;
    this.D_ID = $("input[id*=txtD_ID").val();
    this.residentType = $("select[id*=ddlResidentType").val();
    this.panNumber = $("input[id*=txtPermanentAccountNumber").val();
    this.identificationType = $("select[id*=ddlIdentificationType").val();
    this.identificationNumber = $("input[id*=txtIdentificationNumber").val();
    this.USER_MOBILE = $("input[id*=txtMobileNumber").val();
    this.address = $("input[id*=txtAddress").val();
    this.pinCode = $("input[id*=txtPincodeNumber").val();
    this.country = $("select[id*=country").val();
    this.joiningDate = $("input[id*=txtDateOfJoining").val();
    this.becomingInsiderDate = $("input[id*=txtDateOfBecomingInsider").val();
    this.department = new Department();
    this.location = new Location();
    this.designation = new Designation();
    this.category = new Category();
    this.dinNumber = $("input[id*=txtDinNumber").val();
    this.institutionName = $("textarea[id*=txtInstitution").val();
    this.stream = $("input[id*=txtStream").val();
    this.employerDetails = $("input[id*=txtEmployer").val();
    this.employeeId = $("input[id*=txtEmployeeId").val();
    this.USER_EMAIL = $("input[id*=txtEmailId").val();
    this.PersonalEmail = $("input[id*=txtPersonalEmail").val();
    this.Ssn = $("input[id*=txtSsn").val();

    //alert("Yes checked=" + $("#YesMarried").prop("checked"));
    //alert("No checked=" + $("#NoMarried").prop("checked"));

    if ($("#YesMarried").prop("checked")) {
        this.IsMarried = "Yes";
        isMarried = "Yes";
    }
    else if ($("#NoMarried").prop("checked")) {
        this.IsMarried = "No";
        isMarried = "No";
    }
    //alert("isMarried=" + isMarried);
    this.isFinalDeclared = false;
    this.lstRelative = new Array();
    this.lstInitialHoldingDetail = new Array();
    this.lstPhysicalHoldingDetail = new Array();
    this.lstDematAccount = new Array();
    this.lstTransactionHistory = new Array();
    this.email = new Email();
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
function Department() {
    this.DEPARTMENT_ID = $("select[id*=ddlDepartment]").val() == null ? 0 : ($("select[id*=ddlDepartment]").val().trim() == "" ? 0 : $("select[id*=ddlDepartment]").val());
}
function Designation() {
    this.DESIGNATION_ID = $("select[id*=ddlDesignation]").val() == null ? 0 : ($("select[id*=ddlDesignation]").val().trim() == "" ? 0 : $("select[id*=ddlDesignation]").val());
}
function Location() {

    this.locationId = $("select[id*=ddlLocation]").val() == null ? 0 : ($("select[id*=ddlLocation]").val().trim() == "" ? 0 : $("select[id*=ddlLocation]").val());
}
function Category() {
    this.ID = $("select[id*=ddlCategory]").val() == null ? 0 : ($("select[id*=ddlCategory]").val().trim() == "" ? 0 : $("select[id*=ddlCategory]").val());
    this.subCategory = new SubCategory();
}
function SubCategory() {
    this.ID = $("select[id*= ddlSubCategory]").val() == null ? 0 : ($("select[id*=ddlSubCategory]").val().trim() == "" ? 0 : $("select[id*=ddlSubCategory]").val());
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
function fnRaiseRequest() {
    $("#txtReqReason").val('');
    $("#mdlRequest").modal('show');
}
function fnSubmitRequest() {
    var sReasons = $("#txtReqReason").val();
    //alert("sReasons=" + sReasons);

    if (sReasons == "" || sReasons == null) {
        alert("Please enter reason for request");
    }
    else {
        var token = $("#TokenKey").val();
        $("#Loader").show();
        var webUrl = uri + "/api/Transaction/SaveModifyRequest";
        $.ajax({
            url: webUrl,
            headers: {
                'TokenKeyH': token,
            },
            type: "POST",
            data: JSON.stringify({ Reason: sReasons }),
            async: false,
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            success: function (msg) {
                $("#Loader").hide();
                if (msg.StatusFl == true) {
                    alert('Request for editing the disclosure has been forworded to Compliance Office, once he/she approves the request you can edit your disclosure');
                    window.location = 'dashboard.aspx';
                    //$("#mdlRequest").modal('show');
                    //$("#spnRequest").hide();
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
                        var panNumber = $("input[id*=txtPermanentAccountNumber]").val();
                        if (!(panNumber == "" || panNumber == null)) {
                            $("select[id*='ddlResidentType']").prop('disabled', true);
                        }
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
function setPersonalInformation(msg) {
    //if (msg.User.panNumber != "" && msg.User.panNumber != null) {
    //    //alert("In PAN not blank");
    //    $("select[id*=ddlResidentType]").attr("disabled", "disabled");
    //    $("select[id*=country]").attr("disabled", "disabled");
    //}
    //else {
    //    $("select[id*=ddlResidentType]").removeAttr("disabled");
    //    $("select[id*=country]").removeAttr("disabled");
    //}
    $("select[id*=ddlResidentType]").val(msg.User.residentType);
    $("select[id*=country]").val(msg.User.country);
    if (msg.User.residentType =="INDIAN_RESIDENT") {
        $("select[id*=country]").val("IN").change();
    }
    $("input[id*=txtPermanentAccountNumber").val(msg.User.panNumber);
    $("#ContentPlaceHolder1_ddlIdentificationType").val(msg.User.identificationType);
    $("#ContentPlaceHolder1_txtIdentificationNumber").val(msg.User.identificationNumber);
    $("input[id*=txtMobileNumber]").val(msg.User.USER_MOBILE);
    $("#ContentPlaceHolder1_txtAddress").val(msg.User.address);
    $("input[id*=txtPincodeNumber").val(msg.User.pinCode);

    if (msg.User.joiningDate == "" || msg.User.joiningDate == "1900-01-01") {
        $("input[id*=txtDateOfJoining").datepicker({
            format: $("input[id*=hdnJSDateFormat]").val(),
            autoclose: true
        }).val();
    }
    else {
        $("input[id*=txtDateOfJoining").datepicker({
            format: $("input[id*=hdnJSDateFormat]").val(),
            autoclose: true,
            //showOnFocus: false
        }).val(FormatDate(msg.User.joiningDate, $("input[id*=hdnDateFormat]").val())).attr('readonly', 'readonly');
    }

    if (msg.User.becomingInsiderDate == "" || msg.User.becomingInsiderDate == "1900-01-01") {
        $("input[id*=txtDateOfBecomingInsider").datepicker({
            format: $("input[id*=hdnJSDateFormat]").val(),
            autoclose: true,
        }).val();
    }
    else {
        $("input[id*=txtDateOfBecomingInsider").datepicker({
            autoclose: true,
            showOnFocus: false,
            format: $("input[id*=hdnJSDateFormat]").val()
        }).val(msg.User.becomingInsiderDate != "" ? FormatDate(msg.User.becomingInsiderDate, $("input[id*=hdnDateFormat]").val()) : "").attr('readonly', 'readonly');
    }
    
    if (msg.User.becomingInsiderDate != "") {
        //$("input[id*=txtDateOfBecomingInsider").prop('disabled', 'disabled');
    }
    if (msg.User.designation != null) {
        $("select[id*=ddlDesignation]").val(msg.User.designation.DESIGNATION_ID).change().trigger('change');
    }
    if (msg.User.department != null) {
        $("select[id*=ddlDepartment]").val(msg.User.department.DEPARTMENT_ID).trigger('change');
    }
    if (msg.User.location != null) {
        $("select[id*=ddlLocation]").val(msg.User.location.locationId).trigger('change');
    }
    if (msg.User.category != null) {
        $("select[id*=ddlCategory]").val(msg.User.category.ID).trigger('change');
    }
    if (msg.User.category != null) {
        getAllSubCategories();
        $("select[id*=ddlSubCategory]").val(msg.User.category.subCategory.ID).trigger('change');
    }
    $("input[id*=txtDinNumber").val(msg.User.dinNumber);
    if (msg.User.category.categoryName = ! "Admin" && msg.User.category.categoryName != "Designated Person" && msg.User.category.categoryName != "Connected Person") {
        $("#dvDin").show();
    }
    else {
        $("#dvDin").hide();
        $("input[id*=txtDinNumber").val('N/A');
    }
    $("textarea[id*=txtInstitution").val(msg.User.institutionName);
    $("input[id*=txtStream").val(msg.User.stream);
    $("input[id*=txtEmployer").val(msg.User.employerDetails);
    $("input[id*=txtD_ID").val(msg.User.D_ID);

    $("input[id*=txtEmployeeId]").val(msg.User.employeeId);
    $("input[id*=txtSsn]").val(msg.User.Ssn);
    if (msg.User.IsMarried == "No") {
        $("#YesMarried").prop("checked", false);
        $("#NoMarried").prop("checked", true);
        isMarried = "No";
    }
    else if (msg.User.IsMarried == "Yes") {
        $("#YesMarried").prop("checked", true);
        $("#NoMarried").prop("checked", false);
        isMarried = "Yes";
    }
    else {
        $("#YesMarried").prop("checked", false);
        $("#NoMarried").prop("checked", false);
    }

    if (msg.User.SpouseCnt == 0) {
        $("#dvMaritalStatus").show();
    }
}
function fnGetDPPersonalDetails() {
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/GetPersonDetails";
    $.ajax({
        url: webUrl,
        type: "GET",
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            //debugger;
            if (msg.StatusFl == true) {
                $("#Loader").hide();
                /* Personal Information */
                isMarried = msg.User.IsMarried;
                setPersonalInformation(msg);

                /* Relative Info */
                //setRelativeInformation(msg);

                /* Demat Info */
                //setDematInformation(msg);

                /* Initial Holding */

                //setInitialHoldingDetail(msg);
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
function fnGetDPRelativeDetails() {
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/GetRelativeDetails";
    $.ajax({
        url: webUrl,
        type: "GET",
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            //debugger;
            if (msg.StatusFl == true) {
                $("#Loader").hide();
                /* Personal Information */
                //isMarried = msg.User.IsMarried;
                setRelativeInformation(msg);
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
function fnGetDPDematDetails() {
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/GetDematDetails";
    $.ajax({
        url: webUrl,
        type: "GET",
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            //debugger;
            if (msg.StatusFl == true) {
                $("#Loader").hide();
                /* Personal Information */
                isMarried = msg.User.IsMarried;
                setDematInformation(msg);
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
function fnGetDPHoldingDetails(sInvoked) {
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/GetHoldingDetails";
    $.ajax({
        url: webUrl,
        type: "GET",
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            //debugger;
            if (msg.StatusFl == true) {
                $("#Loader").hide();
                /* Personal Information */
                isMarried = msg.User.IsMarried;
                setInitialHoldingDetail(msg);
                if (sInvoked == "FromPersonal") {
                    fnGenerateDynamicBodyOfInitialHolding();
                }
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