$(document).ready(function () {


    fnListcategory();

});

$("button[id*='btnAddcat']").on("click", function () {
    // alert("hi");
    $("#idcatgory").val(0);
    $("#txtcategoryname").val('');
    $("#stack1").modal('show');
    
});

function isJson(str) {
    try {
        JSON.parse(str);
    } catch (e) {
        return false;
    }
    return true;
}


function fnListcategory() {
    //alert("hi");
    $("#Loader").show();
    var webUrl = uri + "/api/Category/GetCategoryList";
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
            if (msg.StatusFl) {
                var result = "";
                for (var i = 0; i < msg.CategoryList.length; i++) {
                    var seq = i + 1;
                    result += '<tr id="tr_' + msg.CategoryList[i].ID + '">';
                    result += '<td>' + seq + '</td>';
                    result += '<td>' + msg.CategoryList[i].categoryName + '</td>';
                    result += '<td>' + msg.CategoryList[i].createdOn + '</td>';
                    result += '<td>' + msg.CategoryList[i].createdBy + '</td>';
                    
                    result += '<td id="tdEdit_' + msg.CategoryList[i].ID + '"><a  id="a_' + msg.CategoryList[i].ID + '" class="btn btn-outline dark" onclick=\"javascript:EditCat(' + msg.CategoryList[i].ID + ');\">Edit</a>&nbsp;&nbsp;<a  id="a_' + msg.CategoryList[i].ID + '" class="btn btn-outline dark" onclick=\"javascript:Conferm_Delete(' + msg.CategoryList[i].ID + ');\">Delete</a></td>';
                    result += '</tr>';
                }
                var table = $('#tbl-catlist-setup').DataTable();
                table.destroy();
                $("#tbdcatlistlist").html("");
                $("#tbdcatlistlist").html(result);
                initializeDataTable();
                return true;
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

function fnSaveCategory()
{
    var cat_name = $("#txtcategoryname").val();
    if(cat_name==""||cat_name==null)
    {
        alert("Please enter Category Name.");
        return false;

    }

    var cat_id = $("#idcatgory").val();


    var category = new Category(cat_id, cat_name)

    var webUrl = uri + "/api/Category/SaveCategory";
    $("#Loader").show();
    setTimeout(function () {
        $.ajax({
            type: 'POST',
            url: webUrl,
            data: JSON.stringify(category),
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
                    }
                }
                else {
                    $("#Loader").hide();
                    $("#stack1").modal('hide');
                   
                    fnListcategory();
                }


            },
            error: function (error) {
                $("#Loader").hide();

                //   $('#btnSave').removeAttr("data-dismiss");

                if (error.responseText == "Session Expired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                    return false;
                }
                else {
                    alert(error.status + ' ' + error.statusText);
                }
            }
        })
    }, 10);





}

function Category(id,name)
{

    this.ID = id;
    this.categoryName = name;

}


function EditCat(id) {
    $("#Loader").show();
    var webUrl = uri + "/api/Category/EditCategory";
    $.ajax({
        type: "POST",
        url: webUrl,
        data: JSON.stringify({
            ID: id
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: true,
        success: function (msg) {
          
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl) {
                $("#txtcategoryname").val(msg.CategoryList[0].categoryName);
                $("#idcatgory").val(msg.CategoryList[0].ID);
                $("#stack1").modal('show');
                return true;
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
function Conferm_Delete(id) {
    $("#txtDelID").val(id);
    $("#deletecat").show();
}

function Deletecat() {
    var id = $("#txtDelID").val();
    alert(id);

    $("#Loader").show();
    var webUrl = uri + "/api/Category/DeleteCategory";
    $.ajax({
        type: "POST",
        url: webUrl,
        data: JSON.stringify({
            ID: id
        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: true,
        success: function (msg) {
           
            $("#Loader").hide();
            if (isJson(msg)) {
                msg = JSON.parse(msg);
            }
            if (msg.StatusFl) {
                alert(msg.Msg);
                fnListcategory();
                return true;
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
    $("#deletecat").hide();
}

function initializeDataTable() {
    $('#tbl-catlist-setup').DataTable({
        "dom": '<"top"iflp<"clear">>rt<"bottom"iflp<"clear">>',
        dom: 'Bfrtip',
        pageLength: 5,
        buttons: [
         //{
         //    extend: 'pdf',
         //    className: 'btn green btn-outline',
         //    exportOptions: {
         //        columns: [0]
         //    }
         //},
         //{
         //    extend: 'excel',
         //    className: 'btn yellow btn-outline ',
         //    exportOptions: {
         //        columns: [0]
         //    }
         //},
     //    { extend: 'colvis', className: 'btn dark btn-outline', text: 'Columns' }
        ]
    });
}