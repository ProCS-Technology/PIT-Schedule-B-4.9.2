var objresCompany = [];
jQuery(document).ready(function () {
    GetRestrictedCompanies();
    $('#stack1').on('hide.bs.modal', function () {
    });
    $("input[id='txtStockTrade']").attr("disabled", "disabled");
    //$("input[name='rdbIsRestricted']:checked").val() == "Yes" ? $("divStockTradeLimit").hide() : $("divStockTradeLimit").show();
    $("input[name='rdbIsRestricted']").click(function () {
        var isRestricted = $(this).val();
        // alert(isRestricted);
        if (isRestricted == "No") {
            $("input[id='txtStockTrade']").attr("disabled", false);
            $("input[name='rdbForPerpetuity']").attr("disabled", true);
            $("input[id='txtPeriodFrom']").attr("disabled", true);
            $("input[id='txtPeriodTo']").attr("disabled", true);
            $("input[id='rdbForPerpetuityYes']").attr("checked", false)
            $("input[id='rdbForPerpetuityNo']").attr("checked", false)
            return;
        }
        else {
            $("input[id='txtStockTrade']").attr("disabled", true);
            $("input[name='rdbForPerpetuity']").attr("disabled", false);
            $("input[name='rdbForPerpetuity']").click(function () {
                var rdbForPerpetuity = $(this).val();
                if (rdbForPerpetuity == "No") {
                    $("input[id='txtStockTrade']").attr("disabled", true);
                    $("input[id='txtPeriodFrom']").attr("disabled", false);
                    $("input[id='txtPeriodTo']").attr("disabled", false);
                }
                else if (rdbForPerpetuity == "Yes") {
                    $("input[id='txtPeriodFrom']").attr("disabled", true);
                    $("input[id='txtPeriodTo']").attr("disabled", true);
                    $("input[id='txtStockTrade']").attr("disabled", true);
                }
            });
        }
    });
    $(".number").keypress(function (e) {
        //if the letter is not digit then display error and don't type anything
        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
            return false;
        }
        else if (this.value.length == 0 && e.which === 48) {
            return false;
        }
    });
    $('.number').on('paste', function (event) {
        if (event.originalEvent.clipboardData.getData('Text').match(/[^\d]/)) {
            event.preventDefault();
        }
    });
    //multiple checkbox selection
    $('#chkParent').click(function () {
        var isChecked = $(this).prop("checked");
        $('#tbdRestrictedCompaniesList tr:has(td)').find('input[type="checkbox"]').prop('checked', isChecked);
    });
});
function initializeDataTable() {
    $('#tbl-Designation-setup').DataTable({
        //  "dom": '<"top"iflp<"clear">>rt<"bottom"iflp<"clear">>',
        dom: 'Bfrtip',
        pageLength: 10,
        buttons: [
            {
                extend: 'pdf',
                className: 'btn green btn-outline',
                exportOptions: {
                    columns: [0]
                }
            },
            {
                extend: 'excel',
                className: 'btn yellow btn-outline ',
                exportOptions: {
                    columns: [0]
                }
            },
            //    { extend: 'colvis', className: 'btn dark btn-outline', text: 'Columns' }
        ],
        columnDefs: [
            { targets: "no-sort", orderable: false }
        ],
        order: [[1, "asc"]]
    });
}
function GetRestrictedCompanies() {
    $("#Loader").show();
    var webUrl = uri + "/api/RestrictedCompanies/GetRestrictedCompanies";
    $.ajax({
        type: "POST",
        url: webUrl,
        data: JSON.stringify({
            COMPANY_ID: 0
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: true,
        success: function (msg) {
            if (msg.StatusFl == true) {
                var result = "";
                for (var i = 0; i < msg.RestrictedCompaniesList.length; i++) {
                    objresCompany = [];
                    objresCompany = msg.RestrictedCompaniesList[i];
                    result += '<tr id="tr_' + msg.RestrictedCompaniesList[i].ID + '">';
                    result += '<td id="tdCompanyId_' + msg.RestrictedCompaniesList[i].ID + '"><input type="checkbox"  name="chkbxaction" value="' + msg.RestrictedCompaniesList[i].ID + '" /></td>';
                    result += '<td id="tdCompanyName_' + msg.RestrictedCompaniesList[i].ID + '">' + msg.RestrictedCompaniesList[i].companyName + '</td>';
                    result += '<td id="tdCompanyABRR_' + msg.RestrictedCompaniesList[i].ID + '">' + msg.RestrictedCompaniesList[i].companyABRR + '</td>';
                    result += '<td id="tdIsRestricted_' + msg.RestrictedCompaniesList[i].ID + '">' + (msg.RestrictedCompaniesList[i].isRestricted == 1 ? "Yes" : "No") + '</td>';
                    result += '<td id="tdForPerpetuity_' + msg.RestrictedCompaniesList[i].ID + '">' + (msg.RestrictedCompaniesList[i].forPerpetuity == 1 ? "Yes" : "No") + '</td>';
                    result += '<td id="tdStockTradeLimit_' + msg.RestrictedCompaniesList[i].ID + '">' + msg.RestrictedCompaniesList[i].stockTradeLimit + '</td>';
                    result += '<td id="tdPeriodFrom_' + msg.RestrictedCompaniesList[i].ID + '">' + msg.RestrictedCompaniesList[i].restrictionFrom + '</td>';
                    result += '<td id="tdPeriodTo_' + msg.RestrictedCompaniesList[i].ID + '">' + msg.RestrictedCompaniesList[i].restrictionTo + '</td>';
                    result += '<td style="width:200px;" id="tdEditdelete_' + msg.RestrictedCompaniesList[i].ID + '"><a style="background-color:#3598dc; color:white;" data-target="#stack1" data-toggle="modal" id="a' + msg.RestrictedCompaniesList[i].ID + '" class="btn btn-blue" onclick=\"javascript:fnEditRestrictedCompanies(' + objresCompany.ID + ',\'' + objresCompany.isRestricted + '\', \'' + objresCompany.forPerpetuity + '\', \'' + objresCompany.stockTradeLimit + '\', \'' + objresCompany.restrictionFrom + '\', \'' + objresCompany.restrictionTo + '\', \'' + objresCompany.IsHomeCompany + '\');\">Edit</a><a style="margin-left:20px; background-color:#3598dc; color:white;" data-target="#delete" data-toggle="modal" id="d' + msg.RestrictedCompaniesList[i].ID + '" class="btn btn-blue" onclick=\"javascript:DeleteRestrictedCompanies(' + msg.RestrictedCompaniesList[i].ID + ');\">Delete</a></td>';
                    result += '</tr>';
                }
                var table = $('#tbl-Designation-setup').DataTable();
                table.destroy();
                $("#tbdRestrictedCompaniesList").html(result);
                initializeDataTable();
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
function fnSaveRestrictedCompanies() {
    if (fnValidate()) {
        fnAddUpdateRestrictedCompanies();
    }
    else {
        $('#btnSave').removeAttr("data-dismiss");
        return false;
    }
}
function RestrictedCompanies(ID, companyID, companyName, companyABRR, isRestricted, forPerpetuity, stockTradeLimit, periodOfRestrictionFrom, periodOfRestrictionTo, IsHomeCompany) {
    this.ID = ID;
    this.companyID = companyID;
    this.companyName = companyName;
    this.companyABRR = companyABRR;
    this.isRestricted = isRestricted;
    this.forPerpetuity = forPerpetuity;
    this.stockTradeLimit = stockTradeLimit;
    this.periodOfRestrictionFrom = periodOfRestrictionFrom == undefined || periodOfRestrictionFrom.trim() == "" ? null : convertToDateTime(periodOfRestrictionFrom);
    this.periodOfRestrictionTo = periodOfRestrictionTo == undefined || periodOfRestrictionTo.trim() == "" ? null : convertToDateTime(periodOfRestrictionTo);
    this.IsHomeCompany = IsHomeCompany;
}
function convertToDateTime(date) {
    return date.split("/")[1] + "/" + date.split("/")[0] + "/" + date.split("/")[2];
}
function fnAddUpdateRestrictedCompanies() {

    var RestrictedCompaniesColl = [];
    RestrictedCompaniesColl[RestrictedCompaniesColl.length] = new RestrictedCompanies($("input[id='txtID']").val() == 0 ? 0 : $("input[id='txtID']").val(), 0, $("input[id='txtCompanyName']").val(), $("input[id='txtCompanyABRR']").val(), $("input[name='rdbIsRestricted']:checked").val() == "Yes" ? 1 : 0, $("input[name='rdbForPerpetuity']:checked").val() == "Yes" ? 1 : 0, $("input[id='txtStockTrade']").val() == "" ? 0 : $("input[id='txtStockTrade']").val(), $("input[id='txtPeriodFrom']").val(), $("input[id='txtPeriodTo']").val(), $("input[name='rdbIsHomeCompany']:checked").val() == "Yes" ? 1 : 0)
    $("#Loader").show();
    var webUrl = uri + "/api/RestrictedCompanies/SaveRestrictedCompanies";
    $.ajax({
        type: 'POST',
        url: webUrl,
        data: JSON.stringify(RestrictedCompaniesColl),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            if (msg.StatusFl == false) {
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
            else {
                GetRestrictedCompanies();
                fnClearForm();
                // $('#btnSave').attr("data-dismiss", "modal");
                $("#stack1").modal('hide');
                alert(msg.Msg);
                return true;
            }
        },
        error: function (error) {
            $("#Loader").hide();
            alert(error.status + ' ' + error.statusText);
            // $('#btnSave').removeAttr("data-dismiss");
        }
    })
}
function fnEditRestrictedCompanies(ID, isRestricted, forPerpetuity, stockTradeLimit, restrictionFrom, restrictionTo, IsHomeCompany) {
    $("input[id='txtID']").val(ID);
    var selectedTr = $(event.currentTarget).closest('tr').children();
    $("input[id='txtCompanyName']").val($(selectedTr[1]).html());
    $("input[id='txtCompanyABRR']").val($(selectedTr[2]).html());
    if (isRestricted == 1) {
        $("input[id='rdoIsRestrictedYes']").prop("checked", true);
    }
    else {
        $("input[id='rdoIsRestrictedNo']").prop("checked", true);
    }
    forPerpetuity == 1 ? $("input[id='rdoForPerpetuityYes']").prop("checked", true) : $("input[id='rdoForPerpetuitydNo']").prop("checked", true);
    $("input[id='txtStockTrade']").val(stockTradeLimit);
    $("input[id='txtPeriodFrom']").val(restrictionFrom);
    $("input[id='txtPeriodTo']").val(restrictionTo);
    if (IsHomeCompany == 1) {
        $("input[id='rdoIsHomeCompanyYes']").prop("checked", true);
    }
    else {
        $("input[id='rdoIsHomeCompanyNo']").prop("checked", true);
    }

}
function DeleteRestrictedCompanies(ID) {
    var RestrictedCompaniesColl = [];
    RestrictedCompaniesColl[RestrictedCompaniesColl.length] = new RestrictedCompanies(ID);
    $("#Loader").show();
    var webUrl = uri + "/api/RestrictedCompanies/DeleteRestrictedCompanies";
    $.ajax({
        type: 'POST',
        url: webUrl,
        data: JSON.stringify(RestrictedCompaniesColl),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            if (msg.StatusFl == true) {
                alert("Record Deleted successfully !");
                var table = $('#tbl-Designation-setup').DataTable();
                table.destroy();
                //$("#tr_" + msg.Department.DEPARTMENT_ID).remove();
                //initializeDataTable();
                //$("#Loader").hide();
                //fnGetDepartmentList();
                GetRestrictedCompanies();
                $('#btnSave').attr("data-dismiss", "modal");
                return true;
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
function fnCloseModal() {
    fnClearForm();
}
function fnClearForm() {
    $("input[id='txtID']").val(0);
    $("input[id='txtCompanyName']").val("");
    $("input[id='txtCompanyABRR']").val("");
    $("input[name='rdbIsRestricted']").attr('checked', false);
    $("input[name='rdbForPerpetuity']").attr('checked', false);
    $("input[id='txtStockTrade']").val(0);
    $("input[id='txtPeriodFrom']").val("");
    $("input[id='txtPeriodTo']").val("");
    $("input[name='rdbIsHomeCompany']").attr('checked', false);
}
function fnValidate() {

    if ($("#txtCompanyName").val() == undefined || $("#txtCompanyName").val() == null || $("#txtCompanyName").val().trim() == '') {
        alert("Please Enter Company Name");
        return false;
    }

    if ($("#txtCompanyABRR").val() == undefined || $("#txtCompanyABRR").val() == null || $("#txtCompanyABRR").val().trim() == '') {
        alert("Please Enter Company ABRR");
        return false;
    }
    if ($("input[name*='rdbIsRestricted']:checked").val() == undefined || $("input[name*='rdbIsRestricted']:checked").val() == null || $("input[name*='rdbIsRestricted']:checked").val().trim() == '') {
        alert("Please choose Is Restricted?");
        return false;
    }


    if ($("input[name*='rdbIsRestricted']:checked").val() == "No") {
        if ($("#txtStockTrade").val() == undefined || $("#txtStockTrade").val() == null || $("#txtStockTrade").val().trim() == '') {
            alert("Please Enter Stock Trade Limit");
            return false;
        }
    }
    if ($("input[name*='rdbIsRestricted']:checked").val() == "Yes") {
        if ($("input[name*='rdbForPerpetuity']:checked").val() == undefined || $("input[name*='rdbForPerpetuity']:checked").val() == null || $("input[name*='rdbForPerpetuity']:checked").val().trim() == '') {
            alert("Please choose For Perpetuity");
            return false;
        }
        else if ($("input[name*='rdbForPerpetuity']:checked").val() == "No") {

            if ($("#txtPeriodFrom").val() == undefined || $("#txtPeriodFrom").val() == null || $("#txtPeriodFrom").val().trim() == '') {
                alert("Please Enter Period of Restriction From");
                return false;
            }
            if ($("#txtPeriodTo").val() == undefined || $("#txtPeriodTo").val() == null || $("#txtPeriodTo").val().trim() == '') {
                alert("Please Enter Period of Restriction To");
                return false;
            }
            var FromDate = new Date(convertToDateTime($("#txtPeriodFrom").val()));
            var Todate = new Date(convertToDateTime($("#txtPeriodTo").val()));
            if (Todate < FromDate) {
                alert("Period of Restriction To Date Should be greater than From Date");
                return false;
            }
        }
    }


    return true;
}
function convertToDateTime(dateString) {
    return dateString.split("/")[1] + "/" + dateString.split("/")[0] + "/" + dateString.split("/")[2];
}
function fnChangeCompanyStatus(CompanyStatus) {
    var Id = '';
    $("#tbdRestrictedCompaniesList > tr").each(function () {
        var row = $(this);
        if (row.find('input[type="checkbox"]').is(':checked')) {

            Id += $(this).find('input[name="chkbxaction"]').val() + ",";
        }
    });
    Id = Id.replace(/,\s*$/, "");

    if (Id.length > 0) {
        $("#Loader").show();
        var webUrl = uri + "/api/RestrictedCompanies/UpdateIsRestrictedCompanies";
        $.ajax({
            type: 'POST',
            url: webUrl,
            data: JSON.stringify({
                strID: Id,
                isRestricted: CompanyStatus
            }),
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
                    alert(msg.Msg);
                    window.location.reload(true);
                }
            },
            error: function (error) {
                $("#Loader").hide();
                alert(error.status + ' ' + error.statusText);
            }
        });
    }
    else {
        alert("Please Select company");
    }
}