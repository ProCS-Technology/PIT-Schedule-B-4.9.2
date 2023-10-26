var currentSelectedForm = "";
$(document).ready(function () {
    $("#Loader").hide();
    $("#selFormSelection").on('change', function () {
        currentSelectedForm = $(this).val();
    })
    $("#btDownloadForm").on('click', function (e) {
        if (currentSelectedForm == "" || currentSelectedForm == "0") {
            e.preventDefault();
        }
    })
    fnGetForms();
    fnGetUserUploadedForms();
})

function initializeDataTable() {
    $('#tbl-UploadedForm-setup').DataTable({
        dom: 'Bfrtip',
        pageLength: 10,
        "scrollY": "300px",
    //    "scrollX": true,
        buttons: [
         {
             extend: 'pdf',
             className: 'btn green btn-outline',
             exportOptions: {
                 columns: [0, 1, 2]
             }
         },
         {
             extend: 'excel',
             className: 'btn yellow btn-outline ',
             exportOptions: {
                 columns: [0, 1, 2]
             }
         },
        ]
    });
}

function fnDownloadForm() {
    if (currentSelectedForm !== "" && currentSelectedForm !== "0") {
        $("#btDownloadForm").attr('href', 'Forms/' + currentSelectedForm + '.docx');
    }
}

function fnSubmitForm() {
    var UploadAvatar = $('#fileUploadImage').val().split('.').pop().toLowerCase();
    if ($.inArray(UploadAvatar, ['pdf']) != -1) {
        var userData = new FormData();
        if ($("input[id*='fileUploadImage']").get(0).files.length > 0) {
            userData.append("Files", $("input[id*='fileUploadImage']").get(0).files[0]);
        }
        $("#Loader").show();
        var webUrl = uri + "/api/UserIT/SaveForm";
        $.ajax({
            type: 'POST',
            url: webUrl,
            data: userData,
            contentType: false,
            //  async: false,
            processData: false,
            success: function (msg) {
                if (msg.StatusFl == false) {
                    $("#Loader").hide();
                    if (msg.Msg == "SessionExpired") {
                        alert("Your session is expired. Please login again to continue");
                        window.location.href = "../LogOut.aspx";
                    }
                    else {
                        alert(msg.Msg);
                        $('#btnSave').removeAttr("data-dismiss");
                        return false;
                    }
                }
                else {
                    $("#Loader").hide();
                    alert(msg.Msg);
                    window.location.reload();
                }
                $("#userModel").modal('hide');
            },
            error: function (error) {
                $("#Loader").hide();
                alert(error.status + ' ' + error.statusText);
                $('#btnSave').removeAttr("data-dismiss");
            }
        })
    }
    else {
        alert("Please upload proper pdf format");
    }
}

function fnGetForms() {
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/GetCompanyForms";
    $.ajax({
        type: "GET",
        url: webUrl,
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            if (msg.StatusFl == true) {
                var result = "";
                result += '<option value="0"></option>';
                if (msg.UserList !== null) {
                    for (var i = 0; i < msg.UserList.length; i++) {
                        result += '<option value = "' + msg.UserList[i].formName + '">' + msg.UserList[i].formName + '</option>';
                    }
                    $("#selFormSelection").html(result);
                }

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

function fnGetUserUploadedForms() {
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/GetUserUploadedForms";
    $.ajax({
        type: "GET",
        url: webUrl,
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
      //  async: true,
        success: function (msg) {
            if (msg.StatusFl == true) {
                var result = "";
                if (msg.UserList !== null) {
                    for (var i = 0; i < msg.UserList.length; i++) {
                        result += '<tr>';
                        result += '<td>' + msg.UserList[i].USER_NM + '</td>';
                        result += '<td>' + msg.UserList[i].USER_EMAIL + '</td>';
                      //  result += '<td>' + msg.UserList[i].uploadAvatar + '</td>';
                        result += '<td>' + msg.UserList[i].formSubmittedOn + '</td>';
                        result += '<td><a class="fa fa-download" target="_blank" href= "Forms/UploadedFormByUser/' + msg.UserList[i].uploadAvatar + '">' + msg.UserList[i].uploadAvatar + '</a></td>';
                        result += '</tr>';
                    }
                    var table = $('#tbl-UploadedForm-setup').DataTable();
                    table.destroy();
                    $("#tbdUploadedFormList").html(result);
                }
                
                initializeDataTable();
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