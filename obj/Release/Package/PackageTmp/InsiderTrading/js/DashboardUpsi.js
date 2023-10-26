$('input[id*=txtFromDate]').datepicker({
    todayHighlight: true,
    autoclose: true,
    format: $("input[id*=hdnJSDateFormat]").val(),
    clearBtn: true
});
$('input[id*=txtToDate]').datepicker({
    todayHighlight: true,
    autoclose: true,
    format: $("input[id*=hdnJSDateFormat]").val(),
    clearBtn: true
});

$(document).ready(function () {

    window.history.forward();
    function preventBack() { window.history.forward(1); }

    fnGetUpsiCountOnload();
    fnGetInsiderTradingWindowClosureInfo();
    $("#Loader").hide();

})

function fnGetInsiderTradingWindowClosureInfo() {
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/GetInsiderTradingWindowClosureInfo";
    $.ajax({
        url: webUrl,
        type: "GET",
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            if (msg.StatusFl == true) {
                $("#Loader").hide();
                var result = "";
                for (var i = 0; i < msg.InsiderTradingWindowList.length; i++) {
                    if (msg.InsiderTradingWindowList[i].toDate == "31/12/9999") {
                        result += "Please Note: The Trading Window is closed from " + msg.InsiderTradingWindowList[i].fromDate + " till further notified - " + msg.InsiderTradingWindowList[i].remarks;
                    }
                    else {
                        result += "Please Note: The Trading Window is closed from " + msg.InsiderTradingWindowList[i].fromDate + " to " + msg.InsiderTradingWindowList[i].toDate + ". - " + msg.InsiderTradingWindowList[i].remarks;
                    }
                    result += "<span>&#9728;</span>";
                }
                result += " You are requested not to enter into a contra trade transaction, i.e. If you have purchased shares, do not sell them within 6 months from the date of purchase  and vice versa.<span>&#9728;</span>";
                $("#divTradingWindowClosureNotification").html(result);
            }
            else {
                $("#Loader").hide();
                var result = "";
                result += " You are requested not to enter into a contra trade transaction, i.e. If you have purchased shares, do not sell them within 6 months from the date of purchase  and vice versa.<span>&#9728;</span>";
                $("#divTradingWindowClosureNotification").html(result);
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    //  alert(msg.Msg);
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

//======Dashboard upsi search===============
function fnGetUpsiCountOnload() {
    if (true) {
        $("#Loader").show();
        var webUrl = uri + "/api/DashboardUpsi/GetUpsiCount";
        $.ajax({
            type: "POST",
            url: webUrl,
            data: JSON.stringify({ UpsiFrom: ($("#txtFromDate").val()), UpsiTo: ($("#txtToDate").val()) }),
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            success: function (msg) {
                var result = "";

                if (msg.StatusFl) {
                    $("#ActiveUpsi").html(msg.DashboardUpsi.TotalActiveUPSIEvent);
                    $("#InactiveUpsi").html(msg.DashboardUpsi.TotalInactiveUPSIEvent);
                    $("#AbandonedUpsi").html(msg.DashboardUpsi.TotalAbandonedUPSIEvent);
                    $("#PublishedUpsi").html(msg.DashboardUpsi.TotalPublishedUPSIEvent);
                    $("#AllUpsi").html(msg.DashboardUpsi.TotalUPSIEvent);

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
function fnGetUpsiCount() {
    if (fnValidate()) {
        $("#Loader").show();
        var webUrl = uri + "/api/DashboardUpsi/GetUpsiCount";
        $.ajax({
            type: "POST",
            url: webUrl,
            data: JSON.stringify({ UpsiFrom: ($("#txtFromDate").val()), UpsiTo: ($("#txtToDate").val()) }),
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            success: function (msg) {
                var result = "";
                if (msg.StatusFl) {
                    $("#ActiveUpsi").html(msg.DashboardUpsi.TotalActiveUPSIEvent);
                    $("#InactiveUpsi").html(msg.DashboardUpsi.TotalInactiveUPSIEvent);
                    $("#AbandonedUpsi").html(msg.DashboardUpsi.TotalAbandonedUPSIEvent);
                    $("#PublishedUpsi").html(msg.DashboardUpsi.TotalPublishedUPSIEvent);
                    $("#AllUpsi").html(msg.DashboardUpsi.TotalUPSIEvent);
                    $('#txtFromDate').val("");
                    $('#txtToDate').val("");
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
//============================
function fnValidate() {
    //if ($("#bindTrainings").val() == '' || $("#bindTrainings").val() == undefined || $("#bindTrainings").val() == null) {
    //    alert("Please select a training");
    //    return false;
    //}
    if ($("#txtFromDate").val() == '' || $("#txtFromDate").val() == undefined || $("#txtFromDate").val() == null) {
        alert("Please select from date");
        return false;
    }
    else if ($("#txtToDate").val() == '' || $("#txtToDate").val() == undefined || $("#txtToDate").val() == null) {
        alert("Please select to date");
        return false;
    }
    else {
        var FromDate = (($("#txtFromDate").val()));
        var Todate = (($("#txtToDate").val()));

        if (Todate < FromDate) {

            alert("To Date Should be greater than From Date");
            return false;
        }
    }

    return true;
}

function convertToDateTime(date) {
    return date.split("/")[2] + "-" + date.split("/")[1] + "-" + date.split("/")[0];
}