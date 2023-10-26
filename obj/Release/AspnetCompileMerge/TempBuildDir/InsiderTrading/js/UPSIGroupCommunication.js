function fnGrpCommunication(GrpId, GrpNm) {
    $("#txtCommunicationGrpId").val(GrpId);
    $("#lblUPSICommunication").html(GrpNm);

    var webUrl = uri + "/api/UPSIGroup/GetUPSIDesignatedPersons";
    $.ajax({
        url: webUrl,
        type: "POST",
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
                var result = "<option value=''></option>";
                var result1 = "<option value=''></option>";
                $('#ddlUPSISharedBy').empty();
                for (var i = 0; i < msg.UPSIGroups.length; i++) {
                    for (var j = 0; j < msg.UPSIGroups[i].ConnectedPersons.length; j++) {
                        result += "<option value='" + msg.UPSIGroups[i].ConnectedPersons[j].CPEmail + "'>" + msg.UPSIGroups[i].ConnectedPersons[j].CPNm + " (" + msg.UPSIGroups[i].ConnectedPersons[j].CPEmail + ")</option>";
                        result1 += "<option value='" + msg.UPSIGroups[i].ConnectedPersons[j].CPEmail + "'>" + msg.UPSIGroups[i].ConnectedPersons[j].CPNm + " (" + msg.UPSIGroups[i].ConnectedPersons[j].CPEmail + ")</option>";
                    }
                }
                $("#ddlUPSIDesignatedPerson").html(result);
                $("#ddlUPSISharedBy").html(result1);
                $("#ddlUPSISharedBy").val($("input[id*=txtLoggedUser]").val()).trigger('change');//.Change();
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

    var webUrl = uri + "/api/UPSIGroup/GetUPSIConnectedPersons";
    $.ajax({
        url: webUrl,
        type: "POST",
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
                var result = "<option value=''></option>";
                for (var i = 0; i < msg.UPSIGroups.length; i++) {
                    for (var j = 0; j < msg.UPSIGroups[i].ConnectedPersons.length; j++) {
                        result += "<option value='" + msg.UPSIGroups[i].ConnectedPersons[j].CPEmail + "'>" + msg.UPSIGroups[i].ConnectedPersons[j].CPNm + " (" + msg.UPSIGroups[i].ConnectedPersons[j].CPEmail + ")</option>";
                    }
                }
                $("#ddlUPSIConnectedPerson").html(result);
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
function fnValidateGroupCommunication() {
    var CPFlg = true;
    var DPFlg = true;
    var Count = 0;

    var SharedBy = $("#ddlUPSISharedBy").val();
    var ConnectedPersons = $("#ddlUPSIConnectedPerson").val();
    var DesignatedPersons = $("#ddlUPSIDesignatedPerson").val();
    var UPSISharedOn = $("#txtUPSISharedOn").val();
    var UPSISharingMode = $("#ddlUPSISharingMode").val();
    var UPSIAttachment = $("#txtUPSIAttachment").val();
    var UPSIRemarks = $("#txtUPSIRemarks").val();

    //alert("SharedBy=" + SharedBy);
    //alert("DesignatedPersons=" + DesignatedPersons);
    //alert("UPSISharedOn=" + UPSISharedOn);
    //alert("UPSISharingMode=" + UPSISharingMode);
    //alert("UPSIAttachment=" + UPSIAttachment);
    //alert("UPSIRemarks=" + UPSIRemarks);

    if (SharedBy == "" || SharedBy == null) {
        $('#ddlUPSISharedBy').addClass('required-red');
        $('#lblUPSISharedBy').addClass('required');
        Count++;
    }

    if (ConnectedPersons == null && DesignatedPersons == null) {
        alert("Please select UPSI shared with");
    }

    if (UPSISharedOn == "" || UPSISharedOn == null) {
        $('#txtUPSISharedOn').addClass('required-red');
        $('#lblUPSISharedOn').addClass('required');
        Count++;
    }
    else {
        $('#txtUPSISharedOn').removeClass('required-red');
        $('#lblUPSISharedOn').removeClass('required');
    }
    if (UPSISharingMode == "" || UPSISharingMode == null) {
        $('#ddlUPSISharingMode').addClass('required-red');
        $('#lblUPSISharingMode').addClass('required');
        Count++;
    }
    else {
        $('#ddlUPSISharingMode').removeClass('required-red');
        $('#lblUPSISharingMode').removeClass('required');
    }

    var ctrl = $('#txtUPSIAttachment');
    var file = $('#txtUPSIAttachment').val();
    if (file != null && file != "") {
        var ext = file.split(".");
        ext = ext[ext.length - 1].toLowerCase();
        var arExtns = ['xls', 'xlsx', 'pdf', 'ppt', 'pptx', 'doc', 'docx', 'zip', 'txt', 'msg', 'eml'];
        var fname = $('#txtUPSIAttachment')[0].files[0].name;
        var fsize = $('#txtUPSIAttachment')[0].files[0].size;
        if (arExtns.lastIndexOf(ext) == -1) {
            alert("Please select a file with  extension(s).\n" + arExtns.join(', '));
            ctrl.value = '';
            Count++;
        }
    }
    if (Count > 0) {
        return false;
    }
    return true;
}
function fnSaveUPSICommunication() {
    //alert("In function fnSaveUPSICommunication");
    if (fnValidateGroupCommunication()) {
        //alert("In true of function fnSaveUPSICommunication");
        var SharedBy = $("#ddlUPSISharedBy").val();
        var ConnectedPersons = $("#ddlUPSIConnectedPerson").val();
        var DesignatedPersons = $("#ddlUPSIDesignatedPerson").val();
        var UPSISharedOn = $("#txtUPSISharedOn").val();
        var UPSISharedAt = $("#txtUPSISharedAt").val();
        var UPSISharingMode = $("#ddlUPSISharingMode").val();
        var UPSIAttachment = $("#txtUPSIAttachment").val();
        var UPSIRemarks = $("#txtUPSIRemarks").val();
        var test = new FormData();

        //alert(ConnectedPersons);
        //alert(DesignatedPersons);

        //alert("UPSISharedAt=" + UPSISharedAt);
        var sSaveAs = "";
        if ($("input[id*='txtUPSIAttachment']").get(0).files.length > 0) {
            var param1 = new Date();
            var param2 = param1.getDate() + '_' + (param1.getMonth() + 1) + '_' + param1.getFullYear() + '_' + param1.getHours() + '_' + param1.getMinutes() + '_' + param1.getSeconds();
            var fileUpload = $("#txtUPSIAttachment").get(0);
            var files = fileUpload.files;

            for (var i = 0; i < files.length; i++) {
                test.append(files[i].name, files[i]);
            }
            var extn = $('#txtUPSIAttachment').val().split(".");
            extn = extn[extn.length - 1].toLowerCase();
            sSaveAs = 'Attachment_' + param2 + '_File.' + extn;
        }

        var token = $("#TokenKey").val();
        var UPSIGrpId = $("#txtCommunicationGrpId").val();

        test.append("SharedBy", SharedBy);
        test.append("UPSIGrpId", UPSIGrpId);
        test.append('ConnectedPersons', ConnectedPersons);
        test.append('DesignatedPersons', DesignatedPersons);
        test.append('UPSISharedOn', UPSISharedOn);
        test.append('UPSISharedAt', UPSISharedAt);
        test.append('UPSISharingMode', UPSISharingMode);
        test.append('UPSIRemarks', UPSIRemarks);
        test.append('sSaveAs', sSaveAs);

        $("#Loader").show();
        var webUrl = uri + "/api/UPSIGroup/SaveUPSIGroupCommunication";
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
                    alert(msg.Msg);
                    window.location.reload();
                }

            },
            error: function (response) {
                $("#Loader").hide();
                alert(response.status + ' ' + response.statusText);
            }
        });
    }
}