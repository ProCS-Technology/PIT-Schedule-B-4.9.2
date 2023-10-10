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

    /*Added by jitendra*/
    debugger;
    var user = tempUserSelected; //loginid  
    $("input[id*=hdnEmailSubject]").val(Sub);
    $("input[id*=hdnUsers]").val(user);       
    $("input[id*=hdnMailType]").val(Type);
    $("input[id*=hdnMailBody]").val(text);

}

var downloadComplete = false;
var intervalListener;

var start = $("input[id*=hdnEmailTask]").val();
if (start == "Start") {
    $("#LoaderProgerss").show();
    intervalListener = window.setInterval(function () {
        if (!downloadComplete) {
            CallCheckEmailStatus();
        }
    }, 2000);
}

function fnChkStatus() {
    var start = $("input[id*=hdnEmailTask]").val();
    if (start == "Start") {
        $("#LoaderProgerss").show();
        intervalListener = window.setInterval(function () {
            if (!downloadComplete) {
                CallCheckEmailStatus();
            }
        }, 2000);
    }
}
function CallCheckEmailStatus() {
    $.ajax({
        type: "POST",
        url: "Reminder_Master.aspx/CheckDownload",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            updateStatus('completed', r.d);
            if (r.d.indexOf('All emails sent') > -1) {
                downloadComplete = true;
            }
        },
        error: function (r) {
            console.log('Check error : ' + r);
        },
        failure: function (r) {
            console.log('Check failure : ' + r);
        }
    });
    if (downloadComplete) {
        window.clearInterval(intervalListener);
        $("input[id*=hdnEmailTask]").val('');
        $("#LoaderProgerss").hide();
    }
}
function updateStatus(status, msg) {
    document.getElementById('lblMsg').innerHTML = msg;
    if (msg.indexOf('All emails sent') > -1) {
        downloadComplete = true;
        window.clearInterval(intervalListener);
        $("input[id*=hdnEmailTask]").val('');
        $("#LoaderProgerss").hide();
        alert("Reminder Email sent successfully");
    }
}

