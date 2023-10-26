jQuery(document).ready(function () {
    var table = $('#tbl-upsilist-setup').DataTable();
    table.destroy();
    initializeDataTable();
});
function fnClearForm() {
    //alert("In function fnClearForm");
    $("#tbdCPAdd").html('');

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
        '</td>';
    str += '</tr>';
    $("#tbdCPAdd").append(str);
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
    var ConnectedPersons = new Array();
    for (var i = 0; i < $("#tbdCPAdd").children().length; i++) {
        var CP = new Object();

        var sCPFirmNm = $($($($("#tbdCPAdd").children()[i]).children()[0]).children()[0]).val();
        var sCPNm = $($($($("#tbdCPAdd").children()[i]).children()[1]).children()[0]).val();
        var sCPEmail = $($($($("#tbdCPAdd").children()[i]).children()[2]).children()[0]).val();
        var sCPIdentification = $($($($("#tbdCPAdd").children()[i]).children()[3]).children()[0]).val();
        var sCPIdentificationNo = $($($($("#tbdCPAdd").children()[i]).children()[4]).children()[0]).val();
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
                    alert("Connected Persons details updated successfully !");
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
        })
    }
    else {
        alert("Please fill all required(*) field");
    }
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
function validateEmail(value) {
    var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
    if (reg.test(value) == false) {
        return false;
    }
    return true;
}
function fnDownloadTemplate() {
    window.location = 'ConnectedPerson/ConnectedPersonTemplate.xlsx';
}
function fnCPUpload() {
    if ($('#fuCPUploadFile').val() == "" || $('#fuCPUploadFile').val() == null) {
        alert("Please select file to upload");
        return false;
    }
    var ctrl = $('#fuCPUploadFile');
    var file = $('#fuCPUploadFile').val();
    var ext = file.split(".");
    ext = ext[ext.length - 1].toLowerCase();
    var arExtns = ['xls', 'xlsx'];
    if (arExtns.lastIndexOf(ext) == -1) {
        alert("Please select a file with  extension(s).\n" + arExtns.join(', '));
        ctrl.value = '';
        return false;
    }
    else {
        fnUploadCP();
    }
}
function fnUploadCP() {
    var param1 = new Date();
    var param2 = param1.getDate() + '_' + (param1.getMonth() + 1) + '_' + param1.getFullYear() + '_' + param1.getHours() + '_' + param1.getMinutes() + '_' + param1.getSeconds();
    var fileUpload = $("#fuCPUploadFile").get(0);
    var files = fileUpload.files;
    var test = new FormData();
    for (var i = 0; i < files.length; i++) {
        test.append(files[i].name, files[i]);
    }
    var extn = $('#fuCPUploadFile').val().split(".");
    extn = extn[extn.length - 1].toLowerCase();
    var sSaveAs = 'Upload_' + param2 + '_File.' + extn;
    test.append('sSaveAs', sSaveAs);

    var webUrl = uri + "/api/ConnectedPerson/UploadCP";
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
            $("#Loader").hide();
            if (msg.StatusFl == false) {
                if (msg.Msg == "Success") {
                    $('#fuCPUploadFile').val("");
                    return false;
                }
                else {
                    alert(msg.Msg);
                    $('#fuCPUploadFile').val("");
                    $('#btnSaveUpload').removeAttr("data-dismiss");
                    return false;
                }
            }
            else {
                alert("Connected Persons uploaded successfully !");
                window.location.reload();
                $('#btnUploadTemplate').removeAttr("data-target");
                $('#btnSaveUpload').attr("data-dismiss", "modal");
                return true;
            }
        },
        error: function (response) {
            //$.unblockUI();
            $("#Loader").hide();
            alert(response.status + ' ' + response.statusText);
            $('#fuCPUploadFile').val("");
        }
    });
}
function initializeDataTable() {
    $('#tbl-upsilist-setup').DataTable({
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'pdf',
                className: 'btn green btn-outline',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4]
                }
            },
            {
                extend: 'excel',
                className: 'btn yellow btn-outline ',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4]
                }
            }
        ]
    });
}
function fnEdit(CPFirm, CPName, CPEmail, CPIdentificationTyp, CPIdentificationNo, CPStatus) {
    //alert("In function fnEdit");
    //alert("CPFirm=" + CPFirm);
    //alert("CPName=" + CPName);
    //alert("CPEmail=" + CPEmail);
    //alert("CPIdentificationTyp=" + CPIdentificationTyp);
    //alert("CPIdentificationNo=" + CPIdentificationNo);
    //alert("CPStatus=" + CPStatus);
    $("#GrpEditCP").modal('show');

    $("#txtCPFirm").val(CPFirm);
    $("#txtCPName").val(CPName);
    $("#txtEditCPEmail").val(CPEmail);
    $("#ddlIdentificationTyp").val(CPIdentificationTyp);
    $("#txtIdentificationNo").val(CPIdentificationNo);
    $("#ddlStatus").val(CPStatus).change();
}
function fnUpdateCP() {
    //alert("In function fnUpdateCP");
    var sCPFirm = $("#txtCPFirm").val();
    var sCPName = $("#txtCPName").val();
    var sCPEmail = $("#txtEditCPEmail").val();
    var sCPIdentificationTyp = $("#ddlIdentificationTyp").val();
    var sCPIdentificationNo = $("#txtIdentificationNo").val();
    var sCPStatus = $("#ddlStatus").val();

    //alert("sCPFirm=" + sCPFirm);
    //alert("sCPName=" + sCPName);
    //alert("sCPEmail=" + sCPEmail);
    //alert("sCPIdentificationTyp=" + sCPIdentificationTyp);
    //alert("sCPIdentificationNo=" + sCPIdentificationNo);
    //alert("sCPStatus=" + sCPStatus);

    if (sCPFirm == "" || sCPFirm == null) {
        alert("Please enter Firm name");
        return false;
    }
    if (sCPName == "" || sCPName == null) {
        alert("Please enter Connected Person name");
        return false;
    }
    if (sCPEmail == "" || sCPEmail == null) {
        alert("Please enter Connected Person email");
        return false;
    }
    if (sCPIdentificationTyp == "" || sCPIdentificationTyp == null) {
        alert("Please select Identification type");
        return false;
    }
    if (sCPIdentificationNo == "" || sCPIdentificationNo == null) {
        alert("Please enter Identification No");
        return false;
    }
    if (sCPStatus == "" || sCPStatus == null) {
        alert("Please select status");
        return false;
    }

    var ConnectedPersons = new Array();
    var CP = new Object();
    CP.CPFirm = sCPFirm;
    CP.CPName = sCPName;
    CP.CPEmail = sCPEmail;
    CP.CPIdentificationTyp = sCPIdentificationTyp;
    CP.CPIdentificationNo = sCPIdentificationNo;
    CP.CPStatus = sCPStatus;
    ConnectedPersons.push(CP);

    if (ConnectedPersons.length > 0) {
        $("#Loader").show();
        var token = $("#TokenKey").val();
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
                    alert("Connected Persons details updated successfully !");
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
        })
    }
    else {
        alert("Please fill all required(*) field");
    }
}