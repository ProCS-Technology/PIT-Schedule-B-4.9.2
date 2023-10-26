$(document).ready(function () {
    window.history.forward();
    function preventBack() { window.history.forward(1); }
    $("#Loader").hide();
    fnGetTransactionalInfo();
})

function ConvertToDateTime(dateTime) {
    var date = dateTime.split(" ")[0];

    return (date.split("/")[1] + "/" + date.split("/")[0] + "/" + date.split("/")[2]);
}

function initializeDataTable() {
    $('#tbl-InitialHolding-setup').DataTable({
        //  "dom": '<"top"iflp<"clear">>rt<"bottom"iflp<"clear">>',
        dom: 'Bfrtip',
        pageLength: 10,
        buttons: [
         {
             extend: 'pdf',
             className: 'btn green btn-outline',
             exportOptions: {
                 columns: [1, 3, 5, 7, 8,11,12]
             }
         },
         {
             extend: 'excel',
             className: 'btn yellow btn-outline ',
             exportOptions: {
                 columns: [1, 3, 5, 7, 8,11,12]
             }
         },
     //    { extend: 'colvis', className: 'btn dark btn-outline', text: 'Columns' }
        ]
    });
}

function fnGetTransactionalInfo() {
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/GetTransactionalInfo";
    $.ajax({
        url: webUrl,
        type: "GET",
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            if (msg.StatusFl == true) {
                $("#Loader").hide();

                /* Initial Holding */
                setInitialHoldingDetail(msg);

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

function setInitialHoldingDetail(msg) {
    var str = "";
    for (var i = 0; i < msg.User.lstInitialHoldingDetail.length; i++) {
        str += '<tr>';
        str += '<td style="display:none;">' + msg.User.lstInitialHoldingDetail[i].restrictedCompany.ID + '</td>';
        str += '<td>' + msg.User.lstInitialHoldingDetail[i].restrictedCompany.companyName + '</td>';
        str += '<td style="display:none;">' + msg.User.lstInitialHoldingDetail[i].securityType + '</td>';
        str += '<td>' + msg.User.lstInitialHoldingDetail[i].securityTypeName + '</td>';
        str += '<td style="display:none;">' + msg.User.lstInitialHoldingDetail[i].relative.ID + '</td>';
        str += '<td>' + msg.User.lstInitialHoldingDetail[i].relative.relativeName + '</td>';
        str += '<td style="display:none;">' + msg.User.lstInitialHoldingDetail[i].dematAccount.ID + '</td>';
        str += '<td>' + msg.User.lstInitialHoldingDetail[i].dematAccount.accountNo + '</td>';
        str += '<td style="text-align: right;">' + msg.User.lstInitialHoldingDetail[i].noOfSecurities + '</td>';
        str += '<td style="display:none;">' + msg.User.lstInitialHoldingDetail[i].relative.panNumber + '</td>';
        str += '<td style="display:none;">' + msg.User.lstInitialHoldingDetail[i].dematAccount.tradingMemberId + '</td>';
        str += '<td>' + ConvertToDateTime(msg.User.lstInitialHoldingDetail[i].lastModifiedOn) + '</td>';
        // str += '<td>' + msg.User.lstInitialHoldingDetail[i].version + '</td>';
        str += '<td><a class="btn btn-default" data-toggle="modal" data-target="#modalInitialHoldingVersion" onclick="fnGetTransactionalInfoByVersion(' + msg.User.lstInitialHoldingDetail[i].ID + ')">Version</a></td>';
     //   str += '<td><a data-target="#modalAddInitialHoldingDeclarations" data-toggle="modal" class="btn btn-outline dark" onclick=\"javascript:fnEditInitialDeclarationDetail();\">Edit</a></td>';
        str += '</tr>';
    }
    var table = $('#tbl-InitialHolding-setup').DataTable();
    table.destroy();
    $("#tbdInitialDeclaration").html(str);
    initializeDataTable();
}

function fnGetTransactionalInfoByVersion(initialHoldingId) {
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/GetTransactionalInfoByVersion";
    $.ajax({
        url: webUrl,
        type: "GET",
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            if (msg.StatusFl == true) {
                $("#Loader").hide();

                /* Personal Information */
                var str = '';
                for (var i = 0; i < msg.User.lstInitialHoldingDetail.length; i++) {
                    if (initialHoldingId == msg.User.lstInitialHoldingDetail[i].ID) {
                        str += '<div style="text-align:center;"><b><u>Version ' + msg.User.lstInitialHoldingDetail[i].version + '</u></b></div><br/>';
                        str += '<table class="table table-striped table-hover table-bordered">';
                        str += '<tr>';
                        str += '<th>' + 'COMPANY' + '</th>';
                        str += '<td>' + msg.User.lstInitialHoldingDetail[i].restrictedCompany.companyName + '</td>';
                        str += '</tr>';
                        str += '<tr>';
                        str += '<th>' + 'SECURITY TYPE' + '</th>';
                        str += '<td>' + msg.User.lstInitialHoldingDetail[i].securityTypeName + '</td>';
                        str += '</tr>';
                        str += '<tr>';
                        str += '<th>' + 'FOR' + '</th>';
                        str += '<td>' + msg.User.lstInitialHoldingDetail[i].relative.relativeName + '</td>';
                        str += '</tr>';
                        str += '<tr>';
                        str += '<th>' + 'DEMAT ACCOUNT NO' + '</th>';
                        str += '<td>' + msg.User.lstInitialHoldingDetail[i].dematAccount.accountNo + '</td>';
                        str += '</tr>';
                        str += '<tr>';
                        str += '<th>' + 'NUMBER OF SECURITIES' + '</th>';
                        str += '<td>' + msg.User.lstInitialHoldingDetail[i].noOfSecurities + '</td>';
                        str += '</tr>';
                        str += '<tr>';
                        str += '<th>' + 'Last Modified' + '</th>';
                        str += '<td>' + ConvertToDateTime(msg.User.lstInitialHoldingDetail[i].lastModifiedOn) + '</td>';
                        str += '</tr>';
                        str += '</table><br/>';
                    }

                }

                $("#divInitialHoldingVersion").html(str);
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