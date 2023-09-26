var ConnectedPersons = new Array();
$(document).ready(function () {
    $("#Loader").hide();
    fnGetCPUserList();
});
function fnGetCPUserList() {
    $("#Loader").show();
    var webUrl = uri + "/api/ConnectedPerson/GetCPUsers";
    $.ajax({
        type: "GET",
        url: webUrl,
        data: "{}",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            if (msg.StatusFl == true) {
                var result = "";
                result += '<option value="All">All</option>';
                for (var i = 0; i < msg.CPList.length; i++) {
                    result += '<option value="' + msg.CPList[i].CPEmail + '|' + msg.CPList[i].CPIdentificationNo + '">' + msg.CPList[i].CPName + ' (' + msg.CPList[i].CPFirm + '/' + msg.CPList[i].CPIdentificationNo + ')</option>';
                }
                $("#ddlCPUsersList").html(' ');
                $("#ddlCPUsersList").html(result);
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
function fnGrpConnectedPerson(GrpId, GrpNm, CanAddEdit) {
    $("#HiddenUpsiGrpIdCP").val(GrpId);
    $("#tbodyGrpCPMembersList").html("");
    if (CanAddEdit == "Yes") {
        $("#btnSaveMember").show();
    }
    else {
        $("#btnSaveMember").hide();
    }
    $("#Loader").show();
    var token = $("#TokenKey").val();
    var webUrl = uri + "/api/ConnectedPerson/GetUPSIGroupCP";
    $.ajax({
        url: webUrl,
        type: "POST",
        headers: {
            'TokenKeyH': token,
        },
        data: JSON.stringify({
            GrpId: GrpId
        }),
        async: false,
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            $("#Loader").hide();
            if (msg.Msg == "SessionExpired") {
                alert("Your session is expired. Please login again to continue");
                window.location.href = "../LogOut.aspx";
            }
            if (msg.StatusFl == true) {
                var result = "";
                var users = msg.CPList;
                for (var i = 0; i < users.length; i++) {
                    result += '<tr>'
                    result += '<td>' + users[i].CPFirm + '</td>';
                    result += '<td>' + users[i].CPName + '</td>';
                    result += '<td>' + users[i].CPEmail + '</td>';
                    result += '<td>' + users[i].CPIdentificationTyp + '</td>';
                    result += '<td>' + users[i].CPIdentificationNo + '</td>';
                    if (CanAddEdit == "Yes") {
                        result += '<td width="3%"><button id="btnremove" type="button" class="btn-link text-danger" value="' + users[i].CPEmail + '" onclick="fnRemoveCP(\'' + users[i].CPEmail + '\')">x</button></td>';
                    }
                    else {
                        result += '<td width="3%"></td>';
                    }
                    result += '</tr>'
                }
                $("#tbodyGrpCPMembersList").html(result);
            }
            else {
                alert(msg.Msg);
                $("#tbodyGrpCPMembersList").html('');
            }
        },
        error: function (response) {
            $("#Loader").hide();
            if (response.responseText == "Session Expired") {
                alert("Your session is expired. Please login again to continue");
                window.location.href = "../LogOut.aspx";
                return false;
            }
            else {
                alert(response.status + ' ' + response.statusText);
            }
        }
    });
}
function fnAddCP() {
    var str = '<tr>';
    str += '<td style="margin: 5px;">' +
        '<input id="txtFirmNm" class="form-control form-control-inline" placeholder="Enter Firm Name" type="text" autocomplete="off" onchange="removeCPRedClass(\'txtFirmNm\', \'lblFirmNm\')" />' +
        '</td>';
    str += '<td style="margin:5px;">' +
        '<input id="txtCPNm" class="form-control form-control-inline" placeholder="Enter Connected Person" type="text" autocomplete="off" onchange="removeCPRedClass(\'txtCPNm\', \'lblUPSIGrpNm\')" />' +
        '</td>';
    str += '<td style="margin:5px;">' +
        '<input id="txtCPEmail" class="form-control form-control-inline" placeholder="Enter Connected Person" type="text" autocomplete="off" onchange="removeCPRedClass(\'txtCPEmail\', \'lblCPEmail\')" />' +
        '</td>';
    str += '<td style="margin:5px;">' +
        '<select id="ddlCPIdentification" class="form-control" onchange="removeRedClass(\'ddlCPIdentification\',\'lblCPIdentification\')">' +
        '<option value=""></option>' +
        '<option value="AADHAR CARD">AADHAR CARD</option>' +
        '<option value="DRIVING LICENSE">DRIVING LICENSE</option>' +
        '<option value="PAN">PAN</option>' +
        '<option value="PASSPORT">PASSPORT</option>' +
        '<option value="OTHER">OTHER</option>' +
        '</select>' +
        '</td>';
    str += '<td style="margin:5px;">' +
        '<input id="txtCPIdentificationNo" class="form-control form-control-inline" placeholder="Enter Connected Person" type="text" autocomplete="off" onchange="removeCPRedClass(\'txtCPIdentificationNo\', \'lblCPIdentificationNo\')" />' +
        '</td>';
    str += '<td style="margin:5px;">' +
        '<img onclick="javascript:fnAddCP();" src="images/icons/AddButton.png" height="24" width="24" />' +
        '&nbsp;' +
        '<img onclick="javascript:fnDeleteCP(this);" src="images/icons/MinusButton.png" height="24" width="24" />' +
        '</td>';
    str += '</tr>';
    $("#tbdCPAdd").append(str);
}
function fnDeleteCP(cntrl) {
    //deleteDematDetailElement = $(event.currentTarget).closest('tr');
    $(cntrl).closest('tr').remove();
}
function fnSaveConnectedPerson() {
    saveConnectedPersons();
}
function saveConnectedPersons() {
    var ConnectedMembers = new Array();
    for (var i = 0; i < $("#tbdNewCPAdd").children().length; i++) {
        var CP = new Object();
        var sCPFirmNm = $($($($("#tbdNewCPAdd").children()[i]).children()[0]).children()[0]).val();
        var sCPNm = $($($($("#tbdNewCPAdd").children()[i]).children()[1]).children()[0]).val();
        var sCPEmail = $($($($("#tbdNewCPAdd").children()[i]).children()[2]).children()[0]).val();
        var sCPIdentification = $($($($("#tbdNewCPAdd").children()[i]).children()[3]).children()[0]).val();
        var sCPIdentificationNo = $($($($("#tbdNewCPAdd").children()[i]).children()[4]).children()[0]).val();
        var flg = true;
        //alert("sCPFirmNm=" + sCPFirmNm);
        //alert("sCPNm=" + sCPNm);
        //alert("sCPEmail=" + sCPEmail);
        //alert("sCPIdentification=" + sCPIdentification);
        //alert("sCPIdentificationNo=" + sCPIdentificationNo);
        if (sCPFirmNm == undefined || sCPFirmNm == "" || sCPFirmNm == null) {
            flg = false;
        }
        if (sCPNm == undefined || sCPNm == "" || sCPNm == null) {
            flg = false;
        }
        if (sCPEmail == undefined || sCPEmail == "" || sCPEmail == null) {
            flg = false;
        }
        else {
            if (!validateEmail(sCPEmail)) {
                alert("Please enter valid email");
                return false;
            }
        }
        if (sCPIdentification == undefined || sCPIdentification == "" || sCPIdentification == null) {
            flg = false;
        }
        if (sCPIdentificationNo == undefined || sCPIdentificationNo == "" || sCPIdentificationNo == null) {
            flg = false;
        }
        else {
            if (sCPIdentification == "PAN") {
                if (!ValidatePAN(sCPIdentificationNo)) {
                    alert("Please enter valid PAN number");
                    return false;
                }
            }
            else if (sCPIdentification == "AADHAR CARD") {
                var aadhar = sCPIdentificationNo;
                var adharcardTwelveDigit = /^\d{12}$/;
                if (aadhar != '') {
                    if (aadhar.match(adharcardTwelveDigit)) {
                        // return true;
                    }
                    else {
                        alert("Enter valid Aadhar Number");
                        return false;
                    }
                }
            }
        }
        if (flg == true) {
            CP.CPFirmNm = sCPFirmNm;
            CP.CPNm = sCPNm;
            CP.CPEmail = sCPEmail;
            CP.IdentificationTyp = sCPIdentification;
            CP.IdentificationId = sCPIdentificationNo;
            CP.CPStatus = "Active";
            CP.CPType = "New";
            ConnectedMembers.push(CP);
        }
    }

    var users = $('#ddlCPUsersList').val();
    if (users != null) {
        for (i = 0; i < users.length; i++) {
            var CM = new Object();
            if (users[i] != "All") {
                var sCPEmail = users[i];
                CM.CPEmail = sCPEmail.split("|")[0];
                CM.CPFirmNm = "";
                CM.CPNm = "";
                CM.IdentificationTyp = "";
                CM.IdentificationId = sCPEmail.split("|")[1];
                CM.CPStatus = "";
                CM.CPType = "Existing";
                ConnectedMembers.push(CM);
            }
        }
    }

    if (ConnectedMembers.length > 0) {
        if (confirm("Are you sure you want to add these insider persons as connected person to this group? This will trigger an intimation email to these connected person(s)")) {
            $("#Loader").show();
            var token = $("#TokenKey").val();
            var UPSIGrpId = $('#HiddenUpsiGrpIdCP').val();
            var webUrl = uri + "/api/UPSIGroup/SaveUPSIConnectedPersons";
            $.ajax({
                type: 'POST',
                url: webUrl,
                headers: {
                    'TokenKeyH': token,
                },
                data: JSON.stringify({
                    GrpId: UPSIGrpId,
                    ConnectedPersons: ConnectedMembers
                }),
                async: true,
                contentType: "application/json; charset=utf-8",
                datatype: "json",
                success: function (msg) {
                    $("#Loader").hide();
                    if (msg.StatusFl == true) {
                        alert("Members added successfully !");
                        $('#HiddenUpsiGrpId').val(0)
                        $("#dduserslist option:selected").prop("selected", false);
                        $("#dduserslist").trigger("change");
                        $("#ModalMembers").modal('hide');
                        window.location.reload();
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
            });
        }
    }
    else {
        alert("Please select user");
    }



    //var ConnectedPersons = new Array();
    //for (var i = 0; i < $("#tbdCPAdd").children().length; i++) {
    //    var CP = new Object();

    //    var sCPFirmNm = $($($($("#tbdCPAdd").children()[i]).children()[0]).children()[0]).val();
    //    var sCPNm = $($($($("#tbdCPAdd").children()[i]).children()[1]).children()[0]).val();
    //    var sCPEmail = $($($($("#tbdCPAdd").children()[i]).children()[2]).children()[0]).val();
    //    var sCPIdentification = $($($($("#tbdCPAdd").children()[i]).children()[3]).children()[0]).val();
    //    var sCPIdentificationNo = $($($($("#tbdCPAdd").children()[i]).children()[4]).children()[0]).val();
    //    var flg = true;

    //    //alert("sCPNm=" + sCPNm);
    //    //alert("sCPEmail=" + sCPEmail);
    //    //alert("sCPIdentification=" + sCPIdentification);
    //    //alert("sCPIdentificationNo=" + sCPIdentificationNo);
    //    if (sCPFirmNm == undefined || sCPFirmNm == "" || sCPFirmNm == null) {
    //        flg = false;
    //    }
    //    if (sCPNm == undefined || sCPNm == "" || sCPNm == null) {
    //        flg = false;
    //    }
    //    if (sCPEmail == undefined || sCPEmail == "" || sCPEmail == null) {
    //        flg = false;
    //    }
    //    else {
    //        if (!validateEmail(sCPEmail)) {
    //            alert("Please enter valid email");
    //            return false;
    //        }
    //    }
    //    if (sCPIdentification == undefined || sCPIdentification == "" || sCPIdentification == null) {
    //        flg = false;
    //    }
    //    if (sCPIdentificationNo == undefined || sCPIdentificationNo == "" || sCPIdentificationNo == null) {
    //        flg = false;
    //    }
    //    else {
    //        if (sCPIdentification == "PAN") {
    //            if (!ValidatePAN(sCPIdentificationNo)) {
    //                alert("Please enter valid PAN number");
    //                return false;
    //            }
    //        }
    //        else if (sCPIdentification == "AADHAR CARD") {
    //            var aadhar = sCPIdentificationNo;
    //            var adharcardTwelveDigit = /^\d{12}$/;

    //            if (aadhar != '') {
    //                if (aadhar.match(adharcardTwelveDigit)) {
    //                    // return true;
    //                }

    //                else {
    //                    alert("Enter valid Aadhar Number");
    //                    return false;
    //                }
    //            }

    //        }
    //    }
    //    if (flg == true) {
    //        CP.CPFirmNm = sCPFirmNm;
    //        CP.CPNm = sCPNm;
    //        CP.CPEmail = sCPEmail;
    //        CP.IdentificationTyp = sCPIdentification;
    //        CP.IdentificationId = sCPIdentificationNo;
    //        CP.CPStatus = "Active";
    //        ConnectedPersons.push(CP);
    //    }
    //}
    //if (ConnectedPersons.length > 0) {
    //    $("#Loader").show();
    //    var token = $("#TokenKey").val();
    //    var UPSIGrpId = $("#txtCPGrpId").val();
    //    var webUrl = uri + "/api/UPSIGroup/SaveUPSIConnectedPersons";
    //    $.ajax({
    //        url: webUrl,
    //        type: "POST",
    //        headers: {
    //            'TokenKeyH': token,
    //        },
    //        data: JSON.stringify({
    //            GrpId: UPSIGrpId, ConnectedPersons: ConnectedPersons
    //        }),
    //        async: false,
    //        contentType: "application/json; charset=utf-8",
    //        datatype: "json",
    //        success: function (msg) {
    //            $("#Loader").hide();
    //            if (msg.StatusFl == true) {
    //                alert("Connected Persons details updated successfully !");
    //                fnCloseCPModal();
    //            }
    //            else {
    //                if (msg.Msg == "SessionExpired") {
    //                    alert("Your session is expired. Please login again to continue");
    //                    window.location.href = "../LogOut.aspx";
    //                }
    //                else {
    //                    alert(msg.Msg);
    //                }
    //            }
    //        },
    //        error: function (response) {
    //            $("#Loader").hide();
    //            alert(response.status + ' ' + response.statusText);
    //        }
    //    })
    //}
    //else {
    //    alert("Please fill all required(*) field");
    //}
}
function fnAddNewCP() {
    var str = '<tr>';
    str += '<td style="margin: 5px;">' +
        '<input id="txtNewFirmNm" class="form-control form-control-inline" placeholder="Enter Firm Name" type="text" autocomplete="off" onchange="removeCPRedClass(\'txtFirmNm\', \'lblFirmNm\')" />' +
        '</td>';
    str += '<td style="margin:5px;">' +
        '<input id="txtNewCPNm" class="form-control form-control-inline" placeholder="Enter Connected Person" type="text" autocomplete="off" onchange="removeCPRedClass(\'txtCPNm\', \'lblUPSIGrpNm\')" />' +
        '</td>';
    str += '<td style="margin:5px;">' +
        '<input id="txtNewCPEmail" class="form-control form-control-inline" placeholder="Enter Connected Person" type="text" autocomplete="off" onchange="removeCPRedClass(\'txtCPEmail\', \'lblCPEmail\')" />' +
        '</td>';
    str += '<td style="margin:5px;">' +
        '<select id="ddlNewCPIdentification" class="form-control" onchange="removeRedClass(\'ddlCPIdentification\',\'lblCPIdentification\')">' +
        '<option value=""></option>' +
        '<option value="AADHAR CARD">AADHAR CARD</option>' +
        '<option value="DRIVING LICENSE">DRIVING LICENSE</option>' +
        '<option value="PAN">PAN</option>' +
        '<option value="PASSPORT">PASSPORT</option>' +
        '<option value="OTHER">OTHER</option>' +
        '</select>' +
        '</td>';
    str += '<td style="margin:5px;">' +
        '<input id="txtNewCPIdentificationNo" class="form-control form-control-inline" placeholder="Enter Connected Person" type="text" autocomplete="off" onchange="removeCPRedClass(\'txtCPIdentificationNo\', \'lblCPIdentificationNo\')" />' +
        '</td>';
    str += '<td style="margin:5px;">' +
        '<img onclick="javascript:fnAddNewCP();" src="images/icons/AddButton.png" height="24" width="24" />' +
        '&nbsp;' +
        '<img onclick="javascript:fnDeleteNewCP(this);" src="images/icons/MinusButton.png" height="24" width="24" />' +
        '</td>';
    str += '</tr>';
    $("#tbdNewCPAdd").append(str);
}
function fnDeleteNewCP(cntrl) {
    //deleteDematDetailElement = $(event.currentTarget).closest('tr');
    $(cntrl).closest('tr').remove();
}
function fnSaveNewConnectedPerson() {
    var ConnectedPersons = new Array();
    for (var i = 0; i < $("#tbdNewCPAdd").children().length; i++) {
        var CP = new Object();
        var sCPFirmNm = $($($($("#tbdNewCPAdd").children()[i]).children()[0]).children()[0]).val();
        var sCPNm = $($($($("#tbdNewCPAdd").children()[i]).children()[1]).children()[0]).val();
        var sCPEmail = $($($($("#tbdNewCPAdd").children()[i]).children()[2]).children()[0]).val();
        var sCPIdentification = $($($($("#tbdNewCPAdd").children()[i]).children()[3]).children()[0]).val();
        var sCPIdentificationNo = $($($($("#tbdNewCPAdd").children()[i]).children()[4]).children()[0]).val();
        var flg = true;

        //alert("sCPFirmNm=" + sCPFirmNm);
        //alert("sCPNm=" + sCPNm);
        //alert("sCPEmail=" + sCPEmail);
        //alert("sCPIdentification=" + sCPIdentification);
        //alert("sCPIdentificationNo=" + sCPIdentificationNo);
        if (sCPFirmNm == undefined || sCPFirmNm == "" || sCPFirmNm == null) {
            flg = false;
        }
        if (sCPNm == undefined || sCPNm == "" || sCPNm == null) {
            flg = false;
        }
        if (sCPEmail == undefined || sCPEmail == "" || sCPEmail == null) {
            flg = false;
        }
        else {
            if (!validateEmail(sCPEmail)) {
                alert("Please enter valid email");
                return false;
            }
        }
        if (sCPIdentification == undefined || sCPIdentification == "" || sCPIdentification == null) {
            flg = false;
        }
        if (sCPIdentificationNo == undefined || sCPIdentificationNo == "" || sCPIdentificationNo == null) {
            flg = false;
        }
        else {
            if (sCPIdentification == "PAN") {
                if (!ValidatePAN(sCPIdentificationNo)) {
                    alert("Please enter valid PAN number");
                    return false;
                }
            }
            else if (sCPIdentification == "AADHAR CARD") {
                var aadhar = sCPIdentificationNo;
                var adharcardTwelveDigit = /^\d{12}$/;

                if (aadhar != '') {
                    if (aadhar.match(adharcardTwelveDigit)) {
                        // return true;
                    }

                    else {
                        alert("Enter valid Aadhar Number");
                        return false;
                    }
                }

            }
        }
        if (flg == true) {
            CP.CPFirm = sCPFirmNm;
            CP.CPName = sCPNm;
            CP.CPEmail = sCPEmail;
            CP.CPIdentificationTyp = sCPIdentification;
            CP.CPIdentificationNo = sCPIdentificationNo;
            CP.CPStatus = "Active";
            ConnectedPersons.push(CP);
        }
    }
    //alert(ConnectedPersons.length);
    //return false;
    if (ConnectedPersons.length > 0) {
        $("#Loader").show();
        var token = $("#TokenKey").val();
        var UPSIGrpId = $("#txtCPGrpId").val();
        var webUrl = uri + "/api/ConnectedPerson/SaveConnectedPersons";
        $.ajax({
            url: webUrl,
            type: "POST",
            headers: {
                'TokenKeyH': token,
            },
            data: JSON.stringify(ConnectedPersons),
            async: false,
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            success: function (msg) {
                $("#Loader").hide();
                if (msg.StatusFl == true) {
                    alert("Connected Person(s) has been added successfully. Please use Connected Person dropdown to select and add the user !");
                    $("#AddNewConnectedPerson").modal('hide');
                    fnGetCPUserList()
                    //window.location.reload();
                }
                else {
                    if (msg.Msg == "SessionExpired") {
                        alert("Your session is expired. Please login again to continue");
                        window.location.href = "../LogOut.aspx";
                    }
                    else {
                        if (msg.Msg == "Conflict exists") {
                            for (var x = 0; x < msg.CPList.length; x++) {
                                for (var i = 0; i < $("#tbdNewCPAdd").children().length; i++) {
                                    var cntrlEmail = $($($($("#tbdNewCPAdd").children()[i]).children()[2]).children()[0]);
                                    var cntrlIdentificationNo = $($($($("#tbdNewCPAdd").children()[i]).children()[4]).children()[0]);

                                    var sCPEmail = $($($($("#tbdNewCPAdd").children()[i]).children()[2]).children()[0]).val();
                                    var sCPIdentificationNo = $($($($("#tbdNewCPAdd").children()[i]).children()[4]).children()[0]).val();

                                    if (sCPEmail == msg.CPList[x].CPEmail && (msg.CPList[x].CPConflict == "Email" || msg.CPList[x].CPConflict == "Email & PAN")) {
                                        $(cntrlEmail).addClass('required-red');
                                    }
                                    if (sCPIdentificationNo == msg.CPList[x].CPIdentificationNo && (msg.CPList[x].CPConflict == "PAN" || msg.CPList[x].CPConflict == "Email & PAN")) {
                                        $(cntrlIdentificationNo).addClass('required-red');
                                    }
                                }
                            }
                            $("#dvMsg").html("** There is/are conflict with the same Email and Identification number with respect to existing DP & CP, please correct them and proceed");
                            $("#dvMsg").show();
                        }
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
        alert("Please fill all required(*) field");
    }
}
function fnOK() {
    $("#Loader").show();
    var token = $("#TokenKey").val();
    var UPSIGrpId = $("#txtCPGrpId").val();
    var webUrl = uri + "/api/ConnectedPerson/SaveNewConnectedPersons";
    $.ajax({
        url: webUrl,
        type: "POST",
        headers: {
            'TokenKeyH': token,
        },
        data: JSON.stringify(ConnectedPersons),
        async: false,
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            $("#Loader").hide();
            if (msg.StatusFl == true) {
                $("#alertMeassage").modal("hide");
                alert("Successfully added!");
                $("#AddNewConnectedPerson").modal("hide");
                fnGetCPUserList();
                //window.location.reload();
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
function CancelAddNewCP() {
    $("#alertMeassage").modal("hide");
}
function fnCloseNewCPModal() {
    $("#txtCPGrpId").val('0');
    var str = '<tr>';
    str += '<td style="margin: 5px;">' +
        '<input id="txtNewFirmNm" class="form-control form-control-inline" placeholder="Enter Firm Name" type="text" autocomplete="off" onchange="removeCPRedClass(\'txtFirmNm\', \'lblFirmNm\')" />' +
        '</td>';
    str += '<td style="margin:5px;">' +
        '<input id="txtNewCPNm" class="form-control form-control-inline" placeholder="Enter Connected Person" type="text" autocomplete="off" onchange="removeCPRedClass(\'txtCPNm\', \'lblUPSIGrpNm\')" />' +
        '</td>';
    str += '<td style="margin:5px;">' +
        '<input id="txtNewCPEmail" class="form-control form-control-inline" placeholder="Enter Connected Person" type="text" autocomplete="off" onchange="removeCPRedClass(\'txtCPEmail\', \'lblCPEmail\')" />' +
        '</td>';
    str += '<td style="margin:5px;">' +
        '<select id="ddlNewCPIdentification" class="form-control" onchange="removeRedClass(\'ddlCPIdentification\',\'lblCPIdentification\')">' +
        '<option value=""></option>' +
        '<option value="AADHAR CARD">AADHAR CARD</option>' +
        '<option value="DRIVING LICENSE">DRIVING LICENSE</option>' +
        '<option value="PAN">PAN</option>' +
        '<option value="PASSPORT">PASSPORT</option>' +
        '<option value="OTHER">OTHER</option>' +
        '</select>' +
        '</td>';
    str += '<td style="margin:5px;">' +
        '<input id="txtNewCPIdentificationNo" class="form-control form-control-inline" placeholder="Enter Connected Person" type="text" autocomplete="off" onchange="removeCPRedClass(\'txtCPIdentificationNo\', \'lblCPIdentificationNo\')" />' +
        '</td>';
    str += '<td style="margin:5px;">' +
        '<img onclick="javascript:fnAddNewCP();" src="images/icons/AddButton.png" height="24" width="24" />' +
        //'&nbsp;' +
        //'<img onclick="javascript:fnDeleteCP(this);" src="images/icons/MinusButton.png" height="24" width="24" />' +
        '</td>';
    str += '</tr>';
    $("#tbdNewCPAdd").html(str);
    $("#AddNewConnectedPerson").modal('hide');
}
function fnRemoveCP(CPEmail) {
    if (confirm('Are you sure you want to remove this user?')) {        
        $("#Loader").show();
        var token = $("#TokenKey").val();

        var test = new FormData();
        test.append("UPSIGrpId", $("#HiddenUpsiGrpIdCP").val());
        test.append('CPEmail', CPEmail);

        var webUrl = uri + "/api/UPSIGroup/DeleteUPSICp";
        $.ajax({
            type: 'POST',
            url: webUrl,
            headers: {
                'TokenKeyH': token,
            },
            contentType: false,
            processData: false,
            data: test,
            async: true,
            success: function (msg) {
                $("#Loader").hide();
                if (msg.StatusFl == true) {
                    alert("Connected Person removed successfully !");
                    $("#GrpConnectecdPerson").modal('hide');
                    //var table = $('#ttbl-upsilist-setup').DataTable();
                    //table.destroy();
                    //GetUPSIGroupList();
                    window.location.reload();
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
        });
    }
    else {
    }
}