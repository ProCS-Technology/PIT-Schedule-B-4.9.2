$('input[id*=txtFromDate]').datepicker({
    todayHighlight: true,
    autoclose: true,
    format: $("input[id*=hdnJSDateFormat]").val(),
    clearBtn: true
});
$('input[id*=txtToDate]').datepicker({
    todayHighlight: true,
    autoclose: true,
    format: $("input[id*=hdnJSDateFormat]").val(),
    clearBtn: true
});

var table = $('#tbl-UPSIReport-setup').DataTable();
table.destroy();
var result = $("#ContentPlaceHolder1_txtReport").val();
initializeDataTable('tbl-UPSIReport-setup', [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14]);

$(document).ready(function () {
    window.history.forward();
    function preventBack() {
        window.history.forward(1);
    }
})
function initializeDataTable(id, columns) {
    $('#' + id).DataTable({
        dom: 'Bfrtip',
        pageLength: 10,
        "scrollY": "400px",
        "scrollX": true,
        buttons: [
            {
                extend: 'pdf',
                orientation: 'landscape',
                pageSize: 'TABLOID',
                title: 'UPSI Report',
                className: 'btn green btn-outline',
                exportOptions: {
                    columns: columns
                }
            },
            {
                extend: 'excel',
                title: 'UPSI Report - ' + $("select[id*=ddlUPSIGrp] option:selected").text(),
                className: 'btn yellow btn-outline ',
                exportOptions: {
                    columns: columns,
                    format: {
                        body: function (data, column, row, node) {
                            return column === 4 ? "\u200C" + data : data;
                        }
                    }
                }
            },
        ]
    });
}
function fnGetMessageBody(hdrId, lineId) {
    var msg = "";
    if (upsiCommunicationList !== null) {
        for (var i = 0; i < upsiCommunicationList.length; i++) {
            if (upsiCommunicationList[i].hdrId == hdrId && upsiCommunicationList[i].lineId == lineId) {
                msg = upsiCommunicationList[i].body;
                break;
            }
        }
    }
    $("#bdMessage").html(msg);
}
function fnGetAttachmentBody(hdrId, lineId) {
    var sMsg = $("#txtMsg_" + hdrId + "_" + lineId).html();
    var sAttachmentLnk = $("#txtAttachmentLnk_" + hdrId).val();

    if (sAttachmentLnk != "" && sAttachmentLnk != null) {
        sMsg += "<br />" + sAttachmentLnk;
    }
    $("#bdMessage").html(sMsg);
}
function fnValidateUPSIReport() {
    if ($("select[id*=ddlUPSIGrp]").val() == '' || $("select[id*=ddlUPSIGrp]") == undefined || $("select[id*=ddlUPSIGrp]") == null) {
        alert("Please select the UPSI Type/Group");
        return false;
    }
    if ($("select[id*=ddlUsers]").val() == '' || $("select[id*=ddlUsers]") == undefined || $("select[id*=ddlUsers]") == null) {
        alert("Please select the Shared By User");
        return false;
    }
    if ($("input[id*=txtFromDate]").val() == '' || $("input[id*=txtFromDate]").val() == undefined || $("input[id*=txtFromDate]").val() == null) {
        alert("Please select from date");
        return false;
    }
    else if ($("input[id*=txtToDate]").val() == '' || $("input[id*=txtToDate]").val() == undefined || $("input[id*=txtToDate]").val() == null) {
        alert("Please select to date");
        return false;
    }
    else {
        var FromDate = new Date(convertToDateTime($("#txtFromDate").val()));
        var Todate = new Date(convertToDateTime($("#txtToDate").val()));
        if (Todate < FromDate) {
            alert("To Date Should be greater than From Date");
            return false;
        }
    }
    return true;
}
function fnHistoryUPSIGroup(taskid) {
    $("#Loader").show();
    var webUrl = uri + "/api/DashboardIT/GetMyUPSITaskById";
    $.ajax({
        url: webUrl,
        type: "POST",
        async: false,
        data: {
            TaskId: taskid
        },
        success: function (msg) {
            $("#Loader").hide();
            if (msg.Msg == "SessionExpired") {
                alert("Your session is expired. Please login again to continue");
                window.location.href = "../LogOut.aspx";
            }
            if (msg.StatusFl == true) {
                $("#dvUPSITaskMSGBody").html(msg.Dashboard.listUPSITask[0].TaskMailBody);
                $("#dvUPSITaskMSGFrom").html(msg.Dashboard.listUPSITask[0].EmailFrom);
                $("#dvUPSITaskMSGTo").html(msg.Dashboard.listUPSITask[0].EmailTo);
                $("#dvUPSITaskMSGCC").html(msg.Dashboard.listUPSITask[0].EmailCC);
                $("#dvUPSITaskMsgDate").html(msg.Dashboard.listUPSITask[0].EmailDate);
                $("#dvAttechmentlistMsg").html('');
                var result = "";
                for (var i = 0; i < msg.Dashboard.listUPSITask[0].listAttachment.length; i++) {
                    result += '<p>';
                    result += '<a href="emailAttachment/' + msg.Dashboard.listUPSITask[0].listAttachment[i].Attachment + '" target="_blank">' + msg.Dashboard.listUPSITask[0].listAttachment[i].Attachment + '</a>';
                    result += '</p>';
                }
                $("#dvAttechmentlistMsg").html(result);
            }
        },
        error: function (response) {
            $("#Loader").hide();
            $("#txtUPSITaskID").val('');
            if (response.responseText == "Session Expired") {
                alert("Your session is expired. Please login again to continue");
                window.location.href = "../LogOut.aspx";
                return false;
            }
            else {
                alert(response.status + ' ' + response.statusText);
            }
        }
    });
}
function fnHistoryUPSIGroupRemarks(Gpid) {
    $('#tbody').html("");
    $('#tbody_prev').html("");
    $("#Loader").show();
    var webUrl = uri + "/api/UPSIGroupReport/HistoryUPSIGroup";
    $.ajax({
        type: "POST",
        url: webUrl,
        data: JSON.stringify({
            GROUP_ID: Gpid
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            $("#Loader").hide();
            if (msg.StatusFl) {
                $("#group_name_remarks").html('');
                $("#group_name_remarks").html(msg.UPSIMembersGroupList[0].GROUP_NM);
                GroupUserRemarks = msg.UPSIMembersGroupList[0].listGroupUserRemarks;
                var result = "";
                for (var i = 0; i < msg.UPSIMembersGroupList[0].listGroupUserRemarks.length; i++) {
                    var seq = i + 1;
                    result += '<tr id="tr_' + msg.UPSIMembersGroupList[0].listGroupUserRemarks[i].HdrId + '">';
                    result += '<td>' + msg.UPSIMembersGroupList[0].listGroupUserRemarks[i].Email + '</td>';
                    result += '<td>' + msg.UPSIMembersGroupList[0].listGroupUserRemarks[i].mailDate + '</td>';
                    result += '<td><a id="am_' + msg.UPSIMembersGroupList[0].listGroupUserRemarks[i].HdrId + '" class="btn btn-outline dark" onclick=\"javascript:fnHistoryUPSIGroupMSG(' + msg.UPSIMembersGroupList[0].listGroupUserRemarks[i].HdrId + ');\">Click</a></td>';
                    result += '<td><a id="au_' + msg.UPSIMembersGroupList[0].listGroupUserRemarks[i].HdrId + '" class="btn btn-outline dark" onclick=\"javascript:fnHistoryUPSIGroupTO(' + msg.UPSIMembersGroupList[0].listGroupUserRemarks[i].HdrId + ');\">Click</a></td>';
                    result += '<td><a id="aat_' + msg.UPSIMembersGroupList[0].listGroupUserRemarks[i].HdrId + '" class="btn btn-outline dark" onclick=\"javascript:fnHistoryUPSIGroupAttachment(' + msg.UPSIMembersGroupList[0].listGroupUserRemarks[i].HdrId + ');\">Click</a></td>';
                    result += '</tr>';
                }
                var table = $('#adduser_Remarks').DataTable();
                table.destroy();
                $("#tbody_Remarks").html(result);
                $("#UPSI_Remarks").modal('show');
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
            if (response.responseText == "Session Expired") {
                alert("Your session is expired. Please login again to continue");
                $("#Loader").show();
                window.location.href = "../LogOut.aspx";
                return false;
            }
            else {
                alert(response.status + ' ' + response.statusText);
            }
        }
    });
}
function fnHistoryUPSIGroupMSG(hrid) {
    for (var i = 0; i < GroupUserRemarks.length; i++) {
        if (GroupUserRemarks[i].HdrId == hrid) {
            $("#bdMessage").html(GroupUserRemarks[i].msgBody);
        }
    }
    $("#messageBody").modal('show');
}
function fnHistoryUPSIGroupTO(hdrid) {
    var result = ""
    for (var i = 0; i < GroupUserRemarks.length; i++) {
        if (GroupUserRemarks[i].HdrId == hdrid && GroupUserRemarks[i].listUserDetail.length > 0) {
            for (var j = 0; j < GroupUserRemarks[i].listUserDetail.length; j++) {
                result += '<tr id="tr_' + GroupUserRemarks[i].listUserDetail[j].HdrId + '">';
                result += '<td>' + GroupUserRemarks[i].listUserDetail[j].EmailType + '</td>';
                result += '<td>' + GroupUserRemarks[i].listUserDetail[j].Email + '</td>';
                result += '</tr>';
            }
        }
    }
    $("#tbdmailtocc").html('');
    $("#tbdmailtocc").html(result);
    $("#MailtoCC").modal('show');
}
function fnHistoryUPSIGroupAttachment(hdrid) {
    var result = "No Attachment."
    for (var i = 0; i < GroupUserRemarks.length; i++) {
        if (GroupUserRemarks[i].HdrId == hdrid && GroupUserRemarks[i].listRemarksAttachments.length > 0) {
            result = ""
            for (var j = 0; j < GroupUserRemarks[i].listRemarksAttachments.length; j++) {
                result += '<tr id="tr_' + GroupUserRemarks[i].listRemarksAttachments[j].HdrId + '">';
                result += '<td><a href="emailAttachment/' + GroupUserRemarks[i].listRemarksAttachments[j].Attachment + '" target="_blank">' + GroupUserRemarks[i].listRemarksAttachments[j].Attachment + '</a></td>';
                result += '</tr>';
            }
        }
    }
    $("#tbdAttachmentBody").html('');
    $("#tbdAttachmentBody").html(result);
    $("#attachmentBody").modal('show');
}
function CancleHistory_model() {
    $("#UPSI_history").modal('hide');
}
function fnGetUPSIRemarks(upsiRemarks) {
    $("#dvupsiremarks").html("");
    $("#dvupsiremarks").html(GroupUserRemarksX[upsiRemarks]);
}
function convertToDateTime(date) {
    return date.split("/")[1] + "/" + date.split("/")[0] + "/" + date.split("/")[2];
}

function fnDownloadAttechment(attechmentId, fileExtension) {
    debugger;
    var webUrl = uri + "/api/UPSI/GetAttechment?attechmentId=" + attechmentId + "&Ext=" + fileExtension;
    $.ajax({
        url: webUrl,
        type: 'GET',
        //headers: {
        //    Accept: "application/octet-stream; base64",
        //},
        success: function (data) {
            debugger;
            if (fileExtension == '.xls') {
                var uri = 'data:application/vnd.ms-excel;base64,' + data;
                var link = document.createElement("a");
                link.href = uri;
                link.style = "visibility:hidden";
                link.download = "ExcelReport.xls";

            }
            else if (fileExtension == '.xlsx') {
                var uri = 'data:application/vnd.ms-excel;base64,' + data;
                var link = document.createElement("a");
                link.href = uri;
                link.style = "visibility:hidden";
                link.download = "ExcelReport.xls";
            }
            else if (fileExtension == '.pdf') {
                var uri = 'data:application/pdf;base64,' + data;
                var link = document.createElement("a");
                link.href = uri;
                link.style = "visibility:hidden";
                link.download = "PDFReport.pdf";
            }
            else if (fileExtension == '.txt') {
                var uri = 'data:application/octet-stream;base64,' + data;
                var link = document.createElement("a");
                link.href = uri;
                link.style = "visibility:hidden";
                link.download = "TextFile.txt";
            }
            else if (fileExtension == '.png') {
                var uri = 'data:image/png;base64,' + data;
                var link = document.createElement("a");
                link.href = uri;
                link.style = "visibility:hidden";
                link.download = "Img.png";
            }
            else if (fileExtension == '.jpeg') {
                var uri = 'data:image/jpeg;base64,' + data;
                var link = document.createElement("a");
                link.href = uri;
                link.style = "visibility:hidden";
                link.download = "Img.jpeg";
            }
            else if (fileExtension == '.gif') {
                var uri = 'data:image/gif;base64,' + data;
                var link = document.createElement("a");
                link.href = uri;
                link.style = "visibility:hidden";
                link.download = "Img.gif";
            }
            else if (fileExtension == '.zip') {
                var uri = 'data:application/zip;base64,' + data;
                var link = document.createElement("a");
                link.href = uri;
                link.style = "visibility:hidden";
                link.download = "file.zip";
            }
            else if (fileExtension == '.doc'  ) {
                var uri = 'data:application/msword;base64,' + data;
                var link = document.createElement("a");
                link.href = uri;
                link.style = "visibility:hidden";
                link.download = "DocReport.doc";
            }
            else if ( fileExtension == '.docx') {
                var uri = 'data:application/vnd.openxmlformats-officedocument.wordprocessingml.document;base64,' + data;
                var link = document.createElement("a");
                link.href = uri;
                link.style = "visibility:hidden";
                link.download = "DocReport.docx";
            }
            else if (fileExtension == '.pptx') {
                var uri = 'data:application/vnd.openxmlformats-officedocument.presentationml.presentation;base64,' + data;
                var link = document.createElement("a");
                link.href = uri;
                link.style = "visibility:hidden";
                link.download = "File.pptx";
            }
            else if (fileExtension == '.ppt') {
                var uri = 'data:application/vnd.ms-powerpoint,' + data;
                var link = document.createElement("a");
                link.href = uri;
                link.style = "visibility:hidden";
                link.download = "file.ppt";
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

//function fnDownloadBenpos(BenposId) {
//    debugger;
//    var webUrl = uri + "/api/UPSI/GetBenposFile?BenposId=" + BenposId;
//    $.ajax({
//        url: webUrl,
//        type: 'GET',
//        //headers: {
//        //    Accept: "application/pdf; base64",
//        //},
//        success: function (data) {
//            debugger;
//            // Check if it's a PDF
//            if (isPDF(data)) {
//                var uri = 'data:application/pdf;base64,' + data;
//                var link = document.createElement("a");
//                link.href = uri;
//                link.style = "visibility:hidden";
//                link.download = "PDFReport.pdf";
//                document.body.appendChild(link);
//                link.click();
//                document.body.removeChild(link);
//            }
//            // Add additional checks for other file types (Excel, image) if needed.
//            else if (isExcel(data)) {
//                var uri;
//                var link = document.createElement("a");
//                link.style = "visibility:hidden";

//                if (isXLSX(data)) {
//                    uri = 'data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,' + data;
//                    link.download = "ExcelReport.xlsx";
//                } else if (isXLS(data)) {
//                    uri = 'data:application/vnd.ms-excel;base64,' + data;
//                    link.download = "ExcelReport.xls";
//                } else {
//                    console.log('Not an Excel file.');
//                    return; // Exit the function if it's neither XLS nor XLSX.
//                }

//                link.href = uri;
//                document.body.appendChild(link);
//                link.click();
//                document.body.removeChild(link);
//            }
//            else if (isImage(data)) { // Handle image file
//                // Assuming the "isImage" function has already been defined to check for image files.

//                if (isImage(data)) {
//                    // Create a data URI for the image.
//                    var imageMime = "image/png"; // Change the MIME type as needed for the specific image format.
//                    var uri = 'data:' + imageMime + ';base64,' + data;

//                    // Create a link element to trigger the download.
//                    var link = document.createElement("a");
//                    link.href = uri;
//                    link.style = "visibility:hidden";

//                    // Provide a meaningful file name with the appropriate extension.
//                    link.download = "ImageFile.png"; // Change the file name and extension as needed.

//                    // Append the link to the document body, trigger the click event, and remove the link.
//                    document.body.appendChild(link);
//                    link.click();
//                    document.body.removeChild(link);
//                } else {
//                    console.log('Not an image file.');
//                }

//            }
//            else if (isTXT(data)) { // Handle image file
//                // Assuming the "isTXT" function has already been defined to check for TXT files.

//                if (isTXT(data)) {
//                    // Create a data URI for the text file.
//                    var txtMime = "application/octet-stream"; // Set the MIME type for text files.
//                    var uri = 'data:' + txtMime + ';base64,' + data;

//                    // Create a link element to trigger the download.
//                    var link = document.createElement("a");
//                    link.href = uri;
//                    link.style = "visibility:hidden";

//                    // Provide a meaningful file name with the appropriate extension, e.g., ".txt".
//                    link.download = "TextFile.txt"; // Change the file name and extension as needed.

//                    // Append the link to the document body, trigger the click event, and remove the link.
//                    document.body.appendChild(link);
//                    link.click();
//                    document.body.removeChild(link);
//                } else {
//                    console.log('Not a text file.');
//                }

//            }
//            else if (isWord(data)) { // Handle image file
//                // Assuming the "isWord" function has already been defined to check for Word files.

//                if (isWord(data)) {
//                    // Create a data URI for the Word file.
//                    var wordMime = "application/msword"; // Set the MIME type for Word files.
//                    var uri = 'data:' + wordMime + ';base64,' + data;

//                    // Create a link element to trigger the download.
//                    var link = document.createElement("a");
//                    link.href = uri;
//                    link.style = "visibility:hidden";

//                    // Provide a meaningful file name with the appropriate extension, e.g., ".doc" or ".docx".
//                    link.download = "WordDocument.doc"; // Change the file name and extension as needed.

//                    // Append the link to the document body, trigger the click event, and remove the link.
//                    document.body.appendChild(link);
//                    link.click();
//                    document.body.removeChild(link);
//                } else {
//                    console.log('Not a Word document.');
//                }

//            }
//            else {
//                console.log('Unknown file type or error.');
//            }
//        },
//        error: function () {
//            console.log('Error Occurred while Downloading the file.');
//        },
//    });
//}

//// Function to check if the data is a PDF
//function isPDF(data) {
//    // Check the first few bytes of the data to determine if it's a PDF file.
//    return data.startsWith("JVBERi0xLj");
//}

//function isExcel(data) {
//    debugger;
//    // Check the first few bytes for the Excel file signature.
//    // XLS (BIFF) file signature: D0 CF 11 E0 A1 B1 1A E1
//    // XLSX (Office Open XML) file signature: 50 4B 03 04
//    var xlsSignature = "D0CF11E0A1B11AE1";
//    var xlsxSignature = "504B0304";

//    // Remove any white spaces and convert to uppercase for comparison.
//    data = data.replace(/\s/g, "").toUpperCase();

//    return data.includes(xlsSignature) || data.includes(xlsxSignature);
//}

//function isXLSX(data) {
//    // Check the first few bytes for the XLSX file signature.
//    var xlsxSignature = "504B0304"; // Office Open XML format for XLSX files

//    // Remove any white spaces and convert to uppercase for comparison.
//    data = data.replace(/\s/g, "").toUpperCase();

//    return data.includes(xlsxSignature);
//}

//function isXLS(data) {
//    // Check the first few bytes for the XLS file signature.
//    var xlsSignature = "D0CF11E0A1B11AE1"; // Older BIFF format for XLS files

//    // Remove any white spaces and convert to uppercase for comparison.
//    data = data.replace(/\s/g, "").toUpperCase();

//    return data.includes(xlsSignature);
//}

//function isImage(data) {
//    // Check the first few bytes for common image file signatures.
//    // JPEG file signature: FF D8
//    // PNG file signature: 89 50 4E 47
//    // GIF file signature: 47 49 46 38 (GIF87a) or 47 49 46 39 (GIF89a)
//    var jpegSignature = "FFD8";
//    var pngSignature = "89504E47";
//    var gif87aSignature = "47494638";
//    var gif89aSignature = "47494639";

//    // Remove any white spaces and convert to uppercase for comparison.
//    data = data.replace(/\s/g, "").toUpperCase();

//    return (
//        data.startsWith(jpegSignature) ||
//        data.startsWith(pngSignature) ||
//        data.startsWith(gif87aSignature) ||
//        data.startsWith(gif89aSignature)
//    );
//}

//function isTXT(data) {
//    // Check the first few bytes for the TXT file signature.
//    // A TXT file typically doesn't have a specific signature, so you may need to check for the absence of known signatures for other formats.

//    // For example, check for the absence of PDF, Excel, image, and Word signatures.
//    return !isPDF(data) && !isExcel(data) && !isImage(data) && !isWord(data);
//}

//function isWord(data) {
//    // Check the first few bytes for Microsoft Word file signatures.
//    // DOC file signature: D0 CF 11 E0 A1 B1 1A E1 (older DOC format)
//    // DOCX file signature: 50 4B 03 04 (Office Open XML format)
//    var docSignature = "D0CF11E0A1B11AE1";
//    var docxSignature = "504B0304";

//    // Remove any white spaces and convert to uppercase for comparison.
//    data = data.replace(/\s/g, "").toUpperCase();

//    return data.includes(docSignature) || data.includes(docxSignature);
//}
