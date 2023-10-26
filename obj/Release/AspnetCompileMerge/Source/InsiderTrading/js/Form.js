$(document).ready(function () {
    window.history.forward();
    function preventBack() { window.history.forward(1); }
    $("#Loader").hide();


})

function fnGoToForm() {
    var formType = $("#selFormSelection").val();
    if (formType == "Form_A") {
        $("#divFormB").hide();
        $("#divFormC").hide();
        $("#btDownloadFormToPdf").show();
        $("#divFormA").show();
    }
    else if (formType == "Form_B") {
        $("#divFormA").hide();
        $("#divFormC").hide();
        $("#btDownloadFormToPdf").show();
        $("#divFormB").show();
    }
    else if (formType == "Form_C") {
        $("#divFormA").hide();
        $("#divFormB").hide();
        $("#btDownloadFormToPdf").show();
        $("#divFormC").show();
    }
    else {
        $("#btDownloadFormToPdf").hide();
        $("#divFormA").hide();
        $("#divFormB").hide();
        $("#divFormC").hide();
    }
}