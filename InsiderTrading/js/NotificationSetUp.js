$(document).ready(function () {
    GetAllReminder();
    $("button[id*='btnAddReminder']").on("click", function () {
        AddReminder();


    });


});

function AddReminder() {

    $("#reminderID").val('0')
    $("#reminderName").val('')
    $("#typeofReminder").val('0').change();
    $("#typeValue").val('')
    $("#stack1").modal("show");
}

function GetAllReminder() {

    var webUrl = uri + "/api/ReminderSetUp/getallReminder";
    $.ajax({
        url: webUrl,
        type: "POST",
        async: false,
        data: JSON.stringify({
            Reminderid: "0"

        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {

            $("#Loader").hide();
            if (msg.Msg == "SessionExpired") {
                alert("Your session is expired. Please login again to continue");
                window.location.href = "../Login.aspx";
            }

            if (msg.StatusFl == true) {
                var result = "";
                for (var i = 0; i < msg.listReminder.length; i++) {
                    result += '<tr id="tr_' + msg.listReminder[i].REMINDER_ID + '">';
                    //result += '<td>' +() + '</td>';
                    result += '<td>' + msg.listReminder[i].REMINDER_NM + '</td>';
                    if (msg.listReminder[i].REMINDER_TYPE == "1") {
                        result += '<td>One Time</td>';
                    }
                    if (msg.listReminder[i].REMINDER_TYPE == "2") {
                        result += '<td>Days</td>';
                    }
                    if (msg.listReminder[i].REMINDER_TYPE == "3") {
                        result += '<td>Hours</td>';
                    }

                    result += '<td>' + msg.listReminder[i].REMINDER_TYPE_VALUE + '</td>';
                    //result += '<td>' + msg.listUPSIVendor[i].CreatedBy + '</td>';
                    result += '<td id="tdEditDelete_' + msg.listReminder[i].REMINDER_ID + '"><a data-target="#stack1" data-toggle="modal" id="a_' + msg.listReminder[i].REMINDER_ID + '" class="btn btn-outline dark" onclick=\"javascript:fnEditReminderById(' + msg.listReminder[i].REMINDER_ID + ');\">Edit</a></td>';

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

function fnEditReminderById(id) {


    var webUrl = uri + "/api/ReminderSetUp/getallReminderById";
    $.ajax({
        url: webUrl,
        type: "POST",
        async: false,
        data: JSON.stringify({
            REMINDER_ID: id

        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {

            $("#Loader").hide();
            if (msg.Msg == "SessionExpired") {
                alert("Your session is expired. Please login again to continue");
                window.location.href = "../Login.aspx";
            }

            if (msg.StatusFl == true) {
                $("#reminderID").val(msg.listReminder[0].REMINDER_ID)
                $("#reminderName").val(msg.listReminder[0].REMINDER_NM)
                $("#typeofReminder").val(msg.listReminder[0].REMINDER_TYPE).change();
                $("#typeValue").val(msg.listReminder[0].REMINDER_TYPE_VALUE)



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


function fnSaveupsi() {
    debugger;
    var remid = $("#reminderID").val();
    var remName = $("#reminderName").val();
    var remType = $("#typeofReminder").val();
    var remvalu = $("#typeValue").val();
    var Count = 0;
    if (remName == undefined || remName == "" || remName == null) {
        // $('#reminderName').addClass('required-red-border');
        $('#lblreminderName').addClass('required');
        $('#reminderName').addClass('required-red');
        Count++
        return false;

    }
    else {
        $('#lblreminderName').removeClass('required');
        $('#reminderName').removeClass('required-red');

    }

    if (remType == undefined || remType == "" || remType == null || remType == "0") {
        $('#typeofReminder').addClass('required-red-border');
        $('#lbltypeofReminder').addClass('required');
        //$('#reminderName').addClass('required');
        Count++
        return false;
    }
    else {
        $('#lbltypeofReminder').removeClass('required');
        $('#typeofReminder').removeClass('required-red-border');

    }

    if (remType == "0" || remType == "1") {


    }
    else {

        if (remvalu == undefined || remvalu == "" || remvalu == null || remvalu == "0") {
            // $('#reminderName').addClass('required-red-border');
            $('#lbltypeValue').addClass('required');
            $('#typeValue').addClass('required-red');
            Count++
            return false;
        }
        else {
            $('#lblreminderName').removeClass('required');
            $('#reminderName').removeClass('required-red');

        }
    }
    var objReminder = new Object();
    objReminder.REMINDER_ID = remid;
    objReminder.REMINDER_NM = remName;
    objReminder.REMINDER_TYPE = remType;
    objReminder.REMINDER_TYPE_VALUE = remvalu;
    $("#Loader").show();
    var webUrl = uri + "/api/ReminderSetUp/ReminderSave";

    $.ajax({
        url: webUrl,
        type: "POST",
        async: false,
        data: JSON.stringify(objReminder),
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

function removRequried(val) {

    var id = val.id;
    if (id == "reminderName") {
        $('#lblreminderName').removeClass('required');
        $('#reminderName').removeClass('required-red');
    }
    if (id == "typeofReminder") {
        $('#lbltypeofReminder').removeClass('required');
        $('#typeofReminder').removeClass('required-red-border');
    }
    if (id == "typeValue") {
        $('#lbltypeofReminder').removeClass('required');
        $('#typeofReminder').removeClass('required-red-border');
    }
    if (id == "typeValue") {
        $('#lbltypeValue').removeClass('required');
        $('#typeValue').removeClass('required-red');
    }



}