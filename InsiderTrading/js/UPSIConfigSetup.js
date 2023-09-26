jQuery(document).ready(function () {
    fnGetSmtpConfigList();
    $('#stack1').on('hide.bs.modal', function () {
    });

    $(".number").keypress(function (e) {
        //if the letter is not digit then display error and don't type anything

        if (e.which > 31 && (e.which < 48 || e.which > 57)) {
            return false;
        }
        return true;

    });

    $('.number').on('paste', function (event) {
        if (event.originalEvent.clipboardData.getData('Text').match(/[^\d]/)) {
            event.preventDefault();
        }
    });
});

function initializeDataTable() {
    $('#tbl-SmtpConfig-setup').DataTable({
        dom: 'Bfrtip',
        pageLength: 10,
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
            },
        ]
    });
}

function isJson(str) {
    try {
        JSON.parse(str);
    } catch (e) {
        return false;
    }
    return true;
}

function fnGetSmtpConfigList() {
    $("#Loader").show();
    var webUrl = uri + "/api/UPSIConfigIT/GetSmtpConfigList";
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
                for (var i = 0; i < msg.SmtpConfigList.length; i++) {
                    fnEditSmtpConfig(msg.SmtpConfigList[i].SMPT_CONFIG_ID, msg.SmtpConfigList[i].IS_POP, msg.SmtpConfigList[i].DEFAULT_EMAIL_OUTGOING, msg.SmtpConfigList[i].CONTINUAL_DISCLOSURE_EMAIL_OUTGOING, msg.SmtpConfigList[i].SMTP_HOST_NAME_OUTGOING, msg.SmtpConfigList[i].PORT_OUTGOING, msg.SmtpConfigList[i].SSL_OUTGOING, msg.SmtpConfigList[i].USER_DEFAULT_CREDENTIAL_OUTGOING, msg.SmtpConfigList[i].SMTP_USER_NAME_OUTGOING, msg.SmtpConfigList[i].PASSWORD_OUTGOING);
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

function fnAddSmtpConfiguration() {

}

function fnSaveSmtpConfig() {
    if (fnValidate()) {
        fnAddUpdateSmtpConfig();
    }
    else {
        $('#btnSave').removeAttr("data-dismiss");
        return false;
    }
}

function fnAddUpdateSmtpConfig() {
    var smtpConfigurationKey = $('#txtSmtpConfigurationKey').val();

    var isPop = $("#isPop").prop("checked");
    var defaultEmailOutgoing = $("#txtDefaultEmailOutgoing").val();
    var continualDisclosureEmailOutgoing = $('#txtContinualDisclosureEmailOutgoing').val();
    var smtpHostNameOutgoing = $("#txtSmtpHostNameOutgoing").val();
    var portNumberOutgoing = $('#txtPortNumberOutgoing').val();
    var sslOutgoing = $('#txtSslOutgoing').val();
    var userDefaultCredentialOutgoing = $('#txtUserDefaultCredentialOutgoing').val();
    var smtpUserNameOutgoing = $('#txtSmtpUserNameOutgoing').val();
    var passwordOutgoing = $('#txtPasswordOutgoing').val();
    if (smtpConfigurationKey === "") {
        smtpConfigurationKey = 0;
    }

    $("#Loader").show();
    var webUrl = uri + "/api/UPSIConfigIT/SaveSmtpConfig";
    $.ajax({
        type: 'POST',
        url: webUrl,
        data: JSON.stringify({
            SMPT_CONFIG_ID: smtpConfigurationKey, 
            IS_POP: isPop, DEFAULT_EMAIL_OUTGOING: defaultEmailOutgoing,
            CONTINUAL_DISCLOSURE_EMAIL_OUTGOING: continualDisclosureEmailOutgoing,
            SMTP_HOST_NAME_OUTGOING: smtpHostNameOutgoing,
            PORT_OUTGOING: portNumberOutgoing, SSL_OUTGOING: sslOutgoing,
            USER_DEFAULT_CREDENTIAL_OUTGOING: userDefaultCredentialOutgoing,
            SMTP_USER_NAME_OUTGOING: smtpUserNameOutgoing,
            PASSWORD_OUTGOING: passwordOutgoing
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
                    $('#btnSave').removeAttr("data-dismiss");
                    return false;
                }
            }
            else {
                alert(msg.Msg);

                if (msg.SmtpConfig.SMPT_CONFIG_ID == smtpConfigurationKey) {
                    fnEditSmtpConfig(msg.SmtpConfig.SMPT_CONFIG_ID, msg.SmtpConfig.IS_POP, msg.SmtpConfig.DEFAULT_EMAIL_OUTGOING, msg.SmtpConfig.CONTINUAL_DISCLOSURE_EMAIL_OUTGOING, msg.SmtpConfig.SMTP_HOST_NAME_OUTGOING, msg.SmtpConfig.PORT_OUTGOING, msg.SmtpConfig.SSL_OUTGOING, msg.SmtpConfig.USER_DEFAULT_CREDENTIAL_OUTGOING, msg.SmtpConfig.SMTP_USER_NAME_OUTGOING, msg.SmtpConfig.PASSWORD_OUTGOING);
                    
                    $("#Loader").hide();
                }
                else {
                    fnEditSmtpConfig(msg.SmtpConfig.SMPT_CONFIG_ID, msg.SmtpConfig.IS_POP, msg.SmtpConfig.DEFAULT_EMAIL_OUTGOING, msg.SmtpConfig.CONTINUAL_DISCLOSURE_EMAIL_OUTGOING, msg.SmtpConfig.SMTP_HOST_NAME_OUTGOING, msg.SmtpConfig.PORT_OUTGOING, msg.SmtpConfig.SSL_OUTGOING, msg.SmtpConfig.USER_DEFAULT_CREDENTIAL_OUTGOING, msg.SmtpConfig.SMTP_USER_NAME_OUTGOING, msg.SmtpConfig.PASSWORD_OUTGOING);
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

function fnEditSmtpConfig(smtpConfigurationKey, isPop, defaultEmailOutgoing, continualDisclosureEmailOutgoing, smtpHostNameOutgoing, portNumberOutgoing, sslOutgoing, userDefaultCredentialOutgoing, smtpUserNameOutgoing, passwordOutgoing) {
    $('#txtSmtpConfigurationKey').val(smtpConfigurationKey);
    if (isPop == "True") {
        $("#isPop").prop("checked", true);
    }
    else {
        $("#isImap").prop("checked", true);
    }

    $('#txtDefaultEmailOutgoing').val(defaultEmailOutgoing);
    $('#txtContinualDisclosureEmailOutgoing').val(continualDisclosureEmailOutgoing);
    $('#txtSmtpHostNameOutgoing').val(smtpHostNameOutgoing);
    $('#txtPortNumberOutgoing').val(portNumberOutgoing);
    $('#txtSslOutgoing').val(sslOutgoing);
    $("#txtUserDefaultCredentialOutgoing").val(userDefaultCredentialOutgoing);
    $('#txtSmtpUserNameOutgoing').val(smtpUserNameOutgoing);
    $('#txtPasswordOutgoing').val(passwordOutgoing);
}

function fnDeleteSmtpConfig(smtpConfigKey) {
    $("#Loader").show();
    var webUrl = uri + "/api/UPSIConfigIT/DeleteSmtpConfig";
    $.ajax({
        type: 'POST',
        url: webUrl,
        data: JSON.stringify({
            SMPT_CONFIG_ID: smtpConfigKey
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                alert("Record Deleted successfully !");
                var table = $('#tbl-SmtpConfig-setup').DataTable();
                table.destroy();
                $("#tr_" + msg.SmtpConfig.SMPT_CONFIG_ID).remove();
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

function fnValidate() {
    if (!$("#isPop").prop("checked") && !$("#isImap").prop("checked")) {
        alert("Please select one of the email reading process.");
        return false;
    }
    if ($('#txtDefaultEmailOutgoing').val().trim() == "" || $('#txtDefaultEmailOutgoing').val() == null || $('#txtDefaultEmailOutgoing').val() == undefined) {
        alert("Please enter default email");
        return false;
    }
    if (!validateEmail($('#txtDefaultEmailOutgoing').val().trim())) {
        alert("Please enter valid default email");
        return false;
    }
    if ($('#txtContinualDisclosureEmailOutgoing').val().trim() == "" || $('#txtContinualDisclosureEmailOutgoing').val() == null || $('#txtContinualDisclosureEmailOutgoing').val() == undefined) {
        alert("Please enter continual disclosure email");
        return false;
    }
    //if (!validateEmail($('#txtContinualDisclosureEmailOutgoing').val().trim())) {
    //    alert("Please enter valid disclosure email");
    //    return false;
    //}
    if ($('#txtSmtpHostNameOutgoing').val().trim() == "" || $('#txtSmtpHostNameOutgoing').val() == null || $('#txtSmtpHostNameOutgoing').val() == undefined) {
        alert("Please enter smtp host name");
        return false;
    }
    if ($('#txtPortNumberOutgoing').val().trim() == "" || $('#txtPortNumberOutgoing').val() == null || $('#txtPortNumberOutgoing').val() == undefined) {
        alert("Please enter port number");
        return false;
    }
    if ($('#txtSslOutgoing').val() == "" || $('#txtSslOutgoing').val() == "0" || $('#txtSslOutgoing').val() == null || $('#txtSslOutgoing').val() == undefined) {
        alert("Please enter ssl");
        return false;
    }
    if ($('#txtUserDefaultCredentialOutgoing').val() == "" || $('#txtUserDefaultCredentialOutgoing').val() == "0" || $('#txtUserDefaultCredentialOutgoing').val() == null || $('#txtUserDefaultCredentialOutgoing').val() == undefined) {
        alert("Please enter user default credential");
        return false;
    }
    if ($('#txtSmtpUserNameOutgoing').val().trim() == "" || $('#txtSmtpUserNameOutgoing').val() == null || $('#txtSmtpUserNameOutgoing').val() == undefined) {
        alert("Please enter smtp user name");
        return false;
    }
    if ($('#txtPasswordOutgoing').val().trim() == "" || $('#txtPasswordOutgoing').val() == null || $('#txtPasswordOutgoing').val() == undefined) {
        alert("Please enter password");
        return false;
    }
    return true;
}

function validateEmail(value) {
    var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
    if (reg.test(value) == false) {
        return false;
    }
    return true;
}

function fnCloseModal() {
    fnClearForm();
}

function fnClearForm() {
    $('#txtSmtpConfigurationKey').val('');
    $('#txtDefaultEmailOutgoing').val('');
    $('#txtContinualDisclosureEmailOutgoing').val('');
    $('#txtSmtpHostNameOutgoing').val('');
    $('#txtPortNumberOutgoing').val('');
    $('#txtSslOutgoing').val('');
    $("#txtUserDefaultCredentialOutgoing").val('');
    $('#txtSmtpUserNameOutgoing').val('');
    $('#txtPasswordOutgoing').val('');
}
