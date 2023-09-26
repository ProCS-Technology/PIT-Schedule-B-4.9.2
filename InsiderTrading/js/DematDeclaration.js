function saveDematAccounts() {

    var allowNext = true;//when selected not applicable

    var personalInformation = new PersonalInformation();
    for (var index = 0; index < $("#tbdDematList").children().length; index++) {
        var obj = new DematAccount();
        var relative = new Relative();
        obj.ID = $($($("#tbdDematList").children()[index]).children()[0]).html();
        relative.ID = $($($("#tbdDematList").children()[index]).children()[1]).html();
        relative.relativeName = $($($("#tbdDematList").children()[index]).children()[2]).html();
        obj.depositoryName = $($($("#tbdDematList").children()[index]).children()[3]).html();
        obj.clientId = $($($("#tbdDematList").children()[index]).children()[4]).html();
        obj.depositoryParticipantName = $($($("#tbdDematList").children()[index]).children()[5]).html();
        obj.depositoryParticipantId = $($($("#tbdDematList").children()[index]).children()[6]).html();
        obj.tradingMemberId = $($($("#tbdDematList").children()[index]).children()[7]).html();
        obj.accountNo = $($($("#tbdDematList").children()[index]).children()[8]).html();
        obj.status = $($($("#tbdDematList").children()[index]).children()[9]).html();
        relative.lstDematAccount.push(obj);
        personalInformation.lstRelative.push(relative);
        //alert(relative.relativeName);
        if (relative.relativeName == 'Not Applicable') {
            allowNext = false;
        }
    }
    //alert(index);
    //alert(allowNext);
    if (index > 1 && allowNext == false) {
        alert('Please Delete the DEMAT added as "Not Applicable" only in order to continue as you have provided other DEMAT information.');
        return false;
    }

    $("#Loader").show();
    var token = $("#TokenKey").val();

    var webUrl = uri + "/api/Transaction/SaveRelativeDematDetails";
    $.ajax({
        url: webUrl,
        type: "POST",
        headers: {
            'TokenKeyH': token,
        },
        data: JSON.stringify(personalInformation),
        async: false,
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            $("#Loader").hide();
            if (msg.StatusFl == true) {
                alert("Demat account details updated successfully !");
                fnGetDPHoldingDetails();
                //fnGetTransactionalInfo();

                $("#liPI").removeClass('active');
                $("#liRD").removeClass('active');
                $("#liDA").removeClass('active');
                $("#liIH").addClass('active');
                $("#liCon").removeClass('active');

                $("#tab1").removeClass('active');
                $("#tab2").removeClass('active');
                $("#tab3").removeClass('active');
                $("#tab4").addClass('active');
                $("#tab5").removeClass('active');

                $("#tab4").click();
                $("#tab4").show();
                $("#tab5").hide();
                $("#tab1").hide();
                $("#tab2").hide();
                $("#tab3").hide();
                $("#spnTitle").html("Step 4 of 5");

                $("#aSavenContinue").show();
                $("#aSubmitYourDeclaration").hide();
                $("#spnblinkpreview").hide();

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
function DematAccount() {
    this.ID = 0;
    this.depositoryName = $("#ddlDepositoryName").val() == null ? 0 : ($("#ddlDepositoryName").val().trim() == "" ? 0 : $("#ddlDepositoryName").val());
    this.clientId = $("#txtClientId").val() == null ? 0 : ($("#txtClientId").val().trim() == "" ? 0 : $("#txtClientId").val());
    this.depositoryParticipantName = $("#txtDepositoryParticipantName").val() == null ? "" : ($("#txtDepositoryParticipantName").val().trim() == "" ? "" : $("#txtDepositoryParticipantName").val());
    this.depositoryParticipantId = $("#txtDepositoryParticipantId").val() == null ? 0 : ($("#txtDepositoryParticipantId").val().trim() == "" ? 0 : $("#txtDepositoryParticipantId").val());
    this.accountNo = $("#txtDematAccountNumber").val() == null ? 0 : ($("#txtDematAccountNumber").val().trim() == "" ? 0 : $("#txtDematAccountNumber").val());
    if (this.depositoryName == "NSDL") {
        this.depositoryParticipantId = "IN" + this.depositoryParticipantId;
        this.accountNo = "IN" + this.accountNo;
    }
    else {
        this.depositoryParticipantId = this.depositoryParticipantId;
        this.accountNo = this.accountNo;
    }
    this.tradingMemberId = $("#txtTradingMemberId").val() == null ? 0 : ($("#txtTradingMemberId").val().trim() == "" ? 0 : $("#txtTradingMemberId").val());

    this.status = $("#ddlStatusDemat").val() == null ? 0 : ($("#ddlStatusDemat").val().trim() == "" ? 0 : $("#ddlStatusDemat").val());
}
function fnValidateDematDetail() {
    var count = 0;
    if ($("#ddlDematAccountDetailsFor").val() == null || $("#ddlDematAccountDetailsFor").val() == undefined || $("#ddlDematAccountDetailsFor").val().trim() == "") {
        count++;
        $("#lblDematAccountDetailsFor").addClass('requiredlbl');
    }
    else {
        $("#lblDematAccountDetailsFor").removeClass('requiredlbl');
    }
    //alert("Depository Selected=" + $("#ddlDepositoryName").val());
    if ($("#ddlDepositoryName").val() == null || $("#ddlDepositoryName").val() == undefined || $("#ddlDepositoryName").val().trim() == "") {
        count++;
        $("#lblDepositoryName").addClass('requiredlbl');
    }
    else {
        $("#lblDepositoryName").removeClass('requiredlbl');
    }
    if ($("#txtDepositoryParticipantId").val() == null || $("#txtDepositoryParticipantId").val() == undefined || $("#txtDepositoryParticipantId").val().trim() == "") {
        count++;
        $("#lblDepositoryParticipantId").addClass('requiredlbl');
    }
    else {
        var sDepositoryNm = $("#ddlDepositoryName").val();
        //var sDepositoryParticipantId = $("#txtDepositoryParticipantId").val();
        $("#lblDepositoryParticipantId").removeClass('requiredlbl');
        if (sDepositoryNm == "CDSL") {
            if ($("#txtDepositoryParticipantId").val().length != 8) {
                count++;
                $("#lblDepositoryParticipantId").addClass('requiredlbl');
                alert("Depository Participant Id should 8 character long");
            }
        }
        else if (sDepositoryNm == "NSDL") {
            if ($("#txtDepositoryParticipantId").val().length != 6) {
                count++;
                $("#lblDepositoryParticipantId").addClass('requiredlbl');
                alert("Depository Participant Id should 6 character long");
            }
        }
    }
    if ($("#txtClientId").val() == null || $("#txtClientId").val() == undefined || $("#txtClientId").val().trim() == "") {
        count++;
        $("#lblClientId").addClass('requiredlbl');
    }
    else {
        $("#lblClientId").removeClass('requiredlbl');

        var sDepositoryNm = $("#ddlDepositoryName").val();
        if (sDepositoryNm == "CDSL" || sDepositoryNm == "NSDL") {
            if ($("#txtClientId").val().length != 8) {
                count++;
                $("#lblClientId").addClass('requiredlbl');
                alert("Client Id should 8 character long");
            }
        }
    }
    if ($("#txtDematAccountNumber").val() == null || $("#txtDematAccountNumber").val() == undefined || $("#txtDematAccountNumber").val().trim() == "") {
        count++;
        $("#lblDematAccountNumber").addClass('requiredlbl');
    }
    else {
        $("#lblDematAccountNumber").removeClass('requiredlbl');
    }
    if ($("#ddlStatusDemat").val() == null || $("#ddlStatusDemat").val() == undefined || $("#ddlStatusDemat").val().trim() == "") {
        count++;
        $("#lblStatusDemat").addClass('requiredlbl');
    }
    else {
        $("#lblStatusDemat").removeClass('requiredlbl');
    }

    if ($("#txtDematAccountNumber").val() != "Not Applicable") {
        for (var index = 0; index < $("#tbdDematList").children().length; index++) {
            if (($("#spNsdlDematLabel").html() + $("#txtDematAccountNumber").val()) == $($($("#tbdDematList").children()[index]).children()[8]).html() && dematAccountNumberGlobal != ($("#spNsdlDematLabel").html() + $("#txtDematAccountNumber").val())) {
                alert("Demat Account Number already exist");
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
function fnCloseModalAddDematDetail() {
    fnClearFormDematDetail();
}
function fnClearFormDematDetail() {
    $("#ddlDematAccountDetailsFor").val('');
    $("#ddlDepositoryName").val('');
    $("#txtClientId").val('');
    $("#txtDepositoryParticipantName").val('');
    $("#txtDepositoryParticipantId").val('');
    $("#txtTradingMemberId").val('');
    $("#txtDematAccountNumber").val('');
    $("#ddlStatusDemat").val('');
    isEditDemat = false;
    editableElementDemat = null;
    $("#ddlDematAccountDetailsFor").removeClass('requiredlbl');
    $("#ddlDepositoryName").removeClass('requiredlbl');
    $("#txtClientId").removeClass('requiredlbl');
    $("#txtDepositoryParticipantName").removeClass('requiredlbl');
    $("#txtDepositoryParticipantId").removeClass('requiredlbl');
    $("#txtTradingMemberId").removeClass('requiredlbl');
    $("#txtDematAccountNumber").removeClass('requiredlbl');
    $("#ddlStatusDemat").removeClass('requiredlbl');
    $("#spNsdlLabel").html('');
    $("#spNsdlDematLabel").html('');
    dematAccountNumberGlobal = '';
}
function fnEditDematDetail() {
    isEditDemat = true;
    var selectedTr = $(event.currentTarget).closest('tr').children();
    editableElementDemat = selectedTr;
    var relativeId = $(selectedTr[1]).html();
    var relativeName = $(selectedTr[2]).html();
    var depositoryName = $(selectedTr[3]).html();
    var clientId = $(selectedTr[4]).html();
    var depositoryParticipantName = $(selectedTr[5]).html();
    var depositoryParticipantId = $(selectedTr[6]).html();
    var tradingMemberId = $(selectedTr[7]).html();
    var dematAccountNo = $(selectedTr[8]).html();
    dematAccountNumberGlobal = dematAccountNo;
    var status = $(selectedTr[9]).html();

    $("#ddlDematAccountDetailsFor").val(relativeId);
    $("#ddlDepositoryName").val(depositoryName);
    $("#txtClientId").val(clientId);
    $("#txtDepositoryParticipantName").val(depositoryParticipantName);
    if (depositoryName == "NSDL") {
        $("#txtDepositoryParticipantId").val(depositoryParticipantId.replace("IN", ""));
        $("#txtDematAccountNumber").val(dematAccountNo.replace("IN", ""));
    }
    else {
        $("#txtDepositoryParticipantId").val(depositoryParticipantId);
        $("#txtDematAccountNumber").val(dematAccountNo);
    }
    $("#txtTradingMemberId").val(tradingMemberId);

    $("#ddlStatusDemat").val(status);
    if (depositoryName == "NSDL") {
        $("#spNsdlLabel").html('IN');
        $("#spNsdlDematLabel").html('IN');
    }
    else {
        $("#spNsdlLabel").html('');
        $("#spNsdlDematLabel").html('');
    }
}
function getAllDematAccountsOfRelative(relativeId) {
    $("#Loader").show();
    var webUrl = uri + "/api/DematAccount/GetDematAccountListByRelativeId";
    $.ajax({
        url: webUrl,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        data: JSON.stringify({
            ID: relativeId
        }),
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                var result = "";
                result += "<option value=''></option>";
                if (msg.Relative.lstDematAccount !== null) {
                    for (var i = 0; i < msg.Relative.lstDematAccount.length; i++) {
                        result += "<option value='" + msg.Relative.lstDematAccount[i].ID + "'>" + msg.Relative.lstDematAccount[i].accountNo + "</option>";
                    }
                }

                $("#ddlDematAccNo").html(result);
                //$("#ddlPhysicalDematAccNo").html(result);
            }
            else {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    alert(msg.Msg);
                    $("#ddlDematAccNo").html('');
                    $("#ddlPhysicalDematAccNo").html('');
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
function getDematAccountInfoById(dematAccountId) {
    $("#Loader").show();
    var webUrl = uri + "/api/DematAccount/DematAccountInfo";
    $.ajax({
        url: webUrl,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        data: JSON.stringify({
            ID: dematAccountId
        }),
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                $("#txtTradingMemId").val(msg.DematAccount.tradingMemberId);
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
function setDematInformation(msg) {
    var str = "";
    if (msg.User.lstDematAccount !== null) {
        for (var i = 0; i < msg.User.lstDematAccount.length; i++) {
            str += '<tr>';
            str += '<td style="display:none;">' + msg.User.lstDematAccount[i].ID + '</td>';
            if (msg.User.lstDematAccount[i].relative == null) {
                msg.User.lstDematAccount[i].relative = new Object();
                msg.User.lstDematAccount[i].relative.ID = -1;
                msg.User.lstDematAccount[i].relative.relativeName = "Not Applicable";
            }
            str += '<td style="display:none;">' + msg.User.lstDematAccount[i].relative.ID + '</td>';
            str += '<td>' + msg.User.lstDematAccount[i].relative.relativeName + '</td>';
            str += '<td>' + msg.User.lstDematAccount[i].depositoryName + '</td>';
            str += '<td>' + msg.User.lstDematAccount[i].clientId + '</td>';
            str += '<td>' + msg.User.lstDematAccount[i].depositoryParticipantName + '</td>';
            str += '<td>' + msg.User.lstDematAccount[i].depositoryParticipantId + '</td>';
            str += '<td style="display:none;">' + msg.User.lstDematAccount[i].tradingMemberId + '</td>';
            str += '<td>' + msg.User.lstDematAccount[i].accountNo + '</td>';
            str += '<td>' + msg.User.lstDematAccount[i].status + '</td>';

            str += '<td><a data-target="#modalAddDematDetail" data-toggle="modal" onclick=\"javascript:fnEditDematDetail();\">';
            str += '<img src="../assets/images/edit.png" style="width:24px;height:24px;" /></a>&nbsp;';
            //if (msg.User.lstDematAccount[i].isDeleteDemat || msg.User.lstDematAccount[i].relative.relativeName == 'Not Applicable') {
            str += '<a data-target="#modalDeleteDematDetail" data-toggle="modal" onclick=\"javascript:fnDeleteDematDetail();\">';
            str += '<img src="../assets/images/delete.png" style = "width:24px;height:24px;" /></a>';
            //}
            str += '</td>';
            str += '</tr>';
        }
    }
    $("#tbdDematList").html(str);
}
function fnDeleteDematDetail() {
    var selectedTr = $(event.currentTarget).closest('tr').children();
    var dematAccountId = $(selectedTr[0]).html();

    deleteDematDetailElement = $(event.currentTarget).closest('tr');
    $("#txtDeleteDematDetailId").val(dematAccountId);
}
function fnDeleteDematDetailModal() {

    if ($("#txtDeleteDematDetailId").val() == "0") {
        if (deleteDematDetailElement != null) {
            deleteDematDetailElement.remove();
            deleteDematDetailElement = null;
            alert("Record has been deleted successfully!");
        }
    }
    else {
        $("#Loader").show();
        var webUrl = uri + "/api/Transaction/DeleteDematDetail";
        $.ajax({
            url: webUrl,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            data: JSON.stringify({
                dematInfo: { ID: $("#txtDeleteDematDetailId").val() }
            }),
            success: function (msg) {
                $("#Loader").hide();
                if (isJson(msg)) {
                    msg = JSON.parse(msg);
                }
                if (msg.StatusFl == true) {
                    if (deleteDematDetailElement != null) {
                        deleteDematDetailElement.remove();
                        deleteDematDetailElement = null;
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
function fnAddDematDetail() {
    if (fnValidateDematDetail()) {
        var relative = new Relative();
        relative.ID = $("#ddlDematAccountDetailsFor").val() == null ? 0 : ($("#ddlDematAccountDetailsFor").val().trim() == "" ? 0 : $("#ddlDematAccountDetailsFor").val());;
        relative.relativeName = $("#ddlDematAccountDetailsFor option:selected").text();
        var obj = new DematAccount();

        if (!isEditDemat) {
            var str = "";
            str += '<tr>';
            str += '<td style="display:none;">' + obj.ID + '</td>';
            str += '<td style="display:none;">' + relative.ID + '</td>';
            str += '<td>' + relative.relativeName + '</td>';
            str += '<td>' + obj.depositoryName + '</td>';
            str += '<td>' + obj.clientId + '</td>';
            str += '<td>' + obj.depositoryParticipantName + '</td>';
            str += '<td>' + obj.depositoryParticipantId + '</td>';
            str += '<td style="display:none;">' + obj.tradingMemberId + '</td>';
            str += '<td>' + obj.accountNo + '</td>';
            str += '<td>' + obj.status + '</td>';

            str += '<td><a data-target="#modalAddDematDetail" data-toggle="modal" onclick=\"javascript:fnEditDematDetail();\">';
            str += '<img src="../assets/images/edit.png" style="width:24px;height:24px;" /></a>&nbsp;';
            str += '<a data-target="#modalDeleteDematDetail" data-toggle="modal" onclick=\"javascript:fnDeleteDematDetail();\">';
            str += '<img src="../assets/images/delete.png" style = "width:24px;height:24px;" /></a></td>';

            //str += '<td><a data-target="#modalAddDematDetail" data-toggle="modal" class="btn btn-outline dark" onclick=\"javascript:fnEditDematDetail();\">Edit</a>';
            //str += '&nbsp;<a data-target="#modalDeleteDematDetail" data-toggle="modal" class="btn btn-outline dark" onclick=\"javascript:fnDeleteDematDetail();\">Delete</a></td>';
            str += '</tr>';
            $("#tbdDematList").append(str);
        }
        else {
            $(editableElementDemat[1]).html(relative.ID);
            $(editableElementDemat[2]).html(relative.relativeName);
            $(editableElementDemat[3]).html(obj.depositoryName);
            $(editableElementDemat[4]).html(obj.clientId);
            $(editableElementDemat[5]).html(obj.depositoryParticipantName);
            $(editableElementDemat[6]).html(obj.depositoryParticipantId);
            $(editableElementDemat[7]).html(obj.tradingMemberId);
            $(editableElementDemat[8]).html(obj.accountNo);
            $(editableElementDemat[9]).html(obj.status);
        }
        fnClearFormDematDetail();
        $("#modalAddDematDetail").modal('hide');
    }
}