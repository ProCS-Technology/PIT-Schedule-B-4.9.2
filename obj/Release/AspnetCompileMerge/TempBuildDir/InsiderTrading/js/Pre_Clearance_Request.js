var userRole = "";
var userCategory = "";
var lstPreclearanceRequest = null;
var arrDematAccount = null;
var arrForms = new Array();
var arrDetails = new Array();
var arrType = new Array();
var TradeFrom = "";
var TradeTo = "";
var BrokerDetailsManadatory = false;

$(document).ready(function () {

    fnGetDetailsOfUser();
    fnGetTransactionalInfo();
    const urlParams = new URLSearchParams(window.location.search);
    const myParam = urlParams.get('Status');

    if (myParam == "Draft") {
        $('#bindStatus').val(myParam).change();
        fnStatusChange();
    }
    else if (myParam == "InApproval") {
        $('#bindStatus').val(myParam).change();
        fnStatusChange();
    }
    else if (myParam == "Approved") {
        $('#bindStatus').val(myParam).change();
        fnStatusChange();
    }
    else if (myParam == "Rejected") {
        $('#bindStatus').val(myParam).change();
        fnStatusChange();
    }
    else {
        fnGetPreClearanceRequest();
    }
    fnGetTypeOfSecurity();
    fnGetTypeOfRestrictedCompanies();
    fnGetTypeOfTransaction();
    fnGetTradeExchange();
    fnGetRelativeDetail();

    var startDate = new Date();
    startDate.setDate(startDate.getDate());

    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = today.getFullYear();
    today = dd + '/' + mm + '/' + yyyy;

    //$('#txtRequestedTransactionDate').attr('readonly', 'readonly').val(today);

    //$('#txtRequestedTransactionDate').datepicker({
    //    todayHighlight: true,
    //    autoclose: true,
    //    format: "dd/mm/yyyy",
    //    clearBtn: true,
    //    startDate: startDate,
    //    daysOfWeekDisabled: [0, 6]
    //}).attr('readonly', 'readonly').val(today);
    $('.RequestedTransactionDate').datepicker({
        todayHighlight: true,
        autoclose: true,
        format: "dd/mm/yyyy",
        clearBtn: true,
        startDate: "today",
        endDate: "today",
        daysOfWeekDisabled: [0, 6],
        showOnFocus: false
    }).val(today);

    $("#txtActualdateoftransaction").datepicker({
        todayHighlight: true,
        autoclose: true,
        format: "dd/mm/yyyy",
        clearBtn: true,
        endDate: "today",
        daysOfWeekDisabled: [0, 6]
    });

    $("#ddlFor").on('change', function () {
        var SelVal = $(this).val();

        if (!(SelVal == "" || SelVal == null || SelVal == "Select")) {
            var currentValue = $(this).val();
            fnGetDematAccount(currentValue, null);
            var DematAccount = $("#ddlDematAccount").val();
            if (!(DematAccount == "" || DematAccount == null || DematAccount == undefined)) {
                fnGetDematCurrentHoldings(DematAccount);
            }
        }
    }).trigger('change');

    $("#ddlDematAccount").on('change', function () {
        var DematAccount = $(this).val();
        if (!(DematAccount == "" || DematAccount == null || DematAccount == undefined)) {
            fnGetDematCurrentHoldings(DematAccount);
        }
    }).trigger('change');

    $("#txtActualTradequantityBrokerNote").on('focusout', function () {
        if ($("#txtActualTradequantityBrokerNote").val() !== null && $("#txtActualTradequantityBrokerNote").val() !== undefined && $("#txtActualTradequantityBrokerNote").val() !== '' && $("#txtValuePerShare").val() !== null && $("#txtValuePerShare").val() !== undefined && $("#txtValuePerShare").val() !== '') {
            var totalAmount = $("#txtActualTradequantityBrokerNote").val() * $("#txtValuePerShare").val();
            $("#txtTotalamount").val(totalAmount);
        }
        else {
            if ($("#txtTradequantityBrokerNote").val() !== null && $("#txtTradequantityBrokerNote").val() !== undefined && $("#txtTradequantityBrokerNote").val() !== '' && $("#txtValuePerShare").val() !== null && $("#txtValuePerShare").val() !== undefined && $("#txtValuePerShare").val() !== '') {
                var totalAmount = $("#txtTradequantityBrokerNote").val() * $("#txtValuePerShare").val();
                $("#txtTotalamount").val(totalAmount);
            }
        }
    })
    $("#txtValuePerShare").on('focusout', function () {
        if ($("#txtActualTradequantityBrokerNote").val() !== null && $("#txtActualTradequantityBrokerNote").val() !== undefined && $("#txtActualTradequantityBrokerNote").val() !== '' && $("#txtValuePerShare").val() !== null && $("#txtValuePerShare").val() !== undefined && $("#txtValuePerShare").val() !== '') {
            var totalAmount = $("#txtActualTradequantityBrokerNote").val() * $("#txtValuePerShare").val();
            $("#txtTotalamount").val(totalAmount);
        }
        else {
            if ($("#txtTradequantityBrokerNote").val() !== null && $("#txtTradequantityBrokerNote").val() !== undefined && $("#txtTradequantityBrokerNote").val() !== '' && $("#txtValuePerShare").val() !== null && $("#txtValuePerShare").val() !== undefined && $("#txtValuePerShare").val() !== '') {
                var totalAmount = $("#txtTradequantityBrokerNote").val() * $("#txtValuePerShare").val();
                $("#txtTotalamount").val(totalAmount);
            }
        }
    })
    //fnGetPreClearanceRequest();
    //fnGetPreClearanceRequestOther();
    fnCalculateMultiTrade();
    // Add event listener for opening and closing details
    $('#tbl-preclearance-setup tbody').on('click', 'td.details-control', function () {
        var table = $('#tbl-preclearance-setup').DataTable();
        var tr = $(this).closest('tr');
        var row = table.row(tr);

        if (row.child.isShown()) {
            row.child.hide();
            tr.removeClass('shown');
        }
        else {
            row.child(format(row.data())).show();
            tr.addClass('shown');
        }
    });
    $('.filter-status').on('change', function () {

        var tablefilter = $('#tbl-preclearance-setup').DataTable();

        var FilterValue = $('input:radio[name="PreClearancestatus"]:checked').val();

        if (FilterValue == 'Relatives') {
            tablefilter.column(4).search('^((?!\\Self).)*$', true, false, false).draw(false);
        }
        else if (FilterValue == 'Self') {
            var PreClearancestatus = $('input:radio[name="PreClearancestatus"]:checked').map(function () {
                return '^' + this.value + '$';
            }).get().join('|');

            tablefilter.column(4).search(PreClearancestatus, true, false, false).draw(false);

        }
        else if (FilterValue == '') {
            tablefilter.column(4).search('', true, false, false).draw(false);
        }
        else {
            // tablefilter.column(4).search().draw(true);

        }

    }).trigger('change');

});

function format(d) {
    // `d` is the original data object for the row
    var preClearanceRequestId = d.DT_RowId.substr(3);
    var htmlString = '';

    if (lstPreclearanceRequest !== null) {
        if (lstPreclearanceRequest.length > 0) {
            for (var i = 0; i < lstPreclearanceRequest.length; i++) {
                if (lstPreclearanceRequest[i].PreClearanceRequestId == preClearanceRequestId) {
                    if (lstPreclearanceRequest[i].lstBrokerNoteUploaded !== null) {
                        if (lstPreclearanceRequest[i].lstBrokerNoteUploaded.length > 0) {
                            htmlString += '<table class="table table-striped" cellpadding="5" cellspacing="0" border="0" style="padding-left:50px;">' +
                                '<thead>' +
                                '<tr>' +
                                '<th></th>' +
                                '<th>Quantity</th>' +
                                '<th>Value Per Share</th>' +
                                '<th>Estimated Trade Value</th>' +
                                '<th>Actual Transaction Date</th>' +
                                '<th>Remarks</th>' +
                                '<th>Trade Details</th>' +

                                '</tr >' +
                                '</thead >' +
                                '<tbody>';
                            for (var j = 0; j < lstPreclearanceRequest[i].lstBrokerNoteUploaded.length; j++) {
                                htmlString += '<tr>';
                                //    '<td>' + lstPreclearanceRequest[i].lstBrokerNoteUploaded[j].ActualTradeQuantity + '</td>' +
                                //    '<td>' + lstPreclearanceRequest[i].lstBrokerNoteUploaded[j].ValuePerShare + '</td>' +
                                //    '<td>' + lstPreclearanceRequest[i].lstBrokerNoteUploaded[j].TotalAmount + '</td>' +
                                //    '<td>' + lstPreclearanceRequest[i].lstBrokerNoteUploaded[j].ActualTransactionDate + '</td>' +
                                //    '<td>' + lstPreclearanceRequest[i].lstBrokerNoteUploaded[j].remarks + '</td>';
                                //if (lstPreclearanceRequest[i].lstBrokerNoteUploaded[j].BrokerNote != "") {
                                //    htmlString += '<td><a target="_blank" href="TradingRequestDetails/' + lstPreclearanceRequest[i].lstBrokerNoteUploaded[j].BrokerNote + '"><i class="fa fa-download"></i></a></td>';
                                //}
                                //else {
                                //    htmlString += '<td></td>';
                                //}
                                htmlString += '<td>' +
                                    //    '<div class="btn-group" >' +
                                    //    '<a class="btn blue dropdown-toggle" href="javascript:;" data-toggle="dropdown">' +
                                    //    '<i class="fa fa-user"></i> Actions' +
                                    //    '<i class="fa fa-angle-down"></i>' +
                                    //    '</a>' +
                                    //    '<ul class="dropdown-menu">';
                                    //if (!lstPreclearanceRequest[i].lstBrokerNoteUploaded[j].isFormCDJCreated) {
                                    //    htmlString += '<li>' +
                                    //        '<a onclick="javascript:fnCreatFormsCDJ(' + preClearanceRequestId + ',' + lstPreclearanceRequest[i].lstBrokerNoteUploaded[j].brokerNoteId + ',' + lstPreclearanceRequest[i].lstBrokerNoteUploaded[j].isNullTrade + ');"><i class="fa fa-upload"></i>Upload Form</a>' +
                                    //        '</li>' +
                                    //        '<li class="divider"></li>';
                                    //}
                                    //else {
                                    //    htmlString += '<li>' +
                                    //        '<a target="_blank" onclick="javascript:fnDownloadFormsCDJ(' + preClearanceRequestId + ',' + lstPreclearanceRequest[i].lstBrokerNoteUploaded[j].brokerNoteId + ');"><i class="fa fa-download"></i>Download Form</a>' +
                                    //        '</li>' +
                                    //        '<li class="divider"></li>';
                                    //}
                                    //htmlString += '</ul>' +
                                    //    '</div>' +
                                    '</td>' +
                                    '<td>' + lstPreclearanceRequest[i].lstBrokerNoteUploaded[j].ActualTradeQuantity + '</td>' +
                                    '<td>' + lstPreclearanceRequest[i].lstBrokerNoteUploaded[j].ValuePerShare + '</td>' +
                                    '<td>' + lstPreclearanceRequest[i].lstBrokerNoteUploaded[j].TotalAmount + '</td>' +
                                    '<td>' + lstPreclearanceRequest[i].lstBrokerNoteUploaded[j].ActualTransactionDate + '</td>' +
                                    '<td>' + lstPreclearanceRequest[i].lstBrokerNoteUploaded[j].remarks + '</td>' +
                                    '<td>';
                                if (lstPreclearanceRequest[i].lstBrokerNoteUploaded[j].BrokerNote != '') {
                                    htmlString += '<a target="_blank" href="TradingRequestDetails/' + lstPreclearanceRequest[i].lstBrokerNoteUploaded[j].BrokerNote + '"><i class="fa fa-download"></i></a>';
                                }
                                else {
                                    htmlString += 'NA';
                                }
                                htmlString += '</td>' +
                                    '</tr>';
                            }
                            htmlString += '</tbody>' +
                                '</table>';
                        }
                    }
                }
            }
        }
    }
    return htmlString;
}
function isJson(str) {
    try {
        JSON.parse(str);
    } catch (e) {
        return false;
    }
    return true;
}
function initializeDataTable() {
    var table = $('#tbl-preclearance-setup').DataTable({
        //  "dom": '<"top"iflp<"clear">>rt<"bottom"iflp<"clear">>',
        dom: 'Bfrtip',
        pageLength: 10,
        "scrollY": "300px",
        "scrollX": true,
        buttons: [
            {
                extend: 'pdf',
                className: 'btn green btn-outline',
                exportOptions: {
                    columns: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14]
                }
            },
            {
                extend: 'excel',
                className: 'btn yellow btn-outline ',
                exportOptions: {
                    columns: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14]
                }
            },
            //    { extend: 'colvis', className: 'btn dark btn-outline', text: 'Columns' }
        ],
        "columns": [
            {
                "className": 'details-control',
                "orderable": false,
                "data": null,
                "defaultContent": ''
            },
            { "data": "" },
            { "data": "Id" },
            { "data": "for" },
            { "data": "Relation" },
            //{ "data": "company" },
            { "data": "quantity" },
            { "data": "type" },
            { "data": "tradeExchange" },
            { "data": "demat" },
            { "data": "requested" },
            { "data": "status" },
            //  { "data": "brokerNoteUploaded" },
            { "data": "reviewedOn" },
            { "data": "reviewedBy" },
            { "data": "remarks" },

        ],
        "order": [[2, 'desc']]
    });
    table.columns.adjust().draw();
}
function initializeDataTable1() {
    $('#tbl-preclearance-other-setup').DataTable({
        dom: 'lBfrtip',
        pageLength: 10,
        buttons: [
            {
                extend: 'pdf',
                className: 'btn green btn-outline',
                exportOptions: {
                    columns: [1, 2, 3, 4, 5, 6, 7, 8, 9]
                }
            },
            {
                extend: 'excel',
                className: 'btn yellow btn-outline ',
                exportOptions: {
                    columns: [1, 2, 3, 4, 5, 6, 7, 8, 9]
                }
            },
        ],

    });
}
function fnAddPreClearance() {
    //alert("Please review your trade transactions done in the current quarter before proceeding to add pre-clearance request");
    $("#btnSubmitTD").text("Skip");
    var fl = false;// confirm("Please click 'OK' to confirm you have already reported all the trades done in the quarter OR click 'Cancel' to report non reported trades");
    if (fl) {
        $("#btnSubmitPreClearanceRequest").show();
        $("#btnCancelPreClearanceRequest").hide();
        $("#ddlFor").prop("disabled", false);
        $("#ddlTypeOfSecurity").prop("disabled", false);
        $("#ddlRestrictedCompanies").prop("disabled", false);
        $("#txtTradeQuantity").prop("readonly", false);
        $("#ddlTypeOfTransaction").prop("disabled", false);
        $("#ddlTradeExchange").prop("disabled", false);
        $("#ddlDematAccount").prop("disabled", false);
        $("#txtRequestedTransactionDate").prop("disabled", false);
        $("#txtShareCurrentMarketPrice").prop("readonly", false);
        $("#ddlProposedTransactionThrough").prop("disabled", false);
        $("#stack1").modal('show');

        $("#btnSubmitPreClearanceRequest").show();
        //$("#btnSaveAsDraftPreClearanceRequest").show();
        $("#btnCancelPreClearanceRequest").hide();

        $("#ddlFor").prop("disabled", false);
        $("#ddlTypeOfSecurity").prop("disabled", false);
        $("#ddlRestrictedCompanies").prop("disabled", false);
        $("#txtTradeQuantity").prop("readonly", false);
        $("#ddlTypeOfTransaction").prop("disabled", false);
        $("#ddlTradeExchange").prop("disabled", false);
        $("#ddlDematAccount").prop("disabled", false);
        $("#txtRequestedTransactionDate").prop("disabled", false);
        $("#txtShareCurrentMarketPrice").prop("readonly", false);
        $("#ddlProposedTransactionThrough").prop("disabled", false);
    }
    else {
        fnGetTradeTransactions();
        fnAddRow();
        $("#mdlTradeDetails").modal('show');
    }
}
function fnGetTradeTransactions() {
    $("#Loader").show();
    var webUrl = uri + "/api/PreClearanceRequest/GetTradeTransactions";
    $.ajax({
        url: webUrl,
        type: "GET",
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            if (msg.StatusFl == true) {
                $("#Loader").hide();
                var str = "";
                var strSelf = "";
                var strRelative = "";
                var RowCount = 0;
                var RowCountRelative = 0;
                for (var i = 0; i < msg.TradeTransactionList.length; i++) {

                    if (msg.TradeTransactionList[i].AsPer =="ESOP") {
                        msg.TradeTransactionList[i].TransType = msg.TradeTransactionList[i].TransType + ' (ESOP)';
                    }
                    if (msg.TradeTransactionList[i].RelationNm == "Self") {
                        RowCount = parseInt(RowCount) + 1;
                        strSelf += "<tr>";
                        strSelf += "<td>" + RowCount + "</td>";
                        strSelf += "<td>" + msg.TradeTransactionList[i].RelativeNm + "</td>";
                        strSelf += "<td>" + msg.TradeTransactionList[i].RelationNm + "</td>";
                        strSelf += "<td>" + msg.TradeTransactionList[i].TransDt + "</td>";
                        strSelf += "<td>" + msg.TradeTransactionList[i].TransType + "</td>";
                        //strSelf += "<td>" + msg.TradeTransactionList[i].Pan + "</td>";
                        //strSelf += "<td>" + msg.TradeTransactionList[i].Demat + "</td>";
                        strSelf += "<td style='text-align:right;'>" + msg.TradeTransactionList[i].TransQty + "</td>";
                        strSelf += "<td style='text-align:right;'>" + msg.TradeTransactionList[i].TransVal + "</td>";
                        strSelf += "</tr>";
                    }
                    else {
                        RowCountRelative += parseInt(RowCountRelative) + 1;
                        strRelative += "<tr>";
                        strRelative += "<td>" + RowCountRelative + "</td>";
                        strRelative += "<td>" + msg.TradeTransactionList[i].RelativeNm + "</td>";
                        strRelative += "<td>" + msg.TradeTransactionList[i].RelationNm + "</td>";
                        strRelative += "<td>" + msg.TradeTransactionList[i].TransDt + "</td>";
                        strRelative += "<td>" + msg.TradeTransactionList[i].TransType + "</td>";
                        //strRelative += "<td>" + msg.TradeTransactionList[i].Pan + "</td>";
                        //strRelative += "<td>" + msg.TradeTransactionList[i].Demat + "</td>";
                        strRelative += "<td style='text-align:right;'>" + msg.TradeTransactionList[i].TransQty + "</td>";
                        strRelative += "<td style='text-align:right;'>" + msg.TradeTransactionList[i].TransVal + "</td>";
                        strRelative += "</tr>";
                    }
                }

                str += '<tr><td colspan="7" style="background:#fafafa">Self Transactions</td></tr>';

                if (RowCount == 0) {
                    str += '<tr><td colspan="7">No Recent Transactions</td></tr>';
                }
                else {
                    str += strSelf;
                }

                str += '<tr><td colspan="7" style="background:#fafafa">Relative Transactions</td></tr>';

                if (RowCountRelative == 0) {
                    str += '<tr><td colspan="7">No Recent Transactions</td></tr>';
                }
                else {
                    str += strRelative;
                }


                $("#tbdTransactions").html("");
                $("#tbdTransactions").append(str);
            }
            else {
                $("#Loader").hide();
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    $("#tblTrades").hide();
                }
            }
        },
        error: function (response) {
            $("#Loader").hide();
            alert(response.status + ' ' + response.statusText);
        }
    })
}

