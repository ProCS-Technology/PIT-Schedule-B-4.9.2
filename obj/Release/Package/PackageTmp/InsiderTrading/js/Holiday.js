var HolidayListing = null;
$(document).ready(function () {    
    $('#txtdate').datepicker({
        todayHighlight: true,
        autoclose: true,
        format: $("input[id*=hdnJSDateFormat]").val(),
        clearBtn: true
    });
    $('#Loader').hide();
    fnGetHolidayList();
    $('#stack1').on('hide.bs.modal', function () {
    });

});

function initializeDataTable() {
    $('#tbl-Holiday-setup').DataTable({
        dom: 'Bfrtip',
        buttons: [
         {
             extend: 'pdf',
             className: 'btn green btn-outline',
             exportOptions: {
                 columns: [0,1,2]
             }
         },
         {
             extend: 'excel',
             className: 'btn yellow btn-outline ',
             exportOptions: {
                 columns: [0,1,2]
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

function fnGetHolidayList() {

    $("#Loader").show();
    var webUrl = uri + "/api/Holiday/GetHolidayList";
    $.ajax({
        type: "POST",
        url: webUrl,
        data: JSON.stringify({
           ID: 0
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: true,
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                var result = "";
                if (msg.HolidayList !== null) {
                    if (msg.HolidayList.length > 0) {
                        HolidayListing = msg.HolidayList;
                        for (var i = 0; i < msg.HolidayList.length; i++) {
                            var seq = i + 1;
                            result += '<tr id="tr_' + msg.HolidayList[i].ID + '">';
                            result += '<td>' + seq + '</td>';
                            result += '<td>' + msg.HolidayList[i].HOLIDAY_DESCRIPTION + '</td>';
                            result += '<td>' + FormatDate(msg.HolidayList[i].HOLIDAY_DATE, $("input[id*=hdnDateFormat]").val()) + '</td>';

                            result += '<td id="tdEdit_' + msg.HolidayList[i].ID + '"><a data-target="#stack1" data-toggle="modal"  id="a_' + msg.HolidayList[i].ID + '" class="btn btn-outline dark" onclick=\"javascript:EditHoliday(' + msg.HolidayList[i].ID + ');\">Edit</a>&nbsp;&nbsp;<a  id="a_' + msg.HolidayList[i].ID + '" class="btn btn-outline dark" onclick=\"javascript:fnDeleteHoliday(' + msg.HolidayList[i].ID + ');\">Delete</a></td>';
                            result += '</tr>';
                        }
                    }
                }
    

                var table = $('#tbl-Holiday-setup').DataTable();
                table.destroy();
                $("#tbdHolidayList").html("");
                $("#tbdHolidayList").html(result);
                initializeDataTable();
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


function fnClearForm() {
    $('#txtDesc').val("");
    $('#txtdate').val("");
    $('#txtID').val("");
}
function fnCloseModal() {
    fnClearForm();
}

function fnSaveHoliday() {
    if (fnValidate()) {
        fnAddUpdateHoliday();
    }
    else {
        $('#btnSave').removeAttr("data-dismiss");
        return false;
    }
}


function fnAddUpdateHoliday() {
   
    var Holiday_description = $('#txtDesc').val();
    var Holiday_date = $('#txtdate').val();
    var ID = $('#txtID').val();
  

    $("#Loader").show();
    var webUrl = uri + "/api/Holiday/SaveHoliday";
    $.ajax({
        type: 'POST',
        url: webUrl,
        data: JSON.stringify({
            ID: ID, Holiday_description: Holiday_description, Holiday_date: Holiday_date
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
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
                    $('#btnSave').removeAttr("data-dismiss");
                    return false;
                    
                }
            }
            else {
                alert(msg.Msg);
                window.location.reload(true);
                //if (msg.Holiday.ID == ID) {
                //    $("#tdHoliday_description_" + msg.Holiday.ID).html(msg.Holiday.Holiday_description);
                //    $("#a_" + msg.Holiday.ID).attr("onclick", "javascript:fnEditHoliday('" + msg.Holiday.ID + "','" + msg.Holiday.Holiday_description + "');");
                //    $("#a_" + msg.Holiday.ID).attr("data-target", "#stack1");
                //    $("#a_" + msg.Holiday.ID).attr("data-toggle", "modal");
                //    $("#d_" + msg.Holiday.ID).attr("onclick", "javascript:fnDeleteHoliday('" + msg.Holiday.ID + "');");
                //    $("#d_" + msg.Holiday.ID).css({ 'margin-left': '20px' });
                //    $("#d_" + msg.Holiday.ID).attr("data-target", "#delete");
                //    $("#d_" + msg.Holiday.ID).attr("data-target", "#modal");
                //    var table = $('#tbl-Holiday-setup').DataTable();
                //    table.destroy();
                //    initializeDataTable();
                //    $("#Loader").hide();
                //}
                //else {
                //    var result = "";
                //    result += '<tr id="tr_' + msg.Holiday.ID + '">';
                //    result += '<td id="tdHoliday_description_' + msg.Holiday.ID + '">' + msg.Holiday.Holiday_description + '</td>';

                //    result += '<td id="tdEditDelete_' + msg.Holiday.ID + '"><a data-target="#stack1" data-toggle="modal" id="a_' + msg.Holiday.ID + '" class="btn btn-outline dark" onclick=\"javascript:fnEditHoliday(' + msg.Holiday.ID + ',\'' + msg.Holiday.Holiday_description + '\');\">Edit</a><a style="margin-left:20px" data-target="#delete" data-toggle="modal" id="d_' + msg.Holiday.ID + '" class="btn btn-outline dark" onclick=\"javascript:fnDeleteHoliday(' + msg.Holiday.ID + ');\">Delete</a></td>';
                //    result += '</tr>';
                //    var table = $('#tbl-Holiday-setup').DataTable();
                //    table.destroy();
                //    $("#tbdHolidayList").append(result);
                //    initializeDataTable();
                //    $("#Loader").hide();
                //}

                //fnClearForm();
                //$('#btnSave').attr("data-dismiss", "modal");
                //return true;
            }
        },
        error: function (error) {
            $("#Loader").hide();
            alert(error.status + ' ' + error.statusText);
            $('#btnSave').removeAttr("data-dismiss");
        }
    })
}

function fnValidate()
{
    if ($('#txtDesc').val() == "" || $('#txtDesc').val() == null || $('#txtDesc').val() == undefined) {
        alert("Please enter Holiday description");
        return false;
    }
    {
        if ($("#txtdate").val() == "" || $("#txtdate").val() == null || $("#txtdate").val() == undefined) {
            alert("Please enter Holiday date");
            return false;
        }
        return true;
    }
}

function EditHoliday(id) {
    for (var i = 0; i < HolidayListing.length; i++) {
        if (HolidayListing[i].ID == id) {
            $('#txtID').val(id);
            $("#txtDesc").val(HolidayListing[i].HOLIDAY_DESCRIPTION);
            $("#txtdate").val(FormatDate(HolidayListing[i].HOLIDAY_DATE, $("input[id*=hdnDateFormat]").val()));
            break;
        }
    }
}

function fnDeleteHoliday(id) {
    $("#Loader").show();
    var webUrl = uri + "/api/Holiday/DeleteHoliday";
    $.ajax({
        type: 'POST',
        url: webUrl,
        data: JSON.stringify({
            ID: id
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                alert("Record Deleted successfully !");
                var table = $('#tbl-Holiday-setup').DataTable();
                table.destroy();
                $("#tr_" + msg.Holiday.ID).remove();
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
                }
            }
        },
        error: function (response) {
            $("#Loader").hide();
            alert(response.status + ' ' + response.statusText);
        }
    });
}







