function fnGoToTrainingModule(VideoId, VideoTitle) {
    var form = $(document.createElement('form'));
    $(form).attr("action", "VideoModule.aspx");
    $(form).attr("method", "POST");
    $(form).css("display", "none");

    var input_video_id = $("<input>")
        .attr("type", "text")
        .attr("name", "VideoId")
        .val(VideoId);
    $(form).append($(input_video_id));

    var input_video_title = $("<input>")
        .attr("type", "text")
        .attr("name", "VideoTitle")
        .val(VideoTitle);
    $(form).append($(input_video_title));

    form.appendTo(document.body);
    $(form).submit();
}