$(document).ready(function () {

    getCategoryMaster();

});


$("#btnAddSubCat").on('click', function () {
    getCategory();
    $("#txtsubcatid").val(0);
    $("#stack1").modal('show');

});

function getCategory()
{

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
                var result = '<option value="0" selected>Please Select Category</option>';

                for (var i = 0; i < msg.CategoryList.length; i++) {
                   
                    result += '<option value="' + msg.CategoryList[i].ID + '">' + msg.CategoryList[i].categoryName + '</option>'

                    
                }
               
                $("#selectcat_id").html("");
                $("#selectcat_id").html(result);
               
                return true;
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
function isJson(str) {
    try {
        JSON.parse(str);
    } catch (e) {
        return false;
    }
    return true;
}

function fnSaveSubCat()
{
    var cat_id = $("#selectcat_id").val();
    if (cat_id==0 || cat_id==null)
    {
        alert("Please Select Category.");
    }
else{

    }
    var sub_cat_name = $("#txtsubcatname").val();

    if (sub_cat_name == '' || cat_id == null) {
        alert("Please Enter  Sub Category Name.");
    }
    else {

    }
    var sub_cat_id = $("#txtsubcatid").val();
    var cat= new Category(cat_id);


    var subcategory = new SubCategory(sub_cat_id, sub_cat_name, cat)


    var webUrl = uri + "/api/SubCategory/SaveSubCategory";
    $("#Loader").show();
    setTimeout(function () {
        $.ajax({
            type: 'POST',
            url: webUrl,
            data: JSON.stringify(subcategory),
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
                        //alert(msg.Msg);
                        //return false;
                    }

                }
                else {

                    $("#Loader").hide();
                    $("#stack1").modal('hide');
                    alert(msg.Msg);
                    window.location.reload();
                    

                   // fnListcategory();
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


function Category (id)
{

    this.ID=id;
}


function SubCategory(sub_cat_id, sub_cat_name, cat)
{

    this.ID = sub_cat_id;
    this.subCategoryName = sub_cat_name;
    this.category = cat;
    
}


function getCategoryMaster() {
    $("#tbdsubcat").html("");
    $("#Loader").show();
    var webUrl = uri + "/api/SubCategory/ListMasterSubCategory";
    $.ajax({
        type: "POST",
        url: webUrl,
        data: JSON.stringify({
            ID: 0
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

                for (var i = 0; i < msg.SubCategoryList.length; i++) {
                    var seq = i + 1;
                  
                    result += '<tr id="tr_' + msg.SubCategoryList[i].ID + '">';
                    result += '<td>' + seq + '</td>';
                    result += '<td>' + msg.SubCategoryList[i].category.categoryName + '</td>';
                    result += '<td>' + msg.SubCategoryList[i].subCategoryName + '</td>';
                    result += '<td>' + msg.SubCategoryList[i].created_on + '</td>';
                    result += '<td>' + msg.SubCategoryList[i].createdBy + '</td>';

                    result += '<td id="tdEdit_' + msg.SubCategoryList[i].ID + '"><a  id="a_' + msg.SubCategoryList[i].ID + '" class="btn btn-outline dark" onclick=\"javascript:EditSubCategoryMaster(' + msg.SubCategoryList[i].ID + ');\">Edit</a>&nbsp;&nbsp;<a  id="a_' + msg.SubCategoryList[i].ID + '" class="btn btn-outline dark" onclick=\"javascript:Conferm_Delete(' + msg.SubCategoryList[i].ID + ');\">Delete</a></td>';
                    result += '</tr>';
                }
                var table = $('#tbl-subcat-setup').DataTable();
                table.destroy();
                $("#tbdsubcat").html("");
                $("#tbdsubcat").html(result);
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

function Conferm_Delete(id) {
    $("#txtDelID").val(id);
    $("#deletesubcat").show();
}


function EditSubCategoryMaster(id) {
   

    $("#Loader").show();
    var webUrl = uri + "/api/SubCategory/EditSubCategory";
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

                getCategory_foredit(msg.SubCategoryList[0].category.ID)

                $("#txtsubcatname").val(msg.SubCategoryList[0].subCategoryName);
                $("#txtsubcatid").val(msg.SubCategoryList[0].ID);

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


function Deletesubcat() {
    var id = $("#txtDelID").val();

    $("#Loader").show();
    var webUrl = uri + "/api/SubCategory/DeleteSubCategory";
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
                getCategoryMaster();
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
    $("#deletesubcat").hide();
}

function getCategory_foredit(id) {

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
                var result = '<option value="0" selected>Please Select Category</option>';

                for (var i = 0; i < msg.CategoryList.length; i++) {

                    if (msg.CategoryList[i].ID == id)
                    {
                        result += '<option value="' + msg.CategoryList[i].ID + '" selected>' + msg.CategoryList[i].categoryName + '</option>'
                    }
                    else
                    {
                        result += '<option value="' + msg.CategoryList[i].ID + '">' + msg.CategoryList[i].categoryName + '</option>'

                    }
                    


                }

                $("#selectcat_id").html("");
                $("#selectcat_id").html(result);

                return true;
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