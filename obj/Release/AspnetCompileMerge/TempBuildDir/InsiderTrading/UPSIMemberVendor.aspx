<%@ Page Title="" Language="C#" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" AutoEventWireup="true" CodeBehind="UPSIMemberVendor.aspx.cs" Inherits="ProcsDLL.InsiderTrading.UPSIMemberVendor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
    <link href="../assets/global/plugins/select2/css/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/select2/css/select2-bootstrap.min.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .requied {
            color: red;
        }

        .required-red {
            /*color: red !important;*/
            
            border-color: red !important;
        }
         .required-red-border {
            color: red !important;
            border: 1px solid red;
            border-color: red !important;
        }
         .select2-container--default.select2-container--focus .select2-selection--multiple {
  border: 1px solid red!important;
}

select {
  width: 100%;
}

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="portlet light bordered">
<div class="portlet-title">
            <div class="caption font-red-sunglo">
                <i class="icon-settings font-red-sunglo"></i>
                <span class="caption-subject bold uppercase">Connected Entities</span>
            </div>
        </div>

        <div class="portlet-body">
            <div class="table-toolbar">
                <div class="row">
                    <div class="col-md-6">
                        <div class="btn-group-devided">
                            <button id="btnAddupsi" runat="server" class="btn green">
                                Add New Connected Entities
                            </button>
                            &nbsp;
                        </div>
                    </div>
                </div>
            </div>
            <table class="table table-striped table-hover table-bordered" id="tbl-upsiMemberVendorlist-setup">
                <thead>
                    <tr>
                        <%--<th>SNo</th>--%>
                        <th>Entities Name</th>
                       <th>Status</th>
                        <th>Action</th>
                        <%--<th>Status</th>
                        <th>Report</th>--%>
                    </tr>
                </thead>
                <tbody id="tbdupsiMemberVendorlist">
                </tbody>
            </table>
        </div>




     <div class="modal fade" id="stack1" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-dialog-centered" style="width:50% !important";>
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title caption-subject bold uppercase font-red-sunglo">Add Connected Entities</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnCloseModal();"></button>
                </div>
                <div class="modal-body">
               <div class="portlet light bordered">
                            <div class="portlet-body form">
                                <form class="form-horizontal" runat="server" role="form">
                                    <div class="form-body modal-fixheight">
                                     <div class="form-group">
                                            <label id="lblupsiVendorNM" style="text-align: left" class="col-md-4 control-label">Entities Name<span class="required"> * </span></label>
                                            <div class="col-md-8">
                                                <input id="txtupsiVendorNM" class="form-control form-control-inline" placeholder="Enter Vendor Name" size="16" type="text" value="" autocomplete="off"  onchange="removeRedClass('lblupsiVendorNM', 'txtupsiVendorNM')"/>
                                                <input id="idtstupsi" class="form-control form-control-inline" placeholder="Enter UPSI" size="16" type="text" value="" autocomplete="off" style="display: none" />
                                                <input type="hidden" id="upsiVendorid" value="" />
                                            </div>
                                     </div><br />
                                        <div class="form-group">
                                            <label id="lblupsiVendorPanNo" style="text-align: left" class="col-md-4 control-label">Pan No<span class="required"> * </span></label>
                                            <div class="col-md-8">
                                                <input id="txtupsiVendorPanNo" class="form-control form-control-inline" placeholder="Enter Vendor Name" size="16" type="text" value="" autocomplete="off"  onchange="removeRedClass('lblupsiVendorPanNo', 'txtupsiVendorPanNo')"/>
                                                
                                              
                                            </div>
                                     </div><br />
                                           <div class="form-group">
                                            <label id="labelupsiVendorAddNDA" style="text-align: left" class="col-md-4 control-label">Upload Document</label>
                                            <div class="col-md-4">
                                                <div style="margin-top:11px;"><input type="checkbox" id="uploadNDA" name="uploadNDA" value="Yes"/> <label for="uploadNDA"> Upload File</label><br></div>
                                            </div>
                                                <div class="col-md-4">
                                                <input id="upsiVendorAddNDADoc" class="form-control form-control-inline"  type="file" value=""  /><br />
                                                <div id="downloadFile" style="display:none"></div>
                                            </div>
                                     </div><br />
                                     <div class="form-group">
                                            <label id="lblupsiVendorStatus" style="text-align: left" class="col-md-4 control-label">Status<span class="required"> * </span></label>
                                            <div class="col-md-8">
                                                
                                               <select id="txtupsiVendorStatus" class="form-control form-control-inline"  onchange="removeRedClass('lblupsiVendorStatus', 'txtupsiVendorStatus')">
                                                   <option value="0">Select</option>
                                                   <option value="1">Active</option>
                                                   <option value="2">InActive</option>
                                               </select>
                                            </div>
                                     </div><br />
                                    
                                 </div>
                             </form>
                         </div>
                    <div class="form-actions">
                                <div class="row" style="text-align: center">
                                    <div class="col-md-offset-4 col-md-12">
                                        <button id="btnSave" type="button" class="btn green" onclick="javascript:fnSaveupsi();">Save UPSI</button>
                                        <button id="btnCancel1" type="button" data-dismiss="modal" class="btn default" onclick="javascript:fnCloseModal();">Cancel</button>
                                    </div>
                                </div>
                            </div>

                   </div>
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
        <script src="../assets/pages/scripts/components-select2.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/jquery-validation/js/jquery.validate.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/jquery-validation/js/additional-methods.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/bootstrap-wizard/jquery.bootstrap.wizard.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js" type="text/javascript"></script>
        <script src="../assets/pages/scripts/components-date-time-pickers.min.js" type="text/javascript"></script>
        <script src="js/Global.js?<%=DateTime.Now %>" type="text/javascript"></script>
        <script src="js/UPSI_Vendor.js?<%=DateTime.Now %>" type="text/javascript"></script>
        <script type="text/javascript">
            $("#Loader").hide();
        </script>
    </div>
    
</asp:Content>
