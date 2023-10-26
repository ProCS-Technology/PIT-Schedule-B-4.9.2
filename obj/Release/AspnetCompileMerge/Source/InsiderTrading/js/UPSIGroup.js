var UserData = "";
var UPSIType = "";
var MemberName = "";
var arrUpsiGrp = new Array();
$(document).ready(function () {
    $("#Loader").hide();
    GetUPSIGroupList();
    fnGetAllUPSIType();
    fnGetUserList();
    fnGetModeofCommunication();
    $('.datepicker').datepicker({
        todayHighlight: true,
        autoclose: true,
        clearBtn: true,
        //startDate: "today"
    });
});
function GetUPSIGroupList() {
    $("#Loader").show();
    var webUrl = uri + "/api/UPSIGroup/GetUPSIGroupList";
    $.ajax({
        url: webUrl,
        //type: "POST",
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
                arrUpsiGrp = new Array();
                arrUpsiGrp = msg.UPSIGroups;
                for (var i = 0; i < msg.UPSIGroups.length; i++) {
                    result += '<tr>'
                    result += '<td>' + msg.UPSIGroups[i].GrpNm + '</td>';
                    result += '<td>' + msg.UPSIGroups[i].TypNm + '</td>';
                    result += '<td>' + msg.UPSIGroups[i].ValidFrom + '</td>';
                    result += '<td>' + msg.UPSIGroups[i].ValidTo + '</td>';
                    result += '<td id="tdEditDelete_' + msg.UPSIGroups[i].GrpId + '">'
                    result += '<a data-target="#UPSIGrp" data-toggle="modal" id="a_' + msg.UPSIGroups[i].GrpId + '" class="btn btn-outline dark" onclick=\"javascript:fnEditUPSIGroup(\'' + msg.UPSIGroups[i].GrpId + '\',\'' + msg.UPSIGroups[i].GrpNm + '\',\'' + msg.UPSIGroups[i].GrpStatus + '\',\'' + msg.UPSIGroups[i].TypId + '\',\'' + msg.UPSIGroups[i].ValidFrom + '\',\'' + msg.UPSIGroups[i].ValidTo + '\',\'' + msg.UPSIGroups[i].GrpDesc + '\',\'' + msg.UPSIGroups[i].GrpStatus + '\',\'' + msg.UPSIGroups[i].Remarks + '\'); \">Edit</a>';
                    result += '<button type="button" style="margin-left:20px" data-target="#ModalMembers" data-toggle="modal" id="al_' + msg.UPSIGroups[i].GrpId + '" class="btn btn-outline dark" value="' + msg.UPSIGroups[i].GrpId + '" onclick="fnGrpMemberAccess(this.value)">Add D/P</button>';
                    result += '<a style="margin-left:20px" data-target="#GrpConnectecdPerson" data-toggle="modal" id="cp_' + msg.UPSIGroups[i].GrpId + '" class="btn btn-outline dark" onclick=\"javascript:fnGrpConnectedPerson(\'' + msg.UPSIGroups[i].GrpId + '\',\'' + msg.UPSIGroups[i].GrpNm + '\');\">Add C/P</a>';
                    result += '<a style="margin-left:20px" data-target="#GrpCommunication" data-toggle="modal" id="al_' + msg.UPSIGroups[i].GrpId + '" class="btn btn-outline dark" onclick=\"javascript:fnGrpCommunication(\'' + msg.UPSIGroups[i].GrpId + '\',\'' + msg.UPSIGroups[i].GrpNm + '\');\">UPSI</a>';
                    result += '<a style="margin-left:20px" data-target="#GrpExcel" data-toggle="modal" id="al_' + msg.UPSIGroups[i].GrpId + '" class="btn btn-outline dark" onclick=\"javascript:fnGrpExcel(\'' + msg.UPSIGroups[i].GrpId + '\',\'' + msg.UPSIGroups[i].GrpNm + '\');\">Excel</a>';
                    //result += '<a style="margin-left:20px" data-target="#GrpAuditLog" data-toggle="modal" id="al_' + msg.UPSIGroups[i].GrpId + '" class="btn btn-outline dark" onclick=\"javascript:fnGrpAuditLog(' + msg.UPSIGroups[i].GrpId + ');\">Audit Log</a>';
                    result += '</td>';
                    result += '</tr>'
                }
                var table = $('#ttbl-upsilist-setup').DataTable();
                table.destroy();
                $("#tbdupsilist").html(result);
                initializeDataTable();
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
function fnGetAllUPSIType() {
    $("#Loader").show();
    var webUrl = uri + "/api/UPSIGroup/GetUPSIType";
    $.ajax({
        url: webUrl,
        //type: "POST",
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
                var result = "<option id='0'></option>";
                for (var i = 0; i < msg.UPSIGroups.length; i++) {
                    result += '<option value="' + msg.UPSIGroups[i].TypId + '">' + msg.UPSIGroups[i].TypNm + '</option>';
                }
                $("#ddlUPSIType").html(result);
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
function fnGetKeywords() {
    //alert("In function fnGetKeywords");
    //alert("In function fnGetKeywords");
    var upsiTypId = $("#ddlUPSIType").val();
    var GrpId = $("#txtUPSIGrpId").val();
    //alert(upsiTypId);
    if (upsiTypId != null && GrpId==0) {
        $("#Loader").show();
        var webUrl = uri + "/api/UPSIConfig/GetUPSITypeKeywords";
        $.ajax({
            url: webUrl,
            type: "POST",
            data: JSON.stringify({ TypeId: upsiTypId}),
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
                    if (msg.UPSITyp != null) {
                        if (msg.UPSITyp.Keywords != null) {
                            var str = '';
                            for (var x = 0; x < msg.UPSITyp.Keywords.length; x++) {
                                str += '<tr>';
                                str += '<td style="margin:5px;">' +
                                    '<input disabled id="txtKeyword" class="form-control" placeholder="Enter Keyword" type="text" autocomplete="off" value="' + msg.UPSITyp.Keywords[x].keyword + '" />' +
                                    '</td>';
                                str += '<td style="margin:5px;">' +
                                    '<input disabled id="txtOrder" class="form-control numerictextbox" placeholder="Enter Match Order" type="text" autocomplete="off" value="' + msg.UPSITyp.Keywords[x].sequence + '" />' +
                                    '</td>';
                                str += '<td style="margin:5px;">' +
                                    '<img onclick="javascript:fnAddRow();" src="images/icons/AddButton.png" height="24" width="24" />' +
                                    '&nbsp;' +
                                    //'<img onclick="javascript:fnDeleteRow(this);" src="images/icons/MinusButton.png" height="24" width="24" />' +
                                    '</td>';
                                str += '</tr>';
                                //$("#tbodyKeyword").append(str);
                            }
                            $("#tbodyKeyword").html(str);
                        }
                    }
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
}
function fnAddGrp() {
    fnResetForm();
}
function validation() {
    var UPSIGrpId = $("#txtUPSIGrpId").val();
    var UPSITypeId = $("#ddlUPSIType").val();
    var UPSIGroup = $("#txtUPSIGrpNm").val();
    var UPSIDesc = $("#txtUPSIGrpDesc").val();
    var UPSIFromDt = $("#txtFromDt").val();
    var UPSITillDt = $("#txtTillDt").val();
    var UPSIStatus = $("#ddlUPSIGrpStatus").val();
    var UPSIRemarks = $("#txtUPSIGrpRemarks").val();
    var Count = 0;

    /*alert("UPSIGrpId=" + UPSIGrpId);
    alert("UPSITypeId=" + UPSITypeId);
    alert("UPSIGroup=" + UPSIGroup);
    alert("UPSIDesc=" + UPSIDesc);
    alert("UPSIFromDt=" + UPSIFromDt);
    alert("UPSITillDt=" + UPSITillDt);
    alert("UPSIStatus=" + UPSIStatus);
    alert("UPSIRemarks=" + UPSIRemarks);*/
    if (UPSITypeId == undefined || UPSITypeId == "" || UPSITypeId == null || UPSITypeId == "0") {
        $('#lblUPSIType').addClass('required');
        $('#ddlUPSIType').addClass('required-red-border');
        Count++
    }
    else {
        $('#lblUPSIType').removeClass('required');
        $('#ddlUPSIType').removeClass('required-red-border');
    }

    if (UPSIGroup == "" || UPSIGroup == null) {
        $('#lblUPSIGrpNm').addClass('required');
        $('#txtUPSIGrpNm').addClass('required-red-border');
        Count++
    }
    else {
        $('#lblUPSIGrpNm').removeClass('required');
        $('#txtUPSIGrpNm').removeClass('required-red-border');
    }

    if (UPSIFromDt == "" || UPSIFromDt == null || UPSIFromDt == "0") {
        $('#lblValidDt').addClass('required');
        $('#txtFromDt').addClass('required-red-border');
        Count++
    }
    else {
        $('#lblValidDt').removeClass('required');
        $('#txtFromDt').removeClass('required-red-border');
    }

    if (UPSITillDt != null) {
        if (new Date(convertToDateTime(UPSITillDt)) < new Date(convertToDateTime(UPSIFromDt))) {
            Count++;
            alert("To Date Should be greater than From Date");
            $('#lblValidDt').addClass('required');
            $('#txtTillDt').addClass('required-red-border');
            return false;
        }
    }
    else {
        $('#lblValidDt').removeClass('required');
        $('#txtTillDt').removeClass('required-red-border');
    }

    if (UPSIDesc.trim() == "" || UPSIDesc.trim() == null) {
        //alert("In UPSIDesc null or blank");
        $('#lblUPSIGrpDesc').addClass('required');
        $('#txtUPSIGrpDesc').addClass('required-red-border');
        Count++
    }
    else {
        $('#lblUPSIGrpDesc').removeClass('required');
        $('#txtUPSIGrpDesc').removeClass('required-red-border');
    }

    if (UPSIStatus == undefined || UPSIStatus == "" || UPSIStatus == null || UPSIStatus == "0") {
        $('#lblUPSIGrpStatus').addClass('required');
        $('#ddlUPSIStatus').addClass('required-red-border');
        Count++
    }
    else {
        $('#ddlUPSIStatus').removeClass('required-red-border');
        $('#lblUPSIGrpStatus').removeClass('required');
        if (UPSIStatus == "Abandoned" || UPSIStatus == "Published") {
            if (UPSITillDt == "" || UPSITillDt == null) {
                Count++;
                $('#lblValidDt').addClass('required');
                $('#txtTillDt').addClass('required-red-border');
            }
            else {
                $('#lblValidDt').removeClass('required');
                $('#txtTillDt').removeClass('required-red-border');
            }
            if (UPSIRemarks == "" || UPSIRemarks == null) {
                Count++;
                $('#lblUPSIGrpRemarks').addClass('required');
                $('#txtUPSIGrpRemarks').addClass('required-red-border');
            }
            else {
                $('#lblUPSIGrpRemarks').removeClass('required');
                $('#txtUPSIGrpRemarks').removeClass('required-red-border');
            }
        }
    }
    //alert("Count=" + Count);
    //return false;
    if (Count == 0) {
        return true;
    }
    else {
        alert("Please fill required field");
        return false;
    }
}
function fnSaveUPSIGrp() {
    if (validation()) {
        //alert("Passed");
        var webUrl = uri + "/api/UPSIGroup/SaveUPSIGroup";
        var UPSIGrpId = $("#txtUPSIGrpId").val();
        var UPSITypeId = $("#ddlUPSIType").val();
        var UPSIGroup = $("#txtUPSIGrpNm").val();
        var UPSIDesc = $("#txtUPSIGrpDesc").val();
        var UPSIFromDt = $("#txtFromDt").val();
        var UPSITillDt = $("#txtTillDt").val();
        var UPSIStatus = $("#ddlUPSIGrpStatus").val();
        var UPSIRemarks = $("#txtUPSIGrpRemarks").val();
        var token = $("#TokenKey").val();

        var lstKeywords = new Array();
        for (var i = 0; i < $("#tbodyKeyword").children().length; i++) {
            var obj = new Object();
            var keyword = $($($($("#tbodyKeyword").children()[i]).children()[0]).children()[0]).val();
            var matchOrder = $($($($("#tbodyKeyword").children()[i]).children()[1]).children()[0]).val();
            if (keyword != "" && keyword != null) {
                if (matchOrder == "") {
                    alert("Please Enter Match Order");
                    return false;
                }
                else {
                    obj.keyword = keyword;
                    obj.sequence = matchOrder;
                    lstKeywords.push(obj);
                }
            }
        }
        var flag = true;
        if (lstKeywords.length == 0) {
            if (!confirm("Are you sure you want to add UPSI Group without adding keywords")) {
                flag = false;
            }
        }
        if (flag) {
            $.ajax({
                url: webUrl,
                type: "POST",
                headers: {
                    'TokenKeyH': token,
                },
                data: JSON.stringify({
                    GrpId: UPSIGrpId, GrpNm: UPSIGroup, GrpDesc: UPSIDesc, TypId: UPSITypeId, Remarks: UPSIRemarks,
                    ValidFrom: UPSIFromDt, ValidTo: UPSITillDt, GrpStatus: UPSIStatus, GrpDesc: UPSIDesc, Keywords: lstKeywords
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
                        alert(msg.Msg);
                        window.location.reload();
                    }
                    else {
                        alert(msg.Msg);
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
    }
}
function validateEmail(value) {
    var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
    if (reg.test(value) == false) {
        return false;
    }
    return true;
}
function validatePanNO(value) {
    var reg = /([A-Z]){5}([0-9]){4}([A-Z]){1}$/;;
    if (reg.test(value) == false) {
        return false;
    }
    return true;
}
function removeRedClass(elememnt, lbl) {
    if (elememnt == 'ddlUPSIType') {
        var UPSITyp = $("#" + elememnt).val();
        if (UPSITyp != "0") {
            $('#' + lbl).removeClass('required');
            $('#' + elememnt).removeClass('required-red');
            $('#' + elememnt).removeClass('required-red-border');

            var txt = $("#" + elememnt + " option:selected").text();
            //$("#txtUPSIGrpNm").val(txt);
            //$("#txtUPSIGrpNm").attr("disabled", "disabled");
        }
        else {
            //$("#txtUPSIGrpNm").val(txt);
            //$("#txtUPSIGrpNm").removeAttr("disabled");

            $('#ddlUPSIType').addClass('required-red');
            $('#lblUPSIType').addClass('required');
        }
    }
    else {
        $('#' + lbl).removeClass('requied');
        $('#' + elememnt).removeClass('required-red');
        $('#' + elememnt).removeClass('required-red-border');
    }
}
function removeOtherMemberClass(lement) {
    var idofel = lement.id;
    $("input[id*='" + idofel + "']").removeClass('required-red');
    $("input[id*='" + idofel + "']").removeClass('required');
    $("select[id*='" + idofel + "']").removeClass('required-red');
    $("select[id*='" + idofel + "']").removeClass('required');
    $('#' + idofel).removeClass('required');
    $('#' + idofel).removeClass('required-red');
    $('#' + idofel).removeClass('required-red-border');
}
function fnResetForm() {
    $("#txtUPSIGrpId").val("0");
    $("#ddlUPSIType").val('0')
    $('#ddlUPSIType').trigger('change');
    $("input[id*='txtUPSIGrpNm']").val('');
    $("input[id*='txtFromDt']").val('');
    $("input[id*='txtTillDt']").val('');
    $("#txtUPSIGrpDesc").val('');
    $("#ddlUPSIGrpStatus").val('').trigger('change');
    $("#txtUPSIGrpRemarks").val('');

    $("#ddlUPSIType").removeAttr("disabled");
    //$("#txtUPSIGrpNm").removeAttr("disabled");

    $('#lblUPSIType').removeClass('required');
    $('#ddlUPSIType').removeClass('required-red-border');
    $('#txtUPSIGrpNm').removeClass('required-red-border');
    $('#lblUPSIGrpNm').removeClass('required');
    $('#lblUPSIGrpDesc').removeClass('required');
    $('#txtUPSIGrpDesc').removeClass('required-red-border');
    $('#lblValidDt').removeClass('required');
    $('#txtFromDt').removeClass('required-red-border');
    $('#txtTillDt').removeClass('required-red-border');


    $('#lblUPSIGrpStatus').removeClass('required');
    $('#ddlUPSIGrpStatus').removeClass('required-red-border');

    $('#lblUPSIGrpRemarks').removeClass('required');
    $('#txtUPSIGrpRemarks').removeClass('required-red-border');

    
    $("#dduserslist option:selected").prop("selected", false);
    $("#dduserslist").trigger("change");

    var str = '<tr>';
    str += '<td style="margin:5px;">' +
        '<input id="txtKeyword" class="form-control" placeholder="Enter Keyword" type="text" autocomplete="off" />' +
        '</td>';
    str += '<td style="margin:5px;">' +
        '<input id="txtOrder" class="form-control numerictextbox" placeholder="Enter Match Order" type="text" autocomplete="off" />' +
        '</td>';
    str += '<td style="margin:5px;">' +
        '<img onclick="javascript:fnAddRow();" src="images/icons/AddButton.png" height="24" width="24" />' +
        '&nbsp;' +
        '<img onclick="javascript:fnDeleteRow(this);" src="images/icons/MinusButton.png" height="24" width="24" />' +
        '</td>';
    str += '</tr>';
    $("#tbodyKeyword").html(str);
    $(".numerictextbox").keypress(function (e) {
        //if the letter is not digit then display error and don't type anything
        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
            return false;
        }
    });
}
function fnCloseModal() {
    fnResetForm();
    $("#UPSIGrp").modal('hide');
}
function initializeDataTable() {
    $('#tbl-upsilist-setup').DataTable({
        "dom": '<"top"iflp<"clear">>rt<"bottom"iflp<"clear">>',
        dom: 'Bfrtip',
        pageLength: 5,
        buttons: [
        ],
    });
}
function fnEditUPSIGroup(GrpId, GrpNm, GrpStatus, TypId, ValidFrom, ValidTo, GrpDesc, GrpStatus, GrpRemarks) {
    //alert("keywords.length=" + keywords.length);
    //alert("ValidTo=" + ValidTo);
    $("#txtUPSIGrpId").val(GrpId);
    $("#ddlUPSIType").val(TypId);
    $('#ddlUPSIType').trigger('change');
    $("#ddlUPSIType").attr("disabled", "disabled");
    $('#txtUPSIGrpNm').val(GrpNm);
    //$("#txtUPSIGrpNm").attr("disabled", "disabled");
    $('#txtFromDt').val(FormatToDateTime(ValidFrom));
    if (ValidTo != "" && ValidTo != null) {
        $('#txtTillDt').val(FormatToDateTime(ValidTo));
    }
    $('#txtUPSIGrpDesc').val(GrpDesc.trim());
    $('#txtUPSIGrpRemarks').val(GrpRemarks.trim());
    //alert("Just before setting Status");
    $('#ddlUPSIGrpStatus').val(GrpStatus).trigger('change');
    //alert("Just after setting Status");
    var keyword = new Array();
    for (var x = 0; x < arrUpsiGrp.length; x++) {
        //alert("arrUpsiType[" + x + "].TypeId=" + arrUpsiType[x].TypeId);
        if (GrpId == arrUpsiGrp[x].GrpId) {
            for (var i = 0; i < arrUpsiGrp[x].Keywords.length; i++) {
                var obj = new Object();
                obj.keyword = arrUpsiGrp[x].Keywords[i].keyword;
                obj.sequence = arrUpsiGrp[x].Keywords[i].sequence;
                keyword.push(obj);
            }
            break;
        }
    }
    if (keyword.length >= 0) {
        var str = '';
        for (var x = 0; x < keyword.length; x++) {
            str += '<tr>';
            str += '<td style="margin:5px;">' +
                '<input disabled id="txtKeyword" class="form-control" placeholder="Enter Keyword" type="text" autocomplete="off" value="' + keyword[x].keyword + '" />' +
                '</td>';
            str += '<td style="margin:5px;">' +
                '<input disabled id="txtOrder" class="form-control numerictextbox" placeholder="Enter Match Order" type="text" autocomplete="off" value="' + keyword[x].sequence + '" />' +
                '</td>';
            str += '<td style="margin:5px;">' +
                '<img onclick="javascript:fnAddRow();" src="images/icons/AddButton.png" height="24" width="24" />' +
                '&nbsp;' +
                '<img onclick="javascript:fnDeleteRow(this);" src="images/icons/MinusButton.png" height="24" width="24" />' +
                '</td>';
            str += '</tr>';
            //$("#tbodyKeyword").append(str);
        }
        $("#tbodyKeyword").html(str);
    }
}
function fnCloseCPModal() {
    $("#txtCPGrpId").val('0');
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
        '<option value="OTHER">OTHER</option>'+
        '</select>' +
        '</td>';
    str += '<td style="margin:5px;">' +
        '<input id="txtCPIdentificationNo" class="form-control form-control-inline" placeholder="Enter Connected Person" type="text" autocomplete="off" onchange="removeCPRedClass(\'txtCPIdentificationNo\', \'lblCPIdentificationNo\')" />' +
        '</td>';
    str += '<td style="margin:5px;">' +
        '<img onclick="javascript:fnAddCP();" src="images/icons/AddButton.png" height="24" width="24" />' +
        //'&nbsp;' +
        //'<img onclick="javascript:fnDeleteCP(this);" src="images/icons/MinusButton.png" height="24" width="24" />' +
        '</td>';
    str += '</tr>';
    $("#tbdCPAdd").html(str);
    $("#GrpConnectecdPerson").modal('hide');
}
function ValidatePAN(valPan) {
    var regpan = /^([a-zA-Z]){5}([0-9]){4}([a-zA-Z]){1}?$/;
    var fl = true;
    if (valPan == "" || valPan == null || valPan == undefined) {
        fl = false;
    }
    else if (!regpan.test(valPan)) {
        fl = false;
    }
    return fl;
}
function fnGrpMemberAccess(groupid) {
    $("#HiddenUpsiGrpId").val(groupid);
    $("#tbodyGrpMembersList").html("");
    $("#Loader").show();
    var token = $("#TokenKey").val();
    var webUrl = uri + "/api/UPSIGroup/GetUPSIGroupMember";
    $.ajax({
        url: webUrl,
        type: "POST",
        headers: {
            'TokenKeyH': token,
        },
        data: JSON.stringify({
            GrpId: groupid
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
                var users = msg.UPSIGroups[0].ConnectedPersons;
                for (var i = 0; i < users.length; i++) {
                    result += '<tr>'
                    result += '<td>' + users[i].CPNm + '</td>';
                    result += '<td>' + users[i].CPType + '</td>';
                    result += '<td width="3%"><button id="btnremove" type="button" class="btn-link text-danger" value="' + users[i].MapId + '" onclick="fnremovegrpmembers(this.value)">x</button></td>';
                    result += '</tr>'
                }

                $("#tbodyGrpMembersList").html(result);
            }
            else {
                alert(msg.Msg);
                $("#tbodyGrpMembersList").html('');
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
function fnCloseLogModal() {
    $('#GrpLogs').html("");
}
function fnGrpAuditLog(GrpId) {
    //$('#content').load("https://www.google.com/");
    $("#GrpLogs").load("UpsiGrpAuditLog.aspx?Gid=" + GrpId);

    //var token = $("#TokenKey").val();
    //var webUrl = uri + "/api/UPSIGroup/GetUPSIGrpAuditLog";    
    //$.ajax({
    //    url: webUrl,
    //    type: "POST",
    //    headers: {
    //        'TokenKeyH': token,
    //    },
    //    data: JSON.stringify({
    //        GrpId: GrpId
    //    }),
    //    async: false,
    //    contentType: "application/json; charset=utf-8",
    //    datatype: "json",
    //    success: function (msg) {
    //        $("#Loader").hide();
    //        if (msg.Msg == "SessionExpired") {
    //            alert("Your session is expired. Please login again to continue");
    //            window.location.href = "../LogOut.aspx";
    //        }
    //        if (msg.StatusFl == true) {
    //            var result = '';
    //            if (msg.UPSIGroups == null) {
    //                $("#tbodyCPDetail").html(result);
    //            }
    //            else {
    //                for (var i = 0; i < msg.UPSIGroups.length; i++) {
    //                    for (var j = 0; j < msg.UPSIGroups[i].ConnectedPersons.length; j++) {
    //                        //result += '<tr>'
    //                        //result += '<td>' + msg.UPSIGroups[i].ConnectedPersons[j].CPNm + '</td>';
    //                        //result += '<td>' + msg.UPSIGroups[i].ConnectedPersons[j].CPEmail + '</td>';
    //                        //result += '<td>' + msg.UPSIGroups[i].ConnectedPersons[j].IdentificationTyp + '</td>';
    //                        //result += '<td>' + msg.UPSIGroups[i].ConnectedPersons[j].IdentificationId + '</td>';
    //                        ////result += '<td>' + msg.UPSIGroups[i].ConnectedPersons[j].CPStatus + '</td>';
    //                        //result += '</tr>'

    //                        result += '<tr>'
    //                        result += '<td>' + msg.UPSIGroups[i].GrpNm + '</td>';
    //                        result += '<td>' + msg.UPSIGroups[i].TypNm + '</td>';
    //                        result += '<td>' + msg.UPSIGroups[i].ValidFrom + '</td>';
    //                        result += '<td>' + msg.UPSIGroups[i].ValidTo + '</td>';
    //                        result += '<td id="tdEditDelete_' + msg.UPSIGroups[i].GrpId + '">'
    //                        result += '<a data-target="#UPSIGrp" data-toggle="modal" id="a_' + msg.UPSIGroups[i].GrpId + '" class="btn btn-outline dark" onclick=\"javascript:fnEditUPSIGroup(\'' + msg.UPSIGroups[i].GrpId + '\',\'' + msg.UPSIGroups[i].GrpNm + '\',\'' + msg.UPSIGroups[i].GrpStatus + '\',\'' + msg.UPSIGroups[i].TypId + '\',\'' + msg.UPSIGroups[i].ValidFrom + '\',\'' + msg.UPSIGroups[i].ValidTo + '\',\'' + msg.UPSIGroups[i].GrpDesc + '\');\">Edit</a>';
    //                        result += '<a style="margin-left:20px" data-target="#GrpConnectecdPerson" data-toggle="modal" id="cp_' + msg.UPSIGroups[i].GrpId + '" class="btn btn-outline dark" onclick=\"javascript:fnGrpConnectedPerson(\'' + msg.UPSIGroups[i].GrpId + '\',\'' + msg.UPSIGroups[i].GrpNm + '\');\">Connected Person</a>';
    //                        result += '<a style="margin-left:20px" data-target="#GrpAuditLog" data-toggle="modal" id="al_' + msg.UPSIGroups[i].GrpId + '" class="btn btn-outline dark" onclick=\"javascript:fnGrpAuditLog(' + msg.UPSIGroups[i].GrpId + ');\">Audit Log</a>';
    //                        //result = '<asp:UpdatePanel id="Updater" runat="server"><asp:PlaceHolder id="AddControlsToThis" runat="server" /><asp:LinkButton ID="LinkButtonGrpAuditLog" runat="server" style="margin-left:20px" CssClass="btn btn-outline dark" OnClick="LinkButtonGrpAuditLog_Click" CommandArgument="' + msg.UPSIGroups[i].GrpId + '">Audit Log</asp:LinkButton></asp:UpdatePanel>';
    //                        result += '<a style="margin-left:20px" data-target="#GrpCommunication" data-toggle="modal" id="al_' + msg.UPSIGroups[i].GrpId + '" class="btn btn-outline dark" onclick=\"javascript:fnGrpCommunication(\'' + msg.UPSIGroups[i].GrpId + '\',\'' + msg.UPSIGroups[i].GrpNm + '\');\">Communication</a>';
    //                        result += '<button type="button" style="margin-left:20px" data-target="#ModalMembers" data-toggle="modal" id="al_' + msg.UPSIGroups[i].GrpId + '" class="btn btn-outline dark" value="' + msg.UPSIGroups[i].GrpId + '" onclick="fnGrpMemberAccess(this.value)">Add Member</button>';
    //                        result += '</td>';
    //                        result += '</tr>'
    //                    }
    //                }
    //                $("#tbodyCPDetail").html(result);
    //            }
    //        }
    //        else {
    //            alert(msg.Msg);
    //            $("#tbodyCPDetail").html('');
    //        }
    //    },
    //    error: function (response) {
    //        $("#Loader").hide();
    //        if (response.responseText == "Session Expired") {
    //            alert("Your session is expired. Please login again to continue");
    //            window.location.href = "../LogOut.aspx";
    //            return false;
    //        }
    //        else {
    //            alert(response.status + ' ' + response.statusText);
    //        }
    //    }
    //});
}
function convertToDateTime(date) {
    return date.split("/")[1] + "/" + date.split("/")[0] + "/" + date.split("/")[2];
}
function FormatToDateTime(strdate) {
    var date = new Date(strdate);
    return (((date.getDate() > 9) ? date.getDate() : ('0' + date.getDate())) + '/' + ((date.getMonth() > 8) ? (date.getMonth() + 1) : ('0' + (date.getMonth() + 1))) + '/' +   date.getFullYear());
}
function fnAddRow() {
    var str = '<tr>';
    str += '<td style="margin:5px;">' +
        '<input id="txtKeyword" class="form-control" placeholder="Enter Keyword" type="text" autocomplete="off" />' +
        '</td>';
    str += '<td style="margin:5px;">' +
        '<input id="txtOrder" class="form-control numerictextbox" placeholder="Enter Match Order" type="text" autocomplete="off" />' +
        '</td>';
    str += '<td style="margin:5px;">' +
        '<img onclick="javascript:fnAddRow();" src="images/icons/AddButton.png" height="24" width="24" />' +
        '&nbsp;' +
        '<img onclick="javascript:fnDeleteRow(this);" src="images/icons/MinusButton.png" height="24" width="24" />' +
        '</td>';
    str += '</tr>';
    $("#tbodyKeyword").append(str);
    $(".numerictextbox").keypress(function (e) {
        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
            return false;
        }
    });
}
function fnDeleteRow(cntrl) {
    $(cntrl).closest('tr').remove();
}
function fnGetModeofCommunication() {
    $("#Loader").show();
    var webUrl = uri + "/api/UPSIGroup/GetModeofCommunication";

    $.ajax({
        url: webUrl,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        data: {},
        success: function (msg) {
            $("#Loader").hide();
            if (msg.StatusFl == true) {
                var result = "";
                arrType = new Array();

                if (msg.UPSITypeList.length > 1) {
                    result += "<option value='0'>--Select--</option>";
                }
                for (var i = 0; i < msg.UPSITypeList.length; i++) {
                    result += "<option value='" + msg.UPSITypeList[i].COMMTYPE_ID + "'>" + msg.UPSITypeList[i].COMMTYPE_NAME + "</option>";
                    var obj = new Object();
                    obj.Id = msg.UPSITypeList[i].COMMTYPE_ID;
                    obj.Name = msg.UPSITypeList[i].COMMTYPE_NAME;
                    arrType.push(obj);
                }
                $("#ddlUPSISharingMode").append(result);

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