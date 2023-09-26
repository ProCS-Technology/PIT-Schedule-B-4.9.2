$(document).ready(function () {
    window.history.forward();
    function preventBack() { window.history.forward(1); }
    $("#Loader").hide();
    //$('#txtAsOfDate').datepicker({
    //    todayHighlight: true,
    //    autoclose: true,
    //    format: "dd/mm/yyyy",
    //    clearBtn: true,
    //    daysOfWeekDisabled: [0, 1, 2, 3, 4, 6]
    //}).attr('readonly', 'readonly');
    var currentDate = new Date();
    $('#txtFromDate').datepicker({
        todayHighlight: true,
        autoclose: true,
        format: $("input[id*=hdnJSDateFormat]").val(),
        clearBtn: true,
        endDate: "currentDate",
        maxDate: currentDate,
        daysOfWeekDisabled: [0, 2, 3, 4, 5, 6]
        // daysOfWeekDisabled: [0, 1, 2, 6]
    });
    $('#txtToDate').datepicker({
        todayHighlight: true,
        autoclose: true,
        format: $("input[id*=hdnJSDateFormat]").val(),
        clearBtn: true,
        endDate: "currentDate",
        maxDate: currentDate,
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
    $("#Loader").show();
    var webUrl = uri + "/api/Benpos/GetAllBenposHdr";
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
                    if (index == 0) {
                        str += '<td><a class="fa fa-trash" style="color:red;margin-left:10px;" data-target="#modalDeleteBenposDetail" data-toggle="modal" id="d' + msg.BenposHeaderList[index].id + '" onclick=\'javascript:fnDeleteBenposList("' + msg.BenposHeaderList[index].id + '");\'"></a></td>';
                    }
                    else {
                        str += '<td></td>';
                    }
                    str += '</tr>';
                }

                var table = $('#tbl-Benpos-setup').DataTable();
                table.destroy();
                $("#tbdBenpos").html(str);
                initializeDataTableHdr('tbl-Benpos-setup', [0, 1, 2, 3]);
            }
        },
        error: function (error) {
            $("#Loader").hide();
            alert(error.status + ' ' + error.statusText);
        }
    })
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
        alert("Please select company");
        return false;
    }
    else {
        $('#lblCompany').removeClass('requied');
    }
    if ($("#txtFromDate").val() == undefined || $("#txtFromDate").val() == null || $("#txtFromDate").val().trim() == '') {
        count++;
        $('#lblFromDate').addClass('requied');
        alert("Please enter from date");
        return false;
    }
    else {
        $('#lblFromDate').removeClass('requied');
    }
    if ($("#txtToDate").val() == undefined || $("#txtToDate").val() == null || $("#txtToDate").val().trim() == '') {
        count++;
        $('#lblToDate').addClass('requied');
        alert("Please enter to date");
        return false;
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
        alert("Please enter vwap");
        return false;
    }
    else {
        $('#lblVWAP').removeClass('requied');
    }
    var itemFile = $("#fileUploadImage").get(0).files;
    var arrayExtensions = ["xlsx", "xls"];
    if ($('#fileUploadImage').val() == undefined || $('#fileUploadImage').val() == null || $('#fileUploadImage').val().trim() == '') {
        count++;
        $('#lblUpload').addClass('requied');
    }
    else if ($.inArray(itemFile[0].name.split('.').pop().toLowerCase(), arrayExtensions) == -1) {
        count++;
        alert("Only xlsx or xls format is allowed in Benpos Document.");
    }
    else {
        $('#lblUpload').removeClass('requied'); 
    }

    var itemFileESOP = $("#fileUploadImageESOP").get(0).files;
    var arrayExtensionsESOP = ["xlsx", "xls"];
    if (itemFileESOP.length > 0) {
        if ($.inArray(itemFileESOP[0].name.split('.').pop().toLowerCase(), arrayExtensionsESOP) == -1) {
            count++;
            alert("Only xlsx or xls format is allowed in ESOP Document.");
        }
        else {
            $('#lblUploadESOP').removeClass('requied');
        }
    }
    else {
        $('#lblUploadESOP').removeClass('requied');
    }

    if (count > 0) {
        return false;
    }
    else {
        return true;
    }
}
function fnAddUpdateBenposHdr() {
    $("#Loader").show();
    var filesData = new FormData();
    var document = $("#fileUploadImage").get(0).files[0].name;
    var documentESOP = "";
    if ($("#fileUploadImageESOP").get(0).files.length > 0) {
        documentESOP = $("#fileUploadImageESOP").get(0).files[0].name;
    }
    else {
        documentESOP = "";
    }
    var restrictedCompany = $("#ddlRestrictedCompanies").val().trim();
    //var txtAsOfDate = $("#txtAsOfDate").val().trim();
    var fromDate = $("#txtFromDate").val().trim();
    var toDate = $("#txtToDate").val().trim();
    var type = $("select[id*=ddlDepository").val().trim();
    var vwap = $("#txtVWAP").val().trim();

    filesData.append("Object", JSON.stringify({
        restrictedCompany: { ID: restrictedCompany },
        asOfDate: toDate,
        fromDate: fromDate,
        toDate: toDate,
        type: type,
        vwap: vwap,
        fileName: document,
        fileNameESOP: documentESOP
    }));
    filesData.append("FilesBenpos", $("#fileUploadImage").get(0).files[0]);
    filesData.append("FilesESOP", $("#fileUploadImageESOP").get(0).files[0]);
    var webUrl = uri + "/api/Benpos/SaveBenposHdr";
    $.ajax({
        type: 'POST',
        url: webUrl,
        data: filesData,
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
                    $('#fileUploadImage').val("");
                    return false;
                }
            }
            else {
                $("#Loader").hide();
                //  UploadFiles(document);
                if (msg.Msg == undefined) {
                    alert(msg);
                }
                else {
                    alert(msg.Msg);
                    //fnRunNonCompliantFinderExe();
                    if (msg.BenposNonCompliantList != null) {
                        $("#lstNonCompliantTask").modal('show');
                        var result = '';
                        for (i = 0; i < msg.BenposNonCompliantList.length; i++) {
                            result += '<tr>';
                            result += '<td>' + msg.BenposNonCompliantList[i].UserNm + '</td>';
                            result += '<td>' + msg.BenposNonCompliantList[i].RelativeNm + '</td>';
                            result += '<td>' + msg.BenposNonCompliantList[i].RelationNm + '</td>';
                            result += '<td>' + msg.BenposNonCompliantList[i].PAN + '</td>';
                            result += '<td>' + msg.BenposNonCompliantList[i].Folio + '</td>';
                            if (msg.BenposNonCompliantList[i].NonComplianceType == 'Window Closure' || msg.BenposNonCompliantList[i].NonComplianceType == 'Contra Trade') {
                                result += '<td class="text-danger">';
                            }
                            else {
                                result += '<td>';
                            }
                            result += '<td>' + msg.BenposNonCompliantList[i].NonComplianceType + '</td>';
                            result += '<td>' + msg.BenposNonCompliantList[i].Qty + '</td>';
                            result += '<td>' + msg.BenposNonCompliantList[i].TradeVal + '</td>';
                            result += '<td class="display-hide">N/A</td>';
                            result += '</tr>';
                        }

                        $("#tbdNonCompliantTaskList").html(result);

                        var table = $('#tbl-benposReport-setup').DataTable();
                        table.destroy();
                        $("#tbdNonCompliantTaskList").html(result);
                        initializeDataTable('tbl-benposReport-setup', [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]);
                    }

                    $('#BenposModal').modal('hide');
                    fnGetAllBenposHdr();
                    $("#ddlRestrictedCompanies").val('');
                    $("#txtAsOfDate").val('');
                    $("#txtAsOfDate").datepicker("setDate", '');
                    $("select[id*=ddlDepository").val('');
                    $("#fileUploadImage").val('');
                }
                
            }
        },
        error: function (error) {
            $("#Loader").hide();
            alert(error.status + ' ' + error.statusText);
        }
    })
}
function fnClearBenposForm() {
    $('#lblCompany').removeClass('requied');
    $('#lblAsOfDate').removeClass('requied');
    $('#lblType').removeClass('requied');
    $('#lblVWAP').removeClass('requied');
    $('#lblUpload').removeClass('requied');
    $('#lblUploadESOP').removeClass('requied');

    $("#ddlRestrictedCompanies").val('');
    $("#txtAsOfDate").val('');
    //$("select[id*=ddlDepository").val('');
    $("#txtVWAP").val('');
    $("#fileUploadImage").val('');
    $("#fileUploadImageESOP").val("");
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
function fnRunNonCompliantFinderExe() {
    $("#Loader").show();
    var webUrl = uri + "/api/NonCompliantTask/RunNonCompliantFinder";
    $.ajax({
        type: 'GET',
        url: webUrl,
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        //  async: false,
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
                    alert(msg.Msg);
                    return false;
                }
            }
            else {
                $("#lstNonCompliantTask").modal('show');
                var result = '';
                for (i = 0; i < msg.NonCompliantTaskList.length; i++) {
                    result += '<tr>';
                    result += '<td>' + msg.NonCompliantTaskList[i].user.LOGIN_ID + '</td>';
                    //   result += '<td>' + msg.NonCompliantTaskList[i].user.USER_EMAIL + '</td>';
                    result += '<td>' + msg.NonCompliantTaskList[i].user.USER_NM + '</td>';
                    result += '<td>' + msg.NonCompliantTaskList[i].relative.relativeName + '</td>';
                    result += '<td>' + msg.NonCompliantTaskList[i].user.panNumber + '</td>';
                    result += '<td>' + msg.NonCompliantTaskList[i].folioNumber + '</td>';
                    result += '<td>' + msg.NonCompliantTaskList[i].tradeQuantity + '</td>';
                    //    result += '<td>' + msg.NonCompliantTaskList[i].value + '</td>';
                    result += '<td>' + msg.NonCompliantTaskList[i].numberOfShareAsPerBenpos + '</td>';
                    result += '<td>' + msg.NonCompliantTaskList[i].numberOfShareAsPerInitialHolding + '</td>';
                    result += '<td>' + msg.NonCompliantTaskList[i].isNonCompliant + '</td>';
                    result += '</tr>';
                }

                $("#tbdNonCompliantTaskList").html(result);
            }
        },
        error: function (error) {
            $("#Loader").hide();
            alert(error.status + ' ' + error.statusText);
        }
    })
}
function fnSendEmailForNC() {
    $("#Loader").show();
    var webUrl = uri + "/api/NonCompliantTask/SendEmailForNC";
    $.ajax({
        type: 'GET',
        url: webUrl,
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        //   async: false,
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
                    alert(msg.Msg);
                    return false;
                }
            }
            else {
                $("#lstNonCompliantTask").modal('hide');
                alert(msg.Msg);
            }
        },
        error: function (error) {
            $("#Loader").hide();
            alert(error.status + ' ' + error.statusText);
        }
    })
}
function FormatDate(dateString) {
    return dateString.split("/")[1] + "/" + dateString.split("/")[0] + "/" + dateString.split("/")[2];
}
function fnDeleteBenposList(id) {
    $("#txtDeleteBenposListId").val(id);
}
function fnDeleteBenposListModal() {
    if ($("#txtDeleteBenposListId").val() == "0") {
        
    }
    else {
        $("#Loader").show();
        var webUrl = uri + "/api/Benpos/DeleteBenposDetail";
        $.ajax({
            url: webUrl,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            data: JSON.stringify({
                id: $("#txtDeleteBenposListId").val()
            }),
            success: function (msg) {
                $("#Loader").hide();
                if (isJson(msg)) {
                    msg = JSON.parse(msg);
                }
                if (msg.StatusFl == true) {
                    alert("Benpos has been deleted successfully!");
                    window.location.reload();
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
        })
    }
}