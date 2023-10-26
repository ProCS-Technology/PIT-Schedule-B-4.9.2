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
        //  "dom": '<"top"iflp<"clear">>rt<"bottom"iflp<"clear">>',
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
     //    { extend: 'colvis', className: 'btn dark btn-outline', text: 'Columns' }
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
    var webUrl = uri + "/api/SmtpConfigIT/GetSmtpConfigList";
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
                    fnEditSmtpConfig(msg.SmtpConfigList[i].SMPT_CONFIG_ID, msg.SmtpConfigList[i].DEFAULT_EMAIL, msg.SmtpConfigList[i].CONTINUAL_DISCLOSURE_EMAIL, msg.SmtpConfigList[i].SMTP_HOST_NAME, msg.SmtpConfigList[i].PORT, msg.SmtpConfigList[i].SSL, msg.SmtpConfigList[i].SMTP_USER_NAME, msg.SmtpConfigList[i].PASSWORD, msg.SmtpConfigList[i].USER_DEFAULT_CREDENTIAL, msg.SmtpConfigList[i].IS_POP, msg.SmtpConfigList[i].DEFAULT_EMAIL_OUTGOING, msg.SmtpConfigList[i].CONTINUAL_DISCLOSURE_EMAIL_OUTGOING, msg.SmtpConfigList[i].SMTP_HOST_NAME_OUTGOING, msg.SmtpConfigList[i].PORT_OUTGOING, msg.SmtpConfigList[i].SSL_OUTGOING, msg.SmtpConfigList[i].SMTP_USER_NAME_OUTGOING, msg.SmtpConfigList[i].PASSWORD_OUTGOING, msg.SmtpConfigList[i].USER_DEFAULT_CREDENTIAL_OUTGOING);
                    //result += '<tr id="tr_' + msg.SmtpConfigList[i].SMPT_CONFIG_ID + '">';
                    //result += '<td id="tdDefaultEmail_' + msg.SmtpConfigList[i].SMPT_CONFIG_ID + '">' + msg.SmtpConfigList[i].DEFAULT_EMAIL + '</td>';
                    //result += '<td id="tdContinualDisclosureEmail_' + msg.SmtpConfigList[i].SMPT_CONFIG_ID + '">' + msg.SmtpConfigList[i].CONTINUAL_DISCLOSURE_EMAIL + '</td>';
                    //result += '<td id="tdSmtpHostName_' + msg.SmtpConfigList[i].SMPT_CONFIG_ID + '">' + msg.SmtpConfigList[i].SMTP_HOST_NAME + '</td>';
                    //result += '<td id="tdPort_' + msg.SmtpConfigList[i].SMPT_CONFIG_ID + '">' + msg.SmtpConfigList[i].PORT + '</td>';
                    //result += '<td id="tdSsl_' + msg.SmtpConfigList[i].SMPT_CONFIG_ID + '">' + msg.SmtpConfigList[i].SSL + '</td>';
                    //result += '<td id="tdSmtpUserName_' + msg.SmtpConfigList[i].SMPT_CONFIG_ID + '">' + msg.SmtpConfigList[i].SMTP_USER_NAME + '</td>';
                    //result += '<td id="tdPassword_' + msg.SmtpConfigList[i].SMPT_CONFIG_ID + '">' + msg.SmtpConfigList[i].PASSWORD + '</td>';
                    //result += '<td id="tdEditDelete_' + msg.SmtpConfigList[i].SMPT_CONFIG_ID + '"><a data-target="#stack1" data-toggle="modal" id="a_' + msg.SmtpConfigList[i].SMPT_CONFIG_ID + '" class="btn btn-outline dark" onclick=\"javascript:fnEditSmtpConfig(' + msg.SmtpConfigList[i].SMPT_CONFIG_ID + ',\'' + msg.SmtpConfigList[i].DEFAULT_EMAIL + '\',\'' + msg.SmtpConfigList[i].CONTINUAL_DISCLOSURE_EMAIL + '\',\'' + msg.SmtpConfigList[i].SMTP_HOST_NAME + '\',\'' + msg.SmtpConfigList[i].PORT + '\',\'' + msg.SmtpConfigList[i].SSL + '\',\'' + msg.SmtpConfigList[i].SMTP_USER_NAME + '\',\'' + msg.SmtpConfigList[i].PASSWORD + '\');\">Edit</a><a style="margin-left:20px" data-target="#delete" data-toggle="modal" id="d_' + msg.SmtpConfigList[i].SMPT_CONFIG_ID + '" class="btn btn-outline dark" onclick=\"javascript:fnDeleteSmtpConfig(' + msg.SmtpConfigList[i].SMPT_CONFIG_ID + ');\">Delete</a></td>';
                    //result += '</tr>';
                }

                //var table = $('#tbl-SmtpConfig-setup').DataTable();
                //table.destroy();
                //$("#tbdSmptConfigList").html(result);
                //initializeDataTable();
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
    var defaultEmail = $('#txtDefaultEmail').val();
    var smtpConfigurationKey = $('#txtSmtpConfigurationKey').val();
    var continualDisclosureEmail = $('#txtContinualDisclosureEmail').val();
    var smtpHostName = $('#txtSmtpHostName').val();
    var portNumber = $('#txtPortNumber').val();
    var ssl = $('#txtSsl').val();
    var userDefaultCredential= $('#txtUserDefaultCredential').val();
    var smtpUserName = $('#txtSmtpUserName').val();
    var password = $('#txtPassword').val();

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
    var webUrl = uri + "/api/SmtpConfigIT/SaveSmtpConfig";
    $.ajax({
        type: 'POST',
        url: webUrl,
        data: JSON.stringify({
            SMPT_CONFIG_ID: smtpConfigurationKey, DEFAULT_EMAIL: defaultEmail, CONTINUAL_DISCLOSURE_EMAIL: continualDisclosureEmail, SMTP_HOST_NAME: smtpHostName, PORT: portNumber, SSL: ssl, SMTP_USER_NAME: smtpUserName, PASSWORD: password, USER_DEFAULT_CREDENTIAL: userDefaultCredential,
            IS_POP: isPop, DEFAULT_EMAIL_OUTGOING: defaultEmailOutgoing, CONTINUAL_DISCLOSURE_EMAIL_OUTGOING: continualDisclosureEmailOutgoing, SMTP_HOST_NAME_OUTGOING: smtpHostNameOutgoing, PORT_OUTGOING: portNumberOutgoing, SSL_OUTGOING: sslOutgoing, USER_DEFAULT_CREDENTIAL_OUTGOING: userDefaultCredentialOutgoing, SMTP_USER_NAME_OUTGOING: smtpUserNameOutgoing, PASSWORD_OUTGOING: passwordOutgoing
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
                    fnEditSmtpConfig(msg.SmtpConfig.SMPT_CONFIG_ID, msg.SmtpConfig.DEFAULT_EMAIL, msg.SmtpConfig.CONTINUAL_DISCLOSURE_EMAIL, msg.SmtpConfig.SMTP_HOST_NAME, msg.SmtpConfig.PORT, msg.SmtpConfig.SSL, msg.SmtpConfig.SMTP_USER_NAME, msg.SmtpConfig.PASSWORD, msg.SmtpConfig.USER_DEFAULT_CREDENTIAL, msg.SmtpConfig.IS_POP, msg.SmtpConfig.DEFAULT_EMAIL_OUTGOING, msg.SmtpConfig.CONTINUAL_DISCLOSURE_EMAIL_OUTGOING, msg.SmtpConfig.SMTP_HOST_NAME_OUTGOING, msg.SmtpConfig.PORT_OUTGOING, msg.SmtpConfig.SSL_OUTGOING, msg.SmtpConfig.USER_DEFAULT_CREDENTIAL_OUTGOING, msg.SmtpConfig.SMTP_USER_NAME_OUTGOING, msg.SmtpConfig.PASSWORD_OUTGOING);
                    //$("#tdDefaultEmail_" + msg.SmtpConfig.SMPT_CONFIG_ID).html(msg.SmtpConfig.DEFAULT_EMAIL);
                    //$("#tdContinualDisclosureEmail_" + msg.SmtpConfig.SMPT_CONFIG_ID).html(msg.SmtpConfig.CONTINUAL_DISCLOSURE_EMAIL);
                    //$("#tdSmtpHostName_" + msg.SmtpConfig.SMPT_CONFIG_ID).html(msg.SmtpConfig.SMTP_HOST_NAME);
                    //$("#tdPort_" + msg.SmtpConfig.SMPT_CONFIG_ID).html(msg.SmtpConfig.PORT);
                    //$("#tdSsl_" + msg.SmtpConfig.SMPT_CONFIG_ID).html(msg.SmtpConfig.SSL);
                    //$("#tdSmtpUserName_" + msg.SmtpConfig.SMPT_CONFIG_ID).html(msg.SmtpConfig.SMTP_USER_NAME);
                    //$("#tdPassword_" + msg.SmtpConfig.SMPT_CONFIG_ID).html(msg.SmtpConfig.PASSWORD);
                    //$("#a_" + msg.SmtpConfig.SMPT_CONFIG_ID).attr("onclick", "javascript:fnEditSmtpConfig('" + msg.SmtpConfig.SMPT_CONFIG_ID + "','" + msg.SmtpConfig.DEFAULT_EMAIL + "','" + msg.SmtpConfig.CONTINUAL_DISCLOSURE_EMAIL + "','" + msg.SmtpConfig.SMTP_HOST_NAME + "','" + msg.SmtpConfig.PORT + "','" + msg.SmtpConfig.SSL + "','" + msg.SmtpConfig.SMTP_USER_NAME + "','" + msg.SmtpConfig.PASSWORD + "');");
                    //$("#a_" + msg.SmtpConfig.SMPT_CONFIG_ID).attr("data-target", "#stack1");
                    //$("#a_" + msg.SmtpConfig.SMPT_CONFIG_ID).attr("data-toggle", "modal");
                    //$("#d_" + msg.SmtpConfig.SMPT_CONFIG_ID).attr("onclick", "javascript:fnDeleteSmtpConfig('" + msg.SmtpConfig.SMPT_CONFIG_ID + "');");
                    //$("#d_" + msg.SmtpConfig.SMPT_CONFIG_ID).css({ 'margin-left': '20px' });
                    //$("#d_" + msg.SmtpConfig.SMPT_CONFIG_ID).attr("data-target", "#delete");
                    //$("#d_" + msg.SmtpConfig.SMPT_CONFIG_ID).attr("data-target", "#modal");
                    //var table = $('#tbl-SmtpConfig-setup').DataTable();
                    //table.destroy();
                    //initializeDataTable();
                    $("#Loader").hide();
                }
                else {
                    fnEditSmtpConfig(msg.SmtpConfig.SMPT_CONFIG_ID, msg.SmtpConfig.DEFAULT_EMAIL, msg.SmtpConfig.CONTINUAL_DISCLOSURE_EMAIL, msg.SmtpConfig.SMTP_HOST_NAME, msg.SmtpConfig.PORT, msg.SmtpConfig.SSL, msg.SmtpConfig.SMTP_USER_NAME, msg.SmtpConfig.PASSWORD, msg.SmtpConfigList.USER_DEFAULT_CREDENTIAL, msg.SmtpConfig.IS_POP, msg.SmtpConfig.DEFAULT_EMAIL_OUTGOING, msg.SmtpConfig.CONTINUAL_DISCLOSURE_EMAIL_OUTGOING, msg.SmtpConfig.SMTP_HOST_NAME_OUTGOING, msg.SmtpConfig.PORT_OUTGOING, msg.SmtpConfig.SSL_OUTGOING, msg.SmtpConfig.USER_DEFAULT_CREDENTIAL_OUTGOING, msg.SmtpConfig.SMTP_USER_NAME_OUTGOING, msg.SmtpConfig.PASSWORD_OUTGOING);
                    //var result = "";
                    //result += '<tr id="tr_' + msg.SmtpConfig.SMPT_CONFIG_ID + '">';
                    //result += '<td id="tdDefaultEmail_' + msg.SmtpConfig.SMPT_CONFIG_ID + '">' + msg.SmtpConfig.DEFAULT_EMAIL + '</td>';
                    //result += '<td id="tdContinualDisclosureEmail_' + msg.SmtpConfig.SMPT_CONFIG_ID + '">' + msg.SmtpConfig.CONTINUAL_DISCLOSURE_EMAIL + '</td>';
                    //result += '<td id="tdSmtpHostName_' + msg.SmtpConfig.SMPT_CONFIG_ID + '">' + msg.SmtpConfig.SMTP_HOST_NAME + '</td>';
                    //result += '<td id="tdPort_' + msg.SmtpConfig.SMPT_CONFIG_ID + '">' + msg.SmtpConfig.PORT + '</td>';
                    //result += '<td id="tdSsl_' + msg.SmtpConfig.SMPT_CONFIG_ID + '">' + msg.SmtpConfig.SSL + '</td>';
                    //result += '<td id="tdSmtpUserName_' + msg.SmtpConfig.SMPT_CONFIG_ID + '">' + msg.SmtpConfig.SMTP_USER_NAME + '</td>';
                    //result += '<td id="tdPassword_' + msg.SmtpConfig.SMPT_CONFIG_ID + '">' + msg.SmtpConfig.PASSWORD + '</td>';
                    //result += '<td id="tdEditDelete_' + msg.SmtpConfig.SMPT_CONFIG_ID + '"><a data-target="#stack1" data-toggle="modal" id="a_' + msg.SmtpConfig.SMPT_CONFIG_ID + '" class="btn btn-outline dark" onclick=\"javascript:fnEditSmtpConfig(' + msg.SmtpConfig.SMPT_CONFIG_ID + ',\'' + msg.SmtpConfig.DEFAULT_EMAIL + '\',\'' + msg.SmtpConfig.CONTINUAL_DISCLOSURE_EMAIL + '\',\'' + msg.SmtpConfig.SMTP_HOST_NAME + '\',\'' + msg.SmtpConfig.PORT + '\',\'' + msg.SmtpConfig.SSL + '\',\'' + msg.SmtpConfig.SMTP_USER_NAME + '\',\'' + msg.SmtpConfig.PASSWORD + '\');\">Edit</a><a style="margin-left:20px" data-target="#delete" data-toggle="modal" id="d_' + msg.SmtpConfig.SMPT_CONFIG_ID + '" class="btn btn-outline dark" onclick=\"javascript:fnDeleteSmtpConfig(' + msg.SmtpConfig.SMPT_CONFIG_ID + ');\">Delete</a></td>';
                    //result += '</tr>';
                    //var table = $('#tbl-SmtpConfig-setup').DataTable();
                    //table.destroy();
                    //$("#tbdSmptConfigList").append(result);
                    //initializeDataTable();
                    $("#Loader").hide();
                }

                //fnClearForm();
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

function fnEditSmtpConfig(smtpConfigurationKey, defaultEmail, continualDisclosureEmail, smtpHostName, portNumber, ssl, smtpUserName, password, userDefaultCredential, isPop, defaultEmailOutgoing, continualDisclosureEmailOutgoing, smtpHostNameOutgoing, portNumberOutgoing, sslOutgoing, smtpUserNameOutgoing, passwordOutgoing, userDefaultCredentialOutgoing) {
    $('#txtDefaultEmail').val(defaultEmail);
    $('#txtSmtpConfigurationKey').val(smtpConfigurationKey);
    $('#txtContinualDisclosureEmail').val(continualDisclosureEmail);
    $('#txtSmtpHostName').val(smtpHostName);
    $('#txtPortNumber').val(portNumber);
    $('#txtSsl').val(ssl);
    $("#txtUserDefaultCredential").val(userDefaultCredential);
    $('#txtSmtpUserName').val(smtpUserName);
    $('#txtPassword').val(password);
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
    var webUrl = uri + "/api/SmtpConfigIT/DeleteSmtpConfig";
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
    if ($('#txtDefaultEmail').val().trim() == "" || $('#txtDefaultEmail').val() == null || $('#txtDefaultEmail').val() == undefined) {
        alert("Please enter default email");
        event.preventDefault(); return false;
    }
    if (!validateEmail($('#txtDefaultEmail').val().trim())) {
        alert("Please enter valid default email");
        event.preventDefault(); return false;
    }
    if ($('#txtContinualDisclosureEmail').val().trim() == "" || $('#txtContinualDisclosureEmail').val() == null || $('#txtContinualDisclosureEmail').val() == undefined) {
        alert("Please enter continual disclosure email");
        event.preventDefault(); return false;
    }
    //if (!validateEmail($('#txtContinualDisclosureEmail').val().trim())) {
    //    alert("Please enter valid disclosure email");
    //    event.preventDefault();return false;
    //}
    if ($('#txtSmtpHostName').val().trim() == "" || $('#txtSmtpHostName').val() == null || $('#txtSmtpHostName').val() == undefined) {
        alert("Please enter smtp host name");
        event.preventDefault(); return false;
    }
    if ($('#txtPortNumber').val().trim() == "" || $('#txtPortNumber').val() == null || $('#txtPortNumber').val() == undefined) {
        alert("Please enter port number");
        event.preventDefault(); return false;
    }
    if ($('#txtSsl').val().trim() == "" || $('#txtSsl').val() == null || $('#txtSsl').val() == undefined) {
        alert("Please enter ssl");
        event.preventDefault(); return false;
    }
    if ($('#txtUserDefaultCredential').val().trim() == "" || $('#txtUserDefaultCredential').val() == null || $('#txtUserDefaultCredential').val() == undefined) {
        alert("Please enter user default credential");
        event.preventDefault(); return false;
    }
    if ($('#txtSmtpUserName').val().trim() == "" || $('#txtSmtpUserName').val() == null || $('#txtSmtpUserName').val() == undefined) {
        alert("Please enter smtp user name");
        event.preventDefault(); return false;
    }
    if ($('#txtPassword').val().trim() == "" || $('#txtPassword').val() == null || $('#txtPassword').val() == undefined) {
        alert("Please enter password");
        event.preventDefault(); return false;
    }
    if (!$("#isPop").prop("checked") && !$("#isImap").prop("checked")) {
        alert("Please select one of the email reading process.");
        event.preventDefault(); return false;
    }
    if ($('#txtDefaultEmailOutgoing').val().trim() == "" || $('#txtDefaultEmailOutgoing').val() == null || $('#txtDefaultEmailOutgoing').val() == undefined) {
        alert("Please enter default email");
        event.preventDefault(); return false;
    }
    if (!validateEmail($('#txtDefaultEmailOutgoing').val().trim())) {
        alert("Please enter valid default email");
        event.preventDefault(); return false;
    }
    if ($('#txtContinualDisclosureEmailOutgoing').val().trim() == "" || $('#txtContinualDisclosureEmailOutgoing').val() == null || $('#txtContinualDisclosureEmailOutgoing').val() == undefined) {
        alert("Please enter continual disclosure email");
        event.preventDefault(); return false;
    }
    //if (!validateEmail($('#txtContinualDisclosureEmailOutgoing').val().trim())) {
    //    alert("Please enter valid disclosure email");
    //    event.preventDefault();return false;
    //}
    if ($('#txtSmtpHostNameOutgoing').val().trim() == "" || $('#txtSmtpHostNameOutgoing').val() == null || $('#txtSmtpHostNameOutgoing').val() == undefined) {
        alert("Please enter smtp host name");
        event.preventDefault(); return false;
    }
    if ($('#txtPortNumberOutgoing').val().trim() == "" || $('#txtPortNumberOutgoing').val() == null || $('#txtPortNumberOutgoing').val() == undefined) {
        alert("Please enter port number");
        event.preventDefault(); return false;
    }
    if ($('#txtSslOutgoing').val().trim() == "" || $('#txtSslOutgoing').val() == null || $('#txtSslOutgoing').val() == undefined) {
        alert("Please enter ssl");
        event.preventDefault(); return false;
    }
    if ($('#txtUserDefaultCredentialOutgoing').val().trim() == "" || $('#txtUserDefaultCredentialOutgoing').val() == null || $('#txtUserDefaultCredentialOutgoing').val() == undefined) {
        alert("Please enter user default credential");
        event.preventDefault(); return false;
    }
    if ($('#txtSmtpUserNameOutgoing').val().trim() == "" || $('#txtSmtpUserNameOutgoing').val() == null || $('#txtSmtpUserNameOutgoing').val() == undefined) {
        alert("Please enter smtp user name");
        event.preventDefault(); return false;
    }
    if ($('#txtPasswordOutgoing').val().trim() == "" || $('#txtPasswordOutgoing').val() == null || $('#txtPasswordOutgoing').val() == undefined) {
        alert("Please enter password");
        event.preventDefault(); return false;
    }
    return true;
}

function validateEmail(value) {
    var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
    if (reg.test(value) == false) {
        return false;
        event.preventDefault();
    }
    return true;
}

function fnCloseModal() {
    fnClearForm();
}

function fnClearForm() {
    $('#txtDefaultEmail').val('');
    $('#txtSmtpConfigurationKey').val('');
    $('#txtContinualDisclosureEmail').val('');
    $('#txtSmtpHostName').val('');
    $('#txtPortNumber').val('');
    $('#txtSsl').val(0);
    $('#txtUserDefaultCredential').val(0);
    $('#txtSmtpUserName').val('');
    $('#txtPassword').val('');

    $('#txtDefaultEmailOutgoing').val('');
    $('#txtContinualDisclosureEmailOutgoing').val('');
    $('#txtSmtpHostNameOutgoing').val('');
    $('#txtPortNumberOutgoing').val('');
    $('#txtSslOutgoing').val(sslOutgoing);
    $("#txtUserDefaultCredentialOutgoing").val('');
    $('#txtSmtpUserNameOutgoing').val('');
    $('#txtPasswordOutgoing').val('');
}
