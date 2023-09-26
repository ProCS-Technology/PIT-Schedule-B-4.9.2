var isEdit = false;
var editableElement = null;

var isEditDemat = false;
var editableElementDemat = null;

var isEditInitialHoldingDeclarationDetail = false;
var editableElementInitialDeclarationDetail = null;
var dematAccountNumberGlobal = '';

var deleteRelativeDetailElement = null;
var deleteDematDetailElement = null;
var deleteInitialHoldingDetailElement = null;

jQuery(document).ready(function () {
    $("#Loader").hide();
    FormWizard.init();
    getAllDepartments();
    getAllDesignations();
    getLoggedInUserInformation();
    getAllLocations();
    getAllCategories();
    getAllRelations();
    getAllRestrictedCompanies();
    fnGetTypeOfSecurity();
    if ($("#ContentPlaceHolder1_txtCompanyName").val() == "Spencer Retail Ltd.") {
        $("#divSubCategory").hide();
    }

    $("#ddlResidentType").on('change', function () {
        if ($(this).val() == 'NRI') {
            $("#country").prop('disabled', false);
            $("#country").val('');
            $("#txtPermanentAccountNumber").prop('disabled', false);
            $("#txtPermanentAccountNumber").val('');
        }
        else if ($(this).val() == 'FOREIGN_NATIONAL') {
            $("#country").prop('disabled', false);
            $("#country").val('');
            $("#txtPermanentAccountNumber").prop('disabled', true);
            $("#txtPermanentAccountNumber").val('NOT APPLICABLE');
        }
        else if ($(this).val() == 'INDIAN_RESIDENT') {
            $("#country").prop('disabled', true);
            $("#country").val('IN');
            $("#txtPermanentAccountNumber").prop('disabled', false);
            if ($("#txtPermanentAccountNumber").val() == 'NOT APPLICABLE') {
                $("#txtPermanentAccountNumber").val('');
            }
        }
    })
    $("#ddlCategory").on('change', function () {
        getAllSubCategories();
    })
    $("#txtSameAddressRelation").on('click', function () {
        if ($(this).prop('checked')) {
            $("#txtAddressRelation").val($("#txtAddress").val());
        }
        else {
            $("#txtAddressRelation").val('');
        }
    })
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
    $("#ddlDematAccNo").on('change', function () {
        if ($(this).val() !== undefined && $(this).val() !== null && $(this).val().trim() !== "") {
            getDematAccountInfoById($(this).val());
        }
        else {
            $("#txtTradingMemId").val('');
        }
    })
    fnGetApprover();
    fnGetTransactionalInfo();
    fnGetPolicy();
    $("#inAcceptTermsAndConditions").on("click", function () {
        if ($(this).prop("checked")) {
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
    $("#ddlRelation").on('change', function () {
        if ($(this).val() == "0") {
            $("#txtName").val("Not Applicable");
            $("#txtPAN").val("Not Applicable");
            $("#ddlIdentificationTypeRelation").val("NotApplicable");
            $("#txtIdentificationNumberRelation").val("Not Applicable");
            $("#txtAddressRelation").val("Not Applicable");
            $("#txtPhoneRelation").val("0");
            $("#ddlStatusRelation").val("NotApplicable");
            $("#remarks").val('');
            $("#txtSameAddressRelation").attr("disabled", "disabled");
            $("#txtName").attr("disabled", "disabled");
            $("#txtPAN").attr("disabled", "disabled");
            $("#ddlIdentificationTypeRelation").attr("disabled", "disabled");
            $("#txtIdentificationNumberRelation").attr("disabled", "disabled");
            $("#txtAddressRelation").attr("disabled", "disabled");
            $("#txtPhoneRelation").attr("disabled", "disabled");
            $("#ddlStatusRelation").attr("disabled", "disabled");

            if ($("#ddlRelation option:selected").text() == "Material Financial Relation") {
                $("#spRelativeRemarks").hide();
                $("#spMaterialFinancialRelationship").show();
            }
            else {
                $("#spMaterialFinancialRelationship").hide();
                $("#spRelativeRemarks").show();
            }
        }
        else {
            $("#txtName").val("");
            $("#txtPAN").val("");
            $("#ddlIdentificationTypeRelation").val("");
            $("#txtIdentificationNumberRelation").val("");
            $("#txtAddressRelation").val("");
            $("#txtPhoneRelation").val("");
            $("#ddlStatusRelation").val("");
            $("#remarks").val('');
            $("#txtSameAddressRelation").prop("disabled", false);
            $("#txtName").prop("disabled", false);
            $("#txtPAN").prop("disabled", false);
            $("#ddlIdentificationTypeRelation").prop("disabled", false);
            $("#txtIdentificationNumberRelation").prop("disabled", false);
            $("#txtAddressRelation").prop("disabled", false);
            $("#txtPhoneRelation").prop("disabled", false);
            $("#ddlStatusRelation").prop("disabled", false);

            if ($("#ddlRelation option:selected").text() == "Material Financial Relation") {
                $("#spRelativeRemarks").hide();
                $("#spMaterialFinancialRelationship").show();
            }
            else {
                $("#spMaterialFinancialRelationship").hide();
                $("#spRelativeRemarks").show();
            }

        }
    })

    $("#ddlDematAccountDetailsFor").on('change', function () {
        if ($(this).val() == "-1") {
            $("#ddlDepositoryName").val("NotApplicable");
            $("#txtDepositoryParticipantName").val("Not Applicable");
            $("#txtDepositoryParticipantId").val("0");
            $("#txtClientId").val("0");
            $("#txtDematAccountNumber").val("Not Applicable");
            $("#txtTradingMemberId").val("Not Applicable");
            $("#ddlStatusDemat").val("NotApplicable");
            $("#ddlDepositoryName").attr("disabled", "disabled");
            $("#txtDepositoryParticipantName").attr("disabled", "disabled");
            $("#txtDepositoryParticipantId").attr("disabled", "disabled");
            $("#txtClientId").attr("disabled", "disabled");
            $("#txtTradingMemberId").attr("disabled", "disabled");
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

    $("#ddlRestrictedCompanies").on('change', function () {
        if ($(this).val() == "0") {
            $("#ddlSecurityType").val("0");
            $("#ddlFor").val("-1");
            $("#txtPan").val("Not Applicable");
            $("#ddlDematAccNo").html('<option value=""></option><option value="0">Not Applicable</option>');
            $("#ddlDematAccNo").val("0");
            $("#txtTradingMemId").val("Not Applicable");
            $("#txtNumberOfSecurities").val("0");
            $("#ddlSecurityType").attr("disabled", "disabled");
            $("#ddlFor").attr("disabled", "disabled");
            $("#ddlDematAccNo").attr("disabled", "disabled");
            $("#txtNumberOfSecurities").attr("disabled", "disabled");
        }
        else {
            $("#ddlSecurityType").val("");
            $("#ddlFor").val("");
            $("#txtPan").val("");
            $("#ddlDematAccNo").html('');
            $("#ddlDematAccNo").val("");
            $("#txtTradingMemId").val("");
            $("#txtNumberOfSecurities").val("");
            $("#ddlSecurityType").prop("disabled", false);
            $("#ddlFor").prop("disabled", false);
            $("#ddlDematAccNo").prop("disabled", false);
            $("#txtNumberOfSecurities").prop("disabled", false);
        }
    })

    $("#ddlDepositoryName").on('change', function () {
        if ($(this).val() == "NSDL") {
            $('#spNsdlLabel').html("IN");
            $('#spNsdlDematLabel').html("IN");
        }
        else {
            $('#spNsdlLabel').html("");
            $('#spNsdlDematLabel').html("");
        }
    })
});

function isJson(str) {
    try {
        JSON.parse(str);
    } catch (e) {
        return false;
    }
    return true;
}

var FormWizard = function () {


    return {
        //main function to initiate the module
        init: function () {
            if (!jQuery().bootstrapWizard) {
                return;
            }

            function format(state) {
                if (!state.id) return state.text; // optgroup
                return "<img class='flag' src='../assets/global/img/flags/" + state.id.toLowerCase() + ".png'/>&nbsp;&nbsp;" + state.text;
            }

            $("#country_list").select2({
                placeholder: "Select",
                allowClear: true,
                formatResult: format,
                width: 'auto',
                formatSelection: format,
                escapeMarkup: function (m) {
                    return m;
                }
            });

            var form = $('#submit_form');
            var error = $('.alert-danger', form);
            var success = $('.alert-success', form);

            form.validate({
                doNotHideMessage: true, //this option enables to show the error/success messages on tab switch.
                errorElement: 'span', //default input error message container
                errorClass: 'help-block help-block-error', // default input error message class
                focusInvalid: false, // do not focus the last invalid input
                rules: {
                    //account
                    //username: {
                    //    minlength: 5,
                    //    required: true
                    //},
                    //password: {
                    //    minlength: 5,
                    //    required: true
                    //},
                    //rpassword: {
                    //    minlength: 5,
                    //    required: true,
                    //    equalTo: "#submit_form_password"
                    //},
                    //profile
                    resident_type: {
                        required: true
                    },
                    permanent_account_number: {
                        minlength: 10,
                        maxlength: 15,
                        required: true
                    },
                    //identification_type: {
                    //    required: true
                    //},
                    //identification_number: {
                    //    required: true
                    //},
                    employee_id: { required: true },
                    ssn: {required:true},
                    mobile_number: {
                        digits: true,
                        minlength: 10,
                        maxlength: 10
                        //    required: true
                    },
                    address: {
                        required: true
                    },
                    country: {
                        required: true
                    },
                    pincode_number: {
                        digits: true,
                        minlength: 6,
                        maxlength: 10,
                        required: true
                    },
                    date_Of_Joining: {
                        minlength: 6,
                        maxlength: 10,
                        required: true
                    },
                    //date_Of_Becoming_Insider: {
                    //    minlength: 6,
                    //    maxlength: 10,
                    //    required: true
                    //},
                    fullname: {
                        required: true
                    },
                    //email: {
                    //    required: true,
                    //    email: true
                    //},
                    phone: {
                        required: true
                    },
                    gender: {
                        required: true
                    },

                    city: {
                        required: true
                    },

                    //payment
                    card_name: {
                        required: true
                    },
                    card_number: {
                        minlength: 16,
                        maxlength: 16,
                        required: true
                    },
                    card_cvc: {
                        digits: true,
                        required: true,
                        minlength: 3,
                        maxlength: 4
                    },
                    card_expiry_date: {
                        required: true
                    },
                    'payment[]': {
                        required: true,
                        minlength: 1
                    },
                    name_of_educational_institution_of_graduation: {
                        required: true
                    }/*,
                    stream_of_graduation: {
                        required: true
                    }*/
                },

                messages: { // custom messages for radio buttons and checkboxes
                    'payment[]': {
                        required: "Please select at least one option",
                        minlength: jQuery.validator.format("Please select at least one option")
                    }
                },

                errorPlacement: function (error, element) { // render error placement for each input type
                    if (element.attr("name") == "gender") { // for uniform radio buttons, insert the after the given container
                        error.insertAfter("#form_gender_error");
                    } else if (element.attr("name") == "payment[]") { // for uniform checkboxes, insert the after the given container
                        error.insertAfter("#form_payment_error");
                    } else {
                        error.insertAfter(element); // for other inputs, just perform default behavior
                    }
                },

                invalidHandler: function (event, validator) { //display error alert on form submit   
                    success.hide();
                    error.show();
                    App.scrollTo(error, -200);
                },

                highlight: function (element) { // hightlight error inputs
                    $(element)
                        .closest('.form-group').removeClass('has-success').addClass('has-error'); // set error class to the control group
                },

                unhighlight: function (element) { // revert the change done by hightlight
                    $(element)
                        .closest('.form-group').removeClass('has-error'); // set error class to the control group
                },

                success: function (label) {
                    if (label.attr("for") == "gender" || label.attr("for") == "payment[]") { // for checkboxes and radio buttons, no need to show OK icon
                        label
                            .closest('.form-group').removeClass('has-error').addClass('has-success');
                        label.remove(); // remove error label here
                    } else { // display success icon for other inputs
                        label
                            .addClass('valid') // mark the current input as valid and display OK icon
                            .closest('.form-group').removeClass('has-error').addClass('has-success'); // set success class to the control group
                    }
                },

                submitHandler: function (form) {
                    success.show();
                    error.hide();
                    form[0].submit();
                    //add here some ajax code to submit your form or just call form.submit() if you want to submit the form without ajax
                }

            });

            var displayConfirm = function () {
                $('#tab4 .form-control-static', form).each(function () {
                    var input = $('[name="' + $(this).attr("data-display") + '"]', form);
                    if (input.is(":radio")) {
                        input = $('[name="' + $(this).attr("data-display") + '"]:checked', form);
                    }
                    if (input.is(":text") || input.is("textarea")) {
                        $(this).html(input.val());
                    } else if (input.is("select")) {
                        $(this).html(input.find('option:selected').text());
                    } else if (input.is(":radio") && input.is(":checked")) {
                        $(this).html(input.attr("data-title"));
                    } else if ($(this).attr("data-display") == 'payment[]') {
                        var payment = [];
                        $('[name="payment[]"]:checked', form).each(function () {
                            payment.push($(this).attr('data-title'));
                        });
                        $(this).html(payment.join("<br>"));
                    }
                });
            }

            var handleTitle = function (tab, navigation, index) {
                var total = navigation.find('li').length;
                var current = index + 1;
                // set wizard title
                $('.step-title', $('#form_wizard_1')).text('Step ' + (index + 1) + ' of ' + total);
                // set done steps
                jQuery('li', $('#form_wizard_1')).removeClass("done");
                var li_list = navigation.find('li');
                for (var i = 0; i < index; i++) {
                    jQuery(li_list[i]).addClass("done");
                }

                if (current == 1) {
                    $('#form_wizard_1').find('.button-previous').hide();
                } else {
                    $('#form_wizard_1').find('.button-previous').show();
                }

                if (current >= total) {
                    $('#form_wizard_1').find('.button-next').hide();
                    $('#form_wizard_1').find('.button-submit').show();
                    displayConfirm();
                } else {
                    $('#form_wizard_1').find('.button-next').show();
                    $('#form_wizard_1').find('.button-submit').hide();
                }
                App.scrollTo($('.page-title'));
            }

            // default form wizard
            $('#form_wizard_1').bootstrapWizard({
                'nextSelector': '.button-next',
                'previousSelector': '.button-previous',
                onTabClick: function (tab, navigation, index, clickedIndex) {
                    return false;

                    success.hide();
                    error.hide();
                    if (form.valid() == false) {
                        return false;
                    }

                    handleTitle(tab, navigation, clickedIndex);
                },
                onNext: function (tab, navigation, index) {
                    success.hide();
                    error.hide();

                    if (form.valid() == false) {
                        return false;
                    }

                    handleTitle(tab, navigation, index);
                },
                onPrevious: function (tab, navigation, index) {
                    success.hide();
                    error.hide();

                    handleTitle(tab, navigation, index);
                },
                onTabShow: function (tab, navigation, index) {
                    var total = navigation.find('li').length;
                    var current = index + 1;
                    var $percent = (current / total) * 100;
                    $('#form_wizard_1').find('.progress-bar').css({
                        width: $percent + '%'
                    });
                }
            });

            $('#form_wizard_1').find('.button-previous').hide();
            $('#form_wizard_1 .button-submit').click(function () {
                // alert('Your declaration has been submitted');
            }).hide();

            //apply validation on select2 dropdown value change, this only needed for chosen dropdown integration.
            $('#country_list', form).change(function () {
                form.validate().element($(this)); //revalidate the chosen dropdown value and show error or success message for the input
            });
        }

    };

}();

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
                $("#txtUserName").val(msg.User.USER_NM);
                $("#txtEmailId").val(msg.User.USER_EMAIL);
                $("#txtRole").val(msg.User.userRole.ROLE_NM);
                $("#tdMailFromUserName").html(msg.User.USER_NM);
                $("#tdMailFrom").html(msg.User.USER_EMAIL);
                $("#txtPermanentAccountNumber").val(msg.User.panNumber);
                $("#txtMobileNumber").val(msg.User.USER_MOBILE);
                $("#ddlDepartment").val(msg.User.department.DEPARTMENT_ID);
                $("#ddlDesignation").val(msg.User.designation.DESIGNATION_ID);
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
    debugger;
    if ($("#tab1").hasClass('active')) {

    }
    else if ($("#tab2").hasClass('active')) {
        savePersonInformation();
    }
    else if ($("#tab3").hasClass('active')) {
        if ($("#tbdRelativeList").children().length > 0) {
            saveRelativeDetails();
            fnGetRelativeDetail();
        }
        else {
            $('.button-previous').click();
            //var result = "";
            //result += "<option value=''></option>";
            //result += "<option value='0'>Self</option>";
            //$("#ddlDematAccountDetailsFor").html(result);
            //$("#ddlFor").html(result);
            alert('In case you do not have any "Relative" to declare, Kindly elect and Add "Not Applicable" from the options provided in Add button');
        }
    }
    else if ($("#tab4").hasClass('active')) {
        if ($("#tbdDematList").children().length > 0) {
            saveDematAccounts();
        }
        else {
            $('.button-previous').click();
            alert('In case you do not have any "Demat Account" to declare, Kindly elect and Add "Not Applicable" from the options provided in Add button');
        }

    }
    else {
        if ($("#tbdInitialDeclaration").children().length > 0) {
            var tempViewer = $("#viewer").data('ejPdfViewer');
            tempViewer.updateViewerSize()
            initialHoldings();
            fnGenerateDynamicBodyOfInitialHolding();
        }
        else {
            $('.button-previous').click();
            alert('In case you do not have any "Initial Holding" to declare, Kindly elect and Add "Not Applicable" from the options provided in Add button');
        }
    }
}

function savePersonInformation() {
    $("#Loader").show();
    var webUrl = uri + "/api/Transaction/SavePersonalInformation";
    $.ajax({
        url: webUrl,
        type: "POST",
        data: JSON.stringify(new PersonalInformation()),
        async: false,
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                $("#txtD_ID").val(msg.User.D_ID);
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

function saveRelativeDetails() {
    debugger;
    var personalInformation = new PersonalInformation();
    for (var index = 0; index < $("#tbdRelativeList").children().length; index++) {
        var obj = new Relative();
        obj.ID = $($($("#tbdRelativeList").children()[index]).children()[0]).html();
        obj.relativeName = $($($("#tbdRelativeList").children()[index]).children()[1]).html();
        obj.relation.RELATION_ID = $($($("#tbdRelativeList").children()[index]).children()[2]).html();
        obj.relation.RELATION_NM = $($($("#tbdRelativeList").children()[index]).children()[3]).html();
        obj.panNumber = $($($("#tbdRelativeList").children()[index]).children()[4]).html();
        obj.identificationType = $($($("#tbdRelativeList").children()[index]).children()[5]).html();
        obj.identificationNumber = $($($("#tbdRelativeList").children()[index]).children()[6]).html();
        obj.address = $($($("#tbdRelativeList").children()[index]).children()[7]).html();
        obj.mobile = $($($("#tbdRelativeList").children()[index]).children()[8]).html();
        obj.status = $($($("#tbdRelativeList").children()[index]).children()[9]).html();
        obj.remarks = $($($("#tbdRelativeList").children()[index]).children()[10]).html();
        personalInformation.lstRelative.push(obj);
    }

    $("#Loader").show();
    var webUrl = uri + "/api/Transaction/SaveRelativeDetails";
    $.ajax({
        url: webUrl,
        type: "POST",
        data: JSON.stringify(personalInformation),
        async: false,
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                setRelativeInformation(msg);
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

function saveDematAccounts() {
    var personalInformation = new PersonalInformation();


    for (var index = 0; index < $("#tbdDematList").children().length; index++) {
        var obj = new DematAccount();
        var relative = new Relative();
        obj.ID = $($($("#tbdDematList").children()[index]).children()[0]).html();
        relative.ID = $($($("#tbdDematList").children()[index]).children()[1]).html();
        relative.relativeName = $($($("#tbdDematList").children()[index]).children()[2]).html();
        obj.depositoryName = $($($("#tbdDematList").children()[index]).children()[3]).html();
        obj.clientId = $($($("#tbdDematList").children()[index]).children()[4]).html();
        obj.depositoryParticipantName = $($($("#tbdDematList").children()[index]).children()[5]).html();
        obj.depositoryParticipantId = $($($("#tbdDematList").children()[index]).children()[6]).html();
        obj.tradingMemberId = $($($("#tbdDematList").children()[index]).children()[7]).html();
        obj.accountNo = $($($("#tbdDematList").children()[index]).children()[8]).html();
        obj.status = $($($("#tbdDematList").children()[index]).children()[9]).html();
        relative.lstDematAccount.push(obj);
        personalInformation.lstRelative.push(relative);
    }


    $("#Loader").show();
    var webUrl = uri + "/api/Transaction/SaveRelativeDematDetails";
    $.ajax({
        url: webUrl,
        type: "POST",
        data: JSON.stringify(personalInformation),
        async: false,
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                setDematInformation(msg);
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

function initialHoldings() {
    var userPersonalInformation = new PersonalInformation();
    for (var index = 0; index < $("#tbdInitialDeclaration").children().length; index++) {
        var obj = new InitialHoldingDetail();
        obj.ID = $($($("#tbdInitialDeclaration").children()[index]).children()[12]).html();
        obj.restrictedCompany.ID = $($($("#tbdInitialDeclaration").children()[index]).children()[0]).html();
        obj.securityType = $($($("#tbdInitialDeclaration").children()[index]).children()[2]).html();
        obj.relative.ID = $($($("#tbdInitialDeclaration").children()[index]).children()[4]).html();
        obj.dematAccount.accountNo = $($($("#tbdInitialDeclaration").children()[index]).children()[7]).html();
        obj.noOfSecurities = $($($("#tbdInitialDeclaration").children()[index]).children()[8]).html();
        obj.relative.panNumber = $($($("#tbdInitialDeclaration").children()[index]).children()[9]).html();
        obj.dematAccount.tradingMemberId = $($($("#tbdInitialDeclaration").children()[index]).children()[10]).html();

        userPersonalInformation.lstInitialHoldingDetail.push(obj);
    }


    $("#Loader").show();
    var webUrl = uri + "/api/Transaction/SaveInsiderHoldingDeclarationDetail";
    $.ajax({
        url: webUrl,
        type: "POST",
        data: JSON.stringify(userPersonalInformation),
        async: false,
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                setInitialHoldingDetail(msg);
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

function fnSaveConfirmation() {

}

function PersonalInformation() {
    this.ID = 0;
    this.D_ID = $("#txtD_ID").val();
    this.residentType = $("#ddlResidentType").val();
    this.panNumber = $("#txtPermanentAccountNumber").val();
    this.identificationType = $("#ddlIdentificationType").val();
    this.identificationNumber = $("#txtIdentificationNumber").val();
    this.USER_MOBILE = $("#txtMobileNumber").val();
    this.address = $("#txtAddress").val();
    this.pinCode = $("#txtPincodeNumber").val();
    this.country = $("#country").val();
    this.joiningDate = $("#txtDateOfJoining").val();
    this.becomingInsiderDate = $("#txtDateOfBecomingInsider").val();
    this.department = new Department();
    this.location = new Location();
    this.designation = new Designation();
    this.category = new Category();
    this.dinNumber = $("#txtDinNumber").val();
    this.institutionName = $("#txtInstitution").val();
    this.stream = $("#txtStream").val();
    this.employerDetails = $("#txtEmployer").val();
    this.employeeId = $("#txtEmployeeId").val();
    this.ssn = $("#txtSsn").val();

    this.isFinalDeclared = false;
    this.lstRelative = new Array();
    this.lstInitialHoldingDetail = new Array();
    this.email = new Email();
}

function Department() {
    this.DEPARTMENT_ID = $("#ddlDepartment").val() == null ? 0 : ($("#ddlDepartment").val().trim() == "" ? 0 : $("#ddlDepartment").val());
}

function Designation() {
    this.DESIGNATION_ID = $("#ddlDesignation").val() == null ? 0 : ($("#ddlDesignation").val().trim() == "" ? 0 : $("#ddlDesignation").val());
}

function Location() {
    this.locationId = $("#ddlLocation").val() == null ? 0 : ($("#ddlLocation").val().trim() == "" ? 0 : $("#ddlLocation").val());
}

function Category() {
    this.ID = $("#ddlCategory").val() == null ? 0 : ($("#ddlCategory").val().trim() == "" ? 0 : $("#ddlCategory").val());
    this.subCategory = new SubCategory();
}

function SubCategory() {
    this.ID = $("#ddlSubCategory").val() == null ? 0 : ($("#ddlSubCategory").val().trim() == "" ? 0 : $("#ddlSubCategory").val());
}

function Relative() {
    this.ID = 0;
    this.relativeName = $("#txtName").val() == null ? "" : ($("#txtName").val().trim() == "" ? "" : $("#txtName").val());
    this.relation = new Relation();
    this.panNumber = $("#txtPAN").val() == null ? "" : ($("#txtPAN").val().trim() == "" ? "" : $("#txtPAN").val());
    this.identificationType = $("#ddlIdentificationTypeRelation").val() == null ? "" : ($("#ddlIdentificationTypeRelation").val().trim() == "" ? "" : $("#ddlIdentificationTypeRelation").val());
    this.identificationNumber = $("#txtIdentificationNumberRelation").val() == null ? "" : ($("#txtIdentificationNumberRelation").val().trim() == "" ? "" : $("#txtIdentificationNumberRelation").val());
    this.address = $("#txtAddressRelation").val() == null ? "" : ($("#txtAddressRelation").val().trim() == "" ? "" : $("#txtAddressRelation").val());
    this.mobile = $("#txtPhoneRelation").val() == null ? "" : ($("#txtPhoneRelation").val().trim() == "" ? "" : $("#txtPhoneRelation").val());
    this.status = $("#ddlStatusRelation").val() == null ? "" : ($("#ddlStatusRelation").val().trim() == "" ? "" : $("#ddlStatusRelation").val());
    this.remarks = $("#remarks").val() == null ? "" : ($("#remarks").val().trim() == "" ? "" : $("#remarks").val());
    this.lstDematAccount = new Array();
}

function Relation() {
    this.RELATION_ID = $("#ddlRelation").val() == null ? 0 : ($("#ddlRelation").val().trim() == "" ? 0 : $("#ddlRelation").val());
    this.RELATION_NM = $("#ddlRelation option:selected").text();
}

function DematAccount() {
    this.ID = 0;
    this.depositoryName = $("#ddlDepositoryName").val() == null ? 0 : ($("#ddlDepositoryName").val().trim() == "" ? 0 : $("#ddlDepositoryName").val());
    this.clientId = $("#txtClientId").val() == null ? 0 : ($("#txtClientId").val().trim() == "" ? 0 : $("#txtClientId").val());
    this.depositoryParticipantName = $("#txtDepositoryParticipantName").val() == null ? "" : ($("#txtDepositoryParticipantName").val().trim() == "" ? "" : $("#txtDepositoryParticipantName").val());
    this.depositoryParticipantId = $("#txtDepositoryParticipantId").val() == null ? 0 : ($("#txtDepositoryParticipantId").val().trim() == "" ? 0 : $("#txtDepositoryParticipantId").val());
    this.accountNo = $("#txtDematAccountNumber").val() == null ? 0 : ($("#txtDematAccountNumber").val().trim() == "" ? 0 : $("#txtDematAccountNumber").val());
    if (this.depositoryName == "NSDL") {
        this.depositoryParticipantId = "IN" + this.depositoryParticipantId;
        this.accountNo = "IN" + this.accountNo;
    }
    else {
        this.depositoryParticipantId = this.depositoryParticipantId;
        this.accountNo = this.accountNo;
    }
    this.tradingMemberId = $("#txtTradingMemberId").val() == null ? 0 : ($("#txtTradingMemberId").val().trim() == "" ? 0 : $("#txtTradingMemberId").val());

    this.status = $("#ddlStatusDemat").val() == null ? 0 : ($("#ddlStatusDemat").val().trim() == "" ? 0 : $("#ddlStatusDemat").val());
}

function InitialHoldingDetail() {
    this.ID = 0;
    this.restrictedCompany = new RestrictedCompanies();
    this.securityType = $("#ddlSecurityType").val() == null ? 0 : ($("#ddlSecurityType").val().trim() == "" ? 0 : $("#ddlSecurityType").val());
    this.relative = new Relative();
    this.dematAccount = new DematAccount();
    this.noOfSecurities = $("#txtNumberOfSecurities").val() == null ? 0 : ($("#txtNumberOfSecurities").val().trim() == "" ? 0 : $("#txtNumberOfSecurities").val());
}

function RestrictedCompanies() {
    this.ID = $("#ddlRestrictedCompanies").val() == null ? 0 : ($("#ddlRestrictedCompanies").val().trim() == "" ? 0 : $("#ddlRestrictedCompanies").val());
    this.companyName = $("#ddlRestrictedCompanies option:selected").text();
}

function Email() {
    this.id = 0;
    this.mailFromUserName = $("#tdMailFromUserName").html();
    this.mailFrom = $("#tdMailFrom").html();
    this.mailToUserName = $("#tdMailToUserName").html();
    this.mailTo = $("#tdMailTo").html();
    this.subject = $("#tdSubject").html();
    this.body = $('<div>').append($("#templateBodyInitialHoldingDeclaration").closest('table').attr("border", 1).clone()).html();
}

function getAllDepartments() {
    $("#Loader").show();
    var webUrl = uri + "/api/Department/GetDepartmentList";
    $.ajax({
        url: webUrl,
        type: "POST",
        data: JSON.stringify({
            COMPANY_ID: 0
        }),
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
                for (var index = 0; index < msg.DepartmentList.length; index++) {
                    str += '<option value="' + msg.DepartmentList[index].DEPARTMENT_ID + '">' + msg.DepartmentList[index].DEPARTMENT_NM + '</option>';
                }
                $("#ddlDepartment").append(str);
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
        error: function (response) {
            $("#Loader").hide();
            alert(response.status + ' ' + response.statusText);
        }
    })
}

function getAllDesignations() {
    $("#Loader").show();
    var webUrl = uri + "/api/DesignationIT/GetDesignationList";
    $.ajax({
        url: webUrl,
        type: "POST",
        data: JSON.stringify({
            COMPANY_ID: 0
        }),
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
                for (var index = 0; index < msg.DesignationList.length; index++) {
                    str += '<option value="' + msg.DesignationList[index].DESIGNATION_ID + '">' + msg.DesignationList[index].DESIGNATION_NM + '</option>';
                }
                $("#ddlDesignation").append(str);
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

function getAllLocations() {
    $("#Loader").show();
    var webUrl = uri + "/api/Location/GetLocationList";
    $.ajax({
        url: webUrl,
        type: "POST",
        data: JSON.stringify({
            COMPANY_ID: 0
        }),
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
                for (var index = 0; index < msg.LocationList.length; index++) {
                    str += '<option value="' + msg.LocationList[index].locationId + '">' + msg.LocationList[index].locationName + '</option>';
                }
                $("#ddlLocation").append(str);
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

function getAllCategories() {
    $("#Loader").show();
    var webUrl = uri + "/api/Category/GetCategoryList";
    $.ajax({
        url: webUrl,
        type: "POST",
        data: JSON.stringify({
            COMPANY_ID: 0
        }),
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
                for (var index = 0; index < msg.CategoryList.length; index++) {
                    str += '<option value="' + msg.CategoryList[index].ID + '">' + msg.CategoryList[index].categoryName + '</option>';
                }
                $("#ddlCategory").append(str);
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
        error: function (response) {
            $("#Loader").hide();
            alert(response.status + ' ' + response.statusText);
        }
    })
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
                $("#ddlSubCategory").html(str);
            }
            else {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    $("#ddlSubCategory").html('');
                }
            }
        },
        error: function (response) {
            $("#Loader").hide();
            alert(response.status + ' ' + response.statusText);
        }
    })
}

function getAllRelations() {
    $("#Loader").show();
    var webUrl = uri + "/api/Relation/GetRelationList";
    $.ajax({
        url: webUrl,
        type: "POST",
        data: JSON.stringify({
            COMPANY_ID: 0
        }),
        async: false,
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            var str = "";
            str += '<option value=""></option>';
            str += '<option value="0">Not Applicable</option>';
            if (msg.StatusFl == true) {

                for (var index = 0; index < msg.RelationList.length; index++) {
                    str += '<option value="' + msg.RelationList[index].RELATION_ID + '">' + msg.RelationList[index].RELATION_NM + '</option>';
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
            $("#ddlRelation").append(str);
        },
        error: function (response) {
            $("#Loader").hide();
            alert(response.status + ' ' + response.statusText);
        }
    })
}

function fnAddRelativeDetail() {
    if (fnValidateRelativeDetail()) {
        var obj = new Relative();
        if (!isEdit) {
            var str = "";
            str += '<tr>';
            str += '<td style="display:none;">' + obj.ID + '</td>';
            str += '<td>' + obj.relativeName + '</td>';
            str += '<td style="display:none;">' + obj.relation.RELATION_ID + '</td>';
            str += '<td>' + obj.relation.RELATION_NM + '</td>';
            str += '<td>' + obj.panNumber + '</td>';
            str += '<td>' + obj.identificationType + '</td>';
            str += '<td>' + obj.identificationNumber + '</td>';
            str += '<td>' + obj.address + '</td>';
            str += '<td>' + obj.mobile + '</td>';
            str += '<td>' + obj.status + '</td>';
            str += '<td>' + obj.remarks + '</td>';
            str += '<td><a data-target="#modalAddRelativeDetail" data-toggle="modal" class="btn btn-outline dark" onclick=\"javascript:fnEditRelativeDetail();\">Edit</a>';
            str += '&nbsp;<a data-target="#modalDeleteRelativeDetail" data-toggle="modal" class="btn btn-outline dark" onclick=\"javascript:fnDeleteRelativeDetail();\">Delete</a></td>';
            str += '</tr>';
            $("#tbdRelativeList").append(str);
        }
        else {
            $(editableElement[1]).html(obj.relativeName);
            $(editableElement[2]).html(obj.relation.RELATION_ID);
            $(editableElement[3]).html(obj.relation.RELATION_NM);
            $(editableElement[4]).html(obj.panNumber);
            $(editableElement[5]).html(obj.identificationType);
            $(editableElement[6]).html(obj.identificationNumber);
            $(editableElement[7]).html(obj.address);
            $(editableElement[8]).html(obj.mobile);
            $(editableElement[9]).html(obj.status);
            $(editableElement[10]).html(obj.remarks);
        }
        fnClearFormRelativeDetail();
        $("#modalAddRelativeDetail").modal('hide');
    }
}

function fnValidateRelativeDetail() {
    var count = 0;
    if ($("#txtName").val() == null || $("#txtName").val() == undefined || $("#txtName").val().trim() == "") {
        count++;
        $("#lblName").addClass('required');
    }
    else {
        $("#lblName").removeClass('required');
    }
    if ($("#ddlRelation").val() == null || $("#ddlRelation").val() == undefined || $("#ddlRelation").val().trim() == "") {
        count++;
        $("#lblRelation").addClass('required');
    }
    else {
        $("#lblRelation").removeClass('required');
    }

    if (!$("#txtSkipPan").prop('checked')) {
        if ($("#txtPAN").val() == null || $("#txtPAN").val() == undefined || $("#txtPAN").val().trim() == "") {
            count++;
            $("#lblPAN").addClass('required');
        }
        else if (($("#txtPAN").val().length < 10 || $("#txtPAN").val().length > 10) && $("#txtPAN").val() != "Not Applicable") {
            count++;
            $("#lblPAN").addClass('required');
            alert('Please give valid pan of 10 characters.');
        }
        else {
            $("#lblPAN").removeClass('required');
        }
    }
    else {
        if ($("#ddlIdentificationTypeRelation").val() == null || $("#ddlIdentificationTypeRelation").val() == undefined || $("#ddlIdentificationTypeRelation").val().trim() == "" || $("#ddlIdentificationTypeRelation").val() == "NotApplicable") {
            count++;
            $("#lblIdentificationTypeRelation").addClass('required');
        }
        else {
            $("#lblIdentificationTypeRelation").removeClass('required');
        }
        if ($("#txtIdentificationNumberRelation").val() == null || $("#txtIdentificationNumberRelation").val() == undefined || $("#txtIdentificationNumberRelation").val().trim() == "") {
            count++;
            $("#lblIdentificationNumberRelation").addClass('required');
        }
        else {
            $("#lblIdentificationNumberRelation").removeClass('required');
        }
    }

    if ($("#txtAddressRelation").val() == null || $("#txtAddressRelation").val() == undefined || $("#txtAddressRelation").val().trim() == "") {
        count++;
        $("#lblAddressRelation").addClass('required');
    }
    else {
        $("#lblAddressRelation").removeClass('required');
    }
    if ($("#ddlStatusRelation").val() == null || $("#ddlStatusRelation").val() == undefined || $("#ddlStatusRelation").val().trim() == "") {
        count++;
        $("#lblStatusRelation").addClass('required');
    }
    else {
        $("#lblStatusRelation").removeClass('required');
    }
    if ((($("#remarks").val() == null || $("#remarks").val() == undefined || $("#remarks").val().trim() == "") && $("#ddlStatusRelation").val() == "INACTIVE") || (($("#remarks").val() == null || $("#remarks").val() == undefined || $("#remarks").val().trim() == "") && $("#ddlRelation option:selected").text() == "Material Financial Relation")) {
        count++;
        $("#lblRelativeRemarks").addClass('required');
    }
    else {
        $("#lblRelativeRemarks").removeClass('required');
    }
    if (count > 0) {
        return false;
    }
    else {
        return true;
    }
}

function fnCloseModalAddRelativeDetail() {
    fnClearFormRelativeDetail();
}

function fnRemoveClass(obj, val) {
    $("#lbl" + val + "").removeClass('required');
}

function fnClearFormRelativeDetail() {
    $("#txtName").val('');
    $("#ddlRelation").val('');
    $("#txtPAN").val('');
    $("#ddlIdentificationTypeRelation").val('');
    $("#txtIdentificationNumberRelation").val('');
    $("#txtAddressRelation").val('');
    $("#txtPhoneRelation").val('');
    $("#ddlStatusRelation").val('');
    $("#txtSameAddressRelation").prop('checked', false);
    $("#remarks").val('');
    isEdit = false;
    editableElement = null;
    $("#txtSameAddressRelation").prop("disabled", false);
    $("#txtName").prop("disabled", false);
    $("#txtPAN").prop("disabled", false);
    $("#ddlIdentificationTypeRelation").prop("disabled", false);
    $("#txtIdentificationNumberRelation").prop("disabled", false);
    $("#txtAddressRelation").prop("disabled", false);
    $("#txtPhoneRelation").prop("disabled", false);
    $("#ddlStatusRelation").prop("disabled", false);

    $("#lblRelation").removeClass('required');
    $("#lblName").removeClass('required');
    $("#lblPAN").removeClass('required');
    $("#lblIdentificationTypeRelation").removeClass('required');
    $("#lblIdentificationNumberRelation").removeClass('required');
    $("#lblAddressRelation").removeClass('required');
    $("#lblPhoneRelation").removeClass('required');
    $("#lblStatusRelation").removeClass('required');
    $("#lblRelativeRemarks").removeClass('required');

    $("#spMaterialFinancialRelationship").hide();
    $("#spRelativeRemarks").show();
    $("#txtSkipPan").prop("checked", false);
}

function fnEditRelativeDetail() {
    isEdit = true;
    var selectedTr = $(event.currentTarget).closest('tr').children();
    editableElement = selectedTr;
    var relativeName = $(selectedTr[1]).html();
    var relationId = $(selectedTr[2]).html();
    var relation = $(selectedTr[3]).html();
    var pan = $(selectedTr[4]).html();
    var identificationType = $(selectedTr[5]).html();
    var identificationNumber = $(selectedTr[6]).html();
    var address = $(selectedTr[7]).html();
    var mobile = $(selectedTr[8]).html();
    var status = $(selectedTr[9]).html();
    var remarks = $(selectedTr[10]).html();

    $("#txtName").val(relativeName);
    $("#ddlRelation").val(relationId);
    $("#txtPAN").val(pan);
    $("#ddlIdentificationTypeRelation").val(identificationType);
    $("#txtIdentificationNumberRelation").val(identificationNumber);
    $("#txtAddressRelation").val(address);
    $("#txtPhoneRelation").val(mobile);
    $("#ddlStatusRelation").val(status);
    $("#remarks").val(remarks);



    if ($("#ddlRelation").val() == "0") {
        $("#txtSameAddressRelation").attr("disabled", "disabled");
        $("#txtName").attr("disabled", "disabled");
        $("#txtPAN").attr("disabled", "disabled");
        $("#ddlIdentificationTypeRelation").attr("disabled", "disabled");
        $("#txtIdentificationNumberRelation").attr("disabled", "disabled");
        $("#txtAddressRelation").attr("disabled", "disabled");
        $("#txtPhoneRelation").attr("disabled", "disabled");
        $("#ddlStatusRelation").attr("disabled", "disabled");

        if ($("#ddlRelation option:selected").text() == "Material Financial Relation") {
            $("#spRelativeRemarks").hide();
            $("#spMaterialFinancialRelationship").show();
        }
        else {
            $("#spMaterialFinancialRelationship").hide();
            $("#spRelativeRemarks").show();
        }
    }
    else {
        $("#txtSameAddressRelation").prop("disabled", false);
        $("#txtName").prop("disabled", false);
        $("#txtPAN").prop("disabled", false);
        $("#ddlIdentificationTypeRelation").prop("disabled", false);
        $("#txtIdentificationNumberRelation").prop("disabled", false);
        $("#txtAddressRelation").prop("disabled", false);
        $("#txtPhoneRelation").prop("disabled", false);
        $("#ddlStatusRelation").prop("disabled", false);

        if ($("#ddlRelation option:selected").text() == "Material Financial Relation") {
            $("#spRelativeRemarks").hide();
            $("#spMaterialFinancialRelationship").show();
        }
        else {
            $("#spMaterialFinancialRelationship").hide();
            $("#spRelativeRemarks").show();
        }

        if ($("#txtPAN").val() == undefined || $("#txtPAN").val() == null || $("#txtPAN").val().trim() == "") {
            $("#txtPAN").prop("disabled", true);
            $("#txtSkipPan").prop("checked", true);
        }
        else {
            $("#txtPAN").prop("disabled", false);
            $("#txtSkipPan").prop("checked", false);
        }

    }
}

function fnGetRelativeDetail() {
    $("#Loader").show();
    var webUrl = uri + "/api/PreClearanceRequest/GetRelativeDetailList";
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
                result += "<option value=''></option>";
                result += "<option value='-1'>Not Applicable</option>";
                result += "<option value='0'>Self</option>";
                for (var i = 0; i < msg.RelativeDetailList.length; i++) {
                    if (msg.RelativeDetailList[i].relativeName !== "Not Applicable") {
                        result += "<option value='" + msg.RelativeDetailList[i].ID + "'>" + msg.RelativeDetailList[i].relativeName + "</option>";
                    }
                }
                $("#ddlDematAccountDetailsFor").html(result);
                $("#ddlFor").html(result);
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

function fnAddDematDetail() {
    if (fnValidateDematDetail()) {
        var relative = new Relative();
        relative.ID = $("#ddlDematAccountDetailsFor").val() == null ? 0 : ($("#ddlDematAccountDetailsFor").val().trim() == "" ? 0 : $("#ddlDematAccountDetailsFor").val());;
        relative.relativeName = $("#ddlDematAccountDetailsFor option:selected").text();
        var obj = new DematAccount();

        if (!isEditDemat) {
            var str = "";
            str += '<tr>';
            str += '<td style="display:none;">' + obj.ID + '</td>';
            str += '<td style="display:none;">' + relative.ID + '</td>';
            str += '<td>' + relative.relativeName + '</td>';
            str += '<td>' + obj.depositoryName + '</td>';
            str += '<td>' + obj.clientId + '</td>';
            str += '<td>' + obj.depositoryParticipantName + '</td>';
            str += '<td>' + obj.depositoryParticipantId + '</td>';
            str += '<td>' + obj.tradingMemberId + '</td>';
            str += '<td>' + obj.accountNo + '</td>';
            str += '<td>' + obj.status + '</td>';
            str += '<td><a data-target="#modalAddDematDetail" data-toggle="modal" class="btn btn-outline dark" onclick=\"javascript:fnEditDematDetail();\">Edit</a>';
            str += '&nbsp;<a data-target="#modalDeleteDematDetail" data-toggle="modal" class="btn btn-outline dark" onclick=\"javascript:fnDeleteDematDetail();\">Delete</a></td>';
            str += '</tr>';
            $("#tbdDematList").append(str);
        }
        else {
            $(editableElementDemat[1]).html(relative.ID);
            $(editableElementDemat[2]).html(relative.relativeName);
            $(editableElementDemat[3]).html(obj.depositoryName);
            $(editableElementDemat[4]).html(obj.clientId);
            $(editableElementDemat[5]).html(obj.depositoryParticipantName);
            $(editableElementDemat[6]).html(obj.depositoryParticipantId);
            $(editableElementDemat[7]).html(obj.tradingMemberId);
            $(editableElementDemat[8]).html(obj.accountNo);
            $(editableElementDemat[9]).html(obj.status);
        }
        fnClearFormDematDetail();
        $("#modalAddDematDetail").modal('hide');
    }
}

function fnValidateDematDetail() {
    var count = 0;
    if ($("#ddlDematAccountDetailsFor").val() == null || $("#ddlDematAccountDetailsFor").val() == undefined || $("#ddlDematAccountDetailsFor").val().trim() == "") {
        count++;
        $("#lblDematAccountDetailsFor").addClass('required');
    }
    else {
        $("#lblDematAccountDetailsFor").removeClass('required');
    }
    if ($("#ddlDepositoryName").val() == null || $("#ddlDepositoryName").val() == undefined || $("#ddlDepositoryName").val().trim() == "") {
        count++;
        $("#lblDepositoryName").addClass('required');
    }
    else {
        $("#lblDepositoryName").removeClass('required');
    }
    if ($("#txtClientId").val() == null || $("#txtClientId").val() == undefined || $("#txtClientId").val().trim() == "") {
        count++;
        $("#lblClientId").addClass('required');
    }
    else {
        $("#lblClientId").removeClass('required');
    }
    //if ($("#txtDepositoryParticipantName").val() == null || $("#txtDepositoryParticipantName").val() == undefined || $("#txtDepositoryParticipantName").val().trim() == "") {
    //    count++;
    //    $("#lblDepositoryParticipantName").addClass('required');
    //}
    //else {
    //    $("#lblDepositoryParticipantName").removeClass('required');
    //}
    if ($("#txtDepositoryParticipantId").val() == null || $("#txtDepositoryParticipantId").val() == undefined || $("#txtDepositoryParticipantId").val().trim() == "") {
        count++;
        $("#lblDepositoryParticipantId").addClass('required');
    }
    else {
        $("#lblDepositoryParticipantId").removeClass('required');
    }
    //if ($("#txtTradingMemberId").val() == null || $("#txtTradingMemberId").val() == undefined || $("#txtTradingMemberId").val().trim() == "") {
    //    count++;
    //    $("#lblTradingMemberId").addClass('required');
    //}
    //else {
    //    $("#lblTradingMemberId").removeClass('required');
    //}
    if ($("#txtDematAccountNumber").val() == null || $("#txtDematAccountNumber").val() == undefined || $("#txtDematAccountNumber").val().trim() == "") {
        count++;
        $("#lblDematAccountNumber").addClass('required');
    }
    else {
        $("#lblDematAccountNumber").removeClass('required');
    }
    if ($("#ddlStatusDemat").val() == null || $("#ddlStatusDemat").val() == undefined || $("#ddlStatusDemat").val().trim() == "") {
        count++;
        $("#lblStatusDemat").addClass('required');
    }
    else {
        $("#lblStatusDemat").removeClass('required');
    }

    for (var index = 0; index < $("#tbdDematList").children().length; index++) {
        if (($("#spNsdlDematLabel").html() + $("#txtDematAccountNumber").val()) == $($($("#tbdDematList").children()[index]).children()[8]).html() && dematAccountNumberGlobal != ($("#spNsdlDematLabel").html() + $("#txtDematAccountNumber").val())) {
            alert("Demat Account Number already exist");
            count++;
            break;
        }
    }

    if (count > 0) {
        return false;
    }
    else {
        return true;
    }
}

function fnCloseModalAddDematDetail() {
    fnClearFormDematDetail();
}

function fnClearFormDematDetail() {
    $("#ddlDematAccountDetailsFor").val('');
    $("#ddlDepositoryName").val('');
    $("#txtClientId").val('');
    $("#txtDepositoryParticipantName").val('');
    $("#txtDepositoryParticipantId").val('');
    $("#txtTradingMemberId").val('');
    $("#txtDematAccountNumber").val('');
    $("#ddlStatusDemat").val('');
    isEditDemat = false;
    editableElementDemat = null;
    $("#ddlDematAccountDetailsFor").removeClass('required');
    $("#ddlDepositoryName").removeClass('required');
    $("#txtClientId").removeClass('required');
    $("#txtDepositoryParticipantName").removeClass('required');
    $("#txtDepositoryParticipantId").removeClass('required');
    $("#txtTradingMemberId").removeClass('required');
    $("#txtDematAccountNumber").removeClass('required');
    $("#ddlStatusDemat").removeClass('required');
    $("#spNsdlLabel").html('');
    $("#spNsdlDematLabel").html('');
    dematAccountNumberGlobal = '';
}

function fnEditDematDetail() {
    isEditDemat = true;
    var selectedTr = $(event.currentTarget).closest('tr').children();
    editableElementDemat = selectedTr;
    var relativeId = $(selectedTr[1]).html();
    var relativeName = $(selectedTr[2]).html();
    var depositoryName = $(selectedTr[3]).html();
    var clientId = $(selectedTr[4]).html();
    var depositoryParticipantName = $(selectedTr[5]).html();
    var depositoryParticipantId = $(selectedTr[6]).html();
    var tradingMemberId = $(selectedTr[7]).html();
    var dematAccountNo = $(selectedTr[8]).html();
    dematAccountNumberGlobal = dematAccountNo;
    var status = $(selectedTr[9]).html();

    $("#ddlDematAccountDetailsFor").val(relativeId);
    $("#ddlDepositoryName").val(depositoryName);
    $("#txtClientId").val(clientId);
    $("#txtDepositoryParticipantName").val(depositoryParticipantName);
    if (depositoryName == "NSDL") {
        $("#txtDepositoryParticipantId").val(depositoryParticipantId.replace("IN", ""));
        $("#txtDematAccountNumber").val(dematAccountNo.replace("IN", ""));
    }
    else {
        $("#txtDepositoryParticipantId").val(depositoryParticipantId);
        $("#txtDematAccountNumber").val(dematAccountNo);
    }
    $("#txtTradingMemberId").val(tradingMemberId);

    $("#ddlStatusDemat").val(status);
    if (depositoryName == "NSDL") {
        $("#spNsdlLabel").html('IN');
        $("#spNsdlDematLabel").html('IN');
    }
    else {
        $("#spNsdlLabel").html('');
        $("#spNsdlDematLabel").html('');
    }
}

function getAllRestrictedCompanies() {
    $("#Loader").show();
    var webUrl = uri + "/api/RestrictedCompanies/GetRestrictedCompanies";
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
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            var str = "";
            str += '<option value=""></option>';
            str += '<option value="0">Not Applicable</option>';
            if (msg.StatusFl == true) {

                for (var index = 0; index < msg.RestrictedCompaniesList.length; index++) {
                    str += '<option value="' + msg.RestrictedCompaniesList[index].ID + '">' + msg.RestrictedCompaniesList[index].companyName + '</option>';
                }

            }
            else {
                if (msg.StatusFl == false) {
                    //  alert(msg.Msg);
                }
            }
            $("#ddlRestrictedCompanies").html(str);
        },
        error: function (response) {
            $("#Loader").hide();
            alert(response.status + ' ' + response.statusText);
        }
    });
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
                result += "<option value=''></option>";
                result += "<option value='0'>Not Applicable</option>";
                for (var i = 0; i < msg.SecurityTypeList.length; i++) {
                    if (msg.SecurityTypeList[i].Name == "Equity") {
                        result += "<option value='" + msg.SecurityTypeList[i].Id + "'>" + msg.SecurityTypeList[i].Name + "</option>";
                    }
                }
                $("#ddlSecurityType").html(result);
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

function fnAddInitialHoldingDeclarationDetail() {
    if (fnValidateInitialHoldingDeclarationDetail()) {
        var obj = new InitialHoldingDetail();
        obj.relative.ID = $("#ddlFor").val() == null ? 0 : ($("#ddlFor").val().trim() == "" ? 0 : $("#ddlFor").val());
        obj.relative.relativeName = $("#ddlFor option:selected").text();
        obj.dematAccount.ID = $("#ddlDematAccNo").val() == null ? 0 : ($("#ddlDematAccNo").val().trim() == "" ? 0 : $("#ddlDematAccNo").val());
        obj.dematAccount.accountNo = $("#ddlDematAccNo option:selected").text();
        var securityTypeName = $("#ddlSecurityType option:selected").text();
        var panNumber = $("#txtPan").val() == null ? 0 : ($("#txtPan").val().trim() == "" ? 0 : $("#txtPan").val());
        var tradingMemberId = $("#txtTradingMemId").val() == null ? 0 : ($("#txtTradingMemId").val().trim() == "" ? 0 : $("#txtTradingMemId").val());
        if (!isEditInitialHoldingDeclarationDetail) {
            var str = "";
            str += '<tr>';
            str += '<td style="display:none;">' + obj.restrictedCompany.ID + '</td>';
            str += '<td>' + obj.restrictedCompany.companyName + '</td>';
            str += '<td style="display:none;">' + obj.securityType + '</td>';
            str += '<td>' + securityTypeName + '</td>';
            str += '<td style="display:none;">' + obj.relative.ID + '</td>';
            str += '<td>' + obj.relative.relativeName + '</td>';
            str += '<td style="display:none;">' + obj.dematAccount.ID + '</td>';
            str += '<td>' + obj.dematAccount.accountNo + '</td>';
            str += '<td style="text-align: right;">' + obj.noOfSecurities + '</td>';
            str += '<td style="display:none;">' + panNumber + '</td>';
            str += '<td style="display:none;">' + tradingMemberId + '</td>';
            str += '<td><a data-target="#modalAddInitialHoldingDeclarations" data-toggle="modal" class="btn btn-outline dark" onclick=\"javascript:fnEditInitialDeclarationDetail();\">Edit</a>';
            str += '&nbsp;<a data-target="#modalDeleteInitialHoldingDeclarations" data-toggle="modal" class="btn btn-outline dark" onclick=\"javascript:fnDeleteInitialDeclarationDetail();\">Delete</a></td>';
            str += '<td style="display:none;">' + obj.ID + '</td>';
            str += '</tr>';
            $("#tbdInitialDeclaration").append(str);
        }
        else {
            $(editableElementInitialDeclarationDetail[0]).html(obj.restrictedCompany.ID);
            $(editableElementInitialDeclarationDetail[1]).html(obj.restrictedCompany.companyName);
            $(editableElementInitialDeclarationDetail[2]).html(obj.securityType);
            $(editableElementInitialDeclarationDetail[3]).html(securityTypeName);
            $(editableElementInitialDeclarationDetail[4]).html(obj.relative.ID);
            $(editableElementInitialDeclarationDetail[5]).html(obj.relative.relativeName);
            $(editableElementInitialDeclarationDetail[6]).html(obj.dematAccount.ID);
            $(editableElementInitialDeclarationDetail[7]).html(obj.dematAccount.accountNo);
            $(editableElementInitialDeclarationDetail[8]).html(obj.noOfSecurities);
            $(editableElementInitialDeclarationDetail[9]).html(panNumber);
            $(editableElementInitialDeclarationDetail[10]).html(tradingMemberId);
            //  $(editableElementInitialDeclarationDetail[12]).html(obj.ID);
        }
        fnClearFormInitialDeclarationDetail();
        $("#modalAddInitialHoldingDeclarations").modal('hide');
    }
}

function fnCloseModalAddInitialHoldingDeclarations() {
    fnClearFormInitialDeclarationDetail();
}

function fnClearFormInitialDeclarationDetail() {
    $("#ddlRestrictedCompanies").val('');
    $("#ddlSecurityType").val('');
    $("#ddlFor").val('');
    $("#txtPan").val('');
    $("#ddlDematAccNo").val('');
    $("#ddlDematAccNo").html('');
    $("#txtTradingMemId").val('');
    $("#txtNumberOfSecurities").val('');
    isEditInitialHoldingDeclarationDetail = false;
    editableElementInitialDeclarationDetail = null;
    $("#lblRestrictedCompanies").removeClass('required');
    $("#lblSecurityType").removeClass('required');
    $("#lblFor").removeClass('required');
    $("#lblPan").removeClass('required');
    $("#lblDematAccNo").removeClass('required');
    $("#lblTradingMemId").removeClass('required');
    $("#lblNumberOfSecurities").removeClass('required');
    dematAccountNumberGlobal = '';
}

function fnValidateInitialHoldingDeclarationDetail() {
    var count = 0;
    if ($("#ddlRestrictedCompanies").val() == null || $("#ddlRestrictedCompanies").val() == undefined || $("#ddlRestrictedCompanies").val().trim() == "") {
        count++;
        $("#lblRestrictedCompanies").addClass('required');
    }
    else {
        $("#lblRestrictedCompanies").removeClass('required');
    }
    if ($("#ddlSecurityType").val() == null || $("#ddlSecurityType").val() == undefined || $("#ddlSecurityType").val().trim() == "") {
        count++;
        $("#lblSecurityType").addClass('required');
    }
    else {
        $("#lblSecurityType").removeClass('required');
    }
    if ($("#ddlFor").val() == null || $("#ddlFor").val() == undefined || $("#ddlFor").val().trim() == "") {
        count++;
        $("#lblFor").addClass('required');
    }
    else {
        $("#lblFor").removeClass('required');
    }
    //if ($("#txtPan").val() == null || $("#txtPan").val() == undefined || $("#txtPan").val().trim() == "") {
    //    count++;
    //    $("#lblPan").addClass('required');
    //}
    //else {
    //    $("#lblPan").removeClass('required');
    //}
    if ($("#ddlDematAccNo").val() == null || $("#ddlDematAccNo").val() == undefined || $("#ddlDematAccNo").val().trim() == "") {
        count++;
        $("#lblDematAccNo").addClass('required');
    }
    else {
        $("#lblDematAccNo").removeClass('required');
    }
    //if ($("#txtTradingMemId").val() == null || $("#txtTradingMemId").val() == undefined || $("#txtTradingMemId").val().trim() == "") {
    //    count++;
    //    $("#lblTradingMemId").addClass('required');
    //}
    //else {
    //    $("#lblTradingMemId").removeClass('required');
    //}
    if ($("#txtNumberOfSecurities").val() == null || $("#txtNumberOfSecurities").val() == undefined || $("#txtNumberOfSecurities").val().trim() == "") {
        count++;
        $("#lblNumberOfSecurities").addClass('required');
    }
    else {
        $("#lblNumberOfSecurities").removeClass('required');
    }

    for (var index = 0; index < $("#tbdInitialDeclaration").children().length; index++) {
        if ($("#ddlDematAccNo option:selected").text() == $($($("#tbdInitialDeclaration").children()[index]).children()[7]).html() && dematAccountNumberGlobal != $("#ddlDematAccNo option:selected").text()) {
            alert("Demat Account Number has already been declared with holdings. If you want to change the holdings please update the same.");
            count++;
            break;
        }
    }

    if (count > 0) {
        return false;
    }
    else {
        return true;
    }
}

function fnEditInitialDeclarationDetail() {
    isEditInitialHoldingDeclarationDetail = true;
    var selectedTr = $(event.currentTarget).closest('tr').children();
    editableElementInitialDeclarationDetail = selectedTr;
    var restrictedCompanyId = $(selectedTr[0]).html();
    var securityType = $(selectedTr[2]).html();
    var relativeId = $(selectedTr[4]).html();
    // var dematAccountId = $(selectedTr[6]).html();
    var dematAccountNo = $(selectedTr[7]).html();
    dematAccountNumberGlobal = dematAccountNo;
    var numberOfSecurities = $(selectedTr[8]).html();
    var panNumber = $(selectedTr[9]).html();
    var tradingMemberId = $(selectedTr[10]).html();

    $("#ddlRestrictedCompanies").val(restrictedCompanyId);
    $("#ddlSecurityType").val(securityType);
    $("#ddlFor").val(relativeId).change();
    $("#txtPan").val(panNumber);
    // $("#ddlDematAccNo").val(dematAccountId);
    $("#ddlDematAccNo option:contains(" + dematAccountNo + ")").attr('selected', 'selected');
    $("#txtTradingMemId").val(tradingMemberId);
    $("#txtNumberOfSecurities").val(numberOfSecurities);
}

function fnGetApprover() {
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/GetUserList";
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
                    if (msg.UserList[i].isApprover == "Yes") {
                        mailToUserName = msg.UserList[i].USER_NM;
                        mailTo = msg.UserList[i].USER_EMAIL;
                    }
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

function fnGenerateDynamicBodyOfInitialHolding() {
    var result = "";
    for (var index = 0; index < $("#tbdInitialDeclaration").children().length; index++) {
        result += '<tr>';
        var forRelativeOrSelf = $($($("#tbdInitialDeclaration").children()[index]).children()[5]).html();
        var company = $($($("#tbdInitialDeclaration").children()[index]).children()[1]).html();
        var securityType = $($($("#tbdInitialDeclaration").children()[index]).children()[3]).html();
        var dematAccountNo = $($($("#tbdInitialDeclaration").children()[index]).children()[7]).html();
        var numberOfSecurities = $($($("#tbdInitialDeclaration").children()[index]).children()[8]).html();
        result += '<td>' + forRelativeOrSelf + '</td>';
        result += '<td>' + company + '</td>';
        result += '<td>' + securityType + '</td>';
        result += '<td>' + dematAccountNo + '</td>';
        result += '<td style="text-align: right;">' + numberOfSecurities + '</td>';
        result += '</tr>';
    }
    $("#templateBodyInitialHoldingDeclaration").html(result);
}

function fnSendEmailNoticeConfirmation() {
    if ($("#inAcceptTermsAndConditions").prop('checked')) {
        $("#Loader").show();
        var personalInformation = new PersonalInformation();
        personalInformation.isFinalDeclared = true;
        var webUrl = uri + "/api/Transaction/SendEmailNoticeConfirmation";
        $.ajax({
            url: webUrl,
            type: "POST",
            data: JSON.stringify(personalInformation),
            // async: false,
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
    var date = dateTime.split(" ")[0];

    return (date.split("/")[1] + "/" + date.split("/")[0] + "/" + date.split("/")[2]);
}

function setPersonalInformation(msg) {
    $("#ddlResidentType").val(msg.User.residentType);
    if ($("#ddlResidentType").val() == 'NRI') {
        $("#country").prop('disabled', false);
        $("#country").val(msg.User.country);
    }
    else if ($("#ddlResidentType").val() == 'INDIAN_RESIDENT') {
        $("#country").prop('disabled', true);
        $("#country").val(msg.User.country);
    }
    else {
        $("#country").prop('disabled', true);
        $("#country").val(msg.User.country);
    }
    $("#txtPermanentAccountNumber").val(msg.User.panNumber);
    $("#ddlIdentificationType").val(msg.User.identificationType);
    $("#txtIdentificationNumber").val(msg.User.identificationNumber);
    //  $("#txtMobileNumber").val(msg.User.USER_MOBILE);
    $("#txtAddress").val(msg.User.address);
    $("#txtPincodeNumber").val(msg.User.pinCode);
    $('#txtDateOfJoining').datepicker({
        format: "dd/mm/yyyy"
    }).datepicker("setDate", ConvertToDateTime(msg.User.joiningDate));
    $('#txtDateOfBecomingInsider').datepicker({
        format: "dd/mm/yyyy"
    }).datepicker("setDate", msg.User.becomingInsiderDate != "" ? ConvertToDateTime(msg.User.becomingInsiderDate) : "");
    // $("#ddlDepartment").val(msg.User.department.DEPARTMENT_ID);
    $("#ddlLocation").val(msg.User.location.locationId);
    // $("#ddlDesignation").val(msg.User.designation.DESIGNATION_ID);
    $("#ddlCategory").val(msg.User.category.ID);
    getAllSubCategories();
    $("#ddlSubCategory").val(msg.User.category.subCategory.ID);
    $("#txtDinNumber").val(msg.User.dinNumber);
    $("#txtInstitution").val(msg.User.institutionName);
    $("#txtStream").val(msg.User.stream);
    $("#txtEmployer").val(msg.User.employerDetails);
    $("#txtD_ID").val(msg.User.D_ID);
}

function setRelativeInformation(msg) {
    var str = "";
    if (msg.User.lstRelative !== null) {
        for (var i = 0; i < msg.User.lstRelative.length; i++) {
            str += '<tr>';
            str += '<td style="display:none;">' + msg.User.lstRelative[i].ID + '</td>';
            str += '<td>' + msg.User.lstRelative[i].relativeName + '</td>';
            str += '<td style="display:none;">' + msg.User.lstRelative[i].relation.RELATION_ID + '</td>';
            str += '<td>' + msg.User.lstRelative[i].relation.RELATION_NM + '</td>';
            str += '<td>' + msg.User.lstRelative[i].panNumber + '</td>';
            str += '<td>' + msg.User.lstRelative[i].identificationType + '</td>';
            str += '<td>' + msg.User.lstRelative[i].identificationNumber + '</td>';
            str += '<td>' + msg.User.lstRelative[i].address + '</td>';
            str += '<td>' + msg.User.lstRelative[i].mobile + '</td>';
            str += '<td>' + msg.User.lstRelative[i].status + '</td>';
            str += '<td>' + msg.User.lstRelative[i].remarks + '</td>';
            str += '<td><a data-target="#modalAddRelativeDetail" data-toggle="modal" class="btn btn-outline dark" onclick=\"javascript:fnEditRelativeDetail();\">Edit</a>';

            if (msg.User.lstRelative[i].isDeleteRelative) {
                str += '&nbsp;<a data-target="#modalDeleteRelativeDetail" data-toggle="modal" class="btn btn-outline dark" onclick=\"javascript:fnDeleteRelativeDetail();\">Delete</a>';
            }

            str += '</td >';
            str += '</tr>';
        }
    }
    $("#tbdRelativeList").html(str);
}

function setDematInformation(msg) {
    var str = "";
    if (msg.User.lstDematAccount !== null) {
        for (var i = 0; i < msg.User.lstDematAccount.length; i++) {
            str += '<tr>';
            str += '<td style="display:none;">' + msg.User.lstDematAccount[i].ID + '</td>';
            if (msg.User.lstDematAccount[i].relative == null) {
                msg.User.lstDematAccount[i].relative = new Object();
                msg.User.lstDematAccount[i].relative.ID = -1;
                msg.User.lstDematAccount[i].relative.relativeName = "Not Applicable";
            }
            str += '<td style="display:none;">' + msg.User.lstDematAccount[i].relative.ID + '</td>';
            str += '<td>' + msg.User.lstDematAccount[i].relative.relativeName + '</td>';
            str += '<td>' + msg.User.lstDematAccount[i].depositoryName + '</td>';
            str += '<td>' + msg.User.lstDematAccount[i].clientId + '</td>';
            str += '<td>' + msg.User.lstDematAccount[i].depositoryParticipantName + '</td>';
            str += '<td>' + msg.User.lstDematAccount[i].depositoryParticipantId + '</td>';
            str += '<td>' + msg.User.lstDematAccount[i].tradingMemberId + '</td>';
            str += '<td>' + msg.User.lstDematAccount[i].accountNo + '</td>';
            str += '<td>' + msg.User.lstDematAccount[i].status + '</td>';
            str += '<td><a data-target="#modalAddDematDetail" data-toggle="modal" class="btn btn-outline dark" onclick=\"javascript:fnEditDematDetail();\">Edit</a>';

            if (msg.User.lstDematAccount[i].isDeleteDemat) {
                str += '&nbsp;<a data-target="#modalDeleteDematDetail" data-toggle="modal" class="btn btn-outline dark" onclick=\"javascript:fnDeleteDematDetail();\">Delete</a>';
            }

            str += '</td>';
            str += '</tr>';
        }
    }
    $("#tbdDematList").html(str);
}

function setInitialHoldingDetail(msg) {
    var str = "";
    if (msg.User.lstInitialHoldingDetail !== null) {
        for (var i = 0; i < msg.User.lstInitialHoldingDetail.length; i++) {
            str += '<tr>';
            str += '<td style="display:none;">' + msg.User.lstInitialHoldingDetail[i].restrictedCompany.ID + '</td>';
            str += '<td>' + msg.User.lstInitialHoldingDetail[i].restrictedCompany.companyName + '</td>';
            str += '<td style="display:none;">' + msg.User.lstInitialHoldingDetail[i].securityType + '</td>';
            str += '<td>' + msg.User.lstInitialHoldingDetail[i].securityTypeName + '</td>';
            str += '<td style="display:none;">' + msg.User.lstInitialHoldingDetail[i].relative.ID + '</td>';
            str += '<td>' + msg.User.lstInitialHoldingDetail[i].relative.relativeName + '</td>';
            str += '<td style="display:none;">' + msg.User.lstInitialHoldingDetail[i].dematAccount.ID + '</td>';
            str += '<td>' + msg.User.lstInitialHoldingDetail[i].dematAccount.accountNo + '</td>';
            str += '<td style="text-align: right;">' + msg.User.lstInitialHoldingDetail[i].noOfSecurities + '</td>';
            str += '<td style="display:none;">' + msg.User.lstInitialHoldingDetail[i].relative.panNumber + '</td>';
            str += '<td style="display:none;">' + msg.User.lstInitialHoldingDetail[i].dematAccount.tradingMemberId + '</td>';
            str += '<td><a data-target="#modalAddInitialHoldingDeclarations" data-toggle="modal" class="btn btn-outline dark" onclick=\"javascript:fnEditInitialDeclarationDetail();\">Edit</a>';

            if (msg.User.lstInitialHoldingDetail[i].isDeleteInitialHolding) {
                str += '&nbsp;<a data-target="#modalDeleteInitialHoldingDeclarations" data-toggle="modal" class="btn btn-outline dark" onclick=\"javascript:fnDeleteInitialDeclarationDetail();\">Delete</a>';
            }

            str += '</td > ';

            str += '<td style="display:none;">' + msg.User.lstInitialHoldingDetail[i].ID + '</td>';
            str += '</tr>';
        }
    }


    $("#tbdInitialDeclaration").html(str);
}

function fnGetPolicy() {
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
                // $("#aInAcceptTermsAndConditions").attr('href', ("../assets/logos/Policy/" + msg.PolicyList[0].DOCUMENT));

                var pdfviewer = $("#viewer").data("ejPdfViewer");
                pdfviewer.load(msg.PolicyList[0].DOCUMENT);
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
    $("#lblInstitution").removeClass('required');
    $("#lblStream").removeClass('required');
    $("#lblEmployer").removeClass('required');
}

function fnValidateEducationalAndProfessionalDetails() {
    var count = 0;
    if ($("#txtInstitution").val() == null || $("#txtInstitution").val() == undefined || $("#txtInstitution").val().trim() == '') {
        count++;
        $("#lblInstitution").addClass('required');
    }
    else {
        $("#lblInstitution").removeClass('required');
    }
    if ($("#txtStream").val() == null || $("#txtStream").val() == undefined || $("#txtStream").val().trim() == '') {
        count++;
        $("#lblStream").addClass('required');
    }
    else {
        $("#lblStream").removeClass('required');
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

        var webUrl = uri + "/api/Transaction/SaveEducationalAndProfessionalDetails";
        $.ajax({
            url: webUrl,
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
        $("#txtPAN").val('');
        $("#txtPAN").prop("disabled", true);
        $("#lblPAN").removeClass('required');
        $("#lblIdentificationTypeRelation").removeClass('required');
        $("#lblIdentificationNumberRelation").removeClass('required');
        $("#ddlIdentificationTypeRelation").val('');
    }
    else {
        $("#txtPAN").val('');
        $("#txtPAN").prop("disabled", false);
        $("#lblPAN").removeClass('required');
        $("#lblIdentificationTypeRelation").removeClass('required');
        $("#lblIdentificationNumberRelation").removeClass('required');
        $("#ddlIdentificationTypeRelation").val('');
    }
}

function fnDeleteRelativeDetail() {
    var selectedTr = $(event.currentTarget).closest('tr').children();
    var relativeId = $(selectedTr[0]).html();

    deleteRelativeDetailElement = $(event.currentTarget).closest('tr');
    $("#txtDeleteRelativeDetailId").val(relativeId);
}

function fnDeleteDematDetail() {
    var selectedTr = $(event.currentTarget).closest('tr').children();
    var dematAccountId = $(selectedTr[0]).html();

    deleteDematDetailElement = $(event.currentTarget).closest('tr');
    $("#txtDeleteDematDetailId").val(dematAccountId);
}

function fnDeleteInitialDeclarationDetail() {
    var selectedTr = $(event.currentTarget).closest('tr').children();
    var initialHoldingId = $(selectedTr[12]).html();

    deleteInitialHoldingDetailElement = $(event.currentTarget).closest('tr');
    $("#txtDeleteInitialHoldingDetailId").val(initialHoldingId);
}

function fnDeleteRelativeDetailModal() {
    if ($("#txtDeleteRelativeDetailId").val() == "0") {
        if (deleteRelativeDetailElement != null) {
            deleteRelativeDetailElement.remove();
            deleteRelativeDetailElement = null;
            alert("Record has been deleted successfully!");
        }
    }
    else {
        $("#Loader").show();
        var webUrl = uri + "/api/Transaction/DeleteRelativeDetail";
        $.ajax({
            url: webUrl,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            data: JSON.stringify({
                relativeInfo: { ID: $("#txtDeleteRelativeDetailId").val() }
            }),
            success: function (msg) {
                $("#Loader").hide();
                if (isJson(msg)) {
                    msg = JSON.parse(msg);
                }
                if (msg.StatusFl == true) {
                    if (deleteRelativeDetailElement != null) {
                        deleteRelativeDetailElement.remove();
                        deleteRelativeDetailElement = null;
                        alert("Record has been deleted successfully!");
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
}

function fnDeleteDematDetailModal() {

    if ($("#txtDeleteDematDetailId").val() == "0") {
        if (deleteDematDetailElement != null) {
            deleteDematDetailElement.remove();
            deleteDematDetailElement = null;
            alert("Record has been deleted successfully!");
        }
    }
    else {
        $("#Loader").show();
        var webUrl = uri + "/api/Transaction/DeleteDematDetail";
        $.ajax({
            url: webUrl,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            data: JSON.stringify({
                dematInfo: { ID: $("#txtDeleteDematDetailId").val() }
            }),
            success: function (msg) {
                $("#Loader").hide();
                if (isJson(msg)) {
                    msg = JSON.parse(msg);
                }
                if (msg.StatusFl == true) {
                    if (deleteDematDetailElement != null) {
                        deleteDematDetailElement.remove();
                        deleteDematDetailElement = null;
                        alert("Record has been deleted successfully!");
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
}

function fnDeleteInitialHoldingDetailModal() {

    if ($("#txtDeleteInitialHoldingDetailId").val() == "0") {
        if (deleteInitialHoldingDetailElement != null) {
            deleteInitialHoldingDetailElement.remove();
            deleteInitialHoldingDetailElement = null;
            alert("Record has been deleted successfully!");
        }
    }
    else {
        $("#Loader").show();
        var webUrl = uri + "/api/Transaction/DeleteInitialHoldingDetail";
        $.ajax({
            url: webUrl,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            data: JSON.stringify({
                initialHoldingDetailInfo: { ID: $("#txtDeleteInitialHoldingDetailId").val() }
            }),
            success: function (msg) {
                $("#Loader").hide();
                if (isJson(msg)) {
                    msg = JSON.parse(msg);
                }
                if (msg.StatusFl == true) {
                    if (deleteInitialHoldingDetailElement != null) {
                        deleteInitialHoldingDetailElement.remove();
                        deleteInitialHoldingDetailElement = null;
                        alert("Record has been deleted successfully!");
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