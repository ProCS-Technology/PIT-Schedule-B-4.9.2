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

                    var AttFileName = msg.PolicyList[index].DOCUMENT;
                    var FileExtension = getFileExtension(AttFileName);

                    if (['pdf', 'txt', 'xlsx', 'xls', 'doc', 'docx', 'png', 'jpeg', 'gif', 'zip', 'ppt', 'pptx'].includes(FileExtension)) {
                        str += '<td><a class="fa fa-download" onclick="javascript:fnDownloadPolicy(' + msg.PolicyList[index].POLICY_ID + ', \'' + FileExtension + '\');"></a></td>';
                    }

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

function getFileExtension(AttFileName) {
    return AttFileName.split('.').pop();
}

function fnDownloadPolicy(PolicyId, FileExtension) {
    var webUrl = uri + "/api/Policy/GetPolicyFile?PolicyId=" + PolicyId + "&FileExtension=" + FileExtension;
    $.ajax({
        url: webUrl,
        type: 'GET',
        //headers: {
        //    Accept: "application/octet-stream; base64",
        //},
        success: function (data) {
            if (FileExtension == 'xls') {
                var uri = 'data:application/vnd.ms-excel;base64,' + data;
                var link = document.createElement("a");
                link.href = uri;
                link.style = "visibility:hidden";
                link.download = "ExcelReport.xls";

            }
            else if (FileExtension == 'xlsx') {
                var uri = 'data:application/vnd.ms-excel;base64,' + data;
                var link = document.createElement("a");
                link.href = uri;
                link.style = "visibility:hidden";
                link.download = "ExcelReport.xlsx";
            }
            else if (FileExtension == 'pdf') {
                var uri = 'data:application/pdf;base64,' + data;
                var link = document.createElement("a");
                link.href = uri;
                link.style = "visibility:hidden";
                link.download = "PDFReport.pdf";
            }
            else if (FileExtension == 'txt') {
                var uri = 'data:application/octet-stream;base64,' + data;
                var link = document.createElement("a");
                link.href = uri;
                link.style = "visibility:hidden";
                link.download = "TextFile.txt";
            }
            else if (FileExtension == 'png') {
                var uri = 'data:image/png;base64,' + data;
                var link = document.createElement("a");
                link.href = uri;
                link.style = "visibility:hidden";
                link.download = "Img.png";
            }
            else if (FileExtension == 'jpeg') {
                var uri = 'data:image/jpeg;base64,' + data;
                var link = document.createElement("a");
                link.href = uri;
                link.style = "visibility:hidden";
                link.download = "Img.jpeg";
            }
            else if (FileExtension == 'gif') {
                var uri = 'data:image/gif;base64,' + data;
                var link = document.createElement("a");
                link.href = uri;
                link.style = "visibility:hidden";
                link.download = "Img.gif";
            }
            else if (FileExtension == 'zip') {
                var uri = 'data:application/zip;base64,' + data;
                var link = document.createElement("a");
                link.href = uri;
                link.style = "visibility:hidden";
                link.download = "file.zip";
            }
            else if (FileExtension == 'doc') {
                var uri = 'data:application/vnd.openxmlformats-officedocument.wordprocessingml.document;base64,' + data;
                var link = document.createElement("a");
                link.href = uri;
                link.style = "visibility:hidden";
                link.download = "DocReport.doc";
            }
            else if (FileExtension == 'docx') {
                var uri = 'data:application/vnd.openxmlformats-officedocument.wordprocessingml.document;base64,' + data;
                var link = document.createElement("a");
                link.href = uri;
                link.style = "visibility:hidden";
                link.download = "DocReport.docx";
            }
            else if (FileExtension == 'ppt') {
                var uri = 'data:application/vnd.ms-powerpoint;base64,' + data;
                var link = document.createElement("a");
                link.href = uri;
                link.style = "visibility:hidden";
                link.download = "File.ppt";
            }
            else if (FileExtension == 'pptx') {
                var uri = 'data:application/vnd.openxmlformats-officedocument.presentationml.presentation;base64,' + data;
                var link = document.createElement("a");
                link.href = uri;
                link.style = "visibility:hidden";
                link.download = "File.pptx";
            }

            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
        },
        error: function () {
            console.log('error Occured while Downloading CSV file.');
        },
    });
}
function fnAddUpdatePolicy() {
    $("#Loader").show();

    var filesData = new FormData();
    var document = $("#file").get(0).files[0].name;
    var documentSize = $("#file").get(0).files[0].size;
    //alert($("#file").get(0).files[0].size);
    var PolicyID = $('#txtPolicyKey').val();

    if (PolicyID === "") {
        PolicyID = 0;
    }

    filesData.append("Object", JSON.stringify({ POLICY_ID: PolicyID, DOCUMENT: document }));
    filesData.append("FileSize", documentSize);
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
    //var arrayExtensions = ["pdf"];
    //if ($.inArray(itemFile[0].name.split('.').pop().toLowerCase(), arrayExtensions) == -1) {
    //    alert("Only pdf format is allowed in Policy Document.");
    //    return false;
    //}
    return true;
}
function ConvertToDateTime(dateTime) {
    var date = dateTime.split(" ")[0];
    return (date.split("/")[1] + "/" + date.split("/")[0] + "/" + date.split("/")[2]);
}