var uploadedFile = null;
jQuery(document).ready(function () {
    $("#Loader").hide();
    fnGetPolicy();
});

function fnSavePolicy() {
    if (fnValidate()) {
        fnAddUpdatePolicy();
    }
}

function fnGetPolicy() {
    $("#Loader").show();
    var webUrl = uri + "/api/Policy/GetPolicy";
    $.ajax({
        type: 'GET',
        url: webUrl,
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            $("#Loader").hide();
            if (msg.StatusFl == false) {

                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    alert(msg.Msg);
                    return false;
                }
            }
            else {
                $("#txtPolicyKey").val(msg.PolicyList[0].POLICY_ID);
                $("#uploadedPolicyDocument").attr('href', ("../assets/logos/Policy/" + msg.PolicyList[0].DOCUMENT));
                fnGetAllPolicyDocuments();
            }
        },
        error: function (error) {
            $("#Loader").hide();
            alert(error.status + ' ' + error.statusText);
        }
    })
}

function fnGetAllPolicyDocuments() {
    $("#Loader").show();
    var webUrl = uri + "/api/Policy/GetAllPolicyDocuments";
    $.ajax({
        type: 'GET',
        url: webUrl,
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            $("#Loader").hide();
            if (msg.StatusFl == false) {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    alert(msg.Msg);
                    return false;
                }
            }
            else {
                var str = '';
                for (index = 0; index < msg.PolicyList.length; index++) {
                    str += '<tr>';
                    str += '<td>' + FormatDate(msg.PolicyList[index].CREATED_DATE, $("input[id*=hdnDateFormat]").val()) + '</td>';
                    str += '<td>' + msg.PolicyList[index].CREATED_BY + '</td>';
                    str += '<td>' + msg.PolicyList[index].DOCUMENT + '</td>';
                    str += '<td><a class="fa fa-download" target="_blank" href="../assets/logos/Policy/' + msg.PolicyList[index].DOCUMENT + '"></a></td>';
                    str += '</tr>';
                }
                $("#tbdPolicyDocumentList").html(str);
            }
        },
        error: function (error) {
            $("#Loader").hide();
            alert(error.status + ' ' + error.statusText);
        }
    })
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

    filesData.append("Object", JSON.stringify({ POLICY_ID: PolicyID, DOCUMENT: document }));
    filesData.append("Files", $("#file").get(0).files[0]);
    var webUrl = uri + "/api/Policy/SavePolicy";
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
                    window.location.href = "../LogOut.aspx";
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
                fnGetPolicy();
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
        alert("No file chosen to upload document")
    }
    var arrayExtensions = ["pdf"];
    if ($.inArray(itemFile[0].name.split('.').pop().toLowerCase(), arrayExtensions) == -1) {
        alert("Only pdf format is allowed in Policy Document.");
        return false;
    }
    return true;
}

function ConvertToDateTime(dateTime) {
    var date = dateTime.split(" ")[0];

    return (date.split("/")[1] + "/" + date.split("/")[0] + "/" + date.split("/")[2]);
}




