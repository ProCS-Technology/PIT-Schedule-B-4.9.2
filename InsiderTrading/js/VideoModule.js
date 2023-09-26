$(document).ready(function () {
    fnGetVideo();
})
function fnGetVideo() {
    var videoId = $("#ContentPlaceHolder1_txtVideoId").val();
    var videoTitle = $("#ContentPlaceHolder1_txtVideoTitle").val();
    var sUrl = "";
    
    $("#spnTitle").html(videoTitle);

    if (videoId == "1" || videoTitle == "Training Video for SEBI Prohibition of Insider Trading (PIT) Compliances") {
        sUrl = uri + "/InsiderTrading/video/PIT_Compliance.mp4";
    }
    if (videoId == "2" || videoTitle == "Instructional Video - Submission of The Annual Disclosures by The Designated Persons") {
        sUrl = uri + "/InsiderTrading/video/Declaration_Submission.mp4";
    }
    if (videoId == "3" || videoTitle == "Training Video for SEBI Prohibition of Insider Trading (PIT) Compliances") {
        sUrl = uri + "/InsiderTrading/video/Pre_Clearance.mp4";
    }
    
    var strTable = "";
    strTable += '<video id="videoTemplate" style="width: 100%;" height="700" controls autoplay>';
    strTable += '<source id="sourceVideo" src="' + sUrl + '" type="video/mp4">';
    strTable += 'Your browser does not support the video tag.';
    strTable += '</video>';
    $("#divTemplateAudioVideo").html(strTable);
}