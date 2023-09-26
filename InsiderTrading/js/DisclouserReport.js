var Arrfield;
let appEditor;
var mentionFeed = "";
var arrEmail;
$(document).ready(function () {
    $("#Loader").hide();
    fnBindUserList();
    fnBindDisClouserTaskList();
    $('#checkAll').click(function () {
        var table = $('#tblDisclouserReportsetup').DataTable();
        var rows = table.rows({ 'search': 'applied' }).nodes();
        $('input[type="checkbox"]', rows).prop('checked', this.checked);
        //$('input:checkbox').prop('checked', this.checked);
        //$("input[name='checkname']").prop('checked', this.checked);
    });
    $("input[name='checkname']").each(function () {
        //$('input:checkbox').prop('checked', this.checked);
        $("#sendmail").prop("disabled", false);
    });
    Editor();
});
function fnSendMail() {
    var subject = $("#txtSubject").val();
    var template = appEditor.getData();
    if (subject == "") {
        alert("Subject Required");
        return false;
    }
    if (template == "") {
        alert("Template Required");
        return false;
    }
    //var fields = $("input[name='checkname']").serializeArray();
    //if (fields.length == 0) {
    //    alert('Please select atleast one user from report list');
    //    return false;
    //}
    arrEmail = new Array();
    arrEmail = [];
    //datatable has to be initialized to a variable
    var myTable = $('#tblDisclouserReportsetup').dataTable();

    //checkboxes should have a general class to traverse
    var rowcollection = myTable.$(".chk", { "page": "all" });

    //Now loop through all the selected checkboxes to perform desired actions
    rowcollection.each(function (index, elem) {
        if (elem.checked) {
            var checkbox_value = $(elem).val();
            var obj = new Object();
            obj.mailTo = checkbox_value;
            arrEmail.push(obj);
        }
    });

    //alert("Just before");
    $("#Loader").show();
    var webUrl = uri + "/api/ReportsIT/SendDisclousertaskEmail";
    $.ajax({
        type: 'POST',
        url: webUrl,
        data: JSON.stringify({
            subjectReport: subject, reportTemplate: template, emailReport: arrEmail
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        //async: false,
        success: function (msg) {
            if (isJson(msg)) {
                msg = JSON.parse(msg);
                //location.reload();
            }
            if (msg.StatusFl == false) {
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
            else {
                alert(msg.Msg);
                window.location.reload();
            }
            fnClearForm();
            $('#btnSave').attr("data-dismiss", "modal");
            $("#Loader").hide();
            return true;
        },
        error: function (error) {
            $("#Loader").hide();
            alert(error.status + ' ' + error.statusText);            
        }
    })
    //alert("Just after");
    //setInterval(fnCheckStatus, 10);
}
function initializeDataTable() {
    $('#tblDisclouserReportsetup').DataTable({
        dom: 'Bfrtip',
        pageLength: 10,
        //"scrollY": "300px",
        //"scrollX": true,
        //"aaSorting": [[0, "desc"]],
        buttons: [
            {
                extend: 'pdf',
                className: 'btn green btn-outline',
                exportOptions: {
                    columns: [ 1, 2, 3, 4, 5, 6]
                }
            },
            {
                extend: 'excel',
                className: 'btn yellow btn-outline ',
                exportOptions: {
                    columns: [1, 2, 3, 4, 5, 6]
                }
            },

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
function fnBindUserList() {
    //debugger;
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/GetUserList";
    $.ajax({
        type: "GET",
        url: webUrl,
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            if (msg.StatusFl == true) {
                var result = "";
                //result += '<option value="0">All</option>';
                result += '<option value="">Please Select</option>';
                result += '<option value="0">All</option>';
                for (var i = 0; i < msg.UserList.length; i++) {
                    if (msg.UserList[i].LOGIN_ID != 'superadmin') {
                        result += '<option value="' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].USER_NM + '(' + msg.UserList[i].USER_EMAIL + ')' + '</option>';
                    }
                }
                $("#bindUser").html(result);
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
function fnBindDisClouserTaskList() {
   // debugger;
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/GetDisclouserTaskList";
    $.ajax({
        type: "GET",
        url: webUrl,
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            if (msg.StatusFl == true) {
                var result = "";
                //result += '<option value="0">All</option>';
                result += '<option value="0">Please Select</option>';
                /*result += '<option value="0">All</option>';*/
                for (var i = 0; i < msg.UserList.length; i++) {
                    //if (msg.UserList[i].TaskFor == "Initial Disclosure Reminder") {
                    //    result += '<option value="' + msg.UserList[i].TaskFor + '">Initial</option>';
                    //}
                    //else {
                    //    result += '<option value="' + msg.UserList[i].TaskFor + '">' + msg.UserList[i].FinancialYear +'</option>';
                    //}
                    result += '<option value="' + msg.UserList[i].TaskId + '">' + msg.UserList[i].TaskFor + '</option>';
                }
                $("#bindDisclouser").html(result);
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
function fnGetDisclouserReport() {
    if (fnValidate()) {
        //$("#Loader").show();
        var webUrl = uri + "/api/ReportsIT/GetTaskDisclouserReport";
        $.ajax({
            type: "POST",
            url: webUrl,
            data: JSON.stringify({ TaskFor: $("#bindDisclouser").val(), USER_NM: $("#bindUser").val(), status: $("#ddStatus").val() }),
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            success: function (msg) {
                var result = "";
                if (msg.StatusFl) {
                    if (msg.lstDisclouserReport !== null) {
                        if (msg.lstDisclouserReport.length > 0) {
                            for (var i = 0; i < msg.lstDisclouserReport.length; i++) {

                                result += '<tr>';
                                result += '<td><input type="checkbox" class="chk" name="checkname" value="' + msg.lstDisclouserReport[i].USER_EMAIL + '" /></td>';
                                result += '<td style="display:none">' + msg.lstDisclouserReport[i].TaskId + '</td>';                            
                                result += '<td>' + msg.lstDisclouserReport[i].USER_NM + '</td>';
                                result += '<td>' + msg.lstDisclouserReport[i].panNumber + '</td>';
                                result += '<td>' + msg.lstDisclouserReport[i].DepartmentName + '</td>';
                                result += '<td>' + msg.lstDisclouserReport[i].DesignationName + '</td>';
                                result += '<td>' + msg.lstDisclouserReport[i].status + '</td>';
                                result += '<td>' + msg.lstDisclouserReport[i].formSubmittedOn + '</td>';
                                result += "<td><a target='_blank' href='/insidertrading/emailAttachment/" + msg.lstDisclouserReport[i].formName + "'>" + msg.lstDisclouserReport[i].formName + "</a></td>";
                                result += '</tr>';
                            }
                        }
                    }
                    var table = $('#tblDisclouserReportsetup').DataTable();
                    table.destroy();
                    $("#tbdDisclouserReportList").html(result);
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
}
function fnClearForm() {
    $('#txtSubject').val("");
      appEditor.setData("");
}
function fnValidate() {
    if ($("#bindDisclouser").val() == '' || $("#bindDisclouser").val() == undefined || $("#bindDisclouser").val() == null) {
        alert("Please select task");
        return false;
    }
    else if ($("#bindUser").val() == '' || $("#bindUser").val() == undefined || $("#bindUser").val() == null) {
        alert("Please select from User");
        return false;
    }
    else if ($("#ddStatus").val() == '' || $("#ddStatus").val() == undefined || $("#ddStatus").val() == null) {
        alert("Please select to Status");
        return false;
    }
    return true;
}
function Editor(template) {
    ClassicEditor
        .create(document.querySelector("textarea[id*='txtTemplate']"), {
            toolbar: {
                items: [
                    'heading',
                    '|',
                    'bold',
                    'italic',
                    'link',
                    'bulletedList',
                    'numberedList',
                    '|',
                    'outdent',
                    'indent',
                    '|',
                    'imageUpload',
                    'blockQuote',
                    'insertTable',
                    'mediaEmbed',
                    'undo',
                    'redo'
                ]
            },
            language: 'en',
            image: {
                toolbar: [
                    'imageTextAlternative',
                    'imageStyle:inline',
                    'imageStyle:block',
                    'imageStyle:side'
                ]
            },
            table: {
                contentToolbar: [
                    'tableColumn',
                    'tableRow',
                    'mergeTableCells',
                    'tableCellProperties',
                    'tableProperties'
                ]
            },
            mention: {
                feeds: [
                    {
                        marker: '@',
                        //feed: ['@[Name]', '@[FromDate]', '@[ToDate]', '@[Email]', '@[Status]'],
                        feed: Arrfield,
                        minimumCharacters: 0
                    }
                ]
                //feeds: [
                //    {
                //        marker: '@',
                //        feed: [ /* ... */],
                //        // Define the custom item renderer.
                //        itemRenderer: customItemRenderer
                //    }
                //]
            }
        }).then(editor => {
            window.editor = editor;
            appEditor = editor;
            if (template != null && template != undefined) {
                appEditor.setData(template);
            }
        })
        .catch(error => {
            console.error(error);
        });
}
function fnCloseModal() {
    fnClearForm()
}
function fnCheckStatus() {
    //alert("In check status");
    var webUrl = uri + "/api/ReportsIT/SendDisclouserTaskEmailStatus";
    $.ajax({
        type: 'POST',
        url: webUrl,
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == false) {
            }
            else {
                alert(msg.Msg);
                $("#percentage").html(msg.Msg);
            }
            fnClearForm();
            $('#btnSave').attr("data-dismiss", "modal");
            $("#LoaderProgerss").hide();
            return true;
        },
        error: function (error) {
            $("#Loader").hide();
            alert(error.status + ' ' + error.statusText);
        }
    })
}