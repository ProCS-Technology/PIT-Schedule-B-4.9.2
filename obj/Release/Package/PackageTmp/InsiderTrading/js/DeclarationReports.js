var declarationArray = new Array();
$(document).ready(function () {
    window.history.forward();
    function preventBack() { window.history.forward(1); }
    $("#Loader").hide();
    // ChartsAmcharts.init();

    fnBindBusinessUnitList();
    fnGetDeclarationReports();
    ChartsFlotcharts.init();

    $("#bindBusinessUnit").select2({
        placeholder: "Select a company"
    });

    $("#bindBusinessUnit").on('change', function () {
        fnGetDeclarationReports();
        $("#chart_Declarations").html('');
        ChartsFlotcharts.init();
    });
})

function initializeDataTable(id, columns) {
    $('#' + id).DataTable({
        dom: 'Bfrtip',
        pageLength: 10,
        //responsive: true,
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
                    columns: columns
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

function fnBindBusinessUnitList() {
    $("#Loader").show();
    var webUrl = uri + "/api/BusinessUnit/GetAllBusinessUnitList";
    $.ajax({
        type: "GET",
        url: webUrl,
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                var result = "";
                for (var i = 0; i < msg.BusinessUnitList.length; i++) {
                    result += '<option value="' + msg.BusinessUnitList[i].businessUnitId + '">' + msg.BusinessUnitList[i].businessUnitName + '</option>';
                }

                $("#bindBusinessUnit").append(result);
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

function fnGetDeclarationReports() {
    $("#Loader").show();
    var webUrl = uri + "/api/ReportsIT/GetDeclarationReports";
    $.ajax({
        type: "POST",
        url: webUrl,
        data: JSON.stringify({ businessUnit: { businessUnitId: $("#bindBusinessUnit").val() } }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                var result = "";
                if (msg.DeclarationReport !== null) {
                    declarationArray = [];
                    var totalUsers = msg.DeclarationReport.declarationMade + msg.DeclarationReport.declarationNotMade;
                    $("#tdTotalUsers").html(totalUsers);
                    $("#tdUserMadeDeclaration").html(msg.DeclarationReport.declarationMade);
                    $("#tdUserNotMadeDeclaration").html(msg.DeclarationReport.declarationNotMade);
                    declarationArray.push(((msg.DeclarationReport.declarationMade) / (msg.DeclarationReport.totalUsers)) * 100);
                    declarationArray.push(((msg.DeclarationReport.declarationNotMade) / (msg.DeclarationReport.totalUsers)) * 100);
                    for (var i = 0; i < msg.DeclarationReport.lstUser.length; i++) {
                        result += '<tr>';
                        result += '<td>' + msg.DeclarationReport.lstUser[i].USER_NM + '</td>';
                        result += '<td class="display-none">' + msg.DeclarationReport.lstUser[i].USER_EMAIL + '</td>';
                        result += '<td class="display-none">' + msg.DeclarationReport.lstUser[i].userRole.ROLE_NM + '</td>';
                        result += '<td>' + msg.DeclarationReport.lstUser[i].DepartmentName + '</td>';
                        result += '<td>' + msg.DeclarationReport.lstUser[i].DesignationName + '</td>';
                        result += '<td>' + msg.DeclarationReport.lstUser[i].panNumber + '</td>';
                        result += '<td>' + msg.DeclarationReport.lstUser[i].USER_MOBILE + '</td>';
                        result += '<td>' + msg.DeclarationReport.lstUser[i].LOGIN_ID + '</td>';
                        result += '<td class="display-none">' + msg.DeclarationReport.lstUser[i].declarationStartDate + '</td>';
                        result += '<td>' + msg.DeclarationReport.lstUser[i].declarationSubmissionDate + '</td>';
                        result += '<td>' + msg.DeclarationReport.lstUser[i].declarationDueDate + '</td>';
                        result += '</tr>';
                    }

                }
                var table = $('#tbl-DeclarationDetails-setup').DataTable();
                table.destroy();
                $("#tbdDeclarationDetailsList").html(result);
                initializeDataTable('tbl-DeclarationDetails-setup', [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10]);
                // TableDatatablesButtons.init();
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

function fnGetDeclarationReportsBetweenDates() {
    if (fnValidateDeclarationReportsBetweenDates()) {
        var fromDate = $("#txtFromDate").val();
        var toDate = $("#txtToDate").val();
        $("#Loader").show();
        var webUrl = uri + "/api/ReportsIT/GetDeclarationReportsBetweenDates";
        $.ajax({
            type: "POST",
            url: webUrl,
            data: JSON.stringify({ businessUnit: { businessUnitId: $("#bindBusinessUnit").val() }, declarationNotMadeFrom: fromDate, declarationNotMadeTo: toDate }),
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            async: false,
            success: function (msg) {
                if (isJson(msg)) {
                    msg = JSON.parse(msg);
                }
                if (msg.StatusFl == true) {
                    var result = "";
                    if (msg.DeclarationReport !== null) {
                        for (var i = 0; i < msg.DeclarationReport.lstUser.length; i++) {
                            result += '<tr>';
                            result += '<td>' + msg.DeclarationReport.lstUser[i].USER_NM + '</td>';
                            result += '<td class="display-none">' + msg.DeclarationReport.lstUser[i].USER_EMAIL + '</td>';
                            result += '<td class="display-none">' + msg.DeclarationReport.lstUser[i].userRole.ROLE_NM + '</td>';
                            result += '<td>' + msg.DeclarationReport.lstUser[i].DepartmentName + '</td>';
                            result += '<td>' + msg.DeclarationReport.lstUser[i].DesignationName + '</td>';
                            result += '<td>' + msg.DeclarationReport.lstUser[i].panNumber + '</td>';
                            result += '<td>' + msg.DeclarationReport.lstUser[i].USER_MOBILE + '</td>';
                            result += '<td>' + msg.DeclarationReport.lstUser[i].LOGIN_ID + '</td>';
                            result += '<td class="display-none">' + msg.DeclarationReport.lstUser[i].declarationStartDate + '</td>';
                            result += '<td>' + msg.DeclarationReport.lstUser[i].declarationSubmissionDate + '</td>';
                            result += '<td>' + msg.DeclarationReport.lstUser[i].declarationDueDate + '</td>';
                            result += '</tr>';
                        }

                    }
                    var table = $('#tbl-DeclarationDetails-setup').DataTable();
                    table.destroy();
                    $("#tbdDeclarationDetailsList").html(result);
                    initializeDataTable('tbl-DeclarationDetails-setup', [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10]);
                    // TableDatatablesButtons.init();
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
}

function fnValidateDeclarationReportsBetweenDates() {
    if ($("#txtFromDate").val() == undefined || $("#txtFromDate").val() == null || $("#txtFromDate").val().trim() == '') {
        return false;
    }
    if ($("#txtToDate").val() == undefined || $("#txtToDate").val() == null || $("#txtToDate").val().trim() == '') {
        return false;
    }
    else {
        
            var FromDate = new Date(convertToDateTime($("#txtFromDate").val()));
            var Todate = new Date(convertToDateTime($("#txtToDate").val()));

            if (Todate < FromDate) {
                
                alert("To Date Should be greater than From Date");
                return false;
            }
        
    }
    return true;
}

var ChartsFlotcharts = function () {
    var initPieCharts1 = function () {
        var data = [];
        // var series = Math.floor(Math.random() * 10) + 1;
        var series = ["Declarations Made", "Not Made"];
        //  var value = ["70", "30"];
        var value = declarationArray;
        series = series < 5 ? 5 : series;

        // for (var i = 0; i < series; i++) {
        for (var i = 0; i < series.length; i++) {
            data[i] = {
                label: series[i],
                //data: Math.floor(Math.random() * 100) + 1
                data: value[i]
            };
        }

        // INTERACTIVE

        if ($('#chart_Declarations').size() !== 0) {
            $.plot($("#chart_Declarations"), data, {
                series: {
                    pie: {
                        show: true
                    }
                },
                grid: {
                    hoverable: true,
                    clickable: true
                }
            });
            $("#chart_Declarations").bind("plothover", pieHover);
            $("#chart_Declarations").bind("plotclick", pieClick);
        }

        function pieHover(event, pos, obj) {
            if (!obj)
                return;
            percent = parseFloat(obj.series.percent).toFixed(2);
            // $("#hover").html('<span style="font-weight: bold; color: ' + obj.series.color + '">' + obj.series.label + ' (' + percent + '%)</span>');
            $("#chart_Declarations").attr('title', obj.series.label + ' (' + percent + '%)');
        }

        function pieClick(event, pos, obj) {
            if (!obj)
                return;
            percent = parseFloat(obj.series.percent).toFixed(2);
            alert('' + obj.series.label + ': ' + percent + '%');
        }

    }

    return {
        //main function to initiate the module

        init: function () {
            initPieCharts1();
            // initPieCharts2();
        }

    };
}();

function fnUserMadeDeclarationList() {
    $("#Loader").show();
    var webUrl = uri + "/api/ReportsIT/GetUserMadeDeclarationReports";
    $.ajax({
        type: "POST",
        url: webUrl,
        data: JSON.stringify({ businessUnit: { businessUnitId: $("#bindBusinessUnit").val() } }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                var result = "";
                if (msg.DeclarationReport !== null) {
                    for (var i = 0; i < msg.DeclarationReport.lstUser.length; i++) {
                        result += '<tr>';
                        result += '<td style="width:10%">' + (i + 1) + '</td>';
                        result += '<td style="width:20%">' + msg.DeclarationReport.lstUser[i].USER_NM + '</td>';
                        result += '<td style="width:20%">' + msg.DeclarationReport.lstUser[i].USER_EMAIL + '</td>';
                        result += '<td style="width:20%">' + msg.DeclarationReport.lstUser[i].LOGIN_ID + '</td>';
                        result += '<td style="width:20%">' + msg.DeclarationReport.lstUser[i].DepartmentName + '</td>';
                        result += '<td style="width:20%">' + msg.DeclarationReport.lstUser[i].DesignationName + '</td>';
                        result += '<td style="width:20%">' + msg.DeclarationReport.lstUser[i].panNumber + '</td>';
                        result += '<td style="width:20%">' + msg.DeclarationReport.lstUser[i].USER_MOBILE + '</td>';
                        result += '</tr>';
                    }

                }
                var table = $('#tbl-UsersMadeDeclaration-setup').DataTable();
                table.destroy();
                $("#tbodyUsersMadeDeclarationBody").html(result);
                initializeDataTable('tbl-UsersMadeDeclaration-setup', [0, 1, 2, 3, 4, 5, 6, 7]);
                // TableDatatablesButtons.init();
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

function fnUserNotMadeDeclarationList() {
    $("#Loader").show();
    var webUrl = uri + "/api/ReportsIT/GetUserNotMadeDeclarationReports";
    $.ajax({
        type: "POST",
        url: webUrl,
        data: JSON.stringify({ businessUnit: { businessUnitId: $("#bindBusinessUnit").val() } }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                var result = "";
                if (msg.DeclarationReport !== null) {
                    for (var i = 0; i < msg.DeclarationReport.lstUser.length; i++) {
                        result += '<tr>';
                        result += '<td style="width:10%">' + (i + 1) + '</td>';
                        result += '<td style="width:20%">' + msg.DeclarationReport.lstUser[i].USER_NM + '</td>';
                        result += '<td style="width:20%">' + msg.DeclarationReport.lstUser[i].USER_EMAIL + '</td>';
                        result += '<td style="width:20%">' + msg.DeclarationReport.lstUser[i].LOGIN_ID + '</td>';
                        result += '<td style="width:20%">' + msg.DeclarationReport.lstUser[i].DepartmentName + '</td>';
                        result += '<td style="width:20%">' + msg.DeclarationReport.lstUser[i].DesignationName + '</td>';
                        result += '<td style="width:20%">' + msg.DeclarationReport.lstUser[i].panNumber + '</td>';
                        result += '<td style="width:20%">' + msg.DeclarationReport.lstUser[i].USER_MOBILE + '</td>';
                        result += '</tr>';
                    }

                }
                var table = $('#tbl-UsersNotMadeDeclaration-setup').DataTable();
                table.destroy();
                $("#tbodyUsersNotMadeDeclarationBody").html(result);
                initializeDataTable('tbl-UsersNotMadeDeclaration-setup', [0, 1, 2, 3, 4, 5, 6, 7]);
                // TableDatatablesButtons.init();
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
function convertToDateTime(date) {
    return date.split("/")[1] + "/" + date.split("/")[0] + "/" + date.split("/")[2];
}