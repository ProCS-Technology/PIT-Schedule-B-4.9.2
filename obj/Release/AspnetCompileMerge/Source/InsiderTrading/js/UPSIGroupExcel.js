function fnGrpExcel(GrpId,GrpNm) {
    //alert("In function fnGrpExcel");
    //alert("GrpId=" + GrpId);
    //alert("GrpNm=" + GrpNm);
    $("#txtXlsGrpId").val(GrpId);
}
function fnDownloadExcel() {
    //alert("In function fnDownloadExcel");
    //alert("GrpId=" + $("#txtXlsGrpId").val());
    window.open('UPSI/SDDTemplate.xlsx');
}
function fnUploadExcel() {
    //alert("In function fnUploadExcel");
    //alert("GrpId=" + $("#txtXlsGrpId").val());
    if ($('#fuExcelUploadFile').val() == "" || $('#fuExcelUploadFile').val() == null) {
        alert("Please select file to upload");
        return false;
    }
    var ctrl = $('#fuExcelUploadFile');
    var file = $('#fuExcelUploadFile').val();

    var ext = file.split(".");
    ext = ext[ext.length - 1].toLowerCase();
    var arExtns = ['xls', 'xlsx'];
    if (arExtns.lastIndexOf(ext) == -1) {
        alert("Please select a file with  extension(s).\n" + arExtns.join(', '));
        ctrl.value = '';
        return false;
    }
    else {
        fnUploadUPSI();
    }
}
function fnUploadUPSI() {
    //alert("In function fnUploadUPSI");
    var GrpId = $("#txtXlsGrpId").val();
    //alert("GrpId=" + GrpId);
    var param1 = new Date();
    var param2 = param1.getDate() + '_' + (param1.getMonth() + 1) + '_' + param1.getFullYear() + '_' + param1.getHours() + '_' + param1.getMinutes() + '_' + param1.getSeconds();
    var fileUpload = $("#fuExcelUploadFile").get(0);
    var files = fileUpload.files;
    var test = new FormData();
    for (var i = 0; i < files.length; i++) {
        test.append(files[i].name, files[i]);
    }
    var extn = $('#fuExcelUploadFile').val().split(".");
    extn = extn[extn.length - 1].toLowerCase();
    var sSaveAs = 'Upload_' + param2 + '_File.' + extn;
    test.append('sSaveAs', sSaveAs);
    test.append('GrpId', GrpId);

    var webUrl = uri + "/api/UPSIGroup/UploadCommunication";
    $("#Loader").show();
    $.ajax({
        type: 'POST',
        url: webUrl,
        data: test,
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        contentType: false,
        processData: false,
        success: function (msg) {
            //$.unblockUI();
            $("#Loader").hide();
            if (msg.StatusFl == false) {
                if (msg.Msg == "Success") {
                    $('#fuExcelUploadFile').val("");
                    return false;
                }
                else {
                    alert(msg.Msg);
                    $('#fuFoodCostUploadFile').val("");
                    $('#btnSaveUpload').removeAttr("data-dismiss");
                    return false;
                }
            }
            else {
                alert("Securities uploaded successfully !");
                setOtherSecurityHolding(msg);
                $('#btnUpload').removeAttr("data-target");
                $('#btnSaveUpload').attr("data-dismiss", "modal");
                return true;
            }
        },
        error: function (response) {
            //$.unblockUI();
            $("#Loader").hide();
            alert(response.status + ' ' + response.statusText);
            $('#fuExcelUploadFile').val("");
        }
    });
}