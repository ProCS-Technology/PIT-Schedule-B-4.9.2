$(document).ready(function () {

    window.history.forward();
    function preventBack() { window.history.forward(1); }
    $("#Loader").hide();
    // ChartsAmcharts.init();
    ChartsFlotcharts.init();
})

var ChartsFlotcharts = function () {
    var initPieCharts1 = function () {
        var data = [];
        // var series = Math.floor(Math.random() * 10) + 1;
        var series = ["Broker Note Uploaded", "Broker Note Not Uploaded"];
        var value = ["70", "30"];
        series = series < 5 ? 5 : series;

        // for (var i = 0; i < series; i++) {
        for (var i = 0; i < series.length; i++) {
            data[i] = {
                label: series[i],
                //data: Math.floor(Math.random() * 100) + 1
                data: value[i]
            };
        }

        // INTERACTIVE

        if ($('#chart_NonCompliance').size() !== 0) {
            $.plot($("#chart_NonCompliance"), data, {
                series: {
                    pie: {
                        show: true
                    }
                },
                grid: {
                    hoverable: true,
                    clickable: true
                }
            });
            $("#chart_NonCompliance").bind("plothover", pieHover);
            $("#chart_NonCompliance").bind("plotclick", pieClick);
        }

        function pieHover(event, pos, obj) {
            if (!obj)
                return;
            percent = parseFloat(obj.series.percent).toFixed(2);
            $("#hover").html('<span style="font-weight: bold; color: ' + obj.series.color + '">' + obj.series.label + ' (' + percent + '%)</span>');
        }

        function pieClick(event, pos, obj) {
            if (!obj)
                return;
            percent = parseFloat(obj.series.percent).toFixed(2);
            alert('' + obj.series.label + ': ' + percent + '%');
        }

    }

    var initPieCharts2 = function () {
        var data = [];
        // var series = Math.floor(Math.random() * 10) + 1;
        var series = ["Taken", "Not Taken"];
        var value = ["80", "20"];
        series = series < 5 ? 5 : series;

        // for (var i = 0; i < series; i++) {
        for (var i = 0; i < series.length; i++) {
            data[i] = {
                label: series[i],
                // data: Math.floor(Math.random() * 100) + 1
                data: value[i]
            };
        }

        // INTERACTIVE

        if ($('#chart_Difference').size() !== 0) {
            $.plot($("#chart_Difference"), data, {
                series: {
                    pie: {
                        show: true
                    }
                },
                grid: {
                    hoverable: true,
                    clickable: true
                },

            });
            $("#chart_Difference").bind("plothover", pieHover);
            $("#chart_Difference").bind("plotclick", pieClick);
        }

        function pieHover(event, pos, obj) {
            if (!obj)
                return;
            percent = parseFloat(obj.series.percent).toFixed(2);
            $("#hover").html('<span style="font-weight: bold; color: ' + obj.series.color + '">' + obj.series.label + ' (' + percent + '%)</span>');
        }

        function pieClick(event, pos, obj) {
            if (!obj)
                return;
            percent = parseFloat(obj.series.percent).toFixed(2);
            alert('' + obj.series.label + ': ' + percent + '%');
        }

    }
    return {
        //main function to initiate the module
        init: function () {
            initPieCharts1();
            initPieCharts2();
        }

    };
}();