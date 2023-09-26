var arrUpsiType = new Array();
var downloadstatus = "";
var downloadFileName = "";
$(document).ready(function () {
    GetUPSITypeList();
    $(".numerictextbox").keypress(function (e) {
        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
            return false;
        }
    });
});
function validate() {
    if ($("#txtUPSIType").val() == "") {
        alert("Please Enter UPSI Type");
        return false;
    }
    if ($("#ddlStatus").val() == "0") {
        alert("Please Select UPSI type status");
        return false;
    }
    return true;
}
function fnSaveUPSIType() {
    if (validate()) {
        var UPSITypeId = "0";
        var UPSITypeNm = "";
        var status = "";

        UPSITypeId = $("#txtUPSITypeId").val();
        UPSITypeNm = $("#txtUPSIType").val();
        status = $("#ddlStatus").val();
        var keywords = new Array();
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
                    keywords.push(obj);
                }
            }
        }
        var flag = true;
        if (keywords.length == 0) {
            if (!confirm("Are you sure you want to add UPSI type without adding keywords")) {
                flag = false;
            }
        }
        if (flag) {
            $("#Loader").show();
            var webUrl = uri + "/api/UPSIConfig/AddUPSIType";
            $.ajax({
                url: webUrl,
                type: "POST",
                data: JSON.stringify({
                    TypeId: UPSITypeId, TypeNm: UPSITypeNm, Status: status, Keywords: keywords
                }),
                async: false,
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
        //if the letter is not digit then display error and don't type anything
        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
            return false;
        }
    });
}
function fnDeleteRow(cntrl) {
    $(cntrl).closest('tr').remove();
}
function fncloseModal() {
    $("#modalEmailConfig").modal("hide");
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
function GetUPSITypeList() {
    //alert("In function GetUPSITypeList");
    var table = $('#tbl-upsilist-setup').DataTable();
    if (table != "") {
        table.destroy();
    }
    var webUrl = uri + "/api/UPSIConfig/GetUPSITypeList";
    //alert("1..="+webUrl);
    $.ajax({
        type: "GET",
        url: webUrl,
        contentType: "application/json; charset=utf-8",
        datatype: "json",        
        success: function (msg) {
            //alert("In success");
            if (msg.StatusFl) {
                var result = "";
                arrUpsiType = new Array();
                arrUpsiType = msg.UPSITypeList;
                if (msg.UPSITypeList.length > 0) {
                    for (var x = 0; x < msg.UPSITypeList.length; x++) {
                        result += '<tr>';
                        result += '<td>' + msg.UPSITypeList[x].TypeNm + '</td>';
                        result += '<td>' + msg.UPSITypeList[x].Status + '</td>';
                        result += '<td><a data-target="#modalType" data-toggle="modal" class="btn btn-outline dark" onclick=\"javascript:fnEdit(\'' + msg.UPSITypeList[x].TypeId + '\',\'' + msg.UPSITypeList[x].TypeNm + '\',\'' + msg.UPSITypeList[x].Status + '\');\">Edit</a>';
                        result += '<a class="btn btn-outline dark" onclick=\"javascript:fnDelete(\'' + msg.UPSITypeList[x].TypeId + '\')";\">Delete</a></td>';
                        result += '</tr>';
                    }
                    //$("#tbdupsilist").html(result);
                }
                else {
                    result += '<tr>';
                    result += '<td colspan=3 style="text-align:center">No UPSI Type Found</td>';
                    result += '</tr>';
                }
                var table = $('#tbl-upsilist-setup').DataTable();
                table.destroy();
                $("#tbdupsilist").html(result);
                initializeDataTable();
            }
            else {
                if (msg.Msg == "No data found !") {
                    var result = "";
                    result += '<tr>';
                    result += '<td colspan=3 style="text-align:center">No UPSI Found</td>';
                    result += '</tr>';
                    var table = $('#tbl-upsilist-setup').DataTable();
                    table.destroy();
                    $("#tbodyUPSI").html(result);
                    initializeDataTable();
                }
                else {
                    alert(msg.Msg);
                }
            }
        },
        error: function (response) {
            //alert(msg.Msg);
        }
    });
    //alert("2..=" + webUrl);
}
function fnEdit(upsiTypeId, upsiNM, status) {
    reset();
    $("#txtUPSITypeId").val(upsiTypeId);
    $("#txtUPSIType").val(upsiNM);
    $("#ddlStatus").val(status);
    $("#tbodyKeyword").find("tr:not(:nth-child(1))").remove();
    var keyword = new Array();

    //alert("upsiTypeId=" + upsiTypeId);
    //alert("status=" + status);
    //alert("arrUpsiType.length=" + arrUpsiType.length);
        for (var x = 0; x < arrUpsiType.length; x++) {
            //alert("arrUpsiType[" + x + "].TypeId=" + arrUpsiType[x].TypeId);
            if (upsiTypeId == arrUpsiType[x].TypeId) {
                for (var i = 0; i < arrUpsiType[x].Keywords.length; i++) {
                    var obj = new Object();
                    obj.keyword = arrUpsiType[x].Keywords[i].keyword;
                    obj.matchOrder = arrUpsiType[x].Keywords[i].sequence;
                    keyword.push(obj);
                }
                break;
            }
        }
        if (keyword.length >= 0) {
            for (var i = 0; i < keyword.length; i++) {
                if (i == 0) {
                    $("#txtKeyword").val(keyword[i].keyword);
                    $("#txtOrder").val(keyword[i].matchOrder);
                }
                else {
                    var str = '<tr>';
                    str += '<td style="margin:5px;">' +
                        '<input id="txtKeyword' + i + '" class="form-control" placeholder="Enter Keyword" type="text" autocomplete="off" />' +
                        '</td>';
                    str += '<td style="margin:5px;">' +
                        '<input id="txtOrder' + i + '" class="form-control numerictextbox" placeholder="Enter Match Order" type="text" autocomplete="off" />' +
                        '</td>';
                    str += '<td style="margin:5px;">' +
                        '<img onclick="javascript:fnAddRow();" src="images/icons/AddButton.png" height="24" width="24" />' +
                        '&nbsp;' +
                        '<img onclick="javascript:fnDeleteRow(this);" src="images/icons/MinusButton.png" height="24" width="24" />' +
                        '</td>';
                    str += '</tr>';
                    $("#tbodyKeyword").append(str);
                    $("#txtKeyword" + i).val(keyword[i].keyword);
                    $("#txtOrder" + i).val(keyword[i].matchOrder);
                    $(".numerictextbox").keypress(function (e) {
                        //if the letter is not digit then display error and don't type anything
                        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                            return false;
                        }
                    });
                }
            }
        }

    $("#UPSIGrp").modal('show');
}
function fnDelete(upsiTYpeId) {
    var id = upsiTYpeId;
    var webUrl = "api/MultipleEmailConfig/saveEmailConfig";
    setTimeout(function () {
        $.ajax({
            type: 'POST',
            url: webUrl,
            data: JSON.stringify({
                COMPANY_ID: 1, UPSI_TYPE_ID: id, MODE: 'Delete'
            }),
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            async: false,
            success: function (msg) {
                if (msg.StatusFl) {
                    alert(msg.Msg);
                    GetEmailConfigList();
                }
                else {
                    alert(msg.Msg);
                }
            },
            error: function (error) {
                alert(msg.Msg);
            }
        })
    }, 10);
}
function reset() {
    $("#txtUPSITypeId").val("0");
    $("#txtUPSIType").prop("disabled", false);
    $("#txtUPSIType").val("");
    $("#ddlStatus").val("0").change();
    $("#txtKeyword").val("");
    $("#txtOrder").val("");
    $("#tbodyKeyword").find("tr:not(:nth-child(1))").remove();
}