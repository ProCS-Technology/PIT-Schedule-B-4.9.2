
var UserGroupEmailBody = new Array();
$(document).ready(function () {
    $("#Loader").hide();
    $('#txtareaemailbody').summernote({
        height: 150,  
        minHeight: 150,
        maxHeight: null
    });

    fnGetUserGroupList();
    GetUserGroupSentMailList();
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

                result += '<option value="">Select Group</option>';

                for (var i = 0; i < msg.UserGroupList.length; i++) {

                    result += '<option value=" ' + msg.UserGroupList[i].GrpId + '">' + msg.UserGroupList[i].GrpName + '</option>';

                    

                }

                $("#ddusergrouplist").html(' ');
                $("#ddusergrouplist").html(result);

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
function GetUserGroupSentMailList() {
    $("#Loader").show();
    var webUrl = uri + "/api/UserGroup/GetUserGroupSentMailList";
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
                UserGroupEmailBody = new Array();
                for (var i = 0; i < msg.UserGroupSentMailList.length; i++) {

                    UserGroupEmailBody.push(msg.UserGroupSentMailList[i].GrpEmailBody);
                    result += '<tr>'
                    result += '<td>' + msg.UserGroupSentMailList[i].GrpName.substr(0, msg.UserGroupSentMailList[i].GrpName.length-1) + '</td>';
                    result += '<td>' + msg.UserGroupSentMailList[i].GrpEmailSubject + '</td>';
                    result += '<td>' + msg.UserGroupSentMailList[i].CreatedOn + '</td>';
                    //result += '<td>' + msg.UserGroupSentMailList[i].CreatedBy + '</td>';
                    result += '<td id="tdEditDelete_' + msg.UserGroupSentMailList[i].GrpId + '">'
                    result += '<a data-target="#ModalUserGrpEmail" data-toggle="modal" id="a_' + msg.UserGroupSentMailList[i].LogId + '" class="btn btn-outline dark" onclick=\"javascript:fnResendEmail(\'' + msg.UserGroupSentMailList[i].GrpEmailSubject + '\',\'' + i + '\');\">Resend</a>';
                    //result += '<a href="#" style="margin-top:6px" data-target="#ModalEmailBody" data-toggle="modal" id="al_' + msg.UserGroupSentMailList[i].LogId + '" class="btn btn-outline dark" onclick="fnViewEmail(\'' + msg.UserGroupSentMailList[i].GrpEmailSubject + '\',\'' + i + '\')">View Email</button>';
                    
                    result += '</td>';
                    result += '</tr>'

                    
                }

              
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
function fnSendMail() {
    if (validation()) {
        $('#Loader').show();
        var webUrl = uri + "/api/UserGroup/SendUserGroupEmail";
        var Email_Subject = $("input[id*='txtsubject']").val();
        var Email_Body = $("textarea[id*='txtareaemailbody']").val();
        var ddGrpMembersId = $("select[id*='ddusergrouplist']").val().join();

        var token = $("#TokenKey").val();

        $.ajax({
            url: webUrl,
            type: "POST",
            headers: {
                'TokenKeyH': token,
            },
            data: JSON.stringify({
                GrpEmailSubject: Email_Subject, GrpEmailBody: Email_Body, EmailGrpId: ddGrpMembersId
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
function fnResendEmail(Subject, Email) {

    
    $('#txtsubject').val(Subject);
    $('#txtareaemailbody').summernote('code', UserGroupEmailBody[Email]);
}
function fnViewEmail(Subject, Email) {
    $("#spnEmailSubject").html(Subject);
    $("#dvemailbody").html("");
    $("#dvemailbody").html(UserGroupEmailBody[Email]);
    
}
function validation() {
    
    
    var Email_Subject = $("input[id*='txtsubject']").val();
    var Email_Body = $("textarea[id*='txtareaemailbody']").val();
    var UserGroupId = $("select[id*='ddusergrouplist']").val();
    var Count = 0;
        

    if (Email_Subject == "" || Email_Subject == null || Email_Subject == undefined) {
        $('#lblEmailSubject').addClass('required');
        Count++
    }
    else {
        $('#lblEmailSubject').removeClass('required');
    }

    if (Email_Body == "" || Email_Body == null || Email_Body == undefined) {
        $('#lblEmailBody').addClass('required');
        Count++
    }
    else {
        $('#lblEmailBody').removeClass('required');
    }

    if (UserGroupId == undefined || UserGroupId == "" || UserGroupId == null || UserGroupId == "0") {
        $('#lblGroup').addClass('required');
        Count++
    }
    else {
        $('#lblGroup').removeClass('required');
    }
    if (Count == 0) {
        return true;
    }
    else {
        alert('Please fill required field.');
        return false;
    }
}

function removeRedClass(element) {
    $('#' + element).removeClass('required');
}
function fnCloseModal() {
    
    $("#ModalUserGrpEmail").modal('hide');
    fnResetForm();
}
function fnResetForm() {

    $("input[id*='txtsubject']").val('');
    $('#txtareaemailbody').summernote('code', '');
    $("#ddusergrouplist option:selected").prop("selected", false);
    $("#ddusergrouplist").trigger("change");

    $('#lblEmailSubject').removeClass('required');
    $('#lblEmailBody').removeClass('required');
    $('#lblGroup').removeClass('required');
    $("#dvemailbody").html("");
}
$("#txtareaemailbody").on("summernote.change", function (e) {
    $('#lblEmailBody').removeClass('required');
});

function fnCloseModal() {
    fnResetForm();
    $("#ModalUserGrpEmail").modal('hide');
}
function initializeDataTable() {
    //var groupColumn = 0;
    $('#tbl-usergrplist-setup').DataTable({
        "dom": '<"top"iflp<"clear">>rt<"bottom"iflp<"clear">>',
        dom: 'Bfrtip',
        pageLength: 5,
        buttons: [
        ],
        //"columnDefs": [
        //    { "visible": false, "targets": groupColumn }
        //],
        //"order": [[groupColumn, 'asc']],
       
        //"drawCallback": function (settings) {
        //    var api = this.api();
        //    var rows = api.rows({ page: 'current' }).nodes();
        //    var last = null;

        //    api.column(groupColumn, { page: 'current' }).data().each(function (group, i) {
        //        if (last !== group) {
        //            $(rows).eq(i).before(
        //                '<tr class="group"><td colspan="4">' + group + '</td></tr>'
        //            );

        //            last = group;
        //        }
        //    });
        //}
    });
}