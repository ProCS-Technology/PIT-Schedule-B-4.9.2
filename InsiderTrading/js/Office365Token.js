jQuery(document).ready(function () {
    $('#Loader').hide();
    $('#loadingmessage').modal("hide");
    fnLoad();
});
var randomString = function (length) {
    var str = "";
    var range = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    for (var i = 0; i < length; i++) {
        str += range.charAt(Math.floor(Math.random() * range.length));
    }
    return str;
}
var parseQueryStr = function (queryString) {
    var params = {}, keyvals, temp, i, l;
    keyvals = queryString.split("&");
    for (i = 0, l = keyvals.length; i < l; i++) {
        tmp = keyvals[i].split('=');
        params[tmp[0]] = tmp[1];
    }
    return params;
};
function fnLoad() {
    alert("In function fnLoad");
    //window.opener.document.getElementById('ContentPlaceHolder1_txtRedirectUri').value = window.location.href.split("?")[0];
    var params = parseQueryStr(window.location.search.substring(1));
    if (params['code']) {
        window.opener.document.getElementById('ContentPlaceHolder1_txtCode').value = params['code'];
        window.opener.saveOfficeEmailConfigSmart();
        window.close();
        //tokenClick();
    }
    else if (params['error']) {
        alert('Error requesting auth code: ' + params['error'] + ' / ' + params['error_description']);
    }
    else {
        var appClientId = window.opener.document.getElementById('txtClientIdOffice').value;
        var appRedirectUri = window.opener.document.getElementById('ContentPlaceHolder1_txtRedirectUri').value;
        var tenantId = window.opener.document.getElementById('txtTenantIdOffice').value;
        var appSecret = window.opener.document.getElementById('txtClientSecretOffice').value;

        var appState = document.getElementById('ContentPlaceHolder1_txtState').value;
        var appHashedPkce = document.getElementById('ContentPlaceHolder1_txtPkceHashed').value;
        var appPkce = document.getElementById('ContentPlaceHolder1_txtPkce').value;


        window.opener.document.getElementById('ContentPlaceHolder1_txtState').value = appState;
        window.opener.document.getElementById('ContentPlaceHolder1_txtPkce').value = appPkce;
        window.opener.document.getElementById('ContentPlaceHolder1_txtHashedPkce').value = appHashedPkce;


        var requestUrl = 'https://login.microsoftonline.com/' + tenantId + '/oauth2/v2.0/authorize?' +
            'client_id=' + appClientId +
            '&redirect_uri=' + appRedirectUri +
            '&response_type=code' +
            '&scope=' + encodeURIComponent('openid offline_access https://outlook.office.com/IMAP.AccessAsUser.All https://outlook.office.com/SMTP.Send') +
            '&access_type=offline' +
            '&include_granted_scopes=true' +
            '&state=' + appState +
            '&code_challenge=' + appHashedPkce +
            '&code_challenge_method=S256';
        //alert(requestUrl);
        window.location = requestUrl;
    }
}
function tokenClick() {
    var appClientId = window.opener.document.getElementById('txtClientIdOffice').value;
    var appClientSecret = window.opener.document.getElementById('txtClientSecretOffice').value;
    var appRedirectUri = window.opener.document.getElementById('ContentPlaceHolder1_txtRedirectUri').value;
    var tenantId = window.opener.document.getElementById('txtTenantIdOffice').value;
    var appPkce = window.opener.document.getElementById('ContentPlaceHolder1_txtPkce').value;

    xhttp = new XMLHttpRequest(); // Create an AJAX HTTP request object
    xhttp.onreadystatechange = function () {  // Define a handler, which fires when the request completes
        if (xhttp.readyState == 4) { // If the request state = 4 (completed)...
            if (xhttp.status == 200) { // And the status = 200 (OK), then...
                //alert(xhttp.responseText);
                var authInfo = JSON.parse(xhttp.responseText); // Parse the JSON response into an object
                $("#tokengenrated").html("Token Genrated, Please Continue........");
                window.opener.document.getElementById('ContentPlaceHolder1_txtRToken').value = String(authInfo['refresh_token']); // Retrieve the refresh_token field, and display it
                //SaveToken(authInfo['access_token'], authInfo['expires_in'], authInfo['refresh_token'], authInfo['refresh_token_expires_in']);
                window.opener.document.getElementById('ContentPlaceHolder1_txtToken').value = String(authInfo['access_token']);
                //window.opener.document.getElementById('ContentPlaceHolder1_txtTokenTyp').value = String(authInfo['token_type']);
                window.opener.document.getElementById('ContentPlaceHolder1_txtExpiresIn').value = String(authInfo['expires_in']);
                window.opener.document.getElementById('ContentPlaceHolder1_txtScope').value = String(authInfo['scope']);
                //alert("hdn Email=" + window.opener.document.getElementById('hdnSmartEmail').value);
                //alert("text Email=" + window.opener.document.getElementById('txtSmartEmail').value);
                window.opener.document.getElementById('hdnSmartEmail').value = window.opener.document.getElementById('txtSmartEmail').value;
                var dateStringWithTime = moment(new Date()).format('YYYY-MM-DD HH:mm:ss');
                //alert(dateStringWithTime)
                window.opener.document.getElementById('ContentPlaceHolder1_txtStTime').value = dateStringWithTime;
                window.opener.saveEmailConfigSmart();
                window.close();
            }
            else {
                alert('Error requesting access token: ' + xhttp.statusText)
            }
        }
    }

    xhttp.open('POST', 'https://login.microsoftonline.com/' + tenantId + '/oauth2/v2.0/token', true); // Initialize the HTTP request object for POST to the access token URL
    // Build the HTML form request body
    var body = 'grant_type=authorization_code' +  // This is an OAuth2 Authorization Code request
        '&code=' + encodeURIComponent(window.opener.document.getElementById('ContentPlaceHolder1_txtCode').value) +
        '&redirect_uri=' + encodeURIComponent(appRedirectUri) + // Same custom app Redirect URI
        '&code_verifier=' + encodeURIComponent(appPkce) + // User auth code retrieved previously
        '&client_id=' + encodeURIComponent(appClientId);// + // The custom app Client ID
    //'&client_secret=' + encodeURIComponent(appClientSecret);
    alert(body);
    xhttp.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded'); // Sending the content as URL-encoded form data
    xhttp.setRequestHeader('Access-Control-Allow-Origin', 'http://localhost:55533');
    xhttp.send(body); // Execute the AJAX HTTP request
}