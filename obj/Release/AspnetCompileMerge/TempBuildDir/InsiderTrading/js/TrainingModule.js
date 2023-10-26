$(document).ready(function () {
    $("#Loader").hide();
    fnGetTrainingModuleDtl();
})
function fnGetTrainingModuleDtl() {
    $("#Loader").show();
    $('.button-previous').hide();
    $('.button-next').hide();
    $('.btn-final').hide();

    var webUrl = uri + "/api/Training/GetTrainingFileOnLoadToPdfViewer";
    $.ajax({
        type: "POST",
        url: webUrl,
        data: JSON.stringify({ trainingId: $("#ContentPlaceHolder1_txtTrainingModuleId").val() }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            if (msg.StatusFl == true) {
                if (msg.TrainingModuleDetail !== null) {
                    $("#ContentPlaceHolder1_txtTrainingModuleDeatilId").val(msg.TrainingModuleDetail.Id);
                    $("#ContentPlaceHolder1_txtCurrentPage").val(msg.TrainingModuleDetail.sequence);

                    fnUiActionsToPerformOnPdfPage();

                    var path = "/";
                    for (var i = 1; i <= (window.location.pathname.split("/")).length - 2; i++) {
                        path += window.location.pathname.split("/")[i] + "/";
                    }

                    var uri = new URL(window.location.origin + path);
                    var arrayExtensions = ["pdf"];

                    if ($.inArray(msg.TrainingModuleDetail.trainingDocument.split('.').pop().toLowerCase(), arrayExtensions) !== -1) {
                        alert(uri + "emailAttachment/" + msg.TrainingModuleDetail.trainingDocument);
                        var pdfviewer = $("#viewer").data("ejPdfViewer");
                        pdfviewer.load(uri + "emailAttachment/" + msg.TrainingModuleDetail.trainingDocument);

                        var tempViewer = $("#viewer").data('ejPdfViewer');
                        tempViewer.updateViewerSize();
                    }
                    else {
                        var strTable = "";
                        strTable += '<video id="videoTemplate" style="width: 100%;" height="700" controls autoplay>';
                        strTable += '<source id="sourceVideo" src="' + uri + 'emailAttachment/' + msg.TrainingModuleDetail.trainingDocument + '" type="video/mp4">';
                        strTable += 'Your browser does not support the video tag.';
                        strTable += '</video>';
                        $("#divTemplateAudioVideo").html(strTable);
                    }
                }

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
function fnGoToNextPage() {
    $("#Loader").show();
    $('.button-previous').hide();
    $('.button-next').hide();
    $('.btn-final').hide();
    var webUrl = uri + "/api/Training/GetTrainingFileOnNextButtonToPdfViewer";
    $.ajax({
        type: "POST",
        url: webUrl,
        data: JSON.stringify({ trainingId: $("#ContentPlaceHolder1_txtTrainingModuleId").val(), trainingModuleUserStatus: { currentPage: $("#ContentPlaceHolder1_txtCurrentPage").val() } }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            if (msg.StatusFl == true) {
                if (msg.TrainingModuleDetail !== null) {
                    $("#ContentPlaceHolder1_txtTrainingModuleDeatilId").val(msg.TrainingModuleDetail.Id);
                    $("#ContentPlaceHolder1_txtCurrentPage").val(msg.TrainingModuleDetail.sequence);


                    fnUiActionsToPerformOnPdfPage();

                    var path = "/";
                    for (var i = 1; i <= (window.location.pathname.split("/")).length - 2; i++) {
                        path += window.location.pathname.split("/")[i] + "/";
                    }

                    var uri = new URL(window.location.origin + path);

                    var arrayExtensions = ["pdf"];

                    if ($.inArray(msg.TrainingModuleDetail.trainingDocument.split('.').pop().toLowerCase(), arrayExtensions) !== -1) {

                        var pdfviewer = $("#viewer").data("ejPdfViewer");
                        pdfviewer.load(uri + "emailAttachment/" + msg.TrainingModuleDetail.trainingDocument);

                        var tempViewer = $("#viewer").data('ejPdfViewer');
                        tempViewer.updateViewerSize();
                    }
                    else {
                        var strTable = "";
                        strTable += '<video id="videoTemplate" style="width: 100%;" height="700" controls autoplay>';
                        strTable += '<source id="sourceVideo" src="' + uri + 'emailAttachment/' + msg.TrainingModuleDetail.trainingDocument + '" type="video/mp4">';
                        strTable += 'Your browser does not support the video tag.';
                        strTable += '</video>';
                        $("#divTemplateAudioVideo").html(strTable);
                    }
                }

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
function fnGoToPreviousPage() {
    $("#Loader").show();
    $('.button-previous').hide();
    $('.button-next').hide();
    $('.btn-final').hide();
    var webUrl = uri + "/api/Training/GetTrainingFileOnPreviousButtonToPdfViewer";
    $.ajax({
        type: "POST",
        url: webUrl,
        data: JSON.stringify({ trainingId: $("#ContentPlaceHolder1_txtTrainingModuleId").val(), trainingModuleUserStatus: { currentPage: $("#ContentPlaceHolder1_txtCurrentPage").val() } }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            if (msg.StatusFl == true) {
                if (msg.TrainingModuleDetail !== null) {
                    $("#ContentPlaceHolder1_txtTrainingModuleDeatilId").val(msg.TrainingModuleDetail.Id);
                    $("#ContentPlaceHolder1_txtCurrentPage").val(msg.TrainingModuleDetail.sequence);

                    fnUiActionsToPerformOnPdfPage();

                    var path = "/";
                    for (var i = 1; i <= (window.location.pathname.split("/")).length - 2; i++) {
                        path += window.location.pathname.split("/")[i] + "/";
                    }

                    var uri = new URL(window.location.origin + path);

                    var arrayExtensions = ["pdf"];

                    if ($.inArray(msg.TrainingModuleDetail.trainingDocument.split('.').pop().toLowerCase(), arrayExtensions) !== -1) {

                        var pdfviewer = $("#viewer").data("ejPdfViewer");
                        pdfviewer.load(uri + "emailAttachment/" + msg.TrainingModuleDetail.trainingDocument);

                        var tempViewer = $("#viewer").data('ejPdfViewer');
                        tempViewer.updateViewerSize();
                    }
                    else {
                        var strTable = "";
                        strTable += '<video id="videoTemplate" style="width: 100%;" height="700" controls autoplay>';
                        strTable += '<source id="sourceVideo" src="' + uri + 'emailAttachment/' + msg.TrainingModuleDetail.trainingDocument + '" type="video/mp4">';
                        strTable += 'Your browser does not support the video tag.';
                        strTable += '</video>';
                        $("#divTemplateAudioVideo").html(strTable);
                    }
                }

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
function fnSubmitActionTaken(status, id) {
    $("#Loader").show();
    var webUrl = uri + "/api/Training/OnSubmissionOfTrainingFile";
    $.ajax({
        type: "POST",
        url: webUrl,
        data: JSON.stringify({
            trainingId: $("#ContentPlaceHolder1_txtTrainingModuleId").val(),
            trainingModuleUserStatus: { remarks: $("#" + id).val() }
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            if (msg.StatusFl == true) {
                alert('The following task has been completed!');
                window.location.href = "Dashboard.aspx";
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
function fnUiActionsToPerformOnPdfPage() {
    if ($("#ContentPlaceHolder1_txtCurrentPage").val() == 1) {
        $('.button-previous').hide();
        if ($("#ContentPlaceHolder1_txtCurrentPage").val() == $("#ContentPlaceHolder1_txtTotalNoOfPages").val()) {
            $('.button-next').hide();
            $('.btn-final').show();
        }
        else {
            if ($('#ContentPlaceHolder1_txtUserTrainingModuleStatus').val() == 'Pending') {
                setTimeout(function () {
                    $('.button-next').show();
                }, 34000);
            }
            else {
                $('.button-next').show();
            }

            $('.btn-final').hide();
        }
    }
    else {
        setTimeout(function () {
            $('.button-previous').show();
        }, 34000);
        if ($("#ContentPlaceHolder1_txtCurrentPage").val() == $("#ContentPlaceHolder1_txtTotalNoOfPages").val()) {
            $('.button-next').hide();
            $('.btn-final').show();
        }
        else {
            if ($('#ContentPlaceHolder1_txtUserTrainingModuleStatus').val() == 'Pending') {
                setTimeout(function () {
                    $('.button-next').show();
                }, 34000);
            }
            else {
                $('.button-next').show();
            }
            $('.btn-final').hide();
        }
    }
}