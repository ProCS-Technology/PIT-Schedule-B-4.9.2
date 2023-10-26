var grpList = null;
$(document).ready(function () {
    $("#Loader").hide();
    $("#txtUPSITaskID").val('');
    GetAllUPSITask();
    GetAllUPSIGroup();
});
function GetAllUPSITask() {
    $("#txtUPSITaskID").val('');
    $("#UPSITaskTbody").html('');
    $("#Loader").show();
    var webUrl = uri + "/api/DashboardIT/GetMyUPSITask";
    $.ajax({
        url: webUrl,
        type: "POST",
        async: false,
        success: function (msg) {
            $("#Loader").hide();
            if (msg.Msg == "SessionExpired") {
                alert("Your session is expired. Please login again to continue");
                window.location.href = "../LogOut.aspx";
            }
            if (msg.StatusFl == true) {
                var result = "";
                //alert(msg.Dashboard.listUPSITask.length);
                if (msg.Dashboard.listUPSITask != null) {
                    if (msg.Dashboard.listUPSITask.length > 0) {
                        for (var i = 0; i < msg.Dashboard.listUPSITask.length; i++) {
                            result += '<tr id="tr_' + msg.Dashboard.listUPSITask[i].TaskId + '">';
                            result += '<td>' + msg.Dashboard.listUPSITask[i].EmailDate + '</td>';
                            result += '<td>' + msg.Dashboard.listUPSITask[i].EmailFrom + '</td>';
                            var strTo = msg.Dashboard.listUPSITask[i].EmailTo;
                            if (strTo.length > 50) {
                                result += '<td>' + msg.Dashboard.listUPSITask[i].EmailTo.substring(0, 50) + '...</td>';
                            }
                            else {
                                result += '<td>' + msg.Dashboard.listUPSITask[i].EmailTo + '</td>';
                            }
                            var msgSubject = msg.Dashboard.listUPSITask[i].EmailSubject;
                            if (msgSubject.length > 100) {
                                result += '<td>' + msg.Dashboard.listUPSITask[i].EmailSubject.substring(0, 100) + '...</td>';
                            }
                            else {
                                result += '<td>' + msg.Dashboard.listUPSITask[i].EmailSubject + '</td>';
                            }
                            result += '<td><a data-target="#stackUPSIMessage" data-toggle="modal" class="btn btn-outline dark" onclick=\"javascript:fnGetUPSIMessage(' + msg.Dashboard.listUPSITask[i].TaskId + ');\"><i class="fa fa-search"></i></a>&nbsp;&nbsp;';
                            result += '<a data-target="#stack1" data-toggle="modal" id="a_' + msg.Dashboard.listUPSITask[i].TaskId + '" class="btn btn-outline dark" onclick=\"javascript:fnGetUPSITaskById(' + msg.Dashboard.listUPSITask[i].TaskId + ');\"><i class="fa fa-edit"></a></td>';
                        }
                        $("#UPSITaskTbody").html('');
                        $("#UPSITaskTbody").html(result);
                    }
                    else {
                        $("#UPSI_PortletBox").addClass('display-none');
                    }
                }
                
            }
            else {
                $("#UPSI_PortletBox").addClass('display-none');
                //alert(msg.Msg);
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
function GetAllUPSIGroup() {
    $("#Loader").show();
    var webUrl = uri + "/api/UPSIGroup/GetUPSIGroupList";
    $.ajax({
        url: webUrl,
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
                var result = '<option value="0" selected>Select</option>';
                grpList = msg.UPSIGroups;
                for (var i = 0; i < msg.UPSIGroups.length; i++) {
                    result += '<option value="' + msg.UPSIGroups[i].GrpId + '">' + msg.UPSIGroups[i].GrpNm + '</option>';
                }
                $("#textUpsigroup").html('');
                $("#textUpsigroup").html(result);
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
function fnGetUPSITaskById(taskid) {
    $("#Loader").show();
    $("#hdnTaskId").val(taskid);
    var webUrl = uri + "/api/UPSIGroup/GetUPSITaskById?TaskId=" + taskid;
    var token = $("#TokenKey").val();
    $.ajax({
        url: webUrl,
        type: "POST",
        async: false,
        headers: {
            'TokenKeyH': token,
        },
        success: function (msg) {
            $("#Loader").hide();
            if (msg.Msg == "SessionExpired") {
                alert("Your session is expired. Please login again to continue");
                window.location.href = "../LogOut.aspx";
            }
            if (msg.StatusFl == true) {
                var result = '';
                for (var i = 0; i < msg.UPSIGroups[0].ConnectedPersons.length; i++) {
                    result += '<tr>';

                    result += '<td style="margin:5px;">';
                    result += '<input value="' + msg.UPSIGroups[0].ConnectedPersons[i].CPEmail + '" id="txtCPEmail" class="form-control form-control-inline" placeholder="Enter Connected Person" type="text" autocomplete="off" disabled />';
                    result += '</td>';

                    result += '<td style="margin:5px;">';
                    if (msg.UPSIGroups[0].ConnectedPersons[i].CPNm == "") {
                        result += '<input value="' + msg.UPSIGroups[0].ConnectedPersons[i].CPNm + '" id="txtCPNm" class="form-control form-control-inline" placeholder="Enter Connected Person" type="text" autocomplete="off" />';
                    }
                    else {
                        result += '<input disabled value="' + msg.UPSIGroups[0].ConnectedPersons[i].CPNm + '" id="txtCPNm" class="form-control form-control-inline" placeholder="Enter Connected Person" type="text" autocomplete="off" />';
                    }
                    result += '</td>';

                    result += '<td style="margin:5px;">';
                    if (msg.UPSIGroups[0].ConnectedPersons[i].IdentificationTyp == "") {
                        result += '<select id="ddlCPIdentification" class="form-control">';
                    }
                    else {
                        result += '<select id="ddlCPIdentification" disabled class="form-control">';
                    }

                    result += '<option value=""></option>';
                    if (msg.UPSIGroups[0].ConnectedPersons[i].IdentificationTyp == "AADHAR CARD") {
                        result += '<option value="AADHAR CARD" selected>AADHAR CARD</option>';
                    }
                    else {
                        result += '<option value="AADHAR CARD">AADHAR CARD</option>';
                    }

                    if (msg.UPSIGroups[0].ConnectedPersons[i].IdentificationTyp == "DRIVING LICENSE") {
                        result += '<option value="DRIVING LICENSE" selected>DRIVING LICENSE</option>';
                    }
                    else {
                        result += '<option value="DRIVING LICENSE">DRIVING LICENSE</option>';
                    }

                    if (msg.UPSIGroups[0].ConnectedPersons[i].IdentificationTyp == "PAN") {
                        result += '<option value="PAN" selected>PAN</option>';
                    }
                    else {
                        result += '<option value="PAN">PAN</option>';
                    }

                    if (msg.UPSIGroups[0].ConnectedPersons[i].IdentificationTyp == "PASSPORT") {
                        result += '<option value="PASSPORT" selected>PASSPORT</option>';
                    }
                    else {
                        result += '<option value="PASSPORT">PASSPORT</option>';
                    }

                    result += '</select>';
                    result += '</td>';

                    result += '<td style="margin:5px;">';
                    if (msg.UPSIGroups[0].ConnectedPersons[i].IdentificationId == "") {
                        result += '<input value="' + msg.UPSIGroups[0].ConnectedPersons[i].IdentificationId + '" id="txtCPIdentificationNo" class="form-control form-control-inline" placeholder="Enter Identification #" type="text" autocomplete="off" />';
                    }
                    else {
                        result += '<input disabled value="' + msg.UPSIGroups[0].ConnectedPersons[i].IdentificationId + '" id="txtCPIdentificationNo" class="form-control form-control-inline" placeholder="Enter Identification #" type="text" autocomplete="off" />';
                    }
                    result += '</td>';

                    result += '<td style="margin:5px;">';
                    result += '<input value="' + msg.UPSIGroups[0].ConnectedPersons[i].CPStatus + '" id="txtCPType" class="form-control form-control-inline" type="text" style="display:none;" />';
                    result += '<input id="txtCPRemarks" class="form-control form-control-inline" type="text" value="' + msg.UPSIGroups[0].ConnectedPersons[i].CPFirmNm + '" />';
                    result += '</td>';
                    result += '</tr>';
                }
                fnBindAllUPSIGroup(msg.UPSIGroups[0].GrpId);
                $("#tbdCPAdd").html(result);
            }
        },
        error: function (response) {
            $("#Loader").hide();
            $("#txtUPSITaskID").val('');
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
function fnGetUPSIMessage(taskid) {
    $("#Loader").show();
    var webUrl = uri + "/api/DashboardIT/GetMyUPSITaskById";
    $.ajax({
        url: webUrl,
        type: "POST",
        async: false,
        data: {
            TaskId: taskid
        },
        success: function (msg) {
            $("#Loader").hide();
            if (msg.Msg == "SessionExpired") {
                alert("Your session is expired. Please login again to continue");
                window.location.href = "../LogOut.aspx";
            }
            if (msg.StatusFl == true) {
                $("#dvUPSITaskMSGBody").html(msg.Dashboard.listUPSITask[0].TaskMailBody);
                $("#dvUPSITaskMSGFrom").html(msg.Dashboard.listUPSITask[0].EmailFrom);
                $("#dvUPSITaskMSGTo").html(msg.Dashboard.listUPSITask[0].EmailTo);
                $("#dvUPSITaskMSGCC").html(msg.Dashboard.listUPSITask[0].EmailCC);
                $("#dvUPSITaskMsgDate").html(msg.Dashboard.listUPSITask[0].EmailDate);
                $("#dvUPSITaskMsgSubject").html(msg.Dashboard.listUPSITask[0].EmailSubject);                
                $("#dvAttechmentlistMsg").html('');

                if (msg.Dashboard.listUPSITask[0].Group_id == "Subject") {
                    $("#dvSubjectHdr").show();
                    $("#dvMessageHdr").hide();
                    $("#dvAttachmentHdr").hide();
                }
                else if (msg.Dashboard.listUPSITask[0].Group_id == "Message") {
                    $("#dvSubjectHdr").show();
                    $("#dvMessageHdr").show();
                    $("#dvAttachmentHdr").hide();
                }
                else if (msg.Dashboard.listUPSITask[0].Group_id == "MessageAttachment") {
                    $("#dvSubjectHdr").show();
                    $("#dvMessageHdr").show();
                    $("#dvAttachmentHdr").show();
                }
                else {
                    $("#dvSubjectHdr").show();
                    $("#dvMessageHdr").show();
                    $("#dvAttachmentHdr").show();
                }
                

                var result = "";
                for (var i = 0; i < msg.Dashboard.listUPSITask[0].listAttachment.length; i++) {

                    result += '<p>';
                    result += '<a href="UPSI/' + msg.Dashboard.listUPSITask[0].listAttachment[i].Attachment + '" target="_blank">' + msg.Dashboard.listUPSITask[0].listAttachment[i].Attachment + '</a>';
                    result += '</p>';
                }

                $("#dvAttechmentlistMsg").html(result);

            }
            else {
                // alert(msg.Msg);

            }

        },
        error: function (response) {
            $("#Loader").hide();
            $("#txtUPSITaskID").val('');
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
function fnCloseUPSITask() {
    var taskid = $("#hdnTaskId").val();
    //alert("taskid=" + taskid);

    var ConnectedPersons = new Array();
    var sCPGrpId = $('#ddlCPGroup').val();
    var Remarks = $("textarea[id*='txtUpsiRemarks']").val();
    var flg = true;
    for (var i = 0; i < $("#tbdCPAdd").children().length; i++) {
        var CP = new Object();

        var sCPEmail = $($($($("#tbdCPAdd").children()[i]).children()[0]).children()[0]).val();
        var sCPNm = $($($($("#tbdCPAdd").children()[i]).children()[1]).children()[0]).val();
        var sCPIdentification = $($($($("#tbdCPAdd").children()[i]).children()[2]).children()[0]).val();
        var sCPIdentificationNo = $($($($("#tbdCPAdd").children()[i]).children()[3]).children()[0]).val();
        //var sCPGrpId = $($($($("#tbdCPAdd").children()[i]).children()[4]).children()[0]).val();
        var sCPType = $($($($("#tbdCPAdd").children()[i]).children()[4]).children()[0]).val();
        var sCPFirm = $($($($("#tbdCPAdd").children()[i]).children()[4]).children()[1]).val();


        //alert("sCPEmail=" + sCPEmail);
        //alert("sCPNm=" + sCPNm);
        //alert("sCPIdentification=" + sCPIdentification);
        //alert("sCPIdentificationNo=" + sCPIdentificationNo);
        //alert("sCPGrpId=" + sCPGrpId);
        //alert("sCPType=" + sCPType);
        //alert("sCPFirm=" + sCPFirm);
        if (sCPType == "Connected") {
            if (sCPEmail == undefined || sCPEmail == "" || sCPEmail == null) {
                flg = false;
            }
            if (sCPNm == undefined || sCPNm == "" || sCPNm == null) {
                flg = false;
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
            }
        }
        if (sCPGrpId == undefined || sCPGrpId == "" || sCPGrpId == null || sCPGrpId == 0) {
            flg = false;
        }
        if (flg == true) {
            CP.CPNm = sCPNm;
            CP.CPEmail = sCPEmail;
            CP.IdentificationTyp = sCPIdentification;
            CP.IdentificationId = sCPIdentificationNo;
            CP.CPStatus = sCPGrpId;
            CP.CPType = sCPType;
            CP.CPFirmNm = sCPFirm;
            ConnectedPersons.push(CP);
        }
    }
    if (flg == false) {
        alert('Please provide the required(*) information');
        return false;
    }
    if (ConnectedPersons.length > 0) {
        $("#Loader").show();
        var token = $("#TokenKey").val();
        var webUrl = uri + "/api/UPSIGroup/UpdateUPSITask";
        $.ajax({
            url: webUrl,
            type: "POST",
            headers: {
                'TokenKeyH': token,
            },
            data: JSON.stringify({
                GrpId: taskid, ConnectedPersons: ConnectedPersons, Remarks: Remarks
            }),
            async: false,
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            success: function (msg) {
                $("#Loader").hide();
                if (msg.StatusFl == true) {
                    alert("Task updated successfully !");
                    window.location.reload();
                    //fnCloseCPModal();
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
}
function fnUpdateUPSITask() {
    var taskid = $("#txtUPSITaskID").val();
    var groupid = $("#textUpsigroup").val();
    
    return 
    if (groupid == undefined || groupid == "0" || groupid == null) {
        $("#textUpsigroup").addClass('required-red');
        $('#lblUpsigroup').addClass('required');
        return false;
    }
    else {
        $("#textUpsigroup").removeClass('required-red');
        $('#lblUpsigroup').removeClass('required');

    }
    $("#stack1UPSITaskClosed").modal("show");
    var webUrl = uri + "/api/DashboardIT/UpdateUPSITaskById";
    $.ajax({
        url: webUrl,
        type: "POST",
        async: false,
        data: {
            GROUP_ID: groupid,
            TaskId: taskid,
            Status:'closed'

        },
       // contentType: "application/json; charset=utf-8",
        //datatype: "json",
        success: function (msg) {
          
            $("#Loader").hide();
            if (msg.Msg == "SessionExpired") {
                alert("Your session is expired. Please login again to continue");
                window.location.href = "../LogOut.aspx";
            }

            if (msg.StatusFl == true) {
                $("#stack1UPSITaskClosed").modal("hide");
                $("#stack1").modal("hide");

                GetAllUPSITask();
                alert("Task has been updated!");

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
function fnupsiNonCompliance()
{

    var taskid = $("#txtUPSITaskID").val();
    var groupid = '0';
   
    var webUrl = uri + "/api/DashboardIT/UpdateUPSITaskById";
    $.ajax({
        url: webUrl,
        type: "POST",
        async: false,
        data: {
            GROUP_ID: groupid,
            TaskId: taskid,
            Status: 'NonCompliance'

        },
        // contentType: "application/json; charset=utf-8",
        //datatype: "json",
        success: function (msg) {
        
            $("#Loader").hide();
            if (msg.Msg == "SessionExpired") {
                alert("Your session is expired. Please login again to continue");
                window.location.href = "../LogOut.aspx";
            }

            if (msg.StatusFl == true) {

                alert("Task has been updated!");
                $("#stack1UPSITaskClosed").modal("hide");
                $("#stack1").modal("hide");
                
                GetAllUPSITask();

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
function fnUPSIGroupChange() {


    $("#textUpsigroup").removeClass('required-red');
    $('#lblUpsigroup').removeClass('required');
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
function fnDiscardTask() {
    var taskid = $("#hdnTaskId").val();
    var Remarks = $("textarea[id*='txtUpsiRemarks']").val();
    //alert("taskid=" + taskid);
    $("#Loader").show();
    var token = $("#TokenKey").val();
    var webUrl = uri + "/api/UPSIGroup/DiscardUPSITask";
    $.ajax({
        url: webUrl,
        type: "POST",
        headers: {
            'TokenKeyH': token,
        },
        data: JSON.stringify({
            GrpId: taskid, Remarks: Remarks
        }),
        async: false,
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            $("#Loader").hide();
            if (msg.StatusFl == true) {
                alert("Task updated successfully !");
                window.location.reload();
                //fnCloseCPModal();
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
function fnBindAllUPSIGroup(GrpIdX) {
    //alert("In function fnBindAllUPSIGroup");
    //alert("GrpIdX=" + GrpIdX);
    $("#Loader").show();
    var webUrl = uri + "/api/UPSIGroup/GetAllUPSIGroups";
    $.ajax({
        url: webUrl,
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
                $("#ddlCPGroup").empty();
                for (var i = 0; i < msg.UPSIGroups.length; i++) {
                    $("#ddlCPGroup").append($("<option></option>").val(msg.UPSIGroups[i].GrpId).html(msg.UPSIGroups[i].GrpNm));
                }
                //alert("Here just before selecting");
                //alert("GrpIdX=" + GrpIdX);
                $("#ddlCPGroup").val(GrpIdX).change();
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
function fnAddUPSITaskCP() {
    var sHtml = '<tr>';
    sHtml += '<td style="margin:5px;">';
    sHtml += '<input id="txtNewFirmNm" class="form-control form-control-inline" placeholder="Enter Firm Name" type="text" autocomplete="off" />';
    sHtml += '</td>';
    sHtml += '<td style="margin:5px;">';
    sHtml += '<input id="txtNewCPNm" class="form-control form-control-inline" placeholder="Enter Connected Person" type="text" autocomplete="off" />';
    sHtml += '</td>';
    sHtml += '<td style="margin:5px;">';
    sHtml += '<input id="txtNewCPEmail" class="form-control form-control-inline" placeholder="Enter Connected Person" type="text" autocomplete="off" />';
    sHtml += '</td>';
    sHtml += '<td style="margin:5px;">';
    sHtml += '<select id="ddlNewCPIdentification" class="form-control">';
    sHtml += '<option value=""></option>';
    sHtml += '<option value="AADHAR CARD">AADHAR CARD</option>';
    sHtml += '<option value="DRIVING LICENSE">DRIVING LICENSE</option>';
    sHtml += '<option value="PAN">PAN</option>';
    sHtml += '<option value="PASSPORT">PASSPORT</option>';
    sHtml += '<option value="OTHER">OTHER</option>';
    sHtml += '</select>';
    sHtml += '</td>';
    sHtml += '<td style="margin:5px;">';
    sHtml += '<input id="txtNewCPIdentificationNo" class="form-control form-control-inline" placeholder="Enter Connected Person" type="text" autocomplete="off" />';
    sHtml += '</td>';
    sHtml += '<td style="margin:5px;">';
    sHtml += '<img onclick="javascript:fnAddNewCP();" src="images/icons/AddButton.png" height="24" width="24" />';
    sHtml += '</td>';
    sHtml += '</tr>';
    $("#tbdNewCPAdd").html(sHtml);
    $("#AddNewConnectedPerson").modal('show');
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
        if (flg == false) {
            alert("Please fill all required(*) field");
            return false;
        }
    }
    if (ConnectedPersons.length > 0) {
        //$("#alertMeassage").modal("show");
        var strHtml = $("#tbdNewCPAdd").html();
        alert(strHtml);



        var result = '';
        for (var i = 0; i < ConnectedPersons.length; i++) {
            result += '<tr>';

            result += '<td style="margin:5px;">';
            result += '<input value="' + ConnectedPersons[i].CPEmail + '" id="txtCPEmail" class="form-control form-control-inline" placeholder="Enter Connected Person" type="text" autocomplete="off" disabled />';
            result += '</td>';

            result += '<td style="margin:5px;">';
            if (ConnectedPersons[i].CPNm == "") {
                result += '<input value="' + ConnectedPersons[i].CPName + '" id="txtCPNm" class="form-control form-control-inline" placeholder="Enter Connected Person" type="text" autocomplete="off" />';
            }
            else {
                result += '<input disabled value="' + ConnectedPersons[i].CPName + '" id="txtCPNm" class="form-control form-control-inline" placeholder="Enter Connected Person" type="text" autocomplete="off" />';
            }
            result += '</td>';

            result += '<td style="margin:5px;">';
            if (ConnectedPersons[i].CPIdentificationTyp == "") {
                result += '<select id="ddlCPIdentification" class="form-control">';
            }
            else {
                result += '<select id="ddlCPIdentification" disabled class="form-control">';
            }
            result += '<option value=""></option>';
            if (ConnectedPersons[i].CPIdentificationTyp == "AADHAR CARD") {
                result += '<option value="AADHAR CARD" selected>AADHAR CARD</option>';
            }
            else {
                result += '<option value="AADHAR CARD">AADHAR CARD</option>';
            }
            if (ConnectedPersons[i].CPIdentificationTyp == "DRIVING LICENSE") {
                result += '<option value="DRIVING LICENSE" selected>DRIVING LICENSE</option>';
            }
            else {
                result += '<option value="DRIVING LICENSE">DRIVING LICENSE</option>';
            }
            if (ConnectedPersons[i].CPIdentificationTyp == "PAN") {
                result += '<option value="PAN" selected>PAN</option>';
            }
            else {
                result += '<option value="PAN">PAN</option>';
            }
            if (ConnectedPersons[i].CPIdentificationTyp == "PASSPORT") {
                result += '<option value="PASSPORT" selected>PASSPORT</option>';
            }
            else {
                result += '<option value="PASSPORT">PASSPORT</option>';
            }

            result += '</select>';
            result += '</td>';

            result += '<td style="margin:5px;">';
            if (ConnectedPersons[i].CPIdentificationNo == "") {
                result += '<input value="' + ConnectedPersons[i].CPIdentificationNo + '" id="txtCPIdentificationNo" class="form-control form-control-inline" placeholder="Enter Identification #" type="text" autocomplete="off" />';
            }
            else {
                result += '<input disabled value="' + ConnectedPersons[i].CPIdentificationNo + '" id="txtCPIdentificationNo" class="form-control form-control-inline" placeholder="Enter Identification #" type="text" autocomplete="off" />';
            }
            result += '</td>';

            result += '<td style="margin:5px;">';
            result += '<input value="' + ConnectedPersons[i].CPStatus + '" id="txtCPType" class="form-control form-control-inline" type="text" style="display:none;" />';
            result += '<input id="txtCPRemarks" class="form-control form-control-inline" type="text" value="' + ConnectedPersons[i].CPFirm + '" />';
            result += '</td>';
            result += '</tr>';
        }
        //fnBindAllUPSIGroup(msg.UPSIGroups[0].GrpId);
        $("#tbdCPAdd").append(result);

        //$("#tbdCPAdd").append(strHtml);
        $("#AddNewConnectedPerson").modal('hide');
    }
    else {
        alert("Please fill all required(*) field");
    }
}
function fnAddUPSITaskDP() {
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/GetDPUsers";
    $.ajax({
        type: "GET",
        url: webUrl,
        data: "{}",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            if (msg.StatusFl == true) {
                var result = "";
                result += '<option value="all">all</option>';
                for (var i = 0; i < msg.UserList.length; i++) {
                    result += '<option value="' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].USER_NM + ' (' + msg.UserList[i].LOGIN_ID + ')</option>';
                }
                $("#dduserslist").html(' ');
                $("#dduserslist").html(result);
                $("#Loader").hide();
                $("#AddNewDP").modal('show');
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