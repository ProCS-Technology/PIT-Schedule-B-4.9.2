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
        str += '<td><a href="../assets/logos/Policy/' + msg.User.lstFinalDeclaration[i].fileName + '" target="_blank">Policy</a></td>';
        str += '<td>' + msg.User.lstFinalDeclaration[i].PolicyVersion + '</td>';
        if (msg.User.lstFinalDeclaration[i].fileFormEOrF != "") {
            str += '<td><a href="emailAttachment/' + msg.User.lstFinalDeclaration[i].fileFormEOrF + '" target="_blank">' + msg.User.lstFinalDeclaration[i].fileFormB + '</a></td>';
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
function ConvertToDateTime(dateTime) {
    var date = dateTime.split(" ")[0];
    return (date.split("/")[0] + "/" + date.split("/")[1] + "/" + date.split("/")[2]);
}