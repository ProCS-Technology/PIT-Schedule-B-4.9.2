$(document).ready(function () {
    $("#Loader").hide();
    fnGetUserList();
    fnGetDepositoryType();
    fnGetThresholdByTimeSettings();
    fnGetPeriodicDeclaration();
    fnGetCompanyNameAndISIN();
    //fnGetInsiderTradingWindowClosureInfo();
    //$('body').on('click', '.btAddShareValues', function () {
    //    var str = '';
    //    str += '<tr>';
    //    str += '<td><input type="text"/></td>';
    //    str += '<td style="padding-left:4px;padding-top:4px;"><input type="button" class="btn blue btn-outline btAddShareValues" value="Add"/></td>';
    //    str += '<td style="padding-left:4px;padding-top:4px;"><input type="button" class="btn red btn-outline btRemoveShareValues" value="Remove"/></td>';
    //    str += '</tr>';
    //    $('#tbodyNumberOfShares').append(str);
    //})
    //$('body').on('click', '.btRemoveShareValues', function () {
    //    $(this).closest('tr').remove();
    //})
});
function fnRollBack() {
    window.location.reload();
}
function fnGetUserList() {
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/GetUserListForApprover";
    $.ajax({
        url: webUrl,
        type: "GET",
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            if (msg.StatusFl == true) {
                var result = "";
                var approver = "";
                var approverForCO = "";
                for (var i = 0; i < msg.UserList.length; i++) {
                    if (msg.UserList[i].isApprover == "Yes") {
                        approver = msg.UserList[i].LOGIN_ID;
                    }
                    if (msg.UserList[i].isApproverForCO == "Yes") {
                        approverForCO = msg.UserList[i].LOGIN_ID;
                    }

                    result += '<option value="' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].USER_EMAIL + ' (' + msg.UserList[i].USER_NM + ')' + '</option>';
                }
                $("#ddlUserList").append(result);
                $("#ddlUserList").val(approver);

                $("#ddlUserListForCOApprover").append(result);
                $("#ddlUserListForCOApprover").val(approverForCO);
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
    })
}
function fnSaveThresholdLimitAndByTime() {
    if (fnValidateThresholdByTimeSettings()) {
        var depository = new Array();
        for (var i = 0; i < $("#tbodyNumberOfShares").children().length; i++) {
            var obj = new Object();

            var depositoryId = $($("#tbodyNumberOfShares").children()[i]).children()[0].innerHTML;
            obj.depositoryId = depositoryId;
            var depositoryName = $($("#tbodyNumberOfShares").children()[i]).children()[1].innerHTML;
            obj.depositoryName = depositoryName;
            var numberOfShares = $($($("#tbodyNumberOfShares").children()[i]).children()[2]).children()[0].value;
            obj.sharesCount = numberOfShares;
            //for (var j = 0; j < $("#tbodyThresholdLimit").children().length; j++) {
            //    if ($($("#tbodyThresholdLimit").children()[j]).children()[0].innerHTML === depositoryId) {
            var thresholdLimit = $($($("#tbodyThresholdLimit").children()[0]).children()[2]).children()[0].value;
            obj.thresholdLimit = thresholdLimit;
            var byTime = $($($("#tbodyThresholdLimit").children()[0]).children()[3]).children()[0].value;
            obj.byTime = byTime;

            var limitTypeQty = $("input[id='optionsQty']:checked").val();
            var limitTypeVal = $("input[id='optionsVal']:checked").val();

            if (limitTypeQty != undefined) {
                obj.limitType = "Quantity";
            }
            else if (limitTypeVal != undefined) {
                obj.limitType = "Value";
            }
            //break;
            //    }
            //}
            depository.push(obj);
        }
        $("#Loader").show();
        var webUrl = uri + "/api/UserIT/SaveThresholdLimitAndByTime";
        $.ajax({
            url: webUrl,
            type: "POST",
            data: JSON.stringify(depository),
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            async: false,
            success: function (msg) {
                if (msg.StatusFl == true) {
                    window.location.reload();
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
        })
    }
}
function fnGetDepositoryType() {
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/GetDepositoryType";
    $.ajax({
        url: webUrl,
        type: "GET",
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            if (msg.StatusFl == true) {
                $("#frmThresholdSettings").show();
                var tempArr = new Array();
                for (var i = 0; i < msg.DepositoryList.length; i++) {
                    tempArr.push(msg.DepositoryList[i].depositoryName)
                }
                $('#depositoryType').multiselect('select', tempArr);
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
    })
}
function fnValidateThresholdByTimeSettings() {
    var depository = new Array();
    for (var i = 0; i < $("#tbodyNumberOfShares").children().length; i++) {
        var obj = new Object();

        var depositoryId = $($("#tbodyNumberOfShares").children()[i]).children()[0].innerHTML;
        obj.depositoryId = depositoryId;
        var numberOfShares = $($($("#tbodyNumberOfShares").children()[i]).children()[2]).children()[0].value;

        if (numberOfShares.trim() == '' || numberOfShares == null || numberOfShares == undefined) {
            alert("Shares cannot be empty.");
            return false;
        }

        var thresholdLimit = $($($("#tbodyThresholdLimit").children()[0]).children()[2]).children()[0].value;
        if (thresholdLimit.trim() == '' || thresholdLimit == null || thresholdLimit == undefined) {
            alert("Threshold Limit cannot be empty.");
            return false;
        }
        var byTime = $($($("#tbodyThresholdLimit").children()[0]).children()[3]).children()[0].value;
        obj.byTime = byTime;
        if (byTime.trim() == '' || byTime.trim() == '0' || byTime == null || byTime == undefined) {
            alert("Time cannot be empty.");
            return false;
        }


        //for (var j = 0; j < $("#tbodyThresholdLimit").children().length; j++) {
        //    if ($($("#tbodyThresholdLimit").children()[j]).children()[0].innerHTML === depositoryId) {
        //        var thresholdLimit = $($($("#tbodyThresholdLimit").children()[j]).children()[2]).children()[0].value;
        //        if (thresholdLimit.trim() == '' || thresholdLimit == null || thresholdLimit == undefined) {
        //            alert("Threshold Limit cannot be empty.");
        //            return false;
        //        }
        //        var byTime = $($($("#tbodyThresholdLimit").children()[j]).children()[3]).children()[0].value;
        //        obj.byTime = byTime;
        //        if (byTime.trim() == '' || byTime.trim() == '0' || byTime == null || byTime == undefined) {
        //            alert("Time cannot be empty.");
        //            return false;
        //        }
        //        break;
        //    }
        //}
        depository.push(obj);
    }

    var limitTypeQty = $("input[id='optionsQty']:checked").val();
    var limitTypeVal = $("input[id='optionsVal']:checked").val();

    if (limitTypeQty == undefined && limitTypeVal == undefined) {
        alert("Please select Thresold Limit type.");
        return false;
    }

    if (depository.length > 0) {
        var temp = depository[0].byTime;
        for (var i = 1; i < depository.length; i++) {
            if (temp != depository[i].byTime) {
                alert("Time should be same for each deposiotry name.");
                return false;
                break;
            }
        }
    }
    return true;
}
function fnGetThresholdByTimeSettings() {
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/GetThresholdByTimeSettings";
    $.ajax({
        url: webUrl,
        type: "GET",
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            if (msg.StatusFl == true) {
                $("#frmThresholdSettings").show();
                var str = '';
                var str1 = '';
                var tempArr = new Array();
                for (var i = 0; i < msg.DepositoryList.length; i++) {
                    str += '<tr>';
                    str += '<td style="display:none">';
                    str += msg.DepositoryList[i].depositoryId;
                    str += '</td>';
                    str += '<td style="padding-top: 8px;">';
                    str += msg.DepositoryList[i].depositoryName;
                    str += '</td>';
                    str += '<td style="padding-top: 8px;padding-left:2px;">';
                    str += '<input type="number" value="' + msg.DepositoryList[i].sharesCount + '" />';
                    str += '</td>';
                    str += '</tr>';


                    //str1 += '<tr>';
                    //str1 += '<td style="display:none">';
                    //str1 += msg.DepositoryList[i].depositoryId;
                    //str1 += '</td>';
                    //str1 += '<td style="padding-top: 8px;">';
                    //str1 += msg.DepositoryList[i].depositoryName;
                    //str1 += '</td>';
                    //str1 += '<td style="padding-top: 8px;padding-left:2px;">';
                    //str1 += '<input type="number" value="' + msg.DepositoryList[i].thresholdLimit + '"/>';
                    //str1 += '</td>';
                    //str1 += '<td style="padding-top: 8px;padding-left:2px;">';
                    //str1 += '<select id="byTime_' + msg.DepositoryList[i].depositoryId + '">';
                    //str1 += '<option value="0"></option>';
                    //str1 += '<option value="monthly">Monthly</option>';
                    //str1 += '<option value="quarterly">Quarterly</option>';
                    //str1 += '<option value="bi_Annually">Bi-Annually</option>';
                    //str1 += '<option value="annually">Annually</option>';
                    //str1 += '</select>';
                    //str1 += '</td>';
                    //str1 += '</tr>';
                    var obj = new Object();
                    obj.key = msg.DepositoryList[i].depositoryId
                    obj.value = msg.DepositoryList[i].byTime;
                    tempArr.push(obj);

                    if (msg.DepositoryList[i].limitType == 'Value') {
                        $("#optionsQty").prop("checked", false);
                        $("#optionsVal").prop("checked", true);
                    }
                    else if (msg.DepositoryList[i].limitType == 'Quantity') {
                        $("#optionsQty").prop("checked", true);
                        $("#optionsVal").prop("checked", false);
                    }
                    else {
                        $("#optionsQty").prop("checked", false);
                        $("#optionsVal").prop("checked", false);
                    }
                }
                $("#tbodyNumberOfShares").html(str);


                str1 += '<tr>';
                str1 += '<td style="display:none">';
                str1 += msg.DepositoryList[0].depositoryId;
                str1 += '</td>';
                str1 += '<td style="padding-top: 8px;">CDSL, NSDL & Physical</td>';
                str1 += '<td style="padding-top: 8px;padding-left:2px;">';
                str1 += '<input type="number" value="' + msg.DepositoryList[0].thresholdLimit + '"/>';
                str1 += '</td>';
                str1 += '<td style="padding-top: 8px;padding-left:2px;">';
                str1 += '<select id="byTime_' + msg.DepositoryList[0].depositoryId + '">';
                str1 += '<option value="0"></option>';
                str1 += '<option value="monthly">Monthly</option>';
                str1 += '<option value="quarterly">Quarterly</option>';
                str1 += '<option value="bi_Annually">Bi-Annually</option>';
                str1 += '<option value="annually">Annually</option>';
                str1 += '</select>';
                str1 += '</td>';
                str1 += '</tr>';

                if (msg.DepositoryList[0].limitType == 'Value') {
                    $("#optionsQty").prop("checked", false);
                    $("#optionsVal").prop("checked", true);
                }
                else if (msg.DepositoryList[0].limitType == 'Quantity') {
                    $("#optionsQty").prop("checked", true);
                    $("#optionsVal").prop("checked", false);
                }
                else {
                    $("#optionsQty").prop("checked", false);
                    $("#optionsVal").prop("checked", false);
                }

                $("#tbodyThresholdLimit").html(str1);

                $.each(tempArr, function (index, item) {
                    $("#byTime_" + item.key).val(item.value);
                })

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
    })
}
function fnSaveDepositoryType() {
    if (fnValidateDepositoryType()) {
        fnSaveDepositoryTypeOperation();
    }
}
function fnSaveDepositoryTypeOperation() {
    var depositoryType = $("#depositoryType").val();
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/SaveDepositoryTypeOperation";
    $.ajax({
        url: webUrl,
        type: "POST",
        data: JSON.stringify({ depository: depositoryType }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            if (msg.StatusFl == true) {
                $("#Loader").hide();
                window.location.reload();
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
function fnSaveAuthorizedPerson() {
    if (fnValidate()) {
        fnAssignedApprover();
    }
}
function fnAssignedApprover() {
    var selectedUser = $("#ddlUserList").val();
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/AssignedApprover";
    $.ajax({
        url: webUrl,
        type: "POST",
        data: JSON.stringify({ LOGIN_ID: selectedUser }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            if (msg.StatusFl == true) {
                $("#Loader").hide();
                window.location.reload();
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
function fnValidate() {
    var selectedUser = $("#ddlUserList").val();
    var count = 0;
    if (selectedUser == undefined || selectedUser == null || selectedUser == '0') {
        count++;
        $('#lblUser').addClass('requied');

    }
    else {
        $('#lblUser').removeClass('requied');
    }

    if (count > 0) {
        return false;
    }
    else {
        return true;
    }
}
function fnValidateDepositoryType() {
    var count = 0;
    if ($("#depositoryType").val() == null || $("#depositoryType").val() == undefined) {
        count++;
        $('#lblDepositoryType').addClass('requied');
    }
    else {
        $('#lblDepositoryType').removeClass('requied');
    }

    if (count > 0) {
        return false;
    }
    else {
        return true;
    }
}
function fnSaveTaskForPeriodicDeclaration() {
    if (fnVaidateTaskForPeriodicDeclaration()) {
        fnAddTaskForPeriodicDeclaration();
    }
}
function fnAddTaskForPeriodicDeclaration() {
    var declarationDate = $("#txtPeriodicDeclarationDate").val();
    var frequencyInMonths = $("#txtPeriodicDeclarationFrequencyInMonths").val();
    var validTillInDays = $("#txtPeriodicDeclarationValidTill").val();
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/AddTaskForPeriodicDeclaration";
    $.ajax({
        url: webUrl,
        type: "POST",
        data: JSON.stringify({ declarationDate: declarationDate, frequencyInMonths: frequencyInMonths, validTillInDays: validTillInDays }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        //  async: false,
        success: function (msg) {
            if (msg.StatusFl == true) {
                $("#Loader").hide();
                alert(msg.Msg);
                window.location.reload();
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
function fnVaidateTaskForPeriodicDeclaration() {
    if ($("#txtPeriodicDeclarationDate").val() == undefined || $("#txtPeriodicDeclarationDate").val() == null || $("#txtPeriodicDeclarationDate").val().trim() == "") {
        return false;
    }
    if ($("#txtPeriodicDeclarationFrequencyInMonths").val() == undefined || $("#txtPeriodicDeclarationFrequencyInMonths").val() == null || $("#txtPeriodicDeclarationFrequencyInMonths").val().trim() == "") {
        return false;
    }
    if ($("#txtPeriodicDeclarationValidTill").val() == undefined || $("#txtPeriodicDeclarationValidTill").val() == null || $("#txtPeriodicDeclarationValidTill").val().trim() == "") {
        return false;
    }
    return true;
}
function fnGetPeriodicDeclaration() {
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/GetPeriodicDeclaration";
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
                if (msg.PeriodicDeclaration !== undefined && msg.PeriodicDeclaration !== null) {
                    $('#txtPeriodicDeclarationDate').datepicker("setDate", msg.PeriodicDeclaration.declarationDate);
                    $('#txtPeriodicDeclarationValidTill').val(msg.PeriodicDeclaration.validTillInDays);
                    $('#txtPeriodicDeclarationFrequencyInMonths').val(msg.PeriodicDeclaration.frequencyInMonths);
                }
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
function fnSaveCompanyNameAndISIN() {
    if (fnValidateCompanyNameAndISIN()) {
        fnAddCompanyNameAndISIN();
    }
}
function fnAddCompanyNameAndISIN() {
    var companyName = $("#txtCompanyName").val();
    var companyISIN = $("#txtCompanyISIN").val();
    var totalPhysicalShares = $("#txtTotalPhysicalShares").val();
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/AddCompanyNameAndISIN";
    $.ajax({
        url: webUrl,
        type: "POST",
        data: JSON.stringify({ companyName: companyName, companyISIN: companyISIN, totalPhysicalShares: totalPhysicalShares }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            if (msg.StatusFl == true) {
                $("#Loader").hide();
                alert(msg.Msg);
                window.location.reload();
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
function fnValidateCompanyNameAndISIN() {
    if ($("#txtTotalPhysicalShares").val() == undefined || $("#txtTotalPhysicalShares").val() == null || $("#txtTotalPhysicalShares").val().trim() == "") {
        return false;
    }
    if ($("#txtCompanyName").val() == undefined || $("#txtCompanyName").val() == null || $("#txtCompanyName").val().trim() == "") {
        return false;
    }
    if ($("#txtCompanyISIN").val() == undefined || $("#txtCompanyISIN").val() == null || $("#txtCompanyISIN").val().trim() == "") {
        return false;
    }
    return true;
}
function fnGetCompanyNameAndISIN() {
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/GetCompanyNameAndISIN";
    $.ajax({
        url: webUrl,
        type: "GET",
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            if (msg.StatusFl == true) {
                $("#Loader").hide();
                $("#txtCompanyName").val(msg.CompanySettings.companyName);
                $("#txtCompanyISIN").val(msg.CompanySettings.companyISIN);
                $("#txtTotalPhysicalShares").val(msg.CompanySettings.totalPhysicalShares);
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
function fnSaveAuthorizedPersonCO() {
    if (fnValidateCO()) {
        fnAssignedApproverCO();
    }
}
function fnAssignedApproverCO() {
    var selectedUser = $("#ddlUserListForCOApprover").val();
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/AssignedApproverForCO";
    $.ajax({
        url: webUrl,
        type: "POST",
        data: JSON.stringify({ LOGIN_ID: selectedUser }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            if (msg.StatusFl == true) {
                $("#Loader").hide();
                window.location.reload();
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
function fnValidateCO() {
    var selectedUser = $("#ddlUserListForCOApprover").val();
    var count = 0;
    if (selectedUser == undefined || selectedUser == null || selectedUser == '0') {
        count++;
        $('#lblUserForCO').addClass('requied');

    }
    else {
        $('#lblUserForCO').removeClass('requied');
    }

    if (count > 0) {
        return false;
    }
    else {
        return true;
    }
}