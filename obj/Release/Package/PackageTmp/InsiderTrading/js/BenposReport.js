$(document).ready(function () {
    $("#Loader").hide();
    initializeDataTable('tbl-benposReport-setup', [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11], []);
    fnBindBenposUploadedList();
})

function fnBindBenposUploadedList() {
    $("#Loader").show();
    var webUrl = uri + "/api/ReportsIT/GetBenposUploadedList";
    $.ajax({
        type: "GET",
        url: webUrl,
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            if (msg.StatusFl == true) {
                var result = "";
                result += '<option value="0">Please select</option>';
                for (var i = 0; i < msg.lstBenposHeader.length; i++) {
                    result += '<option value="' + msg.lstBenposHeader[i].asOfDate + '">' + msg.lstBenposHeader[i].asOfDate + '</option>';
                }
                $("#bindBenposUploadedOn").html(result);
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

function initializeDataTable(id, columns, data) {
    var dt = $('#' + id).DataTable({
        dom: 'Bfrtip',
        pageLength: 10,
        "scrollY": "300px",
        "scrollX": true,
        buttons: [
         {
             extend: 'pdf',
             className: 'btn green btn-outline',
             exportOptions: {
                 columns: columns
             }
         },
         {
             extend: 'excel',
             className: 'btn yellow btn-outline ',
             exportOptions: {
                 columns: columns,
                 format: {
                     body: function (data, column, row, node) {
                         return column === 3 ? "\u200C" + data : data;
                     }
                 }
             }
         },
     //    { extend: 'colvis', className: 'btn dark btn-outline', text: 'Columns' }
        ],
        "data": data,
        "columns": [
            {
                "class": 'details-control',
                "orderable": false,
                "data": null,
                "defaultContent": ''
            },
        { "data": "loginId", "orderable": false },
        { "data": "name", "orderable": false },
        { "data": "pan", "orderable": false },
        { "data": "relative", "orderable": false },
        { "data": "relation", "orderable": false },
        { "data": "initialHoldingAfterDeclaration", "orderable": false },
        { "data": "holdingAsOnDate", "orderable": false },
        { "data": "tradeDate", "orderable": false },
        { "data": "tradeQuantity", "orderable": false },
        { "data": "isNonCompliant", "orderable": false },
        { "data": "compliantType", "orderable": false }
        ],
        // "order": [[1, 'asc']]
    });

    // Array to track the ids of the details displayed rows
    var detailRows = [];

    $('#tbl-benposReport-setup tbody').on('click', 'tr td.details-control', function () {
        var tr = $(this).closest('tr');
        var row = dt.row(tr);
        var idx = $.inArray(tr.attr('id'), detailRows);

        if (row.child.isShown()) {
            tr.removeClass('details');
            row.child.hide();

            // Remove from the 'open' array
            detailRows.splice(idx, 1);
        }
        else {
            tr.addClass('details');
            row.child(format(row.data())).show();

            // Add to the 'open' array
            if (idx === -1) {
                detailRows.push(tr.attr('id'));
            }
        }
    });

    // On each draw, loop over the `detailRows` array and show any child rows
    dt.on('draw', function () {
        $.each(detailRows, function (i, id) {
            $('#' + id + ' td.details-control').trigger('click');
        });
    });
}

function fnGetBenposReport() {
    if (fnValidate()) {
        $("#Loader").show();
        var webUrl = uri + "/api/ReportsIT/GetBenposReport";
        $.ajax({
            type: "POST",
            url: webUrl,
            data: JSON.stringify({ userId: $("#bindBenposUploadedOn").val() }),
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            success: function (msg) {
                var result = "";
                if (msg.StatusFl == true) {
                    var data = [];
                    //if (msg.lstBenposReport !== null) {
                    //    if (msg.lstBenposReport.length > 0) {
                    //        for (var i = 0; i < msg.lstBenposReport.length; i++) {

                    //            obj.name = msg.lstBenposReport[i].loginId;
                    //            obj.relation = 'Father In Law , Mother In Low';
                    //            obj.pan = 'AVNPA3600J';
                    //            //  obj.folioNumber = msg.lstTradingReport[i].folioNumber;
                    //            obj.folioNumber = '1234567887654321';
                    //            obj.initialHoldingAfterDeclaration = '100';

                    //            obj.holdingAsOnDate = '200';

                    //            obj.tradeDate = '18/08/2020';

                    //            obj.tradeQuantity = '100';

                    //            data.push(obj);

                    //        }
                    //    }
                    //}
                    if (msg.lstBenposReport !== null) {
                        if (msg.lstBenposReport.length > 0) {
                            var table = $('#tbl-benposReport-setup').DataTable();
                            table.destroy();

                            initializeDataTable('tbl-benposReport-setup', [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11], msg.lstBenposReport);
                        }
                        else {
                            var table = $('#tbl-benposReport-setup').DataTable();
                            table.destroy();

                            initializeDataTable('tbl-benposReport-setup', [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11], data);
                        }
                    }
                    else {
                        var table = $('#tbl-benposReport-setup').DataTable();
                        table.destroy();

                        initializeDataTable('tbl-benposReport-setup', [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11], data);
                    }

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
}

function fnValidate() {
    if ($("#bindBenposUploadedOn").val() == '' || $("#bindBenposUploadedOn").val() == undefined || $("#bindBenposUploadedOn").val() == null) {
        alert("Please select the benpos uploaded date");
        return false;
    }
    return true;
}

/* Formatting function for row details - modify as you need */
function format(d) {
    // `d` is the original data object for the row
    return '<table cellpadding="5" cellspacing="0" border="0" style="padding-left:50px;">' +
        '<tr>' +
            '<td>Full name:</td>' +
            '<td>' + d.name + '</td>' +
        '</tr>' +
        '<tr>' +
            '<td>Pan number:</td>' +
            '<td>' + d.pan + '</td>' +
        '</tr>' +
        '<tr>' +
            '<td>Folio number:</td>' +
            '<td>' + d.folioNumber + '</td>' +
        '</tr>' +
        '<tr>' +
            '<td>Extra info:</td>' +
            '<td>And any further details here (images etc)...</td>' +
        '</tr>' +
    '</table>';
}