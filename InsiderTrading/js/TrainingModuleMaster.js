$(document).ready(function () {
    fnGetAllTrainingModules();

    $('body').on('click', '.adddesignation', function () {
        var strTable = fnCreateBarCodeElementUi();
        $('#tbodyVideo').append(strTable);
    });
    $('body').on('click', '.removeadddesignation', function () {

        $(this).closest('tr').remove();
    });
})

function initializeDataTable(id, columns) {
    $('#' + id).DataTable({
        dom: 'Bfrtip',
        pageLength: 10,
        "scrollY": "300px",
        "scrollX": true,
        buttons: [
            {
                extend: 'pdf',
                className: 'btn green btn-outline',
                exportOptions: {
                    columns: columns
                }
            },
            {
                extend: 'excel',
                className: 'btn yellow btn-outline ',
                exportOptions: {
                    columns: columns
                }
            },
            //    { extend: 'colvis', className: 'btn dark btn-outline', text: 'Columns' }
        ]
    });
}

function isJson(str) {
    try {
        JSON.parse(str);
    } catch (e) {
        return false;
    }
    return true;
}

function fnGetAllTrainingModules() {
    $("#Loader").show();
    var webUrl = uri + "/api/Training/GetAllTrainingModulesMaster";
    $.ajax({
        type: "GET",
        url: webUrl,
        data: "{}",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            if (msg.StatusFl == true) {
                var result = "";
                if (msg.TrainingModuleList !== null) {
                    for (var i = 0; i < msg.TrainingModuleList.length; i++) {
                        result += '<tr id="tr_' + msg.TrainingModuleList[i].trainingId + '">';
                        result += '<td id="tdSequence_' + msg.TrainingModuleList[i].trainingId + '">' + (i + 1) + '</td>';
                        result += '<td id="tdTrainingName_' + msg.TrainingModuleList[i].trainingId + '">' + msg.TrainingModuleList[i].trainingName + '</td>';
                        result += '<td id="tdStartDate_' + msg.TrainingModuleList[i].trainingId + '">' + msg.TrainingModuleList[i].trainingStartDate + '</td>';
                        result += '<td id="tdEndDate_' + msg.TrainingModuleList[i].trainingId + '">' + msg.TrainingModuleList[i].trainingEndDate + '</td>';
                        result += '<td id="tdCreatedBy_' + msg.TrainingModuleList[i].trainingId + '">' + msg.TrainingModuleList[i].createdBy + '</td>';

                        if (msg.TrainingModuleList[i].trainingDocument !== null && msg.TrainingModuleList[i].trainingDocument !== undefined && msg.TrainingModuleList[i].trainingDocument !== "") {
                            result += '<td id="tdedit_' + msg.TrainingModuleList[i].trainingId + '"><a data-target="#trainingModel" data-toggle="modal" id="aEdit_' + msg.TrainingModuleList[i].trainingId + '" class="btn btn-success" onclick="javascript:fnEditTrainingModule(\'' + msg.TrainingModuleList[i].trainingId + '\');"><span class="icon-pencil"></span></a>';
                        }
                        else {
                            result += '<td id="tdedit_' + msg.TrainingModuleList[i].trainingId + '"><a data-target="#trainingModelVideo" data-toggle="modal" id="aEdit_' + msg.TrainingModuleList[i].trainingId + '" class="btn btn-success" onclick="javascript:fnEditTrainingModuleVideo(\'' + msg.TrainingModuleList[i].trainingId + '\');"><span class="icon-pencil"></span></a>';
                        }
                        
                        result += '&nbsp;<a id="aDelete_' + msg.TrainingModuleList[i].trainingId + '" class="btn btn-danger" onclick="javascript:fnDeleteTrainingModule(\'' + msg.TrainingModuleList[i].trainingId + '\');"><span class="icon-trash"></span></a>';


                        result += '</td>';
                        result += '</tr>';
                    }
                }
                var table = $('#tbl-training-setup').DataTable();
                table.destroy();
                $("#tbdTrainingList").html(result);
                initializeDataTable('tbl-training-setup', [1, 2, 3, 4]);
                $("#Loader").hide();
            }
            else {
                $("#Loader").hide();
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "Login.aspx";
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

function fnEditTrainingModule(trainingId) {
    $("#Loader").show();
    var webUrl = uri + "/api/Training/GetAllTrainingModulesById";
    $.ajax({
        type: "POST",
        url: webUrl,
        data: JSON.stringify({ trainingId: trainingId }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            if (msg.StatusFl == true) {
                if (msg.TrainingModule !== null) {
                    $("#txtTrainingId").val(msg.TrainingModule.trainingId);
                    $('#txtTrainingName').val(msg.TrainingModule.trainingName);
                    $('#txtTrainingStartDate').val(msg.TrainingModule.trainingStartDate);
                    $('#txtTrainingEndtDate').val(msg.TrainingModule.trainingEndDate);
                    $('#aUserAvatarImageUploaded').attr('href','emailAttachment/' + msg.TrainingModule.trainingDocument);
                }
                $("#Loader").hide();
            }
            else {
                $("#Loader").hide();
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "Login.aspx";
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

function fnDeleteTrainingModule(trainingId) {
    $("#Loader").show();
    var webUrl = uri + "/api/Training/DeleteTrainingModule";
    $.ajax({
        type: "POST",
        url: webUrl,
        data: JSON.stringify({ trainingId: trainingId }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            if (msg.StatusFl == true) {
                window.location.reload(true);
                $("#Loader").hide();
            }
            else {
                $("#Loader").hide();
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "Login.aspx";
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

function fnSaveTrainingModule() {
    if (fnValidateTrainingModal()) {
        var trainingId = $("#txtTrainingId").val().trim();
        var trainingName = $('#txtTrainingName').val().trim();
        var trainingStartDate = $('#txtTrainingStartDate').val().trim();
        var trainingEndDate = $('#txtTrainingEndtDate').val().trim();
        var trainingDocument = $("#fileUploadImage").val().trim();

        var obj = new Object();
        obj.trainingId = trainingId;
        obj.trainingName = trainingName;
        obj.trainingStartDate = trainingStartDate;
        obj.trainingEndDate = trainingEndDate;
        obj.trainingDocument = trainingDocument;

        var trainingModule = new FormData();

        trainingModule.append("Object", JSON.stringify(obj));

        trainingModule.append("Files", $("input[id*='fileUploadImage']").get(0).files[0]);


        $("#Loader").show();
        var webUrl = uri + "/api/Training/SaveTrainingModule";
        $.ajax({
            type: 'POST',
            url: webUrl,
            data: trainingModule,
            contentType: false,
            processData: false,
            success: function (msg) {
                if (msg.StatusFl == true) {
                    alert(msg.Msg);
                    window.location.reload(true);
                    $("#Loader").hide();
                }
                else {
                    $("#Loader").hide();
                    if (msg.Msg == "SessionExpired") {
                        alert("Your session is expired. Please login again to continue");
                        window.location.href = "Login.aspx";
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
}

function fnValidateTrainingModal() {
    var trainingId = $("#txtTrainingId").val().trim();
    var trainingName = $('#txtTrainingName').val().trim();
    var trainingStartDate = $('#txtTrainingStartDate').val().trim();
    var trainingEndDate = $('#txtTrainingEndtDate').val().trim();
    var itemFile = $("#fileUploadImage").get(0).files;
    var arrayExtensions = ["pdf", "docx", "doc", "pptx"];

    var count = 0;

    if (trainingName == undefined || trainingName == null || trainingName == '') {
        count++;
        $('#lblTrainingName').addClass('lblrequired');
        $("#txtTrainingName").addClass('requiredBackground');
    }
    else {
        $('#lblTrainingName').removeClass('lblrequired');
        $("#txtTrainingName").removeClass('requiredBackground');
    }

    if (trainingStartDate == undefined || trainingStartDate == null || trainingStartDate == '') {
        count++;
        $('#lblTrainingStartDate').addClass('lblrequired');
        $("#txtTrainingStartDate").addClass('requiredBackground');
    }
    else {
        $('#lblTrainingStartDate').removeClass('lblrequired');
        $("#txtTrainingStartDate").removeClass('requiredBackground');
    }

    if (trainingEndDate == undefined || trainingEndDate == null || trainingEndDate == '') {
        count++;
        $('#lblTrainingEndDate').addClass('lblrequired');
        $("#txtTrainingEndtDate").addClass('requiredBackground');
    }
    else {
        if (new Date(convertToDateTime(trainingEndDate)) < new Date(convertToDateTime(trainingStartDate))) {
            count++;
            $('#lblTrainingEndDate').addClass('lblrequired');
            $("#txtTrainingEndtDate").addClass('requiredBackground');
            alert("Training End Date Should Be Greater Than Training Start Date");
        }
        else {
            $('#lblTrainingEndDate').removeClass('lblrequired');
            $("#txtTrainingEndtDate").removeClass('requiredBackground');
        }
        
    }

    

    if ($('#fileUploadImage').val() == undefined || $('#fileUploadImage').val() == null || $('#fileUploadImage').val().trim() == '') {
        count++;
        $("#lblUpload").addClass('lblrequired');
        $('#fileUploadImage').addClass('requiredBackground');
    }
    else if ($.inArray(itemFile[0].name.split('.').pop().toLowerCase(), arrayExtensions) == -1) {
        count++;
        alert("Only PDF, DOCX, DOC, PPTX format are allowed in Training Document.");
    }
    else {
        $("#lblUpload").removeClass('lblrequired');
        $('#fileUploadImage').removeClass('requiredBackground');
    }

    if (count > 0) {
        return false;
    }
    else {
        return true;
    }
}

function fnClearValidateTrainingModal() {
    $('#lblTrainingName').removeClass('required');
    $("#txtTrainingName").removeClass('requiredBackground');

    $('#lblTrainingStartDate').removeClass('required');
    $("#txtTrainingStartDate").removeClass('requiredBackground');

    $('#lblTrainingEndDate').removeClass('required');
    $("#txtTrainingEndtDate").removeClass('requiredBackground');

    $("#lblUpload").removeClass('required');
    $('#fileUploadImage').removeClass('requiredBackground');

    $("#txtTrainingId").val('0');
    $('#txtTrainingName').val('');
    $('#txtTrainingStartDate').val('');
    $('#txtTrainingEndtDate').val('');
    $('#aUserAvatarImageUploaded').attr('href', "#");
}

function fnRemoveClass(obj, val1, val2) {
    $("#lbl" + val1).removeClass('required');
    $("#" + val2).removeClass('requiredBackground');
}

function fnSelectedUploadType() {
    if ($("#ddlUploadType").val() == "File") {
        $("#ddlUploadType").val('');
        $("#stack1").modal('hide');
        $("#trainingModel").modal('show');
    }
    else if ($("#ddlUploadType").val() == "Video") {
        $("#ddlUploadType").val('');
        $("#stack1").modal('hide');
        $("#trainingModelVideo").modal('show');
        OpenNew();
    }
    else {
        // do nothing
    }
}

function OpenNew() {
    var strTable = fnCreateBarCodeElementUi();
    $('#tbodyVideo').append(strTable);
}

function fnCreateBarCodeElementUi() {
    var rows = $('#addvideo tbody tr').length;
    var strTable = '<tr id="row_' + rows + '">';
    strTable += '<td>';
    strTable += '<input id="fileUploadImageVideo' + rows + '" type="file" name="..." />';
    strTable += '</td>';
    strTable += '<td>';
    strTable += '<input id="txtAddVideoSequence' + rows + '" type="number" class = "form-control" autocomplete="off"/>';
    strTable += '</td>';
    strTable += '<td><button type="button" class="btn blue btn-outline adddesignation">Add</button>';
    if (rows != '0') {
        strTable += '<td><button type="button" class="btn red btn-outline removeadddesignation">Remove</button></td>';
    }
    strTable += '</tr>';
    return strTable;
}

function fnClearValidateItemModalVideo() {
    $('#lblTrainingNameVideo').removeClass('lblrequired');
    $("#txtTrainingNameVideo").removeClass('requiredBackground');

    $('#lblTrainingStartDateVideo').removeClass('lblrequired');
    $("#txtTrainingStartDateVideo").removeClass('requiredBackground');

    $('#lblTrainingEndDateVideo').removeClass('lblrequired');
    $("#txtTrainingEndtDateVideo").removeClass('requiredBackground');

    $("#txtTrainingIdVideo").val('0');
    $('#txtTrainingNameVideo').val('');
    $('#txtTrainingStartDateVideo').val('');
    $('#txtTrainingEndtDateVideo').val('');
    $('#tbodyVideo').html('');
}

function fnSaveTrainingModuleVideo() {
    if (fnValidateTrainingVideoModal()) {
        var trainingId = $("#txtTrainingIdVideo").val().trim();
        var trainingName = $('#txtTrainingNameVideo').val().trim();
        var trainingStartDate = $('#txtTrainingStartDateVideo').val().trim();
        var trainingEndDate = $('#txtTrainingEndtDateVideo').val().trim();

        var obj = new Object();
        obj.trainingId = trainingId;
        obj.trainingName = trainingName;
        obj.trainingStartDate = trainingStartDate;
        obj.trainingEndDate = trainingEndDate;

        var lstTrainingDocument = new Array();
        var trainingModule = new FormData();

        $("#tbodyVideo > tr").each(function () {
            var trainingFile = $(this).find("td").eq(0).find("input[id*='fileUploadImageVideo']").get(0).files[0].name;
            var sequence = $(this).find("td").eq(1).find("input[id*='txtAddVideoSequence']").val();
            var objTrainingAudioVideo = new Object();
            objTrainingAudioVideo.fileName = trainingFile;
            objTrainingAudioVideo.sequence = sequence;
            lstTrainingDocument.push(objTrainingAudioVideo);
            trainingModule.append(trainingFile, $(this).find("td").eq(0).find("input[id*='fileUploadImageVideo']").get(0).files[0]);
        });
        obj.lstTrainingAudioVideo = lstTrainingDocument;



        trainingModule.append("Object", JSON.stringify(obj));


        $("#Loader").show();
        var webUrl = uri + "/api/Training/SaveTrainingModule";
        $.ajax({
            type: 'POST',
            url: webUrl,
            data: trainingModule,
            contentType: false,
            processData: false,
            success: function (msg) {
                if (msg.StatusFl == true) {
                    alert(msg.Msg);
                    window.location.reload(true);
                    $("#Loader").hide();
                }
                else {
                    $("#Loader").hide();
                    if (msg.Msg == "SessionExpired") {
                        alert("Your session is expired. Please login again to continue");
                        window.location.href = "Login.aspx";
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
}

function fnValidateTrainingVideoModal() {
    var trainingId = $("#txtTrainingIdVideo").val().trim();
    var trainingName = $('#txtTrainingNameVideo').val().trim();
    var trainingStartDate = $('#txtTrainingStartDateVideo').val().trim();
    var trainingEndDate = $('#txtTrainingEndtDateVideo').val().trim();

    var count = 0;

    if (trainingName == undefined || trainingName == null || trainingName == '') {
        count++;
        $('#lblTrainingNameVideo').addClass('lblrequired');
        $("#txtTrainingNameVideo").addClass('requiredBackground');
    }
    else {
        $('#lblTrainingNameVideo').removeClass('lblrequired');
        $("#txtTrainingNameVideo").removeClass('requiredBackground');
    }

    if (trainingStartDate == undefined || trainingStartDate == null || trainingStartDate == '') {
        count++;
        $('#lblTrainingStartDateVideo').addClass('lblrequired');
        $("#txtTrainingStartDateVideo").addClass('requiredBackground');
    }
    else {
        $('#lblTrainingStartDateVideo').removeClass('lblrequired');
        $("#txtTrainingStartDateVideo").removeClass('requiredBackground');
    }

    if (trainingEndDate == undefined || trainingEndDate == null || trainingEndDate == '') {
        count++;
        $('#lblTrainingEndDateVideo').addClass('lblrequired');
        $("#txtTrainingEndtDateVideo").addClass('requiredBackground');
    }
    else {
        if (new Date(convertToDateTime(trainingEndDate)) < new Date(convertToDateTime(trainingStartDate))) {
            count++;
            $('#lblTrainingEndDateVideo').addClass('lblrequired');
            $("#txtTrainingEndtDateVideo").addClass('requiredBackground');
            alert("Training End Date Should Be Greater Than Training Start Date");
        }
        else {
            $('#lblTrainingEndDateVideo').removeClass('lblrequired');
            $("#txtTrainingEndtDateVideo").removeClass('requiredBackground');
        }
    }

    

    if (count > 0) {
        return false;
    }
    else {
        return true;
    }
}

function fnEditTrainingModuleVideo(trainingId) {
    $("#Loader").show();
    var webUrl = uri + "/api/Training/GetAllTrainingModulesById";
    $.ajax({
        type: "POST",
        url: webUrl,
        data: JSON.stringify({ trainingId: trainingId }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            if (msg.StatusFl == true) {
                if (msg.TrainingModule !== null) {
                    $("#txtTrainingIdVideo").val(msg.TrainingModule.trainingId);
                    $('#txtTrainingNameVideo').val(msg.TrainingModule.trainingName);
                    $('#txtTrainingStartDateVideo').val(msg.TrainingModule.trainingStartDate);
                    $('#txtTrainingEndtDateVideo').val(msg.TrainingModule.trainingEndDate);
                    if (msg.TrainingModule.lstItemBarCode !== null) {
                        if (msg.TrainingModule.lstTrainingAudioVideo.length > 0) {
                            for (var i = 0; i < msg.TrainingModule.lstTrainingAudioVideo.length; i++) {
                                var rows = $('#addvideo tbody tr').length;
                                var strTable = '<tr id="row_' + rows + '">';
                                strTable += '<td>';
                                strTable += '<input id="fileUploadImageVideo' + rows + '" type="file" name="..." />';
                                strTable += '</td>';
                                strTable += '<td>';
                                strTable += '<input id="txtAddVideoSequence' + rows + '" type="number" class = "form-control" autocomplete="off" value="' + msg.TrainingModule.lstTrainingAudioVideo[i].sequence +'"/>';
                                strTable += '</td>';
                                strTable += '<td><button type="button" class="btn blue btn-outline adddesignation">Add</button>';
                                if (rows != '0') {
                                    strTable += '<td><button type="button" class="btn red btn-outline removeadddesignation">Remove</button></td>';
                                }
                                strTable += '</tr>';
                                $('#tbodyVideo').append(strTable);
                            }

                        }
                        else {
                            var strTable = fnCreateBarCodeElementUi();
                            $('#tbodyVideo').append(strTable);
                        }
                    }
                }
                $("#Loader").hide();
            }
            else {
                $("#Loader").hide();
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "Login.aspx";
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
function convertToDateTime(date) {
    return date.split("/")[1] + "/" + date.split("/")[0] + "/" + date.split("/")[2];
}