function fnCloseModal() {
    fnClearForm();
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
        success: function (msg) {
            if (msg.StatusFl == true) {
                $("#Loader").hide();

                var options = document.getElementById("ddlForms").options;

                if (options !== null && options !== undefined && options.length > 0) {
                    $.each(options, function (index, item) {
                        if (msg.User.category.ID == item.value) {
                            userCategory = item.text;
                        }
                    })
                }
            }
            else {
                $("#Loader").hide();
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    // alert(msg.Msg);
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
function fnCreatFormsCDJ(preClearanceRequestId, brokerNoteId, isNullTrade) {
    $("#preClearanceRequestIdBN").val(preClearanceRequestId);
    $("input[id*='txtBrokerNoteId']").val(brokerNoteId);
    $("#Loader").show();
    var webUrl = uri + "/api/PreClearanceRequest/GetFormsInfo";
    $.ajax({
        url: webUrl,
        type: "POST",
        data: JSON.stringify({
            PreClearanceRequestId: preClearanceRequestId, brokerNoteId: brokerNoteId
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            if (msg.StatusFl == true) {
                $("#Loader").hide();
                //alert("In success");
                if (msg.PreClearanceRequest.lstFormUrl != null) {
                    $("select[id*='ddlForms']").empty();
                    var strOption = "";
                    var strHtml = "";
                    arrForms = new Array();
                    for (var x = 0; x < msg.PreClearanceRequest.lstFormUrl.length; x++) {
                        var strValue = msg.PreClearanceRequest.lstFormUrl[x];
                        strOption += strValue.split("~")[1] + " & ";
                        arrForms.push(strValue.split("~")[0]);
                        strHtml += '<div id="divForm_' + strValue.split("~")[0] + '">';
                        strHtml += '<div class="col-md-4" id="lblUploadForm_' + strValue.split("~")[0] + '">' + strValue.split("~")[1] + '</div>';
                        strHtml += '<div class="col-md-8">';
                        strHtml += '<input type="file" id="txtUploadForm_' + strValue.split("~")[0] + '" class="form-control" data-tabindex="4" />';
                        strHtml += '</div>';
                        strHtml += '</div><br />';
                    }
                    var sHtml = strHtml.substr(0, strHtml.length - 6);
                    $("#divUploadForm").html(sHtml);

                    var s = strOption.substr(0, strOption.length - 3);
                    ddlForms.append(new Option(s, "All"));

                    if (msg.PreClearanceRequest.isNUllTrade) {
                        fnDisplayNote(null, null, "Non Trade");
                    }
                    else {
                        fnDisplayNote(null, null, "Trade");
                    }
                }
                //$("select[id*='ddlForms']").find('option[value=FORM_J]').remove();
                //if (userCategory.toUpperCase() == "PROMOTER") {
                //    $("select[id*='ddlForms']").find('option[value=FORM_CJ]').remove();
                //    $("#divForm").show();
                //    $("#divAnnexure").show();
                //    $("select[id*='ddlForms']").find('option[value=FORM_DJ]').prop("selected", true);
                //    fnDisplayNote(null, null, "FORM_DJ");
                //}
                //else {
                //    $("select[id*='ddlForms']").find('option[value=FORM_DJ]').remove();
                //    $("#divForm").show();
                //    $("#divAnnexure").show();
                //    $("select[id*='ddlForms']").find('option[value=FORM_CJ]').prop("selected", true);
                //    fnDisplayNote(null, null, "FORM_CJ");
                //}
                $("#btnCancelBrokerNote").trigger("click");
                $("#btnOpenForm").trigger("click");
                //$("#btnOpenForm").attr('onclick', 'javascript:fnShowForm(\'' + msg.PreClearanceRequest.brokerNoteId + '\');');
                //$("#btnOpenForm").attr("data-target", "#modalForms");
                //$("#btnOpenForm").attr("data-toggle", "modal");
                //window.location.reload(); 
                //   $("#tdIsBrokerNoteUploaded_" + msg.PreClearanceRequest.PreClearanceRequestId).html('Y');
                $("#liUploadbrokernote_" + msg.PreClearanceRequest.PreClearanceRequestId).hide();
            }
            else {
                $("#Loader").hide();
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    // alert(msg.Msg);
                    return false;
                }
            }
        },
        error: function (response) {
            $("#Loader").hide();
            alert(response.status + ' ' + response.statusText);
        }
    })

    /*$("select[id*='ddlForms']").find('option[value=FORM_CJ]').remove();
    $("select[id*='ddlForms']").find('option[value=FORM_DJ]').remove();
    $("select[id*='ddlForms']").find('option[value=FORM_J]').remove();
    $("select[id*='ddlForms']").find('option[value=0]').remove();
    if (isNullTrade == true) {
        $("select[id*='ddlForms']").append(new Option("FORM J", "FORM_J"));
        //$("select[id*='ddlForms']").find('option[value=FORM_CJ]').remove();
        //$("select[id*='ddlForms']").find('option[value=FORM_DJ]').remove();
        fnDisplayNote(null, null, "FORM_J");
    }
    else {
        //$("select[id*='ddlForms']").find('option[value=FORM_J]').remove();
        if (userCategory.toUpperCase() == "PROMOTER") {
            //$("select[id*='ddlForms']").find('option[value=FORM_CJ]').remove();
            $("select[id*='ddlForms']").append(new Option("Form C & Disclosure of Pre-cleared Transactions", "FORM_DJ"));
            $("#divForm").show();
            $("#divAnnexure").show();
            $("select[id*='ddlForms']").find('option[value=FORM_DJ]').prop("selected", true);
            fnDisplayNote(null, null, "FORM_DJ");
        }
        else {
            //$("select[id*='ddlForms']").find('option[value=FORM_DJ]').remove();
            $("select[id*='ddlForms']").append(new Option("Form C & Disclosure of Pre-cleared Transactions", "FORM_CJ"));
            $("#divForm").show();
            $("#divAnnexure").show();
            $("select[id*='ddlForms']").find('option[value=FORM_CJ]').prop("selected", true);
            fnDisplayNote(null, null, "FORM_CJ");
        }
    }
    $("#btnCancelBrokerNote").trigger("click");
    $("#btnOpenForm").trigger("click");*/
}
function fnDownloadFormsCDJ(preClearanceRequestId, brokerNoteId) {
    $("#Loader").show();
    var webUrl = uri + "/api/PreClearanceRequest/GetFormsCDJ";
    $.ajax({
        url: webUrl,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        data: JSON.stringify({ PreClearanceRequestId: preClearanceRequestId, brokerNoteId: brokerNoteId }),
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                var formurl = 'emailAttachment/' + msg.PreClearanceRequest.formUrl;
                window.open(formurl, "_blank");
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
            alert(response.status + ' ' + response.statusText);
        }
    })
}
function fnGetTypeOfSecurity() {
    $("#Loader").show();
    var webUrl = uri + "/api/PreClearanceRequest/GetTypeOfSecurityList";
    $.ajax({
        url: webUrl,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        data: {},
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                /*                 
                 var result = "";
                result += "<option value='0'>--Select--</option>";
                for (var i = 0; i < msg.SecurityTypeList.length; i++) {
                    //if (msg.SecurityTypeList[i].Name == "Equity") {
                    //    result += "<option value='" + msg.SecurityTypeList[i].Id + "'>" + msg.SecurityTypeList[i].Name + "</option>";
                    //}
                    result += "<option value='" + msg.SecurityTypeList[i].Id + "'>" + msg.SecurityTypeList[i].Name + "</option>";
                }
                $("#ddlTypeOfSecurity").append(result);
                 * */

                var result = "";
                var result2 = "";
                result += "<option value='0'>--Select--</option>";
                //result2 += "<option value='0'>--Select--</option>";
                for (var i = 0; i < msg.SecurityTypeList.length; i++) {
                    if (msg.SecurityTypeList[i].Name == "Equity") {
                        result2 += "<option value='" + msg.SecurityTypeList[i].Id + "'>" + msg.SecurityTypeList[i].Name + "</option>";
                    }
                    result += "<option value='" + msg.SecurityTypeList[i].Id + "'>" + msg.SecurityTypeList[i].Name + "</option>";
                }
                $("#ddlTypeOfSecurity").append(result2);
                $("#ddlTypeOfSecurityOther").append(result2);
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
            alert(response.status + ' ' + response.statusText);
        }
    })
}
function fnGetTypeOfRestrictedCompanies() {
    $("#Loader").show();
    var webUrl = uri + "/api/PreClearanceRequest/GetTypeOfRestrictedCompaniesList";
    $.ajax({
        url: webUrl,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        data: {},
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            //if (msg.StatusFl == true) {
            //    var result = "";
            //    result += "<option value='0'>--Select--</option>";
            //    for (var i = 0; i < msg.RestrictedCompaniesList.length; i++) {
            //        result += "<option value='" + msg.RestrictedCompaniesList[i].ID + "'>" + msg.RestrictedCompaniesList[i].companyName + "</option>";
            //    }
            //    $("#ddlRestrictedCompanies").append(result);
            //    $("#ddlRestrictedCompaniesBN").append(result);
            //}
            if (msg.StatusFl == true) {
                var resultHomeCompany = "";
                var resultOtherCompany = "";
                var result = "";
                result += "<option value='0'>--Select--</option>";
                for (var i = 0; i < msg.RestrictedCompaniesList.length; i++) {
                    if (msg.RestrictedCompaniesList[i].IsHomeCompany == 0) {
                        resultOtherCompany += "<option value='" + msg.RestrictedCompaniesList[i].ID + "'>" + msg.RestrictedCompaniesList[i].companyName + "</option>";
                    }
                    else if (msg.RestrictedCompaniesList[i].IsHomeCompany == 1) {
                        resultHomeCompany += "<option value='" + msg.RestrictedCompaniesList[i].ID + "'>" + msg.RestrictedCompaniesList[i].companyName + "</option>";
                    }

                }
                //alert(resultOtherCompany); alert(resultHomeCompany); alert(result);
                $("#ddlRestrictedCompanies").append(resultHomeCompany);
                $("#ddlRestrictedCompaniesBN").append(resultHomeCompany);
                $("#ddlRestrictedCompaniesOther").append(resultOtherCompany);
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
            alert(response.status + ' ' + response.statusText);
        }
    })
}
function fnGetTypeOfTransaction() {
    $("#Loader").show();
    var webUrl = uri + "/api/PreClearanceRequest/GetTypeOfTransactionList";
    $.ajax({
        url: webUrl,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        data: {},
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                var result = "";
                arrType = new Array();

                if (msg.TransactionTypeList.length > 1) {
                    result += "<option value='0'>--Select--</option>";
                }
                for (var i = 0; i < msg.TransactionTypeList.length; i++) {
                    result += "<option value='" + msg.TransactionTypeList[i].Id + "'>" + msg.TransactionTypeList[i].Name + "</option>";
                    var obj = new Object();
                    obj.Id = msg.TransactionTypeList[i].Id;
                    obj.Name = msg.TransactionTypeList[i].Name;
                    arrType.push(obj);
                }
                $("#ddlTypeOfTransaction").append(result);
                $("#ddlTypeOfTransactionBN").append(result);
                $("#ddlTypeOfTransactionOther").append(result);
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
            alert(response.status + ' ' + response.statusText);
        }
    })
}
function fnGetTradeExchange() {
    $("#Loader").show();
    var webUrl = uri + "/api/PreClearanceRequest/GetTradeExchangeList";
    $.ajax({
        url: webUrl,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        data: {},
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                var result = "";
                result += "<option value='0'>--Select--</option>";
                for (var i = 0; i < msg.TradeExchangeList.length; i++) {
                    result += "<option value='" + msg.TradeExchangeList[i].Id + "'>" + msg.TradeExchangeList[i].Name + "</option>";
                }
                $("#ddlTradeExchange").append(result);
            }
            else {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    // alert(msg.Msg);
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
function fnGetDematAccount(currentValue, source) {
    $("#Loader").show();
    var webUrl = uri + "/api/PreClearanceRequest/GetDematAccountList";
    $.ajax({
        url: webUrl,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        data: JSON.stringify({
            // DematType: currentValue
            relativeId: currentValue
        }),
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                var result = "";
                arrDematAccount = new Array();

                if (msg.DematDetailList.length > 1) {
                    result += "<option value='0'>--Select--</option>";
                }

                for (var i = 0; i < msg.DematDetailList.length; i++) {
                    var obj = new Object();
                    obj.accountNo = msg.DematDetailList[i].accountNo;
                    obj.CurrentHolding = msg.DematDetailList[i].CurrentHolding;
                    arrDematAccount.push(obj);
                    result += "<option value='" + msg.DematDetailList[i].accountNo + "'>" + msg.DematDetailList[i].accountNo + "</option>";
                }
                if (source == "fnEditBrokerNote") {
                    $("#ddlDematAccountBrokerNote").html(result);
                }
                else {
                    $("#ddlDematAccount").html(result);
                }
            }
            else {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    $("#ddlDematAccount").html('');
                    $("#spncurrentholding").html('0');
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
function fnGetRelativeDetail() {
    $("#Loader").show();
    var webUrl = uri + "/api/PreClearanceRequest/GetRelativeDetailList";
    $.ajax({
        url: webUrl,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        data: {},
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                var result = "";

                //result += "<option value='0'>Self</option>";
                if (msg.RelativeDetailList.length > 1) {
                    result += "<option value='Select'>--Select--</option>";
                }
                for (var i = 0; i < msg.RelativeDetailList.length; i++) {
                    result += "<option value='" + msg.RelativeDetailList[i].ID + "'>" + msg.RelativeDetailList[i].relativeName + "</option>";
                }

                $("#ddlFor").append(result);
                $("#ddlForBN").append(result);
                $("#ddlForOther").append(result);
            }
            else {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
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
function fnGetPreClearanceRequest() {
    $("#Loader").show();
    var webUrl = uri + "/api/PreClearanceRequest/GetPreClearanceRequestList";
    $.ajax({
        url: webUrl,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        data: {},
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                var result = "";
                lstPreclearanceRequest = msg.PreClearanceRequestList;
                for (var i = 0; i < msg.PreClearanceRequestList.length; i++) {
                    userRole = msg.PreClearanceRequestList[i].userRole;
                    var ReqId = (msg.PreClearanceRequestList[i].PreClearanceRequestId + "").padStart(4, "0");
                    result += '<tr id="tr_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">';
                    if (msg.PreClearanceRequestList[i].PreClearanceRequestedForName == "") {
                        msg.PreClearanceRequestList[i].PreClearanceRequestedForName = "Self";
                    }
                    result += '<td></td>';
                    result += '<td id="tdActions_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">';
                    var innerStr = "";
                    innerStr += '<div class="btn-group">';
                    innerStr += '<a class="btn blue dropdown-toggle" href="javascript:;" data-toggle="dropdown">';
                    innerStr += '<i class="fa fa-user"></i> Actions';
                    innerStr += '<i class="fa fa-angle-down"></i>';
                    innerStr += '</a>';
                    innerStr += '<ul class="dropdown-menu">';
                    if (msg.PreClearanceRequestList[i].Status != "Approved" && msg.PreClearanceRequestList[i].Status != "Rejected") {
                        innerStr += '<li>';
                        innerStr += '<a  id="btnEdit_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '" data-toggle="modal" data-target="#stack1" onclick=\"javascript:fnEditPreClearanceRequest(' + msg.PreClearanceRequestList[i].PreClearanceRequestId + ',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedForName + '\',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequestList[i].SecurityTypeName + '\',\'' + msg.PreClearanceRequestList[i].SecurityType + '\',\'' + msg.PreClearanceRequestList[i].TradeCompanyName + '\',\'' + msg.PreClearanceRequestList[i].TradeCompany + '\',\'' + msg.PreClearanceRequestList[i].TradeQuantity + '\',\'' + msg.PreClearanceRequestList[i].TransactionTypeName + '\',\'' + msg.PreClearanceRequestList[i].TransactionType + '\',\'' + msg.PreClearanceRequestList[i].TradeExchangeName + '\',\'' + msg.PreClearanceRequestList[i].TradeExchange + '\',\'' + msg.PreClearanceRequestList[i].DematAccount + '\',\'' + msg.PreClearanceRequestList[i].TradeDate + '\',\'' + msg.PreClearanceRequestList[i].Status + '\',\'' + msg.PreClearanceRequestList[i].shareCurrentMarketPrice + '\',\'' + msg.PreClearanceRequestList[i].proposedTransactionThrough + '\');\">';
                        innerStr += '<i class="fa fa-pencil"></i> View/Withdraw Request </a>';
                        innerStr += '</li>';
                    }
                    else {
                        innerStr += '<li style="display:none">';
                        innerStr += '<a  id="btnEdit_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '" data-toggle="modal" data-target="#stack1" onclick=\"javascript:fnEditPreClearanceRequest(' + msg.PreClearanceRequestList[i].PreClearanceRequestId + ',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedForName + '\',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequestList[i].SecurityTypeName + '\',\'' + msg.PreClearanceRequestList[i].SecurityType + '\',\'' + msg.PreClearanceRequestList[i].TradeCompanyName + '\',\'' + msg.PreClearanceRequestList[i].TradeCompany + '\',\'' + msg.PreClearanceRequestList[i].TradeQuantity + '\',\'' + msg.PreClearanceRequestList[i].TransactionTypeName + '\',\'' + msg.PreClearanceRequestList[i].TransactionType + '\',\'' + msg.PreClearanceRequestList[i].TradeExchangeName + '\',\'' + msg.PreClearanceRequestList[i].TradeExchange + '\',\'' + msg.PreClearanceRequestList[i].DematAccount + '\',\'' + msg.PreClearanceRequestList[i].TradeDate + '\',\'' + msg.PreClearanceRequestList[i].Status + '\',\'' + msg.PreClearanceRequestList[i].shareCurrentMarketPrice + '\',\'' + msg.PreClearanceRequestList[i].proposedTransactionThrough + '\');\">';
                        innerStr += '<i class="fa fa-pencil"></i> View/Withdraw Request </a>';
                        innerStr += '</li>';
                    }

                    //innerStr += '<li>';
                    //innerStr += '<a  id="btnCancelrequest_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '" onclick=\"javascript:fnEditPreClearanceRequest(' + msg.PreClearanceRequestList[i].PreClearanceRequestId + ',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedForName + '\',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequestList[i].SecurityTypeName + '\',\'' + msg.PreClearanceRequestList[i].SecurityType + '\',\'' + msg.PreClearanceRequestList[i].TradeCompanyName + '\',\'' + msg.PreClearanceRequestList[i].TradeCompany + '\',\'' + msg.PreClearanceRequestList[i].TradeQuantity + '\',\'' + msg.PreClearanceRequestList[i].TransactionTypeName + '\',\'' + msg.PreClearanceRequestList[i].TransactionType + '\',\'' + msg.PreClearanceRequestList[i].TradeExchangeName + '\',\'' + msg.PreClearanceRequestList[i].TradeExchange + '\',\'' + msg.PreClearanceRequestList[i].DematAccount + '\',\'' + msg.PreClearanceRequestList[i].TradeDate + '\',\'' + msg.PreClearanceRequestList[i].Status + '\');\">';
                    //innerStr += '<i class="fa fa-times"></i> Cancel Request </a>';
                    //innerStr += '</li>';
                    //msg.PreClearanceRequestList[i].Status == "InApproval" || 
                    if (msg.PreClearanceRequestList[i].Status == "Approved" && msg.PreClearanceRequestList[i].TradeDate.trim() != '' && msg.PreClearanceRequestList[i].TradeExecutedStatus != "Closed") {
                        innerStr += '<li id="liUploadbrokernote_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">';
                        innerStr += '<a  id="btnUploadbrokernote_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '" data-toggle="modal" data-target="#basic1" onclick=\"javascript:fnEditBrokerNote(' + msg.PreClearanceRequestList[i].PreClearanceRequestId + ',\'' + msg.PreClearanceRequestList[i].BrokerNote + '\',\'' + (msg.PreClearanceRequestList[i].ActualTransactionDate !== null ? msg.PreClearanceRequestList[i].ActualTransactionDate : msg.PreClearanceRequestList[i].ActualTransactionDate) + '\',\'' + msg.PreClearanceRequestList[i].ValuePerShare + '\',\'' + msg.PreClearanceRequestList[i].TotalAmount + '\',\'' + msg.PreClearanceRequestList[i].TradeQuantity + '\',\'' + msg.PreClearanceRequestList[i].ActualTradeQuantity + '\',\'' + msg.PreClearanceRequestList[i].Status + '\',\'' + msg.PreClearanceRequestList[i].TradeExchange + '\',\'' + msg.PreClearanceRequestList[i].DematAccount + '\',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequestList[i].TradeCompany + '\',\'' + msg.PreClearanceRequestList[i].TransactionType + '\',\'' + msg.PreClearanceRequestList[i].TradeDate + '\',\'' + msg.PreClearanceRequestList[i].remarks + '\',\'' + msg.PreClearanceRequestList[i].proposedTransactionThrough + '\',\'' + msg.PreClearanceRequestList[i].tradingFrom + '\',\'' + msg.PreClearanceRequestList[i].tradingTo + '\',\'' + msg.PreClearanceRequestList[i].RemainingTradeQuantity + '\');\">';
                        innerStr += '<i class="fa fa-upload"></i>Upload Trade Details </a>';
                        innerStr += '</li>';
                    }
                    else {
                        innerStr += '<li id="liUploadbrokernote_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '" style="display:none">';
                        innerStr += '<a  id="btnUploadbrokernote_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '" data-toggle="modal" data-target="#basic1" onclick=\"javascript:fnEditBrokerNote(' + msg.PreClearanceRequestList[i].PreClearanceRequestId + ',\'' + msg.PreClearanceRequestList[i].BrokerNote + '\',\'' + (msg.PreClearanceRequestList[i].ActualTransactionDate !== null ? msg.PreClearanceRequestList[i].ActualTransactionDate : msg.PreClearanceRequestList[i].ActualTransactionDate) + '\',\'' + msg.PreClearanceRequestList[i].ValuePerShare + '\',\'' + msg.PreClearanceRequestList[i].TotalAmount + '\',\'' + msg.PreClearanceRequestList[i].TradeQuantity + '\',\'' + msg.PreClearanceRequestList[i].ActualTradeQuantity + '\',\'' + msg.PreClearanceRequestList[i].Status + '\',\'' + msg.PreClearanceRequestList[i].TradeExchange + '\',\'' + msg.PreClearanceRequestList[i].DematAccount + '\',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequestList[i].TradeCompany + '\',\'' + msg.PreClearanceRequestList[i].TransactionType + '\',\'' + msg.PreClearanceRequestList[i].TradeDate + '\',\'' + msg.PreClearanceRequestList[i].remarks + '\',\'' + msg.PreClearanceRequestList[i].proposedTransactionThrough + '\',\'' + msg.PreClearanceRequestList[i].tradingFrom + '\',\'' + msg.PreClearanceRequestList[i].tradingTo + '\',\'' + msg.PreClearanceRequestList[i].RemainingTradeQuantity + '\');\">';
                        innerStr += '<i class="fa fa-upload"></i>Upload Trade Details </a>';
                        innerStr += '</li>';
                    }

                    innerStr += '<li id="liZippedTradeFilesDownload_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">';
                    innerStr += '<a href="javascript:fnDownloadTradingZipFile(\'' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '\');"  id="btnZippedTradeFilesDownload_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">';
                    innerStr += '<i class="fa fa-download"></i>Download Trade Files</a>';
                    innerStr += '</li>';
                    innerStr += '<li class="divider"> </li>';
                    innerStr += '</ul>';
                    innerStr += '</div>';
                    //if (msg.PreClearanceRequestList[i].Status == "Approved" || msg.PreClearanceRequestList[i].Status == "Rejected") {
                    //    result += '</td>';
                    //}
                    //else {
                    //    result += innerStr + '</td>';
                    //}
                    result += innerStr + '</td>';
                    result += '<td id="tdPreClearanceRequestId_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + ReqId + '</td>';
                    result += '<td id="tdPreClearanceRequestFor_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].PreClearanceRequestedForName + '</td>';
                    result += '<td id="tdRelationName_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].relationName + '</td>';
                    result += '<td id="tdTradeQuantity_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].TradeQuantity + '</td>';
                    result += '<td id="tdTypeOfTransaction_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].TransactionTypeName + '</td>';
                    result += '<td style="display:none;" id="tdTradeExchange_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].TradeExchangeName + '</td>';
                    result += '<td style="display: none;" id="tdDematAccount_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].DematAccount + '</td>';
                    result += '<td id="tdRequestedTransactionDate_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].TradeDate + '</td>';
                    if (msg.PreClearanceRequestList[i].Status == "InApproval") {
                        msg.PreClearanceRequestList[i].Status = "Pending for  Approval";
                        result += '<td style="background-color:orange" id="tdPreClearanceRequestStatus_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].Status + '</td>';
                    }
                    else if (msg.PreClearanceRequestList[i].Status == "Approved") {
                        if (msg.PreClearanceRequestList[i].TradeDate == "") {
                            result += '<td style="background-color:red" id="tdPreClearanceRequestStatus_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">Pre-clearance Not Taken</td>';
                        }
                        else {
                            result += '<td style="background-color:green" id="tdPreClearanceRequestStatus_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].Status + '</td>';
                        }
                    }
                    else if (msg.PreClearanceRequestList[i].Status == "Draft") {
                        result += '<td id="tdPreClearanceRequestStatus_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].Status + '</td>';
                    }
                    else {
                        result += '<td style="background-color:red" id="tdPreClearanceRequestStatus_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].Status + '</td>';
                    }
                    result += '<td>' + msg.PreClearanceRequestList[i].reviewedOn + '</td>';
                    result += '<td>' + msg.PreClearanceRequestList[i].reviewedBy + '</td>';
                    result += '<td>' + msg.PreClearanceRequestList[i].reviewerRemarks + '</td>';

                    result += '</tr>';
                }

                var table = $('#tbl-preclearance-setup').DataTable();
                table.destroy();
                $("#tbdPreClearanceList").html(result);
                initializeDataTable();
            }
            else {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else if (msg.Msg.toLowerCase() == "no data found !") {

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
function fnDownloadTradingZipFile(PreClearanceRequestId) {
    var webUrl = uri + "/api/PreClearanceRequest/DownloadTradingZipFile";

    var ajax = new XMLHttpRequest();
    ajax.open("Post", webUrl, true);
    ajax.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    ajax.responseType = "blob";
    ajax.onreadystatechange = function () {
        if (this.readyState == 4) {
            var blob = new Blob([this.response], { type: "application/octet-stream" });
            var fileName = "TradingReport.zip";
            saveAs(blob, fileName);

        }
    };

    ajax.send(JSON.stringify({
        PreClearanceRequestId: PreClearanceRequestId
    }));
}
function fnSubmitPreClearanceRequest(status) {
    if (fnValidate()) {
        if (status == "Cancel") {
            $("#modalWithdrawRequest").modal('show');
        }
        else if (status == "InApproval") {
            if ($("input[id*=enableUndertakingBeforeRequest]").val() == "true") {
                fnGetUndertakingTemplateBeforeSubmitPreclearanceRequest();
            }
            else {
                $("#modalSubmitRequest").modal('show');
            }
        }
        else if (status == "Draft") {
            $("#modalSaveAsDraftRequest").modal('show');
        }
        else {
            fnSubmitRequest(status);
        }
    }
}
function fnRequestAction(status) {
    fnSubmitRequest(status);
}
function fnGetUndertakingTemplateBeforeSubmitPreclearanceRequest() {
    $("#Loader").show();
    var tradeQuantity = $('#txtTradeQuantity').val();
    var webUrl = uri + "/api/PreClearanceRequest/GetUndertakingTemplate";
    $.ajax({
        url: webUrl,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        // async: false,
        data: JSON.stringify({
            TradeQuantity: tradeQuantity
        }),
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                if (msg.PreClearanceRequest !== null) {
                    $("#UndertakingPreclearanceRequest").html(msg.PreClearanceRequest.underTakingText);
                    $("#modalUnterkaing").modal('show');
                }

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
            alert(response.status + ' ' + response.statusText);
        }
    })
}
function fnSubmitRequest(status) {
    $("#Loader").show();
    var preClearanceRequestId = $("#preClearanceRequestId").val() == '' ? 0 : $("#preClearanceRequestId").val();
    var preClearanceRequestFor = $('#ddlFor').val();
    var preClearanceRequestForName = $('#ddlFor :selected').text();
    var securityType = $('#ddlTypeOfSecurity').val();
    var securityTypeName = $('#ddlTypeOfSecurity :selected').text();
    var transactionType = $('#ddlTypeOfTransaction').val();
    var transactionTypeName = $('#ddlTypeOfTransaction :selected').text();
    var restrictedCompanyId = $('#ddlRestrictedCompanies').val();
    var restrictedCompanyName = $('#ddlRestrictedCompanies :selected').text();
    var tradeQuantity = $('#txtTradeQuantity').val();
    var tradeExchange = $('#ddlTradeExchange').val() == null ? 0 : $('#ddlTradeExchange').val();
    var tradeExchangeName = $('#ddlTradeExchange :selected').text();
    var dematAccountId = $('#ddlDematAccount').val();
    var requestedTransactionDate = $('#txtRequestedTransactionDate').val();
    var status = status;
    var shareCurrentMarketPrice = $('#txtShareCurrentMarketPrice').val();
    var proposedTransactionThrough = $('#ddlProposedTransactionThrough').val();

    var webUrl = uri + "/api/PreClearanceRequest/SavePreClearanceRequest";
    $.ajax({
        url: webUrl,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        // async: false,
        data: JSON.stringify({
            TradeExchangeName: tradeExchangeName, TradeCompanyName: restrictedCompanyName,
            TransactionTypeName: transactionTypeName, SecurityTypeName: securityTypeName,
            PreClearanceRequestedForName: preClearanceRequestForName,
            PreClearanceRequestId: preClearanceRequestId, TradeCompany: restrictedCompanyId,
            TradeDate: requestedTransactionDate, PreClearanceRequestedFor: preClearanceRequestFor,
            SecurityType: securityType, TransactionType: transactionType, TradeQuantity: tradeQuantity, TradeExchange: tradeExchange, DematAccount: dematAccountId, Status: status,
            shareCurrentMarketPrice: shareCurrentMarketPrice,
            proposedTransactionThrough: proposedTransactionThrough
        }),
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                $('#stack1').modal('hide');
                window.location.reload(true);
                //if (msg.PreClearanceRequest.PreClearanceRequestId == preClearanceRequestId) {
                //    $("#tdPreClearanceRequestFor_" + msg.PreClearanceRequest.PreClearanceRequestId).html(msg.PreClearanceRequest.PreClearanceRequestedForName);
                //    $("#tdTypeOfSecurity_" + msg.PreClearanceRequest.PreClearanceRequestId).html(msg.PreClearanceRequest.SecurityTypeName);
                //    $("#tdRestrictedCompany_" + msg.PreClearanceRequest.PreClearanceRequestId).html(msg.PreClearanceRequest.TradeCompanyName);
                //    $("#tdTradeQuantity_" + msg.PreClearanceRequest.PreClearanceRequestId).html(msg.PreClearanceRequest.TradeQuantity);
                //    $("#tdTypeOfTransaction_" + msg.PreClearanceRequest.PreClearanceRequestId).html(msg.PreClearanceRequest.TransactionTypeName);
                //    $("#tdTradeExchange_" + msg.PreClearanceRequest.PreClearanceRequestId).html(msg.PreClearanceRequest.TradeExchangeName);
                //    $("#tdDematAccount_" + msg.PreClearanceRequest.PreClearanceRequestId).html(msg.PreClearanceRequest.DematAccount);
                //    $("#tdRequestedTransactionDate_" + msg.PreClearanceRequest.PreClearanceRequestId).html(msg.PreClearanceRequest.TradeDate);

                //    if (msg.PreClearanceRequest.Status == "InApproval") {
                //        $("#tdPreClearanceRequestStatus_" + msg.PreClearanceRequest.PreClearanceRequestId).html(msg.PreClearanceRequest.Status);
                //        $("#tdPreClearanceRequestStatus_" + msg.PreClearanceRequest.PreClearanceRequestId).css('background-color', 'orange');
                //    }
                //    else if (msg.PreClearanceRequest.Status == "Approved") {
                //        $("#tdPreClearanceRequestStatus_" + msg.PreClearanceRequest.PreClearanceRequestId).html(msg.PreClearanceRequest.Status);
                //        $("#tdPreClearanceRequestStatus_" + msg.PreClearanceRequest.PreClearanceRequestId).css('background-color', 'green');
                //    }
                //    else if (msg.PreClearanceRequest.Status == "Draft") {
                //        $("#tdPreClearanceRequestStatus_" + msg.PreClearanceRequest.PreClearanceRequestId).html(msg.PreClearanceRequest.Status);
                //        $("#tdPreClearanceRequestStatus_" + msg.PreClearanceRequest.PreClearanceRequestId).css('background-color', 'transparent');
                //    }
                //    else {
                //        $("#tdPreClearanceRequestStatus_" + msg.PreClearanceRequest.PreClearanceRequestId).html(msg.PreClearanceRequest.Status);
                //        $("#tdPreClearanceRequestStatus_" + msg.PreClearanceRequest.PreClearanceRequestId).css('background-color', 'red');
                //    }


                //    $("#btnEdit_" + msg.PreClearanceRequest.PreClearanceRequestId).attr('onclick', 'javascript:fnEditPreClearanceRequest(' + msg.PreClearanceRequest.PreClearanceRequestId + ',\'' + msg.PreClearanceRequest.PreClearanceRequestedForName + '\',\'' + msg.PreClearanceRequest.PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequest.SecurityTypeName + '\',\'' + msg.PreClearanceRequest.SecurityType + '\',\'' + msg.PreClearanceRequest.TradeCompanyName + '\',\'' + msg.PreClearanceRequest.TradeCompany + '\',\'' + msg.PreClearanceRequest.TradeQuantity + '\',\'' + msg.PreClearanceRequest.TransactionTypeName + '\',\'' + msg.PreClearanceRequest.TransactionType + '\',\'' + msg.PreClearanceRequest.TradeExchangeName + '\',\'' + msg.PreClearanceRequest.TradeExchange + '\',\'' + msg.PreClearanceRequest.DematAccount + '\',\'' + msg.PreClearanceRequest.TradeDate + '\',\'' + msg.PreClearanceRequest.Status + '\',\'' + msg.PreClearanceRequest.shareCurrentMarketPrice + '\',\'' + msg.PreClearanceRequest.proposedTransactionThrough + '\');');
                //    //  $("#btnCancelrequest_" + msg.PreClearanceRequest.PreClearanceRequestId).attr('onclick', 'javascript:fnEditPreClearanceRequest(' + msg.PreClearanceRequest.PreClearanceRequestId + ',\'' + msg.PreClearanceRequest.PreClearanceRequestedForName + '\',\'' + msg.PreClearanceRequest.PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequest.SecurityTypeName + '\',\'' + msg.PreClearanceRequest.SecurityType + '\',\'' + msg.PreClearanceRequest.TradeCompanyName + '\',\'' + msg.PreClearanceRequest.TradeCompany + '\',\'' + msg.PreClearanceRequest.TradeQuantity + '\',\'' + msg.PreClearanceRequest.TransactionTypeName + '\',\'' + msg.PreClearanceRequest.TransactionType + '\',\'' + msg.PreClearanceRequest.TradeExchangeName + '\',\'' + msg.PreClearanceRequest.TradeExchange + '\',\'' + msg.PreClearanceRequest.DematAccount + '\',\'' + msg.PreClearanceRequest.TradeDate + '\',\'' + msg.PreClearanceRequest.Status + '\');');
                //    $("#btnUploadbrokernote_" + msg.PreClearanceRequest.PreClearanceRequestId).attr('onclick', 'javascript:fnEditBrokerNote(' + msg.PreClearanceRequest.PreClearanceRequestId + ',\'' + msg.PreClearanceRequest.BrokerNote + '\',\'' + msg.PreClearanceRequest.ActualTransactionDate + '\',\'' + msg.PreClearanceRequest.ValuePerShare + '\',\'' + msg.PreClearanceRequest.TotalAmount + '\',\'' + msg.PreClearanceRequest.TradeQuantity + '\',\'' + msg.PreClearanceRequest.ActualTradeQuantity + '\',\'' + msg.PreClearanceRequest.Status + '\',\'' + msg.PreClearanceRequest.TradeExchange + '\',\'' + msg.PreClearanceRequest.DematAccount + '\',\'' + msg.PreClearanceRequest.PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequest.TradeCompany + '\',\'' + msg.PreClearanceRequest.TransactionType + '\',\'' + msg.PreClearanceRequest.TradeDate + '\',\'' + msg.PreClearanceRequest.remarks + '\');');

                //    $("#btnEdit_" + msg.PreClearanceRequest.PreClearanceRequestId).attr("data-target", "#stack1");
                //    $("#btnEdit_" + msg.PreClearanceRequest.PreClearanceRequestId).attr("data-toggle", "modal");

                //    $("#btnUploadbrokernote_" + msg.PreClearanceRequest.PreClearanceRequestId).attr("data-target", "#basic1");
                //    $("#btnUploadbrokernote_" + msg.PreClearanceRequest.PreClearanceRequestId).attr("data-toggle", "modal");

                //    if (msg.PreClearanceRequest.Status == "InApproval" || msg.PreClearanceRequest.Status == "Approved") {
                //        if (msg.PreClearanceRequest.isBrokerNoteUploaded != 'Y') {
                //            $('#liUploadbrokernote_' + msg.PreClearanceRequest.PreClearanceRequestId).show();
                //        }
                //    }
                //    else {
                //        $('#liUploadbrokernote_' + msg.PreClearanceRequest.PreClearanceRequestId).hide();
                //    }

                //    var table = $('#tbl-preclearance-setup').DataTable();
                //    table.destroy();
                //    initializeDataTable();
                //    alert(msg.Msg);
                //}
                //else {
                //    var result = "";
                //    result += '<tr id="tr_' + msg.PreClearanceRequest.PreClearanceRequestId + '">';
                //    result += '<td id="tdPreClearanceRequestFor_' + msg.PreClearanceRequest.PreClearanceRequestId + '">' + msg.PreClearanceRequest.PreClearanceRequestedForName + '</td>';
                //    result += '<td id="tdTypeOfSecurity_' + msg.PreClearanceRequest.PreClearanceRequestId + '">' + msg.PreClearanceRequest.SecurityTypeName + '</td>';
                //    result += '<td id="tdRestrictedCompany_' + msg.PreClearanceRequest.PreClearanceRequestId + '">' + msg.PreClearanceRequest.TradeCompanyName + '</td>';
                //    result += '<td id="tdTradeQuantity_' + msg.PreClearanceRequest.PreClearanceRequestId + '">' + msg.PreClearanceRequest.TradeQuantity + '</td>';
                //    result += '<td id="tdTypeOfTransaction_' + msg.PreClearanceRequest.PreClearanceRequestId + '">' + msg.PreClearanceRequest.TransactionTypeName + '</td>';
                //    result += '<td style="display:none;" id="tdTradeExchange_' + msg.PreClearanceRequest.PreClearanceRequestId + '">' + msg.PreClearanceRequest.TradeExchangeName + '</td>';
                //    result += '<td  id="tdDematAccount_' + msg.PreClearanceRequest.PreClearanceRequestId + '">' + msg.PreClearanceRequest.DematAccount + '</td>';
                //    result += '<td id="tdRequestedTransactionDate_' + msg.PreClearanceRequest.PreClearanceRequestId + '">' + msg.PreClearanceRequest.TradeDate + '</td>';
                //    if (msg.PreClearanceRequest.Status == "InApproval") {
                //        result += '<td style="background-color:orange" id="tdPreClearanceRequestStatus_' + msg.PreClearanceRequest.PreClearanceRequestId + '">' + msg.PreClearanceRequest.Status + '</td>';
                //    }
                //    else if (msg.PreClearanceRequest.Status == "Approved") {
                //        result += '<td style="background-color:green" id="tdPreClearanceRequestStatus_' + msg.PreClearanceRequest.PreClearanceRequestId + '">' + msg.PreClearanceRequest.Status + '</td>';
                //    }
                //    else if (msg.PreClearanceRequest.Status == "Draft") {
                //        result += '<td style="background-color:transparent" id="tdPreClearanceRequestStatus_' + msg.PreClearanceRequest.PreClearanceRequestId + '">' + msg.PreClearanceRequest.Status + '</td>';
                //    }
                //    else {
                //        result += '<td style="background-color:red" id="tdPreClearanceRequestStatus_' + msg.PreClearanceRequest.PreClearanceRequestId + '">' + msg.PreClearanceRequest.Status + '</td>';
                //    }
                //    result += '<td>' + msg.PreClearanceRequest.isBrokerNoteUploaded + '</td>';
                //    result += '<td>' + msg.PreClearanceRequest.reviewedOn + '</td>';
                //    result += '<td>' + msg.PreClearanceRequest.reviewedBy + '</td>';
                //    result += '<td>' + msg.PreClearanceRequest.reviewerRemarks + '</td>';
                //    // result += '<td id="tdActions_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '"><a data-target="#stack1" data-toggle="modal" id="a_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '" class="btn btn-outline dark" onclick=\"javascript:fnEditDesignation(' + msg.PreClearanceRequestList[i].PreClearanceRequestId + ',\'' + msg.PreClearanceRequestList[i].DESIGNATION_NM + '\');\">Edit</a></td>';
                //    result += '<td id="tdActions_' + msg.PreClearanceRequest.PreClearanceRequestId + '">';
                //    var innerStr = "";
                //    innerStr += '<div class="btn-group">';
                //    innerStr += '<a class="btn blue dropdown-toggle" href="javascript:;" data-toggle="dropdown">';
                //    innerStr += '<i class="fa fa-user"></i> Actions';
                //    innerStr += '<i class="fa fa-angle-down"></i>';
                //    innerStr += '</a>';
                //    innerStr += '<ul class="dropdown-menu pull-right">';
                //    innerStr += '<li>';
                //    innerStr += '<a  id="btnEdit_' + msg.PreClearanceRequest.PreClearanceRequestId + '" data-toggle="modal" data-target="#stack1" onclick=\"javascript:fnEditPreClearanceRequest(' + msg.PreClearanceRequest.PreClearanceRequestId + ',\'' + msg.PreClearanceRequest.PreClearanceRequestedForName + '\',\'' + msg.PreClearanceRequest.PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequest.SecurityTypeName + '\',\'' + msg.PreClearanceRequest.SecurityType + '\',\'' + msg.PreClearanceRequest.TradeCompanyName + '\',\'' + msg.PreClearanceRequest.TradeCompany + '\',\'' + msg.PreClearanceRequest.TradeQuantity + '\',\'' + msg.PreClearanceRequest.TransactionTypeName + '\',\'' + msg.PreClearanceRequest.TransactionType + '\',\'' + msg.PreClearanceRequest.TradeExchangeName + '\',\'' + msg.PreClearanceRequest.TradeExchange + '\',\'' + msg.PreClearanceRequest.DematAccount + '\',\'' + msg.PreClearanceRequest.TradeDate + '\',\'' + msg.PreClearanceRequest.Status + '\',\'' + msg.PreClearanceRequest.shareCurrentMarketPrice + '\',\'' + msg.PreClearanceRequest.proposedTransactionThrough + '\');\">';
                //    innerStr += '<i class="fa fa-pencil"></i> Edit </a>';
                //    innerStr += '</li>';
                //    //innerStr += '<li>';
                //    //innerStr += '<a  id="btnCancelrequest_' + msg.PreClearanceRequest.PreClearanceRequestId + '" onclick=\"javascript:fnEditPreClearanceRequest(' + msg.PreClearanceRequest.PreClearanceRequestId + ',\'' + msg.PreClearanceRequest.PreClearanceRequestedForName + '\',\'' + msg.PreClearanceRequest.PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequest.SecurityTypeName + '\',\'' + msg.PreClearanceRequest.SecurityType + '\',\'' + msg.PreClearanceRequest.TradeCompanyName + '\',\'' + msg.PreClearanceRequest.TradeCompany + '\',\'' + msg.PreClearanceRequest.TradeQuantity + '\',\'' + msg.PreClearanceRequest.TransactionTypeName + '\',\'' + msg.PreClearanceRequest.TransactionType + '\',\'' + msg.PreClearanceRequest.TradeExchangeName + '\',\'' + msg.PreClearanceRequest.TradeExchange + '\',\'' + msg.PreClearanceRequest.DematAccount + '\',\'' + msg.PreClearanceRequest.TradeDate + '\',\'' + msg.PreClearanceRequest.Status + '\');\">';
                //    //innerStr += '<i class="fa fa-times"></i> Cancel Request </a>';
                //    //innerStr += '</li>';
                //    if (msg.PreClearanceRequest.Status == "InApproval" || msg.PreClearanceRequest.Status == "Approved") {
                //        if (msg.PreClearanceRequest.isBrokerNoteUploaded != 'Y') {
                //            innerStr += '<li id="liUploadbrokernote_' + msg.PreClearanceRequest.PreClearanceRequestId + '">';
                //            innerStr += '<a  id="btnUploadbrokernote_' + msg.PreClearanceRequest.PreClearanceRequestId + '" data-toggle="modal" data-target="#basic1" onclick=\"javascript:fnEditBrokerNote(' + msg.PreClearanceRequest.PreClearanceRequestId + ',\'' + msg.PreClearanceRequest.BrokerNote + '\',\'' + msg.PreClearanceRequest.ActualTransactionDate + '\',\'' + msg.PreClearanceRequest.ValuePerShare + '\',\'' + msg.PreClearanceRequest.TotalAmount + '\',\'' + msg.PreClearanceRequest.TradeQuantity + '\',\'' + msg.PreClearanceRequest.ActualTradeQuantity + '\',\'' + msg.PreClearanceRequest.Status + '\',\'' + msg.PreClearanceRequest.TradeExchange + '\',\'' + msg.PreClearanceRequest.DematAccount + '\',\'' + msg.PreClearanceRequest.PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequest.TradeCompany + '\',\'' + msg.PreClearanceRequest.TransactionType + '\',\'' + msg.PreClearanceRequest.TradeDate + '\',\'' + msg.PreClearanceRequest.remarks + '\');\">';
                //            innerStr += '<i class="fa fa-upload"></i>Upload Trade Details </a>';
                //            innerStr += '</li>';
                //        }
                //    }
                //    else {
                //        innerStr += '<li id="liUploadbrokernote_' + msg.PreClearanceRequest.PreClearanceRequestId + '" style="display:none">';
                //        innerStr += '<a  id="btnUploadbrokernote_' + msg.PreClearanceRequest.PreClearanceRequestId + '" data-toggle="modal" data-target="#basic1" onclick=\"javascript:fnEditBrokerNote(' + msg.PreClearanceRequest.PreClearanceRequestId + ',\'' + msg.PreClearanceRequest.BrokerNote + '\',\'' + msg.PreClearanceRequest.ActualTransactionDate + '\',\'' + msg.PreClearanceRequest.ValuePerShare + '\',\'' + msg.PreClearanceRequest.TotalAmount + '\',\'' + msg.PreClearanceRequest.TradeQuantity + '\',\'' + msg.PreClearanceRequest.ActualTradeQuantity + '\',\'' + msg.PreClearanceRequest.Status + '\',\'' + msg.PreClearanceRequest.TradeExchange + '\',\'' + msg.PreClearanceRequest.DematAccount + '\',\'' + msg.PreClearanceRequest.PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequest.TradeCompany + '\',\'' + msg.PreClearanceRequest.TransactionType + '\',\'' + msg.PreClearanceRequest.TradeDate + '\',\'' + msg.PreClearanceRequest.remarks + '\');\">';
                //        innerStr += '<i class="fa fa-upload"></i>Upload Trade Details </a>';
                //        innerStr += '</li>';
                //    }

                //    innerStr += '<li class="divider"> </li>';
                //    innerStr += '</ul>';
                //    innerStr += '</div>';
                //    result += innerStr + '</td>';
                //    result += '</tr>';

                //    var table = $('#tbl-preclearance-setup').DataTable();
                //    table.destroy();
                //    $("#tbdPreClearanceList").append(result);
                //    initializeDataTable();
                //    alert(msg.Msg);
                //}
                //fnClearForm();
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
            alert(response.status + ' ' + response.statusText);
        }
    })
}
function fnValidate() {
    if ($('#ddlFor').val() == undefined || $('#ddlFor').val() == null || $('#ddlFor').val().trim() == '' || $('#ddlFor').val().trim() == 'Select') {
        alert("Please fill all mandatory fields");
        return false;
    }
    if ($('#ddlTypeOfSecurity').val() == undefined || $('#ddlTypeOfSecurity').val() == null || $('#ddlTypeOfSecurity').val().trim() == '' || $('#ddlTypeOfSecurity').val().trim() == '0') {
        alert("Please fill all mandatory fields");
        return false;
    }

    if ($("#ddlFor").val() != "0" && $("select[id*=ddlTypeOfSecurity] option:selected").text() == "ESOP") {
        alert("Cannot request ESOP to your relatives.");
        return false;
    }

    if ($('#ddlRestrictedCompanies').val() == undefined || $('#ddlRestrictedCompanies').val() == null || $('#ddlRestrictedCompanies').val().trim() == '' || $('#ddlRestrictedCompanies').val().trim() == '0') {
        alert("Please fill all mandatory fields");
        return false;
    }
    if ($('#txtTradeQuantity').val() == undefined || $('#txtTradeQuantity').val() == null || $('#txtTradeQuantity').val().trim() == '') {
        alert("Please fill all mandatory fields");
        return false;
    }
    else if (parseInt($('#txtTradeQuantity').val()) > 100000) {
        alert("You cannot trade in more than 1,00,000 (One Lac) shares at a time.");
        return false;
    }
    if ($("#ddlTypeOfTransaction").val() == undefined || $("#ddlTypeOfTransaction").val() == null || $("#ddlTypeOfTransaction").val() == "0") {
        alert("Please fill all mandatory fields");
        return false;
    }
    //if ($('#ddlTradeExchange').val() == undefined || $('#ddlTradeExchange').val() == null || $('#ddlTradeExchange').val().trim() == '' || $('#ddlTradeExchange').val().trim() == '0') {
    //    return false;
    //}
    if ($('#ddlDematAccount').val() == undefined || $('#ddlDematAccount').val() == null || $('#ddlDematAccount').val().trim() == '' || $('#ddlDematAccount').val().trim() == '0') {
        alert("Please fill all mandatory fields");
        return false;
    }
    if ($('#txtRequestedTransactionDate').val() == undefined || $('#txtRequestedTransactionDate').val() == null || $('#txtRequestedTransactionDate').val().trim() == '') {
        alert("Please fill all mandatory fields");
        return false;
    }
    if ($('#txtShareCurrentMarketPrice').val() == undefined || $('#txtShareCurrentMarketPrice').val() == null || $('#txtShareCurrentMarketPrice').val().trim() == '') {
        alert("Please fill all mandatory fields");
        return false;
    }
    return true;
}
function fnEditPreClearanceRequest(preClearanceRequestId, PreClearanceRequestedForName, PreClearanceRequestedFor, SecurityTypeName, SecurityType, TradeCompanyName, TradeCompany, TradeQuantity, TransactionTypeName, TransactionType, TradeExchangeName, TradeExchange, DematAccount, TradeDate, Status, shareCurrentMarketPrice, proposedTransactionThrough) {
    $("#preClearanceRequestId").val(preClearanceRequestId);
    // PreClearanceRequestedForName
    $("#ddlFor").val(PreClearanceRequestedFor);
    //SecurityTypeName
    $("#ddlTypeOfSecurity").val(SecurityType);
    // TradeCompanyName
    $("#ddlRestrictedCompanies").val(TradeCompany);
    $("#txtTradeQuantity").val(TradeQuantity);
    // TransactionTypeName
    $("#ddlTypeOfTransaction").val(TransactionType);
    // TradeExchangeName
    $("#ddlTradeExchange").val(TradeExchange);
    //fnGetDematAccount(TradeExchange, null);
    fnGetDematAccount(PreClearanceRequestedFor, null);
    $("#ddlDematAccount").val(DematAccount);
    $("#txtRequestedTransactionDate").val(TradeDate);
    $("#txtShareCurrentMarketPrice").val(shareCurrentMarketPrice);
    $("#ddlProposedTransactionThrough").val(proposedTransactionThrough);

    if (Status == "InApproval" || Status == "Approved") {
        $("#btnSubmitPreClearanceRequest").hide();
        //$("#btnSaveAsDraftPreClearanceRequest").hide();
        $("#btnCancelPreClearanceRequest").show();

        $("#ddlFor").prop("disabled", true);
        $("#ddlTypeOfSecurity").prop("disabled", true);
        $("#ddlRestrictedCompanies").prop("disabled", true);
        $("#txtTradeQuantity").prop("readonly", true);
        $("#ddlTypeOfTransaction").prop("disabled", true);
        $("#ddlTradeExchange").prop("disabled", true);
        $("#ddlDematAccount").prop("disabled", true);
        $("#txtRequestedTransactionDate").prop("disabled", true);
        $("#txtShareCurrentMarketPrice").prop("readonly", true);
        $("#ddlProposedTransactionThrough").prop("disabled", true);
    }
    else if (Status == "Draft") {
        $("#btnSubmitPreClearanceRequest").show();
        //$("#btnSaveAsDraftPreClearanceRequest").show();
        $("#btnCancelPreClearanceRequest").hide();

        $("#ddlFor").prop("disabled", false);
        $("#ddlTypeOfSecurity").prop("disabled", false);
        $("#ddlRestrictedCompanies").prop("disabled", false);
        $("#txtTradeQuantity").prop("readonly", false);
        $("#ddlTypeOfTransaction").prop("disabled", false);
        $("#ddlTradeExchange").prop("disabled", false);
        $("#ddlDematAccount").prop("disabled", false);
        $("#txtRequestedTransactionDate").prop("disabled", false);
        $("#txtShareCurrentMarketPrice").prop("readonly", false);
        $("#ddlProposedTransactionThrough").prop("disabled", false);
    }
    else if (Status == "Cancel") {
        $("#btnSubmitPreClearanceRequest").show();
        //$("#btnSaveAsDraftPreClearanceRequest").show();
        $("#btnCancelPreClearanceRequest").hide();

        $("#ddlFor").prop("disabled", false);
        $("#ddlTypeOfSecurity").prop("disabled", false);
        $("#ddlRestrictedCompanies").prop("disabled", false);
        $("#txtTradeQuantity").prop("readonly", false);
        $("#ddlTypeOfTransaction").prop("disabled", false);
        $("#ddlTradeExchange").prop("disabled", false);
        $("#ddlDematAccount").prop("disabled", false);
        $("#txtRequestedTransactionDate").prop("disabled", false);
        $("#txtShareCurrentMarketPrice").prop("readonly", false);
        $("#ddlProposedTransactionThrough").prop("disabled", false);
    }
    else {
        //do nothing
    }
}
function AddUpdateBrokerNote() {
    if (fnValidateBrokerNote() && fnValidateMultiTradeList()) {
        if ($("#txtTotalamount").val() == '0') {
            $("#modalZeroTradeValue").modal('show');
        }
        else {
            $("#modalBrokerNoteUploadConfirmation").modal('show');
        }
    }
    else {

    }
}
function goToNullTrade() {
    $("#modalNullTrade").modal('show');
}
function fnSubmitBrokerNote() {
    $("#Loader").show();
    var test = new FormData();
    var preClearanceRequestId = $("#preClearanceRequestIdBN").val();
    // var tempFile = $('#btnBrokernote').get(0).files[0].name;
    //  var brokerNote = tempFile.split('.')[0] + "_" + preClearanceRequestId + "." + tempFile.split('.')[1];
    var brokerNote = '';
    var tradeQuantity = $("#txtTradequantityBrokerNote").val();
    var actualTradeQuantity = $("#txtActualTradequantityBrokerNote").val();
    var valuePerShare = $('#txtValuePerShare').val();

    if (actualTradeQuantity == null || actualTradeQuantity == undefined || actualTradeQuantity.trim() == '') {
        $('#txtTotalamount').val(tradeQuantity * valuePerShare);
    }
    else {
        $('#txtTotalamount').val(actualTradeQuantity * valuePerShare);
    }
    var totalAmount = $('#txtTotalamount').val();
    var actualTransactionDate = $('#txtActualdateoftransaction').val();
    var status = $("#txtStatusBN").val();
    var tradeExchange = $("#txtTradeExchangeBN").val();
    var dematAccountID = $("#txtDematAccountBN").val();
    var exchangeTradedOn = $("#ddlExchangeTradedOn").val();
    var remarks = $("#remarks").val();
    var brokerdetails = $("#txtareabrokerdetails").val();

    var MulTiTradeList = new Array();
    for (var i = 0; i < $("#tbdMultiTrade").children().length; i++) {
        var bNote = new Object();
        var mtDate = $($($($("#tbdMultiTrade").children()[i]).children()[0]).children()[0]).val();
        var mtQty = $($($($("#tbdMultiTrade").children()[i]).children()[1]).children()[0]).val();
        var mtRate = $($($($("#tbdMultiTrade").children()[i]).children()[2]).children()[0]).val();
        var mtAmount = $($($($("#tbdMultiTrade").children()[i]).children()[3]).children()[0]).val();

        bNote.PartialTradeDate = mtDate;
        bNote.PartialTradeQuantity = mtQty;
        bNote.PartialValuePerShare = mtRate;
        bNote.PartialTotalAmount = mtAmount;
        MulTiTradeList.push(bNote);
    }


    test.append("Object", JSON.stringify({ PreClearanceRequestId: preClearanceRequestId, BrokerNote: brokerNote, ActualTransactionDate: actualTransactionDate, ValuePerShare: valuePerShare, TotalAmount: totalAmount, TradeQuantity: tradeQuantity, ActualTradeQuantity: actualTradeQuantity, Status: status, TradeExchange: tradeExchange, DematAccount: dematAccountID, remarks: remarks, exchangeTradedOn: exchangeTradedOn, lstMultiTrade: MulTiTradeList, BrokerDetails: brokerdetails }));
    test.append("Files", $("#btnBrokernote").get(0).files[0]);


    var webUrl = uri + "/api/PreClearanceRequest/SaveBrokerNoteUpload";
    $.ajax({
        url: webUrl,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        contentType: false,
        //async: false,
        processData: false,
        data: test,
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                alert(msg.Msg);
                if (msg.PreClearanceRequest.lstFormUrl !== null) {
                    if (msg.PreClearanceRequest.lstFormUrl.length) {
                        for (var x = 0; x < msg.PreClearanceRequest.lstFormUrl.length; x++) {
                            //alert(msg.PreClearanceRequest.lstFormUrl[x]);
                            window.open("emailAttachment/" + msg.PreClearanceRequest.lstFormUrl[x]);
                        }
                    }
                }

                window.location.reload(true);
                //if (msg.PreClearanceRequest.PreClearanceRequestId == preClearanceRequestId) {
                //    //if (msg.PreClearanceRequest.ExceededTradeLimit != "Yes") {
                //    //    alert(msg.Msg);
                //    //    window.location.reload(true);
                //    //}
                //    //else {
                //    $("input[id*='txtBrokerNoteId']").val(msg.PreClearanceRequest.brokerNoteId);
                //    if (msg.PreClearanceRequest.lstFormUrl != null) {
                //        $("select[id*='ddlForms']").empty();
                //        var strOption = "";
                //        var strHtml = "";
                //        arrForms = new Array();
                //        for (var x = 0; x < msg.PreClearanceRequest.lstFormUrl.length; x++) {
                //            var strValue = msg.PreClearanceRequest.lstFormUrl[x];
                //            strOption += strValue.split("~")[1] + " & ";
                //            arrForms.push(strValue.split("~")[0]);
                //            strHtml += '<div id="divForm_' + strValue.split("~")[0] + '">';
                //            strHtml += '<div class="col-md-4" id="lblUploadForm_' + strValue.split("~")[0] + '">' + strValue.split("~")[1] + '</div>';
                //            strHtml += '<div class="col-md-8">';
                //            strHtml += '<input type="file" id="txtUploadForm_' + strValue.split("~")[0] + '" class="form-control" data-tabindex="4" />';
                //            strHtml += '</div>';
                //            strHtml += '</div><br />';
                //        }
                //        var sHtml = strHtml.substr(0, strHtml.length - 6);
                //        $("#divUploadForm").html(sHtml);

                //        var s = strOption.substr(0, strOption.length - 3);
                //        ddlForms.append(new Option(s, "All"));

                //        if (msg.PreClearanceRequest.isNUllTrade) {
                //            fnDisplayNote(null, null, "Non Trade");
                //        }
                //        else {
                //            fnDisplayNote(null, null, "Trade");
                //        }
                //    }

                //    //$("select[id*='ddlForms']").find('option[value=FORM_J]').remove();
                //    //if (userCategory.toUpperCase() == "PROMOTER") {
                //    //    $("select[id*='ddlForms']").find('option[value=FORM_CJ]').remove();
                //    //    $("#divForm").show();
                //    //    $("#divAnnexure").show();
                //    //    $("select[id*='ddlForms']").find('option[value=FORM_DJ]').prop("selected", true);
                //    //    fnDisplayNote(null, null, "FORM_DJ");
                //    //}
                //    //else {
                //    //    $("select[id*='ddlForms']").find('option[value=FORM_DJ]').remove();
                //    //    $("#divForm").show();
                //    //    $("#divAnnexure").show();
                //    //    $("select[id*='ddlForms']").find('option[value=FORM_CJ]').prop("selected", true);
                //    //    fnDisplayNote(null, null, "FORM_CJ");
                //    //}
                //    $("#btnCancelBrokerNote").trigger("click");
                //    $("#btnOpenForm").trigger("click");

                //    //$("#btnOpenForm").attr('onclick', 'javascript:fnShowForm(\'' + msg.PreClearanceRequest.brokerNoteId + '\');');

                //    //$("#btnOpenForm").attr("data-target", "#modalForms");
                //    //$("#btnOpenForm").attr("data-toggle", "modal");

                //    //window.location.reload(); 
                //    //   $("#tdIsBrokerNoteUploaded_" + msg.PreClearanceRequest.PreClearanceRequestId).html('Y');
                //    $("#liUploadbrokernote_" + msg.PreClearanceRequest.PreClearanceRequestId).hide();
                //    //}
                //}

                //fnClearFormBrokerNote();
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
            alert(response.status + ' ' + response.statusText);
        }
    });
}
function fnDisplayNote(ddlForms, val, strVal) {
    var note = "";
    var mode = "";
    if (ddlForms == null) {
        mode = strVal;
    }
    else {
        mode = $(ddlForms).val();
        fnRemoveClass(ddlForms, val);
    }
    switch (mode) {
        case "Trade":
            note = "To be submitted by the Designated Person for submission to Compliance Officer which will inturn be forwarded to Stock Exchanges, if required (in case the transaction in a calendar quarter is exceeding Rs. 10 lacs).";
            break;
        case "FORM_CJ":
            note = "To be submitted by the Designated Person for submission to Compliance Officer which will inturn be forwarded to Stock Exchanges, if required (in case the transaction in a calendar quarter is exceeding Rs. 10 lacs).";
            break;
        case "FORM_DJ":
            note = "To be submitted by the Designated Person for submission to Compliance Officer which will inturn be forwarded to Stock Exchanges, if required (in case the transaction in a calendar quarter is exceeding Rs. 10 lacs).";
            break;
        case "Non Trade":
            note = "Submit nil disclosure";
            break;
        case "FORM_J":
            note = "Submit nil disclosure";
            break;
        case "Non Trade":
            note = "Submit nil disclosure";
            break;
        default:
            break;
    }
    $("label[id*='lblNote']").text(note);
    $("label[id*='lblNote']").show();
}
function fnShowUploadDiv(chkOverwrite) {
    var check = $(chkOverwrite).prop("checked");
    if (check) {
        $("#divUploadForm").show();
        $("#divUploadLbl").show();
        $("input[id*='txtUploadForm']").val(null);
        $("input[id*='txtUploadFormAnnexure']").val(null);
    }
    else {
        $("#divUploadForm").hide();
        $("#divUploadLbl").hide();
        $("input[id*='txtUploadForm']").val(null);
        $("input[id*='txtUploadFormAnnexure']").val(null);
    }
}
function fnDownloadForm() {
    if ($("select[id*='ddlForms']").val() == "0") {
        alert("Please Select Forms to Download");
        return false;
    }

    $("#Loader").show();
    var preClearanceRequestId = $("#preClearanceRequestIdBN").val();
    var brokerNoteId = $("input[id*='txtBrokerNoteId']").val();
    var webUrl = uri + "/api/PreClearanceRequest/GetForms";
    $.ajax({
        url: webUrl,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        contentType: false,
        data: JSON.stringify({ PreClearanceRequestId: preClearanceRequestId, brokerNoteId: brokerNoteId, formType: $("select[id*='ddlForms']").val() }),
        success: function (msg) {

            if (msg.StatusFl) {
                $("#Loader").hide();
                $("#btnSubmitForm").prop("disabled", false);

                if (msg.PreClearanceReques !== null && msg.PreClearanceRequest.lstFormUrl !== null) {
                    //alert("msg.PreClearanceRequest.lstFormUrl.length=" + msg.PreClearanceRequest.lstFormUrl.length);
                    if (msg.PreClearanceRequest.lstFormUrl.length) {
                        for (var x = 0; x < msg.PreClearanceRequest.lstFormUrl.length; x++) {
                            //alert("msg.PreClearanceRequest.lstFormUrl[" + x + "]=" + msg.PreClearanceRequest.lstFormUrl[x]);
                            window.open(".." + msg.PreClearanceRequest.lstFormUrl[x]);
                        }
                        /*$.each(msg.PreClearanceRequest.lstFormUrl, function (index, element) {
                            //downloadURL1(element);
                            window.open(".." + element, null);
                        });*/
                    }
                }

                //if ($("select[id*='ddlForms']").val() !== "FORM_J") {
                //    setTimeout(function () { downloadURL1(msg.PreClearanceRequest.formUrl); }, 3000);
                //    // downloadURL1(msg.PreClearanceRequest.formUrl);
                //    fnDownloadFormJ();
                //}
                //else {
                //    $("#Loader").hide();
                //    downloadURL1(msg.PreClearanceRequest.formUrl);
                //}
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
            alert(response.status + ' ' + response.statusText);
        }
    });
}
function fnDownloadFormJ() {
    if ($("select[id*='ddlForms']").val() == "0") {
        alert("Please Select Forms to Download");
        return false;
    }

    var preClearanceRequestId = $("#preClearanceRequestIdBN").val();
    var brokerNoteId = $("input[id*='txtBrokerNoteId']").val();
    var webUrl = uri + "/api/PreClearanceRequest/GetForms";
    $.ajax({
        url: webUrl,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        contentType: false,
        data: JSON.stringify({ PreClearanceRequestId: preClearanceRequestId, brokerNoteId: brokerNoteId, formType: "FORM_J" }),
        success: function (msg) {
            $("#Loader").hide();
            if (msg.StatusFl) {

                if (msg.PreClearanceReques !== null && msg.PreClearanceRequest.lstFormUrl !== null) {
                    if (msg.PreClearanceRequest.lstFormUrl.length) {
                        $.each(msg.PreClearanceRequest.lstFormUrl, function (index, element) {
                            downloadURL1(element);
                        })
                    }
                }
                //downloadURL1(msg.PreClearanceRequest.formUrl);

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
            alert(response.status + ' ' + response.statusText);
        }
    });
}
function fnValidateForms() {
    if (!fnValidateSubmitForms()) {
        $('#btnSubmitForm').removeAttr("data-dismiss");
        return false;
    }
    else {
        $("#modalSubmitConfirmation").show();
    }
}
function fnSubmitForms() {
    $("#Loader").show();
    if ($("input[id*='chkOverwrite']").prop('checked') == true) {
        fnSubmitCustomForm();
    }
    else {
        fnSubmitSystemGeneratedForm();
    }
}
function fnSubmitCustomForm() {
    var filesData = new FormData();
    var uploadFormName = "";

    if ($("input[id*='txtUploadForm']").get(0).files.length > 0) {
        uploadFormName = $("input[id*='txtUploadForm']").get(0).files[0].name;
    }

    filesData.append("Object", JSON.stringify({ PreClearanceRequestId: $("#preClearanceRequestIdBN").val(), brokerNoteId: $("input[id*='txtBrokerNoteId']").val(), BrokerNote: uploadFormName, formType: $("select[id*='ddlForms']").val() }));

    for (var x = 0; x < arrForms.length; x++) {
        //var txtUploadForm = $("input[id='txtUploadForm_" + arrForms[x] + "']").get(0);
        if ($("input[id='txtUploadForm_" + arrForms[x] + "']").get(0).files.length > 0) {
            filesData.append($("input[id='txtUploadForm_" + arrForms[x] + "']").get(0).files[0].name, $("input[id='txtUploadForm_" + arrForms[x] + "']").get(0).files[0]);
        }
        //if ($("input[id*='txtUploadFormAnnexure']").get(0).files.length > 0) {
        //    filesData.append($("input[id*='txtUploadFormAnnexure']").get(0).files[0].name, $("input[id*='txtUploadFormAnnexure']").get(0).files[0]);
        //}
    }

    $("#Loader").show();
    var webUrl = uri + "/api/PreClearanceRequest/SubmitCustomForm";
    $.ajax({
        type: 'POST',
        url: webUrl,
        data: filesData,
        contentType: false,
        //  async: false,
        processData: false,
        success: function (msg) {

            if (msg.StatusFl) {
                //alert(msg.Msg);
                //window.location.reload(true);
                $("#Loader").hide();
                alert(msg.Msg);
                window.location.reload(true);
                //if ($("select[id*='ddlForms']").val() !== "FORM_J") {
                //    fnSubmitCustomFormJ();
                //}
                //else {
                //    $("#Loader").hide();
                //    alert(msg.Msg);
                //    window.location.reload(true);
                //}

            }
            else {
                $("#Loader").hide();
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    alert(msg.Msg);
                    $('#btnSubmitForm').removeAttr("data-dismiss");
                }
                return false;
            }
        },
        error: function (error) {
            $("#Loader").hide();
            $('#btnSubmitForm').removeAttr("data-dismiss");
            alert(error.status + ' ' + error.statusText);
        }
    })
}
function fnSubmitCustomFormJ() {
    var filesData = new FormData();
    var uploadFormName = "";

    if ($("input[id*='txtUploadFormAnnexure']").get(0).files.length > 0) {
        uploadFormName = $("input[id*='txtUploadFormAnnexure']").get(0).files[0].name;
    }

    filesData.append("Object", JSON.stringify({ PreClearanceRequestId: $("#preClearanceRequestIdBN").val(), brokerNoteId: $("input[id*='txtBrokerNoteId']").val(), BrokerNote: uploadFormName }));

    if ($("input[id*='txtUploadFormAnnexure']").get(0).files.length > 0) {
        filesData.append("Files", $("input[id*='txtUploadFormAnnexure']").get(0).files[0]);
    }

    var webUrl = uri + "/api/PreClearanceRequest/SubmitCustomForm";
    $.ajax({
        type: 'POST',
        url: webUrl,
        data: filesData,
        contentType: false,
        //  async: false,
        processData: false,
        success: function (msg) {
            $("#Loader").hide();
            if (msg.StatusFl) {
                alert(msg.Msg);
                window.location.reload(true);
            }
            else {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    alert(msg.Msg);
                    $('#btnSubmitForm').removeAttr("data-dismiss");
                }
                return false;
            }
        },
        error: function (error) {
            $("#Loader").hide();
            $('#btnSubmitForm').removeAttr("data-dismiss");
            alert(error.status + ' ' + error.statusText);
        }
    })
}
function fnSubmitSystemGeneratedForm() {
    var filesData = new FormData();

    filesData.append("Object", JSON.stringify({ PreClearanceRequestId: $("#preClearanceRequestIdBN").val(), brokerNoteId: $("input[id*='txtBrokerNoteId']").val(), formType: $("select[id*='ddlForms']").val() }));

    var webUrl = uri + "/api/PreClearanceRequest/SubmitSystemGeneratedForm";
    $("#Loader").show();
    $.ajax({
        type: 'POST',
        url: webUrl,
        data: filesData,
        contentType: false,
        //  async: false,
        processData: false,
        success: function (msg) {
            if (msg.StatusFl) {
                $("#Loader").hide();
                alert(msg.Msg);
                window.location.reload(true);
            }
            else {
                $("#Loader").hide();
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    alert(msg.Msg);
                    $('#btnSubmitForm').removeAttr("data-dismiss");
                }
                return false;
            }
        },
        error: function (error) {
            $("#Loader").hide();
            $('#btnSubmitForm').removeAttr("data-dismiss");
            alert(error.status + ' ' + error.statusText);
        }
    })
}
function fnSubmitSystemGeneratedFormJ() {
    var filesData = new FormData();

    filesData.append("Object", JSON.stringify({ PreClearanceRequestId: $("#preClearanceRequestIdBN").val(), brokerNoteId: $("input[id*='txtBrokerNoteId']").val(), formType: "FORM_J" }));

    var webUrl = uri + "/api/PreClearanceRequest/SubmitSystemGeneratedForm";
    $.ajax({
        type: 'POST',
        url: webUrl,
        data: filesData,
        contentType: false,
        //  async: false,
        processData: false,
        success: function (msg) {
            $("#Loader").hide();
            if (msg.StatusFl) {
                alert(msg.Msg);
                window.location.reload(true);
            }
            else {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    alert(msg.Msg);
                    $('#btnSubmitForm').removeAttr("data-dismiss");
                }
                return false;
            }
        },
        error: function (error) {
            $("#Loader").hide();
            $('#btnSubmitForm').removeAttr("data-dismiss");
            alert(error.status + ' ' + error.statusText);
        }
    })
}
function fnValidateSubmitForms() {
    if ($("input[id*='chkOverwrite']").prop('checked') == true) {
        var arrayExtensions = ["docx", "pdf"];
        var specialChars = "#%&*:<>?/{|}";
        //alert("arrForms.lenght=" + arrForms.length);
        for (var x = 0; x < arrForms.length; x++) {
            var isValid = true;
            var txtUploadForm = $("input[id='txtUploadForm_" + arrForms[x] + "']").get(0);
            var formFile = txtUploadForm.files;
            if (formFile.length == 0) {
                isValid = false;
                $("#lblUploadForm_" + arrForms[x]).addClass('required');
                break;
            }

            if (formFile.length > 0) {
                for (j = 0; j < specialChars.length; j++) {
                    if (formFile[0].name.indexOf(specialChars[j]) > -1) {
                        $("#lblUploadForm_" + arrForms[x]).addClass('required');
                        alert("Special Characters [ # % & * : < > ? / { | } ] are not allowed in Upload Form \n\n\n" + formFile[0].name);
                        isValid = false;
                        break;
                    }
                }
                $("#lblUploadForm").removeClass('required');

                if ($.inArray(formFile[0].name.split('.').pop().toLowerCase(), arrayExtensions) == -1) {
                    $("#lblUploadForm_" + arrForms[x]).addClass('required');
                    alert("Only docx, pdf format is allowed in Upload Forms!");
                    isValid = false;
                    break;
                }
                else {
                    $("#lblUploadForm_" + arrForms[x]).removeClass('required');
                }
            }
        }
        if (!isValid) {
            alert("Only mentioned File Types (docx, pdf) are allowed in Upload Forms!");
            return false;
        }
    }
    else {
        if ($("select[id*='ddlForms']").val() == "0") {
            isValid = false;
            alert("Please Select Form Type to Submit");
            return false;
        }
        else {
            isValid = true;
        }
    }
    return isValid;
}
function fnValidateNullTrade() {
    if ($("textarea[id*='txtNullTradeRemarks']").val() == "") {
        $("#lblNullTradeRemarks").addClass('required');
        $("#btnSubmitNullTrade").removeAttr("data-dismiss");
        return false;
    }
    $("#nullTradeConfirmation").show();
}
function fnRemoveClass(obj, val) {
    $("#lbl" + val + "").removeClass('required');
}
function nullTrade() {
    $("#Loader").show();
    var test = new FormData();
    var preClearanceRequestId = $("#preClearanceRequestIdBN").val();
    var isNullTrade = true;
    test.append("Object", JSON.stringify({ PreClearanceRequestId: preClearanceRequestId, isNUllTrade: isNullTrade, nullTradeRemarks: $("textarea[id*='txtNullTradeRemarks']").val() }));
    var webUrl = uri + "/api/PreClearanceRequest/SaveBrokerNoteUpload";
    $.ajax({
        url: webUrl,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        contentType: false,
        //   async: false,
        processData: false,
        data: test,
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                alert(msg.Msg);
                if (msg.PreClearanceRequest.lstFormUrl != null) {
                    if (msg.PreClearanceRequest.lstFormUrl.length) {
                        for (var x = 0; x < msg.PreClearanceRequest.lstFormUrl.length; x++) {
                            //alert(msg.PreClearanceRequest.lstFormUrl[x]);
                            window.open("emailAttachment/" + msg.PreClearanceRequest.lstFormUrl[x]);
                        }
                    }
                }
                window.location.reload(true);
                //if (msg.PreClearanceRequest.PreClearanceRequestId == preClearanceRequestId) {
                //    $("input[id*='txtBrokerNoteId']").val(msg.PreClearanceRequest.brokerNoteId);
                //    if (msg.PreClearanceRequest.lstFormUrl != null) {
                //        $("select[id*='ddlForms']").empty();
                //        var strOption = "";
                //        var strHtml = "";
                //        arrForms = new Array();
                //        for (var x = 0; x < msg.PreClearanceRequest.lstFormUrl.length; x++) {
                //            var strValue = msg.PreClearanceRequest.lstFormUrl[x];
                //            strOption += strValue.split("~")[1] + " & ";
                //            arrForms.push(strValue.split("~")[0]);
                //            strHtml += '<div id="divForm_' + strValue.split("~")[0] + '">';
                //            strHtml += '<div class="col-md-4" id="lblUploadForm_' + strValue.split("~")[0] + '">' + strValue.split("~")[1] + '</div>';
                //            strHtml += '<div class="col-md-8">';
                //            strHtml += '<input type="file" id="txtUploadForm_' + strValue.split("~")[0] + '" class="form-control" data-tabindex="4" />';
                //            strHtml += '</div>';
                //            strHtml += '</div><br />';
                //        }
                //        var s = strOption.substr(0, strOption.length - 3);
                //        var sHtml = strHtml.substr(0, strHtml.length - 6);
                //        $("#divUploadForm").html(sHtml);

                //        ddlForms.append(new Option(s, "All"));
                //        if (msg.PreClearanceRequest.isNUllTrade) {
                //            fnDisplayNote(null, null, "Non Trade");
                //        }
                //        else {
                //            fnDisplayNote(null, null, "Trade");
                //        }
                //    }
                //    $("#btnCancelBrokerNote").trigger("click");
                //    $("#btnCancelNullTrade").trigger("click");
                //    $("#nullTradeConfirmation").hide();
                //    $("#btnOpenForm").trigger("click");
                //    $("#divForm").hide();
                //    $("#divAnnexure").show();
                //    //window.location.reload();
                //    //$("#tdIsBrokerNoteUploaded_" + msg.PreClearanceRequest.PreClearanceRequestId).html('Y');
                //    $("#liUploadbrokernote_" + msg.PreClearanceRequest.PreClearanceRequestId).hide();
                //}
                //fnClearFormBrokerNote();
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
            alert(response.status + ' ' + response.statusText);
        }
    });
}
function fnHideShow(val) {
    switch (val) {
        case "modalSubmitConfirmation":
            $("#modalSubmitConfirmation").hide();
            break;

        case "nullTradeConfirmation":
            $("#nullTradeConfirmation").hide();
            break;
    }
}
function fnEditBrokerNote(PreClearanceRequestId, BrokerNote, ActualTransactionDate, ValuePerShare, TotalAmount, TradeQuantity, ActualTradeQuantity, Status, TradeExchange, DematAccount, PreClearanceRequestedFor, TradeCompany, TransactionType, TradeDate, Remarks, proposedTransactionThrough, TradingFrom, TradingTo, RemainingTradeQuantity) {
    TradeFrom = TradingFrom;
    TradeTo = TradingTo;
    if (BrokerNote == 'null') {
        BrokerNote = '';
    }
    if (ActualTransactionDate == 'null') {
        ActualTransactionDate = '';
    }

    if (ValuePerShare == 'null') {
        ValuePerShare = '';
    }
    if (TotalAmount == 'null') {
        TotalAmount = '';
    }
    if (ActualTradeQuantity == 'null') {
        ActualTradeQuantity = '';
    }
    if (Remarks == 'null') {
        Remarks = '';
    }
    $("#ddlForBN").val(PreClearanceRequestedFor);
    $("#ddlRestrictedCompaniesBN").val(TradeCompany);
    $("#ddlTypeOfTransactionBN").val(TransactionType);
    $("#txtRequestedTransactionDateBN").val(TradeDate);
    $("#preClearanceRequestIdBN").val(PreClearanceRequestId);
    $("#txtBrokerNoteFileName").val(BrokerNote);
    $('#txtActualdateoftransaction').val(ActualTransactionDate);
    $('#txtValuePerShare').val(ValuePerShare);
    if (ActualTradeQuantity == null || ActualTradeQuantity == undefined || ActualTradeQuantity == '') {
        $('#txtTotalamount').val(ValuePerShare * TradeQuantity);
    }
    else {
        $('#txtTotalamount').val(ValuePerShare * ActualTradeQuantity);
    }
    $("#txtTradequantityBrokerNote").val(TradeQuantity);
    $("#txtActualTradequantityBrokerNote").val(ActualTradeQuantity);
    $("#txtTradeExchangeBN").val(TradeExchange);
    $("#txtStatusBN").val(Status);
    $("#txtDematAccountBN").val(DematAccount);
    $("#remarks").val(Remarks);
    // fnGetDematAccount(TradeExchange, "fnEditBrokerNote");
    fnGetDematAccount(PreClearanceRequestedFor, "fnEditBrokerNote");
    $("#ddlDematAccountBrokerNote").val(DematAccount);
    if (proposedTransactionThrough == "Off-Market Deal") {
        $("#dvExchangeTradedOn").hide();
        $("#dvBrokerDetails").show();
        BrokerDetailsManadatory = true;
    }
    if (Status == "InApproval") {
        //$("#btnSubmitBrokerNote").hide();
        //$("#btnBrokernote").prop("disabled", true);
        //$("#txtValuePerShare").prop("readonly", true);
        //$("#txtTradequantityBrokerNote").prop("readonly", true);
        //$("#txtTotalamount").prop("readonly", true);
        //$("#ddlTradeExchange").prop("disabled", true);
        //$("#ddlDematAccountBrokerNote").prop("disabled", true);
        //$("#txtActualdateoftransaction").prop("disabled", true);
        $("#btnSubmitBrokerNote").show();
        $("#btnBrokernote").prop("disabled", false);
        $("#txtValuePerShare").prop("readonly", false);
        //   $("#txtTradequantityBrokerNote").prop("readonly", false);
        $("#txtTotalamount").prop("readonly", true);
        //    $("#ddlTradeExchange").prop("disabled", false);
        $("#ddlDematAccountBrokerNote").prop("disabled", true);
        $("#txtActualdateoftransaction").prop("disabled", false);
    }
    else if (Status == "Draft") {
        $("#btnSubmitBrokerNote").show();
        $("#btnBrokernote").prop("disabled", false);
        $("#txtValuePerShare").prop("readonly", false);
        //  $("#txtTradequantityBrokerNote").prop("readonly", true);
        $("#txtTotalamount").prop("readonly", true);
        //  $("#ddlTradeExchange").prop("disabled", false);
        $("#ddlDematAccountBrokerNote").prop("disabled", true);
        $("#txtActualdateoftransaction").prop("disabled", false);
    }
    else if (Status == "Cancel") {
        $("#btnSubmitBrokerNote").show();
        $("#btnBrokernote").prop("disabled", false);
        $("#txtValuePerShare").prop("readonly", false);
        //     $("#txtTradequantityBrokerNote").prop("readonly", true);
        $("#txtTotalamount").prop("readonly", true);
        //  $("#ddlTradeExchange").prop("disabled", false);
        $("#ddlDematAccountBrokerNote").prop("disabled", true);
        $("#txtActualdateoftransaction").prop("disabled", false);
    }
    else {
        //do nothing
        $("#dvMultiTrade").show();
        $("#txtRemainingTradequantity").val(RemainingTradeQuantity);
        if (parseInt(RemainingTradeQuantity) > 0) {
            $("#modal-title-nulltrade").html('Please Submit Remarks in case of Null Trade for remaining quantity');
            $("#btnnulltrade").text('Click Here to Report Null Trade for (' + RemainingTradeQuantity + ')');
        }
        else {
            $("#modal-title-nulltrade").html('Please Submit Remarks in case of Null Trade');
            $("#btnnulltrade").text('Click Here to Report Null Trade');
        }
        fnMultiTradeAddRow();
    }
}
function fnValidateBrokerNote() {

    if ($("#ddlForBN").val() == undefined || $("#ddlForBN").val() == null || $("#ddlForBN").val() == "Select") {
        alert("Please fill all mandatory fields");
        return false;
    }
    if ($("#ddlRestrictedCompaniesBN").val() == undefined || $("#ddlRestrictedCompaniesBN").val() == null || $("#ddlRestrictedCompaniesBN").val() == "0") {
        alert("Please fill all mandatory fields");
        return false;
    }
    if ($("#ddlTypeOfTransactionBN").val() == undefined || $("#ddlTypeOfTransactionBN").val() == null || $("#ddlTypeOfTransactionBN").val() == "0") {
        alert("Please fill all mandatory fields");
        return false;
    }
    if ($('#txtRequestedTransactionDateBN').val() == undefined || $('#txtRequestedTransactionDateBN').val() == null || $('#txtRequestedTransactionDateBN').val().trim() == '') {
        alert("Please fill all mandatory fields");
        return false;
    }
    if ($('#txtTradequantityBrokerNote').val() == undefined || $('#txtTradequantityBrokerNote').val() == null || $('#txtTradequantityBrokerNote').val().trim() == '') {
        alert("Please fill all mandatory fields");
        return false;
    }
    //if ($('#txtActualTradequantityBrokerNote').val() == undefined || $('#txtActualTradequantityBrokerNote').val() == null || $('#txtActualTradequantityBrokerNote').val().trim() == '') {
    //    alert("Please fill all mandatory fields");
    //    return false;
    //}
    //if ($('#txtValuePerShare').val() == undefined || $('#txtValuePerShare').val() == null || $('#txtValuePerShare').val().trim() == '') {
    //    alert("Please fill all mandatory fields");
    //    return false;
    //}
    if ($('#ddlDematAccountBrokerNote').val() == undefined || $('#ddlDematAccountBrokerNote').val() == null || $('#ddlDematAccountBrokerNote').val().trim() == '' || $('#ddlDematAccountBrokerNote').val().trim() == '0') {
        alert("Please fill all mandatory fields");
        return false;
    }
    //if ($('#txtActualdateoftransaction').val() == undefined || $('#txtActualdateoftransaction').val() == null || $('#txtActualdateoftransaction').val().trim() == '') {
    //    alert("Please fill all mandatory fields");
    //    return false;
    //}
    //if ($('#remarks').val() == undefined || $('#remarks').val() == null || $('#remarks').val().trim() == '') {
    //    alert("Please fill all mandatory fields");
    //    return false;
    //}

    if (BrokerDetailsManadatory) {
        if ($('#txtareabrokerdetails').val() == undefined || $('#txtareabrokerdetails').val() == null || $('#txtareabrokerdetails').val().trim() == '') {
            alert("Please fill all mandatory fields");
            return false;
        }
    }

    return true;
}
function fnClearFormBrokerNote() {
    $('#btnBrokernote').val('');
    $('#txtTradequantityBrokerNote').val('');
    $('#txtActualTradequantityBrokerNote').val('');
    $('#txtValuePerShare').val('');
    $('#txtTotalamount').val('');
    $('#ddlDematAccountBrokerNote').val('');
    $('#txtActualdateoftransaction').val('');
    $('#remarks').val('');
    $("#ddlExchangeTradedOn").val('');
    $("#dvMultiTradeBN").hide();
    BrokerDetailsManadatory = false;
    $('#txtareabrokerdetails').val('');
    $("#dvBrokerDetails").hide();
    $('#txtRemainingTradequantity').val('');
    $("#btnnulltrade").attr('disabled', false);
}
function fnClearForm() {
    $("#preClearanceRequestId").val('');
    $("#ddlFor")[0].selectedIndex = 0;
    $("#ddlTypeOfSecurity")[0].selectedIndex = 0;
    //$("#ddlRestrictedCompanies").val('');
    $("#txtTradeQuantity").val('');
    $("#ddlTypeOfTransaction")[0].selectedIndex = 0;
    $("#ddlTradeExchange")[0].selectedIndex = 0;
    $("#ddlDematAccount")[0].selectedIndex = 0;
    //$("#txtRequestedTransactionDate").val('');
    $("#txtShareCurrentMarketPrice").val('');
    $("#ddlProposedTransactionThrough")[0].selectedIndex = 0;


    $("#ddlFor").prop("disabled", false);
    $("#ddlTypeOfSecurity").prop("disabled", false);
    $("#ddlRestrictedCompanies").prop("disabled", false);
    $("#txtTradeQuantity").prop("readonly", false);
    $("#ddlTypeOfTransaction").prop("disabled", false);
    $("#ddlTradeExchange").prop("disabled", false);
    $("#ddlDematAccount").prop("disabled", false);
    $("#txtRequestedTransactionDate").prop("disabled", false);
    $("#txtShareCurrentMarketPrice").prop("readonly", false);
    $("#ddlProposedTransactionThrough").prop("disabled", false);
    $("textarea[id='txtNullTradeRemarks']").val('');
    $("#ddlRestrictedCompanies")[0].selectedIndex = 0;
    //$("#tbdMultiTrade").html("");

}
function fnStatusChange() {
    $("#Loader").show();
    var webUrl = uri + "/api/PreClearanceRequest/GetPreClearanceRequestListFilterByStatus";
    $.ajax({
        url: webUrl,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        data: JSON.stringify({ Status: $("#bindStatus").val() }),
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                var result = "";

                if (msg.PreClearanceRequestList !== null) {
                    lstPreclearanceRequest = msg.PreClearanceRequestList;
                    //for (var i = 0; i < msg.PreClearanceRequestList.length; i++) {
                    //    userRole = msg.PreClearanceRequestList[i].userRole;
                    //    result += '<tr id="tr_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">';
                    //    if (msg.PreClearanceRequestList[i].PreClearanceRequestedForName == "") {
                    //        msg.PreClearanceRequestList[i].PreClearanceRequestedForName = "Self";
                    //    }
                    //    result += '<td></td>';
                    //    result += '<td id="tdPreClearanceRequestFor_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].PreClearanceRequestedForName + '</td>';
                    //    result += '<td id="tdRelationName_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].relationName + '</td>';
                    //    result += '<td id="tdTradeQuantity_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].TradeQuantity + '</td>';
                    //    result += '<td id="tdTypeOfTransaction_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].TransactionTypeName + '</td>';
                    //    result += '<td style="display:none;" id="tdTradeExchange_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].TradeExchangeName + '</td>';
                    //    result += '<td id="tdDematAccount_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].DematAccount + '</td>';
                    //    result += '<td id="tdRequestedTransactionDate_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].TradeDate + '</td>';
                    //    if (msg.PreClearanceRequestList[i].Status == "InApproval") {
                    //        result += '<td style="background-color:orange" id="tdPreClearanceRequestStatus_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].Status + '</td>';
                    //    }
                    //    else if (msg.PreClearanceRequestList[i].Status == "Approved") {
                    //        if (msg.PreClearanceRequestList[i].TradeDate == "") {
                    //            result += '<td style="background-color:red" id="tdPreClearanceRequestStatus_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">Pre-clearance Not Taken</td>';
                    //        }
                    //        else {
                    //            result += '<td style="background-color:green" id="tdPreClearanceRequestStatus_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].Status + '</td>';
                    //        }

                    //    }
                    //    else if (msg.PreClearanceRequestList[i].Status == "Draft") {
                    //        result += '<td id="tdPreClearanceRequestStatus_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].Status + '</td>';
                    //    }
                    //    else {
                    //        result += '<td style="background-color:red" id="tdPreClearanceRequestStatus_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].Status + '</td>';
                    //    }
                    //    //   result += '<td id="tdIsBrokerNoteUploaded_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].isBrokerNoteUploaded + '</td>';
                    //    result += '<td>' + msg.PreClearanceRequestList[i].reviewedOn + '</td>';
                    //    result += '<td>' + msg.PreClearanceRequestList[i].reviewedBy + '</td>';
                    //    result += '<td>' + msg.PreClearanceRequestList[i].reviewerRemarks + '</td>';
                    //    // result += '<td id="tdActions_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '"><a data-target="#stack1" data-toggle="modal" id="a_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '" class="btn btn-outline dark" onclick=\"javascript:fnEditDesignation(' + msg.PreClearanceRequestList[i].PreClearanceRequestId + ',\'' + msg.PreClearanceRequestList[i].DESIGNATION_NM + '\');\">Edit</a></td>';
                    //    result += '<td id="tdActions_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">';
                    //    var innerStr = "";
                    //    innerStr += '<div class="btn-group">';
                    //    innerStr += '<a class="btn blue dropdown-toggle" href="javascript:;" data-toggle="dropdown">';
                    //    innerStr += '<i class="fa fa-user"></i> Actions';
                    //    innerStr += '<i class="fa fa-angle-down"></i>';
                    //    innerStr += '</a>';
                    //    innerStr += '<ul class="dropdown-menu pull-right">';
                    //    if (msg.PreClearanceRequestList[i].Status != "Approved") {
                    //        innerStr += '<li>';
                    //        innerStr += '<a  id="btnEdit_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '" data-toggle="modal" data-target="#stack1" onclick=\"javascript:fnEditPreClearanceRequest(' + msg.PreClearanceRequestList[i].PreClearanceRequestId + ',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedForName + '\',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequestList[i].SecurityTypeName + '\',\'' + msg.PreClearanceRequestList[i].SecurityType + '\',\'' + msg.PreClearanceRequestList[i].TradeCompanyName + '\',\'' + msg.PreClearanceRequestList[i].TradeCompany + '\',\'' + msg.PreClearanceRequestList[i].TradeQuantity + '\',\'' + msg.PreClearanceRequestList[i].TransactionTypeName + '\',\'' + msg.PreClearanceRequestList[i].TransactionType + '\',\'' + msg.PreClearanceRequestList[i].TradeExchangeName + '\',\'' + msg.PreClearanceRequestList[i].TradeExchange + '\',\'' + msg.PreClearanceRequestList[i].DematAccount + '\',\'' + msg.PreClearanceRequestList[i].TradeDate + '\',\'' + msg.PreClearanceRequestList[i].Status + '\',\'' + msg.PreClearanceRequestList[i].shareCurrentMarketPrice + '\',\'' + msg.PreClearanceRequestList[i].proposedTransactionThrough + '\');\">';
                    //        innerStr += '<i class="fa fa-pencil"></i> Edit </a>';
                    //        innerStr += '</li>';
                    //    }
                    //    else {
                    //        innerStr += '<li style="display:none">';
                    //        innerStr += '<a  id="btnEdit_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '" data-toggle="modal" data-target="#stack1" onclick=\"javascript:fnEditPreClearanceRequest(' + msg.PreClearanceRequestList[i].PreClearanceRequestId + ',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedForName + '\',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequestList[i].SecurityTypeName + '\',\'' + msg.PreClearanceRequestList[i].SecurityType + '\',\'' + msg.PreClearanceRequestList[i].TradeCompanyName + '\',\'' + msg.PreClearanceRequestList[i].TradeCompany + '\',\'' + msg.PreClearanceRequestList[i].TradeQuantity + '\',\'' + msg.PreClearanceRequestList[i].TransactionTypeName + '\',\'' + msg.PreClearanceRequestList[i].TransactionType + '\',\'' + msg.PreClearanceRequestList[i].TradeExchangeName + '\',\'' + msg.PreClearanceRequestList[i].TradeExchange + '\',\'' + msg.PreClearanceRequestList[i].DematAccount + '\',\'' + msg.PreClearanceRequestList[i].TradeDate + '\',\'' + msg.PreClearanceRequestList[i].Status + '\',\'' + msg.PreClearanceRequestList[i].shareCurrentMarketPrice + '\',\'' + msg.PreClearanceRequestList[i].proposedTransactionThrough + '\');\">';
                    //        innerStr += '<i class="fa fa-pencil"></i> Edit </a>';
                    //        innerStr += '</li>';
                    //    }

                    //    //innerStr += '<li>';
                    //    //innerStr += '<a  id="btnCancelrequest_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '" onclick=\"javascript:fnEditPreClearanceRequest(' + msg.PreClearanceRequestList[i].PreClearanceRequestId + ',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedForName + '\',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequestList[i].SecurityTypeName + '\',\'' + msg.PreClearanceRequestList[i].SecurityType + '\',\'' + msg.PreClearanceRequestList[i].TradeCompanyName + '\',\'' + msg.PreClearanceRequestList[i].TradeCompany + '\',\'' + msg.PreClearanceRequestList[i].TradeQuantity + '\',\'' + msg.PreClearanceRequestList[i].TransactionTypeName + '\',\'' + msg.PreClearanceRequestList[i].TransactionType + '\',\'' + msg.PreClearanceRequestList[i].TradeExchangeName + '\',\'' + msg.PreClearanceRequestList[i].TradeExchange + '\',\'' + msg.PreClearanceRequestList[i].DematAccount + '\',\'' + msg.PreClearanceRequestList[i].TradeDate + '\',\'' + msg.PreClearanceRequestList[i].Status + '\');\">';
                    //    //innerStr += '<i class="fa fa-times"></i> Cancel Request </a>';
                    //    //innerStr += '</li>';
                    //    //msg.PreClearanceRequestList[i].Status == "InApproval" || 
                    //    if (msg.PreClearanceRequestList[i].Status == "Approved" && msg.PreClearanceRequestList[i].TradeDate.trim() != '') {
                    //        //if (msg.PreClearanceRequestList[i].isBrokerNoteUploaded != 'Y') {
                    //        //    innerStr += '<li id="liUploadbrokernote_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">';
                    //        //    innerStr += '<a  id="btnUploadbrokernote_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '" data-toggle="modal" data-target="#basic1" onclick=\"javascript:fnEditBrokerNote(' + msg.PreClearanceRequestList[i].PreClearanceRequestId + ',\'' + msg.PreClearanceRequestList[i].BrokerNote + '\',\'' + (msg.PreClearanceRequestList[i].ActualTransactionDate !== null ? msg.PreClearanceRequestList[i].ActualTransactionDate : msg.PreClearanceRequestList[i].ActualTransactionDate) + '\',\'' + msg.PreClearanceRequestList[i].ValuePerShare + '\',\'' + msg.PreClearanceRequestList[i].TotalAmount + '\',\'' + msg.PreClearanceRequestList[i].TradeQuantity + '\',\'' + msg.PreClearanceRequestList[i].ActualTradeQuantity + '\',\'' + msg.PreClearanceRequestList[i].Status + '\',\'' + msg.PreClearanceRequestList[i].TradeExchange + '\',\'' + msg.PreClearanceRequestList[i].DematAccount + '\',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequestList[i].TradeCompany + '\',\'' + msg.PreClearanceRequestList[i].TransactionType + '\',\'' + msg.PreClearanceRequestList[i].TradeDate + '\',\'' + msg.PreClearanceRequestList[i].remarks + '\');\">';
                    //        //    innerStr += '<i class="fa fa-upload"></i>Upload Trade Details </a>';
                    //        //    innerStr += '</li>';
                    //        //}
                    //        //else {
                    //        //    innerStr += '<li id="liUploadbrokernote_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '" style="display:none">';
                    //        //    innerStr += '<a  id="btnUploadbrokernote_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '" data-toggle="modal" data-target="#basic1" onclick=\"javascript:fnEditBrokerNote(' + msg.PreClearanceRequestList[i].PreClearanceRequestId + ',\'' + msg.PreClearanceRequestList[i].BrokerNote + '\',\'' + (msg.PreClearanceRequestList[i].ActualTransactionDate !== null ? msg.PreClearanceRequestList[i].ActualTransactionDate : msg.PreClearanceRequestList[i].ActualTransactionDate) + '\',\'' + msg.PreClearanceRequestList[i].ValuePerShare + '\',\'' + msg.PreClearanceRequestList[i].TotalAmount + '\',\'' + msg.PreClearanceRequestList[i].TradeQuantity + '\',\'' + msg.PreClearanceRequestList[i].ActualTradeQuantity + '\',\'' + msg.PreClearanceRequestList[i].Status + '\',\'' + msg.PreClearanceRequestList[i].TradeExchange + '\',\'' + msg.PreClearanceRequestList[i].DematAccount + '\',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequestList[i].TradeCompany + '\',\'' + msg.PreClearanceRequestList[i].TransactionType + '\',\'' + msg.PreClearanceRequestList[i].TradeDate + '\',\'' + msg.PreClearanceRequestList[i].remarks + '\');\">';
                    //        //    innerStr += '<i class="fa fa-upload"></i>Upload Trade Details </a>';
                    //        //    innerStr += '</li>';
                    //        //}
                    //        innerStr += '<li id="liUploadbrokernote_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">';
                    //        innerStr += '<a  id="btnUploadbrokernote_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '" data-toggle="modal" data-target="#basic1" onclick=\"javascript:fnEditBrokerNote(' + msg.PreClearanceRequestList[i].PreClearanceRequestId + ',\'' + msg.PreClearanceRequestList[i].BrokerNote + '\',\'' + (msg.PreClearanceRequestList[i].ActualTransactionDate !== null ? msg.PreClearanceRequestList[i].ActualTransactionDate : msg.PreClearanceRequestList[i].ActualTransactionDate) + '\',\'' + msg.PreClearanceRequestList[i].ValuePerShare + '\',\'' + msg.PreClearanceRequestList[i].TotalAmount + '\',\'' + msg.PreClearanceRequestList[i].TradeQuantity + '\',\'' + msg.PreClearanceRequestList[i].ActualTradeQuantity + '\',\'' + msg.PreClearanceRequestList[i].Status + '\',\'' + msg.PreClearanceRequestList[i].TradeExchange + '\',\'' + msg.PreClearanceRequestList[i].DematAccount + '\',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequestList[i].TradeCompany + '\',\'' + msg.PreClearanceRequestList[i].TransactionType + '\',\'' + msg.PreClearanceRequestList[i].TradeDate + '\',\'' + msg.PreClearanceRequestList[i].remarks + '\');\">';
                    //        innerStr += '<i class="fa fa-upload"></i>Upload Trade Details </a>';
                    //        innerStr += '</li>';
                    //    }
                    //    else {
                    //        innerStr += '<li id="liUploadbrokernote_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '" style="display:none">';
                    //        innerStr += '<a  id="btnUploadbrokernote_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '" data-toggle="modal" data-target="#basic1" onclick=\"javascript:fnEditBrokerNote(' + msg.PreClearanceRequestList[i].PreClearanceRequestId + ',\'' + msg.PreClearanceRequestList[i].BrokerNote + '\',\'' + (msg.PreClearanceRequestList[i].ActualTransactionDate !== null ? msg.PreClearanceRequestList[i].ActualTransactionDate : msg.PreClearanceRequestList[i].ActualTransactionDate) + '\',\'' + msg.PreClearanceRequestList[i].ValuePerShare + '\',\'' + msg.PreClearanceRequestList[i].TotalAmount + '\',\'' + msg.PreClearanceRequestList[i].TradeQuantity + '\',\'' + msg.PreClearanceRequestList[i].ActualTradeQuantity + '\',\'' + msg.PreClearanceRequestList[i].Status + '\',\'' + msg.PreClearanceRequestList[i].TradeExchange + '\',\'' + msg.PreClearanceRequestList[i].DematAccount + '\',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequestList[i].TradeCompany + '\',\'' + msg.PreClearanceRequestList[i].TransactionType + '\',\'' + msg.PreClearanceRequestList[i].TradeDate + '\',\'' + msg.PreClearanceRequestList[i].remarks + '\');\">';
                    //        innerStr += '<i class="fa fa-upload"></i>Upload Trade Details </a>';
                    //        innerStr += '</li>';
                    //    }

                    //    innerStr += '<li id="liZippedTradeFilesDownload_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">';
                    //    innerStr += '<a href="javascript:fnDownloadTradingZipFile(\'' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '\');"  id="btnZippedTradeFilesDownload_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">';
                    //    innerStr += '<i class="fa fa-download"></i>Download Trade Files</a>';
                    //    innerStr += '</li>';
                    //    innerStr += '<li class="divider"> </li>';
                    //    innerStr += '</ul>';
                    //    innerStr += '</div>';
                    //    //if (msg.PreClearanceRequestList[i].Status == "Approved" || msg.PreClearanceRequestList[i].Status == "Rejected") {
                    //    //    result += '</td>';
                    //    //}
                    //    //else {
                    //    //    result += innerStr + '</td>';
                    //    //}
                    //    result += innerStr + '</td>';
                    //    result += '</tr>';
                    //}
                    for (var i = 0; i < msg.PreClearanceRequestList.length; i++) {
                        userRole = msg.PreClearanceRequestList[i].userRole;
                        var ReqId = (msg.PreClearanceRequestList[i].PreClearanceRequestId + "").padStart(4, "0");
                        result += '<tr id="tr_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">';
                        if (msg.PreClearanceRequestList[i].PreClearanceRequestedForName == "") {
                            msg.PreClearanceRequestList[i].PreClearanceRequestedForName = "Self";
                        }
                        result += '<td></td>';
                        result += '<td id="tdActions_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">';
                        var innerStr = "";
                        innerStr += '<div class="btn-group">';
                        innerStr += '<a class="btn blue dropdown-toggle" href="javascript:;" data-toggle="dropdown">';
                        innerStr += '<i class="fa fa-user"></i> Actions';
                        innerStr += '<i class="fa fa-angle-down"></i>';
                        innerStr += '</a>';
                        innerStr += '<ul class="dropdown-menu">';
                        if (msg.PreClearanceRequestList[i].Status != "Approved" && msg.PreClearanceRequestList[i].Status != "Rejected") {
                            innerStr += '<li>';
                            innerStr += '<a  id="btnEdit_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '" data-toggle="modal" data-target="#stack1" onclick=\"javascript:fnEditPreClearanceRequest(' + msg.PreClearanceRequestList[i].PreClearanceRequestId + ',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedForName + '\',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequestList[i].SecurityTypeName + '\',\'' + msg.PreClearanceRequestList[i].SecurityType + '\',\'' + msg.PreClearanceRequestList[i].TradeCompanyName + '\',\'' + msg.PreClearanceRequestList[i].TradeCompany + '\',\'' + msg.PreClearanceRequestList[i].TradeQuantity + '\',\'' + msg.PreClearanceRequestList[i].TransactionTypeName + '\',\'' + msg.PreClearanceRequestList[i].TransactionType + '\',\'' + msg.PreClearanceRequestList[i].TradeExchangeName + '\',\'' + msg.PreClearanceRequestList[i].TradeExchange + '\',\'' + msg.PreClearanceRequestList[i].DematAccount + '\',\'' + msg.PreClearanceRequestList[i].TradeDate + '\',\'' + msg.PreClearanceRequestList[i].Status + '\',\'' + msg.PreClearanceRequestList[i].shareCurrentMarketPrice + '\',\'' + msg.PreClearanceRequestList[i].proposedTransactionThrough + '\');\">';
                            innerStr += '<i class="fa fa-pencil"></i> View/Withdraw Request </a>';
                            innerStr += '</li>';
                        }
                        else {
                            innerStr += '<li style="display:none">';
                            innerStr += '<a  id="btnEdit_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '" data-toggle="modal" data-target="#stack1" onclick=\"javascript:fnEditPreClearanceRequest(' + msg.PreClearanceRequestList[i].PreClearanceRequestId + ',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedForName + '\',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequestList[i].SecurityTypeName + '\',\'' + msg.PreClearanceRequestList[i].SecurityType + '\',\'' + msg.PreClearanceRequestList[i].TradeCompanyName + '\',\'' + msg.PreClearanceRequestList[i].TradeCompany + '\',\'' + msg.PreClearanceRequestList[i].TradeQuantity + '\',\'' + msg.PreClearanceRequestList[i].TransactionTypeName + '\',\'' + msg.PreClearanceRequestList[i].TransactionType + '\',\'' + msg.PreClearanceRequestList[i].TradeExchangeName + '\',\'' + msg.PreClearanceRequestList[i].TradeExchange + '\',\'' + msg.PreClearanceRequestList[i].DematAccount + '\',\'' + msg.PreClearanceRequestList[i].TradeDate + '\',\'' + msg.PreClearanceRequestList[i].Status + '\',\'' + msg.PreClearanceRequestList[i].shareCurrentMarketPrice + '\',\'' + msg.PreClearanceRequestList[i].proposedTransactionThrough + '\');\">';
                            innerStr += '<i class="fa fa-pencil"></i> View/Withdraw Request </a>';
                            innerStr += '</li>';
                        }

                        //innerStr += '<li>';
                        //innerStr += '<a  id="btnCancelrequest_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '" onclick=\"javascript:fnEditPreClearanceRequest(' + msg.PreClearanceRequestList[i].PreClearanceRequestId + ',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedForName + '\',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequestList[i].SecurityTypeName + '\',\'' + msg.PreClearanceRequestList[i].SecurityType + '\',\'' + msg.PreClearanceRequestList[i].TradeCompanyName + '\',\'' + msg.PreClearanceRequestList[i].TradeCompany + '\',\'' + msg.PreClearanceRequestList[i].TradeQuantity + '\',\'' + msg.PreClearanceRequestList[i].TransactionTypeName + '\',\'' + msg.PreClearanceRequestList[i].TransactionType + '\',\'' + msg.PreClearanceRequestList[i].TradeExchangeName + '\',\'' + msg.PreClearanceRequestList[i].TradeExchange + '\',\'' + msg.PreClearanceRequestList[i].DematAccount + '\',\'' + msg.PreClearanceRequestList[i].TradeDate + '\',\'' + msg.PreClearanceRequestList[i].Status + '\');\">';
                        //innerStr += '<i class="fa fa-times"></i> Cancel Request </a>';
                        //innerStr += '</li>';
                        //msg.PreClearanceRequestList[i].Status == "InApproval" || 
                        if (msg.PreClearanceRequestList[i].Status == "Approved" && msg.PreClearanceRequestList[i].TradeDate.trim() != '') {
                            innerStr += '<li id="liUploadbrokernote_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">';
                            innerStr += '<a  id="btnUploadbrokernote_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '" data-toggle="modal" data-target="#basic1" onclick=\"javascript:fnEditBrokerNote(' + msg.PreClearanceRequestList[i].PreClearanceRequestId + ',\'' + msg.PreClearanceRequestList[i].BrokerNote + '\',\'' + (msg.PreClearanceRequestList[i].ActualTransactionDate !== null ? msg.PreClearanceRequestList[i].ActualTransactionDate : msg.PreClearanceRequestList[i].ActualTransactionDate) + '\',\'' + msg.PreClearanceRequestList[i].ValuePerShare + '\',\'' + msg.PreClearanceRequestList[i].TotalAmount + '\',\'' + msg.PreClearanceRequestList[i].TradeQuantity + '\',\'' + msg.PreClearanceRequestList[i].ActualTradeQuantity + '\',\'' + msg.PreClearanceRequestList[i].Status + '\',\'' + msg.PreClearanceRequestList[i].TradeExchange + '\',\'' + msg.PreClearanceRequestList[i].DematAccount + '\',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequestList[i].TradeCompany + '\',\'' + msg.PreClearanceRequestList[i].TransactionType + '\',\'' + msg.PreClearanceRequestList[i].TradeDate + '\',\'' + msg.PreClearanceRequestList[i].remarks + '\');\">';
                            innerStr += '<i class="fa fa-upload"></i>Upload Trade Details </a>';
                            innerStr += '</li>';
                        }
                        else {
                            innerStr += '<li id="liUploadbrokernote_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '" style="display:none">';
                            innerStr += '<a  id="btnUploadbrokernote_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '" data-toggle="modal" data-target="#basic1" onclick=\"javascript:fnEditBrokerNote(' + msg.PreClearanceRequestList[i].PreClearanceRequestId + ',\'' + msg.PreClearanceRequestList[i].BrokerNote + '\',\'' + (msg.PreClearanceRequestList[i].ActualTransactionDate !== null ? msg.PreClearanceRequestList[i].ActualTransactionDate : msg.PreClearanceRequestList[i].ActualTransactionDate) + '\',\'' + msg.PreClearanceRequestList[i].ValuePerShare + '\',\'' + msg.PreClearanceRequestList[i].TotalAmount + '\',\'' + msg.PreClearanceRequestList[i].TradeQuantity + '\',\'' + msg.PreClearanceRequestList[i].ActualTradeQuantity + '\',\'' + msg.PreClearanceRequestList[i].Status + '\',\'' + msg.PreClearanceRequestList[i].TradeExchange + '\',\'' + msg.PreClearanceRequestList[i].DematAccount + '\',\'' + msg.PreClearanceRequestList[i].PreClearanceRequestedFor + '\',\'' + msg.PreClearanceRequestList[i].TradeCompany + '\',\'' + msg.PreClearanceRequestList[i].TransactionType + '\',\'' + msg.PreClearanceRequestList[i].TradeDate + '\',\'' + msg.PreClearanceRequestList[i].remarks + '\');\">';
                            innerStr += '<i class="fa fa-upload"></i>Upload Trade Details </a>';
                            innerStr += '</li>';
                        }

                        innerStr += '<li id="liZippedTradeFilesDownload_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">';
                        innerStr += '<a href="javascript:fnDownloadTradingZipFile(\'' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '\');"  id="btnZippedTradeFilesDownload_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">';
                        innerStr += '<i class="fa fa-download"></i>Download Trade Files</a>';
                        innerStr += '</li>';
                        innerStr += '<li class="divider"> </li>';
                        innerStr += '</ul>';
                        innerStr += '</div>';
                        //if (msg.PreClearanceRequestList[i].Status == "Approved" || msg.PreClearanceRequestList[i].Status == "Rejected") {
                        //    result += '</td>';
                        //}
                        //else {
                        //    result += innerStr + '</td>';
                        //}
                        result += innerStr + '</td>';
                        result += '<td id="tdPreClearanceRequestId_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + ReqId + '</td>';
                        result += '<td id="tdPreClearanceRequestFor_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].PreClearanceRequestedForName + '</td>';
                        result += '<td id="tdRelationName_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].relationName + '</td>';
                        result += '<td id="tdTradeQuantity_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].TradeQuantity + '</td>';
                        result += '<td id="tdTypeOfTransaction_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].TransactionTypeName + '</td>';
                        result += '<td style="display:none;" id="tdTradeExchange_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].TradeExchangeName + '</td>';
                        result += '<td  style="display: none;" id="tdDematAccount_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].DematAccount + '</td>';
                        result += '<td id="tdRequestedTransactionDate_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].TradeDate + '</td>';
                        if (msg.PreClearanceRequestList[i].Status == "InApproval") {
                            msg.PreClearanceRequestList[i].Status = "Pending for  Approval";
                            result += '<td style="background-color:orange" id="tdPreClearanceRequestStatus_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].Status + '</td>';
                        }
                        else if (msg.PreClearanceRequestList[i].Status == "Approved") {
                            if (msg.PreClearanceRequestList[i].TradeDate == "") {
                                result += '<td style="background-color:red" id="tdPreClearanceRequestStatus_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">Pre-clearance Not Taken</td>';
                            }
                            else {
                                result += '<td style="background-color:green" id="tdPreClearanceRequestStatus_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].Status + '</td>';
                            }
                        }
                        else if (msg.PreClearanceRequestList[i].Status == "Draft") {
                            result += '<td id="tdPreClearanceRequestStatus_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].Status + '</td>';
                        }
                        else {
                            result += '<td style="background-color:red" id="tdPreClearanceRequestStatus_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].Status + '</td>';
                        }
                        result += '<td>' + msg.PreClearanceRequestList[i].reviewedOn + '</td>';
                        result += '<td>' + msg.PreClearanceRequestList[i].reviewedBy + '</td>';
                        result += '<td>' + msg.PreClearanceRequestList[i].reviewerRemarks + '</td>';

                        result += '</tr>';
                    }

                }

                var table = $('#tbl-preclearance-setup').DataTable();
                table.destroy();
                $("#tbdPreClearanceList").html(result);
                initializeDataTable();
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
            alert(response.status + ' ' + response.statusText);
        }
    })
}
function fnGetDematCurrentHoldings(caccountno) {
    var result = arrDematAccount;
    if (result != null) {

        result.forEach(function (e) {
            if (caccountno == e.accountNo) {
                $("input[id*='txtcurrentholding']").val(e.CurrentHolding);
                $("#spncurrentholding").html(e.CurrentHolding);
            }
        })
    }
}
function fnGetDetailsOfUser() {
    $("#Loader").show();
    var webUrl = uri + "/api/PreClearanceRequest/GetRelativeDetailListBN";
    $.ajax({
        url: webUrl,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        data: {},
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                arrDetails = new Array();
                if (msg.RelativeDetailList != null) {
                    for (var i = 0; i < msg.RelativeDetailList.length; i++) {
                        var obj = new Object();
                        obj.RelativeId = msg.RelativeDetailList[i].ID;
                        obj.RelativeNm = msg.RelativeDetailList[i].relativeName;
                        obj.Pan = msg.RelativeDetailList[i].panNumber;

                        var Demat = new Array();
                        //alert("msg.RelativeDetailList[" + i + "].lstDematAccount.length=" + msg.RelativeDetailList[i].lstDematAccount.length);
                        if (msg.RelativeDetailList[i].lstDematAccount != null) {
                            for (var j = 0; j < msg.RelativeDetailList[i].lstDematAccount.length; j++) {
                                var objdemat = new Object();
                                //alert("msg.RelativeDetailList[" + i + "].lstDematAccount[" + j + "].accountNo=" + msg.RelativeDetailList[i].lstDematAccount[j].accountNo);
                                objdemat.accountNo= msg.RelativeDetailList[i].lstDematAccount[j].accountNo;
                                objdemat.CurrentHolding = msg.RelativeDetailList[i].lstDematAccount[j].CurrentHolding;
                                Demat.push(objdemat);
                            }
                            obj.Demat = Demat;
                            arrDetails.push(obj);
                        }
                    }
                }
            }
            else {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    //alert(msg.Msg);
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
function fnSubmitPreClearanceRequestOther() {
    if (fnValidateOtherSecurity()) {
        $("#Loader").show();
        var preClearanceRequestFor = $('#ddlForOther').val();
        var securityType = $('#ddlTypeOfSecurityOther').val();
        var transactionType = $('#ddlTypeOfTransactionOther').val();
        var restrictedCompanyId = $('#ddlRestrictedCompaniesOther').val();
        var tradeQuantity = $('#txtTradeQuantityOther').val();
        var requestedTransactionDate = $('#txtRequestedTransactionDateOther').val();
        var shareCurrentMarketPrice = $('#txtShareCurrentMarketPriceOther').val();
        var proposedTransactionThrough = $('#ddlProposedTransactionThroughOther').val();

        var webUrl = uri + "/api/PreClearanceRequest/SavePreClearanceRequestOtherCompany";
        $.ajax({
            url: webUrl,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            // async: false,
            data: JSON.stringify({

                PreClearanceRequestedFor: preClearanceRequestFor,
                SecurityType: securityType,
                TradeCompany: restrictedCompanyId,
                TradeQuantity: tradeQuantity,
                TransactionType: transactionType,
                TradeDate: requestedTransactionDate,
                shareCurrentMarketPrice: shareCurrentMarketPrice,
                proposedTransactionThrough: proposedTransactionThrough

            }),
            success: function (msg) {
                $("#Loader").hide();
                if (isJson(msg)) {
                    msg = JSON.parse(msg);
                }
                if (msg.StatusFl == true) {
                    alert(msg.Msg);
                    $('#ModalPreclaranceRequestOtherCompany').modal('hide');
                    window.location.reload(true);

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
                alert(response.status + ' ' + response.statusText);
            }
        })
    }
}
function fnValidateOtherSecurity() {

    if ($("#ddlForOther").val() == undefined || $("#ddlForOther").val() == null || $("#ddlForOther").val() == "Select") {
        alert("Please fill all mandatory fields");
        return false;
    }
    if ($("#ddlTypeOfSecurityOther").val() == undefined || $("#ddlTypeOfSecurityOther").val() == null || $("#ddlTypeOfSecurityOther").val() == "0") {
        alert("Please fill all mandatory fields");
        return false;
    }
    if ($("#ddlRestrictedCompaniesOther").val() == undefined || $("#ddlRestrictedCompaniesOther").val() == null || $("#ddlRestrictedCompaniesOther").val() == "0") {
        alert("Please fill all mandatory fields");
        return false;
    }

    if ($('#txtTradeQuantityOther').val() == undefined || $('#txtTradeQuantityOther').val() == null || $('#txtTradeQuantityOther').val().trim() == '') {
        alert("Please fill all mandatory fields");
        return false;
    }
    if ($("#ddlTypeOfTransactionOther").val() == undefined || $("#ddlTypeOfTransactionOther").val() == null || $("#ddlTypeOfTransactionOther").val() == "0") {
        alert("Please fill all mandatory fields");
        return false;
    }
    if ($('#txtRequestedTransactionDateOther').val() == undefined || $('#txtRequestedTransactionDateOther').val() == null || $('#txtRequestedTransactionDateOther').val().trim() == '') {
        alert("Please fill all mandatory fields");
        return false;
    }

    if ($('#txtShareCurrentMarketPriceOther').val() == undefined || $('#txtShareCurrentMarketPriceOther').val() == null || $('#txtShareCurrentMarketPriceOther').val().trim() == '') {
        alert("Please fill all mandatory fields");
        return false;
    }

    return true;
}
function fnGetPreClearanceRequestOther() {
    $("#Loader").show();
    var webUrl = uri + "/api/PreClearanceRequest/GetPreClearanceRequestOtherCompanyList";
    $.ajax({
        url: webUrl,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        data: {},
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                var result = "";
                lstPreclearanceRequest = msg.PreClearanceRequestList;
                for (var i = 0; i < msg.PreClearanceRequestList.length; i++) {
                    result += '<tr id="tr_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">';
                    if (msg.PreClearanceRequestList[i].PreClearanceRequestedForName == "") {
                        msg.PreClearanceRequestList[i].PreClearanceRequestedForName = "Self";
                    }
                    var RowNo = i + 1;
                    result += '<td>' + RowNo + '</td>';
                    result += '<td id="tdPreClearanceRequestFor_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].PreClearanceRequestedForName + '</td>';
                    result += '<td id="tdRelationName_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].relationName + '</td>';
                    result += '<td id="tdTradeQuantity_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].TradeQuantity + '</td>';
                    result += '<td id="tdTypeOfTransaction_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].TransactionTypeName + '</td>';
                    result += '<td id="tdRequestedTransactionDate_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].TradeDate + '</td>';
                    if (msg.PreClearanceRequestList[i].Status == "Approved") {
                        result += '<td style="background-color:green" id="tdPreClearanceRequestStatus_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].Status + '</td>';
                    }

                    else {
                        result += '<td style="background-color:red" id="tdPreClearanceRequestStatus_' + msg.PreClearanceRequestList[i].PreClearanceRequestId + '">' + msg.PreClearanceRequestList[i].Status + '</td>';
                    }
                    result += '<td>' + msg.PreClearanceRequestList[i].reviewedOn + '</td>';
                    result += '<td>' + msg.PreClearanceRequestList[i].reviewedBy + '</td>';

                    result += '</tr>';
                }

                var table = $('#tbl-preclearance-other-setup').DataTable();
                table.destroy();
                $("#tbdPreClearanceOtherList").html(result);
                initializeDataTable1();
            }
            else {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    //alert(msg.Msg);
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
function fnForTD_onChange(cntrl) {
    var obj = $(cntrl).closest('tr');
    var relative = $(cntrl).val();
    var strOption = "";
    var iCntr = 0;
    //alert(relative)
    if (relative != "-1") {
        $("#btnSubmitTD").text("Submit/Update holdings");
    }
    else {
        $("#btnSubmitTD").text("Skip");
    }
    for (var x = 0; x < arrDetails.length; x++) {
        if (arrDetails[x].RelativeId == relative) {
            $($($(obj).children()[1]).children()[0]).val(arrDetails[x].Pan);

            //$("#txtTDPan" + id).val(arrDetails[x].Pan);
            for (var y = 0; y < arrDetails[x].Demat.length; y++) {
                strOption += "<option value='" + arrDetails[x].Demat[y].accountNo + "'>" + arrDetails[x].Demat[y].accountNo + "</option>";
                iCntr++;
            }
        }
    }
    if (iCntr == 1) {
        $($($(obj).children()[2]).children()[0]).html(strOption);
        //$("#ddlDematTD" + id).html(strOption);
    }
    else {
        var str = "<option value=''>Please Select</option>" + strOption;
        $($($(obj).children()[2]).children()[0]).html(str);
        //$("#ddlDematTD" + id).html(str);
    }
}
function fnAddRow() {
    $("#tbdTrade").html("");
    var str = "";
    str += "<tr>";
    str += "<td style='padding-left:5px;padding-right:5px;padding-top:10px;'>";
    str += "<select id='ddlForTD' class='form-control' onchange='javascript:fnForTD_onChange(this);'>";
    str += "<option value='-1'>Please Select</option>";

    for (var x = 0; x < arrDetails.length; x++) {
        str += "<option value='" + arrDetails[x].RelativeId + "'>" + arrDetails[x].RelativeNm + "</option>";
    }
    str += "</select>";
    str += "</td>";
    str += "<td id='tdPanTD' style='padding-left:5px;padding-right:5px;padding-top:10px;'>";
    str += "<input id='txtTDPan' disabled type='text' class='form-control' />";
    str += "</td>";
    str += "<td id='tdDematTD' style='padding-left:5px;padding-right:5px;padding-top:10px;'>";
    str += "<select id='ddlDematTD' class='form-control'>";
    str += "<option value='-1'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</option>";
    str += "</select>";
    str += "</td>";
    str += "<td style='padding-left:5px;padding-right:5px;padding-top:10px;'>";
    str += "<input id='txtTDDate' type='text' class='form-control bg-white' />";
    str += "</td>";
    str += "<td style='padding-left:5px;padding-right:5px;padding-top:10px;'>";
    str += "<select id='ddlTypeForTD' class='form-control'>";
    str += "<option value='-1'>Please Select</option>";
    for (var x = 0; x < arrType.length; x++) {
        str += "<option value='" + arrType[x].Id + "'>" + arrType[x].Name + "</option>";
    }
    str += "</select>";
    str += "</td>";
    str += "<td style='padding-left:5px;padding-right:5px;padding-top:10px;'>";
    str += "<input id='txtTDQty' type='number' class='form-control' />";
    str += "</td>";
    str += "<td style='padding-left:5px;padding-right:5px;padding-top:10px;'>";
    str += "<input id='txtTDValue' type='text' class='form-control' />";
    str += "</td>";

    str += "<td style='padding-left:5px;padding-right:5px;padding-top:10px;'>";
    str += "<select id='ddlProposedTransactionThrough' class='form-control'>";
    str += "<option value=''>--Select--</option>";
    str += "<option value='Stock Exchange'>Stock Exchange</option>";
    str += "<option value='Off-Market Deal'>Off-Market Deal</option>";
    str += "</select>";
    str += "</td>";

    str += "<td style='padding-left:5px;padding-right:5px;padding-top:10px;'>";
    str += "<select id='ddlExchangeTradedOn' class='form-control'>";
    str += "<option value=''>--Select--</option>";
    str += "<option value='BSE'>BSE</option>";
    str += "<option value='NSE'>NSE</option>";
    str += "</select>";
    str += "</td>";

    //str += "<td style='padding-left:5px;padding-right:5px;padding-top:10px;'>";
    //str += "<input id='txtTDAmount' type='text' class='form-control' />";
    //str += "</td>";
    str += "<td style='padding-left:5px;padding-right:5px;width:5%;padding-top:10px;'>";
    str += "<a onclick='javascript:fnAddNewRow();'>";
    str += "<i class='fa fa-plus'></i>";
    //str += "</a>&nbsp;&nbsp;";
    //str += "<a onclick='javascript:fnRemoveRow(this);'>";
    //str += "<i class='fa fa-minus'></i>";
    //str += "</a>";
    str += "</td>";
    str += "</tr>";
    $("#tbdTrade").append(str);

    fnSetDate();
}
function fnAddNewRow() {
    var str = "";
    str += "<tr>";
    str += "<td style='padding-left:5px;padding-right:5px;padding-top:10px;'>";
    str += "<select id='ddlForTD' class='form-control' onchange='javascript:fnForTD_onChange(this);'>";
    str += "<option value='-1'>Please Select</option>";

    for (var x = 0; x < arrDetails.length; x++) {
        str += "<option value='" + arrDetails[x].RelativeId + "'>" + arrDetails[x].RelativeNm + "</option>";
    }
    str += "</select>";
    str += "</td>";
    str += "<td id='tdPanTD' style='padding-left:5px;padding-right:5px;padding-top:10px;'>";
    str += "<input id='txtTDPan' disabled type='text' class='form-control' />";
    str += "</td>";
    str += "<td id='tdDematTD' style='padding-left:5px;padding-right:5px;padding-top:10px;'>";
    str += "<select id='ddlDematTD' class='form-control'>";
    str += "<option value='-1'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</option>";
    str += "</select>";
    str += "</td>";
    str += "<td style='padding-left:5px;padding-right:5px;padding-top:10px;'>";
    str += "<input id='txtTDDate' type='text' class='form-control' />";
    str += "</td>";
    str += "<td style='padding-left:5px;padding-right:5px;padding-top:10px;'>";
    str += "<select id='ddlTypeForTD' class='form-control'>";
    str += "<option value='-1'>Please Select</option>";
    for (var x = 0; x < arrType.length; x++) {
        str += "<option value='" + arrType[x].Id + "'>" + arrType[x].Name + "</option>";
    }
    str += "</select>";
    str += "</td>";
    str += "<td style='padding-left:5px;padding-right:5px;padding-top:10px;'>";
    str += "<input id='txtTDQty' type='number' class='form-control' />";
    str += "</td>";
    str += "<td style='padding-left:5px;padding-right:5px;padding-top:10px;'>";
    str += "<input id='txtTDValue' type='text' class='form-control' />";
    str += "</td>";

    str += "<td style='padding-left:5px;padding-right:5px;padding-top:10px;'>";
    str += "<select id='ddlProposedTransactionThrough' class='form-control'>";
    str += "<option value=''>--Select--</option>";
    str += "<option value='Stock Exchange'>Stock Exchange</option>";
    str += "<option value='Off-Market Deal'>Off-Market Deal</option>";
    str += "</select>";
    str += "</td>";

    str += "<td style='padding-left:5px;padding-right:5px;padding-top:10px;'>";
    str += "<select id='ddlExchangeTradedOn' class='form-control'>";
    str += "<option value=''>--Select--</option>";
    str += "<option value='BSE'>BSE</option>";
    str += "<option value='NSE'>NSE</option>";
    str += "</select>";
    str += "</td>";
    //str += "<td style='padding-left:5px;padding-right:5px;padding-top:10px;'>";
    //str += "<input id='txtTDAmount' type='text' class='form-control' />";
    //str += "</td>";
    str += "<td style='padding-left:5px;padding-right:5px;width:5%;padding-top:10px;'>";
    str += "<a onclick='javascript:fnAddNewRow();'>";
    str += "<i class='fa fa-plus'></i>";
    str += "</a>&nbsp;&nbsp;";
    str += "<a onclick='javascript:fnRemoveRow(this);'>";
    str += "<i class='fa fa-minus'></i>";
    str += "</a>";
    str += "</td>";
    str += "</tr>";
    $("#tbdTrade").append(str);

    fnSetDate();
}
function fnRemoveRow(cntrl) {
    var obj = $(cntrl).closest('tr');
    $(obj).remove();
    var flag = false;
    for (var i = 0; i < $("#tbdTrade").children().length; i++) {
        var sRelativeId = $($($($("#tbdTrade").children()[i]).children()[0]).children()[0]).val();
        if (sRelativeId != "-1") {
            flag = true;
        }
    }
    if (flag == false) {
        $("#btnSubmitTD").text("Skip");
    }
}
function fnSetDate() {
    //alert("In function fnSetDate()");
    var BPDate = $("input[id*='HiddenLastBenposDate']").val();
    //alert(BPDate);
    $("[id='txtTDDate']").each(function () {
        $(this).datepicker({
            todayHighlight: true,
            autoclose: true,
            format: "dd/mm/yyyy",
            clearBtn: true,
            startDate: BPDate,
            endDate: "today",
            daysOfWeekDisabled: [0, 6]
        }).attr('readonly', 'readonly');
    })
    //$("#txtTDDate").datepicker({
    //    todayHighlight: true,
    //    autoclose: true,
    //    format: "dd/mm/yyyy",
    //    clearBtn: true,
    //    endDate: "today",
    //    daysOfWeekDisabled: [0, 6]
    //}).attr('readonly', 'readonly');
}
function fnSaveBrokerNote() {

    if ($("#tbdTrade").children().length > 0) {
        if (fnValidateBrokerNoteList()) {
            var BNList = new Array();
            for (var i = 0; i < $("#tbdTrade").children().length; i++) {
                var bNote = new Object();
                var sRelativeId = $($($($("#tbdTrade").children()[i]).children()[0]).children()[0]).val();
                var sDemat = $($($($("#tbdTrade").children()[i]).children()[2]).children()[0]).val();
                var sDate = $($($($("#tbdTrade").children()[i]).children()[3]).children()[0]).val();
                var sType = $($($($("#tbdTrade").children()[i]).children()[4]).children()[0]).val();
                var sQty = $($($($("#tbdTrade").children()[i]).children()[5]).children()[0]).val();
                var sRate = $($($($("#tbdTrade").children()[i]).children()[6]).children()[0]).val();
                var sTransactionThrough = $($($($("#tbdTrade").children()[i]).children()[7]).children()[0]).val();
                var sExchange = $($($($("#tbdTrade").children()[i]).children()[8]).children()[0]).val();
                var flg = true;

                if (sRelativeId == undefined || sRelativeId == "-1" || sRelativeId == null) {
                    flg = false;
                }
                if (sDemat == undefined || sDemat == "" || sDemat == null) {
                    flg = false;
                }
                if (sDate == undefined || sDate == "" || sDate == null) {
                    flg = false;
                }
                if (sQty == undefined || sQty == "" || sQty == null) {
                    flg = false;
                }
                if (sRate == undefined || sRate == "" || sRate == null) {
                    flg = false;
                }
                if (sTransactionThrough == undefined || sTransactionThrough == "" || sTransactionThrough == null) {
                    flg = false;
                }
                if (sExchange == undefined || sExchange == "" || sExchange == null) {
                    flg = false;
                }
                if (flg == true) {
                    bNote.PreClearanceRequestId = 0;
                    bNote.PreClearanceRequestedFor = sRelativeId;
                    bNote.relativeId = sRelativeId;
                    bNote.DematAccount = sDemat;
                    bNote.ActualTransactionDate = sDate;
                    bNote.ActualTradeQuantity = sQty;
                    bNote.ValuePerShare = sRate / sQty;
                    bNote.TotalAmount = sRate;
                    bNote.TransactionTypeName = sType;
                    bNote.TransactionType = sType;
                    bNote.TradeExchangeName = sExchange;
                    bNote.exchangeTradedOn = sExchange;
                    bNote.proposedTransactionThrough = sTransactionThrough;
                    BNList.push(bNote);
                }
            }
            if (BNList.length > 0) {
                $("#Loader").show();
                var webUrl = uri + "/api/PreClearanceRequest/SaveBrokerNoteList";
                $.ajax({
                    url: webUrl,
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    datatype: "json",
                    async: false,
                    data: JSON.stringify(BNList),
                    success: function (msg) {
                        $("#Loader").hide();
                        if (isJson(msg)) {
                            msg = JSON.parse(msg);
                        }
                        if (msg.StatusFl == true) {
                            alert("Trade details capture successfully !");
                            $("#mdlTradeDetails").modal('hide');
                            $("#stack1").modal('show');
                        }
                        else {
                            if (msg.Msg == "SessionExpired") {
                                alert("Your session is expired. Please login again to continue");
                                window.location.href = "../LogOut.aspx";
                            }
                            else {
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
            else {
                $("#mdlTradeDetails").modal('hide');
                $("#stack1").modal('show');
            }
        }
        else {
            if ($("#btnSubmitTD").text() == "Skip") {
            $("#mdlTradeDetails").modal('hide');
                $("#stack1").modal('show');
            }
        }
    }
    else {
        $("#mdlTradeDetails").modal('hide');
        $("#stack1").modal('show');
    }
}
function fnValidateBrokerNoteList() {
    var BNList = new Array();
    var BNListDemat = new Array();

    for (var i = 0; i < $("#tbdTrade").children().length; i++) {
        var bNote = new Object();
        var sRelativeId = $($($($("#tbdTrade").children()[i]).children()[0]).children()[0]).val();
        var sDemat = $($($($("#tbdTrade").children()[i]).children()[2]).children()[0]).val();
        var sDate = $($($($("#tbdTrade").children()[i]).children()[3]).children()[0]).val();
        var sType = $($($($("#tbdTrade").children()[i]).children()[4]).children().find("option:selected")).text();
        //var sType = $($($($("#tbdTrade").children()[i]).children()[4]).children()[0] + "option: selected").text();
        
        var sQty = $($($($("#tbdTrade").children()[i]).children()[5]).children()[0]).val();
        var sRate = $($($($("#tbdTrade").children()[i]).children()[6]).children()[0]).val();
        var sTransactionThrough = $($($($("#tbdTrade").children()[i]).children()[7]).children()[0]).val();
        var sExchange = $($($($("#tbdTrade").children()[i]).children()[8]).children()[0]).val();
        var flg = true;

        //alert("sRelativeId=" + sRelativeId);
        //alert("sDemat=" + sDemat);
        //alert("sDate=" + sDate);
        //alert("sType=" + sType);
        //alert("sQty=" + sQty);
        //alert("sRate=" + sRate);

        //alert("sCPEmail=" + sCPEmail);
        //alert("sCPIdentification=" + sCPIdentification);
        //alert("sCPIdentificationNo=" + sCPIdentificationNo);

        if (sRelativeId == undefined || sRelativeId == "-1" || sRelativeId == null) {
            flg = false;
        }
        if (sDemat == undefined || sDemat == "" || sDemat == null) {
            flg = false;
        }
        if (sDate == undefined || sDate == "" || sDate == null) {
            flg = false;
        }
        if (sQty == undefined || sQty == "" || sQty == null) {
            flg = false;
        }
        if (sRate == undefined || sRate == "" || sRate == null) {
            flg = false;
        }
        if (sTransactionThrough == undefined || sTransactionThrough == "" || sTransactionThrough == null) {
            flg = false;
        }
        else {
            if (sTransactionThrough == "Stock Exchange") {
                if (sExchange == undefined || sExchange == "" || sExchange == null) {
                    flg = false;
                }
            }
        }
        


        if (flg == true) {
            bNote.RequestedFor = sRelativeId;
            bNote.Demat = sDemat;
            bNote.TransactionDt = sDate;
            bNote.Qty = sQty;
            bNote.Rate = sRate;
            BNList.push(bNote);
            //alert(sType);
            if (sType.toUpperCase() == "SELL") {
                sQty = parseInt(sQty) * -1;
            }
            //var item = { RelativeId: sRelativeId, DematAccount: sDemat, ActualTradeQuantity: sQty };
            var item = { DematAccount: sDemat, ActualTradeQuantity: sQty };
            if (BNListDemat.length > 0) {
                for (var i = 0; i < BNListDemat.length; i++) {
                    var demat = BNListDemat[i].DematAccount;
                    //var RelativeId = BNListDemat[i].RelativeId;
                    if (demat == sDemat) {
                        
                        BNListDemat[i].ActualTradeQuantity = parseInt(BNListDemat[i].ActualTradeQuantity) + parseInt(sQty);
                    }
                    else {
                        BNListDemat.push(item);
                    }
                }
            }
            else {
                BNListDemat.push(item);
            }
        }
    }

    var flag = true;

    if (BNList.length > 0) {
        if (BNListDemat.length > 0) {
            for (var x = 0; x < arrDetails.length; x++) {

                for (var y = 0; y < arrDetails[x].Demat.length; y++) {
                    var odemat = arrDetails[x].Demat[y].accountNo;
                    var oholding = parseInt(arrDetails[x].Demat[y].CurrentHolding);
                    for (var i = 0; i < BNListDemat.length; i++) {
                        if (//BNListDemat[i].RelativeId == arrDetails[x].RelativeId &&
                            BNListDemat[i].DematAccount == odemat &&
                            (oholding + parseInt(BNListDemat[i].ActualTradeQuantity)) < 0) {
                            alert('please verify entered Quantity for demat '+BNListDemat[i].DematAccount);
                            flag = false;
                            break;
                        }
                    }
                }
            }
            
        }
        
    }
    if (flag == true) {
        return true;
    }
    else {
        return false;
    }
}
function fnClearOtherRequest() {
    $("#ddlForOther").val('');
    $("#ddlTypeOfSecurityOther").val('');
    $("#ddlRestrictedCompaniesOther").val('');
    $("#txtTradeQuantityOther").val('');
    $("#ddlTypeOfTransactionOther").val('');
    $("#txtRequestedTransactionDateOther").val('');
    $("#txtShareCurrentMarketPriceOther").val('');
    $("#ddlProposedTransactionThroughOther").val('');
}

function fnMultiTradeAddRow() {
    $("#tbdMultiTrade").html("");
    var str = "";
    str += "<tr>";
    str += "<td style='padding-left:5px;padding-right:5px;'>";
    str += "<input id='txtMultiTradeDate' type='text' class='form-control bg-white' />";
    str += "</td>";
    str += "<td style='padding-left:5px;padding-right:5px;'>";
    str += "<input id='txtMultiTradeQty' type='number' min='0' class='form-control CalcMultiTrade' />";
    str += "</td>";
    str += "<td style='padding-left:5px;padding-right:5px;'>";
    str += "<input id='txtMultiTradeValue' type='number' min='0' class='form-control CalcMultiTrade' />";
    str += "</td>";
    str += "<td style='padding-left:5px;padding-right:5px;'>";
    str += "<input id='txtMultiTradeAmount' type='text' class='form-control' disabled='disabled' />";
    str += "</td>";
    str += "<td style='padding-left:5px;padding-right:5px;padding-top:10px;'>";
    str += "<a onclick='javascript:fnMultiTradeAddNewRow();'>";
    str += "<i class='fa fa-plus'></i>";
    str += "</a>";
    str += "</td>";
    str += "</tr>";
    $("#tbdMultiTrade").append(str);

    fnSetMultiTradeDate();
}
function fnMultiTradeAddNewRow() {
    var str = "";
    str += "<tr>";
    str += "<td style='padding-left:5px;padding-right:5px;'>";
    str += "<input id='txtMultiTradeDate' type='text' class='form-control bg-white' />";
    str += "</td>";
    str += "<td style='padding-left:5px;padding-right:5px;'>";
    str += "<input id='txtMultiTradeQty' type='number' min='0' class='form-control CalcMultiTrade' />";
    str += "</td>";
    str += "<td style='padding-left:5px;padding-right:5px;'>";
    str += "<input id='txtMultiTradeValue' type='number' min='0' class='form-control CalcMultiTrade' />";
    str += "</td>";
    str += "<td style='padding-left:5px;padding-right:5px;'>";
    str += "<input id='txtMultiTradeAmount' type='text' class='form-control'  disabled='disabled' />";
    str += "</td>";
    str += "<td style='padding-left:5px;padding-right:5px;padding-top:10px;'>";
    str += "<a onclick='javascript:fnMultiTradeAddNewRow();'>";
    str += "<i class='fa fa-plus'></i>";
    str += "</a>&nbsp;&nbsp;";
    str += "<a onclick='javascript:fnRemoveMultiTradeRow(this);'>";
    str += "<i class='fa fa-minus'></i>";
    str += "</a>";
    str += "</td>";
    str += "</tr>";

    $("#tbdMultiTrade").append(str);
    fnSetMultiTradeDate();
}
function fnRemoveMultiTradeRow(cntrl) {
    var obj = $(cntrl).closest('tr');
    $(obj).remove();

}
function fnSetMultiTradeDate() {

    if (new Date(TradeTo) > new Date()) {
        TradeTo = new Date();
    }
    else if (new Date(TradeFrom) > new Date()) {
        TradeFrom = new Date();
    }
    //alert(FormatDate(TradeTo));
    $("[id='txtMultiTradeDate']").each(function () {
        $(this).datepicker({
            todayHighlight: true,
            autoclose: true,
            format: "dd/mm/yyyy",
            clearBtn: true,
            startDate: FormatDate(TradeFrom),
            endDate: FormatDate(TradeTo),
            daysOfWeekDisabled: [0, 6]
        }).attr('readonly', 'readonly');
    })
}
function fnCalculateMultiTrade() {
    $("#TableMultiTrade").on('keyup focusout mousedown', '.CalcMultiTrade', function (event) {

        var $cntrl = $(this).closest('tr');
        var cntrlqty = $($($cntrl).closest('tr').children()[1]).find("input[id*=txtMultiTradeQty]");
        var cntrlsharevalue = $($($cntrl).closest('tr').children()[2]).find("input[id*=txtMultiTradeValue]");
        var cntrlTotal = $($($cntrl).closest('tr').children()[3]).find("input[id*=txtMultiTradeAmount]");
        var cntrldate = $($($cntrl).closest('tr').children()[0]).find("input[id*=txtMultiTradeDate]");

        var qty = $(cntrlqty).val() || 0;
        var sharevalue = $(cntrlsharevalue).val() || 0;

        if (qty.length > 0 && sharevalue.length > 0) {
            var TotalAmount = parseFloat(qty) * parseFloat(sharevalue);
            $(cntrlTotal).val(TotalAmount);
            $("#btnnulltrade").attr('disabled', true);
            
        }
        else {

            $("#btnnulltrade").attr('disabled', false);
        }
    }).trigger('change');
}
function fnValidateMultiTradeList() {
    var validate = true;
    var TotalQty = 0;
    var TotalAmount = 0;
    var MaxDate = "";
    for (var i = 0; i < $("#tbdMultiTrade").children().length; i++) {
        var mtDate = $($($($("#tbdMultiTrade").children()[i]).children()[0]).children()[0]).val();
        var mtQty = $($($($("#tbdMultiTrade").children()[i]).children()[1]).children()[0]).val();
        var mtRate = $($($($("#tbdMultiTrade").children()[i]).children()[2]).children()[0]).val();
        var mtAmount = $($($($("#tbdMultiTrade").children()[i]).children()[3]).children()[0]).val();

        if (mtDate == undefined || mtDate == "" || mtDate == null) {
            validate = false;
        }
        else {
            if (MaxDate == "") {
                MaxDate = mtDate;
            }
            else {
                if (convertToDateTime(mtDate) > convertToDateTime(MaxDate)) {
                    MaxDate = mtDate;
                }
            }


        }

        if (mtQty == undefined || mtQty == "" || mtQty == null) {
            validate = false;
        }

        else {
            TotalQty += parseInt(mtQty);
        }
        if (mtRate == undefined || mtRate == "" || mtRate == null) {
            validate = false;
        }
        if (mtAmount == undefined || mtAmount == "" || mtAmount == null) {
            validate = false;
        }
        else {
            TotalAmount += parseFloat(mtAmount);
        }
    }

    $('#txtActualdateoftransaction').val(MaxDate);
    $('#txtActualTradequantityBrokerNote').val(TotalQty);
    $('#txtValuePerShare').val(TotalAmount / TotalQty);
    $('#txtTotalamount').val(TotalAmount);

    if (TotalQty > parseInt($('#txtTradequantityBrokerNote').val())) {
        alert("Total trade disclosure quantity should be less or equal to the approved quantity");
        return false;
    }
    else if (TotalQty > parseInt($('#txtRemainingTradequantity').val())) {
        alert("Total trade disclosure quantity should be less or equal to the approved quantity");
        return false;
    }
    if (validate) {

        return true;
    }
    else {
        alert("Please fill all mandatory fields");
        return false;
    }
}
function convertToDateTime(date) {
    return date.split("/")[1] + "/" + date.split("/")[0] + "/" + date.split("/")[2];
}
function FormatDate(date) {
    date = new Date(date)
    return date.getDate() + '/' + (date.getMonth() + 1) + '/' + date.getFullYear();
}