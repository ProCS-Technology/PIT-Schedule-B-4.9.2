$(document).ready(function () {

    // alert("Document Is ready!");

    GetAllReminder();
   
    $("#txtNotificationMessage").summernote('code', "");

});

function GetAllReminder() {

    var webUrl = uri + "/api/ReminderModule/GetAllActivity";
    $.ajax({
        url: webUrl,
        type: "POST",
        async: false,
        data: JSON.stringify({
            ACTIVITY_ID: "0"

        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            debugger;
            $("#Loader").hide();
            if (msg.Msg == "SessionExpired") {
                alert("Your session is expired. Please login again to continue");
                window.location.href = "../Login.aspx";
            }

            if (msg.StatusFl == true) {
                var result = "";
                for (var i = 0; i < msg.listReminderModules.length; i++) {
                    result += '<tr id="tr_' + msg.listReminderModules[i].ACTIVITY_ID + '">';
                    //result += '<td>' +() + '</td>';
                    result += '<td>' + msg.listReminderModules[i].MODULE_NAME + '</td>';
                    result += '<td>' + msg.listReminderModules[i].ACTIVITY_NAME + '</td>';
                    result += '<td><input id="textreminderin' + msg.listReminderModules[i].ACTIVITY_ID+'" class="form-control form-control-inline" value="' + msg.listReminderModules[i].REMINDED_IN + '"/></td>';
                    if (msg.listReminderModules[i].UNIT_OF_MEASURE == "Hours") {
                        result += '<td><select id="ddlreminderin' + msg.listReminderModules[i].ACTIVITY_ID +'" class="form-control form-control-inline"><option value="Hours" selected>Hours</option><option value="Days">Days</option></select></td>';
                    }
                    else {
                        result += '<td><select id="ddlreminderin' + msg.listReminderModules[i].ACTIVITY_ID +'" class="form-control form-control-inline"><option value="Hours">Hours</option><option value="Days" selected>Days</option></select></td>';
                    }
                  
                    //result += '<td>' + msg.listUPSIVendor[i].CreatedBy + '</td>';
                    result += '<td id="tdEdit_' + msg.listReminderModules[i].ACTIVITY_ID + '"><a  id="a_' + msg.listReminderModules[i].ACTIVITY_ID + '" class="btn btn-outline dark" onclick=\"javascript:fnEditActivityById(' + msg.listReminderModules[i].ACTIVITY_ID + ');\">Template</a></td>';

                }

                var table = $('#tbl-reminder-setup').DataTable();
                table.destroy();
                $("#tbdreminderlist").html(result);
                initializeDataTable();


            }
            else {
                alert(msg.Msg);
            }

        },
        error: function (response) {
            $("#Loader").hide();
            if (response.responseText == "Session Expired") {
                alert("Your session is expired. Please login again to continue");
                window.location.href = "../Login.aspx";
                return false;
            }
            else {
                alert(response.status + ' ' + response.statusText);
            }

        }


    });



}

function fnEditActivityById(id) {
    $("#textActivityId").val(id);
    
    var webUrl = uri + "/api/ReminderModule/GetActivityById";
    $.ajax({
        url: webUrl,
        type: "POST",
        async: false,
        data: JSON.stringify({
            ACTIVITY_ID: id

        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            debugger;
            $("#Loader").hide();
            if (msg.Msg == "SessionExpired") {
                alert("Your session is expired. Please login again to continue");
                window.location.href = "../Login.aspx";
            }

            if (msg.StatusFl == true) {
                $("#txtNotificationMessage").summernote('code', msg.reminderModules.listfield[0].MsgTemplate);
                var result = "";
                for (var i = 0; i < msg.reminderModules.listfield.length; i++) {
                    result += '<input onClick="fnAppendinSummarNote(this)" type="checkbox" id="field' + msg.reminderModules.listfield[i].filedid + '" name="field' + msg.reminderModules.listfield[i].filedid + '" value="' + msg.reminderModules.listfield[i].fieldname + '"> <label for="field' + msg.reminderModules.listfield[i].filedid + '"> ' + msg.reminderModules.listfield[i].fieldname+'</label><br>'
                }

                $("#checklistdata").html('');

                $("#checklistdata").html(result);
                $("#stack1").modal("show");

            }
            else {
                alert(msg.Msg);
            }

        },
        error: function (response) {
            $("#Loader").hide();
            if (response.responseText == "Session Expired") {
                alert("Your session is expired. Please login again to continue");
                window.location.href = "../Login.aspx";
                return false;
            }
            else {
                alert(response.status + ' ' + response.statusText);
            }

        }


    });

   
    

}


function initializeDataTable() {
    $('#sample_1_2').DataTable({
        "dom": '<"top"iflp<"clear">>rt<"bottom"iflp<"clear">>',
        dom: 'Bfrtip',
        pageLength: 5,
        buttons: [

        ],
    });
}

function fnCloseModal() {


}


function UpdateActivity() {

    debugger;
    var activityid = $("#textActivityId").val();
    var frequence = $("#textreminderin" + activityid).val();
    var unit = $("#ddlreminderin" + activityid).val();
    var template = $("#txtNotificationMessage").summernote("code");
    var objActivity = new Object()
    objActivity.ACTIVITY_ID = activityid;
    objActivity.REMINDED_IN = frequence;
    objActivity.UNIT_OF_MEASURE = unit;
    objActivity.MSG_TEMPLATE = template;

    var webUrl = uri + "/api/ReminderModule/UpdateActivityById";
    $.ajax({
        url: webUrl,
        type: "POST",
        async: false,
        data: JSON.stringify(objActivity),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {

            $("#Loader").hide();
            if (msg.Msg == "SessionExpired") {
                alert("Your session is expired. Please login again to continue");
                window.location.href = "../Login.aspx";
            }

            if (msg.StatusFl == true) {
                $("#stack1").modal('hide');
                alert(msg.Msg);
                window.location.reload();

            }
            else {
                alert(msg.Msg);
            }

        },
        error: function (err) {
            $("#Loader").hide();
            if (err.responseText == "Session Expired") {
                alert("Your session is expired. Please login again to continue");
                window.location.href = "../Login.aspx";
                return false;
            }
            else {
                alert(err.status + ' ' + err.statusText);
            }

        }


    });
}


function fnAppendinSummarNote(data) {

   
    $(".note-editable").append(data.value);

}