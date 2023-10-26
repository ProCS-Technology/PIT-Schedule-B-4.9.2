$(document).ready(function () {
    window.history.forward();
    function preventBack() { window.history.forward(1); }
    $("#Loader").hide();
    $('.datepicker').datepicker({
        todayHighlight: true,
        autoclose: true,
        clearBtn: true,
        endDate: "today"
    });
})

function fnGetMISReport() {
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/GetMISReport";
    $.ajax({
        url: webUrl,
        type: "GET",
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            if (msg.StatusFl == true) {
                $("#Loader").hide();
                if (msg.User !== null && msg.User.misReportPath !== null) {
                    var downloadablePath = uri + "/InsiderTrading/emailAttachment/" + msg.User.misReportPath;
                    window.location.href = downloadablePath;
                }
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
    })
}

function fnValidate() {

    var FromDate = $("input[id*='txtFromDate']").val();
    var Todate = $("input[id*='txtToDate']").val();

    
    if (FromDate == '' || FromDate == undefined || FromDate == null) {
        alert("Please select from date");
        return false;
    }

    if (Todate == '' || Todate == undefined || Todate == null) {
        alert("Please select to date");
        return false;
    }

    else if (new Date(FormatDate(Todate)) < new Date(FormatDate(FromDate))) {
        alert("To Date Should be greater than From Date");
        return false;
    }
    else {
        return true;
    }
}

function FormatDate(dateString) {
    return dateString.split("/")[1] + "/" + dateString.split("/")[0] + "/" + dateString.split("/")[2];
}