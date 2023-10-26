$(document).ready(function () {
    $("#Loader").hide();

    fnGetUserList();
    fnGetApproverList();

    // Allow Only Number

    $(".number").keypress(function (e) {
        
        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
            return false;
        }

        else if (this.value.length == 0 && e.which === 48) {
            return false;
        }

    });
    $('.number').bind("cut copy paste", function (e) {

        if (e.originalEvent.clipboardData.getData('Text').match(/[^\d]/)) {
            e.preventDefault();
        }


    });
});



function initializeDataTable(id, columns) {
    $('#' + id).DataTable({
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'pdf',
                className: 'btn green btn-outline',
                exportOptions: {
                    columns: columns
                }
            },
            {
                extend: 'excel',
                className: 'btn yellow btn-outline ',
                exportOptions: {
                    columns: columns,
                    format: {
                        body: function (data, column, row, node) {
                            return column === 4 ? "\u200C" + data : data;
                        }
                    }
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

function fnCloseAdUserPopUp() {
    window.location.reload(true);
}



function OpenNew() {
    $("span[Id*='spnTitle']").html("New User");
    $("#txtWFId").val('0');
    fnGetUserList();

}

function fnSaveUser() {
    if (fnValidate())

    {
        
          fnSaveApproverSetUp();
       
        
            $("#stack1").modal('hide');
         
    }
    else {
        $('#btnSave').removeAttr("data-dismiss");
        return false;
    }
}

function fnGetUserList() {
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/GetUserList";
    $.ajax({
        type: "GET",
        url: webUrl,
        data: "{}",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            if (msg.StatusFl == true) {
                var result = "";
                result += '<option value="0" selected>Please Select</option>';
                for (var i = 0; i < msg.UserList.length; i++) {
                    result += '<option value="' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].USER_EMAIL + ' (' + msg.UserList[i].USER_NM + ')' + '</option>';

                }
                $("#ddlUser").html(' ');
                $("#ddlUser").html(result);

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

function fnGetApproverList() {
    $("#Loader").show();
    var webUrl = uri + "/api/ApproverSetUp/GetApproverSetUpLIST";
    $.ajax({
        type: "GET",
        url: webUrl,
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: true,
        success: function (msg) {
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl) {
                var result = "";
                for (var i = 0; i < msg.ApproverSetUpList.length; i++) {
                    var seq = i + 1;
                    result += '<tr id="tr_' + msg.ApproverSetUpList[i].WF_ID + '">';
                    result += '<td>' + seq + '</td>';
                    result += '<td>' + msg.ApproverSetUpList[i].USER_LOGIN + '</td>';
                    result += '<td>' + msg.ApproverSetUpList[i].MIN_LIMIT + '</td>';
                    result += '<td>' + msg.ApproverSetUpList[i].MAX_LIMIT + '</td>';
                    result += '<td>' + msg.ApproverSetUpList[i].CREATED_BY + '</td>';

                    result += '<td id="tdEdit_' + msg.ApproverSetUpList[i].WF_ID + '"><a data-target="#userModel" data-toggle="modal"  id="a_' + msg.ApproverSetUpList[i].WF_ID + '" class="btn btn-outline dark" onclick=\"javascript:fnGetApproverListById(' + msg.ApproverSetUpList[i].WF_ID + ');\">Edit</a>&nbsp;&nbsp;<a  id="a_' + msg.ApproverSetUpList[i].WF_ID + '" class="btn btn-outline dark" onclick=\"javascript:DeleteApproverSetUp(' + msg.ApproverSetUpList[i].WF_ID + ');\">Delete</a></td>';
                    result += '</tr>';
                }
                var table = $('#tbl-Designation-setup').DataTable();
                table.destroy();
                $("#tbApprover").html(result);
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

function fnGetApproverListById(id) {
    $("#Loader").show();
    var webUrl = uri + "/api/ApproverSetUp/GetApproverSetUpById";
    $.ajax({
        type: "POST",
        url: webUrl,
        data: JSON.stringify({
            WF_ID: id
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: true,
        success: function (msg) {
          
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl) {

                if (msg.approverSetUp !== null) {
                    $("#txtWFId").val(id);
                    $("#ddlUser").val(msg.approverSetUp.USER_LOGIN);
                    $("#txtMinLimit").val(msg.approverSetUp.MIN_LIMIT);
                    $("#txtMaxLimit").val(msg.approverSetUp.MAX_LIMIT);
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



function fnSaveApproverSetUp() {
    
    var ObjApprover = new Object();
    ObjApprover.WF_ID = $("#txtWFId").val();
    ObjApprover.USER_LOGIN = $('#ddlUser').val();
    ObjApprover.MIN_LIMIT = $('#txtMinLimit').val();
    ObjApprover.MAX_LIMIT = $('#txtMaxLimit').val();


    var webUrl = uri + "/api/ApproverSetUp/SaveApproverSetUp";
    $("#Loader").show();
    setTimeout(function () {
        $.ajax({
            type: 'POST',
            url: webUrl,
            data: JSON.stringify(ObjApprover),




            contentType: "application/json; charset=utf-8",
            datatype: "json",
            async: false,
            success: function (msg) {
              //  debugger;
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
                        $("#stack1").modal('hide');
                        fnGetApproverList();
                        window.location.reload();
                    }
                }
                else {
                    alert(msg.Msg);
                    $("#stack1").modal('hide');
                    fnGetApproverList();   
                    fnClearForm();
                   
                }


            },
            error: function (error) {
                $("#Loader").hide();

                //   $('#btnSave').removeAttr("data-dismiss");

                if (error.responseText == "Session Expired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                    return false;
                }
                else {
                    alert(error.status + ' ' + error.statusText);
                }
            }
        })
    }, 10);

    
}

function DeleteApproverSetUp(id) {
    $("#Loader").show();
       var webUrl = uri + "/api/ApproverSetUp/DeleteApproverSetUp";
    $.ajax({
        type: 'POST',
        url: webUrl,
        data: JSON.stringify({
            WF_ID: id
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
                window.location.reload(true);
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

function fnCloseModal() {
    fnClearForm();
}




function fnClearForm() {
    $('#WF_ID').val("");
   // $('#ddlUser').val("");
    $('#txtMinLimit').val("");
    $('#txtMaxLimit').val("");
    
    $("#stack1").modal('hide');
}
function fnValidate() {
    var USER_LOGIN = $('#ddlUser').val().trim();
    var MIN_LIMIT = $('#txtMinLimit').val().trim();
    var MAX_LIMIT = $('#txtMaxLimit').val().trim();

    var count = 0;
    if (USER_LOGIN == undefined || USER_LOGIN == null || USER_LOGIN == '' || USER_LOGIN=="0") {
        count++;
        $('#lblUser').addClass('requied');
        alert('Please Select User');
        return false;
    }
   
    else {
        $('#lblUser').removeClass('requied');
    }


    if (MIN_LIMIT == undefined || MIN_LIMIT == null || MIN_LIMIT == '') {
        count++;
        $('#lblMinLimit').addClass('requied');
        alert('Please Enter Min Limit ');
        return false;
    }

    else {
        $('#lblMinLimit').removeClass('requied');
    }



    if (MAX_LIMIT == undefined || MAX_LIMIT == null || MAX_LIMIT == '') {
        count++;
        $('#lblMaxLimit').addClass('requied');
        alert('Please Enter Max Limit ');
        return false;
    }

    else {
        $('#lblMaxLimit').removeClass('requied');
    }
    
    if (parseFloat(MAX_LIMIT) <= parseFloat(MIN_LIMIT)) {
        count++;
        $('#lblMaxLimit').addClass('requied');
        alert('Max Limit should be greater than min limit');
        return false;
    }

    else {
        $('#lblMaxLimit').removeClass('requied');
    }
  
    if (count > 0) {
        return false;
    }
    else {
        return true;
    }
}

