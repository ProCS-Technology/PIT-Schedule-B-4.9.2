$(document).ready(function () {

    window.history.forward();
    function preventBack() { window.history.forward(1); }
    $("#Loader").hide();
   // ChartsAmcharts.init();
    ChartsFlotcharts.init();
})

//var ChartsAmcharts = function () {
//    var initChartSample1 = function () {
//        var chart = AmCharts.makeChart("chart_Users", {
//            "type": "pie",
//            "theme": "light",

//            "fontFamily": 'Open Sans',

//            "color": '#888',

//            "dataProvider": [{
//                "status": "Active",
//                "value": 50
//            }, {
//                "status": "In Active",
//                "value": 50
//            }],
//            "valueField": "value",
//            "titleField": "status",
//            "outlineAlpha": 0.4,
//        //    "depth3D": 15,
//            "balloonText": "[[title]]<br><span style='font-size:14px'><b>[[value]]</b> ([[percents]]%)</span>",
//            "angle": 30,
//            "exportConfig": {
//                menuItems: [{
//                    icon: '/lib/3/images/export.png',
//                    format: 'png'
//                }]
//            }
//        });

//        jQuery('.chart_Users_chart_input').off().on('input change', function () {
//            var property = jQuery(this).data('property');
//            var target = chart;
//            var value = Number(this.value);
//            chart.startDuration = 0;

//            if (property == 'innerRadius') {
//                value += "%";
//            }

//            target[property] = value;
//            chart.validateNow();
//        });

//        $('#chart_Users').closest('.portlet').find('.fullscreen').click(function () {
//            chart.invalidateSize();
//        });
//    }

//    var initChartSample2 = function () {
//        var chart = AmCharts.makeChart("chart_Modifications", {
//            "type": "pie",
//            "theme": "light",

//            "fontFamily": 'Open Sans',

//            "color": '#888',

//            "dataProvider": [{
//                "modifications": "Added",
//                "value": 80
//            }, {
//                "modifications": "Edited",
//                "value": 20
//            }],
//            "valueField": "value",
//            "titleField": "modifications",
//            "outlineAlpha": 0.4,
//          //  "depth3D": 15,
//            "balloonText": "[[title]]<br><span style='font-size:14px'><b>[[value]]</b> ([[percents]]%)</span>",
//            "angle": 30,
//            "exportConfig": {
//                menuItems: [{
//                    icon: '/lib/3/images/export.png',
//                    format: 'png'
//                }]
//            }
//        });

//        jQuery('.chart_Modifications_chart_input').off().on('input change', function () {
//            var property = jQuery(this).data('property');
//            var target = chart;
//            var value = Number(this.value);
//            chart.startDuration = 0;

//            if (property == 'innerRadius') {
//                value += "%";
//            }

//            target[property] = value;
//            chart.validateNow();
//        });

//        $('#chart_Modifications').closest('.portlet').find('.fullscreen').click(function () {
//            chart.invalidateSize();
//        });
//    }

//    return {
//        //main function to initiate the module

//        init: function () {

//            initChartSample1();
//            initChartSample2();
//            //initChartSample3();
//            //initChartSample4();
//            //initChartSample5();
//            //initChartSample6();
//            //initChartSample7();
//            //initChartSample8();
//            //initChartSample9();
//            //initChartSample10();
//            //initChartSample11();
//            //initChartSample12();
//        }

//    };
//}()

var ChartsFlotcharts = function () {
    var initPieCharts1 = function () {
        var data = [];
        // var series = Math.floor(Math.random() * 10) + 1;
        var series = ["Active", "In Active"];
        var value = ["70","30"];
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

        if ($('#chart_Users').size() !== 0) {
            $.plot($("#chart_Users"), data, {
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
            $("#chart_Users").bind("plothover", pieHover);
            $("#chart_Users").bind("plotclick", pieClick);
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
        var series = ["Added", "Edited"];
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

        if ($('#chart_Modifications').size() !== 0) {
            $.plot($("#chart_Modifications"), data, {
                series: {
                    pie: {
                        show: true
                    }
                },
                grid: {
                    hoverable: true,
                    clickable: true
                },
                //tooltip: true,
                //tooltipOpts: {
                //    cssClass: "flotTip",
                //    content: "%p.0%, %s",
                //    shifts: {
                //        x: 20,
                //        y: 0
                //    },
                //    defaultTheme: false
                //}
            });
            $("#chart_Modifications").bind("plothover", pieHover);
            $("#chart_Modifications").bind("plotclick", pieClick);
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