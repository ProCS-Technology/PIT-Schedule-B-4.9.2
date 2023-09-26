jQuery(document).ready(function () {
    fnGetUPSIConfig();
});
function fnGetUsers(user) {
    $("#Loader").show();
    var webUrl = uri + "/api/UPSIConfig/GetAllUsers";
    $.ajax({
        type: "POST",
        url: webUrl,
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            if (msg.StatusFl == true) {
                if (msg.lst.length > 0) {
                    var strOption = "";
                    strOption += "<option value=''></option>";
                    for (var x = 0; x < msg.lst.length; x++) {
                        strOption += "<option value='" + msg.lst[x].UserLogin + "'>" + msg.lst[x].UserNm + " (" + msg.lst[x].UserLogin+")</option>";
                    }
                    $("#ddlUsr").html(strOption);
                    $("#ddlUsr").val(user);
                }
                $("#Loader").hide();
            }
            else {

            }
        },
        error: function (response) {
            $("#Loader").hide();
            alert(response.status + ' ' + response.statusText);
        }
    });
}
function fnGetUPSIConfig() {
    $("#Loader").show();
    var webUrl = uri + "/api/UPSIConfig/GetUPSIConfig";
    $.ajax({
        type: "GET",
        url: webUrl,
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            if (msg.StatusFl == true) {
                if (msg.UpsiConfig.EmailAutomation == '') {
                    $("#isAutomateNo").removeAttr("checked");
                    $("#isAutomateYes").removeAttr("checked");
                }
                else if (msg.UpsiConfig.EmailAutomation == 'No') {
                    $("#isAutomateNo").attr("checked", "checked");
                    $("#isAutomateYes").removeAttr("checked");
                }
                else if (msg.UpsiConfig.EmailAutomation == 'Yes') {
                    $("#isAutomateYes").attr("checked", "checked");
                    $("#isAutomateNo").removeAttr("checked");
                }

                if (msg.UpsiConfig.MultipleEmail == '') {
                    $("#isMultipleNo").removeAttr("checked");
                    $("#isMultipleYes").removeAttr("checked");
                }
                else if (msg.UpsiConfig.MultipleEmail == 'No') {
                    $("#isMultipleNo").attr("checked", "checked");
                    $("#isMultipleYes").removeAttr("checked");
                }
                else if (msg.UpsiConfig.MultipleEmail == 'Yes') {
                    $("#isMultipleYes").attr("checked", "checked");
                    $("#isMultipleNo").removeAttr("checked");
                }

                if (msg.UpsiConfig.AccessibleToCO == '') {
                    $("#isUPSINo").removeAttr("checked");
                    $("#isUPSIYes").removeAttr("checked");
                }
                else if (msg.UpsiConfig.AccessibleToCO == 'No') {
                    $("#isUPSINo").attr("checked", "checked");
                    $("#isUPSIYes").removeAttr("checked");
                }
                else if (msg.UpsiConfig.AccessibleToCO == 'Yes') {
                    $("#isUPSINo").removeAttr("checked");
                    $("#isUPSIYes").attr("checked", "checked");
                }

                if (msg.UpsiConfig.ManagedToCO == '') {
                    $("#isManagedNo").removeAttr("checked");
                    $("#isManagedYes").removeAttr("checked");
                    $("#dvAuthorizeUsr").hide();
                }
                else if (msg.UpsiConfig.ManagedToCO == 'No') {
                    $("#isManagedNo").attr("checked", "checked");
                    $("#isManagedYes").removeAttr("checked");
                    $("#dvAuthorizeUsr").hide();
                }
                else if (msg.UpsiConfig.ManagedToCO == 'Yes') {
                    $("#isManagedNo").removeAttr("checked");
                    $("#isManagedYes").attr("checked", "checked");
                    $("#dvAuthorizeUsr").show();
                }

                if (msg.UpsiConfig.AccessibleType == '') {
                    $("#isMessage").removeAttr("checked");
                    $("#isMessagenAttachment").removeAttr("checked");
                }
                else if (msg.UpsiConfig.AccessibleType == 'Subject') {
                    $("#isSubject").attr("checked", "checked");
                    $("#isMessage").removeAttr("checked");
                    $("#isMessagenAttachment").removeAttr("checked");
                }
                else if (msg.UpsiConfig.AccessibleType == 'Message') {
                    $("#isSubject").removeAttr("checked");
                    $("#isMessage").attr("checked", "checked");
                    $("#isMessagenAttachment").removeAttr("checked");
                }
                else if (msg.UpsiConfig.AccessibleType == 'MessageAttachment') {
                    $("#isSubject").removeAttr("checked");
                    $("#isMessage").removeAttr("checked");
                    $("#isMessagenAttachment").attr("checked", "checked");
                }
                fnGetUsers(msg.UpsiConfig.AuthorizedUsr);
                $("#Loader").hide();
            }
            else {
                
            }
        },
        error: function (response) {
            $("#Loader").hide();
            alert(response.status + ' ' + response.statusText);
        }
    });
}
function fnValidate() {
    var EmailAutomation="";
    var MultipleEmail = "";
    var AccessibleToCO = "";
    var AccessibleType = "";
    var ManagedToCO = "";
    var AuthorizedUsr = "";
    AuthorizedUsr = $("#ddlUsr").val();

    if ($("#isAutomateYes").prop("checked")) {
        EmailAutomation = "Yes";
    }
    else if ($("#isAutomateNo").prop("checked")) {
        EmailAutomation = "No";
    }

    if ($("#isMultipleYes").prop("checked")) {
        MultipleEmail = "Yes";
    }
    else if ($("#isMultipleNo").prop("checked")) {
        MultipleEmail = "No";
    }
    if ($("#isManagedYes").prop("checked")) {
        ManagedToCO = "Yes";
    }
    else if ($("#isManagedNo").prop("checked")) {
        ManagedToCO = "No";
    }

    if ($("#isUPSIYes").prop("checked")) {
        AccessibleToCO = "Yes";
    }
    else if ($("#isUPSINo").prop("checked")) {
        AccessibleToCO = "No";
    }

    if ($("#isSubject").prop("checked")) {
        AccessibleType = "Subject";
    }
    else if ($("#isMessage").prop("checked")) {
        AccessibleType = "Message";
    }
    else if ($("#isMessagenAttachment").prop("checked")) {
        AccessibleType = "MessageAttachment";
    }

    if (EmailAutomation == "" || EmailAutomation == null) {
        alert("Please select option whether email integration required or not!");
        return false;
    }

    if (EmailAutomation == "Yes") {
        if (MultipleEmail == "" || MultipleEmail == null) {
            alert("Please select multiple email option!");
            return false;
        }
    }
    //alert("ManagedToCO=" + ManagedToCO);
    if (ManagedToCO == "Yes") {
        if (AuthorizedUsr == "" || AuthorizedUsr == null) {
            alert("Please select user who will manage the UPSI");
            return false;
        }
    }

    if (ManagedToCO == "Yes") {
        if (AccessibleType == "" || AccessibleType == null) {
            alert("Please select whether only message or message with attachment will be visible to compliance officer!");
            return false;
        }
    }

    if (AccessibleToCO == "" || AccessibleToCO == null) {
        alert("Please select option whether all UPSI visible to Compliance Officer!");
        return false;
    }

    if (AccessibleToCO == "Yes") {
        if (AccessibleType == "" || AccessibleType == null) {
            alert("Please select whether only message or message with attachment will be visible to compliance officer!");
            return false;
        }
    }    
    return true;
    
    
}
function saveUPSIConfig() {
    if (fnValidate()) {
        var EmailAutomation = "";
        var MultipleEmail = "";
        var AccessibleToCO = "";
        var AccessibleType = "";
        var ManagedToCO = "";
        var AuthorizedUsr = "";
        AuthorizedUsr = $("#ddlUsr").val();

        if ($("#isAutomateYes").prop("checked")) {
            EmailAutomation = "Yes";
        }
        else if ($("#isAutomateNo").prop("checked")) {
            EmailAutomation = "No";
        }

        if ($("#isMultipleYes").prop("checked")) {
            MultipleEmail = "Yes";
        }
        else if ($("#isMultipleNo").prop("checked")) {
            MultipleEmail = "No";
        }

        if ($("#isUPSIYes").prop("checked")) {
            AccessibleToCO = "Yes";
        }
        else if ($("#isUPSINo").prop("checked")) {
            AccessibleToCO = "No";
        }
        if ($("#isManagedYes").prop("checked")) {
           ManagedToCO = "Yes";
        }
        else if ($("#isManagedNo").prop("checked")) {
            ManagedToCO = "No";
        }

        if ($("#isSubject").prop("checked")) {
            AccessibleType = "Subject";
        }
        else if ($("#isMessage").prop("checked")) {
            AccessibleType = "Message";
        }
        else if ($("#isMessagenAttachment").prop("checked")) {
            AccessibleType = "MessageAttachment";
        }

        //if (ManagedToCO == "Yes") {
        //    $("#modalSubmitConfirmation").modal('show');
        //    return;
        //}

        /*alert("EmailAutomation=" + EmailAutomation);
        alert("MultipleEmail=" + MultipleEmail);
        alert("AccessibleToCO=" + AccessibleToCO);
        alert("AccessibleType=" + AccessibleType);*/

        $("#Loader").show();
        var webUrl = uri + "/api/UPSIConfig/AddUPSIConfig";
        $.ajax({
            url: webUrl,
            type: "POST",
            data: JSON.stringify({
                EmailAutomation: EmailAutomation, MultipleEmail: MultipleEmail, AccessibleToCO: AccessibleToCO,
                ManagedToCO: ManagedToCO, AccessibleType: AccessibleType, AuthorizedUsr: AuthorizedUsr
            }),
            async: false,
            success: function (msg) {
                $("#Loader").hide();
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                if (msg.StatusFl == true) {
                    window.location.reload();
                }
                else {
                    alert(msg.Msg);
                }
            },
            error: function (response) {
                $("#Loader").hide();
                if (response.responseText == "Session Expired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                    return false;
                }
                else {
                    alert(response.status + ' ' + response.statusText);
                }
            }
        });
    }
}
function fnManagedSelect(cntrl) {
    //alert("In function fnManagedSelect");
    //alert($(cntrl).Id);
    if (cntrl.value == "No") {
        $("#ddlUsr").val('');
        $("#dvAuthorizeUsr").hide();
    }
    else {
        $("#dvAuthorizeUsr").show();
    }
}
function fnSubmitForms(Typ) {
    var EmailAutomation = "";
    var MultipleEmail = "";
    var AccessibleToCO = "";
    var AccessibleType = "";
    var ManagedToCO = "";
    var AuthorizedUsr = "";
    AuthorizedUsr = $("#ddlUsr").val();

    if ($("#isAutomateYes").prop("checked")) {
        EmailAutomation = "Yes";
    }
    else if ($("#isAutomateNo").prop("checked")) {
        EmailAutomation = "No";
    }

    if ($("#isMultipleYes").prop("checked")) {
        MultipleEmail = "Yes";
    }
    else if ($("#isMultipleNo").prop("checked")) {
        MultipleEmail = "No";
    }

    if ($("#isUPSIYes").prop("checked")) {
        AccessibleToCO = "Yes";
    }
    else if ($("#isUPSINo").prop("checked")) {
        AccessibleToCO = "No";
    }
    if ($("#isManagedYes").prop("checked")) {
        ManagedToCO = "Yes";
    }
    else if ($("#isManagedNo").prop("checked")) {
        ManagedToCO = "No";
    }

    if ($("#isSubject").prop("checked")) {
        AccessibleType = "Subject";
    }
    else if ($("#isMessage").prop("checked")) {
        AccessibleType = "Message";
    }
    else if ($("#isMessagenAttachment").prop("checked")) {
        AccessibleType = "MessageAttachment";
    }

    /*alert("EmailAutomation=" + EmailAutomation);
    alert("MultipleEmail=" + MultipleEmail);
    alert("AccessibleToCO=" + AccessibleToCO);
    alert("AccessibleType=" + AccessibleType);*/

    $("#Loader").show();
    var webUrl = uri + "/api/UPSIConfig/AddUPSIConfig";
    $.ajax({
        url: webUrl,
        type: "POST",
        data: JSON.stringify({
            EmailAutomation: EmailAutomation, MultipleEmail: MultipleEmail, AccessibleToCO: AccessibleToCO,
            ManagedToCO: ManagedToCO, AccessibleType: AccessibleType, AuthorizedUsr: AuthorizedUsr, AssignTask: Typ
        }),
        async: false,
        success: function (msg) {
            $("#Loader").hide();
            if (msg.Msg == "SessionExpired") {
                alert("Your session is expired. Please login again to continue");
                window.location.href = "../LogOut.aspx";
            }
            if (msg.StatusFl == true) {
                window.location.reload();
            }
            else {
                alert(msg.Msg);
            }
        },
        error: function (response) {
            $("#Loader").hide();
            if (response.responseText == "Session Expired") {
                alert("Your session is expired. Please login again to continue");
                window.location.href = "../LogOut.aspx";
                return false;
            }
            else {
                alert(response.status + ' ' + response.statusText);
            }
        }
    });
}