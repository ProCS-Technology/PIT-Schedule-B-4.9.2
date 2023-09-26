var uploadedFile = null;
jQuery(document).ready(function () {
    $("#Loader").hide();
   
});

function fnSavePolicy() {
    if (fnValidate()) {
        fnAddUpdatePolicy();
    }
}


function fnAddUpdatePolicy() {
    debugger;
    $("#Loader").show();
    var filesData = new FormData();

    //if ($('#file').get(0).files[0] === undefined) {
    //    var document = uploadedFile;
    //}
    //else {
    //    var tempFile = $('#file').get(0).files[0].name;
    //    var document = tempFile.split('.')[0] + "_" + document + "." + tempFile.split('.')[1];
    //}
    var document = $("#file").get(0).files[0].name;
    var PolicyID = $('#txtPolicyKey').val();
    if (PolicyID === "") {
        PolicyID = 0;
    }

    //filesData.append("Object", JSON.stringify({ POLICY_ID: PolicyID, DOCUMENT: document }));
    filesData.append("Files", $("#file").get(0).files[0]);
    var webUrl = uri + "/api/UserBulkUpload/SaveUserTemplate";
    $.ajax({
        type: 'POST',
        url: webUrl,
        data: filesData,
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        contentType: false,
        async: false,
        processData: false,
        success: function (msg) {
            if (msg.StatusFl == false) {
                $("#Loader").hide();
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../Login.aspx";
                }
                else {
                    alert(msg.Msg);
                    $('#btnSave').removeAttr("data-dismiss");
                    $('#file').val("");
                    return false;
                }
            }
            else {
                //  UploadFiles(document);
                alert(msg.Msg);
                
            }
        },
        error: function (error) {
            $("#Loader").hide();
            alert(error.status + ' ' + error.statusText);
        }
    })
}

function fnValidate() {
    var itemFile = $("#file").get(0).files;
    if (itemFile.length == 0) {
        alert("No file choosen to upload document")
    }
    var arrayExtensions = ["xls"];
    var array1Extensions1 = ["xlsx"];
    if ($.inArray(itemFile[0].name.split('.').pop().toLowerCase(), arrayExtensions) == -1 && $.inArray(itemFile[0].name.split('.').pop().toLowerCase(), array1Extensions1) == -1) {
        alert("Only xls or xlsx format is allowed in User Template.");
        return false;
    }
    return true;
}
