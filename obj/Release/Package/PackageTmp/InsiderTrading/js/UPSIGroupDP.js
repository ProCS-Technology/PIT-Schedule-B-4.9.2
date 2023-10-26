$(document).ready(function () {
    $("#Loader").hide();
    fnGetUserList();
});
function fnGetUserList() {
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/GetDPUsers";
    $.ajax({
        type: "GET",
        url: webUrl,
        data: "{}",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            if (msg.StatusFl == true) {
                var result = "";
                result += '<option value="all">all</option>';
                for (var i = 0; i < msg.UserList.length; i++) {
                    result += '<option value="' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].USER_NM + ' (' + msg.UserList[i].LOGIN_ID + ')</option>';
                }
                $("#dduserslist").html(' ');
                $("#dduserslist").html(result);
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
function fnSaveMember() {
    var ConnectedMembers = new Array();
    var users = $('#dduserslist').val();
    if (users != null) {
        for (i = 0; i < users.length; i++) {
            var CM = new Object();
            if (users[i] != "all") {
                var sCMNm = users[i];
                CM.CPNm = sCMNm;
                ConnectedMembers.push(CM);
            }
        }
        $("#Loader").show();
        var token = $("#TokenKey").val();
        var UPSIGrpId = $('#HiddenUpsiGrpId').val();
        var webUrl = uri + "/api/UPSIGroup/SaveUPSIGroupMembers";
        $.ajax({
            type: 'POST',
            url: webUrl,
            headers: {
                'TokenKeyH': token,
            },
            data: JSON.stringify({
                GrpId: UPSIGrpId,
                ConnectedPersons: ConnectedMembers
            }),
            async: true,
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            success: function (msg) {
                $("#Loader").hide();
                if (msg.StatusFl == true) {
                    alert("Members added successfully !");
                    $('#HiddenUpsiGrpId').val(0)
                    $("#dduserslist option:selected").prop("selected", false);
                    $("#dduserslist").trigger("change");
                    $("#ModalMembers").modal('hide');
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
        });
    }
    else {
        alert("Please select user");
    }
}
$('#dduserslist').on("select2:select", function (e) {
    var data = e.params.data.text;
    if (data == 'all') {
        $("#dduserslist > option").prop("selected", "selected");
        $("#dduserslist").trigger("change");
    }
});
function fnremovegrpmembers(Grpid) {
    if (confirm('Are you sure you want to remove this user?')) {
        $("#Loader").show();
        var token = $("#TokenKey").val();
        var webUrl = uri + "/api/UPSIGroup/DeleteUPSIGroupMember";
        $.ajax({
            type: 'POST',
            url: webUrl,
            headers: {
                'TokenKeyH': token,
            },
            data: JSON.stringify({
                GrpId: Grpid
            }),
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            success: function (msg) {
                $("#Loader").hide();
                if (msg.StatusFl == true) {
                    alert("Memberes removed successfully !");
                    $("#ModalMembers").modal('hide');
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
        });
    }
    else {
    }

}