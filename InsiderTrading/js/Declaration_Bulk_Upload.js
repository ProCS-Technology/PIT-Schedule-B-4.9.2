jQuery(document).ready( function () {
    $("#Loader").hide();


});


function Download_Template() {
 
    

    var webUrl = uri + '/api/Declaration_Bulk_Upload/getdownload';
    $.ajax({
        type: 'POST',
        url: webUrl,
        cache: false,
        async: false,
        //data: { ID: id },
        success: function (data) {
            if (data == "Session Expired") {
                sessionexpired();
                return;
            }
            if (data.StatusFl == true) {
                debugger;
                var path_file = data.path;
                //window.open("InsiderTrading/Declaration_Document/Declaration_Document_Download.xlsx", '_blank');
                window.open("Declaration_Document" + path_file, '_blank');
                
                //window.location.href = path_file;
                return;
            }


        },
        error: function (response) {
            alert(response.status + ' ' + response.statusText);
        }
    });

}

function Upload_Template() {
    $("#mdupload_file").modal('show');

}

function close_file_upload() {
    $("#mdupload_file").modal('hide');

}

function Save_Upload_Template() {
    debugger;
    var test = new FormData();

    var fileNames = "";
    var UplodedFileCntrl = $("#fileuploded").get(0);
    var UplodedFile = UplodedFileCntrl.files;

    if (UplodedFile.length > 0) {
        for (var i = 0; i < UplodedFile.length; i++) {
            test.append(UplodedFile[i].name, UplodedFile[i]);


            var extn = $("#fileuploded").val().split(".");
            extn = extn[extn.length - 1].toLowerCase();

            var param1 = new Date();

            var param2 = param1.getTime();
            var param_date = param1.getDate();
            sSaveAs = "Template_Outlet_uploded" + '_' + param_date + '_Product_' + param2 + "." + extn;
            fileNames = sSaveAs;


        }
    }
    test.append('fileNames', fileNames);

    var webUrl = uri + '/api/Declaration_Bulk_Upload/SaveUploadFile';
    $.ajax({
        type: 'POST',
        url: webUrl,
        cache: false,
        data: test,
        contentType: false,
        datatype: "json",
        processData: false,
        async: false,
        success: function (data) {
            debugger;
            if (data == "Session Expired") {
                sessionexpired();
                return;
            }
            if (data.StatusFl == true) {

                alert(data.Msg);
                return;
            }
            else {
                alert(data.Msg);
                return;
            }

        },
        error: function (response) {
            alert(response.status + ' ' + response.statusText);
        }
    });


}