$(document).ready(function () {
    $("#Loader").hide();

    fnlistUPSIVendor()


    $("button[id*='btnAddupsi']").on("click", function () {
        // alert("hi");
        fnAddsUPSI();

    });

   
});

function fnlistUPSIVendor()
{
    $("#Loader").show();
    var webUrl = uri + "/api/UPSIGroupVendor/GetVendorList";

    $.ajax({
        url: webUrl,
        type: "POST",
        async: false,
        data: JSON.stringify({
            VendorId: "0"

        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {

            $("#Loader").hide();
            if (msg.Msg == "SessionExpired") {
                alert("Your session is expired. Please login again to continue");
                window.location.href = "../LogOut.aspx";
            }

            if (msg.StatusFl == true) {
                var result = "";
                for (var i = 0; i < msg.listUPSIVendor.length; i++) {
                    result += '<tr id="tr_' + msg.listUPSIVendor[i].VendorId + '">';
                    //result += '<td>' +() + '</td>';
                    result += '<td>' + msg.listUPSIVendor[i].vendorName + '</td>';
                    if (msg.listUPSIVendor[i].VendorStatus == "1")
                    {
                        result += '<td>Active</td>';
                    }
                    else
                    {
                        result += '<td>InActive</td>';
                    }
                    
                    //result += '<td>' + msg.listUPSIVendor[i].CreatedBy + '</td>';
                    //result += '<td>' + msg.listUPSIVendor[i].CreatedBy + '</td>';
                    result += '<td id="tdEditDelete_' + msg.listUPSIVendor[i].VendorId + '"><a data-target="#stack1" data-toggle="modal" id="a_' + msg.listUPSIVendor[i].VendorId + '" class="btn btn-outline dark" onclick=\"javascript:fnEditVendor(' + msg.listUPSIVendor[i].VendorId + ');\">Edit</a><a style="margin-left:20px" data-target="#delete" data-toggle="modal" id="d_' + msg.listUPSIVendor[i].VendorId + '" class="btn btn-outline dark" onclick=\"javascript:fnDeleteVendor(' + msg.listUPSIVendor[i].VendorId + ');\">Delete</a></td>';

                }

                var table = $('#tbl-upsiMemberVendorlist-setup').DataTable();
                table.destroy();
                $("#tbdupsiMemberVendorlist").html(result);
                initializeDataTable();
                

            }
            else {
                alert(msg.Msg);
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


function fnAddsUPSI() {
   
    //$("#Loader").show();
    $('#txtupsiVendorNM').removeClass('required-red');
    $('#lblupsiVendorNM').removeClass('required');
    $('#txtupsiVendorPanNo').removeClass('required-red');
    $('#lblupsiVendorPanNo').removeClass('required');
    $('#txtupsiVendorStatus').removeClass('required-red');
    $('#lblupsiVendorStatus').removeClass('required');
    $("input[id*='upsiVendorid']").val('0');
    $("#downloadFile").hide();
    $("#stack1").modal('show');


   

 

}






function validation() {


    var textVendorName = $("input[id*='txtupsiVendorNM']").val();
    var textVendorStatus = $("select[id*='txtupsiVendorStatus']").val()
    var textPanno = $("#txtupsiVendorPanNo").val();
   

    var Count = 0;




    if (textVendorName == undefined || textVendorName == "" || textVendorName == null) {
        $('#txtupsiVendorNM').addClass('required-red');
        $('#lblupsiVendorNM').addClass('required');
        Count++
    }
    else {
        $('#txtupsiGroupNM').removeClass('required-red');
        $('#lblupsiGroupNM').removeClass('required');

    }

    if (textPanno == undefined || textPanno == "" || textPanno == null) {
        $('#txtupsiVendorPanNo').addClass('required-red');
        $('#lblupsiVendorPanNo').addClass('required');
        Count++
    }
    else {

        if (!validatePanNO(textPanno)) {

            $('#txtupsiVendorPanNo').addClass('required-red');
            $('#lblupsiVendorPanNo').addClass('required');
            Count++
        }
        else {
            $('#txtupsiVendorPanNo').removeClass('required-red');
            $('#lblupsiVendorPanNo').removeClass('required');
        }
       

    }

    if (textVendorStatus == undefined || textVendorStatus == "" || textVendorStatus == null || textVendorStatus == "0") {
        $('#txtupsiVendorStatus').addClass('required-red');
        $('#lblupsiVendorStatus').addClass('required');
        Count++
    }
    else {
        $('#txtupsiVendorStatus').removeClass('required-red');
        $('#lblupsiVendorStatus').removeClass('required');

    }

    


    if (Count == 0) {
        return true;
    }
    else {
        return false;
    }



}

function validatePanNO(value) {
    var reg = /([A-Z]){5}([0-9]){4}([A-Z]){1}$/;
    if (reg.test(value) == false) {
        return false;
    }
    return true;
}
function removeRedClass(lbl, elememnt) {

    $('#' + lbl).removeClass('required');
    $('#' + elememnt).removeClass('required-red');
    $('#' + elememnt).removeClass('required-red-border');



}


function fnSaveupsi() {
    var status = validation();
   
    var textvendorid = $("input[id*='upsiVendorid']").val();
    var textVendorName = $("input[id*='txtupsiVendorNM']").val();
    var textPanno = $("#txtupsiVendorPanNo").val();
    var uploadStatus = document.getElementById("uploadNDA").checked;
    
    var textVendorStatus = $("select[id*='txtupsiVendorStatus']").val();
    var filesData = new FormData();
    var upsivendorDoc = "";
    if ($("#upsiVendorAddNDADoc").get(0).files[0] !== undefined) {
        upsivendorDoc = $("#upsiVendorAddNDADoc").get(0).files[0].name;
    }
    if (uploadStatus) {
        if ($("#upsiVendorAddNDADoc").get(0).files[0] == undefined) {
            alert("Please select file.");
            return false;
        }
        

    }
    filesData.append("Object", JSON.stringify({
        VendorId: textvendorid,
        vendorName: textVendorName,
        VendorStatus: textVendorStatus,
        fileName: upsivendorDoc,
        PanNo: textPanno

    }));

    if ($("#upsiVendorAddNDADoc").get(0).files[0] !== undefined) {
        filesData.append("Files", $("#upsiVendorAddNDADoc").get(0).files[0]);
    }
    

    if(status==true)
    {
        $("#Loader").show();
        var webUrl = uri + "/api/UPSIGroupVendor/SaveUPSIVendor";

        $.ajax({
            url: webUrl,
            type: "POST",
            async: false,
            data: filesData,
            //contentType: "application/json; charset=utf-8",
           // datatype: "json",
            processData: false,
            contentType: false,
            success: function (msg) {
            
                $("#Loader").hide();
                if (msg.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }

                if (msg.StatusFl==true)
                {
                    $("#stack1").modal('hide');
                    alert(msg.Msg);
                    window.location.reload();

                }
                else
                {
                    alert(msg.Msg);
                }

            },
            error: function(err)
            {
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
    else
    {

        return false;
    }


}

function fnEditVendor(vid)
{
    $("#Loader").show();
    var webUrl = uri + "/api/UPSIGroupVendor/ListUPSIVendor_ById";

    $.ajax({
        url: webUrl,
        type: "POST",
        async: false,
        data: JSON.stringify({
            VendorId: vid

        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {

            $("#Loader").hide();
            if (msg.Msg == "SessionExpired") {
                alert("Your session is expired. Please login again to continue");
                window.location.href = "../LogOut.aspx";
            }

            if (msg.StatusFl == true) {
                $("input[id*='upsiVendorid']").val(msg.listUPSIVendor[0].VendorId);
                $("input[id*='txtupsiVendorNM']").val(msg.listUPSIVendor[0].vendorName);
                $("select[id*='txtupsiVendorStatus']").val(msg.listUPSIVendor[0].VendorStatus).change();
                $("#txtupsiVendorPanNo").val(msg.listUPSIVendor[0].PanNo)
                var str = "";
                if (msg.listUPSIVendor[0].fileName == "") {

                }
                else {
                    str += '<p>';
                    str += '<a href="emailAttachment/' + msg.listUPSIVendor[0].fileName + '" target="_blank">Download</a>'; 
                    str += '</p>';
                    $("#downloadFile").html('');
                    $("#downloadFile").html(str);
                    $("#downloadFile").show();
                }
               
                //$("#stack1").modal('show');
                //return false;

                
            }
            else {
                alert(msg.Msg);
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
function fnDeleteVendor(vid)
{
    $("#Loader").show();
    var webUrl = uri + "/api/UPSIGroupVendor/DeleteUPSIVendor_ById";

    $.ajax({
        url: webUrl,
        type: "POST",
        async: false,
        data: JSON.stringify({
            VendorId: vid

        }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (msg) {

            $("#Loader").hide();
            if (msg.Msg == "SessionExpired") {
                alert("Your session is expired. Please login again to continue");
                window.location.href = "../LogOut.aspx";
            }

            if (msg.StatusFl == true) {

                alert(msg.Msg);
                window.location.reload();
                
                }

              


            
            else {
                alert(msg.Msg);
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


function fnCloseModal() {

    $("input[id*='txtupsiVendorNM']").val('');
    $("select[id*='textVendorStatus']").val('');
    


   // $("#stack1").modal(hide);

}


function initializeDataTable() {
    $('#sample_1_2').DataTable({
        "dom": '<"top"iflp<"clear">>rt<"bottom"iflp<"clear">>',
        dom: 'Bfrtip',
        pageLength: 5,
        buttons: [

        ],
    });
}