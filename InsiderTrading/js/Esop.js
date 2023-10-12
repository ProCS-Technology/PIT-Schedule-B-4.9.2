$(document).ready(function () {
    window.history.forward();
    function preventBack() { window.history.forward(1); }
    $("#Loader").hide();

    getAllRestrictedCompanies();
    fnGetAllEsopHdr();
    fnGetThresholdByTimeSettings();
    fnBindCorporateActionList();
});
function isJson(str) {
    try {
        JSON.parse(str);
    } catch (e) {
        return false;
    }
    return true;
}
function fnRemoveClass(obj, val, source) {
    $("#lbl" + val + "").removeClass('requied');
}
function initializeDataTableHdr(id, columns) {
    $('#' + id).DataTable({
        dom: 'Bfrtip',
        pageLength: 10,
        "scrollY": "350px",
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
                    columns: columns,
                }
            },
        ]
    });
}
function fnGetAllEsopHdr() {
    $("#Loader").show();
    var webUrl = uri + "/api/Benpos/GetAllEsopHdr";
    $.ajax({
        type: 'GET',
        url: webUrl,
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == false) {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    // alert(msg.Msg);
                    return false;
                }
            }
            else {
                var str = '';
                for (index = 0; index < msg.BenposHeaderList.length; index++) {
                    str += '<tr>';
                    str += '<td>' + msg.BenposHeaderList[index].restrictedCompany.companyName + '</td>';
                    str += '<td>' + msg.BenposHeaderList[index].asOfDate + '</td>';
                    //str += '<td style="display:none">' + msg.BenposHeaderList[index].fileName + '</td>';
                    str += '<td><a class="fa fa-download" target="_blank" href="Benpos/' + msg.BenposHeaderList[index].fileName + '"></a></td>';
                    str += '</tr>';
                }

                var table = $('#tbl-Esop-setup').DataTable();
                table.destroy();
                $("#tbdEsop").html(str);
                initializeDataTableHdr('tbl-Esop-setup', [0, 1,2]);
            }
        },
        error: function (error) {
            $("#Loader").hide();
            alert(error.status + ' ' + error.statusText);
        }
    })
}
function fnSubmitEsopFile() {
    if (fnValidateEsopHdr()) {
        fnSaveThresholdLimitAndByTime();
        fnAddUpdateEsopHdr();
    }
}
function fnValidateEsopHdr() {
    var count = 0;
    if ($("#ddlRestrictedCompanies").val() == undefined || $("#ddlRestrictedCompanies").val() == null || $("#ddlRestrictedCompanies").val().trim() == '' || $("#ddlRestrictedCompanies").val() == '0') {
        count++;
        $('#lblCompany').addClass('requied');
    }
    else {
        $('#lblCompany').removeClass('requied');
    }

    var itemFileESOP = $("#fileUploadImageESOP").get(0).files;
    var arrayExtensions = ["xlsx", "xls"];

    if (itemFileESOP == undefined || itemFileESOP == null || itemFileESOP == '') {
        count++;
        $('#lblUploadESOP').addClass('requied');
    }
    else if ($.inArray(itemFileESOP[0].name.split('.').pop().toLowerCase(), arrayExtensions) == -1) {
        count++;
        alert("Only xlsx or xls format is allowed in Esop Document.");
    }
    else {
        $('#lblUploadESOP').removeClass('requied');
    }

    if (count > 0) {
        return false;
    }
    else {
        return true;
    }
}
function fnAddUpdateEsopHdr() {
    $("#Loader").show();
    var filesData = new FormData();
    var documentESOP = $("#fileUploadImageESOP").get(0).files[0].name;
    var restrictedCompany = $("#ddlRestrictedCompanies").val().trim();
    var corporateaction = $("#ddlCorporateAction option:selected").text();

    filesData.append("Object", JSON.stringify({
        restrictedCompany: { ID: restrictedCompany },
        fileNameESOP: documentESOP, Corporate_Action: corporateaction
    }));

    filesData.append("FilesESOP", $("#fileUploadImageESOP").get(0).files[0]);
    var webUrl = uri + "/api/Benpos/SaveEsopHdr";
    $.ajax({
        type: 'POST',
        url: webUrl,
        data: filesData,
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        contentType: false,
        processData: false,
        success: function (msg) {
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == false) {
                $("#Loader").hide();
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    alert(msg.Msg);
                    window.location.reload(true);
                    $('#btnSave').removeAttr("data-dismiss");
                    $("#fileUploadImageESOP").val('');
                    return false;
                }
            }
            else {

                if (msg.Msg == undefined) {
                    alert(msg);
                }
                else {
                    alert(msg.Msg);
                }
                $("#fileUploadImageESOP").val('');
                $('#EsopModal').modal('hide');
                fnGetAllEsopHdr();
            }
        },
        error: function (error) {
            $("#Loader").hide();
            alert(error.status + ' ' + error.statusText);
        }
    })
}
function fnClearEsopForm() {
    $('#lblCompany').removeClass('requied');
    $('#lblUploadESOP').removeClass('requied');
    $("#fileUploadImageESOP").val('');
}
function getAllRestrictedCompanies() {
    $("#Loader").show();
    var webUrl = uri + "/api/RestrictedCompanies/GetRestrictedCompanies";
    $.ajax({
        type: "POST",
        url: webUrl,
        data: JSON.stringify({
            COMPANY_ID: 0
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: true,
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                var str = "";
                //str += '<option value=""></option>';
                for (var index = 0; index < msg.RestrictedCompaniesList.length; index++) {
                    str += '<option value="' + msg.RestrictedCompaniesList[index].ID + '">' + msg.RestrictedCompaniesList[index].companyName + '</option>';
                }
                $("#ddlRestrictedCompanies").html(str);
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
    });
}
function ConvertToDateTime(dateTime) {
    var date = dateTime.split(" ")[0];
    return (date.split("/")[1] + "/" + date.split("/")[0] + "/" + date.split("/")[2]);
}
function fnSaveThresholdLimitAndByTime() {
    if (fnValidateThresholdByTimeSettings()) {
        var depository = new Array();
        for (var i = 0; i < $("#tbodyNumberOfShares").children().length; i++) {
            var obj = new Object();

            var depositoryId = $($("#tbodyNumberOfShares").children()[i]).children()[0].innerHTML;
            obj.depositoryId = depositoryId;
            var depositoryName = $($("#tbodyNumberOfShares").children()[i]).children()[1].innerHTML;
            obj.depositoryName = depositoryName;
            var numberOfShares = $($($("#tbodyNumberOfShares").children()[i]).children()[2]).children()[0].value;
            obj.sharesCount = numberOfShares;
            //for (var j = 0; j < $("#tbodyThresholdLimit").children().length; j++) {
            //    if ($($("#tbodyThresholdLimit").children()[j]).children()[0].innerHTML === depositoryId) {
            var thresholdLimit = $($($("#tbodyThresholdLimit").children()[0]).children()[2]).children()[0].value;
            obj.thresholdLimit = thresholdLimit;
            var byTime = $($($("#tbodyThresholdLimit").children()[0]).children()[3]).children()[0].value;
            obj.byTime = byTime;

            var limitTypeQty = $("input[id='optionsQty']:checked").val();
            var limitTypeVal = $("input[id='optionsVal']:checked").val();

            if (limitTypeQty != undefined) {
                obj.limitType = "Quantity";
            }
            else if (limitTypeVal != undefined) {
                obj.limitType = "Value";
            }
            //break;
            //    }
            //}
            depository.push(obj);
        }
        $("#Loader").show();
        var webUrl = uri + "/api/UserIT/SaveThresholdLimitAndByTime";
        $.ajax({
            url: webUrl,
            type: "POST",
            data: JSON.stringify(depository),
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            async: false,
            success: function (msg) {
                if (msg.StatusFl == true) {
                    
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
        })
    }
}
function fnValidateThresholdByTimeSettings() {
    var depository = new Array();
    for (var i = 0; i < $("#tbodyNumberOfShares").children().length; i++) {
        var obj = new Object();

        var depositoryId = $($("#tbodyNumberOfShares").children()[i]).children()[0].innerHTML;
        obj.depositoryId = depositoryId;
        var numberOfShares = $($($("#tbodyNumberOfShares").children()[i]).children()[2]).children()[0].value;

        if (numberOfShares.trim() == '' || numberOfShares == null || numberOfShares == undefined) {
            alert("Shares cannot be empty.");
            return false;
        }

        var thresholdLimit = $($($("#tbodyThresholdLimit").children()[0]).children()[2]).children()[0].value;
        if (thresholdLimit.trim() == '' || thresholdLimit == null || thresholdLimit == undefined) {
            alert("Threshold Limit cannot be empty.");
            return false;
        }
        var byTime = $($($("#tbodyThresholdLimit").children()[0]).children()[3]).children()[0].value;
        obj.byTime = byTime;
        if (byTime.trim() == '' || byTime.trim() == '0' || byTime == null || byTime == undefined) {
            alert("Time cannot be empty.");
            return false;
        }

        depository.push(obj);
    }

    var limitTypeQty = $("input[id='optionsQty']:checked").val();
    var limitTypeVal = $("input[id='optionsVal']:checked").val();

    if (limitTypeQty == undefined && limitTypeVal == undefined) {
        alert("Please select Thresold Limit type.");
        return false;
    }

    if (depository.length > 0) {
        var temp = depository[0].byTime;
        for (var i = 1; i < depository.length; i++) {
            if (temp != depository[i].byTime) {
                alert("Time should be same for each deposiotry name.");
                return false;
                break;
            }
        }
    }
    return true;
}
function fnGetThresholdByTimeSettings() {
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/GetThresholdByTimeSettings";
    $.ajax({
        url: webUrl,
        type: "GET",
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            if (msg.StatusFl == true) {
                //$("#frmThresholdSettings").show();
                var str = '';
                var str1 = '';
                var tempArr = new Array();
                for (var i = 0; i < msg.DepositoryList.length; i++) {
                    str += '<tr>';
                    str += '<td style="display:none">';
                    str += msg.DepositoryList[i].depositoryId;
                    str += '</td>';
                    str += '<td style="padding-top: 8px;">';
                    str += msg.DepositoryList[i].depositoryName;
                    str += '</td>';
                    str += '<td style="padding-top: 8px;padding-left:2px;">';
                    str += '<input type="number" class="form-control" value="' + msg.DepositoryList[i].sharesCount + '" />';
                    str += '</td>';
                    str += '</tr>';


                    //str1 += '<tr>';
                    //str1 += '<td style="display:none">';
                    //str1 += msg.DepositoryList[i].depositoryId;
                    //str1 += '</td>';
                    //str1 += '<td style="padding-top: 8px;">';
                    //str1 += msg.DepositoryList[i].depositoryName;
                    //str1 += '</td>';
                    //str1 += '<td style="padding-top: 8px;padding-left:2px;">';
                    //str1 += '<input type="number" value="' + msg.DepositoryList[i].thresholdLimit + '"/>';
                    //str1 += '</td>';
                    //str1 += '<td style="padding-top: 8px;padding-left:2px;">';
                    //str1 += '<select id="byTime_' + msg.DepositoryList[i].depositoryId + '">';
                    //str1 += '<option value="0"></option>';
                    //str1 += '<option value="monthly">Monthly</option>';
                    //str1 += '<option value="quarterly">Quarterly</option>';
                    //str1 += '<option value="bi_Annually">Bi-Annually</option>';
                    //str1 += '<option value="annually">Annually</option>';
                    //str1 += '</select>';
                    //str1 += '</td>';
                    //str1 += '</tr>';
                    var obj = new Object();
                    obj.key = msg.DepositoryList[i].depositoryId
                    obj.value = msg.DepositoryList[i].byTime;
                    tempArr.push(obj);

                    if (msg.DepositoryList[i].limitType == 'Value') {
                        $("#optionsQty").prop("checked", false);
                        $("#optionsVal").prop("checked", true);
                    }
                    else if (msg.DepositoryList[i].limitType == 'Quantity') {
                        $("#optionsQty").prop("checked", true);
                        $("#optionsVal").prop("checked", false);
                    }
                    else {
                        $("#optionsQty").prop("checked", false);
                        $("#optionsVal").prop("checked", false);
                    }
                }
                $("#tbodyNumberOfShares").html(str);


                str1 += '<tr>';
                str1 += '<td style="display:none">';
                str1 += msg.DepositoryList[0].depositoryId;
                str1 += '</td>';
                str1 += '<td style="padding-top: 8px;">CDSL, NSDL & Physical</td>';
                str1 += '<td style="padding-top: 8px;padding-left:2px;">';
                str1 += '<input type="number" value="' + msg.DepositoryList[0].thresholdLimit + '"/>';
                str1 += '</td>';
                str1 += '<td style="padding-top: 8px;padding-left:2px;">';
                str1 += '<select id="byTime_' + msg.DepositoryList[0].depositoryId + '">';
                str1 += '<option value="0"></option>';
                str1 += '<option value="monthly">Monthly</option>';
                str1 += '<option value="quarterly">Quarterly</option>';
                str1 += '<option value="bi_Annually">Bi-Annually</option>';
                str1 += '<option value="annually">Annually</option>';
                str1 += '</select>';
                str1 += '</td>';
                str1 += '</tr>';

                if (msg.DepositoryList[0].limitType == 'Value') {
                    $("#optionsQty").prop("checked", false);
                    $("#optionsVal").prop("checked", true);
                }
                else if (msg.DepositoryList[0].limitType == 'Quantity') {
                    $("#optionsQty").prop("checked", true);
                    $("#optionsVal").prop("checked", false);
                }
                else {
                    $("#optionsQty").prop("checked", false);
                    $("#optionsVal").prop("checked", false);
                }

                $("#tbodyThresholdLimit").html(str1);

                $.each(tempArr, function (index, item) {
                    $("#byTime_" + item.key).val(item.value);
                })

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
    })
}
function fnBindCorporateActionList() {
    $("#Loader").show();
    var webUrl = uri + "/api/Benpos/CorporateActionListById";
    $.ajax({
        type: "POST",
        url: webUrl,
        data: JSON.stringify({ }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            if (msg.StatusFl == true) {
                var result = "";
                for (var i = 0; i < msg.BenposHeaderList.length; i++) {
                    result += '<option value="' + msg.BenposHeaderList[i].id + '">' + msg.BenposHeaderList[i].Corporate_Action + '</option>';
                }
                $("#ddlCorporateAction").html(result);
                $("#Loader").hide();
            }
            else {
                $("#Loader").hide();
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    //alert(msg.Msg);
                    $("#ddlCorporateAction").html('');
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