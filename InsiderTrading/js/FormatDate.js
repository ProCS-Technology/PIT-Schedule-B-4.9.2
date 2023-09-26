var monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
var Mask = ["dd/MM/yyyy", "MM/dd/yyyy", "dd-MMM-yyyy", "yyyymmdd", "yyyy-mm-dd"];
function FormatDate(sDate, sMask) {
    //alert("sDate=" + sDate);
    //alert("sMask=" + sMask);
    if (sDate == null || sDate == "") {
        //alert("Invalid Date");
        return "";
    }
    if (sMask == null || sMask == "") {
        return "";
    }
    var flgMask = false;
    for (var x = 0; x < Mask.length; x++) {
        if (sMask == Mask[x]) {
            flgMask = true;
            break;
        }
    }

    if (!flgMask) {
        alert("Invalid Mask format");
        return;
    }
    if (sDate == "1900-01-01" || sDate == "9999-12-31") {
        return "";
    }
    if (sMask == "dd/MM/yyyy") {
        return sDate.split("-")[2] + '/' + sDate.split("-")[1] + '/' + sDate.split("-")[0];
    }
    else if (sMask == "MM/dd/yyyy") {
        return sDate.split("-")[1] + '/' + sDate.split("-")[2] + '/' + sDate.split("-")[0];
    }
    else if (sMask == "dd-MMM-yyyy") {
        //alert("Here in dd-MMM-yyyy");
        //alert(sDate.split(" -")[2]);
        //alert(sDate.split(" -")[2]);
        //alert(sDate.split(" -")[0]);
        return sDate.split("-")[2] + '-' + monthNames[Number(sDate.split("-")[1])-1] + '-' + sDate.split("-")[0];
    }
    else if (sMask == "dd/MMM/yyyy") {
        return sDate.split("-")[2] + '/' + monthNames[sDate.split("-")[1] - 1] + '/' + sDate.split("-")[0];
    }
    else if (sMask == "yyyymmdd") {
        return sDate.split("-")[2] + sDate.split("-")[1] + sDate.split("-")[0];
    }
    else if (sMask == "yyyy-mm-dd") {
        return sDate.split("-")[0] + "-" + sDate.split("-")[1] + "-" + sDate.split("-")[2];
    }
}
function GetCurrentDt() {
    return new Date().toISOString().slice(0, 10);
}
function convertToDateTime(date) {
    //alert("In function convertToDateTime");
    //alert("date=" + date);

    if ($("input[id*=hdnDateFormat]").val() == "dd-MMM-yyyy") {
        var dy = date.split('-')[0];
        var mn = date.split('-')[1];
        var yr = date.split('-')[2];

        //alert("dy=" + dy);
        //alert("mn=" + mn);
        //alert("yr=" + yr);

        if (mn == "Jan") {
            return yr + "-01-" + dy;
        }
        else if (mn == "Feb") {
            return yr + "-02-" + dy;
        }
        else if (mn == "Mar") {
            return yr + "-03-" + dy;
        }
        else if (mn == "Apr") {
            return yr + "-04-" + dy;
        }
        else if (mn == "May") {
            return yr + "-05-" + dy;
        }
        else if (mn == "Jun") {
            return yr + "-06-" + dy;
        }
        else if (mn == "Jul") {
            return yr + "-07-" + dy;
        }
        else if (mn == "Aug") {
            return yr + "-08-" + dy;
        }
        else if (mn == "Sep") {
            return yr + "-09-" + dy;
        }
        else if (mn == "Oct") {
            return yr + "-10-" + dy;
        }
        else if (mn == "Nov") {
            return yr + "-11-" + dy;
        }
        else if (mn == "Dec") {
            return yr + "-12-" + dy;
        }
    }
    else {
        var dy = date.split('-')[0];
        var mn = date.split('-')[1];
        var yr = date.split('-')[2];
        return yr + "-" + mn + "-" + dy;
    }
    //date = new Date(date)
    //return date.getFullYear() + '-' + (date.getMonth() + 1) + '-' + date.getDate();
}