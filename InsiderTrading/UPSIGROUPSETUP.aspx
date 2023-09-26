<%@ Page Title="" Language="C#" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" AutoEventWireup="true" CodeBehind="UPSIGROUPSETUP.aspx.cs" Inherits="ProcsDLL.InsiderTrading.UPSIGROUPSETUP" %>
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
                <span class="caption-subject bold uppercase">UPSI</span>
            </div>
        </div>

        <div class="portlet-body">
            <div class="table-toolbar">
                <div class="row">
                    <div class="col-md-6">
                        <div class="btn-group-devided">
                            <button id="btnAddupsi" runat="server" class="btn green">
                                Add New UPSI Member
                            </button>
                            &nbsp;
                        </div>
                    </div>
                </div>
            </div>
            <table class="table table-striped table-hover table-bordered" id="tbl-upsilist-setup">
                <thead>
                    <tr>
                        <th>SNo</th>
                        <th>UPSI Group</th>
                        <th>Valid From</th>
                        <th>Valid Till</th>
                        <th>Created by</th>
                        <th>Action</th>
                        <%--<th>Status</th>
                        <th>Report</th>--%>
                    </tr>
                </thead>
                <tbody id="tbdupsilist">
                </tbody>
            </table>
        </div>




     <div class="modal fade" id="stack1" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-dialog-centered" style="width:75% !important";>
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title caption-subject bold uppercase font-red-sunglo">Add Member for UPSI</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnCloseModal();"></button>
                </div>
                <div class="modal-body">
               <div class="portlet light bordered">
                            <div class="portlet-body form">
                                <form class="form-horizontal" runat="server" role="form">
                                    <div class="form-body modal-fixheight">
                                     <div class="form-group">
                                            <label id="lblupsiGroupNM" style="text-align: left" class="col-md-4 control-label">Group Name<span class="required"> * </span></label>
                                            <div class="col-md-8">
                                                <input id="txtupsiGroupNM" class="form-control form-control-inline" placeholder="Enter Group Name" size="16" type="text" value="" autocomplete="off"  onchange="removeRedClass('lblupsiGroupNM', 'txtupsiGroupNM')"/>
                                                <input id="idtstupsi" class="form-control form-control-inline" placeholder="Enter UPSI" size="16" type="text" value="" autocomplete="off" style="display: none" />
                                                <input type="hidden" id="txtupsiGroupID" value="" />
                                                <input type="hidden" id="txtupsiCREATEDBY" value=""  runat="server"/>
                                            </div>
                                     </div><br />
                                       

                                     <div class="form-group">
                                            <label id="lblupsiGroupTyp" style="text-align: left" class="col-md-4 control-label">Group Type<span class="required"> * </span></label>
                                            <div class="col-md-8">
                                                <div class="input-group select2-bootstrap-append" id="colupsiDesignatedT">
                                            <select id="txtupsiGroupType" class="form-control select2" multiple="multiple" onchange="removeRedClass('lblupsiGroupTyp', 'colupsiDesignatedT')"> <option value="1">Sect1</option><option value="2">Sect2</option></select>
                                            <span class="input-group-btn"> 
                                                <button class="btn btn-default" type="button" data-select2-open="lblupsiGroupTyp">
                                                    <span class="glyphicon glyphicon-search"></span>
                                                </button>
                                            </span>
                                        </div>
                                             <%--  <select id="txtupsiGroupType" class="form-control form-control-inline"  onchange="removeRedClass('lblupsiGroupTyp', 'txtupsiGroupType')">
                                                   <option value="0">select</option>
                                                    <option value="Sales &amp; Expense">Sales &amp; Expense</option>
                                                    <option value="Financials">Financials</option>
                                                    <option value="Annual Reports">Annual Reports</option>
                                                    <option value="SOP's">SOP's</option>
                                                    <option value="Employee Remuneration">Employee Remuneration</option>
                                                    <option value="Future Management Decisions">Future Management Decisions</option>
                                                    <option value="Key Projections">Key Projections</option>
                                                    <option value="Buyback or Rights Issue">Buyback or Rights Issue</option>
                                                    <option value="Non Performing Assets (NPA's)">Non Performing Assets (NPA's)</option>
                                                    <option value="Creditors &amp; Debtors">Creditors &amp; Debtors</option>
                                                    <option value="Write off">Write off</option>
                                               </select>--%>
                                            </div>
                                     </div><br />
                                     <div class="form-group">
                                          
                                          <label id="lblvalidaty" style="text-align: left" class="col-md-4 control-label">Validity<span class="required"> * </span></label>
                                            
                                          <div class="col-md-4">
                                                    <input id="fromtxtdate" class="form-control form-control-inline date-picker" placeholder="Enter CR date From" size="16" type="text" value="" autocomplete="off"  onchange="removeRedClass('lblvalidaty', 'fromtxtdate')"/>
                                                </div>
                                           <div class="col-md-4">
                                                    <input id="tilltxtdate" class="form-control form-control-inline date-picker" placeholder="Enter CR date Till" size="16" type="text" value="" autocomplete="off"  onchange="removeRedClass('lblvalidaty', 'tilltxtdate')"/>
                                                </div>
                                     </div><br />
                                     <div class="form-group">
                                          
                                          <label id="lblupsiVersion" style="text-align: left" class="col-md-4 control-label">Version</label>
                                            
                                          <div class="col-md-8">
                                                    <input id="upsiVersion" class="form-control form-control-inlin"  size="16" type="text" value="1" readonly="true" />
                                                </div>
                                          
                                     </div><br />
                                     <div class="form-group">
                                            <label id="lblupsiDescription" style="text-align: left" class="col-md-4 control-label">Description <span class="required"> * </span></label>
                                            <div class="col-md-8">
                                                <textarea rows="4" style="margin: 0px; width: 610px; height: 53px;" id="txtupsiDescription" onchange="removeRedClass('lblupsiDescription', 'txtupsiDescription')"></textarea>
                                            </div>
                                     </div><br />
                                     <div class="form-group">
                                         <label id="lblupsiDesignatedM" style="text-align: left" class="col-md-4 control-label">Designated Members<span class="required"> * </span></label>
                                          <div class="col-md-7">
                                         <div class="input-group select2-bootstrap-append" id="colupsiDesignatedM">
                                            <select id="ddlupsiDesignatedM" class="form-control select2" multiple="multiple" onchange="removeRedClass('lblupsiDesignatedM', 'colupsiDesignatedM')"> <option value="1">Sect1</option><option value="2">Sect2</option></select>
                                            <span class="input-group-btn"> 
                                                <button class="btn btn-default" type="button" data-select2-open="upsiDesignatedM1">
                                                    <span class="glyphicon glyphicon-search"></span>
                                                </button>
                                            </span>
                                        </div>
                                              </div>
                                    </div><br />
                                     <div class="form-group">
                                        
                                         <label id="lblupsiNonDesignatedM" style="text-align: left" class="col-md-4 control-label">Non Designated Members<span class="required"> * </span></label>
                                          <div class="col-md-8">
                                            
                                          </div>
                                     </div>
                                     <div class="form-group">
                                           <div class="col-md-12">
                                            <table class="table" id="OtherMemberDetail">
                                                <thead>
                                                    <tr>
                                                        <th>Name</th>
                                                        <th>Email</th>
                                                        <th>PAN No</th>
                                                        <th>Vendor</th>
                                                    </tr>
                                                </thead>
                                                <tbody id="tbodyOtherMemberDetail"></tbody>
                                            </table>
                                          </div>
                                     </div>
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
        <script src="js/UPSI_MEMBER.js?<%=DateTime.Now %>" type="text/javascript"></script>
        <script type="text/javascript">
            $("#Loader").hide();
        </script>
    </div>
    
</asp:Content>
