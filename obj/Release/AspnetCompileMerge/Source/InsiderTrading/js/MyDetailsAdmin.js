var arrFields = new Array();
$(document).ready(function () {
    window.history.forward();
    function preventBack() { window.history.forward(1); }
    $("#Loader").hide();
    fnBindBusinessUnitList();
    fnBindUserList();
    getLoggedInUserInformation();
    fnGetTransactionalInfo();
    GetFormInfo('User Declaration');
    $("#userLoginId").val('');

    $("#bindBusinessUnit").select2({
        placeholder: "Select a company"
    });

    $("#bindUser").select2({
        placeholder: "Select a user"
    });

    $("#bindBusinessUnit").on('change', function () {
        fnBindUserList();
    });

})

function isJson(str) {
    try {
        JSON.parse(str);
    } catch (e) {
        return false;
    }
    return true;
}

function ConvertToDateTime(dateTime) {
    var date = dateTime.split(" ")[0];

    return (date.split("/")[1] + "/" + date.split("/")[0] + "/" + date.split("/")[2]);
}

function fnBindBusinessUnitList() {
    $("#Loader").show();
    var webUrl = uri + "/api/BusinessUnit/GetAllBusinessUnitList";
    $.ajax({
        type: "GET",
        url: webUrl,
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                var result = "";
                for (var i = 0; i < msg.BusinessUnitList.length; i++) {
                    result += '<option value="' + msg.BusinessUnitList[i].businessUnitId + '">' + msg.BusinessUnitList[i].businessUnitName + '</option>';
                }

                $("#bindBusinessUnit").append(result);
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

function fnBindUserList() {
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/GetUserListByBusinessUnitId";
    $.ajax({
        type: "POST",
        url: webUrl,
        data: JSON.stringify({ businessUnit: { businessUnitId: $("#bindBusinessUnit").val() } }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        //   async: false,
        success: function (msg) {
            if (msg.StatusFl == true) {
                var result = "";
                //result += '<option value="0">All</option>';
                result += '<option value="">Please Select</option>';
                for (var i = 0; i < msg.UserList.length; i++) {
                    result += '<option value="' + msg.UserList[i].LOGIN_ID + '">' + msg.UserList[i].USER_NM + '(' + msg.UserList[i].USER_EMAIL + ')' + '</option>';
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
                    $("#bindUser").html('');
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

function fnGetMyDetailsReport() {
    if (fnValidate()) {
        $("#userLoginId").val($("#bindUser").val());
        $("#ContentPlaceHolder1_txtEmployeeId").val($("#bindUser").val());
        getLoggedInUserInformationByFilter();
        $("#Loader").show();
        var webUrl = uri + "/api/UserIT/GetMyDetailReport";
        $.ajax({
            type: "POST",
            url: webUrl,
            data: JSON.stringify({ LOGIN_ID: $("#bindUser").val() }),
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            success: function (msg) {
                var result = "";
                if (msg.StatusFl == true) {
                    $("#Loader").hide();

                    /* Personal Information */
                    setPersonalInformation(msg);
                    GetFormInfo('User Declaration');
                }
                else {
                    $("select[id*=ddlResidentType]").val('').trigger('change');
                    $("select[id*=country]").val('').trigger('change');
                    $("input[id*=txtPermanentAccountNumber").val('');
                    $("#ContentPlaceHolder1_ddlIdentificationType").val('').trigger('change');
                    $("#ContentPlaceHolder1_txtIdentificationNumber").val('');
                    $("input[id*=txtMobileNumber]").val('');
                    $("#ContentPlaceHolder1_txtAddress").val('');
                    $("input[id*=txtPincodeNumber").val('');
                    $("input[id*=txtDateOfJoining").datepicker({
                        format: "dd/mm/yyyy"
                    }).datepicker("setDate", '');
                    $("input[id*=txtDateOfBecomingInsider").datepicker({
                        format: "dd/mm/yyyy"
                    }).datepicker("setDate", '');
                    $("select[id*=ddlDesignation]").val('').trigger('change');
                    $("select[id*=ddlDepartment]").val('').trigger('change');
                    $("select[id*=ddlLocation]").val('').trigger('change');
                    $("select[id*=ddlCategory]").val('').trigger('change');
                    $("select[id*=ddlSubCategory").val('').trigger('change');
                    $("input[id*=txtDinNumber").val('');
                    $("input[id*=txtInstitution").val('');
                    $("input[id*=txtStream").val('');
                    $("input[id*=txtEmployer").val('');
                    $("input[id*=txtD_ID").val('');

                    $("input[id*=txtEmployeeId]").val('');
                    $("input[id*=txtSsn]").val('');
                    $("#lastModifiedOn").val('');
                    $("#version").val('');
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

function fnValidate() {
    if ($("#bindUser").val() == '' || $("#bindUser").val() == undefined || $("#bindUser").val() == null) {
        alert("Please select the user");
        return false;
    }
    return true;
}

function GetFormInfo(module) {
    $("#Loader").show();
    var webUrl = uri + "/api/Transaction/GetFormDetails?moduleNm=" + module;
    $.ajax({
        url: webUrl,
        type: "GET",
        async: false,
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                for (var x = 0; x < msg.Modules.length; x++) {
                    arrFields.push(msg.Modules[x]);
                }

                for (var x = 0; x < arrFields.length; x++) {
                    for (var y = 0; y < arrFields[x].fields.length; y++) {
                        var obj = arrFields[x].fields[y];
                        if (obj.EditFl == 'N') {
                            if (obj.CntrlType == 'TextBox') {
                                $("input[id*=" + obj.ControlId + "]").attr("disabled", "disabled");
                            }
                            else if (obj.CntrlType == 'Dropdown') {
                                $("select[id*=" + obj.ControlId + "]").attr("disabled", "disabled");
                            }
                        }
                        if (obj.RequiredFl == 'N') {
                            var string = obj.DivNm;
                            string = string.replace(/^.{2}/g, 'spn');
                            $("#" + string).hide();
                        }
                        if (obj.DisplayFl == 'N') {
                            if (obj.RequiredFl == "Y") {
                                if (obj.FormatType == 'Date') {
                                    if (obj.CntrlType == 'TextBox') {
                                        $("input[id*=" + obj.ControlId + "]").val('01/01/1900');
                                    }
                                    else {
                                        $("input[id*=" + obj.ControlId + "]").val($("input[id*=" + obj.ControlId + "] option:first").val());
                                    }
                                }
                                else {
                                    if (obj.CntrlType == 'TextBox') {
                                        $("input[id*=" + obj.ControlId + "]").val('N/A');
                                    }
                                    else {
                                        $("input[id*=" + obj.ControlId + "]").val($("input[id*=" + obj.ControlId + "] option:first").val());
                                    }
                                }
                            }
                            $("#" + obj.DivNm).hide();
                            $("." + obj.DivNm + "1").hide();
                        }
                        else {
                            $("#" + obj.DivNm).show();
                            $("." + obj.DivNm + "1").show();
                        }
                    }
                }

                if (module == 'User Declaration') {
                    if ($("select[id*=ddlResidentType]").val() == 'NRI') {
                        $("select[id*=country]").prop('disabled', false);
                        $("#spnIdentificationType").hide();
                        $("#spnIdentification").hide();
                        residentFlag = false;
                    }
                    else if ($("select[id*=ddlResidentType]").val() == 'INDIAN_RESIDENT') {
                        $("select[id*=country]").prop('disabled', true);
                        $("#spnIdentificationType").hide();
                        $("#spnIdentification").hide();
                        residentFlag = true;
                    }
                    else if ($("select[id*=ddlResidentType]").val() == 'FOREIGN_NATIONAL') {
                        residentFlag = false;
                        $("select[id*=country]").prop('disabled', false);
                        $("#dvEsopOrEquity").show();

                        if ($("input[id*=txtPermanentAccountNumber").val() === "NOT APPLICABLE") {
                            $("#txtHoldsEsopOrEquity").prop("checked", false);
                            $("#txtNotHoldsEsopOrEquity").prop("checked", true);
                            $("#dvApplicationPendingForPan").hide();
                            $("#txtApplicationPendingForPAN").prop("checked", false);
                            $("#txtApplicationNotPendingForPAN").prop("checked", true);
                            $("input[id*=txtPermanentAccountNumber]").prop('disabled', true);
                            $("#spnIdentificationType").show();
                            $("#spnIdentification").show();
                        }
                        else {
                            $("#txtHoldsEsopOrEquity").prop("checked", true);
                            $("#txtNotHoldsEsopOrEquity").prop("checked", false);
                            $("#dvApplicationPendingForPan").show();
                            $("#txtApplicationPendingForPAN").prop("checked", false);
                            $("#txtApplicationNotPendingForPAN").prop("checked", true);
                            $("input[id*=txtPermanentAccountNumber]").prop('disabled', false);
                            $("#spnIdentificationType").hide();
                            $("#spnIdentification").hide();
                        }
                    }

                    if ($("select[id*=ddlCategory] option:selected").text().toUpperCase() == 'DIRECTOR' || $("select[id*=ddlCategory] option:selected").text().toUpperCase() == 'PROMOTER') {
                        for (var x = 0; x < arrFields.length; x++) {
                            for (y = 0; y < arrFields[x].fields.length; y++) {
                                var obj = arrFields[x].fields[y];
                                if (obj.DivNm == "dvDin") {
                                    obj.RequiredFl = 'Y';
                                    $("#spnDin").show();
                                }
                            }
                        }
                    }
                    else {
                        for (var x = 0; x < arrFields.length; x++) {
                            for (y = 0; y < arrFields[x].fields.length; y++) {
                                var obj = arrFields[x].fields[y];
                                if (obj.DivNm == "dvDin") {
                                    obj.RequiredFl = 'N';
                                    $("#spnDin").hide();
                                }
                            }
                        }
                    }
                }
            }
            else {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                }
            }
        },
        error: function (response) {
            $("#Loader").hide();
            alert(response.status + ' ' + response.statusText);
        }
    })
}

function getLoggedInUserInformation() {
    $("#Loader").show();
    var webUrl = uri + "/api/Transaction/GetUserDetails";
    $.ajax({
        url: webUrl,
        type: "GET",
        async: false,
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                $("input[id*=txtUserName]").val(msg.User.USER_NM);
                $("input[id*=txtEmailId]").val(msg.User.USER_EMAIL);
                $("input[id*=txtRole]").val(msg.User.userRole.ROLE_NM);
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

function getLoggedInUserInformationByFilter() {
    $("#Loader").show();
    var webUrl = uri + "/api/Transaction/GetUserDetailsByFilter";
    $.ajax({
        url: webUrl,
        type: "POST",
        async: false,
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        data: JSON.stringify({ LOGIN_ID: $("#bindUser").val() }),
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                $("input[id*=txtUserName]").val(msg.User.USER_NM);
                $("input[id*=txtEmailId]").val(msg.User.USER_EMAIL);
                $("input[id*=txtRole]").val(msg.User.userRole.ROLE_NM);
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

function Category() {
    this.ID = $("select[id*=ddlCategory]").val() == null ? 0 : ($("select[id*=ddlCategory]").val().trim() == "" ? 0 : $("select[id*=ddlCategory]").val());
    this.subCategory = new SubCategory();
}

function SubCategory() {
    this.ID = $("select[id*=ddlSubCategory]").val() == null ? 0 : ($("select[id*=ddlSubCategory]").val().trim() == "" ? 0 : $("select[id*=ddlSubCategory]").val());
}

function fnGetTransactionalInfo() {
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/GetTransactionalInfo";
    $.ajax({
        url: webUrl,
        type: "GET",
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            if (msg.StatusFl == true) {
                $("#Loader").hide();

                /* Personal Information */
                setPersonalInformation(msg);

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

function getAllSubCategories() {
    $("#Loader").show();
    var webUrl = uri + "/api/SubCategory/GetSubCategoryList";
    $.ajax({
        url: webUrl,
        type: "POST",
        data: JSON.stringify(new Category()),
        async: false,
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                var str = "";
                str += '<option value=""></option>';
                for (var index = 0; index < msg.SubCategoryList.length; index++) {
                    str += '<option value="' + msg.SubCategoryList[index].ID + '">' + msg.SubCategoryList[index].subCategoryName + '</option>';
                }
                $("select[id*=ddlSubCategory]").html(str);
            }
            else {
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    $("select[id*=ddlSubCategory]").html('');
                }
            }
        },
        error: function (response) {
            $("#Loader").hide();
            alert(response.status + ' ' + response.statusText);
        }
    })
}

function setPersonalInformation(msg) {
    $("select[id*=ddlResidentType]").val(msg.User.residentType).trigger('change');
    $("select[id*=country]").val(msg.User.country).trigger('change');
    $("input[id*=txtPermanentAccountNumber").val(msg.User.panNumber);
    $("#ContentPlaceHolder1_ddlIdentificationType").val(msg.User.identificationType).trigger('change');;

    $("#ContentPlaceHolder1_txtIdentificationNumber").val(msg.User.identificationNumber);
    $("input[id*=txtMobileNumber]").val(msg.User.USER_MOBILE);
    $("#ContentPlaceHolder1_txtAddress").val(msg.User.address);
    $("input[id*=txtPincodeNumber").val(msg.User.pinCode);
    $("input[id*=txtDateOfJoining").datepicker({
        format: "dd/mm/yyyy"
    }).datepicker("setDate", ConvertToDateTime(msg.User.joiningDate));
    $("input[id*=txtDateOfBecomingInsider").datepicker({
        format: "dd/mm/yyyy"
    }).datepicker("setDate", msg.User.becomingInsiderDate != "" ? ConvertToDateTime(msg.User.becomingInsiderDate) : "");
    $("select[id*=ddlDesignation]").val(msg.User.designation.DESIGNATION_ID).trigger('change');
    $("select[id*=ddlDepartment]").val(msg.User.department.DEPARTMENT_ID).trigger('change');
    $("select[id*=ddlLocation]").val(msg.User.location.locationId).trigger('change');
    $("select[id*=ddlCategory]").val(msg.User.category.ID).trigger('change');
    getAllSubCategories();

    $("select[id*=ddlSubCategory").val(msg.User.category.subCategory.ID).trigger('change');
    $("input[id*=txtDinNumber").val(msg.User.dinNumber);
    $("input[id*=txtInstitution").val(msg.User.institutionName);
    $("input[id*=txtStream").val(msg.User.stream);
    $("input[id*=txtEmployer").val(msg.User.employerDetails);
    $("input[id*=txtD_ID").val(msg.User.D_ID);

    $("input[id*=txtEmployeeId]").val(msg.User.employeeId);
    $("input[id*=txtSsn]").val(msg.User.Ssn);
    $("#lastModifiedOn").val(ConvertToDateTime(msg.User.lastModifiedOn));
    $("#version").val(msg.User.version);
}

function fnGetTransactionalInfoByVersion() {
    $("#Loader").show();
    var webUrl = uri + "/api/UserIT/GetTransactionalInfoByVersionByAdmin";
    $.ajax({
        url: webUrl,
        type: "POST",
        data: JSON.stringify({ LOGIN_ID: $("#userLoginId").val() }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            if (msg.StatusFl == true) {
                $("#Loader").hide();

                /* Personal Information */
                var str = '';
                for (var i = 0; i < msg.UserList.length; i++) {
                    str += '<div style="text-align:center;"><b><u>Version ' + msg.UserList[i].version + '</u></b></div><br/>';
                    str += '<table class="table table-striped table-hover table-bordered">';
                    str += '<tr class="dvUsername1">';
                    str += '<th>' + 'Username' + '</th>';
                    str += '<td>' + $("input[id*=txtUserName]").val() + '</td>';
                    str += '</tr>';
                    str += '<tr class="dvLoginId1">';
                    str += '<th>' + 'Login ID' + '</th>';
                    str += '<td>' + $("input[id*=txtLoginId]").val() + '</td>';
                    str += '</tr>';
                    str += '<tr class="dvCompany1">';
                    str += '<th>' + 'Company' + '</th>';
                    str += '<td>' + $("#ContentPlaceHolder1_txtCompanyName").val() + '</td>';
                    str += '</tr>';
                    str += '<tr class="dvEmail1">';
                    str += '<th>' + 'Email' + '</th>';
                    str += '<td>' + $("input[id*=txtEmailId]").val() + '</td>';
                    str += '</tr>';
                    str += '<tr class="dvRole1">';
                    str += '<th>' + 'Role' + '</th>';
                    str += '<td>' + $("input[id*=txtRole]").val() + '</td>';
                    str += '</tr>';
                    str += '<tr class="dvEmployeeId1">';
                    str += '<th>' + 'Employee No.' + '</th>';
                    str += '<td>' + msg.UserList[i].employeeId + '</td>';
                    str += '</tr>';
                    str += '<tr class="dvResidentType1">';
                    str += '<th>' + 'Resident Type' + '</th>';
                    str += '<td>' + fnGetResidentTypeName(msg.UserList[i].residentType) + '</td>';
                    str += '</tr>';
                    str += '<tr class="dvCountry1">';
                    str += '<th>' + 'Country' + '</th>';
                    str += '<td>' + fnGetCountryName(msg.UserList[i].country) + '</td>';
                    str += '</tr>';
                    str += '<tr class="dvPAN1">';
                    str += '<th>' + 'PAN' + '</th>';
                    str += '<td>' + msg.UserList[i].panNumber + '</td>';
                    str += '</tr>';
                    str += '<tr class="dvSsn1">';
                    str += '<th>' + 'SSN #' + '</th>';
                    str += '<td>' + msg.UserList[i].Ssn + '</td>';
                    str += '</tr>';
                    str += '<tr class="dvIdentificationType1">';
                    str += '<th>' + 'Identification Type' + '</th>';
                    str += '<td>' + fnGetIdentificationTypeName(msg.UserList[i].identificationType) + '</td>';
                    str += '</tr>';
                    str += '<tr class="dvIdentification1">';
                    str += '<th>' + 'Identification #' + '</th>';
                    str += '<td>' + msg.UserList[i].identificationNumber + '</td>';
                    str += '</tr>';
                    str += '<tr class="dvMobile1">';
                    str += '<th>' + 'Mobile #' + '</th>';
                    str += '<td>' + msg.UserList[i].USER_MOBILE + '</td>';
                    str += '</tr>';
                    str += '<tr class="dvAddress1">';
                    str += '<th>' + 'Address' + '</th>';
                    str += '<td>' + msg.UserList[i].address + '</td>';
                    str += '</tr>';
                    str += '<tr class="dvPin1">';
                    str += '<th>' + 'PIN' + '</th>';
                    str += '<td>' + msg.UserList[i].pinCode + '</td>';
                    str += '</tr>';
                    str += '<tr class="dvDoj1">';
                    str += '<th>' + 'Date Of Joining' + '</th>';
                    str += '<td>' + ConvertToDateTime(msg.UserList[i].joiningDate) + '</td>';
                    str += '</tr>';
                    str += '<tr class="dvBeomeInsider1">';
                    str += '<th>' + 'Date Becoming Insider' + '</th>';
                    var dateBecomingInsider = msg.UserList[i].becomingInsiderDate != "" ? ConvertToDateTime(msg.UserList[i].becomingInsiderDate) : "";
                    str += '<td>' + dateBecomingInsider + '</td>';
                    str += '</tr>';
                    str += '<tr class="dvCorpLoc1">';
                    str += '<th>' + 'Location' + '</th>';
                    str += '<td>' + msg.UserList[i].location.locationName + '</td>';
                    str += '</tr>';
                    str += '<tr class="dvCategory1">';
                    str += '<th>' + 'Category' + '</th>';
                    str += '<td>' + msg.UserList[i].category.categoryName + '</td>';
                    str += '</tr>';
                    str += '<tr class="dvSubCategory1">';
                    str += '<th>' + 'Sub Category' + '</th>';
                    str += '<td>' + msg.UserList[i].category.subCategory.subCategoryName + '</td>';
                    str += '</tr>';
                    str += '<tr class="dvDepartment1">';
                    str += '<th>' + 'Department' + '</th>';
                    str += '<td>' + msg.UserList[i].department.DEPARTMENT_NM + '</td>';
                    str += '</tr>';
                    str += '<tr class="dvDesignation1">';
                    str += '<th>' + 'Designation' + '</th>';
                    str += '<td>' + msg.UserList[i].designation.DESIGNATION_NM + '</td>';
                    str += '</tr>';
                    str += '<tr class="dvDin1">';
                    str += '<th>' + 'DIN #' + '</th>';
                    str += '<td>' + msg.UserList[i].dinNumber + '</td>';
                    str += '</tr>';
                    str += '<tr class="dvInstitution1">';
                    str += '<th>' + 'Educational Institution/University of Graduation' + '</th>';
                    str += '<td>' + msg.UserList[i].institutionName + '</td>';
                    str += '</tr>';
                    str += '<tr class="dvStream1">';
                    str += '<th>' + 'Stream of Graduation' + '</th>';
                    str += '<td>' + msg.UserList[i].stream + '</td>';
                    str += '</tr>';
                    str += '<tr class="dvEmployer1">';
                    str += '<th>' + 'Details of the Past Employers' + '</th>';
                    str += '<td>' + msg.UserList[i].employerDetails + '</td>';
                    str += '</tr>';
                    str += '<tr>';
                    str += '<th>' + 'Last Modified' + '</th>';
                    str += '<td>' + ConvertToDateTime(msg.UserList[i].lastModifiedOn) + '</td>';
                    str += '</tr>';
                    str += '</table><br/>';
                }

                $("#divMyDetailsVersion").html(str);
                GetFormInfo('User Declaration');
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

function fnGetCountryName(val) {
    var name = "";
    if (val == "AF") {
        name = "Afghanistan";
    }
    else if (val == "AL") {
        name = "Albania";
    }
    else if (val == "DZ") {
        name = "Algeria";
    }
    else if (val == "AS") {
        name = "American Samoa";
    }
    else if (val == "AD") {
        name = "Andorra";
    }
    else if (val == "AO") {
        name = "Angola";
    }
    else if (val == "AI") {
        name = "Anguilla";
    }
    else if (val == "AR") {
        name = "Argentina";
    }
    else if (val == "AM") {
        name = "Armenia";
    }
    else if (val == "AW") {
        name = "Aruba";
    }
    else if (val == "AU") {
        name = "Australia";
    }
    else if (val == "AT") {
        name = "Austria";
    }
    else if (val == "AZ") {
        name = "Azerbaijan";
    }
    else if (val == "BS") {
        name = "Bahamas";
    }
    else if (val == "BH") {
        name = "Bahrain";
    }
    else if (val == "BD") {
        name = "Bangladesh";
    }
    else if (val == "BB") {
        name = "Barbados";
    }
    else if (val == "BY") {
        name = "Belarus";
    }
    else if (val == "BE") {
        name = "Belgium";
    }
    else if (val == "BZ") {
        name = "Belize";
    }
    else if (val == "BJ") {
        name = "Benin";
    }
    else if (val == "BM") {
        name = "Bermuda";
    }
    else if (val == "BT") {
        name = "Bhutan";
    }
    else if (val == "BO") {
        name = "Bolivia";
    }
    else if (val == "BA") {
        name = "Bosnia and Herzegowina";
    }
    else if (val == "BW") {
        name = "Botswana";
    }
    else if (val == "BV") {
        name = "Bouvet Island";
    }
    else if (val == "BR") {
        name = "Brazil";
    }
    else if (val == "IO") {
        name = "British Indian Ocean Territory";
    }
    else if (val == "BN") {
        name = "Brunei Darussalam";
    }
    else if (val == "BG") {
        name = "Bulgaria";
    }
    else if (val == "BF") {
        name = "Burkina Faso";
    }
    else if (val == "BI") {
        name = "Burundi";
    }
    else if (val == "KH") {
        name = "Cambodia";
    }
    else if (val == "CM") {
        name = "Cameroon";
    }
    else if (val == "CA") {
        name = "Canada";
    }
    else if (val == "CV") {
        name = "Cape Verde";
    }
    else if (val == "KY") {
        name = "Cayman Islands";
    }
    else if (val == "CF") {
        name = "Central African Republic";
    }
    else if (val == "TD") {
        name = "Chad";
    }
    else if (val == "CL") {
        name = "Chile";
    }
    else if (val == "CN") {
        name = "China";
    }
    else if (val == "CX") {
        name = "Christmas Island";
    }
    else if (val == "CC") {
        name = "Cocos (Keeling) Islands";
    }
    else if (val == "CO") {
        name = "Colombia";
    }
    else if (val == "KM") {
        name = "Comoros";
    }
    else if (val == "CG") {
        name = "Congo";
    }
    else if (val == "CD") {
        name = "Congo, the Democratic Republic of the";
    }
    else if (val == "CK") {
        name = "Cook Islands";
    }
    else if (val == "CR") {
        name = "Costa Rica";
    }
    else if (val == "CI") {
        name = "Cote d'Ivoire";
    }
    else if (val == "HR") {
        name = "Croatia (Hrvatska)";
    }
    else if (val == "CU") {
        name = "Cuba";
    }
    else if (val == "CY") {
        name = "Cyprus";
    }
    else if (val == "CZ") {
        name = "Czech Republic";
    }
    else if (val == "DK") {
        name = "Denmark";
    }
    else if (val == "DJ") {
        name = "Djibouti";
    }
    else if (val == "DM") {
        name = "Dominica";
    }
    else if (val == "DO") {
        name = "Dominican Republic";
    }
    else if (val == "EC") {
        name = "Ecuador";
    }
    else if (val == "EG") {
        name = "Egypt";
    }
    else if (val == "SV") {
        name = "El Salvador";
    }
    else if (val == "GQ") {
        name = "Equatorial Guinea";
    }
    else if (val == "GQ") {
        name = "Equatorial Guinea";
    }
    else if (val == "ER") {
        name = "Eritrea";
    }
    else if (val == "EE") {
        name = "Estonia";
    }
    else if (val == "ET") {
        name = "Ethiopia";
    }
    else if (val == "FK") {
        name = "Falkland Islands (Malvinas)";
    }
    else if (val == "FO") {
        name = "Faroe Islands";
    }
    else if (val == "FJ") {
        name = "Fiji";
    }
    else if (val == "FI") {
        name = "Finland";
    }
    else if (val == "FR") {
        name = "France";
    }
    else if (val == "GF") {
        name = "French Guiana";
    }
    else if (val == "PF") {
        name = "French Polynesia";
    }
    else if (val == "TF") {
        name = "French Southern Territories";
    }
    else if (val == "GA") {
        name = "Gabon";
    }
    else if (val == "GM") {
        name = "Gambia";
    }
    else if (val == "GE") {
        name = "Georgia";
    }
    else if (val == "GH") {
        name = "Ghana";
    }
    else if (val == "GI") {
        name = "Gibraltar";
    }
    else if (val == "GR") {
        name = "Greece";
    }
    else if (val == "GL") {
        name = "Greenland";
    }
    else if (val == "GD") {
        name = "Grenada";
    }
    else if (val == "GP") {
        name = "Guadeloupe";
    }
    else if (val == "GU") {
        name = "Guam";
    }
    else if (val == "GT") {
        name = "Guatemala";
    }
    else if (val == "GN") {
        name = "Guinea";
    }
    else if (val == "GW") {
        name = "Guinea-Bissau";
    }
    else if (val == "GY") {
        name = "Guyana";
    }
    else if (val == "HT") {
        name = "Haiti";
    }
    else if (val == "HM") {
        name = "Heard and Mc Donald Islands";
    }
    else if (val == "VA") {
        name = "Holy See (Vatican City State)";
    }
    else if (val == "HN") {
        name = "Honduras";
    }
    else if (val == "HK") {
        name = "Hong Kong";
    }
    else if (val == "HU") {
        name = "Hungary";
    }
    else if (val == "IS") {
        name = "Iceland";
    }
    else if (val == "IN") {
        name = "India";
    }
    else if (val == "ID") {
        name = "Indonesia";
    }
    else if (val == "IR") {
        name = "Iran (Islamic Republic of)";
    }
    else if (val == "IQ") {
        name = "Iraq";
    }
    else if (val == "IE") {
        name = "Ireland";
    }
    else if (val == "IL") {
        name = "Israel";
    }
    else if (val == "IT") {
        name = "Italy";
    }
    else if (val == "JM") {
        name = "Jamaica";
    }
    else if (val == "JP") {
        name = "Japan";
    }
    else if (val == "JO") {
        name = "Jordan";
    }
    else if (val == "KZ") {
        name = "Kazakhstan";
    }
    else if (val == "KE") {
        name = "Kenya";
    }
    else if (val == "KI") {
        name = "Kiribati";
    }
    else if (val == "KP") {
        name = "Korea, Democratic People's Republic of";
    }
    else if (val == "KR") {
        name = "Korea, Republic of";
    }
    else if (val == "KW") {
        name = "Kuwait";
    }
    else if (val == "KG") {
        name = "Kyrgyzstan";
    }
    else if (val == "LA") {
        name = "Lao People's Democratic Republic";
    }
    else if (val == "LV") {
        name = "Latvia";
    }
    else if (val == "LB") {
        name = "Lebanon";
    }
    else if (val == "LS") {
        name = "Lesotho";
    }
    else if (val == "LR") {
        name = "Liberia";
    }
    else if (val == "LY") {
        name = "Libyan Arab Jamahiriya";
    }
    else if (val == "LI") {
        name = "Liechtenstein";
    }
    else if (val == "LT") {
        name = "Lithuania";
    }
    else if (val == "LU") {
        name = "Luxembourg";
    }
    else if (val == "MO") {
        name = "Macau";
    }
    else if (val == "MK") {
        name = "Macedonia, The Former Yugoslav Republic of";
    }
    else if (val == "MG") {
        name = "Madagascar";
    }
    else if (val == "MW") {
        name = "Malawi";
    }
    else if (val == "MY") {
        name = "Malaysia";
    }
    else if (val == "MV") {
        name = "Maldives";
    }
    else if (val == "ML") {
        name = "Mali";
    }
    else if (val == "MT") {
        name = "Malta";
    }
    else if (val == "MH") {
        name = "Marshall Islands";
    }
    else if (val == "MQ") {
        name = "Martinique";
    }
    else if (val == "MR") {
        name = "Mauritania";
    }
    else if (val == "MU") {
        name = "Mauritius";
    }
    else if (val == "YT") {
        name = "Mayotte";
    }
    else if (val == "MX") {
        name = "Mexico";
    }
    else if (val == "FM") {
        name = "Micronesia, Federated States of";
    }
    else if (val == "MD") {
        name = "Moldova, Republic of";
    }
    else if (val == "MC") {
        name = "Monaco";
    }
    else if (val == "MN") {
        name = "Mongolia";
    }
    else if (val == "MS") {
        name = "Montserrat";
    }
    else if (val == "MA") {
        name = "Morocco";
    }
    else if (val == "MZ") {
        name = "Mozambique";
    }
    else if (val == "MM") {
        name = "Myanmar";
    }
    else if (val == "NA") {
        name = "Namibia";
    }
    else if (val == "NR") {
        name = "Nauru";
    }
    else if (val == "NP") {
        name = "Nepal";
    }
    else if (val == "NL") {
        name = "Netherlands";
    }
    else if (val == "AN") {
        name = "Netherlands Antilles";
    }
    else if (val == "NC") {
        name = "New Caledonia";
    }
    else if (val == "NZ") {
        name = "New Zealand";
    }
    else if (val == "NI") {
        name = "Nicaragua";
    }
    else if (val == "NE") {
        name = "Niger";
    }
    else if (val == "NG") {
        name = "Nigeria";
    }
    else if (val == "NU") {
        name = "Niue";
    }
    else if (val == "NF") {
        name = "Norfolk Island";
    }
    else if (val == "MP") {
        name = "Northern Mariana Islands";
    }
    else if (val == "NO") {
        name = "Norway";
    }
    else if (val == "OM") {
        name = "Oman";
    }
    else if (val == "PK") {
        name = "Pakistan";
    }
    else if (val == "PW") {
        name = "Palau";
    }
    else if (val == "PA") {
        name = "Panama";
    }
    else if (val == "PG") {
        name = "Papua New Guinea";
    }
    else if (val == "PY") {
        name = "Paraguay";
    }
    else if (val == "PE") {
        name = "Peru";
    }
    else if (val == "PH") {
        name = "Philippines";
    }
    else if (val == "PN") {
        name = "Pitcairn";
    }
    else if (val == "PL") {
        name = "Poland";
    }
    else if (val == "PT") {
        name = "Portugal";
    }
    else if (val == "PR") {
        name = "Puerto Rico";
    }
    else if (val == "QA") {
        name = "Qatar";
    }
    else if (val == "RE") {
        name = "Reunion";
    }
    else if (val == "RO") {
        name = "Romania";
    }
    else if (val == "RU") {
        name = "Russian Federation";
    }
    else if (val == "RW") {
        name = "Rwanda";
    }
    else if (val == "KN") {
        name = "Saint Kitts and Nevis";
    }
    else if (val == "LC") {
        name = "Saint LUCIA";
    }
    else if (val == "VC") {
        name = "Saint Vincent and the Grenadines";
    }
    else if (val == "WS") {
        name = "Samoa";
    }
    else if (val == "SM") {
        name = "San Marino";
    }
    else if (val == "ST") {
        name = "Sao Tome and Principe";
    }
    else if (val == "SA") {
        name = "Saudi Arabia";
    }
    else if (val == "SN") {
        name = "Senegal";
    }
    else if (val == "SC") {
        name = "Seychelles";
    }
    else if (val == "SL") {
        name = "Sierra Leone";
    }
    else if (val == "SG") {
        name = "Singapore";
    }
    else if (val == "SK") {
        name = "Slovakia (Slovak Republic)";
    }
    else if (val == "SI") {
        name = "Slovenia";
    }
    else if (val == "SB") {
        name = "Solomon Islands";
    }
    else if (val == "SO") {
        name = "Somalia";
    }
    else if (val == "ZA") {
        name = "South Africa";
    }
    else if (val == "GS") {
        name = "South Georgia and the South Sandwich Islands";
    }
    else if (val == "ES") {
        name = "Spain";
    }
    else if (val == "LK") {
        name = "Sri Lanka";
    }
    else if (val == "SH") {
        name = "St. Helena";
    }
    else if (val == "PM") {
        name = "St. Pierre and Miquelon";
    }
    else if (val == "SD") {
        name = "Sudan";
    }
    else if (val == "SR") {
        name = "Suriname";
    }
    else if (val == "SJ") {
        name = "Svalbard and Jan Mayen Islands";
    }
    else if (val == "SZ") {
        name = "Swaziland";
    }
    else if (val == "SE") {
        name = "Sweden";
    }
    else if (val == "CH") {
        name = "Switzerland";
    }
    else if (val == "SY") {
        name = "Syrian Arab Republic";
    }
    else if (val == "TW") {
        name = "Taiwan, Province of China";
    }
    else if (val == "TJ") {
        name = "Tajikistan";
    }
    else if (val == "TZ") {
        name = "Tanzania, United Republic of";
    }
    else if (val == "TH") {
        name = "Thailand";
    }
    else if (val == "TG") {
        name = "Togo";
    }
    else if (val == "TK") {
        name = "Tokelau";
    }
    else if (val == "TO") {
        name = "Tonga";
    }
    else if (val == "TT") {
        name = "Trinidad and Tobago";
    }
    else if (val == "TN") {
        name = "Tunisia";
    }
    else if (val == "TR") {
        name = "Turkey";
    }
    else if (val == "TM") {
        name = "Turkmenistan";
    }
    else if (val == "TC") {
        name = "Turks and Caicos Islands";
    }
    else if (val == "TV") {
        name = "Tuvalu";
    }
    else if (val == "UG") {
        name = "Uganda";
    }
    else if (val == "UA") {
        name = "Ukraine";
    }
    else if (val == "AE") {
        name = "United Arab Emirates";
    }
    else if (val == "GB") {
        name = "United Kingdom";
    }
    else if (val == "US") {
        name = "United States";
    }
    else if (val == "UM") {
        name = "United States Minor Outlying Islands";
    }
    else if (val == "UY") {
        name = "Uruguay";
    }
    else if (val == "UZ") {
        name = "Uzbekistan";
    }
    else if (val == "VU") {
        name = "Vanuatu";
    }
    else if (val == "VE") {
        name = "Venezuela";
    }
    else if (val == "VN") {
        name = "Viet Nam";
    }
    else if (val == "VG") {
        name = "Virgin Islands (British)";
    }
    else if (val == "VI") {
        name = "Virgin Islands (U.S.)";
    }
    else if (val == "WF") {
        name = "Wallis and Futuna Islands";
    }
    else if (val == "EH") {
        name = "Western Sahara";
    }
    else if (val == "YE") {
        name = "Yemen";
    }
    else if (val == "ZM") {
        name = "Zambia";
    }
    else if (val == "ZW") {
        name = "Zimbabwe";
    }

    return name;
}

function fnGetResidentTypeName(val) {
    var name = "";
    if (val == "NRI") {
        name = "NRI";
    }
    else if (val == "INDIAN_RESIDENT") {
        name = "INDIAN RESIDENT";
    }
    else if (val == "FOREIGN_NATIONAL") {
        name = "FOREIGN NATIONAL";
    }
    return name;
}

function fnGetIdentificationTypeName(val) {
    var name = "";
    if (val == "DRIVING_LICENSE") {
        name = "DRIVING LICENSE";
    }
    else if (val == "AADHAR_CARD") {
        name = "AADHAR CARD";
    }
    else if (val == "PASSPORT") {
        name = "PASSPORT";
    }
    return name;
}





