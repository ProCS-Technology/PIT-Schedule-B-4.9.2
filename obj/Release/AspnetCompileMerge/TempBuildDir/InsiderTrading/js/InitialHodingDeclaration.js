var rowIndex = -1;
function initialHoldings() {
   
    var allowNext = true;//when selected not applicable
    
    var userPersonalInformation = new PersonalInformation();
    for (var index = 0; index < $("#tbdInitialDeclaration").children().length; index++) {
        var obj = new InitialHoldingDetail();
        obj.ID = $($($("#tbdInitialDeclaration").children()[index]).children()[16]).html();
        obj.restrictedCompany.ID = $($($("#tbdInitialDeclaration").children()[index]).children()[0]).html();
        obj.securityType = $($($("#tbdInitialDeclaration").children()[index]).children()[2]).html();
        obj.relative.ID = $($($("#tbdInitialDeclaration").children()[index]).children()[4]).html();
        obj.dematAccount.accountNo = $($($("#tbdInitialDeclaration").children()[index]).children()[7]).html();
        obj.noOfSecurities = $($($("#tbdInitialDeclaration").children()[index]).children()[8]).html();
        obj.relative.panNumber = $($($("#tbdInitialDeclaration").children()[index]).children()[9]).html();
        obj.dematAccount.tradingMemberId = $($($("#tbdInitialDeclaration").children()[index]).children()[10]).html();
        obj.FY_INITIAL = $($($("#tbdInitialDeclaration").children()[index]).children()[11]).html();
        obj.FY_LAST = $($($("#tbdInitialDeclaration").children()[index]).children()[12]).html();
        obj.TOTAL_BUY = $($($("#tbdInitialDeclaration").children()[index]).children()[13]).html();
        obj.TOTAL_SELL = $($($("#tbdInitialDeclaration").children()[index]).children()[14]).html();
        obj.FINANCIAL_YEAR = financilaYear;
        userPersonalInformation.lstInitialHoldingDetail.push(obj);
        if ($($($("#tbdInitialDeclaration").children()[index]).children()[5]).html() == 'Not Applicable') {
            allowNext = false;
        }
    }
    if (index > 1 && allowNext == false) {
        alert('Please Delete the Equity added as "Not Applicable" only in order to continue as you have provided Holding information.');
        return false;
    }
    
    for (var index = 0; index < $("#tbdPhysicalDeclaration").children().length; index++) {
        var obj = new PhysicalHoldingDetail();
        obj.ID = $($($("#tbdPhysicalDeclaration").children()[index]).children()[10]).html();
        obj.restrictedCompany.ID = $($($("#tbdPhysicalDeclaration").children()[index]).children()[0]).html();
        obj.relative.ID = $($($("#tbdPhysicalDeclaration").children()[index]).children()[4]).html();
        obj.noOfSecurities = $($($("#tbdPhysicalDeclaration").children()[index]).children()[6]).html();
        obj.relative.panNumber = $($($("#tbdPhysicalDeclaration").children()[index]).children()[8]).html();
        obj.securityType = $($($("#tbdPhysicalDeclaration").children()[index]).children()[2]).html();
        obj.dematAccountNo = $($($("#tbdPhysicalDeclaration").children()[index]).children()[7]).html();

        userPersonalInformation.lstPhysicalHoldingDetail.push(obj);
    }
    //for (var index = 0; index < $("#tbdDebtSecurityList").children().length; index++) {
    //    var obj = new DematAccount();
    //    obj.RELATIVE_ID = $($($("#tbdDebtSecurityList").children()[index]).children()[1]).html();
    //    var accountNo = $($($("#tbdDebtSecurityList").children()[index]).children()[3]).find("input[id*=txtAccountNO]");
    //    var CurrentHolding = $($($("#tbdDebtSecurityList").children()[index]).children()[4]).find("input[id*=txtCurrentHolding]");
    //    var FY_INITIAL = $($($("#tbdDebtSecurityList").children()[index]).children()[5]).find("input[id*=txtInitial]");
    //    var FY_LAST = $($($("#tbdDebtSecurityList").children()[index]).children()[6]).find("input[id*=txtLast]");
    //    obj.FINANCIAL_YEAR = financilaYear;
    //    obj.accountNo = $(accountNo).val();
    //    obj.CurrentHolding = $(CurrentHolding).val();
    //    obj.FY_INITIAL = $(FY_INITIAL).val();
    //    obj.FY_LAST = $(FY_LAST).val();
    //    userPersonalInformation.lstDematAccount.push(obj);
    //    if ($(CurrentHolding).val() == "" || $(CurrentHolding).val() == null) {
    //        alert("Please provide all details of Debt Security");
    //        return false;
    //    }
    //    if ($(FY_INITIAL).val() == "" || $(FY_INITIAL).val() == null) {
    //        alert("Please provide all details of Debt Security");
    //        return false;
    //    }
    //    if ($(FY_LAST).val() == "" || $(FY_LAST).val() == null) {
    //        alert("Please provide all details of Debt Security");
    //        return false;
    //    }
    //}

    $("#Loader").show();
    var token = $("#TokenKey").val();

    var webUrl = uri + "/api/Transaction/SaveInsiderHoldingDeclarationDetail";

    $.ajax({
        url: webUrl,
        type: "POST",
        headers: {
            'TokenKeyH': token,

        },
        data: JSON.stringify(userPersonalInformation),
        async: false,
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            $("#Loader").hide();
            if (msg.StatusFl == true) {
                //alert("Initial holding details updated successfully !");
                $("#liPI").removeClass('active');
                $("#liRD").removeClass('active');
                $("#liDA").removeClass('active');
                $("#liIH").removeClass('active');
                $("#liCon").addClass('active');

                $("#tab1").removeClass('active');
                $("#tab2").removeClass('active');
                $("#tab3").removeClass('active');
                $("#tab4").removeClass('active');
                $("#tab5").addClass('active');

                $("#tab5").click();
                $("#tab5").show();
                $("#tab4").hide();
                $("#tab1").hide();
                $("#tab2").hide();
                $("#tab3").hide();
                $("#spnTitle").html("Step 5 of 5");

                setInitialHoldingDetail(msg);

                $("#aSavenContinue").hide();
                $("#aSubmitYourDeclaration").show();
                $("#spnblinkpreview").show();
            }
            else {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    alert(msg.Msg);
                }
            }
        },
        error: function (response) {
            $("#Loader").hide();
            alert(response.status + ' ' + response.statusText);
        }
    })
}
function InitialHoldingDetail() {
    this.ID = 0;
    this.restrictedCompany = new RestrictedCompanies();
    this.securityType = $("select[id*=ddlSecurityType]").val() == null ? 0 : ($("select[id*=ddlSecurityType]").val().trim() == "" ? 0 : $("select[id*=ddlSecurityType]").val());
    this.relative = new Relative();
    this.dematAccount = new DematAccount();
    this.noOfSecurities = $("#txtNumberOfSecurities").val() == null ? 0 : ($("#txtNumberOfSecurities").val().trim() == "" ? 0 : $("#txtNumberOfSecurities").val());
    this.FY_INITIAL = $("#txtApril").val() == null ? 0 : ($("#txtApril").val().trim() == "" ? 0 : $("#txtApril").val());
    this.FY_LAST = $("#txtMarch").val() == null ? 0 : ($("#txtMarch").val().trim() == "" ? 0 : $("#txtMarch").val());
    this.TOTAL_BUY = $("#txtTotalBuy").val() == null ? 0 : ($("#txtTotalBuy").val().trim() == "" ? 0 : $("#txtTotalBuy").val());
    this.TOTAL_SELL = $("#txtTotalSell").val() == null ? 0 : ($("#txtTotalSell").val().trim() == "" ? 0 : $("#txtTotalSell").val());
}
function PhysicalHoldingDetail() {
    this.ID = 0;
    this.restrictedCompany = new RestrictedCompaniesPhysical();
    this.relative = new Relative();
    this.noOfSecurities = $("#txtNumberOfSecuritiesPhysical").val() == null ? 0 : ($("#txtNumberOfSecuritiesPhysical").val().trim() == "" ? 0 : $("#txtNumberOfSecuritiesPhysical").val());
    this.OthersecurityType = $("select[id*=ddlOtherSecurityType]").val() == null ? 0 : ($("select[id*=ddlOtherSecurityType]").val().trim() == "" ? 0 : $("select[id*=ddlOtherSecurityType]").val());
    this.OtherDematAccount = $("select[id*=ddlPhysicalDematAccNo]").val() == null ? 0 : ($("select[id*=ddlPhysicalDematAccNo]").val().trim() == "" ? 0 : $("select[id*=ddlPhysicalDematAccNo]").val());
}
function initializeDataTable() {
    $('#tbl-debtSecurity-setup').DataTable({
        //  "dom": '<"top"iflp<"clear">>rt<"bottom"iflp<"clear">>',
        dom: 'Bfrtip',
        pageLength: 10,
        "scrollX": true,
        buttons: [
            {
                extend: 'pdf',
                className: 'btn green btn-outline',
                exportOptions: {
                    columns: [1, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12]
                }
            },
            {
                extend: 'excel',
                className: 'btn yellow btn-outline ',
                exportOptions: {
                    columns: [1, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12]
                }
            },
            //    { extend: 'colvis', className: 'btn dark btn-outline', text: 'Columns' }
        ]
    });
}
function getDebtSecurity() {
   // debugger;
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/GetDebtSecurityDetails";
    $.ajax({
        url: webUrl,
        type: "GET",
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            //debugger;
            $("#Loader").hide();
            if (msg.StatusFl == true) {
                var str = "";
                for (var i = 0; i < msg.User.lstDematAccount.length; i++) {
                    if (msg.User.lstDematAccount[i].RELATIVE_ID != -1) {
                        str += '<tr>';
                        str += '<td style="display:none;">' + msg.User.lstDematAccount[i].ID + '</td>';
                        str += '<td style="display:none;">' + msg.User.lstDematAccount[i].RELATIVE_ID + '</td>';
                        if (msg.User.lstDematAccount[i].RELATIVE_ID == 0) {
                            str += '<td><input disabled type="text" class="form-control" autocomplete="off" value="Self"></td>';
                        }
                        else {
                            str += '<td><input disabled type="text" class="form-control" autocomplete="off" value="' + msg.User.lstDematAccount[i].RELATIVE_NM + '"></td>';
                        }
                        str += '<td><input id="txtAccountNO" disabled type="text" class="form-control" autocomplete="off" value="' + msg.User.lstDematAccount[i].accountNo + '"></td>';
                        str += '<td><input id="txtCurrentHolding" type="text" class="form-control" autocomplete="off"></td>';
                        str += '<td><input id="txtInitial type="text" class="form-control" autocomplete="off"></td>';
                        str += '<td><input id="txtLast" type="text" class="form-control" autocomplete="off"></td>';
                        str += '</tr>';
                    }                    
                }
                //var table = $('#tbl-debtSecurity-setup').DataTable();
                //table.destroy();
                $("#tbdDebtSecurityList").html(str);
                initializeDataTable();                
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
function RestrictedCompanies() {
    this.ID = $("select[id*=ddlRestrictedCompaniesX]").val() == null ? 0 : ($("select[id*=ddlRestrictedCompaniesX]").val().trim() == "" ? 0 : $("select[id*=ddlRestrictedCompaniesX]").val());
    this.companyName = $("select[id*=ddlRestrictedCompaniesX] option:selected").text();
}
function RestrictedCompaniesPhysical() {
    this.ID = $("select[id*=ddlRestrictedCompaniesPhysical]").val() == null ? 0 : ($("select[id*=ddlRestrictedCompaniesPhysical]").val().trim() == "" ? 0 : $("select[id*=ddlRestrictedCompaniesPhysical]").val());
    this.companyName = $("select[id*=ddlRestrictedCompaniesPhysical] option:selected").text();
}
function fnAddInitialHoldingDeclarationDetail() {
    //alert("In function fnAddInitialHoldingDeclarationDetail");
    if (fnValidateInitialHoldingDeclarationDetail()) {
        //alert("In true");
        var obj = new InitialHoldingDetail();
        obj.relative.ID = $("#ddlFor").val() == null ? 0 : ($("#ddlFor").val().trim() == "" ? 0 : $("#ddlFor").val());
        obj.relative.relativeName = $("#ddlFor option:selected").text();
        obj.dematAccount.ID = $("#ddlDematAccNo").val() == null ? 0 : ($("#ddlDematAccNo").val().trim() == "" ? 0 : $("#ddlDematAccNo").val());
        obj.dematAccount.accountNo = $("#ddlDematAccNo option:selected").text();
        var securityTypeName = $("select[id*=ddlSecurityType] option:selected").text();
        var panNumber = $("#txtPan").val() == null ? 0 : ($("#txtPan").val().trim() == "" ? 0 : $("#txtPan").val());
        var tradingMemberId = $("#txtTradingMemId").val() == null ? 0 : ($("#txtTradingMemId").val().trim() == "" ? 0 : $("#txtTradingMemId").val());
        obj.FY_INITIAL = $("#txtApril").val() == null ? 0 : ($("#txtApril").val().trim() == "" ? 0 : $("#txtApril").val());
        obj.FY_LAST = $("#txtMarch").val() == null ? 0 : ($("#txtMarch").val().trim() == "" ? 0 : $("#txtMarch").val());
        obj.TOTAL_BUY = $("#txtTotalBuy").val() == null ? 0 : ($("#txtTotalBuy").val().trim() == "" ? 0 : $("#txtTotalBuy").val());
        obj.TOTAL_SELL = $("#txtTotalSell").val() == null ? 0 : ($("#txtTotalSell").val().trim() == "" ? 0 : $("#txtTotalSell").val());
        //alert("isEditInitialHoldingDeclarationDetail=" + isEditInitialHoldingDeclarationDetail);
        if (!isEditInitialHoldingDeclarationDetail) {
            var str = "";
            str += '<tr>';
            str += '<td style="display:none;">' + obj.restrictedCompany.ID + '</td>';
            str += '<td>' + obj.restrictedCompany.companyName + '</td>';
            str += '<td style="display:none;">' + obj.securityType + '</td>';
            str += '<td>' + securityTypeName + '</td>';
            str += '<td style="display:none;">' + obj.relative.ID + '</td>';
            str += '<td>' + obj.relative.relativeName + '</td>';
            str += '<td style="display:none;">' + obj.dematAccount.ID + '</td>';
            str += '<td>' + obj.dematAccount.accountNo + '</td>';
            str += '<td style="text-align: right;">' + obj.noOfSecurities + '</td>';
            str += '<td style="display:none;">' + panNumber + '</td>';
            str += '<td style="display:none;">' + tradingMemberId + '</td>';
            str += '<td>' + obj.FY_INITIAL + '</td>';
            str += '<td>' + obj.FY_LAST + '</td>';
            str += '<td>' + obj.TOTAL_BUY + '</td>';
            str += '<td>' + obj.TOTAL_SELL + '</td>';
            str += '<td><a data-target="#modalAddInitialHoldingDeclarations" data-toggle="modal" onclick=\"javascript:fnEditInitialDeclarationDetail();\">';
            str += '<img src="../assets/images/edit.png" style="width:24px;height:24px;" /></a>&nbsp;';
            str += '<a data-target="#modalDeleteInitialHoldingDeclarations" data-toggle="modal" onclick=\"javascript:fnDeleteInitialDeclarationDetail();\">';
            str += '<img src="../assets/images/delete.png" style = "width:24px;height:24px;" /></a></td>';


            //str += '<td><a data-target="#modalAddInitialHoldingDeclarations" data-toggle="modal" class="btn btn-outline dark" onclick=\"javascript:fnEditInitialDeclarationDetail();\">Edit</a>';
            //str += '&nbsp;<a data-target="#modalDeleteInitialHoldingDeclarations" data-toggle="modal" class="btn btn-outline dark" onclick=\"javascript:fnDeleteInitialDeclarationDetail();\">Delete</a></td>';
            str += '<td style="display:none;">' + obj.ID + '</td>';
            str += '</tr>';
            $("#tbdInitialDeclaration").append(str);
        }
        else {
            $(editableElementInitialDeclarationDetail[0]).html(obj.restrictedCompany.ID);
            $(editableElementInitialDeclarationDetail[1]).html(obj.restrictedCompany.companyName);
            $(editableElementInitialDeclarationDetail[2]).html(obj.securityType);
            $(editableElementInitialDeclarationDetail[3]).html(securityTypeName);
            $(editableElementInitialDeclarationDetail[4]).html(obj.relative.ID);
            $(editableElementInitialDeclarationDetail[5]).html(obj.relative.relativeName);
            $(editableElementInitialDeclarationDetail[6]).html(obj.dematAccount.ID);
            $(editableElementInitialDeclarationDetail[7]).html(obj.dematAccount.accountNo);
            $(editableElementInitialDeclarationDetail[8]).html(obj.noOfSecurities);
            $(editableElementInitialDeclarationDetail[9]).html(panNumber);
            $(editableElementInitialDeclarationDetail[10]).html(tradingMemberId);
            $(editableElementInitialDeclarationDetail[11]).html(obj.FY_INITIAL);
            $(editableElementInitialDeclarationDetail[12]).html(obj.FY_LAST);
            $(editableElementInitialDeclarationDetail[13]).html(obj.TOTAL_BUY);
            $(editableElementInitialDeclarationDetail[14]).html(obj.TOTAL_SELL);
            //  $(editableElementInitialDeclarationDetail[12]).html(obj.ID);
        }
        fnClearFormInitialDeclarationDetail();
        $("#modalAddInitialHoldingDeclarations").modal('hide');
    }
}
function fnCloseModalAddInitialHoldingDeclarations() {
    fnClearFormInitialDeclarationDetail();
}

function fnClearFormInitialDeclarationDetail() {
    $("select[id*=ddlRestrictedCompaniesX]").val('');
    $("select[id*=ddlSecurityType]").val('');
    $("#ddlFor").val('');
    $("#txtPan").val('');
    $("#txtPanPhysical").val('');
    $("#ddlDematAccNo").val('');
    $("#ddlDematAccNo").html('');
    $("#txtTradingMemId").val('');
    $("#txtNumberOfSecurities").val('');
    $("#txtApril").val('');
    $("#txtMarch").val('');
    $("#txtTotalBuy").val('');
    $("#txtTotalSell").val('');
    $("select[id*=ddlSecurityType]").prop("disabled", false);
    $("#ddlFor").prop("disabled", false);
    $("#ddlDematAccNo").prop("disabled", false);
    $("#txtNumberOfSecurities").prop("disabled", false);
    $("#txtApril").attr("disabled", false);
    $("#txtMarch").attr("disabled", false);
    $("#txtTotalBuy").attr("disabled", false);
    $("#txtTotalSell").attr("disabled", false);
    isEditInitialHoldingDeclarationDetail = false;
    editableElementInitialDeclarationDetail = null;
    $("#lblRestrictedCompanies").removeClass('requiredlbl');
    $("#lblSecurityType").removeClass('requiredlbl');
    $("#lblFor").removeClass('requiredlbl');
    $("#lblPan").removeClass('requiredlbl');
    $("#lblDematAccNo").removeClass('requiredlbl');
    $("#lblTradingMemId").removeClass('requiredlbl');
    $("#lblNumberOfSecurities").removeClass('requiredlbl');
    dematAccountNumberGlobal = '';
}
function fnValidateInitialHoldingDeclarationDetail() {
    var count = 0;
    if ($("select[id*=ddlRestrictedCompaniesX]").val() == null || $("select[id*=ddlRestrictedCompaniesX]").val() == undefined || $("select[id*=ddlRestrictedCompaniesX]").val().trim() == "") {
        count++;
        $("#lblRestrictedCompanies").addClass('requiredlbl');
    }
    else {
        $("#lblRestrictedCompanies").removeClass('requiredlbl');
    }
    if ($("select[id*=ddlSecurityType]").val() == null || $("select[id*=ddlSecurityType]").val() == undefined || $("select[id*=ddlSecurityType]").val().trim() == "") {
        count++;
        $("#lblSecurityType").addClass('requiredlbl');
    }
    else {
        $("#lblSecurityType").removeClass('requiredlbl');
    }
    if ($("#ddlFor").val() == null || $("#ddlFor").val() == undefined || $("#ddlFor").val().trim() == "") {
        count++;
        $("#lblFor").addClass('requiredlbl');
    }
    else {
        $("#lblFor").removeClass('requiredlbl');
    }
    //if ($("#ddlDematAccNo").val() == null || $("#ddlDematAccNo").val() == undefined || $("#ddlDematAccNo").val().trim() == "") {
    //    count++;
    //    $("#lblDematAccNo").addClass('requiredlbl');
    //}
    //else {
    //    $("#lblDematAccNo").removeClass('requiredlbl');
    //}
    if ($("#txtNumberOfSecurities").val() == null || $("#txtNumberOfSecurities").val() == undefined || $("#txtNumberOfSecurities").val().trim() == "") {
        count++;
        $("#lblNumberOfSecurities").addClass('requiredlbl');
    }
    else {
        $("#lblNumberOfSecurities").removeClass('requiredlbl');
    }
    if ($("#txtApril").val() == null || $("#txtApril").val() == undefined || $("#txtApril").val().trim() == "") {
        count++;
        $("#txtApril").addClass('requiredlbl');
    }
    else {
        $("#lblApril").removeClass('requiredlbl');
    }
    if ($("#txtMarch").val() == null || $("#txtMarch").val() == undefined || $("#txtMarch").val().trim() == "") {
        count++;
        $("#txtMarch").addClass('requiredlbl');
    }
    else {
        $("#lblMarch").removeClass('requiredlbl');
    }
    if ($("#txtTotalBuy").val() == null || $("#txtTotalBuy").val() == undefined || $("#txtTotalBuy").val().trim() == "") {
        count++;
        $("#txtTotalBuy").addClass('requiredlbl');
    }
    else {
        $("#lblTotalBuy").removeClass('requiredlbl');
    }
    if ($("#txtTotalSell").val() == null || $("#txtTotalSell").val() == undefined || $("#txtTotalSell").val().trim() == "") {
        count++;
        $("#txtTotalSell").addClass('requiredlbl');
    }
    else {
        $("#lblTotalSell").removeClass('requiredlbl');
    }
    if ($("#ddlDematAccNo option:selected").text() != "Not Applicable") {
        for (var index = 0; index < $("#tbdInitialDeclaration").children().length; index++) {
            if ($("#ddlDematAccNo option:selected").text() == $($($("#tbdInitialDeclaration").children()[index]).children()[7]).html() && dematAccountNumberGlobal != $("#ddlDematAccNo option:selected").text()) {
                alert("Demat Account Number has already been declared with holdings. If you want to change the holdings please update the same.");
                count++;
                break;
            }
        }
    }

    if ($("#ddlFor").val() != "0" && $("select[id*=ddlSecurityType] option:selected").text() == "ESOP") {
        alert("Cannot add ESOP holding to your relatives.");
        count++;
    }

    if (count > 0) {
        return false;
    }
    else {
        return true;
    }
}
function fnEditInitialDeclarationDetail() {
    isEditInitialHoldingDeclarationDetail = true;
    var selectedTr = $(event.currentTarget).closest('tr').children();
    editableElementInitialDeclarationDetail = selectedTr;
    var restrictedCompanyId = $(selectedTr[0]).html();
    var securityType = $(selectedTr[2]).html();
    var relativeId = $(selectedTr[4]).html();
    var dematAccountNo = $(selectedTr[7]).html();
    dematAccountNumberGlobal = dematAccountNo;
    var numberOfSecurities = $(selectedTr[8]).html();
    var panNumber = $(selectedTr[9]).html();
    var tradingMemberId = $(selectedTr[10]).html();
    var fy_initial = $(selectedTr[11]).html();
    var fy_last = $(selectedTr[12]).html();
    var totalBuy = $(selectedTr[13]).html();
    var totalSell = $(selectedTr[14]).html();

    $("select[id*=ddlRestrictedCompaniesX]").val(restrictedCompanyId);
    $("select[id*=ddlSecurityType]").val(securityType);
    //$("#ddlFor").val(relativeId).change();
    $("#ddlFor").val(relativeId);
    $("#txtPan").val(panNumber);    
    //var result = "<option value=''></option>";
    //for (var i = 0; i < arrDemat.length; i++) {
    //    result += "<option value='" + arrDemat[i].id + "'>" + arrDemat[i].accountNo + "</option>";
    //}
    //$("#ddlDematAccNo").html(result);
    getAllDematAccountsOfRelative(relativeId);
    $("#ddlDematAccNo option:contains(" + dematAccountNo + ")").attr('selected', 'selected');
    $("#txtTradingMemId").val(tradingMemberId);
    $("#txtNumberOfSecurities").val(numberOfSecurities);
    $("#txtApril").val(fy_initial);
    $("#txtMarch").val(fy_last);
    $("#txtTotalBuy").val(totalBuy);
    $("#txtTotalSell").val(totalSell);
}
function fnAddInitialHoldingDeclarationDetailPhysical() {
    if (fnValidateInitialHoldingDeclarationDetailPhysical()) {
        var obj = new PhysicalHoldingDetail();
        obj.relative.ID = $("#ddlForPhysical").val() == null ? 0 : ($("#ddlForPhysical").val().trim() == "" ? 0 : $("#ddlForPhysical").val());
        obj.relative.relativeName = $("#ddlForPhysical option:selected").text();
        var panNumber = $("#txtPanPhysical").val() == null ? 0 : ($("#txtPanPhysical").val().trim() == "" ? 0 : $("#txtPanPhysical").val());
        obj.OtherDematAccount = $("#ddlPhysicalDematAccNo option:selected").text();
        var securityTypeName = $("select[id*=ddlOtherSecurityType] option:selected").text();
        if (!isEditInitialHoldingDeclarationDetailPhysical) {
            var str = "";
            str += '<tr>';
            str += '<td style="display:none;">' + obj.restrictedCompany.ID + '</td>';
            str += '<td>' + obj.restrictedCompany.companyName + '</td>';
            str += '<td style="display:none;">' + obj.OthersecurityType + '</td>';
            str += '<td>' + securityTypeName + '</td>';
            str += '<td style="display:none;">' + obj.relative.ID + '</td>';
            str += '<td>' + obj.relative.relativeName + '</td>';
            str += '<td style="text-align: right;">' + obj.noOfSecurities + '</td>';
            str += '<td>' + obj.OtherDematAccount + '</td>';
            str += '<td style="display:none;">' + panNumber + '</td>';

            str += '<td><a data-target="#modalAddPhysicalShares" data-toggle="modal" onclick=\"javascript:fnEditInitialDeclarationDetailPhysical();\">';
            str += '<img src="../assets/images/edit.png" style="width:24px;height:24px;" /></a>&nbsp;';
            str += '<a data-target="#modalDeletePhysicalHoldingDeclarations" data-toggle="modal" onclick=\"javascript:fnDeleteInitialDeclarationDetailPhysical();\">';
            str += '<img src="../assets/images/delete.png" style = "width:24px;height:24px;" /></a></td>';

            str += '<td style="display:none;">' + obj.ID + '</td>';
            str += '</tr>';
            $("#tbdPhysicalDeclaration").append(str);
        }
        else {
            $(editableElementInitialDeclarationDetailPhysical[0]).html(obj.restrictedCompany.ID);
            $(editableElementInitialDeclarationDetailPhysical[1]).html(obj.restrictedCompany.companyName);
            $(editableElementInitialDeclarationDetailPhysical[2]).html(obj.OthersecurityType);
            $(editableElementInitialDeclarationDetailPhysical[3]).html(securityTypeName);
            $(editableElementInitialDeclarationDetailPhysical[4]).html(obj.relative.ID);
            $(editableElementInitialDeclarationDetailPhysical[5]).html(obj.relative.relativeName);
            $(editableElementInitialDeclarationDetailPhysical[6]).html(obj.noOfSecurities);
            $(editableElementInitialDeclarationDetailPhysical[7]).html(obj.OtherDematAccount);
            $(editableElementInitialDeclarationDetailPhysical[8]).html(panNumber);

        }
        fnClearFormInitialDeclarationDetailPhysical();
        $("#modalAddPhysicalShares").modal('hide');
    }
}
function fnCloseModalAddInitialHoldingDeclarationsPhysical() {
    fnClearFormInitialDeclarationDetailPhysical();
}
function fnClearFormInitialDeclarationDetailPhysical() {
    $("select[id*=ddlRestrictedCompaniesPhysical]").val('');
    $("#ddlForPhysical").val('');
    $("#txtPan").val('');
    $("#txtPanPhysical").val('');
    $("#txtNumberOfSecuritiesPhysical").val('');
    $("select[id*=ddlPhysicalDematAccNo]").html('');
    $("select[id*=ddlOtherSecurityType]").val('');

    isEditInitialHoldingDeclarationDetailPhysical = false;
    editableElementInitialDeclarationDetailPhysical = null;
    $("#lblRestrictedCompaniesPhysical").removeClass('required');
    $("#lblForPhysical").removeClass('required');
    $("#lblPanPhysical").removeClass('required');
    $("#lblNumberOfSecuritiesPhysical").removeClass('required');
    $("#lblOtherSecurityType").removeClass('required');
    $("#lblPhysicalDematAccNo").removeClass('required');
    dematAccountNumberGlobal = '';
}
function fnValidateInitialHoldingDeclarationDetailPhysical() {
    var count = 0;
    if ($("select[id*=ddlRestrictedCompaniesPhysical]").val() == null || $("select[id*=ddlRestrictedCompaniesPhysical]").val() == undefined || $("select[id*=ddlRestrictedCompaniesPhysical]").val().trim() == "") {
        count++;
        $("#lblRestrictedCompaniesPhysical").addClass('required');
    }
    else {
        $("#lblRestrictedCompaniesPhysical").removeClass('required');
    }
    if ($("select[id*=ddlOtherSecurityType]").val() == null || $("select[id*=ddlOtherSecurityType]").val() == undefined || $("select[id*=ddlOtherSecurityType]").val().trim() == "") {
        count++;
        $("#lblOtherSecurityType").addClass('required');
    }
    else {
        $("#lblOtherSecurityType").removeClass('required');
    }


    if ($("#ddlForPhysical").val() == null || $("#ddlForPhysical").val() == undefined || $("#ddlForPhysical").val().trim() == "") {
        count++;
        $("#lblForPhysical").addClass('required');
    }
    else {
        $("#lblForPhysical").removeClass('required');
    }
    //if ($("#ddlPhysicalDematAccNo").val() == null || $("#ddlPhysicalDematAccNo").val() == undefined || $("#ddlPhysicalDematAccNo").val().trim() == "") {
    //    count++;
    //    $("#lblPhysicalDematAccNo").addClass('required');
    //}
    //else {
    //    $("#lblPhysicalDematAccNo").removeClass('required');
    //}
    if ($("#txtNumberOfSecuritiesPhysical").val() == null || $("#txtNumberOfSecuritiesPhysical").val() == undefined || $("#txtNumberOfSecuritiesPhysical").val().trim() == "") {
        count++;
        $("#lblNumberOfSecuritiesPhysical").addClass('required');
    }
    else {
        $("#lblNumberOfSecuritiesPhysical").removeClass('required');
    }

    if ($("#ddlPhysicalDematAccNo option:selected").text() != "Not Applicable") {
        for (var index = 0; index < $("#tbdPhysicalDeclaration").children().length; index++) {
            if ($("#ddlPhysicalDematAccNo option:selected").text() == $($($("#tbdPhysicalDeclaration").children()[index]).children()[7]).html() && $("select[id*=ddlOtherSecurityType]").val() == $($($("#tbdPhysicalDeclaration").children()[index]).children()[2]).html() && dematAccountNumberGlobal != $("#ddlPhysicalDematAccNo option:selected").text()) {
                alert("Demat Account Number has already been declared with holdings. If you want to change the holdings please update the same.");
                count++;
                break;
            }
        }
    }

    if (count > 0) {
        return false;
    }
    else {
        return true;
    }
}
function fnEditInitialDeclarationDetailPhysical() {
    isEditInitialHoldingDeclarationDetailPhysical = true;
    var selectedTr = $(event.currentTarget).closest('tr').children();
    editableElementInitialDeclarationDetailPhysical = selectedTr;
    var restrictedCompanyId = $(selectedTr[0]).html();
    var relativeId = $(selectedTr[4]).html();
    var numberOfSecurities = $(selectedTr[6]).html();
    var panNumber = $(selectedTr[8]).html();
    var SecurityType = $(selectedTr[2]).html();
    var DematAccount = $(selectedTr[7]).html();
    dematAccountNumberGlobal = DematAccount;
    $("select[id*='ddlRestrictedCompaniesPhysical']").val(restrictedCompanyId);
    $("#ddlForPhysical").val(relativeId).change();
    $("#txtPanPhysical").val(panNumber);
    $("#txtNumberOfSecuritiesPhysical").val(numberOfSecurities);
    $("select[id*='ddlOtherSecurityType']").val(SecurityType);
    $("select[id*='ddlPhysicalDematAccNo'] option:contains(" + DematAccount + ")").attr('selected', 'selected');

}

function setInitialHoldingDetail(msg) {
    //debugger;
    var str = "";
    if (msg.User.lstInitialHoldingDetail !== null) {
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
            //alert(msg.User.lstInitialHoldingDetail[i].FY_INITIAL);
            //alert(msg.User.lstInitialHoldingDetail[i].FY_LAST);
            //alert(msg.User.lstInitialHoldingDetail[i].TOTAL_BUY);
            //alert(msg.User.lstInitialHoldingDetail[i].TOTAL_SELL);
            str += '<td>' + msg.User.lstInitialHoldingDetail[i].FY_INITIAL + '</td>';
            str += '<td>' + msg.User.lstInitialHoldingDetail[i].FY_LAST + '</td>';
            str += '<td>' + msg.User.lstInitialHoldingDetail[i].TOTAL_BUY + '</td>';
            str += '<td>' + msg.User.lstInitialHoldingDetail[i].TOTAL_SELL + '</td>';
            str += '<td><a data-target="#modalAddInitialHoldingDeclarations" data-toggle="modal" onclick=\"javascript:fnEditInitialDeclarationDetail();\"><img src="../assets/images/edit.png" style="width:24px;height:24px;" /></a>';

            if (msg.User.lstInitialHoldingDetail[i].isDeleteInitialHolding || msg.User.lstInitialHoldingDetail[i].relative.relativeName == 'Not Applicable') {
                str += '&nbsp;<a data-target="#modalDeleteInitialHoldingDeclarations" data-toggle="modal" onclick=\"javascript:fnDeleteInitialDeclarationDetail();\"><img src="../assets/images/delete.png" style = "width:24px;height:24px;" /></a>';
            }

            str += '</td > ';

            str += '<td style="display:none;">' + msg.User.lstInitialHoldingDetail[i].ID + '</td>';
            str += '</tr>';
        }
    }


    $("#tbdInitialDeclaration").html(str);

    str = "";
    if (msg.User.lstPhysicalHoldingDetail !== null) {
        for (var i = 0; i < msg.User.lstPhysicalHoldingDetail.length; i++) {
            str += '<tr>';
            str += '<td style="display:none;">' + msg.User.lstPhysicalHoldingDetail[i].restrictedCompany.ID + '</td>';
            str += '<td>' + msg.User.lstPhysicalHoldingDetail[i].restrictedCompany.companyName + '</td>';
            str += '<td style="display:none;">' + msg.User.lstPhysicalHoldingDetail[i].securityType + '</td>';
            str += '<td>' + msg.User.lstPhysicalHoldingDetail[i].securityTypeName + '</td>';

            str += '<td style="display:none;">' + msg.User.lstPhysicalHoldingDetail[i].relative.ID + '</td>';
            str += '<td>' + msg.User.lstPhysicalHoldingDetail[i].relative.relativeName + '</td>';
            str += '<td style="text-align: right;">' + msg.User.lstPhysicalHoldingDetail[i].noOfSecurities + '</td>';
            str += '<td>' + msg.User.lstPhysicalHoldingDetail[i].dematAccountNo + '</td>';
            str += '<td style="display:none;">' + msg.User.lstPhysicalHoldingDetail[i].relative.panNumber + '</td>';
            str += '<td><a data-target="#modalAddPhysicalShares" data-toggle="modal" onclick=\"javascript:fnEditInitialDeclarationDetailPhysical();\"><img src="../assets/images/edit.png" style="width:24px;height:24px;" /></a>';

            if (msg.User.lstPhysicalHoldingDetail[i].isDeletePhysicalHolding) {
                str += '&nbsp;<a data-target="#modalDeletePhysicalHoldingDeclarations" data-toggle="modal" onclick=\"javascript:fnDeleteInitialDeclarationDetailPhysical();\"><img src="../assets/images/delete.png" style = "width:24px;height:24px;" /></a>';
            }

            str += '</td > ';

            str += '<td style="display:none;">' + msg.User.lstPhysicalHoldingDetail[i].ID + '</td>';
            str += '</tr>';
        }
    }
    $("#tbdPhysicalDeclaration").html(str);
}
function fnDeleteInitialDeclarationDetail() {
    var selectedTr = $(event.currentTarget).closest('tr').children();
    var initialHoldingId = $(selectedTr[16]).html();

    deleteInitialHoldingDetailElement = $(event.currentTarget).closest('tr');
    $("#txtDeleteInitialHoldingDetailId").val(initialHoldingId);
}
function fnDeleteInitialHoldingDetailModal() {    
    if ($("#txtDeleteInitialHoldingDetailId").val() == "0") {
        if (deleteInitialHoldingDetailElement != null) {
            deleteInitialHoldingDetailElement.remove();
            deleteInitialHoldingDetailElement = null;
            alert("Record has been deleted successfully!");
        }
    }
    else {
        $("#Loader").show();
        var webUrl = uri + "/api/Transaction/DeleteInitialHoldingDetail";
        $.ajax({
            url: webUrl,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            data: JSON.stringify({
                initialHoldingDetailInfo: { ID: $("#txtDeleteInitialHoldingDetailId").val() }
            }),
            success: function (msg) {
                $("#Loader").hide();
                if (isJson(msg)) {
                    msg = JSON.parse(msg);
                }
                if (msg.StatusFl == true) {
                    if (deleteInitialHoldingDetailElement != null) {
                        deleteInitialHoldingDetailElement.remove();
                        deleteInitialHoldingDetailElement = null;
                        alert("Record has been deleted successfully!");
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
}
function fnDeleteInitialDeclarationDetailPhysical() {
    var selectedTr = $(event.currentTarget).closest('tr').children();
    var initialHoldingId = $(selectedTr[10]).html();
    deleteInitialHoldingDetailElementPhysical = $(event.currentTarget).closest('tr');
    $("#txtDeletePhysicalHoldingDetailId").val(initialHoldingId);
}
function fnDeleteInitialHoldingDetailPhysicalModal() {
    if ($("#txtDeletePhysicalHoldingDetailId").val() == "0") {
        if (deleteInitialHoldingDetailElementPhysical != null) {
            deleteInitialHoldingDetailElementPhysical.remove();
            deleteInitialHoldingDetailElementPhysical = null;
            alert("Record has been deleted successfully!");
        }
    }
    else {
        $("#Loader").show();
        var webUrl = uri + "/api/Transaction/DeletePhysicalHoldingDetail";
        $.ajax({
            url: webUrl,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            data: JSON.stringify({
                physicalHoldingDetailInfo: { ID: $("#txtDeletePhysicalHoldingDetailId").val() }
            }),
            success: function (msg) {
                $("#Loader").hide();
                if (isJson(msg)) {
                    msg = JSON.parse(msg);
                }
                if (msg.StatusFl == true) {
                    if (deleteInitialHoldingDetailElementPhysical != null) {
                        deleteInitialHoldingDetailElementPhysical.remove();
                        deleteInitialHoldingDetailElementPhysical = null;
                        alert("Record has been deleted successfully!");
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
}
function fnValidateHoldingforOther() {
    var count = 0;
    //alert("In function fnValidateHoldingforOther");
    var securityType = $("select[id*=ddlOSecurityType]").val();
    //alert("securityType=" + securityType);

    if ($("select[id*=ddlRestrictedCompaniesForOther]").val() == null || $("select[id*=ddlRestrictedCompaniesForOther]").val() == undefined || $("select[id*=ddlRestrictedCompaniesForOther]").val().trim() == "") {
        count++;
        $("#lblRestrictedCompaniesForOther").addClass('requiredlbl');
    }
    else {
        $("#lblRestrictedCompaniesForOther").removeClass('requiredlbl');
    }

    if ($("#ddlForOther").val() == null || $("#ddlForOther").val() == undefined || $("#ddlForOther").val().trim() == "") {
        count++;
        $("#lblForOther").addClass('requiredlbl');
    }
    else {
        $("#lblForOther").removeClass('requiredlbl');
    }
    if ($("select[id*=ddlOSecurityType]").val() == null || $("select[id*=ddlOSecurityType]").val() == undefined || $("select[id*=ddlOSecurityType]").val().trim() == "") {
        count++;
        $("#lblOSecurityType").addClass('requiredlbl');
    }
    else {
        $("#lblOSecurityType").removeClass('requiredlbl');
    }
    if ($("#ddlDematAccNoForOther").val() == null || $("#ddlDematAccNoForOther").val() == undefined || $("#ddlDematAccNoForOther").val().trim() == "") {
        count++;
        $("#lblDematAccNoForOther").addClass('requiredlbl');
    }
    else {
        $("#lblDematAccNoForOther").removeClass('requiredlbl');
    }
    if ($("#txtNumberOfSecuritiesForOther").val() == null || $("#txtNumberOfSecuritiesForOther").val() == undefined || $("#txtNumberOfSecuritiesForOther").val().trim() == "") {
        count++;
        $("#lblNumberOfSecuritiesForOther").addClass('requiredlbl');
    }
    else {
        $("#lblNumberOfSecuritiesForOther").removeClass('requiredlbl');
    }
    //alert("in validation");
    if ($("#ddlDematAccNoForOther option:selected").text() != "Not Applicable") {
        var company_isin = $("select[id*=ddlRestrictedCompaniesForOther]").val();
        var companyID = company_isin.split(",")[0]
        var companyISIN = $("#spnISIN").text();

        for (var index = 0; index < $("#tbdEquityforOther").children().length; index++) {


            if (
                $("#ddlDematAccNoForOther option:selected").text() == $($($("#tbdEquityforOther").children()[index]).children()[7]).html()
                && companyID == $($($("#tbdEquityforOther").children()[index]).children()[0]).html()
                && $("select[id*=ddlOSecurityType]").val() == $($($("#tbdEquityforOther").children()[index]).children()[12]).html()
                && index != rowIndex
            ) {

                //alert("DDL Demat=" + $("#ddlDematAccNoForOther option:selected").text());
                //alert("TBL Demat=" + $($($("#tbdEquityforOther").children()[index]).children()[7]).html());

                //alert("DDL companyID=" + companyID);
                //alert("TBL companyID=" + $($($("#tbdEquityforOther").children()[index]).children()[0]).html());

                //alert("DDL security=" + $("select[id*=ddlOSecurityType]").val());
                //alert("TBL security=" + $($($("#tbdEquityforOther").children()[index]).children()[12]).html());

                //alert("i=" + index);
                //alert("rowIndex=" + rowIndex);

                alert("Demat Account Number has already been declared with holdings. If you want to change the holdings please update the same.");
                count++;
                break;
            }
        }
    }

    if (count > 0) {
        return false;
    }
    else {
        return true;
    }
}
function fnCloseModalAddHoldingforOther() {
    rowIndex = -1;
    $("select[id*=ddlRestrictedCompaniesForOther]").val('').trigger('change');
    $("select[id*=ddlOSecurityType]").val('').trigger('change');
    $("#ddlForOther").val('');
    $("#txtPanForOther").val('');
    $("#ddlDematAccNoForOther").val('');
    $("#ddlDematAccNoForOther").html('');
    $("#txtNumberOfSecuritiesForOther").val('');
    $("#spnISIN").val('');
    $("#HiddenHoldingIdforOther").val('');

    $("#lblRestrictedCompaniesForOther").removeClass('requiredlbl');
    $("#lblForOther").removeClass('requiredlbl');
    $("#lblDematAccNoForOther").removeClass('requiredlbl');
    $("#lblNumberOfSecuritiesForOther").removeClass('requiredlbl');
    $("#spnISIN").text('');
    dematAccountNumberGlobal = '';
    isEditInitialHoldingDeclarationDetailForOther = false;
    editableElementInitialDeclarationDetail = null;

}
function fnEditInitialDeclarationDetailForOther() {
    isEditInitialHoldingDeclarationDetailForOther = true;
    rowIndex = $(event.currentTarget).closest('tr').index();
    //alert("rowIndex=" + rowIndex);
    var selectedTr = $(event.currentTarget).closest('tr').children();
    editableElementInitialDeclarationDetailForOther = selectedTr;
    var restrictedCompanyId = $(selectedTr[0]).html();
    var restrictedCompanyISIN = $(selectedTr[2]).html();
    var securityTypeNm = $(selectedTr[3]).html();
    var securityTypeId = $(selectedTr[12]).html();

    var relativeId = $(selectedTr[4]).html();
    var dematAccountNo = $(selectedTr[7]).html();
    dematAccountNumberGlobal = dematAccountNo;
    var numberOfSecurities = $(selectedTr[8]).html();
    var panNumber = $(selectedTr[9]).html();

    $("select[id*=ddlRestrictedCompaniesForOther]").val(restrictedCompanyId + "," + restrictedCompanyISIN).change();
    $("select[id*=ddlOSecurityType]").val(securityTypeId);
    $("#spnISIN").text(restrictedCompanyISIN);
    $("#ddlForOther").val(relativeId).change();
    $("#txtPanForOther").val(panNumber);
    $("#ddlDematAccNoForOther option:contains(" + dematAccountNo + ")").attr('selected', 'selected');
    $("#txtNumberOfSecuritiesForOther").val(numberOfSecurities);
}
function fnDeleteInitialHoldingforOther() {
    var selectedTr = $(event.currentTarget).closest('tr').children();
    var initialHoldingId = $(selectedTr[11]).html();

    deleteInitialHoldingDetailElementforOther = $(event.currentTarget).closest('tr');
    $("#HiddenHoldingIdforOther").val(initialHoldingId);
}
function fnDeleteInitialHoldingDetailModalforOther() {

    if ($("#HiddenHoldingIdforOther").val() == "0") {
        if (deleteInitialHoldingDetailElementforOther != null) {
            deleteInitialHoldingDetailElementforOther.remove();
            deleteInitialHoldingDetailElementforOther = null;
            alert("Record has been deleted successfully!");
        }
    }
    else {
        $("#Loader").show();
        var webUrl = uri + "/api/Transaction/DeleteInitialHoldingDetail";
        $.ajax({
            url: webUrl,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            data: JSON.stringify({
                initialHoldingDetailInfo: { ID: $("#HiddenHoldingIdforOther").val() }
            }),
            success: function (msg) {
                $("#Loader").hide();
                if (isJson(msg)) {
                    msg = JSON.parse(msg);
                }
                if (msg.StatusFl == true) {
                    if (deleteInitialHoldingDetailElementforOther != null) {
                        deleteInitialHoldingDetailElementforOther.remove();
                        deleteInitialHoldingDetailElementforOther = null;
                        alert("Record has been deleted successfully!");
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
}
function fnOtherSecurityUpload() {
    if ($('#fuOtherSecurityUploadFile').val() == "" || $('#fuOtherSecurityUploadFile').val() == null) {
        alert("Please select file to upload");
        return false;
    }
    var ctrl = $('#fuOtherSecurityUploadFile');
    var file = $('#fuOtherSecurityUploadFile').val();

    var ext = file.split(".");
    ext = ext[ext.length - 1].toLowerCase();
    var arExtns = ['xls', 'xlsx'];
    if (arExtns.lastIndexOf(ext) == -1) {
        alert("Please select a file with  extension(s).\n" + arExtns.join(', '));
        ctrl.value = '';
        return false;
    }
    else {
        fnUploadOtherSecurities();
    }
}
function fnUploadOtherSecurities() {
    var param1 = new Date();
    var param2 = param1.getDate() + '_' + (param1.getMonth() + 1) + '_' + param1.getFullYear() + '_' + param1.getHours() + '_' + param1.getMinutes() + '_' + param1.getSeconds();
    var fileUpload = $("#fuOtherSecurityUploadFile").get(0);
    var files = fileUpload.files;
    var test = new FormData();
    for (var i = 0; i < files.length; i++) {
        test.append(files[i].name, files[i]);
    }
    var extn = $('#fuOtherSecurityUploadFile').val().split(".");
    extn = extn[extn.length - 1].toLowerCase();
    var sSaveAs = 'Upload_' + param2 + '_File.' + extn;
    test.append('sSaveAs', sSaveAs);

    var webUrl = uri + "/api/Transaction/UploadOtherSecurities";
    //$.blockUI({ message: $('#divPreLoader'), baseZ: 100000 });
    $("#Loader").show();
    $.ajax({
        type: 'POST',
        url: webUrl,
        data: test,
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        contentType: false,
        processData: false,
        success: function (msg) {
            //$.unblockUI();
            $("#Loader").hide();
            if (msg.StatusFl == false) {
                if (msg.Msg == "Success") {
                    $('#fuOtherSecurityUploadFile').val("");
                    //$('#btnSaveUpload').removeAttr("data-dismiss");

                    return false;
                }
                else {
                    alert(msg.Msg);
                    $('#fuFoodCostUploadFile').val("");
                    $('#btnSaveUpload').removeAttr("data-dismiss");
                    return false;
                }
            }
            else {
                alert("Securities uploaded successfully !");
                setOtherSecurityHolding(msg);
                $('#btnUpload').removeAttr("data-target");
                $('#btnSaveUpload').attr("data-dismiss", "modal");
                return true;
            }
        },
        error: function (response) {
            //$.unblockUI();
            $("#Loader").hide();
            alert(response.status + ' ' + response.statusText);
            $('#fuFoodCostUploadFile').val("");
        }
    });
}
function setOtherSecurityHolding(msg) {
    str = "";
    if (msg.User.lstHoldingDetailforOther !== null) {
        for (var i = 0; i < msg.User.lstHoldingDetailforOther.length; i++) {
            str += '<tr>';
            str += '<td style="display:none;">' + msg.User.lstHoldingDetailforOther[i].restrictedCompany.ID + '</td>';
            str += '<td>' + msg.User.lstHoldingDetailforOther[i].restrictedCompany.companyName + '</td>';
            str += '<td>' + msg.User.lstHoldingDetailforOther[i].restrictedCompany.ISIN + '</td>';
            str += '<td>' + msg.User.lstHoldingDetailforOther[i].SecurityTypeNm + '</td>';
            str += '<td style="display:none;">' + msg.User.lstHoldingDetailforOther[i].relative.ID + '</td>';
            str += '<td>' + msg.User.lstHoldingDetailforOther[i].relative.relativeName + '</td>';
            str += '<td style="display:none;">' + msg.User.lstHoldingDetailforOther[i].dematAccount.ID + '</td>';
            str += '<td>' + msg.User.lstHoldingDetailforOther[i].dematAccount.accountNo + '</td>';
            str += '<td style="text-align: center;">' + msg.User.lstHoldingDetailforOther[i].noOfSecurities + '</td>';
            str += '<td style="display:none;">' + msg.User.lstHoldingDetailforOther[i].relative.panNumber + '</td>';

            str += '<td><a data-target="#modalEquityHoldingForOther" data-toggle="modal" onclick=\"javascript:fnEditInitialDeclarationDetailForOther();\">';
            str += '<img src="../assets/images/edit.png" style="width:24px;height:24px;" /></a>&nbsp;';
            if (msg.User.lstHoldingDetailforOther[i].isDeleteHolding) {
                str += '<a data-target="#modalDeleteHoldingforOther" data-toggle="modal" onclick=\"javascript:fnDeleteInitialHoldingforOther();\">';
                str += '<img src="../assets/images/delete.png" style = "width:24px;height:24px;" /></a>';
            }
            str += '</td>';

            str += '<td style="display:none;">' + msg.User.lstHoldingDetailforOther[i].ID + '</td>';
            str += '<td style="display:none;">' + msg.User.lstHoldingDetailforOther[i].SecurityTypeId + '</td>';
            str += '</tr>';


            //str += '<tr>';
            //str += '<td style="display:none;">' + msg.User.lstHoldingDetailforOther[i].restrictedCompany.ID + '</td>';
            //str += '<td>' + msg.User.lstHoldingDetailforOther[i].restrictedCompany.companyName + '</td>';
            //str += '<td>' + msg.User.lstHoldingDetailforOther[i].restrictedCompany.ISIN + '</td>';
            //str += '<td style="display:none;">' + msg.User.lstHoldingDetailforOther[i].relative.ID + '</td>';
            //str += '<td>' + msg.User.lstHoldingDetailforOther[i].relative.relativeName + '</td>';
            //str += '<td style="display:none;">' + msg.User.lstHoldingDetailforOther[i].dematAccount.ID + '</td>';
            //str += '<td>' + msg.User.lstHoldingDetailforOther[i].dematAccount.accountNo + '</td>';
            //str += '<td style="text-align: center;">' + msg.User.lstHoldingDetailforOther[i].noOfSecurities + '</td>';
            //str += '<td style="display:none;">' + msg.User.lstHoldingDetailforOther[i].relative.panNumber + '</td>';

            //str += '<td><a data-target="#modalEquityHoldingForOther" data-toggle="modal" onclick=\"javascript:fnEditInitialDeclarationDetailForOther();\">';
            //str += '<img src="../assets/images/edit.png" style="width:24px;height:24px;" /></a>&nbsp;';
            //if (msg.User.lstHoldingDetailforOther[i].isDeleteHolding) {
            //    str += '<a data-target="#modalDeleteHoldingforOther" data-toggle="modal" onclick=\"javascript:fnDeleteInitialHoldingforOther();\">';
            //    str += '<img src="../assets/images/delete.png" style = "width:24px;height:24px;" /></a>';
            //}
            //str += '</td>';

            //str += '<td style="display:none;">' + msg.User.lstHoldingDetailforOther[i].ID + '</td>';
            //str += '</tr>';
        }
    }

    $("#tbdEquityforOther").html(str);
}
function fnAddHoldingforOther() {
    //alert("In function fnAddInitialHoldingDeclarationDetail");
    if (fnValidateHoldingforOther()) {
        //alert("In true");
        var obj = new InitialHoldingDetailForOther();
        obj.relative.ID = $("#ddlForOther").val() == null ? 0 : ($("#ddlForOther").val().trim() == "" ? 0 : $("#ddlForOther").val());
        obj.relative.relativeName = $("#ddlForOther option:selected").text();
        obj.dematAccount.ID = $("#ddlDematAccNoForOther").val() == null ? 0 : ($("#ddlDematAccNoForOther").val().trim() == "" ? 0 : $("#ddlDematAccNoForOther").val());
        obj.dematAccount.accountNo = $("#ddlDematAccNoForOther option:selected").text();
        obj.SecurityTypeNm = $("#ContentPlaceHolder1_ddlOSecurityType option:selected").text();
        obj.SecurityTypeId = $("#ContentPlaceHolder1_ddlOSecurityType").val();

        var panNumber = $("#txtPanForOther").val() == null ? 0 : ($("#txtPanForOther").val().trim() == "" ? 0 : $("#txtPanForOther").val());
        if (!isEditInitialHoldingDeclarationDetailForOther) {
            var str = "";
            str += '<tr>';
            str += '<td style="display:none;">' + obj.restrictedCompany.ID + '</td>';
            str += '<td>' + obj.restrictedCompany.companyName + '</td>';
            str += '<td>' + obj.restrictedCompany.ISIN + '</td>';
            str += '<td>' + obj.SecurityTypeNm + '</td>';
            str += '<td style="display:none;">' + obj.relative.ID + '</td>';
            str += '<td>' + obj.relative.relativeName + '</td>';

            str += '<td style="display:none;">' + obj.dematAccount.ID + '</td>';
            str += '<td>' + obj.dematAccount.accountNo + '</td>';
            str += '<td style="text-align: center;">' + obj.noOfSecurities + '</td>';
            str += '<td style="display:none;">' + panNumber + '</td>';

            str += '<td><a data-target="#modalEquityHoldingForOther" data-toggle="modal" onclick=\"javascript:fnEditInitialDeclarationDetailForOther();\">';
            str += '<img src="../assets/images/edit.png" style="width:24px;height:24px;" /></a>&nbsp;';
            str += '<a data-target="#modalDeleteHoldingforOther" data-toggle="modal" onclick=\"javascript:fnDeleteInitialHoldingforOther();\">';
            str += '<img src="../assets/images/delete.png" style = "width:24px;height:24px;" /></a></td>';

            str += '<td style="display:none;">' + obj.ID + '</td>';
            str += '<td style="display:none;">' + obj.SecurityTypeId + '</td>';
            str += '</tr>';
            $("#tbdEquityforOther").append(str);

            if (confirm('Success! Please click ok to continue add equity.')) {

            } else {
                $("#modalEquityHoldingForOther").modal('hide');
            }
        }
        else {

            //alert(editableElementInitialDeclarationDetailForOther[10]);
            $(editableElementInitialDeclarationDetailForOther[0]).html(obj.restrictedCompany.ID);
            $(editableElementInitialDeclarationDetailForOther[1]).html(obj.restrictedCompany.companyName);
            $(editableElementInitialDeclarationDetailForOther[2]).html(obj.restrictedCompany.ISIN);
            $(editableElementInitialDeclarationDetailForOther[3]).html(obj.SecurityTypeNm);
            $(editableElementInitialDeclarationDetailForOther[4]).html(obj.relative.ID);
            $(editableElementInitialDeclarationDetailForOther[5]).html(obj.relative.relativeName);
            $(editableElementInitialDeclarationDetailForOther[6]).html(obj.dematAccount.ID);
            $(editableElementInitialDeclarationDetailForOther[7]).html(obj.dematAccount.accountNo);
            $(editableElementInitialDeclarationDetailForOther[8]).html(obj.noOfSecurities);
            $(editableElementInitialDeclarationDetailForOther[9]).html(panNumber);
            //$(editableElementInitialDeclarationDetailForOther[10]).html(obj.ID);
            $(editableElementInitialDeclarationDetailForOther[12]).html(obj.SecurityTypeId);
            //alert("Success!");
            $("#modalEquityHoldingForOther").modal('hide');
            isEditInitialHoldingDeclarationDetailForOther = false;
        }
        fnCloseModalAddHoldingforOther();
    }
    //alert($("#tbdEquityforOther").children().length);
}
function InitialHoldingDetailForOther() {
    this.ID = 0;
    this.restrictedCompany = new RestrictedCompaniesForOther();
    this.relative = new Relative();
    this.dematAccount = new DematAccount();
    this.noOfSecurities = $("#txtNumberOfSecuritiesForOther").val() == null ? 0 : ($("#txtNumberOfSecuritiesForOther").val().trim() == "" ? 0 : $("#txtNumberOfSecuritiesForOther").val());
}
function RestrictedCompaniesForOther() {

    var company_isin = $("select[id*=ddlRestrictedCompaniesForOther]").val();
    this.ID = company_isin.split(",")[0] == null ? 0 : company_isin.split(",")[0].trim() == "" ? 0 : company_isin.split(",")[0];
    this.companyName = $("select[id*=ddlRestrictedCompaniesForOther] option:selected").text();
    this.ISIN = $("#spnISIN").text();
}
function fnDownloadTemplate() {
    //alert("In function fnDownloadTemplate");
    var sTemplate = $("input[id*=txtOSTemplateNm]").val();
    //alert("sTemplate=" + sTemplate);
    window.open(sTemplate, "_blank");
}
function fnDownloadAnnualTemplate() {
    //alert("In function fnDownloadAnnualTemplate");
    var sTemplate = $("input[id*=txtOSAnnualTemplateNm]").val();
    //alert("sTemplate=" + sTemplate);
    window.open(sTemplate, "_blank");
}
function fnOtherSecurityAnnualUpload() {
    if ($('#fuOtherSecurityAnnualUploadFile').val() == "" || $('#fuOtherSecurityAnnualUploadFile').val() == null) {
        alert("Please select file to upload");
        return false;
    }
    var ctrl = $('#fuOtherSecurityAnnualUploadFile');
    var file = $('#fuOtherSecurityAnnualUploadFile').val();

    var ext = file.split(".");
    ext = ext[ext.length - 1].toLowerCase();
    var arExtns = ['xls', 'xlsx'];
    if (arExtns.lastIndexOf(ext) == -1) {
        alert("Please select a file with  extension(s).\n" + arExtns.join(', '));
        ctrl.value = '';
        return false;
    }
    else {
        fnUploadOtherSecuritiesAnnual();
    }
}
function fnUploadOtherSecuritiesAnnual() {
    var param1 = new Date();
    var param2 = param1.getDate() + '_' + (param1.getMonth() + 1) + '_' + param1.getFullYear() + '_' + param1.getHours() + '_' + param1.getMinutes() + '_' + param1.getSeconds();
    var fileUpload = $("#fuOtherSecurityAnnualUploadFile").get(0);
    var files = fileUpload.files;
    var test = new FormData();
    for (var i = 0; i < files.length; i++) {
        test.append(files[i].name, files[i]);
    }
    var extn = $('#fuOtherSecurityAnnualUploadFile').val().split(".");
    extn = extn[extn.length - 1].toLowerCase();
    var sSaveAs = 'UploadAnnual_' + param2 + '_File.' + extn;
    test.append('sSaveAs', sSaveAs);

    var webUrl = uri + "/api/Transaction/UploadOtherSecuritiesAnnual";
    //$.blockUI({ message: $('#divPreLoader'), baseZ: 100000 });
    $("#Loader").show();
    $.ajax({
        type: 'POST',
        url: webUrl,
        data: test,
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        contentType: false,
        processData: false,
        success: function (msg) {
            //$.unblockUI();
            $("#Loader").hide();
            if (msg.StatusFl == false) {
                if (msg.Msg == "Success") {
                    $('#fuOtherSecurityAnnualUploadFile').val("");
                    //$('#btnSaveUpload').removeAttr("data-dismiss");
                    $("#dvUploadSecurityAnnual").modal('hide');
                    return false;
                }
                else {
                    alert(msg.Msg);
                    $('#fuOtherSecurityAnnualUploadFile').val("");
                    $('#btnSaveUploadAnnual').removeAttr("data-dismiss");
                    return false;
                }
            }
            else {
                alert("Securities uploaded successfully !");
                setOtherSecurityHolding(msg);
                $('#BtnuploadOtherSecuritiesAnnual').removeAttr("data-target");
                $('#btnSaveUploadAnnual').attr("data-dismiss", "modal");
                return true;
            }
        },
        error: function (response) {
            //$.unblockUI();
            $("#Loader").hide();
            alert(response.status + ' ' + response.statusText);
            $('#fuOtherSecurityAnnualUploadFile').val("");
        }
    });
}
function fnGenerateDynamicBodyOfInitialHolding() {
    var result = "";
    for (var index = 0; index < $("#tbdInitialDeclaration").children().length; index++) {
        result += '<tr>';
        var forRelativeOrSelf = $($($("#tbdInitialDeclaration").children()[index]).children()[5]).html();
        var company = $($($("#tbdInitialDeclaration").children()[index]).children()[1]).html();
        var securityType = $($($("#tbdInitialDeclaration").children()[index]).children()[3]).html();
        var dematAccountNo = $($($("#tbdInitialDeclaration").children()[index]).children()[7]).html();
        var numberOfSecurities = $($($("#tbdInitialDeclaration").children()[index]).children()[8]).html();
        result += '<td>' + forRelativeOrSelf + '</td>';
        result += '<td>' + company + '</td>';
        result += '<td>' + securityType + '</td>';
        result += '<td>' + dematAccountNo + '</td>';
        result += '<td style="text-align: right;">' + numberOfSecurities + '</td>';
        result += '</tr>';
    }

    for (var index = 0; index < $("#tbdPhysicalDeclaration").children().length; index++) {
        result += '<tr>';
        var forRelativeOrSelf1 = $($($("#tbdPhysicalDeclaration").children()[index]).children()[5]).html();
        var company1 = $($($("#tbdPhysicalDeclaration").children()[index]).children()[1]).html();
        var securityType1 = $($($("#tbdPhysicalDeclaration").children()[index]).children()[3]).html();
        var dematAccountNo1 = $($($("#tbdPhysicalDeclaration").children()[index]).children()[7]).html();
        var numberOfSecurities1 = $($($("#tbdPhysicalDeclaration").children()[index]).children()[6]).html();
        result += '<td>' + forRelativeOrSelf1 + '</td>';
        result += '<td>' + company1 + '</td>';
        result += '<td>' + securityType1 + '</td>';
        result += '<td>' + dematAccountNo1 + '</td>';
        result += '<td style="text-align: right;">' + numberOfSecurities1 + '</td>';
        result += '</tr>';
    }
    $("#templateBodyInitialHoldingDeclaration").html(result);
}
//function fnGenerateDynamicBodyOfInitialHolding() {
//    var result = "";
//    for (var index = 0; index < $("#tbdInitialDeclaration").children().length; index++) {
//        result += '<tr>';
//        var forRelativeOrSelf = $($($("#tbdInitialDeclaration").children()[index]).children()[5]).html();
//        var company = $($($("#tbdInitialDeclaration").children()[index]).children()[1]).html();
//        var securityType = $($($("#tbdInitialDeclaration").children()[index]).children()[3]).html();
//        var dematAccountNo = $($($("#tbdInitialDeclaration").children()[index]).children()[7]).html();
//        var numberOfSecurities = $($($("#tbdInitialDeclaration").children()[index]).children()[8]).html();
//        result += '<td>' + forRelativeOrSelf + '</td>';
//        result += '<td>' + company + '</td>';
//        result += '<td>' + securityType + '</td>';
//        result += '<td>' + dematAccountNo + '</td>';
//        result += '<td style="text-align: right;">' + numberOfSecurities + '</td>';
//        result += '</tr>';
//    }

//    for (var index = 0; index < $("#tbdPhysicalDeclaration").children().length; index++) {
//        result += '<tr>';
//        var forRelativeOrSelf1 = $($($("#tbdPhysicalDeclaration").children()[index]).children()[5]).html();
//        var company1 = $($($("#tbdPhysicalDeclaration").children()[index]).children()[1]).html();
//        var securityType1 = $($($("#tbdPhysicalDeclaration").children()[index]).children()[3]).html();
//        var dematAccountNo1 = $($($("#tbdPhysicalDeclaration").children()[index]).children()[7]).html();
//        var numberOfSecurities1 = $($($("#tbdPhysicalDeclaration").children()[index]).children()[6]).html();
//        result += '<td>' + forRelativeOrSelf1 + '</td>';
//        result += '<td>' + company1 + '</td>';
//        result += '<td>' + securityType1 + '</td>';
//        result += '<td>' + dematAccountNo1 + '</td>';
//        result += '<td style="text-align: right;">' + numberOfSecurities1 + '</td>';
//        result += '</tr>';
//    }    
//    $("#templateBodyInitialHoldingDeclaration").html(result);
//}
