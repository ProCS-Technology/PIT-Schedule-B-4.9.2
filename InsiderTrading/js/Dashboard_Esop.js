$(document).ready(function () {
    window.history.forward();
    function preventBack() { window.history.forward(1); }
    $("#Loader").hide();
    fnGetAllEsopByUserId();
});
function isJson(str) {
    try {
        JSON.parse(str);
    } catch (e) {
        return false;
    }
    return true;
}
function fnGetAllEsopByUserId() {
    $("#Loader").show();
    var webUrl = uri + "/api/Benpos/GetEsopListByUser";
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
                    $("#Esop_PortletBox").addClass('display-none');
                    return false;
                }
            }
            else {
                var str = '';
                for (index = 0; index < msg.BenposHeaderList.length; index++) {
                    var RowCount = index + 1;
                    str += '<tr>';
                    str += '<td>' + RowCount + '</td>';
                    str += '<td>' + msg.BenposHeaderList[index].FolioNo + '</td>';
                    str += '<td>' + msg.BenposHeaderList[index].PanNo + '</td>';
                    str += '<td>' + msg.BenposHeaderList[index].Qty + '</td>';
                    str += '<td>' + msg.BenposHeaderList[index].Rate + '</td>';
                    str += '<td>' + msg.BenposHeaderList[index].ESOPAmount + '</td>';
                    str += '<td>' + msg.BenposHeaderList[index].asOfDate + '</td>';
                    str += '<td><a id="a_' + msg.BenposHeaderList[index].id + '" class="btn btn-sm btn-outline dark" data-toggle="modal" data-target="#modalEsopForms" href="#" onclick="javascript:fnGeneratEsopForms(\'' + msg.BenposHeaderList[index].id + '\');">Submit Form</a></td>';
                    str += '</tr>';
                }
                if (msg.BenposHeaderList.length > 0) {
                    var MyCount = parseInt($('#spnMyActionableCount').html()) || 0;
                    var EsopCount = MyCount + parseInt(msg.BenposHeaderList.length);
                    $('#spnMyActionableCount').html('(' + EsopCount + ')');
                    $('#dvMyActionable').show();
                    $("#Esop_PortletBox").removeClass('display-none');
                }
                $("#tbodyEsop").html(str);

            }
        },
        error: function (error) {
            $("#Loader").hide();
            alert(error.status + ' ' + error.statusText);
        }
    })
}
function fnSubmitEsopFile() {
    if (fnValidateEsopHdr()) {
        fnAddUpdateEsopHdr();
    }
}
function ConvertToDateTime(dateTime) {
    var date = dateTime.split(" ")[0];
    return (date.split("/")[1] + "/" + date.split("/")[0] + "/" + date.split("/")[2]);
}
function fnGeneratEsopForms(EsopAllocId) {
    $("input[id*='txtEsopAllocId']").val(EsopAllocId);
    $("#Loader").show();
    $.ajax({
        url: uri + "/api/DashboardIT/SubmitEsopFormC",
        type: "POST",
        data: JSON.stringify({
            Id: EsopAllocId
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: true,
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl) {

                $("select[id*='ddlEsopForms']").empty();
                if (msg.PreClearanceRequest.lstFormUrl != null) {
                    var strOption = "";
                    for (var x = 0; x < msg.PreClearanceRequest.lstFormUrl.length; x++) {
                        var strValue = msg.PreClearanceRequest.lstFormUrl[x];
                        strOption += strValue.split("~")[1] + "&";
                    }
                    var s = strOption.substr(0, strOption.length - 1);
                    $("select[id*='ddlEsopForms']").append(new Option(s, "All"));
                    //fnDisplayNote(null, null, "All");
                }
                $('#modalEsopForms').modal('show');

            }
            else {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    alert(msg.Msg);
                }
            }
        },
        error: function (error) {
            $("#Loader").hide();
            if (error.responseText == "Session Expired") {
                alert("Your session is expired. Please login again to continue");
                window.location.href = "../LogOut.aspx";
                return false;
            }
            else {
                alert(error.status + ' ' + error.statusText);
            }
        }
    });
}
function fnDownloadEsopForm() {
    if ($("select[id*='ddlEsopForms']").val() == "0") {
        return false;
    }

    $("#Loader").show();
    var brokerNoteId = $("input[id*='txtEsopAllocId']").val();
    var webUrl = uri + "/api/PreClearanceRequest/GetEsopForms";
    $.ajax({
        url: webUrl,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        contentType: false,
        data: JSON.stringify({ brokerNoteId: brokerNoteId, formType: $("select[id*='ddlEsopForms']").val() }),
        success: function (msg) {
            $("#Loader").hide();
            if (msg.StatusFl) {
                $("#btnSubmitEsopForms").prop("disabled", false);
                if (msg.PreClearanceReques !== null && msg.PreClearanceRequest.lstFormUrl !== null) {
                    for (var x = 0; x < msg.PreClearanceRequest.lstFormUrl.length; x++) {
                        //alert("msg.PreClearanceRequest.lstFormUrl[" + x + "]=" + msg.PreClearanceRequest.lstFormUrl[x]);
                        window.open(".." + msg.PreClearanceRequest.lstFormUrl[x]);
                    }
                    //if (msg.PreClearanceRequest.lstFormUrl.length) {
                    //    $.each(msg.PreClearanceRequest.lstFormUrl, function (index, element) {
                    //        downloadURL1(element);
                    //    })
                    //}
                }
                //downloadURL1(msg.PreClearanceRequest.formUrl);
            }
            else {
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
    });
}
function fnSubmitSystemGeneratedEsopForm() {
    var filesData = new FormData();
    filesData.append("Object", JSON.stringify({ brokerNoteId: $("input[id*='txtEsopAllocId']").val(), formType: $("select[id*='ddlEsopForms']").val() }));
    var webUrl = uri + "/api/PreClearanceRequest/SubmitSystemGeneratedEsopForm";
    $.ajax({
        type: 'POST',
        url: webUrl,
        data: filesData,
        contentType: false,
        processData: false,
        success: function (msg) {
            $("#Loader").hide();
            if (msg.StatusFl) {
                alert(msg.Msg);
                window.location.reload(true);
            }
            else {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    //  alert(msg.Msg);
                    $('#btnSubmitEsopForms').removeAttr("data-dismiss");
                }
                return false;
            }
        },
        error: function (error) {
            $("#Loader").hide();
            $('#btnSubmitEsopForms').removeAttr("data-dismiss");
            alert(error.status + ' ' + error.statusText);
        }
    })
}