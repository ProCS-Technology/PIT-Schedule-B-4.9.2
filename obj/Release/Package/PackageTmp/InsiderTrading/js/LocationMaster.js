$(document).ready(function () {


    fnListLocation();

});

$("button[id*='btnAddlocation']").on("click", function () {
    // alert("hi");
    $("#idlocation").val(0);
    $("#txtlocationname").val('');
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


function fnListLocation() {
    //alert("hi");
    $("#Loader").show();
    var webUrl = uri + "/api/Location/GetLocationList";
    $.ajax({
        type: "POST",
        url: webUrl,
        data: JSON.stringify({
            companyId: 0
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
                for (var i = 0; i < msg.LocationList.length; i++) {
                    var seq = i + 1;
                    result += '<tr id="tr_' + msg.LocationList[i].locationId + '">';
                    //result += '<td>' + seq + '</td>';
                    result += '<td>' + msg.LocationList[i].locationName + '</td>';
                    //result += '<td>' + msg.LocationList[i].created_on + '</td>';
                    //result += '<td>' + msg.LocationList[i].created_by + '</td>';

                    result += '<td id="tdEdit_' + msg.LocationList[i].locationId + '"><a  id="a_' + msg.LocationList[i].locationId + '" class="btn btn-outline dark" onclick=\"javascript:EditLocation(' + msg.LocationList[i].locationId + ');\">Edit</a>&nbsp;&nbsp;<a  id="a_' + msg.LocationList[i].locationId + '" class="btn btn-outline dark" onclick=\"javascript:Conferm_Delete(' + msg.LocationList[i].locationId + ');\">Delete</a></td>';
                    result += '</tr>';
                }
                var table = $('#tbl-location-setup').DataTable();
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

function fnSaveLocation() {
    var loca_name = $("#txtlocationname").val();
    if (loca_name == "" || loca_name == null) {
        alert("Please enter Location Name.");
        return false;

    }

    var lo_id = $("#idlocation").val();


    var location_d = new Location(lo_id, loca_name)

    var webUrl = uri + "/api/Location/SaveLocation";
    $("#Loader").show();
    setTimeout(function () {
        $.ajax({
            type: 'POST',
            url: webUrl,
            data: JSON.stringify(location_d),
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
                        return false;
                    }


                }
                else {
                    $("#Loader").hide();
                    $("#stack1").modal('hide');

                    fnListLocation();
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

function Location(id, name) {

    this.locationId = id;
    this.locationName = name;

}

function EditLocation(id) {
    $("#Loader").show();
    var webUrl = uri + "/api/Location/EditLocation";
    $.ajax({
        type: "POST",
        url: webUrl,
        data: JSON.stringify({
            locationId: id
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
                $("#txtlocationname").val(msg.LocationList[0].locationName);
                $("#idlocation").val(msg.LocationList[0].locationId);
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
    $("#deleteLocation").show();
}

function DeleteLocation() {
    var id = $("#txtDelID").val();

    $("#Loader").show();
    var webUrl = uri + "/api/Location/DeleteLocation";
    $.ajax({
        type: "POST",
        url: webUrl,
        data: JSON.stringify({
            locationId: id
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
                window.location.reload(true);
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
    $("#deleteLocation").hide();
}

function initializeDataTable() {
    $('#tbl-location-setup').DataTable({
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








