$(document).ready(function () {
    window.history.forward();
    function preventBack() { window.history.forward(1); }
    $("#Loader").hide();
    fnGetTransactionalInfo();
})
function initializeDataTable() {
    $('#tbl-FinalDeclaration-setup').DataTable({
        dom: 'Bfrtip',
        pageLength: 10,
        buttons: [
            {
                extend: 'pdf',
                className: 'btn green btn-outline',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6]
                }
            },
            {
                extend: 'excel',
                className: 'btn yellow btn-outline ',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6]
                }
            },
        ]
    });
}
function fnGetTransactionalInfo() {
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/GetDeclarationForms";
    $.ajax({
        url: webUrl,
        type: "POST",
        data: JSON.stringify({
            LOGIN_ID: null
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            if (msg.StatusFl == true) {
                $("#Loader").hide();
                setFinalDeclartion(msg);
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
    })
}
function setFinalDeclartion(msg) {
    var str = "";
    for (var i = 0; i < msg.User.lstFinalDeclaration.length; i++) {
        str += '<tr>';
        str += '<td>' + ConvertToDateTime(msg.User.lstFinalDeclaration[i].createdOn) + " " + msg.User.lstFinalDeclaration[i].createdOn.split(" ")[1] + '</td>';
        str += '<td>' + msg.User.lstFinalDeclaration[i].createdBy + '</td>';
        var fileName = msg.User.lstFinalDeclaration[i].fileName;
        var ext = fileName.substr(fileName.lastIndexOf('.') + 1);
        var extension = '.' + ext;
        //str += '<td><a href="emailAttachment/' + msg.User.lstFinalDeclaration[i].fileFormEOrF + '" target="_blank">' + msg.User.lstFinalDeclaration[i].fileFormB + '</a></td>';
        /* ////debugger;*/
        //str += '<td><a class="fa fa-download" onclick=\'javascript:fnDownloadDeclaration("' + msg.User.lstFinalDeclaration[i].Id + '");\'>' + msg.User.lstFinalDeclaration[i].fileFormB + '</a></td>';
        if (['.pdf', '.txt', '.xlsx', '.xls', '.doc', '.docx', '.png', '.jpeg', '.gif', '.zip', '.ppt', '.pptx'].includes(extension)) {
            str += '<td><a class="fa fa-download" onclick=\'javascript:fnDownloadPolicy("' + msg.User.lstFinalDeclaration[i].Id + '","' + extension + '");\'>Policy</a></td>';
        }
        //str += '<td><a href="../assets/logos/Policy/' + msg.User.lstFinalDeclaration[i].fileName + '" target="_blank">Policy</a></td>';
        str += '<td>' + msg.User.lstFinalDeclaration[i].PolicyVersion + '</td>';
        if (msg.User.lstFinalDeclaration[i].fileFormEOrF != "") {
            ////debugger;
            var fileName = msg.User.lstFinalDeclaration[i].fileFormEOrF;
            var ext = fileName.substr(fileName.lastIndexOf('.') + 1);
            var extension = '.' + ext;
            //str += '<td><a href="emailAttachment/' + msg.User.lstFinalDeclaration[i].fileFormEOrF + '" target="_blank">' + msg.User.lstFinalDeclaration[i].fileFormB + '</a></td>';
           /* ////debugger;*/
            //str += '<td><a class="fa fa-download" onclick=\'javascript:fnDownloadDeclaration("' + msg.User.lstFinalDeclaration[i].Id + '");\'>' + msg.User.lstFinalDeclaration[i].fileFormB + '</a></td>';
            if (['.pdf', '.txt', '.xlsx', '.xls', '.doc', '.docx', '.png', '.jpeg', '.gif', '.zip', '.ppt', '.pptx'].includes(extension))
            {
                str += '<td><a class="fa fa-download" onclick=\'javascript:fnDownloadDeclaration("' + msg.User.lstFinalDeclaration[i].Id + '","' + extension + '");\'>' + msg.User.lstFinalDeclaration[i].fileFormB + '</a></td>';
            }
            
        }
        else {
            str += '<td></td>';
        }
        str += '</tr>';
    }
    var table = $('#tbl-FinalDeclaration-setup').DataTable();
    table.destroy();
    $("#tbdFinalDeclaration").html(str);
    initializeDataTable();
}

function fnDownloadDeclaration(DeclarationId, fileExtension) {
    //////debugger;
    var webUrl = uri + "/api/UserIT/GetDeclarationFile?DeclarationId=" + DeclarationId + "&Ext=" + fileExtension;
    $.ajax({
        url: webUrl,
        type: 'GET',
        headers: {
            Accept: "application/pdf; base64",
        },
        success: function (data) {
            ////debugger;
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
            else if (fileExtension == '.doc') {
                var uri = 'data:application/msword;base64,' + data;
                var link = document.createElement("a");
                link.href = uri;
                link.style = "visibility:hidden";
                link.download = "DocReport.doc";
            }
            else if (fileExtension == '.docx') {
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

function fnDownloadPolicy(DeclarationId, fileExtension) {
     //debugger;
    var webUrl = uri + "/api/UserIT/GetPolicyFile?DeclarationId=" + DeclarationId + "&Ext=" + fileExtension;
    $.ajax({
        url: webUrl,
        type: 'GET',
        headers: {
            Accept: "application/pdf; base64",
        },
        success: function (data) {
            ////debugger;
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
            else if (fileExtension == '.doc') {
                var uri = 'data:application/msword;base64,' + data;
                var link = document.createElement("a");
                link.href = uri;
                link.style = "visibility:hidden";
                link.download = "DocReport.doc";
            }
            else if (fileExtension == '.docx') {
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
function ConvertToDateTime(dateTime) {
    var date = dateTime.split(" ")[0];
    return (date.split("/")[0] + "/" + date.split("/")[1] + "/" + date.split("/")[2]);
}
