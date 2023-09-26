$(document).ready(function () {
    $("#Loader").hide();
    window.history.forward();
    function preventBack() { window.history.forward(1); }
  

    $('#txtFromDate').datepicker({
        todayHighlight: true,
        autoclose: true,
        format: "dd/mm/yyyy",
        clearBtn: true,
        daysOfWeekDisabled: [0, 2, 3, 4, 5, 6]
        // daysOfWeekDisabled: [0, 1, 2, 6]
    });
    $('#txtToDate').datepicker({
        todayHighlight: true,
        autoclose: true,
        format: "dd/mm/yyyy",
        clearBtn: true,
        daysOfWeekDisabled: [0, 1, 2, 3, 4, 6]
        //daysOfWeekDisabled: [3,4,5]
    });
    getAllRestrictedCompanies();
    fnGetAllBenposHdr();

});
function isJson(str) {
    try {
        JSON.parse(str);
    } catch (e) {
        return false;
    }
    return true;
}
function fnRemoveClass(obj, val, source) {
    $("#lbl" + val + "").removeClass('requied');
}
function ConvertToDateTime(dateTime) {
    var date = dateTime.split(" ")[0];

    return (date.split("/")[1] + "/" + date.split("/")[0] + "/" + date.split("/")[2]);
}
function initializeDataTableHdr(id, columns) {
    $('#' + id).DataTable({
        dom: 'Bfrtip',
        pageLength: 10,
        "scrollY": "350px",
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
                }
            },
            //    { extend: 'colvis', className: 'btn dark btn-outline', text: 'Columns' }
        ]
    });
}
function initializeDataTable(id, columns) {
    $('#' + id).DataTable({
        dom: 'Bfrtip',
        pageLength: 10,
        //"scrollY": "300px",
        //"scrollX": true,
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
function fnGetAllBenposHdr() {
    $("#Loader").hide();
    var webUrl = uri + "/api/Benpos_NEW/GetAllBenposHdr";
    $.ajax({
        type: 'GET',
        url: webUrl,
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
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
                    // alert(msg.Msg);
                    return false;
                }
            }
            else {
                var str = '';
                for (index = 0; index < msg.BenposHeaderList.length; index++) {
                    str += '<tr>';
                    str += '<td>' + msg.BenposHeaderList[index].restrictedCompany.companyName + '</td>';
                    str += '<td>' + msg.BenposHeaderList[index].fromDate + '</td>';
                    str += '<td>' + msg.BenposHeaderList[index].toDate + '</td>';
                    str += '<td>' + msg.BenposHeaderList[index].type + '</td>';
                    str += '<td><a class="fa fa-download" target="_blank" href="Benpos/' + msg.BenposHeaderList[index].fileName + '"></a></td>';
                    str += '</tr>';
                }

                var table = $('#tbl-Benpos-setup').DataTable();
                table.destroy();
                $("#tbdBenpos2").html(str);
                initializeDataTableHdr('tbl-Benpos-setup', [0, 1, 2, 3]);
            }
        },
        error: function (error) {
            $("#Loader").hide();
            alert(error.status + ' ' + error.statusText);
        }
    })
}
function getAllRestrictedCompanies() {
    $("#Loader").show();
    var webUrl = uri + "/api/RestrictedCompanies/GetRestrictedCompanies";
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
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                var str = "";
                str += '<option value=""></option>';
                for (var index = 0; index < msg.RestrictedCompaniesList.length; index++) {
                    str += '<option value="' + msg.RestrictedCompaniesList[index].ID + '">' + msg.RestrictedCompaniesList[index].companyName + '</option>';
                }
                $("#ddlRestrictedCompanies").html(str);
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

function fnSubmitBenposFile() {
    if (fnValidateBenposHdr()) {
        fnAddUpdateBenposHdr();
    }
}
function fnValidateBenposHdr() {
    
    var count = 0;
    if ($("#ddlRestrictedCompanies").val() == undefined || $("#ddlRestrictedCompanies").val() == null || $("#ddlRestrictedCompanies").val().trim() == '' || $("#ddlRestrictedCompanies").val() == '0') {
        count++;
        $('#lblCompany').addClass('requied');
    }
    else {
        $('#lblCompany').removeClass('requied');
    }
   
    if ($("#txtFromDate").val() == undefined || $("#txtFromDate").val() == null || $("#txtFromDate").val().trim() == '') {
        count++;
        $('#lblFromDate').addClass('requied');
    }
    else {
        $('#lblFromDate').removeClass('requied');
    }
    if ($("#txtToDate").val() == undefined || $("#txtToDate").val() == null || $("#txtToDate").val().trim() == '') {
        count++;
        $('#lblToDate').addClass('requied');
    }
    else {
        $('#lblToDate').removeClass('requied');
    }

    var FromDate = new Date(FormatDate($("#txtFromDate").val()));
    var Todate = new Date(FormatDate($("#txtToDate").val()));

    if (Todate < FromDate) {
        count++;
        $('#lblToDate').addClass('requied');
        alert("To Date Should be greater than From Date");
        return false;
    }

    else {
        $('#lblToDate').removeClass('requied');
    }

    if ($("select[id*=ddlDepository").val() == undefined || $("select[id*=ddlDepository").val() == null || $("select[id*=ddlDepository").val().trim() == '' || $("select[id*=ddlDepository").val().trim() == '0') {
        count++;
        $('#lblType').addClass('requied');
    }
    else {
        $('#lblType').removeClass('requied');
    }
    if ($("#txtVWAP").val() == undefined || $("#txtVWAP").val() == null || $("#txtVWAP").val().trim() == '') {
        count++;
        $('#lblVWAP').addClass('requied');
    }
    else {
        $('#lblVWAP').removeClass('requied');
    }

    if ($("#txtfilename").val() == undefined || $("#txtfilename").val() == null || $("#txtfilename").val().trim() == '') {
        count++;
        $('#txtfilename').addClass('requied');
    }
    else {
        $('#txtfilename').removeClass('requied');
    }


    if (count > 0) {
        return false;
    }
    else {
        return true;
    }
}
function fnAddUpdateBenposHdr() {
    
    var restrictedCompany = $("#ddlRestrictedCompanies").val().trim();
    var fromDate = $("#txtFromDate").val().trim();
    var toDate = $("#txtToDate").val().trim();
    var type = $("select[id*=ddlDepository").val().trim();
    var vwap = $("#txtVWAP").val().trim();
    var filename = $("#txtfilename").val().trim();

 
    var webUrl = uri + "/api/BENPOS_NEW/SaveBenposHdr";
    $.ajax({
        type: 'POST',
        url: webUrl,
        data: JSON.stringify({
        restrictedCompany: { ID: restrictedCompany },
        asOfDate: toDate,
        fromDate: fromDate,
        toDate: toDate,
        type: type,
        vwap: vwap,
        fileName: filename
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        contentType: false,
        processData: false,
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
                    window.location.reload(true);
                    $('#btnSave').removeAttr("data-dismiss");
                    return false;
                }
            }
            else {
                alert(msg.Msg);
            }


      var table = $('#tbl-benposReport-setup').DataTable();
            table.destroy();
            $('#BenposModal2').modal('hide');
            fnGetAllBenposHdr();
            $("#Loader").hide();         
            fnClearBenposForm();
                $('#btnSave').attr("data-dismiss", "modal");
           return true;
         
        },
error: function (error) {
    $("#Loader").hide();
    alert(error.status + ' ' + error.statusText);
    $('#btnSave').removeAttr("data-dismiss");
}
    })
}

function fnClearBenposForm() {
    $('#lblCompany').removeClass('requied');
    $('#lblAsOfDate').removeClass('requied');
    $('#lblType').removeClass('requied');
    $('#lblVWAP').removeClass('requied');
    $('#lblUpload').removeClass('requied');

    $("#ddlRestrictedCompanies").val('');
    $("#txtFromDate").val('');
    $("#txtToDate").val('');
    $("#txtVWAP").val('');
    $("#txtfilename").val('');
  
}
function FormatDate(dateString) {
    return dateString.split("/")[1] + "/" + dateString.split("/")[0] + "/" + dateString.split("/")[2];
}