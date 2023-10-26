<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" CodeBehind="Sub_Category_Master.aspx.cs" Inherits="ProcsDLL.InsiderTrading.Sub_Category_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="../assets/global/css/components.min.css" rel="stylesheet" />
    <link href="../assets/global/css/plugins.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/bootstrap-timepicker/css/bootstrap-timepicker.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/css/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <link href="../assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />
    <!-- BEGIN THEME LAYOUT STYLES -->
    <link href="../assets/layouts/layout/css/layout.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/layouts/layout/css/themes/darkblue.min.css" rel="stylesheet" type="text/css" id="style_color" />
    <link href="../assets/layouts/layout/css/custom.min.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .requied {
            color: red;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="portlet light bordered" style="margin-top: -26px;margin-left: -20px;margin-right: -20px;">
        <div class="portlet-title">
            <div class="caption font-red-sunglo">
                <i class="icon-settings font-red-sunglo"></i>
                <span class="caption-subject bold uppercase">Sub Category</span>
            </div>
        </div>
        <div class="portlet-body">
            <div class="table-toolbar">
                <div class="row">
                    <div class="col-md-6">
                        <div class="btn-group-devided">
                            <button id="btnAddSubCat" class="btn green">
                                Add New Subcategory
                            </button>
                            &nbsp;
                        </div>
                    </div>
                </div>
            </div>
            <table class="table table-striped table-hover table-bordered" id="tbl-subcat-setup">
                <thead>
                    <tr>
                        <th>SNo</th>
                        <th>Category Name</th>
                        <th>Sub Category Name</th>
                        <th>Created On</th>
                        <th>Created By</th>
                        <th>Action</th>
                        <%--<th>Status</th>
                        <th>Report</th>--%>
                    </tr>
                </thead>
                <tbody id="tbdsubcat">
                 
                </tbody>
            </table>
        </div>
 

    <div class="modal fade bs-modal-lg" id="stack1" tabindex="-1" aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnCloseModal();"></button>
                    <h4 class="modal-title caption-subject bold uppercase font-red-sunglo">Add Sub Category</h4>
                </div>
                <div class="modal-body">
                    
                    <div class="portlet light bordered">
                        <div class="portlet-body form">
                            <form class="form-horizontal" runat="server" role="form">
                                <div class="form-body modal-fixheight">
                                    <div class="form-group">
                                        <label id="lblcat" style="text-align: left" class="col-md-4 control-label">Select Category<span class="required"> * </span></label>
                                        <div class="col-md-8">
                                            <select id="selectcat_id" class="form-control form-control-inline">
                                               
                                                </select>
                                            <input id="txtcatid" class="form-control form-control-inline"  size="16" type="text" value="" autocomplete="off"  style="display:none"/>
                                        </div>
                                    </div>
                                    
                                    <br />
                                      <div class="form-group">
                                        <label id="lblRole" style="text-align: left" class="col-md-4 control-label">Sub Category<span class="required"> * </span></label>
                                        <div class="col-md-8">
                                            <input id="txtsubcatname" class="form-control form-control-inline" placeholder="Enter SubCategoy Name" size="16" type="text" value="" autocomplete="off" />
                                            <input id="txtsubcatid" class="form-control form-control-inline"  size="16" type="text" value="" autocomplete="off"  style="display:none"/>
                                        </div>
                                    </div>
                                    
                                    <br />
                                

                                </div>
                                <div class="form-actions">
                                    <div class="row" style="text-align: center">
                                        <div class="col-md-offset-4 col-md-12">
                                            <button id="btnSave" type="button" class="btn green" onclick="javascript:fnSaveSubCat();">Save Sub Category</button>
                                            <button id="btnCancel1" type="button" data-dismiss="modal" class="btn default" onclick="javascript:fnCloseModal_CR_add();">Cancel</button>
                                        </div>
                                    </div>
                                </div>
                        
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
  
     <div id="UPSI_history" class="modal fade" tabindex="-1" data-width="500" style="display: none;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title">Group Composition History</h4>
                </div>
                <div class="modal-body">
                    <div class="portlet-body form">
                        <div class="form-body">
                        <div class="row" style="margin-left:0px;margin-right:1px;"> 
                               <div class="form-group">
                                <label class="col-md-3 control-label"><b>Group</b></label>
                                <div class="col-md-6">
                                    <div id="group_name"></div></div>
                                </div>
                            </div>
                        
                           
                        <div class="row" style="margin-left:1px;margin-right:1px;">
                        <div class="form-group">
                                <label class="col-md-3 control-label"><b>Detail</b></label>
                                <div class="col-md-6">
                                    <div id="txtgroupDetail"></div></div>
                                </div>
                           </div>
                            <br />
                        <div class="row" style="margin-left:1px;margin-right:1px;">
                             <div class="form-group" >
                                <label class="col-md-3 control-label"><b>Valid From</b></label>
                                <div class="col-md-6">
                                    <div id="txtvalid_from"></div></div>
                                </div>
                         </div>
                            <br />
                            <div class="row" style="margin-left:1px;margin-right:1px;">
                             <div class="form-group">
                                <label class="col-md-3 control-label"><b>Valid Till</b></label>
                                <div class="col-md-6">
                                    <div id="txtvalid_till"></div></div>
                                </div>
                           </div>
                            <br />
                            <div class="row" style="margin-left:1px;margin-right:1px;">
                                <h4><b>Group Current Composition</b></h4>
                                <table class="table table-striped" id="adduser_prev">
                                    <thead>
                                        <tr>
                                            <th>SEQUENCE</th>
                                            <th>NAME & EMAIL</th>
                                            <th>ROLE</th>
                                        </tr>
                                    </thead>
                                    <tbody id="tbody_prev"></tbody>
                                </table>
                            </div>
                     </div>
                            <div class="row">                                
                                <div class="modal-footer">                                    
                                    <button id="btnCancel9" type="button" class="btn default" data-target="#UPSI_history" data-toggle="modal" onclick="javascript:CancleHistory_model();">Cancel</button>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>        
   

    <div class="modal fade in" id="deletesubcat" tabindex="-1" role="dialog" aria-hidden="True">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="True"></button>
                    <h4 class="modal-title"><b>Are you sure, you want to delete This SubCategory ?</b></h4>
                </div>
                <div class="modal-footer">
                    <input id="txtDelID" type="hidden" value="0" />
                    <button type="button" class="btn dark btn-outline"  data-toggle="modal" data-target="#deleteLocation">NO</button>
                    <input value="YES" id="btnDeleteConfirm" data-dismiss="modal" class="btn red" onclick="Deletesubcat()" type="submit" />
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade in" id="NoFileExists" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content" style="width: 340px;margin-left: 316px;margin-top: 62px;">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="True"></button>
                    <h4 class="modal-title"><b>No File Exists</b></h4>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn dark btn-outline" data-dismiss="modal">Ok</button>
                </div>
            </div>            
        </div>        
    </div>


      <script src="../assets/editor/jquery-te-1.4.0.min.js"></script>
    <script src="../assets/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js"></script>
    <script src="../assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.js"></script>
    <script src="../assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
    <script src="../assets/global/plugins/bootstrap-timepicker/js/bootstrap-timepicker.min.js"></script>
    <script src="../assets/pages/scripts/components-date-time-pickers.min.js" type="text/javascript"></script>
    <script src="../assets/global/scripts/datatable.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/datatables/datatables.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery-validation/js/jquery.validate.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery-validation/js/additional-methods.min.js" type="text/javascript"></script>
    <script src="../assets/global/scripts/jquery-ui.min.js" type="text/javascript"></script>

    <script src="../assets/global/plugins/js.cookie.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js" type="text/javascript"></script>
    <!-- END CORE PLUGINS -->
    <!-- BEGIN PAGE LEVEL PLUGINS -->
    <script src="../assets/global/plugins/select2/js/select2.full.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery-validation/js/jquery.validate.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery-validation/js/additional-methods.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-wizard/jquery.bootstrap.wizard.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script src="../assets/pages/scripts/components-date-time-pickers.min.js" type="text/javascript"></script>
            <script src="js/Global.js" type="text/javascript"></script>
    <script src="js/SubCategoryMaster.js" type="text/javascript"></script>
    <script type="text/javascript">
        $("#Loader").hide();
    </script>
 

</asp:Content>
