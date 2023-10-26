$(document).ready(function () {

  
    $("#Loader").hide();

    viewallphysicalshare();

})


function AddPhysicalShare()
{
    $("input[id*='txtphyshare_id']").val('0');
    $("input[id*='txtphyshare_name']").val('');
    $("input[id*='txtphyshare_issue_date']").val('');
    $("input[id*='txtphyshare_qty']").val('');
    $("#stack1").modal('show');
}


function validate()
{
   
    var name = $("input[id*='txtphyshare_name']").val();
    
    if(name == "" || name==null || name== undefined)
    {

        alert("Please Enter the name");
        return false;
    }

    var issue_date = $("input[id*='txtphyshare_issue_date']").val();
    if (issue_date == "" || issue_date == null || issue_date == undefined) {

        alert("Please Enter the issuedate");
        return false;
    }

   
    var qty = $("input[id*='txtphyshare_qty']").val();
    if (qty == "" || qty == null || qty == undefined) {

        alert("Please Enter the qty");
        return false;
    }


    fnSavePhysical_Share();



}

function fnSavePhysical_Share()
{
    $("#Loader").show();
    var sh_id=  $("input[id*='txtphyshare_id']").val();
   
    var name = $("input[id*='txtphyshare_name']").val();
    var issue_date = $("input[id*='txtphyshare_issue_date']").val();

    var qty = $("input[id*='txtphyshare_qty']").val();
  
    var share_data = new PhysicalShareMaster(sh_id, name, issue_date, qty)

    var url1 = uri + "/api/Physical_share_mastr/save_share";

    $.ajax({
        type: "POST",
        url: url1,
        data: JSON.stringify(share_data),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data)
        {
            $("#Loader").hide();
            if(data.StatusFl)
            {

                alert(data.Msg);
                $("#stack1").modal('hide');
                //  viewallphysicalshare();
                window.location.reload();
            }
            else {
                if (data.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    alert(data.Msg);
                    return false;
                }
            }
            
        },

        error: function (response)
        {
            $("#Loader").hide();
            alert(response.status + ' ' + response.statusText);
        }
    
    });
}

function PhysicalShareMaster(sh_id, name, issue_date2, qty)
{

    this.physical_share_id = sh_id;
    this.name = name;
    this.issue_date = issue_date2;
    this.qty = qty;
}


function viewallphysicalshare()
{
    $("#Loader").show();
    var api_path = uri + "/api/Physical_share_mastr/viewallphysicalshare";
    var share_data = new PhysicalShareMaster(0)
    $.ajax({
        type: "POST",
        url: api_path,
        data: JSON.stringify(share_data),
        contentType: "application/JSON, UTF-8",
        dataType: "JSON",
        async: true,
        success: function (data)
        {
            $("#Loader").hide();
            if (data.StatusFl) {
               
                var str = "";
                for (i = 0; i <= data.PhysicalShareList.length - 1; i++) {
                    str += "<tr>"
                    str += "<td>" + (i+1) + "</td>";
                    str += "<td>" + data.PhysicalShareList[i].name + "</td>";
                    str += "<td>" + data.PhysicalShareList[i].issue_date + "</td>";
                    str += "<td>" + data.PhysicalShareList[i].qty + "</td>";
                    str += "<td>" + data.PhysicalShareList[i].created_by + "</td>";
                    str += "<td><a class='btn btn-outline dark' onclick='javascript:fnEdit(" + data.PhysicalShareList[i].physical_share_id + ")'>EDIT</a>&nbsp;&nbsp;<a class='btn btn-outline dark' onclick='javascript:fndelete(" + data.PhysicalShareList[i].physical_share_id + ")'>Delete</a></td>";
                    str += "</tr>"



                }

                $("#tbdphysical_share_List").html(str);

            }
            else {
                if (data.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    alert(data.Msg);
                    return false;
                }
            }

        },
        error: function (err)
        {
            $("#Loader").hide();
            alert(err.status + ' ' + err.statusText);
        }
    });

}

function fnEdit(id)
{
    $("#Loader").show();
    var api_path = uri + "/api/Physical_share_mastr/editphysicalshare";
    var share_data = new PhysicalShareMaster(id)
    $.ajax({
        type: "POST",
        url: api_path,
        data: JSON.stringify(share_data),
        contentType: "application/JSON, UTF-8",
        dataType: "JSON",
        async: true,
        success: function (data) {
            $("#Loader").hide();
            if (data.StatusFl) {
                $("input[id*='txtphyshare_id']").val(data.PhysicalShareList[0].physical_share_id);
                $("input[id*='txtphyshare_name']").val(data.PhysicalShareList[0].name);
                $("input[id*='txtphyshare_issue_date']").val(data.PhysicalShareList[0].issue_date);
                $("input[id*='txtphyshare_qty']").val(data.PhysicalShareList[0].qty);
                $("#stack1").modal('show');
            }
            else {
                if (data.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    alert(data.Msg);
                    return false;
                }
            }
        },
        error: function (err) {
            $("#Loader").hide();
            alert(err.status + ' ' + err.statusText);
        }
    });

   
}

function fndelete(id) {
    $("#Loader").show();
    var api_path = uri + "/api/Physical_share_mastr/deletephysicalshare";
    var share_data = new PhysicalShareMaster(id)
    $.ajax({
        type: "POST",
        url: api_path,
        data: JSON.stringify(share_data),
        contentType: "application/JSON, UTF-8",
        dataType: "JSON",
        async: true,
        success: function (data) {
            $("#Loader").hide();
            if (data.StatusFl) {
                alert(data.Msg);
                //viewallphysicalshare();
                window.location.reload();
            }
            else {
                if (data.Msg == "SessionExpired") {
                    alert("Your session is expired. Please login again to continue");
                    window.location.href = "../LogOut.aspx";
                }
                else {
                    alert(data.Msg);
                    return false;
                }
            }
        },
        error: function (err) {
            $("#Loader").hide();
            alert(err.status + ' ' + err.statusText);
        }
    });

}

