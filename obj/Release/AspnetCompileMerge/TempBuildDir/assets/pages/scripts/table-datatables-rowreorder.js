


var TableDatatablesRowreorder = function () {


    var initTable1 = function (tbl) {
        var oTable = $(tbl).dataTable({
            rowReorder: true,
            paging: false,
            info: false,
            searching: false,
            //ordering: false,                 
            dom: "<'row'<'col-md-6 col-sm-12'l><'col-md-6 col-sm-12'f>r>t<'row'<'col-md-5 col-sm-12'i><'col-md-7 col-sm-12'p>>"
        });
    }    

    //var initTable2 = function () {
    //    //var table = $('#sample_2');
    //    var table = $('#sample_1002');
    //    var oTable = table.dataTable({
    //        rowReorder: true,
    //        paging: false,
    //        info: false,
    //        searching: false,
    //        //ordering: false,
    //        //dom: "<'row'<'col-md-6 col-sm-12'l><'col-md-6 col-sm-12'f>r>t<'row'<'col-md-5 col-sm-12'i><'col-md-7 col-sm-12'p>>"            
    //        dom: "<'row' <'col-md-12'T>><'row'<'col-md-6 col-sm-12'l><'col-md-6 col-sm-12'f>r>t<'row'<'col-md-5 col-sm-12'i><'col-md-7 col-sm-12'p>>"
    //    });
    //}
    //var initTable3 = function () {
    //    var table = $('#sample_3');

    //    var oTable = table.dataTable({
    //        rowReorder: true,
    //        paging: false,
    //        info: false,
    //        searching: false,
    //        //ordering: false,
    //        dom: "<'row'<'col-md-6 col-sm-12'l><'col-md-6 col-sm-12'f>r>t<'row'<'col-md-5 col-sm-12'i><'col-md-7 col-sm-12'p>>"
    //    });
    //}

    return {
        init: function (tbl) {
            if (!jQuery().dataTable) {
                return;
            }
            initTable1(tbl);
            //initTable2();
            //initTable3();
        }
    };
}();

jQuery(document).ready(function () {

    $("table[id*='sample_']").each(function () {        
        TableDatatablesRowreorder.init(this);        
    });
});