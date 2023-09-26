$(document).ready(function () {
    $("#Loader").hide();    
    fnGetUserGroupList();
});
function fnGetUserGroupList() {
    $("#Loader").show();
    var webUrl = uri + "/api/UserGroup/GetUserGroupList";
    $.ajax({
        url: webUrl,
        //type: "POST",
        async: false,
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            $("#Loader").hide();
            if (msg.Msg == "SessionExpired") {
                alert("Your session is expired. Please login again to continue");
                window.location.href = "../LogOut.aspx";
            }
            if (msg.StatusFl == true) {
                var result = "";
                var result2 = "";

                result2 += '<option value="">Select Group</option>';               
                
                for (var i = 0; i < msg.UserGroupList.length; i++) {

                    result2 += '<option value=" ' + msg.UserGroupList[i].GrpId + '">' + msg.UserGroupList[i].GrpName + '</option>';

                    result += '<tr>'
                    result += '<td>' + msg.UserGroupList[i].GrpName + '</td>';
                    result += '<td class="display-none">' + msg.UserGroupList[i].GroupMembers + '</td>';
                    result += '<td>' + msg.UserGroupList[i].CreatedOn + '</td>';
                    result += '<td id="tdEditDelete_' + msg.UserGroupList[i].GrpId + '">'
                    result += '<a data-target="#ModalUserGrp" data-toggle="modal" id="a_' + msg.UserGroupList[i].GrpId + '" class="btn btn-outline dark" onclick=\"javascript:fnEditUserGroup(\'' + msg.UserGroupList[i].GrpId + '\',\'' + msg.UserGroupList[i].GrpName + '\',\'' + msg.UserGroupList[i].GroupMembers + '\');\">Edit</a>';
                    //result += '<button type="button" style="margin-left:20px" data-target="#ModalMembers" data-toggle="modal" id="al_' + msg.UPSIGroups[i].GrpId + '" class="btn btn-outline dark" value="' + msg.UPSIGroups[i].GrpId + '" onclick="fnGrpMemberAccess(this.value)">Add D/P</button>';
                    //result += '<a style="margin-left:20px" data-target="#GrpConnectecdPerson" data-toggle="modal" id="cp_' + msg.UPSIGroups[i].GrpId + '" class="btn btn-outline dark" onclick=\"javascript:fnGrpConnectedPerson(\'' + msg.UPSIGroups[i].GrpId + '\',\'' + msg.UPSIGroups[i].GrpNm + '\');\">Add C/P</a>';
                    //result += '<a style="margin-left:20px" data-target="#GrpCommunication" data-toggle="modal" id="al_' + msg.UPSIGroups[i].GrpId + '" class="btn btn-outline dark" onclick=\"javascript:fnGrpCommunication(\'' + msg.UPSIGroups[i].GrpId + '\',\'' + msg.UPSIGroups[i].GrpNm + '\');\">UPSI</a>';
                    //result += '<a style="margin-left:20px" data-target="#GrpAuditLog" data-toggle="modal" id="al_' + msg.UPSIGroups[i].GrpId + '" class="btn btn-outline dark" onclick=\"javascript:fnGrpAuditLog(' + msg.UPSIGroups[i].GrpId + ');\">Audit Log</a>';
                    
                    result += '</td>';
                    result += '</tr>'

                    
                }

                $("#ddusergrouplist").html(' ');
                $("#ddusergrouplist").html(result2);

                var table = $('#tbl-usergrplist-setup').DataTable();
                table.destroy();
                $("#tbdusergrplist").html(result);
                initializeDataTable();
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
function validation() {
    
    var UserGroup = $("input[id*='txtGrpName']").val();
    var UserGroupMembers = $("select[id*='dduserslist']").val();
    var Count = 0;

    

    if (UserGroup == "" || UserGroup == null) {
        $('#lblGrpName').addClass('required');
        Count++
    }
    else {
        $('#lblGrpName').removeClass('required');
    }

    if (UserGroupMembers == undefined || UserGroupMembers == "" || UserGroupMembers == null || UserGroupMembers == "0") {
        $('#lblGrpUser').addClass('required');
        Count++
    }
    else {
        $('#lblGrpUser').removeClass('required');
    }
    if (Count == 0) {
        return true;
    }
    else {
        alert('Please fill required field.');
        return false;
    }
}
function fnSaveUsrGrp() {
    if (validation()) {       
       
        var webUrl = uri + "/api/UserGroup/SaveUserGroup";
        var GroupName = $("input[id*='txtGrpName']").val();
        var ddGrpMembers = $("select[id*='dduserslist']").val().join();
        var UsrGrpId = $("#HiddenUserGrpId").val();
        var token = $("#TokenKey").val();

        $.ajax({
            url: webUrl,
            type: "POST",
            headers: {
                'TokenKeyH': token,
            },
            data: JSON.stringify({
                GrpName: GroupName, GroupMembers: ddGrpMembers, GrpId: UsrGrpId
            }),
            async: false,
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            success: function (msg) {
                $("#Loader").hide();
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                if (msg.StatusFl == true) {
                    alert(msg.Msg);
                    fnCloseModal();
                    fnGetUserGroupList();
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
    else {
        
    }
}
function removeRedClass(element) {
    $('#' + element).removeClass('required');
}
function fnResetForm() {

    $("input[id*='txtGrpName']").val('');
    $("#dduserslist option:selected").prop("selected", false);
    $("#dduserslist").trigger("change");
   
    $('#lblGrpName').removeClass('required');
    $('#lblGrpUser').removeClass('required');
    
}
function fnCloseModal() {
    fnResetForm();
    $("#ModalUserGrp").modal('hide');
}
function initializeDataTable() {
    $('#tbl-usergrplist-setup').DataTable({
        "dom": '<"top"iflp<"clear">>rt<"bottom"iflp<"clear">>',
        dom: 'Bfrtip',
        pageLength: 5,
        buttons: [
        ],
    });
}
function fnEditUserGroup(GrpId, GrpNm, GrpMembers) {
    
    $("#HiddenUserGrpId").val(GrpId);
    $('#txtGrpName').val(GrpNm);
    var selectedValues = GrpMembers.split(',');
    $("#dduserslist").val(selectedValues).trigger('change');
}