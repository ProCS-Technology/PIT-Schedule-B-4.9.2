var Arrfield;
let appEditor;
var mentionFeed = "";
var arrEmail;
var downloadComplete = false;
var intervalListener;
$(document).ready(function () {
    
    $('#txtFromDate').datepicker({
        todayHighlight: true,
        autoclose: true,
        format: $("input[id*=hdnJSDateFormat]").val(),
        clearBtn: true
    });
    $('#txtToDate').datepicker({
        todayHighlight: true,
        autoclose: true,
        format: $("input[id*=hdnJSDateFormat]").val(),
        clearBtn: true
    });
    $("#Loader").hide();
    //fnBindUserList();
    //fnBindDisClouserTaskList();
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

    var start = $("input[id*=hdnEmailTask]").val();
    if (start == "Start") {
        $("#LoaderProgerss").show();
        intervalListener = window.setInterval(function () {
            if (!downloadComplete) {
                CallCheckEmailStatus();
            }
        }, 2000);
    }
});
function fnSendMail() {
    //debugger;
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
    //rowcollection.each(function (index, elem) {
    //    if (elem.checked) {
    //        var checkbox_value = $(elem).val();
    //        var obj = new Object();
    //        obj.mailTo = checkbox_value;
    //        arrEmail.push(obj);
    //    }
    //});

    rowcollection.each(function (index, elem) {
        if (elem.checked) {
            var checkbox_value = $(elem).val();
            //var obj = new Object();
            //obj.mailTo = checkbox_value;
            arrEmail.push(checkbox_value);
        }
    });

    $("input[id*=hdnMailTo]").val(arrEmail);
    $("input[id*=hdnEmailSubject]").val(subject);
    $("input[id*=hdnEmailTemplate]").val(template);
    //alert("Just before");
    //$("#Loader").show();
    //var webUrl = uri + "/api/ReportsIT/SendDisclousertaskEmail";
    //$.ajax({
    //    type: 'POST',
    //    url: webUrl,
    //    data: JSON.stringify({
    //        subjectReport: subject, reportTemplate: template, emailReport: arrEmail
    //    }),
    //    contentType: "application/json; charset=utf-8",
    //    datatype: "json",
    //    //async: false,
    //    success: function (msg) {
    //        if (isJson(msg)) {
    //            msg = JSON.parse(msg);
    //            //location.reload();
    //        }
    //        if (msg.StatusFl == false) {
    //            $("#Loader").hide();
    //            if (msg.Msg == "SessionExpired") {
    //                alert("Your session is expired. Please login again to continue");
    //                window.location.href = "../LogOut.aspx";
    //            }
    //            else {
    //                alert(msg.Msg);
    //                return false;
    //            }
    //        }
    //        else {
    //            alert(msg.Msg);
    //            window.location.reload();
    //        }
    //        fnClearForm();
    //        $('#btnSave').attr("data-dismiss", "modal");
    //        $("#Loader").hide();
    //        return true;
    //    },
    //    error: function (error) {
    //        $("#Loader").hide();
    //        alert(error.status + ' ' + error.statusText);
    //    }
    //})
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
                    columns: [1, 2, 3, 4, 5,6]
                }
            },
            {
                extend: 'excel',
                className: 'btn yellow btn-outline ',
                exportOptions: {
                    columns: [1, 2, 3, 4, 5,6]
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
    ////debugger;
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
//function fnBindDisClouserTaskList() {
//    // //debugger;
//    $("#Loader").show();
//    var webUrl = uri + "/api/UserIT/GetDisclouserTaskList";
//    $.ajax({
//        type: "GET",
//        url: webUrl,
//        data: {},
//        contentType: "application/json; charset=utf-8",
//        datatype: "json",
//        async: false,
//        success: function (msg) {
//            if (msg.StatusFl == true) {
//                var result = "";
//                //result += '<option value="0">All</option>';
//                result += '<option value="0">Please Select</option>';
//                /*result += '<option value="0">All</option>';*/
//                for (var i = 0; i < msg.UserList.length; i++) {
//                    //if (msg.UserList[i].TaskFor == "Initial Disclosure Reminder") {
//                    //    result += '<option value="' + msg.UserList[i].TaskFor + '">Initial</option>';
//                    //}
//                    //else {
//                    //    result += '<option value="' + msg.UserList[i].TaskFor + '">' + msg.UserList[i].FinancialYear +'</option>';
//                    //}
//                    result += '<option value="' + msg.UserList[i].TaskId + '">' + msg.UserList[i].TaskFor + '</option>';
//                }
//                $("#bindDisclouser").html(result);
//                $("#Loader").hide();
//            }
//            else {
//                $("#Loader").hide();
//                if (msg.Msg == "SessionExpired") {
//                    alert("Your session is expired. Please login again to continue");
//                    window.location.href = "../LogOut.aspx";
//                }
//                else {
//                    alert(msg.Msg);
//                    return false;
//                }
//            }
//        },
//        error: function (response) {
//            $("#Loader").hide();
//            alert(response.status + ' ' + response.statusText);
//        }
//    });

//}
function fnGetDisclouserReport() {
    if (fnValidate()) {
        
        //$("#Loader").show();
        var webUrl = uri + "/api/ReportsIT/GetPendingTaskReport";
        $.ajax({
            type: "POST",
            url: webUrl,
            data: JSON.stringify({ mailFrom: $("#txtFromDate").val(), mailTo: $("#txtToDate").val() }),
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            success: function (msg) {
                //debugger;
                var result = "";
                if (msg.StatusFl) {
                    if (msg.lstPendingTaskReport !== null) {
                        if (msg.lstPendingTaskReport.length > 0) {
                            for (var i = 0; i < msg.lstPendingTaskReport.length; i++) {

                                result += '<tr>';
                                result += '<td><input type="checkbox" class="chk" name="checkname" value="' + msg.lstPendingTaskReport[i].UserEmail + '" /></td>';
                                //result += '<td style="display:none">' + msg.lstPendingTaskReport[i].TaskId + '</td>';
                                result += '<td>' + msg.lstPendingTaskReport[i].TaskFor + '</td>';
                                result += '<td>' + msg.lstPendingTaskReport[i].mailFrom + '</td>';
                                result += '<td>' + msg.lstPendingTaskReport[i].mailTo + '</td>';
                                result += '<td>' + msg.lstPendingTaskReport[i].EmailDate + '</td>';
                                result += '<td>' + msg.lstPendingTaskReport[i].subject + '</td>';
                                result += '<td>' + msg.lstPendingTaskReport[i].CreatedOn + '</td>';
                                //result += "<td><a target='_blank' href='/insidertrading/emailAttachment/" + msg.lstDisclouserReport[i].formName + "'>" + msg.lstDisclouserReport[i].formName + "</a></td>";
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
    
    if ($("#txtFromDate").val() == '' || $("#txtFromDate").val() == undefined || $("#txtFromDate").val() == null) {
        alert("Please select from date");
        return false;
    }
    else if ($("#txtToDate").val() == '' || $("#txtToDate").val() == undefined || $("#txtToDate").val() == null) {
        alert("Please select to date");
        return false;
    }
    else {
        var FromDate = new Date(convertToDateTime($("#txtFromDate").val()));
        var Todate = new Date(convertToDateTime($("#txtToDate").val()));

        if (Todate < FromDate) {

            alert("To Date Should be greater than From Date");
            return false;
        }
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
function convertToDateTime(date) {
    return date.split("/")[1] + "/" + date.split("/")[0] + "/" + date.split("/")[2];
}

function fnChkStatus() {
    var start = $("input[id*=hdnEmailTask]").val();
    if (start == "Start") {
        $("#LoaderProgerss").show();
        intervalListener = window.setInterval(function () {
            if (!downloadComplete) {
                CallCheckEmailStatus();
            }
        }, 2000);
    }
}
function CallCheckEmailStatus() {
    $.ajax({
        type: "POST",
        url: "PendingTaskReport.aspx/CheckDownload",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            debugger;
            updateStatus('completed', r.d);
            if (r.d.indexOf('All emails sent') > -1) {
                downloadComplete = true;
            }
        },
        error: function (r) {
            console.log('Check error : ' + r);
        },
        failure: function (r) {
            console.log('Check failure : ' + r);
        }
    });
    if (downloadComplete) {
        window.clearInterval(intervalListener);
        $("input[id*=hdnEmailTask]").val('');
        $("#LoaderProgerss").hide();
    }
}
function updateStatus(status, msg) {
    debugger;
    document.getElementById('lblMsg').innerHTML = msg;
    if (msg.indexOf('All emails sent') > -1) {
        downloadComplete = true;
        window.clearInterval(intervalListener);
        $("input[id*=hdnEmailTask]").val('');
        $("#LoaderProgerss").hide();
        alert("Trading window notification sent successfully");
    }
}