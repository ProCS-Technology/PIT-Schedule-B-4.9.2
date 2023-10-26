var userRole = "";
var userCategory = "";
var lstOtherPreclearanceRequest = null;
var lstOtherPreclearanceRequest = null;
var arrDematAccount = null;
var arrForms = new Array();
var arrDetails = new Array();
var arrType = new Array();
$(document).ready(function () {
   // fnGetAllPendingRequest();
   // fnGetOpenPreClearanceRequest();
    fnGetOpenDisclosureRequest();
   // fnGetWOPreclearanceNonCompliance();
})
function fnGetAllPendingRequest() {
    $("#Loader").show();
    var webUrl = uri + "/api/PreClearanceRequest/GetAllPendingPreClearance";
    $.ajax({
        url: webUrl,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        data: {},
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                var result = "";
                if (msg.PreClearanceRequestList.length == 0) {
                    $("#dvActionablePreclearance").hide();
                }
                for (var i = 0; i < msg.PreClearanceRequestList.length; i++) {
                    result += '<tr>';
                    result += '<td>' + msg.PreClearanceRequestList[i].PreClearanceRequestedForName + '</td>';
                    result += '<td>' + msg.PreClearanceRequestList[i].relationName + '</td>';
                    result += '<td>' + msg.PreClearanceRequestList[i].SecurityTypeName + '</td>';
                    result += '<td>' + msg.PreClearanceRequestList[i].TransactionTypeName + '</td>';
                    result += '<td>' + msg.PreClearanceRequestList[i].TradeDate + '</td>';
                    result += '<td>';
                    result += '<a href="PreClearanceRequestApproval.aspx?id=' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '&RequestFor=' + msg.PreClearanceRequestList[i].RequestType + '" class="btn btn-success">Approve/Reject</a>';
                    result += '</td>';
                    result += '</tr>';
                }
                //var table = $('#tbl-preclearance-task').DataTable();
                //table.destroy();
                $("#tbodyPreclearanceApproval").html(result);
                $("#dvActionablePreclearance").show();
                //initializeDataTable();
            }
            else {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    //alert("In here");
                    $("#dvActionablePreclearance").hide();
                    //alert(msg.Msg);
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
function fnGetOpenPreClearanceRequest() {
    $("#Loader").show();
    var webUrl = uri + "/api/PreClearanceRequest/GetOpenPreClearanceRequestList";
    $.ajax({
        url: webUrl,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        data: JSON.stringify({ Status: "Approved" }),
        success: function (msg) {
            $("#Loader").hide();
            debugger;
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                var result = "";
                lstPreclearanceRequest = msg.PreClearanceRequestList;
                for (var i = 0; i < msg.PreClearanceRequestList.length; i++) {
                    userRole = msg.PreClearanceRequestList[i].userRole;
                    var ReqId = (msg.PreClearanceRequestList[i].PreClearanceRequestId + "").padStart(4, "0");
                    result += '<tr id="tr_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">';
                    //result += '<td>' + msg.PreClearanceRequestList[i].PreClearanceRequestedForName + '</td>';
                    result += '<td>' + msg.PreClearanceRequestList[i].relationName + '</td>';
                    result += '<td style="text-align:right;padding-right:10px;">' + msg.PreClearanceRequestList[i].TradeQuantity + '</td>';
                    result += '<td style="text-align:right;padding-right:10px;">' + msg.PreClearanceRequestList[i].ExecutedQty + '</td>';
                    result += '<td>' + msg.PreClearanceRequestList[i].TransactionTypeName + '</td>';
                    result += '<td>' + msg.PreClearanceRequestList[i].TradeCompanyName + '</td>';
                    result += '<td>' + msg.PreClearanceRequestList[i].TradeDate + '</td>';
                    //result += '<td>' + msg.PreClearanceRequestList[i].reviewedOn + '</td>';
                    result += '<td>' + msg.PreClearanceRequestList[i].tradingFrom + '</td>';
                    result += '<td>' + msg.PreClearanceRequestList[i].tradingTo + '</td>';

                    result += '<td>';
                    if (msg.PreClearanceRequestList[i].RequestType == 'Home') {
                        result += '<a title="Uplod Trade Disclosure" href="PreClearanceRequest.aspx?id=' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '&qty=' + msg.PreClearanceRequestList[i].TradeQuantity + '&demat=' + msg.PreClearanceRequestList[i].DematAccount + '&for=' + msg.PreClearanceRequestList[i].PreClearanceRequestedFor + '&cmpnId=' + msg.PreClearanceRequestList[i].TradeCompany + '&cmpnNm=' + msg.PreClearanceRequestList[i].TradeCompanyName + '&transactionTyp=' + msg.PreClearanceRequestList[i].TransactionType + '&reqTyp=Home&validFrm=' + msg.PreClearanceRequestList[i].tradingFrom + '&validTo=' + msg.PreClearanceRequestList[i].tradingTo + '&eQty=' + msg.PreClearanceRequestList[i].ExecutedQty + '&cQty=' + msg.PreClearanceRequestList[i].CancelledQty + '&eStatus=' + msg.PreClearanceRequestList[i].ExecutedStatus + '">';
                    }
                    else {
                        result += '<a title="Upload Trade Disclosure" href="PreClearanceRequest.aspx?id=' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '&qty=' + msg.PreClearanceRequestList[i].TradeQuantity + '&demat=' + msg.PreClearanceRequestList[i].DematAccount + '&for=' + msg.PreClearanceRequestList[i].PreClearanceRequestedFor + '&cmpnId=' + msg.PreClearanceRequestList[i].TradeCompany + '&cmpnNm=' + msg.PreClearanceRequestList[i].TradeCompanyName + '&transactionTyp=' + msg.PreClearanceRequestList[i].TransactionType + '&reqTyp=Other&validFrm=' + msg.PreClearanceRequestList[i].tradingFrom + '&validTo=' + msg.PreClearanceRequestList[i].tradingTo + '&eQty=' + msg.PreClearanceRequestList[i].ExecutedQty + '&cQty=' + msg.PreClearanceRequestList[i].CancelledQty + '&eStatus=' + msg.PreClearanceRequestList[i].ExecutedStatus + '">';
                    }
                    result += '<i class="fa fa-upload"></i></a>&nbsp;&nbsp;&nbsp;';
                    result += '</td>';

                    result += '</tr>';
                }
                //var table = $('#tbl-preclearance-setup').DataTable();
                //table.destroy();
                $("#tbodyBNApproval").html(result);
                $("#dvActionableBN").show();
                //initializeDataTable();
            }
            else {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else if (msg.Msg.toLowerCase() == "no data found !") {
                    $("#dvActionableBN").hide();
                }
                else {
                    $("#dvActionableBN").hide();
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
function fnGetOpenDisclosureRequest() {


    $("#Loader").show();
    var webUrl = uri + "/api/DashboardIT/GetOpenDisclosureRequest";
    $.ajax({
        url: webUrl,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        data: JSON.stringify({ Status: "Approved" }),
        success: function (msg) {
            $("#Loader").hide();
            //debugger;
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                var result = "";
                lstDisclosureRequest = msg.lstActioanble;
                for (var i = 0; i < lstDisclosureRequest.length; i++) {
                    result += '<tr id="tr_' + lstDisclosureRequest[i].TaskId + '">';
                    result += '<td>' + lstDisclosureRequest[i].UserNm + '</td>';
                    result += '<td>' + lstDisclosureRequest[i].TaskCreatedOn + '</td>';
                    result += '<td>' + lstDisclosureRequest[i].TaskType + '</td>';

                    result += '<td>';
                    result += '<a title="Approve Disclosure Request" onclick="javascript:fnRequestAction(\'' + lstDisclosureRequest[i].TaskId + '\',\'Approved\');">';
                    result += '<i class="fa fa-thumbs-o-up"></i></a>&nbsp;&nbsp;&nbsp;';
                    result += '<a title="Reject Disclosure Request" onclick="javascript:fnRequestAction(\'' + lstDisclosureRequest[i].TaskId + '\',\'Rejected\');">';
                    result += '<i class="fa fa-thumbs-o-down"></i></a>&nbsp;&nbsp;&nbsp;';
                    result += '</td>';

                    result += '</tr>';
                }
                //var table = $('#tbl-preclearance-setup').DataTable();
                //table.destroy();
                $("#tbodyDeclarationApproval").html(result);
                $("#dvDeclarationActionable").show();
                //initializeDataTable();
            }
            else {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else if (msg.Msg.toLowerCase() == "no data found!") {
                    $("#tbodyDeclarationApproval").html('');
                    $("#dvDeclarationActionable").hide();
                    $("#dvActionableBN").hide();
                }
                else {
                    $("#dvActionableBN").hide();
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
function fnGetWOPreclearanceNonCompliance() {

}
function fnRequestAction(TaskId, Status) {
    debugger;
    //alert("In function fnRequestAction");
    //alert("TaskId=" + TaskId);
    //alert("Status=" + Status);

    var token = $("#TokenKey").val();
    $("#Loader").show();
    var webUrl = uri + "/api/Transaction/UpdateModifyRequest";
    $.ajax({
        url: webUrl,
        headers: {
            'TokenKeyH': token,
        },
        type: "POST",
        data: JSON.stringify({ TaskId: TaskId, Status: Status }),
        async: false,
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            $("#Loader").hide();
            if (msg.StatusFl == true) {
                //alert('Request for editing the disclosure has been forworded to Compliance Office, once he/she approves the request you can edit your disclosure');
                window.location.reload();
                //$("#mdlRequest").modal('show');
                //$("#spnRequest").hide();
            }
            else {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                }
            }
        },
        error: function (response) {
            $("#Loader").hide();
            alert(response.status + ' ' + response.statusText);
        }
    })
}

function fnReportNC(Type, NCId, emailMsg) {
    //alert("In function fnReportNC");
    //alert("Type=" + Type);
    //alert("NCId=" + NCId);
    if (Type == 'No') {
        if (confirm("Are you sure you want to exempt this Non compliance")) {
            fnMarkCompliant(NCId);
        }
    }
    else {
        $("#txtNCId").val(NCId);
        fnGetNonCompliantDetails(NCId, emailMsg);
    }
}
function fnMarkCompliant(NCId) {
    //alert("In function fnMarkCompliant");
    //alert("NCId=" + NCId);
    $("#Loader").show();
    var webUrl = uri + "/api/DashboardIT/MarkWOPreClearanceNonCompliance";
    $.ajax({
        url: webUrl,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ NCId: NCId }),
        datatype: "json",
        //async: false,
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                window.location.reload();
            }
            else {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    //alert("In here");
                    $("#dvActionablePreclearance").hide();
                    //alert(msg.Msg);
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
function fnGetNonCompliantDetails(NCId) {
    var emailMsg = $("#txtNCMsg_" + NCId).val();
    //alert(emailMsg);
    $('#summernote_1').summernote('code', emailMsg);
    $('#ncWOPreClearanceMail').modal('show');


    //$("#Loader").show();
    //var webUrl = uri + "/api/DashboardIT/MarkWOPreClearanceNonCompliance";
    //$.ajax({
    //    url: webUrl,
    //    type: "GET",
    //    contentType: "application/json; charset=utf-8",
    //    data: JSON.stringify({ NCId: NCId }),
    //    datatype: "json",
    //    async: false,
    //    data: {},
    //    success: function (msg) {
    //        $("#Loader").hide();
    //        if (isJson(msg)) {
    //            msg = JSON.parse(msg);
    //        }
    //        if (msg.StatusFl == true) {
    //            var result = "";
    //            if (msg.PreClearanceRequestList.length == 0) {
    //                $("#dvActionablePreclearance").hide();
    //            }
    //        }
    //        else {
    //            if (msg.Msg == "SessionExpired") {
    //                alert("Your session is expired. Please login again to continue");
    //                window.location.href = "../LogOut.aspx";
    //            }
    //            else {
    //                //alert("In here");
    //                $("#dvActionablePreclearance").hide();
    //                //alert(msg.Msg);
    //                return false;
    //            }
    //        }

    //    },
    //    error: function (response) {
    //        $("#Loader").hide();
    //        alert(response.status + ' ' + response.statusText);
    //    }
    //})
}
function fnSubmitMail() {
    var ncId = $("#txtNCId").val();
    var sMailTo = $("#txtEmailTo_" + ncId).val();
    var emailMsg = $('#summernote_1').summernote('code');

    //alert(emailMsg);

    var test = new FormData();
    test.append("sMailTo", sMailTo);
    test.append("emailMsg", emailMsg);
    var token = $("#TokenKey").val();

    $("#Loader").show();
    var webUrl = uri + "/api/DashboardIT/SubmitNonComplianceEMail";
    $.ajax({
        url: webUrl,
        type: "POST",
        headers: {
            'TokenKeyH': token,
        },
        data: JSON.stringify({
            sMailTo: sMailTo, EmailTemplate: emailMsg, NCId: ncId
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: true,
        success: function (msg) {
            $("#Loader").hide();
            if (msg.StatusFl) {
                if (msg.Msg == "Success") {
                    alert("Message sent successfully !");
                    window.location.reload();
                }
                else {
                    alert(msg.Msg);
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
            alert(response.status + ' ' + response.statusText);
        }
    })
}