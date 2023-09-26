var uploadedFile = null;
jQuery(document).ready(function () {
    $("#Loader").hide();
   
});

function fnSaveAdminManual() {
    if (fnValidateAdmin()) {
        fnAddUpdateAdmin();
    }
}

function fnSaveUserManual() {
    if (fnValidateUser()) {
        fnAddUpdateUser();
    }
}


function fnAddUpdateUser() {
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
    var document = $("#fileUser").get(0).files[0].name;
    var PolicyID = $('#txtPolicyKey').val();
    if (PolicyID === "") {
        PolicyID = 0;
    }

    //filesData.append("Object", JSON.stringify({ POLICY_ID: PolicyID, DOCUMENT: document }));
    filesData.append("Files", $("#fileUser").get(0).files[0]);
    var webUrl = uri + "/api/ManualAdminUser/UploadUserDoc";
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
                    $("#Loader").hide();
                    alert(msg.Msg);
                    $('#btnSave').removeAttr("data-dismiss");
                    $('#file').val("");
                    return false;
                }
            }
            else {
                $("#Loader").hide();
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
function fnAddUpdateAdmin() {
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
    var document = $("#fileAdmin").get(0).files[0].name;
    var PolicyID = $('#txtPolicyKey').val();
    if (PolicyID === "") {
        PolicyID = 0;
    }


    //filesData.append("Object", JSON.stringify({ POLICY_ID: PolicyID, DOCUMENT: document }));
    filesData.append("Files", $("#fileAdmin").get(0).files[0]);
    var webUrl = uri + "/api/ManualAdminUser/UploadAdminDoc";
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
                    $("#Loader").hide();
                    alert(msg.Msg);
                    $('#btnSave').removeAttr("data-dismiss");
                    $('#file').val("");
                    return false;
                }
            }
            else {
                $("#Loader").hide();
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

function fnValidateUser() {
    var itemFile = $("#fileUser").get(0).files;
    if (itemFile.length == 0) {
        alert("No file choosen to upload document")
    }
    var arrayExtensions = ["pdf"];
    if ($.inArray(itemFile[0].name.split('.').pop().toLowerCase(), arrayExtensions) == -1) {
        alert("Only pdf format is allowed in Manual Document.");
        return false;
    }
    return true;
}
function fnValidateAdmin() {
    var itemFile = $("#fileAdmin").get(0).files;
    if (itemFile.length == 0) {
        alert("No file choosen to upload document")
    }
    var arrayExtensions = ["pdf"];
    if ($.inArray(itemFile[0].name.split('.').pop().toLowerCase(), arrayExtensions) == -1) {
        alert("Only pdf format is allowed in Manual Document.");
        return false;
    }
    return true;
}

function ConvertToDateTime(dateTime) {
    var date = dateTime.split(" ")[0];

    return (date.split("/")[1] + "/" + date.split("/")[0] + "/" + date.split("/")[2]);
}




