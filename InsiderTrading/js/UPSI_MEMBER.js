var VendorData = "";
var UserData = "";
var UPSIType = "";
var VendorDataArrAY = new Array();
var MemberName = "";
$(document).ready(function () {
    $("#Loader").hide();
    GetUPSIGroupList();
    fnGetAllVendor();
    fnGetAllUser();
    fnGetAllUPSIType();
    $('body').on('click', '.adddOtherMember', function () {
        var strTable = fnCreateOtherMemberUi();
        $('#tbodyOtherMemberDetail').append(strTable);
    });
    $('body').on('click', '.removeOtherMember', function () {

        $(this).closest('tr').remove();
    });
    $("button[id*='btnAddupsi']").on("click", function () {
        fnAddsUPSI();
    });
    $('#ddlupsiDesignatedM').select2({
        'placeholder': 'Select Designated Members',
        'width': '587px',
        'border-color': 'red !important'
    })
    $('#txtupsiGroupType').select2({
        'placeholder': 'Select UPSI Type',
        'width': '587px',
        'border-color': 'red !important'
    })
    $(document).on('change', '#txtupsiGroupNM', function () {
        $('#txtupsiGroupNM').removeClass('required-red');
        $('#lblupsiGroupNM').removeClass('required-red');
    });
    $('body').on('focus', '.addothermembername', function (e) {
        $('input[id*="txtOtherMemberName"]').autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: uri + "/api/UPSIMembersGreoup/GetNonDesignatedUPSIMember",
                    type: "POST",
                    data: JSON.stringify({
                        GROUP_NM: request.term
                    }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        $("#Loader").hide();
                        if (!data.StatusFl) {
                            if (data.Msg == "SessionExpired") {
                                alert("Your session is expired. Please login again to continue");
                                window.location.href = "../LogOut.aspx";
                            }
                            else {
                                return false;
                            }
                        }
                        else {
                            objUser = [];
                            $("#txtUPSISharedWithActual").val('');
                            $("#txtUPSIMemberPan").val('');
                            $.each(data.UPSIMembersGroupList[0].listNonDesignatedMember, function (index, item) {
                                objUser.push(item);
                            })
                            $('.ui-autocomplete').css({ 'z-index': '2147483647' });
                            response($.map(data.UPSIMembersGroupList[0].listNonDesignatedMember, function (item) {
                                return {
                                    label: item.NAME + " (" + item.EMAIL + ")",
                                    val: item.EMAIL
                                }
                            }))
                        }
                    },
                    error: function (response) {
                        $("#Loader").hide();
                        if (response.responseText == "Session Expired") {
                            alert("Your session is expired. Please login again to continue");
                            $("#Loader").show();
                            window.location.href = "../LogOut.aspx";
                            return false;
                        }
                        else {
                            alert(response.status + ' ' + response.statusText);
                        }
                    }
                });
            },
            select: function (e, i) {
                for (var j = 0; j < objUser.length; j++) {
                    if (objUser[j].EMAIL == i.item.val) {
                        $(this).closest('tr').find("td:eq(1)").find('input[type="text"]').val(objUser[j].EMAIL);
                        $(this).closest('tr').find("td:eq(2)").find('input[type="text"]').val(objUser[j].PAN);
                        $(this).closest('tr').find("td:eq(2)").find('input[type="text"]').attr("disabled", 'disabled');
                        MemberName = objUser[j].NAME;
                        $(this).closest('tr').find("td:eq(3)").find('select').val(objUser[j].VENDOR_ID).change();
                    }
                }
                $(e.target).next().val(i.item.val);
                var tempTdName = $(this);
                setTimeout(function () {
                    tempTdName.closest('tr').find("td:eq(0)").find('input[type="text"]').val(MemberName);
                }, 500)
            },
            minLength: 3
        });
    });
});
function GetUPSIGroupList() {
    $("#Loader").show();
    var webUrl = uri + "/api/UPSIMembersGreoup/GetUPSIGroupList";
    $.ajax({
        url: webUrl,
        type: "POST",
        async: false,
        data: JSON.stringify({
            GROUP_ID: "0"
        }),
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
                for (var i = 0; i < msg.UPSIMembersGroupList.length; i++) {
                    result += '<tr>'
                    result += '<td>' + (i + 1) + '</td>';
                    result += '<td>' + msg.UPSIMembersGroupList[i].GROUP_NM + '</td>';
                    result += '<td>' + msg.UPSIMembersGroupList[i].VALID_FROM + '</td>';
                    result += '<td>' + msg.UPSIMembersGroupList[i].VALID_TLL + '</td>';
                    result += '<td>' + msg.UPSIMembersGroupList[i].CreatedBy + '</td>';
                    result += '<td id="tdEditDelete_' + msg.UPSIMembersGroupList[i].GROUP_ID + '"><a data-target="#stack1" data-toggle="modal" id="a_' + msg.UPSIMembersGroupList[i].GROUP_ID + '" class="btn btn-outline dark" onclick=\"javascript:fnEditUPSIGroup(' + msg.UPSIMembersGroupList[i].GROUP_ID + ');\">Edit</a><a style="margin-left:20px" data-target="#delete" data-toggle="modal" id="d_' + msg.UPSIMembersGroupList[i].GROUP_ID + '" class="btn btn-outline dark" onclick=\"javascript:fnDeleteUPSIGroup(' + msg.UPSIMembersGroupList[i].GROUP_ID + ');\">Delete</a></td>';
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
function fnGetAllVendor() {
    $("#Loader").show();
    var webUrl = uri + "/api/UPSIMembersGreoup/GetVendorList";
    $.ajax({
        url: webUrl,
        type: "POST",
        async: false,
        data: JSON.stringify({
            VendorId: "0"
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            $("#Loader").hide();
            if (msg.Msg == "SessionExpired") {
                alert("Your session is expired. Please login again to continue");
                window.location.href = "../LogOut.aspx";
            }
            if (msg.StatusFl == true) {
                var result = "<option id='0'>Select</option>";
                for (var i = 0; i < msg.UPSIMembersGroup.lisVender.length; i++) {
                    result += '<option value="' + msg.UPSIMembersGroup.lisVender[i].VendorId + '">' + msg.UPSIMembersGroup.lisVender[i].vendorName + '</option>';
                    var obj = new Object()
                    obj.VendorId = msg.UPSIMembersGroup.lisVender[i].VendorId;
                    obj.vendorName = msg.UPSIMembersGroup.lisVender[i].vendorName;
                    VendorDataArrAY.push(obj);
                }
                VendorData = "";
                VendorData = result;
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
    var webUrl = uri + "/api/UPSIMembersGreoup/GetUPSITypeList";
    $.ajax({
        url: webUrl,
        type: "POST",
        async: false,
        data: JSON.stringify({
            VendorId: "0"
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            $("#Loader").hide();
            if (msg.Msg == "SessionExpired") {
                alert("Your session is expired. Please login again to continue");
                window.location.href = "../LogOut.aspx";
            }
            if (msg.StatusFl == true) {
                var result = "<option id='0'>Select</option>";
                for (var i = 0; i < msg.UPSIMembersGroup.listGroupType.length; i++) {
                    result += '<option value="' + msg.UPSIMembersGroup.listGroupType[i].GROUP_TYPE_ID + '">' + msg.UPSIMembersGroup.listGroupType[i].GROUP_TYPE + '</option>';
                }
                UPSIType = "";
                UPSIType = result;
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
function fnGetAllUser() {
    $("#Loader").show();
    var webUrl = uri + "/api/UPSIMembersGreoup/GetUserList";
    $.ajax({
        url: webUrl,
        type: "POST",
        async: false,
        data: JSON.stringify({
            VendorId: "0"
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            $("#Loader").hide();
            if (msg.Msg == "SessionExpired") {
                alert("Your session is expired. Please login again to continue");
                window.location.href = "../LogOut.aspx";
            }
            if (msg.StatusFl == true) {
                var result = "<option id='0'>Select</option>";
                for (var i = 0; i < msg.UPSIMembersGroup.listUser.length; i++) {
                    result += '<option value="' + msg.UPSIMembersGroup.listUser[i].ID + '">' + msg.UPSIMembersGroup.listUser[i].USER_NM + '(' + msg.UPSIMembersGroup.listUser[i].USER_EMAIL + ')</option>';
                }
                UserData = "";
                UserData = result;
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
function fnCreateOtherMemberUi() {
    var rows = $('#OtherMemberDetail tbody tr').length;
    var strTable = '<tr id="row_' + rows + '">';
    strTable += '<td  width="20%">';
    strTable += '<input id="txtOtherMemberName' + rows + '" type="text" class = "form-control addothermembername" autocomplete="off" onchange="removeOtherMemberClass(this)"/>';
    strTable += '</td>';
    strTable += '<td width="20%">';
    strTable += '<input id="txtOtherMemberEmail' + rows + '" type="text" class = "form-control" autocomplete="off" onchange="removeOtherMemberClass(this)"/>';
    strTable += '</td>';
    strTable += '<td width="20%">';
    strTable += '<input id="txtOtherMemberPanNo' + rows + '" type="text" class = "form-control" autocomplete="off" onchange="removeOtherMemberClass(this)"/>';
    strTable += '</td>';
    strTable += '<td width="20%">';
    strTable += '<select id="txtOtherMemberVendor' + rows + '" class = "form-control" onchange="removeOtherMemberClass(this)">' + VendorData + ' </select>';
    strTable += '</td>';
    strTable += '<td width="10%"><button type="button" class="btn blue btn-outline adddOtherMember">Add</button>';
    if (rows != '0') {
        strTable += '<td width="10%"><button type="button" class="btn red btn-outline removeOtherMember">Remove</button></td>';
    }
    strTable += '</tr>';
    return strTable;
}
function fnAddsUPSI() {
    $("#Loader").show();
    $('#txtupsiGroupNM').removeClass('required-red');
    $('#lblupsiGroupNM').removeClass('required');
    $('#txtupsiGroupType').removeClass('required-red');
    $('#lblupsiGroupTyp').removeClass('required');
    $('#fromtxtdate').removeClass('required-red');
    $('#lblvalidaty').removeClass('required');
    $('#tilltxtdate').removeClass('required-red');
    $('#lblvalidaty').removeClass('required');
    $('#lblupsiDescription').removeClass('required');
    $('#txtupsiDescription').removeClass('required-red');
    $('#colupsiDesignatedM').removeClass('required-red-border');
    $('#lblupsiDesignatedM').removeClass('required');
    $('#colupsiDesignatedT').removeClass('required-red-border');
    $('#txtupsiGroupType').removeClass('required-red');
    $('#lblupsiGroupTyp').removeClass('required');
    $('select[id*=ddlupsiDesignatedM]').html('');
    $("#ddlupsiDesignatedM").html(UserData);
    $('select[id*=txtupsiGroupType]').html('');
    $("#txtupsiGroupType").html(UPSIType);

    var strTable = fnCreateOtherMemberUi();
    $('#tbodyOtherMemberDetail').html('');
    $('#tbodyOtherMemberDetail').append(strTable);
    $('#txtupsiGroupID').val('0');
    $('#txtupsiDescription').val('');
    $("input[id*='upsiVersion']").val('1');
    $("input[id*='txtupsiGroupNM']").removeAttr("disabled", "disabled");
    $("select[id*='txtupsiGroupType']").removeAttr("disabled", "disabled");
    $("input[id*='fromtxtdate']").removeAttr("disabled", "disabled");
    $("input[id*='tilltxtdate']").removeAttr("disabled", "disabled");
    $("#stack1").modal('show');
    $("#Loader").hide();
}
function initializeDataTable() {
    $('#sample_1_2').DataTable({
        "dom": '<"top"iflp<"clear">>rt<"bottom"iflp<"clear">>',
        dom: 'Bfrtip',
        pageLength: 5,
        buttons: [
        ],
    });
}
function validation() {
    var textGroupName = $("input[id*='txtupsiGroupNM']").val();
    var textGroupTypeId = $("select[id*='txtupsiGroupType']").val()
    var textFromDate = $("input[id*='fromtxtdate']").val();
    var textTillDate = $("input[id*='tilltxtdate']").val();
    var textVersion = $("input[id*='upsiVersion']").val();
    var textDescription = $("textarea[id*='txtupsiDescription']").val();
    var DesignatedMember = $("select[id*='ddlupsiDesignatedM']").val();
    var Count = 0;
    var panno = new Array()

    $("#tbodyOtherMemberDetail > tr").each(function () {
        var textOtherMemberName = $(this).find("td").eq(0).find("input[type='text']").val();
        var textOtherMemberEmail = $(this).find("td").eq(1).find("input[type='text']").val();
        var textOtherMemberPAN = $(this).find("td").eq(2).find("input[type='text']").val();
        var textOtherMemberVendor = $(this).find("td").eq(3).find("select[id*='txtOtherMemberVendor']").val();
        if (textOtherMemberName == undefined || textOtherMemberName == "" || textOtherMemberName == null) {
            $(this).find("td").eq(0).find("input[type='text']").addClass('required-red');
            Count++
            return false;
        }
        else {
            $(this).find("td").eq(0).find("input[type='text']").removeClass('required-red');
        }
        if (textOtherMemberEmail == undefined || textOtherMemberEmail == "" || textOtherMemberEmail == null) {
            Count++
            $(this).find("td").eq(1).find("input[type='text']").addClass('required-red');
            return false;
        }
        else {
            if (!validateEmail(textOtherMemberEmail)) {
                $(this).find("td").eq(1).find("input[type='text']").addClass('required-red');
                Count++
                return false;
            }
            $(this).find("td").eq(1).find("input[type='text']").removeClass('required-red');
        }
        if (textOtherMemberPAN == undefined || textOtherMemberPAN == "" || textOtherMemberPAN == null || textOtherMemberPAN.length < 10) {
            Count++
            $(this).find("td").eq(2).find("input[type='text']").addClass('required-red');
            return false;
        }
        else {
            if (!validatePanNO(textOtherMemberPAN)) {
                $(this).find("td").eq(2).find("input[type='text']").addClass('required-red');
                Count++
                return false;
            }
            if (panno.length > 0) {
                for (var i = 0; i <= panno.length; i++) {
                    if (panno[i] == textOtherMemberPAN) {
                        $(this).find("td").eq(2).find("input[type='text']").addClass('required-red');
                        Count++
                        return false;
                    }
                }
            }
            $(this).find("td").eq(2).find("input[type='text']").removeClass('required-red');
            panno.push(textOtherMemberPAN);
        }
        if (textOtherMemberVendor == undefined || textOtherMemberVendor == "" || textOtherMemberVendor == null || textOtherMemberVendor == "0" || textOtherMemberVendor == "Select") {
            Count++
            $(this).find("td").eq(3).find("select[id*='txtOtherMemberVendor']").addClass('required-red');
            return false;
        }
        else {
            $(this).find("td").eq(3).find("select[id*='txtOtherMemberVendor']").removeClass('required-red');
        }
    });
    if (textGroupName == undefined || textGroupName == "" || textGroupName == null) {
        $('#txtupsiGroupNM').addClass('required-red');
        $('#lblupsiGroupNM').addClass('required');
        Count++
    }
    else {
        $('#txtupsiGroupNM').removeClass('required-red');
        $('#lblupsiGroupNM').removeClass('required');
    }
    if (textGroupTypeId == undefined || textGroupTypeId == "" || textGroupTypeId == null || textGroupTypeId == "0") {
        $('#colupsiDesignatedT').addClass('required-red-border');
        $('#txtupsiGroupType').addClass('required-red');
        $('#lblupsiGroupTyp').addClass('required');
        Count++
    }
    else {
        $('#txtupsiGroupType').removeClass('required-red');
        $('#lblupsiGroupTyp').removeClass('required');

    }
    if (textFromDate == undefined || textFromDate == "" || textFromDate == null || textFromDate == "0") {
        $('#fromtxtdate').addClass('required-red');
        $('#lblvalidaty').addClass('required');
        Count++
    }
    else {
        $('#fromtxtdate').removeClass('required-red');
        $('#lblvalidaty').removeClass('required');
    }
    if (textTillDate == undefined || textTillDate == "" || textTillDate == null || textTillDate == "0") {
        $('#tilltxtdate').addClass('required-red');
        $('#lblvalidaty').addClass('required');
        Count++
    }
    else {
        $('#tilltxtdate').removeClass('required-red');
        $('#lblvalidaty').removeClass('required');
    }
    if (textDescription == undefined || textDescription == "" || textDescription == null || textDescription == 1) {
        $('#lblupsiDescription').addClass('required');
        $('#txtupsiDescription').addClass('required-red');
        Count++
    }
    else {
        $('#lblupsiDescription').removeClass('required');
        $('#txtupsiDescription').removeClass('required-red');
    }

    if (DesignatedMember == undefined || DesignatedMember == "" || DesignatedMember == null || DesignatedMember == "0") {
        $('#colupsiDesignatedM').addClass('required-red-border');
        $('#lblupsiDesignatedM').addClass('required');
        Count++
    }
    else {
        $('#colupsiDesignatedM').removeClass('required-red-border');
        $('#lblupsiDesignatedM').removeClass('required');
    }
    if (Count == 0) {
        return true;
    }
    else {
        return false;
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
function removeRedClass(lbl, elememnt) {
    $('#' + lbl).removeClass('required');
    $('#' + elememnt).removeClass('required-red');
    $('#' + elememnt).removeClass('required-red-border');
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
function fnSaveupsi() {
    var status = validation();
    if (status) {
        var textGroupid = $("input[id*='txtupsiGroupID']").val();
        var textGroupName = $("input[id*='txtupsiGroupNM']").val();
        var textGroupTypeId = $("select[id*='txtupsiGroupType']").val()
        var textFromDate = $("input[id*='fromtxtdate']").val();
        var textTillDate = $("input[id*='tilltxtdate']").val();
        var textVersion = $("input[id*='upsiVersion']").val();
        var textDescription = $("textarea[id*='txtupsiDescription']").val();
        var DesignatedMember = $("select[id*='ddlupsiDesignatedM']").val();
        var userDate = textFromDate;
        var from = userDate.split("/");
        var fr_date = from.reverse().join("-");
        from_date = fr_date;
        userDate = textTillDate;
        from = userDate.split("/");

        var ti_date = from.reverse().join("-");
        till_date = ti_date;

        var Count = 0;
        var DesignatedmembersDetail = new Array();
        for (var i = 0; i < DesignatedMember.length; i++) {
            var obj = new Object();
            obj.ID = DesignatedMember[i];
            DesignatedmembersDetail.push(obj);
        }
        var UPSITypearray = new Array();
        for (var i = 0; i < textGroupTypeId.length; i++) {
            var obj = new Object();
            obj.GROUP_TYPE_ID = textGroupTypeId[i];
            UPSITypearray.push(obj);
        }
        var othermembersDetail = new Array();
        $("#tbodyOtherMemberDetail > tr").each(function () {
            var textOtherMemberName = $(this).find("td").eq(0).find("input[type='text']").val();
            var textOtherMemberEmail = $(this).find("td").eq(1).find("input[type='text']").val();
            var textOtherMemberPAN = $(this).find("td").eq(2).find("input[type='text']").val();
            var textOtherMemberVendor = $(this).find("td").eq(3).find("select[id*='txtOtherMemberVendor']").val();
            var obj = new Object()

            obj.NAME = textOtherMemberName;
            obj.EMAIL = textOtherMemberEmail;
            obj.PAN = textOtherMemberPAN;
            obj.VENDOR_ID = textOtherMemberVendor;
            othermembersDetail.push(obj);
         );
        var objgroup = new Object();
        objgroup.GROUP_ID = parseInt(textGroupid);
        objgroup.GROUP_NM = textGroupName;
        objgroup.listGroupType = UPSITypearray;
        objgroup.GROUP_DESC = textDescription;
        objgroup.VALID_FROM = from_date;
        objgroup.VALID_TLL = till_date;
        objgroup.STATUS = 'Active';
        objgroup.listDesignatedMember = DesignatedmembersDetail;
        objgroup.listNonDesignatedMember = othermembersDetail;
        var token = $("#TokenKey").val();

        $("#Loader").show();
        var webUrl = uri + "/api/UPSIMembersGreoup/SaveUPSI";
        $.ajax({
            url: webUrl,
            headers: {
                'TokenKeyH': token,
            },
            type: "POST",
            async: false,
            data: JSON.stringify(objgroup),
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            success: function (msg) {
                $("#Loader").hide();
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                if (msg.StatusFl == true) {
                    $("#stack1").modal('hide');
                    alert(msg.Msg);
                    window.location.reload();
                }
                else {
                    alert(msg.Msg);
                }
            },
            error: function (err) {
                $("#Loader").hide();
                if (err.responseText == "Session Expired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                    return false;
                }
                else {
                    alert(err.status + ' ' + err.statusText);
                }
            }
        });
    }
    else {
        return false;
    }
}
function fnCloseModal() {
    $("input[id*='txtupsiGroupNM']").val('');
    $("select[id*='txtupsiGroupType']").val('')
    $("input[id*='fromtxtdate']").val('');
    $("input[id*='tilltxtdate']").val('');
    $("input[id*='upsiVersion']").val('');
    $("select[id*='ddlupsiDesignatedM']").val('');
    $("#tbodyOtherMemberDetail").html('');
    $("#stack1").modal('hide');
}
function fnEditUPSIGroup(id) {
    $("input[id*='txtupsiGroupID']").val(id);
    $("#Loader").show();
    var webUrl = uri + "/api/UPSIMembersGreoup/GetUPSIGroupListByID";
    $.ajax({
        url: webUrl,
        type: "POST",
        async: false,
        data: JSON.stringify({
            GROUP_ID: id
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            $("#Loader").hide();
            if (msg.Msg == "SessionExpired") {
                alert("Your session is expired. Please login again to continue");
                window.location.href = "../LogOut.aspx";
            }
            if (msg.StatusFl == true) {
                if (msg.UPSIMembersGroupList[0].CreatedBy == $("input[id*='txtupsiCREATEDBY']").val()) {
                    $("input[id*='txtupsiGroupNM']").removeAttr("disabled", "disabled");
                    $("select[id*='txtupsiGroupType']").removeAttr("disabled", "disabled");
                    $("input[id*='fromtxtdate']").removeAttr("disabled", "disabled");
                    $("input[id*='tilltxtdate']").removeAttr("disabled", "disabled");
                }
                else {
                    $("input[id*='txtupsiGroupNM']").attr("disabled", "disabled");
                    $("select[id*='txtupsiGroupType']").attr("disabled", "disabled");
                    $("input[id*='fromtxtdate']").attr("disabled", "disabled");
                    $("input[id*='tilltxtdate']").attr("disabled", "disabled");
                }
                $("input[id*='txtupsiGroupID']").val(msg.UPSIMembersGroupList[0].GROUP_ID);
                $("input[id*='txtupsiGroupNM']").val(msg.UPSIMembersGroupList[0].GROUP_NM);
                $("select[id*='txtupsiGroupType']").val(msg.UPSIMembersGroupList[0].GROUP_TYPE).change();
                $("input[id*='fromtxtdate']").val(msg.UPSIMembersGroupList[0].VALID_FROM);
                $("input[id*='tilltxtdate']").val(msg.UPSIMembersGroupList[0].VALID_TLL);
                $("input[id*='upsiVersion']").val(msg.UPSIMembersGroupList[0].VERSION);
                $("textarea[id*='txtupsiDescription']").val(msg.UPSIMembersGroupList[0].GROUP_DESC);

                var degm = new Array()
                for (var i = 0; i < msg.UPSIMembersGroupList[0].listDesignatedMember.length; i++) {
                    degm.push(msg.UPSIMembersGroupList[0].listDesignatedMember[i].ID);
                }
                $("#ddlupsiDesignatedM").html(UserData);
                $("#ddlupsiDesignatedM").val(degm).change();

                var utype = new Array()
                for (var i = 0; i < msg.UPSIMembersGroupList[0].listGroupType.length; i++) {
                    utype.push(msg.UPSIMembersGroupList[0].listGroupType[i].GROUP_TYPE_ID);
                }

                $("#txtupsiGroupType").html(UPSIType);
                $("#txtupsiGroupType").val(utype).change();

                var strTable = "";
                var rows = $('#OtherMemberDetail tbody tr').length;
                for (var i = 0; i < msg.UPSIMembersGroupList[0].listNonDesignatedMember.length; i++) {
                    rows = 1;
                    strTable += '<tr id="row_' + rows + '">';
                    strTable += '<td  width="20%">';
                    strTable += '<input id="txtOtherMemberName' + rows + '" type="text"  value="' + msg.UPSIMembersGroupList[0].listNonDesignatedMember[i].NAME + '" class = "form-control" autocomplete="off" onchange="removeOtherMemberClass(this)"/>';
                    strTable += '</td>';
                    strTable += '<td width="20%">';
                    strTable += '<input id="txtOtherMemberEmail' + rows + '" type="text"  value="' + msg.UPSIMembersGroupList[0].listNonDesignatedMember[i].EMAIL + '" class = "form-control" autocomplete="off" onchange="removeOtherMemberClass(this)"/>';
                    strTable += '</td>';
                    strTable += '<td width="20%">';
                    strTable += '<input id="txtOtherMemberPanNo' + rows + '" type="text" value="' + msg.UPSIMembersGroupList[0].listNonDesignatedMember[i].PAN + '" class = "form-control" autocomplete="off" onchange="removeOtherMemberClass(this)"/>';
                    strTable += '</td>';
                    strTable += '<td width="20%">';

                    var vdata = fnGetAllVendor_byid(msg.UPSIMembersGroupList[0].listNonDesignatedMember[i].VENDOR_ID);
                    strTable += '<select id="txtOtherMemberVendor' + rows + '"  value="' + msg.UPSIMembersGroupList[0].listNonDesignatedMember[i].VENDOR_ID + '" class = "form-control" onchange="removeOtherMemberClass(this)">' + vdata + ' </select>'; vdata
                    strTable += '</td>';
                    strTable += '<td width="10%"><button type="button" class="btn blue btn-outline adddOtherMember">Add</button>';
                    if (rows != '0') {
                        strTable += '<td width="10%"><button type="button" class="btn red btn-outline removeOtherMember">Remove</button></td>';
                    }
                    strTable += '</tr>';
                }
                $('#tbodyOtherMemberDetail').html('');
                $('#tbodyOtherMemberDetail').append(strTable);
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
function fnDeleteUPSIGroup(gid) {
    var webUrl = uri + "/api/UPSIMembersGreoup/DeleteUPSIGroup";
    if (confirm("Do you want to delete this group!")) {
        $.ajax({
            url: webUrl,
            type: "POST",
            async: false,
            data: JSON.stringify({
                GROUP_ID: gid
            }),
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
    else {
        return false;
    }
}
function fnGetAllVendor_byid(vid) {
    var result = "<option id='0'>Select</option>";
    for (var i = 0; i < VendorDataArrAY.length; i++) {
        if (VendorDataArrAY[i].VendorId == vid) {
            result += '<option value="' + VendorDataArrAY[i].VendorId + '" selected >' + VendorDataArrAY[i].vendorName + '</option>';
        }
        else {
            result += '<option value="' + VendorDataArrAY[i].VendorId + '">' + VendorDataArrAY[i].vendorName + '</option>';
        }
    }
    return result;
}