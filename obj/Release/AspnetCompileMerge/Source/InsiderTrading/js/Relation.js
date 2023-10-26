
jQuery(document).ready(function () {
    fnGetRelationList();
    $('#stack1').on('hide.bs.modal', function () {
    });

});

function initializeDataTable() {
    $('#tbl-Relation-setup').DataTable({
        dom: 'Bfrtip',
        buttons: [
         {
             extend: 'pdf',
             className: 'btn green btn-outline',
             exportOptions: {
                 columns: [0, 1 ,2]
             }
         },
         {
             extend: 'excel',
             className: 'btn yellow btn-outline ',
             exportOptions: {
                 columns: [0, 1 ,2]
             }
         },

         //{ extend: 'colvis', className: 'btn dark btn-outline', text: 'Columns' }
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

function fnGetRelationList() {
    $("#Loader").show();
    var webUrl = uri + "/api/Relation/GetRelationList";
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
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                var result = "";
                for (var i = 0; i < msg.RelationList.length; i++) {
                    result += '<tr id="tr_' + msg.RelationList[i].DEPARTMENT_ID + '">';
                    result += '<td id="tdRelationNm_' + msg.RelationList[i].RELATION_ID + '">' + msg.RelationList[i].RELATION_NM + '</td>';
                    result += '<td id="tdRelationIsm_' + msg.RelationList[i].RELATION_ID + '">' + msg.RelationList[i].IS_MANDATORY + '</td>';
                    result += '<td id="tdRelationOrs_' + msg.RelationList[i].RELATION_ID + '">' + msg.RelationList[i].ORDER_SEQUENCE + '</td>';
                    result += '<td id="tdEditdelete_' + msg.RelationList[i].RELATION_ID + '"><a data-target="#stack1" data-toggle="modal" id="a' + msg.RelationList[i].RELATION_ID + '" class="btn btn-outline dark" onclick=\"javascript:fnEditRelation(\''+ msg.RelationList[i].RELATION_ID + '\',\'' + msg.RelationList[i].RELATION_NM + '\',\'' + msg.RelationList[i].IS_MANDATORY + '\',\'' + msg.RelationList[i].ORDER_SEQUENCE + '\');\">Edit</a><a style="margin-left:20px" data-target="#delete" data-toggle="modal" id="d' + msg.RelationList[i].RELATION_ID + '" class="btn btn-outline dark" onclick=\"javascript:Delete1Relation(' + msg.RelationList[i].RELATION_ID + ') ;\">Delete</a></td>';
                    result += '</tr>';
                }

                var table = $('#tbl-Relation-setup').DataTable();
                table.destroy();
                $("#tbdRelationList").html(result);
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

function fnAddRelation() {
}

function fnSaveRelation() {
    if (fnValidate()) {
        fnAddUpdateRelation();
    }
    else {
        $('#btnSave').removeAttr("data-dismiss");
        return false;
    }
}

function fnAddUpdateRelation() {
    debugger;
    var RelationID = $('#txtRelationKey').val();
    var RelationName = $('#txtRelationName').val();
    var IsMandatory = $('#txtIsMandatory').val();
    var OrderSeq = $('#txtOrderSeq').val();
   
    if ($("#YesIsMandatory").prop("checked")) {
        IsMandatory = "Yes";
    }
    else if ($("#NoIsMandatory").prop("checked")) {
        IsMandatory = "No";
    }
    if (RelationID === "") {
        RelationID = 0;
    }
    $("#Loader").show();
    var webUrl = uri + "/api/Relation/SaveRelation";
    $.ajax({
        type: 'POST',
        url: webUrl,
        data: JSON.stringify({
            RELATION_ID: RelationID, RELATION_NM: RelationName, IS_MANDATORY: IsMandatory, ORDER_SEQUENCE: OrderSeq
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
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
                    $('#btnSave').removeAttr("data-dismiss");
                    return false;
                }
              
            }
            else {
                alert(msg.Msg);
                $("#Loader").hide();
                window.location.reload(true);
            }
        },
        error: function (error) {
            $("#Loader").hide();
            alert(error.status + ' ' + error.statusText);
            $('#btnSave').removeAttr("data-dismiss");
        }
    })
}

function fnValidate() {
    var Relation = $('#txtRelationName').val();
    /*var Relation1 = $('#txtIsMandatory').val();*/
    var Relation1 = "";
    var Relation2 = $('#txtOrderSeq').val();
    if (Relation == '' || Relation == null || Relation == '0') {
        $('#lblRelation').addClass('requied');
        return false;
    }
    else {
        $('#lblRelation').removeClass('requied');
    }
    if ($("#YesIsMandatory").prop("checked")) {
        Relation1 = "Yes";
    }
    else if ($("#NoIsMandatory").prop("checked")) {
        Relation1 = "No";
    }
    if (Relation1 == '' || Relation1 == null || Relation1 == '0') {
        $('#lblIsMandatory').addClass('requied');
        return false;
    }
    else {
        $('#lblIsMandatory').removeClass('requied');
    }
    if (Relation2 == '' || Relation2 == null || Relation2 == '0') {
        $('#lblOrderSeq').addClass('requied');
        return false;
    }
    else {
        $('#lblOrderSeq').removeClass('requied');
    }

    return true;
}

function fnClearForm() {
    $('#txtRelationName').val("");
    $('#txtRelationKey').val("");
    $('#YesIsMandatory').prop('checked', false);
    $('#NoIsMandatory').prop('checked', false);
    $('#txtRelationName').prop("disabled", false);
    $('#txtOrderSeq').val("");
}

function fnEditRelation(Relation_key, Relation_name, Is_Mandatory, Order_Seq) {
    debugger;
    $('#txtRelationName').val(Relation_name);
    $('#txtRelationName').prop("disabled", true);
    $('#txtRelationKey').val(Relation_key);
    if (Is_Mandatory== "No") {
        radioButton = document.getElementById("YesIsMandatory");
        radioButton.checked = false;

        radioButton = document.getElementById("NoIsMandatory");
        radioButton.checked = true;
    }
    else if (Is_Mandatory == "Yes") {
        radioButton = document.getElementById("YesIsMandatory");
        radioButton.checked = true;

        radioButton = document.getElementById("NoIsMandatory");
        radioButton.checked = false;
    }
    /*$('#txtIsMandatory').val(Is_Mandatory);*/
    $('#txtOrderSeq').val(Order_Seq);
}

function Delete1Relation(id) {
    $('input[id*=txtDlKey]').val(id);
    $("#deleteProduct").modal(); 
}

function DeleteRelation() {
    $("#Loader").show();
    var id = $('input[id*=txtDlKey]').val();
    var webUrl = uri + "/api/Relation/DeleteRelation";
    $.ajax({
        type: 'POST',
        url: webUrl,
        data: JSON.stringify({
            Relation_ID: id
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (msg) {
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl == true) {
                alert("Record Deleted successfully !");
                //var table = $('#tbl-Relation-setup').DataTable();
                //table.destroy();
                //$("#tr_" + msg.Relation.RELATION_ID).remove();
                //initializeDataTable();
                window.location.reload(true);
                $("#Loader").hide();
               // fnGetRelationList();
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

function fnCloseModal() {
    fnClearForm();
}
