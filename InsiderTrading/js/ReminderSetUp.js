var Arrfield;
let appEditor;
var mentionFeed = "";

$(document).ready(function () {    
    GetReminderName();
    //GetFieldName();
    GetAllReminder();
    $("button[id*='btnAddReminder']").on("click", function () {
        AddReminder();
    });   
    Editor();
});

$("#reminderName").on('change', function () {
    var SelectedVal = $(this).val();
    var webUrl = uri + "/api/ReminderSetUp/GetFieldName";
    $.ajax({
        type: "POST",
        url: webUrl,
        data: JSON.stringify({
            REMINDER_NM: SelectedVal
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: true,
        success: function (msg) {
            if (msg.StatusFl == true) {
                Arrfield = new Array();
                //mentionFeed = "[";
                //feed: ['@[Name]', '@[FromDate]', '@[ToDate]', '@[Email]', '@[Status]'],
                var str = "<textarea id='txtTemplate' name='txtTemplate' class='form - control form - control - solid'></textarea>";
                for (var index = 0; index < msg.listReminder.length; index++) {
                    var obj = new Object();
                    obj.fieldName = "@" + msg.listReminder[index].FIELD_NM + "";
                    //obj.id = msg.listReminder[index].FIELD_NM;
                    //obj.fieldName = msg.listReminder[index].FIELD_NM;
                    Arrfield.push(obj.fieldName);                    
                    //mentionFeed += [obj.fieldName];                      
                }
                $("#divTextarea").html('');
                $("#divTextarea").html(str);
                //mentionFeed += "]";
                //var text = mentionFeed;
                //mentionFeed = text.replace(",]", "]");
                //mentionFeed += ",";
                //alert(Arrfield);
                Editor();
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
            alert(response.status + ' ' + response.statusText);
        }
    });
});

function GetReminderName() {
    $("#Loader").show();
    var webUrl = uri + "/api/ReminderSetUp/GetReminderName";
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
            //debugger;
            $("#Loader").hide();
            if (msg.StatusFl == true) {
                var str = "";
                str += '<option value="0">Select Reminder</option>';
                for (var index = 0; index < msg.listReminder.length; index++) {
                    str += '<option value="' + msg.listReminder[index].REMINDER_NM + '" text="' + msg.listReminder[index].REMINDER_NM + '">' + msg.listReminder[index].REMINDER_NM + '</option>';
                }
                $("#reminderName").html(str);
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
            alert(response.status + ' ' + response.statusText);
        }
    });
}

function GetFieldName() {
    $("#Loader").show();
    var webUrl = uri + "/api/ReminderSetUp/GetFieldName";
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
            //debugger;
            $("#Loader").hide();
            if (msg.StatusFl == true) {
                var str = "";
                //str += '<option value="0">Select Field</option>';
                Arrfield = new Array();                
                for (var index = 0; index < msg.listReminder.length; index++) {
                    var obj = new Object();
                    obj.fieldName = "@" + msg.listReminder[index].FIELD_NM;
                    Arrfield.push(obj);
                    //str += '<option value="' + msg.listReminder[index].FIELD_NM + '">' + msg.listReminder[index].FIELD_NM + '</option>';
                }               
                //$("#ddlField").html(str);
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
            alert(response.status + ' ' + response.statusText);
        }
    });
}

function Editor(template) {
    ClassicEditor
        .create(document.querySelector("textarea[id*='txtTemplate']"), {
            toolbar: {
                items: [
                    'heading',
                    '|',
                    'bold',
                    'italic',
                    'link',
                    'bulletedList',
                    'numberedList',
                    '|',
                    'outdent',
                    'indent',
                    '|',
                    'imageUpload',
                    'blockQuote',
                    'insertTable',
                    'mediaEmbed',
                    'undo',
                    'redo'
                ]
            },
            language: 'en',
            image: {
                toolbar: [
                    'imageTextAlternative',
                    'imageStyle:inline',
                    'imageStyle:block',
                    'imageStyle:side'
                ]
            },
            table: {
                contentToolbar: [
                    'tableColumn',
                    'tableRow',
                    'mergeTableCells',
                    'tableCellProperties',
                    'tableProperties'
                ]
            },
            mention: {
                feeds: [
                    {
                        marker: '@',
                        //feed: ['@[Name]', '@[FromDate]', '@[ToDate]', '@[Email]', '@[Status]'],
                        feed: Arrfield,
                        minimumCharacters: 0
                    }
                ]
                //feeds: [
                //    {
                //        marker: '@',
                //        feed: [ /* ... */],
                //        // Define the custom item renderer.
                //        itemRenderer: customItemRenderer
                //    }
                //]
            }
        }).then(editor => {
            window.editor = editor;
            appEditor = editor;
            if (template != null && template != undefined) {
                appEditor.setData(template);
            }
        })
        .catch(error => {
            console.error(error);
        });
}

