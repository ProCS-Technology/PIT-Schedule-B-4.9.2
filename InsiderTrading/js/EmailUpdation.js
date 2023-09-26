$(document).ready(function () {
    GetUserEmailList();
    $("#NewEmail").val('');
    $("#ConfEmail").val('');
    
    $('#stack1').on('hide.bs.modal', function () {
    });
    $('#stack1').modal('hide');
  

});

function GetUserEmailList() {

    $("#Loader").show();
    var webUrl = uri + "/api/EmailUpdation/GetAllEmailByBU";
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
            debugger;
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                var result = '<option value="0">Select</option>';
                for (var i = 0; i < msg.ListEmails.length; i++) {
                    result += '<option value="' + msg.ListEmails[i].UserLoginId + '">' + msg.ListEmails[i].UserEmail + '(' + msg.ListEmails[i].UserName+')</option>';
                   
                }

               
                $("#ddlEmail").html(result);
                
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
            debugger;
            $("#Loader").hide();
            alert(response.status + ' ' + response.statusText);
        }
    });

}

function fnUpdateEmail() {
    debugger;
    var Loginid = $("#ddlEmail").val();
    var newEmail = $("#NewEmail").val();
    var ConfEmail = $("#ConfEmail").val();
    if (newEmail == undefined) {
        return false;
    }
    if (newEmail == ConfEmail) {

    }
    else {
        alert("New Email and confirm Email Mismatch");
        return false;
    }
    $("#Loader").show();
    var webUrl = uri + "/api/EmailUpdation/UpdateEmail";
    $.ajax({
        type: "POST",
        url: webUrl,
        data: JSON.stringify({
            UserLoginId: Loginid, UserNewEmail: newEmail
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: true,
        success: function (msg) {
            debugger;
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                alert(msg.Msg)
                $("#Loader").hide();
                window.location.href = "../LogOut.aspx";
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
            debugger;
            $("#Loader").hide();
            alert(response.status + ' ' + response.statusText);
        }
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

function fnOpenModel() {
    $("#NewEmail").val('');
    $("#ConfEmail").val('');

    $('#stack1').modal('show');


}