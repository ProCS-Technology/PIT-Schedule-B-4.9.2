var ReminderListing = null;
var userEmailsSelected = [];
$(document).ready(function () {
    $('#Loader').hide();
    fnBindUserList();

    $('#stack1').on('hide.bs.modal', function () {
    });

    $('#bindUser').select2({
        dropdownAutoWidth: true,
        multiple: true,
        width: '100%',
        height: '30px',
        placeholder: "Select Users",
        allowClear: true
    });
    $('.select2-search__field').css('width', '100%');
});
function fnBindUserList() {
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/GetUserList";
    $.ajax({
        type: "GET",
        url: webUrl,
        placeholder: "Select User",
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            if (msg.StatusFl == true) {
                var result = "";

                result += '<option value="0">Select All</option>';
                for (var i = 0; i < msg.UserList.length; i++) {
                    userEmailsSelected.push(msg.UserList[i].USER_EMAIL);
                    result += '<option value="' + msg.UserList[i].USER_EMAIL + '">' + msg.UserList[i].USER_NM + '(' + msg.UserList[i].USER_EMAIL + ')' + '</option>';
                }
                $("#bindUser").html(result);
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
function fnSendReminderEmail() {
    if (val()) {
        details();
    }
    else {

        return false;
    }
}
function val() {
    debugger;
    if ($('#bindTYPE').val() == "0" || $('#bindTYPE').val() == null || $('#bindTYPE').val() == undefined) {
        alert("Please Select Reminder Type");
        return false;
    }
    {
        if ($("#txtSUBJECT").val() == "" || $("#txtSUBJECT").val() == null || $("#txtSUBJECT").val() == undefined) {
            alert("Please enter Subject");
            return false;
        }
        if ($("#bindUser").val() == "" || $("#bindUser").val() == null || $("#bindUser").val() == undefined) {
            alert("Please Select User");
            return false;
        }
        if ($("#TextArea1").val() == "" || $("#TextArea1").val() == null || $("#TextArea1").val() == undefined) {
            alert("Mail Template should not be empty");
            return false;
        }
        return true;
    }
}
function isJson(str) {
    try {
        JSON.parse(str);
    } catch (e) {
        return false;
    }
    return true;
}
function details() {
    var isSelectAll = false;
    var Type = document.getElementById("bindTYPE").value;
    var Sub = document.getElementById("txtSUBJECT").value;
    var tempUserSelected = [];
    for (var i = 0; i < $("#bindUser").select2('data').length; i++) {
        if ($("#bindUser").select2('data')[i].id == "0") {
            isSelectAll = true;
        }
        tempUserSelected.push($("#bindUser").select2('data')[i].id);
    }

    var text = document.getElementById("TextArea1").value;
    if (isSelectAll) {
        tempUserSelected = [];
        tempUserSelected = userEmailsSelected;
    }

    var user = tempUserSelected; //loginid
    $("#Loader").show();
    var webUrl = uri + "/api/Reminder/SendReminder";
    $.ajax({
        type: 'POST',
        url: webUrl,
        data: JSON.stringify({
            mailType: Type, subject: Sub, lstUser: user, mailBody: text
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        // async: true,
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
                alert(msg.Msg);
                window.location.reload(true);
            }
        },
        error: function (error) {
            $("#Loader").hide();
            alert(error.status + ' ' + error.statusText);
            $('#btnsend_reminders').removeAttr("data-dismiss");
        }
    })
}