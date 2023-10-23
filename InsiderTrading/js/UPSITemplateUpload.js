$(document).ready(function () {
    window.history.forward();
    function preventBack() { window.history.forward(1); }
    $("#Loader").hide();
})
function fnUploadUPSITemplate() {
    var isValid = true;
    var txtExcelDoc = $("input[id*='txtExcelDoc']").get(0);
    var excelFile = txtExcelDoc.files;
    var documentSize = $("input[id*='txtExcelDoc']").get(0).files[0].size;
    if (excelFile.length == 0) {
        $("#lblExcel").addClass('required-red');
        isValid = false;
    }
    else {
        var itemFile = $("#txtExcelDoc").get(0).files;
        var arrayExtensions = ["xlsx", "xls"];

        if ($.inArray(itemFile[0].name.split('.').pop().toLowerCase(), arrayExtensions) == -1) {
            isValid = false;
            alert("Only xlsx or xls format is allowed.");
            $("#lblExcel").addClass('required-red');
        }
        else {
            $("#lblExcel").removeClass('required-red');
        }
    }

    var txtZipDoc = $("input[id*='txtZipDoc']").get(0);
    var zipFile = txtZipDoc.files;
    if (excelFile.length == 0) {}
    else {
        var itemFile = $("#txtZipDoc").get(0).files;
        var arrayZipExtension = ["zip"];
        if (itemFile.length > 0) {
            if ($.inArray(itemFile[0].name.split('.').pop().toLowerCase(), arrayZipExtension) == -1) {
                isValid = false;
                alert("Only zip format is allowed.");
                $("#lblZip").addClass('required-red');
            }
            else {
                $("#lblZip").removeClass('required-red');
            }
        }
    }
    if (!isValid) {
        return false;
    }

    $("#Loader").show();
    var filesData = new FormData();
    filesData.append("Excel", $("input[id*='txtExcelDoc']").get(0).files[0]);
    filesData.append("Zip", $("input[id*='txtZipDoc']").get(0).files[0]);
    filesData.append("FileSize", documentSize);

    var webUrl = uri + "/api/UPSI/SaveUPSITemplate";
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
                    alert(msg.Msg);
                    $('#btnSaveItems').removeAttr("data-dismiss");
                }
                return false;
            }
        },
        error: function (error) {
            $("#Loader").hide();
            $('#btnSaveItems').removeAttr("data-dismiss");
            alert(error.status + ' ' + error.statusText);
        }
    })
}
function fnRemoveClass(obj, val) {
    $("#lbl" + val + "").removeClass('required-red');
}
function fnDownloadMOMTemplate() {
    window.location.href = "Declaration_Document/UPSI.xlsx";
}