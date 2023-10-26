$(document).ready(function () {
    window.history.forward();
    function preventBack() { window.history.forward(1); }
    $("#Loader").hide();
    fnBindBusinessUnitList();
    fnBindUserList();
    fnGetTransactionalInfo();

    $("#bindBusinessUnit").select2({
        placeholder: "Select a company"
    });

    $("#bindUser").select2({
        placeholder: "Select a user"
    });

    $("#bindBusinessUnit").on('change', function () {
        fnBindUserList();
    });
})

function isJson(str) {
    try {
        JSON.parse(str);
    } catch (e) {
        return false;
    }
    return true;
}

function fnBindBusinessUnitList() {
    $("#Loader").show();
    var webUrl = uri + "/api/BusinessUnit/GetAllBusinessUnitList";
    $.ajax({
        type: "GET",
        url: webUrl,
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                var result = "";
                for (var i = 0; i < msg.BusinessUnitList.length; i++) {
                    result += '<option value="' + msg.BusinessUnitList[i].businessUnitId + '">' + msg.BusinessUnitList[i].businessUnitName + '</option>';
                }

                $("#bindBusinessUnit").append(result);
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

function fnBindUserList() {
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/GetUserListByBusinessUnitId";
    $.ajax({
        type: "POST",
        url: webUrl,
        data: JSON.stringify({ businessUnit: { businessUnitId: $("#bindBusinessUnit").val() } }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        //   async: false,
        success: function (msg) {
            if (msg.StatusFl == true) {
                var result = "";
                //result += '<option value="0">All</option>';
                result += '<option value="">Please Select</option>';
                for (var i = 0; i < msg.UserList.length; i++) {
                    result += '<option value="' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].USER_NM + '(' + msg.UserList[i].USER_EMAIL + ')' + '</option>';
                }
                $("#bindUser").html(result);
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
                    $("#bindUser").html('');
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

function fnGetMyDetailsReport() {
    if (fnValidate()) {
        // $("#userLoginId").val($("#bindUser").val());
        $("#Loader").show();
        var webUrl = uri + "/api/UserIT/GetMyDetailReport";
        $.ajax({
            type: "POST",
            url: webUrl,
            data: JSON.stringify({ LOGIN_ID: $("#bindUser").val() }),
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            success: function (msg) {
                var result = "";
                if (msg.StatusFl == true) {
                    $("#Loader").hide();

                    /* Final Declaration Info */
                    setFinalDeclartion(msg);
                }
                else {
                    var table = $('#tbl-FinalDeclaration-setup').DataTable();
                    table.destroy();
                    $("#tbdFinalDeclaration").html('');
                    initializeDataTable();
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
}

function fnValidate() {
    if ($("#bindUser").val() == '' || $("#bindUser").val() == undefined || $("#bindUser").val() == null) {
        alert("Please select the user");
        return false;
    }
    return true;
}

function initializeDataTable() {
    $('#tbl-FinalDeclaration-setup').DataTable({
        //  "dom": '<"top"iflp<"clear">>rt<"bottom"iflp<"clear">>',
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
                    columns: [0, 1, 2, 3, 4, 5, 5, 6]
                }
            },
            //    { extend: 'colvis', className: 'btn dark btn-outline', text: 'Columns' }
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
            LOGIN_ID: $("#bindUser").val()
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            if (msg.StatusFl == true) {
                $("#Loader").hide();

                /* Final Declaration Info */
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
    if (msg.User.lstFinalDeclaration != null) {
        for (var i = 0; i < msg.User.lstFinalDeclaration.length; i++) {
            str += '<tr>';
            str += '<td>' + ConvertToDateTime(msg.User.lstFinalDeclaration[i].createdOn) + " " + msg.User.lstFinalDeclaration[i].createdOn.split(" ")[1] + '</td>';
            str += '<td>' + msg.User.lstFinalDeclaration[i].createdBy + '</td>';

            var fileName = msg.User.lstFinalDeclaration[i].fileName;
            var ext = fileName.substr(fileName.lastIndexOf('.') + 1);
            var extension = '.' + ext;
            if (['.pdf', '.txt', '.xlsx', '.xls', '.doc', '.docx', '.png', '.jpeg', '.gif', '.zip', '.ppt', '.pptx'].includes(extension)) {
                str += '<td><a class="fa fa-download" onclick=\'javascript:fnDownloadPolicy("' + msg.User.lstFinalDeclaration[i].Id + '","' + extension + '");\'>Policy</a></td>';
            }
            str += '<td>' + msg.User.lstFinalDeclaration[i].PolicyVersion + '</td>';
            if (msg.User.lstFinalDeclaration[i].fileFormEOrF != "") {

                var AttFileName = msg.User.lstFinalDeclaration[i].fileFormEOrF;
                var FileExtension = getFileExtension(AttFileName);
                if (['pdf', 'txt', 'xlsx', 'xls', 'doc', 'docx', 'png', 'jpeg', 'gif', 'zip', 'ppt', 'pptx'].includes(FileExtension)) {
                    str += '<td><a onclick="fnDownloadAtt(' + msg.User.lstFinalDeclaration[i].Id + ', \'' + FileExtension + '\');" target="_blank">' + msg.User.lstFinalDeclaration[i].fileFormB + '</a></td>';
                }
            }
            else {
                str += '<td></td>';
            }
            str += '</tr>';
        }
    }

    var table = $('#tbl-FinalDeclaration-setup').DataTable();
    table.destroy();
    $("#tbdFinalDeclaration").html(str);
    initializeDataTable();
}

function getFileExtension(AttFileName) {
    return AttFileName.split('.').pop();
}

function ConvertToDateTime(dateTime) {
    var date = dateTime.split(" ")[0];

    return (date.split("/")[1] + "/" + date.split("/")[0] + "/" + date.split("/")[2]);
}


function fnDownloadPolicy(DeclarationId, fileExtension) {
    //debugger;
    var webUrl = uri + "/api/UserIT/GetPolicyFileJK?DeclarationId=" + DeclarationId + "&Ext=" + fileExtension;
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


function fnDownloadAtt(Id, FileExtension) {
    var webUrl = uri + "/api/UserIT/GetAttachmentFile?Id=" + Id + "&FileExtension=" + FileExtension;
    $.ajax({
        url: webUrl,
        type: 'GET',
        headers: {
            Accept: "application/octet-stream; base64",
        },
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