function AddReminder() {   
    $("#reminderID").val('0');
    $("#reminderName").val('');
    $("#reminderName").prop("disabled", false);
    $("#typeofReminder").val('0').change();
    $("#typeValue").val('')
    $("#stack1").modal("show");
    $("#txtSubject").val('');
    $("#txtDuration").val('');
    appEditor.setData('');
    //$(".summernote").summernote('code', '');
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
                    //if (msg.listReminder[i].REMINDER_TYPE == "1") {
                    //    result += '<td>One Time</td>';
                    //}
                    //if (msg.listReminder[i].REMINDER_TYPE == "2") {
                    //    result += '<td>Days</td>';
                    //}
                    //if (msg.listReminder[i].REMINDER_TYPE == "3") {
                    //    result += '<td>Hours</td>';
                    //}
                    result += '<td>' + msg.listReminder[i].REMINDER_TYPE + '</td>';
                    result += '<td>' + msg.listReminder[i].DURATION + '</td>';
                    //result += '<td>' + msg.listUPSIVendor[i].CreatedBy + '</td>';
                    result += '<td id="tdEditDelete_' + msg.listReminder[i].REMINDER_ID + '"><a data-target="#stack1" data-toggle="modal" id="a_' + msg.listReminder[i].REMINDER_ID + '" class="btn btn-outline dark" onclick=\"javascript:fnEditReminderById(' + msg.listReminder[i].REMINDER_ID + ');\">Edit</a><a class="btn btn-outline dark" onclick=\"javascript:fnDelete(' + msg.listReminder[i].REMINDER_ID + ');\">Delete</a></td>';

                }

                var table = $('#tbl-reminder-setup').DataTable();
                table.destroy();
                $("#tbdreminderlist").html(result);
                initializeDataTable();


            }
            else {
                alert(msg.Msg);
            }
            $("#Loader").hide();
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
            REMINDER_ID:id
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
                $("#reminderID").val(msg.listReminder[0].REMINDER_ID);
                $("#reminderName").val(msg.listReminder[0].REMINDER_NM);
                $("#reminderName").prop("disabled", true);
                $("#typeofReminder").val(msg.listReminder[0].REMINDER_TYPE).change();
                $("#typeValue").val(msg.listReminder[0].REMINDER_TYPE_VALUE);
                $("#txtDuration").val(msg.listReminder[0].DURATION);
                $("#txtSubject").val(msg.listReminder[0].SUBJECT); 
                var template = msg.listReminder[0].TEMPLATE;
                getEventNamebyId(msg.listReminder[0].REMINDER_NM, template);
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

function getEventNamebyId(eventName,template) {
    var SelectedVal = eventName;
    var webUrl = uri + "/api/ReminderSetUp/GetFieldName";
    $.ajax({
        type: "POST",
        url: webUrl,
        data: JSON.stringify({
            REMINDER_NM: SelectedVal
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            if (msg.StatusFl == true) {
                Arrfield = new Array();
                var str = "<textarea id='txtTemplate' name='txtTemplate' class='form-control form-control-solid'></textarea>";
                for (var index = 0; index < msg.listReminder.length; index++) {
                    var obj = new Object();
                    obj.fieldName = "@" + msg.listReminder[index].FIELD_NM + "";
                    Arrfield.push(obj.fieldName);                    
                }
                $("#divTextarea").html('');
                $("#divTextarea").html(str);
                Editor(template);
                
                //if (template != null) {
                //    appEditor.setData(template);
                //}
                //alert("done");
                return true;
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
            alert(response.status + ' ' + response.statusText);
        }
    });
}

function fnDelete(id) {
    $("#Loader").show();
    var objReminder = new Object();
    objReminder.REMINDER_ID = id;
    var webUrl = uri + "/api/ReminderSetUp/ReminderDelete";
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
    //debugger;
    var remid = $("#reminderID").val();
    var remName = $("#reminderName").val();
    var remType = $("#typeofReminder").val();
    var remvalu = $("#typeValue").val();
    var duration = $("#txtDuration").val();
    var subject = $("#txtSubject").val();
    var template = appEditor.getData();
    var Count = 0;
    if (remName == undefined || remName == "" || remName == null ) {
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
        Count++
        return false;
    }
    else {
        $('#lbltypeofReminder').removeClass('required');
        $('#typeofReminder').removeClass('required-red-border');

    }
    //if (remType == "0" || remType == "1") {


    //}
    //else {
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
    //}
    if (duration == undefined || duration == "" || duration == null){
        $('#lblDuration').addClass('required');
        $('#txtDuration').addClass('required-red');
        Count++
        return false;
    }
    else {
        $('#lblDuration').removeClass('required');
        $('#txtDuration').removeClass('required-red');
    }
    if (subject == undefined || subject == "" || subject == null) {
        $('#lblSubject').addClass('required');
        $('#txtSubject').addClass('required-red');
        Count++
        return false;
    }
    else {
        $('#lblSubject').removeClass('required');
        $('#txtSubject').removeClass('required-red');
    }
    if (template == undefined || template == "" || template == null) {
        $('#lbltextArea').addClass('required');
        $('#txtTemplate').addClass('required-red');
        Count++
        return false;
    }
    else {
        $('#lbltextArea').removeClass('required');
        $('#txtTemplate').removeClass('required-red');
    }
    var objReminder = new Object();
    objReminder.REMINDER_ID = remid;
    objReminder.REMINDER_NM = remName;
    objReminder.REMINDER_TYPE = remType;
    objReminder.REMINDER_TYPE_VALUE = remvalu;
    objReminder.DURATION = duration;
    objReminder.SUBJECT = subject;
    objReminder.TEMPLATE = template;
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

function fnCloseModal() {

}